using System;
using System.Linq;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Apterid.Bootstrap.Parse.Tests
{
    [TestClass]
    public class LexiconTests
    {
        ApteridParser parser = new ApteridParser();

        [TestMethod]
        public void Parser_Lexicon_IntegerLiteral()
        {
            const string s1 = "123";
            var m1 = parser.GetMatch(s1, parser.DecimalInteger);
            Assert.IsTrue(m1.Success);
            Assert.AreEqual(123, (int)((Syntax.Literal<BigInteger>)m1.Result).Value);

            const string s2 = "-34_56";
            var m2 = parser.GetMatch(s2, parser.DecimalInteger);
            Assert.IsTrue(m2.Success);
            Assert.AreEqual(-3456, (int)((Syntax.Literal<BigInteger>)m2.Result).Value);
        }

        [TestMethod]
        public void Parser_Lexicon_QualifiedIdentifier()
        {
            const string s1 = "a.b.c";
            var parser = new ApteridParser
            {
                SourceText = new SourceText { Buffer = s1 }
            };

            var m1 = parser.GetMatch(s1, parser.QualifiedIdentifier);
            Assert.IsTrue(m1.Success);
            Assert.IsInstanceOfType(m1.Result, typeof(Syntax.QualifiedIdentifier));

            var r1 = m1.Result as Syntax.QualifiedIdentifier;
            Assert.AreEqual(5, r1.Children.Length);
            Assert.IsInstanceOfType(r1.Children[0], typeof(Syntax.Identifier));
            Assert.IsInstanceOfType(r1.Children[1], typeof(Syntax.Punct));
            Assert.IsInstanceOfType(r1.Children[2], typeof(Syntax.Identifier));
            Assert.IsInstanceOfType(r1.Children[3], typeof(Syntax.Punct));
            Assert.IsInstanceOfType(r1.Children[4], typeof(Syntax.Identifier));

            Assert.AreEqual(2, r1.Qualifiers.Count());
            Assert.AreEqual("a", r1.Qualifiers.ElementAt(0).Text);
            Assert.AreEqual("b", r1.Qualifiers.ElementAt(1).Text);
        }

        [TestMethod]
        public void Parser_Lexicon_Identifier()
        {
            const string s1 = "_";
            var m1 = parser.GetMatch(s1, parser.Identifier);
            Assert.IsTrue(m1.Success);
            Assert.AreEqual(s1.Length, m1.NextIndex);

            const string s2 = "_1a";
            var m2 = parser.GetMatch(s2, parser.Identifier);
            Assert.IsTrue(m2.Success);
            Assert.AreEqual(s2.Length, m2.NextIndex);

            const string s4 = "a3_b4";
            var m4 = parser.GetMatch(s4, parser.Identifier);
            Assert.IsTrue(m4.Success);
            Assert.AreEqual(s4.Length, m4.NextIndex);

            const string s3 = "123b_c";
            var m3 = parser.GetMatch(s3, parser.Identifier);
            Assert.IsFalse(m3.Success);

            const string s5 = " ";
            var m5 = parser.GetMatch(s5, parser.Identifier);
            Assert.IsFalse(m5.Success);

            const string s6 = "";
            var m6 = parser.GetMatch(s6, parser.Identifier);
            Assert.IsFalse(m6.Success);

            const string s7 = "a";
            var m7 = parser.GetMatch(s7, parser.Identifier);
            Assert.IsTrue(m7.Success);
            Assert.AreEqual(s7.Length, m7.NextIndex);
        }

        [TestMethod]
        public void Parser_Lexicon_InlineComment()
        {
            const string s1 = "/* baz */ foo */";
            var m1 = parser.GetMatch(s1, parser.InlineComment);
            Assert.IsTrue(m1.Success);
            Assert.AreEqual(0, m1.StartIndex);
            Assert.AreEqual(9, m1.NextIndex);

            const string s2 = "foo */";
            var m2 = parser.GetMatch(s2, parser.InlineComment);
            Assert.IsFalse(m2.Success);
        }

        [TestMethod]
        public void Parser_Lexicon_LineComment()
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
