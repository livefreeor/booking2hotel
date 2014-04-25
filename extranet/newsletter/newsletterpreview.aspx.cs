using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Newsletters;
using System.Text.RegularExpressions;
public partial class admin_maillinglist_newsletterpreview : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["ID"]) && !string.IsNullOrEmpty(Request.QueryString["langId"]))
        {
            Newsletter cNewsletter = new Newsletter();
            int newsid = int.Parse(Request.QueryString["ID"]);
           
            cNewsletter = cNewsletter.GetNewsletterByID(newsid);
            //btnPreview.Visible = false;

            string strBody = string.Empty;
            string strSubject = string.Empty;
            switch(Request.QueryString["langId"])
            {
                case "1":
                    string[] arrEN = Regex.Split(cNewsletter.Subject_Body_EN, "#!#");
                    strBody = arrEN[1];
                    strSubject = arrEN[0];
                    break;
                case "2":
                   string[] arrTH = Regex.Split(cNewsletter.Subject_Body_TH, "#!#");
                   strBody = arrTH[1];
                   strSubject = arrTH[0];
                    break;
            }

            Response.Write("<p><span>Subject: </span>"+strSubject+ "</p>");
            Response.Write(strBody);
            Response.End();
            
        }
    }
}