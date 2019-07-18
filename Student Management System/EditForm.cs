﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Windows.Forms;

namespace Student_Management_System
{
    public partial class EditForm : MetroForm
    {
        public static bool RESET;
        DbEntities db = new DbEntities();
        byte[] bimage;
        public EditForm()
        {
            InitializeComponent();
            this.ShowIcon = true;
            this.Icon = global::Student_Management_System.Properties.Resources.icon;
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            EmbedFont.LoadComfortaaFont();
            EmbedFont.LoadMicrossFont();
            EmbedFont.LoadRalewayFont();
            this.CaptionFont = new Font(EmbedFont.private_fonts.Families[2], 11);
            labelcopyright.Text = "© Copyright - Student Management Sytem | Powered by TabiSoft Solutions";
            labelcopyright.Font = new Font(EmbedFont.private_fonts.Families[0], 8);
            var i = db.StudentDatas.Where(c => c.ID == ModifyStudent.STUDENT_ID).FirstOrDefault();
            var j = db.StudentProfiles.Where(c => c.ID == ModifyStudent.STUDENT_ID).FirstOrDefault();
            this.Text = i.StudentName;

            var cls = db.Randoms.Where(c => c.ID == 6).FirstOrDefault();
            var clasarray = cls.Text.Split(',');

            var sec = db.Randoms.Where(c => c.ID == 7).FirstOrDefault();
            var secarray = sec.Text.Split(',');


            string[] gender = { "Male", "Female" };
            ComboBoxGender.DataSource = gender;
            comboboxclass.DataSource = clasarray;
            comboboxsection.DataSource = secarray;

            txtstdname.Text = i.StudentName;
            txtathername.Text = i.FatherName;
            txtregno.Text = i.RegNo;
            txtmothername.Text = i.MotherName;
            DateofBirthPicker.Value = Convert.ToDateTime(i.DateOfBirth);
            DateofBirthPicker.Text = i.DateOfBirth;
            txtplaceofbirth.Text = i.PlaceOfBirth;
            txtaddress.Text = i.Address;
            txtcnic.Text = i.NIC;
            ComboBoxGender.Text = i.Gender;
            txtreligion.Text = i.Religion;
            txtcontact.Text = i.Contact;
            comboboxclass.Text = i.Class;

            CultureInfo enUS = new CultureInfo("en-US");
            DateTime admitdate;
            bool check = DateTime.TryParseExact(i.AdmitDate, "dd/MM/yyyy", enUS, DateTimeStyles.None, out admitdate);


            Admitdatepicker.Value = admitdate;
            Admitdatepicker.Text = i.AdmitDate;
            comboboxsection.Text = i.Section;
            
            pictureBoxProfile.Image = ByteToImage(ModifyStudent.STUDENT_ID);
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnupload_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpeg*.png;*.jpg)|*.jpeg;*.png;*.jpg";
            if (open.ShowDialog() == DialogResult.OK)
            {
                string image = open.FileName;
                Bitmap bmp = new Bitmap(image);
                FileStream fs = new FileStream(image, FileMode.Open, FileAccess.Read);
                bimage = new byte[fs.Length];
                fs.Read(bimage, 0, Convert.ToInt32(fs.Length));
                fs.Close();

                pictureBoxProfile.Image = new Bitmap(bmp);
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (txtstdname.Text != null && txtstdname.Text != "" && txtathername.Text != null && txtathername.Text != "" && txtregno.Text != null && txtregno.Text != "" &&
                txtmothername.Text != null && txtmothername.Text != "" && DateofBirthPicker.Text != null && DateofBirthPicker.Text != "" && txtaddress.Text != null && txtaddress.Text != "" &&
                txtplaceofbirth.Text != null && txtplaceofbirth.Text != "" && txtcnic.Text != null && txtcnic.Text != "" &&
                ComboBoxGender.Text != null && ComboBoxGender.Text != "" && txtreligion.Text != null && txtreligion.Text != "" &&
                txtcontact.Text != null && txtcontact.Text != "" && comboboxclass.Text != null && comboboxclass.Text != "" &&
                comboboxsection.Text != null && comboboxsection.Text != "" && Admitdatepicker.Text != null && Admitdatepicker.Text != "")
            {
                try
                {
                    var data = db.Randoms.Where(x => x.ID == 6).FirstOrDefault();
                    ArrayList arrayList = new ArrayList();
                    arrayList.AddRange(data.Text.Split(','));

                    bool classcheck = false;

                    for (int i = 0; i <= arrayList.ToArray().Length - 1; i++)
                    {
                        if (comboboxclass.Text == arrayList[i].ToString())
                        {
                            classcheck = true;
                        }
                    }


                    if (classcheck)
                    {
                        int index = 0;
                        for (int x = 0; x <= arrayList.ToArray().Length - 1; x++)
                        {
                            if (comboboxclass.Text == arrayList[x].ToString())
                            {
                                index = x;
                                break;
                            }
                        }
                        var othersfee = db.Randoms.Where(x => x.ID == 10).FirstOrDefault();
                        var latefee = db.Randoms.Where(x => x.ID == 11).FirstOrDefault();
                        var fee = db.Randoms.Where(x => x.ID == 8).FirstOrDefault();
                        var feearray = fee.Text.Split(';');

                        var feearray1 = feearray[index].ToString().Split(',');
                        string fees = feearray1[0].ToString() + "," + feearray1[0].ToString() + "," + feearray1[0].ToString() + "," + feearray1[0].ToString() + "," + feearray1[0].ToString() + "," + feearray1[0].ToString() + "," + feearray1[0].ToString() + "," + feearray1[0].ToString() + "," + feearray1[0].ToString() + "," + feearray1[0].ToString() + "," + feearray1[0].ToString() + "," + feearray1[0].ToString();


                        DateTime dob = Convert.ToDateTime(DateofBirthPicker.Value);
                        DateTime admit = Convert.ToDateTime(Admitdatepicker.Value);

                        var i = db.StudentDatas.Where(c => c.ID == ModifyStudent.STUDENT_ID).FirstOrDefault();
                        var j = db.StudentProfiles.Where(d => d.ID == ModifyStudent.STUDENT_ID).FirstOrDefault();
                        var k = db.StudentFees.Where(c => c.ID == ModifyStudent.STUDENT_ID).FirstOrDefault();

                        i.StudentName = txtstdname.Text;
                        i.FatherName = txtathername.Text;
                        i.RegNo = txtregno.Text;
                        i.MotherName = txtmothername.Text;
                        i.DateOfBirth = dob.ToString("dd/MM/yyyy");
                        i.PlaceOfBirth = txtplaceofbirth.Text;
                        i.Address = txtaddress.Text;
                        i.NIC = txtcnic.Text;
                        i.Gender = ComboBoxGender.Text;
                        i.Religion = txtreligion.Text;
                        i.Contact = txtcontact.Text;
                        i.Class = comboboxclass.Text;
                        i.Section = comboboxsection.Text;
                        i.AdmitDate = admit.ToString("dd/MM/yyyy");
                        i.Fees = fees;

                        if (bimage != null)
                        {
                            j.Profile = bimage;
                        }

                        k.ExamFee = Convert.ToInt32(feearray1[1].ToString());

                        db.Entry(i).State = System.Data.Entity.EntityState.Modified;
                        db.Entry(j).State = System.Data.Entity.EntityState.Modified;
                        db.Entry(k).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {
                    MessageBoxAdv.Show(ex.Message.ToString(), "Something Went wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    db.Dispose();
                    MessageBox.Show("You have successfully updated a new record named as: " + txtstdname.Text, "New record added - Student Management System", MessageBoxButtons.OK, MessageBoxIcon.None);
                    RESET = true;
                        this.Hide();
                }
            }
            else
            {
                MessageBox.Show("All fields are mandatory!", "Error - Student Management Sytem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
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
    }
}
