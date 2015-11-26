using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Apterid.Bootstrap.Common;

namespace Apterid.Bootstrap.Generate
{
    public class ApteridGenerator
    {
        public Context Context { get; }
        public GenerationUnit Unit { get; }
        public Analyze.Module Module { get; }

        public ApteridGenerator(Context context, GenerationUnit generationUnit, Analyze.Module module)
        {
            Context = context;
            Unit = generationUnit;
            Module = module;
        }

        CancellationToken cancel;

        public Task Generate(CancellationToken cancel)
        {
            this.cancel = cancel;

            return Task.Run(() =>
            {
                TypeAttributes atts = TypeAttributes.Sealed;
                atts |= Module.IsPublic ? TypeAttributes.Public : TypeAttributes.NotPublic;

                var mtb = Unit.ModuleBuilder.DefineType(Module.Name.FullName, atts, typeof(Apterid.Module));

                foreach (var type in Module.Types.Values)
                {
                    if (cancel.IsCancellationRequested) throw new OperationCanceledException(cancel);

                    GenerateType(mtb, type);
                }

                foreach (var binding in Module.Bindings.Values)
                {
                    if (cancel.IsCancellationRequested) throw new OperationCanceledException(cancel);

                    GenerateBinding(mtb, binding);
                }

                mtb.CreateType();
            }, 
            cancel);
        }

        void GenerateType(TypeBuilder tb, Analyze.Type type)
        {
        }

        void GenerateBinding(TypeBuilder tb, Analyze.Binding binding)
        {
            if (binding.Expression != null)
            {
                if (binding.Expression is Analyze.Expressions.Literal)
                {
                    GenerateLiteral(tb, binding.Expression as Analyze.Expressions.Literal);
                }
            }
        }

        void GenerateField(TypeBuilder tb, Analyze.Expression expression)
        {
        }

        void GenerateLiteral(TypeBuilder tb, Analyze.Expressions.Literal literal)
        {

        }
    }

    public class GeneratorError : ApteridError
    {
    }
}
