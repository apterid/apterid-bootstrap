using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Apterid.Bootstrap.Common;

namespace Apterid.Bootstrap.Generate
{
    public class ApteridGenerator
    {
        public Context Context { get; }
        public GenerationUnit Unit { get; }

        public bool NeedsReRun { get; protected set; }

        public ApteridGenerator(Context context, GenerationUnit generationUnit)
        {
            Context = context;
            Unit = generationUnit;
        }

        public Task Generate(CancellationToken cancel)
        {
            //var dn = string.Format("ApteridGenerator.Generate({0}): {1}", Unit.OutputFileInfo.FullName, Guid.NewGuid().ToString("D"));
            //var domain = AppDomain.CreateDomain(dn);
            return Task.CompletedTask;
        }
    }

    public class GeneratorError : ApteridError
    {
    }
}
