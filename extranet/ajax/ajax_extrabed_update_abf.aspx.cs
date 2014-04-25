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
    public partial class admin_ajax_extrabed_update_abf : Hotels2BasePageExtra_Ajax
    {
        
        public string qConId
        {
            get{return Request.QueryString["conid"];}
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
            {
                if (this.IsStaffEdit())
                {
                    if(!string.IsNullOrEmpty(this.qConId))
                    {
                        //Response.Write(this.qDateStartPeriod + "---" + this.qDateEndPeriod + "----" + this.qConId);
                        Response.Write(UpdateABF());
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

        public string UpdateABF()
        {
            string result = "False";
            
            ProductConditionExtra cConditionExtra = new ProductConditionExtra();
            int intConditionId = int.Parse(this.qConId);
            try
            {
                byte bytNumABF = 0;
                if (!string.IsNullOrEmpty(Request.Form["check_ABF"]))
                    bytNumABF = 1;
                cConditionExtra.UpdateConditionExtraABF(this.CurrentProductActiveExtra, intConditionId, bytNumABF);

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