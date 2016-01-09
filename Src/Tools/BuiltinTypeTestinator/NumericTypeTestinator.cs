using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BuiltinTypeTestinator
{
    class NumericTypeTestinator
    {
        public Type TypeToTest { get; set; }

        string apteridModuleName;
        string testClassName;

        public NumericTypeTestinator(Type typeToTest)
        {
            TypeToTest = typeToTest;
        }

        public void Generate(StringBuilder sb)
        {
            apteridModuleName = string.Format("{0}Tests", TypeToTest.FullName).Replace(".", "");
            testClassName = apteridModuleName;

            var sourceBuilder = new StringBuilder();
            var testsBuilder = new StringBuilder();

            GenerateSourceAndTests(sourceBuilder, testsBuilder);

            sb.AppendLine(@"using System;");
            sb.AppendLine(@"using Microsoft.VisualStudio.TestTools.UnitTesting;");
            sb.AppendLine();
            sb.AppendLine(@"namespace Apterid.Boostrap.Compile.Tests");
            sb.AppendLine(@"{");
            sb.AppendLine(@"    [TestClass]");
            sb.AppendLine(string.Format(@"    public class {0} : PrimitiveTypeTests", testClassName));
            sb.AppendLine(@"    {");
            sb.AppendLine(@"        const string source = @""");
            sb.AppendLine(string.Format(@"public module {0} =", apteridModuleName));
            sb.AppendLine(sourceBuilder.ToString().Replace("\"", "\"\""));
            sb.AppendLine(@"        "";");
            sb.AppendLine();
            sb.AppendLine(string.Format(@"        public {0}()", testClassName));
            sb.AppendLine(@"            : base(source)");
            sb.AppendLine(@"        {");
            sb.AppendLine(@"        }");
            sb.AppendLine();
            sb.AppendLine(testsBuilder.ToString());
            sb.AppendLine(@"    }");
            sb.AppendLine(@"}");
            sb.AppendLine();
        }

        int counter = 0;

        struct BindingExpected
        {
            string Name;
            string Value;
        }

        string GetNextName()
        {
            return string.Format("b{0}", counter++);
        }

        void GenerateSourceAndTests(StringBuilder sb, StringBuilder tb)
        {
            counter = 0;

            // for each property, instantiate and call on a number of representative values
            var props = TypeToTest.GetRuntimeProperties();

            // for each method with one arg of the type, instantiate and call on a number of representative values
            var allMethods = TypeToTest.GetRuntimeMethods().Where(mi => mi.IsPublic).ToList();
            var instanceMethods = allMethods.Where(mi => !mi.IsStatic).ToList();

            // for each static method with one or two args of the type, instantiate and call on a number of representative values
            var staticMethods = allMethods.Where(mi => mi.IsStatic).ToList();

            // for each static method with name op_*, generate a combination of ops, to a width and depth of 4, and call on a number of representative values

        }

        Random rng = new Random();

        IEnumerable<object> GetValues()
        {
            if (TypeToTest == typeof(int))
            {
                var sub = int.MaxValue / 2;

                var num = rng.Next(4);
                for (int i = 0; i < num; i++)
                    yield return rng.Next() - sub;
                yield return int.MinValue;
                num = rng.Next(4);
                for (int i = 0; i < num; i++)
                    yield return rng.Next() - sub;
                yield return int.MaxValue;
                num = rng.Next(4);
                for (int i = 0; i < num; i++)
                    yield return rng.Next() - sub;
            }
        }
    }
}
