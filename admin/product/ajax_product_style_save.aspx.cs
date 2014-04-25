using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_product_style_save : System.Web.UI.Page
    {
        public string qProductId
        {
            get { return Request.QueryString["pid"]; }
        }

        public string qProductCat
        {
            get { return Request.QueryString["pdcid"]; }
        }

        public string qLocationChecked
        {
            get { return Request.QueryString["CheckedLoc"]; }
        }
       
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qProductCat))
            {
                bool Iscompleted = true;
               
                TypeProduct cTypeProduct = new TypeProduct();
                if (!string.IsNullOrEmpty(this.qLocationChecked))
                {
                    
                    int ret = 0;
                    string[] arrCheck = this.qLocationChecked.Hotels2RightCrl(1).Split(',');
                    cTypeProduct.DeleteProductTypeNotInCustomCheck(int.Parse(this.qProductId), this.qLocationChecked.Hotels2RightCrl(1));

                    foreach (string Item in arrCheck)
                    {
                        if (cTypeProduct.IsHaveProductType(byte.Parse(Item), int.Parse(this.qProductId)) != 1)
                        {
                            ret = cTypeProduct.InsertProductStyle(byte.Parse(Item), int.Parse(this.qProductId));

                        }
                    }
                }
                else
                {
                    cTypeProduct.DeleteProductTypeAllByProductId(int.Parse(this.qProductId));

                }

                Response.Write(Iscompleted);
                Response.End();
            }
            
        }

        
        
    }
}