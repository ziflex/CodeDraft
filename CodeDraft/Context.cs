#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Roslyn.Scripting;
using Roslyn.Scripting.CSharp;

#endregion

namespace CodeDraft.API
{
    public class Context
    {
        private readonly Session _session;

        public string Code { get; set; }

        private Context(Session session, string code)
        {
            this._session = session;
            this.Code = code;
        }

        public ActionResult Execute()
        {
            var success = true;
            object data = null;
            Exception err = null;
            var start = new TimeSpan();
            TimeSpan end;

            try
            {
                data = this._session.Execute(this.Code);
            }
            catch (Exception ex)
            {
                success = false;
                err = ex;
            }
            finally
            {
                end = new TimeSpan();
            }

            return new ActionResult(success, data, err, end - start);
        }

        public void AddReference(Assembly asm)
        {
            this._session.AddReference(asm);
        }

        public void ImportNamespace(string nameSpace)
        {
            this._session.ImportNamespace(nameSpace);
        }

        public static Context Create(Languages language, string code)
        {
            return Create(language, code, null);
        }

        public static Context Create(Languages language, string code, IEnumerable<string> references)
        {
            CommonScriptEngine engine = null;

            if (language == Languages.CSharp)
            {
                engine = new ScriptEngine();
            }
            else
            {
                throw new NotSupportedException(language.ToString());
            }

            Session session = engine.CreateSession();
            session.ImportNamespace("System");
            session.ImportNamespace("System.Collections.Generic");
            session.ImportNamespace("System.Text");
            session.ImportNamespace("System.Threading.Tasks");
            session.ImportNamespace("CodeDraft.API");

            session.AddReference(Assembly.GetExecutingAssembly());

            if (references != null && references.Any())
            {
                references.ToList().ForEach(session.AddReference);
            }

            return new Context(session, code);
        }
    }
}