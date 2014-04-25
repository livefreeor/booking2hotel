using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Production;
using System.Text;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_condition_minimum_save : Hotels2BasePageExtra_Ajax
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (this.Page.IsPostBack)
            {
                if (this.IsStaffAdd())
                {

                    Response.Write(InsertNewMinimumNight());
                    //Response.Write("HELLO");
                }
                else
                {
                    Response.Write("method_invalid");
                }
                Response.End();
                
            }
        }


        public string InsertNewMinimumNight()
        {
            string Iscompleted = "False";

            try
            {

                DateTime dDateStart = Request.Form["hd_min_date_start"].Hotels2DateSplitYear("-");
                DateTime dDateend = Request.Form["hd_min_date_end"].Hotels2DateSplitYear("-");
                byte MinAmount = byte.Parse(Request.Form["min_day_amount"]);

                int ConditionId = int.Parse(Request.Form["ctl00$ContentPlaceHolder1$hdConditionID"]);

                ConditionminimumDayExtranet cConditionMin = new ConditionminimumDayExtranet();
                cConditionMin.InsertMinimumstay(this.CurrentProductActiveExtra, ConditionId, dDateStart, dDateend, MinAmount);
                Iscompleted = "True";
            }
            catch(Exception ex)
            {
                Response.Write("error: " + ex.Message);
                Response.End();
            }

            return Iscompleted;
        }
    }
}