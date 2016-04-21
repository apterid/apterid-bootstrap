// Copyright (C) 2016 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Analyze.Abstract
{
    public class Binding : Scope
    {
        public bool IsPublic { get; internal set; }
        public Annotation Annotation { get; internal set; }
        public Expression Expression { get; internal set; }
        public MemberInfo GeneratedMemberInfo { get; set; }

        public Binding(Parse.Syntax.Node syntaxNode)
            : base(syntaxNode)
        {
        }
    }
}
