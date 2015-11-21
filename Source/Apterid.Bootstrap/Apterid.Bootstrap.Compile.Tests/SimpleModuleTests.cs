using System;
using System.IO;
using System.Linq;
using System.Numerics;
using Apterid.Bootstrap.Compile.Steps;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Apterid.Bootstrap.Compile.Tests
{
    [TestClass]
    public class SimpleModuleTests
    {
        public const string Source =
@"
module One =
  f = 123
";

        [TestMethod]
        public void Compiler_Parse_SimpleModule()
        {
            using (var tester = new CompilerTester(nameof(Compiler_Parse_SimpleModule), Source))
            {
                var compiler = tester.Compiler;
                var context = compiler.Context;
                var compileUnit = context.CompileUnits.First();
                var sourceFile = compileUnit.SourceFiles.First();

                var parse = new ParseSourceFile(tester.Compiler.Context, compileUnit, sourceFile);
                var result = parse.Run();
                Assert.AreEqual(StepStatus.Succeeded, result.Status);

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
