using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.ProductOption;
using Hotels2thailand;
using System.Text;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_room_amen_room_current : Hotels2BasePageExtra_Ajax
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                
                Response.Write(GetAmenTEmplate());
                
                Response.End();
            }
        }

        public string GetAmenTEmplate()
        {
            StringBuilder result = new StringBuilder();
            Option cOption = new Option();
            IList<object> iListOption = cOption.GetProductOptionByCurrentSupplierANDProductIdANDCATID_OpenOnly(this.CurrentSupplierId, this.CurrentProductActiveExtra, 38);

            result.Append("<div class=\"formbox_head\">Amenities Current Room</div>");
            result.Append("<div class=\"formbox_body\" id=\"formbox_body\">");
            result.Append("<table>");
            result.Append("<tr>");
            result.Append("<td>");
            result.Append("<select id=\"select_option_current\" class=\"Extra_Drop\" style=\"width:300px;\" >");

            foreach (Option option in iListOption)
            {
                result.Append("<option value=\""+ option.OptionID +"\">"+option.Title+"</option>");
            }
            result.Append("</select>");
            result.Append("</td>");
            result.Append("</tr>");
            result.Append("</table>");
            result.Append("</div>");
            result.Append("<div class=\"formbox_buttom\" id=\"formbox_buttom\"><input type=\"button\" value=\"Save\" onclick=\"GetFacSelectAddList_fromcurrent();\"   />&nbsp;<input type=\"button\" value=\"Cancel\" onclick=\"DarkmanPopUp_Close();\"  style=\" width:80px\" /></div>");
            return result.ToString();
        }
    }
}