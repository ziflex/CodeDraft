#region

using System;
using Gtk;

#endregion

namespace CodeDraft.Editor.Components.Tabs
{
    public class TabHeader : HBox
    {
        private readonly Button _button;
        private readonly Label _label;
        private string _title;

        public event EventHandler Clicked;

        public string Title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
                this._label.Text = value;
            }
        }

        public TabHeader()
        {
            this._label = new Label();

            this.PackStart(this._label, true, true, 5);

            this._button = new Button
            {
                Relief = ReliefStyle.None,
                FocusOnClick = false
            };
            this._button.Add(new Image(Stock.Close, IconSize.Menu) {WidthRequest = 15, HeightRequest = 15});

            this.PackStart(this._button, false, false, 5);

            this.ShowAll();

            this.InitializeEvents();
        }

        private void InitializeEvents()
        {
            this._button.Clicked += (sender, args) =>
            {
                if (this.Clicked != null)
                {
                    this.Clicked.Invoke(this, new EventArgs());
                }
            };
        }
    }
}