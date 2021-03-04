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
    public partial class frmReport : Form
    {
        int makt;
        public frmReport(int makt)
        {
            this.makt = makt;
            InitializeComponent();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'QLTTNDataSet.sp_loadDanhSachThiSinhKemThongTinKyThi' table. You can move, or remove it, as needed.
            this.sp_loadDanhSachThiSinhKemThongTinKyThiTableAdapter.Fill(this.QLTTNDataSet.sp_loadDanhSachThiSinhKemThongTinKyThi,makt);

            this.reportViewer1.RefreshReport();
        }
    }
}
