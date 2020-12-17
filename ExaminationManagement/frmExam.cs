using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExaminationManagement
{
    public partial class frmExam : Form
    {
        public frmExam()
        {
            InitializeComponent();
            
        }

        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {

        }

        private void chklQuestion_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = chklQuestion.SelectedIndex;
            MessageBox.Show(chklQuestion.SelectedItem.ToString());
        }
    }
}
