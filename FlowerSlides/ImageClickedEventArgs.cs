using System;

namespace FlowerSlides
{
    public class ImageClickedEventArgs : EventArgs
    {
        public ImageClickedEventArgs(string filename)
        {
            Filename = filename;
        }
        public string Filename { get; private set; }
    }
}
