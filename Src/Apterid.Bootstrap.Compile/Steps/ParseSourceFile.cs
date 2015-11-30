// Copyright (C) 2015 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Apterid.Bootstrap.Common;
using Apterid.Bootstrap.Parse;

namespace Apterid.Bootstrap.Compile.Steps
{
    public class ParseSourceFile : CompilerStep<ParseUnit>
    {
        public ParseSourceFile(CompilerContext context, ParseUnit parseUnit)
            : base(context, parseUnit)
        {
        }

        public override Action GetStepAction(CancellationToken cancel)
        {
            return () =>
            { 
                var sourceFile = Unit.SourceFile;

                // verify that the source file exists
                if (!sourceFile.Exists)
                {
                    Unit.AddError<NodeError>(string.Format(ErrorMessages.E_0006_Compiler_InvalidSourceFile, sourceFile.Name));

                    if (Context.AbortOnError)
                        return;
                }

                if (cancel.IsCancellationRequested)
                    throw new OperationCanceledException(cancel);

                // parse
                try
                {
                    if (Unit.Parser == null)
                    {
                        lock (this)
                        {
                            if (Unit.Parser == null)
                                Unit.Parser = new ApteridParser(handle_left_recursion: true);
                        }
                    }

                    var parser = Unit.Parser;
                    parser.SourceFile = sourceFile;
                    sourceFile.Parser = parser;

                    var result = parser.GetMatch(sourceFile.Buffer, parser.ApteridSource);
                    sourceFile.MatchResult = result;
                    sourceFile.MatchState = result.MatchState;

                    if (cancel.IsCancellationRequested)
                        throw new OperationCanceledException(cancel);

                    if (result.Success)
                    {
                        // check for error sections
                        sourceFile.ParseTree = result.Result;

                        var errorSections = sourceFile.GetNodes<Parse.Syntax.ErrorSection>();
                        foreach (var es in errorSections)
                        {
                            var error = new NodeError
                            {
                                SourceFile = sourceFile,
                                Message = ErrorMessages.E_0007_Parser_SyntaxError,
                                ErrorNode = es,
                                ErrorIndex = es.StartIndex
                            };
                            Unit.AddError(error);
                        }
                    }
                    else
                    {
                        var error = new NodeError
                        {
                            SourceFile = sourceFile,
                            Message = result.Error,
                            ErrorIndex = result.ErrorIndex
                        };

                        Unit.AddError(error);
                    }
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    Unit.AddError(new NodeError { Exception = e });
                }
            };
        }
    }
}
