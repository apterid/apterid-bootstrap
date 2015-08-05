using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Apterid.Bootstrap.Common;

namespace Apterid.Bootstrap.Compile
{
    class Builder
    {
        public BuildContext Context { get; protected set; }

        public Builder()
        {
            Context = new BuildContext();
        }

        public void UpdateAssembly(BuildAssembly buildAssembly, bool force = false)
        {
            var cancelSource = new CancellationTokenSource();

            // output file info
            var outputFileInfo = new FileInfo(buildAssembly.Options.OutputPath);

            // parse files
            var parseTasks = buildAssembly.Options.Sources
                .Select(sourcePath => 
                    new BuildTask(() => 
                        ParseSourceFile(buildAssembly, outputFileInfo, sourcePath, force), 
                        cancelSource.Token));

            var parse = new BuildTask(parseTasks, cancelSource.Token);

            //
            
        }

        BuildStatus ParseSourceFile(
            BuildAssembly buildAssembly, 
            FileInfo outputFileInfo, 
            string sourcePath, bool force)
        {
            // verify that the source file exists
            var sourceInfo = new FileInfo(sourcePath);
            if (!sourceInfo.Exists)
            {
                buildAssembly.AddError(string.Format(ErrorMessages.EB_0006_Builder_SourceDoesNotExist, sourceInfo.FullName));
                return BuildStatus.Completed;
            }

            // see if it already exists in our assembly

            return BuildStatus.Completed;
        }
    }
}
