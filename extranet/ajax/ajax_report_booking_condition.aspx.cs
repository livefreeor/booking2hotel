using System;
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
    public partial class admin_ajax_report_booking_condition : Hotels2BasePageExtra_Ajax
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

                Response.Write(BookingCondition());
                Response.End();
            }
        }


        public string BookingCondition()
        {
            StringBuilder result = new StringBuilder();

            try
            {
                //BookingReport_condition_roomnight cBookingreportCondition = new BookingReport_condition_roomnight();
                BookingReport_booking cBookingstat = new BookingReport_booking();
                ProductConditionExtra cConditionExtra = new ProductConditionExtra();
                Option cOption = new Option();
                IList<object> iListOption = cOption.GetProductOptionByProductId_RoomOnlyExtranet(this.CurrentProductActiveExtra, this.CurrentSupplierId);
               
                DateTime dDAteStart = new DateTime();
                DateTime dDAteENd = new DateTime();
                int SummaryTotal = 0;
                string StringSummaryTotal = "";
                IList<object> TotalBooking = null;

                
                
                byte bytChart_Type = byte.Parse(Request.Form["hd_chart_type"]);
                byte bytChartName = byte.Parse(Request.Form["hd_chart_name"]);
                
                switch (bytChart_Type)
                {
                    case 1:
                        
                        dDAteStart = Request.Form["hd_date_start"].Hotels2DateSplitYear("-");
                        dDAteENd = Request.Form["hd_date_end"].Hotels2DateSplitYear("-");
                        //cBookingstat.CountRoomNight_BookingDate(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                        //    dDAteStart, dDAteENd);
                        
                        switch (bytChartName)
                        {
                                // All Booking 
                            case 1:
                                TotalBooking = cBookingstat.getBookingAll(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);
                               
                                SummaryTotal = cBookingstat.CountBookingAll(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDAteStart,
                           dDAteENd);
                                
                                break;
                                // Booking Completed
                             case 2:
                                SummaryTotal = cBookingstat.CountBookingCompleted(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDAteStart, dDAteENd);
                                TotalBooking = cBookingstat.getBookingCompleted(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);
                                
                                break;
                                // Room Night BookingDate
                            case 3:
                                SummaryTotal = cBookingstat.CountRoomNight_BookingDate(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);
                                TotalBooking = cBookingstat.GetRoomNight_BookingDate(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);
                                break;
                                // Room Night CheckIn Date
                            case 4 :
                                SummaryTotal = cBookingstat.CountRoomNight_CheckInDateByPeriod(this.CurrentProductActiveExtra,
                           this.CurrentSupplierId, dDAteStart, dDAteENd);
                                TotalBooking = cBookingstat.GEtRoomNight_CheckInDateByPeriod(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);
                                break;
                        }
                        break;
                    case 2:
                        int intYear = int.Parse(Request.Form["hd_date_month_year"]);
                        dDAteStart = new DateTime(intYear,1,1);
                        dDAteENd = new DateTime(intYear, 12, 31);
                        switch (bytChartName)
                        {
                            // All Booking 
                            case 1:
                                TotalBooking = cBookingstat.getBookingAll(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                           dDAteStart, dDAteENd);
                                SummaryTotal = cBookingstat.CountBookingAll(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDAteStart, dDAteENd);
                                
                                break;
                            // Booking Completed
                            case 2:
                                SummaryTotal = cBookingstat.CountBookingCompleted(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDAteStart, dDAteENd);
                                TotalBooking = cBookingstat.getBookingCompleted(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);
                                break;
                            // Room Night BookingDate
                            case 3:
                                SummaryTotal = cBookingstat.CountRoomNight_BookingDate(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);
                                TotalBooking = cBookingstat.GetRoomNight_BookingDate(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);
                                break;
                            // Room Night CheckIn Date
                            case 4:
                                SummaryTotal = cBookingstat.CountRoomNight_CheckInDateByPeriod(this.CurrentProductActiveExtra,
                            this.CurrentSupplierId, dDAteStart, dDAteENd);
                                TotalBooking = cBookingstat.GEtRoomNight_CheckInDateByPeriod(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                             dDAteStart, dDAteENd);
                                break;
                        }
                        break;
                    case 3:
                        int intYearNow = DateTime.Now.Hotels2ThaiDateTime().Year;
                        dDAteStart = new DateTime(2011, 1, 1);
                        dDAteENd = new DateTime((intYearNow + 4), 12, 31);

                        switch (bytChartName)
                        {
                            // All Booking 
                            case 1:
                                SummaryTotal = cBookingstat.CountBookingAll(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDAteStart, dDAteENd);
                                TotalBooking = cBookingstat.getBookingAll(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);
                                break;
                            // Booking Completed
                            case 2:
                                SummaryTotal = cBookingstat.CountBookingCompleted(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDAteStart, dDAteENd);
                               TotalBooking = cBookingstat.getBookingCompleted(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);
                                break;
                                
                            // Room Night BookingDate
                            case 3:
                                SummaryTotal = cBookingstat.CountRoomNight_BookingDate(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);
                                TotalBooking = cBookingstat.GetRoomNight_BookingDate(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);
                                break;
                            // Room Night CheckIn Date
                            case 4:
                                SummaryTotal = cBookingstat.CountRoomNight_CheckInDateByPeriod(this.CurrentProductActiveExtra,
                            this.CurrentSupplierId, dDAteStart, dDAteENd);
                                TotalBooking = cBookingstat.GEtRoomNight_CheckInDateByPeriod(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);
                                break;
                        }
                        break;
                }


                switch (bytChartName)
                {
                    // All Booking 
                    case 1:
                       
                        StringSummaryTotal = SummaryTotal + "<label> Booking</label>";
                        break;
                    // Booking Completed
                    case 2:
                       
                        StringSummaryTotal = SummaryTotal + "<label> Booking</label>";
                        break;
                    // Room Night BookingDate
                    case 3:
                       
                        StringSummaryTotal = SummaryTotal + "<label> Room Night(s)</label>";
                        break;
                    // Room Night CheckIn Date
                    case 4:
                       
                        StringSummaryTotal = SummaryTotal + "<label> Room Night(s)</label>";
                        break;
                }

                result.Append("<div id=\"condition_box\" >");
                result.Append("<p class=\"summary_total\">"+ StringSummaryTotal + "</p>");
                result.Append("<table width=\"100%\">");


                foreach (Option option in iListOption)
                {
                    List<object> ConditionExtraList = cConditionExtra.getConditionListByOptionId(option.OptionID, 1);


                    
                    if (ConditionExtraList.Count() > 0)
                    {
                        result.Append("<tr>");
                        result.Append("<td >");
                       
                        result.Append("<p class=\"room_title\">");
                        result.Append("<img src=\"http://www.hotels2thailand.com/images_extra/dot_yellow.png\" /><label >" + option.Title + "</label></p>");
                        result.Append("<div id=\"condition_list" + option.OptionID + "\" class=\"condition_select_list\"  >");
                        


                        result.Append("<table>");

                        foreach (ProductConditionExtra conditionList in ConditionExtraList)
                        {

                            result.Append("<tr><td><img src=\"http://www.hotels2thailand.com/images/ico-square-small.png\" />");
                            result.Append("<label>" + conditionList.Title + Hotels2String.AppendConditionDetailExtraNet(conditionList.NumAult, conditionList.Breakfast) + "</label>");
                            result.Append("</td>");
                            int Total = 0;

                            if (TotalBooking.Count() > 0)
                            {
                                if (bytChartName == 1 || bytChartName == 2)
                                    Total = TotalBooking.Where(co => (int)co.GetType().GetProperty("ConditionId").GetValue(co, null) == conditionList.ConditionId).Count();
                                else if (bytChartName == 3 || bytChartName == 4)
                                {
                                    Total = (int)TotalBooking.Where(co => (int)co.GetType().GetProperty("ConditionId").GetValue(co, null) == conditionList.ConditionId)
                                        .Sum(t => (int)t.GetType().GetProperty("TotalPeriodNightStay_Real").GetValue(t, null));
                                }

                            }
                                
                           
                            result.Append("<td class=\"total_result\">" + Total + "</td>");

                            result.Append("</tr>");

                        }
                    }
                    result.Append("</table>");
                    
                    result.Append("</div>");
                    
                    result.Append("</td>");
                    result.Append("</tr>");
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