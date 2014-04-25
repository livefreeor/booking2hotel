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
    public class PromotioncancellExtranet : Hotels2BaseClass
    {
        public int CancelruleId { get; set; }
        public int PromotionId { get; set; }
        public byte Daycancel { get; set; }
        public byte ChargePer { get; set; }
        public byte ChargeNight { get; set; }
        public bool Status { get; set; }

        
        public List<object> getCencelRuleExtranetbyPromotionId(int intPromotionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_option_condition_cancel_content_promotion_extra_net WHERE promotion_id=@promotion_id AND status = 1 ORDER BY day_cancel", cn);
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public byte getTopValuDayCcancel(int intPromotionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT TOp 1 day_cancel FROM tbl_product_option_condition_cancel_content_promotion_extra_net  WHERE promotion_id=@promotion_id AND status = 1 ORDER BY day_cancel DESC", cn);
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (byte)reader[0];
                else
                    return 0;

            }
        }
        public int InsertNewCancelRule(int intProductId, int intPromotionId, byte bytDayCancel, byte bytChargePer, byte bytChargenight)
        {
            int ret = 0;

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_option_condition_cancel_content_promotion_extra_net (promotion_id,day_cancel,charge_percent,charge_night) VALUES (@promotion_id,@day_cancel,@charge_percent,@charge_night)", cn);
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cmd.Parameters.Add("@day_cancel", SqlDbType.TinyInt).Value = bytDayCancel;
                cmd.Parameters.Add("@charge_percent", SqlDbType.TinyInt).Value = bytChargePer;
                cmd.Parameters.Add("@charge_night", SqlDbType.TinyInt).Value = bytChargenight;

                cn.Open();
                ret = ExecuteNonQuery(cmd);

            }
            //#Staff_Activity_Log==========================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_Cancel_Extra, StaffLogActionType.Insert, StaffLogSection.Product,
                intProductId, "tbl_product_option_condition_cancel_content_promotion_extra_net", "promotion_id,day_cancel,charge_percent,charge_night",
                "promotion_id,day_cancel", intPromotionId, bytDayCancel);
            //============================================================================================================================


            return ret;
        }

        public bool UpdateCancelRuleByCanCelIdStatus(int intProductId, int intRuleId, bool bolStatus)
        {
            int ret = 0;

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_option_condition_cancel_content_promotion_extra_net", "status", "cancel_rule_id", intRuleId);
            //============================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option_condition_cancel_content_promotion_extra_net SET status=@status WHERE cancel_rule_id = @cancel_rule_id", cn);
                cmd.Parameters.Add("@cancel_rule_id", SqlDbType.Int).Value = intRuleId;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;

                cn.Open();

                ret = ExecuteNonQuery(cmd);
            }


            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_Cancel_Extra, StaffLogActionType.Update, StaffLogSection.Product, intProductId,
                "tbl_product_option_condition_cancel_content_promotion_extra_net", "status", arroldValue, "cancel_rule_id", intRuleId);
            //==================================================================================================================== COMPLETED ========

            return (ret == 1);
        }
        public bool UpdateCancelRuleByCanCelIdANdDayCancel(int intProductId, int intRuleId, byte byteDayCancel, byte chargenight, byte chargePer)
        {
            int ret = 0;

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_option_condition_cancel_content_promotion_extra_net", "promotion_id,day_cancel,charge_percent,charge_night", "cancel_rule_id", intRuleId);
            //============================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option_condition_cancel_content_promotion_extra_net SET day_cancel=@day_cancel,charge_percent=@charge_percent,charge_night=@charge_night  WHERE cancel_rule_id = @cancel_rule_id", cn);


                cmd.Parameters.Add("@cancel_rule_id", SqlDbType.Int).Value = intRuleId;
                cmd.Parameters.Add("@day_cancel", SqlDbType.TinyInt).Value = byteDayCancel;
                cmd.Parameters.Add("@charge_percent", SqlDbType.TinyInt).Value = chargePer;
                cmd.Parameters.Add("@charge_night", SqlDbType.TinyInt).Value = chargenight;
                cn.Open();

                ret = ExecuteNonQuery(cmd);
            }


            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_Cancel_Extra, StaffLogActionType.Update, StaffLogSection.Product, intProductId,
                "tbl_product_option_condition_cancel_content_promotion_extra_net", "promotion_id,day_cancel,charge_percent,charge_night", arroldValue, "cancel_rule_id", intRuleId);
            //==================================================================================================================== COMPLETED ========

            return (ret == 1);
        }

        public bool DeleteCancelRuleByCanCelIdANdDayCancel(int intProductId, int intRuleID)
        {
            IList<object[]> arroldValue = null;
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            arroldValue = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_product_option_condition_cancel_content_promotion_extra_net", "cancel_rule_id", intRuleID);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM  tbl_product_option_condition_cancel_content_promotion_extra_net WHERE cancel_rule_id = @cancel_rule_id", cn);
                cmd.Parameters.Add("@cancel_rule_id", SqlDbType.Int).Value = intRuleID;

                cn.Open();

                ret = ExecuteNonQuery(cmd);
            }

            //#Staff_Activity_Log================================================================================================ STEP 2 ==
            StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Product_Cancel_Extra, StaffLogActionType.Delete, StaffLogSection.Product, intProductId,
                "tbl_product_option_condition_cancel_content_promotion_extra_net", arroldValue, "cancel_rule_id", intRuleID);
            //============================================================================================================================
            return (ret == 1);

        }


    }
}