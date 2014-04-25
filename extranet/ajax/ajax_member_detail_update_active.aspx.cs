using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.ProductOption;
using System.Text;
using Hotels2thailand.Production;
using Hotels2thailand.Member ;
using Hotels2thailand.Booking;


namespace Hotels2thailand.UI
{
    public partial class admin_ajax_member_detail_update_active : Hotels2BasePageExtra_Ajax
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
                    Response.Write(UpdateActive());
                    Response.End();
                    //try
                    //{
                        
                    //}
                    //catch (Exception ex)
                    //{
                    //    Response.Write(ex.Message + "--" + ex.StackTrace);
                    //    Response.End();
                    //}

                    
                }
                else
                {
                    Response.Write("method_invalid");
                }

                
            }
        }

        public bool UpdateActive()
        {
            bool ret = false;
            Member_customer cMember = new Member_customer();
            int intCusid = int.Parse(Request.Form["cus_id"]);
            //Response.Write(intCusid);
            //Response.End();
            cMember = cMember.getMemberById(intCusid);
            try
            {
                string NewPass = Hotels2thailand.Hotels2String.Hotels2RandomStringNuM(5);

                BookingMailEngine cBookingMail = new BookingMailEngine();

                ret = cBookingMail.SendMemberActivation(cMember.Email, this.CurrentProductActiveExtra, intCusid, NewPass);
                if (ret)
                {
                    ret = cMember.UpdateActivate(intCusid);
                    ret = cMember.GenNewPassword(intCusid, NewPass);
                }
                
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message + "<br/>" + ex.StackTrace);
                Response.End();
            }

            return ret;


           
        }
    }
}