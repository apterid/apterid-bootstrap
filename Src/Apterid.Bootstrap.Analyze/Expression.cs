// Copyright (C) 2015 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verophyle.CSLogic;

namespace Apterid.Bootstrap.Analyze
{
    public abstract class Expression : Scope
    {
        public Type ResolvedType { get; internal set; }
        public abstract Goal<Type> ResolveType(AnalysisUnit unit, TypeResolver tr, Var type);

        public Expression(Parse.Syntax.Node syntaxNode)
            : base(syntaxNode)
        {
        }
    }
}
