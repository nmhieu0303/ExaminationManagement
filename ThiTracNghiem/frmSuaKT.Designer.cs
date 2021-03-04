namespace ThiTracNghiem
{
    partial class frmSuaKT
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gbTongSoDeThi = new System.Windows.Forms.GroupBox();
            this.dtpNgayThi = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.dgvDT = new System.Windows.Forms.DataGridView();
            this.lblTgBatDau = new System.Windows.Forms.Label();
            this.lblMaKhoi = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblLoaiKT = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.gbTongSoThiSinh = new System.Windows.Forms.GroupBox();
            this.btnChonHetHS = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnRdHs = new Guna.UI2.WinForms.Guna2GradientButton();
            this.nudSoHocSinh = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.dgvHS = new System.Windows.Forms.DataGridView();
            this.txtTenKT = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnSuaKT = new Guna.UI2.WinForms.Guna2GradientButton();
            this.gbTongSoDeThi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDT)).BeginInit();
            this.gbTongSoThiSinh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoHocSinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHS)).BeginInit();
            this.SuspendLayout();
            // 
            // gbTongSoDeThi
            // 
            this.gbTongSoDeThi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(24)))), ((int)(((byte)(74)))));
            this.gbTongSoDeThi.Controls.Add(this.dtpNgayThi);
            this.gbTongSoDeThi.Controls.Add(this.dgvDT);
            this.gbTongSoDeThi.Controls.Add(this.lblTgBatDau);
            this.gbTongSoDeThi.ForeColor = System.Drawing.Color.Black;
            this.gbTongSoDeThi.Location = new System.Drawing.Point(13, 160);
            this.gbTongSoDeThi.Margin = new System.Windows.Forms.Padding(4);
            this.gbTongSoDeThi.Name = "gbTongSoDeThi";
            this.gbTongSoDeThi.Padding = new System.Windows.Forms.Padding(4);
            this.gbTongSoDeThi.Size = new System.Drawing.Size(656, 508);
            this.gbTongSoDeThi.TabIndex = 56;
            this.gbTongSoDeThi.TabStop = false;
            this.gbTongSoDeThi.Text = "Chọn một đề thi phù hợp với bộ môn của bạn";
            // 
            // dtpNgayThi
            // 
            this.dtpNgayThi.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(50)))), ((int)(((byte)(101)))));
            this.dtpNgayThi.BorderRadius = 5;
            this.dtpNgayThi.BorderThickness = 1;
            this.dtpNgayThi.CheckedState.Parent = this.dtpNgayThi;
            this.dtpNgayThi.CustomFormat = "dd/MM/yyyy hh:mm:ss";
            this.dtpNgayThi.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(41)))), ((int)(((byte)(86)))));
            this.dtpNgayThi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpNgayThi.ForeColor = System.Drawing.Color.White;
            this.dtpNgayThi.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayThi.HoverState.Parent = this.dtpNgayThi;
            this.dtpNgayThi.Location = new System.Drawing.Point(314, 17);
            this.dtpNgayThi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpNgayThi.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpNgayThi.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpNgayThi.Name = "dtpNgayThi";
            this.dtpNgayThi.ShadowDecoration.Parent = this.dtpNgayThi;
            this.dtpNgayThi.Size = new System.Drawing.Size(317, 46);
            this.dtpNgayThi.TabIndex = 127;
            this.dtpNgayThi.Value = new System.DateTime(2020, 12, 12, 17, 21, 21, 145);
            // 
            // dgvDT
            // 
            this.dgvDT.AllowUserToAddRows = false;
            this.dgvDT.AllowUserToDeleteRows = false;
            this.dgvDT.AllowUserToOrderColumns = true;
            this.dgvDT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDT.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDT.Location = new System.Drawing.Point(21, 68);
            this.dgvDT.Margin = new System.Windows.Forms.Padding(4);
            this.dgvDT.MultiSelect = false;
            this.dgvDT.Name = "dgvDT";
            this.dgvDT.RowHeadersVisible = false;
            this.dgvDT.RowHeadersWidth = 51;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            this.dgvDT.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvDT.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDT.Size = new System.Drawing.Size(610, 414);
            this.dgvDT.TabIndex = 35;
            // 
            // lblTgBatDau
            // 
            this.lblTgBatDau.AutoSize = true;
            this.lblTgBatDau.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTgBatDau.ForeColor = System.Drawing.Color.White;
            this.lblTgBatDau.Location = new System.Drawing.Point(126, 30);
            this.lblTgBatDau.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTgBatDau.Name = "lblTgBatDau";
            this.lblTgBatDau.Size = new System.Drawing.Size(162, 24);
            this.lblTgBatDau.TabIndex = 30;
            this.lblTgBatDau.Text = "Thời gian bắt đầu:";
            // 
            // lblMaKhoi
            // 
            this.lblMaKhoi.AutoSize = true;
            this.lblMaKhoi.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaKhoi.ForeColor = System.Drawing.Color.White;
            this.lblMaKhoi.Location = new System.Drawing.Point(542, 99);
            this.lblMaKhoi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaKhoi.Name = "lblMaKhoi";
            this.lblMaKhoi.Size = new System.Drawing.Size(83, 28);
            this.lblMaKhoi.TabIndex = 35;
            this.lblMaKhoi.Text = "Khối thi";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(666, 99);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 28);
            this.label2.TabIndex = 34;
            this.label2.Text = "Loại kỳ thi:";
            // 
            // lblLoaiKT
            // 
            this.lblLoaiKT.AutoSize = true;
            this.lblLoaiKT.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoaiKT.ForeColor = System.Drawing.Color.White;
            this.lblLoaiKT.Location = new System.Drawing.Point(775, 99);
            this.lblLoaiKT.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLoaiKT.Name = "lblLoaiKT";
            this.lblLoaiKT.Size = new System.Drawing.Size(111, 28);
            this.lblLoaiKT.TabIndex = 33;
            this.lblLoaiKT.Text = "Loại kỳ thi:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(446, 99);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 28);
            this.label1.TabIndex = 31;
            this.label1.Text = "Khối thi:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(332, 39);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(106, 28);
            this.label20.TabIndex = 29;
            this.label20.Text = "Tên kỳ thi:";
            // 
            // gbTongSoThiSinh
            // 
            this.gbTongSoThiSinh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(24)))), ((int)(((byte)(74)))));
            this.gbTongSoThiSinh.Controls.Add(this.btnChonHetHS);
            this.gbTongSoThiSinh.Controls.Add(this.btnRdHs);
            this.gbTongSoThiSinh.Controls.Add(this.nudSoHocSinh);
            this.gbTongSoThiSinh.Controls.Add(this.dgvHS);
            this.gbTongSoThiSinh.ForeColor = System.Drawing.Color.Black;
            this.gbTongSoThiSinh.Location = new System.Drawing.Point(671, 160);
            this.gbTongSoThiSinh.Margin = new System.Windows.Forms.Padding(4);
            this.gbTongSoThiSinh.Name = "gbTongSoThiSinh";
            this.gbTongSoThiSinh.Padding = new System.Windows.Forms.Padding(4);
            this.gbTongSoThiSinh.Size = new System.Drawing.Size(698, 508);
            this.gbTongSoThiSinh.TabIndex = 59;
            this.gbTongSoThiSinh.TabStop = false;
            this.gbTongSoThiSinh.Text = "Tổng số thí sinh được chọn: 0";
            // 
            // btnChonHetHS
            // 
            this.btnChonHetHS.BorderColor = System.Drawing.Color.Cyan;
            this.btnChonHetHS.BorderRadius = 5;
            this.btnChonHetHS.BorderThickness = 1;
            this.btnChonHetHS.CheckedState.Parent = this.btnChonHetHS;
            this.btnChonHetHS.CustomImages.Parent = this.btnChonHetHS;
            this.btnChonHetHS.FillColor = System.Drawing.Color.Transparent;
            this.btnChonHetHS.FillColor2 = System.Drawing.Color.Transparent;
            this.btnChonHetHS.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.btnChonHetHS.ForeColor = System.Drawing.Color.Cyan;
            this.btnChonHetHS.HoverState.BorderColor = System.Drawing.Color.Cyan;
            this.btnChonHetHS.HoverState.CustomBorderColor = System.Drawing.Color.Cyan;
            this.btnChonHetHS.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(54)))), ((int)(((byte)(252)))));
            this.btnChonHetHS.HoverState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnChonHetHS.HoverState.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChonHetHS.HoverState.ForeColor = System.Drawing.Color.White;
            this.btnChonHetHS.HoverState.Parent = this.btnChonHetHS;
            this.btnChonHetHS.Location = new System.Drawing.Point(533, 17);
            this.btnChonHetHS.Name = "btnChonHetHS";
            this.btnChonHetHS.ShadowDecoration.Parent = this.btnChonHetHS;
            this.btnChonHetHS.Size = new System.Drawing.Size(136, 46);
            this.btnChonHetHS.TabIndex = 176;
            this.btnChonHetHS.Text = "Chọn hết";
            // 
            // btnRdHs
            // 
            this.btnRdHs.BorderColor = System.Drawing.Color.Cyan;
            this.btnRdHs.BorderRadius = 5;
            this.btnRdHs.BorderThickness = 1;
            this.btnRdHs.CheckedState.Parent = this.btnRdHs;
            this.btnRdHs.CustomImages.Parent = this.btnRdHs;
            this.btnRdHs.FillColor = System.Drawing.Color.Transparent;
            this.btnRdHs.FillColor2 = System.Drawing.Color.Transparent;
            this.btnRdHs.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.btnRdHs.ForeColor = System.Drawing.Color.Cyan;
            this.btnRdHs.HoverState.BorderColor = System.Drawing.Color.Cyan;
            this.btnRdHs.HoverState.CustomBorderColor = System.Drawing.Color.Cyan;
            this.btnRdHs.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(54)))), ((int)(((byte)(252)))));
            this.btnRdHs.HoverState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnRdHs.HoverState.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRdHs.HoverState.ForeColor = System.Drawing.Color.White;
            this.btnRdHs.HoverState.Parent = this.btnRdHs;
            this.btnRdHs.Location = new System.Drawing.Point(374, 15);
            this.btnRdHs.Name = "btnRdHs";
            this.btnRdHs.ShadowDecoration.Parent = this.btnRdHs;
            this.btnRdHs.Size = new System.Drawing.Size(136, 46);
            this.btnRdHs.TabIndex = 175;
            this.btnRdHs.Text = "Ngẫu nhiên";
            // 
            // nudSoHocSinh
            // 
            this.nudSoHocSinh.BackColor = System.Drawing.Color.Transparent;
            this.nudSoHocSinh.BorderRadius = 5;
            this.nudSoHocSinh.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.nudSoHocSinh.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.nudSoHocSinh.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.nudSoHocSinh.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.nudSoHocSinh.DisabledState.Parent = this.nudSoHocSinh;
            this.nudSoHocSinh.DisabledState.UpDownButtonFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(177)))), ((int)(((byte)(177)))));
            this.nudSoHocSinh.DisabledState.UpDownButtonForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(203)))), ((int)(((byte)(203)))));
            this.nudSoHocSinh.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(41)))), ((int)(((byte)(86)))));
            this.nudSoHocSinh.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.nudSoHocSinh.FocusedState.Parent = this.nudSoHocSinh;
            this.nudSoHocSinh.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudSoHocSinh.ForeColor = System.Drawing.Color.White;
            this.nudSoHocSinh.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudSoHocSinh.Location = new System.Drawing.Point(254, 22);
            this.nudSoHocSinh.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.nudSoHocSinh.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSoHocSinh.Name = "nudSoHocSinh";
            this.nudSoHocSinh.ShadowDecoration.Parent = this.nudSoHocSinh;
            this.nudSoHocSinh.Size = new System.Drawing.Size(113, 35);
            this.nudSoHocSinh.TabIndex = 162;
            this.nudSoHocSinh.UpDownButtonFillColor = System.Drawing.Color.DarkViolet;
            this.nudSoHocSinh.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // dgvHS
            // 
            this.dgvHS.AllowUserToAddRows = false;
            this.dgvHS.AllowUserToDeleteRows = false;
            this.dgvHS.AllowUserToOrderColumns = true;
            this.dgvHS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHS.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvHS.Location = new System.Drawing.Point(29, 68);
            this.dgvHS.Margin = new System.Windows.Forms.Padding(4);
            this.dgvHS.Name = "dgvHS";
            this.dgvHS.RowHeadersVisible = false;
            this.dgvHS.RowHeadersWidth = 51;
            this.dgvHS.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHS.Size = new System.Drawing.Size(640, 414);
            this.dgvHS.TabIndex = 40;
            // 
            // txtTenKT
            // 
            this.txtTenKT.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(50)))), ((int)(((byte)(101)))));
            this.txtTenKT.BorderRadius = 5;
            this.txtTenKT.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenKT.DefaultText = "";
            this.txtTenKT.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTenKT.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTenKT.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenKT.DisabledState.Parent = this.txtTenKT;
            this.txtTenKT.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenKT.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(41)))), ((int)(((byte)(86)))));
            this.txtTenKT.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txtTenKT.FocusedState.FillColor = System.Drawing.Color.White;
            this.txtTenKT.FocusedState.ForeColor = System.Drawing.Color.Black;
            this.txtTenKT.FocusedState.Parent = this.txtTenKT;
            this.txtTenKT.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTenKT.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTenKT.HoverState.Parent = this.txtTenKT;
            this.txtTenKT.Location = new System.Drawing.Point(445, 29);
            this.txtTenKT.Name = "txtTenKT";
            this.txtTenKT.PasswordChar = '\0';
            this.txtTenKT.PlaceholderText = "Type your full name";
            this.txtTenKT.SelectedText = "";
            this.txtTenKT.ShadowDecoration.Parent = this.txtTenKT;
            this.txtTenKT.Size = new System.Drawing.Size(550, 46);
            this.txtTenKT.TabIndex = 121;
            // 
            // btnSuaKT
            // 
            this.btnSuaKT.BorderRadius = 20;
            this.btnSuaKT.CheckedState.Parent = this.btnSuaKT;
            this.btnSuaKT.CustomImages.Parent = this.btnSuaKT;
            this.btnSuaKT.FillColor = System.Drawing.Color.Green;
            this.btnSuaKT.FillColor2 = System.Drawing.Color.Green;
            this.btnSuaKT.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSuaKT.ForeColor = System.Drawing.Color.White;
            this.btnSuaKT.HoverState.Parent = this.btnSuaKT;
            this.btnSuaKT.Location = new System.Drawing.Point(553, 675);
            this.btnSuaKT.Name = "btnSuaKT";
            this.btnSuaKT.ShadowDecoration.Parent = this.btnSuaKT;
            this.btnSuaKT.Size = new System.Drawing.Size(224, 57);
            this.btnSuaKT.TabIndex = 178;
            this.btnSuaKT.Text = "CẬP NHẬT KỲ THI";
            // 
            // frmSuaKT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(24)))), ((int)(((byte)(74)))));
            this.ClientSize = new System.Drawing.Size(1371, 747);
            this.Controls.Add(this.btnSuaKT);
            this.Controls.Add(this.lblMaKhoi);
            this.Controls.Add(this.txtTenKT);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblLoaiKT);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbTongSoThiSinh);
            this.Controls.Add(this.gbTongSoDeThi);
            this.Controls.Add(this.label20);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmSuaKT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cập nhật thông tin kỳ thi";
            this.gbTongSoDeThi.ResumeLayout(false);
            this.gbTongSoDeThi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDT)).EndInit();
            this.gbTongSoThiSinh.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudSoHocSinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbTongSoDeThi;
        private System.Windows.Forms.DataGridView dgvDT;
        private System.Windows.Forms.Label lblTgBatDau;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.GroupBox gbTongSoThiSinh;
        private System.Windows.Forms.DataGridView dgvHS;
        private System.Windows.Forms.Label lblLoaiKT;
        private System.Windows.Forms.Label lblMaKhoi;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2TextBox txtTenKT;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpNgayThi;
        private Guna.UI2.WinForms.Guna2NumericUpDown nudSoHocSinh;
        private Guna.UI2.WinForms.Guna2GradientButton btnChonHetHS;
        private Guna.UI2.WinForms.Guna2GradientButton btnRdHs;
        private Guna.UI2.WinForms.Guna2GradientButton btnSuaKT;
    }
}