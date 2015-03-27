#region

using System.Collections.Generic;
using System.IO;
using CodeDraft.API;

#endregion

namespace CodeDraft.CLI
{
    public class Application
    {
        public ActionResult Execute(string filePath, IEnumerable<string> references)
        {
            string code = File.ReadAllText(filePath);
            Context ctx = Context.Create(Languages.CSharp, code, references);

            return ctx.Execute();
        }
    }
}