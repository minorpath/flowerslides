using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerSlides
{
    public static class FormattingTools
    {
        public static string FormatPlantName(string name)
        {
            // 4259-penstemon-fasciculatus.jpg
            name = Path.GetFileNameWithoutExtension(name);
            var tokens = name.Split(new string[] { "-", "_", " " }, 
                StringSplitOptions.RemoveEmptyEntries);

            string value = "";
            foreach(string token in tokens)
            {
                if( IsNumber(token) )
                    continue;
                
                if( value == "" )
                {
                    value += ToTitleCase(token);
                }
                else if( token.StartsWith("'") || token.EndsWith("'"))
                {
                    value += " " + ToTitleCase(token);
                }
                else
                {
                    value += " " + token.ToLowerInvariant();
                }
            }
            return value;
        }

        private static string ToTitleCase(string token)
        {
            TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
            return textInfo.ToTitleCase(token);
        }

        private static bool IsNumber(string token)
        {
            int temp = 0;
            return int.TryParse(token, out temp);
        }
    }
}
