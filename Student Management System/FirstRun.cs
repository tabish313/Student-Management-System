 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Windows.Forms;
using DevComponents.DotNetBar.Controls;
using System.Collections;
using System.Data.SQLite;
using System.IO.IsolatedStorage;
using System.IO;
using System.Reflection;


namespace Student_Management_System
{
    public partial class FirstRun : MetroForm
    {

        DbEntities db = new DbEntities();
        BackgroundWorker bw;
        ArrayList classarray;
        OpenFileDialog opd;
        bool restore;
        string sourcepath;
        string destpath = Application.StartupPath + @"/Database";

        
        bool mg = false;
        public FirstRun()
        {
            InitializeComponent();
            btnback.Enabled = false;
            btnfinish.Visible = false;
        }

        private void FirstRun_Load(object sender, EventArgs e)
        {
            EmbedFont.LoadComfortaaFont();
            EmbedFont.LoadMicrossFont();
            EmbedFont.LoadRalewayFont();
            this.CaptionFont = new Font(EmbedFont.private_fonts.Families[2], 9);
            labelwelcome.Font = new Font(EmbedFont.private_fonts.Families[0], 24);



            if (!radiorestore.Checked)
            {
                txtfilename.Visible = false;
                btnbrowse.Visible = false;
            }

            for (int i = 1; i <= 20; i++)
            {
                string name = "textBoxX" + i.ToString();
                TextBoxX txt_lbl = this.Controls.Find(name, true).FirstOrDefault() as TextBoxX;
                txt_lbl.WatermarkText = "No. " + i.ToString();
                txt_lbl.TabIndex = i - 0;
            }

            for (int i = 21; i <= 30; i++)
            {
                string name = "textBoxX" + i.ToString();
                TextBoxX txt_lbl = this.Controls.Find(name, true).FirstOrDefault() as TextBoxX;
                txt_lbl.WatermarkText = "Section. " + (i - 20).ToString();
                txt_lbl.TabIndex = i - 0;
            }

            for (int i = 31; i <= 50; i++)
            {
                string name = "text" + i.ToString();
                TextBoxX txt_lbl = this.Controls.Find(name, true).FirstOrDefault() as TextBoxX;
                txt_lbl.Enabled = false;
                txt_lbl.ForeColor = Color.Black;

            }

            for (int i = 51; i <= 70; i++)
            {
                string name = "text" + i.ToString();
                TextBoxX txt_lbl = this.Controls.Find(name, true).FirstOrDefault() as TextBoxX;
                txt_lbl.WatermarkText = "Tution Fee";

            }

            for (int i = 71; i <= 90; i++)
            {
                string name = "text" + i.ToString();
                TextBoxX txt_lbl = this.Controls.Find(name, true).FirstOrDefault() as TextBoxX;
                txt_lbl.WatermarkText = "Exam Fee";

            }
        }

