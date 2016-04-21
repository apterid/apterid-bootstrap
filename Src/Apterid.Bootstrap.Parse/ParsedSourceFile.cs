// Copyright (C) 2016 The Apterid Developers - See LICENSE

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apterid.Bootstrap.Parse.Parser;

namespace Apterid.Bootstrap.Parse
{
    public abstract class ParsedSourceFile : Common.SourceFile
    {
        public ApteridParser Parser { get; set; }
        public IronMeta.Matcher.MatchState<char, Syntax.Node> MatchState { get; set; }
        public IronMeta.Matcher.MatchResult<char, Syntax.Node> MatchResult { get; set; }

        public Syntax.Node ParseTree { get; set; }

        public IEnumerable<T> GetNodes<T>()
            where T : Syntax.Node
        {
            return ParseTree != null
                ? GetNodes<T>(ParseTree)
                : Enumerable.Empty<T>();
        }

        IEnumerable<T> GetNodes<T>(Syntax.Node node)
            where T : Syntax.Node
        {
            var tnode = node as T;
            if (tnode != null) yield return tnode;

            foreach (var n in node.Children.SelectMany(child => GetNodes<T>(child)))
                yield return n;
        }
    }

    public class MemorySourceFile : ParsedSourceFile
    {
        string source;

        public override IEnumerable<char> Buffer { get { return source; } }
        public override bool Exists { get { return true; } }
        public override DateTime LastWriteTimeUtc { get { return DateTime.UtcNow; } }

        public MemorySourceFile(string buffer)
        {
            Name = Guid.NewGuid().ToString("D");
            source = buffer;
        }
    }
}
