#region

using Gtk;

#endregion

namespace CodeDraft.Editor.Components.Tabs.References
{
    public class ReferenceChooser : Window
    {
        public ReferenceChooser() : base(WindowType.Popup)
        {
            this.WindowPosition = WindowPosition.CenterOnParent;
        }

        public string Choose()
        {
            string result = null;

            var fc = new FileChooserDialog("Select reference",
                this,
                FileChooserAction.Open,
                "Cancel", ResponseType.Cancel,
                "Open", ResponseType.Accept);

            if (fc.Run() == (int) ResponseType.Accept)
            {
                result = fc.Filename;
            }

            fc.Destroy();

            return result;
        }
    }
}