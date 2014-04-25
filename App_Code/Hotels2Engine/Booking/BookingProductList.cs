using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;
using System.Data;
using Hotels2thailand.Production;

using Hotels2thailand.LinqProvider.Production;

/// <summary>
/// Summary description for BookingProductShow
/// </summary>
/// 
namespace Hotels2thailand.Booking
{
    public class BookingProductList : Hotels2BaseClass
    {
        public int BookingProductId { get; set; }
        public int ProductID { get; set; }
        public string ProductCode { get; set; }
        public string ProductTitle { get; set; }
        public byte ProductCategory { get; set; }
        public short BookingSupplier { get; set; }
        public byte LangId { get; set; }
        public Nullable<DateTime> DateTimeCheckIn { get; set; }
        public Nullable<DateTime> DateTimeCheckOut { get; set; }
        public Nullable<DateTime> DateTimeConfirmCheckIn { get; set; }
        public byte NumAdult { get; set; }
        public byte NumChild { get; set; }
        public byte NunGolf { get; set; }
        
        public bool Status { get; set; }

        public short StatusId { get; set; }
        public string StatusTitle { get; set; }

        public string FlightNumArr { get; set; }
        public string FlightNumDep { get; set; }
        public DateTime? FlightTimeArr { get; set; }
        public DateTime? FlightTimeDep { get; set; }

        public decimal TotalPriceSales { get; set; }
        public decimal TotalPriceSupplier { get; set; }

        public Nullable<DateTime> ConfirmAvailable { get; set; }
        public Nullable<DateTime> ConfirmFax { get; set; }
        public Nullable<DateTime> ConfirmCheckIn { get; set; }
        public Nullable<DateTime> ConfirmPaymentSupplier { get; set; }
        public Nullable<DateTime> ConfirmReceiveReciept { get; set; }
       // public Nullable<DateTime> ConfirmtimeCheckIn { get; set; }


        //public decimal ConfirmPayment { get; set; }
        //public Nullable<DateTime> DatePayment { get; set; }
        //public string ProductNote { get; set; }

        //public byte NumAdult { get; set; }
        //public byte NumChild { get; set; }
        //public byte NumGolfer { get; set; }
        
        //public Nullable<DateTime> DateTimeCheckInConfirm { get; set; }
        //public string Detail { get; set; }
        
        //public Nullable<DateTime> PrepaidDate { get; set; }
        //public short SupplierID { get; set; }

