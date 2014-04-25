using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using Hotels2thailand;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;
using System.Collections;
using Hotels2thailand.Reviews;
using Hotels2thailand.Booking;
using Hotels2thailand.ProductOption;
using System.Web.Configuration;
public partial class affiliate_include_AffiliateFeedPackage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int productID = int.Parse(Request.QueryString["pid"]);
        string strCurrentIP = Request.QueryString["uid"];

        string result = string.Empty;
        FrontPackageList cFront = new FrontPackageList();
        //FrontPromotionList cFront = new FrontPromotionList();

        result = cFront.GetXmlPackageFeed(productID, 1);
        Response.Write(result);
        

    }

   
}