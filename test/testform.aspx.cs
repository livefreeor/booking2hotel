using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class test_testform : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            lbltxt.Text = "<input type=\"text\" name=\"GG\" />";
        }
    }

    public void btnNext_Onclick(object sender, EventArgs e)
    {
        lblTest.Text = Request.Form["GG"];
        ff.Visible = false;
        dd.Visible = true;
    }

    public void btnEdit_Onclick(object sender, EventArgs e)
    {
        ff.Visible = true;
        dd.Visible = false;
    }
}