using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Compile
{
    public class BuildContext : Common.Context
    {
        public IList<BuildAssembly> Assemblies { get; set; }

        public IEnumerable<BuildError> Errors
        {
            get
            {
                if (Assemblies != null)
                    return Assemblies.SelectMany(ba => ba.Errors);
                else
                    return Enumerable.Empty<BuildError>();
            }
        }
    }
}
