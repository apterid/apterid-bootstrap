// Copyright (C) 2015 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Verophyle.CSLogic;

namespace Apterid.Bootstrap.Analyze.Expressions
{
    public abstract class Literal : Expression
    {
        public abstract object Value { get; }
    }

    public abstract class NumericLiteral : Literal
    {
    }

    public class IntegerLiteral : NumericLiteral
    {
        public BigInteger IntValue { get; internal set; }
        public override object Value { get { return IntValue; } }

        public IntegerLiteral(BigInteger value)
        {
            IntValue = value;
        }

        public override Goal<Type> ResolveType(Var type)
        {
            return Goal.Disj(
                Goal.Conj(Goal.Pred<Type>(type, _ => IntValue.CompareTo(System.Int32.MaxValue) <= 0),
                          Goal.Unify<Type>(type, Builtins.SystemInt32.Instance)),
                Goal.Unify<Type>(type, Builtins.SystemNumericsBigInteger.Instance)
            );
        }
    }
}
