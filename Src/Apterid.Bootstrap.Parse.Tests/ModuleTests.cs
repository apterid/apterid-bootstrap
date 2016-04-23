// Copyright (C) 2016 The Apterid Developers - See LICENSE

using System;
using System.Linq;
using Apterid.Bootstrap.Parse.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Apterid.Bootstrap.Parse.Tests
{
    [TestClass]
    public class ModuleTests
    {
        [TestMethod]
        public void Parser_Module_SimpleModule()
        {
            const string s = 
@"
module Qualified.One = 
    f1 = 123

    public f2 = 314 /* comment */

public module Qualified.Two =
    // comment
    f3 = 3345    
    public f4 = 12345
";
            var parser = new ApteridParser();
            parser.SourceFile = new MemorySourceFile(s);

            var sa = s.Select(c => c).ToArray();
            var m = parser.GetMatch(s, parser.ApteridSource);

            int num, offset;
            m.MatchState.GetLine(m.ErrorIndex, out num, out offset);

            Assert.IsTrue(m.Success, string.Format("{0}:{1}: {2}", num, offset, m.Error));
        }
    }
}
