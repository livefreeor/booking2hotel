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


public partial class ajax_rd_destination_save_edit : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (this.Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["des"]))
            {
                Response.Write(SaveDestination());
                Response.End();
            }
           
            
        }
        
    }


    public string SaveDestination()
    {
        string result = "False";
        Destination cDestination = new Destination();
        DestinationName cDestinationNames = new DestinationName();
        DestinationName cDestinationNamesTh = new DestinationName();

        try
        {

            short shrDestination = short.Parse(Request.QueryString["des"]);
            
            string title = Request.Form["title_en_" + shrDestination];
            string titleTh = Request.Form["title_th_" + shrDestination];

            string FileEn = Request.Form["filename_en_" + shrDestination];
            string FileTh = Request.Form["filename_th_" + shrDestination];

            string DescriptionEn = Request.Form["txt_descrip_en" + shrDestination];
            string DescriptionTh = Request.Form["txt_descrip_th" + shrDestination];

            string strFolder = Request.Form["foleder_name_" + shrDestination];
            cDestination.UpdateDestination(shrDestination, title, strFolder);
            if (cDestinationNames.GetDestination(shrDestination, 1) == null) 
            {
                cDestinationNames.IntsertDestinationName(shrDestination, 1, FileEn, title, DescriptionEn);
            }else
            {
                cDestinationNames.UpdateDestinationName(shrDestination, 1, FileEn, title, DescriptionEn);
            }

            if (cDestinationNamesTh.GetDestination(shrDestination, 2) == null) 
            {
                cDestinationNames.IntsertDestinationName(shrDestination, 2, FileEn, titleTh, DescriptionTh);
            }else
            {
                cDestinationNames.UpdateDestinationName(shrDestination, 2, FileEn, titleTh, DescriptionTh);
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