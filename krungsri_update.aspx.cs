﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand;
using Hotels2thailand.Front;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class krungsri_update : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int PaymentBankID = 0;
        int PaymentID = 0;
        int BookingID = 0;
        string bankData = string.Empty;
        bankData = bankData + "Post:" + Request.Form["respcode"] + "--PaymentID:" + Request.Form["ordernumber"];
        //bankData = bankData + "Get:" + Request.QueryString["PMGWRESP"];
        Hotels2MAilSender.SendmailBooking("visa test", "kiasa555@gmail.com", "Data from krungsri", "", bankData);

        string sqlCommand = string.Empty;
        string connString = WebConfigurationManager.ConnectionStrings["booking2hotelXConnectionString"].ConnectionString;


        if (!string.IsNullOrEmpty(Request.Form["respcode"]))
        {
            bankData = Request.Form["respcode"];

            FrontPaymentMethod payment = new FrontPaymentMethod();
            if (bankData == "00")
            {
                PaymentBankID = int.Parse(Request.Form["ordernumber"]);

                sqlCommand = "select bpay.booking_id,bpay.booking_payment_id";
                sqlCommand = sqlCommand + " from tbl_booking_payment bpay,tbl_booking_payment_bank bpayb";
                sqlCommand = sqlCommand + " where bpay.booking_payment_id=bpayb.booking_payment_id";
                sqlCommand = sqlCommand + " and bpayb.booking_payment_bank_id=" + PaymentBankID;
                //Response.Write(sqlCommand);
                //Response.Flush();
                using (SqlConnection cn = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    BookingID = (int)reader["booking_id"];
                    PaymentID = (int)reader["booking_payment_id"];
                }

                using (SqlConnection cn = new SqlConnection(connString))
                {
                    cn.Open();
                    sqlCommand = "update tbl_booking_payment set confirm_payment=dateadd(hour,14,getdate()) where booking_payment_id=" + PaymentID;
                    SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                    cmd.ExecuteNonQuery();
                }



                //--Allotment Process
                FrontPaymentMethod objPayment = new FrontPaymentMethod();
                objPayment.BookingProcess(BookingID);
            }

        }
        else
        {
            Response.Write("Nothing");
        }
    }
}