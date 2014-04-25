using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.ProductOption;
using System.Text;
using Hotels2thailand.Production;
using Hotels2thailand.Member ;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_member_list : Hotels2BasePageExtra_Ajax
    {
        
        public string qPageNum
        {
            get { return Request.QueryString["pg"]; }

        }

        public string qIsActive
        {
            get { return Request.QueryString["act"]; }
        }

        public string qStatus
        {
            get { return Request.QueryString["sta"]; }
        }

        public string qQuickSearch
        {
            get { return Request.QueryString["qs"]; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {

                Response.Write(memberList());
                Response.End();

            }
        }

        

        public string memberList()
        {
            StringBuilder result = new StringBuilder();

            bool Isactive = false;
            bool Status = false;
            int intPageNum = 0; 
            int intProduct = this.CurrentProductActiveExtra;
            string MenustrStatus = "";
            string str_hdHistory_type = "";

            Member_customer cMember = new Member_customer();
            IList<object> iListMember = null;
            if (string.IsNullOrEmpty(this.qQuickSearch))
            {
                Isactive = bool.Parse(this.qIsActive);
                Status = bool.Parse(this.qStatus);

                intPageNum = int.Parse(this.qPageNum);
                iListMember = cMember.getMember(intProduct, intPageNum, Isactive, Status);



                if (Status)
                {
                    if (Isactive)
                    {
                        MenustrStatus = "<a href=\"\" id=\"block_list\">Block</a>";
                        str_hdHistory_type = "active";
                    }
                    else
                    {
                        MenustrStatus = "<a href=\"\" id=\"block_list\">Block</a>";
                        str_hdHistory_type = "inactive";
                    }
                    //MenustrStatus = "<a href=\"\" id=\"block_list\">Block</a>&nbsp;|&nbsp;<a href=\"\" id=\"active_list\">Activate</a>";
                }
                else
                {
                    MenustrStatus = "<a href=\"\" id=\"block_list\">Unblock</a>";
                    str_hdHistory_type = "blocked";
                }
            }
            else
            {
                iListMember = cMember.getMemberSearch(intProduct, this.qQuickSearch);
                str_hdHistory_type = "search=" + this.qQuickSearch;
            }

            int intMemberCount = iListMember.Count();

            result.Append("<input type=\"hidden\" id=\"hd_history_type\" value=\"" + str_hdHistory_type + "\" />");
            if (intMemberCount > 0)
            {

                double total = Math.Ceiling((intMemberCount / (double)50));
                result.Append("<div id=\"submenu\"><span id=\"action_title\">Active tool:</span>&nbsp;" + MenustrStatus + "</div>");
                result.Append("<input type=\"hidden\" id=\"hd_memberCount\" value=\"" + intMemberCount + "\" />");
                
                result.Append("<table class=\"darkman_tbls\" >");
                result.Append("<tr class=\"darkman_th\">");
                result.Append("<th><input type=\"checkbox\" id=\"check_main\" /></th>");
                result.Append("<th>No.</th>");
                result.Append("<th>Name</th>");
                result.Append("<th>Email</th>");
                result.Append("<th>Activation</th>");
                result.Append("<th>Member status</th>");
                result.Append("</tr>");

                int count = 1;
                string strEvent = "";
                string strActive = "";
                string strStatus = "";
                
                //Response.Write(Isactive + "--" + Status + "---" + intProduct + "---" + intPageNum);
                //Response.End();
                string AppendQueryString = string.Empty;
                if (!string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qSupplierId))
                    AppendQueryString = "&pid=" + this.qProductId + "&supid=" + this.qSupplierId;
                foreach (Member_customer member in iListMember)
                {


                    if ((count % 2) == 0)
                        strEvent = "darkman_row_event";
                    else
                        strEvent = "darkman_row";

                    if (member.Isactive)
                    {
                        strActive = "status_active.png"; 
                    }
                    else
                    {
                        strActive = "status_inactive.png"; 
                    }


                    if (member.Status)
                        strStatus = "member_status_unblock.png";
                    else
                    {
                        strStatus = "member_status_blocked.png";
                       
                    }
                    result.Append("<tr class=\"" + strEvent + " rowstyle\">");
                    result.Append("<td><input type=\"checkbox\" value=\"" + member.CustomerID + "\" name=\"cus_checked\" /></td>");
                    result.Append("<td>" + count + "</td>");
                    result.Append("<td style=\"text-align:left;\"><a class=\"link_member_detail\" href=\"member_detail.aspx?mid=" + member.CustomerID + AppendQueryString+"\">" + member.FullName + "</a></td>");
                    result.Append("<td style=\"text-align:left;\">" + member.Email + "</td>");
                    result.Append("<td><img src=\"http://order.booking2hotels.com/images_extra/" + strActive + "\" /></td>");
                    result.Append("<td><img src=\"http://order.booking2hotels.com/images_extra/" + strStatus + "\" /></td>");
                    result.Append("</tr>");

                    count = count + 1;
                }


                result.Append("</table>");


                if (total > 1)
                {

                    result.Append("<div id=\"page_num\">");
                    result.Append("<table>");
                    result.Append("<tr>");
                    result.Append("<td><span id=\"previous\">Previous</span></td>");
                    result.Append("<td>");
                    result.Append("<ul class=\"ul_page\">");


                    for (int i = 1; i <= total; i++)
                    {
                        if (intPageNum == i)
                            result.Append("<li><a class=\"page_active\" href=\"" + i + "\">" + i + "</a></li>");
                        else
                            result.Append("<li><a href=\"" + i + "\">" + i + "</a></li>");

                    }

                    result.Append("</ul>");
                    result.Append("</td>");
                    result.Append("<td id=\"next\">Next</td>");
                    result.Append("</tr>");
                    result.Append("</table>");
                    result.Append("</div>");
                }
            }
            else
            {

                result.Append("<div  class=\"box_empty\">");
                result.Append("");
                result.Append("<p><label>Sorry!</label> There are no Holidays in this year.</p>");
                result.Append("");
                result.Append("</div>");
            }
            return result.ToString();
        }
    }
}