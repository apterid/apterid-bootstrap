using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apterid.Bootstrap.Common;
using Apterid.Bootstrap.Parse;

namespace Apterid.Bootstrap.Compile.Steps
{
    public class ParseSourceFile : CompilerStep
    {
        public ParserSourceFile SourceFile { get; }

        public ParseSourceFile(CompilerContext context, CompilationUnit compileUnit, ParserSourceFile sourceFile)
            : base(context, compileUnit)
        {
            SourceFile = sourceFile;
        }

        public override StepResult Run()
        {
            // verify that the source file exists
            if (!SourceFile.Exists)
            {
                Unit.AddError<ParsingError>(string.Format(ErrorMessages.E_0006_Compiler_SourceDoesNotExist, SourceFile.Name));
                return Failed();
            }

            if (Context.CancelSource.IsCancellationRequested)
                return Canceled();

            // if the source file is older than the output file, do nothing
            if (!Context.ForceRecompile
                && Unit.OutputFileInfo.Exists
                && Unit.OutputFileInfo.LastWriteTimeUtc >= SourceFile.LastWriteTimeUtc)
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

                if (Context.CancelSource.IsCancellationRequested)
                    return Canceled();

                if (match.Success)
                {
                    // check for error sections
                    SourceFile.ParseTree = match.Result;

                    var errorSections = SourceFile.GetNodes<Parse.Syntax.ErrorSection>();
                    foreach (var es in errorSections)
                    {
                        var error = new ParsingError
                        {
                            SourceFile = SourceFile,
                            Message = ErrorMessages.E_0007_Parser_SyntaxError,
                            ErrorNode = es,
                            ErrorIndex = es.StartIndex
                        };
                        Unit.AddError(error);
                    }

                    return Unit.Errors.Any() ? Failed() : Succeeded();
                }
                else
                {
                    var error = new ParsingError
                    {
                        SourceFile = SourceFile,
                        Message = match.Error,
                        ErrorIndex = match.ErrorIndex
                    };

                    Unit.AddError(error);
                    return Failed();
                }
            }
            catch (Exception e)
            {
                Unit.AddError(new ParsingError { Exception = e });
                return Failed();
            }
        }
    }
}
