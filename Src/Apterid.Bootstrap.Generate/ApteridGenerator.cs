// Copyright (C) 2015 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Apterid.Bootstrap.Analyze.Abstract;
using Apterid.Bootstrap.Analyze.Abstract.Expressions;
using Apterid.Bootstrap.Common;

namespace Apterid.Bootstrap.Generate
{
    public class ApteridGenerator
    {
        public Context Context { get; }
        public GenerationUnit Unit { get; }
        public Analyze.Abstract.Module Module { get; }

        public ApteridGenerator(Context context, GenerationUnit generationUnit, Analyze.Abstract.Module module)
        {
            Context = context;
            Unit = generationUnit;
            Module = module;
        }

        class GenerateModuleInfo
        {
            public GenerateTypeInfo ModuleTypeInfo;
        }

        struct FieldInitInfo
        {
            public Parse.Syntax.Node Node;
            public FieldBuilder Field;
            public Action<ILGenerator> GenLoad;
        }

        class GenerateTypeInfo
        {
            public Type BaseType;
            public TypeBuilder TypeBuilder;
            public ConstructorBuilder CTor;
            public ConstructorBuilder CCTor;

            public IList<FieldInitInfo> FieldsToInit = new List<FieldInitInfo>();
            public IList<FieldInitInfo> StaticFieldsToInit = new List<FieldInitInfo>();

            public IDictionary<Expression, MemberInfo> Bindings = new Dictionary<Expression, MemberInfo>();
        }

        GenerateModuleInfo moduleInfo;
        CancellationToken cancel;

        public void Generate(CancellationToken cancel)
        {
            this.cancel = cancel;
            moduleInfo = new GenerateModuleInfo
            {
                ModuleTypeInfo = DefineModuleType()
            };

            // types and bindings in module
            foreach (var type in Module.Types.Values)
            {
                if (cancel.IsCancellationRequested) throw new OperationCanceledException(cancel);

                GenerateType(moduleInfo.ModuleTypeInfo, type);
            }

            foreach (var binding in Module.Bindings.Values)
            {
                if (cancel.IsCancellationRequested) throw new OperationCanceledException(cancel);

                GenerateBinding(moduleInfo.ModuleTypeInfo, binding);
            }

            GenerateTypeCtors(moduleInfo.ModuleTypeInfo);
            moduleInfo.ModuleTypeInfo.TypeBuilder.CreateType();
        }

        GenerateTypeInfo DefineModuleType()
        {
            var baseType = typeof(Apterid.Module);

            TypeAttributes atts = TypeAttributes.Sealed;
            atts |= Module.IsPublic ? TypeAttributes.Public : TypeAttributes.NotPublic;

            var mtb = Unit.ModuleBuilder.DefineType(Module.Name.FullName, atts, baseType);

            return new GenerateTypeInfo
            {
                BaseType = baseType,
                TypeBuilder = mtb,
                CTor = mtb.DefineConstructor(MethodAttributes.Private | MethodAttributes.HideBySig, CallingConventions.Standard, Type.EmptyTypes),
                CCTor = mtb.DefineConstructor(MethodAttributes.Private | MethodAttributes.Static | MethodAttributes.HideBySig, CallingConventions.Standard, Type.EmptyTypes)
            };
        }

        void GenerateTypeCtors(GenerateTypeInfo typeInfo)
        {
            var baseType = typeInfo.BaseType;
            var tb = typeInfo.TypeBuilder;

            var ctor = typeInfo.CTor;
            {
                var il = ctor.GetILGenerator();

                var mn = Module.SyntaxNode as Parse.Syntax.Module;
                if (mn != null) MarkSequencePoint(il, mn.Name, mn.Name);

                il.Emit(OpCodes.Nop);
                il.Emit(OpCodes.Ldarg_0);
                var mc = baseType.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null);
                il.Emit(OpCodes.Call, mc); // leave this on the stack

                foreach (var fi in typeInfo.FieldsToInit)
                {
                }

                il.Emit(OpCodes.Ret);
            }

            var cctor = typeInfo.CCTor;
            {
                var il = cctor.GetILGenerator();

                var mn = Module.SyntaxNode as Parse.Syntax.Module;
                if (mn != null) MarkSequencePoint(il, mn.Children.First(), mn.Children.First());

                il.Emit(OpCodes.Nop);

                foreach (var fi in typeInfo.StaticFieldsToInit)
                {
                    MarkSequencePoint(il, fi.Node, fi.Node);
                    fi.GenLoad(il);
                    il.Emit(OpCodes.Stsfld, fi.Field);
                }

                il.Emit(OpCodes.Ret);
            }
        }

        void GenerateType(GenerateTypeInfo info, AType type)
        {
        }

