namespace CodeDraft.Editor.Models
{
    public class Reference
    {
        public string Location { get; private set; }

        public Reference(string location)
        {
            this.Location = location;
        }
    }
}