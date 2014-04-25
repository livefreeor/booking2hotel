using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_product_facility_update : System.Web.UI.Page
    {
        public string qProductId
        {
            get { return Request.QueryString["pid"]; }
        }

        public string qProductCat
        {
            get { return Request.QueryString["pdcid"]; }
        }

        public string qFacid
        {
            get { return Request.QueryString["facid"]; }
        }

        public string qValue
        {
            get { return Request.QueryString["val"]; }
        }

        public byte Current_StaffLangId
        {
            get
            {
                Hotels2BasePage cBasePage = new Hotels2BasePage();
                return cBasePage.CurrenStafftLangId;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qProductCat) && !string.IsNullOrEmpty(this.qFacid) && !string.IsNullOrEmpty(this.qValue))
            {
                Response.Write(ProductFacility.UpdateFacility(int.Parse(this.qFacid), this.Current_StaffLangId, this.qValue));
                Response.End();
             }
            
        }

       
        
    }
}