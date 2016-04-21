// Copyright (C) 2016 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Apterid.Bootstrap.Common;
using Verophyle.CSLogic;

namespace Apterid.Bootstrap.Analyze.Abstract.Expressions
{
    public abstract class Literal : Expression
    {
        public abstract object Value { get; }

        public Literal(Parse.Syntax.Node syntaxNode)
            : base(syntaxNode)
        {
        }
    }

    public class Literal<T> : Literal
    {
        public T TypedValue { get; internal set; }
        public override object Value { get { return TypedValue; } }

        Lazy<QualifiedName> TypeName = new Lazy<QualifiedName>(() => new QualifiedName(typeof(T).FullName));

        public Literal(T value, Parse.Syntax.Node syntaxNode)
            : base(syntaxNode)
        {
            TypedValue = value;
        }

        public override Goal<AType> ResolveType(AnalysisUnit unit, TypeResolver tr, Var type)
        {
            var t = tr.ResolveType(TypeName.Value);
            if (t == null)
            {
                unit.AddError(new AnalyzerError(SyntaxNode, string.Format(ErrorMessages.E_0020_Analyzer_UnableToResolveType, TypeName)));
                return _ => Enumerable.Empty<State<AType>>(); // fail
            }

            return Goal.Unify(type, t);
        }
    }

    public abstract class NumericLiteral : Literal
    {
        public NumericLiteral(Parse.Syntax.Node syntaxNode)
            : base(syntaxNode)
        {
        }
    }

    public class IntegerLiteral : NumericLiteral
    {
        static readonly QualifiedName Int32Name = new QualifiedName("System.Int32");
        static readonly QualifiedName BigIntName = new QualifiedName("System.Numerics.BigInteger");

        public BigInteger IntValue { get; internal set; }
        public override object Value { get { return IntValue; } }

        public IntegerLiteral(BigInteger value, Parse.Syntax.Node syntaxNode)
            : base(syntaxNode)
        {
            IntValue = value;
        }

        public override Goal<AType> ResolveType(AnalysisUnit unit, TypeResolver tr, Var type)
        {
            var int32Type = tr.ResolveType(Int32Name);
            if (int32Type == null)
                throw new InternalException("Unable to resolve type 'System.Int32'.");

            var bigIntType = tr.ResolveType(BigIntName);
            if (bigIntType == null)
                throw new InternalException("Unable to resolve type 'System.Numerics.BigInteger'.");

            return Goal.Disj(
                Goal.Conj(Goal.Pred<AType>(type, _ => IntValue.CompareTo(int.MaxValue) <= 0),
                          Goal.Unify(type, int32Type)),
                Goal.Unify(type, bigIntType)
            );
        }
    }
}
