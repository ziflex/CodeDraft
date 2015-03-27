#region

using System.Collections.Generic;
using CodeDraft.API;

#endregion

namespace CodeDraft.Editor.Models
{
    public class Draft
    {
        public string Name { get; private set; }

        public Code Code { get; private set; }

        public ReferenceCollection References { get; private set; }

        public Draft(string name) : this(name, new Code(Languages.CSharp))
        {
        }

        public Draft(string name, Code code) : this(name, code, new List<Reference>())
        {
        }

        public Draft(string name, Code code, IEnumerable<Reference> references)
        {
            this.Name = name;
            this.Code = code;
            this.References = new ReferenceCollection(references);
        }
    }
}