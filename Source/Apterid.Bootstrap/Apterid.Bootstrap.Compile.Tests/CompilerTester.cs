using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apterid.Bootstrap.Parse;

namespace Apterid.Bootstrap.Compile.Tests
{
    class CompilerTester : IDisposable
    {
        FileInfo[] sourceInfo;

        public CompilerAssembly Assembly { get; }
        public CompilerContext Context { get; }
        public ApteridCompiler Compiler { get; }

        public CompilerTester(string name, params string[] sources)
        {
            var apath = Path.Combine(Path.GetTempPath(), name + ".DLL");

            sourceInfo = sources.Select(s =>
                {
                    var spath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".DLL");
                    File.WriteAllText(spath, s);
                    return new FileInfo(spath);
                })
                .ToArray();

            Assembly = new CompilerAssembly
            {
                OutputFileInfo = new FileInfo(apath),
                SourceFiles = sourceInfo
                    .Select(info => new PhysicalSourceFile(info.FullName))
                    .OfType<ParserSourceFile>()
                    .ToList(),
            };

            Context = new CompilerContext
            {
                ForceRecompile = true,
                Assemblies = new List<CompilerAssembly> { Assembly, },
            };

            Compiler = new ApteridCompiler(Context);
        }

        public void Dispose()
        {
            if (Assembly != null && Assembly.OutputFileInfo != null && Assembly.OutputFileInfo.Exists)
            {
                Assembly.OutputFileInfo.Delete();
            }

            if (sourceInfo != null)
            {
                try
                {
                    foreach (var info in sourceInfo)
                    {
                        if (info.Exists)
                            info.Delete();
                    }
                }
                finally
                {
                    sourceInfo = null;
                }
            }
        }
    }
}
