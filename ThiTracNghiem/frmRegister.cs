using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThiTracNghiem
{
    public partial class frmRegister : Form
    {
        AccessData acc = new AccessData();
        frmLogin frmlogin;
        static string[] arrHexa = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };
        public frmRegister()
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
            btnExit.Click += btnExit_Click_1;
            btnLogin.Click += BtnLogin_Click;

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

                    ttn.NguoiDungs.InsertOnSubmit(new NguoiDung
                    {
                        maND = ID,
                        maLND = cmbTypeUser.SelectedValue.ToString(),
                        MatKhau = sb.ToString()
                    });

                    if (cmbTypeUser.SelectedValue.ToString() == "HS")
                    {
                        ttn.HocSinhs.InsertOnSubmit(new HocSinh
                        {
                            maHS = ID,
                            HoTen = txtName.Text,
                            NgaySinh = dtmDOB.Value,
                            maKhoi = cmbGrade.SelectedValue.ToString(),
                            maLop = cmbClass.SelectedValue.ToString()
                        });
                    }
                    else if (cmbTypeUser.SelectedValue.ToString() == "GV")
                    {
                        ttn.GiaoViens.InsertOnSubmit(new GiaoVien
                        {
                            maGV = ID,
                            HoTen = txtName.Text,
                            NgaySinh = dtmDOB.Value,
                            maMH = cmbSubject.SelectedValue.ToString()
                        });

                        //Add teaching 
                        foreach (DataGridViewRow r in dgvListClass.Rows)
                        {
                            DataGridViewCheckBoxCell chkchecking = r.Cells[0] as DataGridViewCheckBoxCell;
                            if (Convert.ToBoolean(chkchecking.Value))
                            {
                                ttn.CT_GiangDays.InsertOnSubmit(new CT_GiangDay
                                {
                                    maGV = ID,
                                    maKhoi = r.Cells[1].Value.ToString(),
                                    maLop = r.Cells[2].Value.ToString()

                                });
                                ttn.SubmitChanges();
                            }
                        }
                    }


                    ttn.SubmitChanges();
                }

                MessageBox.Show("Đăng Ký Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information); // Nếu đang ký thành công => Sẽ có thông báo Thành Công và đồng thời các TextBox sẽ mất giá trị do [B]ClearTextBox()[/B].
                ClearTextBox();
                this.Close();
            };
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            var frm = new frmLogin();
            frm.Show();
            this.Close();
        }

        private void loadcbgrade()
        {
            using (var ttn = new QLTTNDataContext())
            {
                cmbGrade.DataSource = ttn.KhoiLops.Select(kl => new { kl.maKhoi }).ToList();
                cmbGrade.ValueMember = "maKhoi";
                cmbGrade.DisplayMember = "maKhoi";
            }
        }

        private void loadcbclass()
        {
            using (var ttn = new QLTTNDataContext())
            {
                cmbClass.DataSource = ttn.LopHocs.Where(lh => lh.maKhoi == cmbGrade.SelectedValue.ToString()).Select(lh => new { lh.maLop }).ToList();
                cmbClass.ValueMember = "maLop";
                cmbClass.DisplayMember = "maLop";
            }
        }

        private void LoaddgvListClass()
        {
            using (var ttn = new QLTTNDataContext())
            {
                dgvListClass.DataSource = ttn.LopHocs.Select(lh => new { lh.maKhoi, lh.maLop }).ToList();
                dgvListClass.Columns["maKhoi"].Width = 60;
                dgvListClass.Columns["maLop"].Width = 60;

            }
        }

        private void loadcbSub()
        {
            using (var ttn = new QLTTNDataContext())
            {
                cmbSubject.DataSource = ttn.MonHocs.Select(mh => new { mh.maMH, mh.tenMH }).ToList();
                cmbSubject.ValueMember = "maMH";
                cmbSubject.DisplayMember = "tenMH";
            }
        }


        private void loadcbTypeuser()
        {
            using (var ttn = new QLTTNDataContext())
            {
                cmbTypeUser.DataSource = ttn.LoaiNguoiDungs.Select(lnd => new { lnd.maLND, lnd.TenLND }).ToList();
                cmbTypeUser.ValueMember = "maLND";
                cmbTypeUser.DisplayMember = "TenLND";
            }
        }

        private void ClearTextBox()
        {
            txtId.Clear();
            txtPass.Clear();
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void frmRegister_Load(object sender, EventArgs e)
        {

        }
    }
}