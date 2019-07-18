using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Design;
using System.Collections;


namespace Student_Management_System
{
    public partial class FormMain : MetroForm
    {
        DbEntities db = new DbEntities();
        System.Timers.Timer tm = new System.Timers.Timer();
        List<StudentData> li;
        List<StudentFee> fe;
        SearchStudent search;
        StudentSetting stdsetting;
        public static int GETID = 1;

        private ToolTip tip;
        public FormMain()
        {
            InitializeComponent();

            int height = SystemInformation.VirtualScreen.Height;
            int width = SystemInformation.VirtualScreen.Width;

            this.Size = new Size(width - 8, height - 44);
            this.Top = 0;
            this.Left = 4;

            tip = new ToolTip();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            tm.Interval = 1000;
            tm.Elapsed += Tm_Elapsed;
            tm.Start();

            EmbedFont.LoadComfortaaFont();
            EmbedFont.LoadMicrossFont();
            EmbedFont.LoadRalewayFont();
            this.CaptionFont = new Font(EmbedFont.private_fonts.Families[2], 9);
            btndashbaord.TextFont = new Font(EmbedFont.private_fonts.Families[0], 10);
            btnstudent.TextFont = new Font(EmbedFont.private_fonts.Families[0], 10);
            btnfees.TextFont = new Font(EmbedFont.private_fonts.Families[0], 10);
            btnabout.TextFont = new Font(EmbedFont.private_fonts.Families[0], 10);
            btnsetting.TextFont = new Font(EmbedFont.private_fonts.Families[0], 10);
            btnlogout.TextFont = new Font(EmbedFont.private_fonts.Families[0], 10);
            labelschool.Font = new Font(EmbedFont.private_fonts.Families[0], 18);
            labeltotalstd.Font = new Font(EmbedFont.private_fonts.Families[2], 12, FontStyle.Bold);
            labelstd.Font = new Font(EmbedFont.private_fonts.Families[0], 10);
            labeltotalfeede.Font = new Font(EmbedFont.private_fonts.Families[2], 12, FontStyle.Bold);
            labelfeede.Font = new Font(EmbedFont.private_fonts.Families[0], 10);
            labelfee.Font = new Font(EmbedFont.private_fonts.Families[2], 10, FontStyle.Bold);
            labelfee.Text = "RS." + TotalAmountPerMonth() + "/-";
            labelfeelbl.Font = new Font(EmbedFont.private_fonts.Families[0], 10);
            labelcalender.Font = new Font(EmbedFont.private_fonts.Families[0], 9);
            labelclock.Font = new Font(EmbedFont.private_fonts.Families[0], 9);
            labeltime.Font = new Font(EmbedFont.private_fonts.Families[0], 22);
            labelcopyright.Text = "© Copyright - Student Management Sytem | Powered by TabiSoft Solutions";
            labelcopyright.Font = new Font(EmbedFont.private_fonts.Families[0], 9);
            Calendar.Style.Header.Font = new Font(EmbedFont.private_fonts.Families[0], 11, FontStyle.Bold);
            Calendar.Style.Header.DayNamesFont = new Font(EmbedFont.private_fonts.Families[0], 8, FontStyle.Regular);
            Calendar.SelectedDate = DateTime.Now;
            labelstudents.Font = new Font(EmbedFont.private_fonts.Families[0], 18);
            labelfeemain.Font = new Font(EmbedFont.private_fonts.Families[0], 18);
            labelstdadd.Font = new Font(EmbedFont.private_fonts.Families[0], 10);
            labelstddel.Font = new Font(EmbedFont.private_fonts.Families[0], 10);
            labelstdannual.Font = new Font(EmbedFont.private_fonts.Families[0], 10);
            labelprevious.Font = new Font(EmbedFont.private_fonts.Families[0], 10);
            labelstdmod.Font = new Font(EmbedFont.private_fonts.Families[0], 10);
            labelsearchPrevious.Font = new Font(EmbedFont.private_fonts.Families[0], 10);
            labelstdsetting.Font = new Font(EmbedFont.private_fonts.Families[0], 10);
            labelstdearch.Font = new Font(EmbedFont.private_fonts.Families[0], 10);
            labeladdfee.Font = new Font(EmbedFont.private_fonts.Families[0], 10);
            labelfeeDefaulters.Font = new Font(EmbedFont.private_fonts.Families[0], 10);
            labelfeevouchers.Font = new Font(EmbedFont.private_fonts.Families[0], 10);
            labelfeesetting.Font = new Font(EmbedFont.private_fonts.Families[0], 10);
            labelsubmittedfee.Font = new Font(EmbedFont.private_fonts.Families[0], 10);
            labelstudentsub.Font = new Font(EmbedFont.private_fonts.Families[0], 10);


            li = db.StudentDatas.ToList();
            labeltotalstd.Text = li.Count.ToString();
            fe = db.StudentFees.Where(c => c.Submitted == false).ToList();
            labeltotalfeede.Text = fe.Count.ToString();

            var schoolname = db.Randoms.Where(c => c.ID == 1).FirstOrDefault();
            labelschool.Text = schoolname.Text.ToString();
            search = new SearchStudent();
            stdsetting = new StudentSetting();

            GETID = 0;
        }

