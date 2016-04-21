// Copyright (C) 2016 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Threading.Tasks;
using Apterid.Bootstrap.Analyze;
using Apterid.Bootstrap.Analyze.Abstract.Expressions;
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

public module Two = 
  public G = true
  h = 456

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

                var str = sourceFile.ParseTree.ToString();
                var modules = sourceFile.ParseTree.Children.OfType<Parse.Syntax.Module>().ToList();
                Assert.AreEqual(2, modules.Count);

                var moduleOne = modules.Single(m => m.Name.Text == "One");
                {
                    Assert.IsFalse((moduleOne.Flags & Parse.Syntax.Flags.IsPublic) != 0);

                    var bindings = moduleOne.Body.OfType<Parse.Syntax.Binding>().ToList();
                    Assert.AreEqual(1, bindings.Count);
                    Assert.AreEqual("f", bindings.First().Name.Text);

                    var literals = bindings.First().Body.OfType<Parse.Syntax.Literal<BigInteger>>().ToList();
                    Assert.AreEqual(1, literals.Count);

                    var oneTwoThree = new BigInteger(123);
                    Assert.AreEqual(oneTwoThree, literals.Single().Value);
                }

                var moduleTwo = modules.Single(m => m.Name.Text == "Two");
                {
                    Assert.IsTrue((moduleTwo.Flags & Parse.Syntax.Flags.IsPublic) != 0);

                    var bindings = moduleTwo.Body.OfType<Parse.Syntax.Binding>().ToList();
                    Assert.AreEqual(2, bindings.Count);

                    var g = bindings.Single(b => b.Name.Text == "G");
                    Assert.IsTrue((g.Flags & Parse.Syntax.Flags.IsPublic) != 0);

                    var gl = g.Body.OfType<Parse.Syntax.Literal<bool>>().Single();
                    Assert.AreEqual(true, gl.Value);

                    var h = bindings.Single(b => b.Name.Text == "h");
                    Assert.IsFalse((h.Flags & Parse.Syntax.Flags.IsPublic) != 0);
                    var hl = h.Body.OfType<Parse.Syntax.Literal<BigInteger>>().Single();
                    var fourFiveSix = new BigInteger(456);
                    Assert.AreEqual(fourFiveSix, hl.Value);
                }
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
                compileUnit.ParseUnits = new List<ParseUnit>();
                compileUnit.ParseUnits.Add(parseUnit);

                var parse = new Task(new ParseSourceFile(context, parseUnit).GetStepAction(context.CancelSource.Token));
                parse.Start();
                parse.Wait();
                Assert.AreEqual(0, compileUnit.Errors.Count(), "parse errors: " + string.Join("; ", compileUnit.Errors.Select(e => e.Message)));

                var analyzeUnit = compileUnit.AnalysisUnit = new AnalysisUnit { ParseUnits = new List<ParseUnit>() { parseUnit } };
                var analyze = new Task(new AnalyzeSourceFile(context, analyzeUnit, parseUnit).GetStepAction(context.CancelSource.Token));
                analyze.Start();
                analyze.Wait();

                Assert.AreEqual(0, compileUnit.Errors.Count(), "analyze errors: " + string.Join("; ", compileUnit.Errors.Select(e => e.Message)));

                {
                    var module = analyzeUnit.Modules.Values.Single(m => m.Name.Name == "One");
                    Assert.IsFalse(module.IsPublic);

                    var binding = module.Bindings.Values.Single(b => b.Name.Name == "f");
                    Assert.IsFalse(binding.IsPublic);

                    Assert.IsInstanceOfType(binding.Expression, typeof(IntegerLiteral));
                    var intLiteral = binding.Expression as IntegerLiteral;
                    Assert.AreEqual(typeof(int), intLiteral.ResolvedType.CLRType);

                    var val = (int)intLiteral.IntValue;
                    Assert.AreEqual(123, val);
                }

                {
                    var module = analyzeUnit.Modules.Values.Single(m => m.Name.Name == "Two");
                    Assert.IsTrue(module.IsPublic);

                    {
                        var g = module.Bindings.Values.Single(b => b.Name.Name == "G");
                        Assert.IsTrue(g.IsPublic);

                        Assert.IsInstanceOfType(g.Expression, typeof(Literal<bool>));
                        var boolLiteral = g.Expression as Literal<bool>;
                        Assert.AreEqual(typeof(bool), boolLiteral.ResolvedType.CLRType);

                        var val = (bool)boolLiteral.Value;
                        Assert.AreEqual(true, val);
                    }

                    {
                        var h = module.Bindings.Values.Single(b => b.Name.Name == "h");
                        Assert.IsFalse(h.IsPublic);

                        Assert.IsInstanceOfType(h.Expression, typeof(IntegerLiteral));
                        var intLiteral = h.Expression as IntegerLiteral;
                        Assert.AreEqual(typeof(int), intLiteral.ResolvedType.CLRType);

                        var val = (int)intLiteral.IntValue;
                        Assert.AreEqual(456, val);
                    }
                }
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

                var assembly = unit.GenerationUnit.AssemblyBuilder;
                Assert.IsNotNull(assembly);

                var module = assembly.GetType("One");
                Assert.IsNotNull(module);

                var field = module.GetField("f", BindingFlags.NonPublic | BindingFlags.Static);
                Assert.IsNotNull(field);
                Assert.AreEqual(typeof(int), field.FieldType);

                int value = (int)field.GetValue(null);
                Assert.AreEqual(123, value);
            }
        }

        [TestMethod]
        public void Compiler_Generate_SimpleModule_PublicModuleAndBinding()
        {
            using (var tester = new CompilerTester(nameof(Compiler_Generate_SimpleModule_PublicModuleAndBinding), Source))
            {
                tester.Compiler.UpdateAllCompileUnitsAsync().Wait();
                var unit = tester.Compiler.Context.CompileUnits.Single();
                Assert.AreEqual(0, unit.Errors.Count(), string.Format("Errors: {0}", string.Join("; ", unit.Errors.Select(e => e.Message))));

                var assembly = unit.GenerationUnit.AssemblyBuilder;
                Assert.IsNotNull(assembly);

                var module = assembly.GetType("Two");
                Assert.IsNotNull(module);
                Assert.IsTrue(module.IsPublic);

                var field = module.GetField("G", BindingFlags.Public | BindingFlags.Static);
                Assert.IsNotNull(field);
                Assert.AreEqual(typeof(bool), field.FieldType);

                bool value = (bool)field.GetValue(null);
                Assert.AreEqual(true, value);
            }
        }
    }
}
