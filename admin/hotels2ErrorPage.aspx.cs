using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class admin_Hotels2ErrorPage : System.Web.UI.Page
{
    private string ErrorCasequerystring
    {
        get { return Request.QueryString["error"]; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder strbError = new StringBuilder();
        switch (this.ErrorCasequerystring)
        {
            case "203":
                strbError.Append("<h1>No folder Location Source</h1>");
                strbError.Append("<p>Please Login Before <a href=\"login.aspx\">Login Here</a></p>");
                break;
            case "204":
                strbError.Append("<h1>Product Dont Set Location</h1>");
                strbError.Append("<p>You can not to access in this Page<br/> Plasee click back on your Browser to skip this page</p>");
                break;
            case "202":
                strbError.Append("<h1>No folder Destination Source</h1>");
                strbError.Append("<p>Please Login Before <a href=\"login.aspx\">Login Here</a></p>");
                break;
            case "201":
                strbError.Append("<h1>No Folder Picture Sorce</h1>");
                strbError.Append("<p>You can not to access to Control Panel<br/> Please Login Before <a href=\"login.aspx\">Login Here</a></p> ");
                break;
            //case "noactivate":
            //    strbError.Append("<h1>Staff ID Not Activate By BlueHouse</h1>");
            //    strbError.Append("<p>You can not to access to Control Panel<br/> Please Contact BlueHouse Developer For Problem <a href=\"login.aspx\">Login Here</a></p> ");
            //    break;

            

        }

        lblErrortxt.Text = strbError.ToString();
    }
}