using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class extranet_load_tariff : Hotels2BasePageExtra
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.Page.IsPostBack)
            {
                dropRoomDataBind();
                ConditionDataBind();
                dropConditiontitleDataBind();
            }
        }

        public void dropConditiontitleDataBind()
        {
            ProductConditionNameExtra cConditionTitle = new ProductConditionNameExtra();
            conditionTitle.DataSource = cConditionTitle.GetConditionNameList(1);
            conditionTitle.DataTextField = "Title";
            conditionTitle.DataValueField = "ConditionNameId";
            conditionTitle.DataBind();

        }

        public void dropRoomDataBind()
        {
            Option cOption = new Option();

            dropRoom.DataSource = cOption.GetProductOptionByProductId_RoomOnlyExtranet(this.CurrentProductActiveExtra, this.CurrentSupplierId);
            dropRoom.DataTextField = "Title";
            dropRoom.DataValueField = "OptionID";
            dropRoom.DataBind();

        }

        
        public void ConditionDataBind()
        {
            drop_adult.DataSource = this.dicGetNumber(20);
            drop_adult.DataTextField = "Value";
            drop_adult.DataValueField = "Key";
            drop_adult.DataBind();

            drop_child.DataSource = this.dicGetNumberstart0(10);
            drop_child.DataTextField = "Value";
            drop_child.DataValueField = "Key";
            drop_child.DataBind();
            drop_child.SelectedValue = "1";

            drop_extrabed.DataSource = this.dicGetNumberstart0(4);
            drop_extrabed.DataTextField = "Value";
            drop_extrabed.DataValueField = "Key";
            drop_extrabed.DataBind();
            drop_extrabed.SelectedValue = "1";


            drop_breakfast.DataSource = this.dicGetNumberstart0(20);
            drop_breakfast.DataTextField = "Value";
            drop_breakfast.DataValueField = "Key";
            drop_breakfast.DataBind();
            drop_breakfast.SelectedValue = "1";

            drop_breakfast.Items.RemoveAt(0);
            ListItem newList = new ListItem("Room only","0");
            drop_breakfast.Items.Insert(0, newList);
            
        }

        
       
    }
}