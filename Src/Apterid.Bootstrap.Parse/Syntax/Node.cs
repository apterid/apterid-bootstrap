// Copyright (C) 2016 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronMeta.Matcher;

namespace Apterid.Bootstrap.Parse.Syntax
{
    public struct NodeArgs
    {
        public ParsedSourceFile SourceFile;
        public MatchItem<char, Node> Item;
    }

    public abstract class Node
    {
        Node[] children;

        int? start, next;
        protected string text;

        public ParsedSourceFile SourceFile { get; private set; }
        public MatchItem<char, Node> Item { get; private set; }

        public int StartIndex
        {
            get { return start ?? (start = Item.StartIndex).Value; }
            private set { start = value; }
        }
        public int NextIndex
        {
            get { return next ?? (next = Item.NextIndex).Value; }
            private set { next = value; }
        }
        public int Length { get { return Math.Max(0, NextIndex - StartIndex); } }

        public Node Parent { get; private set; }

        public Node[] Children
        {
            get { return children ?? (children = new Node[0]); }
            private set { children = value; }
        }

        public Node(NodeArgs args, params Node[] children)
        {
            this.SourceFile = args.SourceFile;
            this.Item = args.Item;
            this.Children = children;

            foreach (var child in this.Children)
                child.Parent = this;
        }

        public virtual string Text
        {
            get
            {
                if (text == null)
                {
                    if (SourceFile == null || SourceFile.Buffer == null)
                        text = "";
                    else
                        text = string.Concat(SourceFile.Buffer.Skip(StartIndex).Take(Length));
                }
                return text;
            }
        }

        public static void Renumber(Node node, int delta)
        {
            node.StartIndex += delta;
            node.NextIndex += delta;

            foreach (var child in node.Children)
                Renumber(child, delta);
        }

        protected virtual void FormatDetails(StringBuilder sb, MatchState<char, Node> ms = null)
        {
            if (ms != null && Item != null && SourceFile != null)
            {
                int num, offset;
                ms.GetLine(Item.StartIndex, out num, out offset);
                sb.AppendFormat("    @ {0}({1},{2})", SourceFile.Name, num, offset);
            }
        }

        protected virtual IEnumerable<Node> ChildrenToFormat
        {
            get { return Children; }
        }

        public void FormatString(StringBuilder sb, string indent, MatchState<char, Node> ms = null)
        {
            sb.AppendFormat("{0} ", indent);
            FormatDetails(sb, ms);
            sb.AppendLine();

            indent = indent + "  ";
            foreach (var child in ChildrenToFormat.Where(c => c != null))
                child.FormatString(sb, indent, ms);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            FormatString(sb, "");
            return sb.ToString();
        }
    }

    [Flags]
    public enum Flags
    {
        None = 0,
        IsPublic = 1 << 0,
    }

    public abstract class FlaggedNode : Node
    {
        public Flags Flags { get; }

        public FlaggedNode(NodeArgs args, Flags flags, params Node[] children)
            : base(args, children)
        {
            Flags = flags;
        }

        protected override void FormatDetails(StringBuilder sb, MatchState<char, Node> ms = null)
        {
            if ((Flags & Flags.IsPublic) != 0)
                sb.AppendFormat("public ");
            base.FormatDetails(sb, ms);
        }
    }
}
