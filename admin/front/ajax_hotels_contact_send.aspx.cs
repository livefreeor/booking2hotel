using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Reviews;
using Hotels2thailand.Production;
using Hotels2thailand;
using System.Text;
using Hotels2thailand.Front;
using Hotels2thailand.UI;


public partial class ajax_hotels_contact_send : System.Web.UI.Page
{
    public string qporductId
    {
        get { return Request.QueryString["pid"]; }
    }
    public string CusId
    {
        get { return Request.QueryString["cus_id"]; }
    }

    public string LangId
    {
        get { return Request.QueryString["langid"]; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            if (Captcha.CaptchaCheck(Request.Form["captcha_valid"]))
            {
                string strProducttitle = "not identify";
                
                if (!string.IsNullOrEmpty(this.qporductId))
                {
                    Product cProduct = new Product();
                    cProduct = cProduct.GetProductById(int.Parse(this.qporductId));

                    strProducttitle = cProduct.Title;
                }

                StringBuilder mailBody = new StringBuilder();
                mailBody.Append("<div>");
                mailBody.Append("<p>" + Request.Form["mailType"] + "</p>");
                mailBody.Append("<p><span>Booking ID:</span>&nbsp;" + Request.Form["txtbooking"] + "</p>");
                mailBody.Append("<p><span>Product Name:</span>&nbsp;"+ strProducttitle + "</p>");
                mailBody.Append("<p><span>Name:</span>&nbsp;" + Request.Form["txtName"] + "</p>");
                mailBody.Append("<p><span>Phone:</span>&nbsp;" + Request.Form["txtTel"] + "</p>");

                mailBody.Append("<p><span>Message:</span>&nbsp;" + Request.Form["txtMessage"] + "</p>");
                mailBody.Append("</div>");
                string mailDisplay = Request.Form["txtEmail"];
                string mailNameDisplay = Request.Form["txtEmail"];

                string strSubject = Request.Form["mailType"] + "  Booking ID: " + Request.Form["txtbooking"];

                bool IsSent = Hotels2MAilSender.SendmailNormail(mailDisplay, mailNameDisplay, "customer@hotels2thailand.com;reservation@hotels2thailand.com",
                    strSubject, "sent@hotels2thailand.com;sent2@hotels2thailand.com", mailBody.ToString());
                if (IsSent)
                {
                    Response.Write("sent");
                }
                else
                {
                    Response.Write("failed");
                }
            }
            else
            {
                Response.Write("captchar");
            }
            Response.End();
            
        }
    }

    
  
}