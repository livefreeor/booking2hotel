using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.ProductOption;
using System.Text;
using Hotels2thailand.Production;
using Hotels2thailand.Member;
using Hotels2thailand.Booking;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_member_reset_password: Hotels2BasePageExtra_Ajax
    {
        
        public string qmemberId
        {
            get { return Request.QueryString["mid"]; }

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
            {

                if (this.IsStaffEdit())
                {
                    
                        Response.Write(SendMailREset());
                        Response.End();
                    

                }
                else
                {
                    Response.Write("method_invalid");
                }

            }
        }

        public bool SendMailREset()
        {

            Member_customer cMember = new Member_customer();
            int MemberId = int.Parse(this.qmemberId);
            cMember = cMember.getMemberById(MemberId);
            BookingMailEngine cBookingMail = new BookingMailEngine();

            return cBookingMail.SendMemberesetPass(cMember.Email, this.CurrentProductActiveExtra, MemberId);
            

        }
    }
}