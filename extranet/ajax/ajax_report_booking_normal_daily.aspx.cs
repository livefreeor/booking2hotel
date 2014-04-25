using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.ProductOption;
using System.Text;
using Hotels2thailand.Report;


namespace Hotels2thailand.UI
{
    public partial class ajax_report_booking_normal_daily : Hotels2BasePageExtra_Ajax
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.Form["hd_date_start_daily"]) && !string.IsNullOrEmpty(Request.Form["hd_date_end_daily"]) && !string.IsNullOrEmpty(Request.Form["hd_chart_name"]))
                {
                    Response.Write(GetStringArryResultStat());
                    Response.End();
                }
            }
        }


        public string GetStringArryResultStat()
        {
            string result = string.Empty;

            try
            {
                DateTime dDateStart = Request.Form["hd_date_start_daily"].Hotels2DateSplitYear("-");
                DateTime dDateEnd = Request.Form["hd_date_end_daily"].Hotels2DateSplitYear("-");
              
                //DateTime dDateStart = new DateTime(2011, 12, 1);
                //DateTime dDateEnd = new DateTime(2011, 12, 31);
                byte ChartName = byte.Parse(Request.Form["hd_chart_name"]);

                BookingReport_booking cBookingStat = new BookingReport_booking();

                int DateAmount = dDateEnd.Subtract(dDateStart).Days + 1;
                
                DateTime dDateCurrent = new DateTime();
                IList<object> iListBookingAll = null;
                if (ChartName == 1 || ChartName == 2)
                {
                    
                    if(ChartName == 1)
                        iListBookingAll = cBookingStat.getBookingAll(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDateStart, dDateEnd);
                    else
                        iListBookingAll = cBookingStat.getBookingCompleted(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDateStart, dDateEnd);

                    for (int day = 0; day < DateAmount; day++)
                    {
                        int Count = 0;
                        foreach (BookingReport_booking BookingList in iListBookingAll)
                        {
                            dDateCurrent = dDateStart.AddDays(day);

                            if (dDateCurrent.Date == BookingList.DateSubmit.Date)
                            {
                                Count = Count + 1;
                            }
                        }

                        if (Count > 0)
                        {
                            result = result + Count + ",";
                        }
                        else
                        {
                            result = result + "0,";
                        }
                    }
                }

                if (ChartName == 3 || ChartName == 4)
                {
                    if (ChartName == 3)
                        iListBookingAll = cBookingStat.GetRoomNight_BookingDate(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDateStart, dDateEnd);
                    else
                        iListBookingAll = cBookingStat.GEtRoomNight_CheckInDateByPeriod(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDateStart, dDateEnd);

                    for (int day = 0; day < DateAmount; day++)
                    {
                        int Count = 0;
                        foreach (BookingReport_booking BookingList in iListBookingAll)
                        {
                            dDateCurrent = dDateStart.AddDays(day);

                            if (dDateCurrent.Date == BookingList.DateSubmit.Date)
                            {
                                Count = Count + BookingList.TotalPeriodNightStay_Real;
                            }
                        }

                        if (Count > 0)
                        {
                            result = result + Count + ",";
                        }
                        else
                        {
                            result = result + "0,";
                        }
                    }

                }

                result = result.Hotels2RightCrl(1);
            }
            catch (Exception ex)
            {
                Response.Write("error :"  + ex.Message);
                Response.End();
            }

            return result;
        }
    }
}