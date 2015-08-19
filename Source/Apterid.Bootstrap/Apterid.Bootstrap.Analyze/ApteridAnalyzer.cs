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

        public IList<Assembly> Assemblies { get; }

        public ApteridAnalyzer(Context context, IList<Assembly> assemblies)
        {
            Context = context;
            Assemblies = new List<Assembly>(assemblies);
            // TODO: make dummy assemblies from references
        }

        public void Analyze()
        {
        }
    }
}
