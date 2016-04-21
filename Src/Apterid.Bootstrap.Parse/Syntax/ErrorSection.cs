// Copyright (C) 2016 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronMeta.Matcher;

namespace Apterid.Bootstrap.Parse.Syntax
{
    public class ErrorSection : Node
    {
        public ErrorSection(NodeArgs args, params Node[] children)
            : base(args, children)
        {
        }

        protected override void FormatDetails(StringBuilder sb, MatchState<char, Node> ms = null)
        {
            sb.AppendFormat("ERROR SECTION ");
            base.FormatDetails(sb, ms);
        }

        protected override IEnumerable<Node> ChildrenToFormat => Enumerable.Empty<Node>();
    }
}
