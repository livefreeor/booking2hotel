using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.ProductOption;
using System.Text;
using Hotels2thailand.Production;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_condition_minimum_night_list : Hotels2BasePageExtra_Ajax
    {
        public string qConditionId {
            get { return Request.QueryString["conid"]; } 
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(qConditionId))
                {
                    //Response.Write(qConditionId);
                    Response.Write(getPricePeriodList());
                }

                Response.End();


            }
        }


        public string getPricePeriodList()
        {
            string result = string.Empty;
            try
            {

                int intCondition = int.Parse(this.qConditionId);

                result = result + "<h4><img id=\"Image4\" src=\"/images/content.png\" /> Current Minimum Night</h4>";


                result = result + "<table  class=\"tbl_acknow\" cellspacing=\"2\" >";
                result = result + "<tr class=\"header_field\" ><th>Date From</th><th>Date To</th><th>Minimum</th><th>Update</th><th>Bin</th></tr>";

                ConditionminimumDayExtranet cConditionMin = new ConditionminimumDayExtranet();

                List<object> ListMin = cConditionMin.GetConditionMinimumstayListByConditionId(intCondition);


                if (ListMin.Count > 0)
                {


                    foreach (ConditionminimumDayExtranet Min in ListMin)
                    {

                        result = result + "<tr bgcolor=\"#ffffff\" align=\"center\">";
                        result = result + "<td><input type=\"text\" class=\"Extra_textbox\" id=\"min_date_From_" + Min.MinStayId + "\" name=\"min_date_From_" + Min.MinStayId + "\" value=\"" + Min.DateStart.ToString("yyyy-MM-dd") + "\" />" + "<input type=\"checkbox\" style=\"display:none;\" checked=\"checked\" name=\"minimum_checked_list\" value=\"" + Min.MinStayId + "\"  /></td>";

                        result = result + "<td><input type=\"text\" class=\"Extra_textbox\" id=\"min_date_To_" + Min.MinStayId + "\" name=\"min_date_To_" + Min.MinStayId + "\" value=\"" + Min.DateEnd.ToString("yyyy-MM-dd") + "\" /></td>";

                        result = result + "<td><select name=\"min_day_amount_" + Min.MinStayId + "\" class=\"Extra_Drop\" \">";
                        for (int i = 1; i <= 30; i++)
                        {
                            if (i == Min.NumMin)
                                result = result + "<option value=\"" + i + "\" selected=\"selected\">" + i + "</option>";
                            else
                                result = result + "<option value=\"" + i + "\">" + i + "</option>";
                        }
                        result = result + "";
                        result = result + "</select></td>";
                        result = result + "<td><input type=\"button\" class=\"Extra_Button_small_green\" onclick=\"MinDayUpdate('" + Min.MinStayId + "','" + intCondition + "');return false;\" value=\"Save\" /></td>";
                        result = result + "<td><img src=\"/images_extra/bin.png\" onclick=\"DarkmanPopUpComfirm(400,'Are you sure to delete' ,'delMinimum(" + Min.MinStayId + ")');return false;\"  style=\"cursor:pointer;\"  /></td>";

                        result = result + "</tr>";



                    }



                }
                else
                {
                    result = "<div class=\"box_empty\">";

                    result = result + "<p><label>No Minimum Night</label> Please insert new one</p>";

                    result = result + "</div>";

                }


            }
            catch (Exception ex)
            {
                result = "error: " + ex.Message;
            }

            result = result + "</table>";

            return result;
        }
    }
}