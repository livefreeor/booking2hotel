using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using Hotels2thailand.Production;
using System.Net.Mail;

public partial class ajax_thailand_hotels_tell : System.Web.UI.Page
{
    public string qProductId
    {
        get { return Request.QueryString["pd"]; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string stChatcha = Request.Form["captcha_valid"];

        if (Captcha.CaptchaCheck(stChatcha))
        {
            //Response.Redirect("www.mthai.com");
            Response.Write(Sendmail());
        }
        else
            Response.Write("NO");
        //Response.Write(Session["Captcha"]);
        Response.End();
    }

    public string Sendmail()
    {
        

        int intProductdId = int.Parse(Request.QueryString["pd"]);

        ProductContent cProductContent = new ProductContent();
        cProductContent = cProductContent.GetProductContentById(intProductdId, 1);
        Product cProduct = new Product();
        cProduct = cProduct.GetProductById(intProductdId);
        string FolderName = cProduct.DestinationFolderName + "-" + Utility.GetProductType(cProduct.ProductCategoryID)[0, 3];
        string Url = cProductContent.FileMain;

        string maildisplay = Request.Form["email"];
        string mailNamedisplay = Request.Form["fName"];
        string mailTo = Request.Form["friendEmail"];
        string FriendsName = Request.Form["friendName"];
        MailMessage mail = new MailMessage();

        mail.From = new MailAddress(maildisplay, mailNamedisplay);
        mail.To.Add(mailTo);
        mail.Subject = "A hotel I found on Hotels2thailand.com";

        StringBuilder Result = new StringBuilder();
        Result.Append("<div class=\"main\" style=\"margin:0 auto; padding:10px 11px 10px 10px; width:700px; background:#f7fafd;\">\r\n");
        Result.Append("<div class=\"content\" style=\"margin:0 auto; padding:20px; width:660px; border:1px solid #c8d4e9; background:#FFF;\">\r\n");
        Result.Append("<img src=\"http://www.hotels2thailand.com/thailand-hotels-pic/" + cProduct.ProductCode + "_a.jpg\" style=\"float:left; padding:0 20px 20px 0;\" />\r\n");
        Result.Append("<div class=\"detail\" style=\"margin:0px; padding:0px; width:390px; float:left; font-size:14px; font-family:Verdana, Geneva, sans-serif;\">\r\n");
        Result.Append("<img src=\"http://www.hotels2thailand.com/theme_color/blue/images/layout_mail/to_people.jpg\" style=\"float:left; padding-right:5px;\" />\r\n");
        Result.Append("<div class=\"to\" style=\"font-size:18px;\">" + FriendsName + "</div>\r\n");
        Result.Append("<p style=\"margin:10px 0 10px 0; padding:10px 10px 10px 20px; font-size:12px; line-height:18px; border:solid 2px #c8d4e9;\">\r\n");
        Result.Append("<a href=\"http://www.hotels2thailand.com/" + FolderName + "/" + Url + "\">Click here to see the hotel I found</a>\r\n");
        Result.Append("I found this hotel on Hotels2Thailand.com and thought you might find it of interest.\r\n");
        Result.Append("</p>\r\n");
        Result.Append("<div class=\"to\" style=\"font-size:18px; float:right;\">" + mailNamedisplay + "</div>\r\n");
        Result.Append(" <img src=\"http://www.hotels2thailand.com/theme_color/blue/images/layout_mail/to_sent.jpg\" style=\"float:right; padding-right:5px;\"  />\r\n");
        Result.Append("<br style=\"clear:both\" />\r\n");
        Result.Append("</div>\r\n");
        Result.Append("<br style=\"clear:both\" />\r\n");
        Result.Append("<div class=\"detail\" style=\"margin:0px; padding:0px; width:660px; font-size:14px; font-family:Verdana, Geneva, sans-serif;\">\r\n");
        Result.Append("<img src=\"http://www.hotels2thailand.com/theme_color/blue/images/layout_mail/megaphone.jpg\" style=\"float:left; padding-right:8px;\"/>\r\n");
        Result.Append("<p style=\"margin:0px; padding:4px 5px 4px 5px; width:610px; font-size:11px; background:#ebf1f9; float:left;\">\r\n");
        Result.Append("This e-mail was sent at the request of " + mailNamedisplay + ". Your e-mail address will not be sold or used for promotional purposes.</p>\r\n");
        Result.Append("<br style=\"clear:both\" />\r\n");
        Result.Append("</div>\r\n");
        Result.Append("</div>\r\n");
        Result.Append("</div>\r\n");


        mail.Body = Result.ToString();
        mail.IsBodyHtml = true;

        SmtpClient smtpClient = new SmtpClient();
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.Host = "mail.hotels2thailand.com";
        smtpClient.Credentials = new System.Net.NetworkCredential("peerapong@hotels2thailand.com", "F=8fuieji;pq");
        smtpClient.Send(mail);

        return mailTo;
    }

   

    
}