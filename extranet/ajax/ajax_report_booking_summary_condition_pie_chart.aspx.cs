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
    public partial class admin_ajax_report_booking_summary_condition_pie_chart : Hotels2BasePageExtra_Ajax
    {
       


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

            
            //try
            //{
                BookingReport_booking cBookingstat = new BookingReport_booking();
                ProductConditionExtra cConditionExtra = new ProductConditionExtra();
                Option cOption = new Option();
                IList<object> iListOption = cOption.GetProductOptionByProductId_RoomOnlyExtranet(this.CurrentProductActiveExtra, this.CurrentSupplierId);

                DateTime dDAteStart = new DateTime();
                DateTime dDAteENd = new DateTime();


                int SummaryTotal = 0;


                


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

                string TotalSummaryTitle = string.Empty;

                string Type = string.Empty;
                int SumRoomType = iListOption.Count();

                IList<object> TotalBooking = null;
                switch (bytChartName)
                {
                    // All Booking 
                    case 1:
                        
                        Type = "All Booking ";
                        SummaryTotal = cBookingstat.CountBookingAll(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDAteStart,
                           dDAteENd);
                        TotalBooking = cBookingstat.getBookingAll(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);
                        break;
                    // Booking Completed
                    case 2:
                        
                        Type = "No. Of Booking";
                        SummaryTotal = cBookingstat.CountBookingCompleted(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDAteStart, dDAteENd);

                        TotalBooking = cBookingstat.getBookingCompleted(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                                    dDAteStart, dDAteENd);
                        break;
                    // Room Night BookingDate
                    case 3:
                        Type = "Room Night (Booking Date)";
                        SummaryTotal = cBookingstat.CountRoomNight_BookingDate(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                           dDAteStart, dDAteENd);

                        TotalBooking = cBookingstat.GetRoomNight_BookingDate(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                                   dDAteStart, dDAteENd);
                        break;
                    // Room Night CheckIn Date
                    case 4:
                        Type = "Room Night (Check in date)";
                        SummaryTotal = cBookingstat.CountRoomNight_CheckInDateByPeriod(this.CurrentProductActiveExtra,
                            this.CurrentSupplierId, dDAteStart, dDAteENd);

                        TotalBooking = cBookingstat.GEtRoomNight_CheckInDateByPeriod(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                                    dDAteStart, dDAteENd);
                        break;
                }

                
                
                foreach (Option option in iListOption)
                {
                    int RoomSum = 0;
                    string ConditionDetail = string.Empty;
                    List<object> ConditionExtraList = cConditionExtra.getConditionListByOptionId(option.OptionID, 1);
                    if (ConditionExtraList.Count() > 0)
                    {

                        foreach (ProductConditionExtra conditionList in ConditionExtraList)
                        {


                            int Total = 0;
                            if (TotalBooking.Count() > 0)
                            {
                                if (bytChartName == 1 || bytChartName == 2)
                                {
                                    foreach (BookingReport_booking allbookingItem in TotalBooking)
                                    {
                                        if (conditionList.ConditionId == allbookingItem.ConditionId)
                                            Total = Total + 1;
                                    }
                                    
                                }

                                else if (bytChartName == 3 || bytChartName == 4)
                                {

                                    foreach (BookingReport_booking allbookingItem in TotalBooking)
                                    {
                                        if (conditionList.ConditionId == allbookingItem.ConditionId)
                                            Total = Total + allbookingItem.TotalPeriodNightStay_Real;
                                    }
                                    
                                }

                                RoomSum = RoomSum + Total;
                                ConditionDetail = ConditionDetail + conditionList.Title + Hotels2String.AppendConditionDetailExtraNet(conditionList.NumAult, conditionList.Breakfast) + "/" + Total + "!";

                            }

                           


                        }
                    }

                    result = result + option.Title + "*" + RoomSum + "*" + ConditionDetail.Hotels2RightCrl(1) + "%";
                }


            //}
            //catch (Exception ex)
            //{
            //    Response.Write("error: " + ex.Message);
            //    Response.End();
            //}


            return result.Hotels2RightCrl(1).Replace("&nbsp;"," ");


        }
    }
}