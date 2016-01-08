// Copyright (C) 2015 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronMeta.Matcher;

namespace Apterid.Bootstrap.Parse.Syntax
{
    public class Source : Node
    {
        public Source(NodeArgs args, params Node[] children)
            : base(args, children)
        {
        }

        protected override void FormatDetails(StringBuilder sb, MatchState<char, Node> ms = null)
        {
            sb.AppendFormat("source ");
            base.FormatDetails(sb, ms);
        }
    }

    public class Directive : Node
    {
        public Directive(NodeArgs args, params Node[] children)
            : base(args, children)
        {
        }

        protected override void FormatDetails(StringBuilder sb, MatchState<char, Node> ms = null)
        {
            sb.AppendFormat("directive ");
            base.FormatDetails(sb, ms);
        }
    }
}
