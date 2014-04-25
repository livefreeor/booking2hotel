using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Reviews;
using System.Text;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_staff_logout : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                
                Hotels2thailand.Staffs.StaffSessionAuthorize.Logout();
            }
         }

    }
}