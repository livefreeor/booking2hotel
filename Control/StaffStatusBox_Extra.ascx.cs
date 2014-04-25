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
    public partial class Control_StaffStatusBox_Extra : System.Web.UI.UserControl
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
                Staff cStaff = (this.Page as Hotels2BasePageExtra).CurrentStaffobj;
                //string strPathPic = "~/images_staffs/StaffPic" + (this.Page as Hotels2BasePageExtra).CurrentStaffId + ".gif";
                //if ((this.Page as Hotels2BasePageExtra).fileExist(Server.MapPath(strPathPic)))
                //{
                //    imgpisStaff_s.ImageUrl = strPathPic;
                //}
                //else
                //{
                //    imgpisStaff_s.ImageUrl = "~/images_staffs/nopicture.gif";
                //}

                lblStaffName.Text = cStaff.Title;
                //lblStaffCat.Text = cStaff.CatTitle;

                if (cStaff.Cat_Id == 6)
                {
                    //lblStaffCat.Visible = false;
                    
                    hlExtraNetlogout.NavigateUrl = "~/extranet/logout.aspx";
                    //hlREsetPass.NavigateUrl = "~/extranet/resetpassword.aspx?user=" + Hotels2String.EncodeIdToURL(cStaff.Staff_Id.ToString());
                }
                else
                {
                    //hlREsetPass.Visible = false;
                    hlExtraNetlogout.NavigateUrl = "~/admin/staff/ajax_staff_logout.aspx";
                }

                if (cStaff.Cat_Id != 2 && cStaff.Cat_Id != 1 && cStaff.Cat_Id != 7)
                {
                    linkProfile.Visible = false;
                }
                linkProfile.NavigateUrl = "~/admin/staff/staffmanage.aspx?sid=" + cStaff.Staff_Id.ToString();

            }
        }
}
}