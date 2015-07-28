using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apterid.Bootstrap.Parse;

namespace Apterid.Bootstrap.Structure
{
    public class Builder
    {
        public Context Context { get; protected set; }
        public IList<Solution> Solutions { get; protected set; }

        public Builder(Context context)
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
