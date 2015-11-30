// Copyright (C) 2015 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verophyle.CSLogic;

namespace Apterid.Bootstrap.Analyze
{
    public class Type : Scope, IUnifiable<Type>
    {
        public virtual System.Type CLRType { get { throw new NotImplementedException(); } }

        public virtual IEnumerable<State<Type>> Unify(Type other, State<Type> s)
        {
            if (object.ReferenceEquals(this, other))
                yield return s;
        }
    }
}
