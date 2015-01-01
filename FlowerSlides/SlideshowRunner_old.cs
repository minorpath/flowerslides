using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FlowerSlides
{
    class SlideshowRunner_old
    {
        string SlideshowFolder;
        string[] _files;
        BlendPanel blendPanel1;
        Bitmap bm1;
        Bitmap bm2;

        float mBlend;
        Timer _timer;
        int currentIndex = 0;

        public SlideshowRunner_old(string initialFilename, BlendPanel panel)
        {
            SlideshowFolder = Path.GetDirectoryName(initialFilename);
            _files = Utils.GetValidFilesSorted(SlideshowFolder);
            currentIndex = FindIndex(_files, initialFilename);
            blendPanel1 = panel;
            blendPanel1.Image1 = new Bitmap(_files[currentIndex]);
            blendPanel1.Image2 = null;
            blendPanel1.Blend = 0.0F;
            _timer = new Timer();
            _timer.Interval = 50; //time of transition
            _timer.Tick += BlendTick;
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
            mBlend += 0.1F;
            if (mBlend > 1)
            {
                _timer.Stop();
            }
            blendPanel1.Blend = mBlend;
        }

        internal void Stop()
        {
            this._timer.Stop();
        }

        internal void NextPicture()
        {
            _timer.Stop();
            if ((currentIndex + 1) < _files.Length)
            {
                // Attempt to optimize memory...
                //if( blendPanel1.Image2 != null )
                //    blendPanel1.Image1 = bm2;
                blendPanel1.Image1 = new Bitmap(_files[currentIndex]);
                blendPanel1.Image2 = new Bitmap(_files[++currentIndex]);
                //bm2 = new Bitmap(_files[++currentIndex].FullName);
                //blendPanel1.Image2 = bm2;
                mBlend = 0.0F;
                blendPanel1.Blend = mBlend;
                _timer.Start();
            }
        }

        internal void PreviousPicture()
        {
            _timer.Stop();
            if ((currentIndex -1 ) >= 0)
            {
                blendPanel1.Image1.Dispose();
                if( blendPanel1.Image2 != null )
                    blendPanel1.Image2.Dispose();
                blendPanel1.Image1 = new Bitmap(_files[currentIndex]);
                blendPanel1.Image2 = new Bitmap(_files[--currentIndex]);
                mBlend = 0.0F;
                blendPanel1.Blend = mBlend;
                _timer.Start();
            }
        }
    }
}
