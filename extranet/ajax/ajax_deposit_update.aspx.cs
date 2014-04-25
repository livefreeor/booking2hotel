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
    public partial class admin_ajax_deposit_update : Hotels2BasePageExtra_Ajax
    {

        public string qDepId
        {
            get { return Request.QueryString["depid"]; }
        }
        
       
       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
            {
                if (this.IsStaffEdit())
                {
                    if (!string.IsNullOrEmpty(this.qDepId) )
                    {
                        Response.Write(UpdateRate());
                    }
                    else
                    {
                        Response.Write("method");
                    }
                  
                }
                else
                {
                    Response.Write("method_invalid");
                }
                Response.End();
                
            }
        }

        public string UpdateRate()
        {
            string result = "False";
            Productdeposit cDeposit = new Productdeposit();
            int intDepId = int.Parse(this.qDepId);
            
            try
            {
                DateTime dDateStart = Request.Form["hd_dep_date_From_" + intDepId].Hotels2DateSplitYear("-");
                DateTime dDateEnd = Request.Form["hd_dep_date_To_" + intDepId].Hotels2DateSplitYear("-");
                short bytDepAmount = short.Parse(Request.Form["txt_amount_" + intDepId]);
                byte bytDepCat = byte.Parse(Request.Form["deposit_cat_" + intDepId]);
                cDeposit.UpdateDeposit(this.CurrentProductActiveExtra, intDepId, bytDepAmount, bytDepCat, dDateStart, dDateEnd );
                
                result = "True";
            }
            catch (Exception ex)
            {
                Response.Write("error:" + ex.Message);
            }
            return result;
        }

    }
}