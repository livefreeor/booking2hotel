using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.ProductOption;

namespace Hotels2thailand.UI
{
    public partial class admin_productOption_product_option_holidays_supplement : Hotels2BasePage
    {

        //public short getCurrentSupplier
        //{
        //    get
        //    {
        //        Product cProduct = new Product();
        //        cProduct = cProduct.GetProductById(int.Parse(this.qProductId));
        //        return cProduct.SupplierPrice;
        //    }
        //}

        //public string getCurrentSuppliertitle
        //{
        //    get
        //    {
        //        Product cProduct = new Product();
        //        cProduct = cProduct.GetProductById(int.Parse(this.qProductId));
        //        return cProduct.SupplierTitle;
        //    }
        //}
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.Page.IsPostBack)
            {
                Product cProduct = new Product();
                cProduct = cProduct.GetProductById(int.Parse(this.qProductId));
                Destitle.Text = cProduct.DestinationTitle;
                txthead.Text = cProduct.Title;

            }
        }

        

        
    }
}