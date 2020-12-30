﻿using System;
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
        public frmTeacher()
        {
            InitializeComponent();
        }
        private void frmTeacher_Load(object sender, EventArgs e)
        {
            hideControlChangePass();
            hideControlUpadteInfo();
            pnlChoiceQuestion.Hide();
           
        }

 

        private void showControlUpadteInfo()
        {
            txtFullname.Show();
            dtmDOB.Show();
            btnUpdate.Hide();
        }


        private void hideControlUpadteInfo()
        {
            txtFullname.Hide();
            dtmDOB.Hide();
            btnUpdate.Show();
        }
        private void showControlChangePass()
        {
            lblPassCf.Show();
            txtPassCf.Show();
            txtPass.Show();
            btnChangePass.Hide();
        }
        private void hideControlChangePass()
        {
            lblPassCf.Hide();
            txtPassCf.Hide();
            txtPass.Hide();
            btnChangePass.Show();
        }
      

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            showControlUpadteInfo();
            btnChangePass.Enabled = false;
            btnUpdateAvatar.Enabled = false;
        }



        private void btnSaveInfo_Click(object sender, EventArgs e)
        {
            hideControlUpadteInfo();
            btnChangePass.Enabled = true;
            btnUpdateAvatar.Enabled = true;
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            showControlChangePass();
            btnUpdate.Enabled = false;
            btnUpdateAvatar.Enabled = false;
        }

        private void btnSavePass_Click(object sender, EventArgs e)
        {
            hideControlChangePass();
            btnUpdateAvatar.Enabled = true;
            btnUpdate.Enabled = true;
        }



        private void btnSaveAvatar_Click(object sender, EventArgs e)
        {
            btnSaveAvatar.SendToBack();
            btnUpdate.Enabled = true;
            btnChangePass.Enabled = true;
        }

        private void btnUpdateAvatar_Click(object sender, EventArgs e)
        {
            btnUpdateAvatar.SendToBack();
            btnUpdate.Enabled = false;
            btnChangePass.Enabled = false;
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box  
                picAvatar.Image = new Bitmap(open.FileName);
                // image file path  

                //textBox1.Text = open.FileName;
            }
        }

        private void btnAddPaper_Click(object sender, EventArgs e)
        {
            
            transitionListQuestion.Show(pnlChoiceQuestion);
        }

        private void btnCloseChoiceQues_Click(object sender, EventArgs e)
        {
            transitionListQuestion.Hide(pnlChoiceQuestion);
        }

        private void btnWatchQuestion_Click(object sender, EventArgs e)
        {
            transitionListQuestion.Show(pnlChoiceQuestion);
        }

        private void txtQuestion_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
