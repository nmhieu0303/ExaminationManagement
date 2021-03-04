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
    public partial class frmReportKyThiResult : Form
    {
        private int makt;
        public frmReportKyThiResult(int makt)
        {
            this.makt = makt;
            InitializeComponent();
        }

        private void frmReportKyThiResult_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'QLTTNDataSet.sp_loadKetQuaKyThi' table. You can move, or remove it, as needed.
            this.sp_loadKetQuaKyThiTableAdapter.Fill(this.QLTTNDataSet.sp_loadKetQuaKyThi, makt);

            this.reportViewer1.RefreshReport();
        }
    }
}
