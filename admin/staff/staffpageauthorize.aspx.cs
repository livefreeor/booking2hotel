using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using Hotels2thailand.Suppliers;

using System.Text;
using System.IO;

namespace Hotels2thailand.UI
{
    public partial class admin_staffpageauthorize : Hotels2BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!this.Page.IsPostBack)
            {

                StaffPageAuthorize cPage = new StaffPageAuthorize();
                dropMainModule.DataSource = cPage.getdicStaffModuleMain();
                dropMainModule.DataTextField = "Value";
                dropMainModule.DataValueField = "Key";

                dropMainModule.DataBind();

               
                



                DropModuleDataBind();

            }
            
        }

        public void dropMainModule_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropModuleDataBind();
        }

        public void DropModuleDataBind()
        {
            byte bytMainModule = byte.Parse(dropMainModule.SelectedValue);
            StaffPageAuthorize cPage = new StaffPageAuthorize();
            dropModule.DataSource = cPage.getdicStaffModule(bytMainModule);
            dropModule.DataTextField = "Value";
            dropModule.DataValueField = "Key";

            dropModule.DataBind();

            //ListItem newitem = new ListItem("root", "0");
            //dropModule.Items.Insert(0, newitem);
        }


    }
}

