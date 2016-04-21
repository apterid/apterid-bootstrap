// Copyright (C) 2016 The Apterid Developers - See LICENSE

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
            var tempPath =
#if DEBUG
                ".";
#else
                Path.GetTempPath();
#endif

            var nonce = "";
#if !DEBUG
            nonce = "_" + Guid.NewGuid().ToString("D");
#endif

            var outputPath = Path.Combine(tempPath, string.Format("{0}{1}.dll", name, nonce));

            sourceInfos = sources.Select((s, i) =>
                {

                    var spath = Path.Combine(tempPath, string.Format("{0}{1}_{2}.apterid", name, nonce, i));
                    File.WriteAllText(spath, s);
                    return new FileInfo(spath);
                })
                .ToArray();

            Compiler = new ApteridCompiler(forceRecompile: true);
            Compiler.Context.References.Add("mscorlib.dll", new Reference { Assembly = typeof(int).Assembly });
            Compiler.Context.References.Add("System.Numerics.dll", new Reference { Assembly = typeof(System.Numerics.BigInteger).Assembly });

            Compiler.AddCompileUnit(
                CompileOutputMode.CompileLibrary | CompileOutputMode.EmitSymbols, 
                new FileInfo(outputPath),
                sourceInfos.Select(info => new PhysicalSourceFile(info.FullName)));
        }

        public void Dispose()
        {
            if (Compiler != null)
            {
                foreach (var unit in Compiler.Context.CompileUnits)
                {
#if !DEBUG
                    if (unit.OutputFileInfo != null && unit.OutputFileInfo.Exists)
                        unit.OutputFileInfo.Delete();

                    var dir = Path.GetDirectoryName(unit.OutputFileInfo.FullName);
                    var name = Path.GetFileNameWithoutExtension(unit.OutputFileInfo.FullName);
                    var pdb = new FileInfo(Path.Combine(dir, name + ".pdb"));
                    if (pdb.Exists)
                        pdb.Delete();
#endif
                }
            }

            if (sourceInfos != null)
            {
                try
                {
                    foreach (var info in sourceInfos)
                    {
#if !DEBUG
                        if (info.Exists)
                            info.Delete();
#endif
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
