using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Staffs;


/// <summary>
/// Summary description for Option
/// </summary>
/// 
namespace Hotels2thailand.ProductOption
{
    public class ProductOptionIsWeekdayAll : Hotels2BaseClass
    {
        public int WeekdayId { get; set; }
        public int OptionId { get; set; }
        public bool DaySun { get; set; }
        public bool DayMon { get; set; }
        public bool DayTue { get; set; }
        public bool DayWed { get; set; }
        public bool DayThu { get; set; }
        public bool DayFri { get; set; }
        public bool DaySat { get; set; }

        public ProductOptionIsWeekdayAll GetProductOptionIsWeekdayAllById(int intOptionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT weekdayId,option_id,day_sun,day_mon,day_tue,day_wed,day_thu,day_fri,day_sat FROM tbl_product_option_is_week_day_all WHERE option_id=@option_id", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    return (ProductOptionIsWeekdayAll)MappingObjectFromDataReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        public int InsertIsWeekdayExtra(int intProductId, int OptionId, bool bolIssun, bool bolIsmon, bool bolIsTue, bool bolIsWed, bool bolIsThu, bool bolIsFri, bool bolIsSat)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_option_is_week_day_all (option_id,day_sun,day_mon,day_tue,day_wed,day_thu,day_fri,day_sat) VALUES(@option_id,@day_sun,@day_mon,@day_tue,@day_wed,@day_thu,@day_fri,@day_sat); SET @weekdayId= SCOPE_IDENTITY();", cn);

                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = OptionId;
                cmd.Parameters.Add("@day_sun", SqlDbType.Bit).Value = bolIssun;
                cmd.Parameters.Add("@day_mon", SqlDbType.Bit).Value = bolIsmon;
                cmd.Parameters.Add("@day_tue", SqlDbType.Bit).Value = bolIsTue;
                cmd.Parameters.Add("@day_wed", SqlDbType.Bit).Value = bolIsWed;
                cmd.Parameters.Add("@day_thu", SqlDbType.Bit).Value = bolIsThu;
                cmd.Parameters.Add("@day_fri", SqlDbType.Bit).Value = bolIsFri;
                cmd.Parameters.Add("@day_sat", SqlDbType.Bit).Value = bolIsSat;
                cmd.Parameters.Add("@weekdayId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                ExecuteNonQuery(cmd);
                ret = (int)cmd.Parameters["@weekdayId"].Value;


            }
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_weekend, StaffLogActionType.Insert, StaffLogSection.Product,
                intProductId, "tbl_product_option_is_week_day_all", "option_id,day_sun,day_mon,day_tue,day_wed,day_thu,day_fri,day_sat", "weekdayId", ret);
            //========================================================================================================================================================
            return ret;
        }
        public int InsertIsWeekday(int OptionId, bool bolIssun, bool bolIsmon, bool bolIsTue, bool bolIsWed, bool bolIsThu, bool bolIsFri, bool bolIsSat)
        {
            int ret  = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_option_is_week_day_all (option_id,day_sun,day_mon,day_tue,day_wed,day_thu,day_fri,day_sat) VALUES(@option_id,@day_sun,@day_mon,@day_tue,@day_wed,@day_thu,@day_fri,@day_sat); SET @weekdayId= SCOPE_IDENTITY();", cn);

                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = OptionId;
                cmd.Parameters.Add("@day_sun", SqlDbType.Bit).Value = bolIssun;
                cmd.Parameters.Add("@day_mon", SqlDbType.Bit).Value = bolIsmon;
                cmd.Parameters.Add("@day_tue", SqlDbType.Bit).Value = bolIsTue;
                cmd.Parameters.Add("@day_wed", SqlDbType.Bit).Value = bolIsWed;
                cmd.Parameters.Add("@day_thu", SqlDbType.Bit).Value = bolIsThu;
                cmd.Parameters.Add("@day_fri", SqlDbType.Bit).Value = bolIsFri;
                cmd.Parameters.Add("@day_sat", SqlDbType.Bit).Value = bolIsSat;
                cmd.Parameters.Add("@weekdayId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                ExecuteNonQuery(cmd);
                ret = (int)cmd.Parameters["@weekdayId"].Value;
              
                
            }
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_weekend, StaffLogActionType.Insert, StaffLogSection.Product,
                int.Parse(HttpContext.Current.Request.QueryString["pid"]), "tbl_product_option_is_week_day_all", "option_id,day_sun,day_mon,day_tue,day_wed,day_thu,day_fri,day_sat", "weekdayId", ret);
            //========================================================================================================================================================
            return ret;
        }

        public bool UpdateIsWeekdayall(int OptionId, bool bolIssun, bool bolIsmon, bool bolIsTue, bool bolIsWed, bool bolIsThu, bool bolIsFri, bool bolIsSat)
        {
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_option_is_week_day_all", "day_sun,day_mon,day_tue,day_wed,day_thu,day_fri,day_sat", "option_id", OptionId);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option_is_week_day_all SET day_sun=@day_sun,day_mon=@day_mon,day_tue=@day_tue,day_wed=@day_wed,day_thu=@day_thu,day_fri=@day_fri,day_sat=@day_sat WHERE option_id=@option_id", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = OptionId;
                cmd.Parameters.Add("@day_sun", SqlDbType.Bit).Value = bolIssun;
                cmd.Parameters.Add("@day_mon", SqlDbType.Bit).Value = bolIsmon;
                cmd.Parameters.Add("@day_tue", SqlDbType.Bit).Value = bolIsTue;
                cmd.Parameters.Add("@day_wed", SqlDbType.Bit).Value = bolIsWed;
                cmd.Parameters.Add("@day_thu", SqlDbType.Bit).Value = bolIsThu;
                cmd.Parameters.Add("@day_fri", SqlDbType.Bit).Value = bolIsFri;
                cmd.Parameters.Add("@day_sat", SqlDbType.Bit).Value = bolIsSat;

                cn.Open();
                ret = ExecuteNonQuery(cmd);
                

            }
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_weekend, StaffLogActionType.Update, StaffLogSection.Product,
                int.Parse(HttpContext.Current.Request.QueryString["pid"]), "tbl_product_option_is_week_day_all", "day_sun,day_mon,day_tue,day_wed,day_thu,day_fri,day_sat", arroldValue, "option_id", OptionId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }
    }
}