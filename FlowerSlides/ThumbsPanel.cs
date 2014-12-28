using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowerSlides
{
    public class ThumbsPanel : Panel
    {
        private string _currentFolder;
        public ThumbsPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = Color.IndianRed;
            ForeColor = Color.FromArgb(96, 96, 96);
        }

        public string CurrentFolder
        {
            get { return _currentFolder; }
            set { 
                _currentFolder = value;
                LoadImageScroller(this);
                //Invalidate(); 
            }
        }


        /// <summary>
        /// method for grabbing all the images from your MyPictures directory and load up
        /// a custom Image Scroller for use in a WinForms application
        /// </summary>
        /// <param name="p"></param>
        private void LoadImageScroller(Panel p)
        {
            if (_currentFolder == null)
                return;
            int xPosition = 0;
            int imageCount = 0;

            //string array to hold the valid image formats we want
            string[] validExtensions = new string[] { ".jpg", ".bmp", ".gif", ".png" };

            //now a DirectoryInfo object holding the information from our
            //My Pictures directory
            DirectoryInfo info = new DirectoryInfo(_currentFolder);

            foreach (FileInfo f in info.GetFiles())
            {
                for (int i = 0; i < validExtensions.Length; i++)
                {
                    if (f.Extension.ToString().ToLower() == validExtensions[i].ToLower())
                    {
                        PictureBox pb = new PictureBox();
                        pb.Name = "ImagePB" + imageCount;
                        pb.Cursor = Cursors.Hand;

                        //set the Parent of our control to our Panel, this will cause
                        //the PictureBox to be aded to our Panel
                        pb.Parent = p;
                        pb.Size = new Size(130, 98);

                        //set the SizeMode to StretchImage (this will create thumbnails of our images)
                        pb.SizeMode = PictureBoxSizeMode.StretchImage;

                        //now we se the position of the PictureBox (this is where the position variable comes into
                        //play. We set it to it's value plus 10 (brings it off the left edge of the Panel) and
                        //20 pixels down (so it's not riding the top of the Panel)
                        pb.Location = new Point(xPosition + 10, 20);

                        //Here we use the Image.FromFile method to create a new image
                        //from the current file name and set the PictureBox's Image value to it
                        pb.Image = Image.FromFile(f.FullName);

                        //set the Tag property of the image to the current image name
                        pb.Image.Tag = f.FullName;

                        pb.MouseHover += new EventHandler(pb_MouseHover);
                        pb.MouseLeave += new EventHandler(pb_MouseLeave);
                        pb.Click += new EventHandler(pb_Click);

                        //increment the position value, this makes the next PictureBox
                        //to be 5 pixels to the right of the previous
                        xPosition += pb.Width + 10;

                        //increate the count
                        imageCount += 1;
                    }
                }
            }

            //once we are done loading the images display how many and what directory we're in
            //label1.Text = string.Format("{0} image(s) available in directory {1}", count, Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
        }



        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    if (mImg1 == null || mImg2 == null)
        //        e.Graphics.FillRectangle(new SolidBrush(this.BackColor), new Rectangle(0, 0, this.Width, this.Height));
        //    else
        //    {
        //        var rc1 = ResizedRect(mImg1);
        //        var rc2 = ResizedRect(mImg2);
        //        var cm = new ColorMatrix() { Matrix33 = mBlend };
        //        var ia = new ImageAttributes();
        //        ia.SetColorMatrix(cm);
        //        e.Graphics.DrawImage(mImg2, rc2, 0, 0, mImg2.Width, mImg2.Height, GraphicsUnit.Pixel, ia);
        //        cm.Matrix33 = 1F - mBlend;
        //        ia.SetColorMatrix(cm);
        //        e.Graphics.DrawImage(mImg1, rc1, 0, 0, mImg1.Width, mImg1.Height, GraphicsUnit.Pixel, ia);
        //    }
        //    base.OnPaint(e);
        //}

        private Rectangle ResizedRect(Image img)
        {
            float f = (float)this.Height / img.Height;
            int width = (int)(img.Width * f);
            int x = width < this.Width ?
                (this.Width - width) / 2 : 0;

            return new Rectangle(x, 0, width, this.Height);
        }

        void pb_MouseHover(object sender, EventArgs e)
        {
            ((PictureBox)sender).BorderStyle = BorderStyle.Fixed3D;
        }

        void pb_MouseLeave(object sender, EventArgs e)
        {
            //here we are converting the sender (what is clicked) to a PictureBox and
            //removing the border when the mouse leaves it
            ((PictureBox)sender).BorderStyle = BorderStyle.None;
        }

        void pb_Click(object sender, EventArgs e)
        {
            //here we find which PictureBox is clicked and display it's name
            MessageBox.Show(string.Format("Selected Image: {0}", ((PictureBox)sender).Image.Tag.ToString()));
        }


    }
}
