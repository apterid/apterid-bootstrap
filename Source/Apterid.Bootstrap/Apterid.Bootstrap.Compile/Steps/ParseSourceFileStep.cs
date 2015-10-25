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
        public ParserSourceFile SourceFile { get; }

        public ParseSourceFileStep(CompileContext context, CompileUnit compileUnit, ParserSourceFile sourceFile)
            : base(context, compileUnit)
        {
            SourceFile = sourceFile;
        }

        public override StepResult Run()
        {
            // verify that the source file exists
            if (!SourceFile.Exists)
            {
                CompileUnit.AddError<ParseError>(
                    string.Format(ErrorMessages.EB_0006_Compiler_SourceDoesNotExist, SourceFile.Name));
                return Failed();
            }

            // if the source file is older than the output file, do nothing
            if (!Context.ForceRecompile
                && CompileUnit.OutputFileInfo.Exists
                && CompileUnit.OutputFileInfo.LastWriteTimeUtc >= SourceFile.LastWriteTimeUtc)
            {
                return Succeeded();
            }

            // parse
            try
            {
                var parser = new ApteridParser(handle_left_recursion: true);
                parser.SourceFile = SourceFile;
                SourceFile.Parser = parser;

                var match = parser.GetMatch(SourceFile.Buffer, parser.ApteridSource);

                if (match.Success)
                {
                    // check for error sections
                    SourceFile.ParseTree = match.Result;

                    var errorSections = SourceFile.GetNodes<Parse.Syntax.ErrorSection>();
                    foreach (var es in errorSections)
                    {
                        var error = new ParseError
                        {
                            SourceFile = SourceFile,
                            Message = ErrorMessages.EP_0007_Parser_SyntaxError,
                            ErrorNode = es,
                            ErrorIndex = es.StartIndex
                        };
                        CompileUnit.AddError(error);
                    }

                    return CompileUnit.Errors.Any() ? Failed() : Succeeded();
                }
                else
                {
                    var error = new ParseError
                    {
                        SourceFile = SourceFile,
                        Message = match.Error,
                        ErrorIndex = match.ErrorIndex
                    };

                    CompileUnit.AddError(error);
                    return Failed();
                }
            }
            catch (Exception e)
            {
                CompileUnit.AddError(new ParseError { Exception = e });
                return Failed();
            }
        }
    }
}
