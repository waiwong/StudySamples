using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using BWHITD.Sys.Common;

namespace DemoForAIA
{
    public class DalRules
    {
        private static SymmetricAlgorithm _algorithm = new RijndaelManaged();
        private static string _CryptoPwd = "9C579CEC-D8D2-40bd-B26A-E8D7D3C086AD";

        public static DataTable GetBatchInfo(clsConst.BatchStatus batStatus)
        {
            string sql = @"SELECT C_BatchNo, C_CCY, C_Items, C_TotalAmt, C_FileName, C_ImportDate, C_ImportBy
FROM dbo.Raw_BatchInfo ";
            if (batStatus == clsConst.BatchStatus.NeedPrint)
            {
                sql += string.Format(" WHERE C_Status IN ({0},{1})", (int)clsConst.BatchStatus.Assigned, (int)clsConst.BatchStatus.HaveVoid);
            }
            else if (batStatus == clsConst.BatchStatus.CanVoid)
            {
                sql += string.Format(" WHERE C_Status IN ({0},{1})", (int)clsConst.BatchStatus.Printed, (int)clsConst.BatchStatus.HaveVoid);
            }
            else
            {
                sql += string.Format(" WHERE C_Status = {0}", (int)batStatus);
            }

            DataTable dtResult = GlobalParam.Inst.DBI.GetDataTable(sql);
            dtResult.PrimaryKey = new DataColumn[] { dtResult.Columns["C_BatchNo"] };
            return dtResult;
        }

        public static DataTable GetBatchTxNo(string strBatchNo)
        {
            string sql = @"SELECT C_BatchNo, TX_ref_no FROM dbo.Raw_Transaction WHERE C_BatchNo = @BatchNO";
            GlobalParam.Inst.ListSqlParams.Clear();
            GlobalParam.Inst.AddSqlParams("@BatchNO", strBatchNo);

            return GlobalParam.Inst.DBI.GetDataTable(sql, GlobalParam.Inst.ListSqlParams.ToArray());
        }

        public static DataTable GetBatchDetail(string strBatchNo)
        {
            string sql = @"SELECT M.C_BatchNo, M.TX_ref_no, P.ChequeNo, M.Amount, M.CCY, D.Detail, P.AssignBy, CONVERT(VARCHAR
		, P.AssginTime, 111) AS AssginTime
FROM dbo.Raw_Transaction M
INNER JOIN dbo.Raw_Detail D ON M.TX_ref_no = D.TX_ref_no
INNER JOIN dbo.Record_Print P ON M.TX_ref_no = P.TX_ref_no AND P.Print_By IS NULL
WHERE M.C_BatchNo = @BatchNO";
            GlobalParam.Inst.ListSqlParams.Clear();
            GlobalParam.Inst.AddSqlParams("@BatchNO", strBatchNo);

            return GlobalParam.Inst.DBI.GetDataTable(sql, GlobalParam.Inst.ListSqlParams.ToArray());
        }

