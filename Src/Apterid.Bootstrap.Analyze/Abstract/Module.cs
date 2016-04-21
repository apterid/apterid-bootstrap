// Copyright (C) 2016 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Analyze.Abstract
{
    public class Module : Scope
    {
        public bool IsPublic { get; internal set; }
        public IDictionary<QualifiedName, AType> Types { get; } = new Dictionary<QualifiedName, AType>();

        public Module(Parse.Syntax.Node syntaxNode)
            : base(syntaxNode)
        {
        }
    }
}
