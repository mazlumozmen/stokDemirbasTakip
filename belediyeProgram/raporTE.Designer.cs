namespace belediyeProgram
{
    partial class raporTE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(raporTE));
            this.teslimEdilenBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.veritabaniDataSet = new belediyeProgram.veritabaniDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.teslimEdilenTableAdapter = new belediyeProgram.veritabaniDataSetTableAdapters.teslimEdilenTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.teslimEdilenBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.veritabaniDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // teslimEdilenBindingSource
            // 
            this.teslimEdilenBindingSource.DataMember = "teslimEdilen";
            this.teslimEdilenBindingSource.DataSource = this.veritabaniDataSet;
            // 
            // veritabaniDataSet
            // 
            this.veritabaniDataSet.DataSetName = "veritabaniDataSet";
            this.veritabaniDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.teslimEdilenBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "belediyeProgram.Report2.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(984, 561);
            this.reportViewer1.TabIndex = 0;
            // 
            // teslimEdilenTableAdapter
            // 
            this.teslimEdilenTableAdapter.ClearBeforeFill = true;
            // 
            // raporTE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "raporTE";
            this.Text = "Yazdır - DİDİM BELEDİYESİ";
            this.Load += new System.EventHandler(this.raporTE_Load);
            ((System.ComponentModel.ISupportInitialize)(this.teslimEdilenBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.veritabaniDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource teslimEdilenBindingSource;
        private veritabaniDataSet veritabaniDataSet;
        private veritabaniDataSetTableAdapters.teslimEdilenTableAdapter teslimEdilenTableAdapter;
    }
}