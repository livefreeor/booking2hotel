using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class test_table : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ff = "#!#";
        string result = "KKKKKKK" + ff + "DDDDDDD";
        Response.Write(result.Replace("#!#","---"));
        Response.End();
    }
}