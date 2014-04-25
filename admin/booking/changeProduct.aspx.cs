using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Hotels2thailand.DataAccess;
using Hotels2thailand.Production;


public partial class admin_booking_changeProduct : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
           

            string sqlCommand = "select p.product_id,p.title";
            sqlCommand = sqlCommand + " from tbl_product p,tbl_product_location pl ";
            sqlCommand = sqlCommand + " where p.product_id=pl.product_id  and pl.location_id IN ";
            sqlCommand = sqlCommand + " (";
            sqlCommand = sqlCommand + " select top 1 spl.location_id";
            sqlCommand = sqlCommand + " from tbl_product sp,tbl_product_location spl ";
            sqlCommand = sqlCommand + " where sp.product_id=spl.product_id  and sp.product_id =52";
            sqlCommand = sqlCommand + " )";
            sqlCommand = sqlCommand + " and p.product_id<>52 and p.status=1";
            sqlCommand = sqlCommand + " order by p.title asc";

            DataConnect objConn = new DataConnect();
            SqlDataReader reader = objConn.GetDataReader(sqlCommand);

            int countItem = 0;

            string productlist = "<table><tr>";
            while (reader.Read())
            {
                if (countItem % 3 == 0)
                {
                    productlist = productlist + "</tr><tr>";
                }
                productlist = productlist + "<td><input type=\"radio\" name=\"pid\" value=\"" + reader["product_id"] + "\">" + reader["title"] + "</td>";
                countItem = countItem + 1;
            }
            productlist = productlist + "</tr></table>";
            objConn.Close();

            lrlProduct.Text = productlist;
            ProductCatDataBind();
            DestinationDataBind();
        }
    }

    protected void DestinationDataBind()
    {
        StringBuilder desResult = new StringBuilder();
        Destination cDestination = new Destination();
        desResult.Append("<select id=\"dropDestination\">");
        foreach (KeyValuePair<string, string> Item in cDestination.GetDestinationAll())
        {
            if (Item.Key == "30")
                desResult.Append("<option value=\"" + Item.Key + "\" selected=\"selected\">" + Item.Value + "</option>");
            else
                desResult.Append("<option value=\"" + Item.Key + "\">" + Item.Value + "</option>");
        }
        desResult.Append("</select>");

        lrlDes.Text = desResult.ToString();
    }

    protected void ProductCatDataBind()
    {
        StringBuilder desResult = new StringBuilder();
        ProductCategory cProductCat = new ProductCategory();
        desResult.Append("<select id=\"dropProductCat\">");
        foreach (KeyValuePair<string, string> Item in cProductCat.GetProductCategory())
        {
            if (Item.Key == "29")
                desResult.Append("<option value=\"" + Item.Key + "\" selected=\"selected\">" + Item.Value + "</option>");
            else
                desResult.Append("<option value=\"" + Item.Key + "\">" + Item.Value + "</option>");
        }
        desResult.Append("</select>");

        lrlProductCat.Text = desResult.ToString();
    }
}