using System;
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
        public CompilerContext Context { get; }

        public ApteridCompiler(bool forceRecompile = false)
        {
            Context = new CompilerContext
            {
                ForceRecompile = forceRecompile,
            };
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
                throw new InternalException(string.Format(ErrorMessages.E_0008_Compiler_DuplicateOutputFileInfo, outputFileInfo.FullName));

            var unit = new CompilationUnit
            {
                Mode = mode,
                OutputFileInfo = outputFileInfo,
                SourceFiles = sources.ToList(),
            };

            Context.CompileUnits.Add(unit);
        }

        public Task UpdateAllCompileUnitsAsync()
        {
            var tasks = Context.CompileUnits
                .Select(unit => new Steps.Compile(Context, unit).RunAsync(Context.CancelSource.Token))
                .ToArray();

            return Task.WhenAll(tasks);
        }
    }

    public class CompilerError : ApteridError
    {
    }
}
