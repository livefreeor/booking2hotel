using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using Hotels2thailand.Booking;
using Hotels2thailand.Production;
using Hotels2thailand;
using Hotels2thailand.Staffs;

namespace Hotels2thailand.UI
{
    public partial class ajax_booking_card : System.Web.UI.Page
    {
        public string qBookingId
        {
            get
            {
                return Request.QueryString["bid"];
            }
        }

        public short Current_StaffID
        {
            get
            {
                Hotels2thailand.UI.Hotels2BasePage cBasePage = new Hotels2thailand.UI.Hotels2BasePage();
                return cBasePage.CurrentStaffId;
            }
        }

        public Staff Current_Staffobj
        {
            get
            {
                Hotels2thailand.UI.Hotels2BasePage cBasePage = new Hotels2thailand.UI.Hotels2BasePage();
                return cBasePage.CurrentStaffobj;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                //Response.Write(Request.Form["booking_email"]);
                Response.Write(Staffauthorize_CardAccess());
                Response.Flush();
            }
        }

        public string Staffauthorize_CardAccess()
        {
            string result = string.Empty;
            result = "False";
            if (this.Current_Staffobj.Cat_Id == 1 || this.Current_Staffobj.Cat_Id == 2 || this.Current_Staffobj.Cat_Id == 7 || this.Current_Staffobj.Cat_Id == 9
                || this.Current_Staffobj.Cat_Id == 3 || this.Current_Staffobj.Cat_Id == 8)
            {

                //#Staff_Activity_Log Card Detail==========================================================================================================
                StaffActivity cStaffLog = new StaffActivity();
                cStaffLog.InsertNewStaffActivity(null, int.Parse(this.qBookingId), null, null, 41, 4,  this.Current_StaffID.ToString());
                //============================================================================================================================

                result = "True";
            }
            return result;
        }

    }
}