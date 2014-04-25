using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.ProductOption;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_product_list_expired_check : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(Request.QueryString["pid"]))
            {
                Response.Write("TESTssaaa");
            }
            //if (!string.IsNullOrEmpty(Request.QueryString["pid"]))
            //{
            //    int intPromotionId = int.Parse(Request.QueryString["pid"]);
            //    Response.Write("TESasdasdasdadasdasdasdasTssss");
                
            //    //int intPromotionId = int.Parse(Request.QueryString["proId"]);
            //    //int intPromotionId = int.Parse(Request.QueryString["proId"]);
            //    //DateTime dDateExpired = Request.QueryString["proId"].Hotels2DateSplit();
            //    //PromotionCondition cPromotion = new PromotionCondition();

            //    //bool IsExpire = false;
            //    //Nullable<DateTime> dDateExpired = (Nullable<DateTime>)DataBinder.Eval(e.Row.DataItem, "DateExpired");
            //    //if (dDateExpired != null)
            //    //{
            //    //    int Datediff = (int)DateTime.Now.Hotels2DateDiff(DateInterval.Month, (DateTime)dDateExpired);


            //    //    int intRangeExpired = int.Parse(dropExpired.SelectedValue);
            //    //    if (intRangeExpired == 0)
            //    //    {
            //    //        if (Datediff <= 3)
            //    //        {
            //    //            IsExpire = true;
            //    //        }

            //    //    }
            //    //    else
            //    //    {
            //    //        if (Datediff <= intRangeExpired)
            //    //        {
            //    //            IsExpire = true;
            //    //        }
            //    //    }

            //    //    if (IsExpire)
            //    //    {
            //    //        lblSupplierTitle.Text = "<p style=\"margin:0px;padding:0px;color:#dd3822;font-weight:bold;\"><img src=\"../../images/expired.png\">Expired</p>";
            //    //    }
            //    //    else
            //    //    {
            //    //        lblSupplierTitle.CssClass = "expiredStyle";
            //    //    }
            //    //}
            //    //else
            //    //{
            //    //    lblSupplierTitle.Text = "<p style=\"margin:0px;padding:0px;color:#dd3822;font-weight:bold;\"><img src=\"../../images/expired.png\">Expired</p>";
            //    //}
            //    ////cPromotion.getOptionByActivePromotion(intPromotionId);
                
            //    //Response.Write(cPromotion.getOptionByActivePromotion(intPromotionId).Count);
            //}
            //else
            //{
            //    //Request.QueryString["proId"].ToString();
            //    Response.Write("TESTssss");
            //}
            
        }

        
    }
}