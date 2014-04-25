using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Production;
/// <summary>
/// Summary description for ProductPicCategory
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public enum Hotels2PicCat : byte
    {
        Product = 1,
        Option = 2,
        Construction = 3,
        Itinerary = 4
    }

    public class ProductPicCategory:Hotels2BaseClass
    {
        private LinqProductionDataContext dcProduct = new LinqProductionDataContext();

        public static string getCatTitleById(byte bytCatId)
        {
            LinqProductionDataContext dcProduct = new LinqProductionDataContext();
            var Result = dcProduct.tbl_product_pic_cats.SingleOrDefault(pc => pc.cat_id == bytCatId);

            string strNull = "No Item";
            if (Result == null)
                return strNull;
            else
            {
                return Result.title.ToString();
            }
            
        }

        

        public Dictionary<byte, string> getPictureCategoryAll()
        {
            Dictionary<byte, string> dicLsit = new Dictionary<byte, string>();

            var Result = from pc in dcProduct.tbl_product_pic_cats
                         select pc;
           
            foreach (var item in Result)
            {

                dicLsit.Add(item.cat_id, item.title.ToString());
            }
            return dicLsit;
        }
        

    }
   
}
