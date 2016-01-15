using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Apterid.Bootstrap.Compile.Tests
{
    [TestClass]
    public class IntegerTests : PrimitiveTypeTests
    {
        const string source = @"

public module Int32Tests =
  public Add a b = a + b
  public Sub a b = a - b
  public Mul a b = a * b
  public Div a b = a / b

  public AddMul a b c d = a * b + c * d
  public MulAdd a b c d = a + b * c + d
  public SubDiv a b c d = a / b - c / d
  public DivSub a b c d = a - b / c - d

";

        public IntegerTests()
            : base(source)
        {
        }


    }

    public class PrimitiveTypeTests : IDisposable
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
