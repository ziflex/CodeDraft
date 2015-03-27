#region

using System;
using CodeDraft.Editor.Contracts;
using Gtk;

#endregion

namespace CodeDraft.Editor.Components.Tabs.Code
{
    public class CodeEditor : Frame, IComponent<Models.Code>
    {
        private readonly Button _excBtn;

        private readonly TextView _textArea;

        private Models.Code _model;

        public event EventHandler<EventArgs> OnExecuteClick;

        public Models.Code Model
        {
            get { return this._model; }
            set
            {
                this._model = value;
                this._textArea.Buffer.Text = value.Text;
            }
        }

        public CodeEditor()
        {
            this._textArea = new TextView
            {
                HeightRequest = 300
            };
            this._excBtn = new Button("Execute");

            var container = new VBox(false, 2);

            container.PackStart(new Toolbar {this._excBtn}, false, true, 0);
            container.PackStart(new ScrolledWindow {this._textArea});

            this.Add(container);
            this.InitializeEvents();
        }

        private void InitializeEvents()
        {
            this._excBtn.Clicked += (sender, args) =>
            {
                this.Model.Text = this._textArea.Buffer.Text;

                if (this.OnExecuteClick != null)
                {
                    this.OnExecuteClick.Invoke(this, new EventArgs());
                }
            };
        }
    }
}