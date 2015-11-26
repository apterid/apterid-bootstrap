using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Apterid.Bootstrap.Common;
using Apterid.Bootstrap.Generate;

namespace Apterid.Bootstrap.Compile.Steps
{
    class GenerateAssembly : CompilerStep<CompilationUnit>
    {
        public GenerateAssembly(CompilerContext context, CompilationUnit compilationUnit)
            : base(context, compilationUnit)
        {
        }

        public override Action GetStepAction(CancellationToken cancel)
        {
            return () =>
            {
                try
                {
                    var saveToFile = (Unit.Mode & CompileOutputMode.SaveToFile) != 0;
                    var emitSymbols = (Unit.Mode & CompileOutputMode.EmitSymbols) != 0;
                    var optimize = (Unit.Mode & CompileOutputMode.Optimize) != 0;

                    if (saveToFile && !Unit.OutputFileInfo.Directory.Exists)
                    {
                        Unit.AddError(new CompilerError { Message = ErrorMessages.E_0016_Compiler_InvalidOutputDir });
                        saveToFile = false;
                    }

                    var access = saveToFile ? AssemblyBuilderAccess.RunAndSave : AssemblyBuilderAccess.Run;
                    var assemblyName = GetAssemblyName();

                    if (Unit.GenerationUnit.AssemblyBuilder == null)
                    {
                        lock (Unit)
                        {
                            if (Unit.GenerationUnit.AssemblyBuilder == null)
                            {
                                var assemblyBuilder = Unit.GenerationUnit.AssemblyBuilder = saveToFile
                                    ? AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, access, Unit.OutputFileInfo.Directory.FullName)
                                    : AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, access);
                                Unit.GenerationUnit.ModuleBuilder = null;

                                if (emitSymbols)
                                {
                                    var modes = DebuggableAttribute.DebuggingModes.Default;
                                    if (!optimize)
                                        modes |= DebuggableAttribute.DebuggingModes.DisableOptimizations;

                                    var ctor = typeof(DebuggableAttribute).GetConstructor(new[] { typeof(DebuggableAttribute.DebuggingModes) });
                                    var attb = new CustomAttributeBuilder(ctor, new object[] { modes });
                                    assemblyBuilder.SetCustomAttribute(attb);
                                }
                            }
                        }
                    }

                    if (Unit.GenerationUnit.ModuleBuilder == null)
                    {
                        lock (Unit)
                        {
                            if (Unit.GenerationUnit.ModuleBuilder == null)
                            {
                                var moduleBuilder = Unit.GenerationUnit.ModuleBuilder = saveToFile
                                    ? Unit.GenerationUnit.AssemblyBuilder.DefineDynamicModule(assemblyName.Name, Unit.OutputFileInfo.Name, emitSymbols)
                                    : Unit.GenerationUnit.AssemblyBuilder.DefineDynamicModule(assemblyName.Name, emitSymbols);

                                if (emitSymbols)
                                {
                                    foreach (var sf in Unit.SourceFiles)
                                    {
                                        Unit.GenerationUnit.SymbolDocs[sf.Name] = moduleBuilder.DefineDocument(sf.Name,
                                            SourceFile.ApteridLanguageId, SourceFile.ApteridLanguageVendorId, SourceFile.ApteridLanguageSourceFileId);
                                    }
                                }
                            }
                        }
                    }

                    var generators = Unit.AnalysisUnit.Modules.Values
                        .Select(m => Task.Factory.StartNew(new GenerateModule(Context, Unit.GenerationUnit, m)
                                                               .GetStepAction(Context.CancelSource.Token),
                                                           TaskCreationOptions.AttachedToParent))
                        .ToArray();

                    Task.WaitAll(generators);

                    if ((Unit.Mode & CompileOutputMode.SaveToFile) != 0)
                    {
                        var emit = Task.Factory.StartNew(new EmitAssembly(Context, Unit)
                                                             .GetStepAction(Context.CancelSource.Token), 
                                                         TaskCreationOptions.AttachedToParent);
                        emit.Wait();
                    }
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    Unit.AddError(new GeneratorError { Exception = e });
                }
            };
        }

        AssemblyName GetAssemblyName()
        {
            if (!string.IsNullOrWhiteSpace(Unit.OutputName))
                return new AssemblyName(Unit.OutputName);
            if (Unit.OutputFileInfo != null)
                return new AssemblyName(Path.GetFileNameWithoutExtension(Unit.OutputFileInfo.FullName));
            return new AssemblyName(string.Format("Apterid Generated Assembly {0}: {1}", Guid.NewGuid(), DateTime.UtcNow.ToString("u")));
        }
    }
}
