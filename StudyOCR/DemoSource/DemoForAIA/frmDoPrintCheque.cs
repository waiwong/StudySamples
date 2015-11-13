using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using BWHITD.Sys.Common;
using BWHITD.Win.Lib;

namespace DemoForAIA
{
    public partial class frmDoPrintCheque : frmBase
    {
        private string selBatchNo;
        public frmDoPrintCheque() : this(null) { }

        public frmDoPrintCheque(string pBatchNo)
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(pBatchNo))
                this.selBatchNo = pBatchNo;
        }

        private void frmRptResult_Load(object sender, EventArgs e)
        {
            this.InitData();
        }

        private void InitData()
        {
            DataTable dtBatchInfo = DalRules.GetBatchInfo(clsConst.BatchStatus.NeedPrint);
            this.cmbBatchNo.DataSource = dtBatchInfo;
            this.cmbBatchNo.DisplayMember = "C_BatchNo";
            this.cmbBatchNo.ValueMember = "C_BatchNo";

            this.WindowState = FormWindowState.Maximized;
            this.dgvResult.AutoGenerateColumns = false;

            if (!string.IsNullOrEmpty(this.selBatchNo))
            {
                this.cmbBatchNo.Items.Contains(this.selBatchNo);
                this.cmbBatchNo.Text = this.selBatchNo;
                this.btnRefresh.PerformClick();
            }
            else if (this.cmbBatchNo.Items.Count > 0)
            {
                this.cmbBatchNo.SelectedIndex = 0;
                this.RefreshData();
            }
            else if (this.cmbBatchNo.Items.Count == 0)
            {
                this.cmbBatchNo.Text = string.Empty;
                this.dgvResult.DataSource = null;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (this.cmbBatchNo.Items.Count == 0)
            {
                this.InitData();
            }
            else
            {
                this.RefreshData();
            }
        }

        private bool RefreshData()
        {
            bool result = false;
            if (string.IsNullOrEmpty(this.cmbBatchNo.Text))
            {
                CommFunc.MsgInfo("Please select the Batch No.!");
            }
            else
            {
                try
                {
                    Waiting.Show("Being processed");

                    DataTable dtResult = DalRules.GetBatchDetail(this.cmbBatchNo.Text);

                    this.dgvResult.DataSource = dtResult;
                    result = true;
                    Waiting.CloseAll();
                }
                catch (Exception ex)
                {
                    Waiting.CloseAll();
                    CommFunc.MsgErr(ex);
                }
            }

            return result;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (this.RefreshData())
            {
                string strSelBatchNo = this.cmbBatchNo.Text;

                int count = 1;
                Dictionary<int, ChequeItem> pDicPrintItem = new Dictionary<int, ChequeItem>();
                DataTable dtBatchDetail = this.dgvResult.DataSource as DataTable;
                foreach (DataRow dr in dtBatchDetail.Rows)
                {
                    string pTxNo = dr["TX_ref_no"].ToString();
                    string pChequeNo = dr["ChequeNo"].ToString();
                    string pBatchNo = dr["C_BatchNo"].ToString();
                    double pTotalAmt = Convert.ToDouble(dr["Amount"]);
                    List<string> pListDetail = new List<string>();
                    string[] strDetail = dr["Detail"].ToString().Split(new string[] { "INV@" }, StringSplitOptions.None);
                    foreach (var item in strDetail)
                    {
                        pListDetail.Add(item);
                    }

                    ChequeItem checkItemIns = new ChequeItem(pTxNo, pChequeNo, pBatchNo, pTotalAmt, pListDetail);
                    pDicPrintItem.Add(count++, checkItemIns);
                }

                using (var dlg = new frmPrintChequeView(strSelBatchNo, pDicPrintItem))
                {
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                        this.InitData();
                }
            }
        }
    }
}
