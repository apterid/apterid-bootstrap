// Copyright (C) 2015 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
        public CompileOutputMode Mode { get; set; }
        public FileInfo OutputFileInfo { get; set; }
        public string OutputName { get; set; }

        public IList<ParsedSourceFile> SourceFiles { get; set; }
        public IList<ParseUnit> ParseUnits { get; set; }
        public AnalysisUnit AnalysisUnit { get; set; }
        public GenerationUnit GenerationUnit { get; set; }

        public override IEnumerable<Unit> Children => GetChildren();

        IEnumerable<Unit> GetChildren()
        {
            if (ParseUnits != null)
                foreach (var parseUnit in ParseUnits)
                    yield return parseUnit;
            if (AnalysisUnit != null)
                yield return AnalysisUnit;
            if (GenerationUnit != null)
                yield return GenerationUnit;
        }
    }
}
