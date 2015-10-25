using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apterid.Bootstrap.Common;
using Apterid.Bootstrap.Parse;

namespace Apterid.Bootstrap.Compile.Steps
{
    public class ParseSourceFileStep : CompilerStep
    {
        public CompilerAssembly Assembly { get; }
        public ParserSourceFile SourceFile { get; }

        public ParseSourceFileStep(
            CompilerContext context, 
            CompilerAssembly assembly,
            ParserSourceFile sourceFile)
            : base(context)
        {
            Assembly = assembly;
            SourceFile = sourceFile;
        }

        public override StepResult Run()
        {
            // verify that the source file exists
            if (!SourceFile.Exists)
            {
                Assembly.AddError(string.Format(ErrorMessages.EB_0006_Compiler_SourceDoesNotExist, SourceFile.Name));
                return Failed();
            }

            // if the source file is older than the output file, do nothing
            if (!Context.ForceRecompile
                && Assembly.OutputFileInfo.Exists
                && Assembly.OutputFileInfo.LastWriteTimeUtc >= SourceFile.LastWriteTimeUtc)
            {
                return Succeeded();
            }

            // parse
            try
            {
                var parser = new ApteridParser(handle_left_recursion: true)
                {
                    SourceFile = SourceFile,
                };

                var match = parser.GetMatch(SourceFile.Buffer, parser.ApteridSource);

                if (match.Success)
                {
                    // check for error sections
                    SourceFile.ParseTree = match.Result;

                    var errorSections = SourceFile.GetNodes<Parse.Syntax.ErrorSection>();
                    foreach (var es in errorSections)
                    {
                        var error = new ApteridError
                        {
                            SourceFile = SourceFile,
                            Message = ErrorMessages.EP_0007_Parser_SyntaxError,
                            ErrorNode = es,
                            ErrorIndex = es.StartIndex
                        };
                        Assembly.AddError(error);
                    }

                    return Assembly.Errors.Any() ? Failed() : Succeeded();
                }
                else
                {
                    var error = new ApteridError
                    {
                        SourceFile = SourceFile,
                        Message = match.Error,
                        ErrorIndex = match.ErrorIndex
                    };

                    Assembly.AddError(error);
                    return Failed();
                }
            }
            catch (Exception e)
            {
                Assembly.AddError(new ApteridError { Exception = e });
                return Failed();
            }
        }
    }
}
