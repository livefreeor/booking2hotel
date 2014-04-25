using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Production;
/// <summary>
/// Summary description for ProductConstructionCategory
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class ProductConstructionCategory:Hotels2BaseClass
    {
        private LinqProductionDataContext dcProduct = new LinqProductionDataContext();
        public Dictionary<string, string> ConstructionCategory()
        {
            Dictionary<string, string> dataList = new Dictionary<string, string>();
            var result = from pc in dcProduct.tbl_product_construction_cats
                         select pc;

            foreach (var item in result)
            {
                dataList.Add(item.cat_id.ToString(), item.title.ToString());
            }
            return dataList;
        }

        public Dictionary<byte, string> ConstructionCategoryByHaveConstructionrecord(int intProductId)
        {
            Dictionary<byte, string> dataList = new Dictionary<byte, string>();
            var result = from pc in dcProduct.tbl_product_construction_cats
                         select pc;

            ProductConstruction cConstruction = new ProductConstruction();
            foreach (var item in result)
            {
                if (cConstruction.GetConstructionByCatIdAndProductId(item.cat_id, intProductId).Count > 0)
                {
                    dataList.Add(item.cat_id, item.title.ToString());
                }
            }
            return dataList;
        }
    }
}