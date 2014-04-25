using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Reviews;
using System.Text;

namespace Hotels2thailand.UI
{
    public partial class admin_review_site_admin_list : Hotels2BasePage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!this.Page.IsPostBack)
            {


                lblresult.Text = GenTableReviewList();
            }
           
        }

        public string GenTableReviewList()
        {
            StringBuilder result = new StringBuilder();

            ReviewWebsite siteReviewLsit = new ReviewWebsite();
            List<object[]> obj = siteReviewLsit.getReviewListALLCustomUnread();
            result.Append("<table id=\"site_review_list\" cellpadding=\"0\" cellspacing=\"0\">");
            result.Append("<tr><th class=\"tableHead\">Review Id</th><th class=\"tableHead\">Customer Name</th><th class=\"tableHead\">Date Submit</th><th class=\"tableHead\">Rebook Again</th></tr>");
            bool bolRebook = false;

            foreach (object[] items in obj)
            {
                
                DateTime dDatesubmit = (DateTime)items[2];
                result.Append("<tr onmouseover=\"changein(this)\" onmouseout=\"changeout(this)\">");
                result.Append("<td width=\"10%\"><a href=\"review_site_admin_detail.aspx?review_id=" + items[0] + "\">" + items[0] + "</a></td>");
                result.Append("<td width=\"50%\"  style=\"text-align:left;\"><a href=\"review_site_admin_detail.aspx?review_id=" + items[0] + "\">" + items[1] + "</a></td>");
                result.Append("<td width=\"20%\">" + dDatesubmit.ToString("d-MMM-yyyy") + "</td>");
               
                if (items[3] != DBNull.Value)
                {

                    bolRebook = (bool)items[3];
                    if (bolRebook)
                    {
                        result.Append("<td width=\"20%\"><img src=\"../../images/true.png\"</td>");
                    }
                    else
                    {
                        result.Append("<td width=\"20%\"><img src=\"../../images/false.png\"</td>");
                    }
                }
                else
                {
                    result.Append("<td width=\"20%\">" + items[3] + "</td>");
                }


                
                result.Append("</tr>");
            }
            result.Append("</table>");

            return result.ToString();
        }


        
    }
}