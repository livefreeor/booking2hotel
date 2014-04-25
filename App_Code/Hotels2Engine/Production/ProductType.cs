using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using Hotels2thailand.LinqProvider.Production;

/// <summary>
/// Summary description for ProductContent
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class ProductType : Hotels2BaseClass
    {
        public byte TypeID { get; set; }
        public byte ProductCateID { get; set; }
        public string Title { get; set; }
        private LinqProductionDataContext dcProduct = new LinqProductionDataContext();

        public Dictionary<string, string> GetProductTypeAllByProductCat(byte ProductCatId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT type_id, cat_id, title FROM tbl_product_type WHERE cat_id =@cat_id ORDER BY title", cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = ProductCatId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                Dictionary<string, string> dataList = new Dictionary<string, string>();

                while (reader.Read())
                {
                    dataList.Add(reader[0].ToString(), reader[2].ToString());
                }
                return dataList;
            }
            

            
        }

        /// <summary>
        /// return Dictionary Lsit of Product Type By Product category
        /// </summary>
        /// <param name="bytPcat"></param>
        /// <returns></returns>
        public Dictionary<int, string> getProducttypeListByProductCat(byte bytPcat)
        {
            Dictionary<int, string> dicP_Type = new Dictionary<int, string>();
            var result = from pt in dcProduct.tbl_product_types
                         where pt.cat_id == bytPcat
                         select pt;

            foreach (var item in result)
            {
                dicP_Type.Add(item.type_id, item.title);
            }
            return dicP_Type;
        }
        

        public static string GetTypeIdById(byte shrTypeId)
        {
            LinqProductionDataContext dcProduct = new LinqProductionDataContext();
            var result = dcProduct.tbl_product_types.SingleOrDefault(s => s.type_id == shrTypeId);
            if (result == null)
                return null;
            else
            {
                string strTypeId = result.title.ToString();
                return strTypeId;
            }
            
        }

        

        public ProductType GetTitle()
        {
            var result = (from item in dcProduct.tbl_product_types
                          select item).Take(1);
            return (ProductType)MappingObjectFromDataContext(result);
        }
    }
 
}