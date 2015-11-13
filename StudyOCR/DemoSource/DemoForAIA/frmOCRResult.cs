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
    public partial class frmOCRResult : frmBase
    {
        private string selBatchNo;
        private DataTable dtResult = null;
        public frmOCRResult() : this(string.Empty) { }

        public frmOCRResult(string pBatchNo)
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(pBatchNo))
                this.selBatchNo = pBatchNo;
        }

        public frmOCRResult(DataTable pDtResult)
        {
            InitializeComponent();
            this.dtResult = pDtResult;
        }

        private void frmRptResult_Load(object sender, EventArgs e)
        {
            if (DataSetHelper.IsEmptyDataTable(this.dtResult))
            {
                DataTable dtBatchInfo = DalRules.GetBatchInfo(clsConst.BatchStatus.Printed);
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
            }
            else
            {
                this.splitConMain.Panel1Collapsed = true;
                this.dgvResult.Columns.Clear();
                this.dgvResult.AutoGenerateColumns = true;
                this.dgvResult.DataSource = this.dtResult;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cmbBatchNo.Text))
            {
                CommFunc.MsgInfo("Please select the Batch No.!");
                return;
            }

            try
            {
                Waiting.Show("Being processed");

                DataTable dtResult = DalRules.GetOCRDataResult(this.cmbBatchNo.Text);

                this.dgvResult.DataSource = dtResult;

                Waiting.CloseAll();
            }
            catch (Exception ex)
            {
                Waiting.CloseAll();
                CommFunc.MsgErr(ex);
            }
        }
    }
}
