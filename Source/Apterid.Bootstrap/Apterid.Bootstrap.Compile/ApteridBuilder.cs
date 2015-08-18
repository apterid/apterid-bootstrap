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
    public class ApteridBuilder
    {
        public BuildContext Context { get; protected set; }

        public ApteridBuilder()
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
                .Select(sourcePath => new Tasks.ParseSourceFile(Context, buildAssembly, sourcePath, force, cancelSource.Token));

            var parseAll = new BuildTask(parseTasks, cancelSource.Token);

            // analyze
            
        }
    }
}
