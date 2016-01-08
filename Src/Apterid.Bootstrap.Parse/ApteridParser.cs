// Copyright (C) 2015 The Apterid Developers - See LICENSE

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
        public ParsedSourceFile SourceFile { get; set; }

        protected Syntax.Flags GetFlags(IEnumerable<Syntax.Node> keywords)
        {
            var results = Syntax.Flags.None;
            foreach (var keyword in keywords.OfType<Syntax.Keyword>())
            {
                switch (keyword.Text)
                {
                    case "":
                        break;
                    case "public":
                        results |= Syntax.Flags.IsPublic;
                        break;
                    default:
                        throw new ParseException(new NodeError
                        {
                            SourceFile = SourceFile,
                            ErrorNode = keyword,
                            ErrorIndex = keyword.StartIndex,
                            Message = string.Format(ErrorMessages.E_0019_Parser_InvalidKeyword, keyword.Text)
                        });
                }
            }
            return results;
        }

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
            nodeArgs.SourceFile = this.SourceFile;
            nodeArgs.Item = item;

            var parmValues = parms != null
                ? parms.GetType().GetProperties().ToDictionary(pi => pi.Name, pi => pi.GetValue(parms))
                : new Dictionary<string, object>();

            var ctors = typeof(T)
                .GetConstructors()
                .OrderByDescending(ctor => ctor.GetParameters().Length);

            if (children == null || children.Length == 0)
                children = item.Results.ToArray();

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
                            throw new InternalException(ErrorMessages.E_0001_ParserImpl_MakeNodeArgs);
                        arguments.Add(nodeArgs);
                    }
                    else if (typeof(Syntax.Node[]).IsAssignableFrom(parm.ParameterType))
                    {
                        if (parm.Name != "children")
                            throw new InternalException(ErrorMessages.E_0002_ParserImpl_MakeNodeChildren);
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

            throw new InternalException(string.Format(ErrorMessages.E_0003_ParserImpl_MakeNodeNoCtor, typeof(T).FullName));
        }
    }

    public class ParseException : ApteridException
    {
        public NodeError Error { get; }

        public ParseException(NodeError error)
            : base(ErrorCode.Parser, error.Message)
        {
            Error = error;
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
