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
using System.Text.RegularExpressions;
using System.Threading;

namespace ThiTracNghiem
{
    public partial class frmGiaoVien : Form
    {
        private int CheckDS = 0;
        private frmLogin frmLogin = null;
        private GiaoVien GV = null;
        private List<string> lkl = null;
        private BindingSource BSCauHoi = new BindingSource();
        private BindingSource BSDapAn = new BindingSource();
        private BindingSource BSDethi = new BindingSource();
        private BindingSource BSLichThi = new BindingSource();
        private BindingSource BSHocSinh = new BindingSource();
        private BindingSource BSKyThi = new BindingSource();
        private BindingSource BSLichThiThu = new BindingSource();
        private BindingSource BSHocSinhThu = new BindingSource();
        private BindingSource BSKyThiThu = new BindingSource();                      
        private BindingSource BSQlhsHocSinh = new BindingSource();
        private BindingSource BSQlhsKyThi = new BindingSource();
        private BindingSource BSQlhsCauHoi = new BindingSource();
        QLTTNDataContext qlttn = new QLTTNDataContext();
        QLTTNDataContext QLTTN = new QLTTNDataContext();

        private void frmGiaoVien_Load(object sender, EventArgs e)
        {
            QLCHBtnThemCH.Click += QLCHBtnThemCH_Click;
            QLCHBtnXoaCH.Click += QLCHBtnXoaCH_Click;
            QLCHBtnSuaCH.Click += QLCHBtnSuaCH_Click;
            QLCHBtnThemDA.Click += QLCHBtnThemDA_Click;
            QLCHBtnXoaDA.Click += QLCHBtnXoaDA_Click;
            QLCHBtnSuaDA.Click += QLCHBtnSuaDA_Click;
            QLDTBtnThemDT.Click += QLDTBtnThemDT_Click;
            QLDTBtnSuaDT.Click += QLDTBtnSuaDT_Click;
            QLDTBtnRdCauHoi.Click += QLDTBtnRdCauHoi_Click;
            QLKTBtnThemKT.Click += QLKTBtnThemKT_Click;
            QLKTBtnPrintInfo.Click += QLKTBtnPrintInfo_Click;
            QLKTBtnSuaKT.Click += QLKTBtnSuaKT_Click;
            QLKTOTBtnThemKT.Click += QLKTOTBtnThemKT_Click_1;
            QLKTOTBtnSuaKT.Click += QLKTOTBtnSuaKT_Click;
            QLHSRbHS.Checked = true;
            QLHSRbHS.CheckedChanged += QLHSRbHS_CheckedChanged;
            QLHSRbKT.CheckedChanged += QLHSRbKT_CheckedChanged;
            QLHSRbCH.CheckedChanged += QLHSRbCH_CheckedChanged;
            QlchBtnImport.Click += QlchBtnImport_Click;
            QlchBtnExport.Click += QlchBtnExport_Click;

            loadThongTinGV();
        }

