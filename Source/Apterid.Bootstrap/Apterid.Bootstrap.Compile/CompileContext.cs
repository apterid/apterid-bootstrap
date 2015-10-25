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
    public class CompileContext : Common.Context
    {
        public ApteridCompiler Compiler { get; internal set; }
        public ApteridAnalyzer Analyzer { get; internal set; }

        public IList<CompileUnit> CompileUnits { get; } = new List<CompileUnit>();

        public CancellationTokenSource CancelSource { get; } = new CancellationTokenSource();

        internal IList<CompileError> CompileErrors { get; } = new List<CompileError>();

        public IEnumerable<ApteridError> Errors
        {
            get
            {
                return CompileUnits
                    .SelectMany(u => u.Errors)
                    .Concat(Errors);
            }
        }
    }

    public class CompileError : ApteridError
    {
    }
}
