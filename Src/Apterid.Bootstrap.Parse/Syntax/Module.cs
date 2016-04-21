// Copyright (C) 2016 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronMeta.Matcher;

namespace Apterid.Bootstrap.Parse.Syntax
{
    public class Module : FlaggedNode
    {
        QualifiedIdentifier qualId;

        public Identifier Name { get; private set; }
        public IEnumerable<Identifier> Qualifiers
        {
            get { return qualId != null ? qualId.Qualifiers : Enumerable.Empty<Identifier>(); }
        }

        public IEnumerable<Node> Body { get; private set; }

        public Module(NodeArgs args, Flags flags, QualifiedIdentifier name, IEnumerable<Node> body, params Node[] children)
            : base(args, flags, children)
        {

            qualId = name;
            Name = qualId.Identifier;
            Body = body;
        }

        public Module(NodeArgs args, Flags flags, Identifier name, IEnumerable<Node> body, params Node[] children)
            : base(args, flags, children)
        {
            Name = name;
            Body = body;
        }

        protected override void FormatDetails(StringBuilder sb, MatchState<char, Node> ms = null)
        {
            sb.AppendFormat("module ");
            base.FormatDetails(sb, ms);
        }

        protected override IEnumerable<Node> ChildrenToFormat => new Node[] { qualId }.Concat(Body);
    }
}
