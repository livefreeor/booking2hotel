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


public partial class ajax_rd_destination_add : System.Web.UI.Page
{
    public string qLocationId
    {
        get { return Request.QueryString["loc"]; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Page.IsPostBack)
        {
            //Response.Write("HELLO");
            Response.Write(SaveDestination());
                Response.End();
            
        }
        
    }


    public string SaveDestination()
    {
        string result = "False";
        Destination cDestination = new Destination();
        DestinationName cDestinationName = new DestinationName();


        try
        {
            //short shrDestinationId = short.Parse(Request.Form["hd_default_des"]);


            string title = Request.Form["txt_title"];
            string titleTh = Request.Form["txt_title_th"];

            string FileEn = Request.Form["txt_filename"];
            string FileTh = Request.Form["txt_filename_th"];

            string DescriptionEn = Request.Form["txt_descrip_en"];
            string DescriptionTh = Request.Form["txt_descrip_th"];

            string Folder = Request.Form["txt_folder"];

            
            short shrDesId = cDestination.InsertNewDestination(title, Folder);

            cDestinationName.IntsertDestinationName(shrDesId, 1, FileEn, title, DescriptionEn);
            cDestinationName.IntsertDestinationName(shrDesId, 2, FileEn, titleTh, DescriptionTh);

           
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