        private void QlchBtnExport_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog()
            {
                CreatePrompt = false,
                OverwritePrompt = true,
                AddExtension = true,
                Filter = "Excel Workbook|*.xlsx|Excel Workbook 97-2003|*.xls",
                ValidateNames = true,
            })
            {
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    DataTable dtCauHoi = new DataTable();
                    dtCauHoi.TableName = "CauHoi";
                    dtCauHoi.Columns.Add("maCH", typeof(int));
                    dtCauHoi.Columns.Add("NoiDung", typeof(string));
                    dtCauHoi.Columns.Add("maCD", typeof(int));
                    dtCauHoi.Columns.Add("maMH", typeof(string));
                    dtCauHoi.Columns.Add("maKhoi", typeof(string));

                    DataTable dtDapAn = new DataTable();
                    dtDapAn.TableName = "DapAn";
                    dtDapAn.Columns.Add("maCH", typeof(int));
                    dtDapAn.Columns.Add("maDA", typeof(int));
                    dtDapAn.Columns.Add("NoiDung", typeof(string));
                    dtDapAn.Columns.Add("DungSai", typeof(bool));

                    using (var qlttn = new QLTTNDataContext())
                    {
                        List<CauHoi> chs = qlttn.CauHois.Where(ch => ch.maMH == GV.maMH).ToList();
                        for (int i = 0; i < chs.Count; i++)
                        {
                            foreach (var da in chs[i].DapAns)
                            {
                                da.maCH = i + 1;
                                dtDapAn.Rows.Add(da.maCH, da.maDA, da.NoiDung, da.DungSai);
                            }

                            // thay đổi mã câu hỏi chỗ này để xuất ra theo thứ tự cho đẹp, khi import thì canh chỉnh lại, vì maCH
                            // có tính chất identity tự động tăng, khi insert trong db
                            chs[i].maCH = i + 1;

                            dtCauHoi.Rows.Add(chs[i].maCH, chs[i].NoiDung, chs[i].maCD, chs[i].maMH, chs[i].maKhoi);
                        }
                    };

                    XLWorkbook wb = new XLWorkbook();
                    wb.Worksheets.Add(dtCauHoi, dtCauHoi.TableName);
                    wb.Worksheets.Add(dtDapAn, dtDapAn.TableName);


                    wb.SaveAs(sfd.InitialDirectory + sfd.FileName);
                }
            }
        }

        private void QlchBtnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Excel Workbook|*.xlsx|Excel Workbook 97-2003|*.xls", ValidateNames = true })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    using (var stream = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        IExcelDataReader reader;
                        DataSet ds;
                        if (ofd.FilterIndex == 2)
                        {
                            reader = ExcelReaderFactory.CreateBinaryReader(stream);
                        }
                        else
                        {
                            reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        }

                        ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }
                        });
                        reader.Close();


                        List<CauHoi> cauHoiBiTrung = new List<CauHoi>();
                        List<CauHoi> cauHoiKhongDungChuyenMon = new List<CauHoi>();
                        int soCauHoiDuocThem = 0, soDapAnDuocThem = 0;
                        using (var qlttn = new QLTTNDataContext())
                        {
                            DataTable dtCauHoi = ds.Tables["CauHoi"];
                            DataTable dtDapAn = ds.Tables["DapAn"];
                            DataRow firstRow = dtCauHoi.Rows[0];

                            //int lastIdent = (int)qlttn.ExecuteQuery<decimal>("select IDENT_CURRENT('dbo.CauHoi')").FirstOrDefault();
                            try
                            {
                                foreach (DataRow row in dtCauHoi.Rows)
                                {
                                    CauHoi cauHoiTmp = new CauHoi()
                                    {
                                        NoiDung = row["NoiDung"].ToString(),
                                        maCD = int.Parse(row["maCD"].ToString()),
                                        maKhoi = row["maKhoi"].ToString(),
                                        maMH = row["maMH"].ToString()
                                    };
                                    // kiểm tra dòng đó đúng là chuyên môn của giáo viên đó (không phân biệt khối lớp)  thì mới cho vào
                                    if (cauHoiTmp.maMH == GV.maMH)
                                    {
                                        if (qlttn.CauHois.Where(ch => ch.NoiDung.ToLower() == cauHoiTmp.NoiDung.ToLower()).Count() == 0)
                                        {
                                            qlttn.CauHois.InsertOnSubmit(cauHoiTmp);
                                            qlttn.SubmitChanges();
                                            soCauHoiDuocThem++;
                                            foreach (DataRow rowDapAn in dtDapAn.Rows)
                                            {
                                                if (rowDapAn["maCH"].ToString() == row["maCH"].ToString())
                                                {
                                                    DapAn datmp = new DapAn()
                                                    {
                                                        NoiDung = rowDapAn["NoiDung"].ToString(),
                                                        maCH = qlttn.CauHois.Max(ch => ch.maCH),
                                                        DungSai = rowDapAn["DungSai"].ToString().ToLower() == "true" ? true : false
                                                    };
                                                    qlttn.DapAns.InsertOnSubmit(datmp);
                                                    soDapAnDuocThem++;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            cauHoiBiTrung.Add(cauHoiTmp);
                                        }
                                    }
                                    else
                                    {
                                        cauHoiKhongDungChuyenMon.Add(cauHoiTmp);
                                    }
                                }
                                qlttn.SubmitChanges();
                            }
                            catch (Exception err)
                            {
                                //MessageBox.Show(err.Message, "Đã xảy ra một lỗi nào đó trong hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Cursor = Cursors.Default;
                                return;
                            }

                        }
                        LoadCBCauHoi();
                        LoadQLCHDgvDapAn();
                        LoadQLDTDgvCauHoi();
                        LoadQLDTDgvDeThi();
                        Cursor = Cursors.Default;

                        if (cauHoiBiTrung.Count > 0)
                        {
                            string strCauHois = "";
                            for (int i = 0; i < cauHoiBiTrung.Count; i++)
                            {
                                string str = cauHoiBiTrung[i].NoiDung;
                                int maxLeng = 50;
                                if (str.Length > maxLeng)
                                {
                                    str = str.Replace(str.Substring(maxLeng, str.Length - maxLeng), " ...");
                                }
                                strCauHois += $"{Environment.NewLine} {i + 1}. {str}";
                            }
                            MessageBox.Show($">>>> DANH SÁCH NHỮNG CÂU HỎI BỊ TRÙNG <<<< {strCauHois}", "Không thể import", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        }
                        if (cauHoiKhongDungChuyenMon.Count > 0)
                        {
                            string strCauHois = "";
                            for (int i = 0; i < cauHoiKhongDungChuyenMon.Count; i++)
                            {
                                string str = cauHoiKhongDungChuyenMon[i].NoiDung;
                                int maxLeng = 50;
                                if (str.Length > maxLeng)
                                {
                                    str = str.Replace(str.Substring(maxLeng, str.Length - maxLeng), " ...");
                                }
                                strCauHois += $"{Environment.NewLine} {i + 1}. {str}";
                            }
                            MessageBox.Show($">>>> DANH SÁCH NHỮNG CÂU HỎI KHÔNG ĐÚNG CHUYÊN MÔN <<<< {strCauHois}", "Không thể import", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }

                        MessageBox.Show($">>>> KẾT QUẢ IMPORT DANH SÁCH CÂU HỎI <<<< {Environment.NewLine} Tổng số câu hỏi được thêm: {soCauHoiDuocThem} {Environment.NewLine} Tổng số đáp án được thêm: {soDapAnDuocThem}", "Thông báo");
                    }
                }
            }
        }

        private void LoadCBKhoiLop()
        {
           
            
            lkl = GV.CT_GiangDays.Select(ctgd => ctgd.maKhoi).Distinct().ToList();
            cbKhoiLop.DataSource = lkl;
            QLDTLblNguoiTao.Text = GV.HoTen;
            QLDTLblNgayTao.Text = DateTime.UtcNow.ToString("dd-MM-yyyy");
            if (cbKhoiLop.Items.Count != 0)
            {
                cbKhoiLop.SelectedItem = cbKhoiLop.Items[0];
                string txt = cbKhoiLop.SelectedValue.ToString();
            }
            txtFullname.Text = GV.HoTen;
            txtNgaySinh.Text = $"{GV.NgaySinh.Value.ToShortDateString()}";
            lblSubject.Text = $"{QLTTN.MonHocs.Where(mh => mh.maMH == GV.maMH).Single().tenMH}";          
        }
        
        private void LoadCBCauHoi()
        {
            var QLTTN = new QLTTNDataContext();
            
            try
            {
                var cauHoi = (QLTTN.CauHois.Where(ch => ch.maKhoi == cbKhoiLop.SelectedValue.ToString() && ch.maMH == GV.maMH)
                                                .Select(ch => new { ch.maCH, ch.NoiDung, ch.CapDo.TenCD, ch.maCD }).ToList());
                BSCauHoi.DataSource = cauHoi;
            }
            catch (Exception e)
            {
                return;
            }

            if (BSCauHoi.Count > 0)
            {
                QLCHCbDSCH.DataSource = BSCauHoi;
                QLCHCbDSCH.DisplayMember = "NoiDung";
                QLCHCbDSCH.ValueMember = "maCH";

                QLCHCapDo.DataSource = QLTTN.CapDos.ToList();
                QLCHCapDo.DisplayMember = "TenCD";
                QLCHCapDo.ValueMember = "maCD";

                var macd = QLTTN.CauHois.Where(ch => ch.maCH.ToString() == QLCHCbDSCH.SelectedValue.ToString()).Single().maCD;
                QLCHCapDo.SelectedValue = macd;

                if (QLCHTxtCauHoi.DataBindings.Count == 0)
                {
                    QLCHTxtCauHoi.DataBindings.Add("Text", BSCauHoi, "NoiDung", true, DataSourceUpdateMode.Never, "Null value");
                }
                if (QLCHCapDo.DataBindings.Count == 0)
                {
                    QLCHCapDo.DataBindings.Add("SelectedValue", BSCauHoi, "maCD", true, DataSourceUpdateMode.Never, "Null value");
                    QLCHCapDo.DataBindings[0].Format += (s, e) =>
                    {
                        if (e.DesiredType == typeof(string))
                        {
                            int maCapDo = int.Parse(e.Value.ToString());
                            e.Value = (QLCHCapDo.DataSource as List<CapDo>).Where(cd => cd.maCD == maCapDo).FirstOrDefault().TenCD;
                        }
                    };
                    QLCHCapDo.DataBindings[0].Parse += (s, e) =>
                    {
                        if (e.DesiredType == typeof(int))
                        {
                            string noiDungCapDo = e.Value.ToString();
                            e.Value = (QLCHCapDo.DataSource as List<CapDo>).Where(cd => cd.TenCD == noiDungCapDo).FirstOrDefault().maCD;
                        }
                    };
                }
            }
            else
            {
                QLCHCapDo.DataBindings.Clear();
                QLCHTxtCauHoi.DataBindings.Clear();
                QLCHCbDSCH.DataSource = null;
            }
            
        }
        private void LoadQLCHDgvDapAn()
        {
            using (var QLTTN = new QLTTNDataContext())
            {

               if (QLCHCbDSCH.Items.Count > 0)
               {
                   try
                   {
                       BSDapAn.DataSource = QLTTN.CauHois.Where(ch => ch.maCH == int.Parse(QLCHCbDSCH.SelectedValue.ToString())).SingleOrDefault().DapAns.ToList();
                       QLCHDgvDSDA.DataSource = BSDapAn;
                   }
                   catch (Exception ee)
                   {
                       return;
                   }

                    //QLCHDgvDSDA.Columns["maCH"].Visible = false;
                    //QLCHDgvDSDA.Columns["CauHoi"].Visible = false;
                    //QLCHDgvDSDA.Columns["maDA"].Visible = false;
                   QLCHDgvDSDA.AutoGenerateColumns = true;
                  
                   QLCHDgvDSDA.Columns["NoiDung"].DisplayIndex = 1;
                   QLCHDgvDSDA.Columns["NoiDung"].HeaderText = "Nội dung đáp án";
                   QLCHDgvDSDA.Columns["DungSai"].DisplayIndex = 2;
                   QLCHDgvDSDA.Columns["DungSai"].HeaderText = "Đúng Sai";
                  
                    if (QLCHTxtDapAn.DataBindings.Count == 0)
                    {
                        QLCHTxtDapAn.DataBindings.Add("Text", BSDapAn, "NoiDung", true, DataSourceUpdateMode.Never, "Null value");
                    }
                    if (QLCHCkbDungSai.DataBindings.Count == 0)
                    {
                        QLCHCkbDungSai.DataBindings.Add("Checked", BSDapAn, "DungSai", true, DataSourceUpdateMode.Never, false);
                    }
                }
               else
               {
                   QLCHTxtDapAn.DataBindings.Clear();
                   QLCHCkbDungSai.DataBindings.Clear();
                   QLCHDgvDSDA.DataSource = null;
               }

            
                
            }
        }
        private void SetQLCH()
        {
            QLCHTxtCauHoi.Validating += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(QLCHTxtCauHoi.Text))
                {
                    e.Cancel = true;
                    QLCHTxtCauHoi.Focus();
                    errorProvider.SetError(QLCHTxtCauHoi, "Không được để trống câu hỏi");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider.SetError(QLCHTxtCauHoi, "");
                }
            };
            QLCHTxtDapAn.Validating += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(QLCHTxtDapAn.Text))
                {
                    e.Cancel = true;
                    QLCHTxtCauHoi.Focus();
                    errorProvider.SetError(QLCHTxtDapAn, "Không được để trống câu hỏi");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider.SetError(QLCHTxtDapAn, "");
                }
            };
            QLCHCbDSCH.SelectedIndexChanged += (s, e) =>
             {
                 LoadQLCHDgvDapAn();

                 if (QLCHCbDSCH.SelectedValue == null)
                 {
                     return;
                 }
                 var QLTTN = new QLTTNDataContext();
                 
                 if (QLTTN.DeThis.Count() == 0)
                 {
                     return;
                 }
                 QLCHCbDSCH.ValueMember = "maCH";
                 
             };
            
        }

        private void LoadQLDTDgvDeThi()
        {
            var QLTTN = new QLTTNDataContext();
            
            try
            {
                var sourceDt = QLTTN.DeThis.Where(dt => dt.maMH == GV.maMH && dt.maKhoi == cbKhoiLop.SelectedValue.ToString())
                                            .Select(dt => new { dt.maDT, dt.TenDT, dt.GiaoVien.HoTen, dt.ThoiGianLamBai, dt.NgayTao })
                                            .ToList();
                BSDethi.DataSource = sourceDt;
            }
            catch (Exception e)
            {
                return;
            }
            if (BSDethi.Count > 0)
            {
                QLDTDgvDeThi.DataSource = BSDethi;

                QLDTDgvDeThi.Columns["maDT"].HeaderText = "Mã";
                QLDTDgvDeThi.Columns["TenDT"].HeaderText = "Tên đề thi";
                QLDTDgvDeThi.Columns["HoTen"].HeaderText = "Giáo viên ra đề";
                QLDTDgvDeThi.Columns["ThoiGianLamBai"].HeaderText = "Thời gian làm bài";
                QLDTDgvDeThi.Columns["NgayTao"].HeaderText = "Ngày tạo";

                if (qldtTxtTenDT.DataBindings.Count == 0)
                {
                    qldtTxtTenDT.DataBindings.Add("Text", BSDethi, "TenDT", true, DataSourceUpdateMode.Never, "null");
                }
                if (QLDTLblNgayTao.DataBindings.Count == 0)
                {
                    QLDTLblNgayTao.DataBindings.Add("Text", BSDethi, "NgayTao", true, DataSourceUpdateMode.Never, 0);
                }
                if (QLDTLblNguoiTao.DataBindings.Count == 0)
                {
                    QLDTLblNguoiTao.DataBindings.Add("Text", BSDethi, "HoTen", true, DataSourceUpdateMode.Never, 0);
                }
                if (qldtTxtThoiGianLamBai.DataBindings.Count == 0)
                {
                    var bd = new Binding("Text", BSDethi, "ThoiGianLamBai", true, DataSourceUpdateMode.Never, 0);
                    bd.Format += (s, e) =>
                    {
                        if (e.DesiredType == typeof(string))
                        {
                            TimeSpan soPhut = TimeSpan.Parse(e.Value.ToString());
                            e.Value = soPhut.TotalMinutes.ToString();
                        }
                    };
                    qldtTxtThoiGianLamBai.DataBindings.Add(bd);
                }
            }
            else
            {
                qldtTxtTenDT.DataBindings.Clear();
                QLDTLblNguoiTao.DataBindings.Clear();
                QLDTLblNgayTao.DataBindings.Clear();
                qldtTxtThoiGianLamBai.DataBindings.Clear();
                QLDTDgvDeThi.DataSource = null;
            }
            
        }
        private void LoadQLDTDgvCauHoi()
        {
            if (BSCauHoi.Count > 0)
            {
                QLDTDgvCauHoi.DataSource = BSCauHoi;
                QLDTDgvCauHoi.Columns["maCH"].HeaderText = "Mã";
                QLDTDgvCauHoi.Columns["NoiDung"].HeaderText = "Nội dung";
                QLDTDgvCauHoi.Columns["TenCD"].HeaderText = "Cấp độ";
                QLDTDgvCauHoi.Columns["maCD"].Visible = false;
                QLDTDgvCauHoi.AllowUserToOrderColumns = true;
            }
            else
            {
                QLDTDgvCauHoi.DataSource = null;
            }
        }
        private void SetQLDT()
        {
            QLDTDgvCauHoi.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                Name = "Chon",
                HeaderText = "Chọn câu hỏi",
                TrueValue = true,    
                FalseValue = false,
                IndeterminateValue = false
            });
            QLDTDgvDeThi.Columns.Add(new DataGridViewButtonColumn()
            {
                Name = "Xoa",
                HeaderText = "Xóa đề thi",
            });

            qldtTxtThoiGianLamBai.Text = "10";

            qldtTxtThoiGianLamBai.KeyDown += (s, e) =>
             {
                 if (e.KeyValue >= 48 && e.KeyValue <= 57 ||
                    e.KeyValue >= 96 && e.KeyValue <= 105 ||
                    e.KeyCode == Keys.Back ||
                    e.KeyCode == Keys.Delete ||
                    e.KeyCode == Keys.Left ||
                    e.KeyCode == Keys.Right)
                 {
                 }
                 else if (e.KeyCode == Keys.Up)
                 {
                     int soPhut = int.Parse(qldtTxtThoiGianLamBai.Text);
                     soPhut += 5;
                     qldtTxtThoiGianLamBai.Text = soPhut.ToString();
                 }
                 else if (e.KeyCode == Keys.Down)
                 {
                     int soPhut = int.Parse(qldtTxtThoiGianLamBai.Text);
                     soPhut -= 5;
                     qldtTxtThoiGianLamBai.Text = soPhut.ToString();
                 }
                 else
                 {
                     e.SuppressKeyPress = true;
                 }
             };
            qldtTxtThoiGianLamBai.KeyUp += (s, e) =>
             {
                 if (string.IsNullOrWhiteSpace(qldtTxtThoiGianLamBai.Text))
                 {
                     qldtTxtThoiGianLamBai.Text = "5";
                 }
                 int soPhut = int.Parse(qldtTxtThoiGianLamBai.Text);
                 if (soPhut < 0)
                 {
                     qldtTxtThoiGianLamBai.Text = "5";
                 }
                 else if (soPhut > 180)
                 {
                     qldtTxtThoiGianLamBai.Text = "180";
                 }
             };
            qldtTxtTenDT.Validating += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(qldtTxtTenDT.Text))
                {
                    e.Cancel = true;
                    qldtTxtTenDT.Focus();
                    errorProvider.SetError(qldtTxtTenDT, "Không được để trống tên đề thi");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider.SetError(qldtTxtTenDT, "");
                }
            };
           
            QLDTDgvDeThi.CellPainting += (s, e) =>
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (e.ColumnIndex == 0)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                    var w = e.CellBounds.Height - 4;
                    var h = e.CellBounds.Height - 4;
                    var x = e.CellBounds.X + (e.CellBounds.Width - w) / 2;
                    var y = e.CellBounds.Y + 2;
                    e.Graphics.DrawImage(Properties.Resources.delete__1_, x, y, w, h);
                    e.Handled = true;
                }
            };
        }


        private void setQlkt()
        {
            QLKTDgvKT.Columns.Add(new DataGridViewButtonColumn()
            {
                Name = "Xoa",
                Width = 80,
                HeaderText = "Xóa kỳ thi"
            });

            QLKTDgvKT.CellPainting += (s, e) =>
             {
                 if (e.RowIndex < 0)
                 {
                     return;
                 }
                 if (e.ColumnIndex == 0)
                 {
                     e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                     var w = e.CellBounds.Height - 4;
                     var h = e.CellBounds.Height - 4;
                     var x = e.CellBounds.X + (e.CellBounds.Width - w) / 2;
                     var y = e.CellBounds.Y + 2;
                     e.Graphics.DrawImage(Properties.Resources.delete__1_, x, y, w, h);
                     e.Handled = true;
                 }
             };
          
        }
        private void LoadQLKTDgvHocSinh()
        {
            var QLTTN = new QLTTNDataContext();
            
            if (QLTTN.HocSinhs.Count() == 0)
            {
                return;
            }
            if (QLKTDgvKT.SelectedRows.Count > 0 && QLKTDgvDT.SelectedRows.Count > 0)
            {
                int makt = int.Parse(QLKTDgvKT.SelectedRows[0].Cells["maKT"].Value.ToString());
                int madt = int.Parse(QLKTDgvDT.SelectedRows[0].Cells["maDT"].Value.ToString());
                try
                {
                    var dshs = QLTTN.BaiLams.Where(bl => bl.maKT == makt && bl.maDT == madt).Select(bl => bl.HocSinh);
                    QLKTDgvHS.DataSource = dshs.Select(hs => new { hs.maHS, hs.HoTen, hs.maKhoi, hs.maLop, hs.NgaySinh }).ToList();

                    if (dshs.Count() > 0)
                    {

                        QLKTDgvHS.Columns["maKhoi"].Visible = false;

                        QLKTDgvHS.Columns["maHS"].HeaderText = "Mã học sinh";
                        QLKTDgvHS.Columns["HoTen"].HeaderText = "Họ tên";
                        QLKTDgvHS.Columns["maLop"].HeaderText = "Lớp học";
                        QLKTDgvHS.Columns["NgaySinh"].HeaderText = "Ngày sinh";

                        qlktGbTongSoThiSinh.Text = $"Tổng số thí sinh tham gia: {QLKTDgvHS.RowCount}";
                    }
                    else
                    {
                        QLKTDgvHS.DataSource = null;
                    }
                }
                catch (Exception e)
                {
                    return;
                }
            }
            else
            {
                QLKTDgvHS.DataSource = null;
            }
            
        }
        private void LoadQLKTDgvDeThi()
        {
            var QLTTN = new QLTTNDataContext();
            
            if (QLTTN.DeThis.Count() == 0)
            {
                return;
            }
            try
            {
                if (QLKTDgvKT.SelectedRows.Count > 0)
                {
                    int MaKT = int.Parse(QLKTDgvKT.SelectedRows[0].Cells["maKT"].Value.ToString());

                    BSLichThi.DataSource = QLTTN.BuoiThis.Where(bt => bt.maKT == MaKT)
                                                        .Select(bt => new { bt.DeThi.maDT, bt.DeThi.TenDT, bt.DeThi.maMH, bt.DeThi.MonHoc.tenMH, bt.NgayGioThi, bt.DeThi.ThoiGianLamBai });
                    if (BSLichThi.Count > 0)
                    {
                        QLKTDgvDT.DataSource = BSLichThi;
                        QLKTDgvDT.Columns["maDT"].HeaderText = "Mã đề";
                        QLKTDgvDT.Columns["maMH"].Visible = false;
                        QLKTDgvDT.Columns["TenDT"].HeaderText = "Tên đề thi";
                        QLKTDgvDT.Columns["tenMH"].HeaderText = "Môn thi";
                        QLKTDgvDT.Columns["ThoiGianLamBai"].HeaderText = "Thời gian làm bài";
                        QLKTDgvDT.Columns["NgayGioThi"].HeaderText = "Ngày giờ thi";

                        qlktGbTongSoDeThi.Text = $"Tổng số buổi thi (1buổi/1môn): {QLKTDgvDT.RowCount}";
                    }
                    else
                    {
                        QLKTDgvDT.DataSource = null;
                    }
                }
                else
                {
                    QLKTDgvDT.DataSource = null;
                }
            }
            catch (Exception e)
            {
                return;
            }
            
        }
        public void LoadQLKTDgvKyThi()
        {
            using (var QLTTN = new QLTTNDataContext())
            {
                if (QLTTN.KyThis.Count() == 0)
                {
                    return;
                }
                try
                {
                    string makhoi = cbKhoiLop.SelectedValue.ToString();
                    BSKyThi.DataSource = QLTTN.KyThis.Where(kt => kt.maKhoi == makhoi && kt.LoaiKT == "ThiThiet")
                        .Select(kt => new { kt.maKT, kt.TenKT, kt.LoaiKT }).ToList();
                }
                catch (Exception e)
                {
                    return;
                }
                if (BSKyThi.Count > 0)
                {
                    QLKTDgvKT.DataSource = BSKyThi;
                    QLKTDgvKT.Columns["maKT"].HeaderText = "Mã";
                    QLKTDgvKT.Columns["TenKT"].HeaderText = "Tên";
                    QLKTDgvKT.Columns["LoaiKT"].HeaderText = "Phân loại";
                }
                else
                {
                    QLKTDgvKT.DataSource = null;
                }
            }
        }


        private void SetQLKTOT()
        {
            QLKTOTDgvKT.Columns.Add(new DataGridViewButtonColumn()
            {
                Name = "Xoa",
                Width = 80,
                HeaderText = "Xóa kỳ thi"
            });

            QLKTOTDgvKT.CellPainting += (s, e) =>
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (e.ColumnIndex == 0)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                    var w = e.CellBounds.Height - 4;
                    var h = e.CellBounds.Height - 4;
                    var x = e.CellBounds.X + (e.CellBounds.Width - w) / 2;
                    var y = e.CellBounds.Y + 2;
                    e.Graphics.DrawImage(Properties.Resources.delete__1_, x, y, w, h);
                    e.Handled = true;
                }
            };
        }
        private void LoadQLKTOTDgvHocSinh()
        {
            using (var QLTTN = new QLTTNDataContext())
            {
                if (QLTTN.HocSinhs.Count() == 0)
                {
                    return;
                }
                if (QLKTOTDgvKT.SelectedRows.Count > 0 && QLKTOTDgvDT.SelectedRows.Count > 0)
                {
                    int MaKT = int.Parse(QLKTOTDgvKT.SelectedRows[0].Cells["maKT"].Value.ToString());
                    int MaDT = int.Parse(QLKTOTDgvDT.SelectedRows[0].Cells["maDT"].Value.ToString());
                    try
                    {
                        var dshs = QLTTN.BaiLams.Where(bl => bl.maKT == MaKT && bl.maDT == MaDT).Select(bl => bl.HocSinh);
                        QLKTOTDgvHS.DataSource = dshs.Select(hs => new { hs.maHS, hs.HoTen, hs.maKhoi, hs.maLop, hs.NgaySinh }).ToList();

                        if (dshs.Count() > 0)
                        {

                            QLKTOTDgvHS.Columns["maKhoi"].Visible = false;

                            QLKTOTDgvHS.Columns["maHS"].HeaderText = "Mã học sinh";
                            QLKTOTDgvHS.Columns["HoTen"].HeaderText = "Họ tên";
                            QLKTOTDgvHS.Columns["maLop"].HeaderText = "Lớp học";
                            QLKTOTDgvHS.Columns["NgaySinh"].HeaderText = "Ngày sinh";

                            qlktGbTongSoThiSinh.Text = $"Tổng số thí sinh tham gia: {QLKTOTDgvHS.RowCount}";
                        }
                        else
                        {
                            QLKTOTDgvHS.DataSource = null;
                        }
                    }
                    catch (Exception e)
                    {
                        return;
                    }
                }
                else
                {
                    QLKTOTDgvHS.DataSource = null;
                }
            }
        }
        private void LoadQLKTOTDgvDeThi()
        {
            var QLTTN = new QLTTNDataContext();
            
            if (QLTTN.DeThis.Count() == 0)
            {
                return;
            }
            try
            {
                if (QLKTOTDgvKT.SelectedRows.Count > 0)
                {
                    int MaKT = int.Parse(QLKTOTDgvKT.SelectedRows[0].Cells["maKT"].Value.ToString());

                    BSLichThiThu.DataSource = QLTTN.BuoiThis.Where(bt => bt.maKT == MaKT).Select(bt => new { bt.DeThi.maDT, bt.DeThi.TenDT, bt.DeThi.maMH, bt.DeThi.MonHoc.tenMH, bt.NgayGioThi, bt.DeThi.ThoiGianLamBai });
                    if (BSLichThiThu.Count > 0)
                    {
                        QLKTOTDgvDT.DataSource = BSLichThiThu;
                        QLKTOTDgvDT.Columns["maDT"].HeaderText = "Mã đề";
                        QLKTOTDgvDT.Columns["maMH"].Visible = false;
                        QLKTOTDgvDT.Columns["TenDT"].HeaderText = "Tên đề thi";
                        QLKTOTDgvDT.Columns["tenMH"].HeaderText = "Môn thi";
                        QLKTOTDgvDT.Columns["ThoiGianLamBai"].HeaderText = "Thời gian làm bài";
                        QLKTOTDgvDT.Columns["NgayGioThi"].HeaderText = "Ngày giờ thi";

                        qlktGbTongSoDeThi.Text = $"Tổng số đề thi được chọn: {QLKTOTDgvDT.RowCount}";
                    }
                    else
                    {
                        QLKTOTDgvDT.DataSource = null;
                    }
                }
                else
                {
                    QLKTOTDgvDT.DataSource = null;
                }
            }
            catch (Exception e)
            {
                return;
            }
            
        }
        public void LoadQLKTOTDgvKyThi()
        {
            var QLTTN = new QLTTNDataContext();
            
            if (QLTTN.KyThis.Count() == 0)
            {
                return;
            }
            try
            {
                string makhoi = cbKhoiLop.SelectedValue.ToString();
                BSKyThiThu.DataSource = QLTTN.KyThis.Where(kt => kt.maKhoi == makhoi && kt.LoaiKT == "ThiThu")
                    .Select(kt => new { kt.maKT, kt.TenKT, kt.LoaiKT }).ToList();
            }
            catch (Exception e)
            {
                return;
            }
            if (BSKyThiThu.Count > 0)
            {
                QLKTOTDgvKT.DataSource = BSKyThiThu;
                QLKTOTDgvKT.Columns["maKT"].HeaderText = "Mã";
                QLKTOTDgvKT.Columns["TenKT"].HeaderText = "Tên";
                QLKTOTDgvKT.Columns["LoaiKT"].HeaderText = "Phân loại";
            }
            else
            {
                QLKTOTDgvKT.DataSource = null;
            }
            
        }

        private void LoadQlhsDgvHocSinh()
        {
            string makhoi = cbKhoiLop.SelectedValue.ToString();
            var QLTTN = new QLTTNDataContext();
            
            BSQlhsHocSinh.DataSource = QLTTN.HocSinhs.Where(hs => hs.maKhoi == makhoi).Select(hs => new
            {
                hs.maHS,
                hs.HoTen,
                hs.maKhoi,
                hs.maLop,
                hs.NgaySinh,
                SoKTDaThamGia = QLTTN.KyThis.Where(kt => kt.BuoiThis
                                            .Where(bt => bt.BaiLams
                                            .Where(bl => bl.maHS == hs.maHS).Count() > 0).Count() > 0).Count(),
                SoDTDaLam = hs.BaiLams.Count,
                DTB = hs.BaiLams.Average(bl => bl.DiemSo)
            }).ToList();

            if (BSQlhsHocSinh.Count > 0)
            {
                QLHSDgvDS.DataSource = BSQlhsHocSinh;

                QLHSDgvDS.Columns["maKhoi"].Visible = false;

                QLHSDgvDS.Columns["maHS"].HeaderText = "Mã học sinh";
                QLHSDgvDS.Columns["HoTen"].HeaderText = "Họ tên";
                QLHSDgvDS.Columns["maLop"].HeaderText = "Lớp học";
                QLHSDgvDS.Columns["NgaySinh"].HeaderText = "Ngày sinh";
                QLHSDgvDS.Columns["SoKTDaThamGia"].HeaderText = "Tổng số kỳ thi đã tham gia";
                QLHSDgvDS.Columns["SoDTDaLam"].HeaderText = "Tổng số đề thi đã làm";
                QLHSDgvDS.Columns["DTB"].HeaderText = "Điểm trung bình";
            }
            else
            {
                QLHSDgvDS.DataSource = null;
            }
            
        }
        private void LoadQLHSDgvKyThi()
        {
            string makhoi = cbKhoiLop.SelectedValue.ToString();
            var QLTTN = new QLTTNDataContext();
            
            BSQlhsKyThi.DataSource = QLTTN.KyThis.Where(kt => kt.maKhoi == makhoi).Select(kt => new
            {
                kt.maKT,
                kt.TenKT,
                kt.LoaiKT,
                TongSoMonThi = kt.BuoiThis.Count,
                TongSoThiSinh = kt.BuoiThis.Sum(bt => bt.BaiLams.Count),
                DTB = kt.BuoiThis.Average(bt => bt.BaiLams.Average(bl => bl.DiemSo))
            });

            if (BSQlhsKyThi.Count > 0)
            {
                QLHSDgvDS.DataSource = BSQlhsKyThi;


                QLHSDgvDS.Columns["maKT"].HeaderText = "Mã kỳ thi";
                QLHSDgvDS.Columns["TenKT"].HeaderText = "Tên kỳ thi";
                QLHSDgvDS.Columns["LoaiKT"].HeaderText = "Loại kỳ thi";
                QLHSDgvDS.Columns["TongSoMonThi"].HeaderText = "Tổng số môn";
                QLHSDgvDS.Columns["TongSoThiSinh"].HeaderText = "Tổng số thí sinh";
                QLHSDgvDS.Columns["DTB"].HeaderText = "Phổ điểm trung bình";
            }
            else
            {
                QLHSDgvDS.DataSource = null;
            }
            
        }
        private void LoadQLHSDgvCauHoi()
        {
            string makhoi = cbKhoiLop.SelectedValue.ToString();
            var QLTTN = new QLTTNDataContext();
            
            double tongSoDeThi = QLTTN.DeThis.Where(dt => dt.maKhoi == makhoi && dt.maMH == GV.maMH).Count();
            BSQlhsCauHoi.DataSource = QLTTN.CauHois.Where(ch => ch.maKhoi == makhoi && ch.maMH == GV.maMH).Select(ch => new
            {
                ch.maCH,
                ch.NoiDung,
                ch.CapDo.TenCD,
           
            });
            if (BSQlhsCauHoi.Count > 0)
            {
                QLHSDgvDS.DataSource = BSQlhsCauHoi;

                QLHSDgvDS.Columns["maCH"].HeaderText = "Mã câu hỏi";
                QLHSDgvDS.Columns["NoiDung"].HeaderText = "Nội dung";
                QLHSDgvDS.Columns["TenCD"].HeaderText = "Cấp độ";
                
            }
            else
            {
                QLHSDgvDS.DataSource = null;
            }
            
        }

        private bool DaTonTai(DeThi dethiHientai)
        {
            var QLTTN = new QLTTNDataContext();
            

            if (QLTTN.DeThis.Where(dt => dt.TenDT == dethiHientai.TenDT).Count() > 0)
            {
                MessageBox.Show("Tên đề Đã Tồn Tại", "Trùng dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }

            List<int> lmach = new List<int>();
            foreach (DataGridViewRow row in QLDTDgvCauHoi.Rows)
            {
                var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                if (cell.Value == cell.TrueValue)
                {
                    lmach.Add(int.Parse(row.Cells["maCH"].Value.ToString()));
                }
            }
            var DSDeThi = QLTTN.CT_DeThis.GroupBy(ctdt => ctdt.maDT).ToList();
            foreach (var dethi in DSDeThi)
            {
                int SoCauTimThay = 0;
                foreach (var CH in lmach)
                {
                    if (dethi.Where(ctdt => ctdt.maCH == CH).Count() > 0)
                    {
                        SoCauTimThay++;
                    }
                }
                if (SoCauTimThay == dethi.Count())
                {
                    string line = "";
                    for (int i = 0; i < dethi.Count(); i++)
                    {
                        line += $"{ Environment.NewLine } {dethi.ElementAt(i).maCH}. {dethi.ElementAt(i).CauHoi.NoiDung}";
                    }
                    MessageBox.Show($"Đã Tồn Tại tên Để Thi Này: <{dethi.ElementAt(0).DeThi.TenDT}> {line}", "Trùng dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }
            }
            
            return false;
        }

        private Dictionary<TabPage, Color> TabColors = new Dictionary<TabPage, Color>();
        private void SetTabHeader(TabPage page, Color color)
        {
            TabColors[page] = color;
            tabControl1.Invalidate();
        }
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            
            if (e.State == DrawItemState.Selected)
            {
                Brush br = new SolidBrush(Color.LemonChiffon);
                
                e.Graphics.FillRectangle(br, e.Bounds);
                SizeF sz = e.Graphics.MeasureString(tabControl1.TabPages[e.Index].Text, e.Font);
                e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2, e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + 1);

                Rectangle rect = e.Bounds;
                rect.Offset(0, 1);
                rect.Inflate(0, -1);
                e.Graphics.DrawRectangle(Pens.DarkGray, rect);
                e.DrawFocusRectangle();
                
            }
            else
            {
                Brush br = new SolidBrush(TabColors[tabControl1.TabPages[e.Index]]);
                
                e.Graphics.FillRectangle(br, e.Bounds);
                SizeF sz = e.Graphics.MeasureString(tabControl1.TabPages[e.Index].Text, e.Font);
                e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2, e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + 1);

                Rectangle rect = e.Bounds;
                
                e.Graphics.DrawRectangle(Pens.DarkGray, rect);
                e.DrawFocusRectangle();
                
            }
        }

        private int soDongDuocChon(DataGridView dgv)
        {
            int result = 0;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                if (cell.Value == cell.TrueValue)
                {
                    result++;
                }
            }
            return result;
        }

       
        /// <param name="dgv">Datagridview muốn check cần phải có cột DataGridViewCheckBoxColumn.Name = "Chon"</param>
        /// <param name="soDongMuonCheck">Số dòng cần được check, phải nhỏ hơn số dòng trong dgv, nếu lớn hơn thì sẽ chọn hết</param>
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
        public frmGiaoVien(frmLogin frmlogin, GiaoVien GV)
        {
            Thread t = new Thread(new ThreadStart(() =>
            {
                Application.Run(new frmSplashScreen());
            }));
            t.Start();
            this.GV = GV;
            this.frmLogin = frmlogin;
            InitializeComponent();

            tabControl1.SelectedIndex = 2;
            LoadCBKhoiLop();
            LoadCBCauHoi();
            LoadQLCHDgvDapAn();
            SetQLCH();

            SetQLDT();
            LoadQLDTDgvDeThi();
            LoadQLDTDgvCauHoi();

            setQlkt();
            LoadQLKTDgvHocSinh();
            LoadQLKTDgvDeThi();
            LoadQLKTDgvKyThi();

            SetQLKTOT();
            LoadQLKTOTDgvKyThi();
            LoadQLKTOTDgvDeThi();
            LoadQLKTOTDgvHocSinh();


           foreach (TabPage tp in tabControl1.TabPages)
            {
               tp.BackColor = Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(24)))), ((int)(((byte)(74)))));
            }
            this.FormClosing += (s, e) =>
            {
                this.frmLogin.Show();
            };

            this.btnDangXuat.Click += (s, e) =>
            {
                frmlogin.Show();
                this.Close();
            };

            this.tabControl1.SelectedIndexChanged += (s, e) =>
             {
                 if (tabControl1.SelectedIndex == 2)
                 {
                     LoadQLKTDgvHocSinh();
                     LoadQLKTDgvDeThi();
                     LoadQLKTDgvKyThi();
                 }
                 else if (tabControl1.SelectedIndex == 3)
                 {
                     LoadQLKTOTDgvKyThi();
                     LoadQLKTOTDgvDeThi();
                     LoadQLKTOTDgvHocSinh();
                 }
             };

            
            this.cbKhoiLop.SelectedIndexChanged += (s, e) =>
             {
                 LoadCBCauHoi();
                 LoadQLCHDgvDapAn();
                 LoadQLDTDgvCauHoi();
                 LoadQLDTDgvDeThi();
                 LoadQLKTDgvDeThi();
                 LoadQLKTDgvHocSinh();
                 LoadQLKTDgvKyThi();
                 LoadQLKTOTDgvHocSinh();
                 LoadQLKTOTDgvDeThi();
                 LoadQLKTOTDgvKyThi();
             };

            QLHSRbCH.CheckedChanged += (s, e) =>
            {
                if (QLHSRbCH.Checked == true)
                {
                    QLHSDgvDS.DataSource = null;
                    LoadQLHSDgvCauHoi();
                }
            };

           
            QLKTOTDgvKT.CellContentClick += (s, e) =>
            {
                if (e.ColumnIndex == 0)
                {
                    var makt = int.Parse(QLKTOTDgvKT.Rows[e.RowIndex].Cells["maKT"].Value.ToString());
                    using (var QLTTN = new QLTTNDataContext())
                    {
                        KyThi ktDuocChon = QLTTN.KyThis.Where(kt => kt.maKT == makt).FirstOrDefault();

                        if (ktDuocChon.BuoiThis.Where(buoithi => buoithi.NgayGioThi < DateTime.Now).Count() > 0)
                        {
                            MessageBox.Show($"Không được Kỳ thi đang diễn ra", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {
                            QLTTN.BaiLams.DeleteAllOnSubmit(QLTTN.BaiLams.Where(bl => bl.maKT == makt));
                            QLTTN.BuoiThis.DeleteAllOnSubmit(QLTTN.BuoiThis.Where(bt => bt.maKT == makt));
                            QLTTN.KyThis.DeleteOnSubmit(ktDuocChon);
                            QLTTN.SubmitChanges();
                            QLKTOTDgvKT.Rows.RemoveAt(e.RowIndex);
                            LoadQLKTDgvKyThi();
                            LoadQLKTOTDgvDeThi();
                            LoadQLKTOTDgvHocSinh();
                        }
                    };
                }
            };
            QLKTOTDgvKT.SelectionChanged += (s, e) =>
            {
                LoadQLKTOTDgvDeThi();
                LoadQLKTOTDgvHocSinh();
            };
            QLKTOTDgvDT.SelectionChanged += (s, e) =>
            {
                LoadQLKTOTDgvHocSinh();
            };
           
            QLKTDgvKT.CellContentClick += (s, e) =>
            {
                if (e.ColumnIndex == 0)
                {
                    var makt = int.Parse(QLKTDgvKT.Rows[e.RowIndex].Cells["maKT"].Value.ToString());
                    using (var QLTTN = new QLTTNDataContext())
                    {
                        KyThi ktDuocChon = QLTTN.KyThis.Where(kt => kt.maKT == makt).FirstOrDefault();

                        if (ktDuocChon.BuoiThis.Where(buoithi => buoithi.NgayGioThi < DateTime.Now).Count() > 0)
                        {
                            MessageBox.Show($"Không được xóa kỳ thi Đang diễn ra", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {
                            QLTTN.BaiLams.DeleteAllOnSubmit(QLTTN.BaiLams.Where(bl => bl.maKT == makt));
                            QLTTN.BuoiThis.DeleteAllOnSubmit(QLTTN.BuoiThis.Where(bt => bt.maKT == makt));
                            QLTTN.KyThis.DeleteOnSubmit(ktDuocChon);
                            QLTTN.SubmitChanges();
                            QLKTDgvKT.Rows.RemoveAt(e.RowIndex);
                            LoadQLKTDgvKyThi();
                            LoadQLKTDgvDeThi();
                            LoadQLKTDgvHocSinh();
                        }
                    };
                }
            };
            QLKTDgvKT.SelectionChanged += (s, e) =>
             {
                 LoadQLKTDgvDeThi();
                 LoadQLKTDgvHocSinh();
             };
            QLKTDgvDT.SelectionChanged += (s, e) =>
             {
                 LoadQLKTDgvHocSinh();
             };
           QLDTDgvCauHoi.CellValueChanged += (s, e) =>
              {
                  if (e.ColumnIndex == 0)
                  {
                      int soch = 0;
                      foreach (DataGridViewRow row in QLDTDgvCauHoi.Rows)
                      {
                          var chon = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                          if (chon.Value == chon.TrueValue)
                          {
                              soch++;
                              chon.Selected = true;
                          }
                          else
                          {
                              chon.Selected = false;
                          }
                      }
                      qldtLblSoCHDuocChon.Text = soch + " câu";
                  }
              };
           
            QLDTDgvDeThi.CellClick += (s, e) =>
              {
                  if (e.ColumnIndex == 0)
                  {
                      int madt = int.Parse(QLDTDgvDeThi.Rows[e.RowIndex].Cells["maDT"].Value.ToString());
                      using (var QLTTN = new QLTTNDataContext())
                      {
                          QLTTN.CT_DeThis.DeleteAllOnSubmit(QLTTN.CT_DeThis.Where(ctdt => ctdt.maDT == madt));
                          QLTTN.SubmitChanges();
                          QLTTN.DeThis.DeleteOnSubmit(QLTTN.DeThis.Where(dt => dt.maDT == madt).Single());
                          QLTTN.SubmitChanges();
                          if (QLTTN.DeThis.Count() == 0)
                          {
                              QLDTDgvDeThi.Rows.Clear();
                              qldtTxtThoiGianLamBai.Text = "10";
                          }
                      }
                      LoadQLDTDgvDeThi();
                      QLDTDgvCauHoi.Rows.RemoveAt(e.RowIndex);
                  }
                  else
                  {
                      using (var QLTTN = new QLTTNDataContext())
                      {
                          if (QLTTN.DeThis.Count() == 0)
                          {
                              QLDTDgvDeThi.Rows.Clear();
                              return;
                          }
                          if (QLDTDgvDeThi.SelectedRows.Count > 0)
                          {
                              int madt = int.Parse(QLDTDgvDeThi.SelectedRows[0].Cells["maDT"].Value.ToString());
                              var dschtrongDeThi = QLTTN.CT_DeThis.Where(ctdt => ctdt.maDT == madt).GroupBy(ctdt => ctdt.maDT).Single().ToList();
                              foreach (DataGridViewRow row in QLDTDgvCauHoi.Rows)
                              {
                                  var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                                  string str = row.Cells["maCH"].Value.ToString();
                                  var mach = int.Parse(str);
                                  var timthay = dschtrongDeThi.Where(ch => ch.maCH == mach && ch.maDT == madt).Count();
                                  if (timthay == 1)
                                  {
                                      cell.Value = cell.TrueValue;
                                  }
                                  else
                                  {
                                      cell.Value = cell.FalseValue;
                                  }
                              }
                          }
                          else
                          {
                              MessageBox.Show("Không thể load câu hỏi từ đề thi");
                          }
                      }
                  }
            };
            

            t.Abort();
        }



        private void QLKTOTBtnThemKT_Click_1(object sender, EventArgs e)
        {
            frmThemKT frmThemKT = new frmThemKT(this, GV, cbKhoiLop.SelectedValue.ToString(), "ThiThu");
            frmThemKT.Show();
        }

        private void QLKTBtnThemKT_Click(object sender, EventArgs e)
        {
            frmThemKT frmThemKT = new frmThemKT(this, GV, cbKhoiLop.SelectedValue.ToString(), "ThiThiet");
            frmThemKT.Show();
        }

        private void QLCHBtnThemCH_Click(object sender, EventArgs e)
        {
            QLCHBtnThemDA.Enabled = true;
            CheckDS = 0;
            var QLTTN = new QLTTNDataContext();

            if (string.IsNullOrWhiteSpace(QLCHTxtCauHoi.Text))
            {
                MessageBox.Show("Bạn cần phải nhập nội dung cho câu hỏi", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (QLTTN.CauHois.Where(ch => ch.NoiDung.ToLower() == QLCHTxtCauHoi.Text.ToLower()).Count() != 0)
            {
                MessageBox.Show("Câu hỏi này đã có trong danh sách. Xin mời tạo câu hỏi mới", "Trùng record", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            QLTTN.CauHois.InsertOnSubmit(new CauHoi()
            {
                NoiDung = QLCHTxtCauHoi.Text,
                CapDo = QLTTN.CapDos.Where(cd => cd.maCD == int.Parse(QLCHCapDo.SelectedValue.ToString())).SingleOrDefault(),
                maKhoi = cbKhoiLop.SelectedValue.ToString(),
                maMH = GV.maMH
            });
            QLTTN.SubmitChanges();
            LoadCBCauHoi();
            QLCHCbDSCH.SelectedItem = QLCHCbDSCH.Items[QLCHCbDSCH.Items.Count - 1];
            QLCHTxtDapAn.Focus();
           
        }

        private void QLCHBtnXoaCH_Click(object sender, EventArgs e)
        {
            

            using (var QLTTN = new QLTTNDataContext())
            {

                if (QLCHCbDSCH.SelectedValue == null)
                {
                    MessageBox.Show("Loi Không Có Bất Cứ Câu Hỏi Nào");
                    return;
                }

                var cauHoiHienTai = QLTTN.CauHois
                                    .Where(ch => ch.maCH == int.Parse(QLCHCbDSCH.SelectedValue.ToString()))
                                    .FirstOrDefault();
                QLTTN.DapAns.DeleteAllOnSubmit(cauHoiHienTai.DapAns);
                QLTTN.CauHois.DeleteOnSubmit(cauHoiHienTai);
                try
                {
                    QLTTN.SubmitChanges();
                }
                catch (Exception ee)
                {
                    return;
                }
            }
            LoadCBCauHoi();
        }

        private void QLCHBtnSuaCH_Click(object sender, EventArgs e)
        {
            if (QLCHCbDSCH.SelectedValue == null)
            {
                return;
            }

            using (var QLTTN = new QLTTNDataContext())
            {
                var cauHoiHienTai = QLTTN.CauHois.Where(ch => ch.maCH == int.Parse(QLCHCbDSCH.SelectedValue.ToString())).FirstOrDefault();

                cauHoiHienTai.NoiDung = QLCHTxtCauHoi.Text;
                cauHoiHienTai.CapDo = QLTTN.CapDos.Where(cd => cd.maCD == int.Parse(QLCHCapDo.SelectedValue.ToString())).FirstOrDefault();
                cauHoiHienTai.maKhoi = cbKhoiLop.SelectedValue.ToString();
                QLTTN.SubmitChanges();
            }

            LoadCBCauHoi();

        }

        private void QLCHBtnThemDA_Click(object sender, EventArgs e)
        {
            if (QLCHCbDSCH.SelectedValue == null)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(QLCHTxtDapAn.Text))
            {
                MessageBox.Show("Bạn cần phải nhập cho đáp án", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var QLTTN = new QLTTNDataContext();
            
            var cauHoiHienTai = QLTTN.CauHois
            .Where(ch => ch.maCH == int.Parse(QLCHCbDSCH.SelectedValue.ToString()))
            .FirstOrDefault();
            if (cauHoiHienTai.DapAns.Where(da => da.NoiDung.ToLower() == QLCHTxtDapAn.Text.ToLower()).Count() != 0)
            {
                MessageBox.Show("Đáp án này đã có trong danh sách Đáp Án. Xin mời tạo đáp án mới", "Lỗi trùng dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            if (cauHoiHienTai.DapAns.Count >= 4)
            {
                MessageBox.Show("Đã Đủ 4 Câu Trả Lời !");               
                QLCHBtnThemCH.Enabled = true;                         
                return;
            }
      
            QLTTN.DapAns.InsertOnSubmit(new DapAn()
            {
                NoiDung = QLCHTxtDapAn.Text,
                DungSai = QLCHCkbDungSai.Checked,
                CauHoi = QLTTN.CauHois.Where(ch => ch.maCH == int.Parse(QLCHCbDSCH.SelectedValue.ToString())).SingleOrDefault()
            });
            QLTTN.SubmitChanges();        
            LoadQLCHDgvDapAn();
           QLCHDgvDSDA.Rows[QLCHDgvDSDA.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Selected = true;           
            QLCHTxtDapAn.Focus();


        }

        private void QLCHBtnXoaDA_Click(object sender, EventArgs e)
        {
            if (QLCHCbDSCH.SelectedValue == null)
            {
                return;
            }
            var QLTTN = new QLTTNDataContext();
            
            var cauHoiHienTai = QLTTN.CauHois.Where(ch => ch.maCH == int.Parse(QLCHCbDSCH.SelectedValue.ToString())).FirstOrDefault();
            if (cauHoiHienTai.DapAns.Count <= 2)
            {
                MessageBox.Show("Mỗi câu hỏi cần phải có tối thiểu 2 đáp án", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (QLCHDgvDSDA.SelectedRows.Count == 0)
            {
                MessageBox.Show("Hãy chọn đáp án cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            QLTTN.DapAns.DeleteOnSubmit(cauHoiHienTai.DapAns.Where(da => da.maDA == int.Parse(QLCHDgvDSDA.SelectedRows[0].Cells["maDA"].Value.ToString())).FirstOrDefault());
            QLTTN.SubmitChanges();
            
            LoadQLCHDgvDapAn();
        }

        private void QLCHBtnSuaDA_Click(object sender, EventArgs e)
        {
            if (QLCHCbDSCH.SelectedValue == null)
            {
                return;
            }
            var QLTTN = new QLTTNDataContext();
            
            var cauHoiHienTai = QLTTN.CauHois
                                .Where(ch => ch.maCH == int.Parse(QLCHCbDSCH.SelectedValue.ToString()))
                                .FirstOrDefault();

            var dapAnHienTai = cauHoiHienTai.DapAns.Where(da => da.maDA == int.Parse(QLCHDgvDSDA.SelectedRows[0].Cells["maDA"].Value.ToString())).FirstOrDefault();
            dapAnHienTai.NoiDung = QLCHTxtDapAn.Text;
            dapAnHienTai.DungSai = QLCHCkbDungSai.Checked;
            if (dapAnHienTai.DungSai == true)
            {
                CheckDS += 1;
            }


            if (CheckDS < 1)
            {
                MessageBox.Show("Phải có ít nhất 1 đáp án đúng !");
                QLCHBtnThemDA.Enabled = false;
             
                QLCHCbDSCH.Enabled = false;
                return;


            }
            else
            {
                if (CheckDS > 1)
                {
                    MessageBox.Show("1 Câu Hỏi Chỉ Được 1 Đáp Án Đúng !");
                    QLCHBtnThemDA.Enabled = false;
                   
                    QLCHCbDSCH.Enabled = false;
                    return;

                }
                else
                {
                    MessageBox.Show("Sửa Đáp Án Thành Công !");
                    QLCHBtnThemDA.Enabled = false;
                    QLCHBtnThemCH.Enabled = true;
                    QLCHCbDSCH.Enabled = true;
                }
            }


            QLTTN.SubmitChanges();
            
            LoadQLCHDgvDapAn();

            QLCHTxtDapAn.Focus();



        }

        private void QLDTBtnThemDT_Click(object sender, EventArgs e)
        {
            int soch = int.Parse(qldtLblSoCHDuocChon.Text.Replace(" câu", ""));
            if (string.IsNullOrWhiteSpace(qldtTxtTenDT.Text))
            {
                MessageBox.Show("Bạn cần phải nhập tên đề thi", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (soch >= 5)
            {
                var QLTTN = new QLTTNDataContext();
                
                var dethiHientai = new DeThi()
                {
                    maGV = GV.maGV,
                    maMH = GV.maMH,
                    maKhoi = cbKhoiLop.SelectedValue.ToString(),
                    TenDT = qldtTxtTenDT.Text,
                    ThoiGianLamBai = TimeSpan.FromMinutes(double.Parse(qldtTxtThoiGianLamBai.Text)),
                    NgayTao = DateTime.Now
                };

                if (DaTonTai(dethiHientai))
                {
                    return;
                }

                QLTTN.DeThis.InsertOnSubmit(dethiHientai);
                QLTTN.SubmitChanges();

                foreach (DataGridViewRow row in QLDTDgvCauHoi.Rows)
                {
                    int maDTHienTai = (int)QLTTN.ExecuteQuery<decimal>("select IDENT_CURRENT('dbo.DeThi')").FirstOrDefault();

                    var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                    if (cell.Value == cell.TrueValue)
                    {
                        QLTTN.CT_DeThis.InsertOnSubmit(new CT_DeThi()
                        {
                            maDT = maDTHienTai,
                            maCH = int.Parse(row.Cells["maCH"].Value.ToString())
                        });
                    }
                }
                QLTTN.SubmitChanges();
                

                LoadQLDTDgvDeThi();
            }
            else
            {
                MessageBox.Show("cần có ít nhất 5 câu hỏi", "Yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void QLDTBtnSuaDT_Click(object sender, EventArgs e)
        {
            int soch = int.Parse(qldtLblSoCHDuocChon.Text.Replace(" câu", ""));
            if (soch < 5)
            {
                MessageBox.Show("Mỗi đề thi cần có ít nhất 5 câu hỏi", "Yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var QLTTN = new QLTTNDataContext();
            

            int MaDt;
            if (QLDTDgvDeThi.SelectedRows.Count > 0)
            {
                MaDt = int.Parse(QLDTDgvDeThi.SelectedRows[0].Cells["maDT"].Value.ToString());
                var DeThiHienTai = QLTTN.DeThis.Where(dt => dt.maDT == MaDt).Single();

                DeThiHienTai.maGV = GV.maGV;
                DeThiHienTai.maMH = GV.maMH;
                DeThiHienTai.maKhoi = cbKhoiLop.SelectedValue.ToString();
                DeThiHienTai.TenDT = qldtTxtTenDT.Text;
                DeThiHienTai.ThoiGianLamBai = TimeSpan.FromMinutes(double.Parse(qldtTxtThoiGianLamBai.Text));
                DeThiHienTai.NgayTao = DateTime.Now;
                QLTTN.SubmitChanges();

                QLTTN.CT_DeThis.DeleteAllOnSubmit(QLTTN.CT_DeThis.Where(ctdt => ctdt.maDT == MaDt));
                QLTTN.SubmitChanges();

                foreach (DataGridViewRow row in QLDTDgvCauHoi.Rows)
                {
                    var cell = row.Cells["Chon"] as DataGridViewCheckBoxCell;
                    if (cell.Value == cell.TrueValue)
                    {
                        QLTTN.CT_DeThis.InsertOnSubmit(new CT_DeThi()
                        {
                            maDT = MaDt,
                            maCH = int.Parse(row.Cells["maCH"].Value.ToString())
                        });
                    }
                }
                QLTTN.SubmitChanges();

                LoadQLDTDgvDeThi();
            }
            else
            {
                MessageBox.Show("Lựa chọn đề thi cần cập nhật", "Yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }

        private void QLDTBtnRdCauHoi_Click(object sender, EventArgs e)
        {
            CheckDGV(QLDTDgvCauHoi, (int)qldtNudCauHoiNgauNhien.Value);

        }
     
        private void QLKTBtnSuaKT_Click(object sender, EventArgs e)
        {
            if (QLKTDgvKT.RowCount > 0 && QLKTDgvKT.SelectedRows[0] != null)
            {
                int makt = int.Parse(QLKTDgvKT.SelectedRows[0].Cells["maKT"].Value.ToString());
                string MaKhoi = cbKhoiLop.SelectedValue.ToString();
                frmSuaKT frmsuakt = new frmSuaKT(this, GV, makt, MaKhoi, BSKyThi);
                frmsuakt.Show();
            }
            else
            {
                MessageBox.Show("Mời bạn chọn kỳ thi cần cập nhật",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void QLKTBtnPrintInfo_Click(object sender, EventArgs e)
        {
            if (QLKTDgvKT.RowCount > 0 && QLKTDgvKT.SelectedRows.Count > 0)
            {
                int MaKT = int.Parse(QLKTDgvKT.SelectedRows[0].Cells["maKT"].Value.ToString());
                var frmRp = new frmReport(MaKT);
                frmRp.ShowDialog();
            }
            else
            {
                MessageBox.Show("Hãy chọn một kỳ thi", "Thông báo", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        

        private void QLKTOTBtnSuaKT_Click(object sender, EventArgs e)
        {
            if (QLKTOTDgvKT.RowCount > 0 && QLKTOTDgvKT.SelectedRows[0] != null)
            {
                int makt = int.Parse(QLKTOTDgvKT.SelectedRows[0].Cells["maKT"].Value.ToString());
                string maKhoi = cbKhoiLop.SelectedValue.ToString();
                frmSuaKT frmsuakt = new frmSuaKT(this, GV, makt, maKhoi, BSKyThiThu);
                frmsuakt.Show();
            }
            else
            {
                MessageBox.Show("Mời bạn chọn kỳ thi cần cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void QLHSRbHS_CheckedChanged(object sender, EventArgs e)
        {
            if (QLHSRbHS.Checked == true)
            {
                QLHSDgvDS.DataSource = null;
                LoadQlhsDgvHocSinh();
            }
        }

        private void QLHSRbKT_CheckedChanged(object sender, EventArgs e)
        {
            if (QLHSRbKT.Checked == true)
            {
                QLHSDgvDS.DataSource = null;
                LoadQLHSDgvKyThi();
            }
        }

        private void QLHSRbCH_CheckedChanged(object sender, EventArgs e)
        {
            if (QLHSRbCH.Checked == true)
            {
                QLHSDgvDS.DataSource = null;
                LoadQLHSDgvCauHoi();
            }
        }

        private void qldtTxtThoiGianLamBai_ValueChanged(object sender, EventArgs e)
        {

        }

        private void QLDTLblNgayTao_Click(object sender, EventArgs e)
        {

        }

        private void lblTitleInfo_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

                try
                {
                    GV.HoTen = txtFullname.Text;
                    GV.NgaySinh = DateTime.Parse(txtNgaySinh.Text);
                    qlttn.SubmitChanges();
                    loadThongTinGV();
                    MessageBox.Show("Cập nhật thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exec)
                {
                    MessageBox.Show($"Không thể cập nhật thông tin{Environment.NewLine}{exec.Message}", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
        }
        private void loadThongTinGV()
        {
            lblUserId.Text = GV.maGV;
            txtFullname.Text = GV.HoTen;
            txtNgaySinh.Text = GV.NgaySinh.Value.ToShortDateString();
            lblSubject.Text = $"{QLTTN.MonHocs.Where(mh => mh.maMH == GV.maMH).Single().tenMH}";
        }
    }
}
