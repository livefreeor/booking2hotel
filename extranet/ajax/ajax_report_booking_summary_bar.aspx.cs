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
    public partial class admin_ajax_report_booking_summary_bar : Hotels2BasePageExtra_Ajax
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
            StringBuilder resultInside = new StringBuilder();
            try
            {
                BookingReport_booking cBookingstat = new BookingReport_booking();
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
                        

                        switch (bytChartName)
                        {
                            // All Booking 
                            case 1:
                                TotalBooking = cBookingstat.getBookingAll(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);

                                SummaryTotal = cBookingstat.CountBookingAll(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDAteStart,
                           dDAteENd);

                                
                                DateTime dDatecurrent = DateTime.Now;
                                int DateDiff = dDAteENd.Subtract(dDAteStart).Days;
                                int intCountTotal = 0;
                                double Percent = 0;

                                for (int days = 0; days <= DateDiff; days++)
                                {

                                   
                                    dDatecurrent = dDAteStart.AddDays(days);
                                    foreach (BookingReport_booking BarList in TotalBooking)
                                    {
                                        if (dDatecurrent.Date == BarList.DateSubmit.Date)
                                        {
                                            intCountTotal = intCountTotal + 1;
                                        }
                                    }

                                    if (SummaryTotal > 0)
                                        Percent = Convert.ToDouble(((double)intCountTotal * 100) / (double)SummaryTotal);

                                    resultInside.Append(" <tr class=\"tr_bar\" >");
                                    resultInside.Append("<td class=\"td_bar_left\">");
                                    resultInside.Append(dDatecurrent.ToString("ddd, MMM dd, yyyy"));
                                    resultInside.Append("</td>");
                                    resultInside.Append("<td class=\"td_bar_right\">");
                                    resultInside.Append("<div class=\"bar_color\" style=\" width:" + Percent + "%;\">");
                                    resultInside.Append("<span class=\"bar_sum\" >" + intCountTotal + "(" + Percent.ToString("#.##") + "%)</span></div>");
                                    resultInside.Append("</td>");
                                    resultInside.Append("</tr>");

                                    intCountTotal = 0;
                                }

                                
                                break;
                            // Booking Completed
                            case 2:
                                SummaryTotal = cBookingstat.CountBookingCompleted(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDAteStart, dDAteENd);
                                TotalBooking = cBookingstat.getBookingCompleted(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);
                                dDatecurrent = DateTime.Now;
                                DateDiff = dDAteENd.Subtract(dDAteStart).Days;
                                intCountTotal = 0;
                                Percent = 0;
                                for (int days = 0; days <= DateDiff; days++)
                                {
                                    dDatecurrent = dDAteStart.AddDays(days);
                                    foreach (BookingReport_booking BarList in TotalBooking)
                                    {
                                        if (dDatecurrent.Date == BarList.DateSubmit.Date)
                                        {
                                            intCountTotal = intCountTotal + 1;
                                        }
                                    }

                                    if (SummaryTotal > 0)
                                        Percent = Convert.ToDouble(((double)intCountTotal * 100) / (double)SummaryTotal);
                                    resultInside.Append(" <tr  style=\" height:30px; background-color:#ffffff\">");
                                    resultInside.Append("<td class=\"td_bar_left\">");
                                    resultInside.Append(dDatecurrent.ToString("ddd, MMM dd, yyyy"));
                                    resultInside.Append("</td>");
                                    resultInside.Append("<td class=\"td_bar_right\">");
                                    resultInside.Append("<div class=\"bar_color\" style=\" width:" + Percent + "%;\">");
                                    resultInside.Append("<span class=\"bar_sum\" >" + intCountTotal + "(" + Percent + "%)</span></div>");
                                    resultInside.Append("</td>");
                                    resultInside.Append("</tr>");

                                    intCountTotal = 0;
                                }



                                break;
                            // Room Night BookingDate
                            case 3:
                                SummaryTotal = cBookingstat.CountRoomNight_BookingDate(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);
                                TotalBooking = cBookingstat.GetRoomNight_BookingDate(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);

                                dDatecurrent = DateTime.Now;
                                DateDiff = dDAteENd.Subtract(dDAteStart).Days;
                                intCountTotal = 0;
                                Percent = 0;
                                for (int days = 0; days <= DateDiff; days++)
                                {
                                    dDatecurrent = dDAteStart.AddDays(days);
                                    foreach (BookingReport_booking BarList in TotalBooking)
                                    {
                                        if (dDatecurrent.Date == BarList.DateSubmit.Date)
                                        {
                                            intCountTotal = intCountTotal + BarList.TotalPeriodNightStay_Real;
                                        }
                                    }

                                    if (SummaryTotal > 0)
                                        Percent = Convert.ToDouble(((double)intCountTotal * 100) / (double)SummaryTotal);
                                    resultInside.Append(" <tr  style=\" height:30px; background-color:#ffffff\">");
                                    resultInside.Append("<td class=\"td_bar_left\">");
                                    resultInside.Append(dDatecurrent.ToString("ddd, MMM dd, yyyy"));
                                    resultInside.Append("</td>");
                                    resultInside.Append("<td class=\"td_bar_right\">");
                                    resultInside.Append("<div class=\"bar_color\" style=\" width:" + Percent + "%;\">");
                                    resultInside.Append("<span class=\"bar_sum\" >" + intCountTotal + "(" + Percent + "%)</span></div>");
                                    resultInside.Append("</td>");
                                    resultInside.Append("</tr>");
                                    intCountTotal = 0;
                                }

                                break;
                            // Room Night CheckIn Date
                            case 4:
                                SummaryTotal = cBookingstat.CountRoomNight_CheckInDateByPeriod(this.CurrentProductActiveExtra,
                           this.CurrentSupplierId, dDAteStart, dDAteENd);
                                TotalBooking = cBookingstat.GEtRoomNight_CheckInDateByPeriod(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);

                                dDatecurrent = DateTime.Now;
                                DateDiff = dDAteENd.Subtract(dDAteStart).Days;
                                intCountTotal = 0;
                                Percent = 0;
                                for (int days = 0; days <= DateDiff; days++)
                                {
                                    dDatecurrent = dDAteStart.AddDays(days);
                                    foreach (BookingReport_booking BarList in TotalBooking)
                                    {
                                        if (dDatecurrent.Date == BarList.DateSubmit.Date)
                                        {
                                            intCountTotal = intCountTotal + BarList.TotalPeriodNightStay_Real;
                                        }
                                    }

                                    if (SummaryTotal > 0)
                                        Percent = Convert.ToDouble(((double)intCountTotal * 100) / (double)SummaryTotal);

                                    
                                    resultInside.Append(" <tr  style=\" height:30px; background-color:#ffffff\">");
                                    resultInside.Append("<td class=\"td_bar_left\">");
                                    resultInside.Append(dDatecurrent.ToString("ddd, MMM dd, yyyy"));
                                    resultInside.Append("</td>");
                                    resultInside.Append("<td class=\"td_bar_right\">");
                                    resultInside.Append("<div class=\"bar_color\" style=\" width:" + Percent + "%;\">");
                                    resultInside.Append("<span class=\"bar_sum\" >" + intCountTotal + "(" + Percent + "%)</span></div>");
                                    resultInside.Append("</td>");
                                    resultInside.Append("</tr>");
                                    intCountTotal = 0;
                                }

                                break;
                        }

                        

                        break;
                    case 2:
                        int intYear = int.Parse(Request.Form["hd_date_month_year"]);
                        dDAteStart = new DateTime(intYear, 1, 1);
                        dDAteENd = new DateTime(intYear, 12, 31);
                        switch (bytChartName)
                        {
                            // All Booking 
                            case 1:
                                TotalBooking = cBookingstat.getBookingAll(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                           dDAteStart, dDAteENd);
                                SummaryTotal = cBookingstat.CountBookingAll(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDAteStart, dDAteENd);
                                DateTime dDatecurrent = DateTime.Now;
                                int DateDiff = dDAteENd.Subtract(dDAteStart).Days;
                                int intCountTotal = 0;
                                double Percent = 0;
                                for (int month = 1; month <= 12; month++)
                                {
                                    
                                    foreach (BookingReport_booking BarList in TotalBooking)
                                    {
                                        if (month == BarList.DateSubmit.Month && intYear == BarList.DateSubmit.Year)
                                        {
                                            intCountTotal = intCountTotal + 1;
                                        }
                                    }

                                    if (SummaryTotal > 0)
                                        Percent = Convert.ToDouble(((double)intCountTotal * 100) / (double)SummaryTotal);
                                    resultInside.Append(" <tr  style=\" height:30px; background-color:#ffffff\">");
                                    resultInside.Append("<td class=\"td_bar_left\">");

                                    resultInside.Append(Hotels2DateTime.GetMonthName(month,true) + ", " + intYear);


                                    resultInside.Append("</td>");
                                    resultInside.Append("<td class=\"td_bar_right\">");
                                    resultInside.Append("<div class=\"bar_color\" style=\" width:" + Percent + "%;\">");
                                    resultInside.Append("<span class=\"bar_sum\" >" + intCountTotal + "(" + Percent + "%)</span></div>");
                                    resultInside.Append("</td>");
                                    resultInside.Append("</tr>");
                                    intCountTotal = 0;
                                }
                                break;
                            // Booking Completed
                            case 2:
                                SummaryTotal = cBookingstat.CountBookingCompleted(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDAteStart, dDAteENd);
                                TotalBooking = cBookingstat.getBookingCompleted(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);

                                dDatecurrent = DateTime.Now;
                                DateDiff = dDAteENd.Subtract(dDAteStart).Days;
                                intCountTotal = 0;
                                Percent = 0;
                                for (int month = 1; month <= 12; month++)
                                {
                                    
                                    foreach (BookingReport_booking BarList in TotalBooking)
                                    {
                                        if (month == BarList.DateSubmit.Month && intYear == BarList.DateSubmit.Year)
                                        {
                                            intCountTotal = intCountTotal + 1;
                                        }
                                    }

                                    if (SummaryTotal > 0)
                                        Percent = Convert.ToDouble(((double)intCountTotal * 100) / (double)SummaryTotal);

                                    resultInside.Append(" <tr  style=\" height:30px; background-color:#ffffff\">");
                                    resultInside.Append("<td class=\"td_bar_left\">");
                                    resultInside.Append(Hotels2DateTime.GetMonthName(month, true) + ", " + intYear);
                                    resultInside.Append("</td>");
                                    resultInside.Append("<td class=\"td_bar_right\">");
                                    resultInside.Append("<div class=\"bar_color\" style=\" width:" + Percent + "%;\">");
                                    resultInside.Append("<span class=\"bar_sum\" >" + intCountTotal + "(" + Percent + "%)</span></div>");
                                    resultInside.Append("</td>");
                                    resultInside.Append("</tr>");
                                    intCountTotal = 0;
                                }

                                break;
                            // Room Night BookingDate
                            case 3:
                                SummaryTotal = cBookingstat.CountRoomNight_BookingDate(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);
                                TotalBooking = cBookingstat.GetRoomNight_BookingDate(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);

                                
                                dDatecurrent = DateTime.Now;
                                DateDiff = dDAteENd.Subtract(dDAteStart).Days;
                                intCountTotal = 0;
                                Percent = 0;
                                for (int month = 1; month <= 12; month++)
                                {
                                  
                                    foreach (BookingReport_booking BarList in TotalBooking)
                                    {
                                        if (month == BarList.DateSubmit.Month && intYear == BarList.DateSubmit.Year)
                                        {
                                            intCountTotal = intCountTotal + BarList.TotalPeriodNightStay_Real;
                                        }
                                    }

                                    if (SummaryTotal > 0)
                                        Percent = Convert.ToDouble(((double)intCountTotal * 100) / (double)SummaryTotal);
                                    resultInside.Append(" <tr  style=\" height:30px; background-color:#ffffff\">");
                                    resultInside.Append("<td class=\"td_bar_left\">");
                                    resultInside.Append(Hotels2DateTime.GetMonthName(month, true) + ", " + intYear);
                                    resultInside.Append("</td>");
                                    resultInside.Append("<td class=\"td_bar_right\">");
                                    resultInside.Append("<div class=\"bar_color\" style=\" width:" + Percent + "%;\">");
                                    resultInside.Append("<span class=\"bar_sum\" >" + intCountTotal + "(" + Percent + "%)</span></div>");
                                    resultInside.Append("</td>");
                                    resultInside.Append("</tr>");
                                    intCountTotal = 0;
                                }
                                break;
                            // Room Night CheckIn Date
                            case 4:
                                SummaryTotal = cBookingstat.CountRoomNight_CheckInDateByPeriod(this.CurrentProductActiveExtra,
                            this.CurrentSupplierId, dDAteStart, dDAteENd);
                                TotalBooking = cBookingstat.GEtRoomNight_CheckInDateByPeriod(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                             dDAteStart, dDAteENd);

                                
                                dDatecurrent = DateTime.Now;
                                DateDiff = dDAteENd.Subtract(dDAteStart).Days;
                                intCountTotal = 0;
                                Percent = 0;
                                for (int month = 1; month <= 12; month++)
                                {
                                   
                                    foreach (BookingReport_booking BarList in TotalBooking)
                                    {
                                        if (month == BarList.DateSubmit.Month && intYear == BarList.DateSubmit.Year)
                                        {
                                            intCountTotal = intCountTotal + BarList.TotalPeriodNightStay_Real;
                                        }
                                    }

                                    if (SummaryTotal > 0)
                                        Percent = Convert.ToDouble(((double)intCountTotal * 100) / (double)SummaryTotal);
                                    resultInside.Append(" <tr  style=\" height:30px; background-color:#ffffff\">");
                                    resultInside.Append("<td class=\"td_bar_left\">");
                                    resultInside.Append(Hotels2DateTime.GetMonthName(month, true) + ", " + intYear);
                                    resultInside.Append("</td>");
                                    resultInside.Append("<td class=\"td_bar_right\">");
                                    resultInside.Append("<div class=\"bar_color\" style=\" width:" + Percent + "%;\">");
                                    resultInside.Append("<span class=\"bar_sum\" >" + intCountTotal + "(" + Percent + "%)</span></div>");
                                    resultInside.Append("</td>");
                                    resultInside.Append("</tr>");
                                    intCountTotal = 0;
                                }
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

                                DateTime dDatecurrent = DateTime.Now;
                                int DateDiff = dDAteENd.Subtract(dDAteStart).Days;
                                int intCountTotal = 0;
                                double Percent = 0;
                                int YearMax = intYearNow + 4;
                                for (int year = 2011; year <= YearMax; year++)
                                {
                                    
                                    foreach (BookingReport_booking BarList in TotalBooking)
                                    {
                                        if (year == BarList.DateSubmit.Year)
                                        {
                                            intCountTotal = intCountTotal + 1;
                                        }
                                    }

                                    if (SummaryTotal > 0)
                                        Percent = Convert.ToDouble(((double)intCountTotal * 100) / (double)SummaryTotal);
                                    resultInside.Append(" <tr  style=\" height:30px; background-color:#ffffff\">");
                                    resultInside.Append("<td class=\"td_bar_left\">");
                                    resultInside.Append(year);
                                    resultInside.Append("</td>");
                                    resultInside.Append("<td class=\"td_bar_right\">");
                                    resultInside.Append("<div class=\"bar_color\" style=\" width:" + Percent + "%;\">");
                                    resultInside.Append("<span class=\"bar_sum\" >" + intCountTotal + "(" + Percent + "%)</span></div>");
                                    resultInside.Append("</td>");
                                    resultInside.Append("</tr>");
                                    intCountTotal = 0;
                                }
                                break;
                            // Booking Completed
                            case 2:
                                SummaryTotal = cBookingstat.CountBookingCompleted(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDAteStart, dDAteENd);
                                TotalBooking = cBookingstat.getBookingCompleted(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                             dDAteStart, dDAteENd);

                                dDatecurrent = DateTime.Now;
                                DateDiff = dDAteENd.Subtract(dDAteStart).Days;
                                intCountTotal = 0;
                                Percent = 0;
                                YearMax = intYearNow + 4;
                                for (int year = 2011; year <= YearMax; year++)
                                {
                                   
                                    foreach (BookingReport_booking BarList in TotalBooking)
                                    {
                                        if (year == BarList.DateSubmit.Year)
                                        {
                                            intCountTotal = intCountTotal + 1;
                                        }
                                    }

                                    if (SummaryTotal > 0)
                                        Percent = Convert.ToDouble(((double)intCountTotal * 100) / (double)SummaryTotal);
                                    resultInside.Append(" <tr  style=\" height:30px; background-color:#ffffff\">");
                                    resultInside.Append("<td class=\"td_bar_left\">");
                                    resultInside.Append(year);
                                    resultInside.Append("</td>");
                                    resultInside.Append("<td class=\"td_bar_right\">");
                                    resultInside.Append("<div class=\"bar_color\" style=\" width:" + Percent + "%;\">");
                                    resultInside.Append("<span class=\"bar_sum\" >" + intCountTotal + "(" + Percent + "%)</span></div>");
                                    resultInside.Append("</td>");
                                    resultInside.Append("</tr>");
                                    intCountTotal = 0;
                                }
                                break;

                            // Room Night BookingDate
                            case 3:
                                SummaryTotal = cBookingstat.CountRoomNight_BookingDate(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);
                                TotalBooking = cBookingstat.GetRoomNight_BookingDate(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);

                                dDatecurrent = DateTime.Now;
                                DateDiff = dDAteENd.Subtract(dDAteStart).Days;
                                intCountTotal = 0;
                                Percent = 0;
                                YearMax = intYearNow + 4;
                                for (int year = 2011; year <= YearMax; year++)
                                {
                                   
                                    foreach (BookingReport_booking BarList in TotalBooking)
                                    {
                                        if (year == BarList.DateSubmit.Year)
                                        {
                                            intCountTotal = intCountTotal + BarList.TotalPeriodNightStay_Real;
                                        }
                                    }

                                    if (SummaryTotal > 0)
                                        Percent = Convert.ToDouble(((double)intCountTotal * 100) / (double)SummaryTotal);
                                    resultInside.Append(" <tr  style=\" height:30px; background-color:#ffffff\">");
                                    resultInside.Append("<td class=\"td_bar_left\">");
                                    resultInside.Append(year);
                                    resultInside.Append("</td>");
                                    resultInside.Append("<td class=\"td_bar_right\">");
                                    resultInside.Append("<div class=\"bar_color\" style=\" width:" + Percent + "%;\">");
                                    resultInside.Append("<span class=\"bar_sum\" >" + intCountTotal + "(" + Percent + "%)</span></div>");
                                    resultInside.Append("</td>");
                                    resultInside.Append("</tr>");
                                    intCountTotal = 0;
                                }
                                break;
                            // Room Night CheckIn Date
                            case 4:
                                SummaryTotal = cBookingstat.CountRoomNight_CheckInDateByPeriod(this.CurrentProductActiveExtra,
                            this.CurrentSupplierId, dDAteStart, dDAteENd);
                                TotalBooking = cBookingstat.GEtRoomNight_CheckInDateByPeriod(this.CurrentProductActiveExtra, this.CurrentSupplierId,
                            dDAteStart, dDAteENd);

                                dDatecurrent = DateTime.Now;
                                DateDiff = dDAteENd.Subtract(dDAteStart).Days;
                                intCountTotal = 0;
                                Percent = 0;
                                YearMax = intYearNow + 4;
                                for (int year = 2011; year <= YearMax; year++)
                                {
                                  
                                    foreach (BookingReport_booking BarList in TotalBooking)
                                    {
                                        if (year == BarList.DateSubmit.Year)
                                        {
                                            intCountTotal = intCountTotal + BarList.TotalPeriodNightStay_Real;
                                        }
                                    }

                                    if (SummaryTotal > 0)
                                        Percent = Convert.ToDouble(((double)intCountTotal * 100) / (double)SummaryTotal);
                                    resultInside.Append(" <tr  style=\" height:30px; background-color:#ffffff\">");
                                    resultInside.Append("<td class=\"td_bar_left\">");
                                    resultInside.Append(year);
                                    resultInside.Append("</td>");
                                    resultInside.Append("<td class=\"td_bar_right\">");
                                    resultInside.Append("<div class=\"bar_color\" style=\" width:" + Percent + "%;\">");
                                    resultInside.Append("<span class=\"bar_sum\" >" + intCountTotal + "(" + Percent + "%)</span></div>");
                                    resultInside.Append("</td>");
                                    resultInside.Append("</tr>");
                                    intCountTotal = 0;
                                }
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

                result.Append("<p class=\"summary_total_bar\">" + StringSummaryTotal + "</p>");
                result.Append("<div id=\"bar_result\">");
                result.Append("<table cellpadding=\"0\" cellspacing=\"0\"  style=\"background-color:#eeeeee; width:100%\">");


                result.Append(resultInside.ToString());
               



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