using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Report;
using Hotels2thailand.ProductOption;
using Hotels2thailand;
using System.Reflection;
using System.Text;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_report_booking_summary_geo_map : Hotels2BasePageExtra_Ajax
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
            string result = string.Empty;
            
            try
            {
                BookingReport_booking_country cBookingstat = new BookingReport_booking_country();
                
                DateTime dDAteStart = new DateTime();
                DateTime dDAteENd = new DateTime();
                
                IList<object> TotalBooking = null;


                byte bytChart_Type = byte.Parse(Request.Form["hd_chart_type"]);
                byte bytChartName = byte.Parse(Request.Form["hd_chart_name"]);


                switch (bytChart_Type)
                {
                    case 1:

                        dDAteStart = Request.Form["hd_date_start"].Hotels2DateSplitYear("-");
                        dDAteENd = Request.Form["hd_date_end"].Hotels2DateSplitYear("-");
                        
                        break;
                    case 2:
                        int intYear = int.Parse(Request.Form["hd_date_month_year"]);
                        dDAteStart = new DateTime(intYear, 1, 1);
                        dDAteENd = new DateTime(intYear, 12, 31);
                        
                        break;
                    case 3:
                        int intYearNow = DateTime.Now.Hotels2ThaiDateTime().Year;
                        dDAteStart = new DateTime(2011, 1, 1);
                        dDAteENd = new DateTime((intYearNow + 4), 12, 31);

                        break;
                }


                switch (bytChartName)
                {
                    // All Booking 
                    case 1:
                        TotalBooking = cBookingstat.getBookingAll_Country(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                    dDAteStart, dDAteENd);

                        break;
                    // Booking Completed
                    case 2:

                        TotalBooking = cBookingstat.getBookingCompleted_Country(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                    dDAteStart, dDAteENd);

                        break;
                    // Room Night BookingDate
                    case 3:

                        TotalBooking = cBookingstat.GetCountryRoomNight_BookingDate(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                    dDAteStart, dDAteENd);

                        break;
                    // Room Night CheckIn Date
                    case 4:

                        TotalBooking = cBookingstat.GetBookingCountryRoomNight_CheckInDateByPeriod(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                    dDAteStart, dDAteENd);


                        break;
                }

                foreach(BookingReport_booking_country bookingResult in TotalBooking.Where(c=>(int)c.GetType().GetProperty("Total").GetValue(c,null) > 0))
                {
                    //if (!string.IsNullOrEmpty(bookingResult.CountryCode))
                    //    result = result + bookingResult.CountryCode.ToString() + ",";
                    //else
                    result = result + bookingResult.CountryTitle + ",";

                    result = result + bookingResult.Total + "/";
                }
                
                
            }
            catch (Exception ex)
            {
                Response.Write("error: " + ex.Message);
                Response.End();
            }
            return result.Hotels2RightCrl(1);


        }
    }
}