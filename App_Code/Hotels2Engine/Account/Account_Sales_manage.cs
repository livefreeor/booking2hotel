using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;
/// <summary>
/// Summary description for Account_Sales_manage
/// </summary>
/// 
namespace Hotels2thailand.Account
{
    public class Account_Sales_manage : Hotels2BaseClass
    {

        public int BookingId { get; set; }
        public int BookingHotelID { get; set; }
        public string HotelTitle { get; set; }
        public string CusName  { get; set; }
        public DateTime  Checkin { get; set; }
        public DateTime  CheckOut { get; set; }
        public DateTime  DateBooking { get; set; }
        public decimal CommissionVal { get; set; }
        public string Price { get; set; }
    
       




        public Account_Sales_manage()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //for berkeley Hotel Only
        public IList<object> getCom(DateTime dDateCheckOutStart, DateTime dDateCheckOutEnd)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder result = new StringBuilder();

                result.Append("SELECT b.booking_id, bh.booking_hotel_id, p.title , cs.full_name, bp.date_time_check_in, bp.date_time_check_out , b.date_submit, bp.compercentandVal");
                result.Append(" ,(SELECT CAST( SUM(bi.price) AS VARCHAR) + '&' + CAST( SUM(bi.price_supplier) AS VARCHAR) + '&' + CAST( SUM(bi.unit) AS VARCHAR) FROM tbl_booking_item bi WHERe b.booking_id= bi.booking_id AND bi.status = 1)");
                result.Append(" FROM tbl_booking b , tbl_booking_product bp , tbl_booking_hotels bh, tbl_product p , tbl_customer cs");
                result.Append(" WHERE b.booking_id = bp.booking_id  AND p.product_id = bp.product_id AND b.cus_id= cs.cus_id ");
                result.Append(" AND bp.product_id = 3605 AND bp.date_time_check_out  BETWEEN @dateStart AND @dateEnd");
                result.Append(" AND b.status_id = 85 AND bp.status_id =15  AND bp.status= 1  AND bp.product_id = bh.product_id AND bh.booking_id = bp.booking_id ORDER BY b.date_submit ");

                SqlCommand cmd = new SqlCommand(result.ToString(), cn);
                cmd.Parameters.Add("@dateStart", SqlDbType.SmallDateTime).Value = dDateCheckOutStart;
                cmd.Parameters.Add("@dateEnd", SqlDbType.SmallDateTime).Value = dDateCheckOutEnd;

                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
               
               
                
            }

        }
        
    }
}
