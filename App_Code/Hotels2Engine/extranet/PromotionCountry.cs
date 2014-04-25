using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Collections;
using Hotels2thailand;
using Hotels2thailand.Staffs;
using Hotels2thailand.Front;
using Hotels2thailand.ProductOption;
using System.Text.RegularExpressions;


namespace Hotels2thailand.Production
{
    /// <summary>
    /// Summary description for PromotionBenefit
    /// </summary>
    public class PromotionCountry :Hotels2BaseClass
    {
     
        
        public int PromotionId { get; set; }
        public short CountryID { get; set; }
        public string Title { get; set; }

        public PromotionCountry()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public IList<object> GetPromotionCountry(int intPromotionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT pcc.promotion_id, pcc.country_id, con.title FROM tbl_promotion_country_extra_net pcc, tbl_country con WHERE con.country_id = pcc.country_id AND pcc.promotion_id = @promotion_id ", cn);
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public int InsertPromotionCounty(int intProductId, int intPromotionId, short shrCountryId)
        {
            int ret = 0;
            using(SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_promotion_country_extra_net (promotion_id,country_id) VALUES(@promotion_id,@country_id)",cn);
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cmd.Parameters.Add("@country_id", SqlDbType.SmallInt).Value = shrCountryId;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }

            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_promotion, StaffLogActionType.Insert, StaffLogSection.Product,
                intProductId, "tbl_promotion_country_extra_net", "promotion_id,country_id", "promotion_id,country_id", intPromotionId, shrCountryId);
            //========================================================================================================================================================
            return ret;
        }

        public bool DeletePromotionCountryAllByPromotionId(int intPromotionId)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tbl_promotion_country_extra_net WHERE promotion_id=@promotion_id", cn);
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }
            return (ret == 1);
        }
        public bool DeletePromotionCountry(int intProductId, int intPromotionId, short shrCountryId)
        {
           IList<object[]> ilistOld_val =  StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_promotion_country_extra_net", "promotion_id,country_id", intPromotionId, shrCountryId);
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tbl_promotion_country_extra_net WHERE promotion_id = @promotion_id AND country_id=@country_id", cn);
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cmd.Parameters.Add("@country_id", SqlDbType.SmallInt).Value = shrCountryId;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }

            StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Product_promotion, StaffLogActionType.Delete, StaffLogSection.Product, intProductId, "tbl_promotion_country", ilistOld_val, "promotion_id,country_id", intPromotionId, shrCountryId);
            return (ret == 1);
        }
    }
}