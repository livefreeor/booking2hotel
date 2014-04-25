using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Hotels2thailand.Booking;
using Hotels2thailand.ProductOption;

/// <summary>
/// Summary description for BookingItem
/// </summary>
/// 

namespace Hotels2thailand.Booking
{
    public class Bookinghistory : Hotels2BaseClass
    {
        public int BookingId { get; set; }
        public int BookingProductId { get; set; }
        public string Productitle { get; set; }
        public DateTime DateSubmit { get; set; }
        public DateTime DateCheckIn { get; set; }
        public DateTime DateCheckOut { get; set; }
        public DateTime? DateTimeCheckInConfirm { get; set; }
        public string StatusTitle { get; set; }
        public bool Status { get; set; }


        public List<object> GetOrderHistoryByEmail(string strEmail, int intBookingId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString_old))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT ro.order_id , orp.order_product_id, p.title_en, ro.date_submit, orp.date_time_check_in, orp.date_time_check_out, orp.date_time_check_in_confirm, rs.title , ro.status");
                query.Append(" FROM tbl_order_product orp, tbl_order ro, tbl_order_status rs, tbl_product p");
                query.Append(" WHERE orp.order_id = ro.order_id AND rs.status_id = ro.status_id AND p.product_id = orp.product_id");
                query.Append(" AND ro.email = @email AND ro.order_id <> @order_id");
                query.Append(" ORDER BY ro.date_submit DESC");

                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = strEmail;
                cmd.Parameters.Add("@order_id", SqlDbType.Int).Value = intBookingId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }

         }

        public string GetEmailByOrderID(int intOrderID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString_old))
            {

                SqlCommand cmd = new SqlCommand("SELECT email FROM tbl_order WHERE order_id=@order_id", cn);
                cmd.Parameters.Add("@order_id", SqlDbType.Int).Value = intOrderID;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                string strBookingId = string.Empty;
                if (reader.Read())
                {
                    strBookingId = reader[0].ToString();
                }
                return strBookingId;
            }
        }

    }
}