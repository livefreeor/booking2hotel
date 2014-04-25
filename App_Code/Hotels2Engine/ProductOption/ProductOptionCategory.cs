using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Production;

/// <summary>
/// Summary description for ProductOptionCategory
/// </summary>
/// 
namespace Hotels2thailand.ProductOption
{
    public class ProductOptionCategory : Hotels2BaseClass
    {
        public short CategoryID { get; set; }
        public string Title { get; set; }

        private LinqProductionDataContext dcProductCategory = new LinqProductionDataContext();

        public Dictionary<string, string> GetProductCategoryAll()
        {
            Dictionary<string, string> dataList = new Dictionary<string, string>();
            var result = from oc in dcProductCategory.tbl_product_option_cats
                         select oc;

            foreach (var item in result)
            {
                dataList.Add(item.cat_id.ToString(), item.title.ToString());
            }
            return dataList;
        }

        public Dictionary<string, string> GetProductCategoryAllExpGala()
        {
            Dictionary<string, string> dataList = new Dictionary<string, string>();
            var result = from oc in dcProductCategory.tbl_product_option_cats
                         where oc.cat_id != 5
                         select oc;

            foreach (var item in result)
            {
                dataList.Add(item.cat_id.ToString(), item.title.ToString());
            }
            return dataList;
        }

        public Dictionary<string, string> GetProductCategoryAllExpGala(int intProductId, short shrSupplierId)
        {
            //get All Option EXception GALA DINNER
            Dictionary<string, string> dataList = new Dictionary<string, string>();
            var result = from oc in dcProductCategory.tbl_product_option_cats
                         where oc.cat_id != 47
                         select oc;
            Option cOption = new Option();
            foreach (var item in result)
            {
                if (cOption.GetProductOptionByCurrentSupplierANDProductIdANDCATID(shrSupplierId,intProductId, item.cat_id).Count > 0)
                {
                    dataList.Add(item.cat_id.ToString(), item.title.ToString());
                }
            }
            return dataList;
        }

        // GalaDinner Only!!!
        public Dictionary<string, string> GetProductCategoryGalaOnly(int intProductId, short shrSupplierId)
        {
            //get All Option EXception GALA DINNER
            Dictionary<string, string> dataList = new Dictionary<string, string>();
            var result = from oc in dcProductCategory.tbl_product_option_cats
                         where oc.cat_id == 47
                         select oc;
            Option cOption = new Option();
            foreach (var item in result)
            {
                if (cOption.GetProductOptionByCurrentSupplierANDProductIdANDCATID(shrSupplierId, intProductId, item.cat_id).Count > 0)
                {
                    dataList.Add(item.cat_id.ToString(), item.title.ToString());
                }
            }
            return dataList;
        }
    }
}