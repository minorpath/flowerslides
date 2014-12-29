using System.Collections.Generic;
using System.IO;

namespace FlowerSlides
{
    class Utils
    {
        internal static string[] GetValidFilesSorted(string folder)
        {
            var validFiles = new List<string>();
            var validExtensions = new string[] { ".jpg", ".bmp", ".gif", ".png" };
            var info = new DirectoryInfo(folder);

            foreach (var f in info.GetFiles())
            {
                for (var i = 0; i < validExtensions.Length; i++)
                {
                    if (f.Extension.ToString().ToLower() == validExtensions[i].ToLower())
                    {
                        validFiles.Add(f.FullName.ToLowerInvariant());
                    }
                }
            }
            validFiles.Sort();
            return validFiles.ToArray();
        }
    }
}
