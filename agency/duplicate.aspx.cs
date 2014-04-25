using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using Hotels2thailand;
using Hotels2thailand.Front;
public partial class agency_duplicate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            WebClient cWeb = new WebClient();
            string url = "http://www.hotels2thailand.com/bangkok-hotels/the-berkeley-hotel-pratunam-bangkok.asp";
            byte[] bytRequestedHTML;
            bytRequestedHTML = cWeb.DownloadData(url);
            UTF8Encoding cUTF8 = new UTF8Encoding();
            string strRequestedHTML = cUTF8.GetString(bytRequestedHTML);
            strRequestedHTML = strRequestedHTML
                .Replace("href=\"/", "href=\"http://www.hotels2thailand.com/")
                .Replace("src=\"/", "src=\"http://www.hotels2thailand.com/")
   .Replace("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">","");

            //string strKey1 = Utility.GetKeywordReplace(strRequestedHTML, "<!--Box Search Control Script-->", "<!--End Box Search Control Script-->");
            //strRequestedHTML = strRequestedHTML.Replace(strKey1, " ");
           //src="/
            Response.Write(strRequestedHTML);
            Response.End();

           //lbltext.Text = strRequestedHTML;
            
        }
    }
}