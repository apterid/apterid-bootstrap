using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Apterid.Bootstrap.Compile
{
    public enum CompileStatus
    {
        NotStarted,
        InProgress,
        Completed,
        Failed,
        Canceled,
    }

    public class CompileTask
    {
        public CompileContext Context { get; protected set; }
        public CompileAssembly Assembly { get; protected set; }

        public CompileStatus Status { get; private set; }
        public CancellationToken Cancel { get; }
        public IEnumerable<CompileTask> SubTasks { get; }
        public Func<CompileStatus> ProcessAction { get; }

        protected CancellationTokenSource CancelSource { get; set; }

        public CompileTask(CompileTask parent)
            : this(parent.Context, parent.Assembly, parent.CancelSource, Enumerable.Empty<CompileTask>(), null)
        {
        }

        public CompileTask(CompileTask parent, IEnumerable<CompileTask> subTasks)
            : this(parent.Context, parent.Assembly, parent.CancelSource, subTasks)
        {
        }

        public CompileTask(CompileContext context, CompileAssembly assembly, CancellationTokenSource cancelSource)
            : this(context, assembly, cancelSource, Enumerable.Empty<CompileTask>(), null)
        {
        }

        public CompileTask(CompileContext context, CompileAssembly assembly, CancellationTokenSource cancelSource, Func<CompileStatus> process)
            : this(context, assembly, cancelSource, Enumerable.Empty<CompileTask>(), process)
        {
        }

        public CompileTask(CompileContext context, CompileAssembly assembly, CancellationTokenSource cancelSource, IEnumerable<CompileTask> subTasks)
            : this(context, assembly, cancelSource, subTasks, null)
        {
        }

        public CompileTask(CompileContext context, CompileAssembly assembly, CancellationTokenSource cancelSource, 
            IEnumerable<CompileTask> subTasks, Func<CompileStatus> process)
        {
            Context = context;
            Assembly = assembly;
            Status = CompileStatus.NotStarted;
            CancelSource = cancelSource;
            Cancel = cancelSource != null ? cancelSource.Token : default(CancellationToken);
            SubTasks = subTasks.ToArray();
            ProcessAction = process;
        }

        public Task<CompileStatus> Process()
        {
            Status = CompileStatus.InProgress;

            var subs = SubTasks
                .Where(t => t.Status == CompileStatus.NotStarted 
                            || t.Status == CompileStatus.InProgress)
                .Select(t => Task.Run(() => t.Process()))
                .ToArray();

            Task.WaitAll(subs, Cancel);

            if (Cancel.IsCancellationRequested)
            {
                Status = CompileStatus.Canceled;
            }
            else if (subs.All(t => t.Result == CompileStatus.Completed))
            {
                return OnProcess();
            }
            else
            {
                Status = CompileStatus.InProgress;
            }

            return Task.FromResult(Status);
        }

        public virtual Task<CompileStatus> OnProcess()
        {
            if (ProcessAction != null)
                Status = ProcessAction();
            else
                Status = CompileStatus.Completed;
            return Task.FromResult(Status);
        }
    }
}
