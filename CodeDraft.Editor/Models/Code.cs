#region

using CodeDraft.API;

#endregion

namespace CodeDraft.Editor.Models
{
    public class Code
    {
        public Languages Language { get; private set; }

        public string Text { get; set; }

        public Code(Languages language) : this(language, string.Empty)
        {
        }

        public Code(Languages language, string text)
        {
            this.Language = language;
            this.Text = text;
        }
    }
}