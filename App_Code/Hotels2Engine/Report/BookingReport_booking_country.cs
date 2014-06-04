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
using Hotels2thailand.Production;


/// <summary>
/// Summary description for ProductCategory
/// </summary>
/// 
namespace Hotels2thailand.Report
{
    public class BookingReport_booking_country : Hotels2BaseClass
    {

        public int CountryId { get; set; }
        public string CountryTitle { get; set; }
        public string CountryCode { get; set; }
        public int Total { get; set; }
        public DateTime DateCheckIn { get; set; }
        public DateTime DateCheckOut { get; set; }

        public IList<object> getBookingCompleted_Country(int intProductId, short shrSupplierId, DateTime dDateStart, DateTime dDateEnd)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT c.country_id, c.title, c.country_code,");
            query.Append(" (SELECT COUNT(DISTINCT(b.booking_id))");
            query.Append(" FROM tbl_booking b,tbl_booking_product bp, tbl_booking_item bi,tbl_product p, tbl_product_option op");
            query.Append(" WHERE bp.booking_id = b.booking_id AND bp.product_id = @product_id AND bi.booking_id= b.booking_id AND p.product_id = bp.product_id");
            query.Append(" AND bi.option_id = op.option_id AND op.cat_id = 38 AND b.country_id = c.country_id AND bp.status= 1 AND bi.status = 1");
            query.Append(" AND bp.supplier_id = @supplier_id AND b.name_full  NOT LIKE '%test%' AND b.email NOT LIKE '%test%'");
            query.Append(" AND (SELECT COUNT(*) FROM tbl_booking_confirm bf WHERE b.booking_id = bf.booking_id AND bf.confirm_cat_id = 4) > 0");
            query.Append(" AND bp.status_id in(15,28,36,80,81,82) AND b.status_extranet_id NOT IN (3,4)");
            query.Append(" AND b.date_submit BETWEEN @Date_start AND @Date_end ) AS Total");
            query.Append(" FROM tbl_country c ");
            query.Append(" ORDER BY Total DESC, c.title");

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


