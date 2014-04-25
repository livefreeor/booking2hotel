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
    public partial class admin_ajax_condition_name_check_package_update : Hotels2BasePageExtra_Ajax
    {
        public string qOptionId
        {
            get { return Request.QueryString["oid"]; }
        }

        public string qConditionId
        {
            get { return Request.QueryString["con"]; }
        }

        
        public string qGuest
        {
            get { return Request.QueryString["gus"]; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!this.Page.IsPostBack)
            {
                //try
                //{
                if (!string.IsNullOrEmpty(this.qOptionId) && !string.IsNullOrEmpty(this.qConditionId) && !string.IsNullOrEmpty(this.qGuest))
                    {
                        ProductConditionExtra cConExtra = new ProductConditionExtra();
                        int CountCon = cConExtra.CountcheckConditionNameDuplicate_package_update(int.Parse(this.qOptionId), int.Parse(this.qConditionId), this.CurrentSupplierId, byte.Parse(this.qGuest));
                        Response.Write(CountCon);
                        Response.End();
                    }
                //}
                //catch (Exception ex)
                //{
                //    Response.Write("error:" + ex.Message + "<br/>" + ex.StackTrace);
                //    Response.End();
                //}
                
                
            }
        }


        
    }
}