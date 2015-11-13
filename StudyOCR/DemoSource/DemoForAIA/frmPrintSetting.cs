using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BWHITD.Win.Lib;

namespace DemoForAIA
{
    public partial class frmPrintSetting : frmBase
    {
        public string SelPrinter { get; private set; }
        public int MainTray { get; private set; }
        public int AltTray { get; private set; }

        public frmPrintSetting()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (this.cmbPrinter.SelectedIndex > -1)
            {
                this.SelPrinter = this.cmbPrinter.Text;
            }
            else
            {
                CommFunc.MsgInfo("Please Select Printer!");
                this.cmbPrinter.Focus();
                return;
            }

            if (this.cmbMainTray.SelectedIndex > -1)
            {
                this.MainTray = Convert.ToInt16(this.cmbMainTray.SelectedValue);
            }
            else
            {
                CommFunc.MsgInfo("Please Select Main Tray!");
                this.cmbMainTray.Focus();
                return;
            }

            if (this.cmbAltTray.SelectedIndex > -1)
            {
                this.AltTray = Convert.ToInt16(this.cmbAltTray.SelectedValue);
            }
            else
            {
                CommFunc.MsgInfo("Please Select Alt Tray!");
                this.cmbAltTray.Focus();
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void cmbPrinterList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbPrinter.SelectedIndex > -1)
            {   
                PrinterSettings ps = new PrinterSettings();
                ps.PrinterName = this.cmbPrinter.Text;
                if (ps.IsValid)
                {
                    Dictionary<int, string> dicMainTray = new Dictionary<int, string>();
                    Dictionary<int, string> dicAltTray = new Dictionary<int, string>();

                    for (int i = 0; i < ps.PaperSources.Count; i++)
                    {
                        PaperSource currentPaperSource = ps.PaperSources[i];
                        dicMainTray.Add(i, currentPaperSource.SourceName);
                        dicAltTray.Add(i, currentPaperSource.SourceName);
                    }

                    this.cmbMainTray.DataSource = new BindingSource(dicMainTray, null);
                    this.cmbMainTray.DisplayMember = "Value";
                    this.cmbMainTray.ValueMember = "Key";

                    this.cmbAltTray.DataSource = new BindingSource(dicAltTray, null);
                    this.cmbAltTray.DisplayMember = "Value";
                    this.cmbAltTray.ValueMember = "Key";
                }
                else
                {
                    this.cmbMainTray.DataSource = null;
                    this.cmbAltTray.DataSource = null;
                }
            }
        }

        private void frmPrintSetting_Load(object sender, EventArgs e)
        {
            this.cmbPrinter.Items.Clear();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                this.cmbPrinter.Items.Add(printer);
            }
        }
    }
}
