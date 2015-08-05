using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Compile
{
    public class BuildError
    {
        public string Message { get; set; }

        public BuildError(string message)
        {
            Message = message;
        }
    }
}
