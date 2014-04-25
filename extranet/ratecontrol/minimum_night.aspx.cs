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
    public partial class extranet_mimimum_night : Hotels2BasePageExtra
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.Page.IsPostBack)
            {
                int ProductID = this.CurrentProductActiveExtra;


                Option cOption = new Option();
                dropOptionList.DataSource = cOption.GetProductOptionByProductId_RoomOnlyExtranet(ProductID, this.CurrentSupplierId);
                dropOptionList.DataTextField = "Title";
                dropOptionList.DataValueField = "OptionID";
                dropOptionList.DataBind();

                //int OptionId = int.Parse(dropOptionList.SelectedValue);

                //ProductConditionExtra cConditionextra = new ProductConditionExtra();
                //GVConditionList.DataSource = cConditionextra.getConditionListByOptionId(OptionId,1);
                //GVConditionList.DataBind();

            }
           
        }

    }
}