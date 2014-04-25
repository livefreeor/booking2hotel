using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Hotels2thailand.LinqProvider.Production;
using Hotels2thailand.Staffs;
/// <summary>
/// Summary description for ProductConstructor
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class ProductConstruction:Hotels2BaseClass
    {
        public int ConstructionID { get; set; }
        public int ProductID { get; set; }
        public byte CategoryID { get; set; }
        public string Title { get; set; }
        public Nullable<DateTime> TimeOpen { get; set; }
        public Nullable<DateTime> TimeClose { get; set; }
        public bool Status { get; set; }

        private LinqProductionDataContext dcProduct = new LinqProductionDataContext();

        public int Insert(ProductConstruction Construction)
        {
            tbl_product_construction construction = new tbl_product_construction { 
                product_id=Construction.ProductID,
                cat_id = Construction.CategoryID,
                title = Construction.Title,
                time_open = null,
                time_close = null,
                status = true

            };

            dcProduct.tbl_product_constructions.InsertOnSubmit(construction);
            dcProduct.SubmitChanges();

            int ret = construction.construction_id;
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_detail, StaffLogActionType.Insert, StaffLogSection.Product,
                Construction.ProductID, "tbl_product_construction", "product_id,cat_id,title,time_open,time_close,status", "construction_id", ret);
            //========================================================================================================================================================

            return ret;
        }

        public int InsertProductConstruction(int intProductId, byte bytCatid, string strTitle, DateTime dTimeOpen, DateTime dTimeClose, bool Status)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_construction (product_id,cat_id,title,time_open,time_close,status) VALUES (@product_id,@cat_id,@title,@time_open,@time_close,@status); SET @construction_id=SCOPE_IDENTITY()", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCatid;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = strTitle;
                cmd.Parameters.Add("@time_open", SqlDbType.SmallDateTime).Value = dTimeOpen;
                cmd.Parameters.Add("@time_close", SqlDbType.SmallDateTime).Value = dTimeClose;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = Status;
                cmd.Parameters.Add("@construction_id", SqlDbType.Int).Direction = ParameterDirection.Output;

                cn.Open();
                ExecuteNonQuery(cmd);
                int ret = (int)cmd.Parameters["@construction_id"].Value;

                //=== STAFF ACTIVITY =====================================================================================================================================
                StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_detail, StaffLogActionType.Insert, StaffLogSection.Product,
                    intProductId, "tbl_product_construction", "product_id,cat_id,title,time_open,time_close,status", "construction_id", ret);
                //========================================================================================================================================================
                return ret ;
            }
        }

        public bool Update()
        {
            return ProductConstruction.UpdateProductConstruction(this.ConstructionID, this.CategoryID, this.Title, this.TimeOpen, this.TimeClose, this.Status);
        }

        public static bool UpdateProductConstruction(int intConStructionId, byte bytCatId, string strtitle, Nullable<DateTime> dDateStart, Nullable<DateTime> dDateEnd, bool bolStatus)
        {
            ProductConstruction cUpdate = new ProductConstruction
            {
                ConstructionID = intConStructionId,
                CategoryID = bytCatId,
                Title = strtitle,
                TimeOpen = dDateStart,
                TimeClose = dDateEnd,
                Status = bolStatus
            };

            return cUpdate.UpdateConstruction(cUpdate);
        }

        public bool UpdateConstruction(ProductConstruction Construction)
        {
            tbl_product_construction rsConstruction = dcProduct.tbl_product_constructions.Single(c=>c.construction_id==Construction.ConstructionID);


            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep(rsConstruction.cat_id, rsConstruction.title, rsConstruction.time_open, rsConstruction.time_close, rsConstruction.status);
            //============================================================================================================================
                rsConstruction.cat_id = Construction.CategoryID;
                rsConstruction.title = Construction.Title;
                rsConstruction.time_open = Construction.TimeOpen;
                rsConstruction.time_close = Construction.TimeClose;
                rsConstruction.status = Construction.Status;
                
                dcProduct.SubmitChanges();
                //#Staff_Activity_Log================================================================================================ STEP 2 ============
                StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_detail, StaffLogActionType.Update, StaffLogSection.Product, rsConstruction.product_id, "tbl_product_construction",
                    "cat_id,title,time_open,time_close,status", arroldValue, "construction_id", Construction.ConstructionID);
                //==================================================================================================================== COMPLETED ========
                return true;
                
        }

        public List<object> GetConstructionByProductID(int ProductID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT construction_id,product_id,cat_id,title,time_open,time_close,status FROM tbl_product_construction WHERE product_id=@product_id AND status = 1", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = ProductID;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public ProductConstruction GetConstructionByID(int ConstructionID)
        {
            var construction = dcProduct.tbl_product_constructions.Single(c=>c.construction_id==ConstructionID);
            if (construction == null)
                return null;
            else
            {
                return (ProductConstruction)MappingObjectFromDataContext(construction);
            }
            
        }

        public ProductConstruction GetConstructionContentByID(int ConstructionID, byte bytLangId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT co.construction_id,co.product_id,co.cat_id,coc.title,co.time_open,co.time_close,co.status FROM tbl_product_construction co, tbl_product_construction_content coc WHERE coc.construction_id = co.construction_id AND co.construction_id=@construction_id AND coc.lang_id=@lang_id", cn);
                cmd.Parameters.Add("@construction_id", SqlDbType.Int).Value = ConstructionID;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (ProductConstruction)MappingObjectFromDataReader(reader);
                else
                    return null;
            }

        }

        public List<object> GetConstructionByCatIdAndProductId(byte bytCatid, int intProductId)
        {
            var construction = from item in dcProduct.tbl_product_constructions
                               where item.cat_id == bytCatid && item.product_id == intProductId
                               select item;

            return MappingObjectFromDataContextCollection(construction);
        }

        //=========================== CONSTRUCTION LANGUAGE ==============================
        // tbl_product_construction_content

