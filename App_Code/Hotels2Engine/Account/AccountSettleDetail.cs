using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.Staffs;
using Hotels2thailand;

/// <summary>
/// Summary description for AccountSettleReport
/// </summary>
/// 
namespace Hotels2thailand.Account
{
    public class AccountSettleDetail : Hotels2BaseClass
    {
        
        public int BookingPaymentID { get; set; }
        public int BookingId { get; set; }
        public string BookingName { get; set; }
        public string Producttitle { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public decimal Amount { get; set; }
        public decimal Cost { get; set; }
        public decimal PriceTotal { get; set; }
        public decimal PaidTotal { get; set; }
        
        public AccountSettleDetail()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public List<object> GetListPaymentListSettled(string strPaymentId)
        {
            StringBuilder query = new StringBuilder();
            
            query.Append("SELECT bm.booking_payment_id,bm.booking_id, b.name_full,");
            query.Append(" (");
            query.Append(" SELECT TOP 1 p.title  FROM tbl_booking_product bp, tbl_product p ");
            query.Append(" WHERE bp.booking_id = b.booking_id AND bp.status = 1 AND bp.product_id = p.product_id AND p.cat_id <> 31");
            query.Append(" ) AS productName");
            query.Append(" ,(");
            query.Append(" SELECT TOP 1 bp.date_time_check_in FROM tbl_booking_product bp, tbl_product p");
            query.Append(" WHERE bp.booking_id = b.booking_id AND bp.status = 1 AND bp.product_id = p.product_id AND p.cat_id <> 31");
            query.Append(" ) AS DateCheckIn");
            query.Append(" ,(");
            query.Append(" SELECT TOP 1 bp.date_time_check_out FROM tbl_booking_product bp, tbl_product p ");
            query.Append(" WHERE bp.booking_id = b.booking_id AND bp.status = 1 AND bp.product_id = p.product_id AND p.cat_id <> 31");
            query.Append(" ) AS DateCheckIn");
            query.Append(", bm.amount ,");

            query.Append(" (SELECT SUM(bi.price_supplier) FROM tbl_booking_item bi WHERE bi.booking_id = b.booking_id ) AS Cost,");
            query.Append(" (SELECT SUM(bi.price) FROM tbl_booking_item bi WHERE bi.booking_id = b.booking_id ) AS Price_Total,");
            query.Append(" (SELECT SUM(pme.amount) FROM tbl_booking_payment pme WHERE pme.booking_id = b.booking_id ) AS Total_paid");
            query.Append(" FROM tbl_booking_payment  bm, tbl_booking b");
            query.Append(" WHERE bm.booking_id = b.booking_id AND bm.booking_payment_id IN (" + strPaymentId + ")");
            query.Append(" ORDER BY b.booking_id ");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(),cn);
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }

        }





    }
}