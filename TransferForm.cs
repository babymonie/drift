using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drift
{
    public partial class TransferForm : Form
    {
        private readonly string fromPath;
        private readonly string toPath;
        private readonly List<string> items;
        private ListBox lstItems;
        private Button btnTransfer;
        private readonly string fromApp;
        private readonly string toApp;
        private ProgressBar progressBar;
        private Label lblProgress;
        public TransferForm(string fromApp, string toApp, string fromPath, string toPath, List<string> items)
        {
            this.fromApp = fromApp;
            this.toApp = toApp;
            this.fromPath = fromPath;
            this.toPath = toPath;
            this.items = items;
            InitializeComponent();
            this.btnTransfer.Click += BtnTransfer_ClickAsync; // Ensure event is wired up
        }
        private static bool IsBrowserRunning(string browserName)
        {
            switch (browserName)
            {
                case "Opera GX":
                    return Process.GetProcessesByName("opera").Length > 0;
                case "Microsoft Edge":
                    return Process.GetProcessesByName("msedge").Length > 0;
                case "Google Chrome":
                    return Process.GetProcessesByName("chrome").Length > 0;
                case "Brave":
                    return Process.GetProcessesByName("brave").Length > 0;
                case "Vivaldi":
                    return Process.GetProcessesByName("vivaldi").Length > 0;
                case "Firefox":
                    return Process.GetProcessesByName("firefox").Length > 0;

                default:
                    return false;
            }
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransferForm));
            this.lstItems = new System.Windows.Forms.ListBox();
            this.btnTransfer = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstItems
            // 
            this.lstItems.FormattingEnabled = true;
            this.lstItems.Location = new System.Drawing.Point(12, 12);
            this.lstItems.Name = "lstItems";
            this.lstItems.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lstItems.Size = new System.Drawing.Size(260, 95);
            this.lstItems.TabIndex = 0;
            // 
            // btnTransfer
            // 
            this.btnTransfer.Location = new System.Drawing.Point(15, 150);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(75, 23);
            this.btnTransfer.TabIndex = 1;
            this.btnTransfer.Text = "Start Transfer";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 120);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(260, 23);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 2;
            this.progressBar.Click += new System.EventHandler(this.progressBar_Click);
            // 
            // lblProgress
            // 
            this.lblProgress.Location = new System.Drawing.Point(12, 150);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(260, 15);
            this.lblProgress.TabIndex = 3;
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TransferForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 180);
            this.Controls.Add(this.lstItems);
            this.Controls.Add(this.btnTransfer);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblProgress);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TransferForm";
            this.Text = "Transfer Data";
            this.Load += new System.EventHandler(this.TransferForm_Load_1);
            this.ResumeLayout(false);

        }

        private void TransferForm_Load_1(object sender, EventArgs e)
        {
            if (!IsRunAsAdmin())
            {
                MessageBox.Show("Please restart this app as administrator to perform operations.",
                              "Administrator Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
            else
            {
                
                ShowTransferWarning();
            }
        }
        private void ShowTransferWarning()
        {
            string message = "Ready to transfer the following items:\n" +
                             string.Join(", ", items) +
                             "\nPlease ensure browsers are closed before continuing!\n" +
                             "If browsers remain open, data corruption may occur.";
            MessageBox.Show(message, "Transfer Ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool IsRunAsAdmin()
        {
            var id = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(id);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private async void BtnTransfer_ClickAsync(object sender, EventArgs e)
        {
            btnTransfer.Enabled = false;
            try
            {
                var openBrowsers = GetRunningBrowsers();
                if (openBrowsers.Count > 0)
                {
                    var result = ShowBrowserCloseWarning(openBrowsers);
                    if (result != DialogResult.Yes) return;
                }

                if (!CloseAllBrowserProcesses())
                {
                    ShowBrowserCloseError(GetRunningBrowsers());
                    return;
                }

                var progress = new Progress<Tuple<int, string>>(ReportProgress);
                await TransferDataAsync(progress);
                MessageBox.Show("Transfer completed successfully!", "Success",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Transfer failed: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnTransfer.Enabled = true;
            }
        }
        private List<string> GetRunningBrowsers()
        {
            var running = new List<string>();
            foreach (var browser in new[] { fromApp, toApp })
            {
                if (IsBrowserRunning(browser))
                    running.Add(browser);
            }
            return running;
        }
        private DialogResult ShowBrowserCloseWarning(List<string> openBrowsers)
        {
            var message = new StringBuilder();
            message.AppendLine("The following browsers are still running:");
            message.AppendLine(string.Join(", ", openBrowsers));
            message.AppendLine("\nWe will attempt to close them automatically.");
            message.AppendLine("Continue anyway? (Data corruption risk!)");

            return MessageBox.Show(message.ToString(), "Browsers Detected",
                                 MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        private void ShowBrowserCloseError(List<string> remainingBrowsers)
        {
            var message = new StringBuilder();
            message.AppendLine("Failed to close these browsers:");
            message.AppendLine(string.Join(", ", remainingBrowsers));
            message.AppendLine("\nPlease close them manually and try again.");

            MessageBox.Show(message.ToString(), "Browser Close Failed",
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ReportProgress(Tuple<int, string> progress)
        {
            if (progress.Item1 >= 0)
            {
                progressBar.Value = Math.Min(progressBar.Maximum, progress.Item1);
            }
            lblProgress.Text = progress.Item2;
            lstItems.Items.Add(progress.Item2);
            lstItems.TopIndex = lstItems.Items.Count - 1;
        }
        private bool CloseAllBrowserProcesses()
        {
            bool success = true;
            var processes = GetBrowserProcessNames();

            foreach (var processName in processes)
            {
                try
                {
                    foreach (var process in Process.GetProcessesByName(processName))
                    {
                        if (!process.CloseMainWindow())
                        {
                            process.Kill();
                        }
                        process.WaitForExit(5000);
                    }
                }
                catch
                {
                    success = false;
                }
            }
            return success && GetRunningBrowsers().Count == 0;
        }


        private List<string> GetBrowserProcessNames()
        {
            return new List<string>
            {
                "opera", "msedge", "chrome", "brave", "firefox", "vivaldi"
            };
        }

        private async Task TransferDataAsync(IProgress<Tuple<int, string>> progress)
        {
            // Calculate total work first
            var totalFiles = CalculateTotalFiles();
            progressBar.Maximum = totalFiles;
            progressBar.Value = 0;

            await Task.Run(() =>
            {
                int filesProcessed = 0;
                
                foreach (var item in items)
                {
                    progress.Report(Tuple.Create(-1, $"Processing {item}..."));
                    switch (item)
                    {
                        case "Bookmarks":
                            TransferItemWithProgress("Bookmarks", "Bookmarks file", ref filesProcessed, progress);
                            break;
                        case "History":
                            TransferItemWithProgress("History", "History file", ref filesProcessed, progress);
                            break;
                        case "Cookies":
                            CopyNetworkFilesWithProgress(ref filesProcessed, progress);
                            break;
                        default:
                            break;
                    }
                }
            });
        }

        private void UpdateStatus(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateStatus), message);
                return;
            }
            
            lstItems.Items.Add(message);
            lstItems.TopIndex = lstItems.Items.Count - 1;
            Application.DoEvents();
        }

        private int CalculateTotalFiles()
        {
            int count = 0;
            if (items.Contains("Bookmarks"))
            {
                count += File.Exists(Path.Combine(fromPath, "Bookmarks")) ? 1 : 0;
            }
            if (items.Contains("History"))
            {
                count += File.Exists(Path.Combine(fromPath, "History")) ? 1 : 0;
            }
            if (items.Contains("Cookies"))
            {
                count += CountFilesInDirectory(Path.Combine(fromPath, "Network"));
            }
            return count;
        }

        private int CountFilesInDirectory(string path)
        {
            if (!Directory.Exists(path)) return 0;
            return Directory.GetFiles(path, "*", SearchOption.AllDirectories).Length;
        }

        private void TransferItemWithProgress(string fileName, string displayName, 
            ref int filesProcessed, IProgress<Tuple<int, string>> progress)
        {
            try
            {
                string source = Path.Combine(fromPath, fileName);
                string dest = Path.Combine(toPath, fileName);

                if (File.Exists(source))
                {
                    File.Copy(source, dest, true);
                    filesProcessed++;
                    progress.Report(Tuple.Create(filesProcessed, $"{displayName} copied"));
                }
            }
            catch (Exception ex)
            {
                progress.Report(Tuple.Create(filesProcessed, $"Error: {ex.Message}"));
            }
        }

        private void CopyNetworkFilesWithProgress(ref int filesProcessed, 
            IProgress<Tuple<int, string>> progress)
        {
            try
            {
                string sourceDir = Path.Combine(fromPath, "Network");
                string destDir = Path.Combine(toPath, "Network");
                
                if (Directory.Exists(sourceDir))
                {
                    DirectoryCopyWithProgress(sourceDir, destDir, true, ref filesProcessed, progress);
                }
            }
            catch (Exception ex)
            {
                progress.Report(Tuple.Create(filesProcessed, $"Network error: {ex.Message}"));
            }
        }

        private void DirectoryCopyWithProgress(string sourceDir, string destDir, bool copySubDirs,
            ref int filesProcessed, IProgress<Tuple<int, string>> progress)
        {
            var dir = new DirectoryInfo(sourceDir);
            if (!dir.Exists) return;

            Directory.CreateDirectory(destDir);

            foreach (FileInfo file in dir.GetFiles())
            {
                try
                {
                    string tempPath = Path.Combine(destDir, file.Name);
                    file.CopyTo(tempPath, true);
                    filesProcessed++;
                    progress.Report(Tuple.Create(filesProcessed, $"Copied {file.Name}"));
                }
                catch (Exception ex)
                {
                    progress.Report(Tuple.Create(filesProcessed, $"Failed {file.Name}: {ex.Message}"));
                }
            }

            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dir.GetDirectories())
                {
                    string tempPath = Path.Combine(destDir, subdir.Name);
                    DirectoryCopyWithProgress(subdir.FullName, tempPath, copySubDirs, 
                        ref filesProcessed, progress);
                }
            }
        }

        private void progressBar_Click(object sender, EventArgs e)
        {

        }
    }
}
