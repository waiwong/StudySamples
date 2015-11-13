using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BWHITD.Sys.Common;
using BWHITD.Win.Lib;
using Microsoft.Reporting.WinForms;

namespace DemoForAIA
{
    public partial class frmRptViewer : frmBase
    {
        private string strRptPath = string.Empty;
        private Dictionary<string, string> dicParam = new Dictionary<string, string>();
        private Dictionary<string, string> dicDSNameWithSQL = new Dictionary<string, string>();
        private Dictionary<string, DataTable> dicDSNameWithData = new Dictionary<string, DataTable>();

        public frmRptViewer(string strTitle, string pRptPath, Dictionary<string, string> dicDSNameWithSQL)
            : this(strTitle, pRptPath, dicDSNameWithSQL, null, null) { }

        public frmRptViewer(string strTitle, string pRptPath, Dictionary<string, DataTable> dicDSNameWithData)
            : this(strTitle, pRptPath, null, dicDSNameWithData, null) { }

        public frmRptViewer(string strTitle, string pRptPath, Dictionary<string, string> dicDSNameWithSQL, Dictionary<string, string> pdicParam)
            : this(strTitle, pRptPath, dicDSNameWithSQL, null, pdicParam) { }

        public frmRptViewer(string strTitle, string pRptPath, Dictionary<string, DataTable> dicDSNameWithData, Dictionary<string, string> pdicParam)
            : this(strTitle, pRptPath, null, dicDSNameWithData, pdicParam) { }

        public frmRptViewer(string strTitle, string pRptPath, Dictionary<string, string> pdicDSNameWithSQL,
             Dictionary<string, DataTable> pdicDSNameWithData, Dictionary<string, string> pdicParam)
        {
            InitializeComponent();

            this.Text = strTitle;
            this.strRptPath = pRptPath;

            if (pdicDSNameWithSQL != null)
                this.dicDSNameWithSQL = pdicDSNameWithSQL;
            if (pdicDSNameWithData != null)
                this.dicDSNameWithData = pdicDSNameWithData;
            if (pdicParam != null)
                this.dicParam = pdicParam;
        }

        private void frmRptViewer_Load(object sender, EventArgs e)
        {
            Waiting.Show("Preparing to generate report");
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(this.strRptPath) || (this.dicDSNameWithSQL.Count == 0 && this.dicDSNameWithData.Count == 0))
                {
                    CommFunc.MsgInfo("The report path and report data should assign value!");
                    this.Close();
                }
                else
                {
                    this.rptViewer.Reset();
                    this.rptViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                    this.rptViewer.LocalReport.ReportPath = this.strRptPath;
                    this.rptViewer.LocalReport.DataSources.Clear();

                    if (this.dicDSNameWithSQL.Count > 0)
                    {
                        foreach (KeyValuePair<string, string> itemDs in this.dicDSNameWithSQL)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rptDS = new Microsoft.Reporting.WinForms.ReportDataSource();
                            rptDS.Name = itemDs.Key;
                            rptDS.Value = GlobalParam.Inst.DBI.GetDataTable(itemDs.Value);
                            this.rptViewer.LocalReport.DataSources.Add(rptDS);
                        }
                    }

                    if (this.dicDSNameWithData.Count > 0)
                    {
                        foreach (KeyValuePair<string, DataTable> itemDs in this.dicDSNameWithData)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rptDS = new Microsoft.Reporting.WinForms.ReportDataSource();
                            rptDS.Name = itemDs.Key;
                            rptDS.Value = itemDs.Value;
                            this.rptViewer.LocalReport.DataSources.Add(rptDS);
                        }
                    }

                    if (this.dicParam.Count > 0)
                    {
                        ReportParameter[] param = new ReportParameter[this.dicParam.Count];
                        int i = 0;
                        foreach (KeyValuePair<string, string> item in this.dicParam)
                        {
                            param[i] = new Microsoft.Reporting.WinForms.ReportParameter(item.Key, item.Value);
                            i += 1;
                        }

                        this.rptViewer.LocalReport.SetParameters(param);
                    }
                    this.rptViewer.RefreshReport();
                }
            }
            catch (Exception ex)
            {
                Waiting.Close();
                CommFunc.MsgErr(ex);
                this.Close();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void frmRptViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Waiting.CloseAll();
            }
            catch
            {
            }
        }

        private void rptViewer_RenderingBegin(object sender, CancelEventArgs e)
        {
            try
            {
                Waiting.CloseAll();
            }
            catch
            {
            }
        }
    }
}
