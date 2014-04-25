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
    public partial class admin_ajax_room_amen_room : Hotels2BasePageExtra_Ajax
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
            ProductOptionFacility cFac = new ProductOptionFacility();
            IList<object> iListfac = cFac.getOptionFacilityByOptionId(int.Parse(Request.QueryString["oid"]),1);

            
            StringBuilder result = new StringBuilder();

            

            foreach (ProductOptionFacility fac in iListfac)
            {
                result.Append("<p id=\"amen_" + fac.FacilityId + "\"><input type=\"checkbox\" style=\"display:none;\" checked=\"checked\" value=\"" + fac.FacilityId + "\" name=\"amenresult\" /><input type=\"text\" name=\"txt_amen_" + fac.FacilityId + "\" style=\"display:none;\" class=\"Extra_textbox\" value=\"" + fac.Title + "\" />" + fac.Title + "&nbsp;<img src=\"/images_extra/del.png\" onclick=\"del('" + fac.FacilityId + "');return false;\" />&nbsp;</p>");
                //result.Append("<p id=\"amen_" + fac.FacilityId + "\"><input type=\"checkbox\" style=\"display:none;\" checked=\"checked\" value=\"" + fac.FacilityId + "\" name=\"amenresult\" /><input type=\"text\" name=\"txt_amen_" + fac.FacilityId + "\" style=\"display:none;\" class=\"Extra_textbox\" value=\"" + fac.Title + "\" />" + result + "&nbsp;<img src=\"/images_extra/del.png\" onclick=\"del('" + fac.FacilityId + "');return false;\" />&nbsp;</p>");
            }
          

            return result.ToString();
        }
    }
}