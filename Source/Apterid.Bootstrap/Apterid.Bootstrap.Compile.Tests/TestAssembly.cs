using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apterid.Bootstrap.Parse;

namespace Apterid.Bootstrap.Compile.Tests
{
    class TestAssembly : IDisposable
    {
        FileInfo[] sourceInfo;

        public CompileAssembly Assembly { get; private set; }
        public CompileContext Context { get; private set; }
        public ApteridCompiler Compiler { get; private set; }

        public TestAssembly(string name, params string[] sources)
        {
            var apath = Path.Combine(Path.GetTempPath(), name + ".DLL");

            sourceInfo = sources.Select(s =>
                {
                    var spath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".DLL");
                    File.WriteAllText(spath, s);
                    return new FileInfo(spath);
                })
                .ToArray();

            Assembly = new CompileAssembly
            {
                OutputFileInfo = new FileInfo(apath),
                SourceFiles = sourceInfo
                    .Select(info => new PhysicalSourceFile(info.FullName))
                    .OfType<SourceFile>()
                    .ToList(),
            };

            Context = new CompileContext
            {
                ForceRecompile = true,
                Assemblies = new List<CompileAssembly> { Assembly, },
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
