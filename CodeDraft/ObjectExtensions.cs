#region

using System;
using System.Collections.Generic;
using System.Linq;
using CodeDraft.API.Formatters;

#endregion

namespace CodeDraft.API
{
    public static class Extensions
    {
        private static readonly List<IFormatter> formatters = new List<IFormatter>(); 

        static Extensions()
        {
            //formatters = AppDomain.CurrentDomain.GetAssemblies()
            //    .SelectMany(x => x.GetTypes())
            //    .Where(x => x != typeof(DefaultFormatter) && x.GetInterfaces().Contains(typeof(IFormatter)))
            //    .Select(Activator.CreateInstance)
            //    .Cast<IFormatter>()
            //    .ToList();
        }

        public static string Dump(this object that)
        {
            var formatter = formatters.FirstOrDefault();

            if (formatter == null)
            {
                formatter = new DefaultFormatter();
            }

            return formatter.Format(that);
        }
    }
}