        private void btnnext_Click(object sender, EventArgs e)
        {
            int tabindex = tabControl1.SelectedIndex;
            if (radionewprofile.Checked)
            {
                restore = false;
                if (tabControl1.SelectedIndex < 4)
                {
                    tabControl1.SelectedIndex = tabindex + 1;
                }
            }




            if (tabindex == 2)
            {
                for (int i = 31; i <= 50; i++)
                {
                    string name = "text" + i.ToString();
                    string pname = "textBoxX" + (i - 30).ToString();
                    TextBoxX txt_lbl = this.Controls.Find(name, true).FirstOrDefault() as TextBoxX;
                    TextBoxX txt_plbl = this.Controls.Find(pname, true).FirstOrDefault() as TextBoxX;
                    txt_lbl.Enabled = false;
                    txt_lbl.ForeColor = Color.Black;
                    txt_lbl.Text = txt_plbl.Text;
                }
            }

            if (tabindex == 4)
            {
                if (txtuser.Text == txtcuser.Text)
                {
                    if (txtpass.Text == txtcpass.Text)
                    {
                        if (MessageBox.Show("Are you sure you want to save all the information?", "Confirmation - Student Management System", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                        {
                            tabControl1.SelectedIndex = 5;
                            bw = new BackgroundWorker();
                            bw.DoWork += Bw_DoWork;
                            bw.RunWorkerCompleted += Bw_RunWorkerCompleted;

                            if (!bw.IsBusy)
                            {
                                bw.RunWorkerAsync();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Passowrd and Confirm Password does not match!", "Error - Student Management Sytem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Username and Confirm Username does not match!", "Error - Student Management Sytem", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                }
            }

            if (radiorestore.Checked)
            {
                restore = true;
                if (restore)
                {
                    if (MessageBox.Show("Are you sure you want to restore all the data?", "Confirmation - Student Management System", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                    {
                        tabControl1.SelectedIndex = 5;
                        bw = new BackgroundWorker();
                        bw.DoWork += Bw_DoWork1;
                        bw.RunWorkerCompleted += Bw_RunWorkerCompleted;

                        if (!bw.IsBusy)
                        {
                            bw.RunWorkerAsync();
                            labelX8.Text = "Restoring Data";

                        }
                    }
                }
            }


        }

        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pictureBoxloading.Image = Student_Management_System.Properties.Resources.done;
            
            labelX8.Visible = false;
            if (mg)
            {
                labelX9.Text = "Successfully Restored.";
            }
            else
            {
                labelX9.Text = "Successfully Saved.";
            }
            btnfinish.Visible = true;
            checkboxlaunch.Visible = true;
            labelappname.Visible = true;
        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            SavingInformation();
        }

        private void Bw_DoWork1(object sender, DoWorkEventArgs e)
        {
            Restore();
            mg = true;
        }


        private void SavingInformation()
        {
            try
            {
                Random ran1 = new Random()
                {
                    ID = 1,
                    Key = "School Name",
                    Text = txtschoolname.Text
                };

                Random ran2 = new Random()
                {
                    ID = 2,
                    Key = "Address",
                    Text = txtschooladdress.Text
                };

                Random ran3 = new Random()
                {
                    ID = 3,
                    Key = "Contact",
                    Text = txtschoolcontact.Text
                };

                Random ran4 = new Random()
                {
                    ID = 4,
                    Key = "Email",
                    Text = txtschoolEmail.Text
                };
                Random ran5 = new Random()
                {
                    ID = 5,
                    Key = "Owner Name",
                    Text = txtowner.Text
                };

                Random ran6 = new Random()
                {
                    ID = 6,
                    Key = "Classes",
                    Text = Classess().ToString()
                };

                Random ran7 = new Random()
                {
                    ID = 7,
                    Key = "Sections",
                    Text = Sections().ToString()
                };

                Random ran8 = new Random()
                {
                    ID = 8,
                    Key = "Classes Fee",
                    Text = ClassesFee().ToString()
                };

                Random ran9 = new Random()
                {
                    ID = 9,
                    Key = "Others Fee Label",
                    Text = txtothersfeelabel.Text
                };

                Random ran10 = new Random()
                {
                    ID = 10,
                    Key = "Others Fee",
                    Text = txtothersfee.Text
                };

                Random ran11 = new Random()
                {
                    ID = 11,
                    Key = "Late Fee",
                    Text = txtlatefee.Text
                };

                Random ran12 = new Random()
                {
                    ID = 12,
                    Key = "Voucher Charges",
                    Text = txtvouchcharges.Text
                };

                Random ran13 = new Random()
                {
                    ID = 13,
                    Key = "Due Date",
                    Text = txtduedate.Text
                };

                Random ran14 = new Random()
                {
                    ID = 14,
                    Key = "Total Fee Per Month",
                    Text = "0,0,0,0,0,0,0,0,0,0,0,0"
                };

                Random ran15 = new Random()
                {
                    ID = 15,
                    Key = "Every Submission",
                    Text = "0;0;0;0;0;0;0;0;0;0;0;0"
                };

                Random ran16 = new Random()
                {
                    ID = 16,
                    Key = "Submission Date",
                    Text = "0;0;0;0;0;0;0;0;0;0;0;0"
                };

                Random ran17 = new Random()
                {
                    ID = 17,
                    Key = "Submission Name",
                    Text = "0;0;0;0;0;0;0;0;0;0;0;0"
                };


                var guid = Guid.NewGuid();



                Random ran18 = new Random()
                {
                    ID = 18,
                    Key = "Unique ID",
                    Text = guid.ToString().Substring(0, 18).ToString()
                };


                UserLogin user = new UserLogin()
                {
                    UserName = txtuser.Text,
                    Password = txtpass.Text
                };


                db.Randoms.Add(ran1);
                db.Randoms.Add(ran2);
                db.Randoms.Add(ran3);
                db.Randoms.Add(ran4);
                db.Randoms.Add(ran5);
                db.Randoms.Add(ran6);
                db.Randoms.Add(ran7);
                db.Randoms.Add(ran8);
                db.Randoms.Add(ran9);
                db.Randoms.Add(ran10);
                db.Randoms.Add(ran11);
                db.Randoms.Add(ran12);
                db.Randoms.Add(ran13);
                db.Randoms.Add(ran14);
                db.Randoms.Add(ran15);
                db.Randoms.Add(ran16);
                db.Randoms.Add(ran17);
                db.Randoms.Add(ran18);

                db.UserLogins.Add(user);

                db.SaveChanges();

                string fileName = Application.StartupPath + @"\bin\License.lic";


                // Check if file already exists. If yes, delete it.     
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                // Create a new file     
                using (StreamWriter sw = File.CreateText(fileName))
                {

                    sw.WriteLine("Owner Name:" + txtowner.Text);
                    sw.WriteLine("School Name:" + txtschoolname.Text);
                    sw.WriteLine("School Address:" + txtschooladdress.Text);
                    sw.WriteLine("School Contact:" + txtschoolcontact.Text);
                    sw.WriteLine("School Email:" + txtschoolEmail.Text);
                    sw.WriteLine("Installation Date:" + DateTime.Now.ToString("dd/MM/yyyy"));
                    sw.WriteLine("IsActivated:" + ClsTripleDES.Encrypt("False"));
                    string ency = DateTime.Now.ToString("dd/MM/yyyy") + "," + DateTime.Now.AddDays(5).ToString("dd/MM/yyyy") + "," + guid.ToString().Substring(0, 18).ToString() + "," + txtowner.Text + "," + txtschoolname.Text + "," + "True";
                    sw.WriteLine("Licensed Hash:" + ClsTripleDES.Encrypt(ency));
                }

                string[] lines = File.ReadAllLines(Application.StartupPath + @"\bin\User.Config");
                lines[0] = "Owner Name:" + txtowner.Text;
                lines[1] = "School Name:" + txtschoolname.Text;
                lines[2] = "FirstRun:" + "False";
                lines[3] = "DefaultPrinter:null";

                File.WriteAllLines(Application.StartupPath + @"\bin\User.Config", lines);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public string Classess()
        {
            classarray = new ArrayList();

            for (int i = 1; i <= 20; i++)
            {
                string name = "textBoxX" + i.ToString();
                TextBoxX txt_lbl = this.Controls.Find(name, true).FirstOrDefault() as TextBoxX;

                if (txt_lbl.Text != "")
                {
                    classarray.Add(txt_lbl.Text);
                }
            }

            string clas = String.Join(",", classarray.ToArray());

            return clas;
        }

        public string Sections()
        {
            ArrayList secarray = new ArrayList();

            for (int i = 21; i <= 30; i++)
            {
                string name = "textBoxX" + i.ToString();
                TextBoxX txt_lbl = this.Controls.Find(name, true).FirstOrDefault() as TextBoxX;

                if (txt_lbl.Text != "")
                {
                    secarray.Add(txt_lbl.Text);
                }
            }

            string sec = String.Join(",", secarray.ToArray());

            return sec;
        }

        public string ClassesFee()
        {
            ArrayList feearray = new ArrayList();
            string tutionfee, examfee, feefor;
            for (int i = 1; i <= classarray.ToArray().Length; i++)
            {
                string tfee = "text" + (i + 50).ToString();
                TextBoxX txt_tfee = this.Controls.Find(tfee, true).FirstOrDefault() as TextBoxX;

                string efee = "text" + (i + 70).ToString();
                TextBoxX txt_efee = this.Controls.Find(efee, true).FirstOrDefault() as TextBoxX;

                if (txt_tfee.Text != "")
                {
                    tutionfee = txt_tfee.Text;
                }
                else
                {
                    tutionfee = "0";
                }

                if (txt_efee.Text != "")
                {
                    examfee = txt_efee.Text;
                }
                else
                {
                    examfee = "0";
                }
                feefor = tutionfee + "," + examfee;
                feearray.Add(feefor);
            }

            return String.Join(";", feearray.ToArray());
        }


        private void btnback_Click(object sender, EventArgs e)
        {
            int tabindex = tabControl1.SelectedIndex;
            if (!restore)
            {
                if (tabControl1.SelectedIndex > 0)
                {
                    tabControl1.SelectedIndex = tabindex - 1;
                }
            }
            if (restore)
            {
                tabControl1.SelectedIndex = 0;
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex > 0)
            {
                btnback.Enabled = true;
            }
            else
            {
                btnback.Enabled = false;
            }

            if (tabControl1.SelectedIndex == 5)
            {
                btnnext.Enabled = false;
                btnback.Enabled = false;
                btncancel.Enabled = false;
            }
            else
            {
                btnnext.Enabled = true;
                btnback.Enabled = true;
                btncancel.Enabled = true;
            }
        }

        private void radiorestore_CheckChanged(object sender, EventArgs e)
        {
            if (radiorestore.Checked)
            {
                txtfilename.Visible = true;
                btnbrowse.Visible = true;
            }
            else
            {
                txtfilename.Visible = false;
                btnbrowse.Visible = false;
            }
        }

        private void btnfinish_Click(object sender, EventArgs e)
        {
            if (checkboxlaunch.Checked)
            {
                this.Hide();
                Splash splash = new Splash();
                splash.Show();
            }
            else
            {
                Application.Exit();
            }
        }

        private void labelappname_Click(object sender, EventArgs e)
        {
            if (checkboxlaunch.Checked)
            {
                checkboxlaunch.CheckState = CheckState.Unchecked;
            }
            else
            {
                checkboxlaunch.CheckState = CheckState.Checked;
            }
        }

        private void btnbrowse_Click(object sender, EventArgs e)
        {
            

            opd = new OpenFileDialog();
            opd.Title = "Browse Student Management System Backup File";
            opd.Filter = "sms backup files (*.backupsms)|*.backupsms";


            if (opd.ShowDialog() == DialogResult.OK)
            {
                txtfilename.Text = opd.FileName;
            }
        }

        private void Restore()
        {
            System.Threading.Thread.Sleep(5000);


            DbEntities newdb = new DbEntities();
            DbEntityRefresh.Refresh(newdb);
            try
            {
                sourcepath = txtfilename.Text;
                File.Move(sourcepath, Path.ChangeExtension(sourcepath, ".db"));

                if (File.Exists(destpath + "/Database.db"))
                {
                    File.Delete(destpath + "/Database.db");
                }

                File.Copy(Path.GetDirectoryName(sourcepath) + "/Database.db", destpath + "/Database.db");

                File.Move(Path.GetDirectoryName(sourcepath) + "/Database.db", Path.ChangeExtension(Path.GetDirectoryName(sourcepath) + "/Database.db", ".backupsms"));

                var schoolname = newdb.Randoms.Where(c => c.ID == 1).FirstOrDefault();
                var schooladdress = newdb.Randoms.Where(c => c.ID == 2).FirstOrDefault();
                var schoolcontact = newdb.Randoms.Where(c => c.ID == 3).FirstOrDefault();
                var schoolemail = newdb.Randoms.Where(c => c.ID == 4).FirstOrDefault();
                var ownername = newdb.Randoms.Where(c => c.ID == 5).FirstOrDefault();
                var softid = newdb.Randoms.Where(c => c.ID == 18).FirstOrDefault();

                    string fileName = Application.StartupPath + @"\bin\License.lic";


                    // Check if file already exists. If yes, delete it.     
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }

                    // Create a new file     
                    using (StreamWriter sw = File.CreateText(fileName))
                    {

                        sw.WriteLine("Owner Name:" + ownername.Text);
                        sw.WriteLine("School Name:" + schoolname.Text);
                        sw.WriteLine("School Address:" + schooladdress.Text);
                        sw.WriteLine("School Contact:" + schoolcontact.Text);
                        sw.WriteLine("School Email:" + schoolemail.Text);
                        sw.WriteLine("Installation Date:" + DateTime.Now.ToString("dd/MM/yyyy"));
                        sw.WriteLine("IsActivated:" + ClsTripleDES.Encrypt("False"));
                        string ency = DateTime.Now.ToString("dd/MM/yyyy") + "," + DateTime.Now.AddDays(5).ToString("dd/MM/yyyy") + "," + softid.Text + "," + ownername.Text + "," + schoolname.Text + "," + "True";
                        sw.WriteLine("Licensed Hash:" + ClsTripleDES.Encrypt(ency));
                    }

                    string[] lines = File.ReadAllLines(Application.StartupPath + @"\bin\User.Config");
                    lines[0] = "Owner Name:" + txtowner.Text;
                    lines[1] = "School Name:" + txtschoolname.Text;
                    lines[2] = "FirstRun:" + "False";
                    lines[3] = "DefaultPrinter:null";

                    File.WriteAllLines(Application.StartupPath + @"\bin\User.Config", lines);
                

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error - Student Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                newdb.Dispose();
            }
        }
    }
}