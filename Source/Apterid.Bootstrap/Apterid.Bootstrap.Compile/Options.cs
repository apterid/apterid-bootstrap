using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apterid.Bootstrap.Common;

namespace Apterid.Bootstrap.Compile
{
    public class Options
    {
        public enum OutputMode
        {
            Library,
            Executable,
        }

        public OutputMode Mode { get; set; }
        public string OutputPath { get; set; }

        public bool ForceRecompile { get; set; }

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

        public const string Usage = @"Usage: Apterid.Bootstrap.Compile [-m Library|Executable] -o OutputFile [-r DllReference]... SourceFiles...";

        public Options(string[] argv)
        {
            Sources = new List<string>();
            References = new List<string>();

            OptionState state = OptionState.GetSources;
            OutputMode? mode = null;

            foreach (var arg in argv)
            {
                switch (state)
                {
                    case OptionState.GetMode:
                        OutputMode m;
                        if (Enum.TryParse<OutputMode>(arg, out m))
                            mode = m;
                        else
                            throw new CmdLineException(string.Format(ErrorMessages.EC_0005_CmdLine_UnknownOutputMode, arg));
                        state = OptionState.GetSources;
                        break;
                    case OptionState.GetOutput:
                        OutputPath = arg;
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

            // sanity checks
            if (string.IsNullOrWhiteSpace(OutputPath))
                throw new CmdLineException(ErrorMessages.EC_0004_CmdLine_NoOutputPath);

            if (mode == null)
            {
                if (OutputPath.ToUpper().EndsWith(".DLL"))
                    mode = OutputMode.Library;
                else if (OutputPath.ToUpper().EndsWith(".EXE"))
                    mode = OutputMode.Executable;
                else
                    throw new CmdLineException(string.Format(ErrorMessages.EC_0005_CmdLine_UnknownOutputMode, "?"));
            }
        }
    }
}
