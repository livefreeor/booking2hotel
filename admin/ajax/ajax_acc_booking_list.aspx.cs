using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using Hotels2thailand.Booking;
using Hotels2thailand.Production;
using Hotels2thailand;

public partial class ajax_acc_booking_list : System.Web.UI.Page
{
    public string qBokingStatus
    {
        get
        {
            return Request.QueryString["bs"];
        }
    }
    public string qBokingProductStatus
    {
        get
        {
            return Request.QueryString["bps"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            if (string.IsNullOrEmpty(this.qBokingProductStatus) || this.qBokingProductStatus == "0" || this.qBokingProductStatus == "10")
            {
                // booking Status = 68; booking product status = 10
                Response.Write(BookingListGen("68", "10"));

                Response.Write(BookingListGen("96", "97"));

                // booking Status = 71; booking product status = 12
                Response.Write(BookingListGen("72", "12"));

                Response.Write(BookingListGen("98", "22"));
                Response.Write(BookingListGen("94", "95"));
                

                // booking Status = 72; booking product status = 11
                Response.Write(BookingListGen("71", "11"));

                // booking Status = 83; booking product status = 13
                Response.Write(BookingListGen("83", "13"));

                // booking Status = 85; booking product status = 15


                Response.Write(BookingListGen("85", "15"));

                

                Response.Write(BookingListGen("92", "93"));

                Response.Write(BookingListGen("30", "17"));
            }
            else
            {

                Response.Write(BookingListGen(this.qBokingStatus, this.qBokingProductStatus));
            }

            Response.Flush();

        }
    }
    // case 1: normal bookingLst
    public string BookingListGen(string strBookingStatus, string strBookingProductStatus)
    {
        return BookingListBodyBlock(strBookingStatus, strBookingProductStatus, 1);
    }

    public string BookingListBodyBlock(string strBookingStatus, string strBookingProductStatus, byte bytTypeList, string headcolor = "")
    {
        StringBuilder result = new StringBuilder();

        try
        {
            string strStyle = "";
            switch (headcolor)
            {
                case "red":
                    strStyle = "style=\"color:#d90000;\"";
                    break;
                case "green":
                    strStyle = "style=\"color:#508701;\"";
                    break;

            }

            result.Append("<div class=\"Booking_list_block\" id=\"Booking_list_block_" + strBookingProductStatus + "\" >");
            result.Append("<div class=\"BookingStatusProductHead\" " + strStyle + "><img src=\"/images/content.png\" />&nbsp;" + Status.GetStatusTitleById(short.Parse(strBookingStatus)));

            result.Append("&nbsp;&nbsp;<a href=\"void(0);\"  rel=\"" + strBookingStatus + "," + strBookingProductStatus + "\" class=\"btn_move_status\">Booking Status Move </a>&nbsp;&nbsp;|&nbsp;&nbsp;");
            result.Append("<a href=\"void(0);\"  rel=\"" + strBookingStatus + "," + strBookingProductStatus + "\" class=\"btn_close_booking\">Close Booking </a>");
            result.Append("");
            result.Append("");
            result.Append("<label style=\"float:right;margin:0px 10px 0px 0px;\">Order by: <select  id=\"drop_orderBy_" + strBookingProductStatus + "\" class=\"Extra_Drops_small\" onchange=\"GetByOrder('" + strBookingProductStatus + "');\" >");
            result.Append("<option value=\"date_submit\">Booking Recieved</option>");
            result.Append("<option value=\"date_modify\">Last Modify</option>");
            result.Append("<option value=\"Paid\">Paid</option>");
            result.Append("<option value=\"date_time_check_in\">Check-In A-Z</option>");
            result.Append("<option value=\"date_time_check_in_desc\">Check-In Z-A</option>");
            result.Append("<option value=\"date_time_check_out\">Check-Out A-Z</option>");
            result.Append("<option value=\"date_time_check_out_desc\">Check-Out Z-A</option>");
            result.Append("<option value=\"gate_way\">GateWay</option>");
            result.Append("</select></label>");
            result.Append("</div>");

            result.Append("<input type=\"hidden\" id=\"hd_BodyVal_" + strBookingProductStatus + "\" value=\"" + bytTypeList + ";" + strBookingStatus + ";" + strBookingProductStatus + "\" />");
            result.Append("<input type=\"hidden\" id=\"orderby_" + strBookingProductStatus + "\" value=\"date_submit\" />");


            result.Append("<div id=\"Booking_list_" + strBookingProductStatus + "\" ></div>");
            result.Append("<div id=\"Booking_sum_" + strBookingProductStatus + "\" ></div>");
            result.Append("</div>");
        }
        catch (Exception ex)
        {
            Response.Write("error: " + ex.Message);
            Response.End();
        }

        return result.ToString();
    }


    

    

   
    
}