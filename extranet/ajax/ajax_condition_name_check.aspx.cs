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
    public partial class admin_ajax_condition_name_check : Hotels2BasePageExtra_Ajax
    {
        public string qOptionId
        {
            get { return Request.QueryString["oid"]; }
        }

        public string qConditionNameid
        {
            get { return Request.QueryString["connid"]; }
        }

        public string qAdult
        {
            get { return Request.QueryString["adu"]; }
        }

        public string qABF
        {
            get { return Request.QueryString["abf"]; }
        }

        public string qConditionID
        {
            get { return Request.QueryString["conid"]; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!this.Page.IsPostBack)
            {
                //try
                //{
                    if (!string.IsNullOrEmpty(this.qOptionId) && !string.IsNullOrEmpty(this.qConditionNameid) && !string.IsNullOrEmpty(this.qAdult) && !string.IsNullOrEmpty(this.qABF))
                    {
                        ProductConditionExtra cConExtra = new ProductConditionExtra();
                        int CountCon = cConExtra.CountcheckConditionNameDuplicate(int.Parse(this.qOptionId), byte.Parse(this.qConditionNameid), this.CurrentSupplierId, byte.Parse(this.qAdult), byte.Parse(this.qABF), this.qConditionID);
                        Response.Write(CountCon);
                        Response.End();
                    }
                //}
                //catch (Exception ex)
                //{
                //    Response.Write("error:" + ex.Message);
                //    Response.End();
                //}
                
                
            }
        }


        
    }
}