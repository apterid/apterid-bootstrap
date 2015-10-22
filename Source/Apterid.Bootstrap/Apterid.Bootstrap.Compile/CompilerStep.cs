using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Apterid.Bootstrap.Compile
{
    public enum StepResult
    {
        NotStarted,
        Succeeded,
        Deferred,
        Failed,
        Canceled,
    }

    public class CompilerStep
    {
        public CompilerContext Context { get; set; }
        public IList<CompilerStep> SubSteps { get; set; }

        public CompilerStep(CompilerContext context)
        {
            Context = context;
        }

        public virtual StepResult Run()
        {
            return SubSteps != null && SubSteps.Any()
                ? RunSubSteps().Result
                : StepResult.Succeeded;
        }

        protected async Task<StepResult> RunSubSteps()
        {
            bool succeeded = true;
            var remainingSteps = SubSteps;
            do
            {
                var tasks = remainingSteps.Select(s => 
                    Task<Tuple<CompilerStep, StepResult>>.Factory.StartNew(
                        () => Tuple.Create(s, s.Run()),
                        Context.CancelSource.Token,
                        TaskCreationOptions.AttachedToParent,
                        TaskScheduler.Current));

                var results = await Task.WhenAll(tasks);
                if (Context.CancelSource.IsCancellationRequested)
                    return StepResult.Canceled;

                succeeded = results
                    .Where(sr => sr.Item2 != StepResult.Deferred)
                    .All(sr => sr.Item2 == StepResult.Succeeded);

                remainingSteps = results
                    .Where(sr => sr.Item2 == StepResult.Deferred)
                    .Select(sr => sr.Item1)
                    .ToArray();
            }
            while (remainingSteps.Any());

            return succeeded ? StepResult.Succeeded : StepResult.Failed;
        }
    }
}
