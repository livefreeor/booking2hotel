using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class order_complete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Case BLL ONLY 

        
        if (!String.IsNullOrEmpty(Request.QueryString["Ref"]))
        {
            int PaymentBankID = 0;

            PaymentBankID = Convert.ToInt32(Request.QueryString["Ref"]);

            
            if (PaymentBankID >= 30000000)
            {
                Response.Redirect("http://b2b25.booking2hotels.com/order-complete.aspx?Ref=" + Request.QueryString["Ref"]);

            }
        }
    }

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    string bankData = string.Empty;
    //    int PaymentID = 0;
    //    int PaymentBankID = 0;
    //    int BookingID = 0;
    //    byte PaymentType = 1;


    //    Response.Write("OK");
    //    if (!string.IsNullOrEmpty(Request.Form["successcode"]))
    //    {
    //        bankData = bankData + "Post:" + Request.Form["successcode"] + "--Ref:" + Request.Form["Ref"];
    //        //bankData = bankData + "Get:" + Request.QueryString["PMGWRESP"];
    //        //Hotels2MAilSender.SendmailBooking("visa test", "oh_darkman@hotmail.com", "Data from BBL", "", bankData);
    //        //Hotels2MAilSender.SendmailNormail("visa@hotels2thailand.com", "Visa", "kiasa555@gmail.com", "Kbank Return", "visa@hotels2thailand.com", Request.Form["successcode"] + "-" + Request.Form["Ref"]);
    //        PaymentBankID = Convert.ToInt32(Request.Form["Ref"]);



    //        // PaymentID = Convert.ToInt32(Request.Form["Ref"]);

    //        StringBuilder log = new StringBuilder();

    //        log.Append("[{");
    //        log.Append("\"Time\":\"" + DateTime.Now.Hotels2ThaiDateTime() + "\"\r\n");
    //        log.Append(",\"successcode\":\"" + Request.Form["successcode"] + "\"\r\n");
    //        log.Append(",\"Ref\":\"" + Request.Form["Ref"] + "\"\r\n");
    //        log.Append("}]\r\n");
    //        log.Append("---------------ENd \r\n");


    //        Hotels2LogWriter.WriteFile("admin/logfile/paymentlog.html", log.ToString());

    //        if (PaymentBankID >= 30000000)
    //        {
    //            B2bSubmit(Request.Form["successcode"], Request.Form["Ref"]);


    //            Response.End();
    //        }

    //        if (Request.Form["successcode"] == "0")
    //        {
    //            string sqlCommand = string.Empty;
    //            string connString = WebConfigurationManager.ConnectionStrings["booking2hotelXConnectionString"].ConnectionString;

    //            sqlCommand = "select bpay.booking_id,bpay.booking_payment_id";
    //            sqlCommand = sqlCommand + " from tbl_booking_payment bpay,tbl_booking_payment_bank bpayb";
    //            sqlCommand = sqlCommand + " where bpay.booking_payment_id=bpayb.booking_payment_id";
    //            sqlCommand = sqlCommand + " and bpayb.booking_payment_bank_id=" + PaymentBankID;

    //            using (SqlConnection cn = new SqlConnection(connString))
    //            {
    //                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
    //                cn.Open();
    //                SqlDataReader reader = cmd.ExecuteReader();
    //                reader.Read();
    //                BookingID = (int)reader["booking_id"];
    //                PaymentID = (int)reader["booking_payment_id"];
    //            }

    //            using (SqlConnection cn = new SqlConnection(connString))
    //            {
    //                cn.Open();
    //                sqlCommand = "update tbl_booking_payment set confirm_payment=dateadd(hour,14,getdate()) where booking_payment_id=" + PaymentID;
    //                //Response.Write(sqlCommand);
    //                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
    //                cmd.ExecuteNonQuery();
    //            }


    //            FrontPaymentMethod objPayment = new FrontPaymentMethod();
    //            objPayment.BookingProcess(BookingID);
    //        }
    //    }
    //}
}