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
    public partial class frmVoidBatchReAssignChequeNo : frmBase
    {
        private string strBatchNo;
        private DataTable dtBatchInfo = new DataTable();
        public frmVoidBatchReAssignChequeNo(string pBatchNo)
        {
            InitializeComponent();
            this.strBatchNo = pBatchNo;
            this.txtBatchNo.Text = pBatchNo;
            this.txtBatchNo.ReadOnly = true;
        }

        private void frmVoidBatchReAssignChequeNo_Load(object sender, EventArgs e)
        {
            this.InitData();
        }

        private void InitData()
        {
            string sql = "SELECT ISNULL(MAX(CAST(ChequeNo AS INT)), 0) + 1 FROM dbo.Record_Print";
            this.mtxtChequeNo.Text = GlobalParam.Inst.DBI.ExecScalar(sql).ToString();

            this.dtBatchInfo = DalRules.GetBatchInfo(clsConst.BatchStatus.CanVoid);

            DataRow findRow = this.dtBatchInfo.Rows.Find(this.strBatchNo);
            if (findRow != null)
            {
                this.txtCCY.Text = findRow["C_CCY"].ToString();
                this.mtxtCheckItems.Text = findRow["C_Items"].ToString();
                this.txtTotalAmt.Text = findRow["C_TotalAmt"].ToString();
            }
            else
            {
                CommFunc.MsgInfo(string.Format("Can not get detail infomation of select batch No:{0}", this.strBatchNo));
                this.Close();
            }
        }

        private void btnComfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.mtxtChequeNo.Text))
            {
                CommFunc.MsgInfo("Please set the Cheque No.!");
                return;
            }

            int startChequeNo = Convert.ToInt32(this.mtxtChequeNo.Text);

            string strRes = DalRules.VoidAndReAssignChequeNoByBatch(strBatchNo, startChequeNo, GlobalParam.Inst.gsUserID);
            if (string.IsNullOrEmpty(strRes))
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                CommFunc.MsgErr(strRes);
        }
    }
}
