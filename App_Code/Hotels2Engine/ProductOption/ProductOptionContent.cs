using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using Hotels2thailand.Staffs;
using Hotels2thailand.LinqProvider.Production;

/// <summary>
/// Summary description for ProductOptionContent
/// </summary>
/// 
namespace Hotels2thailand.ProductOption
{
    public class ProductOptionContent : Hotels2BaseClass
    {
        private LinqProductionDataContext dcProduct = new LinqProductionDataContext();

        public int OptionID { get; set; }
        public byte LanguageID { get; set; }
        public string Title { get; set; }
        public string TitleSecound { get; set; }
        public string Detail { get; set; }

        public int InsertOptionExtra(int intProductId, int intOptionId, byte bytLangId, string strTitle)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_option_content (option_id,lang_id,title) VALUES(@option_id,@lang_id,@title)", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strTitle;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }

            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_option_detail, StaffLogActionType.Insert, StaffLogSection.Product,
                intProductId, "tbl_product_option_content", "option_id,lang_id,title", "option_id,lang_id", intOptionId, bytLangId);
            //========================================================================================================================================================

            return ret;
        }

        public int InsertOptionContentExtra(int intProductId, int intOptionId, byte bytLangId, string strTitle, string strDetail)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_option_content (option_id,lang_id,title,detail) VALUES(@option_id,@lang_id,@title,@detail)", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strTitle;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = strDetail;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }

            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_option_detail, StaffLogActionType.Insert, StaffLogSection.Product,
                intProductId, "tbl_product_option_content", "option_id,lang_id,title,detail", "option_id,lang_id", intOptionId, bytLangId);
            //========================================================================================================================================================

            return ret;
        }

        public int Insert(ProductOptionContent content)
        {
            tbl_product_option_content productOptionContent = new tbl_product_option_content
            {
                option_id = content.OptionID,
                lang_id = content.LanguageID,
                title = content.Title,
                title_secound = content.TitleSecound,
                detail = content.Detail,
            };
            dcProduct.tbl_product_option_contents.InsertOnSubmit(productOptionContent);
            dcProduct.SubmitChanges();
            int ret = productOptionContent.option_id;
            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_option_detail, StaffLogActionType.Insert, StaffLogSection.Product,
                ProductId, "tbl_product_option_content", "option_id,lang_id,title,title_secound,detail", "option_id,lang_id", content.OptionID, content.LanguageID);
            //========================================================================================================================================================
            return ret;
        }

        public bool UpdateOptionContentLangExtranet(int intProductId, int intOptionId, byte bytLangId, string strTitle, string strDetail)
        {
            int ret = 0;
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_option_content", "title,title_secound,detail", "option_id,lang_id", intOptionId, bytLangId);
            //============================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option_content SET title=@title,  detail=@detail WHERE option_id=@option_id AND lang_id=@lang_id", cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strTitle;

                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = strDetail;
                cn.Open();
                ret = ExecuteNonQuery(cmd);

            }
            
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_option_detail, StaffLogActionType.Update, StaffLogSection.Product,
                intProductId, "tbl_product_option_content", "title,title_secound,detail", arroldValue, "option_id,lang_id", intOptionId, bytLangId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }

        public bool UpdateOptionContentLang(int intOptionId, byte bytLangId, string strTitle,  string strDetail)
        {
            int ret = 0;
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_option_content", "title,title_secound,detail", "option_id,lang_id", intOptionId, bytLangId);
            //============================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_option_content SET title=@title,  detail=@detail WHERE option_id=@option_id AND lang_id=@lang_id",cn);
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strTitle;

                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = strDetail;
                cn.Open();
               ret =  ExecuteNonQuery(cmd);
                
            }
            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_option_detail, StaffLogActionType.Update, StaffLogSection.Product,
                ProductId, "tbl_product_option_content", "title,title_secound,detail", arroldValue, "option_id,lang_id", intOptionId, bytLangId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }
        //public bool UpdateOptionContentLang(ProductOptionContent content)
        //{
        //    tbl_product_option_content RsProductContent = dcProduct.tbl_product_option_contents.Single(poc => poc.option_id == content.OptionID && poc.lang_id == content.LanguageID);
            
        //    //.ActionUpdateMethodStaff_log_FirstStep(RsProductContent.title, RsProductContent.title_secound, RsProductContent.detail);
        //    RsProductContent.title = content.Title;
        //    RsProductContent.title_secound = content.TitleSecound;
        //    RsProductContent.detail = content.Detail;
        //    dcProduct.SubmitChanges();

        //    dcProduct.SubmitChanges();
            
        //    return true;
            
            
        //}

        //public bool Update( )
        //{
        //    return UpdateOptionContent(this.OptionID, this.LanguageID, this.Title, this.TitleSecound, this.Detail);
        //}
        //public static bool UpdateOptionContent(int intOptionId, byte bytLangId, string strTitle, string strTitleSec, string strDetail)
        //{
        //    ProductOptionContent cOptionContent = new ProductOptionContent
        //    {
        //        OptionID = intOptionId,
        //        LanguageID = bytLangId,
        //        Title = strTitle,
        //        TitleSecound = strTitleSec,
        //        Detail = strDetail
        //    };
        //    return cOptionContent.UpdateOptionContentLang(cOptionContent);
        //}

        public List<object> GetProductOptionContentAll()
        {
            var result = from item in dcProduct.tbl_product_option_contents
                         select item;

            return MappingObjectFromDataContextCollection(result);
        }

        public ProductOptionContent GetProductOptionContentById(int ProductOptionId, byte LanguageID)
        {
            var result = dcProduct.tbl_product_option_contents.SingleOrDefault(poc => poc.option_id == ProductOptionId && poc.lang_id == LanguageID);
            if (result != null)
            {
                return (ProductOptionContent)MappingObjectFromDataContext(result);
            }
            else
            {
                return null;
            }
            
        }

    }
}