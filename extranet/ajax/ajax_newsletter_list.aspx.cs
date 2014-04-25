using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Production;
using System.Text;
using Hotels2thailand.Newsletters;
using System.Text.RegularExpressions;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_newsletter_list : Hotels2BasePageExtra_Ajax
    {
        public string qStatusID
        {
            get { return Request.QueryString["temp"]; }
        }

        public string qMailCat
        {
            get { return Request.QueryString["mc"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (string.IsNullOrEmpty(Request.QueryString["temp"]))
                {
                    Response.Redirect("Newsletter_exception.aspx");
                }
                else
                {
                    try
                    {
                        Response.Write(GetNewsletterList(this.qMailCat, this.qStatusID));
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message + "<br/>" + ex.StackTrace);
                    }
                    Response.End();
                }
                //if (Request.QueryString["temp"] == "0")
                //{
                //    title.Text = "<img src='/images/storemail.png'>&nbsp;&nbsp;Newsletters Store";
                //}

                //else if (Request.QueryString["temp"] == "1")
                //{
                //    title.Text = "<img src='/images/sentbox.png'>&nbsp;Sent Box";
                //}
                //else if (Request.QueryString["temp"] == "2")
                //{
                //    title.Text = "<img src='/images/outbox.png'>&nbsp;Out Box";

                //}
                //else if (Request.QueryString["temp"] == "4")
                //{
                //    title.Text = "<img src='/images/templatemail.png'>&nbsp;&nbsp;Newsletters Template";

                //}

            }
        }

        public string GetNewsletterList(string strcat, string strStatus)
        {
            StringBuilder result = new StringBuilder();

            byte bytcat = byte.Parse(strcat);
            byte bytStatus = byte.Parse(strStatus);

            Newsletter cNewsletter = new Newsletter();
            IList<object> objNewsletter = cNewsletter.GetNewsletters(bytcat, bytStatus, this.CurrentProductActiveExtra);

            string strCat = "All Customer";
            if (bytcat == 7)
                strCat = "Member Only";


            string strEmpty = string.Empty;

            if (strStatus == "6")
            {
                strEmpty = "<a href=\"#\" onclick=\"Emptymail();\"  id=\"mail_empty_link\" >Empty</a>";
            }

            if (strStatus == "1" || strStatus == "2")
            {
                strEmpty = "<a href=\"#\" onclick=\"DeleteMail();\"  id=\"mail_empty_link\" >Delete</a>";
            }

            result.Append("<p class=\"pro_status_title\">" + strCat + "&nbsp;" + strEmpty + "</p>");

            if (objNewsletter.Count > 0)
            {

                result.Append("<table width=\"100%\" id=\"tbl_news\" class=\"tbl_acknow\" cellspacing=\"2\" >");
                result.Append("<tr class=\"header_field\" align=\"center\">");
                result.Append("<th style=\"width:5%;\"><input type=\"checkbox\" id=\"mainCheck\"  /> </th><th style=\"width:5%;\">No.</th><th style=\"width:50%;\">Subject </th><th style=\"width:20%;\">Send Date</th><th style=\"width:10%\">No.of Send</th><th style=\"width:5%\">Sent</th><th style=\"width:5%;\">Delete</th>");
               
                result.Append("</tr>");

                int count = 1;

                foreach (Newsletter news in objNewsletter)
                {

                    string bgcolor = "bgcolor=\"#ffffff\"";

                   
                    if (count % 2 == 0)
                        bgcolor = "bgcolor=\"#f2f2f2\"";


                    string strLangSubject = news.Subject_Body_EN;
                    if (string.IsNullOrEmpty(news.Subject_Body_EN))
                        strLangSubject = news.Subject_Body_TH;


                    result.Append("<tr " + bgcolor + ">");
                    result.Append("<td align=\"center\"><input type=\"checkbox\" name=\"chk_news\" value=\""+news.ID+"\" /></td>");
                    result.Append("<td style=\"text-align:center;\">" + count + "</td>");
                    result.Append("<td><a href=\"sendNewsletter.aspx?ID=" + news.ID + "&mc=" +  bytcat + this.AppendQueryString+"\">" +Regex.Split( strLangSubject,"#!#")[0]+ "</a></td>");
                    result.Append("<td style=\"text-align:center;\">" + news.SentDate.ToString("dd-MMM-yyyy") + "</td>");
                    result.Append("<td style=\"text-align:center;\">" + news.TotalSend + "</td>");
                    result.Append("<td style=\"text-align:center;\">" + news.TotalSendOut + "</td>");
                    result.Append("<td style=\"text-align:center;\"><img src=\"/images_extra/bin.png\" style=\"cursor:pointer;\"  onclick=\"DarkmanPopUpComfirm(400,'Are you sure you want to delete? OK, Cancel' ,'delNews(" + news.ID + "," + news.MailCat + ")');return false;\" /></td>");
                    result.Append("</tr>");

                    count = count + 1;
                }
                result.Append("</table>");
            }
            else
            {
                result.Append("<div  class=\"box_empty\">");
                result.Append("");
                result.Append("<p><label>Sorry!</label> There are no mail to display.</p>");
                result.Append("");
                result.Append("</div>");
            }

            result.Append("");
            result.Append("");
            result.Append("");
            result.Append("");


            return result.ToString();
        }

        

    }
}