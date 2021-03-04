using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThiTracNghiem
{
    public partial class Form1 : Form
    {
        static BindingSource BS = new BindingSource();
        CauHoi nullCauHoi = new CauHoi() { maCH = 0, maCD = 0, NoiDung = "" };
        public Form1()
        {
            InitializeComponent();

            using (var QLTTN = new QLTTNDataContext())
            {
                BS.DataSource = (QLTTN.CauHois.Select(ch => new { ch.maCH, ch.maCD, ch.NoiDung }).ToList());
            }
            dgvCauHoi.DataSource = BS;

            Binding bd = txtMaCD.DataBindings.Add("Text", BS, "maCD", true);
            bd.NullValue = "1";
            txtMaCH.DataBindings.Add("Text", BS, "maCH", true, DataSourceUpdateMode.OnValidation, 1);
            txtNoiDung.DataBindings.Add("Text", BS, "NoiDung", true, DataSourceUpdateMode.OnValidation, "Một chuỗi nào đó", " ");
        }
    }
}
