using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronMeta.Matcher;

namespace Apterid.Bootstrap.Parse.Syntax
{
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
