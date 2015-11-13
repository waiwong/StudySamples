namespace DemoForAIA
{
    partial class frmMain
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.tsmiMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmClose = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiImportData = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAssignNo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDoPrintCheque = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVoidCheque = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOCR = new System.Windows.Forms.ToolStripMenuItem();
            this.signatrueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiReport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAssignRpt = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOCRResult = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiWindows = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCascade = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVectical = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHorizontal = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiArrangeIcons = new System.Windows.Forms.ToolStripMenuItem();
            this.ssMain = new System.Windows.Forms.StatusStrip();
            this.tssLabUserInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip.SuspendLayout();
            this.ssMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMenu,
            this.tsmiImportData,
            this.tsmiAssignNo,
            this.tsmiDoPrintCheque,
            this.tsmiVoidCheque,
            this.tsmiOCR,
            this.signatrueToolStripMenuItem,
            this.tsmiReport,
            this.tsmiWindows});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(831, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // tsmiMenu
            // 
            this.tsmiMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmClose});
            this.tsmiMenu.Name = "tsmiMenu";
            this.tsmiMenu.Size = new System.Drawing.Size(37, 20);
            this.tsmiMenu.Text = "&File";
            this.tsmiMenu.Visible = false;
            // 
            // tsmClose
            // 
            this.tsmClose.Name = "tsmClose";
            this.tsmClose.Size = new System.Drawing.Size(152, 22);
            this.tsmClose.Text = "&Close";
            this.tsmClose.Click += new System.EventHandler(this.tsmClose_Click);
            // 
            // tsmiImportData
            // 
            this.tsmiImportData.Name = "tsmiImportData";
            this.tsmiImportData.Size = new System.Drawing.Size(82, 20);
            this.tsmiImportData.Text = "Import Data";
            this.tsmiImportData.Click += new System.EventHandler(this.tsmiImportData_Click);
            // 
            // tsmiAssignNo
            // 
            this.tsmiAssignNo.Name = "tsmiAssignNo";
            this.tsmiAssignNo.Size = new System.Drawing.Size(117, 20);
            this.tsmiAssignNo.Text = "Assign Cheque No";
            this.tsmiAssignNo.Click += new System.EventHandler(this.tsmiAssignNo_Click);
            // 
            // tsmiDoPrintCheque
            // 
            this.tsmiDoPrintCheque.Name = "tsmiDoPrintCheque";
            this.tsmiDoPrintCheque.Size = new System.Drawing.Size(106, 20);
            this.tsmiDoPrintCheque.Text = "Do Print Cheque";
            this.tsmiDoPrintCheque.Click += new System.EventHandler(this.tsmiDoPrintCheque_Click);
            // 
            // tsmiVoidCheque
            // 
            this.tsmiVoidCheque.Name = "tsmiVoidCheque";
            this.tsmiVoidCheque.Size = new System.Drawing.Size(156, 20);
            this.tsmiVoidCheque.Text = "Void and Re-Print Cheque";
            this.tsmiVoidCheque.Click += new System.EventHandler(this.tsmiVoidCheque_Click);
            // 
            // tsmiOCR
            // 
            this.tsmiOCR.Name = "tsmiOCR";
            this.tsmiOCR.Size = new System.Drawing.Size(43, 20);
            this.tsmiOCR.Text = "OCR";
            this.tsmiOCR.Click += new System.EventHandler(this.tsmiOCR_Click);
            // 
            // signatrueToolStripMenuItem
            // 
            this.signatrueToolStripMenuItem.Name = "signatrueToolStripMenuItem";
            this.signatrueToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.signatrueToolStripMenuItem.Text = "Signatrue";
            this.signatrueToolStripMenuItem.Click += new System.EventHandler(this.signatrueToolStripMenuItem_Click);
            // 
            // tsmiReport
            // 
            this.tsmiReport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAssignRpt,
            this.tsmiOCRResult});
            this.tsmiReport.Name = "tsmiReport";
            this.tsmiReport.Size = new System.Drawing.Size(54, 20);
            this.tsmiReport.Text = "Report";
            // 
            // tsmiAssignRpt
            // 
            this.tsmiAssignRpt.Name = "tsmiAssignRpt";
            this.tsmiAssignRpt.Size = new System.Drawing.Size(193, 22);
            this.tsmiAssignRpt.Text = "Assigned Batch Report";
            this.tsmiAssignRpt.Click += new System.EventHandler(this.tsmiAssignRpt_Click);
            // 
            // tsmiOCRResult
            // 
            this.tsmiOCRResult.Name = "tsmiOCRResult";
            this.tsmiOCRResult.Size = new System.Drawing.Size(193, 22);
            this.tsmiOCRResult.Text = "OCR Result";
            this.tsmiOCRResult.Click += new System.EventHandler(this.tsmiOCRResult_Click);
            // 
            // tsmiWindows
            // 
            this.tsmiWindows.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCascade,
            this.tsmiVectical,
            this.tsmiHorizontal,
            this.tsmiArrangeIcons});
            this.tsmiWindows.Name = "tsmiWindows";
            this.tsmiWindows.Size = new System.Drawing.Size(68, 20);
            this.tsmiWindows.Text = "Windows";
            this.tsmiWindows.Click += new System.EventHandler(this.tsmiWindows_Click);
            // 
            // tsmiCascade
            // 
            this.tsmiCascade.Name = "tsmiCascade";
            this.tsmiCascade.Size = new System.Drawing.Size(155, 22);
            this.tsmiCascade.Text = "Cascade";
            this.tsmiCascade.Click += new System.EventHandler(this.tsmiCascade_Click);
            // 
            // tsmiVectical
            // 
            this.tsmiVectical.Name = "tsmiVectical";
            this.tsmiVectical.Size = new System.Drawing.Size(155, 22);
            this.tsmiVectical.Text = "Title &Vectical";
            this.tsmiVectical.Click += new System.EventHandler(this.tsmiVectical_Click);
            // 
            // tsmiHorizontal
            // 
            this.tsmiHorizontal.Name = "tsmiHorizontal";
            this.tsmiHorizontal.Size = new System.Drawing.Size(155, 22);
            this.tsmiHorizontal.Text = "Title &Horizontal";
            this.tsmiHorizontal.Click += new System.EventHandler(this.tsmiHorizontal_Click);
            // 
            // tsmiArrangeIcons
            // 
            this.tsmiArrangeIcons.Name = "tsmiArrangeIcons";
            this.tsmiArrangeIcons.Size = new System.Drawing.Size(155, 22);
            this.tsmiArrangeIcons.Text = "ArrangeIcons";
            // 
            // ssMain
            // 
            this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssLabUserInfo});
            this.ssMain.Location = new System.Drawing.Point(0, 431);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(831, 22);
            this.ssMain.TabIndex = 2;
            // 
            // tssLabUserInfo
            // 
            this.tssLabUserInfo.Name = "tssLabUserInfo";
            this.tssLabUserInfo.Size = new System.Drawing.Size(118, 17);
            this.tssLabUserInfo.Text = "toolStripStatusLabel1";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 453);
            this.Controls.Add(this.ssMain);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "frmMain";
            this.Text = "AIA Print Cheque";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ssMain.ResumeLayout(false);
            this.ssMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmClose;
        private System.Windows.Forms.ToolStripMenuItem tsmiAssignNo;
        private System.Windows.Forms.ToolStripMenuItem tsmiWindows;
        private System.Windows.Forms.ToolStripMenuItem tsmiCascade;
        private System.Windows.Forms.ToolStripMenuItem tsmiVectical;
        private System.Windows.Forms.ToolStripMenuItem tsmiHorizontal;
        private System.Windows.Forms.ToolStripMenuItem tsmiArrangeIcons;
        private System.Windows.Forms.ToolStripMenuItem tsmiVoidCheque;
        private System.Windows.Forms.ToolStripMenuItem tsmiImportData;
        private System.Windows.Forms.ToolStripMenuItem tsmiOCR;
        private System.Windows.Forms.ToolStripMenuItem tsmiDoPrintCheque;
        private System.Windows.Forms.StatusStrip ssMain;
        private System.Windows.Forms.ToolStripStatusLabel tssLabUserInfo;
        private System.Windows.Forms.ToolStripMenuItem tsmiReport;
        private System.Windows.Forms.ToolStripMenuItem tsmiOCRResult;
        private System.Windows.Forms.ToolStripMenuItem tsmiAssignRpt;
        private System.Windows.Forms.ToolStripMenuItem signatrueToolStripMenuItem;
    }
}



