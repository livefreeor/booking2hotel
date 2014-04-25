using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using Hotels2thailand.UI;


    public partial class admin_login_rd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {

                if (HttpContext.Current.Request.Cookies["SessionKey"] != null)
                {
                    HttpCookie objCookie = new HttpCookie("SessionKey");
                    //objCookie.Domain = "www.hotels2thailand.com";
                    objCookie.Expires = DateTime.Now.AddDays(-1d);
                    HttpContext.Current.Response.Cookies.Add(objCookie);
                }

                StaffSessionAuthorize clStaffSesssion = new StaffSessionAuthorize();
                Staff cSessionStaff = new Staff();
                if (Request.Cookies["SessionKey"] != null)
                {
                    
                    int intLogKey = int.Parse(Request.Cookies["SessionKey"]["LogKey"]);
                    clStaffSesssion = clStaffSesssion.IsHaveSessionRecord(intLogKey);
                    cSessionStaff = clStaffSesssion.ClassStaff;
                    if (clStaffSesssion != null && cSessionStaff.Status != false &&
                        clStaffSesssion.LeaveTime == null && cSessionStaff.Cat_Id == 6)
                    {
                        
                        Response.Redirect("~/extranet/mainextra.aspx");
                        //if (cSessionStaff.Cat_Id == 3 || cSessionStaff.Cat_Id == 8)
                        //    Response.Redirect("~/extranet/mainextra.aspx");
                        //else
                        //{
                        //    string LastUrl = clStaffSesssion.LastAccessURL.Split('/')[clStaffSesssion.LastAccessURL.Split('/').Count() - 1].Split('?')[0];
                        //    if (LastUrl == "review_send.aspx" || LastUrl == "print_booking_slip.aspx" || LastUrl == "voucher_admin_preview.aspx"
                        //        || LastUrl == "print_booking_display.aspx" || LastUrl == "voucher_send.aspx")
                        //        Response.Redirect("~/extranet/mainextra.aspx");
                        //    else
                        //        Response.Redirect(clStaffSesssion.LastAccessURL);
                        //}
                        
                    }
                }

            }
        }


        protected void BtLogin_Click(object sender, EventArgs e)
        {

            if (HttpContext.Current.Request.Cookies["SessionKey"] != null)
            {
                HttpCookie objCookie = new HttpCookie("SessionKey");
                //objCookie.Domain = "www.hotels2thailand.com";
                objCookie.Expires = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Cookies.Add(objCookie);
            }


            Staff clStaff = new Staff();
            clStaff = clStaff.HaveStaffLogin(txtUserName.Text, txtPassword.Text);
            if (clStaff == null)
            {
                Hotels2thailand.UI.Hotels2BasePageExtra.RequestLogin();
            }
            else
            {
                if (clStaff.Status == false)
                {
                   
                    HttpContext.Current.Response.Redirect("~/extranet/staff-notification.aspx?error=noactivate");
                }
                else if (clStaff.Cat_Id != 6)
                {
                    HttpContext.Current.Response.Redirect("~/extranet/staff-notification.aspx?error=staff_partner_only");
                }
                else
                {
                    StaffSessionAuthorize StaffSession = new StaffSessionAuthorize();
                    StaffSession.CloseOtherCurrentLogin(clStaff.Staff_Id);

                    StaffSessionAuthorize.SessionCreateExtra(clStaff.Staff_Id);
                }

            }

        }
    }
    
