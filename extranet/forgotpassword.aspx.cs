using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using Hotels2thailand.UI;
using Hotels2thailand;


public partial class admin_forgotpassword : System.Web.UI.Page
    {
        public string qPass 
        {
            get { return Request.QueryString["forgot"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.qPass))
                {
                    if(this.qPass == "completed")
                    {
                        panelForm.Visible = false;
                        panelincompleted.Visible = false;
                        panelcompleted.Visible = true;
                    }

                    if (this.qPass == "incompleted")
                    {
                        panelForm.Visible = false;
                        panelincompleted.Visible = true;
                        panelcompleted.Visible = false;
                    }
                    
                }
                
            }
        }

        
        
        public void BtLogin_Click(object sender, EventArgs e)
        {
            StaffExtra cStaffExtra = new StaffExtra();
            string user = txtUserName.Text.Trim();
            string email = txtEmail.Text.Trim();
            cStaffExtra = cStaffExtra.getStaffbyEmailAndUserName(user, email);
            if (cStaffExtra != null)
            {
                SentMailRecoveryMail(cStaffExtra);
                Response.Redirect("forgotpassword.aspx?forgot=completed");
            }
            else
                Response.Redirect("forgotpassword.aspx?forgot=incompleted");
        }

        
        public void SentMailRecoveryMail(StaffExtra shrStaffId)
        {
            StringBuilder result = new StringBuilder();
            string qUrl = Hotels2String.EncodeIdToURL(shrStaffId.Staff_Id.ToString());
            //string QueryStringResult = HttpUtility.UrlDecode(this.qBookingProductId).Hotels2DecryptedData_SecretKey();
            //QueryStringResult = QueryStringResult.Hotels2RightCrl(20);


            result.Append("<p>Dear " + shrStaffId.Title+ "</p>");
            result.Append("<p>Your request for password retrieval from Booking2hotels Engine :</p>");
            result.Append("<p>Please follow the link to rest your password. Once your password is changed the system will automodifically unlock your account.</p>");
            result.Append("<p>http://manage.booking2hotels.com/extranet/resetpassword.aspx?user=" + qUrl + "</p>");

            result.Append("<p>Best Regard,</p>");
            result.Append("<p>Booking2hotels Team</p>");

            Hotels2MAilSender.SendmailNormail("support@booking2hotels.com", "Booking2hotels Engine Team,", shrStaffId.Email.Trim(), "Booking2hotels Engine reset pasword",
                "", result.ToString());
        }

        
    }
    
