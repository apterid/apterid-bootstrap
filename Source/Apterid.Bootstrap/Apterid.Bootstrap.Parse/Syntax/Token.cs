using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronMeta.Matcher;

namespace Apterid.Bootstrap.Parse.Syntax
{
    public abstract class Token : Leaf
    {
        protected Token(NodeArgs args)
            : base(args)
        {
        }
    }

    public class Punct : Token
    {
        public Punct(NodeArgs args)
            : base(args)
        {
        }
    }

    public class Keyword : Token
    {
        public Keyword(NodeArgs args)
            : base(args)
        {
        }
    }

    public class Identifier : Token
    {
        public Identifier(NodeArgs args)
            : base(args)
        {
        }
    }

    public class QualifiedIdentifier : Node
    {
        public IEnumerable<Identifier> Qualifiers { get; private set; }
        public Identifier Identifier { get; private set; }

        public QualifiedIdentifier(NodeArgs args, params Node[] children)
            : base(args, children)
        {
            Qualifiers = children
                .Take(children.Length - 1)
                .OfType<Identifier>()
                .ToArray();
            Identifier = (Identifier)children.Last();
        }

        public override string Text
        {
            get { return Identifier.Text; }
        }
    }
}
