using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Compile.Tasks
{
    public class UpdateAssembly : CompileTask
    {
        public UpdateAssembly(CompileContext context, CompileAssembly assembly, CancellationTokenSource cancelSource)
            : base(context, assembly, cancelSource)
        {
        }

        public override Task<CompileStatus> OnProcess()
        {
            // tasks to parse files
            var parseTasks = Assembly.SourceFiles.Select(sourceFile => new ParseSourceFile(this, sourceFile));
            var parseAll = new CompileTask(this, parseTasks);

            // tasks to analyze

            // run all
            var allTasks = new CompileTask[] { parseAll, };

            var compileTask = new CompileTask(this, allTasks);
            return compileTask.Process();
        }
    }
}
