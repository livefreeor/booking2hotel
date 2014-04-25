using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Production;
using Hotels2thailand.Staffs;

/// <summary>
/// Summary description for TypeProduct
/// </summary>
/// 

namespace Hotels2thailand.Production
{
    public class TypeProduct:Hotels2BaseClass
    {
        private LinqProductionDataContext dcProduct = new LinqProductionDataContext();
        public byte TypeID { get; set; }
        public int ProductID { get; set; }

        private string _product_type = string.Empty;
        public string TypeTitle
        {
            get
            {
                if (string.IsNullOrEmpty(_product_type))
                {
                    _product_type = ProductType.GetTypeIdById(this.TypeID);
                }
                return _product_type;
            }
        }

        public List<object> getTypeProductCheckedListByProdutId(int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT type_id,product_id FROM tbl_type_product WHERE product_id= @product_id ORDER BY type_id DESC",cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();
                
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
             
        }

        public TypeProduct getTypeProductByTypeIdAndProductId(byte bytTypeId, int intProductId)
        {
            var Result = dcProduct.tbl_type_products.SingleOrDefault(tp => tp.type_id == bytTypeId && tp.product_id == intProductId);
            if (Result == null)
            {
                return null;
            }
            else 
            { 
                return (TypeProduct)MappingObjectFromDataContext(Result); 
            }
        }

        public int InsertProductStyle(byte TypeID, int ProductID)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                 
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_type_product(type_id,product_id)VALUES(@TypeId,@ProductId)", cn);
                cmd.Parameters.Add("@TypeId", SqlDbType.TinyInt).Value = TypeID;
                cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = ProductID;
                cn.Open();

                ret = ExecuteNonQuery(cmd);

                
                
            }
            int ProductId = int.Parse(HttpContext.Current.Request.QueryString["pid"]);
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Product_detail, StaffLogActionType.Insert, StaffLogSection.Product,
                ProductId, "tbl_type_product", "type_id,product_id", "type_id,product_id", TypeID, ProductID);
            //========================================================================================================================================================
            return ret;
        }

        //public int InsertProductStyle(byte TypeID, int ProductID)
        //{
        //    using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        //    {
        //        int ret;
        //        SqlCommand cmdS = new SqlCommand("SELECT COUNT(*) FROM tbl_type_product WHERE type_id=@TypeId AND product_id=@ProductId", cn);
        //        cmdS.Parameters.Add("@TypeId", SqlDbType.TinyInt).Value = TypeID;
        //        cmdS.Parameters.Add("@ProductId", SqlDbType.Int).Value = ProductID;
        //        cn.Open();
        //        int returnV = (int)ExecuteScalar(cmdS);
        //        cn.Close();

        //        if (returnV == 0)
        //        {
        //            SqlCommand cmd = new SqlCommand("INSERT INTO tbl_type_product(type_id,product_id)VALUES(@TypeId,@ProductId)", cn);
        //            cmd.Parameters.Add("@TypeId", SqlDbType.TinyInt).Value = TypeID;
        //            cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = ProductID;
        //            cn.Open();
        //            ret = ExecuteNonQuery(cmd);
                    
        //        }
        //        else
        //        {
        //            ret = 0;
        //        }
        //        return ret;
        //    }
        //}
        

        public bool Delete(byte TypeID, int ProductID)
        {
            var result = dcProduct.tbl_type_products.SingleOrDefault(tp => tp.type_id == TypeID && tp.product_id == ProductID);
            dcProduct.tbl_type_products.DeleteOnSubmit(result);

            dcProduct.SubmitChanges();
            return true;
            
            
        }
        public bool DeleteProductTypeAllByProductId(int ProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tbl_type_product WHERE product_id = @product_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = ProductId;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }

        public bool DeleteProductTypeNotInCustomCheck(int ProductId, string type_NOtIn)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tbl_type_product WHERE product_id = @product_id AND type_id NOT IN(" + type_NOtIn + ")", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = ProductId;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }

        public int IsHaveProductType(byte TypeId, int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM tbl_type_product WHERE product_id=@product_id AND type_id=@type_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@type_id", SqlDbType.TinyInt).Value = TypeId;
                cn.Open();
                int ret = (int)ExecuteScalar(cmd);
                return ret;
            }
        }
    }
}