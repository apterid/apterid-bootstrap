// Copyright (C) 2016 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apterid.Bootstrap.Common;

namespace Apterid.Bootstrap.Parse
{
    public class NodeError : ApteridError
    {
        public ParsedSourceFile SourceFile { get; set; }
        public Syntax.Node ErrorNode { get; set; }
        public int ErrorIndex { get; set; }

        public override string Message
        {
            get
            {
                var msg = base.Message;
                if (SourceFile != null && SourceFile.MatchState != null)
                {
                    int num, offset;
                    var line = SourceFile.MatchState.GetLine(ErrorIndex, out num, out offset);
                    msg = string.Format("({0},{1}): {2}", num, offset, msg);

                    if (!(SourceFile is MemorySourceFile))
                        msg = string.Format("{0}{1}", SourceFile.Name, msg);
                }
                return msg;
            }

            set
            {
                base.Message = value;
            }
        }
    }
}
