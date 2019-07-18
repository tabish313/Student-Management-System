#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Syncfusion.Windows.Forms;

namespace Student_Management_System
{
    public partial class Splash : Form
    {
        private static int NUM = 0;
        public static int GETID=1;
        Timer tm;
        private BackgroundWorker bw;
        public static FormLogin formLogin;
        DbEntities db = new DbEntities();
        public Splash()
        {
            InitializeComponent();
            this.Icon = global::Student_Management_System.Properties.Resources.icon;
            this.ShowIcon = true;
        }


        private void Splash_Load(object sender, EventArgs e)
        {
            bw = new BackgroundWorker();
            bw.DoWork += Bw_DoWork;
            bw.RunWorkerCompleted += Bw_RunWorkerCompleted;

            EmbedFont.LoadComfortaaFont();
            EmbedFont.LoadMicrossFont();
            EmbedFont.LoadRalewayFont();

            labelbtm.Font = new Font(EmbedFont.private_fonts.Families[0], 7);


            tm = new Timer();
            tm.Enabled = true;
            tm.Interval = 5000;
            tm.Tick += Tm_Tick;
            tm.Start();
        }

        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Hide();
            if (LicenseValid())
            {
                if (CheckTrail())
                {
                    if (!(TrailDaysRemaining() < 0))
                    {
                        this.Hide();

                        TrailForm trailForm = new TrailForm();
                        System.Media.SystemSounds.Asterisk.Play();
                        trailForm.Show();
                    }
                }
                else
                {
                    if (!(LicenseDaysRemaining() <= 0))
                    {
                        if(LicenseDaysRemaining()<=3)
                        {
                            formLogin = new FormLogin();
                            formLogin.Show();
                            MessageBox.Show("Dear User, you have only " + LicenseDaysRemaining().ToString() + " Left. Upgrade it new Package!", "License Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                        {
                            formLogin = new FormLogin();
                            formLogin.Show();
                        }
                        
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Your License is expired! Do you want to Activate it now?", "Product Expired - Student Management System", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                        if (result == DialogResult.Yes)
                        {
                            GETID = 0;
                            Activation act = new Activation();
                            
                            act.Show();
                        }
                        else
                        {
                            Application.Exit();
                        }
                    }
                }
            }
        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            formLogin = new FormLogin();
        }

        private void Tm_Tick(object sender, EventArgs e)
        {
            tm.Stop();
            if (!bw.IsBusy)
            {
                bw.RunWorkerAsync();
            }
        }



        private bool LicenseValid()
        {
            try
            {
                var path = Application.StartupPath + @"\bin\";

                string filename = "License.lic";

                string[] lines = File.ReadAllLines(path + filename);

                var arr = lines[7].Split(':');

                string dec = ClsTripleDES.Decrypt(arr[1].ToString());

                var licarr = dec.Split(',');

                CultureInfo enUS = new CultureInfo("en-US");
                DateTime licEndDate;
                bool check = DateTime.TryParseExact(licarr[1].ToString(), "dd/MM/yyyy", enUS, DateTimeStyles.None, out licEndDate);

                var guid = db.Randoms.Where(c => c.ID == 18).FirstOrDefault();
                var owner = db.Randoms.Where(c => c.ID == 5).FirstOrDefault();
                var school = db.Randoms.Where(c => c.ID == 1).FirstOrDefault();

                string LicGuid = licarr[2].ToString();
                string LicOwner = licarr[3].ToString();
                string LicSchool = licarr[4].ToString();



                if (guid.Text.ToString() == LicGuid)
                {
                    if (LicOwner == owner.Text.ToString())
                    {
                        if (LicSchool == school.Text.ToString())
                        {
                            return true;
                        }
                    }
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Error! License Not Valid! Contact your Developer!", "Error - Student Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            return false;
        }

        private bool CheckTrail()
        {

            try
            {
                var path = Application.StartupPath + @"\bin\";

                string filename = "License.lic";

                string[] lines = File.ReadAllLines(path + filename);

                var arr = lines[7].Split(':');

                string dec = ClsTripleDES.Decrypt(arr[1].ToString());

                var licarr = dec.Split(',');

                string trail = licarr[5].ToString();

                bool check = Convert.ToBoolean(trail);
                if (check)
                {
                    return true;
                }

                if (!check)
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured: " + ex.Message.ToString(), "Error - Student Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            return false;
        }

        private int TrailDaysRemaining()
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

                return daysremaining + 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured: " + ex.Message.ToString(), "Error - Student Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            return 0;
        }

        private int LicenseDaysRemaining()
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
