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
    public partial class admin_ajax_deposit_save : Hotels2BasePageExtra_Ajax
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (this.Page.IsPostBack)
            {
                if (this.IsStaffAdd())
                {

                    Response.Write(InsertNewDeposit());
                    //Response.Write("HELLO");
                }
                else
                {
                    Response.Write("method_invalid");
                }
                Response.End();
                
            }
        }


        public string InsertNewDeposit()
        {
            string Iscompleted = "False";

            try
            {

                DateTime dDateStart = Request.Form["hd_dep_date_start"].Hotels2DateSplitYear("-");
                DateTime dDateend = Request.Form["hd_dep_date_end"].Hotels2DateSplitYear("-");
                short DepositAmont = short.Parse(Request.Form["txtAmount"]);
                byte bytDepositCat = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$dropDepositCat"]);
                
                Productdeposit cDeposit = new Productdeposit();
                cDeposit.InsertDeposit(this.CurrentProductActiveExtra, bytDepositCat, DepositAmont, dDateStart, dDateend);
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