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

        internal IList<CompileError> CompileErrors { get; } = new List<CompileError>();

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

    public class CompileError : ApteridError
    {
    }
}
