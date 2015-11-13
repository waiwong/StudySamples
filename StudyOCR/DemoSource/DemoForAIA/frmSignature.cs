using System;
using System.Windows.Forms;
using System.IO;
using BWHITD.Sys.Common;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Drawing;

namespace DemoForAIA
{
    public partial class frmSignature : frmBase
    {
        private string _fileName = "";

        public frmSignature()
        {
            InitializeComponent();
        }

        #region controls event
        private void btnFile_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();

            if (result.Equals(DialogResult.OK))
            {
                txtFilePath.Text = openFileDialog1.FileName;
                _fileName = openFileDialog1.FileName.Substring(txtFilePath.Text.LastIndexOf(@"\") + 1);
            }
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            status.Text = "";
            try
            {
                byte[] buffer = File.ReadAllBytes(txtFilePath.Text);

                using (DB db = new DB())
                {
                    SqlParameter[] par = new SqlParameter[1];
                    par[0] = new SqlParameter("@image", DalRules.EncryptSignature(buffer));
                    int cnt = db.ExecNonQuerySP("UpdateSignature", par);
                }
                status.Text = "Saved to DB!";
            }
            catch (Exception ex)
            {
                status.Text = ex.Message;
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            status.Text = "";
            try
            {
                pictureBox1.Image = DalRules.GetSignature();
                status.Text = "Image Load!";
            }
            catch (Exception ex)
            {
                status.Text = ex.Message;
            }
        }

        #endregion

    }
}
