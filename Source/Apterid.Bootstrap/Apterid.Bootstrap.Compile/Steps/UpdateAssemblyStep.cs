using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Compile.Steps
{
    class UpdateAssemblyStep : CompilerStep
    {
        public CompilerAssembly Assembly { get; }

        public UpdateAssemblyStep(CompilerContext context, CompilerAssembly assembly)
            : base(context)
        {
            Assembly = assembly;

            SubSteps = Assembly.SourceFiles
                .Select(sf =>
                {
                    var parseStep = new ParseSourceFileStep(Context, Assembly, sf);
                    //var analyzeStep = 

                    return parseStep;
                })
                .OfType<CompilerStep>()
                .ToList();
        }
    }
}
