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
    public partial class admin_ajax_product_landmark_cat_drop : System.Web.UI.Page
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
                Response.Write(DropResult());
                Response.End();
            }
            
        }

        public string DropResult()
        {
            Product cProduct = new Product();
            short DestinationId = cProduct.GetProductById(int.Parse(this.qProductId)).DestinationID;
            StringBuilder REsult = new StringBuilder();
            REsult.Append("<select name=\"Landmark_cat\" onchange=\"LandMarkCat_drop();\" class=\"DropDownStyleCustom\" style=\"width:200px;\">");
           
            foreach (KeyValuePair<byte, string> item in LandmarkCategory.getAllLandmarkCategory())
            {
                REsult.Append("<option value=\"" + item.Key+ "\">"+ item.Value+ "</option>");
            }

            REsult.Append("</select>");
           
            return REsult.ToString();
        }
        
    }
}