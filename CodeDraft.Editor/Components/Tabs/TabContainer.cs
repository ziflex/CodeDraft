#region

using System;
using System.Collections.Generic;
using CodeDraft.Editor.Contracts;
using CodeDraft.Editor.Models;
using Gtk;

#endregion

namespace CodeDraft.Editor.Components.Tabs
{
    public class TabContainer : Frame, IComponent<List<Draft>>
    {
        private readonly Notebook _notebook;

        private List<Draft> _model;

        public Tab CurrentTab { get; private set; }

        public List<Draft> Model
        {
            get { return this._model; }
            set
            {
                this._model = value;
                this.Bind();
            }
        }

        public TabContainer()
        {
            this.BorderWidth = 0;

            var alignment = new Alignment(0, 0, 1, 1)
            {
                LeftPadding = 0,
                RightPadding = 0,
                BottomPadding = 0,
                TopPadding = 0,
                BorderWidth = 0
            };

            this._notebook = new Notebook();
            this._notebook.AppendPage(new Label(string.Empty), new Label("+") {WidthRequest = 20});

            alignment.Add(this._notebook);
            this.Add(alignment);

            this.InitializeEvents();
        }

        private void InitializeEvents()
        {
            this._notebook.SwitchPage += (o, args) =>
            {
                if (args.PageNum == (this._notebook.NPages - 1))
                {
                    this.AddTab(new Draft("New draft"));
                }
            };
        }

        private void Bind()
        {
            for (int i = 0; i < this._notebook.NPages; i++)
            {
                this._notebook.RemovePage(i);
            }

            if (this.Model != null)
            {
                this.Model.ForEach(this.AddTab);
            }
        }

        public void AddTab(Draft draft)
        {
            int targetIndex = this._notebook.NPages - 1;
            var tab = new Tab { Model = draft };
            tab.Header.Clicked += (sender, args) =>
            {
                var h = (TabHeader)sender;
                var note = (Notebook) h.Parent;

                var index = Array.FindIndex(this._notebook.Children, widget => widget == note.CurrentPageWidget);

                if (index > -1)
                {
                    this.RemoveTab(index);
                }

            };

            this._notebook.InsertPage(tab, tab.Header, targetIndex);
            this._notebook.ShowAll();
            this._notebook.CurrentPage = targetIndex;
            this.CurrentTab = tab;
        }

        public void RemoveTab(int index)
        {
            this._notebook.RemovePage(index);

            if (this._notebook.NPages == 1)
            {
                this.AddTab(new Draft("New draft"));
            }

            this._notebook.ShowAll();
        }
    }
}