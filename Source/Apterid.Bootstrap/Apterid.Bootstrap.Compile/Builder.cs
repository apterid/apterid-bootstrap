using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Apterid.Bootstrap.Parse;
using Apterid.Bootstrap.Structure;

namespace Apterid.Bootstrap.Compile
{
    class Builder
    {
        public BuildContext Context { get; protected set; }
        public IList<Solution> Solutions { get; protected set; }

        public Builder(BuildContext context)
        {
            Context = context;
        }

        public IList<BuildError> UpdateBuild(IEnumerable<SourceText> sources)
        {
            var errors = new List<BuildError>();

            return errors;
        }
    }
}
