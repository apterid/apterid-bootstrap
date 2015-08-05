using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Apterid.Bootstrap.Common;
using IronMeta.Matcher;

namespace Apterid.Bootstrap.Parse
{
    using SyntaxItem = MatchItem<char, Syntax.Node>;

    public partial class ApteridParser
    {
        public SourceText SourceText { get; set; }

        protected T Make<T>(SyntaxItem item)
            where T : Syntax.Node
        {
            return Make<T>(item, item.Results.ToArray());
        }

        protected T Make<T>(SyntaxItem item, IEnumerable<Syntax.Node> children)
            where T : Syntax.Node
        {
            return Make<T>(item, children.ToArray());
        }

        protected T Make<T>(SyntaxItem item, params Syntax.Node[] children)
            where T : Syntax.Node
        {
            return Make<T>(item, null, children);
        }

        protected T Make<T>(SyntaxItem item, object parms, IEnumerable<Syntax.Node> children)
            where T : Syntax.Node
        {
            return Make<T>(item, parms, children.ToArray());
        }
    

        protected T Make<T>(SyntaxItem item, object parms, params Syntax.Node[] children)
            where T : Syntax.Node
        {
            var nodeArgs = new Syntax.NodeArgs();
            nodeArgs.SourceText = this.SourceText;
            nodeArgs.Item = item;

            var parmValues = parms != null
                ? parms.GetType().GetProperties().ToDictionary(pi => pi.Name, pi => pi.GetValue(parms))
                : new Dictionary<string, object>();

            var ctors = typeof(T)
                .GetConstructors()
                .OrderByDescending(ctor => ctor.GetParameters().Length);

            var arguments = new List<object>();
            foreach (var ctor in ctors)
            {
                bool failed = false;
                foreach (var parm in ctor.GetParameters())
                {
                    object parmValue;

                    if (typeof(Syntax.NodeArgs).IsAssignableFrom(parm.ParameterType))
                    {
                        if (parm.Name != "args")
                            throw new InternalException(ErrorMessages.EI_0001_ParserImpl_MakeNodeArgs);
                        arguments.Add(nodeArgs);
                    }
                    else if (typeof(Syntax.Node[]).IsAssignableFrom(parm.ParameterType))
                    {
                        if (parm.Name != "children")
                            throw new InternalException(ErrorMessages.EI_0002_ParserImpl_MakeNodeChildren);
                        arguments.Add(children);
                    }
                    else if (parmValues.TryGetValue(parm.Name, out parmValue))
                    {
                        arguments.Add(parmValue);
                    }
                    else
                    {
                        failed = true;
                        break;
                    }
                }

                if (failed) continue;

                return (T)ctor.Invoke(arguments.ToArray());
            }

            throw new InternalException(string.Format(ErrorMessages.EI_0003_ParserImpl_MakeNodeNoCtor, typeof(T).FullName));
        }
    }

    internal static class MatchItemExtensions
    {
        public static int Length(this SyntaxItem item)
        {
            return Math.Max(0, item.NextIndex - item.StartIndex);
        }

        public static IEnumerable<T> ResultsOf<T>(this SyntaxItem item)
        {
            return item.Results.OfType<T>();
        }

        public static T ResultOf<T>(this SyntaxItem item)
        {
            return item.Results.OfType<T>().Single();
        }

        public static Syntax.Node Result(this SyntaxItem item)
        {
            return item.Results.First();
        }
    }
}
