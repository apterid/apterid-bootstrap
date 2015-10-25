using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apterid.Bootstrap.Analyze;
using Apterid.Bootstrap.Common;
using Apterid.Bootstrap.Parse;

namespace Apterid.Bootstrap.Compile.Tests
{
    class CompilerTester : IDisposable
    {
        FileInfo[] sourceInfos;

        public ApteridCompiler Compiler { get; }

        public CompilerTester(string name, params string[] sources)
        {
            var outputPath = Path.Combine(Path.GetTempPath(), name + ".DLL");

            sourceInfos = sources.Select(s =>
                {
                    var spath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".apterid");
                    File.WriteAllText(spath, s);
                    return new FileInfo(spath);
                })
                .ToArray();

            Compiler = new ApteridCompiler(forceRecompile: true);

            Compiler.AddCompileUnit(
                OutputMode.Library,
                new FileInfo(outputPath),
                sourceInfos.Select(info => new PhysicalSourceFile(info.FullName)));
        }

        public void Dispose()
        {
            if (Compiler != null)
            {
                foreach (var unit in Compiler.Context.CompileUnits)
                {
                    if (unit.OutputFileInfo != null && unit.OutputFileInfo.Exists)
                        unit.OutputFileInfo.Delete();
                }
            }

            if (sourceInfos != null)
            {
                try
                {
                    foreach (var info in sourceInfos)
                    {
                        if (info.Exists)
                            info.Delete();
                    }
                }
                finally
                {
                    sourceInfos = null;
                }
            }
        }
    }
}
