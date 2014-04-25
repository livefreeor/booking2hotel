using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using Hotels2thailand.LinqProvider.Production;

/// <summary>
/// Summary description for ProductContent
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class Facility:Hotels2BaseClass
    {
        private LinqProductionDataContext dcProduct = new LinqProductionDataContext();

        public int FacilityID { get; set; }
        public int ProductID { get; set; }
        public byte LanguageID { get; set; }
        public string Title { get; set; }

        public int Insert(Facility facility)
        {
            tbl_facility_product fProduct = new tbl_facility_product { 
            product_id=facility.ProductID,
            lang_id=facility.LanguageID,
            title=facility.Title
            };
            dcProduct.tbl_facility_products.InsertOnSubmit(fProduct);
            dcProduct.SubmitChanges();
            return fProduct.fac_id;
        }

        public bool Update(Facility facility)
        {
            tbl_facility_product rsFacility = dcProduct.tbl_facility_products.Single(f=>f.fac_id==facility.FacilityID);
            rsFacility.product_id=facility.ProductID;
            rsFacility.lang_id=facility.LanguageID;
            rsFacility.title = facility.Title;

            try
            {
                dcProduct.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }

          }   
        public List<object> GetFacilityByProductID(int ProductID)
        {
            var result=from item in dcProduct.tbl_facility_products
            where item.product_id==ProductID
            select item;
            return MappingObjectFromDataContextCollection(result);
           
        }
    }
}