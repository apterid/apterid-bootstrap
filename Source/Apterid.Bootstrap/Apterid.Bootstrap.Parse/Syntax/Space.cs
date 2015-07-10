using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Parse.Syntax
{
    public abstract class Space : Node
    {
    }

    public class Whitespace : Space
    {
    }

    public class EndOfLine : Whitespace
    {
    }

    public class EndOfFile : EndOfLine
    {
    }

    public abstract class Comment : Space
    {
    }

    public class EndComment : Comment
    {
    }

    public class InlineComment : Comment
    {
    }
}
