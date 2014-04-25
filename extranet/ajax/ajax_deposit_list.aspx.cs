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
    public partial class admin_ajax_deposit_list : Hotels2BasePageExtra_Ajax
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                 
               Response.Write(getPricePeriodList());
                Response.End();

            }
        }

        public string SelectCompare(string Val1, string Val2)
        {
            string result = "";
            if (Val1 == Val2)
                return result = "Selected=\"Selected\"";
            return result;
        }
        public string getPricePeriodList()
        {
            string result = string.Empty;
            try
            {

                result = result + "<h4><img id=\"Image4\" src=\"/images/content.png\" /> Current Deposit</h4>";


                result = result + "<table  class=\"tbl_acknow\" cellspacing=\"2\" >";
                result = result + "<tr class=\"header_field\" ><th>Date From</th><th>Date To</th><th>Type</th><th>Amount</th><th>Update</th><th>Bin</th></tr>";

                //ConditionminimumDayExtranet cConditionMin = new ConditionminimumDayExtranet();

                Productdeposit cDeposit = new Productdeposit();


                IList<object> ListDep = cDeposit.GetDepositListByProductId(this.CurrentProductActiveExtra);

                

                if (ListDep.Count > 0)
                {


                    foreach (Productdeposit dep in ListDep)
                    {

                        result = result + "<tr bgcolor=\"#ffffff\" align=\"center\">";
                        result = result + "<td><input type=\"text\" class=\"Extra_textbox\" id=\"dep_date_From_" + dep.DepositId + "\" name=\"dep_date_From_" + dep.DepositId + "\" value=\"" + dep.DateStart.ToString("yyyy-MM-dd") + "\" />" + "<input type=\"checkbox\" style=\"display:none;\" checked=\"checked\" name=\"deposit_checked_list\" value=\"" + dep.DepositId + "\"  /></td>";

                        result = result + "<td><input type=\"text\" class=\"Extra_textbox\" id=\"dep_date_To_" + dep.DepositId + "\" name=\"dep_date_To_" + dep.DepositId + "\" value=\"" + dep.DateEnd.ToString("yyyy-MM-dd") + "\" /></td>";

                        result = result + "<td><select name=\"deposit_cat_" + dep.DepositId + "\" class=\"Extra_Drop\" >";
                        result = result + "<option value=\"1\" "+SelectCompare(dep.DepositCat.ToString(),"1")+">Paid Before Check in (%)</option>";
                        result = result + "<option value=\"2\" " + SelectCompare(dep.DepositCat.ToString(), "2") + ">Paid Before Check in (night)</option>";
                        result = result + "<option value=\"3\" " + SelectCompare(dep.DepositCat.ToString(), "3") + ">Paid Before Check in (fix)</option>";
                        
                        result = result + "";
                        result = result + "</select></td>";
                        result = result + "<td><input type=\"text\" id=\"txt_amount_" + dep.DepositId + "\" name=\"txt_amount_" + dep.DepositId + "\" value=\"" + dep.Deposit + "\" class=\"Extra_textbox_yellow\" /></td>";
                        result = result + "<td><input type=\"button\" class=\"Extra_Button_small_green\" onclick=\"DepositUpdate('" + dep.DepositId + "');return false;\" value=\"Save\" /></td>";
                        result = result + "<td><img src=\"/images_extra/bin.png\" onclick=\"DarkmanPopUpComfirm(400,'Are you sure to delete' ,'delDeposit(" + dep.DepositId + ")');return false;\"  style=\"cursor:pointer;\"  /></td>";

                        result = result + "</tr>";
                    }



                }
                else
                {
                    result = "<div class=\"box_empty\">";

                    result = result + "<p><label>No Deposit</label> Please insert new one</p>";

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