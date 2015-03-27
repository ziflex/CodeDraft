#region

using System;
using CodeDraft.API;
using CodeDraft.Editor.Contracts;
using Gtk;

#endregion

namespace CodeDraft.Editor.Components.Tabs.Execution
{
    public class ExecutionResult : Frame, IComponent<ActionResult>
    {
        private readonly Button _hideBtn;

        private readonly TextView _output;

        private ActionResult _model;

        public ActionResult Model
        {
            get { return this._model; }
            set
            {
                this._model = value;
                this.Bind();
            }
        }

        public ExecutionResult()
        {
            this._hideBtn = new Button("Hide");
            this._output = new TextView
            {
                Editable = false
            };

            var container = new VBox(false, 2);

            container.PackStart(new Toolbar {this._hideBtn}, false, true, 0);
            container.PackStart(new ScrolledWindow {this._output}, true, true, 0);
            this.Add(container);
        }

        private void Bind()
        {
            if (this.Model != null)
            {
                if (this.Model.Success)
                {
                    this.ShowSuccess(this.Model.Data);
                }
                else
                {
                    this.ShowError(this.Model.Error);
                }
            }
        }

        private void ShowError(Exception exception)
        {
            this._output.Buffer.Text = exception.Message;
        }

        private void ShowSuccess(object result)
        {
            string text = string.Empty;

            if (result != null)
            {
                text = result.ToString();
            }
            else
            {
                text = "No output";
            }

            this._output.Buffer.Text = text;
        }
    }
}