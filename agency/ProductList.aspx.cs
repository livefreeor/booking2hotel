using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Hotels2thailand;
using Hotels2thailand.Booking;
using Hotels2thailand.Production;
using Hotels2thailand.Front;

public partial class agency_ProductList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Agency_ID"] != null && Session["Agency_ID"].ToString() != "" && Session["Agency_ID"].ToString() != "0") || (Request.QueryString["Staff"] != null && Request.QueryString["Staff"].ToString() == "BHT"))
        {
            if (!IsPostBack)
            {
                byte bytCatID = 0;
                int intDesID = 0;
                if (Request.QueryString["CatID"] != null)
                {
                    bytCatID = Convert.ToByte(Request.QueryString["CatID"].ToString());
                }
                if (Request.QueryString["DesID"] != null)
                {
                    intDesID = Convert.ToInt32(Request.QueryString["DesID"].ToString());
                }
                showDetail(bytCatID, intDesID);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Session timeout. Please login again.');", true);
            Response.Redirect("Default.aspx");
        }        
    }

    private void showDetail(byte bytProductCatID, int intDestinationID)
    {
        StringBuilder cStringBuilder = new StringBuilder();
        Product cProduct = new Product();
        //IList<object> ilistProduct = cProduct.GetB2BProductAllByCatAndDes(0, 0);

        ProductCategory cProductCategory = new ProductCategory();
        string strCatTitle = cProductCategory.getProductCatTitle(bytProductCatID);
        string strThumbnail = "";
        cStringBuilder.Append("<h1>" + strCatTitle + "</h1>");
        Destination cDestination = new Destination();
        Dictionary<string, string> dicDestination = cDestination.GetB2BDestinationByProductCatID(bytProductCatID);
        DataTable dtProduct = new DataTable();
        if (intDestinationID == 0)
        {            
            dtProduct = cProduct.GetB2BProductAllByCatAndDes(bytProductCatID);
            if (dtProduct != null && dtProduct.Rows.Count > 0)
            {
                foreach (KeyValuePair<string, string> item in dicDestination)
                {
                    cStringBuilder.Append("<p class='title_desi'>" + strCatTitle + " in : " + item.Value + "</p>");
                    dtProduct.DefaultView.RowFilter = "";
                    dtProduct.DefaultView.RowFilter = " DestinationTitle = '" + item.Value + "' ";
                    if (dtProduct.DefaultView.Count > 0)
                    {
                        for (int i = 0; i < dtProduct.DefaultView.Count; i++)
                        {
                            FrontProductDetail cFrontproductDetail = new FrontProductDetail();
                            cFrontproductDetail.GetProductDetailByID(Convert.ToInt32(dtProduct.DefaultView[i]["product_id"].ToString()), bytProductCatID, 1);
                            strThumbnail = cFrontproductDetail.Thumbnail;
                            cStringBuilder.Append("<div class='product_box'>");
                            cStringBuilder.Append("<img src='" + strThumbnail + "' class='img_product' />");
                            cStringBuilder.Append("<p><a href='book.aspx?pid=" + dtProduct.DefaultView[i]["product_id"].ToString() + "' class='title-nameproduct'>" + dtProduct.DefaultView[i]["title"].ToString() + "</a></p>");
                            cStringBuilder.Append("<p><a href='book.aspx?pid=" + dtProduct.DefaultView[i]["product_id"].ToString() + "' class='title-location'>" + dtProduct.DefaultView[i]["DestinationTitle"].ToString() + ", Thailand</a></p>");
                            cStringBuilder.Append("</div>");
                        }
                    }
                    dtProduct.DefaultView.RowFilter = "";
                    cStringBuilder.Append("<br class='clear-all' />");
                }
            }
        }
        else
        {
            dtProduct = cProduct.GetB2BProductAllByCatAndDes(bytProductCatID, intDestinationID);
            if (dtProduct != null && dtProduct.Rows.Count > 0)
            {
                cStringBuilder.Append("<p class='title_desi'>" + strCatTitle + " in : " + dtProduct.Rows[0]["DestinationTitle"].ToString() + "</p>");
                for (int i = 0; i < dtProduct.Rows.Count; i++)
                {
                    FrontProductDetail cFrontproductDetail = new FrontProductDetail();
                    cFrontproductDetail.GetProductDetailByID(Convert.ToInt32(dtProduct.Rows[i]["product_id"].ToString()), bytProductCatID, 1);
                    strThumbnail = cFrontproductDetail.Thumbnail;
                    cStringBuilder.Append("<div class='product_box'>");
                    cStringBuilder.Append("<img src='" + strThumbnail + "' class='img_product' />");
                    cStringBuilder.Append("<p><a href='book.aspx?pid=" + dtProduct.Rows[i]["product_id"].ToString() + "' class='title-nameproduct'>" + dtProduct.Rows[i]["title"].ToString() + "</a></p>");
                    cStringBuilder.Append("<p><a href='book.aspx?pid=" + dtProduct.Rows[i]["product_id"].ToString() + "' class='title-location'>" + dtProduct.Rows[i]["DestinationTitle"].ToString() + ", Thailand</a></p>");
                    cStringBuilder.Append("</div>");
                }
            }
        }
        cStringBuilder.Append("<br class='clear-all' />");
        divDetail.InnerHtml = cStringBuilder.ToString();
    }
}