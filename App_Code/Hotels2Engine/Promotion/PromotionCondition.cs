using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using Hotels2thailand.Staffs;

/// <summary>
/// Summary description for Promotion
/// </summary>
/// 
namespace Hotels2thailand.ProductOption
{
    public class PromotionCondition:Hotels2BaseClass
    {
        public PromotionCondition()
        {
           
        }

        public int PromotionId { get; set; }
        public int ConditionId { get; set; }


        public List<object> getConditionByPromotionId(int intPromotionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT promotion_id, condition_id FROM tbl_promotion_condition WHERE promotion_id=@promotion_id AND status = 1", cn);
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public PromotionCondition getConditionByPromotionAndConditionId(int intPromotionId, int intCondition)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT promotion_id, condition_id FROM tbl_promotion_condition WHERE promotion_id=@promotion_id AND condition_id=@condition_id", cn);
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intCondition;
                cn.Open();

                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    return (PromotionCondition)MappingObjectFromDataReader(reader);
                }
                else
                {
                    return null;
                }
                
            }
        }

        public int InsertMappingConditionPromotionId(int intPromotionId, int intCondition)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder Query = new StringBuilder();
                
                Query.Append("INSERT INTO tbl_promotion_condition(promotion_id,condition_id,status)VALUES(@promotion_id,@condition_id,'1')");
                
                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intCondition;
                
                cn.Open();
                ret = ExecuteNonQuery(cmd);
                
            }

            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_promotion, StaffLogActionType.Insert, StaffLogSection.Product,
                int.Parse(HttpContext.Current.Request.QueryString["pid"]), "tbl_promotion_condition", "promotion_id,condition_id,status", "promotion_id,condition_id", intPromotionId, intCondition);
            //========================================================================================================================================================
            return ret;
        }
        public int UPdateMappingConditionPromotionId(int intPromotionId, int intCondition, bool bolStatus)
        {
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_promotion_condition", "status", "promotion_id,condition_id", intPromotionId, intCondition);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder Query = new StringBuilder();
                Query.Append("UPDATE tbl_promotion_condition SET status=@status WHERE promotion_id=@promotion_id AND condition_id=@condition_id");
                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intCondition;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
                cn.Open();
                ExecuteNonQuery(cmd);
                
            }
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_promotion, StaffLogActionType.Update, StaffLogSection.Product, int.Parse(HttpContext.Current.Request.QueryString["pid"]),
                "tbl_promotion_condition", "status", arroldValue, "promotion_id,condition_id", intPromotionId, intCondition);
            //==================================================================================================================== COMPLETED ========
            return ret;
        }

        /// <summary>
        /// AJAX PAGE : get Option active Promotion
        /// </summary>
        /// <param name="intPromotionId"></param>
        /// <returns></returns>
        public IDictionary<int, string> getOptionByActivePromotion(int intPromotionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
               IDictionary<int, string> dicOutput = new Dictionary<int, string>();
                StringBuilder Query = new StringBuilder();
                Query.Append(" SELECT DISTINCT(opc.option_id), op.title");
                Query.Append(" FROM tbl_promotion_condition pcon, tbl_product_option_condition opc, tbl_product_option op");
                Query.Append(" WHERE pcon.condition_id = opc.condition_id AND op.option_id = opc.option_id AND pcon.promotion_id = @promotion_id AND pcon.status = 1 AND opc.status = 1");
                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                
                    while (reader.Read())
                    {
                        dicOutput.Add((int)reader[0], reader[1].ToString());
                    }
                
                return dicOutput;
            }
        }
        

        /// <summary>
        /// AJAX PAGE : get ConditionList active Promotion 
        /// </summary>
        /// <returns></returns>
        public IDictionary<int, string> getPromotionCondition(int intPromotionId, int intOptionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                IDictionary<int, string> dicOutput = new Dictionary<int, string>();
                StringBuilder Query = new StringBuilder();
                Query.Append("SELECT condition_id, title FROM tbl_product_option_condition");
                Query.Append(" WHERE option_id = @option_id AND condition_id IN (");
                Query.Append(" SELECT condition_id FROM tbl_promotion_condition WHERE promotion_id = @promotion_id AND status = 1) AND status = 1");
                SqlCommand cmd = new SqlCommand(Query.ToString(), cn);
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                
                    while (reader.Read())
                    {
                        dicOutput.Add((int)reader[0], reader[1].ToString());
                    }
                
                return dicOutput;
            }
        }
        
         
        
    }
}