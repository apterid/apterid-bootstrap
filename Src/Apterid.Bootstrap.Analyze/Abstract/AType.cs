// Copyright (C) 2015 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verophyle.CSLogic;

namespace Apterid.Bootstrap.Analyze.Abstract
{
    public class AType : Scope, IUnifiable<AType>
    {
        public virtual System.Type CLRType { get; internal set; }

        public AType(Parse.Syntax.Node syntaxNode)
            : base(syntaxNode)
        {
        }

        public virtual IEnumerable<State<AType>> Unify(AType other, State<AType> s)
        {
            if (object.ReferenceEquals(this, other))
                yield return s;
        }
    }
}
