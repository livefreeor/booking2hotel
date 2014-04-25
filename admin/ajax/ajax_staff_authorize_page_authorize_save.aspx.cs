using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using System.Net.Mail;

public partial class ajax_staff_authorize_page_authorize_save : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Page.IsPostBack)
        {
            Response.Write(SavePageAuothorize());
            Response.End();
            
        }
    }

    public string SavePageAuothorize()
    {
        string result = "false";
        string checkpage = Request.Form["check_page_auth"];
        byte bytCatId = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$drop_staff_cat"]);
      

        StaffPageAuthorize cStaffPageAutho = new StaffPageAuthorize();
        try
        {
            result = cStaffPageAutho.UpdatePageAuthorize(checkpage, bytCatId);
            
        }
        catch (Exception ex)
        {
            Response.Write("error : " + ex.Message);
            Response.End();
        }
        return result;
    }
    
}