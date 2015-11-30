// Copyright (C) 2015 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Apterid.Bootstrap.Analyze;
using Apterid.Bootstrap.Common;
using Apterid.Bootstrap.Parse;

namespace Apterid.Bootstrap.Generate
{
    public class GenerationUnit : Unit
    {
        public CompileOutputMode Mode { get; set; }

        public IList<ParseUnit> ParseUnits { get; set; }
        public AnalysisUnit AnalysisUnit { get; set; }

        public AssemblyBuilder AssemblyBuilder { get; set; }
        public ModuleBuilder ModuleBuilder { get; set; }
        public IDictionary<string, ISymbolDocumentWriter> SymbolDocs { get; } = new Dictionary<string, ISymbolDocumentWriter>();

        public override IEnumerable<Unit> Children
        {
            get
            {
                if (AnalysisUnit != null)
                    return new[] { AnalysisUnit };
                else if (ParseUnits != null)
                    return ParseUnits;
                else
                    return Enumerable.Empty<Unit>();
            }
        }
    }
}