        public IList<object> getBookingAll_Country(int intProductId, short shrSupplierId, DateTime dDateStart, DateTime dDateEnd)
        {
            StringBuilder query = new StringBuilder();

            query.Append("SELECT c.country_id, c.title, c.country_code,");
            query.Append(" (SELECT COUNT(DISTINCT(b.booking_id))");
            query.Append(" FROM tbl_booking b,tbl_booking_product bp, tbl_booking_item bi,tbl_product p, tbl_product_option op");
            query.Append(" WHERE bp.booking_id = b.booking_id AND bp.product_id = @product_id AND bi.booking_id= b.booking_id AND p.product_id = bp.product_id");
            query.Append(" AND bi.option_id = op.option_id AND op.cat_id = 38 AND b.country_id = c.country_id");
            query.Append(" AND bp.supplier_id = @supplier_id AND b.name_full  NOT LIKE '%test%' AND b.email NOT LIKE '%test%'");

            query.Append(" AND b.date_submit BETWEEN @Date_start AND @Date_end ) AS Total");
            query.Append(" FROM tbl_country c ");
            query.Append(" ORDER BY Total DESC, c.title");

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

        public IList<object> GetCountryRoomNight_BookingDate(int intProductId, short shrSupplierId, DateTime dDateStart, DateTime dDateEnd)
        {
            StringBuilder query = new StringBuilder();

            query.Append(" SELECT c.country_id, c.title, c.country_code,(");


            query.Append("SELECT  ISNULL(SUM(( bi.unit * (DATEDIFF( DAY, date_time_check_in,date_time_check_out))))	,0)");
            query.Append(" FROM tbl_booking_item bi , tbl_booking b , tbl_booking_product bp, tbl_product_option op WHERE ");
            query.Append(" b.booking_id = bi.booking_id AND op.option_id = bi.option_id AND op.cat_id = 38  AND b.country_id = c.country_id");
            query.Append(" AND b.name_full  NOT LIKE '%test%' AND b.email NOT LIKE '%test%'");
            query.Append(" AND (SELECT COUNT(*) FROM tbl_booking_confirm bf WHERE b.booking_id = bf.booking_id AND bf.confirm_cat_id = 2) > 0");
            query.Append(" AND bp.booking_id = b.booking_id AND bp.booking_product_id = bi.booking_product_id");
            query.Append(" AND bp.product_id = @product_id AND bp.supplier_id = @supplier_id AND b.status = 0 AND bp.status= 1 AND bi.status = 1");
            query.Append(" AND b.date_submit BETWEEN @Date_start AND @Date_end");
            query.Append(" )AS Total");

            query.Append(" FROM tbl_country c ");
            query.Append(" ORDER BY Total DESC, c.title");



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



        public IList<object> GEtCountryRoomNight_CheckInDateByPeriod(int intProductId, short shrSupplierId, DateTime dDateStart, DateTime dDateEnd)
        {
            StringBuilder query = new StringBuilder();

            query.Append("SELECT c.country_id, c.title, c.country_code, SUM(bi.unit) AS Totalunit, bp.date_time_check_in , bp.date_time_check_out ");
            
            query.Append(" FROM tbl_booking b, tbl_booking_product bp, tbl_booking_item bi ,tbl_product_option op, tbl_country c");
            query.Append(" WHERE bp.booking_id=b.booking_id AND c.country_id=b.country_id AND b.status = 0 AND bp.status= 1");
            query.Append(" AND bp.product_id = @product_id AND bp.supplier_id = @supplier_id AND bi.option_id = op.option_id AND op.cat_id = 38 ");
            query.Append(" AND b.name_full  NOT LIKE '%test%' AND b.email NOT LIKE '%test%' AND bi.booking_id = b.booking_id AND bi.status = 1");
            query.Append(" AND (SELECT COUNT(*) FROM tbl_booking_confirm bf WHERE bf.booking_id = bf.booking_id AND bf.confirm_cat_id = 2) > 0");
            query.Append(" AND (bp.date_time_check_in BETWEEN @Date_start AND @Date_end OR bp.date_time_check_out BETWEEN @Date_start AND @Date_end)");
            query.Append(" GROUP BY c.country_id, c.title,c.country_code, bp.date_time_check_in , bp.date_time_check_out ");

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

                
                int intCountryId = 0;
                int Unit = 0;
                string strCountryTitle = string.Empty;
                foreach (BookingReport_booking_country BookingList in iBookingReport)
                {
                   
                   
                    DateDiff_Result = BookingList.DateCheckOut.Subtract(BookingList.DateCheckIn).Days;

                    for (int ds = 0; ds <= DateDiff_Result; ds++)
                    {
                        dDAteCurrent_result = BookingList.DateCheckIn.AddDays(ds);

                        if (dDAteCurrent.Date == dDAteCurrent_result.Date && dDAteCurrent.Date != BookingList.DateCheckOut.Date)
                        {
                            intCountresult = intCountresult + 1;
                            intCountryId = BookingList.CountryId;
                            strCountryTitle = BookingList.CountryTitle;
                            Unit = BookingList.Total;
                        }
                        
                    }

                   
                }

                BookingReport_booking_country cBookingreport = new BookingReport_booking_country
                {
                    CountryId = intCountryId,
                    CountryTitle = strCountryTitle,
                    Total = (intCountresult * Unit)
                    
                };

                IListBookingResult.Add(cBookingreport);
                
            }


            return IListBookingResult;
        }

        public IList<object> GetBookingCountryRoomNight_CheckInDateByPeriod(int intProductId, short shrSupplierId, DateTime dDateStart, DateTime dDateEnd)
        {
        
            Country cCountry = new Country();
            Dictionary<string, string> dicCountry = cCountry.GetCountryAll();
            IList<object> listREsult = this.GEtCountryRoomNight_CheckInDateByPeriod(intProductId, shrSupplierId, dDateStart, dDateEnd);

            //HttpContext.Current.Response.Write(listREsult.Count());
            //HttpContext.Current.Response.End();
            IList<object> iListREsult_final = new List<object>();
            foreach (KeyValuePair<string, string> item in dicCountry)
            {
                int intTotal = 0;

                foreach (BookingReport_booking_country REsult in listREsult)
                {
                    if (REsult.CountryId == int.Parse(item.Key))
                    {
                        intTotal = intTotal + REsult.Total;
                    }
                }
                
                BookingReport_booking_country cBookingreport = new BookingReport_booking_country
                {
                    CountryId = int.Parse(item.Key),
                    CountryTitle = item.Value.ToString(),
                    Total = intTotal

                };

                iListREsult_final.Add(cBookingreport);
            }


            return iListREsult_final.OrderByDescending(c => (int)c.GetType().GetProperty("Total").GetValue(c, null)).ThenBy(n => (string)n.GetType().GetProperty("CountryTitle").GetValue(n, null)).ToList();

        }

       
    }
}