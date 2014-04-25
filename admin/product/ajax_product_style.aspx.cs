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
    public partial class admin_ajax_product_style : System.Web.UI.Page
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
                Response.Write(LocationResult());
                Response.End();
                
            }
            
        }

        public string LocationResult()
        {
            TypeProduct cTypeProduct = new TypeProduct();
            StringBuilder REsult = new StringBuilder();
            REsult.Append("<h4>Product Style &nbsp;<a href=\"\" onclick=\"InsertNewStyle();return false;\"><img src=\"../../images/plus.png\"></a></h4>");
            REsult.Append("<ul>");


            foreach (TypeProduct item in cTypeProduct.getTypeProductCheckedListByProdutId(int.Parse(this.qProductId)))
            {
                REsult.Append("<li>");
                REsult.Append(item.TypeTitle);
                REsult.Append("</li>");
            }
            
            REsult.Append("</ul>");
           
            return REsult.ToString();
        }
        
    }
}