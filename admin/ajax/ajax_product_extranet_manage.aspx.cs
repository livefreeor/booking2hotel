using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.Suppliers;
using Hotels2thailand;

public partial class ajax_product_extranet_manage : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Page.IsPostBack)
        {
            Response.Write(GetproductExtranetManage());
            Response.End();

            
        }
    }


    public string GetproductExtranetManage()
    {
        StringBuilder result = new StringBuilder();

        try
        {
            int intProductId = int.Parse(Request.Form["product_active"]);
            short shrSupplierId = short.Parse(Request.Form["supplier_active"]);


            ProductCommission cProductCom = new ProductCommission();

            Product cProduct = new Product();
            cProduct = cProduct.GetProductById(intProductId);
            string Checked = string.Empty;

            
            result.Append("<p class=\"extra_manage_hotel_name\">" + cProduct.ProductCode+ " : "+ cProduct.Title + "</p>");
            result.Append("<div class=\"active_block\">");
            result.Append("<p class=\"extra_manage_head\">Extranet Active<p>");
       
            if (cProduct.ExtranetActive)
            {
                result.Append("<input type=\"radio\" name=\"product_extra_actives\" value=\"1\" checked=\"checked\" /> Active");
                result.Append("<input type=\"radio\" name=\"product_extra_actives\" value=\"0\" /> Inactive");
            }
            else
            {
                result.Append("<input type=\"radio\" name=\"product_extra_actives\" value=\"1\" /> Active");
                result.Append("<input type=\"radio\" name=\"product_extra_actives\" value=\"0\" checked=\"checked\"  /> Inactive");
            }
            result.Append("<p><input type=\"button\" value=\"update\" onclick=\"SaveACtive();return false;\"  style=\"font-size:11px;\" /></p>");
            result.Append("</div>");
            IList<object> ProductCommission = cProductCom.GetCommissionBySuppierIdAndProductID(shrSupplierId, intProductId);
            result.Append("<div id=\"commission_block\" class=\"commission_block\">");
            result.Append("<p class=\"extra_manage_head\">Commission Manage<p>");

            result.Append("<div  class=\"extra_manage_commission_head\" >");
            result.Append("<table cellpadding=\"0\" cellspacing=\"0\" style=\"width:100%\">");
            result.Append("<tr>");
            result.Append("<td>Date Start</td>");
            result.Append("<td>Date End</td>");
            result.Append("<td>Commission</td>");
            result.Append("</tr>");
            result.Append("</table>");
            result.Append("</div>");
            if (ProductCommission.Count > 0)
            {
                foreach (ProductCommission com in ProductCommission)
                {
                    result.Append("<div id =\"extra_manage_commission_item_" + com.Commission_id + "\" class=\"extra_manage_commission_item\" >");
                    result.Append("<input tpye=\"checkbox\" checked=\"checked\" style=\"display:none;\" value=\"" + com.Commission_id + "\" name=\"check_update\" />");
                    result.Append("<table cellpadding=\"0\" cellspacing=\"0\" style=\"width:90%\"> ");
                    result.Append("<tr>");
                    result.Append("<td><input id=\"date_start_" + com.Commission_id + "\" name=\"date_start_" + com.Commission_id + "\" style=\"width:100px;\" type=\"text\" value=\"" + com.DateStart.ToString("yyyy-MM-dd") + "\" class=\"TextBox_Extra_normal_small\" /></td>");
                    result.Append("<td><input id=\"date_end_" + com.Commission_id + "\" name=\"date_end_" + com.Commission_id + "\" style=\"width:100px;\" type=\"text\" value=\"" + com.DateEnd.ToString("yyyy-MM-dd") + "\" class=\"TextBox_Extra_normal_small\" /></td>");
                    result.Append("<td><input id=\"com_" + com.Commission_id + "\" name=\"com_" + com.Commission_id + "\" style=\"width:30px; text-align:center\" type=\"text\" value=\"" + com.Commission + "\" class=\"TextBox_Extra_normal_small\" /></td>");
                    result.Append("<td><input type=\"button\" value=\"update\" onclick=\"UpdateCom('" + com.Commission_id + "');return false;\"  style=\"font-size:11px;\" /></td>");
                    result.Append("</tr>");
                    result.Append("</table>");
                    result.Append("</div>");
                }
            }
            else
            {
                result.Append("<p>There is no commission for this hotel <a>please insert one below</a></p>");
                result.Append("");
                result.Append("");
                result.Append("");
            }

            result.Append("<div class=\"commission_insert_block\">");
            result.Append("<p>Commission Insert Box</p>");
            result.Append("<table cellpadding=\"0\" cellspacing=\"0\" style=\"width:90%\"> ");
            result.Append("<tr>");
            result.Append("<td><input id=\"date_start_\" name=\"date_start_\" style=\"width:100px;\" type=\"text\"  class=\"TextBox_Extra_normal_small\" /></td>");
            result.Append("<td><input id=\"date_end_\" name=\"date_end_\" style=\"width:100px;\" type=\"text\"  class=\"TextBox_Extra_normal_small\" /></td>");
            result.Append("<td><input id=\"com_\" name=\"com_\" style=\"width:30px; text-align:center\" type=\"text\"  class=\"TextBox_Extra_normal_small\" /></td>");
            result.Append("</tr>");
            result.Append("</table>");
            result.Append("<p><input type=\"button\" value=\"Add New Commission\" onclick=\"AddNewCom();return false;\"  style=\"font-size:11px;\" /></p>");
            result.Append("</div>");
           
            
            result.Append("</div>");

            
            
           
        }
        catch (Exception ex)
        {
            Response.Write("error:" + ex.Message);
            Response.End();
        }

        return result.ToString();
    }
    
    
    
}