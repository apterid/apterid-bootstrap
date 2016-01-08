// Copyright (C) 2015 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Analyze
{
    public class Scope
    {
        public Scope Parent { get; internal set; }
        public QualifiedName Name { get; internal set; }

        public Parse.Syntax.Node SyntaxNode { get; internal set; }

        public IList<Parse.Syntax.Node> PreTrivia { get; } = new List<Parse.Syntax.Node>();
        public IList<Parse.Syntax.Node> PostTrivia { get; } = new List<Parse.Syntax.Node>();

        public IDictionary<QualifiedName, Scope> Children { get; } = new Dictionary<QualifiedName, Scope>();
        public IDictionary<QualifiedName, Binding> Bindings { get; } = new Dictionary<QualifiedName, Binding>();

        public Scope(Parse.Syntax.Node syntaxNode)
        {
            SyntaxNode = syntaxNode;
        }
    }
}
