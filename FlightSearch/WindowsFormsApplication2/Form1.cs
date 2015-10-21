using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Web;
using System.Xml;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        SqlCommand da1, da2, da3, da4;
        SqlDataAdapter adapt1;
        DataSet ds1;
        BindingSource bindingSource1;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string s = @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\UMAC\704\Course Project\DDBpro\FLY.mdf;";
            //s += "DATABASE=FLY;SERVER=localhost;Integrated Security=True;User Instance=False";
            string s=@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\UMAC\704\Course Project\DDBpro\FLY.mdf;Integrated Security=True;Connect Timeout=30";
            string address1="";
            string address2="";
            string address3, address4, address5;
            int flag = 0;
            if (comboBox1 .Text ==""||comboBox2 .Text ==""||comboBox3.Text == ""||comboBox4.Text == ""||comboBox5.Text == "")
            {
                MessageBox.Show("信息不完全！");
                return;
            }
            ///////////////////////////////////////////////////
            conn = new SqlConnection(s);
            conn.Open();
            string txt1 = "Select number From City ";
            txt1 += "Where name= '";
            txt1 += comboBox1 .Text  + " '";
            da1 = new SqlCommand(txt1, conn);
            SqlDataReader dr1;
            dr1 = da1.ExecuteReader();
            while (dr1.Read())
            {
                string[] s1 = new string[]
                {
                    dr1.GetString (0)
                };
                address1 = s1[0];
            }
            dr1.Close();
            ///////////////////////////////////////////////////
            string txt2 = "Select number from City ";
            txt2 += "Where name='";
            txt2 += comboBox2.Text + "'";
            da2 = new SqlCommand(txt2, conn);
            SqlDataReader dr2;
            dr2 = da2.ExecuteReader();
            while (dr2.Read())
            {
                string[] s2 = new string[]
                {
                    dr2.GetString (0)
                };
                address2 = s2[0];
            }
            dr2.Close();
            ///////////////////////////////////////////////////
            address3 = comboBox3.Text;
            address4 = comboBox4.Text ;
            address5 = comboBox5.Text ;
            ///////////////////////////////////////////////////
            DateTime today = DateTime.Now.Date;
            string chosenday = address3 + address4 + address5;
            DateTime choose = DateTime.ParseExact (chosenday, "yyyyMMdd", null);
            if (DateTime.Compare(today, choose) > 0)
            {
                MessageBox.Show("输入时间过期");
                return;
            }
            ///////////////////////////////////////////////////
            XmlDocument doc = new XmlDocument();
           
            doc.Load("http://b2c.csair.com/B2C40/detail-"+address1+address2+"-"+address3+address4+address5+"-1-0-0-0-1-0-0-0-1-0.g2c");
            //XmlTextReader rdr = new XmlTextReader("http://b2c.csair.com/B2C40/detail-"+address1+address2+"-"+address3+address4+address5+"-1-0-0-0-1-0-0-0-1-0.g2c");
            XmlNodeList nodelist = doc.SelectNodes("//FLIGHT");
            List<flight>list = new List<flight>();
            string url = "http://b2c.csair.com/B2C40/detail-" + address1 + address2 + "-" + address3 + address4 + address5 + "-1-0-0-0-1-0-0-0-1-0.g2c";
            System.Diagnostics.Debug.WriteLine(url);
            foreach (XmlNode node in nodelist)
            {
                DateTime takeoff, landing;
                flight f = new flight();
                f.startplace = comboBox1.Text;
                f.destination = comboBox2.Text;
                f.begintime = choose;
                f.flightno = node.SelectSingleNode("./FLIGHTNO/text()").Value;
                f.airline = f.flightno.Substring(0, 2);
                takeoff = DateTime.ParseExact(node.SelectSingleNode("./DEPTIME/text()").Value, "HHmm", null);
                f.takeoff = takeoff.ToLongTimeString();
                landing = DateTime.ParseExact(node.SelectSingleNode("./ARRTIME/text()").Value, "HHmm", null);
                f.landing = landing.ToLongTimeString();
                XmlNodeList price = node.SelectNodes(".//ADULTPRICE/text()");
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
                if (min ==10000f)
                {
                    min = 0f;
                }
                f.firstclass = max;
                f.economy = min;
                list.Add(f);
                flag = 1;  
            }
            //////////////////////////////////////////////////
            foreach (flight f in list)
            {
                 try   //将一组列集体设为unique，这样可以防止输入重复值，所以要抛出异常
                {
                      string txt3 = "Insert into Flight (Startplace,Destination,Begintime,Airline,FlightNo,Takeoff,Landing,Firstclass,Economy)Values('";
                      txt3 += f.startplace  + "','";
                      txt3 += f.destination  + "','";
                      txt3 += f.begintime  + "','";
                      txt3 += f.airline + "','";
                      txt3 += f.flightno + "','";
                      txt3 += f.takeoff + "','";
                      txt3 += f.landing + "' , '";
                      txt3 += f.firstclass + "' , '";
                      txt3 += f.economy + "')";
                      da3 = new SqlCommand(txt3, conn);
                      da3.ExecuteNonQuery();
                 }
                 catch (System.Data.SqlClient.SqlException ext)
                 {
                    
                 } 
            }
            /////////////////////////////////////////////////////
            string ceairURL = "http://www.ceair.com/mu/front/reservation/flight-search!doFlightSearch.shtml?" + "cond.tripType=OW&cond.depCode=" + address1 + "&cond.depDate=" + address3 + "-" + address4 + "-" + address5
                + "&cond.arrCode=" + address2 + "&cond.arrDate=" + address3 + "-" + address4 + "-" + address5 + "&cond.depCodeInt=&cond.arrCodeInt=&cond.depDateInt=2012-12-23&cond.depCodeIntRt=&cond.arrCodeIntRt=&cond.depDateIntRt=2012-12-23&cond.cabinRank=ECONOMY&cond.adultNumber=1&cond.childNumber=0&cond.sortType=1&cond.isInternationalFlight=331&cond.region=CN";
            Byte[] pageData = new WebClient().DownloadData("http://www.ceair.com/mu/front/reservation/flight-search!doFlightSearch.shtml?" + "cond.tripType=OW&cond.depCode=" + address1+ "&cond.depDate="+ address3+"-"+address4+"-"+address5 
                + "&cond.arrCode="+address2+"&cond.arrDate="+address3+"-"+address4+"-"+address5+"&cond.depCodeInt=&cond.arrCodeInt=&cond.depDateInt=2012-12-23&cond.depCodeIntRt=&cond.arrCodeIntRt=&cond.depDateIntRt=2012-12-23&cond.cabinRank=ECONOMY&cond.adultNumber=1&cond.childNumber=0&cond.sortType=1&cond.isInternationalFlight=331&cond.region=CN");
            string pageHtml = Encoding.UTF8.GetString(pageData);
            string pagePart = Regex.Match(pageHtml, "<table\\swidth=\"100%\"\\sborder=\"0\"\\scellspacing=\"1\"\\scellpadding=\"0\"\\sclass=\"flight_info\"\\sid=\"go_table\">\\s[\\s\\S]*</table>").Value;
            Match pageTr = Regex.Match(pagePart, "<tr\\sflightinfourl[\\s\\S]*?</tr>");
            List<string> queue = new List<string>();
            while (pageTr.Success)
            {
                queue.Add(pageTr.Value);
                pageTr = pageTr.NextMatch();
            }
            List<flight> array = new List<flight>();
            foreach (string tr in queue)
            {
                string flight = Regex.Match(tr, "MU\\d{4}&\\d").Value;
                flight = flight.Substring(0, 6);

                string airline = flight.Substring(0, 2);

                string pageTd = Regex.Match(tr, "order_time\\\">\\d{2}:\\d{2}[\\s\\S]*?&rarr;[\\s\\S]*?\\d{2}:\\d{2}").Value;
                Match timeMat = Regex.Match(pageTd, "\\d{2}:\\d{2}");

                string takeoff = timeMat.Value;
                timeMat = timeMat.NextMatch();

                string landing = timeMat.Value;

                string firstclass = Regex.Match(tr, "\\d{3,4}.\\d&F").Value;
                if (firstclass == "")
                {
                    firstclass = "0";
                }
                else
                {
                    firstclass = firstclass.Substring(0, firstclass.Length - 2);
                }
                string economy = Regex.Match(tr, "\\d{3,4}.\\d&Y").Value;
                if (economy == "")
                {
                    economy = "0";
                }
                else
                {
                    economy = economy.Substring(0, economy.Length - 2);
                }
                flight t = new flight();
                t.begintime = choose;
                t.startplace = comboBox1.Text;
                t.destination = comboBox2.Text;
                t.airline = airline;
                t.flightno = flight;
                t.takeoff = takeoff;
                t.landing = landing ;
                t.firstclass = float.Parse(firstclass);
                t.economy = float.Parse(economy);
                array.Add(t);
                flag = 1;
            }
            ////////////////////////////////////////////////////
            foreach (flight t in array )
            {
                try
                {
                    string txt = "Insert into Flight (Startplace,Destination,Begintime,Airline,FlightNo,Takeoff,Landing,Firstclass,Economy)Values('";
                    txt += t.startplace + "','";
                    txt += t.destination + "','";
                    txt += t.begintime + "','";
                    txt += t.airline + "','";
                    txt += t.flightno + "','";
                    txt += t.takeoff + "','";
                    txt += t.landing + "' , '";
                    txt += t.firstclass + "' , '";
                    txt += t.economy + "')";
                    da4 = new SqlCommand(txt, conn);
                    da4.ExecuteNonQuery();
                }
                catch (System.Data.SqlClient.SqlException ext)
                {

                }  
            }
            if (flag == 0)
            {
                MessageBox.Show("该航线暂时无法查询！");
                return;
            }
            /////////////////////////////////////////////////////
            string txt4 = "Select Airline,FlightNo,Takeoff,Landing,Firstclass,Economy from Flight ";
            txt4 += "Where Startplace='";
            txt4 += comboBox1.Text + "' ";
            txt4 += "AND Destination='";
            txt4 += comboBox2.Text + "' ";
            txt4 += "AND Begintime='";
            txt4 += choose + "'";
            adapt1 = new SqlDataAdapter(txt4, conn);
            ds1 = new DataSet();
            adapt1.Fill(ds1, "Flight");
            bindingSource1 = new BindingSource();
            bindingSource1.DataSource = ds1;
            bindingSource1.DataMember = "Flight";
            dataGridView1.DataSource = bindingSource1;
            conn.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
