using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using System.Text;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_condition_del : Hotels2BasePageExtra_Ajax
    {
        public string qConditionId
        {
            get { return Request.QueryString["conid"]; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (this.IsStaffEdit())
                {
                    if (!string.IsNullOrEmpty(this.qConditionId))
                    {
                        Response.Write(DelCondition(int.Parse(this.qConditionId)));
                    }
                }
                else
                {
                    Response.Write("method_invalid");
                }
                
                Response.End();
            }
        }

        public string DelCondition(int intConditionId)
        {
            string result = string.Empty;
            ProductConditionExtra cConditionExtra = new ProductConditionExtra();
            result = cConditionExtra.UpdateConditionExtraStatus(this.CurrentProductActiveExtra, intConditionId, false).ToString();
            
            return result; 
        }
    }
}