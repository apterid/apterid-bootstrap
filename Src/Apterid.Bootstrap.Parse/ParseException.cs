using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apterid.Bootstrap.Common;

namespace Apterid.Bootstrap.Parse
{
    public class ParseException : ApteridException
    {
        public NodeError Error { get; }

        public ParseException(NodeError error)
            : base(ErrorCode.Parser, error.Message)
        {
            Error = error;
        }
    }
}
