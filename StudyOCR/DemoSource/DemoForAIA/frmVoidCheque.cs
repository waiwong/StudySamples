using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BWHITD.Win.Lib;

namespace DemoForAIA
{
    public partial class frmVoidCheque : frmBase
    {
        private string strBatchNo;
        public frmVoidCheque(string pBatchNo)
        {
            InitializeComponent();
            this.strBatchNo = pBatchNo;
            this.txtBatchNo.Text = pBatchNo;
            this.txtBatchNo.ReadOnly = true;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.mtxtVoidChequeNo.Text))
            {
                CommFunc.MsgInfo("Please set the Void Cheque No.!");
                return;
            }

            if (string.IsNullOrEmpty(this.mtxtReAssignChequeNo.Text))
            {
                CommFunc.MsgInfo("Please set the Re-Assign Cheque No.!");
                return;
            }

            string strRes = DalRules.VoidAndReAssignChequeNo(strBatchNo, string.Empty, this.mtxtVoidChequeNo.Text,
                this.mtxtReAssignChequeNo.Text, GlobalParam.Inst.gsUserID);
            if (string.IsNullOrEmpty(strRes))
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                CommFunc.MsgErr(strRes);
        }

        private void frmVoidCheque_Load(object sender, EventArgs e)
        {
            string sql = "SELECT ISNULL(MAX(CAST(ChequeNo AS INT)), 0) + 1 FROM dbo.Record_Print";
            this.mtxtReAssignChequeNo.Text = GlobalParam.Inst.DBI.ExecScalar(sql).ToString();
        }
    }
}
