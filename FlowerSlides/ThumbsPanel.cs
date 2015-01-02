using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowerSlides
{

    public class ThumbsPanel : Panel
    {
        public event EventHandler<ImageClickedEventArgs> ImageClicked;
        public event EventHandler BackClicked;

        FlowLayoutPanel flp;
        PictureBox back;
        private Label FolderLabel;
        private Label DescriptionLabel;
        private string _currentFolder;
        public ThumbsPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = Globals.DarkGray;
            ForeColor = Globals.LightGray;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            ReleaseResources();
            
            flp = new FlowLayoutPanel();
            flp.Parent = this;
            flp.Location = new Point(50, 140);
            flp.Size = new Size(this.Width - 100, this.Height - 190);
            flp.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            flp.AutoScroll = true;

            back = new PictureBox();
            back.Size = new Size(40, 40);
            back.SizeMode = PictureBoxSizeMode.Zoom;
            back.Location = new Point(50, 63);
            back.Cursor = Cursors.Hand;
            back.Click += back_Click;
            back.Image = LoadEmbeddedImage("FlowerSlides.back.png");
            back.Parent = this;

            InitializeLabels();
        }

        private void ReleaseResources()
        {
            if (back != null && back.Image != null)
                back.Image.Dispose();

            if (flp != null)
            {
                foreach (Control ctrl in flp.Controls) 
                {
                    foreach (Control x in ctrl.Controls)
                    {
                        if (x is PictureBox)
                        {
                            var pb = x as PictureBox;
                            if (pb.Image != null)
                                pb.Image.Dispose();
                        }
                    }
                }

            }
            while (Controls.Count > 0)
                Controls[0].Dispose();
        }

        private Image LoadEmbeddedImage(string resourceName)
        {
            var _assembly = Assembly.GetExecutingAssembly();
            var _imageStream = _assembly.GetManifestResourceStream(resourceName);
            return new Bitmap(_imageStream);
        }

        public void Initialize()
        {
            InitializeComponent();
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

            FolderLabel = LabelUtil.CreateTitle(GetFolderName(CurrentFolder));
            FolderLabel.Location = new Point(105, 48);

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
                var image = Utils.LoadBitmap(thumbFile);
                Panel pp = new Panel();
                pp.BackColor = this.BackColor;
                pp.Parent = flp;
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

        void back_Click(object sender, EventArgs e)
        {
            if (BackClicked != null)
                BackClicked(sender, EventArgs.Empty);
        }

    }
}
