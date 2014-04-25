using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using Hotels2thailand.Staffs;


/// <summary>
/// Summary description for ProductCategory
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class ProductPricecheck : Hotels2BaseClass
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public int OptionId { get; set; }
        public string OptionTitle { get; set; }
        public int ConditionId { get; set; }
        public string ConditionTitle { get; set; }
        public int PriceId { get; set; }
        public DateTime DAtePrice { get; set; }
        public decimal Price { get; set; }
        public int CountDuplicate { get; set; }

        public IList<object> GetDuplicationPriceALl()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT p.product_id,p.title , op.option_id , op.title, conx.condition_id, conx.title, conp.price_id , conp.date_price, conp.price,(");
                query.Append(" SELECT COUNT(pr.price_id) FROM tbl_product_option_condition_price_extranet pr ");
                query.Append(" WHERE pr.condition_id = conx.condition_id AND pr.date_price = conp.date_price ");
                query.Append(" AND pr.status = 1");
                query.Append(" )");
                query.Append(" FROM tbl_product p , tbl_product_option op, tbl_product_option_condition_extra_net conx , tbl_product_option_condition_price_extranet conp");
                query.Append(" WHERE p.isextranet = 1 AND p.extranet_active = 1 AND conx.option_id = op.option_id AND conx.condition_id = conp.condition_id");
                // AND p.status = 1 AND op.status = 1 AND conx.status = 1
                query.Append(" AND p.product_id = op.product_id  ");
                //query.Append(" AND p.cat_id = 29  AND conp.status = 1 ");
                query.Append(" AND (");
                query.Append(" SELECT COUNT(pr.price_id) FROM tbl_product_option_condition_price_extranet pr ");
                query.Append(" WHERE pr.condition_id = conx.condition_id AND pr.date_price = conp.date_price 	AND pr.status = 1");
                query.Append(" ) > 1");

                query.Append(" ORDER BY conp.date_price , conp.price_id");

                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
            
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public IList<object> GetDuplicationPrice(int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT p.product_id,p.title , op.option_id , op.title, conx.condition_id, conx.title, conp.price_id , conp.date_price, conp.price,(");
                query.Append(" SELECT COUNT(pr.price_id) FROM tbl_product_option_condition_price_extranet pr ");
                query.Append(" WHERE pr.condition_id = conx.condition_id AND pr.date_price = conp.date_price ");
                query.Append(" AND pr.status = 1");
                query.Append(" )");
                query.Append(" FROM tbl_product p , tbl_product_option op, tbl_product_option_condition_extra_net conx , tbl_product_option_condition_price_extranet conp");
                query.Append(" WHERE p.isextranet = 1 AND p.extranet_active = 1 AND conx.option_id = op.option_id AND conx.condition_id = conp.condition_id");
                query.Append(" AND p.product_id = op.product_id  ");
                query.Append(" AND p.product_id = @product_id");
                query.Append(" AND (");
                query.Append(" SELECT COUNT(pr.price_id) FROM tbl_product_option_condition_price_extranet pr ");
                query.Append(" WHERE pr.condition_id = conx.condition_id AND pr.date_price = conp.date_price 	AND pr.status = 1");
                query.Append(" ) > 1");

                query.Append(" ORDER BY conp.date_price , conp.price_id");

                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public IList<object> GetDuplicationPriceByCOnditionId(int intProductId, int intCOnditionID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT p.product_id,p.title , op.option_id , op.title, conx.condition_id, conx.title, conp.price_id , conp.date_price, conp.price,(");
                query.Append(" SELECT COUNT(pr.price_id) FROM tbl_product_option_condition_price_extranet pr ");
                query.Append(" WHERE pr.condition_id = conx.condition_id AND pr.date_price = conp.date_price ");
                query.Append(" AND pr.status = 1");
                query.Append(" )");
                query.Append(" FROM tbl_product p , tbl_product_option op, tbl_product_option_condition_extra_net conx , tbl_product_option_condition_price_extranet conp");
                query.Append(" WHERE p.isextranet = 1 AND p.extranet_active = 1 AND conx.option_id = op.option_id AND conx.condition_id = conp.condition_id");
                //AND conp.date_price >= DATEADD(HH,14,GETDATE())
                query.Append(" AND p.product_id = op.product_id ");
                query.Append(" AND p.product_id = @product_id ANd conx.condition_id = @condition_id");
                query.Append(" AND (");
                query.Append(" SELECT COUNT(pr.price_id) FROM tbl_product_option_condition_price_extranet pr ");
                query.Append(" WHERE pr.condition_id = conx.condition_id AND pr.date_price = conp.date_price 	AND pr.status = 1");
                query.Append(" ) > 1");

                query.Append(" ORDER BY conp.date_price , conp.price_id");

                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@condition_id", SqlDbType.Int).Value = intCOnditionID;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public bool DeletePrice(int intPriceId)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tbl_staff_activity_price_extranet WHERE price_id = @price_id", cn);
                SqlCommand cmd2 = new SqlCommand("DELETE FROM tbl_product_option_condition_price_extranet WHERE price_id = @price_id", cn);
                cmd.Parameters.Add("@price_id", SqlDbType.Int).Value = intPriceId;
                cmd2.Parameters.Add("@price_id", SqlDbType.Int).Value = intPriceId;

                cn.Open();
                if (ExecuteNonQuery(cmd) == 1)
                   ret = ExecuteNonQuery(cmd2);

                return (ret == 1);
            }
        }
        
    }
}