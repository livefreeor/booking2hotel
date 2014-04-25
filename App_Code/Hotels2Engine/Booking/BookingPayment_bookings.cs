using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for BookingPaymentList
/// </summary>
/// 
namespace Hotels2thailand.Booking
{
    public class BookingPayment_bookings : Hotels2BaseClass
    {
        public int InsertNewBookingPayment(int inbBookingId, string strTitle, byte bytGatWay, byte bytCatID, decimal Amount)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {


                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_booking_payment (booking_id,booking_payment_cat_id,gateway_id,amount,title,date_payment,status)VALUES(@booking_id,@booking_payment_cat_id,@gateway_id,@amount,@title,@date_payment,@status); SET @booking_payment_id=SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = inbBookingId;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strTitle;
                cmd.Parameters.Add("@booking_payment_cat_id", SqlDbType.TinyInt).Value = bytCatID;
                //Default GateWay == 2
                cmd.Parameters.Add("@gateway_id", SqlDbType.TinyInt).Value = bytGatWay;
                cmd.Parameters.Add("@amount", SqlDbType.Money).Value = Amount;
                cmd.Parameters.Add("@date_payment", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = true;
                //cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = strComment;
                cmd.Parameters.Add("@booking_payment_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                ExecuteNonQuery(cmd);
                int ret = (int)cmd.Parameters["@booking_payment_id"].Value;


                if (ret > 0)
                {
                    SqlCommand cmdbank = new SqlCommand("INSERT INTO tbl_booking_payment_bank (booking_payment_id,date_submit,status) VALUES(@booking_payment_id,@date_submit,@status)", cn);
                    cmdbank.Parameters.Add("@booking_payment_id", SqlDbType.Int).Value = ret;
                    cmdbank.Parameters.Add("@date_submit", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                    cmdbank.Parameters.Add("@status", SqlDbType.Bit).Value = true;
                    ExecuteNonQuery(cmdbank);
                }
                return ret;
            }
        }


        public bool UpdateBookingPaymentTransferDetail(int intBookingId, int intbooking_payment_id, string strDetail)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_booking_payment SET comment = @comment WHERE booking_payment_id=@booking_payment_id", cn);
                cmd.Parameters.Add("@booking_payment_id", SqlDbType.Int).Value = intbooking_payment_id;
                cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = strDetail;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);

                return (ret == 1);
            }
        }

        public bool UpdateStatusBookingPayment(int intBookingId, int intbooking_payment_id)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_booking_payment SET status = @status WHERE booking_payment_id=@booking_payment_id", cn);
                cmd.Parameters.Add("@booking_payment_id", SqlDbType.Int).Value = intbooking_payment_id;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = false ;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                
                return (ret==1);
            }
        }

        public bool UpdateBookingPayment(int intbooking_payment_id, string strTitle, byte byteGateWayId, byte bytCatID, decimal Amount, string strComment, bool bolstatus)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_booking_payment SET title=@title, booking_payment_cat_id=@booking_payment_cat_id,gateway_id=@gateway_id, amount=@amount, comment=@comment, status = @status WHERE booking_payment_id=@booking_payment_id", cn);
                cmd.Parameters.Add("@booking_payment_id", SqlDbType.Int).Value = intbooking_payment_id;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strTitle;
                cmd.Parameters.Add("@booking_payment_cat_id", SqlDbType.TinyInt).Value = bytCatID;
                cmd.Parameters.Add("@gateway_id", SqlDbType.Money).Value = byteGateWayId;
                cmd.Parameters.Add("@amount", SqlDbType.Money).Value = Amount;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolstatus;
                cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = strComment;
               
                cn.Open();
                int ret =  ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }

        public bool UpdateBookingPaymentExtra(int intbooking_payment_id, string strTitle,  byte bytCatID, decimal Amount, string strComment, bool bolstatus)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_booking_payment SET title=@title, booking_payment_cat_id=@booking_payment_cat_id, amount=@amount, comment=@comment, status = @status WHERE booking_payment_id=@booking_payment_id", cn);
                cmd.Parameters.Add("@booking_payment_id", SqlDbType.Int).Value = intbooking_payment_id;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strTitle;
                cmd.Parameters.Add("@booking_payment_cat_id", SqlDbType.TinyInt).Value = bytCatID;
                //cmd.Parameters.Add("@gateway_id", SqlDbType.Money).Value = byteGateWayId;
                cmd.Parameters.Add("@amount", SqlDbType.Money).Value = Amount;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolstatus;
                cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = strComment;

                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }
        public bool UpdateBookingPaymentConfirm(int intBookingId, int intbooking_payment_id)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_booking_payment SET confirm_payment = @confirm_payment WHERE booking_payment_id=@booking_payment_id", cn);
                cmd.Parameters.Add("@booking_payment_id", SqlDbType.Int).Value = intbooking_payment_id;
                cmd.Parameters.Add("@confirm_payment", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                cn.Open();

                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }

        public bool UpdateBookingPaymentConfirmSettle(int intBookingId, int intbooking_payment_id)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_booking_payment SET confirm_settle = @confirm_settle WHERE booking_payment_id=@booking_payment_id", cn);
                cmd.Parameters.Add("@booking_payment_id", SqlDbType.Int).Value = intbooking_payment_id;
                cmd.Parameters.Add("@confirm_settle", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                cn.Open();

                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }

        public bool UpdateSwitchBackConfirmPayment(int intBookingId, int intbooking_payment_id)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_booking_payment SET confirm_payment = NULL WHERE booking_payment_id=@booking_payment_id", cn);
                cmd.Parameters.Add("@booking_payment_id", SqlDbType.Int).Value = intbooking_payment_id;
                //cmd.Parameters.Add("@confirm_payment", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                cn.Open();

                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }
        public bool UpdateSwitchBackConfirmSettle(int intBookingId, int intbooking_payment_id)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_booking_payment SET confirm_settle = NULL WHERE booking_payment_id=@booking_payment_id", cn);
                cmd.Parameters.Add("@booking_payment_id", SqlDbType.Int).Value = intbooking_payment_id;
                //cmd.Parameters.Add("@confirm_settle", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                cn.Open();

                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }

        //public bool BookingMove(int BookingFrom, int BookingTo)
        //{
        //    using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        //    {
        //        SqlTransaction trans = cn.BeginTransaction(IsolationLevel.ReadCommitted);
        //        try
        //        {

        //            trans.Commit();
        //        }catch(Exception ex)
        //        {
        //            trans.Rollback();
        //        }
        //    }
            
        //}
        
    }
}