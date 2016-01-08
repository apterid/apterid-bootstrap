// Copyright (C) 2015 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronMeta.Matcher;

namespace Apterid.Bootstrap.Parse.Syntax
{
    public class Binding : FlaggedNode
    {
        public Identifier Name { get; }
        public Pattern Pattern { get; }
        public IEnumerable<Node> Body { get; }

        public Binding(NodeArgs args, Flags flags, Identifier name, Pattern pattern, IEnumerable<Node> body, params Node[] children)
            : base(args, flags, children)
        {
            Name = name;
            Pattern = pattern;
            Body = body;
        }

        protected override IEnumerable<Node> ChildrenToFormat => new Node[] { Name, Pattern }.Concat(Body);

        protected override void FormatDetails(StringBuilder sb, MatchState<char, Node> ms = null)
        {
            sb.AppendFormat("binding ");
            base.FormatDetails(sb, ms);
        }
    }
}
