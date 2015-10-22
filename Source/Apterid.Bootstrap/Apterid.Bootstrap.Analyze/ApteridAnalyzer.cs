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

        public IList<AnalyzerAssembly> Assemblies { get; }

        public ApteridAnalyzer(Context context, IList<AnalyzerAssembly> assemblies)
        {
            Context = context;
            Assemblies = new List<AnalyzerAssembly>(assemblies);
            // TODO: make dummy assemblies from references
        }

        public void Analyze()
        {
        }
    }
}
