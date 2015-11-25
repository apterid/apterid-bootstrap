﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apterid.Bootstrap.Analyze;
using Apterid.Bootstrap.Common;
using Apterid.Bootstrap.Generate;
using Apterid.Bootstrap.Parse;

namespace Apterid.Bootstrap.Compile
{
    public class CompilationUnit : Unit
    {
        public OutputMode Mode { get; internal set; }
        public FileInfo OutputFileInfo { get; internal set; }

        public IList<ParserSourceFile> SourceFiles { get; set; }
        public IList<ParseUnit> ParseUnits { get; set; }
        public AnalysisUnit AnalysisUnit { get; set; }
        public GenerationUnit GenerationUnit { get; set; }

        public override IEnumerable<Unit> Children
        {
            get
            {
                if (GenerationUnit != null)
                    return new[] { GenerationUnit };
                else if (AnalysisUnit != null)
                    return new[] { AnalysisUnit };
                else if (ParseUnits != null)
                    return ParseUnits;
                else
                    return Enumerable.Empty<Unit>();
            }
        }
    }
}
