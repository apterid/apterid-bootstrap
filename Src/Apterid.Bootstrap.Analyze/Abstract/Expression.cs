// Copyright (C) 2016 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verophyle.CSLogic;

namespace Apterid.Bootstrap.Analyze.Abstract
{
    public abstract class Expression : Scope
    {
        public AType ResolvedType { get; internal set; }
        public abstract Goal<AType> ResolveType(AnalysisUnit unit, TypeResolver tr, Var type);

        public Expression(Parse.Syntax.Node syntaxNode)
            : base(syntaxNode)
        {
        }
    }
}
