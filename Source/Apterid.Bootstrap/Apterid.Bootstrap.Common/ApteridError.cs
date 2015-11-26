// Copyright (C) 2015 The Apterid Developers - See LICENSE

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

        const int MaxMessageStringLength = 16;

        public static string Truncate(string str)
        {
            if (str == null) return string.Empty;

            if (str.Length > MaxMessageStringLength)
                str = str.Substring(0, MaxMessageStringLength) + "...";

            return str;
        }
    }
}
