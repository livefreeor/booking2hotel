using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Booking;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Booking
/// </summary>
/// 
namespace Hotels2thailand.Front
{
/// <summary>
/// Summary description for BookingRoomRequirement
/// </summary>
    public class BookingRoomRequirement : Hotels2BaseClass 
{

        public int RequirementID { get; set; }
        public int BookingItemID { get; set; }
        public byte TypeBed { get; set; }
        public byte TypeSmoke { get; set; }
        public byte TypeFloor { get; set; }
        public string Comment { get; set; }

        public BookingRoomRequirement(int bookingItemID, byte typeBed, byte typeSmoke, byte typeFloor, string comment)
        {
            BookingItemID = bookingItemID;
            TypeBed = typeBed;
            TypeSmoke = typeSmoke;
            TypeFloor = typeFloor;
            Comment = comment;

        }
        public int Insert(BookingRoomRequirement data)
        {
            
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("insert into tbl_booking_item_require_hotel (booking_item_id,comment,type_smoking,type_bed,type_floor) values(@booking_item_id,@comment,@type_smoking,@type_bed,@type_floor);SET @require_id=SCOPE_IDENTITY()", cn);


                cmd.Parameters.Add("@booking_item_id", SqlDbType.Int).Value = data.BookingItemID;
                cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = data.Comment;
                cmd.Parameters.Add("@type_bed", SqlDbType.TinyInt).Value = data.TypeBed;
                cmd.Parameters.Add("@type_smoking", SqlDbType.TinyInt).Value = data.TypeSmoke;
                cmd.Parameters.Add("@type_floor", SqlDbType.TinyInt).Value = data.TypeFloor;
                cmd.Parameters.Add("@require_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                ExecuteNonQuery(cmd);
                ret = (int)cmd.Parameters["@require_id"].Value;
            }

            return ret;
        }
}
}