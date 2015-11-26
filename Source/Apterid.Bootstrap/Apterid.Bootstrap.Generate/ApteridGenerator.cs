using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Apterid.Bootstrap.Common;

namespace Apterid.Bootstrap.Generate
{
    public class ApteridGenerator
    {
        public Context Context { get; }
        public GenerationUnit Unit { get; }
        public Analyze.Module Module { get; }

        public ApteridGenerator(Context context, GenerationUnit generationUnit, Analyze.Module module)
        {
            Context = context;
            Unit = generationUnit;
            Module = module;
        }

        CancellationToken cancel;

        public void Generate(CancellationToken cancel)
        {
            this.cancel = cancel;

            TypeAttributes atts = TypeAttributes.Sealed;
            atts |= Module.IsPublic ? TypeAttributes.Public : TypeAttributes.NotPublic;

            var mtb = Unit.ModuleBuilder.DefineType(Module.Name.FullName, atts, typeof(Apterid.Module));

            foreach (var type in Module.Types.Values)
            {
                if (cancel.IsCancellationRequested) throw new OperationCanceledException(cancel);

                GenerateType(mtb, type);
            }

            foreach (var binding in Module.Bindings.Values)
            {
                if (cancel.IsCancellationRequested) throw new OperationCanceledException(cancel);

                GenerateBinding(mtb, binding);
            }

            mtb.CreateType();
        }

        void GenerateType(TypeBuilder tb, Analyze.Type type)
        {
        }

        void GenerateBinding(TypeBuilder tb, Analyze.Binding binding)
        {
            if (binding.Expression != null)
            {
                Analyze.Expressions.Literal literal;

                if ((literal = binding.Expression as Analyze.Expressions.Literal) != null)
                {
                    GenerateLiteral(tb, binding, literal);
                }
            }
        }

        void GenerateField(TypeBuilder tb, Analyze.Expression expression)
        {
        }

        void GenerateLiteral(TypeBuilder tb, Analyze.Binding binding, Analyze.Expressions.Literal literal)
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
            var field = tb.DefineField(binding.Name.Name, literal.ResolvedType.CLRType, atts);

            if (literal.Value == null)
            {
                if (field.FieldType.IsValueType)
                    field.SetConstant(Activator.CreateInstance(field.FieldType));
                else
                    field.SetConstant(null);
            }
            else
            {
                field.SetConstant(ConvertLiteral(literal.Value, field.FieldType));
            }
        }

        object ConvertLiteral(object value, Type tgtType)
        {
            var srcType = value.GetType();

            if (srcType == tgtType)
                return value;

            if (srcType == typeof(System.Numerics.BigInteger))
            {
                var bigval = (System.Numerics.BigInteger)value;
                switch (Type.GetTypeCode(tgtType))
                {
                    case TypeCode.Byte:
                        return (byte)bigval;
                    case TypeCode.Int16:
                        return (short)bigval;
                    case TypeCode.Int32:
                        return (int)bigval;
                    case TypeCode.Int64:
                        return (long)bigval;
                    case TypeCode.SByte:
                        return (sbyte)bigval;
                    case TypeCode.UInt16:
                        return (ushort)bigval;
                    case TypeCode.UInt32:
                        return (uint)bigval;
                    case TypeCode.UInt64:
                        return (ulong)bigval;

                    case TypeCode.Boolean:
                    case TypeCode.Char:
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

            return Convert.ChangeType(value, tgtType);
        }
    }

    public class GeneratorError : Parse.NodeError
    {
    }
}
