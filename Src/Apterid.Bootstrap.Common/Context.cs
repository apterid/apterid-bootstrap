// Copyright (C) 2016 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Common
{
    [Flags]
    public enum CompileOutputMode
    {
        Parse       = 1 << 0,
        Analyze     = 1 << 1,
        Generate    = 1 << 2,
        Library     = 1 << 3,
        Executable  = 1 << 4,
        EmitSymbols = 1 << 5,
        Optimize    = 1 << 6,

        SaveToFile = Library | Executable,

        CompileLibrary = Parse | Analyze | Generate | Library,
        CompileExecutable = Parse | Analyze | Generate | Executable,
    }

    public class Context
    {
        public bool ForceRecompile { get; set; }
        public bool AbortOnError { get; set; }

        public IDictionary<string, SourceFile> Sources { get; } = new Dictionary<string, SourceFile>();
        public IDictionary<string, Reference> References { get; } = new Dictionary<string, Reference>();
    }
}
