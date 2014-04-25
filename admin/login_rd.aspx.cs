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
                Hotels2thailand.Affiliate.SiteStat_Tracker.ClearCookie_Only();

                //StaffSessionAuthorize clStaffSesssion = new StaffSessionAuthorize();
                //Staff cSessionStaff = new Staff();
                //if (Request.Cookies["SessionKey"] != null)
                //{

                //    int intLogKey = int.Parse(Request.Cookies["SessionKey"]["LogKey"]);
                //    clStaffSesssion = clStaffSesssion.IsHaveSessionRecord(intLogKey);
                //    cSessionStaff = clStaffSesssion.ClassStaff;
                //    if (clStaffSesssion != null && cSessionStaff.Status != false &&
                //        clStaffSesssion.LeaveTime == null)
                //    {
                //        //cSessionStaff.Cat_Id == 9 || cSessionStaff.Cat_Id == 10 || cSessionStaff.Cat_Id == 3 || cSessionStaff.Cat_Id == 5 || cSessionStaff.Cat_Id == 4

                //        //if (cSessionStaff.Cat_Id == 1 || cSessionStaff.Cat_Id == 2 || cSessionStaff.Cat_Id == 7 || cSessionStaff.Cat_Id == 8 || cSessionStaff.Cat_Id == 9 || cSessionStaff.Cat_Id == 10)
                //        // {
                //             if (cSessionStaff.Cat_Id == 3 || cSessionStaff.Cat_Id == 8)
                //                 Response.Redirect("~/admin/mainpanel.aspx");
                //             else
                //             {
                //                 string LastUrl = clStaffSesssion.LastAccessURL.Split('/')[clStaffSesssion.LastAccessURL.Split('/').Count() - 1].Split('?')[0];
                //                 if (LastUrl == "review_send.aspx" || LastUrl == "print_booking_slip.aspx" || LastUrl == "voucher_admin_preview.aspx"
                //                     || LastUrl == "print_booking_display.aspx" || LastUrl == "voucher_send.aspx")
                //                     Response.Redirect("~/admin/mainpanel.aspx");
                //                 else
                //                     Response.Redirect(clStaffSesssion.LastAccessURL);
                //             }
                //         //}
                //         //else
                //         //{
                //         //    HttpContext.Current.Response.Redirect("~/admin/accessdenie.aspx?error=accessdenied");
                //         //}
                //    }
                //}
               
            }
        }
       

        protected void BtLogin_Click(object sender, EventArgs e)
        {

            Staff clStaff = new Staff();
            clStaff = clStaff.HaveStaffLogin(txtUserName.Text, txtPassword.Text);
            if (clStaff == null)
            {
                Hotels2thailand.UI.Hotels2BasePage.RequestLogin();
            }
            else
            {
                if (clStaff.Status == false)
                {
                    HttpContext.Current.Response.Redirect("~/admin/accessdenie.aspx?error=noactivate");
                }
                else
                {
                    StaffSessionAuthorize StaffSession = new StaffSessionAuthorize();
                    StaffSession.CloseOtherCurrentLogin(clStaff.Staff_Id);

                    StaffSessionAuthorize.SessionCreate(clStaff.Staff_Id);

                    
                }
                
            }

        }
    }
    
