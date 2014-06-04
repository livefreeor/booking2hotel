using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Report;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Production;
using Hotels2thailand;
using System.Reflection;
using System.Text;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_report_booking_summary_country_detail : Hotels2BasePageExtra_Ajax
    {
       


        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
            {
                //Response.Write("HHHH");
                //Response.End();
                Response.Write(BookingreportSummary());
                Response.End();
            }
        }


        public string BookingreportSummary()
        {
            StringBuilder result = new StringBuilder();
            
            try
            {
                BookingReport_booking_country cBookingstat = new BookingReport_booking_country();
                
                DateTime dDAteStart = new DateTime();
                DateTime dDAteENd = new DateTime();
                
                IList<object> TotalBooking_ALl = null;
                IList<object> TotalBooking_COmpleted = null;
                IList<object> TotalRoomnight_BookingDate = null;
                IList<object> TotalRoomnight_CheckinDate = null;

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

                TotalBooking_ALl = cBookingstat.getBookingAll_Country(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                    dDAteStart, dDAteENd);

                TotalBooking_COmpleted = cBookingstat.getBookingCompleted_Country(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                    dDAteStart, dDAteENd);

                TotalRoomnight_BookingDate = cBookingstat.GetCountryRoomNight_BookingDate(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                    dDAteStart, dDAteENd);

                TotalRoomnight_CheckinDate = cBookingstat.GetBookingCountryRoomNight_CheckInDateByPeriod(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                   dDAteStart, dDAteENd);

                string TotalSummaryTitle = string.Empty;
                int SumType = 0;
                string Type = string.Empty;
                int intSumCountry = 0;

                int intSumAllBooking = TotalBooking_ALl.Sum(c => (int)c.GetType().GetProperty("Total").GetValue(c, null));
                int intSumBookingCompleted = TotalBooking_COmpleted.Sum(c => (int)c.GetType().GetProperty("Total").GetValue(c, null));
                int intSumRoomnightBookingDate = TotalRoomnight_BookingDate.Sum(c => (int)c.GetType().GetProperty("Total").GetValue(c, null));
                int intSumRoonightCheckinDate = TotalRoomnight_CheckinDate.Sum(c => (int)c.GetType().GetProperty("Total").GetValue(c, null));

                switch (bytChartName)
                {
                    // All Booking 
                    case 1:
                        Type = "All Booking ";
                        SumType = intSumAllBooking;
                        intSumCountry = TotalBooking_ALl.Where(c => (int)c.GetType().GetProperty("Total").GetValue(c, null) > 0).Count();
                        break;
                    // Booking Completed
                    case 2:
                        Type = "No. Of Booking";
                        SumType = intSumBookingCompleted;
                        intSumCountry = TotalBooking_COmpleted.Where(c => (int)c.GetType().GetProperty("Total").GetValue(c, null) > 0).Count();
                        break;
                    // Room Night BookingDate
                    case 3:
                        Type = "Room Night (Booking Date)";
                        SumType = intSumRoomnightBookingDate;
                        intSumCountry = TotalRoomnight_BookingDate.Where(c => (int)c.GetType().GetProperty("Total").GetValue(c, null) > 0).Count();
                        break;
                    // Room Night CheckIn Date
                    case 4:
                        Type = "Room Night (Check in date)";
                        SumType = intSumRoonightCheckinDate;
                        intSumCountry = TotalRoomnight_CheckinDate.Where(c => (int)c.GetType().GetProperty("Total").GetValue(c, null) > 0).Count();
                        break;
                }



                
                

               Country cCountry = new Country();
                IList<ArrayList> iArrList = new List<ArrayList>();


                foreach (KeyValuePair<string, string> country in cCountry.GetCountryAll())
               {
                   
                   int intBookingall = 0;
                   int intBookingCom = 0;
                   int intRoomnightBooking = 0;
                   int intRoomnightCheck = 0;
                   foreach (BookingReport_booking_country AllbookingResult in TotalBooking_ALl)
                   {
                       if (AllbookingResult.Total > 0)
                       {
                           if (AllbookingResult.CountryId.ToString() == country.Key && AllbookingResult.Total > 0)
                           {
                               intBookingall = intBookingall + AllbookingResult.Total;
                           }
                       }
                   }

                   foreach (BookingReport_booking_country BookingCompletedResult in TotalBooking_COmpleted)
                   {
                       if (BookingCompletedResult.Total > 0)
                       {
                           if (BookingCompletedResult.CountryId.ToString() == country.Key && BookingCompletedResult.Total > 0)
                           {
                               intBookingCom = intBookingCom + BookingCompletedResult.Total;
                           }
                       }
                   }

                   foreach (BookingReport_booking_country RoomNightBookindateResult in TotalRoomnight_BookingDate)
                   {
                       if (RoomNightBookindateResult.Total > 0)
                       {
                           if (RoomNightBookindateResult.CountryId.ToString() == country.Key && RoomNightBookindateResult.Total > 0)
                           {
                               intRoomnightBooking = intRoomnightBooking + RoomNightBookindateResult.Total;
                           }
                       }
                   }

                   foreach (BookingReport_booking_country RoomNightCheckinResult in TotalRoomnight_CheckinDate)
                   {
                       if (RoomNightCheckinResult.Total > 0)
                       {
                           if (RoomNightCheckinResult.CountryId.ToString() == country.Key && RoomNightCheckinResult.Total > 0)
                           {
                               intRoomnightCheck = intRoomnightCheck + RoomNightCheckinResult.Total;
                           }
                       }
                   }

                   if (intBookingall > 0 || intBookingCom > 0 || intRoomnightBooking > 0 || intRoomnightCheck > 0)
                   {
                       ArrayList arrList = new ArrayList();
                       arrList.Add(country.Key);
                       arrList.Add(country.Value);
                       arrList.Add(intBookingall);
                       arrList.Add(intBookingCom);
                       arrList.Add(intRoomnightBooking);
                       arrList.Add(intRoomnightCheck);

                       iArrList.Add(arrList);

                   }
                   
               }


               TotalSummaryTitle = SumType + " " + Type + " came from " + intSumCountry + " countries/territories";

               result.Append("");
               result.Append("");
               result.Append("");

               result.Append("<p class=\"title_total_sum\">" + TotalSummaryTitle + "</p>");
               result.Append("<div id=\"main_block_country\">");

               result.Append("<div id=\"block_top\">");
               result.Append("<div>");
               result.Append("<p class=\"block_top_name\">All Booking</p>");
               result.Append("<p class=\"block_top_result\">" + intSumAllBooking + "</p>");
               result.Append("</div>");
               result.Append("<div>");
               result.Append("<p class=\"block_top_name\">No. Of Booking</p>");
               result.Append("<p class=\"block_top_result\">"+intSumBookingCompleted+"</p>");
               result.Append("</div>");
               result.Append("<div>");
               result.Append("<p class=\"block_top_name\">Room Night (Booking Date)</p>");
               result.Append("<p class=\"block_top_result\">"+intSumRoomnightBookingDate+"</p>");
               result.Append("</div>");
               result.Append("<div>");
               result.Append("<p class=\"block_top_name\">Room Night (Check in date)</p>");
               result.Append("<p class=\"block_top_result\">"+intSumRoonightCheckinDate+"</p>");
               result.Append("</div>");

               result.Append("</div>");
               result.Append("<div style=\" clear:both\"> </div>");
               result.Append("<div id=\"block_content\">");
               result.Append("<table cellpadding=\"0\" cellspacing=\"2\">");
               result.Append("<tr>");
               result.Append("<th style=\"width:3%\"></th><th style=\"width:25%\">Country</th><th style=\"width:15%; text-align:right\">All Booking</th><th style=\"width:15%;text-align:right\">No. Of Booking</th>");
               result.Append("<th style=\"width:20%;text-align:right\">Room Night (Booking Date)</th><th style=\"width:20%;text-align:right\">Room Night (Check in date)</th>");
               result.Append("</tr>");
               if (iArrList.Count > 0)
               {
                   int CountNum = 1;
                   string BG = "bgcolor=\"#ffffff\"";

                   foreach (ArrayList bookingResult in iArrList)
                   {

                    if(CountNum%2 == 0)
                        BG = "bgcolor=\"#f9f9f9\"";
                       else
                        BG = "bgcolor=\"#ffffff\"";

                       result.Append("<tr "+BG+">");
                       result.Append("<td>" + CountNum + ".</td><td style=\"text-align:left; padding-left:7px;\">" + bookingResult[1] + "</td><td style=\"text-align:right\">" + bookingResult[2] + "</td>");
                       result.Append("<td style=\"text-align:right\">" + bookingResult[3] + "</td><td style=\"text-align:right\">" + bookingResult[4] + "</td><td style=\"text-align:right\">" + bookingResult[5] + "</td>");
                       result.Append("</tr>");

                       CountNum = CountNum + 1;
                   }

               }
               else
               {
                  
                   result.Append("<tr bgcolor=\"#ffffff\">");
                   result.Append("<td colspan=\"6\" style=\"text-align:center;\">There is no data for this view.</td>");
                   result.Append("</tr>");
               }
               

               result.Append("</table>");
               result.Append("</div>");
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
}