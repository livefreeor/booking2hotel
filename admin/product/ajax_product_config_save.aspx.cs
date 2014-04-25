using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_product_config_save : System.Web.UI.Page
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
                Product cProduct = new Product();
                int intProductId = int.Parse(this.qProductId);
                short StatusProCess = short.Parse(Request.Form["drpstatusProcess"]);
                bool bolFlag = false;
                bool bolStatus = true;

                if (Request.Form["rbrec"] == "rbStatusflag")
                    bolFlag = true;

                if (Request.Form["rbStatus"] == "rbStatusDisable")
                    bolStatus = false;
                
               bool IsCompleted =  cProduct.UpdateProductConfiguration(intProductId, StatusProCess, bolFlag, bolStatus);

               Response.Write(IsCompleted);
               Response.End();
              
            }
            
        }

        
    }
}