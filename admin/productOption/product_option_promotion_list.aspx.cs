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
    public partial class admin_product_option_promotion_list : Hotels2BasePage
    {
        

         protected void Page_Load(object sender, EventArgs e)
         {
             if (!this.Page.IsPostBack)
             {
                 hdStatus.Value = "True";
                 Product cProduct = new Product();
                 cProduct = cProduct.GetProductById(int.Parse(this.qProductId));
                 txthead.Text = cProduct.ProductCode + "&nbsp;::&nbsp;" + cProduct.Title;
                 dropSupplier.SelectedValue = cProduct.SupplierPrice.ToString();
                 lnkCreate.NavigateUrl = "product_option_promotion.aspx?PromotionFirstStep=1" + this.AppendCurrentQueryString();

                 dropSupplierDataBind();
                 GVPromotionListDataBind();
             }
             
         }

         public void dropSupplierDataBind()
         {
             ProductSupplier cProductSUp = new ProductSupplier();
             dropSupplier.DataSource = cProductSUp.getSupplierListByProductIdAndActiveOnly(int.Parse(this.qProductId));
             dropSupplier.DataTextField = "SupplierTitle";
             dropSupplier.DataValueField = "SupplierID";
             dropSupplier.DataBind();
         }

         public void dropSupplier_OnSelectedIndexChanged(object sender, EventArgs e)
         {
             GVPromotionListDataBind();
         }

         public void GVPromotionListDataBind()
         {
             if (!string.IsNullOrEmpty(this.qProductId))
             {
                 int intProductId = int.Parse(this.qProductId);
                 short shrSupplierId = short.Parse(dropSupplier.SelectedValue);
                 bool Status = bool.Parse(hdStatus.Value);
                 Promotion cPromotion = new Promotion();
                 GVPromotionList.DataSource = cPromotion.GetPromotionListByProductIdAndSupplierId(intProductId, shrSupplierId, Status);
                 GVPromotionList.DataBind();
             }
             
         }

         public void lnActive_OnClick(object sender, EventArgs e)
         {
             hdStatus.Value = "True";
             GVPromotionListDataBind();
         }

         public void lnInactive_OnClick(object sender, EventArgs e)
         {
             hdStatus.Value = "False";
             GVPromotionListDataBind();
         }
         public void GVPromotionList_OnRowDataBound(object sender, GridViewRowEventArgs e)
         {
             if (e.Row.RowType == DataControlRowType.DataRow)
             {
                 bool bolStatus = (bool)DataBinder.Eval(e.Row.DataItem, "Status");
                 DateTime dDateStart = (DateTime)DataBinder.Eval(e.Row.DataItem, "DateStart");
                 DateTime dDateEnd = (DateTime)DataBinder.Eval(e.Row.DataItem, "DateEnd");

                 DateTime dDateStartUse = (DateTime)DataBinder.Eval(e.Row.DataItem, "DateuseStart");
                 DateTime dDateEndUse = (DateTime)DataBinder.Eval(e.Row.DataItem, "DateuseEnd");

                 int intProId = (int)DataBinder.Eval(e.Row.DataItem, "PromotionId");

                 HyperLink hlTitle = e.Row.Cells[1].FindControl("hlTitle") as HyperLink;
                 HyperLink hlOptionUse = e.Row.Cells[4].FindControl("hlOptionUse") as HyperLink;

                  //radioIsWeekendYes.Attributes.Add("onclick", "javascript:DisableDiv('DivdayofWeek');");

                 hlTitle.Attributes.Add("onmouseover", "javascript:getOptionActive('" + intProId + "',event);");
                 //hlTitle.Attributes.Add("onmouseout", "javascript:getOptionActive('" + intProId + "');");
                 hlTitle.Attributes.Add("onmouseout", "javascript:tooltip_remove();");
                 //hlTitle.Attributes.Add("onmouseout", "javascript:exit();");

                 Label lblDateActive = e.Row.Cells[3].FindControl("lblDateActive") as Label;
                 Label lblDateStay = e.Row.Cells[4].FindControl("lblDateStay") as Label;
                 ImageButton imgStatus = e.Row.Cells[5].FindControl("imgbtnStatus") as ImageButton;

                 lblDateActive.Text = dDateStart.ToString("d-MMMM-yyyy") + "&nbsp;&nbsp;-&nbsp;&nbsp;" + dDateEnd.ToString("d-MMMM-yyyy");
                 lblDateStay.Text = dDateStartUse.ToString("d-MMMM-yyyy") + "&nbsp;&nbsp;-&nbsp;&nbsp;" + dDateEndUse.ToString("d-MMMM-yyyy");
                 if (bolStatus)
                     imgStatus.ImageUrl = "~/images/true.png";
                 else
                     imgStatus.ImageUrl = "~/images/false.png";
             }
         }

         public void imgbtnStatus_StatusUPdate(object sender, EventArgs e)
         {
             ImageButton btn = (ImageButton)sender;
             string[] Argument = btn.CommandArgument.Split(',');

             int intPromotionId = int.Parse(Argument[0]);
             int intRowInDex = int.Parse(Argument[1]);

             GridViewRow GvRow = (GridViewRow)(sender as Control).Parent.Parent;
             int RowIndex = GvRow.RowIndex;
             if (intRowInDex == RowIndex)
             {
                 Promotion cPromotion = new Promotion();
                 cPromotion.UpdateStatus(intPromotionId);
                  
             }
             GVPromotionListDataBind();
             //Response.Redirect(Request.Url.ToString());
         }
         
    }
}