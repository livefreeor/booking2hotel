using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.Staffs;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_product_content_Lang_switch : System.Web.UI.Page
    {
        public string qProductId
        {
            get { return Request.QueryString["pid"]; }
        }

        public string qProductCat
        {
            get { return Request.QueryString["pdcid"]; }
        }

        

        public string qLangId
        {
            get { return Request.QueryString["LangId"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qProductCat) && !string.IsNullOrEmpty(this.qLangId))
            {
                StaffSessionAuthorize cStaffSession = new StaffSessionAuthorize();
                bool IsCompleted = StaffSessionAuthorize.UpdateSessionLog(cStaffSession.CurrentCookieLog, byte.Parse(this.qLangId));
                
                Response.Write(IsCompleted);
                Response.End();
              
            }
            
        }

        
    }
}