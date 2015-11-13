namespace DemoForAIA
{
    partial class frmPrintChequeView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrintChequeView));
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.btnPageSetup = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnZoom = new System.Windows.Forms.ToolStripSplitButton();
            this.tsmiZoomActualSize = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiZoomFullPage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiZoomPageWidth = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiZoomTwoPages = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiZoom500 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiZoom200 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiZoom150 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiZoom100 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiZoom75 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiZoom50 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiZoom25 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiZoom10 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnFirst = new System.Windows.Forms.ToolStripButton();
            this.btnPrev = new System.Windows.Forms.ToolStripButton();
            this.txtStartPage = new System.Windows.Forms.ToolStripTextBox();
            this.lblPageCount = new System.Windows.Forms.ToolStripLabel();
            this.btnNext = new System.Windows.Forms.ToolStripButton();
            this.btnLast = new System.Windows.Forms.ToolStripButton();
            this._separator = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancel = new System.Windows.Forms.ToolStripButton();
            this._preview = new DemoForAIA.ucEnhancedPrintPreview();
            this.tsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsMain
            // 
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPrint,
            this.btnPageSetup,
            this.toolStripSeparator2,
            this.btnZoom,
            this.btnFirst,
            this.btnPrev,
            this.txtStartPage,
            this.lblPageCount,
            this.btnNext,
            this.btnLast,
            this._separator,
            this.btnCancel});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(426, 25);
            this.tsMain.TabIndex = 0;
            this.tsMain.Text = "toolStrip1";
            // 
            // btnPrint
            // 
            this.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(23, 22);
            this.btnPrint.Text = "Print Document";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnPageSetup
            // 
            this.btnPageSetup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPageSetup.Image = ((System.Drawing.Image)(resources.GetObject("btnPageSetup.Image")));
            this.btnPageSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPageSetup.Name = "btnPageSetup";
            this.btnPageSetup.Size = new System.Drawing.Size(23, 22);
            this.btnPageSetup.Text = "Page Setup";
            this.btnPageSetup.Click += new System.EventHandler(this.btnPageSetup_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnZoom
            // 
            this.btnZoom.AutoToolTip = false;
            this.btnZoom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiZoomActualSize,
            this.tsmiZoomFullPage,
            this.tsmiZoomPageWidth,
            this.tsmiZoomTwoPages,
            this.toolStripMenuItem1,
            this.tsmiZoom500,
            this.tsmiZoom200,
            this.tsmiZoom150,
            this.tsmiZoom100,
            this.tsmiZoom75,
            this.tsmiZoom50,
            this.tsmiZoom25,
            this.tsmiZoom10});
            this.btnZoom.Image = ((System.Drawing.Image)(resources.GetObject("btnZoom.Image")));
            this.btnZoom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Size = new System.Drawing.Size(65, 22);
            this.btnZoom.Text = "&Zoom";
            this.btnZoom.ButtonClick += new System.EventHandler(this.btnZoom_ButtonClick);
            this.btnZoom.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.btnZoom_DropDownItemClicked);
            // 
            // tsmiZoomActualSize
            // 
            this.tsmiZoomActualSize.Image = ((System.Drawing.Image)(resources.GetObject("tsmiZoomActualSize.Image")));
            this.tsmiZoomActualSize.Name = "tsmiZoomActualSize";
            this.tsmiZoomActualSize.Size = new System.Drawing.Size(129, 22);
            this.tsmiZoomActualSize.Text = "Actual Size";
            // 
            // tsmiZoomFullPage
            // 
            this.tsmiZoomFullPage.Image = ((System.Drawing.Image)(resources.GetObject("tsmiZoomFullPage.Image")));
            this.tsmiZoomFullPage.Name = "tsmiZoomFullPage";
            this.tsmiZoomFullPage.Size = new System.Drawing.Size(129, 22);
            this.tsmiZoomFullPage.Text = "Full Page";
            // 
            // tsmiZoomPageWidth
            // 
            this.tsmiZoomPageWidth.Image = ((System.Drawing.Image)(resources.GetObject("tsmiZoomPageWidth.Image")));
            this.tsmiZoomPageWidth.Name = "tsmiZoomPageWidth";
            this.tsmiZoomPageWidth.Size = new System.Drawing.Size(129, 22);
            this.tsmiZoomPageWidth.Text = "Page Width";
            // 
            // tsmiZoomTwoPages
            // 
            this.tsmiZoomTwoPages.Image = ((System.Drawing.Image)(resources.GetObject("tsmiZoomTwoPages.Image")));
            this.tsmiZoomTwoPages.Name = "tsmiZoomTwoPages";
            this.tsmiZoomTwoPages.Size = new System.Drawing.Size(129, 22);
            this.tsmiZoomTwoPages.Text = "Two Pages";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(126, 6);
            // 
            // tsmiZoom500
            // 
            this.tsmiZoom500.Name = "tsmiZoom500";
            this.tsmiZoom500.Size = new System.Drawing.Size(129, 22);
            this.tsmiZoom500.Text = "500%";
            // 
            // tsmiZoom200
            // 
            this.tsmiZoom200.Name = "tsmiZoom200";
            this.tsmiZoom200.Size = new System.Drawing.Size(129, 22);
            this.tsmiZoom200.Text = "200%";
            // 
            // tsmiZoom150
            // 
            this.tsmiZoom150.Name = "tsmiZoom150";
            this.tsmiZoom150.Size = new System.Drawing.Size(129, 22);
            this.tsmiZoom150.Text = "150%";
            // 
            // tsmiZoom100
            // 
            this.tsmiZoom100.Name = "tsmiZoom100";
            this.tsmiZoom100.Size = new System.Drawing.Size(129, 22);
            this.tsmiZoom100.Text = "100%";
            // 
            // tsmiZoom75
            // 
            this.tsmiZoom75.Name = "tsmiZoom75";
            this.tsmiZoom75.Size = new System.Drawing.Size(129, 22);
            this.tsmiZoom75.Text = "75%";
            // 
            // tsmiZoom50
            // 
            this.tsmiZoom50.Name = "tsmiZoom50";
            this.tsmiZoom50.Size = new System.Drawing.Size(129, 22);
            this.tsmiZoom50.Text = "50%";
            // 
            // tsmiZoom25
            // 
            this.tsmiZoom25.Name = "tsmiZoom25";
            this.tsmiZoom25.Size = new System.Drawing.Size(129, 22);
            this.tsmiZoom25.Text = "25%";
            // 
            // tsmiZoom10
            // 
            this.tsmiZoom10.Name = "tsmiZoom10";
            this.tsmiZoom10.Size = new System.Drawing.Size(129, 22);
            this.tsmiZoom10.Text = "10%";
            // 
            // btnFirst
            // 
            this.btnFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFirst.Image = ((System.Drawing.Image)(resources.GetObject("btnFirst.Image")));
            this.btnFirst.ImageTransparentColor = System.Drawing.Color.Red;
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(23, 22);
            this.btnFirst.Text = "First Page";
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrev.Image = ((System.Drawing.Image)(resources.GetObject("btnPrev.Image")));
            this.btnPrev.ImageTransparentColor = System.Drawing.Color.Red;
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(23, 22);
            this.btnPrev.Text = "Previous Page";
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // txtStartPage
            // 
            this.txtStartPage.AutoSize = false;
            this.txtStartPage.Name = "txtStartPage";
            this.txtStartPage.Size = new System.Drawing.Size(32, 21);
            this.txtStartPage.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtStartPage.Validating += new System.ComponentModel.CancelEventHandler(this.txtStartPage_Validating);
            this.txtStartPage.Enter += new System.EventHandler(this.txtStartPage_Enter);
            this.txtStartPage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStartPage_KeyPress);
            // 
            // lblPageCount
            // 
            this.lblPageCount.Name = "lblPageCount";
            this.lblPageCount.Size = new System.Drawing.Size(10, 22);
            this.lblPageCount.Text = " ";
            // 
            // btnNext
            // 
            this.btnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
            this.btnNext.ImageTransparentColor = System.Drawing.Color.Red;
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(23, 22);
            this.btnNext.Text = "Next Page";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLast.Image = ((System.Drawing.Image)(resources.GetObject("btnLast.Image")));
            this.btnLast.ImageTransparentColor = System.Drawing.Color.Red;
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(23, 22);
            this.btnLast.Text = "Last Page";
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // _separator
            // 
            this._separator.Name = "_separator";
            this._separator.Size = new System.Drawing.Size(6, 25);
            this._separator.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoToolTip = false;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(59, 22);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // _preview
            // 
            this._preview.AutoScroll = true;
            this._preview.Dock = System.Windows.Forms.DockStyle.Fill;
            this._preview.PrintDoc = null;
            this._preview.Location = new System.Drawing.Point(0, 25);
            this._preview.Name = "_preview";
            this._preview.Size = new System.Drawing.Size(426, 323);
            this._preview.TabIndex = 1;
            this._preview.PageCountChanged += new System.EventHandler(this._preview_PageCountChanged);
            this._preview.StartPageChanged += new System.EventHandler(this._preview_StartPageChanged);
            // 
            // frmPrintCheque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(426, 348);
            this.Controls.Add(this._preview);
            this.Controls.Add(this.tsMain);
            this.Name = "frmPrintCheque";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Print Preview";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton btnPrint;
        private System.Windows.Forms.ToolStripButton btnPageSetup;
        private ucEnhancedPrintPreview _preview;
        private System.Windows.Forms.ToolStripSplitButton btnZoom;
        private System.Windows.Forms.ToolStripMenuItem tsmiZoomActualSize;
        private System.Windows.Forms.ToolStripMenuItem tsmiZoomFullPage;
        private System.Windows.Forms.ToolStripMenuItem tsmiZoomPageWidth;
        private System.Windows.Forms.ToolStripMenuItem tsmiZoomTwoPages;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmiZoom500;
        private System.Windows.Forms.ToolStripMenuItem tsmiZoom200;
        private System.Windows.Forms.ToolStripMenuItem tsmiZoom150;
        private System.Windows.Forms.ToolStripMenuItem tsmiZoom100;
        private System.Windows.Forms.ToolStripMenuItem tsmiZoom75;
        private System.Windows.Forms.ToolStripMenuItem tsmiZoom50;
        private System.Windows.Forms.ToolStripMenuItem tsmiZoom25;
        private System.Windows.Forms.ToolStripMenuItem tsmiZoom10;
        private System.Windows.Forms.ToolStripButton btnFirst;
        private System.Windows.Forms.ToolStripButton btnPrev;
        private System.Windows.Forms.ToolStripTextBox txtStartPage;
        private System.Windows.Forms.ToolStripLabel lblPageCount;
        private System.Windows.Forms.ToolStripButton btnNext;
        private System.Windows.Forms.ToolStripButton btnLast;
        private System.Windows.Forms.ToolStripSeparator _separator;
        private System.Windows.Forms.ToolStripButton btnCancel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}