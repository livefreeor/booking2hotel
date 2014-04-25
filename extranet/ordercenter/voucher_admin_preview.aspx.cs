using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Hotels2thailand.Booking;

namespace Hotels2thailand.UI
{
    public partial class admin_voucher_admin_preview : Hotels2BasePageExtra
    {
        //booking_id
        public string qBookingProductId
        {
            get
            {
                return Request.QueryString["bpid"];
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.Page.IsPostBack)
            {
                BookingVoucher_PrintEngine cBookingPrint = new BookingVoucher_PrintEngine(int.Parse(this.qBookingProductId));
                Response.Write(cBookingPrint.getVoucher(true));
                Response.End();

            }
        }


        

        
        

        
        
        
    }
}