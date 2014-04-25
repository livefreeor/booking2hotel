using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.ProductOption;
using Hotels2thailand;
using Hotels2thailand.Production;
using Hotels2thailand.Suppliers;

namespace Hotels2thailand.UI
{
    public partial class admin_productOption_product_option_rate : Hotels2BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Product cProduct = new Product();
                cProduct = cProduct.GetProductById(int.Parse(this.qProductId));
                Destitle.Text = cProduct.DestinationTitle;
                txthead.Text = cProduct.Title;


                PageDataBind();
                dropProductSupDataBind();
                dropProductSup.SelectedValue = cProduct.SupplierPrice.ToString();
                dropoptionDataBind();
                GvSupplierListDataBind();

                ConditionListDatabind();
                //dropoptionDataBind();

                ConditionInformantionDataBind();
                ConditionPolicyDataBind();

                

                dropSupplier.SelectedValue = cProduct.SupplierPrice.ToString();
                
                panelRateAddDataBind();
                panelMarketDataBind();
            }
        }

        public void PageDataBind()
        {
            if (string.IsNullOrEmpty(this.qConditionId))
            {
                panelPolicyAdd.Visible = false;
                panelRateAdd.Visible = false;
                panelMarket.Visible = false;
                screenBlock.Visible = true;

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
            dropoptionDataBind();
            GvSupplierListDataBind();
            panelPolicyAdd.Visible = false;
            panelRateAdd.Visible = false;
            panelMarket.Visible = false;
            ConditionListDatabind();
            screenBlock.Visible = true;
        }

        public void dropoptionDataBind()
        {
            Option cOption = new Option();
            short shrSupplierId = short.Parse(dropProductSup.SelectedValue);
            dropoption.DataSource = cOption.GetProductOptionByCurrentSupplierANDProductId_All_Except_Gala(shrSupplierId, int.Parse(this.qProductId));
            dropoption.DataTextField = "Title";
            dropoption.DataValueField = "OptionID";

            dropoption.DataBind();
            if (!string.IsNullOrEmpty(this.qConditionId))
            {
                int intConditionId = int.Parse(this.qConditionId);
                ProductOptionCondition cProductoptionCondition = new ProductOptionCondition();
                cProductoptionCondition.getConditionById(intConditionId);

                
                dropoption.SelectedValue = cProductoptionCondition.OptionId.ToString();

            }
            //ListItem NewList = new ListItem("All Option", "0");
            //dropoption.Items.Insert(0, NewList);
        }

        public void dropoption_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GvSupplierListDataBind();
            panelPolicyAdd.Visible = false;
            panelRateAdd.Visible = false;
            panelMarket.Visible = false;
            ConditionListDatabindDropOptionChange();
            screenBlock.Visible = true;
        }

        //============== CONDITION LIST ============================

        public void ConditionListDatabindDropOptionChange()
        {
            ProductOptionCondition cOptionCondition = new ProductOptionCondition();
            int intOptionId = int.Parse(dropoption.SelectedValue);

            //if (!string.IsNullOrEmpty(Request.QueryString["oid"]))
            //{
            //    intOptionId = int.Parse(Request.QueryString["oid"]);
            //}

            ConditionList.DataSource = cOptionCondition.getConditionList(intOptionId);
            ConditionList.DataBind();

        }
        public void ConditionListDatabind()
        {
            ProductOptionCondition cOptionCondition = new ProductOptionCondition();
            int intOptionId = int.Parse(dropoption.SelectedValue);

            if (!string.IsNullOrEmpty(Request.QueryString["oid"]))
            {
                intOptionId = int.Parse(Request.QueryString["oid"]);
            }

            ConditionList.DataSource = cOptionCondition.getConditionList(intOptionId);
            ConditionList.DataBind();
            
        }

        public void ConditionList_OnRowDatabound( object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lnkTitle = e.Row.Cells[0].FindControl("hptitle") as HyperLink;
                int intConditionId = (int)DataBinder.Eval(e.Row.DataItem,"ConditionId");
                int intOptionId = (int)DataBinder.Eval(e.Row.DataItem,"OptionId");
                lnkTitle.NavigateUrl = "~/admin/productOption/product_option_rate.aspx?cdid=" + intConditionId + "&oid=" + intOptionId + this.AppendCurrentQueryString();
                bool bolStatus = (bool)DataBinder.Eval(e.Row.DataItem, "Status");
                if (bolStatus == false)
                {
                    lnkTitle.CssClass = "product_announcement_left_Gv_item_dis";
                    
                }
            }
        }

        //================= CONDITION INSERT BOX ====================
        public void btninsertSave_OnClick(object sender, EventArgs e)
        {
            ProductOptionCondition.InsertQuickCondition(txtConditionInsert.Text, int.Parse(dropoption.SelectedValue));
            
            txtConditionInsert.Text = string.Empty;
            ConditionListDatabind();
        }

        

        public void GvSupplierListDataBind()
        {
            Option cOption = new Option();
            int intOptionId = 0;
            if (!string.IsNullOrEmpty(dropoption.SelectedValue))
            {
                intOptionId = int.Parse(dropoption.SelectedValue);
            }
            GvSupplierList.DataSource = Option.getSupplierListByOption(intOptionId);
            GvSupplierList.DataBind();
        }

        public void GvSupplierList_OnRowDataBound(object sender,  GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               
            }
        }
       

        //========== Condition Information =============
        //public void ConditionDataBind()
           
        public void ConditionInformantionDataBind()
        {
            dropBreakfast.DataSource = this.dicGetNumberstart0(20);
            dropBreakfast.DataTextField = "Value";
            dropBreakfast.DataValueField = "Key";
            dropBreakfast.DataBind();

            dropNumaDult.DataSource = this.dicGetNumberstart0(20);
            dropNumaDult.DataTextField = "Value";
            dropNumaDult.DataValueField = "Key";
            dropNumaDult.DataBind();

            dropNumchild.DataSource = this.dicGetNumberstart0(20);
            dropNumchild.DataTextField = "Value";
            dropNumchild.DataValueField = "Key";
            dropNumchild.DataBind();

            dropNumExpired.DataSource = this.dicGetNumberstart0(20);
            dropNumExpired.DataTextField = "Value";
            dropNumExpired.DataValueField = "Key";
            dropNumExpired.DataBind();

            if (!string.IsNullOrEmpty(this.qConditionId))
            {
                int intConditionId = int.Parse(this.qConditionId);
                ProductOptionCondition cProductoptionCondition = new ProductOptionCondition();
                cProductoptionCondition = cProductoptionCondition.getConditionById(intConditionId);


                txtTitle.Text = cProductoptionCondition.Title;
                dropBreakfast.SelectedValue = cProductoptionCondition.Breakfast.ToString();
                dropNumaDult.SelectedValue = cProductoptionCondition.NumAdult.ToString();
                dropNumchild.SelectedValue = cProductoptionCondition.NumChildren.ToString();
                dropNumExpired.SelectedValue = cProductoptionCondition.NumExtra.ToString();

                if (cProductoptionCondition.Status)
                {
                    radioStatusEnable.Checked = true;
                    radioStatusDisable.Checked = false;
                }
                else
                {
                    radioStatusEnable.Checked = false;
                    radioStatusDisable.Checked = true;
                }

                if (cProductoptionCondition.HasTransfer)
                {
                    radioTransferYes.Checked = true;
                    radioTransferNo.Checked = false;
                }
                else
                {
                    radioTransferYes.Checked = false;
                    radioTransferNo.Checked = true;
                }

                lblHeadtitle.Text = cProductoptionCondition.Title;
                dropoption.SelectedValue = cProductoptionCondition.OptionId.ToString();
        
            }
        }

        public void ConditionSave_Onclick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.qConditionId))
            {
                int intConditionId = int.Parse(this.qConditionId);
                ProductOptionCondition cProductoptionCondition = new ProductOptionCondition();
               cProductoptionCondition =  cProductoptionCondition.getConditionById(intConditionId);
                 cProductoptionCondition.Title = txtTitle.Text ;
                 cProductoptionCondition.Breakfast= byte.Parse( dropBreakfast.SelectedValue) ;
                 cProductoptionCondition.NumAdult = byte.Parse(dropNumaDult.SelectedValue) ;
                cProductoptionCondition.NumChildren= byte.Parse(dropNumchild.SelectedValue) ;
                 cProductoptionCondition.NumExtra = byte.Parse(dropNumExpired.SelectedValue) ;

                 if (radioStatusEnable.Checked )
                 {
                     cProductoptionCondition.Status = true;
                 }

                 if(radioStatusDisable.Checked)
                 {
                     cProductoptionCondition.Status = false;
                 }


                 if (radioTransferYes.Checked)
                 {
                     cProductoptionCondition.HasTransfer = true;
                 }

                 if (radioTransferNo.Checked)
                 {
                     cProductoptionCondition.HasTransfer = false;
                 }

                 cProductoptionCondition.Update();

            }

            Response.Redirect(Request.Url.ToString());
        }
        //================ Condition Rate Market ================

        public void panelMarketDataBind()
        {
            if (!string.IsNullOrEmpty(this.qConditionId))
            {
                dropMarketDataBind();
                
                hplmarketManage.NavigateUrl ="~/admin/product/country_market.aspx?cdid"+ this.qConditionId + "&pdcid=" + this.qProductCat + "&pid=" + this.qProductId;
            }
        }

        public void dropMarketDataBind()
        {
            if (!string.IsNullOrEmpty(this.qConditionId))
            {
                dropMarket.DataSource = CountryMarket.getMarketALL();
                dropMarket.DataTextField = "Value";
                dropMarket.DataValueField = "Key";
                dropMarket.DataBind();

                int intConditionId = int.Parse(this.qConditionId);
                ProductOptionCondition cProductoptionCondition = new ProductOptionCondition();
                cProductoptionCondition = cProductoptionCondition.getConditionById(intConditionId);

                dropMarket.SelectedValue = cProductoptionCondition.MarketId.ToString();
            }

        }

        public void btmarketSave_Onclick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.qConditionId))
            {
                byte bytMarketId= byte.Parse(dropMarket.SelectedValue);
                int intConditionId = int.Parse(this.qConditionId);
                ProductOptionCondition cProductoptionCondition = new ProductOptionCondition();
                cProductoptionCondition = cProductoptionCondition.getConditionById(intConditionId);
                cProductoptionCondition.MarketId = bytMarketId;
                cProductoptionCondition.Update();
            }

            Response.Redirect(Request.Url.ToString());
        }
        //========= Condition Policy ============================

        public void ConditionPolicyDataBind()
        {
            if (!string.IsNullOrEmpty(this.qConditionId))
            {
                dropPolicyCatDataBind();
                chkPolicyDataBind();
                GvPolicyUsedDataBind();
            }
            
        }
        public void dropPolicyCatDataBind()
        {
            dropPolicyCat.DataSource = ProductPolicyAdmin.getPolicyCategoryall();
            dropPolicyCat.DataTextField = "Value";
            dropPolicyCat.DataValueField = "Key";
            dropPolicyCat.DataBind();
        }

        public void dropPolicyCat_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            chkPolicyDataBind();
            
        }

        public void chkPolicyDataBind()
        {
            ProductPolicyAdmin cProductPolicy = new ProductPolicyAdmin();
            chkPolicy.DataSource = cProductPolicy.GetProductPolicyByProductANDCatIDANDNotDuplicatCurrent(int.Parse(this.qProductId), byte.Parse(dropPolicyCat.SelectedValue), int.Parse(this.qConditionId));
            chkPolicy.DataTextField = "Title";
            chkPolicy.DataValueField = "PolicyID";
            chkPolicy.DataBind();
        }
        

        public void btnPolicyAdd_Onclick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.qConditionId))
            {
                int intConditionId = int.Parse(this.qConditionId);
                
                foreach (ListItem chkItem in chkPolicy.Items)
                {
                    if (chkItem.Selected)
                    {
                        ProductOptionConditionPolicy.InsertOptionConditionPolicy(intConditionId, int.Parse(chkItem.Value));
                    }
                }


                chkPolicyDataBind();
                GvPolicyUsedDataBind();
            }
        }

        public void GvPolicyUsedDataBind()
        {
            if (!string.IsNullOrEmpty(this.qConditionId))
            {
                int intConditionId = int.Parse(this.qConditionId);
                ProductOptionConditionPolicy cConditionPolicy = new ProductOptionConditionPolicy();
                GvPolicyUsed.DataSource = cConditionPolicy.getOptionContionPolicyByConditionId(intConditionId);
                GvPolicyUsed.DataBind();
            }
            
        }

        public void btnDel_Onclick(object sender, EventArgs e)
        {
            LinkButton btnL = (LinkButton)sender;


            if (btnL.CommandName == "polDel")
            {
                int intPolicyId = int.Parse(btnL.CommandArgument);
                int intConditionId = int.Parse(this.qConditionId);

                bool delete = ProductOptionConditionPolicy.DeleteOptionConditionPolicy(intConditionId, intPolicyId);
            }
            chkPolicyDataBind();
            GvPolicyUsedDataBind();
            //Response.Redirect(Request.Url.ToString());
        }

        //===================== Rate Add ===============================
        public void panelRateAddDataBind()
        {
            if (!string.IsNullOrEmpty(this.qConditionId))
            {
                dropSupplierDataBind();

                GVProductPeriodDataBind();

                GVRatePeriodCurrentDataBind();
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
            GVProductPeriodDataBind();
            GVRatePeriodCurrentDataBind();
        }

        public void GVProductPeriodDataBind()
        {

            //Response.End();
            ProductOption.ProductOptionPeriod cProductPeriod = new ProductOptionPeriod();
            int intConditionId = int.Parse(this.qConditionId);


            //Response.Write(int.Parse((this.Page as Hotels2BasePage).qProductId ) + "<br/>");
            //Response.Write(short.Parse(dropSupplier.SelectedValue) + "<br/>");
            //Response.Write(intConditionId + "<br/>");
            //Response.Write(cProductPeriod.getProductPeriodListEffectiveCondition(int.Parse((this.Page as Hotels2BasePage).qProductId),
            //    short.Parse(dropSupplier.SelectedValue), intConditionId).Count + "<br/>");
            //Response.End();
            //Response.Write(GVProductPeriod.Rows.Count);
            //Response.End();

            //if (GVProductPeriod.Rows.Count == 0)
            //{
            //   tbnSaveRate.Visible = false;
            //}
            if (cProductPeriod.getProductPeriodListEffectiveCondition(int.Parse((this.Page as Hotels2BasePage).qProductId),
                short.Parse(dropSupplier.SelectedValue), intConditionId).Count == 0)
            {


                tbnSaveRate.Visible = false;

            }
            else
            { tbnSaveRate.Visible = true; }

            GVProductPeriod.DataSource = cProductPeriod.getProductPeriodListEffectiveCondition(int.Parse((this.Page as Hotels2BasePage).qProductId),
                short.Parse(dropSupplier.SelectedValue), intConditionId);
            GVProductPeriod.DataBind();
        }
        public void GVProductPeriod_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DateTime dDateStart = (DateTime)DataBinder.Eval(e.Row.DataItem, "DateStart");
                DateTime dDateEnd = (DateTime)DataBinder.Eval(e.Row.DataItem, "DateEnd");

                
            }
        }

        public void tbnSaveRate_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.qConditionId))
            {
                int intConditionId = int.Parse(this.qConditionId);
                foreach (GridViewRow Gvrow in GVProductPeriod.Rows)
                {
                    CheckBox chkBox = GVProductPeriod.Rows[Gvrow.RowIndex].Cells[0].FindControl("chkPeriod") as CheckBox;
                    TextBox txtPrice = GVProductPeriod.Rows[Gvrow.RowIndex].Cells[3].FindControl("txtPrice") as TextBox;
                    TextBox txtOwn = GVProductPeriod.Rows[Gvrow.RowIndex].Cells[4].FindControl("txtOwn") as TextBox;
                    TextBox txtRack = GVProductPeriod.Rows[Gvrow.RowIndex].Cells[5].FindControl("txtRack") as TextBox;

                    decimal ratePrice = decimal.Parse(txtPrice.Text);
                    decimal rateOwn = decimal.Parse(txtOwn.Text);
                    decimal rateRack = decimal.Parse(txtRack.Text);

                    int intPeriodId = (int)GVProductPeriod.DataKeys[Gvrow.RowIndex].Value;

                    if (chkBox.Checked)
                    {
                        if (ratePrice >= rateOwn)
                        {
                            ProductOptionConditionPrice.InsertRateConditionPrice(intConditionId, intPeriodId, ratePrice, rateOwn, rateRack);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>Checkrate();</script>", false);
                            break;
                        }
                    }

                }
                GVProductPeriodDataBind();
                GVRatePeriodCurrentDataBind();
            }

           
        }



        public void GVRatePeriodCurrentDataBind()
        {
            if (!string.IsNullOrEmpty(this.qConditionId))
            {
                int intConditionId = int.Parse(this.qConditionId);
                short shrSupplierId = short.Parse(dropSupplier.SelectedValue);

                //Response.Write(shrSupplierId);
                //Response.End();
                ProductOptionConditionPrice cConditionPrice = new ProductOptionConditionPrice();
                GVRatePeriodCurrent.DataSource = cConditionPrice.getRateCurrentByConditionId(intConditionId,shrSupplierId);
                GVRatePeriodCurrent.DataBind();
            }
        }

        public void GVRatePeriodCurrent_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (!string.IsNullOrEmpty(this.qProductCat))
            {
                int intProductCat = int.Parse(this.qProductCat);
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (intProductCat != 38 && intProductCat != 34 && intProductCat != 36)
                    {
                        e.Row.Cells[7].Visible = false;
                    }
                }

                if (e.Row.RowType == DataControlRowType.DataRow )
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
                    if (intProductCat != 38 && intProductCat != 34 && intProductCat != 36)
                    {
                        e.Row.Cells[7].Visible = false;
                        

                    }
                    else
                    {
                        DropDownList dropMaxInsert = e.Row.Cells[7].FindControl("dropMaxInsert") as DropDownList;
                        DropDownList dropMinInsert = e.Row.Cells[7].FindControl("dropMinInsert") as DropDownList;
                        TextBox txtAmountInsert = e.Row.Cells[7].FindControl("txtAmountInsert") as TextBox;

                        

                        dropMaxInsert.DataSource = this.dicGetNumber(20);
                        dropMaxInsert.DataTextField = "Value";
                        dropMaxInsert.DataValueField = "Key";
                        dropMaxInsert.DataBind();

                        dropMinInsert.DataSource = this.dicGetNumber(20);
                        dropMinInsert.DataTextField = "Value";
                        dropMinInsert.DataValueField = "Key";
                        dropMinInsert.DataBind();

                        ListItem newList = new ListItem("Infinity", "100");
                        dropMaxInsert.Items.Insert(0, newList);

                        GridView GVSupQuan = e.Row.Cells[7].FindControl("GVSupQuan") as GridView;
                        ProductOptionSupplementQuantity SupQuan = new ProductOptionSupplementQuantity();
                        int intConditionID = int.Parse(this.qConditionId);
                        short shrSupplierID = short.Parse(dropSupplier.SelectedValue);
                        int PeriodId = (int)DataBinder.Eval(e.Row.DataItem,"PeriodId");
                        GVSupQuan.DataSource = SupQuan.GetSupQuantity(intConditionID, shrSupplierID, PeriodId);
                        GVSupQuan.DataBind();

                    }
                }
               


            }
        }

        public void GVSupQuan_OnrowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList dropMin = e.Row.Cells[0].FindControl("dropMin") as DropDownList;
                DropDownList dropMax = e.Row.Cells[1].FindControl("dropMax") as DropDownList;
                dropMin.DataSource = this.dicGetNumber(20);
                dropMin.DataTextField = "Value";
                dropMin.DataValueField = "Key";
                dropMin.DataBind();
                dropMax.DataSource = this.dicGetNumber(20);
                dropMax.DataTextField = "Value";
                dropMax.DataValueField = "Key";
                dropMax.DataBind();

                ListItem newList = new ListItem("Infinity", "100");
                dropMax.Items.Insert(0, newList);
                
                short QuanMax = (short)DataBinder.Eval(e.Row.DataItem, "QuanMax");
                short QuanMin = (short)DataBinder.Eval(e.Row.DataItem, "QuanMin");
                dropMin.SelectedValue = QuanMax.ToString();
                dropMax.SelectedValue = QuanMin.ToString();
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
                            int intConditionID = int.Parse(this.qConditionId);
                            short shrSupplierID = short.Parse(dropSupplier.SelectedValue);
                            int PeriodId = int.Parse(Argument[0]);
                            TextBox txtPrice = GVRatePeriodCurrent.Rows[Item.RowIndex].Cells[3].FindControl("txtPrice") as TextBox;
                            TextBox txtOwn = GVRatePeriodCurrent.Rows[Item.RowIndex].Cells[4].FindControl("txtOwn") as TextBox;
                            TextBox txtRack = GVRatePeriodCurrent.Rows[Item.RowIndex].Cells[5].FindControl("txtRack") as TextBox;
                            decimal ratePrice = decimal.Parse(txtPrice.Text);
                            decimal rateOwn = decimal.Parse(txtOwn.Text);
                            decimal rateRack = decimal.Parse(txtRack.Text);
                            if (ratePrice >= rateOwn)
                            {
                                ProductOptionConditionPrice.UpdateConditionPrice(intConditionID, PeriodId, ratePrice, rateOwn, rateRack);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>Checkrate();</script>", false);
                                break;
                            }
                        }
                    }
                }
            }

            if (btn.CommandName == "addquan")
            {
                
                string[] Argument = btn.CommandArgument.Split(',');
                
                int RowIndex = int.Parse(Argument[1]);
                foreach (GridViewRow Item in GVRatePeriodCurrent.Rows)
                {
                    if (Item.RowType == DataControlRowType.DataRow)
                    {
                        if (Item.RowIndex == RowIndex)
                        {
                            int intConditionID = int.Parse(this.qConditionId);
                            short shrSupplierID = short.Parse(dropSupplier.SelectedValue);
                            int PeriodId = int.Parse(Argument[0]);

                            DropDownList dropMaxInsert = GVRatePeriodCurrent.Rows[Item.RowIndex].Cells[7].FindControl("dropMaxInsert") as DropDownList;
                            DropDownList dropMinInsert = GVRatePeriodCurrent.Rows[Item.RowIndex].Cells[7].FindControl("dropMinInsert") as DropDownList;
                            TextBox txtAmountInsert = GVRatePeriodCurrent.Rows[Item.RowIndex].Cells[7].FindControl("txtAmountInsert") as TextBox;
                            ProductOptionSupplementQuantity.InsertSupQuan(intConditionID, shrSupplierID, PeriodId, short.Parse(dropMinInsert.SelectedValue), short.Parse(dropMaxInsert.SelectedValue), decimal.Parse(txtAmountInsert.Text));

                            GridView GVSupQuan = GVRatePeriodCurrent.Rows[Item.RowIndex].Cells[7].FindControl("GVSupQuan") as GridView;
                            ProductOptionSupplementQuantity SupQuan = new ProductOptionSupplementQuantity();
                            
                            GVSupQuan.DataSource = SupQuan.GetSupQuantity(intConditionID, shrSupplierID, PeriodId);
                            GVSupQuan.DataBind();
                        }
                    }
                }
                
            }

            //Response.Redirect(Request.Url.ToString() + "#current_rate");
            GVRatePeriodCurrentDataBind();

        }

        public void btnSupQuanEdit_Onclick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.CommandName == "SupQuanEdit")
            {
                string[] Argument = btn.CommandArgument.Split(',');
                int RowIndex = int.Parse(Argument[1]);
                
                int intSupquan = int.Parse(Argument[0]);
                foreach (GridViewRow ParentItem in GVRatePeriodCurrent.Rows)
                {
                    GridView GvChild = GVRatePeriodCurrent.Rows[ParentItem.RowIndex].Cells[7].FindControl("GVSupQuan") as GridView;
                    int intConditionID = int.Parse(this.qConditionId);
                    short shrSupplierID = short.Parse(dropSupplier.SelectedValue);
                    int PeriodId = (int)GVRatePeriodCurrent.DataKeys[ParentItem.RowIndex].Value;
                    foreach (GridViewRow ChildItem in GvChild.Rows)
                    {
                        if (ChildItem.RowType == DataControlRowType.DataRow)
                        {
                            if (ChildItem.RowIndex == RowIndex)
                            {
                                

                                DropDownList dropMax = GvChild.Rows[ChildItem.RowIndex].Cells[2].FindControl("dropMax") as DropDownList;
                                DropDownList dropMin = GvChild.Rows[ChildItem.RowIndex].Cells[1].FindControl("dropMin") as DropDownList;
                                TextBox txtAmount = GvChild.Rows[ChildItem.RowIndex].Cells[3].FindControl("txtSup") as TextBox;

                                bool Update = ProductOptionSupplementQuantity.UpdateSupQuan(intSupquan, short.Parse(dropMin.SelectedValue), short.Parse(dropMax.SelectedValue), decimal.Parse(txtAmount.Text));
                             }
                        }
                    }
                    
                    ProductOptionSupplementQuantity SupQuan = new ProductOptionSupplementQuantity();
                    GvChild.DataSource = SupQuan.GetSupQuantity(intConditionID, shrSupplierID, PeriodId);
                    GvChild.DataBind();
                }
            }
        }

        public void lnkStatus_Onclick(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string[] argument = btn.CommandArgument.Split(',');
            int ConditionId= int.Parse(argument[1]);
            int PeriodId = int.Parse(argument[0]);
            ProductOptionConditionPrice PricePeriod = new ProductOptionConditionPrice();
            //Response.Write(ConditionId + "--" + PeriodId);
            //Response.End();
            PricePeriod = PricePeriod.GetConditionPriceByConditionIdAndInPeriodID(ConditionId,PeriodId);
            if(PricePeriod.Status)
            {

                
                PricePeriod.UpdateRatePeriodStatus(ConditionId,PeriodId,false);
            }
            else
            {
                PricePeriod.UpdateRatePeriodStatus(ConditionId,PeriodId,true);
            }
            
            GVRatePeriodCurrentDataBind();
        }
        //public void GVProductPeriod_OnRowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "periodupdate")
        //    {
        //        //int intPeriodId = int.Parse(e.CommandArgument.ToString());

        //        string[] param = e.CommandArgument.ToString().Split(Convert.ToChar("&"));
        //        short Id = Convert.ToInt16(param[0]);
        //        int index = Convert.ToInt32(param[1]);

        //        ProductOptionPeriod cPeriod = new ProductOptionPeriod();

        //    }

        //    GVProductPeriodDataBind();
        //}


    }
}