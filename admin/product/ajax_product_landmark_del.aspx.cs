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
    public partial class admin_ajax_product_landmark_del : System.Web.UI.Page
    {
        public string qProductId
        {
            get { return Request.QueryString["pid"]; }
        }

        public string qProductCat
        {
            get { return Request.QueryString["pdcid"]; }
        }

        public string qLanmarkId
        {
            get { return Request.QueryString["landMarkId"]; }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qProductCat) && !string.IsNullOrEmpty(this.qLanmarkId))
            {
                ProductLandmark cProductLandMark = new ProductLandmark();
                Response.Write(cProductLandMark.DeleteLandMark(int.Parse(this.qProductId), int.Parse(this.qLanmarkId)));
                Response.End();
            }
            
        }

       

        
        
    }
}