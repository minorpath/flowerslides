using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        string _folder = @"C:\Users\hma\Pictures\Blomster";
        bool _slideshowStarted = false;
        FormWindowState _originalWindowState = FormWindowState.Maximized;

        public int count = 0;
        public Bitmap[] pictures;

        SlideshowRunner _slideshow;
        
        public Form1()
        {
            InitializeComponent();
            var settingsFile = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), "FlowerSlides", "Settings.ini");

            // TODO: Read/update last used folder
            // Ask user which folder to use if it does not exist
            // Add option to start program with a folder as input

            this.BackColor = Globals.DarkGray;
            this.pictureTitle.Visible = false;
            this.pictureTitle.Top = 40;
            this.pictureTitle.ForeColor = Globals.LightText;

            thumbsPanel1.BackColor = Globals.DarkGray;
            thumbsPanel1.ImageClicked += thumbsPanel1_ImageClicked;
            thumbsPanel1.CurrentFolder = _folder;
            thumbsPanel1.Location = new Point(0,0);
            thumbsPanel1.Size = Size;

            blendPanel1.Location = new Point(0, 0);
            blendPanel1.Size = Size;
            blendPanel1.Hide();

            slidePanel1.Location = new Point(0, 0);
            slidePanel1.Size = Size;
            slidePanel1.Hide();

            thumbsPanel1.Initialize();

        }

        void thumbsPanel1_ImageClicked(object sender, ImageClickedEventArgs e)
        {
            StartSlideshow(e.Filename);
        }

        private void StartSlideshow(string filename)
        {
            StartFullscreen();
            thumbsPanel1.Hide();
            slidePanel1.CurrentFile = filename;
            slidePanel1.Initialize();
            slidePanel1.Show();
            _slideshow = new SlideshowRunner(filename, slidePanel1);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                EndSlideshow();
        }

        private void EndSlideshow()
        {
            this.pictureTitle.Visible = false;
            ExitFullscreen();
            thumbsPanel1.Show();
            slidePanel1.Hide();
            //blendPanel1.Hide();
            //_slideshow.Stop();
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
