using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Analyze.Analyzer
{
    class Analyzer
    {
        protected ApteridAnalyzer Parent { get; }
        protected AnalysisUnit Unit { get { return Parent.Unit; } }

        public Analyzer(ApteridAnalyzer parent)
        {
            Parent = parent;
        }
    }
}
