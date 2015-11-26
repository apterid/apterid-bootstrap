// Copyright (C) 2015 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Apterid.Bootstrap.Common;

namespace Apterid.Bootstrap.Compile.Steps
{
    class EmitAssembly : CompilerStep<CompilationUnit>
    {
        public EmitAssembly(CompilerContext context, CompilationUnit compilationUnit)
            : base(context, compilationUnit)
        {
        }

        public override Action GetStepAction(CancellationToken cancel)
        {
            return () =>
            {
                if ((Unit.Mode & CompileOutputMode.Executable) != 0)
                {
                    // TODO: look for main function
                }

                Unit.GenerationUnit.AssemblyBuilder.Save(Unit.OutputFileInfo.Name);
            };
        }
    }
}
