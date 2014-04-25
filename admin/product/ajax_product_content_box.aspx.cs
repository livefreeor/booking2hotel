using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.Staffs;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_product_content_box : System.Web.UI.Page
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
                    strResult.Append("<a id=\"Link_Lang_" + langItem.LanguageID + "\" href=\"\" onclick=\"ContentSwitchLangDisplay('" + langItem.LanguageID + "','product_content_box','product_content');return false;\">" + LinkTitle + "</a></td>\r\n");
            }
            strResult.Append("</tr></table>\r\n");
            strResult.Append("</div>\r\n");
                
            return strResult.ToString();
        }

        public string ProductContentbody()
        {
            StringBuilder strResult = new StringBuilder();

            ProductContent cProductContent = new ProductContent();
            cProductContent = cProductContent.GetProductContentById(int.Parse(this.qProductId),this.Current_StaffLangId);

            strResult.Append("<div class=\"productcontentLangBox_item\">");
            strResult.Append("<div class=\"productcontentLangBox_item_left\">");
            strResult.Append("<table style=\"width:100%\">");
            strResult.Append("<tr>");
            strResult.Append("<td><p>Title</p></td>");
            if (cProductContent != null)
                strResult.Append("<td><input type=\"text\" name=\"txtTitle\" id=\"txtTitle\" class=\"TextBox_Extra_normal\" style=\"width:800px;\" value=\"" + cProductContent.Title + "\" /></td>");
            else
                strResult.Append("<td><input type=\"text\" name=\"txtTitle\" id=\"txtTitle\" class=\"TextBox_Extra_normal\" style=\"width:800px;\" value=\"\" /></td>");
            strResult.Append("</tr>");

            strResult.Append("<tr>");
            strResult.Append("<td><p>Title Secound</p></td>");
            if (cProductContent != null)
                strResult.Append("<td><input type=\"text\" name=\"txtTitleSec\" id=\"txtTitleSec\" class=\"TextBox_Extra_normal\" style=\"width:800px;\" value=\"" + cProductContent.TitleSecound + "\" /></td>");
            else
                strResult.Append("<td><input type=\"text\" name=\"txtTitleSec\" id=\"txtTitleSec\" class=\"TextBox_Extra_normal\" style=\"width:800px;\" value=\"\" /></td>");
            strResult.Append("</tr>");

            strResult.Append("<tr>");
            strResult.Append("<td><p>Short</p></td>");

            if (cProductContent != null)
                strResult.Append("<td><textarea id=\"txtShort\" name=\"txtShort\" rows=\"6\" style=\"width:800px;\" class=\"TextBox_Extra_normal\">" + cProductContent.DetailShort + "</textarea></td>");
            else
                strResult.Append("<td><textarea id=\"txtShort\" name=\"txtShort\" rows=\"6\" style=\"width:800px;\" class=\"TextBox_Extra_normal\"></textarea></td>");
            strResult.Append("</tr>");

            strResult.Append("<tr>");
            strResult.Append("<td><p>Detail</p></td>");
            if (cProductContent != null)
                strResult.Append("<td><textarea id=\"txtdetail\" name=\"txtdetail\" rows=\"10\" style=\"width:800px;\" class=\"TextBox_Extra_normal\">" + cProductContent.Detail + "</textarea></td>");
            else
                strResult.Append("<td><textarea id=\"txtdetail\" name=\"txtdetail\" rows=\"10\" style=\"width:800px;\" class=\"TextBox_Extra_normal\"></textarea></td>");
            strResult.Append("</tr>");

            strResult.Append("<tr>");
            strResult.Append("<td><p>Direction</p></td>");
            if (cProductContent != null)
                strResult.Append("<td><textarea id=\"txtDirection\" name=\"txtDirection\" rows=\"6\" style=\"width:800px;\" class=\"TextBox_Extra_normal\">" + cProductContent.Direction + "</textarea></td>");
            else
                strResult.Append("<td><textarea id=\"txtDirection\" name=\"txtDirection\" rows=\"6\" style=\"width:800px;\" class=\"TextBox_Extra_normal\"></textarea></td>");
            strResult.Append("</tr>");

            strResult.Append("<tr>");
            strResult.Append("<td><p>Internet Description</p></td>");
            if (cProductContent != null)
                strResult.Append("<td><textarea id=\"txtInternet\" name=\"txtInternet\" rows=\"6\" style=\"width:800px;\" class=\"TextBox_Extra_normal\">" + cProductContent.DetailInterNet + "</textarea></td>");
            else
                strResult.Append("<td><textarea id=\"txtInternet\" name=\"txtInternet\" rows=\"6\" style=\"width:800px;\" class=\"TextBox_Extra_normal\"></textarea></td>");
            strResult.Append("</tr>");

            strResult.Append("<tr>");
            strResult.Append("<td><p>Address</p></td>");
            if (cProductContent != null)
                strResult.Append("<td><textarea id=\"txtaddress\" name=\"txtaddress\" rows=\"4\" style=\"width:800px;\" class=\"TextBox_Extra_normal\">" + cProductContent.Address + "</textarea></td>");
            else
                strResult.Append("<td><textarea id=\"txtaddress\" name=\"txtaddress\" rows=\"4\" style=\"width:800px;\" class=\"TextBox_Extra_normal\"></textarea></td>");
            strResult.Append("</tr>");

            strResult.Append("<tr>");
            strResult.Append("<td><p>FileName</p></td>");
            string strAttr = string.Empty;
            //try
            //{
            //    StaffSessionAuthorize cStaffSession = new StaffSessionAuthorize();
                
            //    string SessionStaffCat = cStaffSession.HotelsSessionItem;
            //    if (SessionStaffCat != "1" || SessionStaffCat != "2" || SessionStaffCat != "7" || SessionStaffCat != "5" || SessionStaffCat != "10")
            //        strAttr = "disabled=\"disabled\"";

            //}
            //catch(Exception ex)
            //{
            //    Response.Write(ex.Message + "---");
            //    Response.End();
            //}
            if (cProductContent != null)
                strResult.Append("<td><input type=\"text\" name=\"txtFilename\" id=\"txtFilename\" class=\"TextBox_Extra_normal\" " + strAttr + " style=\"width:800px;\" value=\"" + cProductContent.FileMain + "\" /></td>");
            else
                strResult.Append("<td><input type=\"text\" name=\"txtFilename\" id=\"txtFilename\" class=\"TextBox_Extra_normal\" " + strAttr + " style=\"width:800px;\" value=\"\" /></td>");
            
            strResult.Append("</tr>");

            strResult.Append("</table>");
            strResult.Append("</div>");
            
            strResult.Append("<div style=\"clear:both;\"></div>");
            strResult.Append("<div  style=\"text-align:center; margin-top:10px;\">");
            strResult.Append("<input type=\"button\" name=\"btAdd\" id=\"btAdd\" style=\"width:200px;\"  value=\"Save Product Content\" onclick=\"SaveProductContent();\" class=\"btStyleGreen\" />");
            strResult.Append("</div>");
            
            strResult.Append("</div>");
            return strResult.ToString();
        }
        
    }
}