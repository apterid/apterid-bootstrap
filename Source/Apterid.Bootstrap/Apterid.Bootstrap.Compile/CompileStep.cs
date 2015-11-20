using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Apterid.Bootstrap.Compile
{
    public enum StepStatus
    {
        Continue,
        Succeeded,
        Failed,
        Canceled,
    }

    public struct StepResult
    {
        public StepStatus Status;
        public CompileStep Continuation;
    }

    public class CompileStep
    {
        public CompileContext Context { get; }
        public CompileUnit CompileUnit { get; protected set; }

        public List<CompileStep> SubSteps { get; set; }
        public CompileStep Continuation { get; set; }

        public CompileStep(CompileContext context)
        {
            Context = context;
        }

        public CompileStep(CompileContext context, CompileUnit compileUnit)
            : this(context)
        {
            CompileUnit = compileUnit;
        }

        public virtual StepResult Run()
        {
            return SubSteps != null && SubSteps.Any()
                ? new StepResult { Status = RunSubSteps().Result, Continuation = Continuation }
                : Succeeded();
        }

        protected async Task<StepStatus> RunSubSteps()
        {
            var tasks = SubSteps.Select(StartTask).ToList();

            while (tasks.Any())
            {
                Task<StepResult> task = null;

                try
                {
                    task = await Task.WhenAny(tasks);
                }
                catch (OperationCanceledException)
                {
                    return StepStatus.Canceled;
                }
                finally
                {
                    if (task != null && task.IsCompleted)
                        tasks.Remove(task);
                }

                if (Context.CancelSource.IsCancellationRequested)
                    return StepStatus.Canceled;

                switch (task.Result.Status)
                {
                    case StepStatus.Canceled:
                        return StepStatus.Canceled;

                    case StepStatus.Failed:
                        Context.CancelSource.Cancel();
                        return StepStatus.Failed;

                    case StepStatus.Continue:
                        if (task.Result.Continuation != null)
                            tasks.Add(StartTask(task.Result.Continuation));
                        break;

                    case StepStatus.Succeeded:
                        break;
                }
            }

            return StepStatus.Succeeded;
        }

        Task<StepResult> StartTask(CompileStep step)
        {
            return Task<StepResult>.Factory.StartNew(
                step.Run, 
                Context.CancelSource.Token, 
                TaskCreationOptions.AttachedToParent, 
                TaskScheduler.Current);
        }

        protected StepResult Succeeded()
        {
            return new StepResult
            {
                Status = Continuation != null ? StepStatus.Continue : StepStatus.Succeeded,
                Continuation = Continuation
            };
        }

        protected StepResult Failed()
        {
            return new StepResult { Status = StepStatus.Failed };
        }

        protected StepResult Canceled()
        {
            return new StepResult { Status = StepStatus.Canceled };
        }
    }
}
