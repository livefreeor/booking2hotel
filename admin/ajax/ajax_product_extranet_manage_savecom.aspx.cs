using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.Suppliers;
using Hotels2thailand;

public partial class ajax_product_extranet_manage_savecom : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Page.IsPostBack)
        {
            Response.Write(UpdateCommission());
            Response.End();
            
        }
    }


    public bool UpdateCommission()
    {
        
        Product cProduct = new Product();

        int intProductId = int.Parse(Request.Form["product_active"]);
        short shrSupplierId = short.Parse(Request.Form["supplier_active"]);
        int intComId = int.Parse(Request.QueryString["comid"]);
        DateTime dDateStart = Request.Form["hd_date_start_" + intComId].Hotels2DateSplitYear("-");
        DateTime dDateEnd = Request.Form["hd_date_end_" + intComId].Hotels2DateSplitYear("-");
        byte bytCom = byte.Parse(Request.Form["com_" + intComId]);
        
        ProductCommission cProductCom = new ProductCommission();


        return cProductCom.UpdateProductcommissionbyCommissionId(intComId, intProductId, dDateStart, dDateEnd, bytCom);

    }
    
    
    
}