// Copyright (C) 2015 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Parse.Syntax
{
    public class ErrorSection : Node
    {
        public ErrorSection(NodeArgs args, params Node[] children)
            : base(args, children)
        {
        }
    }
}
