﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Apterid.Bootstrap.Analyze;
using Apterid.Bootstrap.Common;
using Apterid.Bootstrap.Compile.Steps;
using Apterid.Bootstrap.Parse;

namespace Apterid.Bootstrap.Compile
{
    public class ApteridCompiler
    {
        public CompileContext Context { get; }

        public ApteridCompiler(bool forceRecompile = false)
        {
            Context = new CompileContext
            {
                ForceRecompile = forceRecompile,
                Compiler = this
            };
            Context.Analyzer = new ApteridAnalyzer(Context);
        }

        public void AddReference(string dllPath)
        {
            throw new NotImplementedException();
        }

        public void AddCompileUnit(
            OutputMode mode, 
            FileInfo outputFileInfo, 
            IEnumerable<ParserSourceFile> sources)
        {
            if (Context.CompileUnits.Any(u => u.OutputFileInfo == outputFileInfo))
                throw new InternalException(
                    string.Format(ErrorMessages.EC_0008_Compiler_DuplicateOutputFileInfo, outputFileInfo.FullName));

            var unit = new CompileUnit
            {
                Mode = mode,
                OutputFileInfo = outputFileInfo,
                SourceFiles = sources.ToList(),
            };

            Context.CompileUnits.Add(unit);
        }

        public StepStatus UpdateAllCompileUnits()
        {
            var compileStep = new CompilerStep(Context)
            {
                SubSteps = Context.CompileUnits
                    .Select(a => new CompileStep(Context, a))
                    .OfType<CompilerStep>()
                    .ToList()
            };

            return compileStep.Run().Status;
        }
    }
}
