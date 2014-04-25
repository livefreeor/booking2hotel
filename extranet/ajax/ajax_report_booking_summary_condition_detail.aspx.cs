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
    public partial class admin_ajax_report_booking_summary_condition_detail : Hotels2BasePageExtra_Ajax
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
                BookingReport_booking cBookingstat = new BookingReport_booking();
                ProductConditionExtra cConditionExtra = new ProductConditionExtra();
                Option cOption = new Option();
                IList<object> iListOption = cOption.GetProductOptionByProductId_RoomOnlyExtranet(this.CurrentProductActiveExtra, this.CurrentSupplierId);
                
                DateTime dDAteStart = new DateTime();
                DateTime dDAteENd = new DateTime();
                

                int SummaryTotal = 0;
                

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

                TotalBooking_ALl = cBookingstat.getBookingAll(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);
                TotalBooking_COmpleted = cBookingstat.getBookingCompleted(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);
                TotalRoomnight_BookingDate = cBookingstat.GetRoomNight_BookingDate(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                           dDAteStart, dDAteENd);
                TotalRoomnight_CheckinDate = cBookingstat.GEtRoomNight_CheckInDateByPeriod(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);
                
                string TotalSummaryTitle = string.Empty;
                
                string Type = string.Empty;
                int SumRoomType = iListOption.Count();

                
                switch (bytChartName)
                {
                    // All Booking 
                    case 1:
                        Type = "All Booking ";
                        SummaryTotal = cBookingstat.CountBookingAll(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDAteStart,
                           dDAteENd);
                        break;
                    // Booking Completed
                    case 2:
                        Type = "No. Of Booking";
                      SummaryTotal = cBookingstat.CountBookingCompleted(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDAteStart, dDAteENd);
                        break;
                    // Room Night BookingDate
                    case 3:
                        Type = "Room Night (Booking Date)";
                         SummaryTotal = cBookingstat.CountRoomNight_BookingDate(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);
                        break;
                    // Room Night CheckIn Date
                    case 4:
                        Type = "Room Night (Check in date)";
                       SummaryTotal = cBookingstat.CountRoomNight_CheckInDateByPeriod(this.CurrentProductActiveExtra,
                           this.CurrentSupplierId, dDAteStart, dDAteENd);
                       break;
                }

                

              TotalSummaryTitle = SummaryTotal + " " + Type + " from " + SumRoomType + " Room Type";

               
               result.Append("<p class=\"title_total_sum\">" + TotalSummaryTitle + "</p>");

               result.Append("<div id=\"booking_condition_block\">");
               result.Append("<table cellpadding=\"0\" cellspacing=\"2\">");
               result.Append("<tr><th>Room&Condition</th><th>All Booking</th><th>No. Of Booking</th><th>Room Night (Booking Date)</th><th>Room Night (Check in date)</th></tr>");
               foreach (Option option in iListOption)
               {
                   result.Append("<tr bgcolor=\"#f9f9f9\"><td  colspan=\"5\" style=\"font-weight:bold;\">" + option.Title + "</td></tr>");
                   List<object> ConditionExtraList = cConditionExtra.getConditionListByOptionId(option.OptionID, 1);
                   if (ConditionExtraList.Count() > 0)
                   {
                       
                       foreach (ProductConditionExtra conditionList in ConditionExtraList)
                       {

                           result.Append("<tr bgcolor=\"#ffffff\">");
                           result.Append("<td style=\"text-align:left; padding-left:7px;\">");
                           result.Append("<img src=\"http://www.hotels2thailand.com/images/ico-square-small.png\" />");
                           result.Append(conditionList.Title + Hotels2String.AppendConditionDetailExtraNet(conditionList.NumAult, conditionList.Breakfast));
                           result.Append("</td>");

                           
                           
                           int intTotalBookingConditionAll = 0;
                           int intTotalBookingCondition_Completed = 0;
                           int intTotalRN_condition_bookingdate = 0;
                           int intTotalRn_condition_checkin_date = 0;


                           if (TotalBooking_ALl.Count() > 0)
                               intTotalBookingConditionAll = TotalBooking_ALl.Where(co => (int)co.GetType().GetProperty("ConditionId").GetValue(co, null) == conditionList.ConditionId).Count();


                           if (TotalBooking_COmpleted.Count() > 0)
                               intTotalBookingCondition_Completed = TotalBooking_COmpleted.Where(co => (int)co.GetType().GetProperty("ConditionId").GetValue(co, null) == conditionList.ConditionId).Count();


                           if (TotalRoomnight_BookingDate.Count() > 0)
                               intTotalRN_condition_bookingdate = TotalRoomnight_BookingDate.Where(co => (int)co.GetType().GetProperty("ConditionId").GetValue(co, null) == conditionList.ConditionId).Sum(t => (int)t.GetType().GetProperty("TotalPeriodNightStay_Real").GetValue(t, null));
                           if (TotalRoomnight_CheckinDate.Count() > 0)
                               intTotalRn_condition_checkin_date = TotalRoomnight_CheckinDate.Where(co => (int)co.GetType().GetProperty("ConditionId").GetValue(co, null) == conditionList.ConditionId).Sum(t => (int)t.GetType().GetProperty("TotalPeriodNightStay_Real").GetValue(t, null));

                           result.Append("<td style=\"text-align:right\">" + intTotalBookingConditionAll + "</td>");
                           result.Append("<td style=\"text-align:right\">" + intTotalBookingCondition_Completed + "</td>");
                           result.Append("<td style=\"text-align:right\">" + intTotalRN_condition_bookingdate + "</td>");
                           result.Append("<td style=\"text-align:right\">" + intTotalRn_condition_checkin_date + "</td>");

                           result.Append("</tr>");

                       }
                   }
               }


               result.Append("</table>");

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