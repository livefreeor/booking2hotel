using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using Hotels2thailand.Production;
using Hotels2thailand.ProductOption;


public partial class ajax_rd_location_add : System.Web.UI.Page
{
    public string qLocationId
    {
        get { return Request.QueryString["loc"]; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Page.IsPostBack)
        {
            
                Response.Write(SaveLocation());
                Response.End();
            
        }
        
    }


    public string SaveLocation()
    {
        string result = "False";
        Location cLocation = new Location();
        LocationName cLocationName = new LocationName();

        try
        {
            short shrDestinationId = short.Parse(Request.Form["hd_default_des"]);


            string title = Request.Form["txt_title"];
            string titleTh = Request.Form["txt_title_th"];

            string FileEn = Request.Form["txt_filename"];
            string FileTh = Request.Form["txt_filename_th"];

            string FolderName = Request.Form["txt_folder"];



           short shrLocationId =  cLocation.InsertLocation(shrDestinationId, true, title, FolderName);
            LocationName cLocationNameEng = new LocationName
            {
                LocationID = shrLocationId,
                LanguageID = 1,
                Title = title,
                FileName = FileEn
            };
            cLocationNameEng.Insert(cLocationNameEng);

            LocationName cLocationNameThai = new LocationName
            {
                LocationID = shrLocationId,
                LanguageID = 2,
                Title = titleTh,
                FileName = FileTh
            };

            if (!string.IsNullOrEmpty(titleTh))
            {
                cLocationNameThai.Insert(cLocationNameThai);
            }
            result = "True";
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            Response.End();
        }

        return result;
    }
    
}