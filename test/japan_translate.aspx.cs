using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class test_japan_translate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnShowCookikie_Click(object sender, EventArgs e)
    {
        string dd = string.Join("&", Request.Cookies["SessionKey"].Values);

        Response.Write(dd);
        Response.End();

        
    }
}