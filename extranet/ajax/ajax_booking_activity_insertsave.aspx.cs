﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using Hotels2thailand.Booking;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class ajax_booking_activity_insertsave : System.Web.UI.Page
    {

        public string qBookingId
        {
            get
            {
                return Request.QueryString["bid"];
            }
        }
        
        public string qBookingType
        {
            get
            {
                return Request.QueryString["bt"];
            }
        }

        public short Current_StaffID
        {
            get
            {
                Hotels2thailand.UI.Hotels2BasePage cBasePage = new Hotels2thailand.UI.Hotels2BasePage();
                return cBasePage.CurrentStaffId;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Response.Write(getBookingActivityInsert());
                Response.Flush();
            }
        }

        public int getBookingActivityInsert()
        {
            string bookingType = Request.Form["bookingType"];
            
            int ret  = 0;
            BookingActivityDisplay cActivity = new BookingActivityDisplay();
            string strDeail = Request.Form["activity_detail"];
            int intBookingProductId = 0;
            if (!string.IsNullOrEmpty(Request.Form["bookingProductId"]))
            {
                intBookingProductId = int.Parse(Request.Form["bookingProductId"]);
            }
            
            
            if (bookingType == "booking")
            {
                
               ret =  cActivity.InsertNewActivityBooking(int.Parse(this.qBookingId), strDeail);
            }

            if (bookingType == "booking_close")
            {
                int intBookingId = int.Parse(this.qBookingId);
                ret = cActivity.InsertNewActivityBooking(intBookingId, strDeail);

                BookingdetailDisplay cBooking = new BookingdetailDisplay();
                BookingActivityDisplay cBookingactivity = new BookingActivityDisplay();

                
                cBooking.UpdateBookingstatus(intBookingId);
                cBookingactivity.InsertAutoActivity(BookingActivityType.CloseBooking, intBookingId);
                ret = 3;
            }

            if (bookingType == "product")
            {
                
                ret = cActivity.InsertNewActivityBookingProduct(int.Parse(this.qBookingId), intBookingProductId, strDeail);
            }

            return ret;
        }



    }
}