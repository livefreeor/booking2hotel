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
    public partial class admin_productOption_option_add : Hotels2BasePage
    {
       protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Product cProduct = new Product();
                cProduct = cProduct.GetProductById(int.Parse(this.qProductId));
                txthead.Text = cProduct.ProductCode + "&nbsp;::&nbsp;" + cProduct.Title;

                //if (!string.IsNullOrEmpty(this.qOptionId))
                //{
                //   Option cOption = 
                //}


                //FvOptionAdd.DefaultMode = FormViewMode.Insert;
                OptionPageDataBind();
                panelWeekDayDataBind();
                // run Java to Selected CheckBoxList Selected ** BackGroundColor
                if (string.IsNullOrEmpty(this.qOptionId))
                {
                    this.Page.ClientScript.RegisterStartupScript(Page.GetType(), null, "<script>fnCheckUnCheck();</script>", false);
                }

            }
        }

        public void OptionPageDataBind()
        {
            if (string.IsNullOrEmpty(this.qOptionId))
            {
                FvOptionAdd.ChangeMode(FormViewMode.Insert);
                ProductOptionCategory cOptionCat = new ProductOptionCategory();
                dropCat.DataSource = cOptionCat.GetProductCategoryAllExpGala();
                dropCat.DataValueField = "Key";
                dropCat.DataTextField = "Value";
                dropCat.DataBind();

                panelOptionCatSelect.Visible = true;
                panelOptionContentLang.Visible = false;
                panelOptionConfig.Visible = false;
                panelOptionFacility.Visible = false;
                //panelItinerary.Visible = false;

                
                    panelWeekDay.Visible = false;
                
                chkBoxSupplierDataBind();
            }
            else
            {
                //panelItinerary.Visible = false;
                //if (this.qProductCat == "34" || this.qProductCat == "36" || this.qProductCat == "38")
                //{
                    
                //    panelItinerary.Visible = true;
                //    lnkItinerary.NavigateUrl = "product_itinerary.aspx?oid=" + this.qOptionId + this.AppendCurrentQueryString();
                //}

                if (this.qProductCat != "29")
                {
                    panelWeekDay.Visible = true;
                }
                else
                {
                    panelWeekDay.Visible = false;
                }

                panelSupplierSelection.Visible = false;
                panelOptionCatSelect.Visible = true;
                panelOptionContentLang.Visible = true;
                panelOptionConfig.Visible = true;
                panelOptionFacility.Visible = true;

                FvOptionAdd.ChangeMode(FormViewMode.Edit);
                Option cOption = new Option();
                ProductOptionCategory cOptionCat = new ProductOptionCategory();

                dropCat.DataSource = cOptionCat.GetProductCategoryAllExpGala();

                

                dropCat.DataValueField = "Key";
                dropCat.DataTextField = "Value";
                dropCat.SelectedValue = cOption.getOptionById(int.Parse(this.qOptionId)).CategoryID.ToString();
                dropCat.DataBind();

                lblOptionTitle.Text = cOption.Title;

                if (cOption.Status == true)
                {
                    rbStatusEnable.Checked = true;
                    rbStatusDisable.Checked = false;
                }
                else
                {
                    rbStatusEnable.Checked = false;
                    rbStatusDisable.Checked = true;
                }

                if (cOption.IsShow == true)
                {
                    rbdetailShow.Checked = true;
                    rbdetailnotShow.Checked = false;
                }
                else
                {
                    rbdetailShow.Checked = false;
                    rbdetailnotShow.Checked = true;
                }

                GvSupplierListDataBind();

            }
        }
        public void chkBoxSupplierDataBind()
        {
            ProductSupplier cProductSUp = new ProductSupplier();
            chkBoxSupplier.DataSource = cProductSUp.getSupplierListByProductIdAndActiveOnly(int.Parse(this.qProductId));
            chkBoxSupplier.DataTextField = "SupplierTitle";
            chkBoxSupplier.DataValueField = "SupplierID";
            chkBoxSupplier.DataBind();

            foreach (ListItem itemCheck in chkBoxSupplier.Items)
            {
                itemCheck.Selected = true;
            }
        }

        public void GvSupplierListDataBind()
        {
            if (!string.IsNullOrEmpty(this.qOptionId))
            {
                int intOptionId = int.Parse(this.qOptionId);
                Option cOption = new Option();
                GvSupplierList.DataSource = Option.getSupplierListByOption(intOptionId);
                GvSupplierList.DataBind();
            }
        }

        public void chkBoxSupplier_DataBound(object sender, EventArgs e)
        {
            
            chkBoxSupplier.Attributes.Add("onclick", "javascript:fnCheckUnCheck('" + chkBoxSupplier.ClientID + "');");
            
        }
       

        protected void dropCat_OnSelectedIndexChanged(object sender ,EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.qOptionId))
            {
                FvOptionAdd.DataBind();
                OptionLangBox.DataBind();
                controlFacBox.DataBind();
            }
           
        }

        public void panelWeekDayDataBind()
        {
            if (this.qProductCat == "34" || this.qProductCat == "36" || this.qProductCat == "38" || this.qProductCat == "32")
            {
                if (!string.IsNullOrEmpty(this.qOptionId))
                {
                    ProductOptionIsWeekdayAll cOptionWeekendAll = new ProductOptionIsWeekdayAll();
                    cOptionWeekendAll = cOptionWeekendAll.GetProductOptionIsWeekdayAllById(int.Parse(this.qOptionId));
                    if (cOptionWeekendAll != null)
                    {
                        if (cOptionWeekendAll.DaySun)
                            chkSun.Checked = true;
                        else
                            chkSun.Checked = false;

                        if (cOptionWeekendAll.DayMon)
                            chkMon.Checked = true;
                        else
                            chkMon.Checked = false;

                        if (cOptionWeekendAll.DayTue)
                            chkTue.Checked = true;
                        else
                            chkTue.Checked = false;

                        if (cOptionWeekendAll.DayWed)
                            chkWed.Checked = true;
                        else
                            chkWed.Checked = false;

                        if (cOptionWeekendAll.DayThu)
                            chkThu.Checked = true;
                        else
                            chkThu.Checked = false;

                        if (cOptionWeekendAll.DayFri)
                            chkFri.Checked = true;
                        else
                            chkFri.Checked = false;

                        if (cOptionWeekendAll.DaySat)
                            chkSat.Checked = true;
                        else
                            chkSat.Checked = false;
                    }


                }
            }
        }

        public void btnWeekday_OnClick(object sender, EventArgs e)
        {
            bool bolSun = false;
            bool bolMon = false;
            bool bolTue = false;
            bool bolWed = false;
            bool bolThu = false;
            bool bolFri = false;
            bool bolSat = false;

            if (chkSun.Checked)
                bolSun = true;

            if (chkMon.Checked)
                bolMon = true;

            if (chkTue.Checked)
                bolTue = true;

            if (chkWed.Checked)
                bolWed = true;

            if (chkThu.Checked)
                bolThu = true;

            if (chkFri.Checked)
                bolFri = true;

            if (chkSat.Checked)
                bolSat = true;
            

            int intOptionId = int.Parse(this.qOptionId);
            ProductOptionIsWeekdayAll cOptionWeekendAll = new ProductOptionIsWeekdayAll();
            if (cOptionWeekendAll.GetProductOptionIsWeekdayAllById(intOptionId) == null)
            {
                cOptionWeekendAll.InsertIsWeekday(intOptionId, bolSun, bolMon, bolTue, bolWed, bolThu, bolFri, bolSat);
            }
            else
            {
                cOptionWeekendAll.UpdateIsWeekdayall(intOptionId, bolSun, bolMon, bolTue, bolWed, bolThu, bolFri, bolSat);
            }

            Response.Redirect(Request.Url.ToString());
        }

        protected void FvOptionAdd_OnItemdataBound(object sender, EventArgs e)
        {
            TextBox txtSize = FvOptionAdd.FindControl("txtSize") as TextBox;
            DropDownList drpHrsStart = FvOptionAdd.FindControl("drpHrsStart") as DropDownList;
            DropDownList drpMinsStart = FvOptionAdd.FindControl("drpMinsStart") as DropDownList;
            DropDownList drpHrsEnd = FvOptionAdd.FindControl("drpHrsEnd") as DropDownList;
            DropDownList drpMinsEnd = FvOptionAdd.FindControl("drpMinsEnd") as DropDownList;


            drpHrsStart.DataSource = this.dicGetTimEHrs(23);
            drpHrsStart.DataTextField = "Value";
            drpHrsStart.DataValueField = "Key";
            drpHrsStart.DataBind();

            drpMinsStart.DataSource = this.dicGetTimEHrs(60);
            drpMinsStart.DataTextField = "Value";
            drpMinsStart.DataValueField = "Key";
            drpMinsStart.DataBind();

            drpHrsEnd.DataSource = this.dicGetTimEHrs(23);
            drpHrsEnd.DataTextField = "Value";
            drpHrsEnd.DataValueField = "Key";
            drpHrsEnd.DataBind();

            drpMinsEnd.DataSource = this.dicGetTimEHrs(60);
            drpMinsEnd.DataTextField = "Value";
            drpMinsEnd.DataValueField = "Key";
            drpMinsEnd.DataBind();

            DropDownList drphrssendStart = FvOptionAdd.FindControl("drphrssendStart") as DropDownList;
            DropDownList drpminssendStart = FvOptionAdd.FindControl("drpminssendStart") as DropDownList;
            DropDownList drphrssendEnd = FvOptionAdd.FindControl("drphrssendEnd") as DropDownList;
            DropDownList drpminssendEnd = FvOptionAdd.FindControl("drpminssendEnd") as DropDownList;

            drphrssendStart.DataSource = this.dicGetTimEHrs(23);
            drphrssendStart.DataTextField = "Value";
            drphrssendStart.DataValueField = "Key";
            drphrssendStart.DataBind();

            drpminssendStart.DataSource = this.dicGetTimEHrs(60);
            drpminssendStart.DataTextField = "Value";
            drpminssendStart.DataValueField = "Key";
            drpminssendStart.DataBind();

            drphrssendEnd.DataSource = this.dicGetTimEHrs(23);
            drphrssendEnd.DataTextField = "Value";
            drphrssendEnd.DataValueField = "Key";
            drphrssendEnd.DataBind();

            drpminssendEnd.DataSource = this.dicGetTimEHrs(60);
            drpminssendEnd.DataTextField = "Value";
            drpminssendEnd.DataValueField = "Key";
            drpminssendEnd.DataBind();

            if (string.IsNullOrEmpty(this.qOptionId))
            {
                txtSize.Text = "0";
            }

           // paneloptionExtra 
            Panel paneloptionExtra = FvOptionAdd.FindControl("paneloptionExtra") as Panel;
            int intOptionCat = int.Parse(dropCat.SelectedValue);
            if (intOptionCat == 43 || intOptionCat == 44 || intOptionCat == 52 || intOptionCat == 53 || intOptionCat == 54)
            {
                paneloptionExtra.Visible = true;
                if (DataBinder.GetPropertyValue(FvOptionAdd.DataItem, "TimeStart") != null)
                {
                    DateTime TimeStart = (DateTime)DataBinder.GetPropertyValue(FvOptionAdd.DataItem, "TimeStart");
                    drpHrsStart.SelectedValue = TimeStart.Hour.ToString();
                    drpMinsStart.SelectedValue = TimeStart.Minute.ToString();
                }
                if(DataBinder.GetPropertyValue(FvOptionAdd.DataItem, "TimeEnd") != null)
                {
                    DateTime TimeEnd = (DateTime)DataBinder.GetPropertyValue(FvOptionAdd.DataItem, "TimeEnd");
                    drpHrsEnd.SelectedValue = TimeEnd.Hour.ToString();
                    drpMinsEnd.SelectedValue = TimeEnd.Minute.ToString();
                    
                }
                if(DataBinder.GetPropertyValue(FvOptionAdd.DataItem, "TimePickUp") != null)
                {
                    DateTime TimePickUp = (DateTime)DataBinder.GetPropertyValue(FvOptionAdd.DataItem, "TimePickUp");
                    drphrssendStart.SelectedValue = TimePickUp.Hour.ToString();
                    drpminssendStart.SelectedValue = TimePickUp.Minute.ToString();
                }
                if (DataBinder.GetPropertyValue(FvOptionAdd.DataItem, "TimeSent") != null)
                {
                    DateTime TimeSent = (DateTime)DataBinder.GetPropertyValue(FvOptionAdd.DataItem, "TimeSent");
                    drphrssendEnd.SelectedValue = TimeSent.Hour.ToString();
                    drpminssendEnd.SelectedValue = TimeSent.Minute.ToString();
                }
            }
            else
            {
                paneloptionExtra.Visible = false;
            }
        }
        protected void InsertButton_Onclick(object sender, EventArgs e)
        {
            
            this.Page.ClientScript.RegisterStartupScript(Page.GetType(), null, "<script>fnCheckUnCheck();</script>", false);
            
        }
        protected void FvOptionAdd_OnItemInserting(object sender, FormViewInsertEventArgs e)
        {
            int count = 0;
            foreach(ListItem chkListItem in chkBoxSupplier.Items)
            {
                if(chkListItem.Selected )
                {
                    count = count + 1;
                }
            }
            if (count == 0)
            {
                e.Cancel = true;
            }
            else
            {
                e.Values["CategoryID"] = short.Parse(dropCat.SelectedValue);
                e.Values["ProductID"] = int.Parse(this.qProductId);
                Option cOption = new Option();
                int intCountOption = cOption.GetProductOptionByCatId(int.Parse(this.qProductId), short.Parse(dropCat.SelectedValue)).Count;
                e.Values["Priority"] = (intCountOption + 1).ToString();

                int intOptionCat = int.Parse(dropCat.SelectedValue);
                if (intOptionCat == 43 || intOptionCat == 44 || intOptionCat == 52 || intOptionCat == 53 || intOptionCat == 54)
                {
                    DropDownList drpHrsStart = FvOptionAdd.FindControl("drpHrsStart") as DropDownList;
                    DropDownList drpMinsStart = FvOptionAdd.FindControl("drpMinsStart") as DropDownList;
                    DropDownList drpHrsEnd = FvOptionAdd.FindControl("drpHrsEnd") as DropDownList;
                    DropDownList drpMinsEnd = FvOptionAdd.FindControl("drpMinsEnd") as DropDownList;

                    DateTime dDateShowTimeStart = new DateTime(1999, 9, 9, int.Parse(drpHrsStart.SelectedValue), int.Parse(drpMinsStart.SelectedValue), 0);
                    DateTime dDateShowTimeEnd = new DateTime(1999, 9, 9, int.Parse(drpHrsEnd.SelectedValue), int.Parse(drpMinsEnd.SelectedValue), 0);
                    e.Values["TimeStart"] = dDateShowTimeStart;
                    e.Values["TimeEnd"] = dDateShowTimeEnd;


                    DropDownList drphrssendStart = FvOptionAdd.FindControl("drphrssendStart") as DropDownList;
                    DropDownList drpminssendStart = FvOptionAdd.FindControl("drpminssendStart") as DropDownList;
                    DropDownList drphrssendEnd = FvOptionAdd.FindControl("drphrssendEnd") as DropDownList;
                    DropDownList drpminssendEnd = FvOptionAdd.FindControl("drpminssendEnd") as DropDownList;

                    DateTime dDateSendTimeStart = new DateTime(1999, 9, 9, int.Parse(drphrssendStart.SelectedValue), int.Parse(drpminssendStart.SelectedValue), 0);
                    DateTime dDateSendTimeEnd = new DateTime(1999, 9, 9, int.Parse(drphrssendEnd.SelectedValue), int.Parse(drpminssendEnd.SelectedValue), 0);
                    e.Values["TimePickUp"] = dDateSendTimeStart;
                    e.Values["TimeSent"] = dDateSendTimeEnd;
                }
            }
            

            
        }

        protected void ObjFvOption_OnInserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            int count = 0;

            foreach (ListItem itemchked in chkBoxSupplier.Items)
            {
                if (itemchked.Selected)
                {
                    count = count + 1;
                }
            }

            if (count == 0)
            {

                
            }
            
        }

        protected void FvOptionAdd_OnItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            short intOptionCat = short.Parse(dropCat.SelectedValue);
            

            if (rbStatusEnable.Checked)
            {
                e.NewValues["Status"] = true;
            }
            else
            {
                e.NewValues["Status"] = false;
            }

           
            if (intOptionCat == 43 || intOptionCat == 44 || intOptionCat == 52 || intOptionCat == 53 || intOptionCat == 54)
            {
                
                DropDownList drpHrsStart = FvOptionAdd.FindControl("drpHrsStart") as DropDownList;
                DropDownList drpMinsStart = FvOptionAdd.FindControl("drpMinsStart") as DropDownList;
                DropDownList drpHrsEnd = FvOptionAdd.FindControl("drpHrsEnd") as DropDownList;
                DropDownList drpMinsEnd = FvOptionAdd.FindControl("drpMinsEnd") as DropDownList;

                DateTime dDateShowTimeStart = new DateTime(1999, 9, 9, int.Parse(drpHrsStart.SelectedValue), int.Parse(drpMinsStart.SelectedValue), 0);
                DateTime dDateShowTimeEnd = new DateTime(1999, 9, 9, int.Parse(drpHrsEnd.SelectedValue), int.Parse(drpMinsEnd.SelectedValue), 0);
                e.NewValues["TimeStart"] = dDateShowTimeStart;
                e.NewValues["TimeEnd"] = dDateShowTimeEnd;

                e.NewValues["Priority"] = e.OldValues["Priority"];

                DropDownList drphrssendStart = FvOptionAdd.FindControl("drphrssendStart") as DropDownList;
                DropDownList drpminssendStart = FvOptionAdd.FindControl("drpminssendStart") as DropDownList;
                DropDownList drphrssendEnd = FvOptionAdd.FindControl("drphrssendEnd") as DropDownList;
                DropDownList drpminssendEnd = FvOptionAdd.FindControl("drpminssendEnd") as DropDownList;

                DateTime dDateSendTimeStart = new DateTime(1999, 9, 9, int.Parse(drphrssendStart.SelectedValue), int.Parse(drpminssendStart.SelectedValue), 0);
                DateTime dDateSendTimeEnd = new DateTime(1999, 9, 9, int.Parse(drphrssendEnd.SelectedValue), int.Parse(drpminssendEnd.SelectedValue), 0);
                e.NewValues["TimePickUp"] = dDateSendTimeStart;
                e.NewValues["TimeSent"] = dDateSendTimeEnd;
            }

            e.NewValues["CategoryID"] = intOptionCat;
         }

        protected void FvOptionAdd_OnItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            int intOptionId = int.Parse(this.qOptionId);
            ProductOptionIsWeekdayAll cOptionWeekendAll = new ProductOptionIsWeekdayAll();
            if (cOptionWeekendAll.GetProductOptionIsWeekdayAllById(intOptionId) == null)
            {
                cOptionWeekendAll.InsertIsWeekday(intOptionId, true, true, true, true, true, true, true);
            }
           Response.Redirect(Request.UrlReferrer.ToString());
        }

        protected void ObjFvOption_OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            
            foreach (ListItem itemchk in chkBoxSupplier.Items)
            {
                if (itemchk.Selected)
                {
                    int inserted = Option.insertOptionMappingSupplier((int)e.ReturnValue, short.Parse(itemchk.Value));
                }
            }

            int intOptionId = (int)e.ReturnValue;

            //Response.Write(intOptionId);
            //Response.End();
            ProductOptionIsWeekdayAll cOptionWeekendAll = new ProductOptionIsWeekdayAll();
            //cOptionWeekendAll = cOptionWeekendAll.GetProductOptionIsWeekdayAllById(intOptionId);
            if (cOptionWeekendAll.GetProductOptionIsWeekdayAllById(intOptionId) == null)
            {
                cOptionWeekendAll.InsertIsWeekday(intOptionId, true, true, true, true, true, true, true);
            }

           Response.Redirect(Request.UrlReferrer.ToString() + "&oid=" + e.ReturnValue);
        }

        protected void btConfig_Onclik(object sender, EventArgs e)
        {
            Option cOption = new Option();
            cOption.getOptionById(int.Parse(this.qOptionId));

            if (rbStatusEnable.Checked)
            {
                cOption.Status = true;
            }
            if (rbStatusDisable.Checked)
            {
                cOption.Status = false;
            }

            if (rbdetailShow.Checked)
            {
                cOption.IsShow = true;
            }

            if (rbdetailnotShow.Checked)
            {
                cOption.IsShow = false;
            }

            cOption.Update();
            Response.Redirect(Request.UrlReferrer.ToString());
        }
        protected void PriorityBinding()
        {
            Option cOption = new Option();
        }
        

    }
}