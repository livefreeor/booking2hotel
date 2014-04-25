using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_product_facility_template : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

            Response.Write(FacilityList());
            Response.End();
        }

        public string FacilityList()
        {
            string result = string.Empty;

            ProductFacilitytempalte cFacTemplate = new ProductFacilitytempalte();
            byte Cat_Id = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$dropFacCat"]);
            IList<object> iListFac = cFacTemplate.getFacilityByCatId(Cat_Id);
            result = result + "<table>";
            foreach (ProductFacilitytempalte FacItem in iListFac)
            {
                result = result + "<tr>";
                result = result + "<td><input type=\"checkbox\" value=\"" +FacItem.Fac_id+ "\" name=\"chk_fac_toUpdate\" checked=\"checked\" style=\"display:none;\" /></td>";
                result = result + "<td>ENG: &nbsp;<input type=\"text\" value=\"" + System.Web.HttpUtility.HtmlDecode(System.Web.HttpUtility.HtmlEncode(FacItem.TitleEn)) + "\" style=\"width:350px\" class=\"TextBox_Extra_normal\" name=\"facUpdate_EN_" + FacItem.Fac_id + "\" /></td>";
                result = result + "<td>&nbsp;&nbsp;THAI: &nbsp;<input type=\"text\" value=\"" + System.Web.HttpUtility.HtmlDecode(System.Web.HttpUtility.HtmlEncode(FacItem.TitleTh)) + "\" style=\"width:350px\" class=\"TextBox_Extra_normal\" name=\"facUpdate_TH_" + FacItem.Fac_id + "\" /></td>";
                result = result + "</tr>";
            }
            result = result + "</table>";

            result = result + "<br/></br/><input type=\"button\" value=\"Update\" onclick=\"FacUpdate();return false;\" />";
            return result;
        }
       
        
    }
}