// Copyright (C) 2016 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        protected override void FormatDetails(StringBuilder sb, MatchState<char, Node> ms = null)
        {
            sb.AppendFormat("token \"{0}\"", Regex.Escape(Text));
            base.FormatDetails(sb, ms);
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
        public IList<Identifier> Qualifiers { get; private set; }
        public Identifier Name { get; private set; }

        public QualifiedIdentifier(NodeArgs args, IEnumerable<Node> q, Identifier n, params Node[] children)
            : base(args, children)
        {
            Qualifiers = q.OfType<Identifier>().ToArray();
            Name = n;
        }

        public override string Text
        {
            get { return Name.Text; }
        }

        protected override void FormatDetails(StringBuilder sb, MatchState<char, Node> ms = null)
        {
            if (Qualifiers != null)
                sb.AppendFormat("qid {0}{1} ", string.Join(".", Qualifiers.Select(q => q.Text)), Name.Text);
            else
                sb.AppendFormat("qid {0} ", Name.Text);
            base.FormatDetails(sb, ms);
        }
    }
}
