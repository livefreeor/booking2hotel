using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand;

public partial class test_xml : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void ss_onclick(object sender, EventArgs e)
    {
        dd.Text = dd.Text.Hotels2SPcharacter_remove();
            //
        //Response.Write(dd.Text.Hotels2SPcharacter_remove());
        //Response.End();
    }
}