using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Production;
using Hotels2thailand.Staffs;

/// <summary>
/// Summary description for ProductOptioPublicHolidays
/// </summary>
/// 
namespace Hotels2thailand.ProductOption
{
    public class ProductOptioPublicHolidays:Hotels2BaseClass
    {
        public ProductOptioPublicHolidays()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int HolidayId { get; set; }
        public string Title { get; set; }
        public DateTime HolidayDate { get; set; }

        private LinqProductionDataContext dcOptionHolidays = new LinqProductionDataContext();

        public List<object> getAllPublicHolidaysByYear(DateTime dDateYear)
        {

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_public_holidays WHERE YEAR(holidays_date) = " + dDateYear.Year + " ORDER BY holidays_date", cn);
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
            //var Result = from ph in dcOptionHolidays.tbl_product_public_holidays
            //             where ph.holidays_date.Year == dDateYear.Year
            //             orderby ph.holidays_date
            //             select ph;
            //return MappingObjectFromDataContextCollection(Result);
        }



        public ProductOptioPublicHolidays getPublicHolidaybyId(int holidayId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_public_holidays WHERE holidays_id=@holidays_id", cn);
                cmd.Parameters.Add("@holidays_id", SqlDbType.TinyInt).Value = holidayId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (ProductOptioPublicHolidays)MappingObjectFromDataReader(reader);
                else
                    return null;
            }
            
        }

        //public int InsertHoliday(ProductOptioPublicHolidays cProductholidays)
        //{
        //    tbl_product_public_holiday cInsert = new tbl_product_public_holiday
        //    {
        //        holidays_date_title = cProductholidays.Title,
        //        holidays_date = cProductholidays.HolidayDate
        //    };

        //    dcOptionHolidays.tbl_product_public_holidays.InsertOnSubmit(cInsert);
        //    dcOptionHolidays.SubmitChanges();
        //    int ret = 1;
        //    return ret;
        //}

        public int InsertHoliday(string Title, DateTime dDate)
        {
            byte ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_public_holidays (holidays_date_title,holidays_date)VALUES(@holidays_date_title,@holidays_date); SET @holidays_id = SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@holidays_date_title", SqlDbType.NVarChar).Value = Title;
                cmd.Parameters.Add("@holidays_date", SqlDbType.SmallDateTime).Value = dDate;
                cmd.Parameters.Add("@holidays_id", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cn.Open();

                ExecuteNonQuery(cmd);
                ret = (byte)cmd.Parameters["@holidays_id"].Value;
            }

            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_holiday, StaffLogActionType.Insert, StaffLogSection.NULL,
                null, "tbl_product_public_holidays", "holidays_date_title,holidays_date", "holidays_id", ret);
            //========================================================================================================================================================
            return ret;

        }

        //public bool UpdateHolidays(ProductOptioPublicHolidays cProductholidays)
        //{
        //    var Update = dcOptionHolidays.tbl_product_public_holidays.SingleOrDefault(ph => ph.holidays_id == cProductholidays.HolidayId);
        //    Update.holidays_date_title = cProductholidays.Title;
        //    Update.holidays_date = cProductholidays.HolidayDate;
        //    dcOptionHolidays.SubmitChanges();

        //    int ret = 1;
        //    return (ret==1);
        //}

        public bool UpdateHolidays(int bytHolidays, string strTitl, DateTime dDate)
        {
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_public_holidays", "holidays_date_title,holidays_date", "holidays_id", bytHolidays);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_public_holidays SET holidays_date_title=@holidays_date_title, holidays_date=@holidays_date WHERE holidays_id=@holidays_id", cn);
                cmd.Parameters.Add("@holidays_id", SqlDbType.TinyInt).Value = bytHolidays;
                cmd.Parameters.Add("@holidays_date_title", SqlDbType.NVarChar).Value = strTitl;
                cmd.Parameters.Add("@holidays_date", SqlDbType.SmallDateTime).Value = dDate;
                cn.Open();

                ret = ExecuteNonQuery(cmd);
                
            }

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_holiday, StaffLogActionType.Update, StaffLogSection.NULL,
                null, "tbl_product_public_holidays", "holidays_date_title,holidays_date", arroldValue, "holidays_id", bytHolidays);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }

        //public bool Update()
        //{
        //    return ProductOptioPublicHolidays.UpdatePublicHolidays(this.HolidayId, this.Title, this.HolidayDate);
        //}

        //public static bool UpdatePublicHolidays(byte bytHolidaysId, string strTitle, DateTime datHolidayDate)
        //{
        //    ProductOptioPublicHolidays cPublicHolidays = new ProductOptioPublicHolidays
        //    {
        //         HolidayId  = bytHolidaysId,
        //         Title = strTitle,
        //         HolidayDate = datHolidayDate
        //    };

        //    return cPublicHolidays.UpdateHolidays(cPublicHolidays);

        //}
    }
}