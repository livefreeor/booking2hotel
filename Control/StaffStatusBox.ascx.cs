using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.UI;
using Hotels2thailand.Staffs;

namespace Hotels2thailand.UI.Controls
{
    public partial class Control_StaffStatusBox : System.Web.UI.UserControl
    {
        //private StaffSessionAuthorize _staffSession = null;
        //public StaffSessionAuthorize ClassStaffSession
        //{
        //    get
        //    {
        //        if (_staffSession == null)
        //        {
        //            if (Session["staff"] != null && Request.Cookies["SessionKey"] != null)
        //            {
        //                int intLogKey = int.Parse(Request.Cookies["SessionKey"]["LogKey"]);
        //                StaffSessionAuthorize clStaffSession = new StaffSessionAuthorize();

        //                _staffSession = clStaffSession.GetSessionRecord(intLogKey);
        //            }
        //        }
        //        return _staffSession;
        //    }
        //}


        protected void Page_Load(object sender, EventArgs e)
        {
            BindMode();
        }


        //protected void linkSignout_Click(object sender, EventArgs e)
        //{
        //    StaffSessionAuthorize.Logout();
        //}

        public void BindMode()
        {
            if (Session["staff"] != null)
            {
                string strPathPic = "~/images_staffs/StaffPic" + (this.Page as Hotels2BasePage).CurrentStaffId + ".gif";
                if ((this.Page as Hotels2BasePage).fileExist(Server.MapPath(strPathPic)))
                {
                    imgpisStaff_s.ImageUrl = strPathPic;
                }
                else
                {
                    imgpisStaff_s.ImageUrl = "~/images_staffs/nopicture.gif";
                }

                lblStaffName.Text = (this.Page as Hotels2BasePage).CurrentStaffobj.Title;
                lblStaffCat.Text = (this.Page as Hotels2BasePage).CurrentStaffobj.CatTitle;
                linkProfile.NavigateUrl = "~/admin/staff/staffmanage.aspx?sid=" + (this.Page as Hotels2BasePage).CurrentStaffId.ToString();

            }
        }
}
}