using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using Hotels2thailand;
using System.Net.Mail;
using System.IO;
public partial class ajax_staff_authorize_page_refresh : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Page.IsPostBack)
        {
            Response.Write(btnRefresh());
            Response.End();
            
        }
    }


    public string btnRefresh()
    {
        string result = "false";
        try
        {

            byte MainModuleId = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$dropMainModule"]);
            byte ModuleId = byte.Parse(Request.Form["ctl00$ContentPlaceHolder1$dropModule"]);
            

            StaffPageAuthorize cPageResult = new StaffPageAuthorize();
            string folderModule = cPageResult.getStaffModuleFolderName(ModuleId);
            string folderMainModule = cPageResult.getStaffMainModuleFolderName(MainModuleId);
            DirectoryInfo FolderMain = new DirectoryInfo(Server.MapPath("~/" + folderMainModule + ""));
             DirectoryInfo FolderModule = new DirectoryInfo(Server.MapPath("~/" + folderMainModule + "/"+folderModule+""));
            if (MainModuleId > 0)
            {
                
                if (FolderMain.Exists && FolderModule.Exists)
                {
                   
                   foreach (FileInfo filename in FolderModule.GetFiles())
                    {
                        if (filename.Name.ToString().Split('.').Count() < 3 && filename.Name.ToString().IndexOf("ajax") == -1)
                        {
                            bool IsHave = false;
                            foreach (StaffPageAuthorize obj in cPageResult.GetListPageAuthorizeByModulId(ModuleId))
                            {
                                if (obj.PageFileName == filename.Name.ToString())
                                {
                                    IsHave = true;
                                }
                            }
                            if (!IsHave)
                            {
                                cPageResult.InsertPageFile(filename.Name.ToString(), ModuleId, filename.CreationTime.Hotels2ThaiDateTime(), 
                                    DateTime.Now.Hotels2ThaiDateTime());
                            }
                        }
                    }
                }
             }
            

            result = "true";
        }
        catch (Exception ex)
        {
            Response.Write("error:"  + ex.Message);
            Response.End();
        }

        return result;

    }
    
    
}