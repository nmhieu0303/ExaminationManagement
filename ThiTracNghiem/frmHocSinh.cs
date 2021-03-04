using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Linq;
using System.IO;
using ClosedXML.Excel;
using System.Security.Cryptography;

namespace ThiTracNghiem
{
    public partial class frmHocSinh : Form
    {
        static string[] arrHexa = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };
        frmLogin frmlogin = null;
        // frmLogin frmlogin = null;
        HocSinh hs = null;
        private BindingSource bsLichThi = new BindingSource();
        private BindingSource bsKyThi = new BindingSource();
        private BindingSource bsThongTinKyThi = new BindingSource();
        private BindingSource bsKyThiThu = new BindingSource();
        private BindingSource bsThongTinKyThiThu = new BindingSource();
        QLTTNDataContext qlttn = new QLTTNDataContext();
        BuoiThi btGanNhat = null;
        KyThi ktGanNhat = null;
        BaiLam bl = null;

        /// <summary>
        ///  trỏ tới button câu hỏi đang hiển thị câu hỏi hiện tại
        /// </summary>
        Button btnCauHoiHienTai = null;
        public frmHocSinh(frmLogin frmlogin, HocSinh hs)
        {

            
            this.frmlogin = frmlogin;
            this.hs = qlttn.HocSinhs.Where(hs1 => hs1.maHS == hs.maHS).FirstOrDefault();
            InitializeComponent();
            btGanNhat = qlttn.BuoiThis.Where(bt => bt.KyThi.LoaiKT == "ThiThiet" &&
                                                        bt.BaiLams.Where(bl => bl.maHS == hs.maHS && bl.DiemSo == -1).Count() > 0 &&
                                                        (bt.NgayGioThi > DateTime.Now ||
                                                        (DateTime.Now >= bt.NgayGioThi && DateTime.Now <= bt.NgayGioThi + bt.DeThi.ThoiGianLamBai)))
                                                        .OrderBy(bt => bt.NgayGioThi)
                                                        .FirstOrDefault();
            if (btGanNhat != null)
            {
                ktGanNhat = btGanNhat.KyThi;
                bl = btGanNhat.BaiLams.Where(bl => bl.maHS == hs.maHS).FirstOrDefault();
                SetUpThi();
            }
            else
            {
                MessageBox.Show("Chào mừng bạn đến với ứng dụng thi trắc nghiệm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowChildControl(tabControl1.TabPages[0], false);
                tabControl1.SelectedIndex = 1;
            }
            btnXuatBangDiem.Click += btnXuatBangDiem_Click;
            btnNopBai.Click += BtnNopBai_Click;

            this.FormClosing += (s, e) =>
            {
                frmlogin.Show();
            };

            tabControl1.SelectedIndexChanged += (s, e) =>
            {
                if (tabControl1.SelectedIndex == 1)
                {
                    loadThongTinHocSinh();
                    loadCbKyThis();
                    loadDgvDaThi();
                    loadDgvLichThi();
                }
                else if (tabControl1.SelectedIndex == 2)
                {
                    loadCbKyThiThu();
                    loadDgvKyThiThu();
                }
            };
            btnLogout.Click += btnLogout_Click;
            btnThiThu.Click += btnThiThu_Click;
            txtHoTenHS.Validating += Txt_Validating;
            txtNgaySinh.Validating += Txt_Validating;
            txtMatKhauCu.Validating += Txt_Validating;
            txtMatKhauMoi.Validating += Txt_Validating;
            cbKyThis.SelectedIndexChanged += cbKyThis_SelectedIndexChanged;
            txtNgaySinh.Validating += (s, e) =>
            {
                try
                {
                    epMain.SetError(txtNgaySinh, "");
                    var ns = DateTime.Parse(txtNgaySinh.Text);
                }
                catch (Exception exec)
                {
                    epMain.SetError(txtNgaySinh, "Hãy nhập đúng ngày sinh");
                    txtNgaySinh.Focus();
                    return;
                }
            };
            btnCapNhat.Click += BtnCapNhat_Click;
            cbKyThiThu.SelectedIndexChanged += CbKyThiThu_SelectedIndexChanged;
            btnSavePass.Click += BtnSavePass_Click;
            btnChangePass.Click += BtnChangePass_Click;
            Load += FrmHocSinh_Load;
            txtMatKhauMoi.Hide();
            lblPassNew.Hide();

        }

        
        private void BtnChangePass_Click(object sender, EventArgs e)
        {
            txtMatKhauMoi.Show();
            lblPassNew.Show();
            btnChangePass.Hide();
            txtMatKhauCu.Enabled = true;
        }

        private void BtnCapNhat_Click(object sender, EventArgs e)
        {
            txtMatKhauMoi.Hide();
            lblPassNew.Hide();
            btnChangePass.Show();
            try
            {
                hs.HoTen = txtHoTenHS.Text;
                hs.NgaySinh = DateTime.Parse(txtNgaySinh.Text);
                qlttn.SubmitChanges();
                loadThongTinHocSinh();
                MessageBox.Show("Cập nhật thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exec)
            {
                MessageBox.Show($"Không thể cập nhật thông tin{Environment.NewLine}{exec.Message}", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void BtnSavePass_Click(object sender, EventArgs e)
        {
            try
            {
                var password = txtMatKhauCu.Text;
                var paswordcf = txtMatKhauMoi.Text;
                if (!string.IsNullOrWhiteSpace(txtMatKhauCu.Text) && !string.IsNullOrWhiteSpace(txtMatKhauMoi.Text))
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
                    if (hs.NguoiDung.MatKhau == sb.ToString())
                    {
                        bytesPw = ASCIIEncoding.ASCII.GetBytes(paswordcf);
                        bytesPwHashed = sha1.ComputeHash(bytesPw);
                        sb = new StringBuilder();
                        foreach (byte b in bytesPwHashed)
                        {
                            sb.Append(arrHexa[b >> 4]);
                            sb.Append(arrHexa[b & 15]);
                        }
                        qlttn.HocSinhs.Where(hs1 => hs1.maHS == hs.maHS).FirstOrDefault().NguoiDung.MatKhau = sb.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Mật khẩu cũ không đúng", "Báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                qlttn.SubmitChanges();
                txtMatKhauMoi.Hide();
                lblPassNew.Hide();
                MessageBox.Show("Đổi thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch (Exception exec)
            {
                MessageBox.Show($"Không thể cập nhật thông tin{Environment.NewLine}{exec.Message}", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void SetUpThi()
        {
            /// setup thông tin kỳ thi
            lblTenKT.Text = ktGanNhat.TenKT;
            lblTenDT.Text = btGanNhat.DeThi.TenDT;
            lblTenMonThi.Text = btGanNhat.DeThi.MonHoc.tenMH;
            lblThoiGianBatDau.Text = btGanNhat.NgayGioThi.ToString();
            lblThoiGianLamBai.Text = btGanNhat.DeThi.ThoiGianLamBai.ToString();
            lblGVRaDe.Text = btGanNhat.DeThi.GiaoVien.HoTen;
            lblLoaiKT.Text = ktGanNhat.LoaiKT;
            if (ktGanNhat.LoaiKT == "ThiThiet")
            {
                btnXemDA.Visible = false;
            }
            else
            {
                btnXemDA.Visible = true;
            }

            timer1.Interval = 1000;
            timer1.Tick += Timer1_Tick;

            ShowChildControl(tabControl1.TabPages[0], false);
            var lblCountDown = new Label
            {
                Name = "lblCountDown",
                Text = $"Thời gian đến buổi thi kế tiếp còn {btGanNhat.NgayGioThi - DateTime.Now}",
                AutoSize = false,
                Size = new Size(800, 300),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Times New Roman", 24, FontStyle.Bold),
            };
            lblCountDown.Left = (tabControl1.TabPages[0].Width - lblCountDown.Width) / 2;
            lblCountDown.Top = (tabControl1.TabPages[0].Height - lblCountDown.Height) / 2;
            tabControl1.TabPages[0].Controls.Add(lblCountDown);

            timer1.Start();
        }

        private void CbKyThiThu_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDgvKyThiThu();
        }

        private void FrmHocSinh_Load(object sender, EventArgs e)
        {
            loadThongTinHocSinh();
        }



        private void Txt_Validating(object sender, CancelEventArgs e)
        {
            var txt = sender as Guna.UI2.WinForms.Guna2TextBox;
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                epMain.SetError(txt, "Không được để trống");
                txt.Focus();
            }
            else
            {
                epMain.SetError(txt, "");
            }
        }

        private void BtnNopBai_Click(object sender, EventArgs e)
        {
            if (btGanNhat.NgayGioThi + btGanNhat.DeThi.ThoiGianLamBai <= DateTime.Now)
            {
                timerThoiGianLamBai.Stop();
                NopBai();
            }
            else
            {
                var ds = MessageBox.Show("Thời gian vẫn còn, bạn có muốn nộp bài luôn hay không", "Hỏi ý kiến", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                switch (ds)
                {
                    case DialogResult.OK:
                        timerThoiGianLamBai.Stop();
                        NopBai();
                        break;
                    case DialogResult.Cancel:
                        break;
                    default:
                        break;
                }
            }
        }


        /// <summary>
        /// kiểm tra thời gian tới buổi thi gần nhất hay chưa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer1_Tick(object sender, EventArgs e)
        {
            var listControl = tabControl1.TabPages[0].Controls.Find("lblCountDown", false);

            Label lblCountDown;
            if (listControl.Count() == 0)
            {
                lblCountDown = new Label
                {
                    Name = "lblCountDown",
                    Text = $"Thời gian đến buổi thi kế tiếp còn {btGanNhat.NgayGioThi - DateTime.Now}",
                    AutoSize = false,
                    Size = new Size(800, 300),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Times New Roman", 24, FontStyle.Bold),
                };
                tabControl1.TabPages[0].Controls.Add(lblCountDown);
            }
            else
            {
                lblCountDown = listControl.ElementAt(0) as Label;
            }

            if (btGanNhat.NgayGioThi <= DateTime.Now)
            {
                timer1.Stop();
                tabControl1.TabPages[0].Controls.Remove(lblCountDown);
                ShowChildControl(tabControl1.TabPages[0], true);
                HienThiCauHoi();
                timerThoiGianLamBai.Interval = 1000;
                timerThoiGianLamBai.Tick += TimerThoiGianLamBai_Tick;
                timerThoiGianLamBai.Start();
                MessageBox.Show("Chúc bạn làm bài thật tốt", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                lblCountDown.Text = $"Thời gian đến buổi thi kế tiếp còn {btGanNhat.NgayGioThi - DateTime.Now}";
            }
        }

        private void TimerThoiGianLamBai_Tick(object sender, EventArgs e)
        {
            if (btGanNhat.NgayGioThi + btGanNhat.DeThi.ThoiGianLamBai <= DateTime.Now)
            {
                timerThoiGianLamBai.Stop();
                NopBai();
            }
            else
            {
                TimeSpan tp = (DateTime.Now - (btGanNhat.NgayGioThi + btGanNhat.DeThi.ThoiGianLamBai)).Value;
                lblThoiGianConLai.Text = $"{tp.Hours}:{tp.Minutes}:{tp.Seconds}";
            }
        }

        private void HienThiCauHoi()
        {
            int left = 30, top = 50, space = 10, wid = 50, hei = 50, soBtnDong = 2;
            var dsch = btGanNhat.DeThi.CT_DeThis;

            for (int i = dsch.Count - 1; i >= 0; i--)
            {
                Button btn = new Button();
                btn.Name = $"btn{dsch[i].maCH}";
                btn.Tag = dsch[i];
                btn.BackColor = Color.Salmon;
                btn.Width = wid;
                btn.Height = hei;
                btn.Left = left + (btn.Width + space) * (i % soBtnDong);
                btn.Top = top + (btn.Height + space) * (i / soBtnDong);
                btn.Text = i.ToString();
                btn.Click += BtnCH_Click;

                pnlChonCH.Controls.Add(btn);
            }
        }

        private void BtnCH_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            btnCauHoiHienTai = btn;
            var ch = btn.Tag as CT_DeThi;
            lblNoiDungCH.Text = ch.CauHoi.NoiDung;

            CT_BaiLam ctblCuaHS = null;

            if (ktGanNhat.LoaiKT == "ThiThu")
            {
                string goiy = "";
                if (string.IsNullOrWhiteSpace(ch.CauHoi.GoiY))
                {
                    goiy = "Không có gợi ý";
                }
                else
                {
                    goiy = ch.CauHoi.GoiY;
                }
                lblNoiDungCH.Text += $"{Environment.NewLine}Gợi ý: {goiy}";

                ctblCuaHS = ch.CT_BaiLams.Where(ctbl => ctbl.maHS == hs.maHS && ctbl.maKT == ktGanNhat.maKT && ctbl.maDT == btGanNhat.maDT && ctbl.maCH == ch.maCH).FirstOrDefault();
            }

            int left = 30, top = 20, space = 20, i = 0;

            pnlDapAn.Controls.Clear();
            foreach (var da in ch.CauHoi.DapAns)
            {
                var rbDapAn = new RadioButton();
                rbDapAn.AutoSize = true;
                rbDapAn.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                rbDapAn.Text = da.NoiDung;
                rbDapAn.Tag = da;
                rbDapAn.Left = left;
                rbDapAn.Top = top + (rbDapAn.Height + space) * i;

                rbDapAn.CheckedChanged += RbDapAn_CheckedChanged;

                pnlDapAn.Controls.Add(rbDapAn);
                i++;
            }
        }

        private void RbDapAn_CheckedChanged(object sender, EventArgs e)
        {
            var rb = sender as RadioButton;
            if (rb.Checked)
            {
                var da = rb.Tag as DapAn;
                var btnCauHoi = pnlChonCH.Controls.Find($"btn{da.maCH}", false)[0];
                btnCauHoi.BackColor = Color.GreenYellow;

                var chDaLam = qlttn.CT_BaiLams.Where(ctbl => ctbl.maKT == ktGanNhat.maKT
                                                && ctbl.maDT == btGanNhat.maDT
                                                && ctbl.maHS == hs.maHS
                                                && ctbl.maCH == da.maCH).FirstOrDefault();
                if (chDaLam != null)
                {
                    qlttn.CT_BaiLams.DeleteOnSubmit(chDaLam);
                }
                qlttn.CT_BaiLams.InsertOnSubmit(new CT_BaiLam
                {
                    maKT = ktGanNhat.maKT,
                    maDT = btGanNhat.maDT,
                    maHS = hs.maHS,
                    maCH = da.maCH,
                    maDA = da.maDA,
                    DungSai = da.DungSai
                });
                qlttn.SubmitChanges();
            }
        }

        private void NopBai()
        {
            qlttn.SubmitChanges();
            var blHocSinh = hs.BaiLams.Where(bl => bl.maKT == ktGanNhat.maKT && bl.maDT == btGanNhat.maDT).FirstOrDefault();
            qlttn.Refresh(RefreshMode.OverwriteCurrentValues, blHocSinh);
            ShowChildControl(tabControl1.TabPages[0], false);

            if (ktGanNhat.LoaiKT == "ThiThu")
            {
                MessageBox.Show(" ---------------- KẾT QUẢ ----------------"
                    + $"{Environment.NewLine}" + $" - Điểm số: {blHocSinh.DiemSo}"
                    + $"{Environment.NewLine}" + $" - Tổng số câu đúng: {blHocSinh.DiemSo}"
                    , "Kết quả bài làm", MessageBoxButtons.OK);
                //qlttn.CT_BaiLams.DeleteAllOnSubmit(blHocSinh.CT_BaiLams);
                //bl = qlttn.BaiLams.Where(bl => bl.maHS == hs.maHS && bl.maKT == ktGanNhat.maKT && bl.maDT == btGanNhat.maDT).FirstOrDefault();
                //qlttn.SubmitChanges();
                // qlttn.ExecuteCommand($"update BaiLam set DiemSo = 0 where maKT={bl.maKT} and maDT={bl.maDT} and maHS={bl.maHS}");
                //this.Close();
                //return;
            }

            /// nếu như thí sinh đã hoàn thành buổi thi cuối cùng thì hiển thị lên cho học sinh xem điểm cả kỳ thi
            if (ktGanNhat.BuoiThis.OrderByDescending(bt => bt.NgayGioThi).FirstOrDefault() == btGanNhat)
            {
                var ds = MessageBox.Show("Đã lưu bài làm. Bạn có muốn xem điểm thi hay không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                switch (ds)
                {
                    case DialogResult.OK:
                        tabControl1.SelectedIndex = 1;
                        break;
                    case DialogResult.Cancel:
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show($"Đã nộp bài!{Environment.NewLine}Hãy đăng nhập lại để chuẩn bị cho buổi thi kế tiếp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }


        private void ShowChildControl(Control c, bool show)
        {
            foreach (Control ctrl in c.Controls)
            {
                ctrl.Visible = show;
            }
        }

        private void loadDgvLichThi()
        {
            btGanNhat = qlttn.BuoiThis.Where(bt => bt.KyThi.LoaiKT == "ThiThiet" &&
                                            bt.BaiLams.Where(bl => bl.maHS == hs.maHS && bl.DiemSo == -1).Count() > 0 &&
                                            (bt.NgayGioThi > DateTime.Now ||
                                            (DateTime.Now >= bt.NgayGioThi && DateTime.Now <= bt.NgayGioThi + bt.DeThi.ThoiGianLamBai))).FirstOrDefault();
            if (btGanNhat != null)
            {
                ktGanNhat = btGanNhat.KyThi;
                bsLichThi.DataSource = ktGanNhat.BuoiThis.Select(bt => new
                {
                    bt.KyThi.TenKT,
                    bt.DeThi.MonHoc.tenMH,
                    bt.NgayGioThi,
                    bt.DeThi.ThoiGianLamBai
                }).ToList();
                dgvLichThi.DataSource = bsLichThi;
                dgvLichThi.Columns["TenKT"].HeaderText = "Kỳ thi";
                dgvLichThi.Columns["tenMH"].HeaderText = "Môn thi";
                dgvLichThi.Columns["NgayGioThi"].HeaderText = "Thời gian bắt đầu";
                dgvLichThi.Columns["ThoiGianLamBai"].HeaderText = "Thời gian làm bài";
            }
        }

        private void loadCbKyThis()
        {
            bsKyThi.DataSource = qlttn.KyThis.Where(kt => kt.LoaiKT == "ThiThiet" && kt.BuoiThis.Where(bt => bt.BaiLams.Where(bl => bl.maHS == hs.maHS && bl.DiemSo != null).Count() > 0).Count() > 0).Select(kt => new { kt.maKT, kt.TenKT }).ToList();
            if (bsKyThi.Count > 0)
            {
                cbKyThis.ValueMember = "maKT";
                cbKyThis.DisplayMember = "TenKT";
                cbKyThis.DataSource = bsKyThi;
            }
        }

        private void loadDgvDaThi()
        {
            if (cbKyThis.SelectedValue != null)
            {

                int makt = int.Parse(cbKyThis.SelectedValue.ToString());
                qlttn.Refresh(RefreshMode.OverwriteCurrentValues, qlttn.BaiLams.Where(bl => bl.maHS == hs.maHS && bl.maKT == makt));

                var kythi = qlttn.KyThis.Where(kt => kt.maKT == makt).FirstOrDefault();

                try
                {
                    bsThongTinKyThi.DataSource = kythi.BuoiThis.Select(bt => new
                    {
                        bt.DeThi.MonHoc.tenMH,
                        DiemSo = qlttn.BaiLams.Where(bl => bl.maHS == hs.maHS && bl.maDT == bt.maDT && bl.maKT == bt.maKT).FirstOrDefault().DiemSo,
                        SoCauDung = qlttn.BaiLams.Where(bl => bl.maHS == hs.maHS && bl.maDT == bt.maDT && bl.maKT == bt.maKT).FirstOrDefault().DiemSo
                    }).ToList();
                }
                catch (Exception)
                {
                    return;
                }

                dgvKetQuaKT.DataSource = bsThongTinKyThi;

                dgvKetQuaKT.Columns["tenMH"].HeaderText = "Môn thi";
                dgvKetQuaKT.Columns["DiemSo"].HeaderText = "Điểm";
                dgvKetQuaKT.Columns["SoCauDung"].HeaderText = "Số câu đúng";

            }
        }

        private void loadThongTinHocSinh()
        {
            txtMaHS.Text = hs.maHS;
            txtHoTenHS.Text = hs.HoTen;
            txtNgaySinh.Text = hs.NgaySinh.Value.ToShortDateString();
            txtMatKhauCu.Clear();
            txtMatKhauMoi.Clear();


        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmHocSinh_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmlogin.Show();
        }

        private void cbKyThis_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDgvDaThi();
        }

        /*============================================= thi thử =======================================*/
        private void loadCbKyThiThu()
        {
            bsKyThiThu.DataSource = qlttn.KyThis.Where(kt => kt.LoaiKT == "ThiThu" && kt.BuoiThis.Where(bt => bt.BaiLams.Where(bl => bl.maHS == hs.maHS && bl.DiemSo == -1).Count() > 0).Count() > 0).Select(kt => new { kt.maKT, kt.TenKT }).ToList();
            if (bsKyThiThu.Count > 0)
            {
                cbKyThiThu.ValueMember = "maKT";
                cbKyThiThu.DisplayMember = "TenKT";
                cbKyThiThu.DataSource = bsKyThiThu;
            }
        }

        private void loadDgvKyThiThu()
        {
            if (cbKyThiThu.SelectedValue != null)
            {
                int makt = int.Parse(cbKyThiThu.SelectedValue.ToString());
                qlttn.Refresh(RefreshMode.OverwriteCurrentValues, qlttn.BaiLams.Where(bl => bl.maHS == hs.maHS && bl.maKT == makt));

                var kythi = qlttn.KyThis.Where(kt => kt.maKT == makt).FirstOrDefault();
                bsThongTinKyThiThu.DataSource = kythi.BuoiThis.Select(bt => new
                {
                    bt.maDT,
                    bt.maKT,
                    bt.DeThi.MonHoc.tenMH,
                    bt.DeThi.TenDT,
                    bt.DeThi.ThoiGianLamBai,
                    bt.DeThi.GiaoVien.HoTen
                }).ToList();

                dgvTTKyThiThu.DataSource = bsThongTinKyThiThu;

                dgvTTKyThiThu.Columns["maDT"].Visible = false;
                dgvTTKyThiThu.Columns["maKT"].Visible = false;
                dgvTTKyThiThu.Columns["tenMH"].HeaderText = "Môn thi";
                dgvTTKyThiThu.Columns["TenDT"].HeaderText = "Đề thi";
                dgvTTKyThiThu.Columns["ThoiGianLamBai"].HeaderText = "Thời gian làm bài";
                dgvTTKyThiThu.Columns["HoTen"].HeaderText = "Giáo viên ra đề";

            }
        }

        private void btnThiThu_Click(object sender, EventArgs e)
        {
            if (dgvTTKyThiThu.SelectedRows.Count > 0)
            {
                int madt = int.Parse(dgvTTKyThiThu.SelectedRows[0].Cells["maDT"].Value.ToString());
                int makt = int.Parse(dgvTTKyThiThu.SelectedRows[0].Cells["maKT"].Value.ToString());
                btGanNhat = qlttn.BuoiThis.Where(bt => bt.maKT == makt && bt.maDT == madt).FirstOrDefault();
                btGanNhat.NgayGioThi = DateTime.Now;
                ktGanNhat = btGanNhat.KyThi;
                tabControl1.SelectedIndex = 0;
                SetUpThi();
            }
            else
            {
                MessageBox.Show("Hãy chọn một đề thi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnXemDA_Click(object sender, EventArgs e)
        {
            if (btnCauHoiHienTai == null)
            {
                return;
            }
            int index = lblNoiDungCH.Text.IndexOf($"{Environment.NewLine}Đáp án:");

            if (index == -1)
            {
                var noiDungDapAn = (btnCauHoiHienTai.Tag as CT_DeThi).CauHoi.DapAns.Where(da => da.DungSai == true).FirstOrDefault().NoiDung;
                lblNoiDungCH.Text += $"{Environment.NewLine}Đáp án: {noiDungDapAn}";
                btnXemDA.Text = "Ẩn đáp án";
            }
            else
            {
                lblNoiDungCH.Text = lblNoiDungCH.Text.Remove(index);
                btnXemDA.Text = "Xem đáp án";
            }
        }

        private void btnXuatBangDiem_Click(object sender, EventArgs e)
        {
            if (cbKyThis.SelectedValue != null)
            {
                int makt = int.Parse(cbKyThis.SelectedValue.ToString());
                var frmketqua = new frmReportKetQuaKyThi(makt, hs.maHS);
                frmketqua.Show();
            }
        }

        private void bttnGbTtHS_Enter(object sender, EventArgs e)
        {

        }

        private void btnNopBai_Click_1(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void lblThoiGianLamBai_Click(object sender, EventArgs e)
        {

        }

        private void frmHocSinh_Load_1(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMatKhauCu_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
