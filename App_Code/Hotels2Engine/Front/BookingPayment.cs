using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Booking;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Booking
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class BookingPayment:Hotels2BaseClass
    {
        private LinqBookingDataContext dcPayment = new LinqBookingDataContext();

        public int BookingPaymentID { get; set; }
        public int BookingID { get; set; }
        public byte PaymentCategoryID { get; set; }
        public byte GatewayID { get; set; }
        public decimal Amount { get; set; }
        public Nullable<decimal> Settle_Amount { get; set; }
        public string Title { get; set; }
        public DateTime DatePayment { get; set; }
        public Nullable<DateTime> ConfirmPayment { get; set; }
        public Nullable<DateTime> ConfirmSettle { get; set; }
        public bool Status { get; set; }
        public string Comment { get; set; }

        public BookingPayment()
        {
            DatePayment = DateTime.Now;
            ConfirmPayment = null;
            ConfirmSettle = null;

        }

        public BookingPayment GetBookingPaymentByID(int bookingPaymentID)
        {
            using(SqlConnection cn=new SqlConnection(this.ConnectionString))
            {
                string strCommand = "select booking_payment_id,booking_id,booking_payment_cat_id,gateway_id,amount,settle_amount,title,date_payment,confirm_payment,confirm_settle,status,comment";
                strCommand = strCommand + " from tbl_booking_payment";
                strCommand = strCommand + " where booking_payment_id=" + bookingPaymentID;
                SqlCommand cmd = new SqlCommand(strCommand, cn);
                cn.Open();
                //HttpContext.Current.Response.Write(sqlCommand+"<br>");

                SqlDataReader reader = cmd.ExecuteReader();
                BookingPayment item = new BookingPayment();

                if (reader.Read())
                {
                    item.BookingPaymentID = (int)reader["booking_payment_id"];
                    item.BookingID = (int)reader["booking_id"];
                    item.PaymentCategoryID = (byte)reader["booking_payment_cat_id"];
                    item.GatewayID = (byte)reader["gateway_id"];
                    item.Amount = (decimal)reader["amount"];

                    if (!string.IsNullOrEmpty(reader["settle_amount"].ToString()))
                    {
                        item.Settle_Amount = (decimal)reader["settle_amount"];
                    }

                    item.Title = reader["title"].ToString();
                    item.DatePayment = (DateTime)reader["date_payment"];

                    if (!string.IsNullOrEmpty(reader["confirm_payment"].ToString()))
                    {
                        item.ConfirmPayment = (DateTime)reader["confirm_payment"];
                    }

                    if (!string.IsNullOrEmpty(reader["confirm_settle"].ToString()))
                    {
                        item.ConfirmSettle = (DateTime)reader["confirm_settle"];
                    }

                    item.Status = (bool)reader["status"];
                    item.Comment = reader["comment"].ToString();
                }
                return item;
            }
            

        }

        public int Insert(BookingPayment data)
        {
            tbl_booking_payment payment = new tbl_booking_payment
            {
                //booking_payment_id=data.BookingPaymentID,
                booking_id = data.BookingID,
                booking_payment_cat_id = data.PaymentCategoryID,
                gateway_id = data.GatewayID,
                amount = data.Amount,
                settle_amount = data.Settle_Amount,
                title = data.Title,
                date_payment = data.DatePayment,
                //confirm_payment = data.ConfirmPayment,
                //confirm_settle = data.ConfirmSettle,
                status = data.Status,
                comment = data.Comment
            };

            dcPayment.tbl_booking_payments.InsertOnSubmit(payment);
            dcPayment.SubmitChanges();
            return payment.booking_payment_id;
        }

        public bool Update(BookingPayment data)
        {
            HttpContext.Current.Response.Write(data.BookingID + "<br>");
            HttpContext.Current.Response.Write(data.PaymentCategoryID + "<br>");
            HttpContext.Current.Response.Write(data.GatewayID + "<br>");
            HttpContext.Current.Response.Write(data.Amount + "<br>");
            HttpContext.Current.Response.Write(data.Settle_Amount + "<br>");
            HttpContext.Current.Response.Write(data.Title + "<br>");
            HttpContext.Current.Response.Write(data.DatePayment + "<br>");
            HttpContext.Current.Response.Write(data.ConfirmPayment + "<br>");
            HttpContext.Current.Response.Write(data.ConfirmSettle + "<br>");
            HttpContext.Current.Response.Write(data.Status + "<br>");
            HttpContext.Current.Response.Write(data.Comment + "<br>");

            return true;

            //tbl_booking_payment rsPayment = dcPayment.tbl_booking_payments.SingleOrDefault(item => item.booking_payment_id == data.BookingPaymentID);

            //rsPayment.booking_id = data.BookingID;
            //rsPayment.booking_payment_cat_id = data.PaymentCategoryID;
            //rsPayment.gateway_id = data.GatewayID;
            //rsPayment.amount = data.Amount;
            //rsPayment.settle_amount = data.Settle_Amount;
            //rsPayment.title = data.Title;
            //rsPayment.date_payment = data.DatePayment;
            //rsPayment.confirm_payment = data.ConfirmPayment;
            //rsPayment.confirm_settle = data.ConfirmSettle;
            //rsPayment.status = data.Status;
            //rsPayment.comment = data.Comment;

            //dcPayment.SubmitChanges();
            //return true;
        }


        public int UpdateConfirmPayment(int bookingPaymentID)
        {
            DataConnect objConn = new DataConnect();
            string sqlCommand = "update tbl_booking_payment set comfirm_payment=" + new DateTime().Hotels2ThaiDateTime() + " where booking_payment_id=" + bookingPaymentID;
            int result = objConn.ExecuteNonQuery(sqlCommand);
            objConn.Close();
            return result;

        }

        public int UpdateConfirmSettlement(int bookingPaymentID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string strCommand = "update tbl_booking_payment set comfirm_settlement=" + new DateTime().Hotels2ThaiDateTime() + " where booking_payment_id=" + bookingPaymentID;
                SqlCommand cmd = new SqlCommand(strCommand,cn);
                cn.Open();
                int result = cmd.ExecuteNonQuery();
                return result;
            }
            
        }

        public Dictionary<byte, string> GetGatewayAll()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                Dictionary<byte, string> dataList = new Dictionary<byte, string>();

                string strCommand = "select gateway_id,title from tbl_gateway order by title";
                SqlCommand cmd = new SqlCommand(strCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dataList.Add((byte)reader["gateway_id"], reader["title"].ToString());
                }
                return dataList;
            }
            
        }

        public Dictionary<byte, string> GetPaymentCategoryAll()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                Dictionary<byte, string> dataList = new Dictionary<byte, string>();

                string strCommand = "select booking_payment_cat_id, title from tbl_booking_payment_category order by title";
                SqlCommand cmd = new SqlCommand(strCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dataList.Add((byte)reader["booking_payment_cat_id"], reader["title"].ToString());
                }
                return dataList;
            }
            
        }

    }
}