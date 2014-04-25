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
/// Summary description for ProductPicCategory
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class ProductPicName : Hotels2BaseClass
    {
        private LinqProductionDataContext dcProduct = new LinqProductionDataContext();

        public int PicID { get; set; }
        public byte LangId { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }


        public ProductPicName getProductPicNameByIdAndLang(int intPicId, byte bytLangId)
        {
            var Result = dcProduct.tbl_product_pic_contents.SingleOrDefault(pc => pc.pic_id == intPicId && pc.lang_id == bytLangId);
            if (Result == null)
            {
                return null;
            }
            else
            {
                return (ProductPicName)MappingObjectFromDataContext(Result);
            }
        }

        public List<object> getProductPicNameBypicId(int intPicId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_pic_content WHERE pic_id=@pic_id", cn);
                cmd.Parameters.Add("@pic_id", SqlDbType.Int).Value = intPicId;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public int insertNewProductPicContent(ProductPicName cProductPicContent)
        {
            var insert = new tbl_product_pic_content
            {
                pic_id = cProductPicContent.PicID,
                lang_id = cProductPicContent.LangId,
                title = cProductPicContent.Title,
                detail = cProductPicContent.Detail
            };

            dcProduct.tbl_product_pic_contents.InsertOnSubmit(insert);
            dcProduct.SubmitChanges();

            int ret = insert.pic_id;
            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_picture, StaffLogActionType.Insert, StaffLogSection.Product,
                ProductId, "tbl_product_pic_content", "pic_id,lang_id,title,detail", "pic_id,lang_id", cProductPicContent.PicID, cProductPicContent.LangId);
            //========================================================================================================================================================
            return ret;
        }

        public int insertNewProductPicContent(int intPidId, byte bytLandId, string strTitle)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_pic_content (pic_id,lang_id,title) VALUES (@pic_id,@lang_id,@title)", cn);
                cmd.Parameters.Add("@pic_id", SqlDbType.Int).Value = intPidId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLandId;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strTitle;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
                
            }
            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_picture, StaffLogActionType.Insert, StaffLogSection.Product,
                ProductId, "tbl_product_pic_content", "pic_id,lang_id,title", "pic_id,lang_id", intPidId, bytLandId);
            //========================================================================================================================================================
            return ret;
        }

        public bool Update()
        {
            return ProductPicName.UpdateProductPictureContent(this.PicID, this.LangId, this.Title, this.Detail);
        }
        public bool UpdateProductPicContent(ProductPicName cProductPicContent)
        {
            var update = dcProduct.tbl_product_pic_contents.SingleOrDefault(pc => pc.pic_id == cProductPicContent.PicID && pc.lang_id == cProductPicContent.LangId);
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep(update.title, update.detail);
            //============================================================================================================================
            if (update != null)
            {
                update.title = cProductPicContent.Title;
                update.detail = cProductPicContent.Detail;
                dcProduct.SubmitChanges();
                int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
                //#Staff_Activity_Log================================================================================================ STEP 2 ============
                StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_picture, StaffLogActionType.Update, StaffLogSection.Product,
                    ProductId, "tbl_product_pic_content", "title,detail", arroldValue, "pic_id,lang_id", cProductPicContent.PicID, cProductPicContent.LangId);
                //==================================================================================================================== COMPLETED ========
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public static bool UpdateProductPictureContent(int intPicId, byte bytLangId, string strTitle, string strDetail)
        {
            ProductPicName cProductPicName = new ProductPicName
            {
                PicID = intPicId,
                LangId = bytLangId,
                Title = strTitle,
                Detail = strDetail
            };
            return cProductPicName.UpdateProductPicContent(cProductPicName);
        }

    }
}