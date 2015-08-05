using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Compile
{
    public class BuildContext
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

    public class BuildAssembly
    {
        public Options Options { get; set; }
        public FileInfo OutputFileInfo { get; set; }
        public IList<BuildError> Errors { get; set; }

        public Structure.Assembly StructureAssembly { get; set; }

        public void AddError(string message)
        {
            Errors.Add(new BuildError(message));
        }
    }
}
