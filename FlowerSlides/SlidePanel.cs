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
using FlowerSlides.Extensions;

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
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            if (pb != null && pb.Image != null)
                pb.Image.Dispose();
            while (Controls.Count > 0)
                Controls[0].Dispose();

            pb = new PictureBox();
            pb.Parent = this;
            pb.Size = new Size(10,10);
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            pb.Location = new Point(10,10);

            labelPanel = new Panel();
            labelPanel.Parent = this;
            labelPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            labelPanel.Width = this.Width;
            labelPanel.Height = 60;
        }

        public void Initialize()
        {
            pb.Size = new Size(this.Width - 100, this.Height - 120);
            int x = (this.Width - pb.Width) / 2;
            pb.Location = new Point(x, 60);
            UpdateLabels();
            LoadImage(this);
        }

        private void UpdateLabels()
        {
            if (CurrentFile == null)
                return;
            
            labelPanel.SuspendLayout();

            while (labelPanel.Controls.Count > 0)
                labelPanel.Controls[0].Dispose();
            
            LatinNameLabel = new Label
            {
                AutoSize = true,
                Text = FormattingTools.GetLatinName(CurrentFile),
                Location = new Point(105, -10),
                Font = new Font("Segoe UI Light", 30F, FontStyle.Italic | FontStyle.Bold, GraphicsUnit.Point),
                ForeColor = Globals.LightText
            };
            labelPanel.Controls.Add(LatinNameLabel);

            HybridNameLabel = new Label
            {
                AutoSize = true,
                Text = FormattingTools.GetHybridName(CurrentFile),
                Location = new Point(105, -10),
                Font = new Font("Segoe UI Light", 30F, FontStyle.Regular | FontStyle.Bold, GraphicsUnit.Point),
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

            pb.Image = Utils.LoadBitmap(_currentFile);
            pb.Image.ExifRotate();

            //Release resources from old image
            if (oldImage != null)
                ((IDisposable)oldImage).Dispose(); 

        }

    }
}
