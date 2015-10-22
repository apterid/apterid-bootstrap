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
        public CompilerContext Context { get; protected set; }

        public ApteridCompiler()
        {
            Context = new CompilerContext();
        }

        public ApteridCompiler(CompilerContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            Context = context;
        }

        public StepResult UpdateAllAssemblies()
        {
            var compileStep = new CompilerStep(Context)
            {
                SubSteps = Context.Assemblies
                    .Select(a => new UpdateAssemblyStep(Context, a))
                    .Cast<CompilerStep>()
                    .ToList()
            };

            return compileStep.Run();
        }
    }
}
