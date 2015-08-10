using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Compile
{
    public class BuildAssembly
    {
        public Options Options { get; set; }
        public FileInfo OutputFileInfo { get; set; }
        public IList<BuildError> Errors { get; set; }

        public Analyze.Assembly StructureAssembly { get; set; }

        public void AddError(string message)
        {
            Errors.Add(new BuildError(message));
        }
    }
}
