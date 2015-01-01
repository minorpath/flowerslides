﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowerSlides
{

    public class ThumbsPanel : Panel
    {
        public event EventHandler<ImageClickedEventArgs> ImageClicked;

        private Label FolderLabel;
        private Label DescriptionLabel;
        private List<Panel> Thumbnails;

        private string _currentFolder;
        public ThumbsPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = Globals.DarkGray;
            ForeColor = Globals.LightGray;
            Thumbnails = new List<Panel>();
            Resize += ThumbsPanel_Resize;
        }

        void ThumbsPanel_Resize(object sender, EventArgs e)
        {
            RepositionThumbnails();
        }

        private void RepositionThumbnails()
        {
            int xOffset = 50;
            int xPosition = xOffset;
            int yPosition = 140;
            foreach(Panel pp in Thumbnails)
            {
                pp.Location = new Point(xPosition + 10, yPosition);

                // set position of next PictureBox 10 pixels to the right of the previous
                xPosition += pp.Width + 4;

                // Wrap thumbnails
                if (xPosition > (this.Width - (xOffset + pp.Width)))
                {
                    xPosition = xOffset;
                    yPosition += 120 + 10;
                }
            }
        }

        public void Initialize()
        {
            InitializeLabels();
            Utils.BuildThumbnails(_currentFolder);
            LoadImageScroller(this);
        }

        private void InitializeLabels()
        {
            if (CurrentFolder == null)
                return;
            Panel labelPanel = new Panel();
            labelPanel.Parent = this;
            labelPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            labelPanel.Width = this.Width;
            labelPanel.Height = 120;

            FolderLabel = new Label
            {
                AutoSize = true,
                Text = GetFolderName(CurrentFolder),
                Location = new Point(105, 48),
                Font = new Font("Segoe UI Light", 30F, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = Globals.LightText
            };
            labelPanel.Controls.Add(FolderLabel);

            int offsetX = FolderLabel.Width + 122;
            int offsetY = FolderLabel.Bottom;
            DescriptionLabel = new Label
            {
                AutoSize = true,
                Text = GetDescription(CurrentFolder),
                Left = offsetX,
                Font = new Font("Segoe UI Light", 16F, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = Globals.DarkText
            };
            labelPanel.Controls.Add(DescriptionLabel);
            DescriptionLabel.Top = FolderLabel.Bottom - (DescriptionLabel.Height + 6);
        }

        private string GetDescription(string folder)
        {
            var files = new DirectoryInfo(folder).GetFiles();
            return "Bildebibliotek " + files.Length + "  filer";
        }

        private string GetFolderName(string path)
        {
            if (string.IsNullOrEmpty(path))
                return "";
            path = Path.GetDirectoryName(path);
            var tokens = path.Split(new char[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
            return tokens[tokens.Length - 1];
        }

        public string CurrentFolder
        {
            get { return _currentFolder; }
            set { 
                _currentFolder = value;
            }
        }

        /// <summary>
        /// Warning: This method is very memory consuming
        /// </summary>
        /// <param name="p"></param>
        private void LoadImageScroller(Panel p)
        {
            if (_currentFolder == null)
                return;

            int xOffset = 50;
            int xPosition = xOffset;
            int yPosition = 140;
            int imageCount = 0;

            string[] files = Utils.GetValidFilesSorted(_currentFolder);
            foreach (string filename in files)
            {
                var thumbFile = Utils.BuildThumbnail(filename);
                var image = Image.FromFile(thumbFile);

                Panel pp = new Panel();
                pp.BackColor = this.BackColor;
                pp.Parent = p;
                pp.Location = new Point(xPosition + 10, yPosition);
                pp.Height = 120 + 6;
                pp.Width = CalcProportionalWidth(image, pp.Height) + 6;

                PictureBox pb = new PictureBox();
                pb.Name = "ImagePB" + imageCount;
                pb.Cursor = Cursors.Hand;

                ToolTip t = new ToolTip();
                t.InitialDelay = 500;
                t.SetToolTip(pb, FormattingTools.FormatPlantName(filename));

                //set the Parent of our control to our Panel, this will cause
                //the PictureBox to be aded to our Panel
                pb.Parent = pp;
                pb.Size = new Size(pp.Width - 6, pp.Height - 6);
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.Location = new Point(3, 3);
                pb.Image = image;
                pb.Image.Tag = filename;

                pb.MouseEnter += new EventHandler(pb_MouseEnter);
                //pb.MouseHover += new EventHandler(pb_MouseHover);
                pb.MouseLeave += new EventHandler(pb_MouseLeave);
                pb.Click += new EventHandler(pb_Click);

                // set position of next PictureBox 10 pixels to the right of the previous
                xPosition += pb.Width + 10;

                // Wrap thumbnails
                if (xPosition > (this.Width - (xOffset+pp.Width)))
                {
                    xPosition = xOffset;
                    yPosition += 120 + 10;
                }
                imageCount += 1;

                Thumbnails.Add(pp);
            }
        }

        private int CalcProportionalWidth(Image img, int height)
        {
            float f = (float)height / img.Height;
            return (int)(img.Width * f);
        }

        void pb_MouseEnter(object sender, EventArgs e)
        {
            ((PictureBox)sender).Parent.BackColor = this.ForeColor;
        }

        void pb_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).Parent.BackColor = this.BackColor;
        }

        void pb_Click(object sender, EventArgs e)
        {
            var filename = ((PictureBox)sender).Image.Tag.ToString();
            if (ImageClicked != null)
                ImageClicked(sender, new ImageClickedEventArgs(filename));
        }

    }
}