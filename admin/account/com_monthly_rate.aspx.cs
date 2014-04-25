using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Account;

namespace Hotels2thailand.UI
{
    public partial class admin_account_com_monthly_rate : Hotels2BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                monthly_hotelList cmonthly = new monthly_hotelList();
                IList<object> iListRegular = cmonthly.getProductListComMonthly(1);
                IList<object> iListAdvance = cmonthly.getProductListComMonthly(2);

                gvhotelListregular.DataSource = cmonthly.getProductListComMonthly(1);
                gvhotelListregular.DataBind();
                if(iListRegular.Count > 0)
                    gvhotelListregular.HeaderRow.TableSection = TableRowSection.TableHeader;

                GvAdvance.DataSource = iListAdvance;
                GvAdvance.DataBind();
                if(iListAdvance.Count > 0)
                    GvAdvance.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        protected void GvAdvance_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDateSubmit = e.Row.Cells[5].FindControl("lblDAte") as Label;
                DateTime? LatestDAte = (DateTime?)DataBinder.Eval(e.Row.DataItem, "LatestInvoid");
                if (LatestDAte.HasValue)
                    lblDateSubmit.Text = ((DateTime)LatestDAte).ToString("dd MMM yyyy");
                else
                    lblDateSubmit.Text = "No Payment";
            }
        }
        protected void gvhotelListregular_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDateSubmit = e.Row.Cells[5].FindControl("lblDAte") as Label;
                DateTime? LatestDAte = (DateTime?)DataBinder.Eval(e.Row.DataItem, "LatestInvoid");
                if (LatestDAte.HasValue)
                    lblDateSubmit.Text = ((DateTime)LatestDAte).ToString("dd MMM yyyy");
                else
                    lblDateSubmit.Text = "No Payment";
            }
        }
}
}