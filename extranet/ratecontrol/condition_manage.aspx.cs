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
    public partial class extranet_condition_manage : Hotels2BasePageExtra
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.Page.IsPostBack)
            {

                if (!string.IsNullOrEmpty(this.qConditionId) && !string.IsNullOrEmpty(this.qOptionId))
                {
                    Option cOptionId = new Option();
                    lblTitle.Text = cOptionId.getOptionById(int.Parse(this.qOptionId)).Title;


                    drop_adult.DataSource = this.dicGetNumber(20);
                    drop_adult.DataTextField = "Value";
                    drop_adult.DataValueField = "Key";
                    drop_adult.DataBind();

                    drop_child.DataSource = this.dicGetNumberstart0(10);
                    drop_child.DataTextField = "Value";
                    drop_child.DataValueField = "Key";
                    drop_child.DataBind();

                    drop_extrabed.DataSource = this.dicGetNumberstart0(4);
                    drop_extrabed.DataTextField = "Value";
                    drop_extrabed.DataValueField = "Key";
                    drop_extrabed.DataBind();


                    drop_breakfast.DataSource = this.dicGetNumberstart0(20);
                    drop_breakfast.DataTextField = "Value";
                    drop_breakfast.DataValueField = "Key";
                    drop_breakfast.DataBind();

                    drop_breakfast.Items.RemoveAt(0);
                    ListItem newList = new ListItem("Room only", "0");
                    drop_breakfast.Items.Insert(0, newList);

                    
                    int intConditionId = int.Parse(this.qConditionId.Hotels2DecryptedData_SecretKey().Hotels2RightCrl(20));

                    ProductConditionExtra cConditionExtra = new ProductConditionExtra();

                    cConditionExtra = cConditionExtra.getConditionByConditionId(intConditionId);



                    lblTitleCondition.Text = cConditionExtra.Title + Hotels2String.AppendConditionDetailExtraNet(cConditionExtra.NumAult, cConditionExtra.Breakfast);
                    hd_ConditionName.Value = cConditionExtra.ConditionNameId.ToString();

                    drop_adult.SelectedValue = cConditionExtra.NumAult.ToString();
                    drop_child.SelectedValue = cConditionExtra.NumChild.ToString();
                    drop_breakfast.SelectedValue = cConditionExtra.Breakfast.ToString();
                    drop_extrabed.SelectedValue = cConditionExtra.NumExtra.ToString();


                    hd_ConditionID.Value = intConditionId.ToString();

                    //ScriptManager.RegisterStartupScript(this, Page.GetType(), "1", "<script>getPolicyList('" + intConditionId + "');</script>", false);

                    //ScriptManager.RegisterStartupScript(this, Page.GetType(), "2", "<script>getCancelList('" + intConditionId + "');</script>", false);

                    
                    //ScriptManager.RegisterStartupScript(this, Page.GetType(), "3", "<script>SaveCondition('" + intConditionId + "');</script>", false);

                    //ScriptManager.RegisterStartupScript(this, Page.GetType(), "4", "<script>resetCondition('" + intConditionId + "');</script>", false);
                    //drop_breakfast.SelectedValue = cConditionExtra.Breakfast.ToString();


                    //PolicyDataBind(intConditionId);

                    //GvcancelPeriodDataBind(intConditionId);
                }
            }
        }


        
    }
}