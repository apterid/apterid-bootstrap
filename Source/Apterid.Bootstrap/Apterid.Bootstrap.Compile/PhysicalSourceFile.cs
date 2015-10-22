using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apterid.Bootstrap.Parse;

namespace Apterid.Bootstrap.Compile
{
    public class PhysicalSourceFile : ParserSourceFile
    {
        FileInfo fileInfo;
        string buffer;

        public PhysicalSourceFile(string fname)
        {
            fileInfo = new FileInfo(fname);
            Name = fileInfo.FullName;
        }

        public override bool Exists { get { return fileInfo.Exists; } }
        public override DateTime LastWriteTimeUtc { get { return fileInfo.LastWriteTimeUtc; } }

        public override IEnumerable<char> Buffer
        {
            get
            {
                if (buffer == null)
                    buffer = File.ReadAllText(fileInfo.FullName);
                return buffer;
            }
        }
    }
}
