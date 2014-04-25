using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Hotels2thailand;
using Hotels2thailand.Staffs;


/// <summary>
/// Summary description for ProductCategory
/// </summary>
/// 
namespace Hotels2thailand.Report
{
    public class BookingDashBoardExtra : Hotels2BaseClass
    {

        public int KeyVal { get; set; }
        public DateTime dDate { get; set; }
        public decimal Num { get; set; }

        public string GenPageId(int intProductId)
        {
            string result = string.Empty;
            string key = "9";
            string Lang = "01";
            string[] Cat = { "016", "017", "018", "019", "020", "021", "022", "023" };
            string Id = "0000000";
            for (int  cats = 0 ; cats <Cat.Count() ; cats ++)
            {
                result = result + key + Lang + Cat[cats] + Id.Hotels2RightCrl(intProductId.ToString().Length) + intProductId + ",";
            }
            return result;
        }


        public IList<object> getCountImpression(int intProductID, DateTime dDate, DateInterval DateInterVal )
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT tp.period_id, tp.date_start");
            query.Append(" ,CEILING(ISNULl((SELECT SUM(tcop.num_count) FROM tbl_track_count_page tcop WHERE tcop.period_id = tp.period_id AND tcop.page_id IN ( " + GenPageId(intProductID).Hotels2RightCrl(1));

                query.Append(" )),0) * @multiple_factor)");
                query.Append(" FROM tbl_track_period tp");

                if (DateInterVal == DateInterval.Day)
                query.Append(" WHERE MONTH(tp.date_start) =@date_start AND YEAR(tp.date_start) =@date_start_year  ORDER BY tp.date_start");
                if (DateInterVal == DateInterval.Month)
                query.Append(" WHERE YEAR(tp.date_start) =@date_start_year  ORDER BY tp.date_start");
                if (DateInterVal == DateInterval.Year)
                query.Append(" ORDER BY tp.date_start");

                using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                    cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductID;
                   
                    cmd.Parameters.Add("@date_start", SqlDbType.Int).Value = dDate.Month;
                    cmd.Parameters.Add("@date_start_year", SqlDbType.Int).Value = dDate.Year;

                    cmd.Parameters.Add("@multiple_factor", SqlDbType.Decimal).Value = 1;
                    cn.Open();
                    return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
                }
         }

        public IList<object> getAllBookingByProductIDANdSupplier(int intProductID, short shrSupplierId, DateTime dDate, DateInterval DateInterVal)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT DISTINCT(b.booking_id), b.date_submit ");
            query.Append(" FROM tbl_booking b,tbl_booking_product bp");
            query.Append(" WHERE bp.booking_id = b.booking_id AND bp.product_id = @product_id ");
            query.Append(" AND bp.supplier_id = @supplier_id AND b.name_full  NOT LIKE '%test%' AND b.email NOT LIKE '%test%'");

            if (DateInterVal == DateInterval.Day)
                query.Append(" AND MONTH(b.date_submit) =@date_submit AND YEAR(b.date_submit) =@date_submit_year ORDER BY b.date_submit");
            if (DateInterVal == DateInterval.Month)
                query.Append("  AND YEAR(b.date_submit) =@date_submit_year ORDER BY b.date_submit");
            if (DateInterVal == DateInterval.Year)
                query.Append("  ORDER BY b.date_submit");
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductID;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@date_submit", SqlDbType.Int).Value = dDate.Month;
                cmd.Parameters.Add("@date_submit_year", SqlDbType.Int).Value = dDate.Year;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }




        public IList<object> getCompletedBookingByProductIDANdSupplier(int intProductID, short shrSupplierId, DateTime dDate, DateInterval DateInterVal)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT DISTINCT(b.booking_id), b.date_submit ");
            query.Append(" FROM tbl_booking b,tbl_booking_product bp");
            query.Append(" WHERE bp.booking_id = b.booking_id AND bp.product_id = @product_id ");
            query.Append(" AND bp.supplier_id = @supplier_id AND b.name_full  NOT LIKE '%test%' AND b.email NOT LIKE '%test%'");
            query.Append(" AND (SELECT COUNT(*) FROM tbl_booking_confirm bf WHERE b.booking_id = bf.booking_id AND bf.confirm_cat_id = 2) > 0");
            if (DateInterVal == DateInterval.Day)
                query.Append(" AND MONTH(b.date_submit) =@date_submit AND YEAR(b.date_submit) =@date_submit_year ORDER BY b.date_submit");
            if (DateInterVal == DateInterval.Month)
                query.Append("  AND YEAR(b.date_submit) =@date_submit_year ORDER BY b.date_submit");
            if (DateInterVal == DateInterval.Year)
                query.Append("  ORDER BY b.date_submit");
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductID;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@date_submit", SqlDbType.Int).Value = dDate.Month;
                cmd.Parameters.Add("@date_submit_year", SqlDbType.Int).Value = dDate.Year;
                cn.Open();
                
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
               
            }
        }
       
    }
}