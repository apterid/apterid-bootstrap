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

        public ParseException(Syntax.NodeArgs args, string message)
            : base(ErrorCode.Parser, message)
        {
            Error = new NodeError
            {
                Message = message,
                SourceFile = args.SourceFile,
                ErrorIndex = args.Item.StartIndex,                
            };
        }
    }
}
