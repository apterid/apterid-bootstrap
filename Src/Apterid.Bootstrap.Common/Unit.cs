// Copyright (C) 2015 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Common
{
    public abstract class Unit
    {
        IList<ApteridError> errors = null;

        public abstract IEnumerable<Unit> Children { get; }

        public IEnumerable<ApteridError> Errors
        {
            get
            {
                IEnumerable<ApteridError> es = errors != null ? errors : Enumerable.Empty<ApteridError>();
                var children = Children;
                if (children != null)
                    es = es.Concat(children.SelectMany(child => child.Errors));
                return es;
            }
        }

        public void AddError<T>(string message)
            where T : ApteridError, new()
        {
            if (errors == null) errors = new List<ApteridError>();
            errors.Add(new T { Message = message });
        }

        public void AddError(ApteridError error)
        {
            if (errors == null) errors = new List<ApteridError>();
            errors.Add(error);
        }
    }
}
