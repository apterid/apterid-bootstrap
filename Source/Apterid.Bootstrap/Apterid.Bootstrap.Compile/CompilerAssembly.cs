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
    public class CompilerAssembly
    {
        IList<ApteridError> errors = new List<ApteridError>();

        public FileInfo OutputFileInfo { get; set; }
        public OutputMode Mode { get; set; }

        public IList<ParserSourceFile> SourceFiles { get; set; }
        public Analyze.AnalyzerAssembly AnalyzedAssembly { get; set; }

        public IList<ApteridError> Errors { get { return errors; } }

        public void AddError(string message)
        {
            Errors.Add(new ApteridError { Message = message });
        }

        public void AddError(ApteridError error)
        {
            Errors.Add(error);
        }
    }
}
