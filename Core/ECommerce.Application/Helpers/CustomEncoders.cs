using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.Helpers
{
    static public class CustomEncoders
    {
        public static string UrlEncode(this string value)
        {
            //url de kullanılabilecek formta getirme işlemi " vs. engellemek için
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            return WebEncoders.Base64UrlEncode(bytes);
        }
        public static string UrlDecode(this string value)
        {
            byte[] bytes = WebEncoders.Base64UrlDecode(value);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
