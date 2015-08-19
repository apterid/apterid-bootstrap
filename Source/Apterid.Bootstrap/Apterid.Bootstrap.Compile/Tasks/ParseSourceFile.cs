using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Apterid.Bootstrap.Common;
using Apterid.Bootstrap.Parse;

namespace Apterid.Bootstrap.Compile.Tasks
{
    public class ParseSourceFile : CompileTask
    {
        SourceFile sourceFile;

        public ParseSourceFile(CompileContext context, CompileAssembly assembly, CancellationTokenSource cancelSource, SourceFile sourceFile)
            : base(context, assembly, cancelSource)
        {
            this.sourceFile = sourceFile;
        }

        public ParseSourceFile(CompileTask parent, SourceFile sourceFile)
            : base(parent)
        {
            this.sourceFile = sourceFile;
        }

        public override Task<CompileStatus> OnProcess()
        {
            // verify that the source file exists
            if (!sourceFile.Exists)
            {
                Assembly.AddError(string.Format(ErrorMessages.EB_0006_Compiler_SourceDoesNotExist, sourceFile.Name));
                return Task.FromResult(CompileStatus.Failed);
            }

            // see if we actually need to build the file
            if (!Context.ForceRecompile
                && Assembly.OutputFileInfo.Exists 
                && Assembly.OutputFileInfo.LastWriteTimeUtc >= sourceFile.LastWriteTimeUtc)
            {
                return Task.FromResult(CompileStatus.Completed);
            }

            // parse
            try
            {
                var parser = new ApteridParser(handle_left_recursion: true)
                {
                    SourceFile = sourceFile
                };

                var match = parser.GetMatch(sourceFile.Buffer, parser.ApteridSource);

                if (match.Success)
                {
                    sourceFile.ParseTree = match.Result;

                    foreach (var errorSection in sourceFile.GetNodes<Parse.Syntax.ErrorSection>())
                    {
                        var error = new ApteridError
                        {
                            SourceFile = sourceFile,
                            Message = ErrorMessages.EP_0007_Parser_SyntaxError,
                            ErrorNode = errorSection,
                            ErrorIndex = errorSection.StartIndex
                        };
                        Assembly.AddError(error);
                    }

                    return Assembly.Errors.Any()
                        ? Task.FromResult(CompileStatus.Failed)
                        : Task.FromResult(CompileStatus.Completed);
                }
                else
                {
                    var error = new ApteridError
                    {
                        SourceFile = sourceFile,
                        Message = match.Error,
                        ErrorIndex = match.ErrorIndex
                    };

                    Assembly.AddError(error);
                    return Task.FromResult(CompileStatus.Failed);
                }
            }
            catch (Exception e)
            {
                Assembly.AddError(new ApteridError { Exception = e });
                return Task.FromResult(CompileStatus.Failed);
            }
        }
    }
}
