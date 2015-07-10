using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Apterid.Bootstrap.Parse.Tests
{
    [TestClass]
    public class LexiconTests
    {
        ApteridParser parser = new ApteridParser();

        [TestMethod]
        public void Parser_Lexicon_TestLineComment()
        {
            const string s1 = "// comment\nblahblahblah";
            var m1 = parser.GetMatch(s1, parser.LineComment);
            Assert.IsTrue(m1.Success);
            Assert.AreEqual(0, m1.StartIndex);
            Assert.AreEqual(11, m1.NextIndex);

            const string s2 = "// comment";
            var m2 = parser.GetMatch(s2, parser.LineComment);
            Assert.IsTrue(m2.Success);
            Assert.AreEqual(0, m2.StartIndex);
            Assert.AreEqual(10, m2.NextIndex);

            const string s3 = "ghuwrbgu8 48h2 //";
            var m3 = parser.GetMatch(s3, parser.LineComment);
            Assert.IsFalse(m3.Success);

            const string s4 = "//\nhghg";
            var m4 = parser.GetMatch(s4, parser.LineComment);
            Assert.IsTrue(m4.Success);

            const string s5 = "";
            var m5 = parser.GetMatch(s5, parser.LineComment);
            Assert.IsFalse(m5.Success);
        }
    }
}
