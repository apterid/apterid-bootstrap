// Copyright (C) 2016 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Apterid.Bootstrap.Common;
using IronMeta.Matcher;

namespace Apterid.Bootstrap.Parse.Parser
{
    public partial class ApteridParser
    {
        internal ApteridLexicon lex;
        internal ApteridPatterns pat;
        internal ApteridExpressions exp;

        public ApteridLexicon Lexicon => lex;
        public ApteridPatterns Patterns => pat;
        public ApteridExpressions Expressions => exp;

        protected override void Init()
        {
            lex = new ApteridLexicon();

            pat = new ApteridPatterns();
            pat.lex = lex;

            exp = new ApteridExpressions();
            exp.lex = lex;
            exp.pat = pat;

            Children.Add(lex);
            Children.Add(pat);
            Children.Add(exp);
            base.Init();
        }
    }

    public partial class ApteridExpressions
    {
        internal ApteridLexicon lex;
        internal ApteridPatterns pat;
    }

    public partial class ApteridPatterns
    {
        internal ApteridLexicon lex;
    }

    public partial class ApteridLexicon
    {
    }
}
