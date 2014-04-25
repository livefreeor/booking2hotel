using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class admin_product_product_policy : Hotels2BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Product cProduct = new Product();
                cProduct = cProduct.GetProductById(int.Parse(this.qProductId));
                Destitle.Text = cProduct.DestinationTitle;
                txthead.Text = cProduct.Title;

                dropPolicyCatDataBind();

                GVpolicyCatDataBind();

                lnkCreate.NavigateUrl = lnkCreate.NavigateUrl + "?pid=" + this.qProductId + "&pdcid=" + this.qProductCat;
            }
        }

        public void dropPolicyCatDataBind()
        {
            dropPolicyCat.DataSource = ProductPolicyAdmin.getPolicyCategoryall();
            dropPolicyCat.DataTextField = "Value";
            dropPolicyCat.DataValueField = "Key";
            dropPolicyCat.DataBind();
        }

        public void dropPolicyCat_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GVpolicyCatDataBind();
        }

        public void GVpolicyCatDataBind()
        {
            GVpolicyCat.DataSource = ProductPolicyAdmin.getPolicyTypeIsHaveRecord(int.Parse(this.qProductId), byte.Parse(dropPolicyCat.SelectedValue));
            GVpolicyCat.DataBind();
        }
        public void GVpolicyCat_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ProductPolicyAdmin cProductPolicy = new ProductPolicyAdmin();
                GridView GvPolicy = e.Row.Cells[0].FindControl("GVpolicyList") as GridView;

                byte bytTypeId = (byte)GVpolicyCat.DataKeys[e.Row.RowIndex].Value;

                GvPolicy.DataSource = cProductPolicy.GetProductPolicyByProductANDCatIDANDTypId(int.Parse(this.qProductId), byte.Parse(dropPolicyCat.SelectedValue), bytTypeId);
                GvPolicy.DataBind();
            }
        }

        public void GVpolicyList_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink cLinkTitle = e.Row.Cells[1].FindControl("hkTitle") as HyperLink;
                Label lblPeriod = e.Row.Cells[1].FindControl("lblPolicyPeriod") as Label;

                DateTime dStart = (DateTime)DataBinder.Eval(e.Row.DataItem, "DateStart");
                DateTime dEnd = (DateTime)DataBinder.Eval(e.Row.DataItem, "DateEnd");
                lblPeriod.Text = dStart.ToString("dd-MMM-yyyy") + "&nbsp;&nbsp;-&nbsp;&nbsp;" + dEnd.ToString("dd-MMM-yyyy");
                cLinkTitle.NavigateUrl = cLinkTitle.NavigateUrl + "&pid=" + this.qProductId + "&pdcid=" + this.qProductCat;
                Label lblNumpolicy = e.Row.Cells[0].FindControl("lblNumpolicy") as Label; 
                bool Status = (bool)DataBinder.Eval(e.Row.DataItem, "Status");
                if (!Status)
                {
                    cLinkTitle.CssClass = "linkDisable";
                    lblNumpolicy.CssClass = "linkDisable";
                }
            }
        }


    }
}