using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.ProductOption;
using System.Text;
using Hotels2thailand.Production;
using Hotels2thailand.Booking;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_dash_board_promotion : Hotels2BasePageExtra_Ajax
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {

                Response.Write(PromotionResult());
                Response.End();
                
            }
        }


        public string PromotionResult()
        {
            StringBuilder result = new StringBuilder();
            string Exp = "3";



            try
            {
                PromotionExxtranet cPromotionExtra = new PromotionExxtranet();
                int intProActive = cPromotionExtra.getPromotionListExtranet(this.CurrentProductActiveExtra, this.CurrentSupplierId, true).Count;
                int intProInactive = cPromotionExtra.getPromotionListExtranet(this.CurrentProductActiveExtra, this.CurrentSupplierId, false).Count;
                int intProExpried = cPromotionExtra.getPromotionListExtranetExprired(this.CurrentProductActiveExtra, this.CurrentSupplierId, true, 3).Count;
                 string queryString = "";
                if (!string.IsNullOrEmpty(Request.QueryString["pid"]) && !string.IsNullOrEmpty(Request.QueryString["supid"]))
                    queryString = "&pid=" + Request.QueryString["pid"] + "&supid=" + Request.QueryString["supid"];

                result.Append("<div style=\"border-bottom:1px solid #e2e2e2\" class=\"block\">");
                result.Append("<p class=\"topic\"><img src=\"http://www.hotels2thailand.com/images_extra/dot_yellow.png\" />&nbsp;<a href=\"promotion/promotion.aspx?status=1" + queryString + "\" target=\"_Blank\">Promotion Active</a></p>");
                result.Append("<p class=\"booking_sum\"><span class=\"result\">" + intProActive + "</span>&nbsp;Promotions</p>");
                result.Append("</div>");

                result.Append("<div style=\"border-bottom:1px solid #e2e2e2\" class=\"block\">");
                result.Append("<p class=\"topic\"><img src=\"http://www.hotels2thailand.com/images_extra/dot_yellow.png\" />&nbsp;<a href=\"promotion/promotion.aspx?status=0" + queryString + "\" target=\"_Blank\">Promotion Inactive</a></p>");
                result.Append("<p class=\"booking_sum\"><span class=\"result\">" + intProInactive + "</span>&nbsp;Promotions</p>");
                result.Append("</div>");

                result.Append("<div class=\"block\">");
                result.Append("<p class=\"topic\"><img src=\"http://www.hotels2thailand.com/images_extra/dot_yellow.png\" />&nbsp;<a href=\"promotion/promotion.aspx?exp=" + Exp + queryString + "\" target=\"_Blank\">Promotion Expired in 3 Month</a></p>");
                result.Append("<p class=\"booking_sum\"><span class=\"result\">" + intProExpried + "</span>&nbsp;Promotions</p>");
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
}