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

namespace Drift
{
    public partial class Form1 : Form
    {
        private Label lblTransfer;
        private CheckedListBox clbItems;
        private Label lblFrom;
        private Label lblTo;
        private ComboBox cmbFrom;
        private ComboBox cmbTo;
        private Button btnNext;

        // Ensure there is only one InitializeComponent method.  
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lblTransfer = new System.Windows.Forms.Label();
            this.clbItems = new System.Windows.Forms.CheckedListBox();
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.cmbFrom = new System.Windows.Forms.ComboBox();
            this.cmbTo = new System.Windows.Forms.ComboBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTransfer
            // 
            this.lblTransfer.AutoSize = true;
            this.lblTransfer.Location = new System.Drawing.Point(12, 84);
            this.lblTransfer.Name = "lblTransfer";
            this.lblTransfer.Size = new System.Drawing.Size(179, 13);
            this.lblTransfer.TabIndex = 0;
            this.lblTransfer.Text = "What do you want to transfer today?";
            // 
            // clbItems
            // 
            this.clbItems.CheckOnClick = true;
            this.clbItems.Items.AddRange(new object[] {
            "Bookmarks",
            "History",
            "Cookies"});
            this.clbItems.Location = new System.Drawing.Point(15, 104);
            this.clbItems.Name = "clbItems";
            this.clbItems.Size = new System.Drawing.Size(200, 94);
            this.clbItems.TabIndex = 1;
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(12, 209);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(33, 13);
            this.lblFrom.TabIndex = 2;
            this.lblFrom.Text = "From:";
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(12, 244);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(23, 13);
            this.lblTo.TabIndex = 4;
            this.lblTo.Text = "To:";
            // 
            // cmbFrom
            // 
            this.cmbFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFrom.Location = new System.Drawing.Point(60, 206);
            this.cmbFrom.Name = "cmbFrom";
            this.cmbFrom.Size = new System.Drawing.Size(155, 21);
            this.cmbFrom.TabIndex = 3;
            // 
            // cmbTo
            // 
            this.cmbTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTo.Location = new System.Drawing.Point(60, 241);
            this.cmbTo.Name = "cmbTo";
            this.cmbTo.Size = new System.Drawing.Size(155, 21);
            this.cmbTo.TabIndex = 5;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(95, 279);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 6;
            this.btnNext.Text = "Next";
            this.btnNext.Click += new System.EventHandler(this.BtnNext_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Drift.Properties.Resources.icon;
            this.pictureBox2.Location = new System.Drawing.Point(12, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(77, 50);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Drift.Properties.Resources.text;
            this.pictureBox1.Location = new System.Drawing.Point(88, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(127, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(234, 307);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblTransfer);
            this.Controls.Add(this.clbItems);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.cmbFrom);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.cmbTo);
            this.Controls.Add(this.btnNext);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Drift";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    private void LoadSupportedApps()
        {
            var browsers = new Dictionary<string, string>
            {
                { "Opera GX", GetBrowserPath("Opera GX") },
                { "Microsoft Edge", GetBrowserPath("Microsoft Edge") },
                { "Google Chrome", GetBrowserPath("Google Chrome") },
                { "Brave", GetBrowserPath("Brave") },
                { "Vivaldi", GetBrowserPath("Vivaldi") },
                { "Firefox", GetBrowserPath("Firefox") }
            };

            foreach (var browser in browsers)
            {
                if (browser.Value != null)
                {
                    cmbFrom.Items.Add(browser.Key);
                    cmbTo.Items.Add(browser.Key);
                }
            }
        }

        private string GetBrowserPath(string browserName)
        {
            string user = Environment.UserName;
            return browserName switch
            {
                "Opera GX" => CheckPath($@"C:\Users\{user}\AppData\Roaming\Opera Software\Opera GX Stable"),
                "Microsoft Edge" => CheckPath($@"C:\Users\{user}\AppData\Local\Microsoft\Edge\User Data\Default"),
                "Google Chrome" => CheckPath($@"C:\Users\{user}\AppData\Local\Google\Chrome\User Data\Default"),
                "Brave" => CheckPath($@"C:\Users\{user}\AppData\Local\BraveSoftware\Brave-Browser\User Data\Default"),
                "Vivaldi" => CheckPath($@"C:\Users\{user}\AppData\Local\Vivaldi\User Data\Default"),
                "Firefox" => GetFirefoxProfilePath(),
                _ => null
            };
        }

        private string CheckPath(string path) => Directory.Exists(path) ? path : null;

        private string GetFirefoxProfilePath()
        {
            string profilesDir = $@"C:\Users\{Environment.UserName}\AppData\Roaming\Mozilla\Firefox\Profiles";
            if (!Directory.Exists(profilesDir)) return null;
            
            var profile = Directory.GetDirectories(profilesDir)
                .FirstOrDefault(d => d.EndsWith(".default-release"));
            return profile ?? Directory.GetDirectories(profilesDir).FirstOrDefault();
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (cmbFrom.SelectedItem == null || cmbTo.SelectedItem == null)
            {
                MessageBox.Show("Please select both 'From' and 'To' apps.", "Missing selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedItems = clbItems.CheckedItems.Cast<string>().ToList();
            var scanForm = new ScanForm(cmbFrom.SelectedItem.ToString(), cmbTo.SelectedItem.ToString(), selectedItems);
      
            scanForm.Show();
            this.Hide(); // Optional: Hide the main form
        }


        public Form1()
        {
            InitializeComponent();
            LoadSupportedApps();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void clbItems_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
            {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
