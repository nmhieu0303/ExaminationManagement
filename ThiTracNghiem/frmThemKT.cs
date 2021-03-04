using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThiTracNghiem
{
    public partial class frmThemKT : Form
    {
        BindingSource bsLichThi = new BindingSource();
        string maKhoi;
        GiaoVien GV;
        private string LoaiKT;
        private List<string> lkl = null;

        public frmThemKT(frmGiaoVien frmgv, GiaoVien GV, string maKhoi, string LoaiKT)
        {
            frmgv.Enabled = false;
            this.GV = GV;
            this.maKhoi = maKhoi;
            this.LoaiKT = LoaiKT;
            InitializeComponent();
            setQlkt();
            LoadQLKTDgvDeThi();
            LoadQLKTDgvHocSinh();


            this.FormClosing += (s, e) =>
            {
                frmgv.Enabled = true;
            };
            btnThemKT.Click += (s, e) =>
             {
                 if (string.IsNullOrWhiteSpace(txtTenKT.Text))
                 {
                     MessageBox.Show("Không được để trống tên kỳ thi", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     return;
                 }
                 if (dtpNgayThi.Value < DateTime.Now)
                 {
                     MessageBox.Show("Ngày giờ thi phải lớn hơn ngày giờ hiện tại", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     return;
                 }

                 List<string> dsHocSinh = new List<string>();
                 // sau cái vòng lặp này thì sẽ lấy ra được danh sách học sinh thi
                 foreach (DataGridViewRow row in dgvHS.Rows)
                 {
                     var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                     if (cell.Value == cell.TrueValue)
                     {
                         dsHocSinh.Add(row.Cells["maHS"].Value.ToString());
                     }
                 }
                 if (dsHocSinh.Count == 0)
                 {
                     MessageBox.Show($"Mời bạn chọn các thí sinh (lưu ý chọn các thí sinh không bận thi vào thời gian {dtpNgayThi.Value}", "Thông báo",
                                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                     return;
                 }

                 List<int> dsMaDT = new List<int>();
                 foreach (DataGridViewRow row in dgvDT.Rows)
                 {
                     var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                     if (cell.Value == cell.TrueValue && row.Cells["maMH"].Value.ToString() == GV.maMH)
                     {
                         dsMaDT.Add(int.Parse(row.Cells["maDT"].Value.ToString()));
                     }
                 }

                 if (dsMaDT.Count == 0)
                 {
                     MessageBox.Show("Hãy chọn một đề thi của bộ môn mà bạn đang phụ trách cho kỳ thi", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     return;
                 }
                 using (var qlttn = new QLTTNDataContext())
                 {

                     qlttn.KyThis.InsertOnSubmit(new KyThi
                     {
                         TenKT = txtTenKT.Text,
                         maKhoi = this.maKhoi,
                         maGV = GV.maGV
                     });
                     qlttn.SubmitChanges();
                     int maktVuaThem = (int)qlttn.ExecuteQuery<decimal>("select IDENT_CURRENT('dbo.KyThi')").FirstOrDefault();

                     if (LoaiKT == "ThiThiet")
                     {
                         qlttn.KyThis.Where(kt => kt.maKT == maktVuaThem).FirstOrDefault().LoaiKT = "ThiThiet";
                         BuoiThi bt = new BuoiThi()
                         {
                             maKT = maktVuaThem,
                             maDT = dsMaDT[0],
                             NgayGioThi = dtpNgayThi.Value
                         };
                         qlttn.BuoiThis.InsertOnSubmit(bt);

                         foreach (var hs in dsHocSinh)
                         {
                             qlttn.BaiLams.InsertOnSubmit(new BaiLam
                             {
                                 maHS = hs,
                                 maDT = bt.maDT,
                                 maKT = maktVuaThem,
                                 DiemSo = -1
                             });
                         }
                         qlttn.SubmitChanges();
                         frmgv.LoadQLKTDgvKyThi();
                     }
                     else if (LoaiKT == "ThiThu")
                     {
                         qlttn.KyThis.Where(kt => kt.maKT == maktVuaThem).FirstOrDefault().LoaiKT = "ThiThu";
                         foreach (var madt in dsMaDT)
                         {
                             BuoiThi bt = new BuoiThi()
                             {
                                 maKT = maktVuaThem,
                                 maDT = madt,
                                 NgayGioThi = dtpNgayThi.Value
                             };
                             qlttn.BuoiThis.InsertOnSubmit(bt);

                             foreach (var hs in dsHocSinh)
                             {
                                 qlttn.BaiLams.InsertOnSubmit(new BaiLam
                                 {
                                     maHS = hs,
                                     maDT = bt.maDT,
                                     maKT = maktVuaThem,
                                     DiemSo = -1
                                 });
                             }
                         }

                         qlttn.SubmitChanges();
                         frmgv.LoadQLKTOTDgvKyThi();
                     }
                 }

                 MessageBox.Show("Thêm kỳ thi thành công", "Thông báo");
                 this.Close();
             };
        }
        private void setQlkt()
        {
            dtpNgayThi.Value = DateTime.Now + TimeSpan.FromDays(1);

            dgvHS.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                Name = "Chon",
                HeaderText = "Chọn học sinh",
                Width = 80,
                TrueValue = true,
                FalseValue = false,
                IndeterminateValue = false
            });

            dgvDT.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                Name = "Chon",
                HeaderText = "Chọn đề thi",
                Width = 80,
                TrueValue = true,
                FalseValue = false,
                IndeterminateValue = false
            });

            btnRdHs.Click += (s, e) =>
            {
                CheckDGV(dgvHS, (int)nudSoHocSinh.Value);
            };
            btnChonHetHS.Click += (s, e) =>
            {
                CheckDGV(dgvHS, dgvHS.RowCount);
            };
            dtpNgayThi.ValueChanged += (s, e) =>
            {
             //  LoadQLKTDgvHocSinh();
            };
            dgvDT.CellContentClick += (s, e) =>
              {
                  if (LoaiKT == "ThiThiet")
                  {
                      CheckDGV(dgvDT, 0);
                  }
              };
            dgvHS.CellValueChanged += (s, e) =>
              {
                  gbTongSoThiSinh.Text = $"Tổng số thí sinh: {soDongDuocChon(dgvHS)}";
              };
        }
        
        /// <param name="dgv">bắt buộc dgv phải có cột có Name = "Chon"</param>
        /// <returns></returns>
        private int soDongDuocChon(DataGridView dgv)
        {
            int soDong = 0;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                if (cell.Value == cell.TrueValue)
                {
                    soDong++;
                }
            }
            return soDong;
        }
        private void LoadQLKTDgvHocSinh()
        {
            using (var qlttn = new QLTTNDataContext())
            {

                lkl = GV.CT_GiangDays.Select(ctgd => ctgd.maKhoi).Distinct().ToList();
                CbKhoiLopKT.DataSource = lkl;

                if (qlttn.HocSinhs.Count() == 0)
                {
                    MessageBox.Show("Không có dữ liệu học sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                try
                {
                    TimeSpan thoiGianThi = TimeSpan.FromMinutes(0);
                    DateTime thoiGianBatDau = dtpNgayThi.Value;

                    var dtDuocChon = getDeThiDuocChon();
                    if (dtDuocChon != null)
                    {
                        thoiGianThi = (TimeSpan)dtDuocChon.ThoiGianLamBai;
                    }

                    var dshsRanh = qlttn.HocSinhs.Where(hs => hs.maKhoi == maKhoi
                                                                && hs.BaiLams.Where(bl => bl.BuoiThi.NgayGioThi > thoiGianBatDau + thoiGianThi
                                                                                        || bl.BuoiThi.NgayGioThi + bl.BuoiThi.DeThi.ThoiGianLamBai < thoiGianBatDau
                                                                                   )
                                                                            .Count() == hs.BaiLams.Count
                                                               )
                                                         .Select(hs => new QLTTNDataContext.MyHocSinh
                                                         {
                                                             maHS = hs.maHS,
                                                             HoTen = hs.HoTen,
                                                             maKhoi = hs.maKhoi,
                                                             maLop = hs.maLop,
                                                             NgaySinh = (DateTime)hs.NgaySinh
                                                         }).ToList();


                    dgvHS.DataSource = dshsRanh;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return;
                }
                if (dgvHS.RowCount > 0)
                {
                    dgvHS.Columns["maHS"].Width = 80;
                    dgvHS.Columns["HoTen"].Width = 160;
                    dgvHS.Columns["maLop"].Width = 80;
                    dgvHS.Columns["NgaySinh"].Width = 80;
                    dgvHS.Columns["maKhoi"].Visible = false;

                    dgvHS.Columns["maHS"].HeaderText = "Mã học sinh";
                    dgvHS.Columns["HoTen"].HeaderText = "Họ tên";
                    dgvHS.Columns["maLop"].HeaderText = "Lớp học";
                    dgvHS.Columns["NgaySinh"].HeaderText = "Ngày sinh";
                }
                else
                {
                    MessageBox.Show($"Hiện tất cả các học sinh của khối <{maKhoi}> đều đang bận thi" +
                                    $"{Environment.NewLine}Vui lòng chọn thời gian khác để tạo kỳ thi",
                                    "Thông báo",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
            }
        }
        private void LoadQLKTDgvDeThi()
        {
            using (var qlttn = new QLTTNDataContext())
            {
                if (qlttn.DeThis.Count() == 0)
                {
                    MessageBox.Show("Không có dữ liệu đề thi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                try
                {
                    bsLichThi.DataSource = qlttn.DeThis.Where(dt => dt.maMH == GV.maMH)
                                                       .Select(dt => new { dt.maDT, dt.TenDT, dt.maMH, dt.MonHoc.tenMH, dt.ThoiGianLamBai });
                    if (bsLichThi.Count > 0)
                    {
                        dgvDT.DataSource = bsLichThi;
                        dgvDT.Columns["maDT"].HeaderText = "Mã";
                        dgvDT.Columns["maDT"].Width = 30;
                        dgvDT.Columns["maMH"].Visible = false;
                        dgvDT.Columns["TenDT"].HeaderText = "Tên đề thi";
                        dgvDT.Columns["TenDT"].Width = 140;
                        dgvDT.Columns["tenMH"].HeaderText = "Môn thi";
                        dgvDT.Columns["ThoiGianLamBai"].HeaderText = "Thời gian làm bài";
                        dgvDT.Columns["ThoiGianLamBai"].Width = 130;
                    }
                    else
                    {
                        dtpNgayThi.DataBindings.Clear();
                        MessageBox.Show("Không có dữ liệu đề thi", "Thông báo tab Quản lý kỳ thi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return;
                }
            }
        }
        
        private QLTTNDataContext.MyDeThi getDeThiDuocChon()
        {
            QLTTNDataContext.MyDeThi dt = null;
            foreach (DataGridViewRow row in dgvDT.Rows)
            {
                var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                if (cell.Value == cell.TrueValue)
                {
                    dt = new QLTTNDataContext.MyDeThi
                    {
                        maDT = int.Parse(row.Cells["maDT"].Value.ToString()),
                        TenDT = row.Cells["TenDT"].Value.ToString(),
                        maGV = row.Cells["maGV"].Value.ToString(),
                        maKhoi = row.Cells["maKhoi"].Value.ToString(),
                        maMH = row.Cells["maMH"].Value.ToString(),
                        ThoiGianLamBai = TimeSpan.Parse(row.Cells["ThoiGianLamBai"].Value.ToString())
                    };
                    break;
                }
            }
            return dt;
        }
        private void CheckDGV(DataGridView dgv, int soDongMuonCheck)
        {
            if (dgv.RowCount <= 0)
            {
                return;
            }
            if (soDongMuonCheck > dgv.RowCount)
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                    cell.Value = cell.TrueValue;
                }
                return;
            }

            Random rd = new Random();
            int maxCau = soDongMuonCheck;

            if (maxCau > dgv.RowCount)
            {
                maxCau = dgv.RowCount;
            }
            foreach (DataGridViewRow row in dgv.Rows)
            {
                var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                cell.Value = cell.FalseValue;
            }
            List<int> li = new List<int>();
            while (li.Count < maxCau)
            {
                int dongNgauNhien = rd.Next(0, dgv.RowCount);

                if (!li.Contains(dongNgauNhien))
                {
                    li.Add(dongNgauNhien);
                    var cell = dgv.Rows[dongNgauNhien].Cells["Chon"] as DataGridViewCheckBoxCell;
                    cell.Value = cell.TrueValue;
                }
            }
        }

        private void dtpNgayThi_ValueChanged(object sender, EventArgs e)
        {
            dtpNgayThi.Format = DateTimePickerFormat.Custom;
            dtpNgayThi.CustomFormat = "MM/dd/yyyy hh:mm:ss tt";
        }
    }
}
