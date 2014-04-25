using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand;
using Hotels2thailand.Production;

public partial class b2b_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        B2bAgency cB2bAgency = new B2bAgency();
        if (txtPassword.Text.Trim() != "" && txtUsername.Text.Trim() != "")
        {
            cB2bAgency.CheckB2bAgencyLogin(txtUsername.Text.Trim(), txtPassword.Text.Trim());
            if (cB2bAgency.agency_id != null && cB2bAgency.agency_id != 0)
            {
                Session["Agency_ID"] = cB2bAgency.agency_id;                
                Response.Redirect("SearchProduct.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Login Failed. Your log in is incorrect. Please retry.');", true);
            }
        }
    }
}