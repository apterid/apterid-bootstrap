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
        public SourceText SourceText;
        public MatchItem<char, Node> Item;
    }

    public abstract class Node
    {
        Node[] children;

        int? start, next;
        protected string text;

        public SourceText SourceText { get; private set; }
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
            this.SourceText = args.SourceText;
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
                    if (SourceText == null || SourceText.Buffer == null)
                        text = "";
                    else
                        text = string.Concat(SourceText.Buffer.Skip(StartIndex).Take(Length));
                }
                return text;
            }
        }
    }

    public abstract class Leaf : Node
    {
        public Leaf PrevLeaf { get; private set; }
        public Leaf NextLeaf { get; private set; }

        public Leaf(NodeArgs args)
            : base(args)
        {
        }

        public static void ConnectLeaves(Node root)
        {
            Leaf prev = null;
            ConnectLeaves(root, ref prev);
        }

        static void ConnectLeaves(Node node, ref Leaf prev)
        {
            Leaf leaf = node as Leaf;
            if (leaf != null)
            {
                leaf.PrevLeaf = prev;
                if (prev != null) prev.NextLeaf = leaf;
                prev = leaf;
            }
            else
            {
                foreach (var child in node.Children)
                    ConnectLeaves(child, ref prev);
            }
        }
    }
}
