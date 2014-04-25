using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using Hotels2thailand.Production;
using Hotels2thailand;
using System.Net.Mail;

public partial class ajax_product_thaicontent_preview : System.Web.UI.Page
{
   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Page.IsPostBack)
        {
            Response.Write(Preview());
            Response.End();
        }
    }



    public string Preview()
    {
        string result = string.Empty;
        try
        {
            string detail = Request.Form["detail"];

            result = detail.Trim().Hotels2XMlReader();
        }
        catch
        {
            Response.Write("error");
            Response.End();
        }
        



        return result;
    }

    
}