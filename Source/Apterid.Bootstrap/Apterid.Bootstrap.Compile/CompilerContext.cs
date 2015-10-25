using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Apterid.Bootstrap.Analyze;
using Apterid.Bootstrap.Parse;

namespace Apterid.Bootstrap.Compile
{
    public class CompilerContext : Common.Context
    {
        public ApteridCompiler Compiler { get; set; }
        public ApteridAnalyzer Analyzer { get; set; }

        public IList<CompilerAssembly> Assemblies { get; set; }

        public CancellationTokenSource CancelSource { get; } = new CancellationTokenSource();

        public IEnumerable<ApteridError> Errors
        {
            get
            {
                return Assemblies != null
                    ? Assemblies.SelectMany(a => a.Errors)
                    : Enumerable.Empty<ApteridError>();
            }
        }
    }
}
