using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ECommerceApi.Infrastructure.Operations
{
    public static class NameOperations
    {
        public static string CharacterRegulatory(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

           
            name = name.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new();
            foreach (char c in name)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }
            name = sb.ToString().Normalize(NormalizationForm.FormC);

            
            name = Regex.Replace(name, @"[^a-zA-Z0-9\s-]", "");

            
            name = Regex.Replace(name, @"\s+", "-").Trim('-');

            return name.ToLower();
        }

    }
}
