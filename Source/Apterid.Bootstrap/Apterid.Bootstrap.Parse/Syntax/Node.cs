using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronMeta.Matcher;

namespace Apterid.Bootstrap.Parse.Syntax
{
    public abstract class Node
    {
        IList<Node> children;

        int? start, next;
        protected string text;

        public SourceText SourceText { get; set; }
        public MatchItem<char, Node> Item { get; set; }
        public int Indent { get; set; }

        public int StartIndex
        {
            get { return start ?? (start = Item.StartIndex).Value; }
            set { start = value; }
        }
        public int NextIndex
        {
            get { return next ?? (next = Item.NextIndex).Value; }
            set { next = value; }
        }
        public int Length { get { return Math.Max(0, NextIndex - StartIndex); } }

        public Node Prev { get; set; }
        public Node Next { get; set; }
        public IList<Node> Children { get { return children ?? (children = new List<Node>()); } }

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
}
