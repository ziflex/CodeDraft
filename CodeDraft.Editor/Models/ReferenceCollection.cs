#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace CodeDraft.Editor.Models
{
    public class ReferenceCollection : List<Reference>
    {
        public ReferenceCollection()
        {
        }

        public ReferenceCollection(IEnumerable<Reference> references) : base(references)
        {
        }

        public bool Contains(string location)
        {
            return this.Any(x => string.Compare(x.Location, location, StringComparison.InvariantCultureIgnoreCase) == 0);
        }
    }
}