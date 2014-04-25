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
    public partial class admin_ajax_room_amen_template_list : Hotels2BasePageExtra_Ajax
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

            result.Append("<div id=\"amen_template_block\" class=\"formbox_head\">Amenities Template</div>");
            result.Append("<div class=\"formbox_body\" id=\"formbox_body\">");
            result.Append("<table>");
            result.Append("<tr>");
            int count = 0;
            foreach (ProductFacilitytempalte fac in iListFac)
            {
                count = count + 1;
                result.Append("<td><input type=\"checkbox\" value=\"" + fac.TitleEn + "\" name=\"chek_fac_temp_" + fac.Fac_id + "\" id=\"chek_fac_temp_" + fac.Fac_id + "\" /></td><td style=\"font-size:11px;color:#84785a\">" + fac.TitleEn + "</td>");

                if (count > 4)
                {
                    result.Append("</tr><tr>");
                    count = 0;
                }

                
            }
            result.Append("</tr>");
            result.Append("</table>");
            result.Append("</div>");
            result.Append("<div class=\"formbox_buttom\" id=\"formbox_buttom\"><input type=\"button\" value=\"Save\" onclick=\"GetFacSelectAddList_fromtemp();\"   />&nbsp;<input type=\"button\" value=\"Cancel\" onclick=\"DarkmanPopUp_Close();\"  style=\" width:80px\" /></div>");
            return result.ToString();
        }
    }
}