using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Apterid.Bootstrap.Compile.Tests
{
    [TestClass]
    public class FunctionTests
    {
        const string Source =
@"
module Compile_Function =
  f_001 = () => 1
";

        [TestMethod]
        public void Compiler_Compile_Function_001_Unit_Literal()
        {
            using (var tester = new CompilerTester(nameof(Compiler_Compile_Function_001_Unit_Literal), Source))
            {
                tester.Compiler.UpdateAllCompileUnitsAsync().Wait();
                var unit = tester.Compiler.Context.CompileUnits.Single();
                Assert.AreEqual(0, unit.Errors.Count(), unit.GetFirstError());

                var assembly = unit.GenerationUnit.AssemblyBuilder;
                Assert.IsNotNull(assembly);

                var module = assembly.GetType("Compile_Function");
                Assert.IsNotNull(module);

                var function = module.GetMethod("f_001", BindingFlags.NonPublic | BindingFlags.Static);
                Assert.IsNotNull(function);

                var result = function.Invoke(null, null);
                Assert.IsInstanceOfType(result, typeof(int));
                Assert.AreEqual(1, (int)result);
            }
        }
    }
}
