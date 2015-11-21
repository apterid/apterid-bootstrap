using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Compile.Steps
{
    class Generate : Bootstrap.Compile.CompilerStep
    {
        public Generate(CompilerContext context, CompilationUnit compileUnit)
            : base(context, compileUnit)
        {
        }
    }
}
