using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using Hotels2thailand.Booking;
using Hotels2thailand;

namespace Hotels2thailand.UI
{
    public partial class ajax_booking_status_edit : Hotels2BasePageExtra_Ajax
    {
        public string qBookingId
        {
            get
            {
                return Request.QueryString["bid"];
            }
        }
        public string qBookingProductId
        {
            get
            {
                return Request.QueryString["bpid"];
            }
        }

        public string qStatusId
        {
            get
            {
                return Request.QueryString["sid"];
            }
        }
        public string qType
        {
            get
            {
                return Request.QueryString["type"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                
                Response.Write(getBookingItemEditSave());
                Response.End();
            }
        }
        public bool getBookingItemEditSave()
        {

            StatusBooking cStatus = new StatusBooking();
            bool result = false;
            if (this.qType == "booking")
            {
                result = cStatus.UpdateBookingStatus(int.Parse(this.qBookingId), short.Parse(this.qStatusId));
            }

            if (this.qType == "product")
            {
                result = cStatus.UpdateProductBookingStatus(int.Parse(this.qBookingProductId), short.Parse(this.qStatusId));
            }


            if (this.qType == "aff")
            {
                result = cStatus.UpdateBookingStatusAff(int.Parse(this.qBookingId), short.Parse(this.qStatusId));
            }
            return result;


        }



    }
}