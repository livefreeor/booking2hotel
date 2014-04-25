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
using Hotels2thailand.Production;
public partial class agency_book : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Agency_ID"] != null && Session["Agency_ID"].ToString() != "" && Session["Agency_ID"].ToString() != "0")
        {
            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["pid"]))
                {

                    int intProduct_id = int.Parse(Request.QueryString["pid"]);
                    ProductBookingEngine cProductEngine = new ProductBookingEngine();
                    cProductEngine = cProductEngine.GetProductbookingEngine(intProduct_id);

                    if (cProductEngine.B2bCat != 29)
                        hd_isSingle.Value = "True";

                    if (cProductEngine.Is_B2b)
                    {
                        WebClient cWeb = new WebClient();
                        string url = cProductEngine.WebsiteName;
                        byte[] bytRequestedHTML;
                        bytRequestedHTML = cWeb.DownloadData(url);
                        UTF8Encoding cUTF8 = new UTF8Encoding();
                        string strRequestedHTML = cUTF8.GetString(bytRequestedHTML);
                        strRequestedHTML = strRequestedHTML
                            .Replace("href=\"/", "href=\"http://www.hotels2thailand.com/")
                            .Replace("src=\"/", "src=\"http://www.hotels2thailand.com/")
                .Replace("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">", "");


                        string strKey2 = Utility.GetKeywordReplace(strRequestedHTML, "<body>", "</body>");
                        strRequestedHTML = strKey2.Replace("<body>", "").Replace("</body>", "").Replace("<div id=\"main\">", "<div id=\"main\" style=\"display:none;\">");

                        string strKey3 = Utility.GetKeywordReplace(strRequestedHTML, "<!--footer-->", "</script>");
                        strRequestedHTML = strRequestedHTML.Replace(strKey3, " ");

                        lbltext.Text = strRequestedHTML;
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Session timeout. Please login again.');", true);
                Response.Redirect("Default.aspx");
            }            
        }
    }

    //protected override void OnPreRender(EventArgs e)
    //{
    //    base.OnPreRender(e);
    //    //Label1.Text += "OnPreRender ";

    //    //HtmlLink style1 = new HtmlLink();
    //    //style1.Attributes.Add("type", "text/css");
    //    //style1.Attributes.Add("rel", "stylesheet");
    //    //style1.Attributes.Add("href", "~/Css/StyleSheet.css");
    //    //Page.Header.Controls.AddAt(0,style1);

    //    HtmlGenericControl jqueryInclude = new HtmlGenericControl("script");
    //    jqueryInclude.Attributes.Add("type", "text/javascript");
    //    jqueryInclude.Attributes.Add("src", Globals.Settings.App_Base["jqueryPath"]);
    //    Page.Header.Controls.AddAt(0, jqueryInclude);

    //}
}