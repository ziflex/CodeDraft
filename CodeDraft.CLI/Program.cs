#region

using System;
using CodeDraft.API;
using CommandLine;

#endregion

namespace CodeDraft.CLI
{
    internal class MainClass
    {
        public static void Main(string[] args)
        {
            var options = new Options();
            if (!Parser.Default.ParseArguments(args, options))
            {
                return;
            }

            var formatter = new OutputFormatter();

            try
            {
                ActionResult result = new Application().Execute(options.FilePath, options.References);
                formatter.Format(result);
            }
            catch (Exception ex)
            {
                formatter.Format(new ActionResult(false, null, ex, new TimeSpan(0, 0, 0, 0)));
            }
        }
    }
}