using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apterid.Bootstrap.Parse;

namespace Apterid.Bootstrap.Compile
{
    public class CompileContext : Common.Context
    {
        public IList<CompileAssembly> Assemblies { get; set; }

        public IEnumerable<ApteridError> Errors
        {
            get
            {
                if (Assemblies != null)
                    return Assemblies.SelectMany(ba => ba.Errors);
                else
                    return Enumerable.Empty<ApteridError>();
            }
        }
    }
}
