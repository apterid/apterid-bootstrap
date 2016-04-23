// Copyright (C) 2016 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apterid.Bootstrap.Common;
using IronMeta.Matcher;

namespace Apterid.Bootstrap.Parse.Syntax
{
    public class Module : FlaggedNode
    {
        public QualifiedIdentifier Name { get; private set; }
        public IList<Node> Body { get; private set; }

        public Module(NodeArgs args, Flags flags, QualifiedIdentifier name, IEnumerable<Node> body, params Node[] children)
            : base(args, flags, children)
        {
            if (name == null) throw new ParseException(args, ErrorMessages.E_0021_ParserImpl_NoModuleName);

            Name = name;
            Body = body.ToArray();
        }

        protected override void FormatDetails(StringBuilder sb, MatchState<char, Node> ms = null)
        {
            sb.AppendFormat("module ");
            base.FormatDetails(sb, ms);
        }

        protected override IEnumerable<Node> ChildrenToFormat => new Node[] { Name }.Concat(Body);
    }
}
