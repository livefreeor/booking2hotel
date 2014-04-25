using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Hotels2thailand.DataAccess;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for BookingPaymentList
/// </summary>
/// 
namespace Hotels2thailand.Booking
{
    public class BookingPaymentCat : Hotels2BaseClass
    {
        public Dictionary<byte, string> GetPaymentCatList()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                Dictionary<byte, string> dicGateWay = new Dictionary<byte, string>();
                SqlCommand cmd = new SqlCommand("SELECT booking_payment_cat_id,title FROM tbl_booking_payment_category", cn);
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    dicGateWay.Add((byte)reader[0], reader[1].ToString());
                }
                return dicGateWay;
            }
        }
        
    }
}