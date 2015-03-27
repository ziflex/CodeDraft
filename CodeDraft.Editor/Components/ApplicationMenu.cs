#region

using Gtk;

#endregion

namespace CodeDraft.Editor.Components
{
    public class ApplicationMenu : MenuBar
    {
        public ApplicationMenu()
        {
            this.Append(new MenuItem("File"));
            this.Append(new MenuItem("Edit"));
            this.Append(new MenuItem("Code"));
        }
    }
}