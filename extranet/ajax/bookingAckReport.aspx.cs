using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Booking;
using Hotels2thailand.Front;
namespace Hotels2thailand.UI
{
    public partial class vGenerator_bookingAckReport : Hotels2BasePageExtra_Ajax
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FrontBookingAcknowledge objAcknow = new FrontBookingAcknowledge();
            //List<object> AcknowList = objAcknow.GetBookingAcknowledgeAll(455);

            string acknowTable = string.Empty;

            byte StatusExtranetID = byte.Parse(Request.QueryString["act"]);
            byte SortBy = byte.Parse(Request.QueryString["sort"]);
            bool search_date_in = false;
            bool search_date_recieve = false;
            DateTime date_check_in = new DateTime();
            DateTime date_check_out = new DateTime();
            DateTime date_recieve_start = new DateTime();
            DateTime date_recieve_end = new DateTime();
            string keySearch = string.Empty;




            List<object> AcknowList = new List<object>();
            if (string.IsNullOrEmpty(Request.QueryString["k"]))
            {

                if (!string.IsNullOrEmpty(Request.QueryString["date_in"]))
                {
                    date_check_in = Utility.ConvertDateInput(Request.QueryString["date_in"]);
                    date_check_out = Utility.ConvertDateInput(Request.QueryString["date_out"]);
                    search_date_in = true;
                }

                if (!string.IsNullOrEmpty(Request.QueryString["rdate_in"]))
                {
                    date_recieve_start = Utility.ConvertDateInput(Request.QueryString["rdate_in"]);
                    date_recieve_end = Utility.ConvertDateInput(Request.QueryString["rdate_out"]);
                    search_date_recieve = true;
                }


                if (search_date_in && search_date_recieve)
                {
                    AcknowList = objAcknow.GetBookingAcknowledgeByDateAll(this.CurrentProductActiveExtra, StatusExtranetID, SortBy, date_recieve_start, date_recieve_end, date_check_in, date_check_out);
                }

                if (search_date_in)
                {
                    //HttpContext.Current.Response.Write("date in");
                    //HttpContext.Current.Response.End();
                    AcknowList = objAcknow.GetBookingAcknowledgeByDateSubmit(this.CurrentProductActiveExtra, StatusExtranetID, SortBy, date_check_in, date_check_out);
                }

                if (search_date_recieve)
                {
                    //HttpContext.Current.Response.Write("date submit");
                    //HttpContext.Current.Response.End();
                    AcknowList = objAcknow.GetBookingAcknowledgeByDateRecieve(this.CurrentProductActiveExtra, StatusExtranetID, SortBy, date_recieve_start, date_recieve_end);
                }

                if (!search_date_in && !search_date_recieve)
                {
                    //HttpContext.Current.Response.Write("normal");
                    //HttpContext.Current.Response.End();
                    AcknowList = objAcknow.GetBookingAcknowledgeByStatus(this.CurrentProductActiveExtra, StatusExtranetID, SortBy);
                }

            }
            else
            {
                keySearch = Request.QueryString["k"].Replace("'", "");
                //Response.Write(keySearch);
                //Response.End();
                AcknowList = objAcknow.GetBookingAcknowledgeByKeyword(this.CurrentProductActiveExtra, byte.Parse(Request.QueryString["ct"]), keySearch);
                //int propcount= AcknowList[0].GetType().GetProperties().Count();
            }



