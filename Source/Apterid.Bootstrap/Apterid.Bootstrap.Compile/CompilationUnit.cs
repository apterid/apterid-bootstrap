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
    public class CompilationUnit : Unit
    {
        public OutputMode Mode { get; internal set; }
        public FileInfo OutputFileInfo { get; internal set; }

        public IList<ParserSourceFile> SourceFiles { get; internal set; }
        public AnalysisUnit AnalyzeUnit { get; internal set; }
    }
}
