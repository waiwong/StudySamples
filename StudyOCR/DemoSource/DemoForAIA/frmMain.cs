using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using BWHITD.Sys.Common;
using BWHITD.Win.Lib;

namespace DemoForAIA
{
    public partial class frmMain : Form
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
        }

        #endregion

        #region Event

        #region Form Events
        private void frmMain_Load(object sender, EventArgs e)
        {
#if DEBUG
            GlobalParam.Inst.gsUserID = Environment.UserName.ToString();
            GlobalParam.Inst.gsUserDept = Environment.UserDomainName;
            GlobalParam.Inst.gsUserGroup = "TEST";
#endif

            string localVersion = SysUtil.GetAssemblyVersion();
            this.Text = Application.CompanyName + " - " + Application.ProductName + " - Version [" + localVersion + "]";

            this.tssLabUserInfo.Text = string.Format("Current User [{0}] -- Group [{1}] -- Dept/Branch [{2}]",
               GlobalParam.Inst.gsUserID, GlobalParam.Inst.gsUserGroup, GlobalParam.Inst.gsUserDept);

#if UAT
	        this.Text += "<For UAT>";	
#elif DEBUG
            this.Text += "<For DEBUG>";
#endif

            this.WindowState = FormWindowState.Maximized;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Form[] charr = this.MdiChildren;

                foreach (Form chform in charr)
                {
                    chform.Close();
                }
            }
            catch (Exception ex)
            {
                Log.LogErr(ex);
            }
        }

        #endregion

        #region Tool Bar Events

        private void tsmClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmiImportData_Click(object sender, EventArgs e)
        {
            MdiFormLoader.LoadFormType(typeof(frmImportData), this);
        }

        private void tsmiAssignNo_Click(object sender, EventArgs e)
        {
            MdiFormLoader.LoadFormType(typeof(frmAssignChequeNo), this);
        }

        private void tsmiVoidCheque_Click(object sender, EventArgs e)
        {
            MdiFormLoader.LoadFormType(typeof(frmVoidReAssignCheque), this);
        }

        private void tsmiOCR_Click(object sender, EventArgs e)
        {
            MdiFormLoader.LoadFormType(typeof(frmOCR), this);
        }

        private void tsmiOCRResult_Click(object sender, EventArgs e)
        {
            MdiFormLoader.LoadFormType(typeof(frmOCRResult), this);
        }

        private void tsmiAssignRpt_Click(object sender, EventArgs e)
        {
            MdiFormLoader.LoadFormType(typeof(frmAssignedRpt), this);
        }

        private void tsmiDoPrintCheque_Click(object sender, EventArgs e)
        {
            MdiFormLoader.LoadFormType(typeof(frmDoPrintCheque), this);
        }

        private void tsmiCascade_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void tsmiVectical_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void tsmiHorizontal_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void tsmiWindows_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void signatrueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdiFormLoader.LoadFormType(typeof(frmSignature), this);
        }
        #endregion


        #endregion
    }
}
