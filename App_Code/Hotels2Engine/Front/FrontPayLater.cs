using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Hotels2thailand;
using Hotels2thailand.DataAccess;

/// <summary>
/// Summary description for FrontPayLater
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class FrontPayLater:Hotels2BaseClass
    {
        public int ProductID { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public byte DayPayment { get; set; }
        public byte DepositPercent { get; set; }
        public byte DepositRoomNight { get; set; }

        public FrontPayLater()
        {
            ProductID = 0;
            //
            // TODO: Add constructor logic here
            //
        }
        public void GetPayLaterByDate(int ProductID, DateTime DateStart)
        {

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select pplan.product_id,pplan.date_start,pplan.date_end,pplan.day_payment,pplan.deposit_percent,pplan.deposit_room_night";
                sqlCommand = sqlCommand + " from tbl_product p,tbl_product_payment_plan pplan";
                sqlCommand = sqlCommand + " where p.product_id=pplan.product_id and p.product_id=" + ProductID;
                sqlCommand = sqlCommand + " and " + DateStart.Hotels2DateToSQlString() + " between pplan.date_start and pplan.date_end";
                sqlCommand = sqlCommand + " and p.payment_type_id=2 and datediff(day,dateadd(hour,14,getDate())," + DateStart.Hotels2DateToSQlString() + ")>=pplan.day_advance";
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                FrontPayLater result = new FrontPayLater();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    this.ProductID = (int)reader["product_id"];
                    this.DateStart = (DateTime)reader["date_start"];
                    this.DateEnd = (DateTime)reader["date_end"];
                    this.DayPayment = (byte)reader["day_payment"];
                    this.DepositPercent = (byte)reader["deposit_percent"];
                    this.DepositRoomNight = (byte)reader["deposit_room_night"];
                }
            }

        }

        public string GetPayLaterPlan(int ProductID)
        {
            string result = string.Empty;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select pplan.product_id,pplan.date_start,pplan.date_end,pplan.day_payment,pplan.deposit_percent,pplan.deposit_room_night,pplan.day_advance";
                sqlCommand = sqlCommand + " from tbl_product p,tbl_product_payment_plan pplan";
                sqlCommand = sqlCommand + " where p.product_id=pplan.product_id and p.product_id=" + ProductID;
                sqlCommand = sqlCommand + " and p.payment_type_id=2 and pplan.date_end>GETDATE()";
                sqlCommand = sqlCommand + " order by pplan.date_start desc";
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                int rowCount = 1;
                while (reader.Read())
                {
                    if (rowCount == 1)
                    {
                        result = result + "<table class=\"tblNoColor\">";
                        result = result + "<tr><td colspan=\"2\"><strong>Book now pay later</strong></td></tr>";
                        result = result + "<tr><td colspan=\"2\">Book Today, Pay Later Just $1 to secure your rooms / products today and pay the rest at date that is closure to check in</td></tr>";

                    }

                    result = result + "<tr><td><br/><strong>Stay Period:</strong> " + Convert.ToDateTime(reader["date_start"]).ToString("MMM dd, yyyy") + " - " + Convert.ToDateTime(reader["date_end"]).ToString("MMM dd, yyyy") + "<br/><strong>Advance Booking Requires:</strong> " + reader["day_advance"] + " Days before check in date.</td></tr>";
                    rowCount = rowCount + 1;
                }


                if (rowCount > 1)
                {

                    result = result + "</table>";
                }
            }
            return result;
        }
        public string GetPayLaterPlanByDate(int ProductID,DateTime DateCheck)
        {
            string result = string.Empty;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select pplan.product_id,pplan.date_start,pplan.date_end,pplan.day_payment,pplan.deposit_percent,pplan.deposit_room_night,pplan.day_advance";
                sqlCommand = sqlCommand + " from tbl_product p,tbl_product_payment_plan pplan";
                sqlCommand = sqlCommand + " where p.product_id=pplan.product_id and p.product_id=" + ProductID;
                sqlCommand = sqlCommand + " and " + DateCheck.Hotels2ThaiDateTime().Hotels2DateToSQlString() + " between pplan.date_start and pplan.date_end";
                sqlCommand = sqlCommand + " and datediff(day,getdate()," + DateCheck.Hotels2ThaiDateTime().Hotels2DateToSQlString() + ")>pplan.day_advance";
                sqlCommand = sqlCommand + " and p.payment_type_id=2";
                sqlCommand = sqlCommand + " order by pplan.date_start desc";

                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    result = "<strong>Book now pay later</strong><br/>Book Today, Pay Later Just $1 to secure your rooms / products today and pay the rest at date that is closure to check in";
                }

            }
            

            //HttpContext.Current.Response.Write(sqlCommand);
            
            //int rowCount = 1;
            //while (reader.Read())
            //{
            //    if (rowCount == 1)
            //    {
            //        result = result + "<table class=\"tblNoColor\">";
            //        result = result + "<tr><td colspan=\"2\"><strong>Book now pay later condition</strong></td></tr>";


            //    }
            //    result = result + "<tr><td>" + Convert.ToDateTime(reader["date_start"]).ToString("dd MMM yyyy") + "-" + Convert.ToDateTime(reader["date_end"]).ToString("dd MMM yyyy") + "</td><td> advance: " + reader["day_advance"] + " days</td></tr>";
            //    rowCount = rowCount + 1;
            //}


            //if (rowCount > 1)
            //{

            //    result = result + "</table>";
            //}

            
            return result;
        }
    }
}