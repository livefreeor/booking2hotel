using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.Suppliers;
using Hotels2thailand.Staffs;

namespace Hotels2thailand.UI
{
    public partial class admin_extranet_extranetManage : Hotels2BasePage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qSupplierId))
                {
                    GvCommssionDataBind();
                }
            }
        }



        public void GvCommssionDataBind()
        {
            ProductCommission cProductCom = new ProductCommission();
            GvCommssionPeriod.DataSource = cProductCom.GetCommissionBySuppierIdAndProductID(short.Parse(this.qSupplierId), int.Parse(this.qProductId));
            GvCommssionPeriod.DataBind();
        }

        public void GvCommssionPeriod_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DateTime dDateStart = (DateTime)DataBinder.Eval(e.Row.DataItem, "DateStart");
                DateTime dDateEnd = (DateTime)DataBinder.Eval(e.Row.DataItem, "DateEnd");

                Hotels2thailand.UI.Controls.Control_DatepickerCalendar controlDatePickers = e.Row.Cells[1].FindControl("DateTimePicker") as Hotels2thailand.UI.Controls.Control_DatepickerCalendar;
                controlDatePickers.DateStart = dDateStart;
                controlDatePickers.DateEnd = dDateEnd;
                controlDatePickers.DataBind();
            }
        }

        public void GvCommssionPeriod_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "periodupdate")
            {
                //int intPeriodId = int.Parse(e.CommandArgument.ToString());

                string[] param = e.CommandArgument.ToString().Split(Convert.ToChar("&"));
                int intComId  = int.Parse(param[0]);
                int index = int.Parse(param[1]);
                Hotels2thailand.UI.Controls.Control_DatepickerCalendar controlDatePickers = GvCommssionPeriod.Rows[index].Cells[1].FindControl("DateTimePicker") as Hotels2thailand.UI.Controls.Control_DatepickerCalendar;

                TextBox txtCom = GvCommssionPeriod.Rows[index].Cells[1].FindControl("txtCom") as TextBox;
                ProductCommission cProductCom = new ProductCommission();
                cProductCom.UpdateProductcommissionbyCommissionId(intComId, int.Parse(this.qProductId), controlDatePickers.GetDatetStart, controlDatePickers.GetDatetEnd,
                    byte.Parse(txtCom.Text));
                
            }

            Response.Redirect(Request.Url.ToString());
        }

        public void btPeriodSubmit_OnClick(object sender, EventArgs e)
        {
            
            ProductCommission cproduct = new ProductCommission();
            cproduct.Insertnewcommission(int.Parse(this.qProductId), short.Parse(this.qSupplierId), DateTimePicker.GetDatetStart, 
                DateTimePicker.GetDatetEnd,byte.Parse(txtCom.Text));

            Response.Redirect(Request.Url.ToString());
            


        }
    }
}