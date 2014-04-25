using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Hotels2thailand.Report;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_dash_board_bookingStat_Month : Hotels2BasePageExtra_Ajax
    {
        public string qMonth
        {
            get { return Request.QueryString["m"]; }
        }

        public string qYear
        {
            get { return Request.QueryString["y"]; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {

                if(!string.IsNullOrEmpty(this.qMonth) && !string.IsNullOrEmpty(this.qYear))
                {
                    DateTime dDate = new DateTime(int.Parse(this.qYear),int.Parse(this.qMonth),1);

                    
                    Response.Write(GetStringArryResultStat(dDate));
                    Response.End();
                }
                
                
            }
        }


        public string GetStringArryResultStat(DateTime dDate)
        {
            string result = string.Empty;

            try
            {
                BookingDashBoardExtra cBookingStat = new BookingDashBoardExtra();
                //IList<object> iListBookingImpress = cBookingStat.getCountImpression(this.CurrentProductActiveExtra, dDate, DateInterval.Month); ;

                DateTime Date = new DateTime(dDate.Year, dDate.Month, 1);

                //for (int month = 1; month <= 12; month++)
                //{

                //    int Count = 0;
                    
                //    foreach (BookingDashBoardExtra bookingStat in iListBookingImpress)
                //    {
                //        if (bookingStat.dDate.Year == Date.Year && bookingStat.dDate.Month == month)
                //        {
                //            Count = Count + (int)bookingStat.Num;
                //        }
                //    }

                //    if (Count > 0)
                //    {
                //        result = result + Count + ",";
                //    }
                //    else
                //    {
                //        result = result + "0,";
                //    }


                //}

                //result = result.Hotels2RightCrl(1);
                //result = result + "%";

                IList<object> iListBookingAll = cBookingStat.getAllBookingByProductIDANdSupplier(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDate, DateInterval.Month);

                for (int month = 1; month <= 12; month++)
                    {
                        int Count = 0;
                        foreach (BookingDashBoardExtra BookingList in iListBookingAll)
                        {
                            if (BookingList.dDate.Year == Date.Year && BookingList.dDate.Month == month)
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

                result = result.Hotels2RightCrl(1);
                result = result + "%";

                IList<object> iListBookingCompleted = cBookingStat.getCompletedBookingByProductIDANdSupplier(this.CurrentProductActiveExtra, this.CurrentSupplierId, dDate, DateInterval.Month);

                for (int month = 1; month <= 12; month++)
                {
                    int Count = 0;
                    foreach (BookingDashBoardExtra BookingList in iListBookingCompleted)
                    {
                        if (BookingList.dDate.Year == Date.Year && BookingList.dDate.Month == month)
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