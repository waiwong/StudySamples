using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Windows.Forms;
using BWHITD.Win.Lib;

namespace DemoForAIA
{
    /// <summary>
    /// Represents a dialog containing a <see cref="ucEnhancedPrintPreview"/> control
    /// used to preview and print <see cref="PrintDocument"/> objects.
    /// </summary>
    /// <remarks>
    /// This dialog is similar to the standard <see cref="PrintPreviewDialog"/>
    /// but provides additional options such printer and page setup buttons,
    /// a better UI based on the <see cref="ToolStrip"/> control, and built-in
    /// PDF export.
    /// </remarks>
    internal partial class frmPrintChequeView : frmBase
    {
        #region fields
        private readonly Font printFont = new Font("Segoe UI", 10);
        private readonly string strBatchNo = string.Empty;
        private Dictionary<int, ChequeItem> dicPrintItem;
        private PrintDocument printdoc;
        private int currItemIndex = 1;
        private int currPage = 1;

        private string strSelPrinter;
        private int mainTray;
        private int altTray;
        private Image imgSignature;

        #endregion

        #region ctor

        /// <summary>
        /// Initializes a new instance of a <see cref="frmPrintCheque"/>.
        /// </summary>
        public frmPrintChequeView(string strBatchNo, ChequeItem pPrintItem)
        {
            this.InitForm();
            this.strBatchNo = strBatchNo;
            this.dicPrintItem = new Dictionary<int, ChequeItem>();
            this.dicPrintItem.Add(1, pPrintItem);
        }

        /// <summary>
        /// Initializes a new instance of a <see cref="frmPrintCheque"/>.
        /// </summary>
        public frmPrintChequeView(string strBatchNo, Dictionary<int, ChequeItem> pDicPrintItem)
        {
            this.InitForm();
            this.strBatchNo = strBatchNo;
            this.dicPrintItem = pDicPrintItem;
        }

        protected void InitForm()
        {
            InitializeComponent();
            this.printdoc = new PrintDocument();
            this.printdoc.PrintPage += new PrintPageEventHandler(this.printDoc_PrintPage);
            this.printdoc.QueryPageSettings += new QueryPageSettingsEventHandler(this.printDoc_QueryPageSettings);
            this.printdoc.BeginPrint += this.PrintDoc_BeginPrint;
            this.printdoc.EndPrint += this.PrintDoc_EndPrint;

            this.btnPageSetup.Visible = false;

            this.imgSignature = DalRules.GetSignature();
        }
        #endregion

        #region overloads

        /// <summary>
        /// Overridden to assign document to preview control only after the 
        /// initial activation.
        /// </summary>
        /// <param name="e"><see cref="EventArgs"/> that contains the event data.</param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (this.imgSignature == null)
            {
                CommFunc.MsgErr("Can not get signature.");
                this.Close();
            }
            else
            {
                if (string.IsNullOrEmpty(this.strSelPrinter))
                {
                    using (var dlg = new frmPrintSetting())
                    {
                        // show dialog
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            this.currItemIndex = 1;
                            this.strSelPrinter = dlg.SelPrinter;
                            this.mainTray = dlg.MainTray;
                            this.altTray = dlg.AltTray;
                            this.printdoc.DefaultPageSettings.PrinterSettings.PrinterName = this.strSelPrinter;
                        }
                    }
                }

