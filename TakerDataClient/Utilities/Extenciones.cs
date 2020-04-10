using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebTakerData.Utilities
{
    public static class Extenciones
    {
        public static bool IsNull(this object T)
        {
            return T == null;
        }

        public static string removeDiacritics(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            text = text.Normalize(NormalizationForm.FormD);
            var chars = text.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            string strJoin = new string(chars).Normalize(NormalizationForm.FormC);
            strJoin = strJoin.Replace('Y', 'I');
            strJoin = strJoin.ToUpper();
            strJoin = strJoin.Trim();
            return strJoin.Replace(" ", string.Empty);
        }

        public static string removeDiacriticNormal(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;
            text = text.Normalize(NormalizationForm.FormD);
            var chars = text.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            string strJoin = new string(chars).Normalize(NormalizationForm.FormC);
            strJoin = strJoin.Replace('Y', 'I');
            strJoin = strJoin.Trim();
            return strJoin.Replace(" ", string.Empty);
        }
    }
}
