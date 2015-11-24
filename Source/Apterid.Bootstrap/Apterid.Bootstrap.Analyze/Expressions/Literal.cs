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
        public object Value { get; internal set; }
    }

    public abstract class NumericLiteral : Literal
    {
    }

    public class IntegerLiteral : NumericLiteral
    {
        public new BigInteger Value { get; internal set; }

        public IntegerLiteral(BigInteger value)
        {
            (this as Literal).Value = value;
            Value = value;
        }

        public override Goal<Type> ResolveType(Var type)
        {
            return Goal.Disj(
                Goal.Conj(Goal.Pred<Type>(type, _ => Value.CompareTo(System.Int32.MaxValue) <= 0),
                          Goal.Unify<Type>(type, Builtins.SystemInt32.Instance)),
                Goal.Unify<Type>(type, Builtins.SystemNumericsBigInteger.Instance)
            );
        }
    }
}
