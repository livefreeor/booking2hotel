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
    public partial class admin_ajax_product_facility_box : System.Web.UI.Page
    {
        public string qProductId
        {
            get { return Request.QueryString["pid"]; }
        }

        public string qProductCat
        {
            get { return Request.QueryString["pdcid"]; }
        }

        public byte Current_StaffLangId
        {
            get 
            { 
                Hotels2BasePage cBasePage = new Hotels2BasePage();
                return cBasePage.CurrenStafftLangId;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qProductCat))
            {
                Result.Text = ProductContentHeadMenu() + ProductContentbody();
                
            }
            
        }

        public string ProductContentHeadMenu()
        {
            Language cLang = new Language();
            List<object> ListLang =  cLang.GetLanguageAll();
            StringBuilder strResult = new StringBuilder();

            
            strResult.Append("<div class=\"productmenuBox\">\r\n");
            strResult.Append("<table id=\"productcontentLang_DlLang\" cellspacing=\"0\" style=\"border-collapse:collapse;\"><tr>\r\n");

            foreach (Language langItem in cLang.GetLanguageAll())
            {
                string LinkTitle = string.Empty;
                if (langItem.LanguageID == 1)
                    LinkTitle = "<img src=\"../../images/flagENG.png\" />&nbsp;" + langItem.Title;

                else if (langItem.LanguageID == 2)
                    LinkTitle = "<img src=\"../../images/flagTH.png\" />&nbsp;" + langItem.Title;

                if (this.Current_StaffLangId == langItem.LanguageID)
                    strResult.Append("<td class=\"itemactiveLang\" style=\"border-width:0px;border-style:None;\">\r\n");
                else
                    strResult.Append("<td class=\"iteminactiveLang\" style=\"border-width:0px;border-style:None;\">\r\n");
                strResult.Append("<a id=\"Link_Lang_" + langItem.LanguageID + "\" href=\"\" onclick=\"ContentSwitchLangDisplay('" + langItem.LanguageID + "','Product_facility_box','product_content');return false;\">" + LinkTitle + "</a></td>\r\n");
            }
            strResult.Append("</tr></table>\r\n");
            strResult.Append("</div>\r\n");
                
            return strResult.ToString();
        }

        public string ProductContentbody()
        {
            StringBuilder strResult = new StringBuilder();

            ProductFacility cProductFacility = new ProductFacility();
            

            strResult.Append("<div class=\"productcontentLangBox_item\">");
            strResult.Append("<div class=\"productcontentLangBox_item_left\" style=\" width:100%; text-align:center\">");

            strResult.Append("<div id=\"facility_textinput\" style=\"text-align:left;\">");
            //strResult.Append("<input type=\"text\" style=\"width:300px;\" class=\"TextBox_Extra_normal\">&nbsp;");
            //strResult.Append("<input type=\"button\" value=\"Add New Facility\" class=\"btStyleGreen\" style=\"width:150px;\" onclick=\"SaveproductFacility_single();\" ><a href=\"\" onclick=\"InsertTemplate();return false;\">Insert From Template</a>");
            strResult.Append("<a href=\"\" onclick=\"InsertTemplate();return false;\">Insert From Template</a>");
            strResult.Append("</div> ");

            strResult.Append("</div>");
            strResult.Append("<div style=\"clear:both;\"></div> ");


            strResult.Append("<div id=\"CurrentFac\" style=\" text-align:left; margin-top:20px;\">");

            
            int count = 1;
            strResult.Append("<table id=\"dlCurrentFac\" class=\"dlCurrentFac_style\">");
                strResult.Append("<tr>");
            foreach(ProductFacility item in cProductFacility.getFacilityByProductId(int.Parse(this.qProductId), this.Current_StaffLangId))
            {
                strResult.Append("<td class=\"dlCurrentFac_Item_Style\">");

                strResult.Append("<div id=\"itemlist_" + item.Fac_id + "\" style=\" display:block\" class=\"fac_itemlist\">");
                strResult.Append("<img src=\"../../images/greenbt.png\" rel=\"FacList\">&nbsp;");
                strResult.Append("<a href=\"\" onclick=\"ShowEditMode('itemedit_" + item.Fac_id + "','itemlist_" + item.Fac_id + "');return false;\">" + item.Title + "</a>");
                strResult.Append("</div>");
                strResult.Append("<div id=\"itemedit_" + item.Fac_id + "\" style=\"display:none;position:absolute;\" class=\"fac_itemlist_edit\">");
                strResult.Append("<div class=\"fac_itemlist_edit_body\">");
                //strResult.Append("<input type=\"text\" id=\"txttitle_" + item.Fac_id + "\"  name=\"txttitle_" + item.Fac_id + "\" value=\"" + item.Title + "\" style=\"width:300px;\" class=\"TextBox_Extra\" />&nbsp;");
                //strResult.Append("<input type=\"button\" value=\"Save\"  class=\"btStyleGreen_small\" onclick=\"UpdateFacility('" + item.Fac_id + "','itemlist_" + item.Fac_id + "');\" /><br/>");
                strResult.Append("<a href=\"\" onclick=\"ShowDisplayMode('itemedit_" + item.Fac_id + "','itemlist_" + item.Fac_id + "');return false;\">Cancel</a>&nbsp;:&nbsp;");
                strResult.Append("<a href=\"\" onclick=\"DelProductFac('"+ item.Fac_id+"');return false;\">Remove</a>");
                strResult.Append("</div>");
                strResult.Append("<div class=\"fac_itemlist_edit_foot\">");
                strResult.Append("<img src=\"../../images/tooltip_pointer.png\">");
                strResult.Append("</div>");
                strResult.Append("</div>");

                strResult.Append("</td>");

                if ((count % 4) == 0)
                {
                strResult.Append("</tr><tr>");
                }
                count = count + 1;
            }
            strResult.Append("</tr>");
            strResult.Append("</table>");

            strResult.Append("</div>");

            strResult.Append("</div>");


            
        
         
     
            return strResult.ToString();
        }
        
    }
}