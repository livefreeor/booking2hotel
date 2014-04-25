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
    public partial class admin_ajax_market_content_box : System.Web.UI.Page
    {
        public string qMarketId
        {
            get { return Request.QueryString["mrid"]; }
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
            if (!this.Page.IsPostBack)
            {
                try
                {
                    Result.Text = ProductContentHeadMenu() + ProductContentbody();
                }
                catch(Exception ex)
                {
                    Result.Text = ex.Message;
                }
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
                strResult.Append("<a id=\"Link_Lang_" + langItem.LanguageID + "\" href=\"\" onclick=\"ContentSwitchLangDisplayMain('" + langItem.LanguageID + "','content_lang');return false;\">" + LinkTitle + "</a></td>\r\n");
            }
            strResult.Append("</tr></table>\r\n");
            strResult.Append("</div>\r\n");
                
            return strResult.ToString();
        }

        public string ProductContentbody()
        {
            StringBuilder strResult = new StringBuilder();

            CountryMarketContent cMarketContent = new CountryMarketContent();
            cMarketContent = cMarketContent.getMarketContentbyId(byte.Parse(this.qMarketId), this.Current_StaffLangId);

            strResult.Append("<div class=\"productcontentLangBox_item\">");
            strResult.Append("<div class=\"productcontentLangBox_item_left\">");
            strResult.Append("<table style=\"width:100%\">");
            strResult.Append("<tr>");
            strResult.Append("<td><p>Title</p></td>");
            if (cMarketContent != null)
                strResult.Append("<td><input type=\"text\" name=\"txtTitle\" id=\"txtTitle\" class=\"TextBox_Extra_normal\" style=\"width:800px;\" value=\"" + cMarketContent.Title + "\" /></td>");
            else
                strResult.Append("<td><input type=\"text\" name=\"txtTitle\" id=\"txtTitle\" class=\"TextBox_Extra_normal\" style=\"width:800px;\" value=\"\" /></td>");
            strResult.Append("</tr>");

            strResult.Append("<tr>");
            strResult.Append("<td><p>Detail</p></td>");
            if (cMarketContent != null)
                strResult.Append("<td><textarea id=\"txtdetail\" name=\"txtdetail\" rows=\"10\" style=\"width:800px;\" class=\"TextBox_Extra_normal\">" + cMarketContent.Detail + "</textarea></td>");
            else
                strResult.Append("<td><textarea id=\"txtdetail\" name=\"txtdetail\" rows=\"10\" style=\"width:800px;\" class=\"TextBox_Extra_normal\"></textarea></td>");
            strResult.Append("</tr>");

            strResult.Append("</table>");
            strResult.Append("</div>");
            
            strResult.Append("<div style=\"clear:both;\"></div>");
            strResult.Append("<div  style=\"text-align:center; margin-top:10px;\">");
            strResult.Append("<input type=\"button\" name=\"btAdd\" id=\"btAdd\" style=\"width:200px;\"  value=\"Save Product Content\" onclick=\"SavemarketContent();\" class=\"btStyleGreen\" />");
            strResult.Append("</div>");
            
            strResult.Append("</div>");
            return strResult.ToString();
        }
        
    }
}