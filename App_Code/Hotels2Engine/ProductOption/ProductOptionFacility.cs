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
/// Summary description for ProductOptionFacility
/// </summary>
/// 
namespace Hotels2thailand.ProductOption
{
    public class ProductOptionFacility : Hotels2BaseClass
    {
        public ProductOptionFacility()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int FacilityId { get; set; }
        public int OptionId { get; set; }
        public byte LangId { get; set; }
        public string Title { get; set; }

        private LinqProductionDataContext dcOption = new LinqProductionDataContext();

        public ProductOptionFacility getOptionFacilityById(int intFacId)
        {
            var Result = dcOption.tbl_product_option_facilities.SingleOrDefault(of => of.fac_id == intFacId);
            return (ProductOptionFacility)MappingObjectFromDataContext(Result);
        }

        public List<object> getOptionFacilityByOptionId(int intOptionId, byte bytLangId)
        {
            var Result = from of in dcOption.tbl_product_option_facilities
                         where of.option_id == intOptionId && of.lang_id == bytLangId
                         orderby of.title
                         select of;

            return MappingObjectFromDataContextCollection(Result);
        }

        public int InsertNewOptionFacility_Extra(ProductOptionFacility cOptionFacility, int intProductId)
        {
            var Insert = new tbl_product_option_facility
            {
                option_id = cOptionFacility.OptionId,
                lang_id = cOptionFacility.LangId,
                title = cOptionFacility.Title
            };

            dcOption.tbl_product_option_facilities.InsertOnSubmit(Insert);
            dcOption.SubmitChanges();
           // int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_option_detail, StaffLogActionType.Insert, StaffLogSection.Product,
                intProductId, "tbl_product_option_facility", "option_id,lang_id,title", "option_id,lang_id", cOptionFacility.OptionId, cOptionFacility.LangId);
            //========================================================================================================================================================
            return Insert.fac_id;
        }

        public int InsertNewOptionFacility(ProductOptionFacility cOptionFacility)
        {
            var Insert = new tbl_product_option_facility
            {
                option_id = cOptionFacility.OptionId,
                lang_id = cOptionFacility.LangId,
                title = cOptionFacility.Title
            };

            dcOption.tbl_product_option_facilities.InsertOnSubmit(Insert);
            dcOption.SubmitChanges();
            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_option_detail, StaffLogActionType.Insert, StaffLogSection.Product,
                ProductId, "tbl_product_option_facility", "option_id,lang_id,title", "option_id,lang_id", cOptionFacility.OptionId, cOptionFacility.LangId);
            //========================================================================================================================================================
            return Insert.fac_id;
        }

        public static int InsertOptionFacility(int intOptionId, byte bytLangId, string strTitle)
        {
            ProductOptionFacility cInsert = new ProductOptionFacility
            {
                OptionId = intOptionId,
                LangId = bytLangId,
                Title = strTitle
            };


            return cInsert.InsertNewOptionFacility(cInsert);
        }

        public bool UpdateOptionFacility(ProductOptionFacility cOptionFacility)
        {
            var update = dcOption.tbl_product_option_facilities.SingleOrDefault(of => of.fac_id == cOptionFacility.FacilityId);
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep(update.title);
            //============================================================================================================================
            update.title = cOptionFacility.Title;

            dcOption.SubmitChanges();

            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_option_detail, StaffLogActionType.Update, StaffLogSection.Product, ProductId,
                "tbl_product_option_facility", "title", arroldValue, "fac_id", cOptionFacility.FacilityId);
            //==================================================================================================================== COMPLETED ========
            return true;
        }
        public bool Update()
        {
            return UpdateOptionFac(this.FacilityId, this.Title);
        }

        public bool Delete()
        {
            return DeleteOptionFaci(this.FacilityId);
        }

        public static bool UpdateOptionFac(int intFacId, string strTitle)
        {
            ProductOptionFacility cUpdate = new ProductOptionFacility
            {
                FacilityId = intFacId,
                Title = strTitle
            };

            return cUpdate.UpdateOptionFacility(cUpdate);
        }


        public bool DeleteFacByOptionId(int intOptionID, byte bytLangId)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tbl_product_option_facility WHERE option_id= @option_id AND lang_id=@lang_id", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionID;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }
            return (ret == 1);
        }
        public static bool DeleteOptionFaci(int intFacId)
        {
            ProductOptionFacility cDel = new ProductOptionFacility
            {
                FacilityId = intFacId,
            };
            return cDel.DeleteOptionFacility(cDel);
        }



        public bool DeleteOptionFacility(ProductOptionFacility cOptionFacility)
        {
            IList<object[]> arroldValue = null;


            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            arroldValue = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_product_option_facility", "fac_id", cOptionFacility.FacilityId);
            //============================================================================================================================

            var delete = dcOption.tbl_product_option_facilities.SingleOrDefault(of => of.fac_id == cOptionFacility.FacilityId);
            dcOption.tbl_product_option_facilities.DeleteOnSubmit(delete);
            dcOption.SubmitChanges();
            
            //#Staff_Activity_Log================================================================================================ STEP 2 ==
            StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Product_option_detail, StaffLogActionType.Delete, StaffLogSection.Product, int.Parse(HttpContext.Current.Request.QueryString["pid"]),
                "tbl_product_option_facility", arroldValue, "fac_id", cOptionFacility.FacilityId);
            //============================================================================================================================
            return true;
        }





    }
}