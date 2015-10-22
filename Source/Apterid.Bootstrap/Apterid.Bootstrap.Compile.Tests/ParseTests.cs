﻿using System;
using System.IO;
using System.Linq;
using System.Numerics;
using Apterid.Bootstrap.Compile.Steps;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Apterid.Bootstrap.Compile.Tests
{
    [TestClass]
    public class ParseTests
    {
        [TestMethod]
        public void Compiler_Parse_SimpleModule()
        {
            var source = @"
module One =
  f = 123
";

            using (var ta = new CompilerTester(nameof(Compiler_Parse_SimpleModule), source))
            {
                var sourceFile = ta.Assembly.SourceFiles.First();
                var parse = new ParseSourceFileStep(ta.Context, ta.Assembly, sourceFile);
                var status = parse.Run();
                Assert.AreEqual(StepResult.Succeeded, status);

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
