// Copyright (C) 2015 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid
{
    /// <summary>
    /// Base class for Apterid modules.
    /// Apterid modules are final classes with only static members that inherit from Apterid.Module.
    /// </summary>
    public abstract class Module
    {
        protected Module()
        {
        }
    }
}
