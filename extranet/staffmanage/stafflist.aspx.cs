using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using Hotels2thailand;

namespace Hotels2thailand.UI
{
    public partial class extranet_stafflist : Hotels2BasePageExtra
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                GvStaffDataBind();
            }
            
        }

        public void GvStaffDataBind()
        {
            StaffExtra cStaff = new StaffExtra();
            if (string.IsNullOrEmpty(this.qProductId))
            {
                
                short StaffID = this.CurrentStaffId;

                GvStaffList.DataSource = cStaff.GetStaffListExtraByStaffID(StaffID);
            }
            else
            {
                GvStaffList.DataSource = cStaff.GetStaffListExtraByProductID(int.Parse(this.qProductId));
            }
            GvStaffList.DataBind();

        }

        public void GVstaffListRole_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                short StaffID = (short)GvStaffList.DataKeys[e.Row.RowIndex].Value;
                HyperLink hlStaffManage = e.Row.Cells[6].FindControl("lnManageStaff") as HyperLink;

                string Random = StaffID + Hotels2String.Hotels2RandomStringNuM(40);
                hlStaffManage.NavigateUrl = "~/extranet/staffmanage/staffmanage.aspx?s=" + Random.Hotels2EncryptedData() + this.AppendCurrentQueryString();
            }
        }
    }
}