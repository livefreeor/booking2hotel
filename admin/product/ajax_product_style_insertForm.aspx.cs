using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_product_style_insertForm : System.Web.UI.Page
    {
        public string qProductId
        {
            get { return Request.QueryString["pid"]; }
        }

        public string qProductCat
        {
            get { return Request.QueryString["pdcid"]; }
        }

       
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qProductCat))
            {
                ProductType cProductType = new ProductType();

                Product_style_list.DataSource = cProductType.GetProductTypeAllByProductCat(byte.Parse(this.qProductCat));
                Product_style_list.DataTextField = "Value";
                Product_style_list.DataValueField = "Key";
                Product_style_list.DataBind();

                TypeProduct cTypeProduct = new TypeProduct();

                List<object> ListItem = cTypeProduct.getTypeProductCheckedListByProdutId(int.Parse(this.qProductId));
                //List<object> ListItem = cProductLocation.getLocationListByProductId(int.Parse(this.qProductId));

                foreach (ListItem chkitem in Product_style_list.Items)
                {
                    chkitem.Attributes.Add("id", chkitem.Value);
                    foreach (TypeProduct item in ListItem)
                    {
                        if (chkitem.Value == item.TypeID.ToString())
                        {
                            chkitem.Selected = true;
                        }
                    }
                    
                }
            }
            
        }
        

       
        
    }
}