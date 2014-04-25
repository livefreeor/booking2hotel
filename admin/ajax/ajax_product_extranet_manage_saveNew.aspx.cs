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

public partial class ajax_product_extranet_manage_saveNew : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Page.IsPostBack)
        {
            Response.Write(InsertNewCom());
            Response.End();
            
        }
    }


    public bool InsertNewCom()
    {
        int ret = 0;
        try
        {
            Product cProduct = new Product();

            int intProductId = int.Parse(Request.Form["product_active"]);
            short shrSupplierId = short.Parse(Request.Form["supplier_active"]);

            DateTime dDateStart = Request.Form["hd_date_start_"].Hotels2DateSplitYear("-");
            DateTime dDateEnd = Request.Form["hd_date_end_"].Hotels2DateSplitYear("-");


            byte bytCom = byte.Parse(Request.Form["com_"]);

            ProductCommission cProductCom = new ProductCommission();
            cProductCom.Insertnewcommission(intProductId, shrSupplierId, dDateStart,
                   dDateEnd, bytCom);
            ret = 1;
        }
        catch (Exception ex)
        {
            Response.Write("error: " + ex.Message);
            Response.End();
        }

        return (ret == 1);

    }
    
    
    
}