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
    public partial class Product_Report : Hotels2BaseClass
    {

        public int ProductID { get; set; }
        public byte ProductCategoryID { get; set; }
        public short DestinationID { get; set; }
        public string DestinationTitle { get; set; }
        public string ProductCode { get; set; }
        public string ProductTitle { get; set; }
        public short SupplierPrice { get; set; }
        public string Suppliertitle { get; set; }
        public DateTime? dLastDateRate { get; set; }



        public int GetCountProductAll_OnlineByProductCat(byte bytProductCat)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(product_id) FROM tbl_product WHERE cat_id = @cat_id AND status = 1", cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytProductCat;
                cn.Open();
                return (int)ExecuteScalar(cmd);
            }
        }
        public IList<object> GetProductAll_OnlineByProductCat_Contract_WholeSalse(byte bytProductCat, short shrDestinationId, bool IsAll)
        {
            StringBuilder query = new StringBuilder();

            query.Append("SELECT p.product_id, p.cat_id, d.destination_id, d.title ,p.product_code, p.title, p.supplier_price,s.title,");
            query.Append(" (SELECT TOP 1 pr.date_end FROM tbl_product_option_condition_price conp, tbl_product_period pr WHERE pr.supplier_id = p.supplier_price  AND pr.product_id=p.product_id ORDER BY pr.date_end DESC) as expire_direct");
            query.Append(" FROM tbl_product p ,tbl_destination d, tbl_supplier s, tbl_supplier_category sc");
            query.Append(" WHERE p.destination_id = d.destination_id AND p.cat_id = @cat_id ");
            query.Append(" and p.supplier_price = s.supplier_id  and s.cat_id = sc.cat_id");
            query.Append(" AND p.status = 1 AND s.cat_id = 1 AND sc.cat_id = 1");

            if (!IsAll)
            query.Append(" AND p.destination_id = @des_id");

            query.Append(" ORDER BY  d.title, p.title");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytProductCat;
                if (!IsAll)
                    cmd.Parameters.Add("@des_id", SqlDbType.SmallInt).Value = shrDestinationId;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }


        public IList<object> GetProductAll_OnlineByProductCat_Contract(byte bytProductCat, short shrDestinationId, bool IsAll)
        {
            StringBuilder query = new StringBuilder();

            query.Append("SELECT p.product_id, p.cat_id, d.destination_id, d.title ,p.product_code, p.title, p.supplier_price,s.title,");
            query.Append(" (SELECT TOP 1 pr.date_end FROM tbl_product_option_condition_price conp, tbl_product_period pr WHERE pr.supplier_id = p.supplier_price  AND pr.product_id=p.product_id ORDER BY pr.date_end DESC) as expire_direct");
            query.Append(" FROM tbl_product p ,tbl_destination d, tbl_supplier s");
            query.Append(" WHERE p.destination_id = d.destination_id  AND p.cat_id = @cat_id ");
            query.Append(" and p.supplier_price = s.supplier_id");
            query.Append(" AND p.status = 1 ");
            if (!IsAll)
                query.Append(" AND p.destination_id = @des_id");

            query.Append(" ORDER BY  d.title, p.title");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytProductCat;
                if (!IsAll)
                    cmd.Parameters.Add("@des_id", SqlDbType.SmallInt).Value = shrDestinationId;
                
                
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public IList<object> GetProductAll_OnlineByProductCat_Contract_Expired(byte bytProductCat, short shrDestinationId, bool IsAll)
        {
            StringBuilder query = new StringBuilder();

            query.Append("SELECT p.product_id, p.cat_id, d.destination_id, d.title ,p.product_code, p.title, p.supplier_price,s.title,");
            query.Append(" (SELECT TOP 1 pr.date_end FROM tbl_product_option_condition_price conp, tbl_product_period pr WHERE pr.supplier_id = p.supplier_price  AND pr.product_id=p.product_id ORDER BY pr.date_end DESC) as expire_direct");
            query.Append(" FROM tbl_product p ,tbl_destination d, tbl_supplier s");
            query.Append(" WHERE p.destination_id = d.destination_id");
            query.Append(" and p.supplier_price = s.supplier_id AND p.cat_id = @cat_id");
            query.Append(" AND p.status = 1 AND p.isextranet = 0");
            query.Append(" AND ((SELECT TOP 1 pr.date_end FROM tbl_product_option_condition_price conp, tbl_product_period pr WHERE pr.supplier_id = p.supplier_price  AND");
            query.Append(" pr.product_id=p.product_id ORDER BY pr.date_end DESC )<= DATEADD(HH,14,GETDATE()) ");
            query.Append(" OR (SELECT TOP 1 pr.date_end FROM tbl_product_option_condition_price conp, tbl_product_period pr WHERE pr.supplier_id = p.supplier_price  AND pr.product_id=p.product_id ORDER BY pr.date_end DESC) IS NULL )");
            if (!IsAll)
                query.Append(" AND p.destination_id = @des_id");

            query.Append(" ORDER BY  d.title, p.title");

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytProductCat;
                if (!IsAll)
                    cmd.Parameters.Add("@des_id", SqlDbType.SmallInt).Value = shrDestinationId;
                   
                
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public IList<object> GetProductAll_OnlineByProductCat_Extranet(byte bytProductCat, short shrDestinationId, bool IsAll)
        {
            StringBuilder query = new StringBuilder();

            query.Append("SELECT p.product_id, p.cat_id, d.destination_id, d.title ,p.product_code, p.title, p.supplier_price,s.title,");
            query.Append(" (SELECT TOP 1 pri.date_price FROM tbl_product_option op, tbl_product_option_condition_extra_net cone,");
            query.Append(" tbl_product_option_condition_price_extranet pri");
            query.Append(" WHERE op.product_id = p.product_id AND op.option_id = cone.option_id AND p.cat_id = @cat_id ");
            query.Append(" AND cone.condition_id = pri.condition_id ORDER BY date_price DESC");
            query.Append(" )");

            query.Append(" FROM tbl_product p ,tbl_destination d, tbl_supplier s");
            query.Append(" WHERE p.destination_id = d.destination_id");
            query.Append(" and p.supplier_price = s.supplier_id");
            query.Append(" AND p.status = 1 AND p.isextranet = 1 AND p.extranet_active = 1");

            if (!IsAll)
                query.Append(" AND p.destination_id = @des_id");

            query.Append(" ORDER BY  d.title, p.title");



            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytProductCat;
                if (!IsAll)
                    cmd.Parameters.Add("@des_id", SqlDbType.SmallInt).Value = shrDestinationId;

                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
        public IList<object> GetProductAll_OnlineByProductCat_Extranet_Expired(byte bytProductCat, short shrDestinationId, bool IsAll)
        {
            StringBuilder query = new StringBuilder();

            query.Append("SELECT p.product_id, p.cat_id, d.destination_id, d.title ,p.product_code, p.title, p.supplier_price,s.title,");
            query.Append(" (SELECT TOP 1 pri.date_price FROM tbl_product_option op, tbl_product_option_condition_extra_net cone,");
            query.Append(" tbl_product_option_condition_price_extranet pri");
            query.Append(" WHERE op.product_id = p.product_id AND op.option_id = cone.option_id ");
            query.Append(" AND cone.condition_id = pri.condition_id ORDER BY date_price DESC");
            query.Append(" )");

            query.Append(" FROM tbl_product p ,tbl_destination d, tbl_supplier s");
            query.Append(" WHERE p.destination_id = d.destination_id");
            query.Append(" and p.supplier_price = s.supplier_id");
            query.Append(" AND p.status = 1 AND p.isextranet = 1 AND p.extranet_active = 1 AND p.cat_id = @cat_id ");

            query.Append(" AND (SELECT TOP 1 pri.date_price FROM tbl_product_option op, tbl_product_option_condition_extra_net cone,");
            query.Append(" tbl_product_option_condition_price_extranet pri");
            query.Append(" WHERE op.product_id = p.product_id AND op.option_id = cone.option_id ");
            query.Append(" AND cone.condition_id = pri.condition_id ORDER BY date_price DESC");
            query.Append(" ) <= DATEADD(HH,14,GETDATE())");

            if (!IsAll)
                query.Append(" AND p.destination_id = @des_id");

            query.Append(" ORDER BY  d.title, p.title");



            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytProductCat;

                if (!IsAll)
                    cmd.Parameters.Add("@des_id", SqlDbType.SmallInt).Value = shrDestinationId;
                
                
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }



        public IList<object> GetProduct_NewPromotion_contract(byte bytProductCat, short shrDestinationId, byte bytMonth, bool IsAll)
        {
            StringBuilder query = new StringBuilder();

            query.Append("SELECT DISTINCT(p.product_id), p.cat_id, p.destination_id, d.title, p.product_code, p.title, p.supplier_price, s.title");
            query.Append(" FROM tbl_product p, tbl_promotion pro ,tbl_destination d, tbl_supplier s ");
            query.Append(" WHERE p.product_id = pro.product_id AND p.status = 1 AND p.isextranet = 0 AND p.cat_id = @cat_id ");
            query.Append(" AND MONTH(pro.date_submit) = @month AND YEAR(pro.date_submit) = YEAR(DATEADD(HH,14,GETDATE()))");
            query.Append(" AND pro.status = 1 AND d.destination_id = p.destination_id AND s.supplier_id = p.supplier_price");
            
            if (!IsAll)
             query.Append(" AND p.destination_id = @des_id ");
            
            query.Append(" ORDER BY  d.title, p.title");
            

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytProductCat;
                cmd.Parameters.Add("@month", SqlDbType.TinyInt).Value = bytMonth;
                if (!IsAll)
                    cmd.Parameters.Add("@des_id", SqlDbType.SmallInt).Value = shrDestinationId;
                
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public IList<object> GetProduct_New(byte bytProductCat, short shrDestinationId, byte bytMonth, bool IsAll)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT p.product_id , p.cat_id, p.destination_id, d.title, p.product_code, p.title, p.supplier_price, s.title ");
            query.Append(" FROM tbl_product p , tbl_supplier s, tbl_destination d ");
            query.Append(" WHERE d.destination_id = p.destination_id AND s.supplier_id = p.supplier_price AND p.status = 1 ");
            query.Append(" AND MONTH(p.date_submit) = @month AND YEAR(p.date_submit) =  YEAR(DATEADD(HH,14,GETDATE()))");
            query.Append(" AND p.cat_id = @cat_id");
            if (!IsAll)
                query.Append(" AND p.destination_id = @des_id ");
            query.Append(" ORDER BY  d.title, p.title");


            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytProductCat;
                cmd.Parameters.Add("@month", SqlDbType.TinyInt).Value = bytMonth;
                if (!IsAll)
                    cmd.Parameters.Add("@des_id", SqlDbType.SmallInt).Value = shrDestinationId;
               
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

    }
}