//        
//
//

        public Dictionary<string, string> getProductConstructionByProductId(int intProductId, byte bytLangId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT pco.title, pco.detail");
                query.Append(" FROM tbl_product_construction pc, tbl_product_construction_content pco");
                query.Append(" WHERE pc.construction_id = pco.construction_id AND pco.lang_id = @lang_id AND pc.product_id = @product_id");
                


                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                Dictionary<string, string> iDic = new Dictionary<string, string>();
                while (reader.Read())
                {
                    iDic.Add(reader[0].ToString(), reader[1].ToString());
                }
                return iDic;

            }
        }

        public static ArrayList getConStructioncontentBYIdAndLangId(int intConstrucId, byte bytLangId)
        {
            LinqProductionDataContext dcProduct = new LinqProductionDataContext();
            var Result = dcProduct.tbl_product_construction_contents.SingleOrDefault(cos => cos.construction_id == intConstrucId && cos.lang_id == bytLangId);
            if (Result == null)
                return null;
            else
            {
                ArrayList arryItem = new ArrayList();
                arryItem.Add(Result.title);
                arryItem.Add(Result.detail);
                return arryItem;
            }
        }

        

        public int InsertConStructionContent(int intConstrucId, byte bytLangId, string strTitle, string Detail)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_construction_content (construction_id,lang_id,title,detail) VALUES (@construction_id,@lang_id,@title,@detail)", cn);
                cmd.Parameters.Add("@construction_id", SqlDbType.Int).Value = intConstrucId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strTitle;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = Detail;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);

                int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
                //=== STAFF ACTIVITY =====================================================================================================================================
                StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_detail, StaffLogActionType.Insert, StaffLogSection.Product,
                    ProductId, "tbl_product_construction_content", "construction_id,lang_id,title,detail", "construction_id,lang_id", intConstrucId, bytLangId);
                //========================================================================================================================================================

                return ret;
            }
        }

        public static bool UpdateConStructionContent(int intConstrucId, byte bytLangId, string strTitle, string Detail)
        {
            LinqProductionDataContext dcProduct = new LinqProductionDataContext();
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product_construction_content","title,detail","construction_id,lang_id",intConstrucId,bytLangId);
            //============================================================================================================================

            var Update = dcProduct.ExecuteCommand("UPDATE tbl_product_construction_content SET title={0},detail={1} WHERE construction_id={2} AND lang_id={3}", strTitle, Detail, intConstrucId, bytLangId);

            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_detail, StaffLogActionType.Update, StaffLogSection.Product, ProductId, "tbl_product_construction_content",
                "title,detail", arroldValue, "construction_id,lang_id", intConstrucId, bytLangId);
            //==================================================================================================================== COMPLETED ========
            return (Update == 1);
        }
    }
}