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
    public partial class admin_ajax_product_landmark_insert : System.Web.UI.Page
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
                //Response.Write(Request.Url.ToString());
                //Response.End();
                string REsult = string.Empty;
                ProductLandmark cProductLandMark = new ProductLandmark();
                if (cProductLandMark.IsHaveRecord(int.Parse(this.qProductId), int.Parse(this.qLanmarkId)) > 0)
                {
                    REsult = "have";
                }
                else
                {
                    int ret = cProductLandMark.InsertProductLandMarkFirst(int.Parse(this.qProductId), int.Parse(this.qLanmarkId));
                    REsult = "ok";
                }

                Response.Write(REsult);
                Response.End();
            }
            
        }
        

       
        
    }
}