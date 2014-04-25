using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Production;

/// <summary>
/// Summary description for ProductNearByGroup
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class ProductNearByGroup : Hotels2BaseClass
    {
        public short GroupNearByID { get; set; }
        public string Title { get; set; }
        private LinqProductionDataContext dcProduct = new LinqProductionDataContext();
        public Dictionary<string, string> GroupNearByAll()
        {
            Dictionary<string, string> dataList = new Dictionary<string, string>();
            var result = from c in dcProduct.tbl_product_nearby_groups
                         select c;

            foreach (var item in result)
            {
                dataList.Add(item.group_nearby_id.ToString(), item.title.ToString());
            }
            return dataList;
        }
    }
}