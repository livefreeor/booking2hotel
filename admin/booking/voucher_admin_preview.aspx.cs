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
    public partial class admin_voucher_admin_preview : Hotels2BasePage
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
                int intBookingProductId = int.Parse(this.qBookingProductId);
                BookingProductDisplay cBookingProduct = new BookingProductDisplay();
                cBookingProduct = cBookingProduct.getBookingProductDisplayByBookingProductId(intBookingProductId);

                if (!cBookingProduct.cProductBookingEngine.Is_B2b)
                {
                    BookingVoucher_PrintEngine cBookingPrint = new BookingVoucher_PrintEngine(intBookingProductId);
                    Response.Write(cBookingPrint.getVoucher(true));
                    Response.End();
                }
                else
                {
                    Hotels2thailand.BookingB2b.BookingVoucher_PrintEngineB2b cVoucherB2b = new BookingB2b.BookingVoucher_PrintEngineB2b(intBookingProductId);
                    Response.Write(cVoucherB2b.getVoucher());
                    Response.End();

                }
                

            }
        }


        

        
        

        
        
        
    }
}