using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand;
using System.Text;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_condition_list_min_day : Hotels2BasePageExtra_Ajax
    {
        public string qOptionId
        {
            get { return Request.QueryString["oid"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.qOptionId))
                {
                    Response.Write(GetconditionList(int.Parse(this.qOptionId)));
                    //Response.Write("HELL");
                }
                Response.End();
            }
        }

        
        
        public string GetconditionList(int intOptionid)
        {
            StringBuilder result = new StringBuilder();
            ProductConditionExtra cConditionextra = new ProductConditionExtra();
            List<object> ListCondition = cConditionextra.getConditionListByOptionId(intOptionid, 1);

            if (ListCondition.Count() > 0)
            {

                result.Append("<table class=\"tbl_acknow\" cellspacing=\"2\">");
                result.Append("<tr class=\"header_field\">");
                result.Append("<th >No.</th>");
                result.Append("<th>Condition title</th>");
                result.Append("<th>Minimum Detail</th>");
                result.Append("<th>Manage</th>");
                
                result.Append("</tr>");
                result.Append("");


                int RowCount = 1;
                foreach (ProductConditionExtra cCondition in ListCondition)
                {
                    string endCode = cCondition.ConditionId.ToString() + Hotels2String.Hotels2RandomStringNuM(20);
                    endCode = endCode.Hotel2EncrytedData_SecretKey();

                    result.Append("<tr bgcolor=\"#ffffff\" align=\"center\">");
                    result.Append("<td >" + RowCount + "</td>");
                    result.Append("<td align=\"left\">" + cCondition.Title + Hotels2String.AppendConditionDetailExtraNet(cCondition.NumAult, cCondition.Breakfast) +"</td>");


                    result.Append("<td><a href=\"\" onclick=\"return false;\" class=\"tooltip\" >Minimum Detail");
                    result.Append("<span class=\"tooltip_content\">");

                    ConditionminimumDayExtranet cConditionMin = new ConditionminimumDayExtranet();
                    List<object> ListConditionMin = cConditionMin.GetConditionMinimumstayListByConditionId(cCondition.ConditionId);

                    if (ListConditionMin.Count > 0)
                    {
                        foreach(ConditionminimumDayExtranet Min in ListConditionMin)
                        {
                        result.Append("<table >");
                        result.Append("<tr>");
                        result.Append("<td>");
                        result.Append("<img src=\"/images/ico-square-small.png\" />");
                        result.Append("</td>");
                        result.Append("<td>");
                        result.Append("From <label style=\"font-weight:bold\">" + Min.DateStart.ToString("dd-MMM-yyyy") + "</label>");
                        result.Append("</td>");
                        result.Append("<td>");
                        result.Append("To <label style=\"font-weight:bold\">" + Min.DateEnd.ToString("dd-MMM-yyyy") + "</label>");
                        result.Append("</td>");
                        result.Append("<td>");
                        result.Append(",<label style=\"font-weight:bold\"> " + Min.NumMin + "</label> Night(s)");
                        result.Append("</td>");
                        result.Append("</tr>");
                        result.Append("</table>");
                        }
                    }
                    else
                    {
                        result.Append("<p style=\"font-weight:bold\">No Mimimum night is added</p>");
                    }
                    
                    result.Append("</span></a>");
                    result.Append("</td>");
                    result.Append("<td><img src=\"../../images_extra/edit.png\" style=\"cursor:pointer;\" onclick=\"EditCondition('" + endCode + "');\" /></td>");
                   
                    result.Append("");
                    result.Append("</tr>");

                    RowCount = RowCount + 1;
                }
                result.Append("</table>");
                result.Append("");
                result.Append("");
                result.Append("");
            }
            else
            {
                result.Append("<div class=\"box_empty\">");
                result.Append("");
                result.Append("<p>Sorry! No Condition for this room type</p>");
                result.Append("");
                result.Append("</div>");
            }
            return result.ToString();
        }

        public string ConvertRuletoStringWording(byte bytDayCancel, byte bytPercentCh, byte nightCh, bool Islast)
        {
            string result = string.Empty;

            string strDaycancel = string.Empty;
            string strperch = string.Empty;
            string strpernig = string.Empty;

            //string[] Sequence = {"st", "st", "nd", "rd", "th"};

            strDaycancel = bytDayCancel.ToString();
            if (bytDayCancel == 0)
                strDaycancel = "No-Show";
            else if (bytDayCancel > 0 && Islast)
            {
                strDaycancel = "More than &nbsp;" + bytDayCancel + "&nbsp;days prior to arrival";
            }
            else
            {
                strDaycancel = "Within&nbsp;" + bytDayCancel + "&nbsp;Days";
            }

            if (bytPercentCh == 0 && nightCh > 0)
                result = strDaycancel + ",&nbsp;" + nightCh + getSequence(nightCh) + "&nbsp;Night Charged";
            else if (bytPercentCh > 0 && nightCh == 0)
                result = strDaycancel + ",&nbsp;" + bytPercentCh + "&nbsp;Percent Charged";
            else
                result = "incorrect format";

            return result;
        }

        public string getSequence(byte bytNum)
        {
            string strSeq = "st";
            if (bytNum == 2)
                strSeq = "nd";
            else if (bytNum == 3)
                strSeq = "rd";
            else if (bytNum > 3)
                strSeq = "th";

            return strSeq;
        }
    }
}