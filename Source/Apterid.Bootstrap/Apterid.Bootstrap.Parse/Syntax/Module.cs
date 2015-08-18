using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Parse.Syntax
{
    public class Module : Node
    {
        QualifiedIdentifier qualId;

        public Identifier Name { get; private set; }
        public IEnumerable<Identifier> Qualifiers
        {
            get
            {
                return qualId != null ? qualId.Qualifiers : Enumerable.Empty<Identifier>();
            }
        }

        public IEnumerable<Node> Body { get; private set; }

        public Module(NodeArgs args, QualifiedIdentifier name, IEnumerable<Node> body, params Node[] children)
            : base(args, children)
        {
            qualId = name;
            Name = qualId.Identifier;
            Body = body;
        }

        public Module(NodeArgs args, Identifier name, IEnumerable<Node> body, params Node[] children)
            : base(args, children)
        {
            Name = name;
            Body = body;
        }
    }
}
