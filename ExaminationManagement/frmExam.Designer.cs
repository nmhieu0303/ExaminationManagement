
namespace ExaminationManagement
{
    partial class frmExam
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExam));
            this.pnlQuestionGrp = new System.Windows.Forms.Panel();
            this.btnHelp = new Guna.UI2.WinForms.Guna2ImageButton();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.rdoAnswerD = new System.Windows.Forms.RadioButton();
            this.rdoAnswerA = new System.Windows.Forms.RadioButton();
            this.rdoAnswerB = new System.Windows.Forms.RadioButton();
            this.rdoAnswerC = new System.Windows.Forms.RadioButton();
            this.guna2ImageButton1 = new Guna.UI2.WinForms.Guna2ImageButton();
            this.btnPrev = new Guna.UI2.WinForms.Guna2ImageButton();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.pnlLeft = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.proTime = new Guna.UI2.WinForms.Guna2CircleProgressBar();
            this.lblTimeCountDown = new System.Windows.Forms.Label();
            this.pnlTop = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlTitle = new Guna.UI2.WinForms.Guna2Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblNameTeacher = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblDoTime = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblSub = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.guna2GradientButton1 = new Guna.UI2.WinForms.Guna2GradientButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pnlCtr = new Guna.UI2.WinForms.Guna2Panel();
            this.chklQuestion = new System.Windows.Forms.CheckedListBox();
            this.tmrDoTime = new System.Windows.Forms.Timer(this.components);
            this.pnlQuestionGrp.SuspendLayout();
            this.guna2Panel1.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.guna2Panel2.SuspendLayout();
            this.proTime.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlCtr.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlQuestionGrp
            // 
            this.pnlQuestionGrp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlQuestionGrp.Controls.Add(this.btnHelp);
            this.pnlQuestionGrp.Controls.Add(this.guna2Panel1);
            this.pnlQuestionGrp.Controls.Add(this.guna2ImageButton1);
            this.pnlQuestionGrp.Controls.Add(this.btnPrev);
            this.pnlQuestionGrp.Controls.Add(this.lblQuestion);
            this.pnlQuestionGrp.Location = new System.Drawing.Point(486, 316);
            this.pnlQuestionGrp.Name = "pnlQuestionGrp";
            this.pnlQuestionGrp.Size = new System.Drawing.Size(943, 430);
            this.pnlQuestionGrp.TabIndex = 6;
            // 
            // btnHelp
            // 
            this.btnHelp.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnHelp.CheckedState.Parent = this.btnHelp;
            this.btnHelp.HoverState.ImageSize = new System.Drawing.Size(60, 60);
            this.btnHelp.HoverState.Parent = this.btnHelp;
            this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
            this.btnHelp.ImageRotate = 0F;
            this.btnHelp.ImageSize = new System.Drawing.Size(60, 60);
            this.btnHelp.Location = new System.Drawing.Point(440, 356);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnHelp.PressedState.Parent = this.btnHelp;
            this.btnHelp.Size = new System.Drawing.Size(64, 64);
            this.btnHelp.TabIndex = 12;
            this.btnHelp.Click += new System.EventHandler(this.guna2ImageButton2_Click);
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BorderColor = System.Drawing.Color.Blue;
            this.guna2Panel1.BorderRadius = 5;
            this.guna2Panel1.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.guna2Panel1.BorderThickness = 1;
            this.guna2Panel1.Controls.Add(this.rdoAnswerD);
            this.guna2Panel1.Controls.Add(this.rdoAnswerA);
            this.guna2Panel1.Controls.Add(this.rdoAnswerB);
            this.guna2Panel1.Controls.Add(this.rdoAnswerC);
            this.guna2Panel1.Location = new System.Drawing.Point(39, 128);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.ShadowDecoration.Parent = this.guna2Panel1;
            this.guna2Panel1.Size = new System.Drawing.Size(871, 220);
            this.guna2Panel1.TabIndex = 11;
            // 
            // rdoAnswerD
            // 
            this.rdoAnswerD.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoAnswerD.ForeColor = System.Drawing.Color.White;
            this.rdoAnswerD.Location = new System.Drawing.Point(12, 168);
            this.rdoAnswerD.Name = "rdoAnswerD";
            this.rdoAnswerD.Size = new System.Drawing.Size(855, 32);
            this.rdoAnswerD.TabIndex = 12;
            this.rdoAnswerD.TabStop = true;
            this.rdoAnswerD.Text = "Hôm nay là thứ 5";
            this.rdoAnswerD.UseVisualStyleBackColor = true;
            // 
            // rdoAnswerA
            // 
            this.rdoAnswerA.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoAnswerA.ForeColor = System.Drawing.Color.White;
            this.rdoAnswerA.Location = new System.Drawing.Point(12, 20);
            this.rdoAnswerA.Name = "rdoAnswerA";
            this.rdoAnswerA.Size = new System.Drawing.Size(855, 32);
            this.rdoAnswerA.TabIndex = 11;
            this.rdoAnswerA.TabStop = true;
            this.rdoAnswerA.Text = "Hôm nay là thứ 5";
            this.rdoAnswerA.UseVisualStyleBackColor = true;
            // 
            // rdoAnswerB
            // 
            this.rdoAnswerB.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoAnswerB.ForeColor = System.Drawing.Color.White;
            this.rdoAnswerB.Location = new System.Drawing.Point(12, 68);
            this.rdoAnswerB.Name = "rdoAnswerB";
            this.rdoAnswerB.Size = new System.Drawing.Size(855, 32);
            this.rdoAnswerB.TabIndex = 10;
            this.rdoAnswerB.TabStop = true;
            this.rdoAnswerB.Text = "Hôm nay là thứ 5 Hôm nay là thứ 5 Hôm nay là thứ 5 Hôm nay là thứ 5 Hôm nay là th" +
    "ứ 5";
            this.rdoAnswerB.UseVisualStyleBackColor = true;
            // 
            // rdoAnswerC
            // 
            this.rdoAnswerC.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoAnswerC.ForeColor = System.Drawing.Color.White;
            this.rdoAnswerC.Location = new System.Drawing.Point(12, 117);
            this.rdoAnswerC.Name = "rdoAnswerC";
            this.rdoAnswerC.Size = new System.Drawing.Size(855, 32);
            this.rdoAnswerC.TabIndex = 9;
            this.rdoAnswerC.TabStop = true;
            this.rdoAnswerC.Text = "Hôm nay là thứ 5";
            this.rdoAnswerC.UseVisualStyleBackColor = true;
            // 
            // guna2ImageButton1
            // 
            this.guna2ImageButton1.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.guna2ImageButton1.CheckedState.Parent = this.guna2ImageButton1;
            this.guna2ImageButton1.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("guna2ImageButton1.HoverState.Image")));
            this.guna2ImageButton1.HoverState.ImageSize = new System.Drawing.Size(40, 40);
            this.guna2ImageButton1.HoverState.Parent = this.guna2ImageButton1;
            this.guna2ImageButton1.Image = ((System.Drawing.Image)(resources.GetObject("guna2ImageButton1.Image")));
            this.guna2ImageButton1.ImageRotate = 0F;
            this.guna2ImageButton1.ImageSize = new System.Drawing.Size(40, 40);
            this.guna2ImageButton1.Location = new System.Drawing.Point(510, 361);
            this.guna2ImageButton1.Name = "guna2ImageButton1";
            this.guna2ImageButton1.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.guna2ImageButton1.PressedState.Parent = this.guna2ImageButton1;
            this.guna2ImageButton1.Size = new System.Drawing.Size(98, 57);
            this.guna2ImageButton1.TabIndex = 10;
            // 
            // btnPrev
            // 
            this.btnPrev.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnPrev.CheckedState.Parent = this.btnPrev;
            this.btnPrev.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("btnPrev.HoverState.Image")));
            this.btnPrev.HoverState.ImageSize = new System.Drawing.Size(40, 40);
            this.btnPrev.HoverState.Parent = this.btnPrev;
            this.btnPrev.Image = ((System.Drawing.Image)(resources.GetObject("btnPrev.Image")));
            this.btnPrev.ImageRotate = 0F;
            this.btnPrev.ImageSize = new System.Drawing.Size(40, 40);
            this.btnPrev.Location = new System.Drawing.Point(336, 361);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnPrev.PressedState.Parent = this.btnPrev;
            this.btnPrev.Size = new System.Drawing.Size(98, 57);
            this.btnPrev.TabIndex = 9;
            // 
            // lblQuestion
            // 
            this.lblQuestion.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion.ForeColor = System.Drawing.Color.White;
            this.lblQuestion.Location = new System.Drawing.Point(3, 9);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.lblQuestion.Size = new System.Drawing.Size(942, 108);
            this.lblQuestion.TabIndex = 7;
            this.lblQuestion.Text = "Hôm nay là thứ mấy? Hôm nay là thứ 5 Hôm nay là thứ 5 Hôm nay là thứ 5 Hôm nay là" +
    " thứ 5 Hôm nay là thứ 5 Hôm nay là thứ 5 Hôm nay là thứ 5 Hôm nay là thứ 5 Hôm n" +
    "ay là thứ 5 Hôm nay là thứ 5";
            this.lblQuestion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.Transparent;
            this.pnlLeft.Controls.Add(this.chklQuestion);
            this.pnlLeft.Controls.Add(this.panel1);
            this.pnlLeft.Controls.Add(this.guna2Panel2);
            this.pnlLeft.Controls.Add(this.pnlCtr);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.ShadowDecoration.Parent = this.pnlLeft;
            this.pnlLeft.Size = new System.Drawing.Size(419, 758);
            this.pnlLeft.TabIndex = 8;
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel2.Controls.Add(this.label7);
            this.guna2Panel2.Controls.Add(this.panel3);
            this.guna2Panel2.Controls.Add(this.proTime);
            this.guna2Panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel2.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.ShadowDecoration.Parent = this.guna2Panel2;
            this.guna2Panel2.Size = new System.Drawing.Size(419, 243);
            this.guna2Panel2.TabIndex = 3;
            // 
            // proTime
            // 
            this.proTime.Controls.Add(this.lblTimeCountDown);
            this.proTime.Location = new System.Drawing.Point(117, 19);
            this.proTime.Name = "proTime";
            this.proTime.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.proTime.ShadowDecoration.Parent = this.proTime;
            this.proTime.Size = new System.Drawing.Size(160, 160);
            this.proTime.TabIndex = 2;
            // 
            // lblTimeCountDown
            // 
            this.lblTimeCountDown.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeCountDown.ForeColor = System.Drawing.Color.White;
            this.lblTimeCountDown.Location = new System.Drawing.Point(33, 68);
            this.lblTimeCountDown.Name = "lblTimeCountDown";
            this.lblTimeCountDown.Size = new System.Drawing.Size(95, 28);
            this.lblTimeCountDown.TabIndex = 15;
            this.lblTimeCountDown.Text = "24:00:00";
            this.lblTimeCountDown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Transparent;
            this.pnlTop.Controls.Add(this.pnlTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(419, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.ShadowDecoration.Parent = this.pnlTop;
            this.pnlTop.Size = new System.Drawing.Size(1082, 304);
            this.pnlTop.TabIndex = 9;
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.label3);
            this.pnlTitle.Controls.Add(this.label6);
            this.pnlTitle.Controls.Add(this.lblNameTeacher);
            this.pnlTitle.Controls.Add(this.label5);
            this.pnlTitle.Controls.Add(this.panel2);
            this.pnlTitle.Controls.Add(this.lblDoTime);
            this.pnlTitle.Controls.Add(this.label4);
            this.pnlTitle.Controls.Add(this.lblSub);
            this.pnlTitle.Controls.Add(this.label2);
            this.pnlTitle.Controls.Add(this.label1);
            this.pnlTitle.Location = new System.Drawing.Point(70, 19);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.ShadowDecoration.Parent = this.pnlTitle;
            this.pnlTitle.Size = new System.Drawing.Size(942, 268);
            this.pnlTitle.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(414, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(185, 28);
            this.label3.TabIndex = 14;
            this.label3.Text = "Giáo dục công dân";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(328, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 28);
            this.label6.TabIndex = 13;
            this.label6.Text = "Mã đề: ";
            // 
            // lblNameTeacher
            // 
            this.lblNameTeacher.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameTeacher.ForeColor = System.Drawing.Color.White;
            this.lblNameTeacher.Location = new System.Drawing.Point(442, 149);
            this.lblNameTeacher.Name = "lblNameTeacher";
            this.lblNameTeacher.Size = new System.Drawing.Size(335, 28);
            this.lblNameTeacher.TabIndex = 12;
            this.lblNameTeacher.Text = "Nguyễn Minh Hiếu";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(328, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 28);
            this.label5.TabIndex = 11;
            this.label5.Text = "Giáo viên: ";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.panel2.Location = new System.Drawing.Point(210, 48);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(567, 5);
            this.panel2.TabIndex = 10;
            // 
            // lblDoTime
            // 
            this.lblDoTime.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDoTime.ForeColor = System.Drawing.Color.White;
            this.lblDoTime.Location = new System.Drawing.Point(508, 110);
            this.lblDoTime.Name = "lblDoTime";
            this.lblDoTime.Size = new System.Drawing.Size(169, 28);
            this.lblDoTime.TabIndex = 4;
            this.lblDoTime.Text = "120 phút";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(328, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(174, 28);
            this.label4.TabIndex = 3;
            this.label4.Text = "Thời gian làm bài:";
            // 
            // lblSub
            // 
            this.lblSub.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSub.ForeColor = System.Drawing.Color.White;
            this.lblSub.Location = new System.Drawing.Point(393, 71);
            this.lblSub.Name = "lblSub";
            this.lblSub.Size = new System.Drawing.Size(185, 28);
            this.lblSub.TabIndex = 2;
            this.lblSub.Text = "Giáo dục công dân";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(328, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 28);
            this.label2.TabIndex = 1;
            this.label2.Text = "Môn:";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(942, 44);
            this.label1.TabIndex = 0;
            this.label1.Text = "KÌ THI TRUNG HỌC PHỔ THÔNG QUỐC GIA 2020";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(0, 243);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(152, 395);
            this.panel1.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.ForeColor = System.Drawing.Color.White;
            this.panel3.Location = new System.Drawing.Point(0, 238);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(419, 5);
            this.panel3.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(110, 197);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(184, 38);
            this.label7.TabIndex = 15;
            this.label7.Text = "List question";
            // 
            // guna2GradientButton1
            // 
            this.guna2GradientButton1.Animated = true;
            this.guna2GradientButton1.BorderRadius = 10;
            this.guna2GradientButton1.CheckedState.Parent = this.guna2GradientButton1;
            this.guna2GradientButton1.CustomImages.Parent = this.guna2GradientButton1;
            this.guna2GradientButton1.FillColor = System.Drawing.Color.Green;
            this.guna2GradientButton1.FillColor2 = System.Drawing.Color.YellowGreen;
            this.guna2GradientButton1.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold);
            this.guna2GradientButton1.ForeColor = System.Drawing.Color.White;
            this.guna2GradientButton1.HoverState.Parent = this.guna2GradientButton1;
            this.guna2GradientButton1.IndicateFocus = true;
            this.guna2GradientButton1.Location = new System.Drawing.Point(79, 35);
            this.guna2GradientButton1.Name = "guna2GradientButton1";
            this.guna2GradientButton1.ShadowDecoration.Parent = this.guna2GradientButton1;
            this.guna2GradientButton1.Size = new System.Drawing.Size(224, 56);
            this.guna2GradientButton1.TabIndex = 0;
            this.guna2GradientButton1.Text = "Submit";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.ForeColor = System.Drawing.Color.White;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(419, 5);
            this.panel4.TabIndex = 6;
            // 
            // pnlCtr
            // 
            this.pnlCtr.Controls.Add(this.panel4);
            this.pnlCtr.Controls.Add(this.guna2GradientButton1);
            this.pnlCtr.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlCtr.Location = new System.Drawing.Point(0, 638);
            this.pnlCtr.Name = "pnlCtr";
            this.pnlCtr.ShadowDecoration.Parent = this.pnlCtr;
            this.pnlCtr.Size = new System.Drawing.Size(419, 120);
            this.pnlCtr.TabIndex = 0;
            // 
            // chklQuestion
            // 
            this.chklQuestion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(24)))), ((int)(((byte)(74)))));
            this.chklQuestion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chklQuestion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chklQuestion.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chklQuestion.ForeColor = System.Drawing.Color.White;
            this.chklQuestion.FormattingEnabled = true;
            this.chklQuestion.Items.AddRange(new object[] {
            "Câu 1",
            "Câu 2",
            "Câu 3",
            "Câu 4",
            "Câu 5",
            "Câu 6",
            "Câu 7",
            "Câu 8",
            "Câu 9",
            "Câu 10",
            "Câu 11",
            "Câu 12",
            "Câu 13"});
            this.chklQuestion.Location = new System.Drawing.Point(152, 243);
            this.chklQuestion.Name = "chklQuestion";
            this.chklQuestion.Size = new System.Drawing.Size(267, 395);
            this.chklQuestion.TabIndex = 14;
            // 
            // frmExam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(24)))), ((int)(((byte)(74)))));
            this.ClientSize = new System.Drawing.Size(1501, 758);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlQuestionGrp);
            this.MinimumSize = new System.Drawing.Size(1519, 805);
            this.Name = "frmExam";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmExam";
            this.pnlQuestionGrp.ResumeLayout(false);
            this.guna2Panel1.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.guna2Panel2.ResumeLayout(false);
            this.guna2Panel2.PerformLayout();
            this.proTime.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.pnlCtr.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlQuestionGrp;
        private System.Windows.Forms.Label lblQuestion;
        private Guna.UI2.WinForms.Guna2ImageButton guna2ImageButton1;
        private Guna.UI2.WinForms.Guna2ImageButton btnPrev;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.RadioButton rdoAnswerD;
        private System.Windows.Forms.RadioButton rdoAnswerA;
        private System.Windows.Forms.RadioButton rdoAnswerB;
        private System.Windows.Forms.RadioButton rdoAnswerC;
        private Guna.UI2.WinForms.Guna2Panel pnlLeft;
        private Guna.UI2.WinForms.Guna2Panel pnlTop;
        private Guna.UI2.WinForms.Guna2Panel pnlTitle;
        private System.Windows.Forms.Label lblNameTeacher;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblDoTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblSub;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private Guna.UI2.WinForms.Guna2ImageButton btnHelp;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private Guna.UI2.WinForms.Guna2CircleProgressBar proTime;
        private System.Windows.Forms.Label lblTimeCountDown;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label7;
        private Guna.UI2.WinForms.Guna2Panel pnlCtr;
        private System.Windows.Forms.Panel panel4;
        private Guna.UI2.WinForms.Guna2GradientButton guna2GradientButton1;
        private System.Windows.Forms.CheckedListBox chklQuestion;
        private System.Windows.Forms.Timer tmrDoTime;
    }
}