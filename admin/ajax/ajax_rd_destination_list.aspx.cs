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


public partial class ajax_rd_destination_list : System.Web.UI.Page
{
  

    
    protected void Page_Load(object sender, EventArgs e)
    {

        Response.Write(GetDestinationList());
            Response.End();
        
    }


    public string GetDestinationList()
    {
        DestinationName dDestinationContent = new DestinationName();
        
        Destination dDestination = new Destination();
        Location cLocation = new Location();
        

        StringBuilder result = new StringBuilder();
        try
        {

            IList<object> iListDes = dDestination.GetDestinationAllList();
            result.Append("<h4><img src=\"../../images/content.png\" /> Destination List</h4>");
            result.Append("<table>");
            result.Append("<tr><td>title</td><td>File Name</td></tr>");
            result.Append("");
            result.Append("");
            foreach (Destination des in iListDes)
            {
                dDestinationContent = dDestinationContent.GetDestination(des.DestinationID, 1);
                DestinationName dDestinationContentTh = new DestinationName();
                dDestinationContentTh = dDestinationContentTh.GetDestination(des.DestinationID, 2);
                dDestination = dDestination.GetDestinationById(des.DestinationID);
                string TitleTh = string.Empty;
                string fileNameTh = string.Empty;
                string Des = string.Empty;
                if (dDestinationContentTh != null)
                {
                    TitleTh = dDestinationContentTh.Title;
                    fileNameTh = dDestinationContentTh.FileName;
                    Des = dDestinationContentTh.ShortDetail;
                }
                result.Append("<tr>");
                result.Append("<td>");
                result.Append("<table>");
                result.Append("<tr>");
                result.Append("<td><input type=\"textbox\" style=\"width:150px;\"  class=\"textBoxStyle_s\" value=\"" + dDestinationContent.Title + "\" name=\"title_en_" + des.DestinationID + "\" /></td>");
                result.Append("</tr>");
                result.Append("<tr>");
                result.Append("<td><input type=\"textbox\"  style=\"width:150px;\" class=\"textBoxStyle_s\" value=\"" + TitleTh + "\" name=\"title_th_" + des.DestinationID + "\" /></td>");
                result.Append("</tr>");
                result.Append("</table>");
                result.Append("</td>");
                result.Append("<td>");
                result.Append("<table>");
                result.Append("<tr>");
                result.Append("<td><input type=\"textbox\" style=\"width:300px;\" class=\"textBoxStyle_s\" value=\"" + dDestinationContent.FileName + "\" name=\"filename_en_" + des.DestinationID + "\" /></td>");
                result.Append("</tr>");
                result.Append("<tr>");
                result.Append("<td><input type=\"textbox\" style=\"width:300px;\" class=\"textBoxStyle_s\" value=\"" + fileNameTh + "\" name=\"filename_th_" + des.DestinationID + "\" /></td>");
                result.Append("</tr>");
                result.Append("</table>");
                result.Append("</td>");
                result.Append("</td>");
                result.Append("<td><input type=\"textbox\" style=\"width:150px;\" class=\"textBoxStyle_s\" value=\"" + dDestination.FolderDestination + "\" name=\"foleder_name_" + des.DestinationID + "\" /></td>");
                result.Append("");
                result.Append("");
                result.Append("");
                result.Append("</tr>");
                result.Append("<tr><td colspan=\"7\">");
                result.Append("<textarea name=\"txt_descrip_en" + des.DestinationID + "\" rows=\"5\" cols=\"20\" style=\"width:100%\">" + dDestinationContent.ShortDetail + "</textarea>");
                result.Append("</td></tr>");
                result.Append("<tr><td colspan=\"7\">");
                result.Append("<textarea name=\"txt_descrip_th" + des.DestinationID + "\" rows=\"5\"  cols=\"20\" style=\"width:100%\">" + Des + "</textarea>");
                result.Append("</td></tr>");
                result.Append("<tr><td><input type=\"button\" value=\"Save\" onclick=\"Saveedit('" + des.DestinationID + "')\" /></td></tr>");

            }
            result.Append("");
            result.Append("");
            result.Append("");
            result.Append("");
            result.Append("</table>");
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            Response.End();
        }
        return result.ToString();
    }
    
}