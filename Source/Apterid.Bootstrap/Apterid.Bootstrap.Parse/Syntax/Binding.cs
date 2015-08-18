using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Parse.Syntax
{
    public class Binding : Node
    {
        public Identifier Name { get; }
        public Pattern Pattern { get; }
        public IEnumerable<Node> Body { get; }

        public Binding(NodeArgs args, Identifier name, Pattern pattern, IEnumerable<Node> body, params Node[] children)
            : base(args, children)
        {
            Name = name;
            Pattern = pattern;
            Body = body;
        }
    }
}
