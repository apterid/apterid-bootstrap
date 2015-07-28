using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Compile
{
    class Options
    {
        public enum OutputMode
        {
            Library,
            Executable,
        }

        public OutputMode Mode { get; set; }
        public string Output { get; set; }

        public IList<string> Sources { get; set; }
        public IList<string> References { get; set; }

        private enum OptionState
        {
            GetMode,
            GetOutput,
            GetSources,
            GetReference,
        }

        const string ModeSwitch = "-m";
        const string OutputSwitch = "-o";
        const string ReferenceSwitch = "-r";

        public const string Usage = @"usage: Apterid.Bootstrap.Compile [-m Library|Executable] -o OutputFile [-r DllReference] SourceFiles...";

        public Options(string[] argv)
        {
            Sources = new List<string>();
            References = new List<string>();

            OptionState state = OptionState.GetSources;

            foreach (var arg in argv)
            {
                switch (state)
                {
                    case OptionState.GetMode:
                        OutputMode mode;
                        if (Enum.TryParse<OutputMode>(arg, out mode))
                            Mode = mode;
                        else
                            throw new ArgumentException(string.Format("Unknown compiler output mode '{0}'", arg));
                        state = OptionState.GetSources;
                        break;
                    case OptionState.GetOutput:
                        Output = arg;
                        state = OptionState.GetSources;
                        break;
                    case OptionState.GetReference:
                        References.Add(arg);
                        state = OptionState.GetSources;
                        break;
                    case OptionState.GetSources:
                        if (arg == ModeSwitch)
                            state = OptionState.GetMode;
                        else if (arg == OutputSwitch)
                            state = OptionState.GetOutput;
                        else if (arg == ReferenceSwitch)
                            state = OptionState.GetReference;
                        else
                            Sources.Add(arg);
                        break;
                }
            }
        }
    }
}
