using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Apterid.Bootstrap.Parse;

namespace Apterid.Bootstrap.Compile
{
    public class BuildAssembly
    {
        public Options Options { get; set; }
        public FileInfo OutputFileInfo { get; set; }
        public IList<BuildError> Errors { get; set; }

        public IList<SourceText> Sources { get; set; }
        public Analyze.Assembly AnalyzeAssembly { get; set; }

        public void AddError(string message)
        {
            Errors.Add(new BuildError { Message = message });
        }

        public void AddError(BuildError error)
        {
            Errors.Add(error);
        }
    }
}
