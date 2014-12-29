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
        FormWindowState _originalWindowState = FormWindowState.Maximized;

        public int count = 0;
        public Bitmap[] pictures;

        SlideshowRunner _slideshow;
        
        public Form1()
        {
            InitializeComponent();

            this.BackColor = Globals.DarkGray;
            this.pictureTitle.Visible = false;
            this.pictureTitle.Top = 40;
            this.pictureTitle.ForeColor = Globals.LightText;

            thumbsPanel1.BackColor = Globals.DarkGray;
            thumbsPanel1.ImageClicked += thumbsPanel1_ImageClicked;
            thumbsPanel1.CurrentFolder = @"C:\Users\hma\Pictures\Blomster";
            thumbsPanel1.Location = new Point(0,0);
            thumbsPanel1.Size = Size;

            blendPanel1.Location = new Point(0, 0);
            blendPanel1.Size = Size;
            blendPanel1.Hide();
        }

        void thumbsPanel1_ImageClicked(object sender, ImageClickedEventArgs e)
        {
            StartSlideshow(e.Filename);
        }

        private void StartSlideshow(string filename)
        {
            StartFullscreen();
            thumbsPanel1.Hide();
            blendPanel1.Show();
            _slideshow = new SlideshowRunner(filename, blendPanel1);
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
            thumbsPanel1.Show();
            blendPanel1.Hide();
            _slideshow.Stop();
        }

        private void StartFullscreen()
        {
            _originalWindowState = WindowState;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        private void ExitFullscreen()
        {
            FormBorderStyle = FormBorderStyle.Sizable;
            WindowState = _originalWindowState;
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
            _slideshow.PreviousPicture();
        }

        private void NextPicture()
        {
            _slideshow.NextPicture();
        }

        private void ShowPicture(string filename)
        {
            var image = Image.FromFile(filename);
            this.pictureTitle.Text = FormatPictureTitle(filename);
            this.pictureTitle.Visible = true;
        }

        private string FormatPictureTitle(string filename)
        {
            return Path.GetFileNameWithoutExtension(filename);
        }

    }
}
