using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;

namespace Hotels2thailand.UI
{
    public partial class mainextra : Hotels2BasePageExtra
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                
                staff_id.Value = this.CurrentStaffId.ToString();
                
            }
        }
    }
}