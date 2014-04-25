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
    public partial class admin_ajax_deposit_del : Hotels2BasePageExtra_Ajax
    {
        public string qDepId
        {
            get { return Request.QueryString["depid"]; }
        }


        

        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!this.Page.IsPostBack)
            {
                if (this.IsstaffDelete())
                {
                    if (!string.IsNullOrEmpty(this.qDepId))
                    {
                        
                        //Response.Write(this.qDateStartPeriod + "---" + this.qDateEndPeriod + "----" + this.qConId);
                        Response.Write(DelDep());
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

        public string DelDep()
        {
            string result = "False";
            Productdeposit cDeposit = new Productdeposit();
            int intDepID = int.Parse(this.qDepId);
            
            try
            {
                cDeposit.UpdateStatusDeposit(this.CurrentProductActiveExtra, intDepID);

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