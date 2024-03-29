﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Booking;
using Hotels2thailand.Production;


namespace Hotels2thailand.UI
{
    public partial class extranet_booking_booking_list : Hotels2BasePageExtra
    {
        public string qBokingId
        {
            get
            {
                return Request.QueryString["bid"];
            }
        }
        public string qBokingStatus
        {
            get
            {
                return Request.QueryString["bs"];
            }
        }
        public string qBokingProductStatus
        {
            get
            {
                return Request.QueryString["bps"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                GenBookingStatus();
            }
            
        }

        protected void GenBookingStatus()
        {
            Status cStatus = new Status();
            StringBuilder cResult = new StringBuilder();
            int count = 1;
            short shrStatusProductId = 0;
            foreach (Status item in cStatus.GetStatusByCatIdBooking(2))
            {

                switch (item.StatusID)
                {
                    //case 68:
                    //    shrStatusProductId = 10;
                    //    break;
                    case 71:
                        shrStatusProductId = 11;
                        break;
                    case 72:
                        shrStatusProductId = 12;
                        break;
                    case 83:
                        shrStatusProductId = 13;
                        break;
                    case 85:
                        shrStatusProductId = 15;
                        break;
                    case 92:
                        shrStatusProductId = 93;
                        break;
                    case 30:
                        shrStatusProductId = 17;
                        break;
                    case 94:
                        shrStatusProductId = 95;
                        break;
                }

                if (count == cStatus.GetStatusByCatIdBooking(2).Count)
                    cResult.Append("<a href=\"booking_list.aspx?bs=" + item.StatusID + "\" id=\"" + item.StatusID + "\" onclick=\"GetpageBookingStatus('" + item.StatusID + "','" + shrStatusProductId + "');return false;\" >" + item.Title + "</a>");
                else
                    cResult.Append("<a href=\"booking_list.aspx?bs=" + item.StatusID + "\" id=\"" + item.StatusID + "\" onclick=\"GetpageBookingStatus('" + item.StatusID + "','" + shrStatusProductId + "');return false;\" >" + item.Title + "</a>");
                count = count + 1;
            }

            bookingStatus.Text = cResult.ToString();
        }
        
    }
}