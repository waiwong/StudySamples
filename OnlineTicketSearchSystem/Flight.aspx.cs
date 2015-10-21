using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.OleDb;
public partial class About : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void cal_SelectionChanged(object sender, EventArgs e)
    {
        this.text3.Text = this.cal.SelectedDate.ToString("yyyy-MM-dd");
        this.cal.Visible = false;
    }


    protected void btn_Click1(object sender, EventArgs e)
    {
        GridView1.Visible = true;

        //连接数据库
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["flightConnectionString"].ToString());
        string sql = "select count(*) from flight_Info where startCity='" + text1.Text + "' and lastCity='" + text2.Text + "' and theDate='" + text3.Text + "'";
        
        //如果localDB不存在，则将webservice返回的数据插入到数据库
        if (ComClass.ValidateUser(sql) == 0)
        {
        string startCity =this.text1.Text;
        string lastCity = this.text2.Text;
        string theDate = this.text3.Text;
        string userID = null;

        //连接webservice
        cn.com.webxml.webservice.DomesticAirline web = new cn.com.webxml.webservice.DomesticAirline();
        DataSet ds = web.getDomesticAirlinesTime(startCity, lastCity, theDate, userID);

        //得到结果的每一行
        foreach (DataTable table in ds.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                Session["Company"] = row[0];
                Session["AirlineCode"] = row[1];
                Session["StartDrome"] = row[2];
                Session["ArriveDrome"] = row[3];
                Session["StartTime"] = row[4];
                Session["ArriveTime"] = row[5];
                Session["Mode"] = row[6];
                Session["AirlineStop"] = row[7];
                Session["Week"] = row[8];
                Session["startCity"] = text1.Text;
                Session["lastCity"] = text2.Text;
                Session["theDate"] = text3.Text;
                //插入数据库
                SqlDataSource1.Insert();
            }
        }
         }


    }


    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        cal.Visible = true;
    }
    protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
}