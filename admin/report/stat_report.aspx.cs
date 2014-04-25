using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;
using Hotels2thailand.Front;

public partial class temp2_stat_report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataConnect objConn=new DataConnect();
        short destination=short.Parse(Request.QueryString["dest"]);
        short location = 0;
        byte category = byte.Parse(Request.QueryString["cat"]);
        byte sortBy = byte.Parse(Request.QueryString["sortby"]);
        string isExtranet=Request.QueryString["ext"];

        if(isExtranet=="on")
        {
            isExtranet="1";
        }else{
            isExtranet="0";
        }

        bool checkAdmin = false;
        if(!string.IsNullOrEmpty(Request.QueryString["l6fpvf"]))
        {
            checkAdmin = true;
        }

        DateTime date_start = Utility.ConvertDateInput(Request.QueryString["date_in"]);
        DateTime date_end = Utility.ConvertDateInput(Request.QueryString["date_out"]);

        if (!string.IsNullOrEmpty(Request.QueryString["loc"]))
        {
            location = byte.Parse(Request.QueryString["loc"]);
        }

       

        string sqlCommand = "rp_booking_stat '" + destination + "','" + location + "','" + category + "'," + date_start.Hotels2DateToSQlString() + "," + date_end.Hotels2DateToSQlString() + ",'"+isExtranet+"','" + sortBy + "'";

        //Response.Write(sqlCommand);
        SqlDataReader reader = objConn.GetDataReader(sqlCommand);

        string statDisplay = "<table class=\"tbl_stat\">\n";

        if (checkAdmin)
        {
            statDisplay = statDisplay + "<th rowspan=\"2\">Hotel</th><th colspan=\"2\">All</th><th colspan=\"2\">Paid</th><th colspan=\"2\">Complete</th>\n";
            statDisplay = statDisplay + "<tr>\n";
            statDisplay = statDisplay+"<th>Booking</th><th>Revenue</th><th>booking</th><th>Revenue</th><th>Booking</th><th>Revenue</th>\n";
            statDisplay = statDisplay+"</tr>\n";
        }else{
            statDisplay = statDisplay + "<tr>\n";
            statDisplay = statDisplay + "<th>Hotel</th><th>Booking All</th><th>Booking Paid</th><th>Booking Complete</th>\n";
            statDisplay = statDisplay + "</tr>\n";
        }
  	    

        int countRow = 0;
        int sumBooking= 0;
        int sumBookingPaid = 0;
        int sumBookingComplete=0;
        decimal sumRevenue = 0;
        decimal sumRevenuePaid = 0;
        decimal sumRevenueComplete = 0;

        while(reader.Read())
        {
           
           if (countRow %2==0)
           {
            statDisplay = statDisplay + "<tr align=\"right\" bgColor=\"#FFFFFF\">\n";
           }else{
               statDisplay = statDisplay + "<tr align=\"right\" bgColor=\"#F2F2F2\">\n";
           }
           sumBooking = sumBooking+ (int)reader["booking_all"];
           sumBookingPaid = sumBookingPaid + (int)reader["booking_paid_all"];
           sumBookingComplete = sumBookingComplete + (int)reader["booking_complete"];
           sumRevenue = sumRevenue + (decimal)reader["revenue_all"];
           sumRevenuePaid = sumRevenuePaid + (decimal)reader["booking_paid_revenue"];
           sumRevenueComplete = (decimal)reader["booking_revenue_complete"];

           if (checkAdmin)
           {
               statDisplay = statDisplay + "<td align=\"left\">" + reader["title"] + "</td><td>" + reader["booking_all"] + "</td><td align=\"right\">" + string.Format("{0:#,0}", reader["revenue_all"]) + "</td><td>" + reader["booking_paid_all"].ToString() + "</td><td align=\"right\">" + string.Format("{0:#,0}", reader["booking_paid_revenue"]) + "</td><td>" + reader["booking_complete"] + "</td><td align=\"right\">" + string.Format("{0:#,0}", reader["booking_revenue_complete"]) + "</th>\n";
           }
           else {
               statDisplay = statDisplay + "<td align=\"left\">" + reader["title"] + "</td><td>" + reader["booking_all"] + "</td><td>" + reader["booking_paid_all"].ToString() + "</td><td>" + reader["booking_complete"] + "</td>\n";
           }
            statDisplay = statDisplay + "</tr>\n";
            countRow = countRow+1;
        }

        if (!checkAdmin)
        {
            statDisplay = statDisplay + "<tr align=\"right\" bgColor=\"#cfeaf1\">\n";
            statDisplay = statDisplay + "<td align=\"left\"><strong>Total</strong></td><td><strong>" + sumBooking + "</strong></td><td><strong>" + sumBookingPaid + "</strong></td><td><strong>" + sumBookingComplete + "</strong></td>\n";
            statDisplay = statDisplay + "</tr>\n";
        }
        else {
            statDisplay = statDisplay + "<tr align=\"right\" bgColor=\"#cfeaf1\">\n";
            statDisplay = statDisplay + "<td align=\"left\"><strong>Total</strong></td><td><strong>" + sumBooking + "</strong></td><td><strong>" + string.Format("{0:#,0}", sumRevenue) + "</strong></td><td><strong>" + sumBookingPaid + "</strong></td><td><strong>" + string.Format("{0:#,0}", sumRevenuePaid) + "</strong></td><td><strong>" + sumBookingComplete + "</strong></td><td><strong>" + string.Format("{0:#,0}", sumRevenueComplete) + "</strong></td>\n";
            statDisplay = statDisplay + "</tr>\n";
        }
        
        statDisplay = statDisplay + "</table>";
        Response.Write(statDisplay);
    }
}