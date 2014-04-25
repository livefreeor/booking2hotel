using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Web.Security;
using System.Net;
using System.IO;
using System.Xml;
using System.Data.SqlClient;
using Hotels2thailand;
using System.Web.Configuration;


public partial class vGenerator_FeedCurrency : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //XmlTextReader xmlReader = new XmlTextReader("http://www.bangkokbank.com/RSS/FXRatesTh.xml");
        WebRequest myRequest = WebRequest.Create("http://www.bangkokbank.com/RSS/FXRatesTh.xml");
        WebResponse myResponse = myRequest.GetResponse();

        Stream rssStream = myResponse.GetResponseStream();
        XmlDocument rssDoc = new XmlDocument();
        rssDoc.Load(rssStream);

        XmlNodeList rssItems = rssDoc.SelectNodes("rss/channel/item");

        string title = "";
        string link = "";
        string description = "";
        string currencyPrefix = string.Empty;
        string currenceCode = string.Empty;

        for (int i = 2; i < rssItems.Count; i++)
        {
            XmlNode rssDetail;

            rssDetail = rssItems.Item(i).SelectSingleNode("title");

            if (rssDetail != null)
            {
                title = rssDetail.InnerText;
            }
            else
            {
                title = "";
            }

            rssDetail = rssItems.Item(i).SelectSingleNode("link");
            if (rssDetail != null)
            {
                link = rssDetail.InnerText;
            }
            else
            {
                link = "";
            }

            rssDetail = rssItems.Item(i).SelectSingleNode("description");

            if (rssDetail != null)
            {
                description = rssDetail.InnerText;
            }
            else
            {
                description = "";
            }

            string[] arrCurrency = description.Split('=');
            currencyPrefix = arrCurrency[0].Trim().Split(' ')[0];
            currenceCode = arrCurrency[1].Trim().Split(' ')[1];

            if (currenceCode == "USD50")
            {
                currenceCode = "USD";
            }

            //Response.Write("update tbl_currency set prefix=" + currencyPrefix + " where code='" + currenceCode + "' and status=1<Br>");
            string connString = WebConfigurationManager.ConnectionStrings["hotels2thailandXConnectionString"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(connString))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "update tbl_currency set prefix=@prefix where code=@code and status=1";
                cmd.Parameters.Add("@prefix", SqlDbType.Float).Value = float.Parse(currencyPrefix);
                cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = currenceCode;
                cmd.Connection = cn;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            
            //Response.Write("<p><b><a href='" + link + "' target='new'>" + title + "</a></b><br/>");
            //Response.Write("<p>" + arrCurrency[0].Trim().Split(' ')[0] + "--" + arrCurrency[1].Trim().Split(' ')[1] + "</p>");
        }
        Response.Write("Last Update:" + DateTime.Now.Hotels2ThaiDateTime());
    }
}