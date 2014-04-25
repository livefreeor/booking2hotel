using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.Suppliers;

namespace Hotels2thailand.UI
{
    public partial class admin_supplier_payment_policy : Hotels2BasePage
    {
         protected void Page_Load(object sender, EventArgs e)
         {
            if (!this.Page.IsPostBack)
            {
                // Show title Supplier
                Supplier cSupplier = new Supplier();
                cSupplier = cSupplier.getSupplierById(short.Parse(this.qSupplierId));
                lblhead.Text = cSupplier.SupplierTitle;

                dropDayAdvance.DataSource = this.dicGetNumber(60);
                dropDayAdvance.DataTextField = "Value";
                dropDayAdvance.DataValueField = "Key";
                dropDayAdvance.DataBind();

                lblPayment.Text = cSupplier.PaymentTypeTitle;

                GVSupPolicyDataBind();
            }
          }

         public void lblPaymentDataBind()
         {
             lblPayment.Text = "";
         }

         public void GVSupPolicyDataBind()
         {
             if (!string.IsNullOrEmpty(this.qSupplierId))
             {
                 SupplierPaymentPolicy SupPaymentPolicy = new SupplierPaymentPolicy();
                 short shortSupplierId = short.Parse(this.qSupplierId);

                 GVSupPolicy.DataSource = SupPaymentPolicy.getListSupplierPaymentPolicyBySupplierId(shortSupplierId);
                 GVSupPolicy.DataBind();
             }
         }

         public void GVSupPolicy_OnRowDataBound(object sender, GridViewRowEventArgs e)
         {
             if (e.Row.RowType == DataControlRowType.DataRow)
             {
                 Hotels2thailand.UI.Controls.Control_DatepickerCalendar controlDatepicker = e.Row.Cells[1].FindControl("dDatePicker") as Hotels2thailand.UI.Controls.Control_DatepickerCalendar;
                 DateTime dDateStart = (DateTime)DataBinder.Eval(e.Row.DataItem, "DateStart");
                 DateTime dDateEnd = (DateTime)DataBinder.Eval(e.Row.DataItem, "DateEnd");
                 controlDatepicker.DateStart = dDateStart;
                 controlDatepicker.DateEnd = dDateEnd;
                 controlDatepicker.DataBind();

                 DropDownList dropMini = e.Row.Cells[2].FindControl("drpAdvance") as DropDownList;
                 dropMini.DataSource = this.dicGetNumber(60);
                 dropMini.DataTextField = "Value";
                 dropMini.DataValueField = "Key";
                 dropMini.DataBind();

                 byte bytMiniStay = (byte)DataBinder.Eval(e.Row.DataItem, "DayAdvance");
                 dropMini.SelectedValue = bytMiniStay.ToString();
             }
         }

         public void btnSave_Onclick(object sender, EventArgs e)
         {
             if (!string.IsNullOrEmpty(this.qSupplierId))
             {
                 byte bytdayAdvance = byte.Parse(dropDayAdvance.SelectedValue);
                 short shrPolicyId = short.Parse(this.qSupplierId);

                 int intsert = SupplierPaymentPolicy.insertNewSupplierPaymentPolicy(shrPolicyId, DatePicker.GetDatetStart, DatePicker.GetDatetEnd, bytdayAdvance);
             }

             Response.Redirect(Request.Url.ToString());
         }


         public void btnminsave_OnClick(object sender, EventArgs e)
         {
             Button btnMini = (Button)sender;

             if (btnMini.CommandName == "miniedit")
             {
                 string[] argument = btnMini.CommandArgument.Split(',');
                 int bytSupPolicyId = int.Parse(argument[0]);
                 int RowInDex = int.Parse(argument[1]);

                 foreach (GridViewRow GvRow in GVSupPolicy.Rows)
                 {
                     if (RowInDex == GvRow.RowIndex)
                     {
                         Hotels2thailand.UI.Controls.Control_DatepickerCalendar controlDatepicker =
                         GVSupPolicy.Rows[GvRow.RowIndex].Cells[1].FindControl("dDatePicker") as Hotels2thailand.UI.Controls.Control_DatepickerCalendar;
                         DropDownList dropDayAdvance = GVSupPolicy.Rows[GvRow.RowIndex].Cells[2].FindControl("drpAdvance") as DropDownList;

                         SupplierPaymentPolicy.UpdateSupPaymentPolicy(bytSupPolicyId, controlDatepicker.GetDatetStart, controlDatepicker.GetDatetEnd, byte.Parse(dropDayAdvance.SelectedValue));
                          
                     }
                 }
             }

             GVSupPolicyDataBind();
         }




        
    }
}