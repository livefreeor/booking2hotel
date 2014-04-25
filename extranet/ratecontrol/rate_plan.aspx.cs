using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class extranet_rate_plan : Hotels2BasePageExtra
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.Page.IsPostBack)
            {
                Country cCountry = new Country();
                listCountry.DataSource = cCountry.GetCountryExtranet(this.CurrentSupplierId, this.CurrentProductActiveExtra);
                listCountry.DataTextField = "Title";
                listCountry.DataValueField = "CountryID";
                listCountry.DataBind();
            }
           
        }

    }
}