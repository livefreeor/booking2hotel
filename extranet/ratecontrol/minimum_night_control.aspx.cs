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
    public partial class extranet_minimum_night_control : Hotels2BasePageExtra
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.Page.IsPostBack)
            {

                if (!string.IsNullOrEmpty(this.qConditionId) && !string.IsNullOrEmpty(this.qOptionId))
                {
                    Option cOptionId = new Option();
                    lblTitle.Text = cOptionId.getOptionById(int.Parse(this.qOptionId)).Title;

                    int intConditionId = int.Parse(this.qConditionId.Hotels2DecryptedData_SecretKey().Hotels2RightCrl(20));

                    hdConditionID.Value = intConditionId.ToString();

                    ProductConditionExtra cConditionExtra = new ProductConditionExtra();

                    cConditionExtra = cConditionExtra.getConditionByConditionId(intConditionId);


                    //ConditionDataBind();

                    lblTitleCondition.Text = cConditionExtra.Title + Hotels2String.AppendConditionDetailExtraNet(cConditionExtra.NumAult, cConditionExtra.Breakfast);


                }
            }
           
        }

    }
}