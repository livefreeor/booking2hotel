using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using Hotels2thailand.LinqProvider.Production;

/// <summary>
/// Summary description for ProductCategory
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class ProductCategory : Hotels2BaseClass
    {
        private LinqProductionDataContext dcProduct = new LinqProductionDataContext();
        public byte ProductCate_ID { get; set; }
        public string Title { get; set; }
        public string FolderCat { get; set; }

        public List<object> GetProductCategoryAll()
        {
            var result = from item in dcProduct.tbl_product_categories
                         select item;
            return MappingObjectFromDataContextCollection(result);
        }

       

        public ProductCategory GetProductCategoryByID(byte ProductCat_ID)
        {
            var result = dcProduct.tbl_product_categories.Single(pc => pc.cat_id == ProductCate_ID);
            return (ProductCategory)MappingObjectFromDataContext(result);
        }

        public string getProductCatTitle(byte bytCatId)
        {
            string strTitle = string.Empty;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT title FROM tbl_product_category WHERE cat_id = @cat_id", cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCatId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    strTitle = reader[0].ToString();
                }

            }
            return strTitle;
        }
        public Dictionary<string,string> GetProductCategory()
        {
            Dictionary<string, string> dataList = new Dictionary<string, string>();
            var result = from pc in dcProduct.tbl_product_categories
                         select pc;

            foreach(var item in result){
                dataList.Add(item.cat_id.ToString(), item.title.ToString());
            }
            return dataList;
        }
    }
}