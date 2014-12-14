using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowerSlides
{
    // TODO: Initial selection of folder to start slideshow from

    // Investigate slides transition
    // - http://stackoverflow.com/questions/6710631/transition-between-images-for-picturebox-in-c-sharp
    // - http://stackoverflow.com/questions/3270919/transition-of-images-in-windows-forms-picture-box
    public partial class Form1 : Form
    {
        string _folder = @"C:\Users\hma\Pictures";
        bool _slideshowStarted = false;
        List<string> files;
        int currentIndex = 0;

        // Thumbnails 10px mellom hvert i horisontal retning

        // Farge grå tekst
        Color dark = Color.FromArgb(29, 29, 29);
        Color greyText = Color.FromArgb(165, 165, 165);
        Color lightText = Color.FromArgb(214, 214, 214);

        // BlendPanel
        private float mBlend;
        private int mDir = 1;
        public int count = 0;
        public Bitmap[] pictures;

        public void myPhoto()
        {
            pictures = new Bitmap[9];
            pictures[0] = new Bitmap(@"C:\Users\hma\Pictures\Blomster\PenstemonPenshamCzar.jpg");
            pictures[1] = new Bitmap(@"C:\Users\hma\Pictures\Blomster\Penstemon-001.jpg");
            pictures[2] = new Bitmap(@"C:\Users\hma\Pictures\Blomster\4259-penstemon-fasciculatus.jpg");
            pictures[3] = new Bitmap(@"C:\Users\hma\Pictures\Blomster\Penstemon_campanulatus-CR.jpg");

            timer1.Interval = 50; //time of transition
            timer1.Tick += BlendTick;
            try
            {
                blendPanel1.Image1 = pictures[count];
                blendPanel1.Image2 = pictures[++count];
            }
            catch
            {

            }
            timer1.Enabled = true;
        }
        private void BlendTick(object sender, EventArgs e)
        {
            mBlend += mDir * 0.04F;
            if (mBlend > 1)
            {
                mBlend = 0.0F;
                if ((count + 1) < pictures.Length)
                {
                    blendPanel1.Image1 = pictures[count];
                    blendPanel1.Image2 = pictures[++count];
                }
                else
                {
                    blendPanel1.Image1 = pictures[count];
                    blendPanel1.Image2 = pictures[0];
                    count = 0;
                }
            }
            blendPanel1.Blend = mBlend;
        }
        public Form1()
        {
            InitializeComponent();

            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            this.BackColor = dark;
            this.pictureTitle.Visible = false;
            this.pictureTitle.Top = 40;
            this.pictureTitle.ForeColor = lightText;
            myPhoto();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                EndSlideshow();
        }

        private void StartSlideshow()
        {
            StartFullscreen();
            ShowPicture(files[0]);
        }

        private void EndSlideshow()
        {
            this.pictureTitle.Visible = false;
            ExitFullscreen();
            ShowThumbnails();
        }
       

        private void ShowThumbnails()
        {
//            throw new NotImplementedException();
        }

        private void StartFullscreen()
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        private void ExitFullscreen()
        {
            FormBorderStyle = FormBorderStyle.Sizable;
            WindowState = FormWindowState.Normal;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                if (_slideshowStarted)
                    return;

                files = Directory
                    .EnumerateFiles(_folder) //<--- .NET 4.5
                    .Where(file => file.ToLower().EndsWith("jpg") || file.ToLower().EndsWith("jpeg"))
                    .ToList();

                StartSlideshow();
            }
            if (e.KeyCode == Keys.Right)
            {
                NextPicture();
            }
            if (e.KeyCode == Keys.Left)
            {
                PreviousPicture();

            }
        }

        private void PreviousPicture()
        {
            currentIndex--;
            if (currentIndex == -1)
                currentIndex = files.Count-1;

            ShowPicture(files[currentIndex]);
        }

        private void NextPicture()
        {
            currentIndex++;
            if (currentIndex == files.Count)
                currentIndex = 0;

            ShowPicture(files[currentIndex]);
        }

        private void ShowPicture(string filename)
        {
            var image = Image.FromFile(filename);
            this.pictureBox1.Bounds = this.Bounds;
            this.pictureBox1.Height -= 80;
            this.pictureBox1.Top = 80;
            this.pictureBox1.Image = image;

            this.pictureTitle.Text = FormatPictureTitle(filename);
            this.pictureTitle.Visible = true;
        }

        private string FormatPictureTitle(string filename)
        {
            return Path.GetFileNameWithoutExtension(filename);
        }

    }
}
