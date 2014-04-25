using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class ajax_product_option_holidays_sup_insert : System.Web.UI.Page
    {
        public string qProductId
        {
            get { return Request.QueryString["pid"]; }
        }

        public string qSupplierId
        {
            get { return Request.QueryString["supid"]; }
        }

        public short getCurrentSupplier
        {
            get
            {
                if (string.IsNullOrEmpty(this.qSupplierId))
                {
                    Product cProduct = new Product();
                    cProduct = cProduct.GetProductById(int.Parse(this.qProductId));
                    return cProduct.SupplierPrice; 
                }
                else
                {
                    return short.Parse(this.qSupplierId);
                }
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(this.qProductId))
            {

                ProductOptionListDataBind();
                chkOptionListDataBind();
            }
            
        }

        public void ProductOptionListDataBind()
        {
            Option cOption = new Option();
            short shrSupplierId = this.getCurrentSupplier;
            ProductOptionList.DataSource = cOption.GetProductOptionByCurrentSupplierANDProductId(shrSupplierId, int.Parse(this.qProductId));
            ProductOptionList.DataTextField = "Title";
            ProductOptionList.DataValueField = "OptionID";

            ProductOptionList.DataBind();
            ListItem NewListdefault = new ListItem("Please Select Room Type", "none");
            ListItem NewList = new ListItem("All Option", "0");
            ProductOptionList.Items.Insert(0, NewListdefault);
            ProductOptionList.Items.Insert(1, NewList);


        }

        public void chkOptionListDataBind()
        {
            short shrSupplierId = this.getCurrentSupplier;
            Option cOption = new Option();
            chkOptionList.DataSource = cOption.GetProductOptionByCurrentSupplierANDProductId(shrSupplierId, int.Parse(this.qProductId));
            chkOptionList.DataTextField = "Title";
            chkOptionList.DataValueField = "OptionID";
            chkOptionList.DataBind();

        }

        
    }
}