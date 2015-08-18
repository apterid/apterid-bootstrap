using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Apterid.Bootstrap.Common;
using Apterid.Bootstrap.Parse;

namespace Apterid.Bootstrap.Compile
{
    public class CompileAssembly
    {
        IList<CompileError> errors = new List<CompileError>();

        public FileInfo OutputFileInfo { get; set; }
        public OutputMode Mode { get; set; }

        public IList<SourceFile> SourceFiles { get; set; }
        public Analyze.Assembly AnalyzedAssembly { get; set; }

        public IList<CompileError> Errors { get { return errors; } }

        public void AddError(string message)
        {
            Errors.Add(new CompileError { Message = message });
        }

        public void AddError(CompileError error)
        {
            Errors.Add(error);
        }
    }
}
