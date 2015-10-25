using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Apterid.Bootstrap.Common;
using Apterid.Bootstrap.Parse;

namespace Apterid.Bootstrap.Analyze
{
    public class ApteridAnalyzer
    {
        public Context Context { get; }

        public ApteridAnalyzer(Context context)
        {
            Context = context;
            // TODO: make dummy assemblies from references
        }

        public void Analyze(AnalyzeUnit analyzeUnit, ParserSourceFile sourceFile)
        {
            var moduleNodes = sourceFile.GetNodes<Parse.Syntax.Module>();
            foreach (var moduleNode in moduleNodes)
            {
                var module = new Module();

                analyzeUnit.Modules.Add(module);
            }
        }
    }

    public class AnalyzeException : ApteridException
    {
    }
}
