using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebTakerData.Utilities
{
    public static class Csv
    {
        public static void exportToFile<T>(IEnumerable<T> values, char tabDelimited, bool includeHeaderLine, string path) where T : class
        {
            File.WriteAllText(path, export(values, tabDelimited, includeHeaderLine));
        }

        public static byte[] exportToBinary<T>(IEnumerable<T> values, char tabDelimited, bool includeHeaderLine) where T : class
        {
            return Encoding.UTF8.GetBytes(export(values, tabDelimited, includeHeaderLine));
        }

        public static Stream exportToStream<T>(IEnumerable<T> values, char tabDelimited, bool includeHeaderLine) where T : class
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(export(values, tabDelimited, includeHeaderLine)));
        }

        private static string export<T>(IEnumerable<T> values, char tabDelimited, bool includeHeaderLine) where T : class
        {
            StringBuilder sb = new StringBuilder();

            IList<PropertyInfo> propertyInfos = typeof(T).GetProperties();

            if (includeHeaderLine)
            {
                foreach (PropertyInfo property in propertyInfos)
                    sb.Append(property.Name).Append(tabDelimited);

                sb.Remove(sb.Length - 1, 1).AppendLine();
            }

            foreach (T item in values)
            {
                foreach (PropertyInfo property in propertyInfos)
                    sb.Append(MakeValueCsvFriendly(property.GetValue(item, null))).Append(tabDelimited);

                sb.Remove(sb.Length - 1, 1).AppendLine();
            }

            return sb.ToString();
        }

        private static string MakeValueCsvFriendly(object value)
        {
            if (value == null) return string.Empty;
            if (value is Nullable && ((INullable)value).IsNull) return string.Empty;

            if (value is DateTime)
            {
                if (((DateTime)value).TimeOfDay.TotalSeconds == 0)
                    return ((DateTime)value).ToString("yyyy-MM-dd");
                return ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss");
            }
            string output = value.ToString();

            if (output.Contains(",") || output.Contains("\""))
                output = '"' + output.Replace("\"", "\"\"") + '"';

            return output;
        }
    }
}
