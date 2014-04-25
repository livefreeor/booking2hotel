using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.Booking;

/// <summary>
/// Summary description for Currency
/// </summary>
/// 
namespace Hotels2thailand.Booking
{
    public class BookingGuest_Engine : Hotels2BaseClass
    {


        public int GuestId { get; set; }
        public byte CatId { get; set; }
        public int BookingProductId { get; set; }
        public int? BookingItemId { get; set; }
        public string GuestName { get; set; }
        public DateTime? DateBirth { get; set; }
        public string Comment { get; set; }
        public string PassportId { get; set; }

        public BookingGuest_Engine getGuestByBookingItem(int intBookingProductId, int intBookingItemId)
        {
            using(SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT guest_id,cat_id,booking_product_id,booking_item_id,guest_name,date_birth,comment,passport_id FROM tbl_booking_product_guest WHERE booking_product_id = @booking_product_id AND booking_item_id =@booking_item_id",cn);
                cmd.Parameters.Add("@booking_product_id",SqlDbType.Int).Value = intBookingProductId;
                cmd.Parameters.Add("@booking_item_id",SqlDbType.Int).Value = intBookingItemId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if(reader.Read())
                    return (BookingGuest_Engine)MappingObjectFromDataReader(reader);
                else
                    return null;
               
            }
        }

        

    }
}