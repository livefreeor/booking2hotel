using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Suppliers;

namespace Hotels2thailand.UI
{
    
    public partial class admin_supplier_supplier_account_list : Hotels2BasePage
    {
        public string strQueryString
        {
            get
            {
                return Request.QueryString["supid"];
            }
        }

        public string strAccQueryString
        {
            get
            {
                return Request.QueryString["acid"];
            }
        }

        protected void supplierCreate_Onclick(object sender, EventArgs e)
        {
            if (FormSupAccAdd.Visible == false)
            {
                FormSupAccAdd.Visible = true;
            }
        }

       protected void Page_Load(object sender, EventArgs e)
        {
            // Show title Supplier
            Supplier cSupplier = new Supplier();
            lblhead.Text = cSupplier.getSupplierById(short.Parse(this.qSupplierId)).SupplierTitle;

            if (string.IsNullOrEmpty(this.strAccQueryString))
            {
                FormSupAccAdd.Visible = false;
            }
            else
            {
                FormSupAccAdd.Visible = true;
            }
            
            if (!string.IsNullOrEmpty(this.strQueryString) && string.IsNullOrEmpty(this.strAccQueryString))
            {
                FormSupAccAdd.DefaultMode = FormViewMode.Insert;
            }
            else
            {
                FormSupAccAdd.DefaultMode = FormViewMode.Edit;
            }  
        }

       protected void FormSupAccAdd_OnCommand(object sender, FormViewCommandEventArgs e)
       {
           if (e.CommandName == "UpdateCancel")
           {
                Server.Transfer("supplier_account_list.aspx?supid=" + Request.QueryString["supid"]);
           }
       }

        protected void gridSupplierAccount_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "updateflagDefault")
            {
                SupplierAccount clSupplierAcc = new SupplierAccount();
                clSupplierAcc = clSupplierAcc.getSupplierAccountById(short.Parse(e.CommandArgument.ToString()));
                if (clSupplierAcc.getSupplierAccountAllBySupplierIDAndFlagDefaultTrue(short.Parse(this.strQueryString)).Count > 0)
                {
                    foreach (SupplierAccount item in clSupplierAcc.getSupplierAccountAllBySupplierIDAndFlagDefaultTrue(short.Parse(this.strQueryString)))
                    {
                        if (item.AccountId != short.Parse(e.CommandArgument.ToString()))
                        {
                            SupplierAccount clSupplierAccs = new SupplierAccount();
                            clSupplierAccs = clSupplierAccs.getSupplierAccountById(item.AccountId);
                            clSupplierAccs.FlagDefault = false;
                            clSupplierAccs.Update();
                        }
                        
                    }
                }
                if (clSupplierAcc.FlagDefault)
                {
                    clSupplierAcc.FlagDefault = false;
                }
                else
                {
                    clSupplierAcc.FlagDefault = true;
                }
                clSupplierAcc.Update();
            }

            Response.Redirect("supplier_account_list.aspx?supid=" + Request.QueryString["supid"]);
        }

        protected void gridSupplierAccount_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                bool bolStatus = (bool)DataBinder.Eval(e.Row.DataItem, "FlagDefault");
                ImageButton linkStaffStat = (ImageButton)e.Row.Cells[2].FindControl("imgButton");

               
                //Image imgStaffStat = (Image)e.Row.Cells[4].FindControl("imagestaffstat");


                if (bolStatus)
                {
                    linkStaffStat.ImageUrl = "../../images/staffactive.png";
                }
                else
                {
                    linkStaffStat.ImageUrl = "../../images/staffinactive.png";
                }
            }
        }

        protected void FormSupAccAdd_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            e.Values["SupplierId"] = short.Parse(this.strQueryString);

        }

        protected void FormSupAccAdd_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            //Server.Transfer("supplier_account_list.aspx?supid=" + Request.QueryString["supid"]);
            Response.Redirect(Request.Url.ToString());
        }

        protected void FormSupAccAdd_Updated(object sender, FormViewUpdatedEventArgs e)
        {
            Server.Transfer("supplier_account_list.aspx?supid=" + Request.QueryString["supid"]);
        }

        protected void FormSupAccAdd_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            e.NewValues["SupplierId"] = short.Parse(this.strQueryString);
        }
        //protected void ObjectDataSource2_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        //{
        //    int id = (int)e.ReturnValue;
        //    Server.Transfer("supplier_account_list.aspx?supid=" + this.ClientQueryString + "&acid=" + id);
        //}
    }
}