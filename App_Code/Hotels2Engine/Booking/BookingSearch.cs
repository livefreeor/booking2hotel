using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.Booking;
using System.Data.SqlClient;

/// <summary>
/// Summary description for BookingActivity
/// </summary>
/// 
namespace Hotels2thailand.Booking
{
    public class BookingSearch : Hotels2BaseClass
    {
        public int BookingID { get; set; }
        public int BookingHotelId { get; set; }
        public string ProductTitle { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string BookingName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime DateSubmit { get; set; }
        public bool Status { get; set; }

        public List<object> SearchResultBookingId(string KeyWord)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT b.booking_id, bh.booking_hotel_id, pc.title, bp.date_time_check_in , bp.date_time_check_out, b.name_full, b.email, pc.address, b.date_submit, b.status");
            query.Append(" FROM tbl_booking b, tbl_booking_product bp, tbl_product_content pc, tbl_booking_hotels bh, tbl_product_booking_engine bg");
            query.Append(" WHERE bp.product_id=pc.product_id AND bh.booking_id = b.booking_id AND bh.product_id = bp.product_id AND b.booking_id= bp.booking_id AND b.lang_id=pc.lang_id AND bg.product_id = bp.product_id AND bg.manage_id = 2  AND   b.booking_id=@booking_id");


            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = int.Parse(KeyWord.Trim());
                
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
        public List<object> SearchResultBookingHotelId(string KeyWord)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT b.booking_id, bh.booking_hotel_id, pc.title, bp.date_time_check_in , bp.date_time_check_out, b.name_full, b.email, pc.address, b.date_submit, b.status");
            query.Append(" FROM tbl_booking b, tbl_booking_product bp, tbl_product_content pc, tbl_booking_hotels bh, tbl_product_booking_engine bg");
            query.Append(" WHERE bp.product_id=pc.product_id AND bg.product_id = bp.product_id AND bg.manage_id = 2  AND bp.booking_id=b.booking_id AND b.lang_id=pc.lang_id AND  b.booking_id = bh.booking_id AND bh.product_id = bp.product_id AND bh.booking_hotel_id = @booking_hotel_id");


            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@booking_hotel_id", SqlDbType.Int).Value = int.Parse(KeyWord.Trim());
                
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
        public List<object> SearchResultBookingDate(string KeyWordDate_start, string KeyWordDate_end)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT b.booking_id, bh.booking_hotel_id, pc.title, bp.date_time_check_in , bp.date_time_check_out, b.name_full, b.email, pc.address, b.date_submit, b.status");
            query.Append(" FROM tbl_booking b, tbl_booking_product bp, tbl_product_content pc, tbl_booking_hotels bh , tbl_product_booking_engine bg");
            query.Append(" WHERE bp.product_id=pc.product_id AND bg.product_id = bp.product_id AND bg.manage_id = 2  AND bp.booking_id=b.booking_id AND b.lang_id=pc.lang_id AND  b.booking_id = bh.booking_id AND bh.product_id = bp.product_id AND b.date_submit BETWEEN '" + KeyWordDate_start + "' AND '" + KeyWordDate_end + "'");


            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> SearchResultBookingCheckin(string KeyWordDate_start, string KeyWordDate_end)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT b.booking_id, bh.booking_hotel_id, pc.title, bp.date_time_check_in , bp.date_time_check_out, b.name_full, b.email, pc.address, b.date_submit, b.status");
            query.Append(" FROM tbl_booking b, tbl_booking_product bp, tbl_product_content pc, tbl_booking_hotels bh , tbl_product_booking_engine bg");
            query.Append(" WHERE bp.product_id=pc.product_id AND bg.product_id = bp.product_id AND bg.manage_id = 2  AND bp.booking_id=b.booking_id AND b.lang_id=pc.lang_id AND  b.booking_id = bh.booking_id AND bh.product_id = bp.product_id AND bp.date_time_check_in BETWEEN '" + KeyWordDate_start + "' AND '" + KeyWordDate_end + "'");


            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
        public List<object> SearchResultBookingCheckOut(string KeyWordDate_start, string KeyWordDate_end)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT b.booking_id, bh.booking_hotel_id, pc.title, bp.date_time_check_in , bp.date_time_check_out, b.name_full, b.email, pc.address, b.date_submit, b.status");
            query.Append(" FROM tbl_booking b, tbl_booking_product bp, tbl_product_content pc, tbl_booking_hotels bh, tbl_product_booking_engine bg");
            query.Append(" WHERE bp.product_id=pc.product_id AND bg.product_id = bp.product_id AND bg.manage_id = 2  AND bp.booking_id=b.booking_id AND b.lang_id=pc.lang_id AND  b.booking_id = bh.booking_id AND bh.product_id = bp.product_id AND bp.date_time_check_out BETWEEN '" + KeyWordDate_start + "' AND '" + KeyWordDate_end + "'");


            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> SearchResultbookingPayment(string KeyWord)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT b.booking_id, bh.booking_hotel_id, pc.title, bp.date_time_check_in , bp.date_time_check_out, b.name_full, b.email, pc.address, b.date_submit, b.status FROM tbl_booking_payment bpy, tbl_booking_payment_bank bpb, tbl_booking b,tbl_booking_product bp, tbl_product_content pc, tbl_booking_hotels bh, tbl_product_booking_engine bg WHERE b.booking_id = bpy.booking_id AND bpy.booking_payment_id = bpb.booking_payment_id AND bp.booking_id = b.booking_id  AND pc.product_id = bp.product_id AND bh.product_id= bp.product_id AND bh.booking_id = b.booking_id AND pc.lang_id = 1 AND bg.product_id = bp.product_id AND bpb.booking_payment_bank_id = @booking_payment_id");
     
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@booking_payment_id", SqlDbType.Int).Value = int.Parse(KeyWord.Trim());
                
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
        public List<object> SearchResultbookingName(string KeyWord)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT b.booking_id, bh.booking_hotel_id, pc.title, bp.date_time_check_in , bp.date_time_check_out, b.name_full, b.email, pc.address, b.date_submit, b.status");
            query.Append(" FROM tbl_product p, tbl_product_content pc, tbl_booking b , tbl_booking_product bp, tbl_booking_hotels bh, tbl_product_booking_engine bg");
            query.Append(" WHERE p.product_id = pc.product_id  AND bg.product_id = bp.product_id AND bg.manage_id = 2  AND bh.booking_id=b.booking_id AND bh.product_id=bp.product_id AND pc.lang_id = 1 AND p.product_id = bp.product_id ");
            query.Append(" AND b.booking_id = bp.booking_id ");
            query.Append(" AND b.name_full LIKE '%" + KeyWord.Trim() + "%'");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> SearchResultbookingEmail(string KeyWord)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT b.booking_id, bh.booking_hotel_id, pc.title, bp.date_time_check_in , bp.date_time_check_out, b.name_full, b.email, pc.address, b.date_submit, b.status");
            query.Append(" FROM tbl_product p, tbl_product_content pc, tbl_booking b , tbl_booking_product bp, tbl_booking_hotels bh, tbl_product_booking_engine bg");
            query.Append(" WHERE p.product_id = pc.product_id AND bg.product_id = bp.product_id AND bg.manage_id = 2  AND bh.booking_id = b.booking_id AND bh.product_id = bp.product_id AND pc.lang_id = 1 AND p.product_id = bp.product_id ");
            query.Append(" AND b.booking_id = bp.booking_id ");
            query.Append(" AND b.email LIKE '%" + KeyWord.Trim() + "%'");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> SearchResultProductAddress(string KeyWord)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT b.booking_id, bh.booking_hotel_id, pc.title, bp.date_time_check_in , bp.date_time_check_out, b.name_full, b.email, pc.address, b.date_submit, b.status");
            query.Append(" FROM tbl_product p, tbl_product_content pc, tbl_booking b , tbl_booking_product bp, tbl_booking_hotels bh, tbl_product_booking_engine bg");
            query.Append(" WHERE p.product_id = pc.product_id AND bg.product_id = bp.product_id AND bg.manage_id = 2  AND bh.booking_id = b.booking_id AND bh.product_id= bp.product_id AND pc.lang_id = 1 AND p.product_id = bp.product_id ");
            query.Append(" AND b.booking_id = bp.booking_id ");
            query.Append(" AND pc.address LIKE '%" + KeyWord.Trim() + "%'");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> SearchResultProductName(string KeyWord)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT b.booking_id, bh.booking_hotel_id, pc.title, bp.date_time_check_in , bp.date_time_check_out, b.name_full, b.email, pc.address, b.date_submit, b.status");
            query.Append(" FROM tbl_product p, tbl_product_content pc, tbl_booking b , tbl_booking_product bp, tbl_booking_hotels bh, tbl_product_booking_engine bg");
            query.Append(" WHERE p.product_id = pc.product_id AND bg.product_id = bp.product_id AND bg.manage_id = 2  AND bh.booking_id = b.booking_id AND bh.product_id= bp.product_id AND pc.lang_id = 1 AND p.product_id = bp.product_id ");
            query.Append(" AND b.booking_id = bp.booking_id ");
            query.Append(" AND pc.title LIKE '%" + KeyWord.Trim() + "%'");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
    }
}
