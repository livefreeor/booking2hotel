using System;
using System.Data;
using System.Data.SqlClient; 
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Production;
using Hotels2thailand.Production;
using Hotels2thailand.Staffs;


/// <summary>
/// Summary description for ProductOptionCondition
/// </summary>
/// 
namespace Hotels2thailand.ProductOption
{
    public class ProductOptionConditionPolicy : Hotels2BaseClass
    {
        public int PolicyId { get; set; }
        public int ConditionId { get; set; }
        
        public string Title { get; set; }
        public DateTime Datestart { get; set; }
        public DateTime DateEnd { get; set; }
        public bool Status { get; set; }


        //======================== OPTION CONDITION POLICY ============================
        //tbl_product_option_condition_policy


        public List<object> getOptionContionPolicyByConditionId(int intConditionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT cp.policy_id, cp.condition_id,  pc.title, pc.date_start, pc.date_end, pc.status FROM tbl_product_option_condition_policy cp, tbl_product_policy pc WHERE cp.policy_id = pc.policy_id AND cp.condition_id = @condition_id", cn);
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intConditionId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        //public static int[] GetPolicyListbyConditionId(int intConditionId)
        //{
           
        //   LinqProductionDataContext dcOption = new LinqProductionDataContext();
        //   var Result = from pc in dcOption.tbl_product_option_condition_policies
        //                where pc.condition_id == intConditionId
        //                select pc.policy_id;
                        
        //   return Result.ToArray();

        //}

        public static int InsertOptionConditionPolicy(int intConditionId, int intPolidyId)
        {
            LinqProductionDataContext dcOption = new LinqProductionDataContext();
            
            int intInsert = dcOption.ExecuteCommand("INSERT INTO tbl_product_option_condition_policy (condition_id,policy_id) VALUES({0},{1})", intConditionId, intPolidyId);
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_policy, StaffLogActionType.Insert, StaffLogSection.Product,
                int.Parse(HttpContext.Current.Request.QueryString["pid"]), "tbl_product_option_condition_policy", "condition_id,policy_id", "condition_id,policy_id", intConditionId, intPolidyId);
            //========================================================================================================================================================
            return intInsert;
        }


        public static bool DeleteOptionConditionPolicy(int intConditionId, int intPolidyId)
        {
            LinqProductionDataContext dcOption = new LinqProductionDataContext();
            IList<object[]> arroldValue = null;
            

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            arroldValue = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_product_option_condition_policy", "condition_id,policy_id", intConditionId, intPolidyId);
            //============================================================================================================================
            int intInsert = dcOption.ExecuteCommand("DELETE FROM tbl_product_option_condition_policy WHERE condition_id={0} AND policy_id={1}", intConditionId, intPolidyId);

            //#Staff_Activity_Log================================================================================================ STEP 2 ==
            StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Product_policy, StaffLogActionType.Delete, StaffLogSection.Product, int.Parse(HttpContext.Current.Request.QueryString["pid"]),
                "tbl_product_option_condition_policy", arroldValue, "condition_id,policy_id", intConditionId, intPolidyId);
            //============================================================================================================================
            return (intInsert==1);
        }

    }
}