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
    public partial class frmTeacher : Form
    {
        private frmLogin frmLogin;
        private Teacher teacher;

        public frmTeacher(frmLogin frmLogin, Teacher teacher)
        {
            InitializeComponent();
        }
    }
}