        public static string UpdateAssignChequeNo(string strBatchNo, Dictionary<string, string> pDicTxNoAndChequeNo, string strUserID)
        {
            string strResult = string.Empty;
            try
            {
                int printCount = Convert.ToInt16(GlobalParam.Inst.DBI.ExecScalar(string.Format(@"SELECT COUNT(*)
FROM dbo.Record_Print
WHERE C_BatchNo = '{0}' AND Print_By IS NOT NULL", strBatchNo)));
                if (printCount > 0)
                {
                    strResult = "The batch have been printed.";
                }
                else
                {
                    GlobalParam.Inst.DBI.BeginTransaction();
                    string sqlDel = @"DELETE FROM dbo.Record_Print WHERE C_BatchNo = @BatchNO";
                    GlobalParam.Inst.ListSqlParams.Clear();
                    GlobalParam.Inst.AddSqlParams("@BatchNO", strBatchNo);
                    GlobalParam.Inst.DBI.ExecNonQuery(sqlDel, GlobalParam.Inst.ListSqlParams.ToArray());

                    int execRes = 0;
                    foreach (var item in pDicTxNoAndChequeNo)
                    {
                        string sqlInsert = @"INSERT INTO dbo.Record_Print (C_BatchNo,TX_ref_no, ChequeNo, AssignBy)
VALUES (@C_BatchNo,@TX_ref_no, @ChequeNo, @AssignBy)";
                        GlobalParam.Inst.ListSqlParams.Clear();
                        GlobalParam.Inst.AddSqlParams("@C_BatchNo", strBatchNo);
                        GlobalParam.Inst.AddSqlParams("@TX_ref_no", item.Key);
                        GlobalParam.Inst.AddSqlParams("@ChequeNo", item.Value);
                        GlobalParam.Inst.AddSqlParams("@AssignBy", strUserID);

                        execRes = GlobalParam.Inst.DBI.ExecNonQuery(sqlInsert, GlobalParam.Inst.ListSqlParams.ToArray());
                        if (execRes == 0)
                        {
                            strResult = string.Format("Update Assign ChequeNo Failed:{0}-->{1}-->{2}", strBatchNo, item.Key, item.Value);
                            break;
                        }
                    }

                    if (string.IsNullOrEmpty(strResult))
                    {
                        string updateSQL = "UPDATE dbo.Raw_BatchInfo SET C_Status = @BatStatus WHERE C_BatchNo = @BatchNo";
                        GlobalParam.Inst.ListSqlParams.Clear();
                        GlobalParam.Inst.AddSqlParams("@BatStatus", (int)clsConst.BatchStatus.Assigned);
                        GlobalParam.Inst.AddSqlParams("@BatchNO", strBatchNo);
                        execRes = GlobalParam.Inst.DBI.ExecNonQuery(updateSQL, GlobalParam.Inst.ListSqlParams.ToArray());
                        if (execRes == 0)
                        {
                            strResult = string.Format("Update Batch status Failed:{0}", strBatchNo);
                        }
                    }

                    if (string.IsNullOrEmpty(strResult))
                        GlobalParam.Inst.DBI.CommitTransaction();
                    else
                        GlobalParam.Inst.DBI.AbortTransaction();
                }
            }
            catch (Exception ex)
            {
                GlobalParam.Inst.DBI.AbortTransaction();
                Log.LogErr(ex);
                strResult = ex.Message;
            }

            return strResult;
        }

        public static DataTable GetAssignChequeNoRpt(string strBatchNo)
        {
            string sql = @"SELECT M.C_BatchNo, M.TX_ref_no, P.ChequeNo, M.Amount, M.CCY, P.AssignBy, CONVERT(VARCHAR, P.
		AssginTime, 111) AS AssginTime
FROM dbo.Raw_Transaction M
INNER JOIN dbo.Record_Print P ON M.TX_ref_no = P.TX_ref_no
WHERE M.C_BatchNo = @BatchNO";
            GlobalParam.Inst.ListSqlParams.Clear();
            GlobalParam.Inst.AddSqlParams("@BatchNO", strBatchNo);

            return GlobalParam.Inst.DBI.GetDataTable(sql, GlobalParam.Inst.ListSqlParams.ToArray());
        }

        public static DataTable GetRawBatchInfo()
        {
            string sql = @"select * from Raw_BatchInfo";

            return GlobalParam.Inst.DBI.GetDataTable(sql);
        }

        public static string AddOCRData(string strBatchNo, Dictionary<string, string> dicOcrData, string strUserID)
        {
            string strResult = string.Empty;
            try
            {
                GlobalParam.Inst.DBI.BeginTransaction();
                string sqlDel = @"DELETE FROM dbo.OCR_RESULT WHERE C_BatchNO = @BatchNO";
                GlobalParam.Inst.ListSqlParams.Clear();
                GlobalParam.Inst.AddSqlParams("@BatchNO", strBatchNo);
                GlobalParam.Inst.DBI.ExecNonQuery(sqlDel, GlobalParam.Inst.ListSqlParams.ToArray());

                foreach (var item in dicOcrData)
                {
                    string sqlInsert = "INSERT INTO.dbo.OCR_RESULT (C_BatchNO, PID, CID, C_UserID) VALUES (@BatchNO, @PID, @CID, @C_UserID)";
                    GlobalParam.Inst.ListSqlParams.Clear();
                    GlobalParam.Inst.AddSqlParams("@BatchNO", strBatchNo);
                    GlobalParam.Inst.AddSqlParams("@PID", item.Value);
                    GlobalParam.Inst.AddSqlParams("@CID", item.Key);
                    GlobalParam.Inst.AddSqlParams("@C_UserID", strUserID);

                    int execRes = GlobalParam.Inst.DBI.ExecNonQuery(sqlInsert, GlobalParam.Inst.ListSqlParams.ToArray());
                    if (execRes == 0)
                    {
                        strResult = string.Format("Add OCR Data Failed:{0}-->{1}-->{2}", strBatchNo, item.Key, item.Value);
                    }
                }

                if (string.IsNullOrEmpty(strResult))
                    GlobalParam.Inst.DBI.CommitTransaction();
                else
                    GlobalParam.Inst.DBI.AbortTransaction();
            }
            catch (Exception ex)
            {
                GlobalParam.Inst.DBI.AbortTransaction();
                Log.LogErr(ex);
                strResult = ex.Message;
            }

            return strResult;
        }

        public static DataTable GetOCRDataResult(string strBatchNo)
        {
            string sql = @"SELECT C_BatchNo, PID, CID, C_UserID, C_Time FROM dbo.OCR_RESULT WHERE C_BatchNo = @BatchNO";
            GlobalParam.Inst.ListSqlParams.Clear();
            GlobalParam.Inst.AddSqlParams("@BatchNO", strBatchNo);

            return GlobalParam.Inst.DBI.GetDataTable(sql, GlobalParam.Inst.ListSqlParams.ToArray());
        }

        public static string DoPrintChequeNo(string strBatchNo, string strUserID)
        {
            string strResult = string.Empty;
            try
            {
                GlobalParam.Inst.DBI.BeginTransaction();
                string sqlDel = @"UPDATE dbo.Record_Print
SET Print_By = @Print_By, Print_Date = GETDATE()
WHERE C_BatchNo = @BatchNo";
                GlobalParam.Inst.ListSqlParams.Clear();
                GlobalParam.Inst.AddSqlParams("@Print_By", strUserID);
                GlobalParam.Inst.AddSqlParams("@BatchNO", strBatchNo);
                int execRes = GlobalParam.Inst.DBI.ExecNonQuery(sqlDel, GlobalParam.Inst.ListSqlParams.ToArray());
                if (execRes == 0)
                {
                    strResult = string.Format("Update Batch Print Infomation Failed:{0}", strBatchNo);
                }
                else
                {
                    string updateSQL = "UPDATE dbo.Raw_BatchInfo SET C_Status = @BatStatus WHERE C_BatchNo = @BatchNo";
                    GlobalParam.Inst.ListSqlParams.Clear();
                    GlobalParam.Inst.AddSqlParams("@BatStatus", (int)clsConst.BatchStatus.Printed);
                    GlobalParam.Inst.AddSqlParams("@BatchNO", strBatchNo);
                    execRes = GlobalParam.Inst.DBI.ExecNonQuery(updateSQL, GlobalParam.Inst.ListSqlParams.ToArray());
                    if (execRes == 0)
                    {
                        strResult = string.Format("Update Batch status Failed:{0}", strBatchNo);
                    }
                }

                if (string.IsNullOrEmpty(strResult))
                    GlobalParam.Inst.DBI.CommitTransaction();
                else
                    GlobalParam.Inst.DBI.AbortTransaction();
            }
            catch (Exception ex)
            {
                GlobalParam.Inst.DBI.AbortTransaction();
                Log.LogErr(ex);
                strResult = ex.Message;
            }

            return strResult;
        }

        public static DataTable GetVoidChequeDetail(string strBatchNo)
        {
            string sql = @"SELECT M.C_BatchNo, M.TX_ref_no, P.ChequeNo, M.Amount, M.CCY, V.ChequeNo as VoidChequeNo, V.Void_By, CONVERT(VARCHAR
		, V.Void_Date, 111) AS Void_Date
FROM dbo.Raw_Transaction M
INNER JOIN dbo.Record_Void V ON M.TX_ref_no = V.TX_ref_no 
INNER JOIN dbo.Record_Print P ON M.TX_ref_no = P.TX_ref_no AND V.ReAssignNo = P.ChequeNo AND P.Print_By IS NULL
WHERE M.C_BatchNo = @BatchNO";
            GlobalParam.Inst.ListSqlParams.Clear();
            GlobalParam.Inst.AddSqlParams("@BatchNO", strBatchNo);

            return GlobalParam.Inst.DBI.GetDataTable(sql, GlobalParam.Inst.ListSqlParams.ToArray());
        }

        public static string VoidAndReAssignChequeNo(string strBatchNo, string strTxNo, string strVoidNo, string strReAssignNo, string strUserID)
        {
            bool needCommitTrans = true;
            if (GlobalParam.Inst.DBI.IsInTransaction)
                needCommitTrans = false;

            string strResult = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(strTxNo))
                {
                    string checkSql = @"SELECT TX_ref_no FROM dbo.Record_Print M WHERE M.C_BatchNo = @C_BatchNo AND M.ChequeNo = @ChequeNo AND NOT EXISTS (
		SELECT *
		FROM dbo.Record_Void V
		WHERE M.TX_ref_no=V.TX_ref_no AND V.ChequeNo = @ChequeNo
		)";
                    GlobalParam.Inst.ListSqlParams.Clear();
                    GlobalParam.Inst.AddSqlParams("@C_BatchNo", strBatchNo);
                    GlobalParam.Inst.AddSqlParams("@ChequeNo", strVoidNo);

                    strTxNo = GlobalParam.Inst.DBI.ExecScalar(checkSql, GlobalParam.Inst.ListSqlParams.ToArray(), string.Empty).ToString();
                }

