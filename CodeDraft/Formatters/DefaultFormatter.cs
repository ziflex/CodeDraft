using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CodeDraft.API.Formatters
{
    public sealed class DefaultFormatter : IFormatter
    {
        public string Format(object target)
        {
            var result = string.Empty;

            if (target != null)
            {
                var type = target.GetType();

                if (type.IsClass)
                {
                    if (type.GetInterfaces().Contains(typeof (IEnumerable)))
                    {
                        result = this.FormatEnumerable(target);
                    }
                    else if (type != typeof (string))
                    {
                        result = this.FormatClass(target);
                    }
                }

                if (string.IsNullOrWhiteSpace(result))
                {
                    result = target.ToString();
                }
            }

            return result;
        }

        private string FormatEnumerable(object target)
        {
            var that = target as IEnumerable;
            var builder = new StringBuilder();
            int count = 0;

            builder.AppendLine(target.GetType().ToString());

            foreach (var item in that)
            {
                if (item != null)
                {
                    builder.AppendLine(item.ToString());
                }
                else
                {
                    builder.AppendLine("null");
                }

                count++;
            }

            builder.AppendLine(string.Format("Count: {0}", count));

            return builder.ToString();
        }

        private string FormatClass(object target)
        {
            var sb = new StringBuilder();
            Type type = target.GetType();
            PropertyInfo[] props = type.GetProperties();

            sb.AppendLine(type.ToString());

            foreach (var prop in props)
            {
                object val = prop.GetValue(target);

                sb.AppendLine("-----");
                sb.AppendLine(string.Format("Property: {0}", prop.Name));
                sb.AppendLine(string.Format("Type: {0}", prop.PropertyType));
                sb.AppendLine(string.Format("Value: {0}", val != null ? val.Dump() : "null"));
            }

            return sb.ToString();
        }
    }
}
