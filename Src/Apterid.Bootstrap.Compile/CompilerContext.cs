// Copyright (C) 2015 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Apterid.Bootstrap.Analyze;
using Apterid.Bootstrap.Common;
using Apterid.Bootstrap.Parse;

namespace Apterid.Bootstrap.Compile
{
    public class CompilerContext : Context
    {
        public CancellationTokenSource CancelSource { get; } = new CancellationTokenSource();
        public IList<CompilationUnit> CompileUnits { get; } = new List<CompilationUnit>();
        public IList<CompilerError> CompileErrors { get; } = new List<CompilerError>();

        public IEnumerable<ApteridError> Errors
        {
            get
            {
                return CompileUnits
                    .SelectMany(u => u.Errors)
                    .Concat(CompileErrors);
            }
        }
    }
}
