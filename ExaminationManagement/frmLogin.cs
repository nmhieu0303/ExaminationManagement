using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;


namespace ExaminationManagement
{
    public partial class frmLogin : Form
    {
        static Form frm = null;
        static User user = null;
        static string[] arrHexa = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };
        public frmLogin()
        {

            InitializeComponent();
            txtId.Validating += TxtId_Validating;
            txtPass.Validating += TxtId_Validating;
            btnSignUp.Click += BtnSignUp_Click;
            btnLogin.Click += (_, e) =>
            {
                var username = txtId.Text;
                var password = txtPass.Text;

                using (var ttn = new QLTTNDataContext())
                {
                    user = ttn.Users.Where(us => us.id == username ).FirstOrDefault();
                    if (user != null)
                    {
                        var sha1 = new SHA1CryptoServiceProvider();
                        var bytesPw = ASCIIEncoding.ASCII.GetBytes(password);
                        var bytesPwHashed = sha1.ComputeHash(bytesPw);
                        var sb = new StringBuilder();
                        foreach (byte b in bytesPwHashed)
                        {
                            sb.Append(arrHexa[b >> 4]);
                            sb.Append(arrHexa[b & 15]);
                        }
                        frm = null;
                        if (user.typeUser_id == "HS" && sb.ToString()==user.pass)
                        {
                            //frm = new frmStudent(this, user.Student);
                        }
                        else if (user.typeUser_id == "GV" && sb.ToString() == user.pass)
                        {
                            frm = new frmTeacher(this, user.Teacher);
                        }
                        else if (user.typeUser_id == "AD")
                        {
                            //frm = new frmAdmin(this, nguoiDung);
                        }
                        if (frm != null)
                        {
                            frm.Show();
                            this.Hide();
                        }
                    }
                }
            };

        }


        private void TxtId_Validating(object sender, CancelEventArgs e)
        {
            var ctrl = sender as Guna.UI2.WinForms.Guna2TextBox;
            var strInput = ctrl.Text;
            if (strInput.Length == 0)
            {
                errorProviderMain.SetError(ctrl, "not input");
            }
            else
            {
                errorProviderMain.SetError(ctrl, "");
                errorProviderMain.Clear();
            }
        }

        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            var frmdangky = new frmRegister();
            frmdangky.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
