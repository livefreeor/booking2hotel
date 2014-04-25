using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;

using System.Text;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_staff_user_check : Hotels2BasePageExtra_Ajax
    {
        public string qStaffUser
        {
            get { return Request.QueryString["user"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!this.Page.IsPostBack)
            {
                
                StaffExtra cStaff = new StaffExtra();

                int IsDuplicatUser = cStaff.CheckStaffUser(this.qStaffUser.Trim(), this.CurrentSupplierId);
                    
                if(IsDuplicatUser > 0)
                    Response.Write("userone");
                else
                    Response.Write("nouser");
                
                Response.End();
                
            }
        }


        
    }
}