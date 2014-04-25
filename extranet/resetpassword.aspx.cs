using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using Hotels2thailand.UI;
using Hotels2thailand;


public partial class admin_resetpassword : System.Web.UI.Page
{
        public string qUsrReset
        {
            get { return Request.QueryString["user"]; }
        }

        public string qReset
        {
            get { return Request.QueryString["reset"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.qReset))
                {
                    panelCompleted.Visible = true;
                    panelForm.Visible = false;
                }
            }
        }


        protected void BtLogin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.qUsrReset))
            {
               string Userid =  Hotels2String.DecodeIdFromURL(this.qUsrReset);
               Staff.updateStaffPassWord(short.Parse(Userid), txtNewPass.Text);
               
                Response.Redirect("resetpassword.aspx?reset=completed");
            }
            else
            {

            }
        }
    }
    
