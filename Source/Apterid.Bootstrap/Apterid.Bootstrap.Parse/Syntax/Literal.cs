using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Parse.Syntax
{
    public class Literal : Leaf
    {
        public object Value { get; private set; }
        public Type ValueType { get; private set; }

        public Literal(NodeArgs args, object value, Type valueType)
            : base(args)
        {
            this.Value = value;
            this.ValueType = valueType;
        }
    }

    public class Literal<T> : Literal
    {
        public new T Value { get; private set; }

        public Literal(NodeArgs args, T value)
            : base(args, value, typeof(T))
        {
            this.Value = value;
        }
    }
}
