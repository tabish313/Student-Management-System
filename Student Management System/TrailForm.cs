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
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Windows.Forms;
namespace Student_Management_System
{
    public partial class TrailForm : MetroForm
    {
        public static bool TIMER_RUN = false;
        Timer tm;
        public static int tick = 10;
        public TrailForm()
        {
            InitializeComponent();
        }

        private void TrailForm_Load(object sender, EventArgs e)
        {
            EmbedFont.LoadComfortaaFont();
            EmbedFont.LoadMicrossFont();
            EmbedFont.LoadRalewayFont();

            labelbtm.Font = new Font(EmbedFont.private_fonts.Families[0], 8);
            this.CaptionFont = new Font(EmbedFont.private_fonts.Families[2], 9);
            string label = @"You have <b><font color='#C0504D'><font size='+8'>" + TrailDaysRemaining().ToString("00") + "</font></font></b> Days Remaining.";
            labelX2.Text = label;
            btntrydemo.Enabled = false;
            tm = new Timer();
            tm.Tick += Tm_Tick;
            tm.Interval = 1000;

            TIMER_RUN = true;
        }

        private void Tm_Tick(object sender, EventArgs e)
        {
            tick--;
            btntrydemo.Text = "Try demo in " + tick + "s";
            if (tick == 0)
            {
                tm.Stop();
                TIMER_RUN = false;
                btntrydemo.Enabled = true;
                btntrydemo.Text = "Try demo";

            }
        }

        private void panelEx1_Click(object sender, EventArgs e)
        {

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

                return daysremaining;

            }
            catch (Exception)
            {

            }
            return 0;
        }

        private void btntrydemo_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormLogin formLogin = new FormLogin();
            formLogin.Show();
        }

        private void TrailForm_Shown(object sender, EventArgs e)
        {
            if(!(TrailDaysRemaining()<=0))
            {

                tm.Start();
            }
        }

        private void TrailForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnactivate_Click(object sender, EventArgs e)
        {
            this.Hide();
            Activation act = new Activation();
            act.Show();
        }
    }
}
