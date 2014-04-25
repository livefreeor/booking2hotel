using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using System.Net.Mail;

public partial class ajax_staff_authorize_page_authorize : System.Web.UI.Page
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
        byte bytstaffCat = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$drop_staff_cat"]);
        try
        {
            StaffPageAuthorize cStaffPage = new StaffPageAuthorize();
            StaffPageAuthorizeResult cStaffPageAuthorize = new StaffPageAuthorizeResult();
            IDictionary<byte, string> idicMainModule = cStaffPage.getdicStaffModuleMain();
            foreach (KeyValuePair<byte, string> mainmodule in idicMainModule)
            {
                result.Append("<div id=\"main_module_" + mainmodule.Key + "\" class=\"main_module\">");
                result.Append("<p class=\"main_modult_title\">" + mainmodule.Value + "<p>");
                IDictionary<byte, string> idicModule = cStaffPage.getdicStaffModule(mainmodule.Key);
                foreach (KeyValuePair<byte, string> module in idicModule)
                {
                    result.Append("<div id=\"module_" + module.Key + "\" class=\"module\">");
                    result.Append("<p class=\"modult_title\">" + module.Value + " ");
                    result.Append("<a href=\"\" id=\"a_checkAll_" + module.Key + "\"  >check all</a>&nbsp;|&nbsp;");
                    result.Append("<a href=\"\" id=\"a_clearall_" + module.Key + "\"  >clear</a>&nbsp;");
                    result.Append("<input type=\"button\" value=\"save\"  onclick=\"SavePageAuthorize();\"  /><p>");
                    result.Append("<div class=\"table_page_list\">");
                    result.Append("<table class=\"table_module_page\" cellpadding=\"0\" cellspacing=\"2\">");
                    result.Append("<tr style=\"background-color:#3f5d9d\"><th>Page id</th><th>File Name</th><th>Is Access</th></tr>");
                    string IsCheck = string.Empty;
                    int count = 1;
                    foreach (StaffPageAuthorizeResult Page in cStaffPageAuthorize.GetStaffPageResult_Action(true, module.Key, bytstaffCat))
                    {

                        string bg = string.Empty;
                        if (count % 2 == 0)
                            bg = "#f2f2f2";
                        else
                            bg = "#ffffff";

                        result.Append("<tr style=\"background-color:"+bg+"\">");
                        if (Page.staffCAtIsAuthorize > 0)
                            IsCheck = "checked=\"checked\"";
                        else
                            IsCheck = string.Empty;

                        result.Append("<td>" + Page.PageId + "</td>");
                        result.Append("<td style=\"padding:0px 0px 0px 5px;text-align:left;\">"+Page.PageFileName+"</td>");
                        result.Append("<td><input type=\"checkbox\" " + IsCheck + " value=\"" + Page.PageId + "\" id=\"check_page_auth_" + Page.PageId + "\" name=\"check_page_auth\" /></td>");
                        result.Append("");
                        result.Append("</tr>");
                        count = count + 1;
                    }
                    result.Append("</table>");
                    result.Append("</div>");
                    result.Append("</div>");
                }
                

                result.Append("</div>");
            }
            
        }
        catch (Exception ex)
        {
            Response.Write("error : " + ex.Message);
            Response.End();
        }
        return result.ToString();
    }
    
}