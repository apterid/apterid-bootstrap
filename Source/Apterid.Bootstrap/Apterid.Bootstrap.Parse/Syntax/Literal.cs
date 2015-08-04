using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Parse.Syntax
{
    public class Literal<T> : Leaf
    {
        public T Value { get; private set; }

        public Literal(NodeArgs args, T value)
            : base(args)
        {
            this.Value = value;
        }
    }
}
