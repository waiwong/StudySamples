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
    public partial class frmVoidReAssignCheque : frmBase
    {
        public frmVoidReAssignCheque()
        {
            InitializeComponent();
        }

        private void frmRptResult_Load(object sender, EventArgs e)
        {
            this.InitData();
        }

        private void InitData()
        {
            DataTable dtBatchInfo = DalRules.GetBatchInfo(clsConst.BatchStatus.CanVoid);
            this.cmbBatchNo.DataSource = dtBatchInfo;
            this.cmbBatchNo.DisplayMember = "C_BatchNo";
            this.cmbBatchNo.ValueMember = "C_BatchNo";

            this.WindowState = FormWindowState.Maximized;
            this.dgvResult.AutoGenerateColumns = false;

            if (this.cmbBatchNo.Items.Count > 0)
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
            this.RefreshData();
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

                    DataTable dtResult = DalRules.GetVoidChequeDetail(this.cmbBatchNo.Text);

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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cmbBatchNo.Text))
            {
                CommFunc.MsgInfo("Please select the Batch No.!");
            }
            else
            {
                using (var dlg = new frmVoidCheque(this.cmbBatchNo.Text))
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        this.RefreshData();
                    }
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string strSelBatchNo = this.cmbBatchNo.Text;

            if (string.IsNullOrEmpty(strSelBatchNo))
            {
                CommFunc.MsgInfo("Please select the Batch No.!");
                return;
            }

            string strTitle = "Assign Cheque No. Report";
            string strRptPath = "RptRDLC\\AssignChequeRpt.rdlc";
            Dictionary<string, DataTable> dicDSNameWithData = new Dictionary<string, DataTable>();
            dicDSNameWithData.Add("dsMain", DalRules.GetVoidChequeDetail(strSelBatchNo));
            Dictionary<string, string> dicParam = new Dictionary<string, string>();
            dicParam.Add("BatchNo", strSelBatchNo);
            dicParam.Add("IsReAssign", "1");

            using (var dlg = new frmRptViewer(strTitle, strRptPath, dicDSNameWithData, dicParam))
            {
                dlg.ShowDialog(this);
            }
        }

        private void btnVoidAll_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cmbBatchNo.Text))
            {
                CommFunc.MsgInfo("Please select the Batch No.!");
            }
            else if (CommFunc.MsgQue("Please Confirm to continue?"))
            {
                using (var dlg = new frmVoidBatchReAssignChequeNo(this.cmbBatchNo.Text))
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        this.RefreshData();
                    }
                }
            }
        }

        private void cmbBatchNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbBatchNo.SelectedIndex >= 0)
            {
                this.btnRefresh.PerformClick();
            }
        }
    }
}
