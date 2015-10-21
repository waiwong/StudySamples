using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.Sql;
using System.Data.SqlClient;

/// <summary>
///ComClass 的摘要说明
/// </summary>
public class ComClass
{
	public ComClass()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    private static SqlConnection getCon()
    {
        return new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["flightConnectionString"].ToString());
    }
    public static int ValidateUser(string sql)
    {
        int flag = 0;
        SqlConnection conn = getCon();
        conn.Open();
        SqlCommand cmd = new SqlCommand(sql, conn);
        flag = int.Parse(cmd.ExecuteScalar().ToString());
        conn.Close();
        return flag;
    }
}
