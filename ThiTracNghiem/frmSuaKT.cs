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

    public partial class frmSuaKT : Form
    {
        string maKhoi;
        int makt;
        public int madt = -1;

        BindingSource BSLichThi = new BindingSource();
        GiaoVien GV;
        BuoiThi BT;
        QLTTNDataContext.MyDeThi DT;
        List<QLTTNDataContext.MyHocSinh> DSHS;
        QLTTNDataContext QLTTN = new QLTTNDataContext();
        KyThi KTCCN;

        
        public List<string> DSMaHSDuocChon = new List<string>();
        public DialogResult DResult;

        public frmSuaKT(frmGiaoVien frmgv, GiaoVien GV, int makt, string maKhoi, BindingSource bsKyThi)
        {
            frmgv.Enabled = false;
            this.GV = GV;
            this.maKhoi = maKhoi;
            this.makt = makt;

            KTCCN = QLTTN.KyThis.Where(kt => kt.maKT == makt).FirstOrDefault();
            BT = QLTTN.BuoiThis.Where(BT => BT.maKT == makt && BT.DeThi.maMH == GV.maMH).FirstOrDefault();
            if (BT != null)
            {
                DSHS = QLTTN.BaiLams.Where(bl => bl.maKT == BT.maKT && bl.maDT == BT.maDT).
                               Select(hs => new QLTTNDataContext.MyHocSinh
                               {
                                   maHS = hs.maHS,
                                   HoTen = hs.HocSinh.HoTen,
                                   maKhoi = hs.HocSinh.maKhoi,
                                   maLop = hs.HocSinh.maLop,
                                   NgaySinh = (DateTime)hs.HocSinh.NgaySinh
                               }).
                               ToList();
                DT = QLTTN.KyThis.Where(kt => kt.maKT == this.makt).FirstOrDefault()
                                            .BuoiThis.Where(BT => BT.DeThi.maMH == GV.maMH)
                                            .Select(BT => new QLTTNDataContext.MyDeThi
                                            {
                                                maDT = BT.maDT,
                                                maGV = BT.DeThi.maGV,
                                                maKhoi = BT.DeThi.maKhoi,
                                                maMH = BT.DeThi.maMH,
                                                TenDT = BT.DeThi.TenDT,
                                                ThoiGianLamBai = (TimeSpan)BT.DeThi.ThoiGianLamBai
                                            }).FirstOrDefault();
            }


            InitializeComponent();

            Load += (s, e) =>
              {
                  SetQLKT();
                  LoadQLKTDgvDeThi();
                  LoadQLKTDgvHocSinh();
                  txtTenKT.DataBindings.Add("Text", bsKyThi, "TenKT", true, DataSourceUpdateMode.Never);
                  lblLoaiKT.DataBindings.Add("Text", bsKyThi, "LoaiKT", true, DataSourceUpdateMode.Never);
                  lblMaKhoi.Text = maKhoi;
              };

            this.FormClosing += (s, e) =>
            {
                frmgv.Enabled = true;
            };
            btnSuaKT.Click += (s, e) =>
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

                foreach (DataGridViewRow row in dgvHS.Rows)
                {
                    var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                    if (cell.Value == cell.TrueValue)
                    {
                        DSMaHSDuocChon.Add(row.Cells["maHS"].Value.ToString());
                    }
                }
                if (DSMaHSDuocChon.Count == 0)
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

                try
                {
                    var KTCCN = QLTTN.KyThis.Where(kythi => kythi.maKT == makt).FirstOrDefault();
                    KTCCN.TenKT = txtTenKT.Text;

                    if (KTCCN.LoaiKT == "ThiThiet")
                    {
                        if (BT != null)
                        {
                            QLTTN.BaiLams.DeleteAllOnSubmit(BT.BaiLams);
                            QLTTN.BuoiThis.DeleteOnSubmit(BT);
                            QLTTN.SubmitChanges();
                        }
                        BT = new BuoiThi
                        {
                            maKT = KTCCN.maKT,
                            maDT = dsMaDT[0],
                            NgayGioThi = dtpNgayThi.Value
                        };

                        if (dsMaDT.Count>0)
                        {
                            // tạo buổi thi mới
                            QLTTN.BuoiThis.InsertOnSubmit(BT);

                            // thêm các thí sinh vào buổi thi mới này
                            foreach (var mahs in DSMaHSDuocChon)
                            {
                                QLTTN.BaiLams.InsertOnSubmit(new BaiLam
                                {
                                    maKT = KTCCN.maKT,
                                    maDT = dsMaDT[0],
                                    maHS = mahs,
                                    DiemSo = -1
                                });
                            }
                        }
                    }
                    else if (KTCCN.LoaiKT == "ThiThu")
                    {
                        foreach (var BT in KTCCN.BuoiThis)
                        {
                            QLTTN.BaiLams.DeleteAllOnSubmit(BT.BaiLams);
                            QLTTN.BuoiThis.DeleteOnSubmit(BT);
                            QLTTN.SubmitChanges();
                        }

                        foreach (var madt in dsMaDT)
                        {
                            BuoiThi BT = new BuoiThi()
                            {
                                maKT = KTCCN.maKT,
                                maDT = madt,
                                NgayGioThi = dtpNgayThi.Value
                            };
                            QLTTN.BuoiThis.InsertOnSubmit(BT);
                            QLTTN.SubmitChanges();

                            foreach (var hs in DSHS)
                            {
                                QLTTN.BaiLams.InsertOnSubmit(new BaiLam
                                {
                                    maHS = hs.maHS,
                                    maDT = BT.maDT,
                                    maKT = KTCCN.maKT,
                                    DiemSo = -1
                                });
                            }
                        }
                    }

                    QLTTN.SubmitChanges();
                    frmgv.LoadQLKTOTDgvKyThi();
                    MessageBox.Show("Cập nhật thành công", "Thông báo");
                    this.Close();

                }
                catch (Exception exc)
                {
                    MessageBox.Show($"KHÔNG THỂ CẬP NHẬT KỲ THI ĐÃ DIỄN RA {Environment.NewLine}{exc.Message}", "Thông báo từ btnCapNhatKT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            };
        }
        private void SetQLKT()
        {
            if (GV.maGV == KTCCN.maGV)
            {
                txtTenKT.ReadOnly = false;
            }
            else
            {
                txtTenKT.ReadOnly = true;
            }

            using (var QLTTN = new QLTTNDataContext())
            {
                var ktCanCapNhat = QLTTN.KyThis.Where(kt => kt.maKT == makt).FirstOrDefault();
                txtTenKT.Text = ktCanCapNhat.TenKT;
                var buoiThiCanCapNhat = ktCanCapNhat.BuoiThis.Where(BT => BT.DeThi.maMH == GV.maMH).FirstOrDefault();
                if (buoiThiCanCapNhat != null)
                {
                    dtpNgayThi.Value = (DateTime)buoiThiCanCapNhat.NgayGioThi;
                }
                lblTgBatDau.Text = $"Thời gian bắt đầu môn {QLTTN.MonHocs.Where(mh => mh.maMH == GV.maMH).FirstOrDefault().tenMH}: ";
            }

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
               // LoadQLKTDgvHocSinh();
            };
            dgvDT.CellContentClick += (s, e) =>
            {
                if (KTCCN.LoaiKT == "ThiThiet")
                {
                    CheckDGV(dgvDT, 0);
                }
            };
            dgvDT.CellValueChanged += (s, e) =>
            {
                var dgv = s as DataGridView;
                if (e.ColumnIndex == 0)
                {
                    var cell = dgv.Rows[e.RowIndex].Cells[0] as DataGridViewCheckBoxCell;
                    if (cell.Value == cell.TrueValue)
                    {
                        dgv.Rows[e.RowIndex].Selected = true;
                    }
                    else
                    {
                        dgv.Rows[e.RowIndex].Selected = false;
                    }
                }
            };
            dgvHS.CellValueChanged += (s, e) =>
            {
                var dgv = s as DataGridView;
                if (e.ColumnIndex == 0)
                {
                    gbTongSoThiSinh.Text = $"Tổng số thí sinh: {soDongDuocChon(dgv)}";
                    var cell = dgv.Rows[e.RowIndex].Cells[0] as DataGridViewCheckBoxCell;
                    if (cell.Value == cell.TrueValue)
                    {
                        dgv.Rows[e.RowIndex].Selected = true;
                    }
                    else
                    {
                        dgv.Rows[e.RowIndex].Selected = false;
                    }
                }
            };
        }

       
        /// <param name="dgv">bắt buộc dgv phải có cột có Name = "Chon"</param>
        
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
            DateTime thoiGianBatDau = dtpNgayThi.Value;
            TimeSpan thoiGianThi = TimeSpan.FromSeconds(0);

            var dtDuocChon = getDeThiDuocChon();
            if (dtDuocChon != null)
            {
                thoiGianThi = (TimeSpan)dtDuocChon.ThoiGianLamBai;
            }

            if (QLTTN.HocSinhs.Count() == 0)
            {
                MessageBox.Show("Không có dữ liệu học sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                if (BT != null)
                {
                    var dshsRanh = QLTTN.HocSinhs.Where(hs => hs.maKhoi == maKhoi && hs.BaiLams.Where(bl => bl.BuoiThi.NgayGioThi > thoiGianBatDau + thoiGianThi|| bl.BuoiThi.NgayGioThi + bl.BuoiThi.DeThi.ThoiGianLamBai < thoiGianBatDau).Count() == hs.BaiLams.Count && hs.BaiLams.Any(bl => BT.BaiLams.Contains(bl)) == false)
                    .Select(hs => new QLTTNDataContext.MyHocSinh
                    {
                        maHS = hs.maHS,
                        HoTen = hs.HoTen,
                        maKhoi = hs.maKhoi,
                        maLop = hs.maLop,
                        NgaySinh = (DateTime)hs.NgaySinh
                    }).ToList();
                    if (DSHS != null)
                    {
                        dshsRanh.InsertRange(0, DSHS);
                    }
                    dgvHS.DataSource = dshsRanh;
                }
                else
                {
                    var dshsRanh = QLTTN.HocSinhs.Where(hs => hs.maKhoi == maKhoi && hs.BaiLams.Where(bl => bl.BuoiThi.NgayGioThi > thoiGianBatDau + thoiGianThi|| bl.BuoiThi.NgayGioThi + bl.BuoiThi.DeThi.ThoiGianLamBai < thoiGianBatDau).Count() == hs.BaiLams.Count)
                    .Select(hs => new QLTTNDataContext.MyHocSinh
                    {
                        maHS = hs.maHS,
                        HoTen = hs.HoTen,
                        maKhoi = hs.maKhoi,
                        maLop = hs.maLop,
                        NgaySinh = (DateTime)hs.NgaySinh
                    }).ToList();
                    if (DSHS != null)
                    {
                        dshsRanh.InsertRange(0, DSHS);
                    }
                    dgvHS.DataSource = dshsRanh;

                }
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

                if (DSHS != null)
                {
                    var dsmahs = DSHS.Select(hs => hs.maHS);
                    foreach (DataGridViewRow row in dgvHS.Rows)
                    {
                        var mahs = row.Cells["maHS"].Value.ToString();
                        if (dsmahs.Contains(mahs))
                        {
                            var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                            cell.Value = cell.TrueValue;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show($"Hiện tất cả các học sinh của khối <{maKhoi}> đều đang bận thi" +
                                $"{Environment.NewLine}Vui lòng chọn thời gian khác để tạo buổi thi",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }
        private void LoadQLKTDgvDeThi()
        {
            using (var QLTTN = new QLTTNDataContext())
            {
                if (QLTTN.DeThis.Count() == 0)
                {
                    MessageBox.Show("Không có dữ liệu đề thi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                try
                {
                    BSLichThi.DataSource = QLTTN.DeThis.Where(DT => DT.maMH == GV.maMH && DT.maKhoi == maKhoi)
                    .Select(DT => new QLTTNDataContext.MyDeThi
                    {
                        maDT = DT.maDT,
                        TenDT = DT.TenDT,
                        maMH = DT.maMH,
                        maGV = DT.maGV,
                        TenMH = DT.MonHoc.tenMH,
                        maKhoi = DT.maKhoi,
                        ThoiGianLamBai = (TimeSpan)DT.ThoiGianLamBai
                    });

                    if (BSLichThi.Count > 0)
                    {
                        dgvDT.DataSource = BSLichThi;
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
                        MessageBox.Show("Không có đề thi", "Thông báo Quản lý kỳ thi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                    
                    if (KTCCN.LoaiKT == "ThiThiet")
                    {
                        if (DT != null)
                        {
                            foreach (DataGridViewRow row in dgvDT.Rows)
                            {
                                var madt = int.Parse(row.Cells["maDT"].Value.ToString());
                                if (madt == DT.maDT)
                                {
                                    var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                                    cell.Value = cell.TrueValue;
                                    break;
                                }
                            }
                        }
                    }
                    else if (KTCCN.LoaiKT == "ThiThu")
                    {
                        foreach (DataGridViewRow row in dgvDT.Rows)
                        {
                            var madt = int.Parse(row.Cells["maDT"].Value.ToString());
                            if (KTCCN.BuoiThis.Where(BT => BT.maDT == madt).Count() > 0)
                            {
                                var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                                cell.Value = cell.TrueValue;
                            }
                        }
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
            QLTTNDataContext.MyDeThi DT = null;
            foreach (DataGridViewRow row in dgvDT.Rows)
            {
                var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                if (cell.Value == cell.TrueValue)
                {
                    DT = new QLTTNDataContext.MyDeThi
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
            return DT;
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
