#region



#endregion

namespace CodeDraft.Editor
{
    internal class MainClass
    {
        public static void Main(string[] args)
        {
            Gtk.Application.Init();
            var win = new Application();
            win.ShowAll();
            win.Title = "Code Draft";
            Gtk.Application.Run();
        }
    }
}