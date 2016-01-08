// Copyright (C) 2015 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Analyze
{
    public class Module : Scope
    {
        public bool IsPublic { get; internal set; }
        public IDictionary<QualifiedName, Type> Types { get; } = new Dictionary<QualifiedName, Type>();

        public Module(Parse.Syntax.Node syntaxNode)
            : base(syntaxNode)
        {
        }
    }
}
