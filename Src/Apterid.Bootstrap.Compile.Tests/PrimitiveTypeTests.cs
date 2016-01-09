using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Apterid.Bootstrap.Compile.Tests
{
    class PrimitiveTypeTests : IDisposable
    {
        public string SourceCode { get; }
        CompilerTester compilerTester;

        public PrimitiveTypeTests(string sourceCode)
        {
            SourceCode = sourceCode;
            compilerTester = new CompilerTester(this.GetType().FullName, sourceCode);
        }

        public void Dispose()
        {
            compilerTester.Dispose();
        }
    }
}
