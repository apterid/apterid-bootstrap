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
    public class ParseSourceFile : BuildTask
    {
        public BuildContext Context { get; set; }
        public BuildAssembly BuildAssembly { get; set; }
        public FileInfo SourceFileInfo { get; set; }

        bool force;

        public ParseSourceFile(BuildContext context, BuildAssembly buildAssembly, string sourcePath, bool force, CancellationToken cancel)
            : base(cancel)
        {
            this.Context = context;
            this.BuildAssembly = buildAssembly;
            this.SourceFileInfo = new FileInfo(sourcePath);
            this.force = force;
        }

        public override Task<BuildStatus> OnProcess()
        {
            // verify that the source file exists
            if (!SourceFileInfo.Exists)
            {
                BuildAssembly.AddError(string.Format(ErrorMessages.EB_0006_Builder_SourceDoesNotExist, SourceFileInfo.FullName));
                return Task.FromResult(BuildStatus.Failed);
            }

            // see if we actually need to build the file
            if (BuildAssembly.Options.ForceRecompile 
                || !BuildAssembly.OutputFileInfo.Exists
                || BuildAssembly.OutputFileInfo.LastWriteTimeUtc < SourceFileInfo.LastWriteTimeUtc)
            {
                return Task.FromResult(BuildStatus.Completed);
            }

            // parse
            try
            {
                var sourceText = new SourceText(SourceFileInfo.FullName, File.ReadAllText(SourceFileInfo.FullName));
                BuildAssembly.Sources.Add(sourceText);

                var parser = new ApteridParser(handle_left_recursion: true)
                {
                    SourceText = sourceText
                };

                var match = parser.GetMatch(sourceText.Buffer, parser.ApteridSource);

                if (match.Success)
                {
                    sourceText.ParseTree = match.Result;



                }
                else
                {
                    var error = new BuildError
                    {
                        SourceText = sourceText,
                        Message = match.Error,
                        ErrorIndex = match.ErrorIndex
                    };

                    BuildAssembly.AddError(error);
                    return Task.FromResult(BuildStatus.Failed);
                }
            }
            catch (Exception e)
            {
                BuildAssembly.AddError(new BuildError { Exception = e });
                return Task.FromResult(BuildStatus.Failed);
            }
        }
    }
}
