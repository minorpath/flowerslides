using System;

namespace FlowerSlides
{
    public class FolderSelectedEventArgs : EventArgs
    {
        public FolderSelectedEventArgs(string path)
        {
            Path = path;
        }
        public string Path { get; private set; }
    }
}
