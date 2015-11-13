using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using BWHITD.Sys.Common;

namespace DemoForAIA
{
    public sealed class GlobalParam
    {
        private static GlobalParam mInstance;
        private DB mdbi;
        private bool mIsBCP = false;
        private GlobalParam()
        {
            if (!SystemConst.IsAssignDBConn)
            {
                SystemConst.AssignDBConnByFile();
            }

            this.mIsBCP = SystemConst.IsBCP;
            this.mdbi = new DB(SystemConst.DBConnParam);
        }

        public static GlobalParam Inst
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new GlobalParam();
                }
                return mInstance;
            }
        }

        public DB DBI
        {
            get { return this.mdbi; }
        }

        private List<SqlParameter> m_listSqlParams;
        public List<SqlParameter> ListSqlParams
        {
            get
            {
                if (this.m_listSqlParams == null)
                {
                    this.m_listSqlParams = new System.Collections.Generic.List<SqlParameter>();
                }

                return this.m_listSqlParams;
            }
            set
            {
                this.m_listSqlParams = value;
            }
        }

        public void AddSqlParams(string paramName, object paramValue)
        {
            this.m_listSqlParams.Add(new SqlParameter(paramName, paramValue));
        }

        public void ClearAll()
        {
            try
            {
                if (this.mdbi != null)
                {
                    this.mdbi.Dispose();
                }

                if (this.m_listSqlParams != null)
                {
                    this.m_listSqlParams.Clear();
                }
            }
            catch (Exception ex)
            {
                Log.LogErr(ex);
            }
        }

        public bool IsBCP
        {
            get { return this.mIsBCP; }
        }

        public string gsUserID { get; set; }

        public string gsUserGroup { get; set; }

        public string gsUserDept { get; set; }
    }
}
