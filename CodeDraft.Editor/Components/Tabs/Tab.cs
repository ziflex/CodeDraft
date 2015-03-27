#region

using System.Linq;
using CodeDraft.API;
using CodeDraft.Editor.Components.Tabs.Code;
using CodeDraft.Editor.Components.Tabs.Execution;
using CodeDraft.Editor.Components.Tabs.References;
using CodeDraft.Editor.Contracts;
using CodeDraft.Editor.Models;
using Gtk;

#endregion

namespace CodeDraft.Editor.Components.Tabs
{
    public class Tab : HPaned, IComponent<Draft>
    {
        private readonly CodeEditor _codeEditor;

        private readonly ExecutionResult _executionResult;

        private readonly ReferencesList _references;

        private Context _ctx;
        private Draft _model;

        public Draft Model
        {
            get { return this._model; }
            set
            {
                this._model = value;
                this.Bind();
            }
        }

        public TabHeader Header { get; private set; }

        public Tab()
        {
            this.Header = new TabHeader();
            this._references = new ReferencesList
            {
                WidthRequest = 200
            };
            this._codeEditor = new CodeEditor();
            this._executionResult = new ExecutionResult();

            var leftFrame = new Frame {this._references};
            var rightFrame = new Frame {new VPaned {this._codeEditor, this._executionResult}};

            this.Add(leftFrame);
            this.Add(rightFrame);
            this.InitializeEvents();
        }

        private void InitializeEvents()
        {
            this._codeEditor.OnExecuteClick += (sender, args) => this.Execute();
            this._references.OnChange += (sender, args) => this._ctx = null;
        }

        private void Bind()
        {
            ReferenceCollection refModel = null;
            CodeDraft.Editor.Models.Code codeModel = null;
            var headerTitle = string.Empty;

            if (this.Model != null)
            {
                refModel = this.Model.References;
                codeModel = this.Model.Code;
                headerTitle = this.Model.Name;
            }

            this._references.Model = refModel;
            this._codeEditor.Model = codeModel;
            this.Header.Title = headerTitle;
        }

        private void Execute()
        {
            //Task<ActionResult>.Factory
            //    .StartNew(() => this.GetContext().Execute())
            //    .ContinueWith(t => this._executionResult.ShowResult(t.Result));
            var ctx = this.GetContext();
            ctx.Code = this.Model.Code.Text;
            this.ShowResult(ctx.Execute());
        }

        private Context GetContext()
        {
            if (this._ctx == null)
            {
                this._ctx = Context.Create(this.Model.Code.Language, this.Model.Code.Text,
                    this.Model.References.Select(x => x.Location));
            }

            return this._ctx;
        }

        private void ShowResult(ActionResult result)
        {
            this._executionResult.Model = result;
        }
    }
}