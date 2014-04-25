using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Production;
using Hotels2thailand.Staffs;

/// <summary>
/// Summary description for Annoucement
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class ProductMinimumStay : Hotels2BaseClass
    {
        private LinqProductionDataContext dcProduct = new LinqProductionDataContext();

        public int MinimumStayId { get; set; }
        public int ProductID { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public byte MiniTotal { get; set; }
        public bool Status { get; set; }
        

        public List<object> getListMiniListByProductId(int intProductID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT mini_stay_id,product_id,date_start,date_end,num_minimum,status FROM tbl_product_minimum_stay WHERE product_id=@product_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductID;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }

        }

        public int InsertNewMinimumStay(int ProductId, DateTime dDateStart , DateTime dDateEnd, byte bytMiniTotal)
        {
            int ret = 0 ;
            int miniId = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_minimum_stay (product_id,date_start,date_end,num_minimum)VALUES(@product_id,@date_start,@date_end,@num_minimum);SET @mini_stay_id =SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = ProductId;
                cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = dDateEnd;
                cmd.Parameters.Add("@num_minimum", SqlDbType.TinyInt).Value = bytMiniTotal;
                cmd.Parameters.Add("@mini_stay_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
                miniId = (int)cmd.Parameters["@mini_stay_id"].Value;
                
            }

            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_minimumstay, StaffLogActionType.Insert, StaffLogSection.Product,
                ProductId, "tbl_product_minimum_stay", "product_id,date_start,date_end,num_minimum", "mini_stay_id", miniId);
            //========================================================================================================================================================
            return ret;
            
        }

        public  bool UpdateMiniStay(int bytMiniStayID, DateTime dDateStart, DateTime dDateEnd, byte bytMiniTotal)
        {
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_minimum_stay", "date_start,date_end,num_minimum", "mini_stay_id", bytMiniStayID);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_minimum_stay SET date_start= @date_start,date_end=@date_end, num_minimum=@num_minimum WHERE mini_stay_id=@mini_stay_id", cn);
                cmd.Parameters.Add("@mini_stay_id", SqlDbType.Int).Value = bytMiniStayID;
                cmd.Parameters.Add("@date_start", SqlDbType.SmallDateTime).Value = dDateStart;
                cmd.Parameters.Add("@date_end", SqlDbType.SmallDateTime).Value = dDateEnd;
                cmd.Parameters.Add("@num_minimum", SqlDbType.TinyInt).Value = bytMiniTotal;

                cn.Open();
                ret = ExecuteNonQuery(cmd);
                
            }
            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_minimumstay, StaffLogActionType.Update, StaffLogSection.Product, ProductId,
                "tbl_product_minimum_stay", "date_start,date_end,num_minimum", arroldValue, "mini_stay_id", ProductId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }

        public bool UpdateMiniStayStatus(int bytMiniStayID, bool bolStatus)
        {
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_minimum_stay", "status", "mini_stay_id", bytMiniStayID);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_minimum_stay SET status= @status WHERE mini_stay_id=@mini_stay_id", cn);
                cmd.Parameters.Add("@mini_stay_id", SqlDbType.Int).Value = bytMiniStayID;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
                

                cn.Open();
                ret = ExecuteNonQuery(cmd);

            }
            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_minimumstay, StaffLogActionType.Update, StaffLogSection.Product, ProductId,
                "tbl_product_minimum_stay", "status", arroldValue, "mini_stay_id", ProductId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }

        public ProductMinimumStay getMinimumById(int bytMiniId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT mini_stay_id,product_id,date_start,date_end,num_minimum FROM tbl_product_minimum_stay WHERE mini_stay_id=@mini_stay_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = bytMiniId;
                cn.Open();

                IDataReader reader = ExecuteReader(cmd);
                if (reader.Read())
                {
                    return (ProductMinimumStay)MappingObjectFromDataReader(reader);
                }
                else
                {
                    return null;
                }
                
            }
            
        }
        

        

        

        

        
    }
}