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
    



    protected void btn_Click1(object sender, EventArgs e)
    {
        GridView1.Visible = true;
        

        //连接数据库
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["flightConnectionString"].ToString());
        string sql = "select count(*) from Train_Info where StartStation like'"+'%' +text1.Text +'%'+ "' and ArriveStation like'"  +'%'+text2.Text+'%'+ "'";
       
        if (ComClass.ValidateUser(sql) == 0)
        {

        string StartStation = this.text1.Text;
        string ArriveStation = this.text2.Text;

        //连接webservice
        cn.com.webxml.webservice.TrainTimeWebService web = new cn.com.webxml.webservice.TrainTimeWebService();
        DataSet ds = web.getStationAndTimeByStationName(StartStation, ArriveStation, "");
               //如果localDB不存在，则将webservice返回的数据插入到数据库
               foreach (DataTable table in ds.Tables)
               {


                   foreach (DataRow row in table.Rows)
                   {
                       Session["TrainCode"] = row[0];
                       Session["FirstStation"] = row[1];
                       Session["LastStation"] = row[2];
                       Session["StartStation"] = row[3];
                       Session["StartTime"] = row[4];
                       Session["ArriveStation"] = row[5];
                       Session["ArriveTime"] = row[6];
                       Session["KM"] = row[7];
                       Session["UseDate"] = row[8];

                       SqlDataSource1.Insert();
                   }
               }
        }


    }


    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
           
}