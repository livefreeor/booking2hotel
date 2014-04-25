using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for FrontBookingHotel
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class FrontBookingHotel:Hotels2BaseClass
    {
        public int BookingID { get; set; }
        public int ProductID { get; set; }
        public int BookingHotelID { get; set; }

        public FrontBookingHotel()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void Insert(FrontBookingHotel data)
        {
            using(SqlConnection cn=new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("insert into tbl_booking_hotels (booking_id,product_id,booking_hotel_id) values(@booking_id,@product_id,@booking_hotel_id)", cn);
                cmd.Parameters.Add("@booking_id", SqlDbType.Int).Value = data.BookingID;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = data.ProductID;
                cmd.Parameters.Add("@booking_hotel_id", SqlDbType.Int).Value = data.BookingHotelID;
                cn.Open();
                ExecuteNonQuery(cmd);
            }
        }
    }
}