        public BookingProductList getTOP1ProductListShowFirstByBookingId(int intBookingID)
        {

            StringBuilder query = new StringBuilder();
            query.Append("SELECT TOP 1 bp.booking_product_id, p.product_id, p.product_code, p.title, p.cat_id, bp.supplier_id, b.lang_id, bp.date_time_check_in, bp.date_time_check_out,bp.date_time_check_in_confirm, bp.num_adult, bp.num_child, bp.num_golfer, bp.status,bp.status_id, st.title,");
            query.Append("  b.flight_arrival_number,b.flight_departure_number,b.flight_arrival_time,b.flight_departure_time, ");
            query.Append(" (SELECT SUM(bix.price) FROM tbl_booking_item bix WHERE bix.status = 1 AND bp.booking_product_id = bix.booking_product_id) As TotalPriceSale,");
            query.Append(" (SELECT SUM(bix.price_supplier) FROM tbl_booking_item bix WHERE bix.status = 1 AND bp.booking_product_id = bix.booking_product_id) As TotalPriceOwn,");
            query.Append(" (SELECT TOP 1 bpcon.date_submit FROM tbl_booking_product_confirm bpcon WHERE bp.booking_product_id = bpcon.booking_product_id AND bpcon.confirm_cat_id = 8 AND bpcon.status = 1 ORDER BY bpcon.date_submit DESC) AS Confirm_Avaliable,");
            query.Append(" (SELECT TOP 1 bpcon.date_submit FROM tbl_booking_product_confirm bpcon WHERE bp.booking_product_id = bpcon.booking_product_id AND bpcon.confirm_cat_id = 9 AND bpcon.status = 1 ORDER BY bpcon.date_submit DESC) AS Confirm_Fax,");
            query.Append(" (SELECT TOP 1 bpcon.date_submit FROM tbl_booking_product_confirm bpcon WHERE bp.booking_product_id = bpcon.booking_product_id AND bpcon.confirm_cat_id = 11 AND bpcon.status = 1 ORDER BY bpcon.date_submit DESC) AS Confirm_checkIn,");
            query.Append(" (SELECT TOP 1 bpcon.date_submit FROM tbl_booking_product_confirm bpcon WHERE bp.booking_product_id = bpcon.booking_product_id AND bpcon.confirm_cat_id = 10 AND bpcon.status = 1 ORDER BY bpcon.date_submit DESC) AS Confirm_Payment_Supplier,");
            query.Append(" (SELECT TOP 1 bpcon.date_submit FROM tbl_booking_product_confirm bpcon WHERE bp.booking_product_id = bpcon.booking_product_id AND bpcon.confirm_cat_id = 3 AND bpcon.status = 1 ORDER BY bpcon.date_submit DESC) AS Confirm_Receive_Reciept");
            //query.Append(" (SELECT TOP 1 bpcon.date_submit FROM tbl_booking_product_confirm bpcon WHERE bp.booking_product_id = bpcon.booking_product_id AND bpcon.confirm_cat_id = 13 AND bpcon.status = 1 ORDER BY bpcon.date_submit DESC) AS Confirm_time_checkin");
            query.Append(" FROM tbl_product p, tbl_booking_product bp, tbl_status st, tbl_booking b");
            query.Append(" WHERE p.product_id = bp.product_id AND st.status_id = bp.status_id AND bp.booking_id = @booking_id AND b.booking_id = bp.booking_id ORDER BY bp.status DESC");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingID;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (BookingProductList)MappingObjectFromDataReader(reader);
                else
                    return null;
               
            }
        }

