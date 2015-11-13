using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;
using BWHITD.Sys.Common;

namespace DemoForAIA
{
    public partial class frmImportData : frmBase
    {
        private string _connStr = ConfigurationManager.ConnectionStrings["CPMConnection"].ConnectionString;
        private ArrayList _alTX = new ArrayList();
        private Dictionary<string, string> _dicDetail = new Dictionary<string, string>();
        private string _fileName = "";
        private string _batchNo = "";

        public frmImportData()
        {
            InitializeComponent();
        }

        #region Controls Event
        private void btnFile_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();

            initUI();

            if (result.Equals(DialogResult.OK))
            {
                txtFilePath.Text = openFileDialog1.FileName;
                _fileName = openFileDialog1.FileName.Substring(txtFilePath.Text.LastIndexOf(@"\") + 1);
                //btnChkFile.Visible = true;
                doChecking(txtFilePath.Text);
            }
        }

        private void btnChkFile_Click(object sender, EventArgs e)
        {
            //if (!txtFilePath.Text.Trim().Equals(string.Empty))
            //{
            //    int txCount = 0;
            //    int lineNo = 0;
            //    int itemCount = 0;

            //    try
            //    {
            //        if (doVerify(out txCount, out lineNo, out itemCount))
            //        {
            //            status.Text = string.Format("Transaction records: {0}.", txCount);
            //            btnInsert.Visible = true;
            //        }
            //        else
            //        {
            //            status.Text = String.Format("File invalid - Line {0}, item {1}/113", lineNo, itemCount);
            //            btnInsert.Visible = false;
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        status.Text = ex.Message;
            //        //writeLog(ex);
            //    }
            //}
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            //if (IsPrintedBatch())
            //{
            //    status.Text = "Insert Failed! PRINTED RECORDS FOUND!";
            //}
            //else
            //{
            //    doInsertToDB();
            //}
        }

        private bool IsPrintedBatch()
        {
            using (DB dbChk = new DB())
            {
                int printCount = (int)dbChk.ExecScalar(string.Format("SELECT count(1) FROM Raw_BatchInfo WHERE C_Status <> 0 AND C_BatchNo = '{0}'", _batchNo));
                if (printCount.Equals(1))
                    return true;
                else
                    return false;
            }
        }

        #endregion

        #region methods
        private void initUI()
        {
            btnChkFile.Visible = false;
            btnInsert.Visible = false;
        }

        private void doInsertToDB()
        {
            using (DB db = new DB())
            {
                try
                {
                    db.BeginTransaction();

                    //TRUNCATE table Raw_Transaction
                    SqlParameter[] par = new SqlParameter[1];
                    par[0] = new SqlParameter("@batchNo", _batchNo);

                    db.ExecNonQuerySP("DeleteRawDataByFileName", par);

                    //Insert transaction records
                    foreach (string txVals in _alTX)
                    {
                        db.ExecNonQuery(string.Format("INSERT Raw_Transaction VALUES ({0})", txVals));
                    }

                    //Insert detail records
                    foreach (var detail in _dicDetail)
                    {
                        db.ExecNonQuery(string.Format("INSERT Raw_Detail VALUES ('{0}',N'{1}')", detail.Key, detail.Value.Replace("'", "''")));
                    }

                    //Insert BatchInfo
                    SqlParameter[] par1 = new SqlParameter[3];
                    par1[0] = new SqlParameter("batchNo", _batchNo);
                    par1[1] = new SqlParameter("fileName", _fileName);
                    par1[2] = new SqlParameter("inputBy", GlobalParam.Inst.gsUserID);
                    db.ExecNonQuerySP("InsertBatchInfo", par1);

                    db.CommitTransaction();
                    status.Text = "Inserted to DB.";
                }
                catch (Exception ex)
                {
                    db.AbortTransaction();
                    status.Text = ex.Message;
                    Log.LogErr(ex);
                }
            }
        }

        /// <summary>
        ///Checking for each transaction that must contain 113 items.
        /// </summary>
        private bool doVerify(out int txCount, out int lineNo, out int itemCount)
        {
            string txNo = "";
            string detail = "";
            string tmpStr = "";
            string tmpCCY = "";
            string[] lines = File.ReadAllLines(txtFilePath.Text, Encoding.Default);
            txCount = 0;
            itemCount = 0;
            lineNo = 0;
            _alTX.Clear();
            _dicDetail.Clear();

            _batchNo = string.Format("B_{0}", _fileName);

            try
            {
                foreach (string line in lines)
                {
                    lineNo++;
                    if (!line.StartsWith("INV@"))
                    {
                        txCount++;
                        string[] items = line.Split('@');

                        //Get CCY from line 1
                        if (lineNo.Equals(1))
                            tmpCCY = items[3].ToString();
                        else //checking for CCY, Same CCY in one file.
                        {
                            if (!tmpCCY.Equals(items[3].ToString()))
                            {
                                txNo = items[7].ToString();
                                throw new Exception("-99");
                            }
                        }

                        //insert transaction record
                        tmpStr = getSqlVals(items);
                        _alTX.Add(tmpStr);

                        //insert transaction detail
                        insertDetailToDic(txNo, detail);
                        detail = "";

                        //set new transaction no
                        txNo = items[7].ToString();
                        itemCount = items.Count();
                    }
                    else
                        detail += line;

                    if (!itemCount.Equals(113))
                        return false;
                }
                insertDetailToDic(txNo, detail);
                detail = "";
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("-99"))
                {
                    throw new Exception(string.Format("Different CCY found! Transaction no.: {0}", txNo));
                }
                else
                {
                    throw new Exception(string.Format("Duplicate transaction no.: {0}", txNo));
                }
            }

            return true;
        }

