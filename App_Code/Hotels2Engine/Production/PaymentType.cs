using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Production;
/// <summary>
/// Summary description for destination
/// </summary>
/// 
namespace Hotels2thailand.Production
{
public class PaymentType:Hotels2BaseClass
{
    public byte PaymentTypeID { get; set; }
    public string Title { get; set; }

    private LinqProductionDataContext dcProduct = new LinqProductionDataContext();

    public Dictionary<string,string> GetPaymentTypeAll()
    {
        var result = from item in dcProduct.tbl_product_payment_types
                     select item;
        Dictionary<string, string> dataList = new Dictionary<string, string>();
        foreach (var item in result)
        {
            dataList.Add(item.payment_type_id.ToString(), item.title.ToString());
        }
        return dataList;
        //return MappingObjectFromDataContextCollection(result);
    }
}
}