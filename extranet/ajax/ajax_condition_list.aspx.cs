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
    public partial class admin_ajax_condition_listt : Hotels2BasePageExtra_Ajax
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
                result.Append("<th width=\"5%\">No.</th>");
                result.Append("<th width=\"10%\">Condition title</th>");
                result.Append("<th width=\"5%\">Adult</th>");
                result.Append("<th width=\"10%\">Policy</th>");
                result.Append("<th width=\"10%\">Cancelltion</th>");
                result.Append("<th width=\"5%\">Edit</th>");
                result.Append("<th width=\"5%\">Delete</th>");
                result.Append("</tr>");
                result.Append("");

                int RowCount = 1;


                foreach (ProductConditionExtra cCondition in ListCondition)
                {
                    string endCode = cCondition.ConditionId.ToString() + Hotels2String.Hotels2RandomStringNuM(20);
                    endCode = endCode.Hotel2EncrytedData_SecretKey();

                    result.Append("<tr bgcolor=\"#ffffff\" align=\"center\">");
                    result.Append("<td >" + RowCount + "</td>");


                    result.Append("<td align=\"left\">" + cCondition.Title + Hotels2String.AppendConditionDetailExtraNet(cCondition.Breakfast) + "</td>");

                    if (cCondition.NumAult <= 3)
                        result.Append("<td align=\"left\"><img src=\"http://www.hotels2thailand.com/images_extra/adult_" + cCondition.NumAult + ".png\" alt=\"For adult " + cCondition.NumAult + "\" title=\"For adult " + cCondition.NumAult + "\" /></td>");
                    else
                        result.Append("<td align=\"left\"><img src=\"http://www.hotels2thailand.com/images_extra/adult_more" + cCondition.NumAult + ".png\" alt=\"For adult " + cCondition.NumAult + "\" title=\"For adult " + cCondition.NumAult + "\" /></td>");

                    result.Append("");
                    ProductConditionContentExtra cPolicyExtra = new ProductConditionContentExtra();
                    List<object> listPolicy = cPolicyExtra.GetListConditionDetail_policyByConditionId(cCondition.ConditionId, 1);
                    result.Append("<td ><a href=\"\" onclick=\"return false;\"  class=\"tooltip\" >policy");
                    result.Append("<span class=\"tooltip_content\" style=\"font-size:11px;\">");
                    foreach (ProductConditionContentExtra policy in listPolicy)
                    {
                        result.Append("<p><img src=\"/images/ico-square-small.png\" />&nbsp;<label style=\"font-weight:bold;\">" + policy.Title + "</label>&nbsp;:&nbsp;<label style=\"font-size:11px;\">" + policy.Detail + "</label></p>");
                        result.Append("");
                        result.Append("");
                    }

                    result.Append("</span>");
                    result.Append("</a>");
                    result.Append("</td>");
                    result.Append("<td><a href=\"\" onclick=\"return false;\" class=\"tooltip\" >cancellation");
                    result.Append("<span class=\"tooltip_content\" >");

                    ProductCondition_Cancel_Extra cConditionCancel = new ProductCondition_Cancel_Extra();
                    List<object> ListCancel = cConditionCancel.GetProductCancelExtraListByConditionID(cCondition.ConditionId);
                    result.Append("<table >");
                    
                    int rowCount = 0;
                    foreach (ProductCondition_Cancel_Extra cancel in ListCancel)
                    {
                        string rowcolor = "#edeff4";
                       
                        result.Append("<tr bgcolor=\"" + rowcolor + "\">");
                        result.Append("<td>");
                        result.Append("<label style=\"font-weight:bold;\">" + cancel.DateStart.ToString("dd-MMM-yyy") + "&nbsp; - &nbsp;" + cancel.DateEnd.ToString("dd-MMM-yyy") + "</label>");
                        result.Append("");
                        result.Append("<table style=\"font-size:11px;\">");
                        ProductCondition_Cancel_Extra_Rule cCancelRule = new ProductCondition_Cancel_Extra_Rule();
                        List<object> listRule = cCancelRule.getCencelRuleExtranetbyCancelId(cancel.CancelID);

                        int count = 1;
                        foreach (ProductCondition_Cancel_Extra_Rule rule in listRule)
                        {
                           
                            result.Append("<tr>");
                            result.Append("<td> <img src=\"/images/ico-square-small.png\" />&nbsp;");
                            result.Append("" + Hotels2String.CancellationGenerateWording(true,rule.DayCancel, 0,0,rule.ChargePercent, rule.Chargenight) + "");
                            result.Append("</td>");
                            result.Append("</tr>");

                            count = count + 1;
                        }
                        result.Append("</table>");
                        result.Append("</td>");
                        
                        rowCount = rowCount + 1;
                    }

                    result.Append("</tr>");
                    result.Append("</table>");
                  
                    result.Append("</span></a>");
                    result.Append("</td>");
                    result.Append("<td><img src=\"../../images_extra/edit.png\" style=\"cursor:pointer;\" onclick=\"EditCondition('" + endCode + "');\" /></td>");
                    result.Append("<td><img src=\"../../images_extra/bin.png\" style=\"cursor:pointer;\" onclick=\"DarkmanPopUpComfirm(400,'Are you sure you want to delete? OK, Cancel' ,'delCondition(" + cCondition.ConditionId + ")');return false;\" /></td>");
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
                result.Append("<p><label>Sorry!</label> No Condition for this room type</p>");
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