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


public partial class ajax_rd_location_list : System.Web.UI.Page
{
    public string qDesId
    {
        get { return Request.QueryString["des"]; }
    }

    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.qDesId))
        {
            Response.Write(GetLocList());
            Response.End();
        }
    }


    public string GetLocList()
    {
        Location cLocation = new Location();
        

        StringBuilder result = new StringBuilder();
        try
        {

            IList<object> iListlocation = cLocation.getLocationByDetinationOptional(short.Parse(this.qDesId));
            result.Append("<h4><img src=\"../../images/content.png\" /> Location List</h4>");
            result.Append("<table>");
            result.Append("<tr><td>title</td><td>File Name</td><td>Folder Name</td><td>Save</td></tr>");
            result.Append("");
            result.Append("");
            foreach (Location location in iListlocation)
            {
                result.Append("<tr>");
                result.Append("<td>");
                result.Append("<table>");
                result.Append("<tr>");
                result.Append("<td><input type=\"textbox\" style=\"width:150px;\"  class=\"textBoxStyle_s\" value=\"" + location.TitleEng + "\" name=\"title_en_" + location.LocationID + "\" /></td>");
                result.Append("</tr>");
                result.Append("<tr>");
                result.Append("<td><input type=\"textbox\"  style=\"width:150px;\" class=\"textBoxStyle_s\" value=\"" + location.TitleTHai + "\" name=\"title_th_" + location.LocationID + "\" /></td>");
                result.Append("</tr>");
                result.Append("</table>");
                result.Append("</td>");
                result.Append("<td>");
                result.Append("<table>");
                result.Append("<tr>");
                result.Append("<td><input type=\"textbox\" style=\"width:300px;\" class=\"textBoxStyle_s\" value=\"" + location.FileNameENG + "\" name=\"filename_en_" + location.LocationID + "\" /></td>");
                result.Append("</tr>");
                result.Append("<tr>");
                result.Append("<td><input type=\"textbox\" style=\"width:300px;\" class=\"textBoxStyle_s\" value=\"" + location.FileNameTHAI + "\" name=\"filename_th_" + location.LocationID + "\" /></td>");
                result.Append("</tr>");
                result.Append("</table>");
                result.Append("</td>");
                result.Append("<td><input type=\"textbox\" style=\"width:150px;\" class=\"textBoxStyle_s\" value=\"" + location.FolderName + "\" name=\"foleder_name_" + location.LocationID + "\" /></td>");
                result.Append("<td><input type=\"button\" value=\"Save\" onclick=\"Saveedit('" + location.LocationID + "')\" /></td>");
                result.Append("");
                result.Append("");
                result.Append("");
                result.Append("</tr>");
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