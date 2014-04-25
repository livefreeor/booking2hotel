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
    public partial class extranet_Condition_Control : Hotels2BasePageExtra
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.Page.IsPostBack)
            {
                int ProductID = this.CurrentProductActiveExtra;


                //Response.Write(this.CurrentSupplierId);
                //Response.End();
                Option cOption = new Option();
                dropOptionList.DataSource = cOption.GetProductOptionByProductId_RoomOnlyExtranet(ProductID,this.CurrentSupplierId);
                dropOptionList.DataTextField = "Title";
                dropOptionList.DataValueField = "OptionID";
                dropOptionList.DataBind();

                //int OptionId = int.Parse(dropOptionList.SelectedValue);

                //ProductConditionExtra cConditionextra = new ProductConditionExtra();
                //GVConditionList.DataSource = cConditionextra.getConditionListByOptionId(OptionId,1);
                //GVConditionList.DataBind();

            }
        }


        //public void GVConditionList_OnRowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        int intConditionID = (int)GVConditionList.DataKeys[e.Row.DataItemIndex].Value;

        //        HyperLink hpManage = e.Row.Cells[1].FindControl("hlConditionManage") as HyperLink;

        //        string endCode = intConditionID.ToString() + Hotels2String.Hotels2RandomStringNuM(20);

        //        hpManage.NavigateUrl = "condition_manage.aspx?cdid=" + endCode.Hotel2EncrytedData_SecretKey();
        //    }
        //}

        //public void dropOptionList_OnSelectIndexChange(object sender, EventArgs e)
        //{
            

        //    int OptionId = int.Parse(dropOptionList.SelectedValue);

        //    ProductConditionExtra cConditionextra = new ProductConditionExtra();
        //    GVConditionList.DataSource = cConditionextra.getConditionListByOptionId(OptionId,1);
        //    GVConditionList.DataBind();
        //}
        
       
    }
}