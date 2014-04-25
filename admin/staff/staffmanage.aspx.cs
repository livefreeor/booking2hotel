using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using Hotels2thailand.Suppliers;
using Hotels2thailand.UI.Controls;
namespace Hotels2thailand.UI
{
    public partial class admin_staffmanage : Hotels2BasePage
    {
        public string QueryString
        {
            get { return Request.QueryString["sid"]; }
        }

        
        public short shrQueryString
        {
            get { return Convert.ToInt16(this.QueryString); }
        }

        public string QueryStaffType
        {
            get { return Request.QueryString["t"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
           
           BindingMode();
        }

        protected void userInsertBox_OnDataSaved(object sender, EventArgs e)
        {
            userStaffList.BindGrid(); 
        }

        protected void uploadbox_OnDataPictureUploaded(object sender, EventArgs e)
        {
           
            userInsertBox.FormViewBind();
            System.Web.UI.UserControl userMaster = (System.Web.UI.UserControl)Page.Master.FindControl("userStaffLoginBox");
            userMaster.DataBind();
            //this.Page.Master.FindControl("userStaffLoginBox") as 
        }

        protected void btAddnewStaff_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.QueryString))
            {
                panelInsert.Visible = true;

                panelListStaff.Visible = false;

                btimgAddnewStaff.Visible = false;

                //call Method in UserControl

                Control_StaffAddEditBox control = (Control_StaffAddEditBox)Page.LoadControl("~/Control/StaffAddEditBox.ascx");
                control.ButtonAddnewStaff();
            }
        }

        protected void BindingMode()
        {

            if (!String.IsNullOrEmpty(this.QueryString))
            {
                panelInsert.Visible = true;
                panelListStaff.Visible = false;
                //Authorization Manage
                if (this.SessionId != 2 && this.SessionId != 7)
                {
                    panelStaffTopMenu.Visible = false;
                    HidePanelActivityBox();
                }
                else
                {
                    ShowPabelActivityBox();
                }
                
                panelUploadBox.Visible = true;
                btimgAddnewStaff.Visible = false;

                //Control_StaffAddEditBox control = (Control_StaffAddEditBox)Page.LoadControl("~/Control/StaffAddEditBox.ascx");
                //lblStaffHeadPage.Text = "<p>" + control.StaffTypeCheck + " <br /><span class=\"titleDes\">Manage Bluehouse Travel Staff Every Department</span></p>";
                //control.StaffTypeCheck
            }
            else
            {
                panelInsert.Visible = false;
                panelListStaff.Visible = true;
                HidePanelActivityBox();
                panelUploadBox.Visible = false;
            }

            //if()

            //if (!String.IsNullOrEmpty(this.QueryStaffType))
            //{
            //    if (this.QueryStaffType == "bluehouse")
            //    {
            //        lblStaffHeadPage.Text = "<p>BlueHouse Staff <br /><span class=\"titleDes\">Manage Bluehouse Travel Staff Every Department</span></p>";

            //    }
            //    if (this.QueryStaffType == "partners")
            //    {
            //        lblStaffHeadPage.Text = "<p>Partner Staff <br /><span class=\"titleDes\">Manage Partner From Supplier Every Department</span></p>";

            //    }
            //}
            //else
            //{
            //    lblStaffHeadPage.Text = "<p>BlueHouse Staff <br /><span class=\"titleDes\">Manage Bluehouse Travel Staff Every Department</span></p>";
            //}

            
        }

        protected void ShowPabelActivityBox()
        {
            //panelStaffActivity.Visible = true;
            
        }

        protected void HidePanelActivityBox()
        {
            //panelStaffActivity.Visible = false;
        }
    }
}

