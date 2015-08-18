using System;
using System.IO;
using System.Linq;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Apterid.Bootstrap.Compile.Tests
{
    [TestClass]
    public class ParseTests
    {
        [TestMethod]
        public void Compiler_Parse_SimpleModule_001()
        {
            var source = @"
module One =
  f = 123
";

            using (var ta = new TestAssembly(nameof(Compiler_Parse_SimpleModule_001), source))
            {
                var sourceFile = ta.Assembly.SourceFiles.First();
                var parse = new Compile.Tasks.ParseSourceFile(ta.Context, ta.Assembly, null, sourceFile);
                var status = parse.Process().Result;
                Assert.AreEqual(CompileStatus.Completed, status);

                var modules = sourceFile.ParseTree.Children.OfType<Parse.Syntax.Module>().ToList();
                Assert.AreEqual(1, modules.Count);

                var bindings = modules.First().Body.OfType<Parse.Syntax.Binding>().ToList();
                Assert.AreEqual(1, bindings.Count);
                Assert.AreEqual("f", bindings.First().Name.Text);

                var literals = bindings.First().Body.OfType<Parse.Syntax.Literal<BigInteger>>().ToList();
                Assert.AreEqual(1, literals.Count);

                var oneTwoThree = new BigInteger(123);
                Assert.AreEqual(oneTwoThree, literals.First().Value);
            }
        }
    }
}
