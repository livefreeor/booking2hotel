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
    public partial class admin_ajax_room_amen_add_manual : Hotels2BasePageExtra_Ajax
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
            ProductFacilitytempalte cFactemp = new ProductFacilitytempalte();
            IList<object> iListFac = cFactemp.getFacilityByCatId(2);

            result.Append("<div class=\"formbox_head\">Amenities Insert Block</div>");
            result.Append("<div class=\"formbox_body\" id=\"formbox_body\">");
            result.Append("<table>");
            result.Append("<tr>");
            result.Append("<td><input type=\"text\" id=\"txt_amen_manual\" class=\"Extra_textbox\" style=\"width:250px;\" /></td>");
            result.Append("</tr>");
            result.Append("</table>");
            result.Append("</div>");
            result.Append("<div class=\"formbox_buttom\" id=\"formbox_buttom\"><input type=\"button\" value=\"Save\" onclick=\"GetFacSelectAddList_frommanual();\"   />&nbsp;<input type=\"button\" value=\"Cancel\" onclick=\"DarkmanPopUp_Close();\"  style=\" width:80px\" /></div>");
            return result.ToString();
        }
    }
}