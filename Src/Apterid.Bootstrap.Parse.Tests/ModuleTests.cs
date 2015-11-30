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
    
    f2 = 314 /* comment */

module Qualified.Two =
    // comment
    f3 = 3345    
    f4 = -12345

";
            var sa = s.Select(c => c).ToArray();
            var m = parser.GetMatch(s, parser.ApteridSource);

            Assert.IsTrue(m.Success);
        }
    }
}
