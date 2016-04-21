// Copyright (C) 2015 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apterid.Bootstrap.Common;

namespace Apterid.Bootstrap.Parse
{
    public class ParseUnit : Unit
    {
        public ParsedSourceFile SourceFile { get; set; }
        public Parser.ApteridParser Parser { get; set; }

        public override IEnumerable<Unit> Children => Enumerable.Empty<Unit>();
    }
}
