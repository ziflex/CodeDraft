#region

using CommandLine;
using CommandLine.Text;

#endregion

namespace CodeDraft.CLI
{
    public class Options
    {
        [Option('f', "file", HelpText = "Source file path", Required = true)]
        public string FilePath { get; set; }

        [OptionArray('r', "refernces", HelpText = "References paths")]
        public string[] References { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
                (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}