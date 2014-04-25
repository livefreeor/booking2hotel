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
/// Summary description for ProductPicCategory
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class ProductPic : Hotels2BaseClass
    {
        private LinqProductionDataContext dcProduct = new LinqProductionDataContext();

        public int PicID { get; set; }
        public byte PicCategoryID { get; set; }
        public byte PicTypeID { get; set; }
        public int ProductID { get; set; }
        public System.Nullable<int> OptionID { get; set; }
        public System.Nullable<int> ConstructionID { get; set; }
        public System.Nullable<int> ItineraryID { get; set; }
        public string PicCode { get; set; }
        public string Title { get; set; }
        public byte Priority { get; set; }
        public bool Status { get; set; }


        public string getProductlogo(int intProductId)
        {
            string result = "";
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 pic_code FROm tbl_product_pic WHERe cat_id = 1 ANd type_id = 9 AND status = 1 AND product_id = @product_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                if (reader.Read())
                    result = reader[0].ToString();
            }

            return "http://www.booking2hotels.com" + result.Trim();
        }

        public List<object> GetProductPicByProductID(int ProductID)
        {
            var picture = from item in dcProduct.tbl_product_pics
                                      where item.product_id==ProductID
                                      select item;
            return MappingObjectFromDataContextCollection(picture);
        }

        public ProductPic GetProductPicByID(int PicID)
        {
            var Result = dcProduct.tbl_product_pics.SingleOrDefault(p => p.pic_id == PicID);
            if (Result == null)
            {
                return null;
            }
            else
            {
                return (ProductPic)MappingObjectFromDataContext(Result);
            }
        }

        public List<object> getProductPicList(byte bytCatId, byte bytTypeId, int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_pic WHERE cat_id=@cat_id AND type_id=@type_id AND product_id=@product_id  ORDER BY priority", cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCatId;
                cmd.Parameters.Add("@type_id", SqlDbType.TinyInt).Value = bytTypeId;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

       
        public List<object> getProductPicList(byte bytCatId, byte bytTypeId, int intProductId, int intOptionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_pic WHERE cat_id=@cat_id AND type_id=@type_id AND product_id=@product_id AND option_id=@option_id ORDER BY priority", cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCatId;
                cmd.Parameters.Add("@type_id", SqlDbType.TinyInt).Value = bytTypeId;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        

        public List<object> getProductPicListConstruction(byte bytCatId, byte bytTypeId, int intProductId, int intConstructionId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_pic WHERE cat_id=@cat_id AND type_id=@type_id AND product_id=@product_id AND construction_id=@construction_id ORDER BY priority", cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCatId;
                cmd.Parameters.Add("@type_id", SqlDbType.TinyInt).Value = bytTypeId;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@construction_id", SqlDbType.Int).Value = intConstructionId;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> getProductPicListItinerary(byte bytCatId, byte bytTypeId, int intProductId, int intItinerary)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_pic WHERE cat_id=@cat_id AND type_id=@type_id AND product_id=@product_id AND itinerary_id=@itinerary_id ORDER BY priority", cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCatId;
                cmd.Parameters.Add("@type_id", SqlDbType.TinyInt).Value = bytTypeId;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@itinerary_id", SqlDbType.Int).Value = intItinerary;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
        
       

        

        public int InsertNewPicProduct(byte CatId, byte bytTypeId, int intProductId, string strPicCode, string strTitle, byte bytPriority)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_pic (cat_id,type_id,product_id,pic_code,title,priority) VALUES(@cat_id,@type_id,@product_id,@pic_code,@title,@priority); SET @pic_id=SCOPE_IDENTITY()", cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = CatId;
                cmd.Parameters.Add("@type_id", SqlDbType.TinyInt).Value = bytTypeId;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@pic_code", SqlDbType.VarChar).Value = strPicCode;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = strTitle;
                cmd.Parameters.Add("@priority", SqlDbType.TinyInt).Value = bytPriority;
                cmd.Parameters.Add("@pic_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                ExecuteNonQuery(cmd);
                ret = (int)cmd.Parameters["@pic_id"].Value;
            }

            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_picture, StaffLogActionType.Insert, StaffLogSection.Product,
                intProductId, "tbl_product_pic", "cat_id,type_id,product_id,pic_code,title,priority", "pic_id", ret);
            //========================================================================================================================================================
            return ret;
        }
        public int InsertNewPicOption(byte CatId, byte bytTypeId, int intProductId, int intOptionId, string strPicCode, string strTitle, byte bytPriority)
        {
            int ret = 0;

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_pic (cat_id,type_id,product_id,option_id,pic_code,title,priority) VALUES(@cat_id,@type_id,@product_id,@option_id,@pic_code,@title,@priority); SET @pic_id=SCOPE_IDENTITY()", cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = CatId;
                cmd.Parameters.Add("@type_id", SqlDbType.TinyInt).Value = bytTypeId;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@option_id", SqlDbType.Int).Value = intOptionId;
                cmd.Parameters.Add("@pic_code", SqlDbType.VarChar).Value = strPicCode;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = strTitle;
                cmd.Parameters.Add("@priority", SqlDbType.TinyInt).Value = bytPriority;
                cmd.Parameters.Add("@pic_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                ExecuteNonQuery(cmd);
                ret = (int)cmd.Parameters["@pic_id"].Value;
                
            }

            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_picture, StaffLogActionType.Insert, StaffLogSection.Product,
                intProductId, "tbl_product_pic", "cat_id,type_id,product_id,option_id,pic_code,title,priority", "pic_id", ret);
            //========================================================================================================================================================
            return ret;
        }
        public int InsertNewPicConstruction(byte CatId, byte bytTypeId, int intProductId, int intConTructionId, string strPicCode, string strTitle, byte bytPriority)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_pic (cat_id,type_id,product_id,construction_id,pic_code,title,priority) VALUES(@cat_id,@type_id,@product_id,@construction_id,@pic_code,@title,@priority); SET @pic_id=SCOPE_IDENTITY()", cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = CatId;
                cmd.Parameters.Add("@type_id", SqlDbType.TinyInt).Value = bytTypeId;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@construction_id", SqlDbType.Int).Value = intConTructionId;
                cmd.Parameters.Add("@pic_code", SqlDbType.VarChar).Value = strPicCode;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = strTitle;
                cmd.Parameters.Add("@priority", SqlDbType.TinyInt).Value = bytPriority;
                cmd.Parameters.Add("@pic_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                ExecuteNonQuery(cmd);
                ret = (int)cmd.Parameters["@pic_id"].Value;
                
            }
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_picture, StaffLogActionType.Insert, StaffLogSection.Product,
                intProductId, "tbl_product_pic", "cat_id,type_id,product_id,construction_id,pic_code,title,priority", "pic_id", ret);
            //========================================================================================================================================================
            return ret;
        }

        public int InsertNewPicItinerary(byte CatId, byte bytTypeId, int intProductId, int ItineraryId, string strPicCode, string strTitle, byte bytPriority)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_pic (cat_id,type_id,product_id,itinerary_id,pic_code,title,priority) VALUES(@cat_id,@type_id,@product_id,@itinerary_id,@pic_code,@title,@priority); SET @pic_id=SCOPE_IDENTITY()", cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = CatId;
                cmd.Parameters.Add("@type_id", SqlDbType.TinyInt).Value = bytTypeId;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@itinerary_id", SqlDbType.Int).Value = ItineraryId;
                cmd.Parameters.Add("@pic_code", SqlDbType.VarChar).Value = strPicCode;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = strTitle;
                cmd.Parameters.Add("@priority", SqlDbType.TinyInt).Value = bytPriority;
                cmd.Parameters.Add("@pic_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                ExecuteNonQuery(cmd);
                ret = (int)cmd.Parameters["@pic_id"].Value;
                
            }
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_picture, StaffLogActionType.Insert, StaffLogSection.Product,
                intProductId, "tbl_product_pic", "cat_id,type_id,product_id,itinerary_id,pic_code,title,priority", "pic_id", ret);
            //========================================================================================================================================================
            return ret;
        }

        public bool UpdatePicture(ProductPic picture)
        {
            tbl_product_pic rsPicture = dcProduct.tbl_product_pics.Single(pp=>pp.pic_id==picture.PicID);

            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep(rsPicture.cat_id, rsPicture.type_id, rsPicture.pic_code, rsPicture.title, rsPicture.priority, rsPicture.status);
            //============================================================================================================================
                rsPicture.cat_id=picture.PicCategoryID;
                rsPicture.type_id = picture.PicTypeID;
                rsPicture.pic_code = picture.PicCode;
                rsPicture.title = picture.Title;
                rsPicture.priority = picture.Priority;
                rsPicture.status = picture.Status;
                try
                {
                    dcProduct.SubmitChanges();
                    //#Staff_Activity_Log================================================================================================ STEP 2 ============
                    StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_picture, StaffLogActionType.Update, StaffLogSection.Product, picture.ProductID,
                        "tbl_product_pic", "cat_id,type_id,pic_code,title,priority,status", arroldValue, "pic_id", picture.PicID);
                    //==================================================================================================================== COMPLETED ========
                    return true;
                }
                catch {
                    return false;
                }
        }

        public bool Update()
        {
            return ProductPic.UpdateProductPicture(this.PicID, this.PicCategoryID, this.PicTypeID, this.PicCode, this.Title, this.Priority, this.Status);
        }
        public static bool UpdateProductPicture(int intPicId, byte bytPicCatId, byte bytPicTypeId,  string strFileName, string strTitle, byte bytPriority, bool bolStatus)
        {
            ProductPic cProductPic = new ProductPic
            {
                 PicID = intPicId,
                 PicCategoryID = bytPicCatId,
                 PicTypeID = bytPicTypeId,
                 PicCode = strFileName,
                 Title = strTitle,
                 Priority = bytPriority,
                 Status = bolStatus
            };

            return cProductPic.UpdatePicture(cProductPic);

        }
        public bool UpdatePriorityPic(int intPicId, byte bytPri)
        {
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_pic","priority","pic_id",intPicId);
            //============================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product_pic SET priority=@priority WHERE pic_id=@pic_id", cn);
                cmd.Parameters.Add("@pic_id", SqlDbType.Int).Value = intPicId;
                cmd.Parameters.Add("@priority", SqlDbType.TinyInt).Value = bytPri;
                cn.Open();

                ret = ExecuteNonQuery(cmd);

            }
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_picture, StaffLogActionType.Update, StaffLogSection.Product, int.Parse(HttpContext.Current.Request.QueryString["pid"]),
                "tbl_product_pic", "priority", arroldValue, "pic_id", intPicId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }

        public bool DeleteProductPic(int intPicId)
        {
            //bool fret = false;
            int del = 0;

            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            ProductPicName cPicName = new ProductPicName();
            if (cPicName.getProductPicNameBypicId(intPicId).Count > 0)
            {
                foreach (ProductPicName item in cPicName.getProductPicNameBypicId(intPicId))
                {
                    IList<object[]> arroldValue = null;
                    //#Staff_Activity_Log================================================================================================ STEP 1 ==
                    arroldValue = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_product_pic_content", "pic_id", item.PicID);
                    //============================================================================================================================
                    using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand("DELETE FROM tbl_product_pic_content WHERE pic_id=@pic_id", cn);
                        cmd.Parameters.Add("@pic_id", SqlDbType.Int).Value = intPicId;
                        cn.Open();
                        del = ExecuteNonQuery(cmd);
                    }
                    //#Staff_Activity_Log================================================================================================ STEP 2 ==
                    StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Product_picture, StaffLogActionType.Delete, StaffLogSection.Product, ProductId,
                        "tbl_product_pic_content", arroldValue, "pic_id", item.PicID);
                    //============================================================================================================================
                }
            }
            
                var PicDel = dcProduct.tbl_product_pics.SingleOrDefault(pp => pp.pic_id == intPicId);
                IList<object[]> arroldValue2 = null;


                //#Staff_Activity_Log================================================================================================ STEP 1 ==
                arroldValue2 = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_product_pic", "pic_id", intPicId);
                //============================================================================================================================

                dcProduct.tbl_product_pics.DeleteOnSubmit(PicDel);
                dcProduct.SubmitChanges();
                

                //#Staff_Activity_Log================================================================================================ STEP 2 ==
                StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Product_picture, StaffLogActionType.Delete, StaffLogSection.Product, ProductId,
                    "tbl_product_pic_content", arroldValue2, "pic_id", intPicId);
                //============================================================================================================================
            
            return true;
        }
    }
}