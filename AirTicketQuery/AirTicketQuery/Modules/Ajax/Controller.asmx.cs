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
    [System.Web.Script.Services.ScriptService]
    public class Controller : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
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

        [WebMethod(EnableSession = true)]
        public void Flight_Get()
        {
            string jsonStr = string.Empty;
            HttpRequest req = Context.Request;
            string strFromCity = req.Form["FromCity"];
            string strToCity = req.Form["ToCity"];
            string strDeparture = req.Form["Departure"];
            string sort = req.Form["sort"];
            int page = Convert.ToInt16(req.Form["page"] ?? "1");
            int rowsCnt = Convert.ToInt16(req.Form["rows"] ?? "100");
            string order = req.Form["order"];
            Hashtable ht = new Hashtable();
            List<Flight> lstFlight = new List<Flight>();
            try
            {
                List<Flight> rsltLstFlight = new List<Flight>();
                if (!string.IsNullOrEmpty(strFromCity) && !string.IsNullOrEmpty(strToCity) && !string.IsNullOrEmpty(strDeparture))
                {
                    lstFlight.AddRange(this.CSAIR_Get(strFromCity, strToCity, strDeparture));
                    lstFlight = EntityUtil.SortList(lstFlight, sort, order.ToEnum<EntityUtil.SortOrder>());

                    Flight[] tmpFlights = new Flight[rowsCnt];
                    lstFlight.CopyTo(page * rowsCnt, tmpFlights, 0, rowsCnt);
                    rsltLstFlight.AddRange(tmpFlights);
                }

                int total = lstFlight.Count;
                ht.Add("rows", EntityUtil.Create(rsltLstFlight));
                ht.Add("total", total);
            }
            catch (Exception ex)
            {
                ht.Add("err", ex.Message);
                if (!ht.ContainsKey("rows"))
                {
                    ht.Add("rows", EntityUtil.Create(lstFlight));
                }

                ht.Add("total", 0);
            }

            jsonStr = JsonConvert.SerializeObject(ht);
            ResponseWrite(jsonStr);
        }

        private List<Flight> CSAIR_Get(string fromCity, string toCity, string departDate)
        {
            List<Flight> lstFlight = new List<Flight>();
            // http://b2c.csair.com/B2C40/detail-SHACAN-20151211-1-0-0-0-1-0-0-0-1-0.g2c
            DateTime dtDepart = DateTime.Parse(departDate);
            string strUrl = string.Format("http://b2c.csair.com/B2C40/detail-{0}{1}-{2}-1-0-0-0-1-0-0-0-1-0.g2c", fromCity, toCity, dtDepart.ToString("yyyyMMdd"));
            XmlDocument doc = new XmlDocument();
            doc.Load(strUrl);
            XmlHelper xmlHelper = new XmlHelper(doc);
            XmlNodeList nodelist = xmlHelper.GetXmlNodeListByXpath("/page/FLIGHTS/SEGMENT/DATEFLIGHT/DIRECTFLIGHT");
            foreach (XmlNode node in nodelist)
            {
                Flight f = new Flight();
                f.C_From = fromCity;
                f.C_To = toCity;
                f.C_Departure = departDate;
                f.C_FlightNo = XmlNodeHelper.ParseByNode(node, "FLIGHTNO");
                f.C_Airline = XmlNodeHelper.ParseByNode(node, "AIRLINE");
                f.C_DEPTIME = XmlNodeHelper.ParseByNode(node, "DEPTIME");
                f.C_ARRTIME = XmlNodeHelper.ParseByNode(node, "ARRTIME");
                f.C_TotalTime = XmlNodeHelper.ParseByNode(node, "TIMEDURINGFLIGHT_en");
                StringBuilder sbPriceInfo = new StringBuilder();
                XmlNodeList xnlPrice = node.SelectNodes("CABINS/CABIN");
                foreach (XmlNode childNodePrice in xnlPrice)
                {
                    string nodeName = XmlNodeHelper.ParseByNode(childNodePrice, "NAME");
                    //if (nodeName.Equals("J"))
                    //    f.C_FirstClass = Convert.ToDecimal(XmlNodeHelper.ParseByNode(childNodePrice, "ADULTPRICE"));
                    //else if (nodeName.Equals("C"))
                    //    f.C_Economy = Convert.ToDecimal(XmlNodeHelper.ParseByNode(childNodePrice, "ADULTPRICE"));
                    //else if (nodeName.Equals("D"))
                    //    f.C_Business = Convert.ToDecimal(XmlNodeHelper.ParseByNode(childNodePrice, "ADULTPRICE"));
                    sbPriceInfo.AppendFormat("nodeName:{0}->ADULTPRICE:{1}->DISCOUNT:{2}->ADULTFAREBASIS:{3}->GBADULTPRICE:{4}"
                        + "->BRANDTYPE:{5}->MILEAGESTANDARD:{6}",
                        nodeName, XmlNodeHelper.ParseByNode(childNodePrice, "ADULTPRICE") ?? string.Empty
                        , XmlNodeHelper.ParseByNode(childNodePrice, "DISCOUNT") ?? string.Empty
                        , XmlNodeHelper.ParseByNode(childNodePrice, "ADULTFAREBASIS") ?? string.Empty
                        , XmlNodeHelper.ParseByNode(childNodePrice, "GBADULTPRICE") ?? string.Empty
                        , XmlNodeHelper.ParseByNode(childNodePrice, "BRANDTYPE") ?? string.Empty
                        , XmlNodeHelper.ParseByNode(childNodePrice, "MILEAGESTANDARD") ?? string.Empty);
                }

                f.C_Remark = sbPriceInfo.ToString();
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
