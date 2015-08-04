using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Parse.Syntax
{
    public class Function : Node
    {
        public Function(NodeArgs args, params Node[] children)
            : base(args, children)
        {
        }
    }
}
