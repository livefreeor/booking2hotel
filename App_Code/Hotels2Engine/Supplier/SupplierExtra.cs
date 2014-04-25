using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Supplier;
using Hotels2thailand.LinqProvider.Production;
using Hotels2thailand.Staffs;
using Hotels2thailand.Production;
using System.Data.SqlClient;
using Hotels2thailand;
using System.Data;


/// <summary>
/// Summary description for Supplier
/// include tbl_Supplier and tbl_SupplierCategory
/// </summary>
/// 
namespace Hotels2thailand.Suppliers
{
    public partial class Supplier : Hotels2BaseClass
    {
        public List<object> getSupplierByProductExtraNet(  short shrDestinationId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT supplier_id,cat_id,payment_type_id,title,title_common,title_company,address,address_office,comment,tax_vat,tax_service,tax_local,status FROM tbl_supplier sp WHERE (SELECT COUNT(p.product_id) FROM tbl_product p WHERE p.supplier_price = sp.supplier_id AND p.destination_id = @destination_id AND p.isextranet = 1) > 0 ORDER BY title", cn);
                cmd.Parameters.Add("@destination_id", SqlDbType.SmallInt).Value = shrDestinationId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }


    }
}