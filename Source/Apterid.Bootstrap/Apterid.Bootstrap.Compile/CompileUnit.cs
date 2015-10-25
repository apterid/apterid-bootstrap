using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Apterid.Bootstrap.Analyze;
using Apterid.Bootstrap.Common;
using Apterid.Bootstrap.Parse;

namespace Apterid.Bootstrap.Compile
{
    public class CompileUnit
    {
        public OutputMode Mode { get; internal set; }
        public FileInfo OutputFileInfo { get; internal set; }

        public IList<ParserSourceFile> SourceFiles { get; internal set; }
        public AnalyzeUnit AnalyzeUnit { get; internal set; }

        IList<ApteridError> errors = new List<ApteridError>();
        public IList<ApteridError> Errors { get { return errors; } }

        public void AddError<T>(string message)
            where T : ApteridError, new()
        {
            Errors.Add(new T { Message = message });
        }

        public void AddError(ApteridError error)
        {
            Errors.Add(error);
        }
    }
}
