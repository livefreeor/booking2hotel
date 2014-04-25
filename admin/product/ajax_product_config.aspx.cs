using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_product_config : System.Web.UI.Page
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
                Status cStatus = new Status();
                cProduct = cProduct.GetProductById(int.Parse(this.qProductId));
                if (cProduct.FlagFeature)
                {
                    rbStatusflag.Checked = true;
                    rbStatusunflag.Checked = false;
                }
                else
                {
                    rbStatusflag.Checked = false;
                    rbStatusunflag.Checked = true;
                }

                if (cProduct.Status)
                {
                    rbStatusEnable.Checked = true;
                    rbStatusDisable.Checked = false;
                }
                else
                {
                    rbStatusEnable.Checked = false;
                    rbStatusDisable.Checked = true;
                }

                drpstatusProcess.DataSource = cStatus.GetStatusByCatId(1);
                drpstatusProcess.DataTextField = "Value";
                drpstatusProcess.DataValueField = "Key";
                drpstatusProcess.DataBind();
                drpstatusProcess.SelectedValue = cProduct.StatusID.ToString();
            }
            
        }

        
    }
}