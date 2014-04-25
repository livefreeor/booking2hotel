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
    public class ProductConditionContentExtra : Hotels2BaseClass
    {
        public int ContentId { get; set; }
        public int ConditionId { get; set; }
        public byte LangId { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public bool Status { get; set; }


        public List<object> GetListConditionDetail_policyByConditionId(int intConditionId, byte bytlangId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT content_id,condition_id,lang_id,title,detail,status FROM tbl_product_option_condition_content_extra_net WHERE condition_id=@condition_id AND lang_id=@lang_id AND status=1", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytlangId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public int insertConditionDetail(int intProductId, int intConditionId, byte bytLangId, string strTitle, string strDetail)
        {
            int ret = 0;
            int ContentID = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_option_condition_content_extra_net (condition_id,lang_id,title,detail) VALUES (@condition_id,@lang_id,@title,@detail);SET @content_id=SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strTitle;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = strDetail;
                cmd.Parameters.Add("@content_id", SqlDbType.Int).Direction = ParameterDirection.Output;

                cn.Open();

                ret = ExecuteNonQuery(cmd);
                ContentID = (int)cmd.Parameters["@content_id"].Value;
              }

            //#Staff_Activity_Log==========================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_Condition_Extra, StaffLogActionType.Insert, StaffLogSection.Product,
                intProductId, "tbl_product_option_condition_content_extra_net", "condition_id,lang_id,title,detail",
                "content_id", ContentID);
            //============================================================================================================================
            return ConditionId;
        }

        public bool UpdateConditionStatusExtra(int intProductId, int intContentId, bool bolStatus)
        {
            int ret = 0;

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_option_condition_content_extra_net", "status", "content_id", intContentId);
            //============================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option_condition_content_extra_net SET status=@status WHERE content_id = @content_id", cn);
                cmd.Parameters.Add("@content_id", SqlDbType.Int).Value = intContentId;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
              

                cn.Open();

                ret = ExecuteNonQuery(cmd);
            }


            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_Condition_Extra, StaffLogActionType.Update, StaffLogSection.Product, intProductId,
                "tbl_product_option_condition_content_extra_net", "status", arroldValue, "content_id", intContentId);
            //==================================================================================================================== COMPLETED ========

            return (ret == 1);
        }

        public bool UpdateConditionExtra(int intProductId, int intContentId, string Title,  string strDetail)
        {
            int ret = 0;
            
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_option_condition_content_extra_net", "title,detail", "content_id", intContentId);
            //============================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option_condition_content_extra_net SET title=@title , detail=@detail WHERE content_id = @content_id", cn);
                cmd.Parameters.Add("@content_id", SqlDbType.Int).Value = intContentId;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = Title;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = strDetail;
                
                cn.Open();

                ret = ExecuteNonQuery(cmd);
            }


            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_Condition_Extra, StaffLogActionType.Update, StaffLogSection.Product, intProductId,
                "tbl_product_option_condition_content_extra_net", "title,detail", arroldValue, "content_id", intContentId);
            //==================================================================================================================== COMPLETED ========

            return (ret == 1);
        }

        public bool DelConditionPolicyExtra(int intProductId, int intContentId)
        {
            IList<object[]> arroldValue = null;
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            arroldValue = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_product_option_condition_content_extra_net", "content_id", intContentId);
            //============================================================================================================================
            
            


            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM  tbl_product_option_condition_content_extra_net WHERE content_id = @content_id", cn);
                cmd.Parameters.Add("@content_id", SqlDbType.Int).Value = intContentId;
                
                cn.Open();

                ret = ExecuteNonQuery(cmd);
            }


            //#Staff_Activity_Log================================================================================================ STEP 2 ==
            StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Product_Condition_Extra, StaffLogActionType.Delete, StaffLogSection.Product, intProductId,
                "tbl_product_option_condition_content_extra_net", arroldValue, "content_id", intContentId);
            //============================================================================================================================
            return (ret == 1);
        }
    }
}