using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExaminationManagement;
using System.Security.Cryptography;

namespace ExaminationManagement
{
    
    public partial class frmRegister : Form
    {
        static string[] arrHexa = { "0", "1", "2", "3", "4", "5","6","7","8","9", "A", "B", "C", "D", "E","F" };
        AccessData acc = new AccessData();
        frmLogin frmlogin;
        public frmRegister(frmLogin frmlogin)
        {
            
            InitializeComponent();
            dgvListClass.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "choose",
                TrueValue = true,
                FalseValue = false,
                IndeterminateValue = false,
                Width = 20
            });
            loadcbTypeuser();
            loadcbgrade();
            loadcbclass();
            loadcbSub();
            LoaddgvListClass();
            cmbTypeUser.SelectedIndexChanged += (s, e) =>
            {
                lblSub.Visible = false;
                txtClass.Visible = false;
                lblname.Visible = false;
                lblGrade.Visible = false;
                lblbirth.Visible = false;

                txtName.Visible = false;
                dtmDOB.Visible = false;
                cmbSubject.Visible = false;
                cmbClass.Visible = false;
                cmbGrade.Visible = false;
                dgvListClass.Visible = false;

                if (cmbTypeUser.SelectedValue.ToString() == "HS")
                {

                    lblname.Visible = true;
                    lblGrade.Visible = true;
                    txtClass.Visible = true;
                    lblbirth.Visible = true;

                    cmbGrade.Visible = true;
                    cmbClass.Visible = true;

                    txtName.Visible = true;
                    dtmDOB.Visible = true;

                }
                else if (cmbTypeUser.SelectedValue.ToString() == "GV")
                {
                    lblSub.Visible = true;
                    txtClass.Visible = true;
                    lblname.Visible = true;

                    lblbirth.Visible = true;

                    cmbSubject.Visible = true;

                    dgvListClass.Visible = true;
                    txtName.Visible = true;
                    dtmDOB.Visible = true;
                }
                else if (cmbTypeUser.SelectedValue.ToString() == "AD")
                {

                }

            };
            cmbTypeUser.SelectedIndex = 2;
            btnSignUp.Click += (_, e) =>
            {
            AccessData acc = new AccessData();
            var ID = txtId.Text;
            var password = txtPass.Text;
            var sha1 = new SHA1CryptoServiceProvider();
            var bytesPw = ASCIIEncoding.ASCII.GetBytes(password);
            var bytesPwHashed = sha1.ComputeHash(bytesPw);
            var sb = new StringBuilder();
            foreach (byte b in bytesPwHashed)
            {
                sb.Append(arrHexa[b >> 4]);
                sb.Append(arrHexa[b & 15]);
            }
            using (var ttn = new QLTTNDataContext())
            {
                ttn.Users.InsertOnSubmit(new User
                {
                    id = ID,
                    typeUser_id = cmbTypeUser.SelectedValue.ToString(),
                    pass = sb.ToString()
                });
                ttn.Students.InsertOnSubmit(new Student
                {
                    id = ID,
                    name = txtName.Text,
                    dob = dtmDOB.Value,
                    grade_id = cmbGrade.SelectedValue.ToString(),
                    class_id = cmbClass.SelectedValue.ToString()
                });
                ttn.Teachers.InsertOnSubmit(new Teacher
                {
                    id = ID,
                    name = txtName.Text,
                    dob = dtmDOB.Value,
                    sub_id = cmbSubject.SelectedValue.ToString()
                });
                ttn.SubmitChanges();
            }

            MessageBox.Show("Đăng Ký Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information); // Nếu đang ký thành công => Sẽ có thông báo Thành Công và đồng thời các TextBox sẽ mất giá trị do [B]ClearTextBox()[/B].
            ClearTextBox();
            frmlogin.Show();
           this.Close();
    };
        }


        private void loadcbgrade()
        {
            using (var ttn = new QLTTNDataContext())
            {
                cmbGrade.DataSource = ttn.Grades.Select(kl => new { kl.id }).ToList();
                cmbGrade.ValueMember = "id";
                cmbGrade.DisplayMember = "id";
            }
        }

        private void loadcbclass()
        {
            using (var ttn = new QLTTNDataContext())
            {
                cmbClass.DataSource = ttn.Classes.Where(lh => lh.grade_id == cmbGrade.SelectedValue.ToString()).Select(lh => new { lh.id }).ToList();
                cmbClass.ValueMember = "id";
                cmbClass.DisplayMember = "id";
            }
        }

        private void LoaddgvListClass()
        {
            using (var ttn = new QLTTNDataContext())
            {
                dgvListClass.DataSource = ttn.Classes.Select(lh => new { lh.grade_id, lh.id }).ToList();
                dgvListClass.Columns["grade_id"].Width = 60;
                dgvListClass.Columns["id"].Width = 60;

            }
        }

        private void loadcbSub()
        {
            using (var ttn = new QLTTNDataContext())
            {
                cmbSubject.DataSource = ttn.Subjects.Select(mh => new { mh.id, mh.name }).ToList();
                cmbSubject.ValueMember = "id";
                cmbSubject.DisplayMember = "name";
            }
        }


        private void loadcbTypeuser()
        {
            using (var ttn = new QLTTNDataContext())
            {
                cmbTypeUser.DataSource = ttn.TypeOfUsers.Select(lnd => new { lnd.id, lnd.name }).ToList();
                cmbTypeUser.ValueMember = "id";
                cmbTypeUser.DisplayMember = "name";
            }
        }
        
        private void ClearTextBox()
        {
            txtId.Clear();
            txtPass.Clear();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
