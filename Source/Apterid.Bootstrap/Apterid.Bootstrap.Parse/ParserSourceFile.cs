﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apterid.Bootstrap.Parse.Syntax;

namespace Apterid.Bootstrap.Parse
{
    public abstract class ParserSourceFile : Common.SourceFile
    {
        public ApteridParser Parser { get; set; }
        public Node ParseTree { get; set; }

        public IEnumerable<T> GetNodes<T>()
            where T : Node
        {
            return ParseTree != null
                ? GetNodes<T>(ParseTree)
                : Enumerable.Empty<T>();
        }

        IEnumerable<T> GetNodes<T>(Node node)
            where T : Node
        {
            var tnode = node as T;
            if (tnode != null) yield return tnode;

            foreach (var n in node.Children.SelectMany(child => GetNodes<T>(child)))
                yield return n;
        }

        public IEnumerable<ApteridError> Errors
        {
        }
    }

    public class MemorySourceFile : ParserSourceFile
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
