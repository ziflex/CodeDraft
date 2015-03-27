#region

using CodeDraft.API;
using CodeDraft.CLI.Helpers;

#endregion

namespace CodeDraft.CLI
{
    public class OutputFormatter
    {
        public void Format(ActionResult result)
        {
            if (result.Success)
            {
                ConsoleHelper.Success(result.Data != null ? result.Data.ToString() : "Success");
            }
            else
            {
                ConsoleHelper.Error(result.Error.ToString());
            }
        }
    }
}