                if (string.IsNullOrEmpty(strTxNo))
                {
                    strResult = string.Format("Can not get the Tx No of the void Cheque NO:{0}-->{1}", strBatchNo, strVoidNo);
                }
                else
                {
                    string checkSql = @"SELECT COUNT(*) FROM dbo.Record_Print
WHERE C_BatchNo = @C_BatchNo AND TX_ref_no = @TX_ref_no AND ChequeNo = @ChequeNo AND Print_By IS
	 NULL";
                    GlobalParam.Inst.ListSqlParams.Clear();
                    GlobalParam.Inst.AddSqlParams("@C_BatchNo", strBatchNo);
                    GlobalParam.Inst.AddSqlParams("@TX_ref_no", strTxNo);
                    GlobalParam.Inst.AddSqlParams("@ChequeNo", strVoidNo);

                    if (Convert.ToInt16(GlobalParam.Inst.DBI.ExecScalar(checkSql, GlobalParam.Inst.ListSqlParams.ToArray(), "0")) > 0)
                    {
                        strResult = string.Format("The Cheque No has not yet printed:{0}-->{1}-->{2}", strBatchNo, strTxNo, strVoidNo);
                    }
                    else
                    {
                        if (needCommitTrans)
                            GlobalParam.Inst.DBI.BeginTransaction();
                        string sqlInsertVoidNo = @"INSERT INTO dbo.Record_Void (TX_ref_no, ChequeNo,ReAssignNo, Void_By, Void_Date)
VALUES (@TX_ref_no, @ChequeNo,@ReAssignNo, @Void_By, GETDATE())";
                        GlobalParam.Inst.ListSqlParams.Clear();
                        GlobalParam.Inst.AddSqlParams("@TX_ref_no", strTxNo);
                        GlobalParam.Inst.AddSqlParams("@ChequeNo", strVoidNo);
                        GlobalParam.Inst.AddSqlParams("@ReAssignNo", strReAssignNo);
                        GlobalParam.Inst.AddSqlParams("@Void_By", strUserID);

                        int execRes = GlobalParam.Inst.DBI.ExecNonQuery(sqlInsertVoidNo, GlobalParam.Inst.ListSqlParams.ToArray());
                        if (execRes == 0)
                        {
                            strResult = string.Format("Add Void ChequeNo Record Failed:{0}-->{1}-->{2}", strBatchNo, strTxNo, strVoidNo);
                        }
                        else
                        {
                            string sqlInsertReAssginNo = @"INSERT INTO dbo.Record_Print (C_BatchNo,TX_ref_no, ChequeNo, AssignBy)
VALUES (@C_BatchNo,@TX_ref_no, @ChequeNo, @AssignBy)";
                            GlobalParam.Inst.ListSqlParams.Clear();
                            GlobalParam.Inst.AddSqlParams("@C_BatchNo", strBatchNo);
                            GlobalParam.Inst.AddSqlParams("@TX_ref_no", strTxNo);
                            GlobalParam.Inst.AddSqlParams("@ChequeNo", strReAssignNo);
                            GlobalParam.Inst.AddSqlParams("@AssignBy", strUserID);

                            execRes = GlobalParam.Inst.DBI.ExecNonQuery(sqlInsertReAssginNo, GlobalParam.Inst.ListSqlParams.ToArray());
                            if (execRes == 0)
                            {
                                strResult = string.Format("Update Assign ChequeNo Failed:{0}-->{1}-->{2}", strBatchNo, strTxNo, strReAssignNo);
                            }
                        }

                        if (needCommitTrans && string.IsNullOrEmpty(strResult))
                        {
                            string updateSQL = "UPDATE dbo.Raw_BatchInfo SET C_Status = @BatStatus WHERE C_BatchNo = @BatchNo";
                            GlobalParam.Inst.ListSqlParams.Clear();
                            GlobalParam.Inst.AddSqlParams("@BatStatus", (int)clsConst.BatchStatus.HaveVoid);
                            GlobalParam.Inst.AddSqlParams("@BatchNO", strBatchNo);
                            execRes = GlobalParam.Inst.DBI.ExecNonQuery(updateSQL, GlobalParam.Inst.ListSqlParams.ToArray());
                            if (execRes == 0)
                            {
                                strResult = string.Format("Update Batch status Failed:{0}", strBatchNo);
                            }
                        }

                        if (needCommitTrans)
                        {
                            if (string.IsNullOrEmpty(strResult))
                                GlobalParam.Inst.DBI.CommitTransaction();
                            else
                                GlobalParam.Inst.DBI.AbortTransaction();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (needCommitTrans)
                    GlobalParam.Inst.DBI.AbortTransaction();
                Log.LogErr(ex);
                strResult = ex.Message;
            }

            return strResult;
        }

        public static string VoidAndReAssignChequeNoByBatch(string strBatchNo, int reAssginStartNo, string strUserID)
        {
            string strResult = string.Empty;
            try
            {
                string checkSql = @"SELECT COUNT(*) FROM dbo.Record_Print M
WHERE M.C_BatchNo = @C_BatchNo AND EXISTS (
		SELECT *
		FROM dbo.Record_Void V
		WHERE M.TX_ref_no = V.TX_ref_no
		)";
                GlobalParam.Inst.ListSqlParams.Clear();
                GlobalParam.Inst.AddSqlParams("@C_BatchNo", strBatchNo);

                if (Convert.ToInt16(GlobalParam.Inst.DBI.ExecScalar(checkSql, GlobalParam.Inst.ListSqlParams.ToArray(), "0")) > 0)
                {
                    strResult = string.Format("The Batch exists void cheque:{0}", strBatchNo);
                }
                else
                {
                    string getTxNoSql = @"SELECT TX_ref_no,ChequeNo FROM dbo.Record_Print WHERE C_BatchNo = @C_BatchNo";
                    GlobalParam.Inst.ListSqlParams.Clear();
                    GlobalParam.Inst.AddSqlParams("@C_BatchNo", strBatchNo);
                    Dictionary<string, string> dicTxNoAndChequeNo = new Dictionary<string, string>();
                    int startChequeNo = reAssginStartNo;
                    DataTable dtBatchDetail = GlobalParam.Inst.DBI.GetDataTable(getTxNoSql, GlobalParam.Inst.ListSqlParams.ToArray());
                    foreach (DataRow dr in dtBatchDetail.Rows)
                    {
                        dicTxNoAndChequeNo.Add(dr["TX_ref_no"].ToString(), dr["ChequeNo"].ToString() + "|" + startChequeNo.ToString());
                        startChequeNo++;
                    }

                    int execRes = 0;
                    GlobalParam.Inst.DBI.BeginTransaction();
                    foreach (var item in dicTxNoAndChequeNo)
                    {
                        string strTxNo = item.Key;
                        string strVoidNo = item.Value.Split('|')[0];
                        string strReAssignNo = item.Value.Split('|')[1];

                        strResult = VoidAndReAssignChequeNo(strBatchNo, strTxNo, strVoidNo, strReAssignNo, strUserID);
                        if (!string.IsNullOrEmpty(strResult))
                            break;
                    }

                    if (string.IsNullOrEmpty(strResult))
                    {
                        string updateSQL = "UPDATE dbo.Raw_BatchInfo SET C_Status = @BatStatus WHERE C_BatchNo = @BatchNo";
                        GlobalParam.Inst.ListSqlParams.Clear();
                        GlobalParam.Inst.AddSqlParams("@BatStatus", (int)clsConst.BatchStatus.HaveVoid);
                        GlobalParam.Inst.AddSqlParams("@BatchNO", strBatchNo);
                        execRes = GlobalParam.Inst.DBI.ExecNonQuery(updateSQL, GlobalParam.Inst.ListSqlParams.ToArray());
                        if (execRes == 0)
                        {
                            strResult = string.Format("Update Batch status Failed:{0}", strBatchNo);
                        }
                    }

                    if (string.IsNullOrEmpty(strResult))
                        GlobalParam.Inst.DBI.CommitTransaction();
                    else
                        GlobalParam.Inst.DBI.AbortTransaction();
                }
            }
            catch (Exception ex)
            {
                GlobalParam.Inst.DBI.AbortTransaction();
                Log.LogErr(ex);
                strResult = ex.Message;
            }

            return strResult;
        }

        public static byte[] EncryptSignature(byte[] data)
        {
            GetKey(_CryptoPwd);

            ICryptoTransform encryptor = _algorithm.CreateEncryptor();

            byte[] cryptoData = encryptor.TransformFinalBlock(data, 0, data.Length);

            return cryptoData;
        }

        public static byte[] DecryptSignature(byte[] cryptoData)
        {
            GetKey(_CryptoPwd);

            ICryptoTransform decryptor = _algorithm.CreateDecryptor();

            byte[] data = decryptor.TransformFinalBlock(cryptoData, 0, cryptoData.Length);

            return data;
        }

        private static void GetKey(string password)
        {
            byte[] salt = new byte[8];

            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);

            int length = Math.Min(passwordBytes.Length, salt.Length);

            for (int i = 0; i < length; i++)
                salt[i] = passwordBytes[i];

            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt);

            _algorithm.Key = key.GetBytes(_algorithm.KeySize / 8);
            _algorithm.IV = key.GetBytes(_algorithm.BlockSize / 8);
        }

        public static Image GetSignature()
        {
            Image result = null;

            try
            {
                byte[] buffer = DalRules.DecryptSignature((byte[])GlobalParam.Inst.DBI.ExecScalar("SELECT top 1 signature from Signature"));
                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(buffer, 0, buffer.Length);
                    result = Image.FromStream(ms);
                }
            }
            catch (Exception ex)
            {
                Log.LogErr(ex);
            }

            return result;
        }
    }
}
