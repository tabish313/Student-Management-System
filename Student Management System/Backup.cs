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
using Syncfusion.Windows.Forms;

namespace Student_Management_System
{
    public partial class Backup : MetroForm
    {
        private string databasepath = Application.StartupPath + @"/Database/Database.db";
        FolderBrowserDialog folderDlg;
        string folderpath = "";
        BackgroundWorker bw;
        string backuppath = @"\Student Management System\Backup\";
        public Backup()
        {
            InitializeComponent();
            EmbedFont.LoadComfortaaFont();
            EmbedFont.LoadMicrossFont();
            EmbedFont.LoadRalewayFont();

            labelbtm.Font = new Font(EmbedFont.private_fonts.Families[0], 9);
            this.CaptionFont = new Font(EmbedFont.private_fonts.Families[2], 9);
        }

        private void Backup_Load(object sender, EventArgs e)
        {
            long length = new System.IO.FileInfo(databasepath).Length;

            if (Convert.ToInt32(ConvertBytesToKB(length)) > 1000)
            {
                textBoxsize.Text = ConvertBytesToMB(length).ToString() + " MB";
            }
            else
            {
                textBoxsize.Text = ConvertBytesToKB(length).ToString() + " KB";
            }

            var lastModified = System.IO.File.GetLastWriteTime(databasepath);

            textBoxlast.Text = lastModified.ToString("dd/MM/yyyy hh:mm tt");

        }

        public double ConvertBytesToKB(long bytes)
        {
            return (bytes / 1024f);
        }

        public double ConvertBytesToMB(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }

        private void btnbrowse_Click(object sender, EventArgs e)
        {
            folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.  
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBoxpath.Text = folderDlg.SelectedPath;
                folderpath = folderDlg.SelectedPath;
                Environment.SpecialFolder root = folderDlg.RootFolder;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            if (folderpath != "")
            {
                bw = new BackgroundWorker();
                bw.DoWork += Bw_DoWork;
                bw.RunWorkerCompleted += Bw_RunWorkerCompleted;
                btnBackup.Enabled = false;
                btnClose.Enabled = false;

                if (!bw.IsBusy)
                {
                    bw.RunWorkerAsync();
                }
            }
            else
            {
                MessageBox.Show("Please select a folder to backup!", "Error - Student Managemnent System", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnBackup.Enabled = true;
            btnClose.Enabled = true;
        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {


                if (!Directory.Exists(folderpath + backuppath))
                {
                    DirectoryInfo di = Directory.CreateDirectory(folderpath + backuppath);
                }

                if (File.Exists(folderpath + backuppath + "Database.backupsms"))
                {
                    File.Delete(folderpath + backuppath + "Database.backupsms");
                }

                if (File.Exists(folderpath + backuppath + "Database.db"))
                {
                    File.Delete(folderpath + backuppath + "Database.db");
                }

                File.Copy(databasepath, folderpath + backuppath + "Database.db");

                File.Move(folderpath + backuppath + "Database.db", Path.ChangeExtension(folderpath + backuppath + "Database.db", ".backupsms"));
                MessageBox.Show("Your database is successfully backed up! \nBackup Path: " + folderpath + backuppath + "Database.backupsms", "Successfully Backup - Student Management System", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch(Exception ex)
            {

            }
        }
    }
}
