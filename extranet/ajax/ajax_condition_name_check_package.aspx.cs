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
    public partial class admin_ajax_condition_name_check_package : Hotels2BasePageExtra_Ajax
    {
        public string qOptionId
        {
            get { return Request.QueryString["oid"]; }
        }

        public string qConditionNameid
        {
            get { return Request.QueryString["connid"]; }
        }

        public string qGuest
        {
            get { return Request.QueryString["gus"]; }
        }

        public string qIsAdult
        {
            get { return Request.QueryString["iadu"]; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!this.Page.IsPostBack)
            {
                bool bolIsAdult = false;

                if (qIsAdult == "0")
                    bolIsAdult = true;
                //else if (qIsAdult == "1")
                //    bolIsAdult = false;

                //try
                //{
                        if (!string.IsNullOrEmpty(this.qOptionId) && !string.IsNullOrEmpty(this.qConditionNameid) && !string.IsNullOrEmpty(this.qIsAdult) && !string.IsNullOrEmpty(this.qGuest))
                        {
                            ProductConditionExtra cConExtra = new ProductConditionExtra();
                            int CountCon = cConExtra.CountcheckConditionNameDuplicate_package_insert(int.Parse(this.qOptionId), byte.Parse(this.qConditionNameid), this.CurrentSupplierId, byte.Parse(this.qGuest), bolIsAdult);
                            Response.Write(CountCon);
                            Response.End();
                        }
                        else
                        {
                            Response.Write("NO DATA querysTring");
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