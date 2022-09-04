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
using WMPLib;

namespace STAGApp.Forms
{
    public partial class FormPlayer : Form
    {
        public FormPlayer()
        {
            InitializeComponent();
        }

        private void listFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            MediaFile file = listFiles.SelectedItem as MediaFile;
            if (file!=null)
            {
                axWindowsMediaPlayer.URL = file.Path;
                axWindowsMediaPlayer.Ctlcontrols.play();
            }
        }

        private void b1Otevři_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Multiselect = true, ValidateNames = true, Filter = "WMV|*.wmv|WAV|*.wav|MP3|*.mp3|MP4|*.mp4|MKV|*.mkv|AVI|*.avi" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    List<MediaFile> files = new List<MediaFile>();
                    foreach (string fileName in ofd.FileNames)
                    {
                        FileInfo fi = new FileInfo(fileName);
                        files.Add(new MediaFile() { FileName = Path.GetFileNameWithoutExtension(fi.FullName), Path = fi.FullName });
                    }

                    listFiles.DataSource = files;
                    listFiles.ValueMember = "Path";
                    listFiles.DisplayMember = "FileName";
                }
            }
        }

        private void FormPlayer_Load(object sender, EventArgs e)
        {
            listFiles.ValueMember = "Path";
            listFiles.DisplayMember = "FileName";
        }
    }
}
