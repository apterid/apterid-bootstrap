using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Analyze.Builtins
{
    public abstract class Number : Type
    {
    }

    public abstract class Integer : Number
    {
    }

    public class SystemInt32 : Integer
    {
        public static SystemInt32 Instance = new SystemInt32();

        public override System.Type CLRType => typeof(System.Int32);

        private SystemInt32()
        {
            Parent = DotNetSystem.Instance;
        }
    }

    public class SystemNumericsBigInteger : Integer
    {
        public static SystemNumericsBigInteger Instance = new SystemNumericsBigInteger();

        public override System.Type CLRType => typeof(System.Numerics.BigInteger);

        private SystemNumericsBigInteger()
        {
            Parent = DotNetSystem.Numerics.Instance;
        }
    }
}
