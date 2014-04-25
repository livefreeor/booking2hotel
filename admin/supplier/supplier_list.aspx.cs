using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Suppliers;

namespace Hotels2thailand.UI
{
    public partial class admin_supplier_supplier_list : Hotels2BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                txtSearch.Text = string.Empty;
                GridSupplierDataBind();
            }
            

        }
        public void GridSupplierDataBind()
        {
            Supplier cSupplier = new Supplier();

            bool Status = true;
            if (dropStatus.SelectedValue == "0")
                Status = false;

            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                gridSupplier.DataSource = cSupplier.getListSupplierAllbyStatus(Status);
                gridSupplier.DataBind();
            }
            else
            {
                //Response.Write("TEST");
                //Response.End();
                gridSupplier.DataSource = cSupplier.getListSupplierAllAdVancesearch(txtSearch.Text);
                gridSupplier.DataBind();
            }
        }

        public void gridSupplier_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Image imgStatus = e.Row.Cells[4].FindControl("imgStatus") as Image;
                bool Status = (bool)DataBinder.Eval(e.Row.DataItem, "Status");

                if (!Status)
                {
                    imgStatus.ImageUrl = "../../images/false.png";
                }
            }
        }

        //<asp:ObjectDataSource ID="objSupplier" runat="server" SelectMethod="getListSupplierAll" TypeName="Hotels2thailand.Suppliers.Supplier"></asp:ObjectDataSource>DataSourceID="objSupplier"
        protected void gridSupplier_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridSupplier.PageIndex = e.NewPageIndex;
            gridSupplier.DataBind();

        }

        public void dropStatus_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            GridSupplierDataBind();
        }

        public void txtSearch_OnClick(object sender, EventArgs e)
        {
            GridSupplierDataBind();
        }
    }
}