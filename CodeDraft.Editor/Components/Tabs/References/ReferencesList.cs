#region

using System;
using System.Linq;
using CodeDraft.Editor.Contracts;
using CodeDraft.Editor.Models;
using Gtk;

#endregion

namespace CodeDraft.Editor.Components.Tabs.References
{
    public class ReferencesList : VBox, IComponent<ReferenceCollection>
    {
        private readonly Button _addBtn;

        private readonly ReferenceChooser _refChooser;

        private readonly Button _rmBtn;

        private readonly ListStore _store;

        private readonly TreeView _tree;

        private ReferenceCollection _model;

        public ReferenceCollection Model
        {
            get { return this._model; }
            set
            {
                this._model = value;
                this.Bind();
            }
        }

        public event EventHandler OnChange;

        public ReferencesList() : base(false, 2)
        {
            this._addBtn = new Button("+") {new Image(Stock.Add, IconSize.SmallToolbar) {WidthRequest = 15}};
            this._rmBtn = new Button("-") {new Image(Stock.Remove, IconSize.SmallToolbar) {WidthRequest = 15}};
            this._refChooser = new ReferenceChooser();
            this._store = new ListStore(typeof (string));
            this._tree = new TreeView
            {
                Model = this._store,
                HeadersVisible = true
            };

            this._tree.AppendColumn("References", new CellRendererText(), "text", 0);

            this.PackStart(new Toolbar {this._addBtn, this._rmBtn}, false, true, 0);
            this.PackStart(this._tree, true, true, 0);
            this.ShowAll();
            this.Bind();
            this.InitializeEvents();
        }

        private void Bind()
        {
            this._store.Clear();

            if (this._model != null)
            {
                this._model.ForEach(
                    x => this._store.AppendValues(System.IO.Path.GetFileNameWithoutExtension(x.Location)));
            }
        }

        private void InitializeEvents()
        {
            this._addBtn.Clicked += (sender, args) => this.AddReference();
            this._rmBtn.Clicked += (sender, args) => this.RemoveReference();
        }

        private void AddReference()
        {
            string filePath = this._refChooser.Choose();
            if (!this._model.Contains(filePath))
            {
                this._model.Add(new Reference(filePath));
                this._store.AppendValues(System.IO.Path.GetFileNameWithoutExtension(filePath));
                this._tree.ShowAll();

                this.Notify();
            }
        }

        private void RemoveReference()
        {
            var selected = this._tree.Selection.GetSelectedRows().FirstOrDefault();

            if (selected != null)
            {
                TreeIter iter;
                if (this._store.GetIter(out iter, selected))
                {
                    this._store.Remove(ref iter);
                    this._model.RemoveAt(selected.Indices.First());
                    this.Notify();
                }
            }
        }

        private void Notify()
        {
            if (this.OnChange != null)
            {
                this.OnChange.Invoke(this, new EventArgs());
            }
        }
    }
}