using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowerSlides
{

    public class SlidePanel : Panel
    {
        private Panel labelPanel;
        private Label LatinNameLabel;
        private Label HybridNameLabel;
        private PictureBox pb;

        private string _currentFile;
        public string CurrentFile
        {
            get { return _currentFile; }
            set
            {
                _currentFile = value;
            }
        }

        public SlidePanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            //BackColor = Globals.DarkGray;
            //ForeColor = Globals.LightGray;
            this.BackColor = Color.Yellow;
            InitializeComponent();
            //PictureBox pict = new PictureBox();
            //pict.Size = new Size(100, 100);
            //pict.Location = new Point(100, 100);
            //pict.Image = Image.FromFile(@"C:\Users\hma\Pictures\Blomster\Penstemon 'Raven'.jpg");
            //pict.SizeMode = PictureBoxSizeMode.Zoom;
            //pict.BackColor = Color.Pink;
            //pict.Parent = this;
        }

        private void InitializeComponent()
        {
            pb = new PictureBox();
            pb.Parent = this;
            pb.Size = new Size(10,10);
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            pb.Location = new Point(10,10);
            pb.Click += new EventHandler(pb_Click);

            labelPanel = new Panel();
            labelPanel.Parent = this;
            labelPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            labelPanel.Width = this.Width;
            labelPanel.Height = 120;
        }

        public void Initialize()
        {
            pb.Size = new Size(this.Width - 100, this.Height - 120);
            int x = (this.Width - pb.Width) / 2;
            pb.Location = new Point(x, 120);
            UpdateLabels();
            LoadImage(this);
        }

        private void UpdateLabels()
        {
            if (CurrentFile == null)
                return;
            
            labelPanel.SuspendLayout();
            labelPanel.Controls.Clear();
            
            LatinNameLabel = new Label
            {
                AutoSize = true,
                Text = FormattingTools.GetLatinName(CurrentFile),
                Location = new Point(105, 48),
                Font = new Font("Segoe UI Light", 30F, FontStyle.Italic, GraphicsUnit.Point),
                ForeColor = Globals.LightText
            };
            labelPanel.Controls.Add(LatinNameLabel);

            HybridNameLabel = new Label
            {
                AutoSize = true,
                Text = FormattingTools.GetHybridName(CurrentFile),
                Location = new Point(105, 48),
                Font = new Font("Segoe UI Light", 30F, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = Globals.LightText
            };
            labelPanel.Controls.Add(HybridNameLabel);

            // Center the labels
            SizeF size1;
            SizeF size2;
            using (Graphics g = CreateGraphics())
            {
                size1 = g.MeasureString(LatinNameLabel.Text, LatinNameLabel.Font, this.Width);
                size2 = g.MeasureString(HybridNameLabel.Text, HybridNameLabel.Font, this.Width);
            }
            var totalWidth = (int)(Math.Ceiling(size1.Width) + Math.Ceiling(size2.Width));
            int offsetX = (this.Width - totalWidth) / 2;
            LatinNameLabel.Left = offsetX;
            HybridNameLabel.Left = offsetX + LatinNameLabel.Width;

            labelPanel.ResumeLayout();
        }

        private void LoadImage(Panel p)
        {
            if (_currentFile == null)
                return;

            //Backup old image in pictureBox
            var oldImage = pb.Image;

            pb.Image = LoadBitmap(_currentFile);
            
            //Release resources from old image
            if (oldImage != null)
                ((IDisposable)oldImage).Dispose(); 

        }

        public static Bitmap LoadBitmap(string path)
        {
            //Open file in read only mode
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            //Get a binary reader for the file stream
            using (BinaryReader reader = new BinaryReader(stream))
            {
                //copy the content of the file into a memory stream
                var memoryStream = new MemoryStream(reader.ReadBytes((int)stream.Length));
                //make a new Bitmap object the owner of the MemoryStream
                return new Bitmap(memoryStream);
            }
        }
        void pb_Click(object sender, EventArgs e)
        {
            var filename = ((PictureBox)sender).Image.Tag.ToString();
            //if (ImageClicked != null)
            //    ImageClicked(sender, new ImageClickedEventArgs(filename));
        }

    }
}
