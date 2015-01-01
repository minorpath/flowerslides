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
        // Penstemon 'Watermelon Taffey' should return Penstemon in italics
        // The 'Watermelon Taffey' part is the Hybrid-name
        internal static string GetLatinName(string filepath)
        {
            var name = Path.GetFileNameWithoutExtension(filepath);
            var tokens = name.Split(new string[] { "-", "_", " " },
                StringSplitOptions.RemoveEmptyEntries);

            string value = "";
            foreach (string token in tokens)
            {
                if (IsNumber(token))
                    continue;

                if (value == "")
                {
                    value += ToTitleCase(token);
                }
                else if (token.StartsWith("'") || token.EndsWith("'"))
                {
                    // Ignore
                }
                else
                {
                    value += " " + token.ToLowerInvariant();
                }
            }
            return value;
        }

        // Penstemon 'Watermelon Taffey' should return 'Watermelon Taffey' in italics
        // The 'Watermelon Taffey' part is the Hybrid-name
        internal static string GetHybridName(string filepath)
        {
            var name = Path.GetFileNameWithoutExtension(filepath);
            var tokens = name.Split(new string[] { "-", "_", " " },
                StringSplitOptions.RemoveEmptyEntries);

            string value = "";
            foreach (string token in tokens)
            {
                if (IsNumber(token))
                    continue;

                if (token.StartsWith("'") || token.EndsWith("'"))
                {
                    if (value.Length > 0)
                        value += " ";
                    value += token;
                }
            }
            return value;
        }

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
