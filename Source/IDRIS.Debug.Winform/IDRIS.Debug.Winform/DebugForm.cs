// FormMain.cs - 08/02/2018

using System;
using System.IO;
using System.Windows.Forms;

namespace IDRIS.Debug.Winform
{
    public partial class DebugForm : Form
    {
        private string appTitle = "IDRIS Debug";

        public DebugForm()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Text = appTitle;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileInfo fileInfo = new FileInfo(Application.ExecutablePath);
            string version = fileInfo.LastWriteTime.ToString("yyyy.MM.dd.HHmm");
            MessageBox.Show(
                $"{Environment.CurrentDirectory}\r\n\r\nVersion {version}"
                , $"About {appTitle}"
                , MessageBoxButtons.OK);
        }
    }
}
