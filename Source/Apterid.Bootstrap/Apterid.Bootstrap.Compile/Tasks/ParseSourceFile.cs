using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Apterid.Bootstrap.Common;

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



            return Task.FromResult(BuildStatus.Completed);
        }
    }
}
