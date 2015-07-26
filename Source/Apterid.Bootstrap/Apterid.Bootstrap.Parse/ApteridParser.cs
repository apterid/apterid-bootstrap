using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronMeta.Matcher;

namespace Apterid.Bootstrap.Parse
{
    using SyntaxItem = MatchItem<char, Syntax.Node>;

    public partial class ApteridParser
    {
        public SourceText SourceText { get; set; }

        protected T Make<T>(SyntaxItem item, Action<T> post = null)
            where T : Syntax.Node, new()
        {
            var node = new T()
            {
                SourceText = this.SourceText,
                Item = item,
            };

            if (post != null)
                post(node);

            return node;
        }
    }

    internal static class MatchItemExtensions
    {
        public static int Length(this SyntaxItem item)
        {
            return Math.Min(0, item.NextIndex - item.StartIndex);
        }

        public static IEnumerable<T> ResultsOf<T>(this SyntaxItem item)
        {
            return item.Results.OfType<T>();
        }

        public static T ResultOf<T>(this SyntaxItem item)
        {
            return item.Results.OfType<T>().Single();
        }

        public static Syntax.Node Result(this SyntaxItem item)
        {
            return item.Results.First();
        }
    }
}