        public List<object> getProductListShowFirstByBookingId(int intBookingID)
        {

            StringBuilder query = new StringBuilder();
            query.Append("SELECT bp.booking_product_id, p.product_id, p.product_code, p.title, p.cat_id, bp.supplier_id, b.lang_id, bp.date_time_check_in, bp.date_time_check_out,bp.date_time_check_in_confirm, bp.num_adult, bp.num_child, bp.num_golfer, bp.status,bp.status_id, st.title,");
            query.Append("  b.flight_arrival_number,b.flight_departure_number,b.flight_arrival_time,b.flight_departure_time, ");
            query.Append(" (SELECT SUM(bix.price) FROM tbl_booking_item bix WHERE bix.status = 1 AND bp.booking_product_id = bix.booking_product_id) As TotalPriceSale,");
            query.Append(" (SELECT SUM(bix.price_supplier) FROM tbl_booking_item bix WHERE bix.status = 1 AND bp.booking_product_id = bix.booking_product_id) As TotalPriceOwn,");
            query.Append(" (SELECT TOP 1 bpcon.date_submit FROM tbl_booking_product_confirm bpcon WHERE bp.booking_product_id = bpcon.booking_product_id AND bpcon.confirm_cat_id = 8 AND bpcon.status = 1 ORDER BY bpcon.date_submit DESC) AS Confirm_Avaliable,");
            query.Append(" (SELECT TOP 1 bpcon.date_submit FROM tbl_booking_product_confirm bpcon WHERE bp.booking_product_id = bpcon.booking_product_id AND bpcon.confirm_cat_id = 9 AND bpcon.status = 1 ORDER BY bpcon.date_submit DESC) AS Confirm_Fax,");
            query.Append(" (SELECT TOP 1 bpcon.date_submit FROM tbl_booking_product_confirm bpcon WHERE bp.booking_product_id = bpcon.booking_product_id AND bpcon.confirm_cat_id = 11 AND bpcon.status = 1 ORDER BY bpcon.date_submit DESC) AS Confirm_checkIn,");
            query.Append(" (SELECT TOP 1 bpcon.date_submit FROM tbl_booking_product_confirm bpcon WHERE bp.booking_product_id = bpcon.booking_product_id AND bpcon.confirm_cat_id = 10 AND bpcon.status = 1 ORDER BY bpcon.date_submit DESC) AS Confirm_Payment_Supplier,");
            query.Append(" (SELECT TOP 1 bpcon.date_submit FROM tbl_booking_product_confirm bpcon WHERE bp.booking_product_id = bpcon.booking_product_id AND bpcon.confirm_cat_id = 3 AND bpcon.status = 1 ORDER BY bpcon.date_submit DESC) AS Confirm_Receive_Reciept");
            //query.Append(" (SELECT TOP 1 bpcon.date_submit FROM tbl_booking_product_confirm bpcon WHERE bp.booking_product_id = bpcon.booking_product_id AND bpcon.confirm_cat_id = 13 AND bpcon.status = 1 ORDER BY bpcon.date_submit DESC) AS Confirm_time_checkin");
            query.Append(" FROM tbl_product p, tbl_booking_product bp, tbl_status st, tbl_booking b");
            query.Append(" WHERE p.product_id = bp.product_id AND st.status_id = bp.status_id AND bp.booking_id = @booking_id AND b.booking_id = bp.booking_id ORDER BY bp.status DESC");
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(),cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingID;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> getProductListShowFirstByBookingId_customerDisplay(int intBookingID)
        {

            StringBuilder query = new StringBuilder();
            query.Append("SELECT bp.booking_product_id, p.product_id, p.product_code, pc.title, p.cat_id,bp.supplier_id, b.lang_id, bp.date_time_check_in, bp.date_time_check_out,bp.date_time_check_in_confirm, bp.num_adult, bp.num_child, bp.num_golfer, bp.status,bp.status_id, st.title,");
            query.Append("  b.flight_arrival_number,b.flight_departure_number,b.flight_arrival_time,b.flight_departure_time, ");
            query.Append(" (SELECT SUM(bix.price) FROM tbl_booking_item bix WHERE bix.status = 1 AND bp.booking_product_id = bix.booking_product_id) As TotalPriceSale,");
            query.Append(" (SELECT SUM(bix.price_supplier) FROM tbl_booking_item bix WHERE bix.status = 1 AND bp.booking_product_id = bix.booking_product_id) As TotalPriceOwn,");
            query.Append(" (SELECT TOP 1 bpcon.date_submit FROM tbl_booking_product_confirm bpcon WHERE bp.booking_product_id = bpcon.booking_product_id AND bpcon.confirm_cat_id = 8 AND bpcon.status = 1 ORDER BY bpcon.date_submit DESC) AS Confirm_Avaliable,");
            query.Append(" (SELECT TOP 1 bpcon.date_submit FROM tbl_booking_product_confirm bpcon WHERE bp.booking_product_id = bpcon.booking_product_id AND bpcon.confirm_cat_id = 9 AND bpcon.status = 1 ORDER BY bpcon.date_submit DESC) AS Confirm_Fax,");
            query.Append(" (SELECT TOP 1 bpcon.date_submit FROM tbl_booking_product_confirm bpcon WHERE bp.booking_product_id = bpcon.booking_product_id AND bpcon.confirm_cat_id = 11 AND bpcon.status = 1 ORDER BY bpcon.date_submit DESC) AS Confirm_checkIn,");
            query.Append(" (SELECT TOP 1 bpcon.date_submit FROM tbl_booking_product_confirm bpcon WHERE bp.booking_product_id = bpcon.booking_product_id AND bpcon.confirm_cat_id = 10 AND bpcon.status = 1 ORDER BY bpcon.date_submit DESC) AS Confirm_Payment_Supplier,");
            query.Append(" (SELECT TOP 1 bpcon.date_submit FROM tbl_booking_product_confirm bpcon WHERE bp.booking_product_id = bpcon.booking_product_id AND bpcon.confirm_cat_id = 3 AND bpcon.status = 1 ORDER BY bpcon.date_submit DESC) AS Confirm_Receive_Reciept");
            //query.Append(" (SELECT TOP 1 bpcon.date_submit FROM tbl_booking_product_confirm bpcon WHERE bp.booking_product_id = bpcon.booking_product_id AND bpcon.confirm_cat_id = 13 AND bpcon.status = 1 ORDER BY bpcon.date_submit DESC) AS Confirm_time_checkin");
            query.Append(" FROM tbl_product p, tbl_booking_product bp, tbl_status st, tbl_booking b, tbl_product_content pc");
            query.Append(" WHERE p.product_id = bp.product_id AND st.status_id = bp.status_id AND bp.booking_id = @booking_id AND b.booking_id = bp.booking_id AND pc.product_id = p.product_id AND pc.lang_id = b.lang_id");
            query.Append(" AND bp.status = 1");
            query.Append(" ORDER BY bp.status DESC");
            query.Append("");
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingID;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public BookingProductList getTop1ProductListShowFirstByBookingId_customerDisplay(int intBookingID)
        {

            StringBuilder query = new StringBuilder();
            query.Append("SELECT TOP 1 bp.booking_product_id, p.product_id, p.product_code, pc.title, p.cat_id,bp.supplier_id, b.lang_id, bp.date_time_check_in, bp.date_time_check_out,bp.date_time_check_in_confirm, bp.num_adult, bp.num_child, bp.num_golfer, bp.status,bp.status_id, st.title,");
            query.Append("  b.flight_arrival_number,b.flight_departure_number,b.flight_arrival_time,b.flight_departure_time, ");
            query.Append(" (SELECT SUM(bix.price) FROM tbl_booking_item bix WHERE bix.status = 1 AND bp.booking_product_id = bix.booking_product_id) As TotalPriceSale,");
            query.Append(" (SELECT SUM(bix.price_supplier) FROM tbl_booking_item bix WHERE bix.status = 1 AND bp.booking_product_id = bix.booking_product_id) As TotalPriceOwn,");
            query.Append(" (SELECT TOP 1 bpcon.date_submit FROM tbl_booking_product_confirm bpcon WHERE bp.booking_product_id = bpcon.booking_product_id AND bpcon.confirm_cat_id = 8 AND bpcon.status = 1 ORDER BY bpcon.date_submit DESC) AS Confirm_Avaliable,");
            query.Append(" (SELECT TOP 1 bpcon.date_submit FROM tbl_booking_product_confirm bpcon WHERE bp.booking_product_id = bpcon.booking_product_id AND bpcon.confirm_cat_id = 9 AND bpcon.status = 1 ORDER BY bpcon.date_submit DESC) AS Confirm_Fax,");
            query.Append(" (SELECT TOP 1 bpcon.date_submit FROM tbl_booking_product_confirm bpcon WHERE bp.booking_product_id = bpcon.booking_product_id AND bpcon.confirm_cat_id = 11 AND bpcon.status = 1 ORDER BY bpcon.date_submit DESC) AS Confirm_checkIn,");
            query.Append(" (SELECT TOP 1 bpcon.date_submit FROM tbl_booking_product_confirm bpcon WHERE bp.booking_product_id = bpcon.booking_product_id AND bpcon.confirm_cat_id = 10 AND bpcon.status = 1 ORDER BY bpcon.date_submit DESC) AS Confirm_Payment_Supplier,");
            query.Append(" (SELECT TOP 1 bpcon.date_submit FROM tbl_booking_product_confirm bpcon WHERE bp.booking_product_id = bpcon.booking_product_id AND bpcon.confirm_cat_id = 3 AND bpcon.status = 1 ORDER BY bpcon.date_submit DESC) AS Confirm_Receive_Reciept");
            //query.Append(" (SELECT TOP 1 bpcon.date_submit FROM tbl_booking_product_confirm bpcon WHERE bp.booking_product_id = bpcon.booking_product_id AND bpcon.confirm_cat_id = 13 AND bpcon.status = 1 ORDER BY bpcon.date_submit DESC) AS Confirm_time_checkin");
            query.Append(" FROM tbl_product p, tbl_booking_product bp, tbl_status st, tbl_booking b, tbl_product_content pc");
            query.Append(" WHERE p.product_id = bp.product_id AND st.status_id = bp.status_id AND bp.booking_id = @booking_id AND b.booking_id = bp.booking_id AND pc.product_id = p.product_id AND pc.lang_id = b.lang_id");
            query.Append(" AND bp.status = 1");
            query.Append(" ORDER BY bp.status DESC");
            query.Append("");
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingID;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                if (reader.Read())
                    return (BookingProductList)MappingObjectFromDataReader(reader);
                else
                    return null;
            }
        }

        public BookingProductList getProductByBookingProductID(int intBookingProductId)
        {

            StringBuilder query = new StringBuilder();
            query.Append("SELECT bp.booking_product_id, p.product_id, p.product_code, p.title, p.cat_id, bp.supplier_id, b.lang_id, bp.date_time_check_in, bp.date_time_check_out, bp.date_time_check_in_confirm,bp.num_adult, bp.num_child, bp.num_golfer, bp.status,bp.status_id, st.title,");
            query.Append("  b.flight_arrival_number,b.flight_departure_number,b.flight_arrival_time,b.flight_departure_time, ");
            query.Append(" (SELECT SUM(bix.price) FROM tbl_booking_item bix WHERE bix.status = 1 AND bp.booking_product_id = bix.booking_product_id) As TotalPriceSale,");
            query.Append(" (SELECT SUM(bix.price_supplier) FROM tbl_booking_item bix WHERE bix.status = 1 AND bp.booking_product_id = bix.booking_product_id) As TotalPriceOwn,");
            query.Append(" (SELECT TOP 1 bpcon.date_submit FROM tbl_booking_product_confirm bpcon WHERE bp.booking_product_id = bpcon.booking_product_id AND bpcon.confirm_cat_id = 8 AND bpcon.status = 1 ORDER BY bpcon.date_submit DESC) AS Confirm_Avaliable,");
            query.Append(" (SELECT TOP 1 bpcon.date_submit FROM tbl_booking_product_confirm bpcon WHERE bp.booking_product_id = bpcon.booking_product_id AND bpcon.confirm_cat_id = 9 AND bpcon.status = 1 ORDER BY bpcon.date_submit DESC) AS Confirm_Fax,");
            query.Append(" (SELECT TOP 1 bpcon.date_submit FROM tbl_booking_product_confirm bpcon WHERE bp.booking_product_id = bpcon.booking_product_id AND bpcon.confirm_cat_id = 11 AND bpcon.status = 1 ORDER BY bpcon.date_submit DESC) AS Confirm_checkIn,");
            query.Append(" (SELECT TOP 1 bpcon.date_submit FROM tbl_booking_product_confirm bpcon WHERE bp.booking_product_id = bpcon.booking_product_id AND bpcon.confirm_cat_id = 10 AND bpcon.status = 1 ORDER BY bpcon.date_submit DESC) AS Confirm_Payment_Supplier,");
            query.Append(" (SELECT TOP 1 bpcon.date_submit FROM tbl_booking_product_confirm bpcon WHERE bp.booking_product_id = bpcon.booking_product_id AND bpcon.confirm_cat_id = 3 AND bpcon.status = 1 ORDER BY bpcon.date_submit DESC) AS Confirm_Receive_Reciept");
            //query.Append(" (SELECT TOP 1 bpcon.date_submit FROM tbl_booking_product_confirm bpcon WHERE bp.booking_product_id = bpcon.booking_product_id AND bpcon.confirm_cat_id = 13 AND bpcon.status = 1 ORDER BY bpcon.date_submit DESC) AS Confirm_time_checkin");
            query.Append(" FROM tbl_product p, tbl_booking_product bp, tbl_status st, tbl_booking b");
            query.Append(" WHERE p.product_id = bp.product_id AND st.status_id = bp.status_id AND bp.booking_product_id = @booking_product_id AND b.booking_id = bp.booking_id ORDER BY bp.status DESC");
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@booking_product_id", SqlDbType.Int).Value = intBookingProductId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (BookingProductList)MappingObjectFromDataReader(reader);
                else
                    return null;
                
            }
        }
        
    }
}