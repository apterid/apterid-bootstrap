using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Common
{
    public abstract class ApteridError
    {
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}
