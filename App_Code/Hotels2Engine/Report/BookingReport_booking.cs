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
    public class BookingReport_booking : Hotels2BaseClass
    {

        public int BookingId { get; set; }
        public DateTime DateSubmit { get; set; }
        public int ConditionId { get; set; }
        public string ConditionTitle { get; set; }

        public DateTime DateCheckIn { get; set; }
        public DateTime DateCheckOut { get; set; }
        public int TotalNightStay { get; set; }
        public int TotalUnit { get; set; }
        public int TotalPeriodNightStay_Real { get; set; }

        public IList<object> getBookingCompleted(int intProductId, short shrSupplierId, DateTime dDateStart, DateTime dDateEnd)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT DISTINCT(b.booking_id), b.date_submit, bi.condition_id, bi.condition_title");
            query.Append(" FROM tbl_booking b,tbl_booking_product bp, tbl_booking_item bi,tbl_product p, tbl_product_option op");
            query.Append(" WHERE bp.booking_id = b.booking_id AND bp.product_id = @product_id  AND bi.booking_id= b.booking_id AND p.product_id = bp.product_id");
            query.Append(" AND bi.option_id = op.option_id AND op.cat_id = 38 AND b.status = 0 AND bp.status= 1 AND bi.status = 1");
            query.Append(" AND bp.supplier_id = @supplier_id AND b.name_full  NOT LIKE '%test%' AND b.email NOT LIKE '%test%'");
            query.Append(" AND (SELECT COUNT(*) FROM tbl_booking_confirm bf WHERE b.booking_id = bf.booking_id AND bf.confirm_cat_id = 2) > 0");
            query.Append(" AND b.date_submit BETWEEN @Date_start AND @Date_end ");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@Date_start", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@Date_end", SqlDbType.SmallDateTime).Value = dDateEnd;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public int CountBookingCompleted(int intProductId, short shrSupplierId, DateTime dDateStart, DateTime dDateEnd)
        {
            StringBuilder query = new StringBuilder();
              query.Append("SELECT COUNT(DISTINCT(b.booking_id) )");
              query.Append(" FROM tbl_booking b,tbl_booking_product bp");
              query.Append(" WHERE bp.booking_id = b.booking_id AND bp.product_id = @product_id AND b.status = 0 AND bp.status= 1");
              query.Append(" AND bp.supplier_id = @supplier_id AND b.name_full  NOT LIKE '%test%' AND b.email NOT LIKE '%test%'");
              query.Append(" AND (SELECT COUNT(*) FROM tbl_booking_confirm bf WHERE b.booking_id = bf.booking_id AND bf.confirm_cat_id = 2) > 0");

              query.Append(" AND b.date_submit BETWEEN @Date_start AND @Date_end ");

              using (SqlConnection cn = new SqlConnection(this.ConnectionString))
              {
                  SqlCommand cmd = new SqlCommand(query.ToString(),cn);
                  cmd.Parameters.Add("@product_id",SqlDbType.Int).Value = intProductId;
                  cmd.Parameters.Add("@supplier_id",SqlDbType.SmallInt).Value = shrSupplierId;
                  cmd.Parameters.Add("@Date_start", SqlDbType.SmallDateTime).Value = dDateStart;
                  cmd.Parameters.Add("@Date_end", SqlDbType.SmallDateTime).Value = dDateEnd;
                  
                  cn.Open();

                  return (int)ExecuteScalar(cmd);
              }
        }

        public IList<object> getBookingAll(int intProductId, short shrSupplierId, DateTime dDateStart, DateTime dDateEnd)
        {
            StringBuilder query = new StringBuilder();

            query.Append("SELECT DISTINCT(b.booking_id), b.date_submit, bi.condition_id, bi.condition_title");
            query.Append(" FROM tbl_booking b,tbl_booking_product bp, tbl_booking_item bi,tbl_product p, tbl_product_option op");
            query.Append(" WHERE bp.booking_id = b.booking_id AND bp.product_id = @product_id AND bi.booking_id= b.booking_id AND p.product_id = bp.product_id");
            query.Append(" AND bi.option_id = op.option_id AND op.cat_id = 38");
            query.Append(" AND bp.supplier_id = @supplier_id AND b.name_full  NOT LIKE '%test%' AND b.email NOT LIKE '%test%'");
            query.Append(" AND b.date_submit BETWEEN @Date_start AND @Date_end ");
            query.Append(" ORDER BY b.date_submit");


            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@Date_start", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@Date_end", SqlDbType.SmallDateTime).Value = dDateEnd;
                cn.Open();

                return  MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public int CountBookingAll(int intProductId, short shrSupplierId, DateTime dDateStart, DateTime dDateEnd)
        {
            StringBuilder query = new StringBuilder();

             query.Append("SELECT COUNT(DISTINCT(b.booking_id))");
             query.Append(" FROM tbl_booking b,tbl_booking_product bp");
             query.Append(" WHERE bp.booking_id = b.booking_id AND bp.product_id = @product_id ");
             query.Append(" AND bp.supplier_id = @supplier_id AND b.name_full  NOT LIKE '%test%' AND b.email NOT LIKE '%test%'");

             query.Append(" AND b.date_submit BETWEEN @Date_start AND @Date_end ");
             //query.Append(" ORDER BY b.date_submit");
           

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@Date_start", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@Date_end", SqlDbType.SmallDateTime).Value = dDateEnd;
                
                cn.Open();

                return (int)ExecuteScalar(cmd);
            }
        }

        public IList<object> GetRoomNight_BookingDate(int intProductId, short shrSupplierId, DateTime dDateStart, DateTime dDateEnd)
        {
            StringBuilder query = new StringBuilder();

            query.Append("SELECT  b.booking_id, b.date_submit , bi.condition_id, bi.condition_title ,bp.date_time_check_in, bp.date_time_check_out,");
            query.Append(" bi.unit,(DATEDIFF( DAY, date_time_check_in,date_time_check_out)) As num_room_night,");
            query.Append(" ( bi.unit * (DATEDIFF( DAY, date_time_check_in,date_time_check_out))) As num_room_night_final");
            query.Append(" FROM tbl_booking_item bi , tbl_booking b , tbl_booking_product bp, tbl_product_option op WHERE");

            query.Append(" b.booking_id = bi.booking_id AND op.option_id = bi.option_id AND op.cat_id = 38 AND b.status = 0 AND bp.status= 1 AND bi.status = 1");
            query.Append(" AND b.name_full  NOT LIKE '%test%' AND b.email NOT LIKE '%test%'");
            query.Append(" AND (SELECT COUNT(*) FROM tbl_booking_confirm bf WHERE b.booking_id = bf.booking_id AND bf.confirm_cat_id = 2) > 0");
            query.Append(" AND bp.booking_id = b.booking_id AND bp.booking_product_id = bi.booking_product_id");
            query.Append(" AND bp.product_id = @product_id AND bp.supplier_id = @supplier_id");
            
            query.Append(" AND b.date_submit BETWEEN @Date_start AND @Date_end ");
            query.Append(" ORDER BY b.date_submit ");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@Date_start", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@Date_end", SqlDbType.SmallDateTime).Value = dDateEnd;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }


        public int CountRoomNight_BookingDate(int intProductId, short shrSupplierId, DateTime dDateStart, DateTime dDateEnd)
        {
            StringBuilder query = new StringBuilder();

            query.Append("SELECT ISNULL( SUM(num_room_night_final),0) ");
            query.Append(" FROM ");
            query.Append(" (");

            query.Append(" SELECT ( bi.unit * (DATEDIFF( DAY, date_time_check_in,date_time_check_out))) As num_room_night_final");

            query.Append(" FROM tbl_booking_item bi , tbl_booking b , tbl_booking_product bp, tbl_product_option op WHERE b.status = 0 AND bp.status= 1 AND ");
            query.Append(" b.booking_id = bi.booking_id AND op.option_id = bi.option_id AND op.cat_id = 38 AND bi.status = 1");
            query.Append(" AND b.name_full  NOT LIKE '%test%' AND b.email NOT LIKE '%test%'");
            query.Append(" AND (SELECT COUNT(*) FROM tbl_booking_confirm bf WHERE b.booking_id = bf.booking_id AND bf.confirm_cat_id = 2) > 0");


            query.Append(" AND bp.booking_id = b.booking_id AND bp.booking_product_id = bi.booking_product_id");
           
            query.Append(" AND bp.product_id = @product_id AND bp.supplier_id = @supplier_id");
            
           
            query.Append(" AND b.date_submit BETWEEN @Date_start AND @Date_end ");
            query.Append(" ) AS temp");


            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@Date_start", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@Date_end", SqlDbType.SmallDateTime).Value = dDateEnd;
                
                cn.Open();

                return (int)ExecuteScalar(cmd);
            }
        }


        public IList<object> GEtRoomNight_CheckInDateByPeriod(int intProductId, short shrSupplierId, DateTime dDateStart, DateTime dDateEnd)
        {
            StringBuilder query = new StringBuilder();

            query.Append("SELECT b.booking_id , b.date_submit, bi.condition_id, bi.condition_title, bp.date_time_check_in , bp.date_time_check_out , ");
            query.Append(" DATEDIFF( DAY, bp.date_time_check_in,bp.date_time_check_out) As TotalNightStay ,");
            query.Append(" SUM(bi.unit) AS Totalunit ");
            query.Append(" FROM tbl_booking b, tbl_booking_product bp, tbl_booking_item bi ,tbl_product_option op");
            query.Append(" WHERE bp.booking_id=b.booking_id AND b.status = 0 AND bp.status= 1");
            query.Append(" AND bp.product_id = @product_id AND bp.supplier_id = @supplier_id AND bi.option_id = op.option_id AND op.cat_id = 38");
            query.Append(" AND b.name_full  NOT LIKE '%test%' AND b.email NOT LIKE '%test%' AND bi.booking_id = b.booking_id AND bi.status = 1");
            query.Append(" AND (SELECT COUNT(*) FROM tbl_booking_confirm bf WHERE bf.booking_id = bf.booking_id AND bf.confirm_cat_id = 2) > 0");
            query.Append(" AND (bp.date_time_check_in BETWEEN @Date_start AND @Date_end OR bp.date_time_check_out BETWEEN @Date_start AND @Date_end)");
            query.Append(" GROUP BY b.booking_id , b.date_submit, bi.condition_id, bi.condition_title, bp.date_time_check_in , bp.date_time_check_out ");

            IList<object> iBookingReport = null;
            IList<object> IListBookingResult = new List<object>();
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cmd.Parameters.Add("@Date_start", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@Date_end", SqlDbType.SmallDateTime).Value = dDateEnd;
                cn.Open();
                
              iBookingReport = MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }

            
            int DateDiff = (int)dDateEnd.Subtract(dDateStart).Days;

            int DateDiff_Result = 0;
            DateTime dDAteCurrent = DateTime.Now;
            DateTime dDAteCurrent_result = DateTime.Now;
            
            for (int d = 0; d <= DateDiff; d++)
            {
                int intCountresult = 0;
                dDAteCurrent = dDateStart.AddDays(d);

                int intConditionId = 0;
                int Unit = 0;
                foreach (BookingReport_booking BookingList in iBookingReport)
                {
                     
                    DateDiff_Result = BookingList.DateCheckOut.Subtract(BookingList.DateCheckIn).Days;

                    for (int ds = 0; ds <= DateDiff_Result; ds++)
                    {
                        dDAteCurrent_result = BookingList.DateCheckIn.AddDays(ds);

                        if (dDAteCurrent.Date == dDAteCurrent_result.Date && dDAteCurrent.Date != BookingList.DateCheckOut.Date)
                        {
                            intCountresult = intCountresult + 1;
                            intConditionId = BookingList.ConditionId;
                            Unit = BookingList.TotalUnit;
                        }
                        
                    }

                    
                }
                
                BookingReport_booking cBookingreport = new BookingReport_booking
                {
                    DateSubmit = dDAteCurrent,
                    ConditionId = intConditionId,
                    TotalPeriodNightStay_Real = (intCountresult * Unit)
                    
                };

                IListBookingResult.Add(cBookingreport);
                
            }


            return IListBookingResult;
        }

        public int CountRoomNight_CheckInDateByPeriod(int intProductId, short shrSupplierId, DateTime dDateStart, DateTime dDateEnd)
        {
            int result = 0;

            IList<object> TotalRoomNight = this.GEtRoomNight_CheckInDateByPeriod(intProductId, shrSupplierId, dDateStart, dDateEnd);
            foreach (BookingReport_booking rm in TotalRoomNight)
            {
                result = result + rm.TotalPeriodNightStay_Real;
            }

            return result;
        }

       
    }
}