using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;


    public partial class staff_notifications : System.Web.UI.Page 
    {
        private string ErrorCasequerystring
        {
            get { return Request.QueryString["error"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            StringBuilder strbError = new StringBuilder();

            strbError.Append("<div style=\"font-size:20px;margin:20px 0 0 0; font-weight:bold;\">We are in the process of updating the system temporarily.</div> <br/>");
            strbError.Append("Sorry for the inconvenience.");

            lblErrortxt.Text = strbError.ToString();
        }
    }
