using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Apterid.Bootstrap.Generate;

namespace Apterid.Bootstrap.Compile.Steps
{
    class GenerateAssembly : CompilerStep<GenerationUnit>
    {
        ApteridGenerator Generator { get; set; }

        public GenerateAssembly(CompilerContext context, GenerationUnit generationUnit)
            : base(context, generationUnit)
        {
        }

        public async override Task RunAsync(CancellationToken cancel)
        {
            if (Generator == null)
                Generator = new ApteridGenerator(Context, Unit);
            await Generator.Generate(cancel);
        }
    }
}
