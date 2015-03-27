#region

using CodeDraft.Editor.Components;
using CodeDraft.Editor.Components.Tabs;
using Gtk;

#endregion

namespace CodeDraft.Editor
{
    public class Application : Window
    {
        private readonly ApplicationMenu _menu;

        private readonly TabContainer _tabs;

        public Application()
            : base(WindowType.Toplevel)
        {
            var box = new VBox(false, 2);
            this._menu = new ApplicationMenu();
            this._tabs = new TabContainer();

            box.PackStart(this._menu, false, true, 0);
            box.PackStart(this._tabs, true, true, 0);

            this.Add(box);

            this.Resize(1000, 800);
            this.SetPosition(WindowPosition.Center);
        }

        protected void OnDeleteEvent(object sender, DeleteEventArgs a)
        {
            Gtk.Application.Quit();
            a.RetVal = true;
        }
    }
}