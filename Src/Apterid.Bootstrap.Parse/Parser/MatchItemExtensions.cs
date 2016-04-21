using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronMeta.Matcher;
namespace Apterid.Bootstrap.Parse.Parser
{
    using SyntaxItem = MatchItem<char, Syntax.Node>;

    internal static class MatchItemExtensions
    {
        public static int Length(this SyntaxItem item)
        {
            return Math.Max(0, item.NextIndex - item.StartIndex);
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
