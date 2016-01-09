using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuiltinTypeTestinator
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                if (args.Length != 1)
                {
                    Console.Error.WriteLine("usage: BuildinTypeTestinator TypeName");
                    return 1;
                }

                var typeName = args[0];
                var sb = new StringBuilder();
                var type = Type.GetType(typeName);
                if (type == null)
                {
                    Console.Error.WriteLine("Unable to resolve type {0}", typeName);
                    return 3;
                }

                var ntt = new NumericTypeTestinator(type);
                ntt.Generate(sb);
                Console.WriteLine(sb.ToString());

                return 0;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("{0}", e);
                return 2;
            }
        }
    }
}