            Response.Write(RenderTableDisplay(AcknowList, StatusExtranetID));

        }

        public string displayBookingStatus(bool bookingStatus)
        {
            string result = string.Empty;
            if (!bookingStatus)
            {
                result = "<span class=\"fStatus_booking_open\">Open</span>";
            }
            else
            {
                result = "<span class=\"fStatus_booking_closed\">Closed</span>";
            }
            return result;
        }
        public string GetAcknowledgeStatusTitle(byte StatusExtranetID)
        {
            string result = string.Empty;
            switch (StatusExtranetID)
            {
                case 1:
                    result = "Waiting for Confirm Acknowledge";
                    break;
                case 2:
                    result = "Acknowledge Confirm Completed";
                    break;
                case 3:
                    result = "Waiting for Confirm Cancel";
                    break;
                case 4:
                    result = "Cancel Completed";
                    break;
            }
            return result;
        }
        public string RenderTableDisplay(List<object> AcknowList, byte statusID)
        {
            string acknowTable = string.Empty;
            int status_temp = 0;
            int countRow = 1;
            string bgDefault = "#ffffff";

            if (statusID == 0)
            {
                foreach (FrontBookingAcknowledge item in AcknowList)
                {
                    if (countRow % 2 == 0)
                    {
                        bgDefault = "#f2f2f2";
                    }
                    else
                    {
                        bgDefault = "#ffffff";
                    }

                    if (status_temp != item.BookingExtranetStatus)
                    {
                        bgDefault = "#ffffff";

                        if (countRow != 1)
                        {
                            acknowTable = acknowTable + "</table>\n";
                        }
                        acknowTable = acknowTable + "<table class=\"tbl_acknow\" cellspacing=\"2\" bgcolor=\"#e4e4e4\">\n";
                        acknowTable = acknowTable + "<tr class=\"tbl_acknow_row_white\">\n";
                        acknowTable = acknowTable + "<td colspan=\"8\" class=\"header_category\">" + GetAcknowledgeStatusTitle(item.BookingExtranetStatus) + "</td>\n";
                        acknowTable = acknowTable + "</tr>\n";
                        acknowTable = acknowTable + "<tr align=\"center\" class=\"header_field\">\n";
                        acknowTable = acknowTable + "<th width=\"100\">Booking ID</th>\n";
                        acknowTable = acknowTable + "<th width=\"100\">Ack. ID</th>\n";
                        acknowTable = acknowTable + "<th width=\"100\">Ack. By</th>\n";
                        acknowTable = acknowTable + "<th width=\"160\">Guest Name</th>\n";
                        acknowTable = acknowTable + "<th width=\"130\">Request Date</th>\n";
                        acknowTable = acknowTable + "<th width=\"130\">In/Out</th>\n";
                        acknowTable = acknowTable + "<th width=\"130\">Ack. Date</th>\n";
                        acknowTable = acknowTable + "<th>Status</th>\n";
                        acknowTable = acknowTable + "</tr>\n";
                    }

                    acknowTable = acknowTable + "<tr bgcolor=\"" + bgDefault + "\">\n";
                    acknowTable = acknowTable + "<td align=\"center\"><a href=\"\" onclick=\"GetBookingDetail(" + item.BookingProductID + ");return false;\">" + item.BookingID + "</a></td>\n";

                    if (string.IsNullOrEmpty(item.AcknowledgeID))
                    {
                        switch (item.BookingExtranetStatus)
                        {
                            case 1:
                                acknowTable = acknowTable + "<td align=\"center\"><img src=\"/images/input-edit.png\" class=\"ackButton\"></td>\n";
                                break;
                            case 3:
                                acknowTable = acknowTable + "<td align=\"center\"><img src=\"/images/input-edit.png\" class=\"cancelButton\"></td>\n";
                                break;
                            default:
                                acknowTable = acknowTable + "<td align=\"center\">N/A</td>\n";
                                break;
                        }


                    }
                    else
                    {
                        acknowTable = acknowTable + "<td align=\"center\">" + item.AcknowledgeID + "</td>\n";
                    }

                    if (item.StaffTitle=="Auto")
                    {
                        acknowTable = acknowTable + "<td align=\"center\">" + item.StaffTitle + "</td>\n";
                    }else{
                        acknowTable = acknowTable + "<td>" + item.StaffTitle + "</td>\n";
                    }
                    
                    acknowTable = acknowTable + "<td>" + item.Fullname + "</td>\n";
                    acknowTable = acknowTable + "<td align=\"right\">" + item.DateSubmit.ToString("ddd, dd MMM yyyy") + "</td>\n";
                    acknowTable = acknowTable + "<td align=\"right\">" + item.DateCheckIn.ToString("ddd, dd MMM yyyy") + "<br/>\n";
                    acknowTable = acknowTable + item.DateCheckOut.ToString("ddd, dd MMM yyyy") + "</td>\n";
                    if (item.BookingExtranetStatus == 1 || item.BookingExtranetStatus == 3)
                    {
                        acknowTable = acknowTable + "<td align=\"center\">N/A</td>\n";
                    }
                    else {
                        acknowTable = acknowTable + "<td align=\"right\">" + item.DateConfirm.ToString("ddd, dd MMM yyyy") + "</td>\n";
                    }
                    acknowTable = acknowTable + "<td align=\"center\">" + displayBookingStatus(item.Status) + "</td>\n";
                    acknowTable = acknowTable + "</tr>\n";
                    status_temp = item.BookingExtranetStatus;
                    countRow = countRow + 1;
                }
                if (!string.IsNullOrEmpty(acknowTable))
                {
                    acknowTable = acknowTable + "</table>\n";
                }
                else
                {
                    acknowTable = "<div class=\"no_result\">No result click <a href=\"acknow.html\">back</a> to search again</div>";
                }
            }
            else
            {
                foreach (FrontBookingAcknowledge item in AcknowList)
                {
                    if (countRow % 2 == 0)
                    {
                        bgDefault = "#f2f2f2";
                    }
                    else
                    {
                        bgDefault = "#ffffff";
                    }

                    if (status_temp != item.BookingExtranetStatus)
                    {
                        bgDefault = "#ffffff";

                        if (countRow != 1)
                        {
                            acknowTable = acknowTable + "</table>\n";
                        }

                        if (statusID == 1 || statusID == 3)
                        {
                            acknowTable = acknowTable + "<form id=\"frmSave\">\n";
                            acknowTable = acknowTable + "<table class=\"tbl_acknow\" cellspacing=\"2\" bgcolor=\"#e4e4e4\">\n";
                            acknowTable = acknowTable + "<tr class=\"tbl_acknow_row_white\">\n";
                            acknowTable = acknowTable + "<td colspan=\"8\" class=\"header_category\">" + GetAcknowledgeStatusTitle(item.BookingExtranetStatus) + "</td>\n";
                            acknowTable = acknowTable + "</tr>\n";
                            acknowTable = acknowTable + "<tr align=\"center\" class=\"header_field\">\n";
                            acknowTable = acknowTable + "<th>#</th>\n";
                            acknowTable = acknowTable + "<th>Booking ID</th>\n";
                            acknowTable = acknowTable + "<th>Ack. ID</th>\n";
                            acknowTable = acknowTable + "<th>Ack. By</th>\n";
                            acknowTable = acknowTable + "<th>Guest Name</th>\n";
                            acknowTable = acknowTable + "<th>Request Date</th>\n";
                            acknowTable = acknowTable + "<th>In/Out</th>\n";
                            acknowTable = acknowTable + "<th>Acknowledge Date</th>\n";
                            acknowTable = acknowTable + "</tr>\n";
                        }
                        else
                        {
                            acknowTable = acknowTable + "<table class=\"tbl_acknow\" cellspacing=\"2\" bgcolor=\"#e4e4e4\">\n";
                            acknowTable = acknowTable + "<tr class=\"tbl_acknow_row_white\">\n";
                            acknowTable = acknowTable + "<td colspan=\"8\" class=\"header_category\">" + GetAcknowledgeStatusTitle(item.BookingExtranetStatus) + "</td>\n";
                            acknowTable = acknowTable + "</tr>\n";
                            acknowTable = acknowTable + "<tr align=\"center\" class=\"header_field\">\n";
                            acknowTable = acknowTable + "<th>Booking ID</th>\n";
                            acknowTable = acknowTable + "<th>Ack. ID</th>\n";
                            acknowTable = acknowTable + "<th>Ack. By</th>\n";
                            acknowTable = acknowTable + "<th>Guest Name</th>\n";
                            acknowTable = acknowTable + "<th>Request Date</th>\n";
                            acknowTable = acknowTable + "<th>In/Out</th>\n";
                            acknowTable = acknowTable + "<th>Acknowledge Date</th>\n";
                            acknowTable = acknowTable + "<th>Status</th>\n";
                            acknowTable = acknowTable + "</tr>\n";
                        }

                    }
                    if (statusID == 1 || statusID == 3)
                    {
                        acknowTable = acknowTable + "<tr bgcolor=\"" + bgDefault + "\">\n";
                        acknowTable = acknowTable + "<td><input type=\"checkbox\"  name=\"booking_id\" value=\"" + item.BookingID + "\"/></td>\n";
                        acknowTable = acknowTable + "<td align=\"center\"><a href=\"#\" onclick=\"GetBookingDetail(" + item.BookingProductID + ");return false;\">" + item.BookingID + "</a></td>\n";
                        acknowTable = acknowTable + "<td align=\"center\"><input type=\"text\" name=\"ack_" + item.BookingID + "\"/></td>\n";
                        acknowTable = acknowTable + "<td align=\"center\">" + item.StaffTitle + "</td>\n";
                        acknowTable = acknowTable + "<td>" + item.Fullname + "</td>\n";
                        acknowTable = acknowTable + "<td align=\"right\">" + item.DateSubmit.ToString("ddd, dd MMM yyyy") + "</td>\n";
                        acknowTable = acknowTable + "<td align=\"right\">" + item.DateCheckIn.ToString("ddd, dd MMM yyyy") + "<br/>\n";
                        acknowTable = acknowTable + item.DateCheckOut.ToString("ddd, dd MMM yyyy") + "</td>\n";
                        acknowTable = acknowTable + "<td align=\"center\">N/A</td>\n";
                        acknowTable = acknowTable + "</tr>\n";
                    }
                    else
                    {
                        acknowTable = acknowTable + "<tr bgcolor=\"" + bgDefault + "\">\n";
                        acknowTable = acknowTable + "<td align=\"center\"><a href=\"#\" onclick=\"GetBookingDetail(" + item.BookingProductID + ");return false;\">" + item.BookingID + "</a></td>\n";
                        if (string.IsNullOrEmpty(item.AcknowledgeID))
                        {
                            acknowTable = acknowTable + "<td align=\"center\">N/A</td>\n";
                        }
                        else
                        {
                            acknowTable = acknowTable + "<td align=\"center\">" + item.AcknowledgeID + "</td>\n";
                        }

                        acknowTable = acknowTable + "<td>" + item.StaffTitle + "</td>\n";
                        acknowTable = acknowTable + "<td>" + item.Fullname + "</td>\n";
                        acknowTable = acknowTable + "<td align=\"right\">" + item.DateSubmit.ToString("ddd, dd MMM yyyy") + "</td>\n";
                        acknowTable = acknowTable + "<td align=\"right\">" + item.DateCheckIn.ToString("ddd, dd MMM yyyy") + "<br/>\n";
                        acknowTable = acknowTable + item.DateCheckOut.ToString("ddd, dd MMM yyyy") + "</td>\n";
                        acknowTable = acknowTable + "<td align=\"right\">" + item.DateConfirm.ToString("ddd, dd MMM yyyy") + "</td>\n";
                        acknowTable = acknowTable + "<td align=\"center\">" + displayBookingStatus(item.Status) + "</td>\n";
                        acknowTable = acknowTable + "</tr>\n";
                    }

                    status_temp = item.BookingExtranetStatus;
                    countRow = countRow + 1;
                }
                if (!string.IsNullOrEmpty(acknowTable))
                {
                    acknowTable = acknowTable + "</table>\n";
                    if (statusID == 1 || statusID == 3)
                    {
                        acknowTable = acknowTable + "<tr><td colspan=\"8\" align=\"center\">";

                        if (statusID == 1)
                        {
                            acknowTable = acknowTable + "<input type=\"hidden\" name=\"status_ack\" value=\"2\">\n";
                        }
                        else
                        {
                            acknowTable = acknowTable + "<input type=\"hidden\" name=\"status_ack\" value=\"4\">\n";
                        }

                        acknowTable = acknowTable + "<input type=\"button\" name=\"btnSave\" value=\"Save\" onClick=\"saveBooking()\"></td></tr>\n";
                        acknowTable = acknowTable + "</form>\n";
                    }
                }
                else
                {
                    acknowTable = "<div class=\"no_result\">No result click <a href=\"acknow.html\">back</a> to search again</div>";
                }
            }
            return acknowTable;
        }
    }
}