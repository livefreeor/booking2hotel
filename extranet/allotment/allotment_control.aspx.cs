using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.ProductOption;

namespace Hotels2thailand.UI
{
    public partial class extranet_allotment_allotment_control : Hotels2BasePageExtra
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                dropOptionDataBind();
                dropAllotDataBind();
            }
        }

        public void dropOptionDataBind()
        {
            Option cOption = new Option();

            List<object> ListRoom = cOption.GetProductOptionByProductId_RoomOnlyExtranet(this.CurrentProductActiveExtra, this.CurrentSupplierId);
            //dropOption.DataSource = ListRoom;
            //dropOption.DataTextField = "Title";
            //dropOption.DataValueField = "OptionID";
            //dropOption.DataBind();

            ListItem newList = new ListItem("All Room Type", "0");
            //dropOption.Items.Insert(0, newList);


            dropOptionInsert.DataSource = ListRoom;
            dropOptionInsert.DataTextField = "Title";
            dropOptionInsert.DataValueField = "OptionID";
            dropOptionInsert.DataBind();

            dropOptionInsert.Items.Insert(0, newList);

            chkRoom.DataSource = ListRoom;
            chkRoom.DataTextField = "Title";
            chkRoom.DataValueField = "OptionID";
            chkRoom.DataBind();
        }
        

        public void dropAllotDataBind()
        {
            dropCutoff.DataSource = this.dicGetNumberstart0(90);
            dropCutoff.DataTextField = "Value";
            dropCutoff.DataValueField = "Key";
            dropCutoff.DataBind();

            dropAllot.DataSource = this.dicGetNumberstart0(50);
            dropAllot.DataTextField = "Value";
            dropAllot.DataValueField = "Key";
            dropAllot.DataBind();
        }
    }
}
    