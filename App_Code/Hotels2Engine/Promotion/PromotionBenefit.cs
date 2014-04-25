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


namespace Hotels2thailand.ProductOption
{
    /// <summary>
    /// Summary description for PromotionBenefit
    /// </summary>
    public class PromotionBenefit :Hotels2BaseClass
    {
        public PromotionBenefit()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int BenefitID { get; set; }
        public int PromotionId { get; set; }
        public byte DaydiscountStart { get; set; }
        public byte DaydiscountNum { get; set; }
        public byte TypeId { get; set; }
        public decimal DiscountAmount { get; set; }
        public byte Priority { get; set; }
        public bool Status { get; set; }

        public List<object> GetBenefitListByPromotionId(int intPromotionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT benefit_id, promotion_id, day_discount_start, day_discount_num, type_id, discount, priority, status FROM tbl_promotion_benefit WHERE promotion_id=@promotion_id ORDER BY status, priority", cn);
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public int CheckBenefitIsRecord(int intPromotionId, byte bytDiscountstart, byte bytdiscountNum, byte bytProTypeId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM tbl_promotion_benefit WHERE promotion_id=@promotion_id AND day_discount_start=@day_discount_start AND day_discount_num=@day_discount_num AND type_id=@type_id", cn);
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cmd.Parameters.Add("@day_discount_start", SqlDbType.TinyInt).Value = bytDiscountstart;
                cmd.Parameters.Add("@day_discount_num", SqlDbType.TinyInt).Value = bytdiscountNum;
                cmd.Parameters.Add("@type_id", SqlDbType.TinyInt).Value = bytProTypeId;
                cn.Open();

                int ret = (int)ExecuteScalar(cmd);
                return ret;
            }
        }

        public int InsertBenefit(int intPromotionId, byte bytDiscountstart, byte bytdiscountNum, byte bytProTypeId, decimal decDiscount, byte bytPriority)
        {
            int ret  = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO  tbl_promotion_benefit (promotion_id, day_discount_start, day_discount_num, type_id, discount, priority) VALUES (@promotion_id, @day_discount_start, @day_discount_num, @type_id, @discount, @priority) ; SET @benefit_id = SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@promotion_id", SqlDbType.Int).Value = intPromotionId;
                cmd.Parameters.Add("@day_discount_start", SqlDbType.TinyInt).Value = bytDiscountstart;
                cmd.Parameters.Add("@day_discount_num", SqlDbType.TinyInt).Value = bytdiscountNum;
                cmd.Parameters.Add("@type_id", SqlDbType.TinyInt).Value = bytProTypeId;
                cmd.Parameters.Add("@discount", SqlDbType.Money).Value = decDiscount;
                cmd.Parameters.Add("@priority", SqlDbType.TinyInt).Value = bytPriority;
                cmd.Parameters.Add("@benefit_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();

                ExecuteNonQuery(cmd);
                ret = (int)cmd.Parameters["@benefit_id"].Value;

            }
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_promotion, StaffLogActionType.Insert, StaffLogSection.Product,
                int.Parse(HttpContext.Current.Request.QueryString["pid"]), "tbl_promotion_benefit", "promotion_id,day_discount_start, day_discount_num, type_id, discount, priority", "promotion_id", ret);
            //========================================================================================================================================================
            return ret;
        }

        public bool UpdateBenefitByBenefitId(int intBenefitId, byte bytDiscountstart, byte bytdiscountNum, byte bytProTypeId, decimal decDiscount, byte bytPriority)
        {
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_promotion_benefit", "day_discount_start,day_discount_num,type_id,discount,priority", "benefit_id", intBenefitId);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_promotion_benefit SET day_discount_start=@day_discount_start, day_discount_num=@day_discount_num, type_id=@type_id, discount=@discount, priority=@priority WHERE benefit_id=@benefit_id", cn);
                cmd.Parameters.Add("@benefit_id", SqlDbType.Int).Value = intBenefitId;
                cmd.Parameters.Add("@day_discount_start", SqlDbType.TinyInt).Value = bytDiscountstart;
                cmd.Parameters.Add("@day_discount_num", SqlDbType.TinyInt).Value = bytdiscountNum;
                cmd.Parameters.Add("@type_id", SqlDbType.TinyInt).Value = bytProTypeId;
                cmd.Parameters.Add("@discount", SqlDbType.Money).Value = decDiscount;
                cmd.Parameters.Add("@priority", SqlDbType.TinyInt).Value = bytPriority;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
                
            }

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_holiday, StaffLogActionType.Update, StaffLogSection.Product,
                int.Parse(HttpContext.Current.Request.QueryString["pid"]), "tbl_promotion_benefit", "day_discount_start,day_discount_num,type_id,discount,priority", arroldValue, "benefit_id", intBenefitId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);

        }

        public bool UpdateBenefitByBenefitIdStatus(int intBenefitId, bool bolStatus)
        {
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_promotion_benefit", "status", "benefit_id", intBenefitId);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_promotion_benefit SET status=@status WHERE benefit_id=@benefit_id", cn);
                cmd.Parameters.Add("@benefit_id", SqlDbType.Int).Value = intBenefitId;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
                cn.Open();
                ret = ExecuteNonQuery(cmd);

            }

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_holiday, StaffLogActionType.Update, StaffLogSection.Product,
                int.Parse(HttpContext.Current.Request.QueryString["pid"]), "tbl_promotion_benefit", "status", arroldValue, "benefit_id", intBenefitId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);

        }
    }
}