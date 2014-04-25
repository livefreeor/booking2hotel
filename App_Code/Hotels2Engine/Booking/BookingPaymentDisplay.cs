using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Booking;
using Hotels2thailand.Staffs;
using System.Data;
using System.Data.SqlClient;
using Hotels2thailand.DataAccess;

/// <summary>
/// Summary description for BookingPaymentShow
/// </summary>
/// 
namespace Hotels2thailand.Booking
{
    public class BookingPaymentDisplay : Hotels2BaseClass
    {
        public int PaymentId { get; set; }
        public int BookingId { get; set; }
        public byte CatId { get; set; }
        public byte GateWayId { get; set; }
        public byte PaymentTypeId { get; set; }
        public string CatTitle { get; set; }
        public string GateWayTitle { get; set; }
        public decimal Amount { get; set; }
        public string Title { get; set; }
        public decimal SettleAmount { get; set; }
        public DateTime DatePayment { get; set; }
        public DateTime? ConfirmPayment { get; set; }
        public DateTime? ConfirmSettle { get; set; }
        public bool Status { get; set; }
        public string Comment { get; set; }
        public int intBookingPaymentBank {
            get { return this.intGetBookingPaymentBank(this.PaymentId); }
        }


        public int intGetBookingPaymentBank(int intPaymentId)
        {
            int intBookingPaymnetBankId = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("SELECT MAX(booking_payment_bank_id) FROM tbl_booking_payment_bank WHERE  booking_payment_id=@payment_id ", cn);
               
                cmd.Parameters.AddWithValue("@payment_id", intPaymentId);
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    intBookingPaymnetBankId = (reader[0] == DBNull.Value ? 0 : (int)reader[0]);
               
            }

            return intBookingPaymnetBankId;
        }

        public bool UpdatePaymentBankSend (int intPaymentId)
        {

            
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {

                SqlCommand cmd2 = new SqlCommand("UPDATE tbl_booking_payment_bank SET status= 0 , date_send= @date_send WHERE booking_payment_bank_id=@booking_payment_bank_id", cn);

                cmd2.Parameters.AddWithValue("@booking_payment_bank_id", intPaymentId);
                cmd2.Parameters.AddWithValue("@date_send", DateTime.Now.Hotels2ThaiDateTime());
                
                cn.Open();
                return (ExecuteNonQuery(cmd2) == 1);

                
            }

            
        }

        public int InsertBookingPaymentBank(int intPaymentId)
        {

            int intBookingPaymnetBankId = 0;
            string strConfirm = "";

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {

                SqlCommand cmd1 = new SqlCommand("SELECT confirm_payment FROM tbl_booking_payment WHERE booking_payment_id = @booking_payment_id", cn);
                cmd1.Parameters.AddWithValue("@booking_payment_id", intPaymentId);
                cn.Open();
                IDataReader reader = ExecuteReader(cmd1);
                
                if (reader.Read())
                    strConfirm = reader[0].ToString();
                    reader.Close();

                    if (string.IsNullOrEmpty(strConfirm))
                {
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO tbl_booking_payment_bank (booking_payment_id,date_submit,status) VALUES(@booking_payment_id,@date_submit,1);SET @booking_payment_bank_id = SCOPE_IDENTITY();", cn);

                    cmd2.Parameters.AddWithValue("@booking_payment_id", intPaymentId);
                    cmd2.Parameters.AddWithValue("@date_submit", DateTime.Now.Hotels2ThaiDateTime());
                    cmd2.Parameters.Add("@booking_payment_bank_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                    
                    ExecuteNonQuery(cmd2);

                    intBookingPaymnetBankId = (int)cmd2.Parameters["@booking_payment_bank_id"].Value;
                }
            }

            return intBookingPaymnetBankId;
        }

        public IList<object> GetBookingPayment_PaidOnlyByBookingId(int intBookingId)
        {
            

                StringBuilder query = new StringBuilder();
                query.Append("SELECT bpm.booking_payment_id, bpm.booking_id, bpm.booking_payment_cat_id, bpm.gateway_id,");
                query.Append(" (SELECT b.payment_type_id FROM tbl_booking b WHERE b.booking_id = bpm.booking_id ),");
                query.Append(" (SELECT sbpc.title FROM tbl_booking_payment_category sbpc where sbpc.booking_payment_cat_id = bpm.booking_payment_cat_id),");
                query.Append(" (SELECT sg.title booking_payment_id FROM tbl_gateway sg where sg.gateway_id = bpm.gateway_id),");
                query.Append(" bpm.amount, bpm.title, bpm.settle_amount, bpm.date_payment, bpm.confirm_payment, bpm.confirm_settle, bpm.status, bpm.comment From tbl_booking_payment bpm WHERE bpm.booking_id=@booking_id AND bpm.confirm_payment IS NOT NULL");
                query.Append(" AND bpm.status = 1 ORDER BY bpm.date_payment");

                using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                    cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingId;
                    cn.Open();
                    return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
                }


                //SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_booking_payment WHERE booking_id=@booking_id AND status=1 AND confirm_payment IS NOT NULL ", cn);
                //cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingId;
                //cn.Open();
                //return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            
        }


