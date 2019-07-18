using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Windows.Forms;

namespace Student_Management_System
{
    public partial class PreviousStudent : MetroForm
    {
        public static int STUDENT_ID;
        Bitmap _profileimage_male = Student_Management_System.Properties.Resources.profile_male;
        DbEntities db = new DbEntities();
        BackgroundWorker bwsearch;
        Loading ld;
        int ID;
        string StudentName;
        string FatherName;
        string RegNo;
        bool done;
        string result;
        string name;
        bool job = false;
        public PreviousStudent()
        {
            InitializeComponent();
            this.ShowIcon = true;
            this.Icon = global::Student_Management_System.Properties.Resources.icon;
        }

        private void PreviousStudent_Load(object sender, EventArgs e)
        {
            EmbedFont.LoadComfortaaFont();
            EmbedFont.LoadMicrossFont();
            EmbedFont.LoadRalewayFont();
            this.CaptionFont = new Font(EmbedFont.private_fonts.Families[2], 9);
            labelresult.Font = new Font(EmbedFont.private_fonts.Families[2], 9);
            labelcopyright.Text = "© Copyright - Student Management Sytem | Powered by TabiSoft Solutions";
            labelcopyright.Font = new Font(EmbedFont.private_fonts.Families[0], 8);
            Admitdatepicker.Value = DateTime.Now;
        }

