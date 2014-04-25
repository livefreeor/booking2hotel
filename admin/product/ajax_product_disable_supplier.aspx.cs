using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Hotels2thailand.Production;
using Hotels2thailand;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_product_disable_supplier : System.Web.UI.Page
    {
        public string qSupplierId
        {
            get { return Request.QueryString["supid"]; }
        }

        public string qProductId
        {
            get { return Request.QueryString["pid"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.qSupplierId) && !string.IsNullOrEmpty(this.qProductId))
                {
                    //Response.Write(this.qSupplierId + "<br/>");
                    //Response.Write(this.qProductId);
                    //Response.End();
                    ProductSupplier cProductSupplier = new ProductSupplier();
                    Response.Write(cProductSupplier.UpdateStatusProductSupplier(short.Parse(this.qSupplierId), int.Parse(this.qProductId)));
                    Response.End();

                }
            }
            
        }
        


        
    }
}