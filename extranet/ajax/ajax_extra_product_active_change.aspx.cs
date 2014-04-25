using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;

namespace Hotels2thailand.UI
{
    public partial class extranet_ajax_ajax_extra_product_active_change : Hotels2BasePageExtra_Ajax
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                
                //int staffId = (this.Page as Hotels2BasePageExtra).CurrentStaffId;
                //int intLogKey = int.Parse(HttpContext.Current.Request.Cookies["SessionKey"]["ProductActive"]);

                int intLogKey = this.CurrentProductActiveExtra;

                Response.Write(this.CurrentProductActiveExtra);
                
                Response.End();
            }
        }

        public string getProductSelect()
        {
            StaffProduct_Extra cStaffProduct = new StaffProduct_Extra();
            StringBuilder result = new StringBuilder();

            result.Append("<form id=\"paymeny_insert_form\" action=\"\" >");

            result.Append("<div class=\"formbox_head\">Insert New Payment</div>");
            result.Append("<div class=\"formbox_body\">");
            result.Append("<table id=\"tbl_payment_list\" cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:left;\">");
            

            foreach (StaffProduct_Extra Product in cStaffProduct.getProductByStaffId(this.CurrentStaffId))
            {
                //if (this.CurrentProductActiveExtra == Product.ProductID)
                //{
               //result.Append(Response.Cookies["SessionKey"]["ProductActive"].ToString());
                    result.Append("<tr style=\"background-color:#ffffff; height:25px;\" >");
                    result.Append("<td><input type=\"radio\" id=\"radio_" + Product.ProductID + "\" checked=\"\" value=\"" + Product.ProductID + "\" name=\"ProductSelect\"  /></td>");
                    result.Append("<td>&nbsp;" + Product.ProductTitle + "</td>");
                    result.Append("</tr>");
                //}
            }
            
            
            
            result.Append("</table>");
            result.Append("</div>");
            result.Append("<div class=\"formbox_buttom\"><input type=\"button\" value=\"Save\" onclick=\"InsertNewPayment();\"  class=\"btStyleGreen\" />&nbsp;<input type=\"button\" value=\"Cancel\" onclick=\"DarkmanPopUp_Close();\" class=\"btStyleWhite\" style=\" width:80px\" /></div>");

            result.Append("</form>");


            return result.ToString();
        }
    }
}