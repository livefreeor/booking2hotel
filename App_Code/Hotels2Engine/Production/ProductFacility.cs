using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Production;
using Hotels2thailand.Staffs;

/// <summary>
/// Summary description for ProductFacility
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class ProductFacility : Hotels2BaseClass
    {
        public ProductFacility()
        {
            
        }

        private LinqProductionDataContext dcProduct = new LinqProductionDataContext();

        public int Fac_id { get; set; }
        public int ProductId { get; set; }
        public byte LangId { get; set; }
        public string  Title { get; set; }
        

        public List<object> getFacilityByProductId(int intProductId, byte bytLangId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT fac_id,product_id,lang_id,title FROM tbl_facility_product WHERE product_id=@product_id AND lang_id=@lang_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
               
            }
         }

        
        public static  int InsertNewFac(int intProductId, byte bytLangId, string strTitle)
        {
            ProductFacility cFac = new ProductFacility();
            using (SqlConnection cn = new SqlConnection(cFac.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_facility_product (product_id,lang_id,title)VALUES(@product_id,@lang_id,@title); SET @fac_id=SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strTitle;
                cmd.Parameters.Add("@fac_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                 int ret  = cFac.ExecuteNonQuery(cmd);
                 int facId = (int)cmd.Parameters["@fac_id"].Value;
                //=== STAFF ACTIVITY ================================================================================================================================================================================================
                StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_detail, StaffLogActionType.Insert, StaffLogSection.Product,
                    intProductId, "tbl_facility_product", "product_id,lang_id,title", "fac_id", facId);
                //===================================================================================================================================================================================================
                return ret;
            }
        }

        

        

        public static bool UpdateFacility(int intFacId, byte langId,  string strValue)
        {
            ProductFacility cFac = new ProductFacility();
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_facility_product", "title,lang_id", "fac_id,lang_id", intFacId, langId);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(cFac.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_facility_product SET title=@title WHERE fac_id = @fac_id AND lang_id = @lang_id", cn);
                cmd.Parameters.Add("@fac_id", SqlDbType.Int).Value = intFacId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = langId;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strValue;
                cn.Open();
               ret = cFac.ExecuteNonQuery(cmd);
                
            }

            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_detail, StaffLogActionType.Update, StaffLogSection.Product, ProductId,
                "tbl_facility_product", "title,lang_id", arroldValue, "fac_id,lang_id", intFacId, langId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }

        public static bool DeleteFac(int FacId)
        {
            IList<object[]> arroldValue = null;

            int ret = 0;
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            arroldValue = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_facility_product", "fac_id", FacId);
            //============================================================================================================================
            ProductFacility cFac  = new ProductFacility();
            using (SqlConnection cn = new SqlConnection(cFac.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tbl_facility_product WHERE fac_id = @fac_id",cn);
                cmd.Parameters.Add("@fac_id", SqlDbType.Int).Value = FacId;
                cn.Open();
                ret = cFac.ExecuteNonQuery(cmd);
                
                
            }
            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //#Staff_Activity_Log================================================================================================ STEP 2 ==
            StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Product_detail, StaffLogActionType.Delete, StaffLogSection.Product,ProductId,
                "tbl_facility_product" ,arroldValue, "fac_id",FacId);
            //============================================================================================================================

            return (ret == 1);
        }


    }
}