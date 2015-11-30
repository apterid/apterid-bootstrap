// Copyright (C) 2015 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apterid.Bootstrap.Common;
using Apterid.Bootstrap.Parse;

namespace Apterid.Bootstrap.Analyze
{
    public class AnalysisUnit : Unit
    {
        public IList<ParseUnit> ParseUnits { get; set; }

        public IDictionary<QualifiedName, Module> Modules { get; } = new Dictionary<QualifiedName, Module>();

        public override IEnumerable<Unit> Children
        {
            get
            {
                if (ParseUnits != null)
                    return ParseUnits;
                else
                    return Enumerable.Empty<Unit>();
            }
        }
    }
}
