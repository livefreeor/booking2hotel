using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.ProductOption;

namespace Hotels2thailand.UI
{
    public partial class extranet_allotment_allotment_edit : Hotels2BasePageExtra
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                dropOptionDataBind();
                
            }
        }

        public void dropOptionDataBind()
        {
            Option cOption = new Option();

            List<object> ListRoom = cOption.GetProductOptionByProductId_RoomOnlyExtranet(this.CurrentProductActiveExtra, this.CurrentSupplierId);
            

            ListItem newList = new ListItem("All Room Type", "0");



            dropOption.DataSource = ListRoom;
            dropOption.DataTextField = "Title";
            dropOption.DataValueField = "OptionID";
            dropOption.DataBind();

            dropOption.Items.Insert(0, newList);

            chkRoom.DataSource = ListRoom;
            chkRoom.DataTextField = "Title";
            chkRoom.DataValueField = "OptionID";
            chkRoom.DataBind();
        }
        

        
    }
}
    