using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using Hotels2thailand.UI;


    public partial class admin_login_check : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Hotels2thailand.Affiliate.SiteStat_Tracker.ClearCookie_Only();
                
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_REFERER"]))
                {
                    
                    string IPrefer = "http://10.1.1.96/admin/login.aspx";
                    if (HttpContext.Current.Request.ServerVariables["HTTP_REFERER"].ToString().Trim() == IPrefer.Trim())
                    {

                        LoginCheck();

                        
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect("~/admin/accessdenie.aspx?error=missRef");
                    }
                }
                else
                {
                    HttpContext.Current.Response.Redirect("~/admin/accessdenie.aspx?error=missRef");
                }
            }
        }


        protected void LoginCheck()
        {
            
            Staff clStaff = new Staff();
            clStaff = clStaff.HaveStaffLogin(Request.QueryString["user"], Request.QueryString["pass"]);
            if (clStaff == null)
            {
                HttpContext.Current.Response.Redirect("~/admin/accessdenie.aspx?error=loginfailed");
                //Hotels2thailand.UI.Hotels2BasePage.RequestLogin();
            }
            else
            {
                
                if (clStaff.Status == false)
                {
                    HttpContext.Current.Response.Redirect("~/admin/accessdenie.aspx?error=noactivate");
                }
                else
                {
                    StaffSessionAuthorize clStaffSesssion = new StaffSessionAuthorize();
                    Staff cSessStaff = new Staff();
                    if (Request.Cookies["SessionKey"] != null)
                    {

                        int intLogKey = int.Parse(Request.Cookies["SessionKey"]["LogKey"]);
                        clStaffSesssion = clStaffSesssion.IsHaveSessionRecord(intLogKey);
                        cSessStaff = clStaffSesssion.ClassStaff;
                        if (clStaffSesssion != null && cSessStaff.Status != false &&
                            clStaffSesssion.LeaveTime == null && cSessStaff.Staff_Id == clStaff.Staff_Id)
                        {
                            if (cSessStaff.Cat_Id == 3 || cSessStaff.Cat_Id == 8)
                                Response.Redirect("~/admin/mainpanel.aspx");
                            else
                            {
                                string LastUrl = clStaffSesssion.LastAccessURL.Split('/')[clStaffSesssion.LastAccessURL.Split('/').Count() - 1].Split('?')[0];
                                if (LastUrl == "review_send.aspx" || LastUrl == "print_booking_slip.aspx" || LastUrl == "voucher_admin_preview.aspx"
                                    || LastUrl == "print_booking_display.aspx" || LastUrl == "voucher_send.aspx")
                                    Response.Redirect("~/admin/mainpanel.aspx");
                                else
                                    Response.Redirect(clStaffSesssion.LastAccessURL);
                            }
                            //Response.Redirect(clStaffSesssion.LastAccessURL);
                        }
                        else
                        {
                            StaffSessionAuthorize StaffSession = new StaffSessionAuthorize();
                            StaffSession.CloseOtherCurrentLogin(clStaff.Staff_Id);

                            StaffSessionAuthorize.SessionCreate(clStaff.Staff_Id);
                        }
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
    }
