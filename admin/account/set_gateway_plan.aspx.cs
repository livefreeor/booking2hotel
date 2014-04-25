using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using Hotels2thailand;
using Hotels2thailand.Booking;
using Hotels2thailand.DataAccess;

namespace Hotels2thailand.UI
{
    public partial class admin_account_set_gateway_plan : Hotels2BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {

                Gateway cGateway = new Gateway();
                GVGateWay.DataSource = cGateway.GetGateWayAllByStatus();
                GVGateWay.DataBind();
            }
 
        }

        public void GVGateWay_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Image imgActive = e.Row.Cells[0].FindControl("imgActive") as Image;
                ImageButton btnActives = e.Row.Cells[1].FindControl("imgbtnActive") as ImageButton;

                DropDownList dropTimeStart = e.Row.Cells[3].FindControl("dropTimestart") as
 DropDownList;
                dropTimeStart.DataSource = this.dicGetTimEHrs(23);
                dropTimeStart.DataTextField = "Value";
                dropTimeStart.DataValueField = "Key";
                dropTimeStart.DataBind();

                DropDownList dropTimeSEnd = e.Row.Cells[4].FindControl("dropTimeEnd") as DropDownList;
                dropTimeSEnd.DataSource = this.dicGetTimEHrs(23);
                dropTimeSEnd.DataTextField = "Value";
                dropTimeSEnd.DataValueField = "Key";
                dropTimeSEnd.DataBind();


                bool Daymon = (bool)DataBinder.Eval(e.Row.DataItem,"DayMon");
                bool DayTue = (bool)DataBinder.Eval(e.Row.DataItem, "DayTue");
                bool DayWed = (bool)DataBinder.Eval(e.Row.DataItem, "DayWed");
                bool DayThu = (bool)DataBinder.Eval(e.Row.DataItem, "DayThu");
                bool DayFri = (bool)DataBinder.Eval(e.Row.DataItem, "DayFri");
                bool DaySat = (bool)DataBinder.Eval(e.Row.DataItem, "DaySat");
                bool DaySun = (bool)DataBinder.Eval(e.Row.DataItem, "DaySun");

                bool GatWayActive = (bool)DataBinder.Eval(e.Row.DataItem, "GatWayActive");
                bool Status = (bool)DataBinder.Eval(e.Row.DataItem, "Status");


                CheckBox chkDayMon = e.Row.Cells[5].FindControl("chkDayMon") as CheckBox;
                CheckBox chkDayTue = e.Row.Cells[5].FindControl("chkDayTue") as CheckBox;
                CheckBox chkDayWed = e.Row.Cells[5].FindControl("chkDayWed") as CheckBox;
                CheckBox chkDayThu = e.Row.Cells[5].FindControl("chkDayThu") as CheckBox;
                CheckBox chkDayFri = e.Row.Cells[5].FindControl("chkDayFri") as CheckBox;
                CheckBox chkDaySat = e.Row.Cells[5].FindControl("chkDaySat") as CheckBox;
                CheckBox chkDaySun = e.Row.Cells[5].FindControl("chkDaySun") as CheckBox;

                if (Daymon)
                    chkDayMon.Checked = true;
                if (DayTue)
                    chkDayTue.Checked = true;
                if (DayWed)
                    chkDayWed.Checked = true;
                if (DayThu)
                    chkDayThu.Checked = true;
                if (DayFri)
                    chkDayFri.Checked = true;
                if (DaySat)
                    chkDaySat.Checked = true;
                if (DaySun)
                    chkDaySun.Checked = true;

                if (GatWayActive){
                    imgActive.ImageUrl = "~/images/status.png";
                    btnActives.ImageUrl = "~/images/old-go-down.png";
                }
                else{
                    imgActive.ImageUrl = "~/images/status-offline.png";
                    btnActives.ImageUrl = "~/images/arrow_large_up.png";
                    
                }
                byte bytTimeStart = (byte)DataBinder.Eval(e.Row.DataItem, "TimeStart");
                byte bytTimeEnd = (byte)DataBinder.Eval(e.Row.DataItem, "TimeEnd");

                dropTimeStart.SelectedValue = bytTimeStart.ToString();
                dropTimeSEnd.SelectedValue = bytTimeEnd.ToString();
            }
        }

        public void imgbtnActive_Onclick(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;

            string[] Argument = btn.CommandArgument.Split(',');

            byte bytgateWayId = byte.Parse(Argument[0]);
            bool bolIsActive = bool.Parse(Argument[1]);

            Gateway cGateWay = new Gateway();

            if (bolIsActive)
                cGateWay.UpdateGateWayActive(bytgateWayId, false);
            else
                cGateWay.UpdateGateWayActive(bytgateWayId, true);

            Response.Redirect(Request.Url.ToString());
        }

        public void btnSaveAll_Onclick(object sender, EventArgs e)
        {
            Gateway cGateWay = new Gateway();
            foreach(GridViewRow GVRow in GVGateWay.Rows)
            {
                if(GVRow.RowType == DataControlRowType.DataRow)
                {
                    DropDownList dropTimeStart = GVGateWay.Rows[GVRow.RowIndex].Cells[3].FindControl("dropTimestart") as DropDownList;
                    DropDownList dropTimeSEnd = GVGateWay.Rows[GVRow.RowIndex].Cells[4].FindControl("dropTimeEnd") as DropDownList;

                    CheckBox chkDayMon = GVGateWay.Rows[GVRow.RowIndex].Cells[5].FindControl("chkDayMon") as CheckBox;
                    CheckBox chkDayTue = GVGateWay.Rows[GVRow.RowIndex].Cells[5].FindControl("chkDayTue") as CheckBox;
                    CheckBox chkDayWed = GVGateWay.Rows[GVRow.RowIndex].Cells[5].FindControl("chkDayWed") as CheckBox;
                    CheckBox chkDayThu = GVGateWay.Rows[GVRow.RowIndex].Cells[5].FindControl("chkDayThu") as CheckBox;
                    CheckBox chkDayFri = GVGateWay.Rows[GVRow.RowIndex].Cells[5].FindControl("chkDayFri") as CheckBox;
                    CheckBox chkDaySat = GVGateWay.Rows[GVRow.RowIndex].Cells[5].FindControl("chkDaySat") as CheckBox;
                    CheckBox chkDaySun = GVGateWay.Rows[GVRow.RowIndex].Cells[5].FindControl("chkDaySun") as CheckBox;

                    bool bolDayMon = false; bool bolDayTue = false; bool bolDayWed = false; bool bolDaythu = false;
                    bool bolDayFri = false;  bool bolDaySat = false; bool bolDaySun = false;

                    if (chkDayMon.Checked) { bolDayMon = true; }
                    if (chkDayTue.Checked) { bolDayTue = true; }
                    if (chkDayWed.Checked) { bolDayWed = true; }
                    if (chkDayThu.Checked) { bolDaythu = true; }
                    if (chkDayFri.Checked) { bolDayFri = true; }
                    if (chkDaySat.Checked) { bolDaySat = true; }
                    if (chkDaySun.Checked) { bolDaySun = true; }

                    byte bytTimstart = byte.Parse(dropTimeStart.SelectedValue);
                    byte bytTimend= byte.Parse(dropTimeSEnd.SelectedValue);

                    byte byteGayeWayId = (byte)GVGateWay.DataKeys[GVRow.RowIndex].Value;
                    cGateWay.UpdateGateWayByGateWayId(byteGayeWayId, bytTimstart, bytTimend, bolDayMon, bolDayTue,
                        bolDayWed, bolDaythu, bolDayFri, bolDaySat, bolDaySun);
                }
            }

            Response.Redirect(Request.Url.ToString());
        }
        
    }
}