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
    }
}
