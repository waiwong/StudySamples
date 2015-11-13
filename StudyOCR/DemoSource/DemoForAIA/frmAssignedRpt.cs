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
    public partial class frmAssignedRpt : frmBase
    {
        private string selBatchNo;
        public frmAssignedRpt() : this(null) { }

        public frmAssignedRpt(string pBatchNo)
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
            DataTable dtBatchInfo = DalRules.GetBatchInfo(clsConst.BatchStatus.Assigned);
            this.cmbBatchNo.DataSource = dtBatchInfo;
            this.cmbBatchNo.DisplayMember = "C_BatchNo";
            this.cmbBatchNo.ValueMember = "C_BatchNo";

            if (!string.IsNullOrEmpty(this.selBatchNo))
            {
                this.cmbBatchNo.Items.Contains(this.selBatchNo);
                this.cmbBatchNo.Text = this.selBatchNo;
                this.btnRePrint.PerformClick();
            }
            else if (this.cmbBatchNo.Items.Count == 0)
            {
                this.cmbBatchNo.Text = string.Empty;
            }
        }

        private void btnRePrint_Click(object sender, EventArgs e)
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
            dicDSNameWithData.Add("dsMain", DalRules.GetAssignChequeNoRpt(strSelBatchNo));
            Dictionary<string, string> dicParam = new Dictionary<string, string>();
            dicParam.Add("BatchNo", strSelBatchNo);
            dicParam.Add("IsReAssign", "0");

            using (var dlg = new frmRptViewer(strTitle, strRptPath, dicDSNameWithData, dicParam))
            {
                dlg.ShowDialog(this);
            }
        }
    }
}
