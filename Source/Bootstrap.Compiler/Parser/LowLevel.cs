using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Compiler.Parser
{
    using Span = IronMeta.Matcher.MatchItem<char, Token>;

    public enum TokenKind
    {
        Whitespace,
        Comment,
        Literal,
        Keyword,
        Identifier,
        Bracket,
    }

    public class Token
    {
        public TokenKind Kind { get; set; }
        public int Line { get; set; }
        public int Offset { get; set; }
        public Span Indent { get; set; }
        public Span Span { get; set; }
        public Span FullSpan { get; set; }
    }

    public class LiteralToken<T> : Token
    {
        public Type LiteralType { get; set; }
        public T Value { get; set; }
    }

    public partial class LowLevel : IronMeta.Matcher.CharMatcher<Token>
    {
    }
}
