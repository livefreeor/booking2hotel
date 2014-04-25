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
    public partial class admin_productOption_product_option_gala : Hotels2BasePage
    {
        

         protected void Page_Load(object sender, EventArgs e)
         {
             
             if (!this.Page.IsPostBack)
             {
                 
                 Product cProduct = new Product();
                 cProduct = cProduct.GetProductById(int.Parse(this.qProductId));
                 Destitle.Text = cProduct.DestinationTitle;
                 txthead.Text = cProduct.Title;
                 //lnkOptionCreate.NavigateUrl = "option_add.aspx?pdcid=" + this.qProductCat + "&pid=" + this.qProductId;


                 dropProductSupDataBind();
                 dropProductSup.SelectedValue = cProduct.SupplierPrice.ToString();
                 GVOptionDataBind();
                 

                 galaDetailBinding();
                 GVOptionDataBind();

                 if (string.IsNullOrEmpty(this.qOptionId))
                 {
                     screenBlock.Visible = true;
                 }

                 panelRateAddDataBind();
                 //ProductOptionGala cOptionGala = new ProductOptionGala();

                 //if (cOptionGala.GetProductOptionGalabyOptionId(int.Parse(this.qOptionId)) == null)
                 //{
                 //    panelRateAdd.Visible = false;
                 //}

             }
             else
             {
                 Content_Lang_box.DataBind();
             }
             
             
         }

         public void GVOptionDataBind()
         {
             short shrSupId = short.Parse(dropProductSup.SelectedValue);
             Option cOption = new Option();
             gvChildOption.DataSource = cOption.GetProductOptionByCurrentSupplierANDProductIdANDCATID(short.Parse(dropProductSup.SelectedValue), int.Parse(this.qProductId), 47);
             gvChildOption.DataBind();
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
             
             GVOptionDataBind();
             Content_Lang_box.DataBind();
             screenBlock.Visible = true;

             panelGalaContentLang.Visible = false;
             panelGalaDetail.Visible = false;
             
         }
        
         protected void gvChildOption_OnRowDataBound(object sender, GridViewRowEventArgs e)
         {
             if (e.Row.RowType == DataControlRowType.DataRow)
             {
                 Button btnDis = e.Row.Cells[0].FindControl("tbnDis") as Button;
                 HyperLink lnkTitle = e.Row.Cells[0].FindControl("lOption") as HyperLink;
                 bool bolStatus = (bool)DataBinder.Eval(e.Row.DataItem, "Status");
                 if (bolStatus == false)
                 {
                     lnkTitle.CssClass = "product_announcement_left_Gv_item_dis";
                     btnDis.Text = "Enable";
                 }

             }
         }

         

         public void btnSave_Onclick(object sender, EventArgs e)
         {
              short shrSupId = short.Parse(dropProductSup.SelectedValue);
              ProductOptionGala.insertOptionGala(int.Parse(this.qProductId), txtTitle.Text, shrSupId);
              txtTitle.Text = string.Empty;

              GVOptionDataBind();
         }

         public void galaBtn_Cilck(object sender, EventArgs e)
         {
             Button btn = (Button)sender;
             
             if (btn.CommandName == "ancSave")
             {
                 string[] Argument = btn.CommandArgument.Split(',');
                 int intGalaId = int.Parse(Argument[0]);
                 int RowIndex = int.Parse(Argument[1]);

                 foreach (GridViewRow item in gvChildOption.Rows)
                 {
                     if (item.RowType == DataControlRowType.DataRow)
                     {
                         if (item.RowIndex == RowIndex)
                         {
                             TextBox txttitle = gvChildOption.Rows[item.RowIndex].Cells[0].FindControl("txtTitle") as TextBox;
                             Option cOption = new Option();
                             cOption.GetProductOptionById(intGalaId);
                             cOption.Title = txttitle.Text;
                             cOption.Update();
                             
                         }
                     }
                 }
             }

             if (btn.CommandName == "ancDis")
             {
                 string[] Argument = btn.CommandArgument.Split(',');
                 int intGalaId = int.Parse(Argument[0]);
                 int RowIndex = int.Parse(Argument[1]);

                 Option cOption = new Option();
                 cOption.GetProductOptionById(intGalaId);
                 
                 if (cOption.Status)
                 {
                     cOption.Status = false;
                     cOption.Update();
                 }
                 else
                 {
                     cOption.Status = true;
                     cOption.Update();
                 }
             }

             GVOptionDataBind();
             //Response.Redirect(Request.Url.ToString());
         }

        //--------------------------------------------------- Right 
         public void galaDetailBinding()
         {
             if (!string.IsNullOrEmpty(this.qOptionId))
             {
                 Datepicker.DateStart = DateTime.Now;
                 Datepicker.DateEnd = DateTime.Now.AddDays(1);
                 Datepicker.DataBind();
                 dropMarketDataBind();
                 Option cOption = new Option();
                 lblHeadtitle.Text = cOption.getOptionById(int.Parse(this.qOptionId)).Title;
                 ProductOptionGala cOptionGala = new ProductOptionGala();

                 if (cOptionGala.GetProductOptionGalabyOptionId(int.Parse(this.qOptionId)) != null)
                 {
                     
                     //short shrSupId = short.Parse(dropProductSup.SelectedValue);
                     //ProductOptionGala.InsertOptionGalaOnly(int.Parse(this.qOptionId));

                     cOptionGala = cOptionGala.GetProductOptionGalabyOptionId(int.Parse(this.qOptionId));
                     Datepicker.DateStart = cOptionGala.DateStart;
                     Datepicker.DateEnd = cOptionGala.DateEnd;
                     Datepicker.DataBind();

                     
                     
                     // 0 == ADULT, 1 == CHILD
                     //int intGalaFor = int.Parse(dropGalaFor.SelectedValue);
                     if (cOptionGala.RequireAdult)
                     {
                         dropGalaFor.SelectedValue = "0";
                     }

                     if (cOptionGala.RequireChild)
                     {
                         dropGalaFor.SelectedValue = "1";
                     }


                     // 0 == ALL Day; 1 == Select Only One day
                     if (cOptionGala.DefaultGala == 0)
                     {
                         dropCalculate.SelectedValue = "0";
                     }

                     if (cOptionGala.DefaultGala == 1)
                     {
                         dropCalculate.SelectedValue = "1";
                     }

                     if (cOptionGala.IsCompulsory)
                     {
                         radioyes.Checked = true;
                         radioNo.Checked = false;
                     }
                     else
                     {
                         radioyes.Checked = false;
                         radioNo.Checked = true;
                     }

                     if (Option.getSupplierId(int.Parse(this.qOptionId)) != 0)
                     {
                         dropProductSup.SelectedValue = Option.getSupplierId(int.Parse(this.qOptionId)).ToString();
                     }


                     
                 }
                 
              }
             else
             {
                 
                 panelGalaContentLang.Visible = false;
                 panelGalaDetail.Visible = false;
             }
         }

         public void dropMarketDataBind()
         {
             dropMarket.DataSource = CountryMarket.getMarketALL();
             dropMarket.DataTextField = "Value";
             dropMarket.DataValueField = "Key";
             dropMarket.DataBind();

             ProductOptionCondition cProductOptionCondition = new ProductOptionCondition();
             int count = cProductOptionCondition.getListConditionByOptionId(int.Parse(this.qOptionId)).Count;
             int intConditionId = 0;

             if (count > 0)
             {
                 intConditionId = cProductOptionCondition.getConditionByOptionIdGalaOnly(int.Parse(this.qOptionId));
                 dropMarket.SelectedValue = cProductOptionCondition.getConditionById(intConditionId).MarketId.ToString();
             }
             
         }

         public void btngalasave_OnClick(object sender, EventArgs e)
         {
             
             ProductOptionGala cOptionGala = new ProductOptionGala();
             cOptionGala = cOptionGala.GetProductOptionGalabyOptionId(int.Parse(this.qOptionId));

             byte bytMarketId = byte.Parse(dropMarket.SelectedValue);
             int intOptionId = int.Parse(this.qOptionId);
             Option cOption = new Option();
             string Optiontitle = cOption.getOptionById(intOptionId).Title;
             int intProductId = int.Parse(this.qProductId);
             short shrSupplierId = short.Parse(dropProductSup.SelectedValue);
             DateTime dDateStart = Datepicker.GetDatetStart;
             DateTime dDateEnd = Datepicker.GetDatetEnd;
             int intGalaFor = int.Parse(dropGalaFor.SelectedValue);
             int intGalaCal = int.Parse(dropCalculate.SelectedValue);

             bool galaForAdult = false;
             bool galaForChild = false;

             if (intGalaFor == 0)
             {
                 galaForAdult = true;
                 galaForChild = false;
             }
             if (intGalaFor == 1)
             {
                 galaForAdult = false;
                 galaForChild = true;
             }

             bool Iscom = false;
             if (radioyes.Checked)
             {
                 Iscom = true;
             }
             if (radioNo.Checked)
             {
                 Iscom = false;
             }
             
             
             if (cOptionGala == null)
             {
                 // INSERT OPTION GALA to tbl_product_option GALA
                 ProductOptionGala cInsert = new ProductOptionGala
                 {
                     OptionID = int.Parse(this.qOptionId),
                     DateStart = Datepicker.GetDatetStart,
                     DateEnd = Datepicker.GetDatetEnd,
                     RequireAdult = galaForAdult,
                     RequireChild = galaForChild,
                     DefaultGala = (byte)intGalaCal,
                     IsCompulsory = Iscom
                 };
                 int option = cInsert.InsertOptionGala(cInsert);
                 

                 ProductOptionCondition cProductOptionCondition = new ProductOptionCondition();
                int count = cProductOptionCondition.getListConditionByOptionId(int.Parse(this.qOptionId)).Count;
                int intConditionId = 0;
             
                 if (count == 0)
                 {
                     intConditionId = cProductOptionCondition.InsertNewCondition(byte.Parse(dropMarket.SelectedValue), "Gala Dinner Condition", int.Parse(this.qOptionId), 0, 0, 0, 0, true);
                 }
                 else
                 {
                     intConditionId = cProductOptionCondition.getConditionByOptionIdGalaOnly(int.Parse(this.qOptionId));
                     cProductOptionCondition.UpdateMarketcondition(intConditionId, byte.Parse(dropMarket.SelectedValue));
                 }
                     

             }
             else
             {
                 cOptionGala.DateStart = Datepicker.GetDatetStart;
                 cOptionGala.DateEnd = Datepicker.GetDatetEnd;
                 if (intGalaFor == 0)
                 {
                     cOptionGala.RequireAdult = true;
                     cOptionGala.RequireChild = false;
                 }

                 if (intGalaFor == 1)
                 {
                     cOptionGala.RequireAdult = false;
                     cOptionGala.RequireChild = true;
                 }

                 if (radioyes.Checked)
                 {
                     cOptionGala.IsCompulsory = true;
                 }
                 if (radioNo.Checked)
                 {
                     cOptionGala.IsCompulsory = false;
                 }

                 cOptionGala.DefaultGala = (byte)intGalaCal;
                 cOptionGala.Update();

                 ProductOptionCondition cProductOptionCondition = new ProductOptionCondition();
                 int count = cProductOptionCondition.getListConditionByOptionId(int.Parse(this.qOptionId)).Count;
                 int intConditionId = 0;

                 if (count == 0)
                 {
                     intConditionId = cProductOptionCondition.InsertNewCondition(byte.Parse(dropMarket.SelectedValue), "Gala Dinner Condition", int.Parse(this.qOptionId), 0, 0, 0, 0, true);
                 }
                 else
                 {
                     intConditionId = cProductOptionCondition.getConditionByOptionIdGalaOnly(int.Parse(this.qOptionId));
                    cProductOptionCondition.UpdateMarketcondition(intConditionId, byte.Parse(dropMarket.SelectedValue));
                 }
                 
             }

             Response.Redirect(Request.Url.ToString());

      }

         public void panelRateAddDataBind()
         {
             if (!string.IsNullOrEmpty(this.qOptionId))
             {
                 //dropSupplierDataBind();

                 GVProductPeriodDataBind();

                 GVRatePeriodCurrentDataBind();
             }
         }

         public void GVProductPeriodDataBind()
         {
             ProductOptionGala cOptionGala = new ProductOptionGala();
             ProductOptionCondition cProductOptionCondition = new ProductOptionCondition();
             int count = cProductOptionCondition.getListConditionByOptionId(int.Parse(this.qOptionId)).Count;



             if (cOptionGala.GetProductOptionGalabyOptionId(int.Parse(this.qOptionId)) != null)
             {
                 if (count > 0)
                 {
                     int intConditionId = cProductOptionCondition.getConditionByOptionIdGalaOnly(int.Parse(this.qOptionId));
                     short shrSupplierId = short.Parse(dropProductSup.SelectedValue);

                     ProductOption.ProductOptionPeriod cProductPeriod = new ProductOptionPeriod();
                     GVProductPeriod.DataSource = cProductPeriod.getProductPeriodListEffectiveCondition(int.Parse((this.Page as Hotels2BasePage).qProductId),
                         shrSupplierId, intConditionId);

                 }
             }
            
             GVProductPeriod.DataBind();
         }

         public void GVProductPeriod_OnRowDataBound(object sender, GridViewRowEventArgs e)
         {
             if (e.Row.RowType == DataControlRowType.DataRow)
             {
                 DateTime dDateStart = (DateTime)DataBinder.Eval(e.Row.DataItem, "DateStart");
                 DateTime dDateEnd = (DateTime)DataBinder.Eval(e.Row.DataItem, "DateEnd");

                 RadioButton rd = (RadioButton)e.Row.Cells[0].FindControl("radioPeriod");
                 rd.Attributes.Add("onclick", "javascript:fnCheckUnCheck('" + rd.ClientID + "');");
             }
         }

         public void tbnSaveRate_OnClick(object sender, EventArgs e)
         {
             ProductOptionGala cOptionGala = new ProductOptionGala();
             ProductOptionCondition cProductOptionCondition = new ProductOptionCondition();
             int count = cProductOptionCondition.getListConditionByOptionId(int.Parse(this.qOptionId)).Count;
             int intConditionId = 0;
             if (cOptionGala.GetProductOptionGalabyOptionId(int.Parse(this.qOptionId)) != null)
             {
                 if (count == 0)
                 {
                     intConditionId = cProductOptionCondition.InsertNewCondition(byte.Parse(dropMarket.SelectedValue), "Gala Dinner Condition", int.Parse(this.qOptionId), 0, 0, 0, 0, true);
                 }
                 else
                 {
                     intConditionId = cProductOptionCondition.getConditionByOptionIdGalaOnly(int.Parse(this.qOptionId));
                 }
                     
                     foreach (GridViewRow Gvrow in GVProductPeriod.Rows)
                     {
                         RadioButton rd = (RadioButton)GVProductPeriod.Rows[Gvrow.RowIndex].Cells[0].FindControl("radioPeriod");

                         TextBox txtPrice = GVProductPeriod.Rows[Gvrow.RowIndex].Cells[3].FindControl("txtPrice") as TextBox;
                         TextBox txtOwn = GVProductPeriod.Rows[Gvrow.RowIndex].Cells[4].FindControl("txtOwn") as TextBox;
                         TextBox txtRack = GVProductPeriod.Rows[Gvrow.RowIndex].Cells[5].FindControl("txtRack") as TextBox;

                         decimal ratePrice = decimal.Parse(txtPrice.Text);
                         decimal rateOwn = decimal.Parse(txtOwn.Text);
                         decimal rateRack = decimal.Parse(txtRack.Text);

                         int intPeriodId = (int)GVProductPeriod.DataKeys[Gvrow.RowIndex].Value;

                         if (rd.Checked)
                         {
                            ProductOptionConditionPrice.InsertRateConditionPrice(intConditionId, intPeriodId, ratePrice, rateOwn, rateRack);
                         }

                     }
                 
                 
                 GVProductPeriodDataBind();
                 GVRatePeriodCurrentDataBind();
             }
             else
             {
                 // alert
             }


         }

         public void tbnSaveEdit_Onclick(object sender, EventArgs e)
         {
             Button btn = (Button)sender;

             if (btn.CommandName == "Editrate")
             {
                 string[] Argument = btn.CommandArgument.Split(',');

                 int RowIndex = int.Parse(Argument[1]);
                 foreach (GridViewRow Item in GVRatePeriodCurrent.Rows)
                 {
                     if (Item.RowType == DataControlRowType.DataRow)
                     {
                         if (Item.RowIndex == RowIndex)
                         {
                             //int intConditionID = int.Parse(this.qConditionId);
                             int intConditionID = int.Parse(Argument[2]);
                             short shrSupplierID = short.Parse(dropProductSup.SelectedValue);
                             int PeriodId = int.Parse(Argument[0]);
                             
                             TextBox txtPrice = GVRatePeriodCurrent.Rows[Item.RowIndex].Cells[3].FindControl("txtPrice") as TextBox;
                             TextBox txtOwn = GVRatePeriodCurrent.Rows[Item.RowIndex].Cells[4].FindControl("txtOwn") as TextBox;
                             TextBox txtRack = GVRatePeriodCurrent.Rows[Item.RowIndex].Cells[5].FindControl("txtRack") as TextBox;
                             decimal ratePrice = decimal.Parse(txtPrice.Text);
                             decimal rateOwn = decimal.Parse(txtOwn.Text);
                             decimal rateRack = decimal.Parse(txtRack.Text);

                             ProductOptionConditionPrice.UpdateConditionPrice(intConditionID, PeriodId, ratePrice, rateOwn, rateRack);
                         }
                     }
                 }
             }

             

             //Response.Redirect(Request.Url.ToString() + "#current_rate");
             GVRatePeriodCurrentDataBind();

         }
         public void GVRatePeriodCurrentDataBind()
         {
             ProductOptionGala cOptionGala = new ProductOptionGala();
             ProductOptionCondition cProductOptionCondition = new ProductOptionCondition();
             int count = cProductOptionCondition.getListConditionByOptionId(int.Parse(this.qOptionId)).Count;

             

             if (cOptionGala.GetProductOptionGalabyOptionId(int.Parse(this.qOptionId)) != null)
             {
                 if (count > 0)
                 {
                     int intConditionId = cProductOptionCondition.getConditionByOptionIdGalaOnly(int.Parse(this.qOptionId));
                     short shrSupplierId = short.Parse(dropProductSup.SelectedValue);

                     ProductOptionConditionPrice cConditionPrice = new ProductOptionConditionPrice();

                     //Response.Write(cConditionPrice.getRateCurrentByConditionId(intConditionId, shrSupplierId).Count + "++++++" + shrSupplierId + "+++++" + intConditionId);
                     //Response.End();
                     GVRatePeriodCurrent.DataSource = cConditionPrice.getRateCurrentByConditionId(intConditionId, shrSupplierId);
                     GVRatePeriodCurrent.DataBind();

                 }
             }
         }

         public void GVRatePeriodCurrent_OnRowDataBound(object sender, GridViewRowEventArgs e)
         {

             if (!string.IsNullOrEmpty(this.qProductCat))
             {
                 
                 

                 if (e.Row.RowType == DataControlRowType.DataRow)
                 {
                         bool bolStatus = (bool)DataBinder.Eval(e.Row.DataItem, "Status");
                         if (!bolStatus)
                         {
                             //
                             TextBox txtPrice = e.Row.Cells[3].FindControl("txtPrice") as TextBox;
                             TextBox txtOwn = e.Row.Cells[4].FindControl("txtOwn") as TextBox;
                             TextBox txtRack = e.Row.Cells[5].FindControl("txtRack") as TextBox;
                             Button btn = e.Row.Cells[6].FindControl("tbnSaveEdit") as Button;
                             txtPrice.Text = "Disable";
                             txtOwn.Text = "Disable";
                             txtRack.Text = "Disable";
                             txtRack.ForeColor = System.Drawing.Color.FromArgb((byte)179, (byte)179, (byte)193);
                             txtOwn.ForeColor = System.Drawing.Color.FromArgb((byte)179, (byte)179, (byte)193);
                             txtPrice.ForeColor = System.Drawing.Color.FromArgb((byte)179, (byte)179, (byte)193);
                             btn.Enabled = false;

                             e.Row.ForeColor = System.Drawing.Color.FromArgb((byte)179, (byte)179, (byte)193);
                             //e.Row.CssClass = "disableRow";
                             e.Row.BackColor = System.Drawing.Color.FromArgb((byte)255, (byte)235, (byte)232);

                         }

                     
                    
                 }



             }
         }

         public void lnkStatus_Onclick(object sender, EventArgs e)
         {
             LinkButton btn = (LinkButton)sender;
             string[] argument = btn.CommandArgument.Split(',');
             int ConditionId = int.Parse(argument[1]);
             int PeriodId = int.Parse(argument[0]);
             ProductOptionConditionPrice PricePeriod = new ProductOptionConditionPrice();

             PricePeriod = PricePeriod.GetConditionPriceByConditionIdAndInPeriodID(ConditionId, PeriodId);
             if (PricePeriod.Status)
             {


                 PricePeriod.UpdateRatePeriodStatus(ConditionId, PeriodId, false);
             }
             else
             {
                 PricePeriod.UpdateRatePeriodStatus(ConditionId, PeriodId, true);
             }

             GVRatePeriodCurrentDataBind();
         }
    }
}