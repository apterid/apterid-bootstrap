// Copyright (C) 2015 The Apterid Developers - See LICENSE

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Apterid.Bootstrap.Parse.Tests
{
    [TestClass]
    public class ModuleTests
    {
        ApteridParser parser = new ApteridParser();

        [TestMethod]
        public void Parser_Module_SimpleModule()
        {
            var s = @"
module Qualified.One = 
    f1 = 123
    
    public f2 = 314 /* comment */

public module Qualified.Two =
    // comment
    f3 = 3345    
    public f4 = 12345

";
            var sa = s.Select(c => c).ToArray();
            var m = parser.GetMatch(s, parser.ApteridSource);

            int num, offset;
            m.MatchState.GetLine(m.ErrorIndex, out num, out offset);

            Assert.IsTrue(m.Success, string.Format("{0}:{1}: {2}", num, offset, m.Error));
        }
    }
}
