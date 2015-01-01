using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FlowerSlides
{
    class SlideshowRunner
    {
        string SlideshowFolder;
        string[] _files;
        SlidePanel slidePanel;
        int currentIndex = 0;

        public SlideshowRunner(string initialFilename, SlidePanel panel)
        {
            SlideshowFolder = Path.GetDirectoryName(initialFilename);
            _files = Utils.GetValidFilesSorted(SlideshowFolder);
            currentIndex = FindIndex(_files, initialFilename);
            slidePanel = panel;
        }

        private int FindIndex(string[] files, string filename)
        {
            for (var i = 0; i < files.Length; i++)
            {
                if (files[i].ToLowerInvariant() ==
                    filename.ToLowerInvariant())
                {
                    return i;
                }
            }
            throw new Exception("Could not find file: " + filename);
        }

        private void BlendTick(object sender, EventArgs e)
        {
        }

        internal void Stop()
        {
            //this._timer.Stop();
        }

        internal void NextPicture()
        {
            //_timer.Stop();
            if ((currentIndex + 1) < _files.Length)
            {
                // TODO: Attempt to optimize memory...
                slidePanel.CurrentFile = _files[++currentIndex];
                slidePanel.Initialize();
                //_timer.Start();
            }
        }

        internal void PreviousPicture()
        {
            //_timer.Stop();
            if ((currentIndex -1 ) >= 0)
            {
                slidePanel.CurrentFile = _files[--currentIndex];
                slidePanel.Initialize();
                //_timer.Start();
            }
        }
    }
}
