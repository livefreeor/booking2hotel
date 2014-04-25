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
    public partial class admin_product_option_promotion : Hotels2BasePage
    {
         

         protected void Page_Load(object sender, EventArgs e)
         {
             if (!this.Page.IsPostBack)
             {
                 dropDownSupplierListDataBind();

                 Product cProduct = new Product();
                 cProduct = cProduct.GetProductById(int.Parse(this.qProductId));
                 txthead.Text = cProduct.ProductCode + "&nbsp;::&nbsp;" + cProduct.Title;
                 dropDownSupplierList.SelectedValue = cProduct.SupplierPrice.ToString();
                 radioIsWeekendYes.Attributes.Add("onclick", "javascript:DisableDiv('DivdayofWeek');");
                 radioIsWeekendNo.Attributes.Add("onclick", "javascript:EnableDiv('DivdayofWeek');");

                 radioTimeproYes.Attributes.Add("onclick", "javascript:EnableDiv('DivTimePeriod');");
                 radioTimeproNo.Attributes.Add("onclick", "javascript:DisableDiv('DivTimePeriod');");
                 

                     
                panelInformationdataBind();
                PanelBenefitDataBind();
                PanelProdetailDataBind();
                PanelRoomSelectDataBind();

                if (!string.IsNullOrEmpty(Request.QueryString["PromotionFirstStep"]))
                {
                    panelSupplierSelection.Visible = true;
                    panelSupplierActive.Visible = false;
                }
                else
                {
                    panelSupplierSelection.Visible = false;
                    panelSupplierActive.Visible = true;
                    
                    SupplierActiveDataBind();
                    
                }
             }
             
         }

        // panelSupplierActive==========================================================================
         public void SupplierActiveDataBind()
         {
             int intPromotionId = int.Parse(this.qPromotionId);
             Promotion cPromotion = new Promotion();

             short SupplierId = cPromotion.GetPromotionById(intPromotionId).SupplierId;
             Suppliers.Supplier cSupplier = new Suppliers.Supplier();
             SupplierActive.Text = " Promotion For :" + cSupplier.GetSupplierbyId(SupplierId).SupplierTitle;
         }
         // panel Supplier Selection ===================================================================
         public void dropDownSupplierListDataBind()
         {
             ProductSupplier cProductSUp = new ProductSupplier();
             dropDownSupplierList.DataSource = cProductSUp.getSupplierListByProductIdAndActiveOnly(int.Parse(this.qProductId));
             dropDownSupplierList.DataTextField = "SupplierTitle";
             dropDownSupplierList.DataValueField = "SupplierID";
             dropDownSupplierList.DataBind();
         }
        
        //panelProdetail=================================================================================
         public void PanelProdetailDataBind()
         {
             DropProcatDataBind();
             if (!string.IsNullOrEmpty(this.qPromotionId))
             {
                 int intPromotionId = int.Parse(this.qPromotionId);
                 Promotion cPromotion = new Promotion();
                 cPromotion = cPromotion.GetPromotionById(intPromotionId);

                 txtPrograme.Text = cPromotion.Title;
                 dropPromotionCat.SelectedValue = cPromotion.ProCatId.ToString();
                 controlProgrameStart.DateStart = cPromotion.DateStart;
                 controlProgrameStart.DateEnd = cPromotion.DateEnd;
                 controlProgrameStart.DataBind();
                 txtComment.Text = cPromotion.Comment;

                 dropDownSupplierList.SelectedValue = cPromotion.SupplierId.ToString();

                 if (cPromotion.Status)
                 {
                     radioStatus.SelectedValue = "1";
                 }
                 else
                 {
                     radioStatus.SelectedValue = "0";
                 }
             }
             else
             {
                 panelPromotionCOntent.Visible = false;
             }
             
             
             
         }

         public void DropProcatDataBind()
         {
             PromotionCatAndType cPromotionCat = new PromotionCatAndType();
             dropPromotionCat.DataSource = cPromotionCat.GetPromotionCat();
             dropPromotionCat.DataTextField = "Value";
             dropPromotionCat.DataValueField = "Key";
             dropPromotionCat.DataBind();
         }

        

        public void btnSaveProdetail_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.qPromotionId))
            {
                Promotion cPromotion = new Promotion();
                byte bytCatId = byte.Parse(dropPromotionCat.SelectedValue);
                int intProductId = int.Parse(this.qProductId);
                short shrSupplierId = short.Parse(dropDownSupplierList.SelectedValue);
                int ProinsertId = cPromotion.InsertNewPromotionPanelDetail(bytCatId, intProductId, shrSupplierId, txtPrograme.Text, controlProgrameStart.GetDatetStart, controlProgrameStart.GetDatetEnd, txtComment.Text);

                Response.Redirect("product_option_promotion.aspx?proId=" + ProinsertId + this.AppendCurrentQueryString());
            }
            else
            {
                Promotion cPromotion = new Promotion();
                byte bytCatId = byte.Parse(dropPromotionCat.SelectedValue);
                int intPromotionId = int.Parse(this.qPromotionId);
                bool Status = true;
                if (radioStatus.Items[1].Selected)
                {
                    Status = false;
                }
                bool Proupdate = cPromotion.UpdatePromotionPanelDetail(intPromotionId, bytCatId, txtPrograme.Text, controlProgrameStart.GetDatetStart, controlProgrameStart.GetDatetEnd, txtComment.Text, Status);
                Response.Redirect(Request.Url.ToString());
            }
        }

        //Panel ProductInformation===========================================================================
        public void panelInformationdataBind()
        {
            if (!string.IsNullOrEmpty(this.qPromotionId))
            {
                dropDataBind();

                int intPromotionId = int.Parse(this.qPromotionId);
                Promotion cPromotion = new Promotion();
                cPromotion = cPromotion.GetPromotionById(intPromotionId);
                dropPromotinonException.SelectedValue = cPromotion.IsWeekEndAll.ToString();
                if (cPromotion.IsSun && cPromotion.IsMon && cPromotion.IsTue && cPromotion.IsWed && cPromotion.IsThu && cPromotion.IsFri && cPromotion.IsSat)
                {
                    radioIsWeekendYes.Checked = true;
                    radioIsWeekendNo.Checked = false;
                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(Page.GetType(), null, "<script>showDiv('DivdayofWeek');</script>", false);
                    radioIsWeekendYes.Checked = false;
                    radioIsWeekendNo.Checked = true;

                    if (!cPromotion.IsSun) { chkDayofWeek.Items[0].Selected = false; } else { chkDayofWeek.Items[0].Selected = true; }
                    if (!cPromotion.IsMon) { chkDayofWeek.Items[1].Selected = false; } else { chkDayofWeek.Items[1].Selected = true; }
                    if (!cPromotion.IsTue) { chkDayofWeek.Items[2].Selected = false; } else { chkDayofWeek.Items[2].Selected = true; }
                    if (!cPromotion.IsWed) { chkDayofWeek.Items[3].Selected = false; } else { chkDayofWeek.Items[3].Selected = true; }
                    if (!cPromotion.IsThu) { chkDayofWeek.Items[4].Selected = false; } else { chkDayofWeek.Items[4].Selected = true; }
                    if (!cPromotion.IsFri) { chkDayofWeek.Items[5].Selected = false; } else { chkDayofWeek.Items[5].Selected = true; }
                    if (!cPromotion.IsSat) { chkDayofWeek.Items[6].Selected = false; } else { chkDayofWeek.Items[6].Selected = true; }
                }

                dropDayMin.SelectedValue = cPromotion.DayMin.ToString();
                dropRoomMin.SelectedValue = cPromotion.QuantityMin.ToString();
                dropDayAdvanceMin.SelectedValue = cPromotion.DayAdVanceMin.ToString();
                radioIsholiday.SelectedValue = cPromotion.IsHolidayCharge.ToString();
                dropMaxset.SelectedValue = cPromotion.MaxRepeatSet.ToString();
                radioBreakfast.SelectedValue = cPromotion.IsBreakfast.ToString();
                txtBreakfastCharge.Text = cPromotion.BreakfastCharge.ToString("#,0");
                controlDateUsePro.DateStart = cPromotion.DateuseStart;
                controlDateUsePro.DateEnd = cPromotion.DateuseEnd;
                controlDateUsePro.DataBind();

                if (cPromotion.TimeStart != null && cPromotion.TimeEnd != null)
                {
                    this.Page.ClientScript.RegisterStartupScript(Page.GetType(), null, "<script>showDiv('DivTimePeriod');</script>", false);
                    radioTimeproYes.Checked = true;
                    radioTimeproNo.Checked = false;
                    DateTime dDateOpen = (DateTime)cPromotion.TimeStart;
                    DateTime dDateClose = (DateTime)cPromotion.TimeEnd;

                    drpHrsStart.SelectedValue = dDateOpen.Hour.ToString();
                    drpMinsStart.SelectedValue = dDateOpen.Minute.ToString();

                    drpHrsEnd.SelectedValue = dDateClose.Hour.ToString();
                    drpMinsEnd.SelectedValue = dDateClose.Minute.ToString();
                }
                else
                {
                    radioTimeproNo.Checked = true;
                    radioTimeproYes.Checked = false;

                }

            }
            else
            {
                panelPromotioninformation.Visible = false;
            }
            
        }

        public void dropDataBind()
        {

            drpHrsStart.DataSource = this.dicGetTimEHrs(23);
            drpHrsStart.DataTextField = "Value";
            drpHrsStart.DataValueField = "Key";
            drpHrsStart.DataBind();

            drpHrsEnd.DataSource = this.dicGetTimEHrs(23);
            drpHrsEnd.DataTextField = "Value";
            drpHrsEnd.DataValueField = "Key";
            drpHrsEnd.DataBind();

            dropDayMin.DataSource = this.dicGetNumber(30);
            dropDayMin.DataTextField = "Value";
            dropDayMin.DataValueField = "Key";
            dropDayMin.DataBind();

            dropRoomMin.DataSource = this.dicGetNumber(30);
            dropRoomMin.DataTextField = "Value";
            dropRoomMin.DataValueField = "Key";
            dropRoomMin.DataBind();

            dropDayAdvanceMin.DataSource = this.dicGetNumber(180);
            dropDayAdvanceMin.DataTextField = "Value";
            dropDayAdvanceMin.DataValueField = "Key";
            dropDayAdvanceMin.DataBind();

            dropMaxset.DataSource = this.dicGetNumber(10);
            dropMaxset.DataTextField = "Value";
            dropMaxset.DataValueField = "Key";
            dropMaxset.DataBind();

            ListItem newList = new ListItem("Infinity", "100");
            dropMaxset.Items.Insert(0, newList);
            
        }

        public void btnProInformationSave_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.qPromotionId))
            {
                int intPromotionId = int.Parse(this.qPromotionId);
                byte bytdayMin = byte.Parse(dropDayMin.SelectedValue);
                byte bytRoomMin = byte.Parse(dropRoomMin.SelectedValue);
                short shrDayAdVance = short.Parse(dropDayAdvanceMin.SelectedValue);
                byte IsHolidays = byte.Parse(radioIsholiday.SelectedValue);
                byte MaxSet = byte.Parse(dropMaxset.SelectedValue);

                byte bytIsBreakfast = byte.Parse(radioBreakfast.SelectedValue);
                decimal aBFCharge = 0;
                if (bytIsBreakfast == 3)
                {
                    aBFCharge = decimal.Parse(txtBreakfastCharge.Text);
                }

                DateTime dDateUseStart = controlDateUsePro.GetDatetStart;
                DateTime dDateuseEnd = controlDateUsePro.GetDatetEnd;

                Nullable<DateTime> tTimeStart = DateTime.MinValue;
                Nullable<DateTime> tTimeEnd = DateTime.MinValue;
                if (radioTimeproYes.Checked)
                {
                    tTimeStart = new DateTime(1900, 09, 09, int.Parse(drpHrsStart.SelectedValue), int.Parse(drpMinsStart.SelectedValue), 0);
                    tTimeEnd = new DateTime(1900, 09, 09, int.Parse(drpHrsEnd.SelectedValue), int.Parse(drpMinsEnd.SelectedValue), 0);
                }
                if (radioTimeproNo.Checked)
                {
                    tTimeStart = null;
                    tTimeEnd = null;
                }

                Promotion cPromotion = new Promotion();
                 if (radioIsWeekendYes.Checked)
                {
                    //Response.Write("TEST");
                    //Response.End();
                    cPromotion.UpdatePromotionPanelInformation(intPromotionId, 1, true, true, true, true, true, true, true, bytdayMin, bytRoomMin,
                        shrDayAdVance, IsHolidays, MaxSet, bytIsBreakfast, aBFCharge, dDateUseStart, dDateuseEnd, (Nullable<DateTime>)tTimeStart, (Nullable<DateTime>)tTimeEnd);
                }

                if(radioIsWeekendNo.Checked)
                {
                   
                    bool IsSun = true;
                    bool IsMon = true;
                    bool IsTue = true;
                    bool IsWed = true;
                    bool IsThu = true;
                    bool IsFri = true;
                    bool IsSat = true;

                    if (chkDayofWeek.Items[0].Selected == false) { IsSun = false; }
                    if (chkDayofWeek.Items[1].Selected == false) { IsMon = false; }
                    if (chkDayofWeek.Items[2].Selected == false) { IsTue = false; }
                    if (chkDayofWeek.Items[3].Selected == false) { IsWed = false; }
                    if (chkDayofWeek.Items[4].Selected == false) { IsThu = false; }
                    if (chkDayofWeek.Items[5].Selected == false) { IsFri = false; }
                    if (chkDayofWeek.Items[6].Selected == false) { IsSat = false; }

                    byte bytProExcept = byte.Parse(dropPromotinonException.SelectedValue);
                    
                    cPromotion.UpdatePromotionPanelInformation(intPromotionId, bytProExcept, IsMon, IsTue, IsWed, IsThu, IsFri, IsSat, IsSun, bytdayMin, bytRoomMin,
                        shrDayAdVance, IsHolidays, MaxSet, bytIsBreakfast, aBFCharge, dDateUseStart, dDateuseEnd, (Nullable<DateTime>)tTimeStart, (Nullable<DateTime>)tTimeEnd);
                }

                Response.Redirect(Request.Url.ToString());

            }
        }

        //Panel Benefit=======================================================================
        public void PanelBenefitDataBind()
        {
            if (!string.IsNullOrEmpty(this.qPromotionId))
            {
                DropPromotionBenefitDataBind();
                GvProBenefitListDataBind();
            }
            else
            {
                panelbenefit.Visible = false;
            }
        }

        public void GvProBenefitList_OnDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                byte bytProtype = (byte)DataBinder.Eval(e.Row.DataItem, "TypeId");
                byte bytStartDisnight = (byte)DataBinder.Eval(e.Row.DataItem, "DaydiscountStart");
                byte bytNoDisNight = (byte)DataBinder.Eval(e.Row.DataItem, "DaydiscountNum");
                byte bytPirority = (byte)DataBinder.Eval(e.Row.DataItem, "Priority");
                decimal strdisAmount = (decimal)DataBinder.Eval(e.Row.DataItem, "DiscountAmount");
                bool bolStatus = (bool)DataBinder.Eval(e.Row.DataItem, "Status");

                DropDownList dropProType = e.Row.Cells[2].FindControl("dropProType") as DropDownList;
                PromotionCatAndType cProcatType = new PromotionCatAndType();
                dropProType.DataSource = cProcatType.GetPromotionType();
                dropProType.DataTextField = "Value";
                dropProType.DataValueField = "Key";
                dropProType.DataBind();
                dropProType.SelectedValue = bytProtype.ToString();

                DropDownList dropStartDisnight = e.Row.Cells[3].FindControl("dropStartDisnight") as DropDownList;
                dropStartDisnight.DataSource = this.dicGetNumber(30);
                dropStartDisnight.DataTextField = "Value";
                dropStartDisnight.DataValueField = "Key";
                dropStartDisnight.DataBind();
                dropStartDisnight.SelectedValue = bytStartDisnight.ToString();

                DropDownList dropNoDisNight = e.Row.Cells[4].FindControl("dropNoDisNight") as DropDownList;
                dropNoDisNight.DataSource = this.dicGetNumber(30);
                dropNoDisNight.DataTextField = "Value";
                dropNoDisNight.DataValueField = "Key";
                dropNoDisNight.DataBind();
                dropNoDisNight.SelectedValue = bytNoDisNight.ToString();

                DropDownList dropPirority = e.Row.Cells[5].FindControl("dropPirority") as DropDownList;
                dropPirority.DataSource = this.dicGetNumber(10);
                dropPirority.DataTextField = "Value";
                dropPirority.DataValueField = "Key";
                dropPirority.DataBind();
                dropPirority.SelectedValue = bytPirority.ToString();


                TextBox dropdisAmount = e.Row.Cells[6].FindControl("txtDisAmount") as TextBox;
                dropdisAmount.Text = strdisAmount.ToString("0,0");
                ImageButton imgStatus = e.Row.Cells[7].FindControl("imgbtnStatus") as ImageButton;
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

            int intBenefitID = int.Parse(Argument[0]);
            int intRowInDex = int.Parse(Argument[1]);
            bool Status = bool.Parse(Argument[2]);

            GridViewRow GvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int RowIndex = GvRow.RowIndex;
            if (intRowInDex == RowIndex)
            {
                PromotionBenefit cProNebefit = new PromotionBenefit();
                if(Status)
                    cProNebefit.UpdateBenefitByBenefitIdStatus(intBenefitID,false);
                else
                    cProNebefit.UpdateBenefitByBenefitIdStatus(intBenefitID,true);

            }
            PanelBenefitDataBind();
            //Response.Redirect(Request.Url.ToString());
        }
        public void DropPromotionBenefitDataBind()
        {
            PromotionCatAndType cProcatType = new PromotionCatAndType();
            dropproType.DataSource = cProcatType.GetPromotionType();
            dropproType.DataTextField = "Value";
            dropproType.DataValueField = "Key";
            dropproType.DataBind();

            dropStartDisCountnight.DataSource = this.dicGetNumber(30);
            dropStartDisCountnight.DataTextField = "Value";
            dropStartDisCountnight.DataValueField = "Key";
            dropStartDisCountnight.DataBind();

            dropNumDiscountnight.DataSource = this.dicGetNumber(30);
            dropNumDiscountnight.DataTextField = "Value";
            dropNumDiscountnight.DataValueField = "Key";
            dropNumDiscountnight.DataBind();
        }

       

        public void GvProBenefitListDataBind()
        {
            if (!string.IsNullOrEmpty(this.qPromotionId))
            {
                int Promotionid = int.Parse(this.qPromotionId);
                PromotionBenefit cProBenefit = new PromotionBenefit();
                GvProBenefitList.DataSource = cProBenefit.GetBenefitListByPromotionId(Promotionid);
                GvProBenefitList.DataBind();
            }
            
        }

        public void btnBenefitSave_OnClick(object sender, EventArgs e)
        {
            byte bytTpyeId = byte.Parse(dropproType.SelectedValue);
            byte bytStartDiscount = byte.Parse(dropStartDisCountnight.SelectedValue);
            byte bytNumdisCount = byte.Parse(dropNumDiscountnight.SelectedValue);
            decimal decAmount = decimal.Parse(txtDiscount.Text);
            byte bytPriority =  (byte)(GvProBenefitList.Rows.Count + 1);
                    
             if (!string.IsNullOrEmpty(this.qPromotionId))
            {
                 int Promotionid = int.Parse(this.qPromotionId);
                PromotionBenefit cPromotionBenefit = new PromotionBenefit();
                if (cPromotionBenefit.CheckBenefitIsRecord(Promotionid, bytStartDiscount, bytNumdisCount, bytTpyeId) > 0)
                {
                    panelAlertBox.Visible = true;
                    //this.Page.ClientScript.RegisterStartupScript(Page.GetType(), null, "<script>Alertlightbox('No Period Match');</script>", false);
                }
                else
                {
                    cPromotionBenefit.InsertBenefit(Promotionid, bytStartDiscount, bytNumdisCount, bytTpyeId, decAmount, bytPriority);
                    panelAlertBox.Visible = false;
                }
                 
             }

             GvProBenefitListDataBind();
        }

        public void tbnBenefitSaveEdit_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.qPromotionId))
            {
                Button btn = (Button)sender;
                int intBenefitId = int.Parse(btn.CommandArgument);

                GridViewRow GvRow = (GridViewRow)(sender as Control).Parent.Parent;
                int RowinDex = GvRow.RowIndex;

                DropDownList dropProType = GvProBenefitList.Rows[RowinDex].Cells[2].FindControl("dropProType") as DropDownList;
                DropDownList dropStartDisnight = GvProBenefitList.Rows[RowinDex].Cells[3].FindControl("dropStartDisnight") as DropDownList;
                DropDownList dropNoDisNight = GvProBenefitList.Rows[RowinDex].Cells[4].FindControl("dropNoDisNight") as DropDownList;
                DropDownList dropPirority = GvProBenefitList.Rows[RowinDex].Cells[5].FindControl("dropPirority") as DropDownList;
                TextBox dropdisAmount = GvProBenefitList.Rows[RowinDex].Cells[6].FindControl("txtDisAmount") as TextBox;
                byte bytTpyeId = byte.Parse(dropProType.SelectedValue);
                byte bytStartDiscount = byte.Parse(dropStartDisnight.SelectedValue);
                byte bytNumdisCount = byte.Parse(dropNoDisNight.SelectedValue);
                decimal decAmount = decimal.Parse(dropdisAmount.Text);
                byte bytPriority = byte.Parse(dropPirority.SelectedValue);


                PromotionBenefit cPromotionBenefit = new PromotionBenefit();
                cPromotionBenefit.UpdateBenefitByBenefitId(intBenefitId, bytStartDiscount, bytNumdisCount, bytTpyeId, decAmount, bytPriority);
                //int Promotionid = int.Parse(this.qPromotionId);
                //if (cPromotionBenefit.CheckBenefitIsRecord(Promotionid, bytStartDiscount, bytNumdisCount, bytTpyeId) > 0)
                //{
                //    this.Page.ClientScript.RegisterStartupScript(Page.GetType(), null, "<script>Alertlightbox('No Period Match');</script>", false);
                //}
                //else
                //{
                    
                //}
                
            }
            GvProBenefitListDataBind();
        }

        //PanelRoom Selected =================================================================================

        public void PanelRoomSelectDataBind()
        {
            if (!string.IsNullOrEmpty(this.qPromotionId))
            {

                //dropDownSupplierListDataBind();
                ProductOptionListDataBind();

            }
            else
            {
                panelRoomSelect.Visible = false;
            }
            
        }

        

        //public void dropDownSupplierList_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ProductOptionListDataBind();
        //}

        public void ProductOptionListDataBind()
        {
           short SupplierId = 0;
            //if (!string.IsNullOrEmpty(Request.QueryString["PromotionFirstStep"]))
            //{
            //    SupplierId = short.Parse(dropDownSupplierList.SelectedValue);
            //}
            //else
            //{
                Promotion cPromotion = new Promotion();
                int intPromotionId = int.Parse(this.qPromotionId);
                SupplierId = cPromotion.GetPromotionById(intPromotionId).SupplierId;
                
            //}

            Option cOption = new Option();
            //short shrSupplierId = short.Parse(dropDownSupplierList.SelectedValue);
            GvRoomTypeSelectionList.DataSource = cOption.GetProductOptionByCurrentSupplierANDProductId(SupplierId, int.Parse(this.qProductId));
            GvRoomTypeSelectionList.DataBind();

            //ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>fnCheckUnCheckOnLoad();</script>", false);
            //ScriptManager.RegisterStartupScript(Page.GetType(), null, "<script>fnCheckUnCheckOnLoad();</script>", false);
            //this.Page.ClientScript.RegisterStartupScript(Page.GetType(), null, "<script>fnCheckUnCheckOnLoad();</script>", false);
           
        }

        public void GvRoomTypeSelectionList_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRoom = e.Row.Cells[0].FindControl("chkRoom") as CheckBox;
                GridView GVCondition = e.Row.Cells[0].FindControl("GVConditionSelectionLit") as GridView;


                int intOptionId = (int)GvRoomTypeSelectionList.DataKeys[e.Row.DataItemIndex].Value;
                ProductOptionCondition cOptionCondition = new ProductOptionCondition();
                GVCondition.DataSource = cOptionCondition.getConditionListOpenOnly(intOptionId);
                GVCondition.DataBind();


                chkRoom.Attributes.Add("onclick", "javascript:fnCheckUnCheck('" + chkRoom.ClientID + "');");
            }
        }

        public void GVConditionSelectionLit_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Response.Write(ViewState["ConditionItem"]);
                //Response.End();
                CheckBox chkCondition = e.Row.Cells[0].FindControl("chkCondition") as CheckBox;
                chkCondition.Attributes.Add("onclick", "javascript:fnChildCheckUnCheck('" + chkCondition.ClientID + "');");

                int intConditionId = (int)DataBinder.Eval(e.Row.DataItem, "ConditionId");

                int intPromotionId = int.Parse(this.qPromotionId);
                PromotionCondition PromotionCondition = new PromotionCondition();

               
                    foreach (PromotionCondition ItemConditionId in PromotionCondition.getConditionByPromotionId(intPromotionId))
                    {
                        if (ItemConditionId.ConditionId == intConditionId)
                        {
                            
                            chkCondition.Checked = true;
                            ScriptManager.RegisterStartupScript(this, Page.GetType(), intConditionId.ToString(), "<script>fnCheckUnCheckOnLoad('" + chkCondition.ClientID + "');</script>", false);
                            
                        }
                    }

                

            }
        }

        public void btnRoomSelectionListSave_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.qPromotionId))
            {
                int intPromotionId = int.Parse(this.qPromotionId);
                
            
                foreach (GridViewRow RowItem in GvRoomTypeSelectionList.Rows)
                {
                    GridView GvChild = GvRoomTypeSelectionList.Rows[RowItem.RowIndex].Cells[0].FindControl("GVConditionSelectionLit") as GridView;
                    foreach (GridViewRow RowItemChild in GvChild.Rows)
                    {
                        PromotionCondition cPromotionCondition = new PromotionCondition();
                        int intConditionId = (int)GvChild.DataKeys[RowItemChild.RowIndex].Value;
                        CheckBox chkList = GvChild.Rows[RowItemChild.RowIndex].Cells[0].FindControl("chkCondition") as CheckBox;

                        if (chkList.Checked)
                        {

                            cPromotionCondition = cPromotionCondition.getConditionByPromotionAndConditionId(intPromotionId, intConditionId);
                            if (cPromotionCondition == null)
                            {
                                PromotionCondition cPromotionConditionInsert = new PromotionCondition();
                                cPromotionConditionInsert.InsertMappingConditionPromotionId(intPromotionId, intConditionId);
                            }
                            else
                            {
                                cPromotionCondition.UPdateMappingConditionPromotionId(intPromotionId, intConditionId, true);
                            }
                        }
                        else
                        {
                            cPromotionCondition = cPromotionCondition.getConditionByPromotionAndConditionId(intPromotionId, intConditionId);
                            if (cPromotionCondition != null)
                            {
                                cPromotionCondition.UPdateMappingConditionPromotionId(intPromotionId, intConditionId, false);
                            }
                            
                        }
                    }
                }
            }

            Response.Redirect(Request.Url.ToString() + "#conditionSel");
            //ProductOptionListDataBind();

        }
    }
}