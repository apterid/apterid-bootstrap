using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Threading.Tasks;
using Apterid.Bootstrap.Analyze;
using Apterid.Bootstrap.Compile.Steps;
using Apterid.Bootstrap.Parse;
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
                var parseUnit = new ParseUnit { SourceFile = sourceFile };

                var parse = new Task(new ParseSourceFile(context, parseUnit).GetStepAction(context.CancelSource.Token));
                parse.Start();
                parse.Wait();
                Assert.AreEqual(0, compileUnit.Errors.Count(), "parse errors: " + string.Join("; ", compileUnit.Errors.Select(e => e.Message)));

                var modules = sourceFile.ParseTree.Children.OfType<Parse.Syntax.Module>().ToList();
                Assert.AreEqual(1, modules.Count);
                Assert.AreEqual("One", modules.First().Name.Text);

                var bindings = modules.First().Body.OfType<Parse.Syntax.Binding>().ToList();
                Assert.AreEqual(1, bindings.Count);
                Assert.AreEqual("f", bindings.First().Name.Text);

                var literals = bindings.First().Body.OfType<Parse.Syntax.Literal<BigInteger>>().ToList();
                Assert.AreEqual(1, literals.Count);

                var oneTwoThree = new BigInteger(123);
                Assert.AreEqual(oneTwoThree, literals.First().Value);
            }
        }

        [TestMethod]
        public void Compiler_Analyze_SimpleModule()
        {
            using (var tester = new CompilerTester(nameof(Compiler_Analyze_SimpleModule), Source))
            {
                var compiler = tester.Compiler;
                var context = compiler.Context;
                var compileUnit = context.CompileUnits.First();
                var sourceFile = compileUnit.SourceFiles.First();
                var parseUnit = new ParseUnit { SourceFile = sourceFile };

                var parse = new Task(new ParseSourceFile(context, parseUnit).GetStepAction(context.CancelSource.Token));
                parse.Start();
                parse.Wait();
                Assert.AreEqual(0, compileUnit.Errors.Count(), "parse errors: " + string.Join("; ", compileUnit.Errors.Select(e => e.Message)));

                var analyzeUnit = new AnalysisUnit { ParseUnits = new List<ParseUnit>() { parseUnit } };
                var analyze = new Task(new AnalyzeSourceFile(context, analyzeUnit, parseUnit).GetStepAction(context.CancelSource.Token));
                analyze.Start();
                analyze.Wait();

                Assert.AreEqual(0, compileUnit.Errors.Count(), "analyze errors: " + string.Join("; ", compileUnit.Errors.Select(e => e.Message)));

                var module = analyzeUnit.Modules.Values.Single(m => m.Name.Name == "One");
                var binding = module.Bindings.Values.Single(b => b.Name.Name == "f");
                Assert.IsFalse(binding.IsPublic);

                Assert.IsInstanceOfType(binding.Expression, typeof(Analyze.Expressions.IntegerLiteral));
                var intLiteral = binding.Expression as Analyze.Expressions.IntegerLiteral;

                Assert.IsInstanceOfType(intLiteral.ResolvedType, typeof(Analyze.Builtins.SystemInt32));
                var val = (int)intLiteral.IntValue;
                Assert.AreEqual(123, val);
            }
        }

        [TestMethod]
        public void Compiler_Generate_SimpleModule()
        {
            using (var tester = new CompilerTester(nameof(Compiler_Generate_SimpleModule), Source))
            {
                tester.Compiler.UpdateAllCompileUnitsAsync().Wait();
                var unit = tester.Compiler.Context.CompileUnits.Single();
                Assert.AreEqual(0, unit.Errors.Count(), string.Format("Errors: {0}", string.Join("; ", unit.Errors.Select(e => e.Message))));

                var assembly = Assembly.LoadFrom(unit.OutputFileInfo.FullName);
                Assert.IsNotNull(assembly);

                var module = assembly.GetType("One");
                Assert.IsNotNull(module);

                var field = module.GetField("f", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                Assert.IsNotNull(field);
                Assert.AreEqual(typeof(int), field.FieldType);

                int value = (int)field.GetValue(null);
                Assert.AreEqual(123, value);
            }
        }
    }
}
