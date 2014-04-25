using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Reviews;
public partial class affiliate_include_AffiliateFeedReview : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        int productID=int.Parse(Request.QueryString["pid"]);
        string result = string.Empty;
        ReviewGenerate objReview = new ReviewGenerate(29);
        result = objReview.GetReviewXMLByProductId(productID);
        Response.Write(result);
    }


}