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
        private Teacher gv;

        //Binding Source
        private BindingSource bsCauHoi = new BindingSource();
        private BindingSource bsDapAn = new BindingSource();

        public frmTeacher()
        {
            InitializeComponent();
            
        }

        

        private void frmTeacher_Load(object sender, EventArgs e)
        {
            hideControlChangePass();
            hideControlUpadteInfo();
            pnlChoiceQuestion.Hide();
            loadCbKhoiLop();
            loadQlchCbCauHoi();
            loadQlchDgvDapAn();
            btnAddQues.Click += BtnAddQues_Click;
            btnUpdateQues.Click += BtnUpdateQues_Click;
            btnRemoveQues.Click += BtnRemoveQues_Click;
            btnAddAns.Click += BtnAddAns_Click;
            btnUpdateAns.Click += BtnUpdateAns_Click;
            btnRemoveAns.Click += BtnRemoveAns_Click;
            cbQuesList.SelectedIndexChanged += CbQuesList_SelectedIndexChanged;
            cbKhoiLop.SelectedIndexChanged += CbKhoiLop_SelectedIndexChanged;

        }

       




        /* ********************************************************* QUẢN LÝ CÂU HỎI  ******************************************   */
        // thêm câu hỏi và đáp án 
        private void BtnAddQues_Click(object sender, EventArgs e)
        {
            using (var qlttn = new QLTTNDataContext())
            {
                if (string.IsNullOrWhiteSpace(txtQuestion.Text))
                {
                    MessageBox.Show("Bạn cần phải nhập nội dung cho câu hỏi", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (qlttn.Questions.Where(ch => ch.content.ToLower() == txtQuestion.Text.ToLower()).Count() != 0)
                {
                    MessageBox.Show("Câu hỏi này đã có trong danh sách. Xin mời tạo câu hỏi mới", "Trùng record", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var cauhoi = qlttn.Questions.Where(ch => ch.Answers == null || ch.Answers.Count < 2).FirstOrDefault();
                if (cauhoi != null)
                {
                    MessageBox.Show($"Xin mời nhập tối thiểu 2 đáp án cho câu hỏi sau trước khi thêm câu hỏi mới:{Environment.NewLine} <{cauhoi.content}>", "Câu hỏi chưa có đáp án", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }

                qlttn.Questions.InsertOnSubmit(new Question()
                {
                    content = txtQuestion.Text,
                    LevelQue = qlttn.LevelQues.Where(cd => cd.id == int.Parse(qlchCbCapDo.SelectedValue.ToString())).SingleOrDefault(),
                    grade_id = cbKhoiLop.SelectedValue.ToString(),
                    //sub_id = gv.sub_id
                });
                qlttn.SubmitChanges();
                
                if(qlttn.Questions.Where(ch => ch.content == txtQuestion.Text).Count() != 0)
                {
                    MessageBox.Show("Thêm câu hỏi thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            loadQlchCbCauHoi();
        }

        private void BtnRemoveQues_Click(object sender, EventArgs e)
        {
            using (var qlttn = new QLTTNDataContext())
            {
                //if (qlchLbDeThiSuDungCauHoi.Items.Count > 0)
                //{
                //    string str = $"Không thể xóa câu hỏi này vì nó đang được sử dụng trong các đề thi: ";
                //    foreach (var item in qlchLbDeThiSuDungCauHoi.Items)
                //    {
                //        str += $"{ Environment.NewLine }{item.ToString()}";
                //    }
                //    str += $"{ Environment.NewLine }Để xóa cần phải loại câu hỏi này khỏi các đề thi trên.";
                //    MessageBox.Show(str, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //    return;
                //}
                if (cbQuesList.SelectedValue == null)
                {
                    return;
                }

                var cauHoiHienTai = qlttn.Questions
                                    .Where(ch => ch.id == int.Parse(cbQuesList.SelectedValue.ToString()))
                                    .FirstOrDefault();
                qlttn.Answers.DeleteAllOnSubmit(cauHoiHienTai.Answers);
                qlttn.Questions.DeleteOnSubmit(cauHoiHienTai);
                try
                {
                    qlttn.SubmitChanges();
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            loadQlchCbCauHoi();
        }

        private void BtnUpdateQues_Click(object sender, EventArgs e)
        {
            if (cbQuesList.SelectedValue == null)
            {
                return;
            }

            using (var qlttn = new QLTTNDataContext())
            {
                var cauHoiHienTai = qlttn.Questions
                                    .Where(ch => ch.id == int.Parse(cbQuesList.SelectedValue.ToString()))
                                    .FirstOrDefault();

                cauHoiHienTai.content = txtQuestion.Text;
                cauHoiHienTai.LevelQue = qlttn.LevelQues.Where(cd => cd.id == int.Parse(qlchCbCapDo.SelectedValue.ToString())).FirstOrDefault();
                cauHoiHienTai.grade_id = cbKhoiLop.SelectedValue.ToString();
                qlttn.SubmitChanges();
            }
            loadQlchCbCauHoi();
        }

        //chức năng xóa đáp án
        private void BtnRemoveAns_Click(object sender, EventArgs e)
        {
            if (cbQuesList.SelectedValue == null)
            {
                return;
            }
            using (var qlttn = new QLTTNDataContext())
            {
                var cauHoiHienTai = qlttn.Questions
                                    .Where(ch => ch.id == int.Parse(cbQuesList.SelectedValue.ToString()))
                                    .FirstOrDefault();
                if (cauHoiHienTai.Answers.Count <= 2)
                {
                    MessageBox.Show("Mỗi câu hỏi cần phải có tối thiểu 2 đáp án", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dgvAnswerList.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Hãy chọn đáp án cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                qlttn.Answers.DeleteOnSubmit(cauHoiHienTai.Answers.Where(da => da.id == int.Parse(dgvAnswerList.SelectedRows[0].Cells["id"].Value.ToString())).FirstOrDefault());
                qlttn.SubmitChanges();
            }
            loadQlchDgvDapAn();
        }

        //chức năng chỉnh sửa đáp án
        private void BtnUpdateAns_Click(object sender, EventArgs e)
        {
            if (cbQuesList.SelectedValue == null)
            {
                return;
            }
            using (var qlttn = new QLTTNDataContext())
            {
                var cauHoiHienTai = qlttn.Questions
                                    .Where(ch => ch.id == int.Parse(cbQuesList.SelectedValue.ToString()))
                                    .FirstOrDefault();

                var dapAnHienTai = cauHoiHienTai.Answers.Where(da => da.id == int.Parse(dgvAnswerList.SelectedRows[0].Cells["id"].Value.ToString())).FirstOrDefault();
                dapAnHienTai.content = txtAnswer.Text;
                dapAnHienTai.correct = qlchCkbDungSai.Checked;
                //dapAnHienTai.DungSai = bool.Parse(txtDungSai.Text);

                qlttn.SubmitChanges();
            }
            loadQlchDgvDapAn();
            txtAnswer.Focus();
        }
           
        //chức năng thêm đáp án cho câu hỏi
        private void BtnAddAns_Click(object sender, EventArgs e)
        {
            if (cbQuesList.SelectedValue == null)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(txtAnswer.Text))
            {
                MessageBox.Show("Bạn cần phải nhập nội dung cho đáp án", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            using (var qlttn = new QLTTNDataContext())
            {
                var cauHoiHienTai = qlttn.Questions
                                    .Where(ch => ch.id == int.Parse(cbQuesList.SelectedValue.ToString()))
                                    .FirstOrDefault();
                if (cauHoiHienTai.Answers.Where(da => da.content.ToLower() == txtAnswer.Text.ToLower()).Count() != 0)
                {
                    MessageBox.Show("Đáp án này đã có trong danh sách. Xin mời tạo đáp án mới", "Lỗi trùng dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
                if (cauHoiHienTai.Answers.Count > 4 )
                {
                    MessageBox.Show("Mỗi câu hỏi có tối đa 4 đáp án", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                qlttn.Answers.InsertOnSubmit(new Answer()
                {
                    content = txtAnswer.Text,
                    correct = qlchCkbDungSai.Checked,
                    Question = qlttn.Questions.Where(ch => ch.id == int.Parse(cbQuesList.SelectedValue.ToString())).SingleOrDefault()
                });
                qlttn.SubmitChanges();
            }
            loadQlchDgvDapAn();
            dgvAnswerList.Rows[dgvAnswerList.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Selected = true;
            txtAnswer.Focus();
        }



        private void loadCbKhoiLop()
        {
            using (var qlttn = new QLTTNDataContext())
            {
                var khoiLop = qlttn.Grades.Select(kl => kl.id).Distinct().ToList();
                cbKhoiLop.DataSource = khoiLop;
                cbKhoiLop.DisplayMember = "id";
                if (cbKhoiLop.Items.Count != 0)
                {
                    cbKhoiLop.SelectedItem = cbKhoiLop.Items[0];
                    string txt = cbKhoiLop.SelectedValue.ToString();
                }
            }
        }

        private void loadQlchCbCauHoi()
        {
            using (var qlttn = new QLTTNDataContext())
            {
                try
                {
                    var cauHoi = (qlttn.Questions.Select(ch => new { ch.id, ch.content, ch.level_id, ch.LevelQue.name }).ToList());
                    bsCauHoi.DataSource = cauHoi;

                }
                catch (Exception e)
                {
                    //MessageBox.Show(e.Message);
                    return;
                }

                if (bsCauHoi.Count > 0)
                {
                    cbQuesList.DataSource = bsCauHoi;
                    cbQuesList.DisplayMember = "content";
                    cbQuesList.ValueMember = "id";

                    qlchCbCapDo.DataSource = qlttn.LevelQues.ToList();
                    qlchCbCapDo.DisplayMember = "name";
                    qlchCbCapDo.ValueMember = "id";

                    var macd = qlttn.Questions.Where(ch => ch.id.ToString() == cbQuesList.SelectedValue.ToString()).Single().level_id;
                    qlchCbCapDo.SelectedValue = macd;

                    if (txtQuestion.DataBindings.Count == 0)
                    {
                        txtQuestion.DataBindings.Add("Text", bsCauHoi, "content", true, DataSourceUpdateMode.Never, "Null value");
                    }
                    if (qlchCbCapDo.DataBindings.Count == 0)
                    {
                        qlchCbCapDo.DataBindings.Add("SelectedValue", bsCauHoi, "level_id", true, DataSourceUpdateMode.Never, "Null value");
                        qlchCbCapDo.DataBindings[0].Format += (s, e) =>
                        {
                            if (e.DesiredType == typeof(string))
                            {
                                int maCapDo = int.Parse(e.Value.ToString());
                                e.Value = (qlchCbCapDo.DataSource as List<LevelQue>).Where(cd => cd.id == maCapDo).FirstOrDefault().name;
                            }
                        };
                        qlchCbCapDo.DataBindings[0].Parse += (s, e) =>
                        {
                            if (e.DesiredType == typeof(int))
                            {
                                string noiDungCapDo = e.Value.ToString();
                                e.Value = (qlchCbCapDo.DataSource as List<LevelQue>).Where(cd => cd.name == noiDungCapDo).FirstOrDefault().id;
                            }
                        };
                    }
                }
                else
                {
                    qlchCbCapDo.DataBindings.Clear();
                    txtQuestion.DataBindings.Clear();
                    MessageBox.Show("Không có dữ liệu câu hỏi", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbQuesList.DataSource = null;
                }
            }
        }

        private void loadQlchDgvDapAn()
        {
            using (var qlttn = new QLTTNDataContext())
            {
                if (cbQuesList.Items.Count > 0)
                {
                    try
                    {
                        bsDapAn.DataSource = qlttn.Questions.Where(ch => ch.id == int.Parse(cbQuesList.SelectedValue.ToString())).SingleOrDefault().Answers.ToList();
                        dgvAnswerList.DataSource = bsDapAn;
                    }
                    catch (Exception e)
                    {
                        //MessageBox.Show(e.Message);
                        return;
                    }

                    dgvAnswerList.Columns["ques_id"].Visible = false;
                    dgvAnswerList.Columns["id"].DisplayIndex = 1;
                    dgvAnswerList.Columns["id"].Width = 115;
                    dgvAnswerList.Columns["id"].HeaderText = "id";
                    dgvAnswerList.Columns["content"].DisplayIndex = 2;
                    dgvAnswerList.Columns["content"].Width = 310;
                    dgvAnswerList.Columns["content"].HeaderText = "Answer";
                    dgvAnswerList.Columns["correct"].DisplayIndex = 3;
                    dgvAnswerList.Columns["correct"].Width = 115;
                    dgvAnswerList.Columns["correct"].HeaderText = "bool";

                    if (txtAnswer.DataBindings.Count == 0)
                    {
                        txtAnswer.DataBindings.Add("Text", bsDapAn, "content", true, DataSourceUpdateMode.Never, "Null value");
                       
                    }
                    //if (qlchCkbDungSai.DataBindings.Count == 0)
                    //{
                    //    qlchCkbDungSai.DataBindings.Add("Checked", bsDapAn, "DungSai", true, DataSourceUpdateMode.Never, false);
                    //}
                }
                else
                {
                    txtAnswer.DataBindings.Clear();
                    //qlchCkbDungSai.DataBindings.Clear();
                    //MessageBox.Show("Không có dữ liệu đáp án", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dgvAnswerList.DataSource = null;
                }
            }        
        }

        private void CbQuesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadQlchDgvDapAn();
            if (cbQuesList.SelectedValue == null)
            {
                return;
            }
            using (var qlttn = new QLTTNDataContext())
            {
    
                cbQuesList.ValueMember = "id";
                //qlchLbDeThiSuDungCauHoi.DataSource = qlttn.DeThis.Where(dt => dt.CT_DeThis.
                //                                                            Where(ctdt => ctdt.maCH == int.Parse(qlchCbDsch.SelectedValue.ToString())).Count() > 0)
                //                                                 .Select(dt => new { dt.maDT, dt.TenDT }).ToList();
            }
        }

        private void CbKhoiLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadQlchCbCauHoi();
            loadQlchDgvDapAn();
            //loadQldtDgvCauHoi();
            //loadQldtDgvDeThi();
            //loadQlktDgvDeThi();
            //loadQlktDgvHocSinh();
            //loadQlktDgvKyThi();
            //loadQlkttotDgvHocSinh();
            //loadQlkttotDgvDeThi();
            //loadQlkttotDgvKyThi();
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

        
    }
}
