using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using Hotels2thailand.DataAccess;
using System.Data;
using System.Data.SqlClient;
using Hotels2thailand.Booking;
using Hotels2thailand;
using System.Web.Configuration;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Hotels2thailand.Staffs;

public partial class order_complete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Case BLL ONLY 

       
        if (!this.Page.IsPostBack)
        {
            StringBuilder result = new StringBuilder();
            string strHeaderTxt = "";
            if (!String.IsNullOrEmpty(Request.QueryString["Ref"]))
            {
                int PaymentBankID = 0;

                PaymentBankID = Convert.ToInt32(Request.QueryString["Ref"]);

            
                if (PaymentBankID >= 30000000)
                {
                    Response.Redirect("http://b2b25.booking2hotels.com/order-complete.aspx?Ref=" + Request.QueryString["Ref"]);
                    Response.End();
                }

               
               // int PaymentBankID = Convert.ToInt32(Request.QueryString["Ref"]);
                int PaymentID = 0;

                int BookingID = 0;
                int intBookingHotelID = 0;
                int intChainID = 0;
                string sqlCommand = string.Empty;
                string connString = WebConfigurationManager.ConnectionStrings["booking2hotelXConnectionString"].ConnectionString;

                sqlCommand = "select bpay.booking_id,bpay.booking_payment_id";
                sqlCommand = sqlCommand + " from tbl_booking_payment bpay,tbl_booking_payment_bank bpayb";
                sqlCommand = sqlCommand + " where bpay.booking_payment_id=bpayb.booking_payment_id";
                sqlCommand = sqlCommand + " and bpayb.booking_payment_bank_id=" + PaymentBankID;

                using (SqlConnection cn = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        BookingID = (int)reader["booking_id"];
                        PaymentID = (int)reader["booking_payment_id"];
                    }

                    reader.Close();


                    SqlCommand cmd2 = new SqlCommand("SELECT bh.booking_hotel_id, pch.chain_id FROM tbl_booking b, tbl_booking_product bp, tbl_booking_hotels bh,tbl_product_chain pch WHERe b.booking_id = bp.booking_id AND bp.product_id = bh.product_id AND b.booking_id = bh.booking_id AND pch.product_id = bp.product_id AND b.booking_id =" + BookingID, cn);

                    IDataReader reader1 = cmd2.ExecuteReader();
                    if (reader1.Read())
                    {
                        intBookingHotelID = (int)reader1[0];
                        intChainID = (int)reader1[1];
                    }


                }

                bool IsConfirm = false;
                //Check Paid Already 
                using (SqlConnection cn = new SqlConnection(connString))
                {
                    cn.Open();
                    sqlCommand = "SELECT confirm_payment FROM tbl_booking_payment WHERE confirm_payment IS NOT NULL AND booking_payment_id=" + PaymentID;
                    //sqlCommand = "update tbl_booking_payment set confirm_payment=dateadd(hour,14,getdate()) where booking_payment_id=" + PaymentID;
                    //Response.Write(sqlCommand);
                    SqlCommand cmd = new SqlCommand(sqlCommand, cn);

                    if (cmd.ExecuteScalar() == null)
                        IsConfirm = false;
                    else
                        IsConfirm = true;
                }

               
               
                if (IsConfirm)
                {

                    result.Append("<p>");

                    result.Append("Your reservation is confirmed and paid in full. Your order number is #" + intBookingHotelID + ". A voucher confirmation will be sent to your email in shortly. In case there is ether some technical error or your inaccurate e-mail please kindly send us a reminder or recheck your email address.");
                    result.Append("</p>");

                    strHeaderTxt = "thank you: Your reservation is confirmed and paid in full";

                }
                else
                {

                    result.Append("<p>Your reservation will be processed and revert to you in shortly.Your order id is");
                    result.Append("<span style=\"color:#F00; font-weight:bold\">#" + intBookingHotelID + ".</span> Under normal circumstances,");
                    result.Append("<span style=\"color:#093;\">you will hear from us within 24 hours.</span> In case there is ether some technical error or your inaccurate e-mail ");
                    result.Append(" please kindly send us a reminder or recheck your email address.");
                    result.Append("</p>");

                    strHeaderTxt = "thank you: Your reservation will be processed";
                    //this.Page.Header.Title = "HHHH";

                }

                wording.Text = result.ToString();
                //string[] queryString = Request.QueryString["ch"].Split('@');
                //StaffChain cChain = new StaffChain();
                //int intChainID = cChain.GetChainIDByAgentID(intChainID));


                lblLogo.Text = "<img src=\"/images/chain/img_ch_" + intChainID + ".jpg\" />";
                this.Page.Header.Title = strHeaderTxt;

                //bookinghotelID.Text = queryString[1];


                //int intPID = Convert.ToInt32(Request.QueryString["PID"]);
                //frmCheckRate.Action = "ProductDetail.aspx?PID=" + intPID;
                //Agent cAgent = new Agent();
                //cAgent = cAgent.GetAgentProfileByProductID(int.Parse(queryString[0]), intPID);
                //lbtAgentName.Text = cAgent.agent_name;


            }


            if(!String.IsNullOrEmpty(Request.QueryString["Com"]))
            {
                if (Request.QueryString["Com"] == "yes")
                {

                    result.Append("<p>");

                    result.Append("Your reservation is confirmed and paid in full. Your order number is #" + Request.QueryString["bhid"] + ". A voucher confirmation will be sent to your email in shortly. In case there is ether some technical error or your inaccurate e-mail please kindly send us a reminder or recheck your email address.");
                    result.Append("</p>");

                    strHeaderTxt = "thank you: Your reservation is confirmed and paid in full";

                }
                else
                {

                    result.Append("<p>Your reservation will be processed and revert to you in shortly.Your order id is");
                    result.Append("<span style=\"color:#F00; font-weight:bold\">#" + Request.QueryString["bhid"] + ".</span> Under normal circumstances,");
                    result.Append("<span style=\"color:#093;\">you will hear from us within 24 hours.</span> In case there is ether some technical error or your inaccurate e-mail ");
                    result.Append(" please kindly send us a reminder or recheck your email address.");
                    result.Append("</p>");

                    strHeaderTxt = "thank you: Your reservation will be processed";
                    //this.Page.Header.Title = "HHHH";

                }

                this.Page.Header.Title = strHeaderTxt;

                wording.Text = result.ToString();

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