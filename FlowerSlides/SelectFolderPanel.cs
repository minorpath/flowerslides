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
    class SelectFolderPanel : Panel
    {
        public event EventHandler<FolderSelectedEventArgs> FolderSelected;

        private string SettingsFilePath;
        public SelectFolderPanel(string settingsFilePath)
        {
            SettingsFilePath = settingsFilePath;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();

        }

        private void InitializeComponent()
        {

            Label title = LabelUtil.CreateTitle("FlowerSlides");
            title.Location = new Point(105, 48);
            this.Controls.Add(title);

            Label start = LabelUtil.CreateSubTitle("Start");
            start.Location = new Point(112, 160);
            this.Controls.Add(start);

            Label openFolder = LabelUtil.CreateLabel("Velg katalog...", 12F, Globals.BlueText, FontStyle.Bold);
            openFolder.Location = new Point(114, 200);
            openFolder.Click += openFolder_Click;
            openFolder.Cursor = Cursors.Hand;
            this.Controls.Add(openFolder);

            Label recent = LabelUtil.CreateSubTitle("Nylig brukte plasseringer");
            recent.Location = new Point(113, 260);
            this.Controls.Add(recent);

            if (!string.IsNullOrEmpty(SettingsFilePath) &&
                File.Exists(SettingsFilePath))
            {
                var lines = File.ReadAllLines(SettingsFilePath);
                int yPos = 300;
                foreach (var line in lines)
                {
                    if (Directory.Exists(line))
                    {
                        Label recentPath = LabelUtil.CreateLabel(line, 12F, Globals.BlueText, FontStyle.Bold);
                        recentPath.Location = new Point(114, yPos);
                        recentPath.Cursor = Cursors.Hand;
                        recentPath.Click += recentPath_Click;
                        this.Controls.Add(recentPath);
                        yPos += 25;
                    }
                }
            }

        }

        void recentPath_Click(object sender, EventArgs e)
        {
            var path = (sender as Label).Text;
            if (!Directory.Exists(path))
                MessageBox.Show("Følgende plassering er ikke lenger gyldig: " + Environment.NewLine + path);

            UpdateRecentPaths(path);
            if (FolderSelected != null)
                FolderSelected(this, new FolderSelectedEventArgs(path));
        }

        void openFolder_Click(object sender, EventArgs e)
        {
            var browser = new FolderBrowserDialog();
            if (browser.ShowDialog() == DialogResult.OK)
            {
                var path = browser.SelectedPath;
                UpdateRecentPaths(path);
                if (FolderSelected != null)
                    FolderSelected(this, new FolderSelectedEventArgs(path));

            }
        }

        private void UpdateRecentPaths(string path)
        {
            if (string.IsNullOrEmpty(SettingsFilePath))
                return;
            if (File.Exists(SettingsFilePath))
            {
                var lines = File.ReadAllLines(SettingsFilePath);
                List<string> paths = new List<string>();
                paths.AddRange(lines);

                var listEntry = paths.Where(entry =>
                      string.Equals(entry, path, StringComparison.CurrentCultureIgnoreCase))
                      .FirstOrDefault();

                if (listEntry != null)
                    paths.Remove(listEntry);
                paths.Insert(0, path);
                while (paths.Count > 5)
                    paths.RemoveAt(5);
                File.WriteAllLines(SettingsFilePath, paths.ToArray());
            }
            else
            {
                var dir = Path.GetDirectoryName(SettingsFilePath);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                File.WriteAllText(SettingsFilePath, path);
            }
        }



        internal void Initialize()
        {
            InitializeComponent();
        }
    }
}
