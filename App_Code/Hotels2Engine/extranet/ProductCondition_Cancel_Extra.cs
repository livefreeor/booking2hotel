using System;
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
    public class ProductCondition_Cancel_Extra : Hotels2BaseClass
    {
        public int CancelID { get; set; }
        public int ConditionId { get; set; }
        public DateTime  DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public String Title { get; set; }
        public bool Status { get; set; }


        public List<object> GetProductCancelExtraListByConditionID(int intConditionID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT cancel_id,condition_id,date_start,date_end,title,status FROM tbl_product_option_condition_cancel_extra_net WHERE condition_id=@condition_id AND status = 1 ORDER BY date_end ", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionID;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public int InsertNewConditionCancel(int intProductId, int ConditionId, DateTime dDateStart, DateTime dDateEnd, string strTitle)
        {
            int ret = 0;
            int CancelId = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_option_condition_cancel_extra_net (condition_id,date_start,date_end,title) VALUES (@condition_id,@date_start,@date_end,@title);SET @cancel_id=SCOPE_IDENTITY();",cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = ConditionId;
                cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = dDateEnd;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = strTitle;
                cmd.Parameters.Add("@cancel_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
                CancelId = (int)cmd.Parameters["@cancel_id"].Value;
            }
            //#Staff_Activity_Log==========================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_Cancel_Extra, StaffLogActionType.Insert, StaffLogSection.Product,
                intProductId, "tbl_product_option_condition_cancel_extra_net", "condition_id,date_start,date_end,title",
                "cancel_id", CancelId);
            //============================================================================================================================


            return CancelId;
        }

        public bool UpdateConditionCancelExtraNet(int intProductId, int intCancelID, DateTime dDateStart, DateTime dDateEnd)
        {
            int ret = 0;

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_option_condition_cancel_extra_net", "date_start,date_end", "cancel_id", intCancelID);
            //============================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option_condition_cancel_extra_net SET date_start=@date_start, date_end=@date_end WHERE cancel_id=@cancel_id",cn);
                cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = dDateEnd;
                cmd.Parameters.Add("@cancel_id", SqlDbType.Int).Value = intCancelID;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }


            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_Cancel_Extra, StaffLogActionType.Update, StaffLogSection.Product, intProductId,
                "tbl_product_option_condition_cancel_extra_net", "date_start,date_end", arroldValue, "cancel_id", intCancelID);
            //==================================================================================================================== COMPLETED ========

            return (ret == 1);
        }

        public bool UpdateConditionCancelExtraNetStatus(int intProductId, int intCancelID, bool Status)
        {
            int ret = 0;

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_option_condition_cancel_extra_net", "status", "cancel_id", intCancelID);
            //============================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option_condition_cancel_extra_net SET status=@status WHERE cancel_id=@cancel_id", cn);

                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = Status;
                cmd.Parameters.Add("@cancel_id", SqlDbType.Int).Value = intCancelID;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }


            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_Cancel_Extra, StaffLogActionType.Update, StaffLogSection.Product, intProductId,
                "tbl_product_option_condition_cancel_extra_net", "status", arroldValue, "cancel_id", intCancelID);
            //==================================================================================================================== COMPLETED ========

            return (ret == 1);
        }

    }
}