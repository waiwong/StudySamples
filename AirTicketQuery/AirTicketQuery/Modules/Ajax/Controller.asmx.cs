using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;
using AirTicketQuery.Modules.Code;

namespace WebProj.Modules.Ajax
{
    /// <summary>
    /// Summary description for Controller
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Controller : System.Web.Services.WebService
    {    
        public void City_Get()
        {
            List<SqlParameter> listParam = new List<SqlParameter>();
            string jsonStr = string.Empty;
            HttpRequest req = Context.Request;

            Hashtable ht = new Hashtable();
            DB dbi = new DB(SystemConst.DBConnString);
            string sqlBase = @"SELECT * FROM dbo.City";
            try
            {
                string sql = sqlBase + " ORDER BY C_ID";
                ht.Add("rows", dbi.GetDataTable(sql, listParam.ToArray()));
            }
            catch (Exception ex)
            {
                ht.Add("err", ex.Message);
                if (!ht.ContainsKey("rows"))
                {
                    ht.Add("rows", dbi.GetDataTable(sqlBase + " WHERE 1=2"));
                }
            }

            jsonStr = JsonConvert.SerializeObject(ht);
            ResponseWrite(jsonStr);
        }       
        
        public void Flight_Get()
        {
            //http://b2c.csair.com/B2C40/detail-SHACAN-20151211-1-0-0-0-1-0-0-0-1-0.g2c

            //List<SqlParameter> listParam = new List<SqlParameter>();
            //string jsonStr = string.Empty;
            //HttpRequest req = Context.Request;
            //string strC_ID = req.Form["C_ID"];
            //string searchName = req.Form["searchName"];
            //string searchVal = req.Form["searchVal"];
            //string category = req.Form["category"];
            //string sort = req.Form["sort"];
            //string page = req.Form["page"] ?? "1";
            //string rowsCnt = req.Form["rows"] ?? "100";
            //string order = req.Form["order"];
            //string sqlWhere = string.Empty;
            //if (!string.IsNullOrEmpty(searchVal))
            //{
            //    sqlWhere += string.Format(" AND {0} LIKE '%' + @{0} + '%'", searchName);
            //    listParam.Add(new SqlParameter("@" + searchName, searchVal));
            //}

            //if (!string.IsNullOrEmpty(category))
            //{
            //    sqlWhere += " AND C_CATEGORY = @C_CATEGORY";
            //    listParam.Add(new SqlParameter("@C_CATEGORY", category));
            //}

            //if (!string.IsNullOrEmpty(strC_ID))
            //{
            //    sqlWhere += " AND C_ID = @C_ID";
            //    listParam.Add(new SqlParameter("@C_ID", strC_ID));
            //}

            //Hashtable ht = new Hashtable();
            //DB dbi = new DB(SystemConst.DBConnString);
            //string sqlBase = @"SELECT * FROM dbo.V_RECORD_MAIN WHERE C_DEL=0";
            //string sqlCnt = @"SELECT COUNT(1) FROM dbo.V_RECORD_MAIN WHERE C_DEL=0";
            //try
            //{
            //    string sql = sqlBase + sqlWhere;
            //    sqlCnt += sqlWhere;

            //    if (!string.IsNullOrEmpty(sort))
            //        sql += string.Format(" ORDER BY {0} {1}", sort, order);

            //    int total = Convert.ToInt16(dbi.ExecScalar(sqlCnt, listParam.ToArray(), "0"));
            //    DataTable dt = dbi.GetPageData(sql, int.Parse(page), int.Parse(rowsCnt), listParam.ToArray());
            //    ht.Add("rows", dt);
            //    ht.Add("total", total);
            //}
            //catch (Exception ex)
            //{
            //    ht.Add("err", ex.Message);
            //    if (!ht.ContainsKey("rows"))
            //    {
            //        ht.Add("rows", dbi.GetDataTable(sqlBase + " AND 1=2"));
            //    }

            //    ht.Add("total", 0);
            //}

            //jsonStr = JsonConvert.SerializeObject(ht);
            //ResponseWrite(jsonStr);
        }

        private List<Flight> CSAIR_Get(string fromCity,string toCity,string departDate)
        {
            List<Flight> lstFlight = new List<Flight>();
            // http://b2c.csair.com/B2C40/detail-SHACAN-20151211-1-0-0-0-1-0-0-0-1-0.g2c
            // http://b2c.csair.com/B2C40/detail-PEKCAN-20151024-1-0-0-0-1-0-0-0-1-0.g2c
            DateTime dtDepart = DateTime.Parse(departDate);
            string strUrl = string.Format("http://b2c.csair.com/B2C40/detail-{0}{1}-{2}-1-0-0-0-1-0-0-0-1-0.g2c", fromCity, toCity, dtDepart.ToString("yyyyMMdd"));
            XmlDocument doc = new XmlDocument();
            doc.Load(strUrl);
            XmlHelper xmlHelper = new XmlHelper(doc);
            XmlNodeList nodelist = xmlHelper.GetXmlNodeListByXpath("/page/FLIGHTS/SEGMENT/DATEFLIGHT/DIRECTFLIGHT");
            foreach (XmlNode node in nodelist)
            {
                DateTime takeoff, landing;
                Flight f = new Flight();
                f.C_From = fromCity;
                f.C_To = toCity;
                f.C_Departure = departDate;
                f.C_FlightNo =  XmlNodeHelper.ParseByNode(node,"FLIGHTNO");
                f.C_Airline = XmlNodeHelper.ParseByNode(node, "AIRLINE");
                f.C_DEPTIME = XmlNodeHelper.ParseByNode(node, "DEPTIME");
                f.C_ARRTIME = XmlNodeHelper.ParseByNode(node, "ARRTIME");
                f.C_TotalTime = XmlNodeHelper.ParseByNode(node, "TIMEDURINGFLIGHT_en");
                              
                XmlNodeList price = xmlHelper.GetXmlNodeListByXpath("");
                float max = 10f, min = 10000f;
                foreach (XmlNode m in price)
                {
                    if (float.Parse(m.Value) > max)
                    {
                        max = float.Parse(m.Value);
                    }
                    if (float.Parse(m.Value) < min)
                    {
                        min = float.Parse(m.Value);
                    }
                }
                if (max == 10f)
                {
                    max = 0f;
                }
                if (min == 10000f)
                {
                    min = 0f;
                }
                f.firstclass = max;
                f.economy = min;
                lstFlight.Add(f);
               
            }

            return lstFlight;       
        }

        #region Private Methods
        private void ResponseWrite(string msg)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "text/json";
            Context.Response.Write(msg);
        }

        private object CheckNull(object strValue)
        {
            if (strValue == null || string.IsNullOrEmpty(strValue.ToString()))
                return DBNull.Value;
            else
                return strValue;
        }

        private string CheckStrNull(object strValue)
        {
            if (strValue == null || string.IsNullOrEmpty(strValue.ToString()))
                return string.Empty;
            else
                return strValue.ToString();
        }
        #endregion
    }
}
