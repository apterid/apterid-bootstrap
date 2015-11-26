// Copyright (C) 2015 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Apterid.Bootstrap.Common;

namespace Apterid.Bootstrap.Compile
{
    public abstract class CompilerStep<TUnit> : CompilerStep
        where TUnit : Unit
    {
        public TUnit Unit { get; }

        public CompilerStep(CompilerContext context, TUnit unit)
            : base(context)
        {
            Unit = unit;
        }
    }

    public abstract class CompilerStep
    {
        public CompilerContext Context { get; }

        public CompilerStep(CompilerContext context)
        {
            Context = context;
        }

        public abstract Action GetStepAction(CancellationToken cancel);
    }
}
