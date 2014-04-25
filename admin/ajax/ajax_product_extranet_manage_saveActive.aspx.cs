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

public partial class ajax_product_extranet_manage_saveActive : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Page.IsPostBack)
        {
            Response.Write(UpdateProductActive_Extranet());
            Response.End();
            
        }
    }


    public bool UpdateProductActive_Extranet()
    {
        
        Product cProduct = new Product();

        int intProductId = int.Parse(Request.Form["product_active"]);
        short shrSupplierId = short.Parse(Request.Form["supplier_active"]);

        bool Isactive = true;
        if (Request.Form["product_extra_actives"] == "0")
            Isactive = false;

        return cProduct.UpdateExtranetActive(intProductId, Isactive);

    }
    
    
    
}