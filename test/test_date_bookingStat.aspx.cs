using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Hotels2thailand.Production;
using Hotels2thailand.Report;
using Hotels2thailand.PDF;

using EvoPdf.HtmlToPdf;


public partial class test_date_bookingStat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                BookingReport_booking_country cBookingstat = new BookingReport_booking_country();
                DateTime dDAteStart = new DateTime(2012, 1, 1);
                DateTime dDateEnd = new DateTime(2012, 12,31 );
                IList<object> iListBookingstat = cBookingstat.GetBookingCountryRoomNight_CheckInDateByPeriod(455, 496, dDAteStart, dDateEnd);

                foreach (BookingReport_booking_country item in iListBookingstat)
                {
                    Response.Write(item.CountryId + "--" + item.CountryTitle + "---" + item.Total  + "</br>");
                    Response.Flush();
                }
                //int Count = cBookingstat.CountRoomNight_BookingDate(2124, 2446, dDAteStart, dDateEnd);
                //Response.Write(Count);
                Response.End();
                //Response.Write(iListBookingstat.Count);
                
            }
            //ctrlDemoLinksBox.LoadDemo("GettingStarted");
        }

    }

