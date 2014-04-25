using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Suppliers;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class admin_extranet_extranethotelList : Hotels2BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {

                Destination cDestination = new Destination();
                dropDesExtranet.DataSource = cDestination.GetDestinationExtranetOnly();
                dropDesExtranet.DataTextField = "Value";
                dropDesExtranet.DataValueField = "Key";
                dropDesExtranet.DataBind();

                dropDesExtranet.SelectedValue = "30";
            }
        }


    }
}