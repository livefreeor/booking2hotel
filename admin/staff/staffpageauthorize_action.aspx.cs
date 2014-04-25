using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using Hotels2thailand.Suppliers;


namespace Hotels2thailand.UI
{
    public partial class admin_staffpageauthorize_action : Hotels2BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!this.Page.IsPostBack)
            {

                GetCatlist();

            }
            
        }

        public void GetCatlist()
        {
            Staff cStaff = new Staff();
            drop_staff_cat.DataSource = Staff.getCategoryByBlueHouseStaff();
            drop_staff_cat.DataValueField = "Key";
            drop_staff_cat.DataTextField = "Value";
            drop_staff_cat.DataBind();
        }

    }
}