        public List<object> GEtPaymentByBookingId(int intBookingId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT bpm.booking_payment_id, bpm.booking_id, bpm.booking_payment_cat_id, bpm.gateway_id,");
            query.Append(" (SELECT b.payment_type_id FROM tbl_booking b WHERE b.booking_id = bpm.booking_id ),");
            query.Append(" (SELECT sbpc.title FROM tbl_booking_payment_category sbpc where sbpc.booking_payment_cat_id = bpm.booking_payment_cat_id),");
            query.Append(" (SELECT sg.title booking_payment_id FROM tbl_gateway sg where sg.gateway_id = bpm.gateway_id),");
            query.Append(" bpm.amount, bpm.title, bpm.settle_amount, bpm.date_payment, bpm.confirm_payment, bpm.confirm_settle, bpm.status, bpm.comment From tbl_booking_payment bpm WHERE bpm.booking_id=@booking_id");
            query.Append(" AND bpm.status = 1 ORDER BY bpm.date_payment");
            
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }



        public int GEtPaymentByBookingIdNotConfirm(int intBookingId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT COUNT(bpm.booking_payment_id)");
            query.Append(" From tbl_booking_payment bpm WHERE bpm.confirm_payment IS NULL");
            query.Append(" AND bpm.status = 1 AND bpm.booking_id=@booking_id ");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingId;
                cn.Open();

                return (int)ExecuteScalar(cmd);
                
            }
        }


        public List<object> GEtPaymentByBookingIdBookingMove(int intBookingId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT bpm.booking_payment_id, bpm.booking_id, bpm.booking_payment_cat_id, bpm.gateway_id,");
            query.Append(" (SELECT b.payment_type_id FROM tbl_booking b WHERE b.booking_id = bpm.booking_id ),");
            query.Append(" (SELECT sbpc.title FROM tbl_booking_payment_category sbpc where sbpc.booking_payment_cat_id = bpm.booking_payment_cat_id),");
            query.Append(" (SELECT sg.title booking_payment_id FROM tbl_gateway sg where sg.gateway_id = bpm.gateway_id),");
            query.Append(" bpm.amount, bpm.title, bpm.settle_amount, bpm.date_payment, bpm.confirm_payment, bpm.confirm_settle, bpm.status, bpm.comment From tbl_booking_payment bpm WHERE bpm.booking_id=@booking_id");
            query.Append(" ORDER BY bpm.date_payment");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public BookingPaymentDisplay GEtPaymentByPaymentId(int intPaymentId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT bpm.booking_payment_id, bpm.booking_id, bpm.booking_payment_cat_id, bpm.gateway_id,");
            query.Append(" (SELECT b.payment_type_id FROM tbl_booking b WHERE b.booking_id = bpm.booking_id ),");
            query.Append(" (SELECT sbpc.title FROM tbl_booking_payment_category sbpc where sbpc.booking_payment_cat_id = bpm.booking_payment_cat_id),");
            query.Append(" (SELECT sg.title booking_payment_id FROM tbl_gateway sg where sg.gateway_id = bpm.gateway_id),");
            query.Append(" bpm.amount, bpm.title, bpm.settle_amount, bpm.date_payment, bpm.confirm_payment, bpm.confirm_settle, bpm.status, bpm.comment From tbl_booking_payment bpm WHERE bpm.booking_payment_id=@booking_payment_id");
            

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@booking_payment_id", SqlDbType.Int).Value = intPaymentId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (BookingPaymentDisplay)MappingObjectFromDataReader(reader);
                else
                    return null;
                
            }
        }



        public BookingPaymentDisplay GEtPaymentByBookingId_Latest(int intBookingID)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT TOP 1 bpm.booking_payment_id, bpm.booking_id, bpm.booking_payment_cat_id, bpm.gateway_id,");
            query.Append(" (SELECT b.payment_type_id FROM tbl_booking b WHERE b.booking_id = bpm.booking_id ),");
            query.Append(" (SELECT sbpc.title FROM tbl_booking_payment_category sbpc where sbpc.booking_payment_cat_id = bpm.booking_payment_cat_id),");
            query.Append(" (SELECT sg.title booking_payment_id FROM tbl_gateway sg where sg.gateway_id = bpm.gateway_id),");
            query.Append(" bpm.amount, bpm.title, bpm.settle_amount, bpm.date_payment, bpm.confirm_payment, bpm.confirm_settle, bpm.status, bpm.comment From tbl_booking_payment bpm WHERE bpm.booking_id=@booking_id ORDER BY date_payment DESC ");


            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingID;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (BookingPaymentDisplay)MappingObjectFromDataReader(reader);
                else
                    return null;

            }
        }

        public bool BookingPaymenyMove(int BookingFrom, int BookingTo, int? AffSiteId, int intPaymentIdFrom)
        {
            bool result = false;
            int ret = 0;
            int affSiteId = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                cn.Open();

                SqlCommand cmd = new SqlCommand("UPDATE tbl_booking_payment SET booking_id = @bookingIdTo WHERE booking_payment_id = @paymentFrom", cn);
                cmd.Parameters.Add("@bookingIdTo", SqlDbType.Int).Value = BookingTo;
                cmd.Parameters.Add("@paymentFrom", SqlDbType.Int).Value = intPaymentIdFrom;
                ret = ExecuteNonQuery(cmd);
                 


                

                SqlCommand cmd1 = new SqlCommand("SELECT ISNULL(aff_site_id,0) FROM tbl_booking WHERE booking_id=@booking_id",cn);
                cmd1.Parameters.Add("@booking_id", SqlDbType.Int).Value = BookingFrom;

                affSiteId = (int)ExecuteScalar(cmd1);

                if (affSiteId != 0)
                {

                    SqlCommand cmd2 = new SqlCommand("UPDATE tbl_booking SET aff_site_id= @aff_site_id WHERE booking_id = @booking_idTo", cn);
                    cmd2.Parameters.Add("@aff_site_id", SqlDbType.Int) .Value = affSiteId;
                    cmd2.Parameters.Add("@booking_idTo", SqlDbType.Int).Value = BookingTo;
                    ret = ExecuteNonQuery(cmd2);

                    SqlCommand cmd3 = new SqlCommand("UPDATE tbl_booking SET aff_site_id = NULL WHERE booking_id = @booking_idFrom", cn);
                    cmd3.Parameters.Add("@booking_idFrom", SqlDbType.Int).Value = BookingFrom;
                    ret = ExecuteNonQuery(cmd3);

                    SqlCommand cmd4 = new SqlCommand("SELECT * FROM tbl_site_order WHERE booking_id = @booking_idFrom",cn);
                    cmd4.Parameters.Add("@booking_idFrom", SqlDbType.Int).Value = BookingFrom;
                    IDataReader reader = ExecuteReader(cmd4, CommandBehavior.SingleRow);
                    if (reader.Read())
                    {
                        using (SqlConnection cn1 = new SqlConnection(this.ConnectionString))
                        {
                            SqlCommand cmd5 = new SqlCommand("UPDATE tbl_site_order SET booking_id = @booking_idTo WHERE site_id = @site_id AND booking_id = @booking_idFrom", cn1);
                            cmd5.Parameters.Add("@booking_idTo", SqlDbType.Int).Value = BookingTo;
                            cmd5.Parameters.Add("@booking_idFrom", SqlDbType.Int).Value = BookingFrom;
                            cmd5.Parameters.Add("@site_id", SqlDbType.Int).Value = affSiteId;
                            cn1.Open();
                            ret = ExecuteNonQuery(cmd5);
                        }
                    }

                    
                }
                
            }

            result = (ret == 1);
            return result;
        }

        
        
    }
}