        void GenerateBinding(GenerateTypeInfo typeInfo, Binding binding)
        {
            if (binding.Expression != null)
            {
                Literal literal;

                // literal -> static field
                if ((literal = binding.Expression as Literal) != null)
                {
                    GenerateLiteralBinding(typeInfo, binding, literal);
                }
            }
        }

        void GenerateField(TypeBuilder tb, Expression expression)
        {
        }

        void GenerateLiteralBinding(GenerateTypeInfo typeInfo, Binding binding, Literal literal)
        {
            if (literal.ResolvedType == null)
            {
                Unit.AddError(new GeneratorError
                {
                    Message = string.Format(ErrorMessages.E_0017_Generator_UnresolvedType, ApteridError.Truncate(literal.SyntaxNode.Text)),
                    ErrorNode = literal.SyntaxNode
                });
                return;
            }

            var atts = FieldAttributes.Static | FieldAttributes.InitOnly;
            atts |= binding.IsPublic ? FieldAttributes.Public : FieldAttributes.Private;
            var field = typeInfo.TypeBuilder.DefineField(binding.Name.Name, literal.ResolvedType.CLRType, atts);
            binding.GeneratedMemberInfo = field;

            typeInfo.Bindings.Add(literal, field);

            if (literal.Value == null)
            {
                if (field.FieldType.IsValueType)
                    field.SetConstant(Activator.CreateInstance(field.FieldType));
                else
                    field.SetConstant(null);
            }
            else
            {
                typeInfo.StaticFieldsToInit.Add(new FieldInitInfo
                {
                    Node = binding.SyntaxNode,
                    Field = field,
                    GenLoad = il => EmitLoadLiteral(il, literal, field.FieldType)
                });
            }
        }

        void EmitLoadLiteral(ILGenerator il, Literal literal, Type tgtType)
        {
            MarkSequencePoint(il, literal.SyntaxNode, literal.SyntaxNode);

            Literal<bool> boolLiteral;
            IntegerLiteral intLiteral;
            if ((intLiteral = literal as IntegerLiteral) != null)
            {
                EmitLoadIntegerLiteral(il, intLiteral, tgtType);
            }
            else if ((boolLiteral = literal as Literal<bool>) != null)
            {
                if (boolLiteral.TypedValue == true)
                    il.Emit(OpCodes.Ldc_I4_1);
                else
                    il.Emit(OpCodes.Ldc_I4_0);
            }
            else
            {
                Unit.AddError<GeneratorError>("Don't know how to generate literal of type " + literal.GetType().FullName);
            }
        }

        void EmitLoadIntegerLiteral(ILGenerator il, IntegerLiteral literal, Type tgtType)
        {
            var bigval = literal.IntValue;

            switch (Type.GetTypeCode(tgtType))
            {
                case TypeCode.Byte:   il.Emit(OpCodes.Ldc_I4,   (byte)bigval); break;
                case TypeCode.Int16:  il.Emit(OpCodes.Ldc_I4,  (short)bigval); break;
                case TypeCode.Int32:  il.Emit(OpCodes.Ldc_I4,    (int)bigval); break;
                case TypeCode.Int64:  il.Emit(OpCodes.Ldc_I8,   (long)bigval); break;
                case TypeCode.SByte:  il.Emit(OpCodes.Ldc_I4,  (sbyte)bigval); break;
                case TypeCode.UInt16: il.Emit(OpCodes.Ldc_I4, (ushort)bigval); break;
                case TypeCode.UInt32: il.Emit(OpCodes.Ldc_I4,   (uint)bigval); break;
                case TypeCode.UInt64: il.Emit(OpCodes.Ldc_I8,  (ulong)bigval); break;
                case TypeCode.Char:   il.Emit(OpCodes.Ldc_I4,   (char)bigval); break;

                case TypeCode.Boolean:
                case TypeCode.DateTime:
                case TypeCode.DBNull:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Empty:
                case TypeCode.String:
                case TypeCode.Object:
                    throw new Exception(string.Format(ErrorMessages.E_0018_Generator_InvalidNumericLiteral, bigval, tgtType.Name));
                default:
                    break;
            }
        }

        void MarkSequencePoint(ILGenerator il, Parse.Syntax.Node start, Parse.Syntax.Node end)
        {
            var sf = start.SourceFile;
            ISymbolDocumentWriter doc;
            if (Unit.SymbolDocs.TryGetValue(sf.Name, out doc))
            {
                int startLine, startColumn, endLine, endColumn;
                sf.MatchState.GetLine(start.StartIndex, out startLine, out startColumn);
                sf.MatchState.GetLine(end.NextIndex, out endLine, out endColumn);

                il.MarkSequencePoint(doc, startLine, startColumn, endLine, endColumn);
            }
        }
    }

    public class GeneratorError : Parse.NodeError
    {
    }
}
