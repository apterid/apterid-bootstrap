using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Parse.Syntax
{
    public abstract class Leaf : Node
    {
        public Leaf(NodeArgs args)
            : base(args)
        {
        }
    }
}
