namespace DemoForAIA
{
    partial class frmRptViewer
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
            this.rptViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rptViewer
            // 
            this.rptViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rptViewer.Location = new System.Drawing.Point(0, 0);
            this.rptViewer.Name = "rptViewer";
            this.rptViewer.ShowBackButton = false;
            this.rptViewer.ShowDocumentMapButton = false;
            this.rptViewer.ShowExportButton = false;
            this.rptViewer.ShowParameterPrompts = false;
            this.rptViewer.ShowPromptAreaButton = false;
            this.rptViewer.ShowRefreshButton = false;
            this.rptViewer.Size = new System.Drawing.Size(292, 266);
            this.rptViewer.TabIndex = 0;
            this.rptViewer.RenderingBegin += new System.ComponentModel.CancelEventHandler(this.rptViewer_RenderingBegin);
            // 
            // frmRptViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.rptViewer);
            this.Name = "frmRptViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Show Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmRptViewer_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRptViewer_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rptViewer;
    }
}