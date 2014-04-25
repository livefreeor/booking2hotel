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


public partial class ajax_rd_location_save_edit : System.Web.UI.Page
{
    public string qLocationId
    {
        get { return Request.QueryString["loc"]; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(this.qLocationId))
            {
                Response.Write(SaveLocation());
                Response.End();
            }
        }
        
    }


    public string SaveLocation()
    {
        string result = "False";
        Location cLocation = new Location();

        try
        {

            short shrLocationId = short.Parse(this.qLocationId);

            string title = Request.Form["title_en_" + shrLocationId];
            string titleTh = Request.Form["title_th_" + shrLocationId];

            string FileEn = Request.Form["filename_en_" + shrLocationId];
            string FileTh = Request.Form["filename_th_" + shrLocationId];

            string FolderName = Request.Form["foleder_name_" + shrLocationId];

            cLocation.UpdateLocation(shrLocationId, title, FolderName);
            LocationName cLocationNameEng = new LocationName
            {
                LocationID = shrLocationId,
                LanguageID = 1,
                Title = title,
                FileName = FileEn
            };
            cLocationNameEng.Update(cLocationNameEng);

            LocationName cLocationNameThai = new LocationName
            {
                LocationID = shrLocationId,
                LanguageID = 2,
                Title = titleTh,
                FileName = FileTh
            };

            if (!string.IsNullOrEmpty(titleTh))
            {
                if (cLocationNameThai.GetLocationNameByID(shrLocationId, 2) != null)
                {
                    cLocationNameThai.Update(cLocationNameThai);
                }
                else
                {
                    cLocationNameThai.Insert(cLocationNameThai);
                }
            }
            else
            {
                if (cLocationNameThai.GetLocationNameByID(shrLocationId, 2) != null)
                {
                    cLocationNameThai.Update(cLocationNameThai);
                }
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