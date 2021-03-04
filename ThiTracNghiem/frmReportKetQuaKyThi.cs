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
    public partial class frmReportKetQuaKyThi : Form
    {
        int makt;
        string mahs;
        public frmReportKetQuaKyThi(int makt, string mahs)
        {
            this.makt = makt;
            this.mahs = mahs;
            InitializeComponent();
        }

        private void frmReportKetQuaKyThi_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'QLTTNDataSet.sp_loadKetQuaThiCuaHocSinh' table. You can move, or remove it, as needed.
            this.sp_loadKetQuaThiCuaHocSinhTableAdapter.Fill(this.QLTTNDataSet.sp_loadKetQuaThiCuaHocSinh, makt, mahs);
            this.reportViewer1.RefreshReport();
        }
    }
}
