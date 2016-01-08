// Copyright (C) 2015 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IronMeta.Matcher;

namespace Apterid.Bootstrap.Parse.Syntax
{
    public abstract class Space : Leaf
    {
        protected Space(NodeArgs args)
            : base(args)
        {
        }

        protected override void FormatDetails(StringBuilder sb, MatchState<char, Node> ms = null)
        {
            sb.AppendFormat("space \"{0}\" ", Regex.Escape(Text));
            base.FormatDetails(sb, ms);
        }
    }

    public class Whitespace : Space
    {
        public Whitespace(NodeArgs args)
            : base(args)
        {
        }
    }

    public class EndOfLine : Whitespace
    {
        public EndOfLine(NodeArgs args)
            : base(args)
        {
        }
    }

    public class EndOfFile : EndOfLine
    {
        public EndOfFile(NodeArgs args)
            : base(args)
        {
        }
    }

    public abstract class Comment : Space
    {
        protected Comment(NodeArgs args)
            : base(args)
        {
        }
    }

    public class EndComment : Comment
    {
        public EndComment(NodeArgs args)
            : base(args)
        {
        }
    }

    public class InlineComment : Comment
    {
        public InlineComment(NodeArgs args)
            : base(args)
        {
        }
    }
}
