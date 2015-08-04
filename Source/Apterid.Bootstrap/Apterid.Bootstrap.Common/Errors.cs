using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Common
{
    public enum ErrorCode
    {
        Internal,
        Unknown
    }

    public class ApteridException : Exception
    {
        public ErrorCode Code { get; private set; }

        public ApteridException(ErrorCode code, string message, Exception innerException)
            : base(message, innerException)
        {
            this.Code = code;
        }

        public ApteridException(ErrorCode code, string message)
            : this(code, message, null)
        {
        }

        public ApteridException(ErrorCode code)
            : this(code, code.ToString() + " Error", null)
        {
        }

        public ApteridException(string message)
            : this(ErrorCode.Unknown, message, null)
        {
        }

        public ApteridException()
            : this(ErrorCode.Unknown, "Unknown Error", null)
        {
        }
    }

    public class InternalException : ApteridException
    {
        public InternalException(string message)
            : base(ErrorCode.Internal, message)
        {
        }

        public InternalException()
            : base(ErrorCode.Internal)
        {
        }
    }
}
