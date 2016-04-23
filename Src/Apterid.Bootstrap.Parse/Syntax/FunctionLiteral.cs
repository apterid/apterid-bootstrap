using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Parse.Syntax
{
    public class FunctionLiteral : Node
    {
        public Pattern Pattern { get; }
        public IList<Node> Body { get; }

        public FunctionLiteral(NodeArgs args, Pattern pattern, IEnumerable<Node> body, params Node[] children)
            : base(args, children)
        {
            Pattern = pattern;
            Body = body.ToArray();
        }
    }
}
