using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Apterid.Bootstrap.Compile
{
    public enum BuildStatus
    {
        NotStarted,
        InProgress,
        Completed,
        Canceled,
    }

    public class BuildTask
    {
        public BuildStatus Status { get; private set; }
        public CancellationToken Cancel { get; }
        public IEnumerable<BuildTask> SubTasks { get; }
        public Func<BuildStatus> ProcessAction { get; }

        public BuildTask(CancellationToken cancel)
            : this(new List<BuildTask>(), null, cancel)
        {
        }

        public BuildTask(Func<BuildStatus> process, CancellationToken cancel)
            : this(new List<BuildTask>(), process, cancel)
        {
        }

        public BuildTask(IEnumerable<BuildTask> subTasks, CancellationToken cancel)
            : this(subTasks, null, cancel)
        {
        }

        public BuildTask(IEnumerable<BuildTask> subTasks, Func<BuildStatus> process, CancellationToken cancel)
        {
            Status = BuildStatus.NotStarted;
            Cancel = cancel;
            SubTasks = subTasks.ToArray();
            ProcessAction = process;
        }

        public Task<BuildStatus> Process()
        {
            Status = BuildStatus.InProgress;

            var subs = SubTasks
                .Where(t => t.Status == BuildStatus.NotStarted 
                            || t.Status == BuildStatus.InProgress)
                .Select(t => Task.Run(() => t.Process()))
                .ToArray();

            Task.WaitAll(subs, Cancel);

            if (Cancel.IsCancellationRequested)
            {
                Status = BuildStatus.Canceled;
            }
            else if (subs.All(t => t.Result == BuildStatus.Completed))
            {
                return OnProcess();
            }
            else
            {
                Status = BuildStatus.InProgress;
            }

            return Task.FromResult(Status);
        }

        public virtual Task<BuildStatus> OnProcess()
        {
            if (ProcessAction != null)
                Status = ProcessAction();
            else
                Status = BuildStatus.Completed;
            return Task.FromResult(Status);
        }
    }
}
