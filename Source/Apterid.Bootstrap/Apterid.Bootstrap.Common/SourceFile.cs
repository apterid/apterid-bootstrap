using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Common
{
    public abstract class SourceFile
    {
        public string Name { get; protected set; }
        public virtual bool Exists { get; }
        public virtual DateTime LastWriteTimeUtc { get; }

        public abstract IEnumerable<char> Buffer { get; }
    }
}
