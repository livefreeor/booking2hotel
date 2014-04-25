using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Production;

/// <summary>
/// Summary description for ProductTypeName
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class ProductTypeName:Hotels2BaseClass
    {
        public byte Type_Id { get; set; }
        public byte LangId { get; set; }
        public string ProductTypeNameTitle { get; set; }

        private LinqProductionDataContext dcProduct = new LinqProductionDataContext();

        public ProductTypeName getTypeNameByLangIdAndTypeId(byte bytType_Id, byte bytLangid)
        {
            
            var result = dcProduct.tbl_product_type_names.SingleOrDefault(pt =>pt.type_id == bytType_Id && pt.lang_id==bytLangid);
            if (result == null)
            {
                return null;
            }
            else
            {
                return (ProductTypeName)MappingObjectFromDataContext(result);
            }
            
        }

        public int insertNewLanguage(ProductTypeName cProductTypeName)
        {
            tbl_product_type_name tblProductTypeName = new tbl_product_type_name
            {
                type_id = cProductTypeName.Type_Id,
                lang_id = cProductTypeName.LangId,
                title = cProductTypeName.ProductTypeNameTitle
            };

            dcProduct.tbl_product_type_names.InsertOnSubmit(tblProductTypeName);
            dcProduct.SubmitChanges();

            return tblProductTypeName.lang_id;
        }

        public static int insertNewProductTypeName(byte bytType_id, byte bytLangId, string strTitle)
        {

            ProductTypeName cProductType = new ProductTypeName
            {
                Type_Id = bytType_id,
                LangId = bytLangId,
                ProductTypeNameTitle = strTitle
            };
            return cProductType.insertNewLanguage(cProductType);
        }

        public bool updateProductTypeName(ProductTypeName cProductTypeName)
        {
            var update = dcProduct.tbl_product_type_names.SingleOrDefault(pt => pt.type_id == cProductTypeName.Type_Id && pt.lang_id == cProductTypeName.LangId);
            update.title = cProductTypeName.ProductTypeNameTitle;

            dcProduct.SubmitChanges();
            bool ret = true;
            return ret;
        }

        public bool Update()
        {
            return ProductTypeName.UpdateProductTypeNames(this.Type_Id, this.LangId, this.ProductTypeNameTitle);
        }
        

        public static bool UpdateProductTypeNames(byte bytType_id, byte bytLangId, string strTitle)
        {
            ProductTypeName cProductType = new ProductTypeName
            {
                Type_Id = bytType_id,
                LangId = bytLangId,
                ProductTypeNameTitle = strTitle
            };

            return cProductType.updateProductTypeName(cProductType);
        }
    }
}