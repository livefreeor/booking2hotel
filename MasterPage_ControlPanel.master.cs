using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.UI;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI.MasterPage
{
    public partial class Masterpage_MasterPage_Control_Panel : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Page.ClientScript.RegisterClientScriptInclude(Page.GetType(), null, "../Scripts/lert.js");
            if (!string.IsNullOrEmpty(Request.QueryString["pdcid"]))
            {
                //ProductCategory cProductCat = new ProductCategory();
                //lblProductCat.Text = cProductCat.getProductCatTitle(byte.Parse(Request.QueryString["pdcid"]));
            }
        }
    }
}
