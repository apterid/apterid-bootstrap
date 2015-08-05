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
    class ParseSourceFile : BuildTask
    {
        public BuildContext Context { get; set; }
        public BuildAssembly BuildAssembly { get; set; }
        public FileInfo SourceFileInfo { get; set; }

        bool force;

        public ParseSourceFile(string sourcePath, bool force, CancellationToken cancel)
            : base(cancel)
        {
            SourceFileInfo = new FileInfo(sourcePath);
            this.force = force;
        }

        public override Task<BuildStatus> OnProcess()
        {
            // verify that the source file exists
            if (!SourceFileInfo.Exists)
            {
                BuildAssembly.AddError(string.Format(ErrorMessages.EB_0006_Builder_SourceDoesNotExist, SourceFileInfo.FullName));
                return Task.FromResult(BuildStatus.Completed);
            }



            return Task.FromResult(BuildStatus.Completed);
        }
    }
}