        private void Tm_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                labeltime.Text = DateTime.Now.ToString("hh:mm:ss tt");
            }));
        }

        bool logout = false;
        private void btnlogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to log out?", "Log Out - Student Management Sytem", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
            {
                this.Hide();
                logout = true;

                FormLogin lgn = new FormLogin();
                lgn.ShowDialog();
            }
        }

        private void btndashbaord_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                return;
            }
            else
            {
                tabControl.SelectedIndex = 0;
            }
        }

        private void panelmenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnstudent_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 1)
            {
                return;
            }
            else
            {
                tabControl.SelectedIndex = 1;
            }
        }

        private void btnfees_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 2)
            {
                return;
            }
            else
            {
                tabControl.SelectedIndex = 2;
            }
        }

        private void btnabout_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 3)
            {
                return;
            }
            else
            {
                var schoolname = db.Randoms.Where(c => c.ID == 1).FirstOrDefault();
                var ownername = db.Randoms.Where(c => c.ID == 5).FirstOrDefault();

                label4.Text = "School Name: " + schoolname.Text.ToString();
                label6.Text = "Owner Name: " + ownername.Text.ToString();
                tabControl.SelectedIndex = 3;
            }
        }

        private void btnsetting_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 4)
            {
                return;
            }
            else
            {
                tabControl.SelectedIndex = 4;
            }
        }

        private void tabPagedash_Click(object sender, EventArgs e)
        {

        }



        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            DbEntityRefresh.Refresh(db);


            li = db.StudentDatas.ToList();
            labeltotalstd.Text = li.Count.ToString();

            fe = db.StudentFees.Where(c => c.Submitted == false).ToList();
            labeltotalfeede.Text = fe.Count.ToString();


            labelfee.Text = "RS." + TotalAmountPerMonth() + "/-";

            switch (tabControl.SelectedIndex)
            {
                case 0:
                    this.btndashbaord.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(128)))), ((int)(((byte)(159)))));
                    this.btnstudent.Normalcolor = Color.Transparent;
                    this.btnfees.Normalcolor = Color.Transparent;
                    this.btnsetting.Normalcolor = Color.Transparent;
                    this.btnabout.Normalcolor = Color.Transparent;
                    this.btnsetting.Normalcolor = Color.Transparent;
                    break;
                case 1:
                    this.btnstudent.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(128)))), ((int)(((byte)(159)))));
                    this.btndashbaord.Normalcolor = Color.Transparent;
                    this.btnfees.Normalcolor = Color.Transparent;
                    this.btnsetting.Normalcolor = Color.Transparent;
                    this.btnabout.Normalcolor = Color.Transparent;
                    this.btnsetting.Normalcolor = Color.Transparent;
                    break;
                case 2:
                    this.btnstudent.Normalcolor = Color.Transparent;
                    this.btndashbaord.Normalcolor = Color.Transparent;
                    this.btnfees.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(128)))), ((int)(((byte)(159)))));
                    this.btnsetting.Normalcolor = Color.Transparent;
                    this.btnabout.Normalcolor = Color.Transparent;
                    this.btnsetting.Normalcolor = Color.Transparent;
                    break;
                case 3:
                    this.btnstudent.Normalcolor = Color.Transparent;
                    this.btndashbaord.Normalcolor = Color.Transparent;
                    this.btnfees.Normalcolor = Color.Transparent;
                    this.btnsetting.Normalcolor = Color.Transparent;
                    this.btnabout.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(128)))), ((int)(((byte)(159)))));
                    this.btnsetting.Normalcolor = Color.Transparent;
                    break;
                case 4:
                    this.btnstudent.Normalcolor = Color.Transparent;
                    this.btndashbaord.Normalcolor = Color.Transparent;
                    this.btnfees.Normalcolor = Color.Transparent;
                    this.btnsetting.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(128)))), ((int)(((byte)(159)))));
                    this.btnabout.Normalcolor = Color.Transparent;
                    break;
            }
        }

        private void paneladdstd_Click(object sender, EventArgs e)
        {
            AddStudent ad = new AddStudent();
            ad.ShowDialog();

        }

        private void labelstdadd_Click(object sender, EventArgs e)
        {
            paneladdstd_Click(sender, e);
        }
        private void pictureBoxaddstd_Click(object sender, EventArgs e)
        {
            paneladdstd_Click(sender, e);
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void panelmodstd_Click(object sender, EventArgs e)
        {

            ModifyStudent mod = new ModifyStudent();
            mod.ShowDialog();
        }

        private Point NewChildLocation()
        {
            return new Point(this.Left + (this.Width - 232), Bottom - 52);
        }

        private void pictureBoxmodstd_Click(object sender, EventArgs e)
        {
            panelmodstd_Click(sender, e);
        }

        private void labelstdmod_Click(object sender, EventArgs e)
        {
            panelmodstd_Click(sender, e);
        }

        private void paneldelstd_Click(object sender, EventArgs e)
        {
            DeleteStudent del = new DeleteStudent();
            del.ShowDialog();
        }

        private void stdelete_Click(object sender, EventArgs e)
        {
            paneldelstd_Click(sender, e);
        }

        private void labelstddel_Click(object sender, EventArgs e)
        {
            paneldelstd_Click(sender, e);
        }

        private void FormMain_Activated(object sender, EventArgs e)
        {
            DbEntityRefresh.Refresh(db);
            LicenseInformation();
            FormMain_Shown(sender, e);

        }

        private void panelstdsearch_Click(object sender, EventArgs e)
        {

            search.ShowDialog();
        }

        private void stdsearch_click(object sender, EventArgs e)
        {
            panelstdsearch_Click(sender, e);
        }

        private void labelstdearch_Click(object sender, EventArgs e)
        {
            panelstdsearch_Click(sender, e);
        }

        private void panelfeevouchers_Click(object sender, EventArgs e)
        {
            FeeVoucher feeVoucher = new FeeVoucher();
            feeVoucher.ShowDialog();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            panelfeevouchers_Click(sender, e);
        }

        private void labelfeevouchers_Click(object sender, EventArgs e)
        {
            panelfeevouchers_Click(sender, e);
        }

        private void paneladdfee_Click(object sender, EventArgs e)
        {
            FeeSubmission submission = new FeeSubmission();
            submission.ShowDialog();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            paneladdfee_Click(sender, e);
        }

        private void labeladdfee_Click(object sender, EventArgs e)
        {
            paneladdfee_Click(sender, e);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        public string TotalAmountPerMonth()
        {
            int index = 0;

            switch (DateTime.Now.Month)
            {
                case 1:
                    index = 0;
                    break;
                case 2:
                    index = 1;
                    break;
                case 3:
                    index = 2;
                    break;
                case 4:
                    index = 3;
                    break;
                case 5:
                    index = 4;
                    break;
                case 6:
                    index = 5;
                    break;
                case 7:
                    index = 6;
                    break;
                case 8:
                    index = 7;
                    break;
                case 9:
                    index = 8;
                    break;
                case 10:
                    index = 9;
                    break;
                case 11:
                    index = 10;
                    break;
                case 12:
                    index = 11;
                    break;
            }

            var amount = db.Randoms.Where(c => c.ID == 14).FirstOrDefault();
            var amountarray = amount.Text.Split(',');
            return amountarray[index].ToString();
        }

        private void panelfeedefaulters_Click(object sender, EventArgs e)
        {
            FeeDefaulters feeDefaulters = new FeeDefaulters();
            feeDefaulters.ShowDialog();
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            panelfeedefaulters_Click(sender, e);
        }

        private void labelfeeDefaulters_Click(object sender, EventArgs e)
        {
            panelfeedefaulters_Click(sender, e);
        }

        private void panelsubmittedfee_Click(object sender, EventArgs e)
        {
            StaffSubmission stf = new StaffSubmission();
            stf.ShowDialog();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            panelsubmittedfee_Click(sender, e);
        }

        private void labelsubmittedfee_Click(object sender, EventArgs e)
        {
            panelsubmittedfee_Click(sender, e);
        }

        private void panelallstdsubmit_Click(object sender, EventArgs e)
        {
            StudentSubmission std = new StudentSubmission();
            std.ShowDialog();
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            panelallstdsubmit_Click(sender, e);
        }

        private void labelstudentsub_Click(object sender, EventArgs e)
        {
            panelallstdsubmit_Click(sender, e);
        }

        private void panelfeesetting_Click(object sender, EventArgs e)
        {
            FeeSetting feeSetting = new FeeSetting();
            feeSetting.ShowDialog();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            panelfeesetting_Click(sender, e);
        }

        private void labelfeesetting_Click(object sender, EventArgs e)
        {
            panelfeesetting_Click(sender, e);
        }

        private void panelstdsetting_Click(object sender, EventArgs e)
        {
            stdsetting.ShowDialog();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            panelstdsetting_Click(sender, e);
        }

        private void labelstdsetting_Click(object sender, EventArgs e)
        {
            panelstdsetting_Click(sender, e);
        }

        private string[] LicenseInformation()
        {


            string[] information = { "", "", "", "", "", "" };

            var path = Application.StartupPath + @"\bin\";

            string filename = "License.lic";

            string[] lines = File.ReadAllLines(path + filename);

            var arr = lines[7].Split(':');

                string dec = ClsTripleDES.Decrypt(arr[1].ToString());

                var licarr = dec.Split(',');

                information[0] = licarr[3];
                information[1] = licarr[4];
                information[2] = licarr[0];
                information[3] = licarr[1];
                information[4] = licarr[5];

            
            return information;
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            labelLicname.Text = "Licensed to: " + LicenseInformation()[0];
            labelLicSchool.Text = "School Name: " + LicenseInformation()[1];
            labelLicStarts.Text = "License Starts: " + LicenseInformation()[2];
            labelLicEnds.Text = "License Ends: " + LicenseInformation()[3];
            labelTrail.Text = "Trail Mode: " + LicenseInformation()[4];

            if (LicenseInformation()[4] == "True")
            {
                btnactivate.Visible = true;
            }

            if(LicenseDaysRemaining<=3)
            {
                btnactivate.Visible = true;
            }

            var schooladress = db.Randoms.Where(c => c.ID == 2).FirstOrDefault();
            var schoolemail = db.Randoms.Where(c => c.ID == 4).FirstOrDefault();
            var schoolcontact = db.Randoms.Where(c => c.ID == 3).FirstOrDefault();

            txtschooladdress.Text = schooladress.Text;
            txtschoolcontact.Text = schoolcontact.Text;
            txtschoolemail.Text = schoolemail.Text;

            ArrayList arrayList = new ArrayList();
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                arrayList.Add(printer);
            }

            var arr = arrayList.ToArray();

            comboboxprinters.DataSource = arr;

            string filename = Application.StartupPath + @"\bin\User.config";

            string[] lines = File.ReadAllLines(filename);

            var sp = lines[3].Split(':');

            comboboxprinters.Text = sp[1].ToString();

            txtdefaultprinter.Text = sp[1].ToString();
        }

        private void btnactivate_Click(object sender, EventArgs e)
        {

            Activation act = new Activation();
            act.ShowDialog();
        }
        private void btnprintersave_Click(object sender, EventArgs e)
        {
            string filename = Application.StartupPath + @"\bin\User.config";

            string[] lines = File.ReadAllLines(filename);

            lines[3] = "DefaultPrinter:" + comboboxprinters.Text;

            File.WriteAllLines(filename, lines);

            MessageBox.Show("Your Default printer is successfully changed. \nDefault Printer Name:" + comboboxprinters.Text, "Success - Student Management System", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            DbEntityRefresh.Refresh(db);
            FormMain_Shown(sender, e);
        }

        private void btnschoolsave_Click(object sender, EventArgs e)
        {
            try
            {
                var schooladress = db.Randoms.Where(c => c.ID == 2).FirstOrDefault();
                var schoolemail = db.Randoms.Where(c => c.ID == 4).FirstOrDefault();
                var schoolcontact = db.Randoms.Where(c => c.ID == 3).FirstOrDefault();

                schooladress.Text = txtschooladdress.Text;
                schoolcontact.Text = txtschoolcontact.Text;
                schoolemail.Text = txtschoolemail.Text;

                db.Entry(schooladress).State = System.Data.Entity.EntityState.Modified;
                db.Entry(schoolemail).State = System.Data.Entity.EntityState.Modified;
                db.Entry(schoolcontact).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                MessageBox.Show("Your School Data is sucessfully updated", "Data Updated - Student Management Sytem", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                DbEntityRefresh.Refresh(db);
                FormMain_Shown(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error Occured: " + ex.Message.ToString(), "Error - Student Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnusersave_Click(object sender, EventArgs e)
        {
            var user = db.UserLogins.Where(c => c.UserName == textBoxuser.Text).FirstOrDefault();
            if (user != null)
            {
                if (textBoxnewuser.Text != "")
                {
                    if (textBoxnewuser.Text == textBoxcuser.Text)
                    {
                        user.UserName = textBoxnewuser.Text;
                        db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        MessageBox.Show("Username successfully changed!", "Data Updated - Student Management Sytem", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        DbEntityRefresh.Refresh(db);
                        FormMain_Shown(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("New Username and Confirm Username does not match!", "Error - Student Management System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a new username!", "Error - Student Management System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("You have entered an incorrect current username!", "Error - Student Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnpasssave_Click(object sender, EventArgs e)
        {
            var pass = db.UserLogins.Where(c => c.UserName == FormLogin.USERNAME).FirstOrDefault();
            if (pass.Password == textBoxpass.Text)
            {
                if (textBoxnewpass.Text != "")
                {
                    if (textBoxnewpass.Text == textBoxcpass.Text)
                    {
                        pass.Password = textBoxnewpass.Text;
                        db.Entry(pass).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        MessageBox.Show("Password successfully changed!", "Data Updated - Student Management Sytem", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        DbEntityRefresh.Refresh(db);
                        FormMain_Shown(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("New Password and Confirm Password does not match!", "Error - Student Management System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a new password!", "Error - Student Management System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("You have entered an incorrect current password!", "Error - Student Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBoxCare_MouseHover(object sender, EventArgs e)
        {
            this.tip.SetToolTip(this.pictureBoxCare, "Customer Care \nContact: +92-332-0274751 \nEmail: mr.tabish@gmail.com");
        }

        private void pictureBoxBackup_MouseHover(object sender, EventArgs e)
        {
            this.tip.SetToolTip(this.pictureBoxBackup, "Backup Database");
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                FeeSubmission feeSubmission = new FeeSubmission();
                feeSubmission.ShowDialog();
            }

            if (e.Control && e.KeyCode == Keys.S)
            {
                SearchStudent search = new SearchStudent();
                search.ShowDialog();
            }

            if (e.Control && e.KeyCode == Keys.A)
            {
                AddStudent search = new AddStudent();
                search.ShowDialog();
            }

            if (e.Control && e.KeyCode == Keys.V)
            {
                FeeVoucher vouc = new FeeVoucher();
                vouc.ShowDialog();
            }
        }

        private void pictureBoxBackup_Click(object sender, EventArgs e)
        {
            Backup backup = new Backup();
            backup.ShowDialog();
        }

        private void panelpreviousstd_Click(object sender, EventArgs e)
        {
            PreviousStudent previous = new PreviousStudent();
            previous.ShowDialog();
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            panelpreviousstd_Click(sender, e);
        }

        private void labelprevious_Click(object sender, EventArgs e)
        {
            panelpreviousstd_Click(sender, e);
        }

        private void panelsearchprevious_Click(object sender, EventArgs e)
        {
            DbEntityRefresh.Refresh(db);
            PreviousStudentList lst = new PreviousStudentList();
            lst.ShowDialog();
        }

        private void labelsearchPrevious_Click(object sender, EventArgs e)
        {
            panelsearchprevious_Click(sender, e);
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {
            panelsearchprevious_Click(sender, e);
        }

        private void panelannualstd_Click(object sender, EventArgs e)
        {
            AnnualReport rpt = new AnnualReport();
            rpt.ShowDialog();
        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            panelannualstd_Click(sender, e);
        }

        private void labelstdannual_Click(object sender, EventArgs e)
        {
            panelannualstd_Click(sender, e);
        }

        private void btnannualsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Are you sure you want to submit Software's Annual Report?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                if (result == DialogResult.Yes)
                {
                    var a = db.Randoms.Where(c => c.ID == 14).FirstOrDefault();
                    var b = db.Randoms.Where(c => c.ID == 15).FirstOrDefault();
                    var d = db.Randoms.Where(c => c.ID == 16).FirstOrDefault();
                    var f = db.Randoms.Where(c => c.ID == 17).FirstOrDefault();

                    a.Text = "0,0,0,0,0,0,0,0,0,0,0,0";
                    b.Text = "0;0;0;0;0;0;0;0;0;0;0;0";
                    d.Text = b.Text;
                    f.Text = d.Text;

                    db.Entry(a).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(b).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(d).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(f).State = System.Data.Entity.EntityState.Modified;

                    db.SaveChanges();
                    MessageBox.Show("Software's Annual Report Submitted Successfully!", "Student Management System", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);


                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error Occured: " + ex.Message.ToString(), "Student Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            stdsearch_click(sender, e);
        }

        private void labelstdearch_Click_1(object sender, EventArgs e)
        {
            stdsearch_click(sender, e);
        }
        private int LicenseDaysRemaining
        {
            get
            {
                try
                {

                    var path = Application.StartupPath + @"\bin\";

                    string filename = "License.lic";

                    string[] lines = File.ReadAllLines(path + filename);

                    var arr = lines[7].Split(':');

                    string dec = ClsTripleDES.Decrypt(arr[1].ToString());

                    var licarr = dec.Split(',');

                    int date = DateTime.Now.Day;
                    int month = DateTime.Now.Month;
                    int year = DateTime.Now.Year;

                    CultureInfo enUS = new CultureInfo("en-US");
                    DateTime licEndDate;
                    bool check = DateTime.TryParseExact(licarr[1].ToString(), "dd/MM/yyyy", enUS, DateTimeStyles.None, out licEndDate);
                    DateTime now = new DateTime(year, month, date);
                    double day = (licEndDate - now).TotalDays;

                    int daysremaining = Convert.ToInt32(day);

                    return daysremaining;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured: " + ex.Message.ToString(), "Error - Student Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                return 0;
            }
        }
    }
}
