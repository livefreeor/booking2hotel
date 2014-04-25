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
    public partial class ajax_report_booking_normal_year : Hotels2BasePageExtra_Ajax
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
            {
                if ( !string.IsNullOrEmpty(Request.Form["hd_chart_name"]))
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
                int intYear = DateTime.Now.Hotels2ThaiDateTime().Year;
                DateTime dDateStart = new DateTime(2011, 1, 1);
                DateTime dDateEnd = new DateTime((intYear + 4), 12, 31);
                byte ChartName = byte.Parse(Request.Form["hd_chart_name"]);

                BookingReport_booking cBookingStat = new BookingReport_booking();

                IList<object> iListBookingAll = null;
                if (ChartName == 1 || ChartName == 2)
                {
                    
                    if(ChartName == 1)
                        iListBookingAll = cBookingStat.getBookingAll(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDateStart, dDateEnd);
                    else
                        iListBookingAll = cBookingStat.getBookingCompleted(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDateStart, dDateEnd);

                    for (int year = 2011; year <= (intYear + 4); year++)
                    {
                        int Count = 0;
                        foreach (BookingReport_booking BookingList in iListBookingAll)
                        {
                            

                            if (year == BookingList.DateSubmit.Year)
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

                    for (int year = 2011; year <= (intYear + 4); year++)
                    {
                        int Count = 0;
                        foreach (BookingReport_booking BookingList in iListBookingAll)
                        {


                            if (year == BookingList.DateSubmit.Year)
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