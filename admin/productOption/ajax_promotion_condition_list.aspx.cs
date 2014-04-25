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
    public partial class admin_ajax_promotion_condition_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write("TEST");
            if (!string.IsNullOrEmpty(Request.QueryString["proId"]))
            {
                int intPromotionId = int.Parse(Request.QueryString["proId"]);
                
                PromotionCondition cPromotion = new PromotionCondition();
                ////cPromotion.getOptionByActivePromotion(intPromotionId);
                GVOptionList.DataSource = cPromotion.getOptionByActivePromotion(intPromotionId);
                GVOptionList.DataBind();
                //Response.Write(cPromotion.getOptionByActivePromotion(intPromotionId).Count);
            }
            else
            {
                //Request.QueryString["proId"].ToString();
                Response.Write("TESTssss");
            }
            
        }

        public void GVOptionList_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int intPromotionId = int.Parse(Request.QueryString["proId"]);
                int intOptionId = (int)DataBinder.Eval(e.Row.DataItem,"Key");
                GridView GvCondition = e.Row.Cells[0].FindControl("GVConditionList") as GridView;
                PromotionCondition cPromotion = new PromotionCondition();

                GvCondition.DataSource = cPromotion.getPromotionCondition(intPromotionId, intOptionId);
                GvCondition.DataBind();
            }

        }

        public void GVConditionList_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblTitle = e.Row.Cells[0].FindControl("lblConditionTitle") as Label;
                lblTitle.Text = DataBinder.Eval(e.Row.DataItem, "Value").ToString();
            }
        }
    }
}