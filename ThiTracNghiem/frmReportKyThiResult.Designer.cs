namespace ThiTracNghiem
{
    partial class frmReportKyThiResult
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
            this.sp_loadKetQuaKyThiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.QLTTNDataSet = new ThiTracNghiem.QLTTNDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.sp_loadKetQuaKyThiTableAdapter = new ThiTracNghiem.QLTTNDataSetTableAdapters.sp_loadKetQuaKyThiTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.sp_loadKetQuaKyThiBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QLTTNDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // sp_loadKetQuaKyThiBindingSource
            // 
            this.sp_loadKetQuaKyThiBindingSource.DataMember = "sp_loadKetQuaKyThi";
            this.sp_loadKetQuaKyThiBindingSource.DataSource = this.QLTTNDataSet;
            // 
            // QLTTNDataSet
            // 
            this.QLTTNDataSet.DataSetName = "QLTTNDataSet";
            this.QLTTNDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource1.Name = "dsKetQuaKyThi";
            reportDataSource1.Value = this.sp_loadKetQuaKyThiBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "ThiTracNghiem.rpKetQuaKyThi.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(12, 12);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(797, 463);
            this.reportViewer1.TabIndex = 0;
            // 
            // sp_loadKetQuaKyThiTableAdapter
            // 
            this.sp_loadKetQuaKyThiTableAdapter.ClearBeforeFill = true;
            // 
            // frmReportKyThiResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 487);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmReportKyThiResult";
            this.Text = "frmReportKyThiResult";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmReportKyThiResult_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sp_loadKetQuaKyThiBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QLTTNDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource sp_loadKetQuaKyThiBindingSource;
        private QLTTNDataSet QLTTNDataSet;
        private QLTTNDataSetTableAdapters.sp_loadKetQuaKyThiTableAdapter sp_loadKetQuaKyThiTableAdapter;
    }
}