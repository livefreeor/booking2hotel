using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using Hotels2thailand;
using Hotels2thailand.Booking;
using Hotels2thailand.DataAccess;

namespace Hotels2thailand.UI
{
    public partial class admin_account_currency_edit : Hotels2BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                string sqlList = "";
                string strPeriodList = "";

                sqlList="select currency_log_id, date_start, date_end from tbl_currency_log order by currency_log_id desc";

                DataConnect currency = new DataConnect();

                SqlDataReader currencyList = currency.GetDataReader(sqlList);

                while (currencyList.Read())
                {
                    strPeriodList = strPeriodList + "<li><a href='currency_show.aspx?cid=" + currencyList["currency_log_id"] + "' target='_blank'>" + currencyList["date_start"] + "-" + currencyList["date_end"] + "</a></li>";
                }

                currencyList.Close();

                period_list.Text = strPeriodList;

                // Gv Bind
                GVCurrencyDataBind();
            }
        }

        public void GVCurrencyDataBind()
        {
            Currency cCurrency = new Currency();
            GVCurrency.DataSource = cCurrency.GetCurrencyAll();
            GVCurrency.DataBind();
        }

        public void btnSaveEdit_OnClick(object sender, EventArgs e)
        {
            Currency cCurrency = new Currency();
            int logId = cCurrency.InsertCurrencyLog();

            foreach (GridViewRow GvRow in GVCurrency.Rows)
            {
                if (GvRow.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtPrefix = GVCurrency.Rows[GvRow.RowIndex].Cells[3].FindControl("txtPrefix") as TextBox;
                    byte CurrencyId = (byte)GVCurrency.DataKeys[GvRow.RowIndex].Value;
                    
                    //cCurrency.GetCurrencyById(CurrencyId);
                    //if(cCurrency.Prefix != float.Parse(txtPrefix.Text))
                    //{
                        cCurrency.UpdateCurrencyById(CurrencyId,  float.Parse(txtPrefix.Text));
                        cCurrency.InsertLog(logId, CurrencyId);
                    //}
                }
            }

            Response.Redirect(Request.Url.ToString());

        }
        
    }
}