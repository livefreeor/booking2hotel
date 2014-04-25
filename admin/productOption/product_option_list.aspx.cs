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
    public partial class admin_productOption_product_option_list : Hotels2BasePage
    {
        

         protected void Page_Load(object sender, EventArgs e)
         {
             if (!this.Page.IsPostBack)
             {
                 Product cProduct = new Product();
                 cProduct = cProduct.GetProductById(int.Parse(this.qProductId));
                 Destitle.Text = cProduct.DestinationTitle;
                 txthead.Text = cProduct.Title;
                 lnkOptionCreate.NavigateUrl = "option_add.aspx?pdcid=" + this.qProductCat + "&pid=" + this.qProductId;


                 dropProductSupDataBind();
                 dropProductSup.SelectedValue = cProduct.SupplierPrice.ToString();
                 GVOptionCatDataBindCurrentSupplier(cProduct.SupplierPrice);



                 GVSupplierListDuplicateDataBind();
                 GVProductPeriodDataBind();
                
             }
             
         }
         
         public void GVSupplierListDuplicateDataBind()
         {
             if (!string.IsNullOrEmpty(dropProductSup.SelectedValue))
             {
                 short SupplierId =  short.Parse(dropProductSup.SelectedValue);
                 int intProductId = int.Parse(this.qProductId);
                 Option cOption = new Option();
                 GvSupplierListDup.DataSource = cOption.getSupplierListNotCurrent(intProductId, SupplierId);
                 GvSupplierListDup.DataBind();
             }
         }

         public void GvSupplierListDup_OnRowDataBound(object sender, GridViewRowEventArgs e)
         {
             if (e.Row.RowType == DataControlRowType.DataRow)
             {
                 GridView GVoptionListDup = e.Row.Cells[0].FindControl("GVoptionListDup") as GridView;
                 //sup Condition
                 short shrSup = (short)DataBinder.Eval(e.Row.DataItem, "Key");
                 // sup Current
                 short SupplierId = short.Parse(dropProductSup.SelectedValue);
                 int intProductId = int.Parse(this.qProductId);
                 Option cOption = new Option();
                 GVoptionListDup.DataSource = cOption.getOPtionListNotCurrent(intProductId, shrSup, SupplierId);
                 GVoptionListDup.DataBind();
             }
         }

         public void btnDupli_OnClick(object sender, EventArgs e)
         {
             short SupplierId = short.Parse(dropProductSup.SelectedValue);
             foreach (GridViewRow GvSupParent in GvSupplierListDup.Rows)
             {
                 GridView GvChild = GvSupplierListDup.Rows[GvSupParent.RowIndex].Cells[0].FindControl("GVoptionListDup") as GridView;
                 foreach (GridViewRow GvChildRow in GvChild.Rows)
                 {
                     CheckBox chkBox = GvChild.Rows[GvChildRow.RowIndex].Cells[0].FindControl("chkOptionDUp") as CheckBox;
                     int intOption = (int)GvChild.DataKeys[GvChildRow.RowIndex].Value;
                     if (chkBox.Checked)
                     {
                         Option.insertOptionMappingSupplier(intOption, SupplierId);
                     }

                 }
             }

             Response.Redirect(Request.Url.ToString());
         }

         public void GVoptionListDup_OnRowDataBound(object sender, GridViewRowEventArgs e)
         {
             if (e.Row.RowType == DataControlRowType.DataRow)
             {

             }
         }

         public void GVOptionCatDataBindCurrentSupplier(short shrSupId)
         {
             if (!string.IsNullOrEmpty(dropProductSup.SelectedValue))
             {
                 //Response.Write(shrSupId);
                 //Response.End();
                 //short shrSupId = short.Parse(dropProductSup.SelectedValue);
                 ProductOptionCategory cOptionCategory = new ProductOptionCategory();
                 GvOptionCat.DataSource = cOptionCategory.GetProductCategoryAllExpGala(int.Parse(this.qProductId), shrSupId);
                 GvOptionCat.DataBind();
             }
         }

        
         public void GVOptionCatDataBind()
         {
             if (!string.IsNullOrEmpty(dropProductSup.SelectedValue))
             {


                 short shrSupId = short.Parse(dropProductSup.SelectedValue);
                 
                 ProductOptionCategory cOptionCategory = new ProductOptionCategory();
                 GvOptionCat.DataSource = cOptionCategory.GetProductCategoryAllExpGala(int.Parse(this.qProductId), shrSupId);
                 GvOptionCat.DataBind();
             }
         }

         public void dropProductSupDataBind()
         {
             ProductSupplier cProductSup = new ProductSupplier();
             dropProductSup.DataSource = cProductSup.getSupplierListByProductIdAndActiveOnly(int.Parse(this.qProductId)); 
             dropProductSup.DataTextField = "SupplierTitle";
             dropProductSup.DataValueField = "SupplierID";
             dropProductSup.DataBind();
         }

         protected void dropProductSup_OnSelectedIndexChanged(object sender, EventArgs e)
         {
             GVProductPeriodDataBind();
             GVOptionCatDataBind();
             
         }
         protected void GvOptionCat_OnRowDataBound(object sender, GridViewRowEventArgs e)
         {
             if (e.Row.RowType == DataControlRowType.DataRow)
             {
                GridView GvChildOption = e.Row.Cells[0].FindControl("gvChildOption") as GridView;
                 Option cOption = new Option();
                 short shrCatId = Convert.ToInt16(GvOptionCat.DataKeys[e.Row.DataItemIndex].Value.ToString());

                 short shrSupId = short.Parse(dropProductSup.SelectedValue);
                 int intProductId = int.Parse(this.qProductId);

                 //Response.Write(shrSupId);
                 //Response.End();
                 GvChildOption.DataSource = cOption.GetProductOptionByCurrentSupplierANDProductIdANDCATID(shrSupId, int.Parse(this.qProductId), shrCatId);
                 GvChildOption.DataBind();
             }

             
         }
         protected void gvChildOption_OnRowDataBound(object sender, GridViewRowEventArgs e)
         {
             if (e.Row.RowType == DataControlRowType.DataRow)
             {
                 Dictionary<string, string> dicPriority = new Dictionary<string, string>();
                 for (int count = 1; count < 40; count++)
                 {
                     dicPriority.Add(count.ToString(), count.ToString());
                 }

                 byte bytPriority = (byte)DataBinder.Eval(e.Row.DataItem, "Priority");
                 DropDownList dropPriority = e.Row.Cells[0].FindControl("drpPriority") as DropDownList;
                 dropPriority.DataSource = dicPriority;
                 dropPriority.DataTextField = "Key";
                 dropPriority.DataValueField = "Value";
                 dropPriority.DataBind();
                 dropPriority.SelectedValue = bytPriority.ToString();

                 Image imgBtBullet = e.Row.Cells[0].FindControl("imgBtGreen") as Image;
                 bool bolStatus = (bool)DataBinder.Eval(e.Row.DataItem, "Status");
                 HyperLink Ltitle = e.Row.Cells[0].FindControl("lOption") as HyperLink;
                 if (!bolStatus)
                 {
                     Ltitle.CssClass = "linkDisable";
                     imgBtBullet.ImageUrl = "~/images/greenbt-gray.png";
                     dropPriority.Enabled = false;
                     
                 }
             }
         }

         protected void drpPriority_OnSelectIndexChanged(object sender, EventArgs e)
         {
             foreach (GridViewRow grdParent in GvOptionCat.Rows)
             {
                 GridView grichild = GvOptionCat.Rows[grdParent.RowIndex].Cells[0].FindControl("gvChildOption") as GridView;
                 foreach (GridViewRow grdRow in grichild.Rows)
                 {
                     DropDownList drdList = (grichild.Rows[grdRow.RowIndex].Cells[0].FindControl("drpPriority")) as DropDownList;
                     int intOptionId = (int)grichild.DataKeys[grdRow.RowIndex].Value;
                     Option cOption = new Option();
                     cOption.getOptionById(intOptionId);
                     cOption.Priority = byte.Parse(drdList.SelectedValue);
                     cOption.Update();
                 }
             }

             short shrSupId = short.Parse(dropProductSup.SelectedValue);
             
             ProductOptionCategory cOptionCategory = new ProductOptionCategory();
             GvOptionCat.DataSource = cOptionCategory.GetProductCategoryAllExpGala(int.Parse(this.qProductId), shrSupId);
             GvOptionCat.DataBind();
         }
        //--------------------------------------------------- Right 
         public void GVProductPeriodDataBind()
         {
             
             //Response.End();
             ProductOption.ProductOptionPeriod cProductPeriod = new ProductOptionPeriod();

             
             GVProductPeriod.DataSource = cProductPeriod.getProductPeriodListEffective(int.Parse(this.qProductId), 
                 short.Parse(dropProductSup.SelectedValue));
             GVProductPeriod.DataBind();
         }
         public void GVProductPeriod_OnRowDataBound(object sender, GridViewRowEventArgs e)
         {
             if (e.Row.RowType == DataControlRowType.DataRow)
             {
                 DateTime dDateStart = (DateTime)DataBinder.Eval(e.Row.DataItem, "DateStart");
                 DateTime dDateEnd = (DateTime)DataBinder.Eval(e.Row.DataItem, "DateEnd");

                 Hotels2thailand.UI.Controls.Control_DatepickerCalendar controlDatePickers = e.Row.Cells[1].FindControl("DateTimePicker") as Hotels2thailand.UI.Controls.Control_DatepickerCalendar;
                 controlDatePickers.DateStart = dDateStart;
                 controlDatePickers.DateEnd = dDateEnd;
                 controlDatePickers.DataBind();
             }
         }

         public void GVProductPeriod_OnRowCommand(object sender, GridViewCommandEventArgs e)
         {
             if (e.CommandName == "periodupdate")
             {
                 //int intPeriodId = int.Parse(e.CommandArgument.ToString());

                 string[] param = e.CommandArgument.ToString().Split(Convert.ToChar("&"));
                 short Id = Convert.ToInt16(param[0]);
                 int index = Convert.ToInt32(param[1]);
                 Hotels2thailand.UI.Controls.Control_DatepickerCalendar controlDatePickers = GVProductPeriod.Rows[index].Cells[1].FindControl("DateTimePicker") as Hotels2thailand.UI.Controls.Control_DatepickerCalendar;
                 ProductOptionPeriod cPeriod = new ProductOptionPeriod();
                 cPeriod = cPeriod.getProductPeriodById(Id);
                 cPeriod.DateStart = controlDatePickers.GetDatetStart;
                 cPeriod.DateEnd = controlDatePickers.GetDatetEnd;
                 cPeriod.Update();
             }

             GVProductPeriodDataBind();
         }
         public void btPeriodSubmit_OnClick(object sender, EventArgs e)
         {
             //DateTime dStart = new DateTime(2010,10,09);
             //Response.Write(DateTimePicker.GetDatetStart);
             //Response.Write(DateTimePicker.GetDatetEnd);
             //Response.End();
             Production.Product cProduct = new Production.Product();
             ProductOptionPeriod cPeriod = new ProductOptionPeriod
             {

                 ProductId = int.Parse((this.Page as Hotels2BasePage).qProductId),
                 SupplierId = short.Parse(dropProductSup.SelectedValue),
                 DateStart = DateTimePicker.GetDatetStart,
                 DateEnd = DateTimePicker.GetDatetEnd
             };
             cPeriod.insertNewPeriod(cPeriod);

             GVProductPeriodDataBind();


         }
    }
}