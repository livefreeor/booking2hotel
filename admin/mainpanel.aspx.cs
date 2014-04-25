using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;
using Hotels2thailand.Staffs;

namespace Hotels2thailand.UI
{
    public partial class mainpanel : Hotels2BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {

                if (this.SessionId == 12)
                {
                    panel_account.Visible = false;
                    panelRd.Visible = false;
                }
            }
        }
    }
}

