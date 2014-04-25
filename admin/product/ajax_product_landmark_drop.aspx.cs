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
    public partial class admin_ajax_product_landmark_drop : System.Web.UI.Page
    {
        public string qProductId
        {
            get { return Request.QueryString["pid"]; }
        }

        public string qProductCat
        {
            get { return Request.QueryString["pdcid"]; }
        }

        public string qLandMarkCat
        {
            get { return Request.QueryString["LanCat"]; }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qProductCat) && !string.IsNullOrEmpty(this.qLandMarkCat))
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
            REsult.Append("<select name=\"LandMark\" class=\"DropDownStyleCustom\" style=\"width:200px;\">");

            Landmark cLandMark = new Landmark();
            foreach (Landmark item in cLandMark.GetLanfmarkByCatAndDesId(byte.Parse(this.qLandMarkCat), DestinationId)) 
            {
                REsult.Append("<option value=\"" + item.LandmarkID + "\">" + item.Title + "</option>");
            }

            REsult.Append("</select> &nbsp;<a href=\"\" onclick=\"InsertNewLandmark();return false;\"><img src=\"../../images/plus.png\"></a>");
           
            return REsult.ToString();
        }
        
    }
}