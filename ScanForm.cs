using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Drift
{
    public partial class ScanForm : Form
    {
        private string fromApp;
        private string toApp;
        private List<string> items;
        private string fromPath;
        private string toPath;

        public ScanForm(string from, string to, List<string> selectedItems)
        {
            InitializeComponent();
            fromApp = from;
            toApp = to;
            items = selectedItems;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScanForm));
            this.SuspendLayout();
            // 
            // ScanForm
            // 
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ScanForm";
            this.Text = "Scanning Apps...";
            this.Load += new System.EventHandler(this.ScanForm_Load);
            this.ResumeLayout(false);

        }

        private void ScanForm_Load(object sender, EventArgs e)
        {
            fromPath = FindDefaultPath(fromApp);
            toPath = FindDefaultPath(toApp);

            if (string.IsNullOrEmpty(fromPath))
            {
                MessageBox.Show($"Could not find {fromApp} profile folder automatically.\nPlease select it manually.", "Profile Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                fromPath = AskUserForPath();
            }

            if (string.IsNullOrEmpty(toPath))
            {
                MessageBox.Show($"Could not find {toApp} profile folder automatically.\nPlease select it manually.", "Profile Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                toPath = AskUserForPath();
            }

            if (!string.IsNullOrEmpty(fromPath) && !string.IsNullOrEmpty(toPath))
            {
                var transferForm = new TransferForm(fromApp, toApp, fromPath, toPath, items);
                transferForm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Transfer cannot continue without both paths.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private string FindDefaultPath(string appName)
        {
            string user = Environment.UserName;
            string path = null;

            if (appName == "Opera GX")
                path = $@"C:\Users\{user}\AppData\Roaming\Opera Software\Opera GX Stable";
            else if (appName == "Microsoft Edge")
                path = $@"C:\Users\{user}\AppData\Local\Microsoft\Edge\User Data\Default";

            return (path != null && Directory.Exists(path)) ? path : null;
        }

        private string AskUserForPath()
        {
            using (var dialog = new FolderBrowserDialog()
            {
                Description = "Please select the browser profile folder.",
                ShowNewFolderButton = false
            })
            {
                return dialog.ShowDialog() == DialogResult.OK ? dialog.SelectedPath : null;
            }
        }

        private void ScanForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}