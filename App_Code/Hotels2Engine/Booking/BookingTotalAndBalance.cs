using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Hotels2thailand.Booking;
using Hotels2thailand.Staffs;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for BookingConfirm
/// </summary>
/// 
namespace Hotels2thailand.Booking
{
    public class BookingTotalAndBalance : Hotels2BaseClass
    {
        public decimal SumPrice { get; set; }
        public decimal SumPriceDisplay { get; set; }
        public decimal SumPriceSupplier { get; set; }

        public BookingTotalAndBalance CalcullatePriceTotalByBookingId(int bookingID)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT SUM(bi.price), SUM(bi.price_display), SUM(bi.price_supplier)");
            query.Append(" FROM tbl_booking b, tbl_booking_item bi, tbl_booking_product bp");
            query.Append(" WHERE b.booking_id = bp.booking_id AND bp.booking_product_id = bi.booking_product_id AND bp.status = 1 AND bi.status = 1 AND b.booking_id = @booking_id");
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = bookingID;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);

                if (reader.Read())
                    return (BookingTotalAndBalance)MappingObjectFromDataReader(reader);
                else
                    return null;
            }
        }

        public decimal getbalanceByBookingId(int bookingID)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT ");
            query.Append(" ISNULL((SELECT SUM(amount) FROM tbl_booking_payment bpm WHERE bpm.confirm_payment IS NOT NULL AND bpm.booking_id = b.booking_id AND bpm.status = 1),0)-");
            query.Append(" ISNULL((SELECT SUM(bi.price) FROM tbl_booking_item bi , tbl_booking_product bp WHERE bp.booking_product_id = bi.booking_product_id AND bp.status = 1 AND b.booking_id = bi.booking_id AND bi.status = 1),0)");
            query.Append(" FROM tbl_booking b WHERE b.booking_id = @booking_id");
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = bookingID;
                cn.Open();
                return (decimal)ExecuteScalar(cmd);
            }
        }

        public decimal GetPriceTotalPaidByBookingId(int intBookingID)
        {

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT ISNULL(SUM(amount),0) FROM tbl_booking_payment WHERE confirm_payment IS NOT NULL AND booking_id = @booking_id", cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = intBookingID;
                cn.Open();
                decimal Total = (decimal)ExecuteScalar(cmd);

                return Total;

            }
        }
        
    }
}