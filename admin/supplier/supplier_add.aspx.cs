using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hotels2thailand.UI
{

    public partial class admin_supplier_supplier_add : Hotels2BasePage
    {
        public string strQueryString
        {
            get
            {
                return Request.QueryString["supid"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(this.strQueryString))
            {
                FvSupplierAdd.DefaultMode = FormViewMode.Insert;
                panelSupmenuSupplier.Visible = false;
            }
            else
            {
                FvSupplierAdd.DefaultMode = FormViewMode.Edit;
                linkContact.NavigateUrl = "supplier_contact.aspx?supid=" + this.strQueryString;
                LinkAccount.NavigateUrl = "supplier_account_list.aspx?supid=" + this.strQueryString;
                linksuppolicy.NavigateUrl = "supplier_payment_policy.aspx?supid=" + this.strQueryString;
                //DropDownList dropDownCategoryId = (DropDownList)FvSupplierAdd.FindControl("ddlCategoryId");
                //Response.Write(dropDownCategoryId.SelectedValue);
            }  
        }

        public void FvSupplierAdd_OndataBound(object sender, EventArgs e)
        {
            if (FvSupplierAdd.CurrentMode == FormViewMode.Edit)
            {
                bool bolInternetFree = (bool)DataBinder.GetPropertyValue(FvSupplierAdd.DataItem, "Status");

                RadioButton RadioEnable = FvSupplierAdd.FindControl("radioEnable") as RadioButton;
                RadioButton RadioDisable = FvSupplierAdd.FindControl("radioDisable") as RadioButton;
                if (bolInternetFree)
                {
                    RadioEnable.Checked = true;
                    RadioDisable.Checked = false;
                }
                else
                {
                    RadioEnable.Checked = false;
                    RadioDisable.Checked = true;
                }
            }
        }

        public void FvSupplierAdd_OnItemInserting(object sender, FormViewInsertEventArgs e)
        {
            RadioButton RadioEnable = FvSupplierAdd.FindControl("radioEnable") as RadioButton;
            RadioButton RadioDisable = FvSupplierAdd.FindControl("radioDisable") as RadioButton;

            if (RadioEnable.Checked)
            {
                e.Values["Status"] = true;
            }
            if (RadioDisable.Checked)
            {
                e.Values["Status"] = false;
            }
        }

        public void FvSupplierAdd_OnItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            RadioButton RadioEnable = FvSupplierAdd.FindControl("radioEnable") as RadioButton;
            RadioButton RadioDisable = FvSupplierAdd.FindControl("radioDisable") as RadioButton;

            if (RadioEnable.Checked)
            {
                e.NewValues["Status"] = true;
            }
            if (RadioDisable.Checked)
            {
                e.NewValues["Status"] = false;
            }
        }

        protected void FvSupplierAdd_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            if (FvSupplierAdd.CurrentMode == FormViewMode.Insert)
            {
                FvSupplierAdd.ChangeMode(FormViewMode.Edit);
            }

            //Response.Redirect("supplier_add.aspx" + ObjectDataSource1
        }

        protected void ObjectDataSource1_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            int id = (int)e.ReturnValue;
            Response.Redirect("supplier_add.aspx?supid=" + id);
            //if (string.IsNullOrEmpty(Request.QueryString["wizard"]))
            //{
                

            //}
            //else
            //{

            //    Response.Redirect(Request.UrlReferrer.ToString());
            //}
        }

        protected void ObjectDataSource1_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            //Response.Write("inserting");
            //Response.End();
        }

        protected void FvSupplierAdd_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            if (e.CommandName == "Cancel")
            {
                Response.Redirect("supplier_list.aspx");
            }
                
            
        }
    }
}