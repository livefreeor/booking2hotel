using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using Hotels2thailand.Staffs;


/// <summary>
/// Summary description for ProductCategory
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class ProductPriceExtra_period_longweek_end : Hotels2BaseClass
    {

        public int longWeekend_SupplementID { get; set; }
        public int ConditionId { get; set; }
        public string DateTitle { get; set; }
        public DateTime DateSupplement { get; set; }
        public decimal Supplement { get; set; }
        public bool Status { get; set; }

        public int InsertLongWeekend_supplement( int inProductId, int intConditionId, string strTitle, DateTime dDateSuple, decimal decSupplement)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_price_longweekend_extra_net (condition_id,date_title,date_supplement,supplement,status)VALUES(@condition_id,@date_title,@date_supplement,@supplement,@status); SET @long_weekend_supplement_id = SCOPE_IDENTITY();", cn);

                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                cmd.Parameters.Add("@date_title", SqlDbType.NVarChar).Value = strTitle;
                cmd.Parameters.Add("@date_supplement", SqlDbType.SmallDateTime).Value = dDateSuple;
                cmd.Parameters.Add("@supplement", SqlDbType.SmallMoney).Value = decSupplement;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = true;
                cmd.Parameters.Add("@long_weekend_supplement_id", SqlDbType.Int).Direction = ParameterDirection.Output;

                cn.Open();
                ExecuteNonQuery(cmd);
                ret = (int)cmd.Parameters["@long_weekend_supplement_id"].Value;
            }



            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_Supplement, StaffLogActionType.Insert, StaffLogSection.Product,
               inProductId, "tbl_product_price_longweekend_extra_net", "date_title,date_supplement,supplement,status", "long_weekend_supplement_id", ret);
            //========================================================================================================================================================

            return ret;
        }

        public ProductPriceExtra_period_longweek_end GetLongWeekEnd_ByDate(DateTime dDateSuple)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_price_longweekend_extra_net WHERE date_supplement = @date_supplement", cn);
                cmd.Parameters.Add("@date_supplement", SqlDbType.SmallDateTime).Value = dDateSuple;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (ProductPriceExtra_period_longweek_end)MappingObjectFromDataReader(reader);
                else
                    return null;
            }
        }


        public IList<object> GetLongWeekEndList_ByConditionId(int intConditionId, DateTime dDateFrom, DateTime dDAteTo)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_price_longweekend_extra_net WHERE condition_id = @condition_id AND date_supplement BETWEEN '" + dDateFrom.ToString("yyyy-MM-dd") + "' AND '" + dDAteTo.ToString("yyyy-MM-dd") + "'", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public int GetCOuntLongWeekEndList_ByConditionIdAndDAteHoliday(int intConditionId, DateTime dDateholiday)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_price_longweekend_extra_net WHERE condition_id = @condition_id AND date_supplement=@date_supplement AND status = 1", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                cmd.Parameters.Add("@date_supplement", SqlDbType.SmallDateTime).Value = dDateholiday;
                cn.Open();
                return (int)ExecuteScalar(cmd);
            }
        }

        public bool DisableWeekendByProcess(int intConditionId, DateTime dDateFrom, DateTime dDAteTo)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_price_longweekend_extra_net SET status = 0 WHERE condition_id = @condiiton_id AND date_supplement BETWEEN '" + dDateFrom.ToString("yyyy-MM-dd") + "' AND '" + dDAteTo.ToString("yyyy-MM-dd") + "' ", cn);
                cn.Open();
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
               return ( ExecuteNonQuery(cmd) == 1);
            }
        }
       
    }
}