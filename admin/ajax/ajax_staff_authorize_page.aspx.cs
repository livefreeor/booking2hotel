using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using System.Net.Mail;

public partial class ajax_staff_authorize_page : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Page.IsPostBack)
        {
            Response.Write(GetPageresult());
            Response.End();
            
        }
    }

    public string GetPageresult()
    {
        StringBuilder result = new StringBuilder();
        try
        {
            StaffPageAuthorizeResult sPage = new StaffPageAuthorizeResult();
            byte ModuleId = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$dropModule"]);
            IList<object> Listpage = sPage.GetStaffPageResult(true, ModuleId);

            result.Append("<table cellpadding=\"0\" cellspacing=\"2\" class=\"table_staff_page\">");
            result.Append("<tr>");
            result.Append("<th>No.</th><th>PageId</th><th>Module</th>");
            result.Append("<th>Folder</th><th>File name</th><th>Date Submit</th><th>Date Modify</th><th>Status</th>");
            result.Append("</tr>");
            int count = 0;
            string Bg = string.Empty;

            if (Listpage.Count > 0)
            {
                foreach (StaffPageAuthorizeResult PageList in Listpage)
                {
                    count = count + 1;

                    if (count % 2 == 0)
                        Bg = "#f2f2f2";
                    else
                        Bg = "#ffffff";
                    result.Append("<tr style=\"background-color:"+Bg+";\">");
                    result.Append("<td>" + count + "</td>");
                    result.Append("<td>" + PageList.PageId + "</td>");
                    result.Append("<td>" + PageList.ModuleName + "(" + PageList.ModuleId + ")</td>");
                    result.Append("<td>" + PageList.ModuleFolderName + "</td>");
                    result.Append("<td>" + PageList.PageFileName + "</td>");
                    result.Append("<td>" + PageList.DateCreate.ToString("dd-MMM-yyyy") + "</td>");
                    result.Append("<td>" + PageList.DateModify.ToString("dd-MMM-yyyy") + "</td>");
                    result.Append("<td>" + PageList.Status + "</td>");
                    result.Append("</tr>");
                }
            }
            else
            {
                result.Append("<tr>");
                result.Append("<td colspan=\"7\">No Page</td>");
                result.Append("</tr>");
            }

            result.Append("</table>");
            
        }
        catch (Exception ex)
        {
            Response.Write("error : " + ex.Message);
            Response.End();
        }
        return result.ToString();
    }
    
}