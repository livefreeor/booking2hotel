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
using System.Net.Mail;

public partial class ajax_product_picture_list_del : System.Web.UI.Page
{
    public string qProductId
    {
        get { return Request.QueryString["pid"]; }
    }

    public string qImageCat
    {
        get { return Request.QueryString["imgcat_id"]; }
    }
    public string qMaxrow
    {
        get { return Request.QueryString["maxrow"]; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.qProductId))
        {
            //Response.Write(Request.Url.ToString());
            Response.Write(DelPic());
            Response.End();
        }
    }

    protected string DelPic()
    {
        string Result = "False";

         
        if (!string.IsNullOrEmpty(Request.Form["checkPic_Del"]))
        {
            string[] arrPicId = Request.Form["checkPic_Del"].Split(',');
            foreach (string item in arrPicId)
            {
                int intPicId = int.Parse(item);
                ProductPic cProductPic = new ProductPic();
                cProductPic.DeleteProductPic(intPicId);
                Result = "True";
            }
        }
        else
        { Result = "Empty"; }

        return Result;
    }
    

    
}