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
namespace Hotels2thailand.Production
{
    public class PublicholidayExtranet : Hotels2BaseClass
    {

        public int HolidayId { get; set; }
        public string Title { get; set; }
        public DateTime HolidayDate { get; set; }

        

        public List<object> getAllPublicHolidaysByYear(DateTime dDateYear)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_public_holidays_extra_net WHERE YEAR(holidays_date)=@holidays_date ORDER BY holidays_date, holidays_date_title", cn);
                cmd.Parameters.Add("@holidays_date", SqlDbType.VarChar).Value = dDateYear.Year;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
            //var Result = from ph in dcOptionHolidays.tbl_product_public_holidays
            //             where ph.holidays_date.Year == dDateYear.Year
            //             orderby ph.holidays_date
            //             select ph;
            //return MappingObjectFromDataContextCollection(Result);
        }

        public IList<object> getAllPublicHolidaysByCurrentDate()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_public_holidays_extra_net WHERE holidays_date >= CONVERT(DateTime, convert(varchar(20),@holidays_date,101)) ORDER BY holidays_date, holidays_date_title", cn);
                cmd.Parameters.Add("@holidays_date", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public PublicholidayExtranet getPublicHolidaybyId(int holidayId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_public_holidays_extra_net WHERE holidays_id=@holidays_id", cn);
                cmd.Parameters.Add("@holidays_id", SqlDbType.TinyInt).Value = holidayId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (PublicholidayExtranet)MappingObjectFromDataReader(reader);
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
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_public_holidays_extra_net (holidays_date_title,holidays_date)VALUES(@holidays_date_title,@holidays_date); SET @holidays_id = SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@holidays_date_title", SqlDbType.NVarChar).Value = Title;
                cmd.Parameters.Add("@holidays_date", SqlDbType.SmallDateTime).Value = dDate;
                cmd.Parameters.Add("@holidays_id", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cn.Open();

                ExecuteNonQuery(cmd);
                ret = (byte)cmd.Parameters["@holidays_id"].Value;
            }

            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_holiday, StaffLogActionType.Insert, StaffLogSection.NULL,
                null, "tbl_product_public_holidays_extra_net", "holidays_date_title,holidays_date", "holidays_id", ret);
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
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_public_holidays_extra_net", "holidays_date_title,holidays_date", "holidays_id", bytHolidays);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_public_holidays_extra_net SET holidays_date_title=@holidays_date_title, holidays_date=@holidays_date WHERE holidays_id=@holidays_id", cn);
                cmd.Parameters.Add("@holidays_id", SqlDbType.TinyInt).Value = bytHolidays;
                cmd.Parameters.Add("@holidays_date_title", SqlDbType.NVarChar).Value = strTitl;
                cmd.Parameters.Add("@holidays_date", SqlDbType.SmallDateTime).Value = dDate;
                cn.Open();

                ret = ExecuteNonQuery(cmd);

            }

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_holiday, StaffLogActionType.Update, StaffLogSection.NULL,
                null, "tbl_product_public_holidays_extra_net", "holidays_date_title,holidays_date", arroldValue, "holidays_id", bytHolidays);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }

    }

    
}