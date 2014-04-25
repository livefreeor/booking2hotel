using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Report;
using Hotels2thailand;
using System.Reflection;
using System.Text;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_report_booking_summary : Hotels2BasePageExtra_Ajax
    {
        public string qDateStart
        {
            get { return Request.QueryString["date_start"]; }
        }

        public string qDateEnd
        {
            get { return Request.QueryString["date_end"]; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
            {
                
                Response.Write(BookingreportSummary());
                Response.End();
            }
        }


        public string BookingreportSummary()
        {
            StringBuilder result = new StringBuilder();

            try
            {
                BookingReport_booking cBookingstat = new BookingReport_booking();
                DateTime dDAteStart = new DateTime();
                DateTime dDAteENd = new DateTime();

                int CountBookingConpleted = 0;
                int CountBookingAll = 0;
                int CountRoomnightBookingDate = 0;
                int CountRoomnight_checkingDate = 0;
                byte bytChart_Type = byte.Parse(Request.Form["hd_chart_type"]);
                
                switch (bytChart_Type)
                {
                    case 1:
                        dDAteStart = Request.Form["hd_date_start"].Hotels2DateSplitYear("-");
                        dDAteENd = Request.Form["hd_date_end"].Hotels2DateSplitYear("-");
                        
                        CountBookingConpleted = cBookingstat.CountBookingCompleted(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDAteStart, dDAteENd);
                        CountBookingAll = cBookingstat.CountBookingAll(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDAteStart, 
                            dDAteENd);

                        CountRoomnightBookingDate = cBookingstat.CountRoomNight_BookingDate(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);
                        CountRoomnight_checkingDate = cBookingstat.CountRoomNight_CheckInDateByPeriod(this.CurrentProductActiveExtra,
                            this.CurrentSupplierId, dDAteStart, dDAteENd);
                        break;
                    case 2:
                        int intYear = int.Parse(Request.Form["hd_date_month_year"]);
                        dDAteStart = new DateTime(intYear,1,1);
                        dDAteENd = new DateTime(intYear, 12, 31);
                        
                        CountBookingConpleted = cBookingstat.CountBookingCompleted(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDAteStart, dDAteENd);
                        CountBookingAll = cBookingstat.CountBookingAll(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDAteStart, dDAteENd);

                        CountRoomnightBookingDate = cBookingstat.CountRoomNight_BookingDate(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);
                        CountRoomnight_checkingDate = cBookingstat.CountRoomNight_CheckInDateByPeriod(this.CurrentProductActiveExtra, 
                            this.CurrentSupplierId, dDAteStart, dDAteENd);
                        break;
                    case 3:
                        int intYearNow = DateTime.Now.Hotels2ThaiDateTime().Year;
                        dDAteStart = new DateTime(2011, 1, 1);
                        dDAteENd = new DateTime((intYearNow + 4), 12, 31);
                        //dDAteStart = Request.Form["hd_date_start"].Hotels2DateSplitYear("-");
                        //dDAteENd = Request.Form["hd_date_end"].Hotels2DateSplitYear("-");

                        CountBookingConpleted = cBookingstat.CountBookingCompleted(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDAteStart, dDAteENd);
                        CountBookingAll = cBookingstat.CountBookingAll(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDAteStart, dDAteENd);

                        CountRoomnightBookingDate = cBookingstat.CountRoomNight_BookingDate(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);
                        CountRoomnight_checkingDate = cBookingstat.CountRoomNight_CheckInDateByPeriod(this.CurrentProductActiveExtra,
                            this.CurrentSupplierId, dDAteStart, dDAteENd);
                        break;
                }

                //DateTime dDAteStart = new DateTime(2011, 12, 1);
                //DateTime dDAteENd = new DateTime(2011, 12, 31);
                string QueryStringAppend = "?pdr=" + Request.Form["hd_date_start"] + "/" + Request.Form["hd_date_end"] 
                    + "&pdm=" + Request.Form["hd_date_month_year"] 
                    + "&ct=" + Request.Form["hd_chart_type"]
                    + this.AppendCurrentQueryString();
                

                result.Append("<div><table><tr>");
                result.Append("<td valign=\"top\"><img src=\"http://hotels2thailand.com/images_extra/chart.png\" /></td>");
                result.Append("<td valign=\"bottom\" style=\" width:60px; text-align:center;\"><span>" + CountBookingAll + "</span></td>");
                result.Append("<td  valign=\"bottom\"><a href=\"booking_report_detail.aspx" + QueryStringAppend + "&cn=1\">All Booking </a></td>");
                result.Append("</tr></table></div>");
                result.Append("<div><table><tr>");
                result.Append("<td valign=\"top\"><img src=\"http://hotels2thailand.com/images_extra/chart.png\" /></td>");
                result.Append("<td valign=\"bottom\" style=\" width:60px;text-align:center;\"><span>" + CountBookingConpleted + "</span></td>");
                result.Append("<td  valign=\"bottom\"><a href=\"booking_report_detail.aspx" + QueryStringAppend + "&cn=2\">No. of Booking</a></td>");
                result.Append("</tr></table></div>");
                result.Append("<div><table><tr><td valign=\"top\"><img src=\"http://hotels2thailand.com/images_extra/chart.png\" /></td>");
                result.Append("<td valign=\"bottom\" style=\" width:60px;text-align:center;\"><span></asp:Literal>" + CountRoomnightBookingDate + "</span></td>");
                result.Append("<td  valign=\"bottom\"><a href=\"booking_report_detail.aspx" + QueryStringAppend + "&cn=3\">Room Night(s) Booking date</a></td>");
                result.Append("</tr></table></div>");
                result.Append("<div><table><tr><td valign=\"top\"><img src=\"http://hotels2thailand.com/images_extra/chart.png\" /></td>");
                result.Append("<td valign=\"bottom\" style=\" width:60px;text-align:center;\"><span></asp:Literal>" + CountRoomnight_checkingDate + "</span></td>");
                result.Append("<td  valign=\"bottom\"><a href=\"booking_report_detail.aspx" + QueryStringAppend + "&cn=4\">Room Night(s) Stay Date</a></td></tr></table></div>");

            }
            catch (Exception ex)
            {
                Response.Write("error: " + ex.Message);
                Response.End();
            }
            return result.ToString();


        }
    }
}