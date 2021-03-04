namespace ThiTracNghiem
{
    partial class frmReportKetQuaKyThi
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.sp_loadKetQuaThiCuaHocSinhBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.QLTTNDataSet = new ThiTracNghiem.QLTTNDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.sp_loadKetQuaThiCuaHocSinhTableAdapter = new ThiTracNghiem.QLTTNDataSetTableAdapters.sp_loadKetQuaThiCuaHocSinhTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.sp_loadKetQuaThiCuaHocSinhBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QLTTNDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // sp_loadKetQuaThiCuaHocSinhBindingSource
            // 
            this.sp_loadKetQuaThiCuaHocSinhBindingSource.DataMember = "sp_loadKetQuaThiCuaHocSinh";
            this.sp_loadKetQuaThiCuaHocSinhBindingSource.DataSource = this.QLTTNDataSet;
            // 
            // QLTTNDataSet
            // 
            this.QLTTNDataSet.DataSetName = "QLTTNDataSet";
            this.QLTTNDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "dsKetQuaThiCuaHocSinh";
            reportDataSource1.Value = this.sp_loadKetQuaThiCuaHocSinhBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "ThiTracNghiem.rpKetQuaThiCuaHocSinh.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(832, 474);
            this.reportViewer1.TabIndex = 0;
            // 
            // sp_loadKetQuaThiCuaHocSinhTableAdapter
            // 
            this.sp_loadKetQuaThiCuaHocSinhTableAdapter.ClearBeforeFill = true;
            // 
            // frmReportKetQuaKyThi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 474);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmReportKetQuaKyThi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmReportKetQuaKyThi";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmReportKetQuaKyThi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sp_loadKetQuaThiCuaHocSinhBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QLTTNDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource sp_loadKetQuaThiCuaHocSinhBindingSource;
        private QLTTNDataSet QLTTNDataSet;
        private QLTTNDataSetTableAdapters.sp_loadKetQuaThiCuaHocSinhTableAdapter sp_loadKetQuaThiCuaHocSinhTableAdapter;
    }
}