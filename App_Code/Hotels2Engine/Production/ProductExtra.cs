using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Hotels2thailand.Production;
using Hotels2thailand.Staffs;
/// <summary>
/// Summary description for Product
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public partial class Product : Hotels2BaseClass
    {
        public List<object> getProductCustomByHotelCode(string strHotelCode)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product WHERE product_code IN (" + strHotelCode + ")", cn);
                
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
        public List<object> GetProductListShowExtranetBySupplierID(short shrSupplierID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT product_id,cat_id,destination_id,supplier_price,group_nearby_id,status_id,payment_type_id,product_code,title,");
                query.Append(" star,coor_lat,coor_long,comment,is_internet_free,is_internet_have,point_popular,flag_feature,status,product_phone,num_pic,is_new_pic,is_favorite,isextranet,extranet_active");
                query.Append(" FROM tbl_product WHERE Isextranet = 1 AND supplier_price=@supplier_price ORDER BY title");
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@supplier_price", SqlDbType.SmallInt).Value = shrSupplierID;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }

        }
       
        public bool UpdateExtranetActive(int intProductId, bool ExtranetActive)
        {
            int ret = 0;
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_product", "extranet_active", "product_id", intProductId);
            //============================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_product SET extranet_active=@extranet_active WHERE product_id=@product_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@extranet_active", SqlDbType.Bit).Value = ExtranetActive;
                cn.Open();

                ret = ExecuteNonQuery(cmd);
            }

            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Product_detail, StaffLogActionType.Update, StaffLogSection.Product, intProductId,
                "tbl_product", "extranet_active", arroldValue, "product_id", intProductId);
            //==================================================================================================================== COMPLETED ========

            return (ret == 1);
        }

        public List<object> GetProductListShowExtranet()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT product_id,cat_id,destination_id,supplier_price,group_nearby_id,status_id,payment_type_id,product_code,title,");
                query.Append(" star,coor_lat,coor_long,comment,is_internet_free,is_internet_have,point_popular,flag_feature,status,product_phone,num_pic,is_new_pic,is_favorite,isextranet,extranet_active");
                query.Append(" FROM tbl_product WHERE Isextranet = 1");
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);

                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }

        }


        public List<object> getProductBySupplierId(short shrSupplierId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT product_id,cat_id,destination_id,supplier_price,group_nearby_id,status_id,payment_type_id,product_code,title,");
                query.Append(" star,coor_lat,coor_long,comment,is_internet_free,is_internet_have,point_popular,flag_feature,status,product_phone,num_pic,is_new_pic,is_favorite,isextranet,extranet_active");
                query.Append(" FROM tbl_product WHERE supplier_price = @supplier_price");
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@supplier_price", SqlDbType.SmallInt).Value = shrSupplierId;

                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }

        }

        public List<object> getProductBySupplierIdExtranetActive(short shrSupplierId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT product_id,cat_id,destination_id,supplier_price,group_nearby_id,status_id,payment_type_id,product_code,title,");
                query.Append(" star,coor_lat,coor_long,comment,is_internet_free,is_internet_have,point_popular,flag_feature,status,product_phone,num_pic,is_new_pic,is_favorite,isextranet,extranet_active");
                query.Append(" FROM tbl_product WHERE supplier_price = @supplier_price AND Isextranet = 1");
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@supplier_price", SqlDbType.SmallInt).Value = shrSupplierId;

                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }

        }

        public List<object> getProductBySupplierIdExcepProduct(short shrSupplierId, string ProductIdExcept)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT product_id,cat_id,destination_id,supplier_price,group_nearby_id,status_id,payment_type_id,product_code,title,");
                query.Append(" star,coor_lat,coor_long,comment,is_internet_free,is_internet_have,point_popular,flag_feature,status,product_phone,num_pic,is_new_pic,is_favorite,isextranet,extranet_active");
                query.Append(" FROM tbl_product WHERE supplier_price = @supplier_price AND product_id NOT IN (" + ProductIdExcept + ")");
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@supplier_price", SqlDbType.SmallInt).Value = shrSupplierId;

                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }

        }
    }
}