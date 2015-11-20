using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Common
{
    public abstract class Unit
    {
        public IList<ApteridError> Errors { get; } = new List<ApteridError>();

        public void AddError<T>(string message)
            where T : ApteridError, new()
        {
            Errors.Add(new T { Message = message });
        }

        public void AddError(ApteridError error)
        {
            Errors.Add(error);
        }
    }
}
