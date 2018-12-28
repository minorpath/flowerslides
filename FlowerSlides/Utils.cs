using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ImageResizer;

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
                        validFiles.Add(f.FullName);
                    }
                }
            }
            validFiles.Sort();
            return validFiles.ToArray();
        }

        /// <summary>
        /// Gets the path to a folder where the thumbnails for images
        /// found in the provided 'folder' parameter 
        /// </summary>
        /// <param name="folder">The path to the folder where images are stored</param>
        /// <returns></returns>
        internal static string GetThumbnailFolder(string path)
        {
            var thumbsFolder = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), "FlowerSlides", "Thumbs");
            var folder = Path.GetDirectoryName(path);
            var dirName = StripIllegalFilenameChars(folder);
            thumbsFolder = Path.Combine(thumbsFolder, dirName);
            return thumbsFolder;
        }

        internal static void BuildThumbnails(string _folder)
        {
            var files = Utils.GetValidFilesSorted(_folder);
            foreach (var file in files)
            {
                BuildThumbnail(file);
            }
        }

        internal static string BuildThumbnail(string filepath)
        {
            var thumbsFolder = GetThumbnailFolder(filepath);
            if (!Directory.Exists(thumbsFolder))
                Directory.CreateDirectory(thumbsFolder);

            var outfile = Path.GetFileName(filepath);
            outfile = Path.Combine(thumbsFolder, outfile);
            if (ShouldGenerateNewThumbnail(outfile))
            {
                ImageBuilder.Current.Build(filepath, outfile,
                    new ResizeSettings("height=120&format=jpg&autorotate=true"));
            }
            return outfile;
        }

        private static bool ShouldGenerateNewThumbnail(string outfile)
        {
            if (!File.Exists(outfile))
                return true;

            return false;
        }

        public static string StripIllegalFilenameChars(string path)
        {
            return path.Replace(":", "").Replace("\\", "");
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }


        public static Bitmap LoadBitmap(string path)
        {
            //Open file in read only mode
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            //Get a binary reader for the file stream
            using (BinaryReader reader = new BinaryReader(stream))
            {
                //copy the content of the file into a memory stream
                var memoryStream = new MemoryStream(reader.ReadBytes((int)stream.Length));
                //make a new Bitmap object the owner of the MemoryStream
                return new Bitmap(memoryStream);
            }
        }

    }
}