        private void checkBoxstruckoff_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxstruckoff.Checked)
            {
                checkBoxGraduated.Checked = false;
            }
            else
            {
                checkBoxGraduated.Checked = true;
            }
        }

        private void checkBoxGraduated_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxGraduated.Checked)
            {
                checkBoxstruckoff.Checked = false;
            }
            else
            {
                checkBoxstruckoff.Checked = true;
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            bwsearch = new BackgroundWorker();
            bwsearch.DoWork += Bwsearch_DoWork;
            bwsearch.RunWorkerCompleted += Bwsearch_RunWorkerCompleted;

            name = txtstdname.Text;
            name = Regex.Replace(name, @"(^\w)|(\s\w)", m => m.Value.ToUpper());

            Bitmap btmap = Student_Management_System.Properties.Resources.loading_f;
            ld = new Loading("Searching....", "#EE3322", btmap);
            ld.Show();
            if (!bwsearch.IsBusy)
            {
                btnsearch.Enabled = false;
                bwsearch.RunWorkerAsync();
            }

        }

        private void Bwsearch_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ld.Hide();

            btnsearch.Enabled = true;
            if (done)
            {
                labelstdid.Text = ID.ToString();
                STUDENT_ID = ID;
                labelstdreg.Text = RegNo;
                labelstdname.Text = StudentName;
                labelfathername.Text = FatherName;
                pictureBoxProfile.Image = ByteToImage(ID);
                labelresult.Visible = false;
                job = true;
            }
            else
            {
                labelresult.Text = result;
                labelresult.ForeColor = Color.Red;
                labelresult.Visible = true;
                labelstdid.Text = "Nil";
                labelstdreg.Text = "Nil";
                labelstdname.Text = "Nil";
                labelfathername.Text = "Nil";
                pictureBoxProfile.Image = _profileimage_male;
                job = false;
            }
        }

        private void Bwsearch_DoWork(object sender, DoWorkEventArgs e)
        {
            done = false;
            if ((txtId.Text != "" && txtId.Text != null) ||
                 (txtregno.Text != "" && txtregno.Text != null) ||
                 (txtstdname.Text != null && txtstdname.Text != ""))
            {

                if (txtId.Text != "" && txtId.Text != null && txtregno.Text == "" && txtstdname.Text == "")
                {
                    int a = 0;
                    bool check = int.TryParse(txtId.Text, out a);
                    if (check)
                    {
                        int id = Convert.ToInt32(txtId.Text);
                        var det = db.StudentDatas.Where(c => c.ID == id).FirstOrDefault();

                        if (det != null)
                        {
                            ID = det.ID;
                            RegNo = det.RegNo;
                            StudentName = det.StudentName;
                            FatherName = det.FatherName;
                            done = true;
                            return;
                        }
                        else
                        {
                            result = "*No record found with given data!";
                        }
                    }
                    else
                    {
                        result = "*Enter ID is not in correct format!";
                    }

                }
                if ((txtregno.Text != "" && txtregno.Text != null) &&
                    (txtId.Text == "") &&
                    (txtstdname.Text == ""))
                {
                    var det = db.StudentDatas.Where(c => c.RegNo == txtregno.Text).FirstOrDefault();

                    if (det != null)
                    {
                        ID = det.ID;
                        RegNo = det.RegNo;
                        StudentName = det.StudentName;
                        FatherName = det.FatherName;
                        done = true;
                        return;
                    }
                    else
                    {
                        result = "*No record found with given data!";
                    }
                }
                if ((txtstdname.Text != "" && txtstdname.Text != null) &&
                    (txtregno.Text == "") &&
                    (txtId.Text == ""))
                {
                    var det = db.StudentDatas.Where(c => c.StudentName == name).FirstOrDefault();

                    if (det != null)
                    {
                        ID = det.ID;
                        RegNo = det.RegNo;
                        StudentName = det.StudentName;
                        FatherName = det.FatherName;
                        done = true;
                        return;
                    }
                    else
                    {
                        result = "*No record found with given data!";
                    }
                }

                if (txtId.Text != "" && txtId.Text != null &&
                txtregno.Text != "" &&
                (txtstdname.Text == ""))
                {
                    int a = 0;
                    bool check = int.TryParse(txtId.Text, out a);
                    if (check)
                    {
                        int id = Convert.ToInt32(txtId.Text);
                        var det = db.StudentDatas.Where(c => c.ID == id && c.RegNo == txtregno.Text).FirstOrDefault();

                        if (det != null)
                        {
                            ID = det.ID;
                            RegNo = det.RegNo;
                            StudentName = det.StudentName;
                            FatherName = det.FatherName;
                            done = true;
                            return;
                        }
                        else
                        {
                            result = "*No record found with given data!";
                        }
                    }
                    else
                    {
                        result = "*Enter ID is not in correct format!";

                    }
                }

                if (txtId.Text != "" && txtId.Text != null &&
                txtstdname.Text != "" &&
                (txtregno.Text == ""))
                {
                    int a = 0;
                    bool check = int.TryParse(txtId.Text, out a);
                    if (check)
                    {
                        int id = Convert.ToInt32(txtId.Text);
                        var det = db.StudentDatas.Where(c => c.ID == id && c.StudentName == name).FirstOrDefault();

                        if (det != null)
                        {
                            ID = det.ID;
                            RegNo = det.RegNo;
                            StudentName = det.StudentName;
                            FatherName = det.FatherName;
                            done = true;
                            return;
                        }
                        else
                        {
                            result = "*No record found with given data!";
                        }
                    }
                    else
                    {
                        result = "*Enter ID is not in correct format!";

                    }
                }

                if (txtregno.Text != "" && txtregno.Text != null &&
                txtstdname.Text != "" &&
                (txtId.Text == ""))
                {
                    var det = db.StudentDatas.Where(c => c.RegNo == txtregno.Text && c.StudentName == name).FirstOrDefault();

                    if (det != null)
                    {
                        ID = det.ID;
                        RegNo = det.RegNo;
                        StudentName = det.StudentName;
                        FatherName = det.FatherName;
                        done = true;
                        return;
                    }
                    else
                    {
                        result = "*No record found with given data!";
                    }
                }

                if (txtregno.Text != "" && txtregno.Text != null &&
                txtstdname.Text != "" && txtstdname.Text != null &&
                (txtId.Text != null && txtId.Text != ""))
                {
                    int a = 0;
                    bool check = int.TryParse(txtId.Text, out a);
                    if (check)
                    {
                        int id = Convert.ToInt32(txtId.Text);
                        var det = db.StudentDatas.Where(c => c.ID == id && c.RegNo == txtregno.Text && c.StudentName == txtstdname.Text).FirstOrDefault();

                        if (det != null)
                        {
                            ID = det.ID;
                            RegNo = det.RegNo;
                            StudentName = det.StudentName;
                            FatherName = det.FatherName;
                            done = true;
                            return;
                        }
                        else
                        {
                            result = "*No record found with given data!";
                        }
                    }
                    else
                    {
                        result = "*Enter ID is not in correct format!";
                    }
                }
            }
            else
            {
                result = "*Enter any one field";
            }
        }
        public Bitmap ByteToImage(int id)
        {
            MemoryStream mStream = new MemoryStream();
            var data = db.StudentProfiles.Where(c => c.ID == id).FirstOrDefault();
            byte[] pData = data.Profile;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if(job)
            {
                string mode = "";

                if(checkBoxGraduated.Checked)
                {
                    mode = "Graduated";
                }

                if(checkBoxstruckoff.Checked)
                {
                    mode = "Struck Off";
                }

                DateTime date = Convert.ToDateTime(Admitdatepicker.Value);
                try
                {
                    var data = db.StudentDatas.Where(c => c.ID == ID).FirstOrDefault();
                    var profile = db.StudentProfiles.Where(c => c.ID == ID).FirstOrDefault();
                    var fee = db.StudentFees.Where(c => c.ID == ID).FirstOrDefault();

                    PreStudent students = new PreStudent()
                    {
                        StudentID = ID,
                        StudentName = StudentName,
                        FatherName = data.FatherName,
                        MotherName = data.MotherName,
                        RegNo = data.RegNo,
                        DateOfBirth = data.DateOfBirth,
                        PlaceOfBirth = data.PlaceOfBirth,
                        NIC = data.NIC,
                        Gender = data.Gender,
                        Religion = data.Religion,
                        Address = data.Address,
                        Contact = data.Contact,
                        Class = data.Class,
                        Section = data.Section,
                        AdmitDate = data.AdmitDate,
                        Session = data.Session,
                        Mode = mode,
                        StruckOffDate = date.ToString("dd/MM/yyyy")
                    };

                    db.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                    db.Entry(fee).State = System.Data.Entity.EntityState.Deleted;
                    db.Entry(profile).State = System.Data.Entity.EntityState.Deleted;
                    db.PreviousStudents.Add(students);
                    db.SaveChanges();

                    DbEntityRefresh.Refresh(db);

                    labelresult.Text = "Successfully Saved!";
                    labelresult.ForeColor = Color.Green;
                    labelresult.Visible = true;
                }
                catch(Exception ex)
                {
                    MessageBox.Show("An error occured: " + ex.Message.ToString(), "Error - Student Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
