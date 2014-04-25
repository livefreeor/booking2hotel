using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class admin_product_minimum_stay : Hotels2BasePage
    {
         protected void Page_Load(object sender, EventArgs e)
         {
            if (!this.Page.IsPostBack)
            {
                dropMiniMum.DataSource = this.dicGetNumberstart0(60);
                dropMiniMum.DataTextField = "Value";
                dropMiniMum.DataValueField = "Key";
                dropMiniMum.DataBind();

                GVMiniListDataBind();
            }
          }

         public void GVMiniListDataBind()
         {
             if (!string.IsNullOrEmpty(this.qProductId))
             {
                 ProductMinimumStay cMiniStay = new ProductMinimumStay();
                 int intProductId = int.Parse(this.qProductId);

                 GVMiniList.DataSource = cMiniStay.getListMiniListByProductId(intProductId);
                 GVMiniList.DataBind();
             }
         }

         public void GVMiniList_OnRowDataBound(object sender, GridViewRowEventArgs e)
         {
             if (e.Row.RowType == DataControlRowType.DataRow)
             {
                 Hotels2thailand.UI.Controls.Control_DatepickerCalendar controlDatepicker = e.Row.Cells[1].FindControl("dDatePicker") as Hotels2thailand.UI.Controls.Control_DatepickerCalendar;
                 DateTime dDateStart = (DateTime)DataBinder.Eval(e.Row.DataItem, "DateStart");
                 DateTime dDateEnd = (DateTime)DataBinder.Eval(e.Row.DataItem, "DateEnd");
                 controlDatepicker.DateStart = dDateStart;
                 controlDatepicker.DateEnd = dDateEnd;
                 controlDatepicker.DataBind();

                 DropDownList dropMini = e.Row.Cells[2].FindControl("drpMini") as DropDownList;
                 dropMini.DataSource = this.dicGetNumber(60);
                 dropMini.DataTextField = "Value";
                 dropMini.DataValueField = "Key";
                 dropMini.DataBind();

                 ImageButton imgStatus = e.Row.Cells[4].FindControl("imgbtnStatus") as ImageButton;
                 bool bolStatus = (bool)DataBinder.Eval(e.Row.DataItem, "Status");

                 byte bytMiniStay = (byte)DataBinder.Eval(e.Row.DataItem, "MiniTotal");
                 dropMini.SelectedValue = bytMiniStay.ToString();

                 if (bolStatus)
                     imgStatus.ImageUrl = "~/images/true.png";
                 else
                     imgStatus.ImageUrl = "~/images/false.png";
             }
         }

         public void btnSave_Onclick(object sender, EventArgs e)
         {
             if (!string.IsNullOrEmpty(this.qProductId))
             {
                 ProductMinimumStay cMinimum = new ProductMinimumStay();
                 byte bytMinimum = byte.Parse(dropMiniMum.SelectedValue);
                 int intProductId = int.Parse(this.qProductId);
                 int intsert = cMinimum.InsertNewMinimumStay(intProductId, DatePicker.GetDatetStart, DatePicker.GetDatetEnd, bytMinimum);
             }

             Response.Redirect(Request.Url.ToString());
         }


         public void btnminsave_OnClick(object sender, EventArgs e)
         {
             Button btnMini = (Button)sender;

             if (btnMini.CommandName == "miniedit")
             {
                 string[] argument = btnMini.CommandArgument.Split(',');
                 int bytMiniId = int.Parse(argument[0]);
                 int RowInDex = int.Parse(argument[1]);

                 foreach (GridViewRow GvRow in GVMiniList.Rows)
                 {
                     if (RowInDex == GvRow.RowIndex)
                     {
                         Hotels2thailand.UI.Controls.Control_DatepickerCalendar controlDatepicker =
                         GVMiniList.Rows[GvRow.RowIndex].Cells[1].FindControl("dDatePicker") as Hotels2thailand.UI.Controls.Control_DatepickerCalendar;
                         DropDownList dropMini = GVMiniList.Rows[GvRow.RowIndex].Cells[2].FindControl("drpMini") as DropDownList;
                         ProductMinimumStay cMinimum = new ProductMinimumStay();
                         cMinimum.UpdateMiniStay(bytMiniId, controlDatepicker.GetDatetStart, controlDatepicker.GetDatetEnd, byte.Parse(dropMini.SelectedValue));
                     }
                 }
             }

             GVMiniListDataBind();
         }


         public void imgbtnStatus_StatusUPdate(object sender, EventArgs e)
         {
             ImageButton btn = (ImageButton)sender;
             string[] Argument = btn.CommandArgument.Split(',');

             int intMinimumId = int.Parse(Argument[0]);
             int intRowInDex = int.Parse(Argument[1]);
             bool Status = bool.Parse(Argument[2]);

             GridViewRow GvRow = (GridViewRow)(sender as Control).Parent.Parent;
             int RowIndex = GvRow.RowIndex;
             if (intRowInDex == RowIndex)
             {
                 ProductMinimumStay cMiniMumStay = new ProductMinimumStay();
                 if (Status)
                     cMiniMumStay.UpdateMiniStayStatus(intMinimumId, false);
                 else
                     cMiniMumStay.UpdateMiniStayStatus(intMinimumId, true);

             }
             GVMiniListDataBind();
             //Response.Redirect(Request.Url.ToString());
         }



    }
}