using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Apterid.Bootstrap.Generate;

namespace Apterid.Bootstrap.Compile.Steps
{
    class GenerateModule : CompilerStep<GenerationUnit>
    {
        Analyze.Module Module { get; set; }
        ApteridGenerator Generator { get; set; }

        public GenerateModule(CompilerContext context, GenerationUnit generationUnit, Analyze.Module module)
            : base(context, generationUnit)
        {
            Module = module;
        }

        public override Action GetStepAction(CancellationToken cancel)
        {
            return () =>
            {
                if (Generator == null)
                {
                    lock (this)
                    {
                        if (Generator == null)
                            Generator = new ApteridGenerator(Context, Unit, Module);
                    }
                }

                Generator.Generate(cancel);
            };
        }
    }
}
