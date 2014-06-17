using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand;

public partial class test_testform : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }


    protected void btnNext_Onclick(object sender, EventArgs e)
    {
        //txtKey

        Response.Write(txtKey.Text.CreateMD5Hash().ToLower());
    }
}