using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using Hotels2thailand.Staffs;
/// <summary>
/// Summary description for ProductFacility
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class ProductFacilitytempalte : Hotels2BaseClass
    {
        public ProductFacilitytempalte()
        {
            
        }

        public int Fac_id { get; set; }
        public byte CatID { get; set; }
        public string  TitleEn { get; set; }
        public string TitleTh { get; set; }
        public string TitleShow
        {
            get 
            {
                return this.TitleEn + ":" + this.TitleTh;
            
            }
        }
        public string TitleVal
        {
            get
            {
                return this.TitleEn + "%" + this.TitleTh;

            }
        }

        public List<object> getFacilityByCatId(byte bytCatId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT fac_id,cat_id,title_en,title_th FROM tbl_facility_product_template WHERE cat_id=@cat_id ORDER BY title_en", cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCatId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
         }


        public int InsertNewFacTemplate(byte bytCatId, string strTitleEn, string strTitleTh)
        {

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_facility_product_template (cat_id,title_en,title_th)VALUES(@cat_id,@title_en,@title_th); SET @fac_id=SCOPE_IDENTITY();", cn);

                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCatId;
                cmd.Parameters.Add("@title_en", SqlDbType.VarChar).Value = strTitleEn;
                cmd.Parameters.Add("@title_th", SqlDbType.NVarChar).Value = strTitleTh;
                cmd.Parameters.Add("@fac_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                 int ret  = ExecuteNonQuery(cmd);
                 int facId = (int)cmd.Parameters["@fac_id"].Value;
                //=== STAFF ACTIVITY ================================================================================================================================================================================================
                 StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Central_Data_FacilityTemplate, StaffLogActionType.Insert, StaffLogSection.NULL,
                    null, "tbl_facility_product_template", "cat_id,title_en,title_th", "fac_id", facId);
                //===================================================================================================================================================================================================
                return ret;
            }
        }





        public bool UpdateFacility(int intFacId, string strTitleEn, string strTitleTh)
        {

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_facility_product_template", "title_en,title_th",
                "fac_id", intFacId);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_facility_product_template SET title_en=@title_en ,  title_th=@title_th WHERE fac_id = @fac_id", cn);
                cmd.Parameters.Add("@fac_id", SqlDbType.Int).Value = intFacId;
                cmd.Parameters.Add("@title_en", SqlDbType.VarChar).Value = strTitleEn;
                cmd.Parameters.Add("@title_th", SqlDbType.NVarChar).Value = strTitleTh;
                cn.Open();
               ret = ExecuteNonQuery(cmd);
                
            }


            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Central_Data_FacilityTemplate, StaffLogActionType.Update, StaffLogSection.NULL, null,
                "tbl_facility_product_template", "title_en,title_th", arroldValue, "fac_id", intFacId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }

        public bool DeleteFac(int FacId)
        {
            IList<object[]> arroldValue = null;

            int ret = 0;
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            arroldValue = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_facility_product_template", "fac_id", FacId);
            //============================================================================================================================
            ProductFacility cFac  = new ProductFacility();
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tbl_facility_product_template WHERE fac_id = @fac_id", cn);
                cmd.Parameters.Add("@fac_id", SqlDbType.Int).Value = FacId;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
                
            }

            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //#Staff_Activity_Log================================================================================================ STEP 2 ==
            StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Central_Data_FacilityTemplate, StaffLogActionType.Delete, StaffLogSection.NULL, null,
                "tbl_facility_product_template", arroldValue, "fac_id", FacId);
            //============================================================================================================================

            return (ret == 1);
        }


    }
}