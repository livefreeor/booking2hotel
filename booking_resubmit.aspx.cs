using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;
using Hotels2thailand;
using Hotels2thailand.Booking;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class booking_resubmit : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Write("<h1>We are in the process of updating the system temporarily.</h2>");
        //Response.End();
        string connString = WebConfigurationManager.ConnectionStrings["booking2hotelXConnectionString"].ConnectionString;
        //Process Decode Get BookingPaymentID
        string QueryStringResult = HttpUtility.UrlDecode(Request.QueryString["pcode"]).Hotels2DecryptedData_SecretKey();
        QueryStringResult = QueryStringResult.Hotels2RightCrl(20);

        //Response.Write(QueryStringResult);
        //Response.Flush();
       // Response.End();
        int BookingPaymentID = int.Parse(QueryStringResult);
        int BookingPaymentBankID = 0;
        byte gatewayID = 0;
        string sqlCommand = string.Empty;
        bool isPaymentBankOpen = false;

        using (SqlConnection cn = new SqlConnection(connString))
        {
            sqlCommand = "select top 1 bpayb.booking_payment_bank_id,bpay.gateway_id,bpayb.status";
            sqlCommand = sqlCommand+" from tbl_booking_payment bpay,tbl_booking_payment_bank bpayb";
            sqlCommand = sqlCommand+" where bpay.booking_payment_id=bpayb.booking_payment_id";
            sqlCommand = sqlCommand + " and bpay.booking_payment_id=" + BookingPaymentID;
            sqlCommand = sqlCommand + " order by bpayb.booking_payment_bank_id desc";
            SqlCommand cmd = new SqlCommand(sqlCommand, cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            gatewayID = (byte)reader["gateway_id"];
            BookingPaymentBankID = (int)reader["booking_payment_bank_id"];
            isPaymentBankOpen = (bool)reader["status"];
        }

        BookingPaymentDisplay bookingPaymentHotel = new BookingPaymentDisplay();
        if (!isPaymentBankOpen)
        {
            BookingPaymentBankID = bookingPaymentHotel.InsertBookingPaymentBank(BookingPaymentID);
            if(BookingPaymentBankID==0)
            {
                //Transaction Confirmed already
                //redirect to page
                string errorMessage = "<br><br><div style=\"color:#C09853;margin:0 auto;width:650px;font-family:arial;padding-left:60px; min-height:60px; background:url(/images/icon/ico_info.png) no-repeat top left;\">This transaction has been paid to our payment system. Our payment gateway system can not accept the duplicate payment to the same Booking ID. Please make sure that you aren’t going to pay twice.</div>";
                Response.Write(errorMessage);
                Response.End();
            }
        }
        bookingPaymentHotel.UpdatePaymentBankSend(BookingPaymentBankID);

        //if (gatewayID == 9 || gatewayID == 10 || gatewayID == 12)
        //{
        //    BookingPaymentDisplay bookingPaymentHotel = new BookingPaymentDisplay();
        //    BookingPaymentBankID = bookingPaymentHotel.InsertBookingPaymentBank(BookingPaymentID);
        //}


        
        FrontPaymentMethod payment = new FrontPaymentMethod();
        Response.Write(payment.GetPaymentFormByPaymentID(BookingPaymentBankID));
    }
}