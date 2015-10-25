using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Apterid.Bootstrap.Common;
using Apterid.Bootstrap.Compile.Steps;

namespace Apterid.Bootstrap.Compile
{
    public class ApteridCompiler
    {
        public CompilerContext Context { get; }

        public StepStatus UpdateAllAssemblies()
        {
            var compileStep = new CompilerStep(Context)
            {
                SubSteps = Context.Assemblies
                    .Select(a => new UpdateAssemblyStep(Context, a))
                    .OfType<CompilerStep>()
                    .ToList()
            };

            return compileStep.Run().Status;
        }

        public ApteridCompiler(CompilerContext context)
        {
            Context = context;
        }
    }
}
