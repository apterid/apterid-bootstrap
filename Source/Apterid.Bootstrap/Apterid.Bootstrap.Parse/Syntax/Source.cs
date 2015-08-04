using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Parse.Syntax
{
    public class Source : Node
    {
        public Source(NodeArgs args, params Node[] children)
            : base(args, children)
        {
        }
    }

    public class Directive : Node
    {
        public Directive(NodeArgs args, params Node[] children)
            : base(args, children)
        {
        }
    }
}
