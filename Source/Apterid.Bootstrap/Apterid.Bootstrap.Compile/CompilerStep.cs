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
        public CompilerStep Continuation;
    }

    public class CompilerStep
    {
        public CompilerContext Context { get; }
        public CompilationUnit Unit { get; protected set; }

        public List<CompilerStep> SubSteps { get; set; }
        public CompilerStep Continuation { get; set; }

        public CompilerStep(CompilerContext context)
        {
            Context = context;
        }

        public CompilerStep(CompilerContext context, CompilationUnit compileUnit)
            : this(context)
        {
            Unit = compileUnit;
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

        Task<StepResult> StartTask(CompilerStep step)
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
