// Copyright (C) 2016 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Common
{
    public abstract class SourceFile
    {
        public static readonly Guid ApteridLanguageId = new Guid("{97123B43-478B-4957-A914-938E556CC370}");
        public static readonly Guid ApteridLanguageVendorId = new Guid("{149FCFA7-A682-4DF0-86AA-82BC916C8EC8}");
        public static readonly Guid ApteridLanguageSourceFileId = new Guid("{EEA00038-6543-44CD-BD67-6720CA47012A}");

        public string Name { get; protected set; }
        public virtual bool Exists { get; }
        public virtual DateTime LastWriteTimeUtc { get; }

        public abstract IEnumerable<char> Buffer { get; }
    }
}
