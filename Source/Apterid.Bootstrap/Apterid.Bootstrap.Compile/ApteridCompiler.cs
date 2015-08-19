using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Apterid.Bootstrap.Common;

namespace Apterid.Bootstrap.Compile
{
    public class ApteridCompiler
    {
        public CompileContext Context { get; protected set; }

        public ApteridCompiler()
        {
            Context = new CompileContext();
        }

        public ApteridCompiler(CompileContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            Context = context;
        }

        public void AddAssembly(CompileAssembly assembly)
        {

        }

        public Task<CompileStatus> UpdateAssemblies()
        {
            var cancelSource = new CancellationTokenSource();
            var assemblyTasks = Context.Assemblies.Select(a => new Tasks.UpdateAssembly(Context, a, cancelSource));
            var compileTask = new CompileTask(Context, null, cancelSource, assemblyTasks);
            return compileTask.Process();
        }

        public Task<CompileStatus> UpdateAssembly(CompileAssembly assembly, CancellationTokenSource cancelSource = null)
        {
            if (cancelSource == null)
                cancelSource = new CancellationTokenSource();
            var assemblyTask = new Tasks.UpdateAssembly(Context, assembly, cancelSource);
            return assemblyTask.Process();
        }
    }
}
