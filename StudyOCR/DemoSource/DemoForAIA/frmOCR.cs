using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BWHITD.Sys.Common;
using BWHITD.Win.Lib;

namespace DemoForAIA
{
    public partial class frmOCR : frmBase
    {
        public frmOCR()
        {
            InitializeComponent();
        }

        private void frmOCR_Load(object sender, EventArgs e)
        {
            DataTable dtBatchInfo = DalRules.GetBatchInfo(clsConst.BatchStatus.Printed);
            this.cmbBatchNo.DataSource = dtBatchInfo;
            this.cmbBatchNo.DisplayMember = "C_BatchNo";
            this.cmbBatchNo.ValueMember = "C_BatchNo";
            if (this.cmbBatchNo.Items.Count > 0)
                this.cmbBatchNo.SelectedIndex = 0;
        }

        private void txtDir_DoubleClick(object sender, EventArgs e)
        {
            this.btnSelDir.PerformClick();
        }

        private void btnSelDir_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.ShowNewFolderButton = false;

                string chooseFolder = string.Empty;
                if (!string.IsNullOrEmpty(this.txtDir.Text.Trim()))
                {
                    chooseFolder = this.txtDir.Text.Trim();
                }

                if (Directory.Exists(chooseFolder))
                {
                    fbd.SelectedPath = chooseFolder;
                }

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    this.txtDir.Text = fbd.SelectedPath;
                }
            }
        }

        private void btnOCR_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cmbBatchNo.Text))
            {
                CommFunc.MsgInfo("Please select the Batch No.!");
                return;
            }

            if (string.IsNullOrEmpty(this.txtDir.Text) || !Directory.Exists(this.txtDir.Text.Trim()))
            {
                this.btnSelDir.PerformClick();
            }

            if (string.IsNullOrEmpty(this.txtDir.Text))
            {
                CommFunc.MsgInfo("Please set the OCR File Folder!");
            }
            else
            {
                this.DoOCR(this.txtDir.Text.Trim());
            }
        }

        /// <summary>
        /// Check for Images
        /// read text from these images.
        /// save text from each image in text file automaticly.
        /// handle problems with images
        /// </summary>
        /// <param name="ocrFilesFolder">Set Directory Path to check for Images in it</param>
        public void DoOCR(string strOcrFilesFolder)
        {
            Waiting.Show("Processing");
            Dictionary<string, string> dicOcrResult = new Dictionary<string, string>();

            StringBuilder sbErrMsg = new StringBuilder();
            string tmpSplitPath = Path.Combine(Path.GetTempPath(), "00_OCR_TEMP");
            
#if DEBUG
            tmpSplitPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "00_OCR_TEMP");
            System.Diagnostics.Debug.WriteLine(tmpSplitPath);

            DataTable dtResult = new DataTable();
            dtResult.Columns.Add(new DataColumn("FileName"));
            dtResult.Columns.Add(new DataColumn("KeyName"));
            dtResult.Columns.Add(new DataColumn("MODI"));
            dtResult.Columns.Add(new DataColumn("Tessnet"));
            dtResult.Columns.Add(new DataColumn("Tesseract"));
            dtResult.Columns.Add(new DataColumn("Tesseract_exe"));
#endif
            try
            {
                OCRFile ocrFileIns = new OCRFile();
                ocrFileIns.PreparePathAndSplitTiff(strOcrFilesFolder, tmpSplitPath);

                string tmpClonePath = Path.Combine(tmpSplitPath, "00_ClonePath");
                if (!Directory.Exists(tmpClonePath))
                {
                    Directory.CreateDirectory(tmpClonePath);
                }

                IEnumerator files = Directory.GetFiles(tmpSplitPath, "*.tif", SearchOption.TopDirectoryOnly).GetEnumerator();
                while (files.MoveNext())
                {
                    string currFile = files.Current.ToString();

                    if (!Path.GetExtension(currFile).ToLower().Equals(".tif"))
                    {
                        sbErrMsg.AppendLine(string.Format("This image ({0}) is not a tif file", Path.GetFileName(currFile)));
                        continue;
                    }

                    string strChequeNo = string.Empty;
                    string strTxNo = string.Empty;
                    string strFileName = Path.GetFileNameWithoutExtension(currFile);

                    int tryCount = 0;
                    int maxTryTime = 1;
                    while (tryCount <= maxTryTime)
                    {
                        bool modiResult = true;

                        foreach (var item in ocrFileIns.DicOrcRect)
                        {
                            string strKeyName = string.Empty;
                            string strMODI = string.Empty;
                            string strTessnet = string.Empty;
                            string strTesseract = string.Empty;
                            string strTesseract_exe = string.Empty;

                            try
                            {
                                strKeyName = item.Key;
                                strTessnet = ocrFileIns.UseTessnet2(currFile, item.Key, item.Value);

                                string cropFileFullName = ocrFileIns.CropImage(currFile, tmpClonePath, item.Key, item.Value);

                                try
                                {
                                    strTesseract = ocrFileIns.UseTesseractExe(cropFileFullName);
                                    strTesseract_exe = ocrFileIns.UseTesseract(cropFileFullName);
                                    strMODI = ocrFileIns.UseMODI(cropFileFullName);
                                }
                                finally
                                {
                                    if (File.Exists(cropFileFullName))
                                    {
#if DEBUG
                                        System.Diagnostics.Debug.WriteLine("File.Exists:" + cropFileFullName);
#else
                                        File.Delete(cropFileFullName);
#endif
                                    }
                                }

#if DEBUG
                                DataRow newDR = dtResult.NewRow();
                                newDR["FileName"] = strFileName;
                                newDR["KeyName"] = strKeyName;
                                newDR["MODI"] = strMODI;
                                newDR["Tessnet"] = strTessnet;
                                newDR["Tesseract"] = strTesseract;
                                newDR["Tesseract_exe"] = strTesseract_exe;
                                dtResult.Rows.Add(newDR);
#endif
                            }
                            catch (Exception ex)
                            {
                                modiResult = false;
                                if (tryCount >= maxTryTime)
                                {
                                    sbErrMsg.AppendLine(string.Format("This image ({0}) hasn't a text or has a problem", Path.GetFileName(currFile)));
                                    Log.LogErr(ex);
                                }
                            }

                            if (!modiResult)
                            {
                                if (item.Key == ocrFileIns.ChequeNoDicKey)
                                {
                                    strChequeNo = strMODI;
                                }
                                else if (item.Key == ocrFileIns.TxNoDicKey)
                                {
                                    strTxNo = strMODI;
                                }

                                break;
                            }
                        }

                        if (!modiResult)
                            tryCount++;
                        else
                            break;
                    }

                    if (!string.IsNullOrEmpty(strChequeNo))
                    {
                        dicOcrResult.Add(strChequeNo, strTxNo);
                    }
                }
            }
            finally
            {
                Waiting.CloseAll();
            }

            if (sbErrMsg.Length > 0)
            {
                CommFunc.MsgErr(sbErrMsg.ToString());
            }
            else
            {

                string strRes = DalRules.AddOCRData(this.cmbBatchNo.Text, dicOcrResult, GlobalParam.Inst.gsUserID);
                if (string.IsNullOrEmpty(strRes))
                {
#if DEBUG
                    using (var dlg = new frmOCRResult(dtResult))
                    {
                        dlg.ShowDialog();
                    }
#else
                    using (var dlg = new frmOCRResult(this.cmbBatchNo.Text))
                    {
                        dlg.ShowDialog();
                    }
#endif
                }
                else
                {
                    CommFunc.MsgErr(strRes);
                }

            }
        }
    }
}
