using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;


    public partial class admin_Accessdenie : System.Web.UI.Page 
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
                case "loginfailed":
                    strbError.Append("<h1>Login Failed</h1>");
                    strbError.Append("<p>Your log in is incorrect. Please retry <a href=\"http://order.booking2hotels.com/admin/login_rd.aspx\">Login Here</a></p>");
                    break;
                case "loginfailed_ex":
                    strbError.Append("<h1>Login Failed</h1>");
                    strbError.Append("<p>Please Login Before From BLUEHOUSE only!</p>");
                    break;
                case "accessdenied" :
                    strbError.Append("<h1>Access Denied this Page</h1>");
                    strbError.Append("<p>You can not to access in this Page<br/> Plasee click back on your Browser to skip this page</p>");
                    break;
                case "logout":
                    strbError.Append("<h1>Log Out Completed</h1>");
                    strbError.Append("<p>Please Login Before <a href=\"http://order.booking2hotels.com/admin/login_rd.aspx\">Login Here</a></p>");
                    break;
                case "logout_ex":
                    strbError.Append("<h1>Log Out Completed</h1>");
                    strbError.Append("<p>Please Login Before</p>");
                    break;
                case "requestlogin_111":
                case "requestlogin_222":
                case "requestlogin_333":
                case "requestlogin_444":
                case "requestlogin_555":
                case "requestlogin_666":
                    strbError.Append("<h1>Please Login</h1>");
                    strbError.Append("<p>You can not to access to Control Panel<br/> Please Login Before <a href=\"http://order.booking2hotels.com/admin/login_rd.aspx\">Login Here</a></p> ");

                    break;
                case "requestlogin_ex":
                    strbError.Append("<h1>Please Login</h1>");
                    strbError.Append("<p>You can not to access to Control Panel<br/> Please Login Before From BLUEHOUSE only!</p> ");
                    break;
                case "noactivate":
                    strbError.Append("<h1>Staff ID Not Activate By BlueHouse</h1>");
                    strbError.Append("<p>You can not to access to Control Panel<br/> Please Contact BlueHouse Developer For this Problem </p> ");
                    break;
                case "missRef":
                    strbError.Append("<h1>Login From BlueHouse Only</h1>");
                    strbError.Append("<p>You can not to access to Control Panel<br/> Please Login From BlueHouse 's Office <a href=\"http://order.booking2hotels.com/admin/login_rd.aspx\">Login Here</a> </p> ");
                    break;


                    
            }

            lblErrortxt.Text = strbError.ToString();
        }
    }
