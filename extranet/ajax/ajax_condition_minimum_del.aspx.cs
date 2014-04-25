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
    public partial class admin_ajax_condition_minimum_del : Hotels2BasePageExtra_Ajax
    {
        public string qMinId
        {
            get { return Request.QueryString["minid"]; }
        }


        public string qConId
        {
            get { return Request.QueryString["conid"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!this.Page.IsPostBack)
            {
                if (this.IsstaffDelete())
                {
                    if (!string.IsNullOrEmpty(this.qMinId))
                    {
                        
                        //Response.Write(this.qDateStartPeriod + "---" + this.qDateEndPeriod + "----" + this.qConId);
                        Response.Write(DelMinimum());
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

        public string DelMinimum()
        {
            string result = "False";
            ConditionminimumDayExtranet cConditionMin = new ConditionminimumDayExtranet();
            int intMinId = int.Parse(this.qMinId);
            
            try
            {
                cConditionMin.UpdateMinimumStayStatus(this.CurrentProductActiveExtra, intMinId, false);

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