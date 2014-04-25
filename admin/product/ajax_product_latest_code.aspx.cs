using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_product_latest_code : System.Web.UI.Page
    {
        public string qDestinationId
        {
            get { return Request.QueryString["desid"]; }
        }

        public string qProductCat
        {
            get { return Request.QueryString["pdcid"]; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.Page.IsPostBack)
            {

                Product cProduct = new Product();
                //Response.Write(this.qProductCat + "++"  + this.qDestinationId);
                //Response.End();
                Response.Write(cProduct.GetLatestProductCode(byte.Parse(this.qProductCat), short.Parse(this.qDestinationId)));
                Response.End();
            }
            
        }

        
    }
}