                if (this.printdoc != null)
                    this._preview.PrintDoc = this.printdoc;
            }
        }

        /// <summary>
        /// Overridden to cancel any ongoing previews when closing form.
        /// </summary>
        /// <param name="e"><see cref="FormClosingEventArgs"/> that contains the event data.</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (this._preview.IsRendering && !e.Cancel)
            {
                this._preview.Cancel();
            }
        }

        #endregion

        #region main commands

        protected void btnPrint_Click(object sender, EventArgs e)        
        {
            if (string.IsNullOrEmpty(this.strSelPrinter))
            {
                using (var dlg = new frmPrintSetting())
                {
                    // show dialog
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    { 
                        this.strSelPrinter = dlg.SelPrinter;
                        this.mainTray = dlg.MainTray;
                        this.altTray = dlg.AltTray;
                        this.printdoc.DefaultPageSettings.PrinterSettings.PrinterName = this.strSelPrinter;
                    }
                }
            }

            this.currItemIndex = 1;
            // print selected page range
            this._preview.Print();
            string strRes = DalRules.DoPrintChequeNo(strBatchNo, GlobalParam.Inst.gsUserID);
            if (!string.IsNullOrEmpty(strRes))
            {
                CommFunc.MsgErr(strRes);
            }
            else
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        protected void btnPageSetup_Click(object sender, EventArgs e)
        {
            using (var dlg = new PageSetupDialog())
            {
                dlg.Document = this.printdoc;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    //// to show new page layout
                    this._preview.RefreshPreview();
                }
            }
        }

        protected void printDoc_QueryPageSettings(object sender, QueryPageSettingsEventArgs e)
        {
            if (this.currPage == 1)
            {
                e.PageSettings.PaperSource = this.printdoc.PrinterSettings.PaperSources[this.mainTray];
            }
            else
            {
                e.PageSettings.PaperSource = this.printdoc.PrinterSettings.PaperSources[this.altTray];
            }
        }

        protected void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            ChequeItem currItem = this.dicPrintItem[this.currItemIndex];

            if (this.currPage == 1)
            {
                e.Graphics.DrawString(currItem.TxNo.PadLeft(8, '0'), this.printFont, Brushes.Red, currItem.RcTxNo);

#if DEBUG
                e.Graphics.DrawString(currItem.ChequeNo.PadLeft(8, '0'), this.printFont, Brushes.Red, currItem.RcChequeNo);
#endif
                e.Graphics.DrawString(currItem.BatchNo, this.printFont, Brushes.Red, currItem.RcBatchNo);
                string strTotalAmt = NumToWord.changeCurrencyToWords(currItem.TotalAmt);
                e.Graphics.DrawString(strTotalAmt, this.printFont, Brushes.Red, currItem.RcTotalAmt);
                Rectangle rc = currItem.RcListDetail;
                foreach (var item in currItem.dicDetail[this.currPage])
                {
                    e.Graphics.DrawString(item, this.printFont, Brushes.Black, rc);
                    rc.Y += rc.Height;
                }

                e.Graphics.DrawImage(this.imgSignature, currItem.RcSignature);
            }
            else
            {
                string pageH_TxNo = string.Format("Tx NO:{0}", currItem.TxNo);
                SizeF measureH_TxNo = e.Graphics.MeasureString(pageH_TxNo, new Font("Arial", 11f));
                PointF pointH_TxNo = new PointF(e.MarginBounds.Right - measureH_TxNo.Width, e.MarginBounds.Top - measureH_TxNo.Height);
                //TODO:
                //  e.Graphics.DrawString(pageH_TxNo, new Font("Arial", 11f), Brushes.Gray, pointH_TxNo);
                e.Graphics.DrawString(pageH_TxNo, new Font("Arial", 11f), Brushes.Gray, currItem.RcTxNo);
                string pageH_BatchNo = string.Format("Batch NO:{0}", currItem.BatchNo);
                SizeF measureH_BatchNo = e.Graphics.MeasureString(pageH_BatchNo, new Font("Arial", 11f));
                PointF pointH_BatchNo = new PointF(e.MarginBounds.Left, e.MarginBounds.Top - measureH_BatchNo.Height);
                //TODO:
                //  e.Graphics.DrawString(pageH_BatchNo, new Font("Arial", 11f), Brushes.Gray, pointH_BatchNo);
                e.Graphics.DrawString(pageH_BatchNo, new Font("Arial", 11f), Brushes.Gray, currItem.RcBatchNo);

                //TODO:
                // Rectangle rc = e.MarginBounds;
                Rectangle rc = currItem.RcListDetail;
                rc.Y = rc.Y + this.printFont.Height;
                rc.Height = this.printFont.Height + 10;

                foreach (var item in currItem.dicDetail[this.currPage])
                {
                    e.Graphics.DrawString(item, this.printFont, Brushes.Black, rc);
                    rc.Y += rc.Height;
                }

                rc.Y = e.MarginBounds.Bottom - 10;
                //TODO: Add Page Count Indicate
                string pageText = string.Format("Page {0}/{1}",
                    this.currPage, this.dicPrintItem[this.currItemIndex].dicDetail.Count);
                e.Graphics.DrawString(pageText, new Font("Arial", 11f), Brushes.Gray, rc);
            }

            //TODO:
            if (!string.IsNullOrEmpty(this.strSelPrinter))
            {
                //// move on to next page
                if (this.currPage == this.dicPrintItem[this.currItemIndex].dicDetail.Count)
                {
                    this.currItemIndex++;
                    this.currPage = 1;
                    e.HasMorePages = this.currItemIndex <= this.dicPrintItem.Count;
                }
                else if (this.currPage < this.dicPrintItem[this.currItemIndex].dicDetail.Count)
                {
                    this.currPage++;
                    e.HasMorePages = true;
                }
            }
            else
            {
                this.currItemIndex++;
                this.currPage = 1;
                e.HasMorePages = this.currItemIndex <= this.dicPrintItem.Count;
            }
        }
        #endregion

        #region zoom

        protected void btnZoom_ButtonClick(object sender, EventArgs e)
        {
            this._preview.ZoomMode = this._preview.ZoomMode == ZoomMode.ActualSize
                ? ZoomMode.FullPage
                : ZoomMode.ActualSize;
        }
        protected void btnZoom_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == tsmiZoomActualSize)
            {
                this._preview.ZoomMode = ZoomMode.ActualSize;
            }
            else if (e.ClickedItem == tsmiZoomFullPage)
            {
                this._preview.ZoomMode = ZoomMode.FullPage;
            }
            else if (e.ClickedItem == tsmiZoomPageWidth)
            {
                this._preview.ZoomMode = ZoomMode.PageWidth;
            }
            else if (e.ClickedItem == tsmiZoomTwoPages)
            {
                this._preview.ZoomMode = ZoomMode.TwoPages;
            }
            if (e.ClickedItem == tsmiZoom10)
            {
                this._preview.Zoom = .1;
            }
            else if (e.ClickedItem == tsmiZoom100)
            {
                this._preview.Zoom = 1;
            }
            else if (e.ClickedItem == tsmiZoom150)
            {
                this._preview.Zoom = 1.5;
            }
            else if (e.ClickedItem == tsmiZoom200)
            {
                this._preview.Zoom = 2;
            }
            else if (e.ClickedItem == tsmiZoom25)
            {
                this._preview.Zoom = .25;
            }
            else if (e.ClickedItem == tsmiZoom50)
            {
                this._preview.Zoom = .5;
            }
            else if (e.ClickedItem == tsmiZoom500)
            {
                this._preview.Zoom = 5;
            }
            else if (e.ClickedItem == tsmiZoom75)
            {
                this._preview.Zoom = .75;
            }
        }
        #endregion

        #region page navigation

        protected void btnFirst_Click(object sender, EventArgs e)
        {
            this._preview.StartPage = 0;
        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            this._preview.StartPage--;
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            this._preview.StartPage++;
        }
        protected void btnLast_Click(object sender, EventArgs e)
        {
            this._preview.StartPage = this._preview.PageCount - 1;
        }
        protected void txtStartPage_Enter(object sender, EventArgs e)
        {
            txtStartPage.SelectAll();
        }
        protected void txtStartPage_Validating(object sender, CancelEventArgs e)
        {
            this.CommitPageNumber();
        }
        protected void txtStartPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            var c = e.KeyChar;
            if (c == (char)13)
            {
                this.CommitPageNumber();
                e.Handled = true;
            }
            else if (c > ' ' && !char.IsDigit(c))
            {
                e.Handled = true;
            }
        }
        protected void CommitPageNumber()
        {
            int page;
            if (int.TryParse(txtStartPage.Text, out page))
            {
                this._preview.StartPage = page - 1;
            }
        }
        protected void _preview_StartPageChanged(object sender, EventArgs e)
        {
            var page = this._preview.StartPage + 1;
            txtStartPage.Text = page.ToString();
        }
        private void _preview_PageCountChanged(object sender, EventArgs e)
        {
            this.Update();
            Application.DoEvents();
            lblPageCount.Text = string.Format("of {0}", this._preview.PageCount);
        }

        #endregion

        #region job control

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (this._preview.IsRendering)
            {
                this._preview.Cancel();
            }
            else
            {
                Close();
            }
        }

        protected void PrintDoc_BeginPrint(object sender, PrintEventArgs e)
        {
            btnCancel.Text = "&Cancel";
            btnPrint.Enabled = btnPageSetup.Enabled = false;
        }

        protected void PrintDoc_EndPrint(object sender, PrintEventArgs e)
        {
            btnCancel.Text = "&Close";
            btnPrint.Enabled = btnPageSetup.Enabled = true;
        }

        #endregion
    }

    /// <summary>
    /// Specifies the zoom mode for the <see cref="ucEnhancedPrintPreview"/> control.
    /// </summary>
    internal enum ZoomMode
    {
        /// <summary>
        /// Show the preview in actual size.
        /// </summary>
        ActualSize,

        /// <summary>
        /// Show a full page.
        /// </summary>
        FullPage,

        /// <summary>
        /// Show a full page width.
        /// </summary>
        PageWidth,

        /// <summary>
        /// Show two full pages.
        /// </summary>
        TwoPages,

        /// <summary>
        /// Use the zoom factor specified by the <see cref="ucEnhancedPrintPreview.Zoom"/> property.
        /// </summary>
        Custom
    }

    /// <summary>
    /// Represents a preview of one or two pages in a <see cref="PrintDocument"/>.
    /// </summary>
    /// <remarks>
    /// This control is similar to the standard <see cref="PrintPreviewControl"/> but
    /// it displays pages as they are rendered. By contrast, the standard control 
    /// waits until the entire document is rendered before it displays anything.
    /// </remarks>
    internal class ucEnhancedPrintPreview : UserControl
    {
        #region fields
        private const int MARGIN = 4;

        private PrintDocument mPrintDoc;
        private ZoomMode mZoomMode;
        private double mZoom;
        private int mStartPage;
        private Brush mBackBrush;
        private Point ptLast;
        private PointF ptfHimm2pix = new PointF(-1, -1);
        private List<Image> listImg = new List<Image>();
        private bool bCancel, bRendering;

        #endregion

        #region ctor

        /// <summary>
        /// Initializes a new instance of a <see cref="ucEnhancedPrintPreview"/> control.
        /// </summary>
        public ucEnhancedPrintPreview()
        {
            this.BackColor = SystemColors.AppWorkspace;
            this.ZoomMode = ZoomMode.FullPage;
            this.StartPage = 0;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        #endregion

        #region object model

        /// <summary>
        /// Gets or sets the <see cref="PrintDocument"/> being previewed.
        /// </summary>
        public PrintDocument PrintDoc
        {
            get
            {
                return this.mPrintDoc;
            }
            set
            {
                if (value != this.mPrintDoc)
                {
                    this.mPrintDoc = value;
                    this.RefreshPreview();
                }
            }
        }

        /// <summary>
        /// Regenerates the preview to reflect changes in the document layout.
        /// </summary>
        public void RefreshPreview()
        {
            // render into PrintController
            if (this.mPrintDoc != null)
            {
                // prepare to render preview document
                this.listImg.Clear();
                PrintController savePC = this.mPrintDoc.PrintController;

                // render preview document
                try
                {
                    this.bCancel = false;
                    this.bRendering = true;
                    this.mPrintDoc.PrintController = new PreviewPrintController();
                    this.mPrintDoc.PrintPage += this.PrintDoc_PrintPage;
                    this.mPrintDoc.EndPrint += this.PrintDoc_EndPrint;
                    this.mPrintDoc.Print();
                }
                finally
                {
                    this.bCancel = false;
                    this.bRendering = false;
                    this.mPrintDoc.PrintPage -= this.PrintDoc_PrintPage;
                    this.mPrintDoc.EndPrint -= this.PrintDoc_EndPrint;
                    this.mPrintDoc.PrintController = savePC;
                }
            }

            // update
            this.OnPageCountChanged(EventArgs.Empty);
            this.UpdatePreview();
            this.UpdateScrollBars();
        }

        /// <summary>
        /// Stops rendering the <see cref="PrintDoc"/>.
        /// </summary>
        public void Cancel()
        {
            this.bCancel = true;
        }

        /// <summary>
        /// Gets a value that indicates whether the <see cref="PrintDoc"/> is being rendered.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsRendering
        {
            get { return this.bRendering; }
        }

        /// <summary>
        /// Gets or sets how the zoom should be adjusted when the control is resized.
        /// </summary>
        [DefaultValue(ZoomMode.FullPage)]
        public ZoomMode ZoomMode
        {
            get
            {
                return this.mZoomMode;
            }
            set
            {
                if (value != this.mZoomMode)
                {
                    this.mZoomMode = value;
                    this.UpdateScrollBars();
                    this.OnZoomModeChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets a custom zoom factor used when the <see cref="ZoomMode"/> property
        /// is set to <b>Custom</b>.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public double Zoom
        {
            get
            {
                return this.mZoom;
            }
            set
            {
                if (value != this.mZoom || ZoomMode != ZoomMode.Custom)
                {
                    ZoomMode = ZoomMode.Custom;
                    this.mZoom = value;
                    this.UpdateScrollBars();
                    this.OnZoomModeChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets the first page being previewed.
        /// </summary>
        /// <remarks>
        /// There may be one or two pages visible depending on the setting of the
        /// <see cref="ZoomMode"/> property.
        /// </remarks>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int StartPage
        {
            get
            {
                return this.mStartPage;
            }
            set
            {
                // validate new setting
                if (value > this.PageCount - 1) value = this.PageCount - 1;
                if (value < 0) value = 0;

                // apply new setting
                if (value != this.mStartPage)
                {
                    this.mStartPage = value;
                    this.UpdateScrollBars();
                    this.OnStartPageChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets the number of pages available for preview.
        /// </summary>
        /// <remarks>
        /// This number increases as the document is rendered into the control.
        /// </remarks>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PageCount
        {
            get { return this.listImg.Count; }
        }

        /// <summary>
        /// Gets or sets the control's background color.
        /// </summary>
        [DefaultValue(typeof(Color), "AppWorkspace")]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                this.mBackBrush = new SolidBrush(value);
            }
        }

        /// <summary>
        /// Gets a list containing the images of the pages in the document.
        /// </summary>
        [Browsable(false)]
        public List<Image> PageImages
        {
            get { return this.listImg; }
        }

        /// <summary>
        /// Prints the current document honoring the selected page range.
        /// </summary>
        public void Print()
        {
            // select pages to print
            var ps = this.mPrintDoc.PrinterSettings;
            int first = ps.MinimumPage - 1;
            int last = ps.MaximumPage - 1;
            switch (ps.PrintRange)
            {
                case PrintRange.AllPages:
                    this.PrintDoc.Print();
                    return;
                case PrintRange.CurrentPage:
                    first = last = this.StartPage;
                    break;
                case PrintRange.Selection:
                    first = last = this.StartPage;
                    if (ZoomMode == ZoomMode.TwoPages)
                    {
                        last = Math.Min(first + 1, this.PageCount - 1);
                    }
                    break;
                case PrintRange.SomePages:
                    first = ps.FromPage - 1;
                    last = ps.ToPage - 1;
                    break;
            }

            // print using helper class
            var dp = new DocumentPrinter(this, first, last);
            dp.Print();
        }

        #endregion

        #region events

        /// <summary>
        /// Occurs when the value of the <see cref="StartPage"/> property changes.
        /// </summary>
        public event EventHandler StartPageChanged;

        /// <summary>
        /// Raises the <see cref="StartPageChanged"/> event.
        /// </summary>
        /// <param name="e"><see cref="EventArgs"/> that provides the event data.</param>
        protected void OnStartPageChanged(EventArgs e)
        {
            if (this.StartPageChanged != null)
            {
                this.StartPageChanged(this, e);
            }
        }

        /// <summary>
        /// Occurs when the value of the <see cref="PageCount"/> property changes.
        /// </summary>
        public event EventHandler PageCountChanged;

        /// <summary>
        /// Raises the <see cref="PageCountChanged"/> event.
        /// </summary>
        /// <param name="e"><see cref="EventArgs"/> that provides the event data/</param>
        protected void OnPageCountChanged(EventArgs e)
        {
            if (this.PageCountChanged != null)
            {
                this.PageCountChanged(this, e);
            }
        }

        /// <summary>
        /// Occurs when the value of the <see cref="ZoomMode"/> property changes.
        /// </summary>
        public event EventHandler ZoomModeChanged;

        /// <summary>
        /// Raises the <see cref="ZoomModeChanged"/> event.
        /// </summary>
        /// <param name="e"><see cref="EventArgs"/> that contains the event data.</param>
        protected void OnZoomModeChanged(EventArgs e)
        {
            if (this.ZoomModeChanged != null)
            {
                this.ZoomModeChanged(this, e);
            }
        }

        #endregion

        #region overrides

        // painting
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // we're painting it all, so don't call base class
            //base.OnPaintBackground(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Image img = this.GetImage(this.StartPage);
            if (img != null)
            {
                Rectangle rc = this.GetImageRectangle(img);
                if (rc.Width > 2 && rc.Height > 2)
                {
                    // adjust for scrollbars
                    rc.Offset(AutoScrollPosition);

                    // render single page
                    if (this.mZoomMode != ZoomMode.TwoPages)
                    {
                        this.RenderPage(e.Graphics, img, rc);
                    }
                    else
                    {
                        //// render two pages
                        // render first page
                        rc.Width = (rc.Width - MARGIN) / 2;
                        this.RenderPage(e.Graphics, img, rc);

                        // render second page
                        img = this.GetImage(this.StartPage + 1);
                        if (img != null)
                        {
                            // update bounds in case orientation changed
                            rc = this.GetImageRectangle(img);
                            rc.Width = (rc.Width - MARGIN) / 2;

                            // render second page
                            rc.Offset(rc.Width + MARGIN, 0);
                            this.RenderPage(e.Graphics, img, rc);
                        }
                    }
                }
            }

            // paint background
            e.Graphics.FillRectangle(this.mBackBrush, ClientRectangle);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            this.UpdateScrollBars();
            base.OnSizeChanged(e);
        }

        // pan by dragging preview pane
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left && AutoScrollMinSize != Size.Empty)
            {
                Cursor = Cursors.NoMove2D;
                this.ptLast = new Point(e.X, e.Y);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left && Cursor == Cursors.NoMove2D)
                Cursor = Cursors.Default;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (Cursor == Cursors.NoMove2D)
            {
                int dx = e.X - this.ptLast.X;
                int dy = e.Y - this.ptLast.Y;
                if (dx != 0 || dy != 0)
                {
                    Point pt = AutoScrollPosition;
                    AutoScrollPosition = new Point(-(pt.X + dx), -(pt.Y + dy));
                    this.ptLast = new Point(e.X, e.Y);
                }
            }
        }

        // keyboard support
        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Right:
                case Keys.Down:
                case Keys.PageUp:
                case Keys.PageDown:
                case Keys.Home:
                case Keys.End:
                    return true;
            }
            return base.IsInputKey(keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Handled)
                return;

            switch (e.KeyCode)
            {
                // arrow keys scroll or browse, depending on ZoomMode
                case Keys.Left:
                case Keys.Up:
                case Keys.Right:
                case Keys.Down:

                    // browse
                    if (ZoomMode == ZoomMode.FullPage || ZoomMode == ZoomMode.TwoPages)
                    {
                        switch (e.KeyCode)
                        {
                            case Keys.Left:
                            case Keys.Up:
                                this.StartPage--;
                                break;
                            case Keys.Right:
                            case Keys.Down:
                                this.StartPage++;
                                break;
                        }
                        break;
                    }

                    // scroll
                    Point pt = AutoScrollPosition;
                    switch (e.KeyCode)
                    {
                        case Keys.Left:
                            pt.X += 20;
                            break;
                        case Keys.Right:
                            pt.X -= 20;
                            break;
                        case Keys.Up:
                            pt.Y += 20;
                            break;
                        case Keys.Down:
                            pt.Y -= 20;
                            break;
                    }

                    AutoScrollPosition = new Point(-pt.X, -pt.Y);
                    break;

                // page up/down browse pages
                case Keys.PageUp:
                    this.StartPage--;
                    break;
                case Keys.PageDown:
                    this.StartPage++;
                    break;

                // home/end 
                case Keys.Home:
                    AutoScrollPosition = Point.Empty;
                    this.StartPage = 0;
                    break;
                case Keys.End:
                    AutoScrollPosition = Point.Empty;
                    this.StartPage = this.PageCount - 1;
                    break;

                default:
                    return;
            }

            // if we got here, the event was handled
            e.Handled = true;
        }

        #endregion

        #region implementation

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            this.SyncPageImages(false);
            if (this.bCancel)
            {
                e.Cancel = true;
            }
        }

        private void PrintDoc_EndPrint(object sender, PrintEventArgs e)
        {
            this.SyncPageImages(true);
        }

        private void SyncPageImages(bool lastPageReady)
        {
            var pv = (PreviewPrintController)this.mPrintDoc.PrintController;
            if (pv != null)
            {
                var pageInfo = pv.GetPreviewPageInfo();
                int count = lastPageReady ? pageInfo.Length : pageInfo.Length - 1;
                for (int i = this.listImg.Count; i < count; i++)
                {
                    var img = pageInfo[i].Image;
                    this.listImg.Add(img);

                    this.OnPageCountChanged(EventArgs.Empty);

                    if (this.StartPage < 0) this.StartPage = 0;
                    if (i == this.StartPage || i == this.StartPage + 1)
                    {
                        Refresh();
                    }

                    Application.DoEvents();
                }
            }
        }

        private Image GetImage(int page)
        {
            return page > -1 && page < this.PageCount ? this.listImg[page] : null;
        }

        private Rectangle GetImageRectangle(Image img)
        {
            // start with regular image rectangle
            Size sz = this.GetImageSizeInPixels(img);
            Rectangle rc = new Rectangle(0, 0, sz.Width, sz.Height);

            // calculate zoom
            Rectangle rcCli = this.ClientRectangle;
            switch (this.mZoomMode)
            {
                case ZoomMode.ActualSize:
                    this.mZoom = 1;
                    break;
                case ZoomMode.TwoPages:
                    rc.Width *= 2; // << two pages side-by-side
                    goto case ZoomMode.FullPage;
                case ZoomMode.FullPage:
                    double zoomX = (rc.Width > 0) ? rcCli.Width / (double)rc.Width : 0;
                    double zoomY = (rc.Height > 0) ? rcCli.Height / (double)rc.Height : 0;
                    this.mZoom = Math.Min(zoomX, zoomY);
                    break;
                case ZoomMode.PageWidth:
                    this.mZoom = (rc.Width > 0) ? rcCli.Width / (double)rc.Width : 0;
                    break;
            }

            // apply zoom factor
            rc.Width = (int)(rc.Width * this.mZoom);
            rc.Height = (int)(rc.Height * this.mZoom);

            // center image
            int dx = (rcCli.Width - rc.Width) / 2;
            if (dx > 0) rc.X += dx;
            int dy = (rcCli.Height - rc.Height) / 2;
            if (dy > 0) rc.Y += dy;

            // add some extra space
            rc.Inflate(-MARGIN, -MARGIN);
            if (this.mZoomMode == ZoomMode.TwoPages)
            {
                rc.Inflate(-MARGIN / 2, 0);
            }

            // done
            return rc;
        }

        private Size GetImageSizeInPixels(Image img)
        {
            // get image size
            SizeF szf = img.PhysicalDimension;

            // if it is a metafile, convert to pixels
            if (img is Metafile)
            {
                // get screen resolution
                if (this.ptfHimm2pix.X < 0)
                {
                    using (Graphics g = this.CreateGraphics())
                    {
                        this.ptfHimm2pix.X = g.DpiX / 2540f;
                        this.ptfHimm2pix.Y = g.DpiY / 2540f;
                    }
                }

                // convert himetric to pixels
                szf.Width *= this.ptfHimm2pix.X;
                szf.Height *= this.ptfHimm2pix.Y;
            }

            // done
            return Size.Truncate(szf);
        }

        private void RenderPage(Graphics g, Image img, Rectangle rc)
        {
            // draw the page
            rc.Offset(1, 1);
            g.DrawRectangle(Pens.Black, rc);
            rc.Offset(-1, -1);
            g.FillRectangle(Brushes.White, rc);
            g.DrawImage(img, rc);
            g.DrawRectangle(Pens.Black, rc);

            // exclude cliprect to paint background later
            rc.Width++;
            rc.Height++;
            g.ExcludeClip(rc);
            rc.Offset(1, 1);
            g.ExcludeClip(rc);
        }

        private void UpdateScrollBars()
        {
            // get image rectangle to adjust scroll size
            Rectangle rc = Rectangle.Empty;
            Image img = this.GetImage(this.StartPage);
            if (img != null)
            {
                rc = this.GetImageRectangle(img);
            }

            // calculate new scroll size
            Size scrollSize = new Size(0, 0);
            switch (this.mZoomMode)
            {
                case ZoomMode.PageWidth:
                    scrollSize = new Size(0, rc.Height + (2 * MARGIN));
                    break;
                case ZoomMode.ActualSize:
                case ZoomMode.Custom:
                    scrollSize = new Size(rc.Width + (2 * MARGIN), rc.Height + (2 * MARGIN));
                    break;
            }

            // apply if needed
            if (scrollSize != AutoScrollMinSize)
            {
                AutoScrollMinSize = scrollSize;
            }

            // ready to update
            this.UpdatePreview();
        }

        private void UpdatePreview()
        {
            // validate current page
            if (this.mStartPage < 0) this.mStartPage = 0;
            if (this.mStartPage > this.PageCount - 1) this.mStartPage = this.PageCount - 1;

            // repaint
            this.Invalidate();
        }

        #endregion

        #region nested class

        // helper class that prints the selected page range in a PrintDocument.
        internal class DocumentPrinter : PrintDocument
        {
            private int mFirst, mLast, mIndex;
            private List<Image> mImgList;

            public DocumentPrinter(ucEnhancedPrintPreview preview, int first, int last)
            {
                // save page range and image list
                this.mFirst = first;
                this.mLast = last;
                this.mImgList = preview.PageImages;

                // copy page and printer settings from original document
                DefaultPageSettings = preview.PrintDoc.DefaultPageSettings;
                PrinterSettings = preview.PrintDoc.PrinterSettings;
            }

            protected override void OnBeginPrint(PrintEventArgs e)
            {
                // start from the first page
                this.mIndex = this.mFirst;
            }
            protected override void OnPrintPage(PrintPageEventArgs e)
            {
                // render the current page and increment the index
                e.Graphics.PageUnit = GraphicsUnit.Display;
                e.Graphics.DrawImage(this.mImgList[this.mIndex++], e.PageBounds);

                // stop when we reach the last page in the range
                e.HasMorePages = this.mIndex <= this.mLast;
            }
        }

        #endregion
    }
}
