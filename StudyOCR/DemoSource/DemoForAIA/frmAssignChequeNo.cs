using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BWHITD.Sys.Common;
using BWHITD.Win.Lib;

namespace DemoForAIA
{
    public partial class frmAssignChequeNo : frmBase
    {
        private DataTable dtBatchInfo = new DataTable();

        public frmAssignChequeNo()
        {
            InitializeComponent();
        }

        private void frmAssignChequeNo_Load(object sender, EventArgs e)
        {
            this.InitData();
        }

        private void InitData()
        {
            string sql = "SELECT ISNULL(MAX(CAST(ChequeNo AS INT)), 0) + 1 FROM dbo.Record_Print";
            this.mtxtChequeNo.Text = GlobalParam.Inst.DBI.ExecScalar(sql).ToString();
            this.dtBatchInfo = DalRules.GetBatchInfo(clsConst.BatchStatus.New);
            this.cmbBatchNo.DataSource = this.dtBatchInfo;
            this.cmbBatchNo.DisplayMember = "C_BatchNo";
            this.cmbBatchNo.ValueMember = "C_BatchNo";

            this.cmbBatchNo.SelectedIndexChanged += new System.EventHandler(this.cmbBatchNo_SelectedIndexChanged);
            if (this.cmbBatchNo.Items.Count > 0)
            {
                this.cmbBatchNo.SelectedIndex = 0;
                this.cmbBatchNo_SelectedIndexChanged(null, null);
            }
            else
            {
                this.cmbBatchNo.Text = string.Empty;
            }
        }

        private void btnComfirm_Click(object sender, EventArgs e)
        {
            string strSelBatchNo = this.cmbBatchNo.Text;

            if (string.IsNullOrEmpty(strSelBatchNo))
            {
                CommFunc.MsgInfo("Please select the Batch No.!");
                return;
            }

            if (string.IsNullOrEmpty(this.mtxtChequeNo.Text))
            {
                CommFunc.MsgInfo("Please set the Cheque No.!");
                return;
            }

            Dictionary<string, string> pDicTxNoAndChequeNo = new Dictionary<string, string>();

            int startChequeNo = Convert.ToInt32(this.mtxtChequeNo.Text);

            DataTable dtBatchDetail = DalRules.GetBatchTxNo(strSelBatchNo);
            foreach (DataRow dr in dtBatchDetail.Rows)
            {
                pDicTxNoAndChequeNo.Add(dr["TX_ref_no"].ToString(), startChequeNo.ToString());
                startChequeNo++;
            }

            string strRes = DalRules.UpdateAssignChequeNo(strSelBatchNo, pDicTxNoAndChequeNo, GlobalParam.Inst.gsUserID);
            if (string.IsNullOrEmpty(strRes))
            {
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

                this.InitData();
            }
            else
                CommFunc.MsgErr(strRes);
        }

        private void cmbBatchNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbBatchNo.SelectedIndex >= 0)
            {
                string strSelBatchNo = this.cmbBatchNo.Text;
                DataRow findRow = this.dtBatchInfo.Rows.Find(strSelBatchNo);
                if (findRow != null)
                {
                    this.txtCCY.Text = findRow["C_CCY"].ToString();
                    this.mtxtCheckItems.Text = findRow["C_Items"].ToString();
                    this.txtTotalAmt.Text = findRow["C_TotalAmt"].ToString();
                }
                else
                {
                    CommFunc.MsgInfo(string.Format("Can not get detail infomation of select batch No:{0}", strSelBatchNo));
                }
            }
        }
    }
}
