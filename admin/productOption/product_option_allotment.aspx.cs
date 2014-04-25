using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Hotels2thailand.Production;
using Hotels2thailand.ProductOption;

namespace Hotels2thailand.UI
{
    public partial class admin_product_option_allotment : Hotels2BasePage
    {

        public DateTime PublicdDateStartEdit
        {
            get { return this.dateRangeEdit.GetDatetStart; }
        }

        public DateTime PublicdDateEndEdit
        {
            get { return this.dateRangeEdit.GetDatetEnd; ; }
        }

        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.Page.IsPostBack)
            {
                Product cProduct = new Product();
                cProduct = cProduct.GetProductById(int.Parse(this.qProductId));
                Destitle.Text = cProduct.DestinationTitle;
                txthead.Text = cProduct.Title;

                dropDownSupplierListDataBind();
                dropDownSupplierList.SelectedValue = cProduct.SupplierPrice.ToString();
                panelAllotaddEditDataBind();
                GvRomAllotListDAtaBind();

            }
            
            
        }

        public void dropDownSupplierListDataBind()
        {
            ProductSupplier cProductSUp = new ProductSupplier();
            dropDownSupplierList.DataSource = cProductSUp.getSupplierListByProductIdAndActiveOnly(int.Parse(this.qProductId));
            dropDownSupplierList.DataTextField = "SupplierTitle";
            dropDownSupplierList.DataValueField = "SupplierID";
            dropDownSupplierList.DataBind();
        }

        public void dropDownSupplierList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            panelAllotaddEditDataBind();
            GvRomAllotListDAtaBind();
        }

        

        //Panel Add Edit Allotment ========================================================
        public void panelAllotaddEditDataBind()
        {
            chkOptionListDataBind();
            ProductOptionListDataBind();
            dropNumRoomDataBind();
            dropcutoffDataBind();
        }

        public void dropcutoffDataBind()
        {
            dropcutoff.DataSource = this.dicGetNumberstart0(90);
            dropcutoff.DataTextField = "Value";
            dropcutoff.DataValueField = "Key";
            dropcutoff.DataBind();
        }
        public void dropNumRoomDataBind()
        {
            dropNumRoom.DataSource = this.dicGetNumber(10);
            dropNumRoom.DataTextField = "Value";
            dropNumRoom.DataValueField = "Key";
            dropNumRoom.DataBind();

        }

        public void ProductOptionListDataBind()
        {
            Option cOption = new Option();
            short shrSupplierId = short.Parse(dropDownSupplierList.SelectedValue);
            ProductOptionList.DataSource = cOption.GetProductOptionByCurrentSupplierANDProductId(shrSupplierId, int.Parse(this.qProductId));
            ProductOptionList.DataTextField = "Title";
            ProductOptionList.DataValueField = "OptionID";

            ProductOptionList.DataBind();
            ListItem NewList = new ListItem("All Option", "0");
            ProductOptionList.Items.Insert(0, NewList);
        }

        public void chkOptionListDataBind()
        {
            short shrSupplierId = short.Parse(dropDownSupplierList.SelectedValue);
            Option cOption = new Option();
            chkOptionList.DataSource = cOption.GetProductOptionByCurrentSupplierANDProductId(shrSupplierId, int.Parse(this.qProductId));
            chkOptionList.DataTextField = "Title";
            chkOptionList.DataValueField = "OptionID";
            chkOptionList.DataBind();

        }

        public void btnAllotmentSave_OnClick(object sender, EventArgs e)
        {
            short shrSupplierId = short.Parse(dropDownSupplierList.SelectedValue);
            byte bytCutoff = byte.Parse(dropcutoff.SelectedValue);
            byte bytNumRoom = byte.Parse(dropNumRoom.SelectedValue);
            DateTime dDateStart = DatePicker.GetDatetStart;
            DateTime dDateEnd = DatePicker.GetDatetEnd;
            bool Status = true;
            if (raioStatus.SelectedValue == "0")
                Status = false;

            Allotment cAllotment = new Allotment();
            int count = 0;
            foreach (ListItem item in chkOptionList.Items)
            {
                if (item.Selected)
                {
                    count = count + 1;
                }
            }

            if (count > 0)
            {
                foreach (ListItem checkeditem in chkOptionList.Items)
                {
                    if (checkeditem.Selected)
                    {
                        cAllotment.InsertNewallotandUpdateBydateRange(shrSupplierId, int.Parse(checkeditem.Value), dDateStart, dDateEnd, bytCutoff, 1, bytNumRoom, Status);
                    }
                }
            }
            else
            {
                
                int intOptionSelected = int.Parse(ProductOptionList.SelectedValue);
                if (intOptionSelected == 0)
                {
                    Option cOption = new Option();
                    foreach (Option cOptionItem in cOption.GetProductOptionByCurrentSupplierANDProductId(shrSupplierId,int.Parse(this.qProductId)))
                    {
                        cAllotment.InsertNewallotandUpdateBydateRange(shrSupplierId, cOptionItem.OptionID, dDateStart, dDateEnd, bytCutoff, 1, bytNumRoom, Status);
                    }
                 }
                else
                {
                    
                    cAllotment.InsertNewallotandUpdateBydateRange(shrSupplierId, intOptionSelected, dDateStart, dDateEnd, bytCutoff, 1, bytNumRoom, Status);
                    
                }
            }
            Response.Redirect(Request.Url.ToString());
            //GvRomAllotListDAtaBind();
            
        }

        //panelAllotRoom List ================================================

        public void GvRomAllotListDAtaBind()
        {
            if(!string.IsNullOrEmpty(this.qProductId))
            {
                int intProductId = int.Parse(this.qProductId);
                short shrSupplierID = short.Parse(dropDownSupplierList.SelectedValue);
                Allotment cAllotment = new Allotment();
                GvRomAllotList.DataSource = cAllotment.getDicActiveOptionAllotment(intProductId, shrSupplierID);
                GvRomAllotList.DataBind();
            }
        }

        public void GvRomAllotList_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                short shrSupplierId = short.Parse(dropDownSupplierList.SelectedValue);
                int intOptiondId = (int)DataBinder.Eval(e.Row.DataItem, "Key");
                Label lblDateStart = e.Row.Cells[2].FindControl("lblDateStart") as Label;
                Label lbldateEnd = e.Row.Cells[2].FindControl("lbldateEnd") as Label;
                //HyperLink hlnormalEdit = e.Row.Cells[3].FindControl("hlnormalEdit") as HyperLink;
                //hlnormalEdit.NavigateUrl = hlnormalEdit.NavigateUrl + "&supid=" + dropDownSupplierList.SelectedValue;
                Allotment cAllotment = new Allotment();

                ArrayList arr = cAllotment.getDateRangeByOptionId(intOptiondId, shrSupplierId);

                lblDateStart.Text = Convert.ToDateTime(arr[0]).ToString("d-MMM-yyyy");
                lbldateEnd.Text = Convert.ToDateTime(arr[1]).ToString("d-MMM-yyyy");
                //e.Row.Attributes.Add("onclick", "javascript:getDivDateRange('dateRange" + intOptiondId.ToString() + "','" + intOptiondId.ToString() + "');");
                
                //ScriptManager.RegisterStartupScript(this, Page.GetType(), intOptiondId.ToString(), "<script>getDivDateRange('dateRange" + intOptiondId.ToString() + "','" + intOptiondId.ToString() + "');</script>", false);
                
            }
        }

        public void btnNormalEdit_OnClick(object sender, EventArgs e)
        {
            
            StringBuilder strOption = new StringBuilder();
            int count = 0;
            foreach (GridViewRow GvRow in GvRomAllotList.Rows)
            {
                if (GvRow.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = GvRomAllotList.Rows[GvRow.RowIndex].Cells[0].FindControl("chkEdit") as CheckBox;
                    int intOptionId = (int)GvRomAllotList.DataKeys[GvRow.RowIndex].Value;
                    if (chk.Checked)
                    {
                        strOption.Append(intOptionId + ",");
                        count = count + 1;
                    }
                }
                
            }
            string dateStart = dateRangeEdit.GetDatetStart.Hotels2DateToSQlStringNoSingleCode();
            string dateend = dateRangeEdit.GetDatetEnd.Hotels2DateToSQlStringNoSingleCode();
            if (count > 0)
            {
                string arrOptionIdEncod = strOption.ToString().Hotels2EncryptedData();

                Response.Redirect("product_option_allotment_edit.aspx?oid=" + arrOptionIdEncod + "&supid=" + dropDownSupplierList.SelectedValue + "&ds=" + dateStart +"&de="+ dateend + this.AppendCurrentQueryString());
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(Page.GetType(), null, "<script>Alertlightbox('No Period Match');</script>", false);
                //this.Page.ClientScript.RegisterStartupScript(Page.GetType(), null, "<script>alert('TEST')</script>", false);
            }
        }
        

    }
}