﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apterid.Bootstrap.Common;
using Apterid.Bootstrap.Parse;

namespace Apterid.Bootstrap.Analyze
{
    public class AnalyzeUnit : Unit
    {
        public IList<Module> Modules { get; } = new List<Module>();
    }
}
