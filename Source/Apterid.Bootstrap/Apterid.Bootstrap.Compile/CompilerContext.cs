using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Apterid.Bootstrap.Parse;

namespace Apterid.Bootstrap.Compile
{
    public class CompilerContext : Common.Context
    {
        public CancellationTokenSource CancelSource { get; } = new CancellationTokenSource();

        public IList<CompilerAssembly> Assemblies { get; set; }

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
