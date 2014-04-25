using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Affiliate;
using System.Text;

namespace Hotels2thailand.UI
{
    public partial class admin_ajax_affiliate_site_stat : System.Web.UI.Page
    {
        public string qUrlREf { get { return Request.QueryString["Url_ref"]; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.qUrlREf))
                {
                    
                    //string Urlref = HttpUtility.UrlDecode(this.qUrlREf);

                    SiteStat_Tracker cTracker = new SiteStat_Tracker();
                    Response.Write(cTracker.TrackingCheckAff_Site(this.qUrlREf));
                    Response.End();

                    
                }
            }
               
        }

       



       
    }
}