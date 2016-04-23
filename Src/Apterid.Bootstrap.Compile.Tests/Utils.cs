using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apterid.Bootstrap.Common;

namespace Apterid.Bootstrap.Compile.Tests
{
    static class UnitExtensions
    {
        public static string GetFirstError(this Unit unit)
        {
            if (unit == null || unit.Errors == null)
                return "";

            var error = unit.Errors.FirstOrDefault();
            if (error == null)
                return "";

            return error.Message;
        }
    }
}
