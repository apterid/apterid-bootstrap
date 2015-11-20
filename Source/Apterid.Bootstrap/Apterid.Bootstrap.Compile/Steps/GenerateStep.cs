using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Compile.Steps
{
    class GenerateStep : Compile.CompileStep
    {
        public GenerateStep(CompileContext context, CompileUnit compileUnit)
            : base(context, compileUnit)
        {
        }
    }
}
