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
    public partial class admin_extranet_public_holidays : Hotels2BasePage
    {
         protected void Page_Load(object sender, EventArgs e)
         {
             if (!this.Page.IsPostBack)
             {
                 dropYearDataBind();
                 GVListDataBind();
             }
         }

         public void dropYearDataBind()
         {
             dropyear.DataSource = Hotels2thailand.Hotels2DateTime.GetYearList();
             dropyear.DataTextField = "Key";
             dropyear.DataValueField = "Value";
             dropyear.DataBind();
         }

         public void dropyear_OnSelectedIndexChanged(object sender, EventArgs e)
         {
             GVListDataBind();
         }

         public void btnSave_Onclick(object sender, EventArgs e)
         {
             
             PublicholidayExtranet cPublic = new PublicholidayExtranet();
             cPublic.InsertHoliday(txttitle.Text, dDatePicker.GetDatetStart);


             Response.Redirect(Request.Url.ToString());
             //txttitle.Text = string.Empty;
             //GVListDataBind();
         }

         public void GVListDataBind()
         {
             string strYear = dropyear.SelectedValue;
             //Response.Write(strYear);
             //Response.End();
             DateTime dDateYear = new DateTime(int.Parse(strYear), 9, 9);
             PublicholidayExtranet cPublicHolidays = new PublicholidayExtranet();

             GVList.DataSource = cPublicHolidays.getAllPublicHolidaysByYear(dDateYear);
             GVList.DataBind();
         }

         public void GVList_OnRowDataBound(object sender, GridViewRowEventArgs e)
         {
             if (e.Row.RowType == DataControlRowType.DataRow)
             {
                 Hotels2thailand.UI.Controls.Control_DatepickerCalendar_single ControlDate = e.Row.Cells[2].FindControl("dDatePicker") as Hotels2thailand.UI.Controls.Control_DatepickerCalendar_single;

                 DateTime dDate = (DateTime)DataBinder.Eval(e.Row.DataItem, "HolidayDate");
                 ControlDate.DateStart = dDate;
                 ControlDate.DataBind();
             }
         }

         public void btnSaveEdit_OnClick(object sender, EventArgs e)
         {

             Button btn = (Button)sender;
             byte HolidayId = byte.Parse(btn.CommandArgument);
             if (btn.CommandName == "save")
             {
                 GridViewRow GvRow = (GridViewRow)(sender as Control).Parent.Parent;
                 int RowIndex = GvRow.RowIndex;

                 TextBox txtTitle = GVList.Rows[RowIndex].Cells[1].FindControl("txtTitle") as TextBox;
                 Hotels2thailand.UI.Controls.Control_DatepickerCalendar_single dDatePicker = GVList.Rows[RowIndex].Cells[2].FindControl("dDatePicker") as Hotels2thailand.UI.Controls.Control_DatepickerCalendar_single;
                 PublicholidayExtranet cPublicHolidays = new PublicholidayExtranet();
                 cPublicHolidays.UpdateHolidays(HolidayId, txtTitle.Text, dDatePicker.GetDatetStart);
                 GVListDataBind();
             }

         }
    }
}