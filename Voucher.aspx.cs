using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Booking;
using Hotels2thailand;

public partial class Voucher : System.Web.UI.Page
{
    public string qBookingProductId
    {
        get { return Request.QueryString["id"]; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            string QueryStringResult = HttpUtility.UrlDecode(this.qBookingProductId).Hotels2DecryptedData_SecretKey();
            QueryStringResult = QueryStringResult.Hotels2RightCrl(20);

            BookingProductDisplay cBookingProduct = new BookingProductDisplay();
            BookingConfirmEngine cConfirmEngine = new BookingConfirmEngine();

            BookingVoucher_PrintEngine cBookingPrint = new BookingVoucher_PrintEngine(int.Parse(QueryStringResult));

            int bookingId = (int)cBookingProduct.getBookingIdByBookingProductId(int.Parse(QueryStringResult));


            //Confirm Open Voucher
            BookingdetailDisplay cBookingDetail = new BookingdetailDisplay();
            cBookingDetail = cBookingDetail.GetBookingDetailListByBookingId(bookingId);
            if (!cBookingDetail.ConfirmVoucher.HasValue)
            {
                cConfirmEngine.UpdateConfirmCustomer_OpenVoucher(bookingId);
            }


            int intBookingProductId = int.Parse(QueryStringResult);
            cBookingProduct = cBookingProduct.getBookingProductDisplayByBookingProductId(intBookingProductId);

            if (!cBookingProduct.cProductBookingEngine.Is_B2b)
            {
                Response.Write(cBookingPrint.getVoucher(false));
                Response.End();
            }
            else
            {
                Hotels2thailand.BookingB2b.BookingVoucher_PrintEngineB2b cVoucherB2b = new Hotels2thailand.BookingB2b.BookingVoucher_PrintEngineB2b(intBookingProductId);
                Response.Write(cVoucherB2b.getVoucher());
                Response.End();
            }
            

        }
    }
}