using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Windows.Forms;
using Syncfusion.WinForms.ListView;

namespace Student_Management_System
{
    public partial class AnnualReport : MetroForm
    {
        private DbEntities db = new DbEntities();
        string currentclass, nextclass;
        int index = 0;

        string classname = "";
        string section = "";

        int tutionfee, examfee;

        public static ArrayList IDarrayList = new ArrayList();

        
        public AnnualReport()
        {
            InitializeComponent();
            this.ShowIcon = true;
            this.Icon = global::Student_Management_System.Properties.Resources.icon;
        }

        private void AnnualReport_Load(object sender, EventArgs e)
        {
            EmbedFont.LoadComfortaaFont();
            EmbedFont.LoadMicrossFont();
            EmbedFont.LoadRalewayFont();
            this.CaptionFont = new Font(EmbedFont.private_fonts.Families[2], 9);
            labelcopyright.Text = "© Copyright - Student Management Sytem | Powered by TabiSoft Solutions";
            labelcopyright.Font = new Font(EmbedFont.private_fonts.Families[0], 8);
            var cls = db.Randoms.Where(c => c.ID == 6).FirstOrDefault();
            var clasarray = cls.Text.Split(',');

            var sec = db.Randoms.Where(c => c.ID == 7).FirstOrDefault();
            var secarray = sec.Text.Split(',');

            comboboxclass.DataSource = clasarray;
            comboboxsection.DataSource = secarray;

            


        }

        private void btngenerate_Click(object sender, EventArgs e)
        {
            try
            {
                DbEntityRefresh.Refresh(db);
                var check = db.StudentDatas.Where(c => c.Class == comboboxclass.Text && c.Section == comboboxsection.Text).FirstOrDefault();
                if (check != null)
                {
                    foreach (var item in db.StudentDatas.Where(c => c.Class == comboboxclass.Text && c.Section == comboboxsection.Text))
                    {
                        IDarrayList.Add(item.ID);
                    }


                    var cls = db.Randoms.Where(c => c.ID == 6).FirstOrDefault();
                    var clasarray = cls.Text.Split(',');

                    for (int i = 0; i <= clasarray.Length - 1; i++)
                    {
                        if (comboboxclass.Text == clasarray[i])
                        {
                            index = i;
                            break;
                        }
                    }

                    currentclass = clasarray[index].ToString();
                    if (clasarray.Length > (index + 1))
                    {
                        nextclass = clasarray[index + 1].ToString();
                    }

                    var fee = db.Randoms.Where(x => x.ID == 8).FirstOrDefault();
                    var feearray = fee.Text.Split(';');

                    var feearray1 = feearray[index+1].ToString().Split(',');
                    string fees = feearray1[0].ToString() + "," + feearray1[0].ToString() + "," + feearray1[0].ToString() + "," + feearray1[0].ToString() + "," + feearray1[0].ToString() + "," + feearray1[0].ToString() + "," + feearray1[0].ToString() + "," + feearray1[0].ToString() + "," + feearray1[0].ToString() + "," + feearray1[0].ToString() + "," + feearray1[0].ToString() + "," + feearray1[0].ToString();



                    for (int i = 0; i <= IDarrayList.ToArray().Length - 1; i++)
                    {
                        int id = Convert.ToInt32(IDarrayList[i].ToString());
                        var data = db.StudentDatas.Where(c => c.ID == id).FirstOrDefault();
                        var stdfee = db.StudentFees.Where(c => c.ID == id).FirstOrDefault();

                        data.Class = nextclass;
                        data.Fees = fees;
                        stdfee.ExamFee = Convert.ToInt32(feearray1[1].ToString());

                        db.Entry(data).State = System.Data.Entity.EntityState.Modified;

                        db.Entry(stdfee).State = System.Data.Entity.EntityState.Modified;

                    }

                    db.SaveChanges();
                    MessageBox.Show("Report of class: " + comboboxclass.Text + " " + comboboxsection.Text + " submitted successfully!", "Student Management System", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("Sorry! No record found with this class!", "Student Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error Occured: " + ex.Message.ToString(), "Student Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        
        private void btnreset_Click(object sender, EventArgs e)
        {
            comboboxclass.Text = "";
            comboboxsection.Text = "";
        }

        private void AnnualReport_Activated(object sender, EventArgs e)
        {
            DbEntityRefresh.Refresh(db);
        }
    }
}