        private string getSqlVals(string[] items)
        {
            string vals = "";

            //format date, convert 20130101 to 2013-01-01
            items[6] = items[6].Insert(6, "-").Insert(4, "-");

            foreach (string item in items)
            {
                vals += ",N'" + item + "'";
            }

            vals += string.Format(",'{0}'", _batchNo);

            vals = vals.Replace("N''", "NULL").Substring(1);

            return vals;
        }

        private void insertDetailToDic(string txNo, string detail)
        {
            if (!detail.Equals(string.Empty))
            {
                _dicDetail.Add(txNo, detail);
            }
        }

        private void doChecking(string filePath)
        {
            if (!filePath.Trim().Equals(string.Empty))
            {
                int txCount = 0;
                int lineNo = 0;
                int itemCount = 0;
                string msg = "";

                try
                {
                    if (doVerify(out txCount, out lineNo, out itemCount))
                    {
                        msg = string.Format("Transaction records: {0}.", txCount);
                        //status.Text = msg;
                        if (MessageBox.Show(string.Format("{0} Import Data?", msg), "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            //do import
                            doImportData();
                        }

                        //btnInsert.Visible = true;
                    }
                    else
                    {
                        msg = String.Format("File invalid - Line {0}, item {1}/113", lineNo, itemCount);
                        //status.Text = msg;
                        MessageBox.Show(msg, "Invalid data was found!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //btnInsert.Visible = false;
                    }

                }
                catch (Exception ex)
                {
                    //status.Text = ex.Message;
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //writeLog(ex);
                }
            }
            //return result;
        }

        private void doImportData()
        {
            if (IsPrintedBatch())
            {
                MessageBox.Show("Insert Failed! PRINTED RECORDS FOUND!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //status.Text = "Insert Failed! PRINTED RECORDS FOUND!";
            }
            else
            {
                doInsertToDB();
            }        
        }

        #endregion

        private void btnReport_Click(object sender, EventArgs e)
        {
            string strTitle = "Import Data Report";
            string strRptPath = "RptRDLC\\ImportDataRpt.rdlc";
            Dictionary<string, DataTable> dicDSNameWithData = new Dictionary<string, DataTable>();
            dicDSNameWithData.Add("dsMain", DalRules.GetRawBatchInfo());

            using (var dlg = new frmRptViewer(strTitle, strRptPath, dicDSNameWithData))
            {
                dlg.ShowDialog(this);
            }
        }

    }
}
