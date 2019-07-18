using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;
using Syncfusion.Windows.Forms;
namespace Student_Management_System
{
    public partial class Activation : MetroForm
    {
        DbEntities db = new DbEntities();
        BackgroundWorker bw;
        BackgroundWorker bwreq;
        BackgroundWorker bwreg;
        string result;
        string reqresult;
        bool req = false;
        bool validate = false;
        public Activation()
        {
            InitializeComponent();
            pictureBoxreq.Visible = false;
            labelrequest.Visible = false;
        }

        private void Activation_Load(object sender, EventArgs e)
        {
            EmbedFont.LoadComfortaaFont();
            EmbedFont.LoadMicrossFont();
            EmbedFont.LoadRalewayFont();

            labelbtm.Font = new Font(EmbedFont.private_fonts.Families[0], 8);
            this.CaptionFont = new Font(EmbedFont.private_fonts.Families[2], 9);

            btnlaunch.Text = "Launch in " + TrailForm.tick.ToString() + "s";
            if (FormMain.GETID == 0 || Splash.GETID == 0)
            {
                btnlaunch.Visible = false;
            }
            else
            {
                btnlaunch.Visible = true;
            }
            bw = new BackgroundWorker();
            bw.DoWork += Bw_DoWork;
            bw.RunWorkerCompleted += Bw_RunWorkerCompleted;

        }

        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            txtpublickey.Text = result;
            labelfetching.Visible = false;
            pictureBoxfetching.Visible = false;

        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {

            result = SoftwareID();

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        Timer tm = new Timer();

        private void Activation_Shown(object sender, EventArgs e)
        {
            btnlaunch.Enabled = false;
            
            tm.Enabled = true;
            tm.Interval = 1000;
            tm.Tick += Tm_Tick;

            if (!bw.IsBusy)
            {
                bw.RunWorkerAsync();
            }

            if (TrailForm.TIMER_RUN)
            {
                if (TrailForm.tick > 0)
                {
                    tm.Start();
                    
                }
            }

        }

        private void Tm_Tick(object sender, EventArgs e)
        {
            btnlaunch.Text = "Launch in " + TrailForm.tick.ToString() + "s";
            if (TrailForm.tick == 0)
            {
                tm.Stop();
                btnlaunch.Enabled = true;
                btnlaunch.Text = "Launch";

            }
        }

        private void labelvalidate_Click(object sender, EventArgs e)
        {

        }

        private void btnrequest_Click(object sender, EventArgs e)
        {
            pictureBoxreq.Visible = true;
            labelrequest.Visible = true;

            bwreq = new BackgroundWorker();
            bwreq.DoWork += Bwreq_DoWork;
            bwreq.RunWorkerCompleted += Bw_RunWorkerCompleted1;


            if (IsConnectedToNetwork)
            {


                if (!bwreq.IsBusy)
                {
                    bwreq.RunWorkerAsync();

                    btnrequest.Enabled = false;
                }

            }
            else
            {
                req = false;
                MessageBox.Show("No Internet Connection", "Error - Student Management Sytem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pictureBoxreq.Visible = false;
                labelrequest.Visible = false;
            }
        }

        private void Bw_RunWorkerCompleted1(object sender, RunWorkerCompletedEventArgs e)
        {
            btnrequest.Enabled = true;
            if (req)
            {
                pictureBoxreq.Visible = false;
                labelrequest.Visible = false;
                MessageBox.Show("Your request is successfully sent to our team", "Done - Student Management Sytem", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                pictureBoxreq.Visible = false;
                labelrequest.Visible = false;
            }
        }

        private void Bwreq_DoWork(object sender, DoWorkEventArgs e)
        {
            LicenseEntity licdb = new LicenseEntity();

            try
            {

                DbEntityRefresh.Refresh(db);

                if (RequestExists())
                {
                    req = false;
                    MessageBox.Show("Please be patience! Your request is under proces!", "Error - Student Management Sytem", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {

                    SoftwareIDCheck(txtpublickey.Text);
                    var schoolname = db.Randoms.Where(c => c.ID == 1).FirstOrDefault();
                    var Address = db.Randoms.Where(c => c.ID == 2).FirstOrDefault();
                    var Contact = db.Randoms.Where(c => c.ID == 3).FirstOrDefault();
                    var Email = db.Randoms.Where(c => c.ID == 4).FirstOrDefault();
                    var OwnerName = db.Randoms.Where(c => c.ID == 5).FirstOrDefault();
                    var softid = db.Randoms.Where(c => c.ID == 18).FirstOrDefault();
                    License lic = new License()
                    {
                        SoftwareID = softid.Text.ToString().Substring(0, 18),
                        SchoolName = schoolname.Text,
                        SchoolAddress = Address.Text,
                        SchoolContact = Contact.Text,
                        SchoolEmail = Email.Text,
                        OwnerName = OwnerName.Text,
                        IstallationDate = InstallationDate().ToString(),
                        InProcess = true,
                        IsPayment = false,
                        LicenseKey = LicenseKey(),
                        LicenseStarts = LicenseStarts(),
                        LicenseEnds = LicenseEnds()
                    };

                    licdb.Licenses.Add(lic);
                    licdb.SaveChanges();
                    req = true;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Error - Student Management Sytem", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            finally
            {
                licdb.Dispose();
            }
        }



        private string InstallationDate()
        {

            var path = Application.StartupPath + @"\bin\";

            string filename = "License.lic";

            string[] lines = File.ReadAllLines(path + filename);

            var arr = lines[5].Split(':');

            return arr[1].ToString();

        }


        private string LicenseKey()
        {
            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
            string word = "";
            // Make a random number generator.
            System.Random rand = new System.Random();

            for (int j = 1; j <= 24; j++)
            {

                int letter_num = rand.Next(0, letters.Length - 1);

                if (j == 5 || j == 10 || j == 15 || j == 20)
                {
                    word += "-";
                }
                else
                {
                    // Append the letter.
                    word += letters[letter_num];
                }
            }
            return word;
        }

        public static bool IsConnectedToNetwork
        {
            get
            {
                Network network = new Network();
                return network.IsAvailable;
            }
        }

        private bool RequestExists()
        {
            LicenseEntity licdb = new LicenseEntity();
            try
            {

                var uid = licdb.Licenses.Where(c => c.SoftwareID == txtpublickey.Text).FirstOrDefault();
                if (uid != null)
                {
                    bool InProcess = uid.InProcess;

                    if (InProcess)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                { return false; }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured: " + ex.Message.ToString(), "Error - Student Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                licdb.Dispose();

            }
        }

        private string SoftwareID()
        {

            var path = Application.StartupPath + @"\bin\";

            string filename = "License.lic";

            string[] lines = File.ReadAllLines(path + filename);

            var arr = lines[7].Split(':');

            string dec = ClsTripleDES.Decrypt(arr[1].ToString());

            var licarr = dec.Split(',');

            string LicGuid = licarr[2].ToString();


            return LicGuid.Substring(0, 18);

        }


        private string LicenseStarts()
        {


            var path = Application.StartupPath + @"\bin\";

            string filename = "License.lic";

            string[] lines = File.ReadAllLines(path + filename);

            var arr = lines[7].Split(':');

            string dec = ClsTripleDES.Decrypt(arr[1].ToString());

            var licarr = dec.Split(',');


            return licarr[0].ToString();

        }


        private string LicenseEnds()
        {

            var path = Application.StartupPath + @"\bin\";

            string filename = "License.lic";

            string[] lines = File.ReadAllLines(path + filename);

            var arr = lines[7].Split(':');

            string dec = ClsTripleDES.Decrypt(arr[1].ToString());

            var licarr = dec.Split(',');


            return licarr[1].ToString();

        }


        private void btnregister_Click(object sender, EventArgs e)
        {
            pictureBoxvalidate.Visible = true;
            labelvalidate.Visible = true;
            labelvalidate.ForeColor = Color.Green;
            labelvalidate.Text = "Checking license key.. This may take a few minutes.";

            bwreg = new BackgroundWorker();
            bwreg.DoWork += Bwreg_DoWork;
            bwreg.RunWorkerCompleted += Bwreg_RunWorkerCompleted;



            if (IsConnectedToNetwork)
            {
                if (!bwreg.IsBusy)
                {
                    bwreg.RunWorkerAsync();
                    btnregister.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("No Internet Connection", "Error - Student Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);

                labelvalidate.Visible = false;
                pictureBoxvalidate.Visible = false;
            }
        }

        private void Bwreg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnregister.Enabled = true;
            if (!validate)
            {
                labelvalidate.Text = "*License key not valid...";
                labelvalidate.ForeColor = Color.Red;
                labelvalidate.Visible = true;
                pictureBoxvalidate.Visible = false;
            }
            else
            {
                labelvalidate.Visible = false;
                pictureBoxvalidate.Visible = false;
            }
        }

        private void Bwreg_DoWork(object sender, DoWorkEventArgs e)
        {
            DbEntityRefresh.Refresh(db);

            LicenseEntity licdb = new LicenseEntity();

            try
            {
                var a = licdb.Licenses.Where(c => c.SoftwareID == txtpublickey.Text).FirstOrDefault();
                if (a != null)
                {
                    var uid = db.Randoms.Where(c => c.ID == 18).FirstOrDefault();
                    bool IsPayement = a.IsPayment;
                    if (IsPayement)
                    {
                        if (txtlicense.Text == a.LicenseKey)
                        {

                            var path = Application.StartupPath + @"\bin\";

                            string filename = "License.lic";

                            string[] lines = File.ReadAllLines(path + filename);

                            string ency = a.LicenseStarts + "," + a.LicenseEnds + "," + uid.Text + "," + a.OwnerName + "," + a.SchoolName + "," + "False";

                            lines[7] = "Licensed Hash:" + ClsTripleDES.Encrypt(ency);

                            File.WriteAllLines(path + "/" + filename, lines);

                            a.InProcess = false;

                            licdb.Entry(a).State = System.Data.Entity.EntityState.Modified;
                            licdb.SaveChanges();

                            validate = true;
                        }
                        MessageBox.Show("Student Management System is successfully activated until " + a.LicenseEnds, "Successfully Activated", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }

                    else
                    {
                        validate = false;
                    }
                }
                else
                {
                    MessageBox.Show("Your request doesnot exist at our License server! Please send request then register!", "Error - Student Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured: " + ex.Message.ToString(), "Error - Student Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                licdb.Dispose();

            }
        }

        private void SoftwareIDCheck(string id)
        {
            LicenseEntity licdb = new LicenseEntity();
            try
            {
                var uid = licdb.Licenses.Where(c => c.SoftwareID == id).FirstOrDefault();
                if (uid != null)
                {
                    licdb.Entry(uid).State = System.Data.Entity.EntityState.Deleted;
                    licdb.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured: " + ex.Message.ToString(), "Error - Student Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                licdb.Dispose();

            }
        }

        private void Activation_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (FormMain.GETID == 0)
            {
                this.Hide();
            }
            else
            {
                Application.Exit();
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            if (FormMain.GETID == 0)
            {
                this.Hide();
            }
            else
            {
                Application.Exit();
            }

        }

        private void btnlaunch_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormLogin frm = new FormLogin();
            frm.Show();
        }
    }
}