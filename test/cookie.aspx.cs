using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Affiliate;

public partial class test_cookie : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie objCookie = Request.Cookies["site_id"];
        objCookie.Domain = "hotels2thailand.com";
       
        lblcookie.Text = objCookie.Value;
    }

    public void ff_onclick(object sender, EventArgs e)
    {
        SiteStat_Tracker.ClearCookie_Only();
        
    }
}