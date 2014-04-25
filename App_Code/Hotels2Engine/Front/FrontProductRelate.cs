using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Hotels2thailand.DataAccess;

/// <summary>
/// Summary description for FrontProductRelate
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class FrontProductRelate:Hotels2BaseClass
    {
        public int ProductID { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public short DestinationID { get; set; }
        public string DestinationTitle { get; set; }
        public float Star { get; set; }
        public short LocationID { get; set; }
        public string LocationTitle { get; set; }
        private byte _langID = 1;

        public byte LangID
        {
            set { _langID = value; }
        }

        private List<FrontProductRelate> productRelateList;
	    public FrontProductRelate()
	    {
		    //
		    // TODO: Add constructor logic here
		    //
	    }

        public void LoadAllProductInDestination(int destinationID,byte categoryID)
        {
            string productTitleDefault = string.Empty;
            string filePath = string.Empty;

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select p.product_id,";
                sqlCommand = sqlCommand + "(select spc.title from tbl_product_content spc where spc.product_id=p.product_id and spc.lang_id=" + _langID + ") as second_lang,";
                sqlCommand = sqlCommand + " pc.title,(d.folder_destination+'-'+pcat.folder_cat+'/'+pc.file_name_main) as file_name,p.destination_id,dn.title as destination_title,p.star,pl.location_id,ln.title as location_title";
                sqlCommand = sqlCommand + " from tbl_product p,tbl_product_content pc,tbl_destination d,tbl_destination_name dn,tbl_product_category pcat,tbl_product_location pl,tbl_location_name ln";
                sqlCommand = sqlCommand + " where d.destination_id=dn.destination_id and d.destination_id=p.destination_id and p.product_id=pc.product_id and p.cat_id= pcat.cat_id and pl.product_id=p.product_id and pl.location_id=ln.location_id";
                sqlCommand = sqlCommand + " and dn.lang_id=" + _langID + " and pc.lang_id=1 and ln.lang_id=" + _langID + " and p.status=1";
                sqlCommand = sqlCommand + " and p.cat_id=" + categoryID;
                sqlCommand = sqlCommand + " and d.destination_id=" + destinationID;
                sqlCommand = sqlCommand + " order by pc.title asc";
                //HttpContext.Current.Response.Write(sqlCommand);
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                productRelateList = new List<FrontProductRelate>();
                while (reader.Read())
                {
                    filePath = reader["file_name"].ToString();
                    if(_langID==1)
                    {
                        productTitleDefault = reader["title"].ToString();
                    }else{
                        productTitleDefault = reader["second_lang"].ToString();
                        filePath = filePath.Replace(".asp", "-th.asp");
                        if (string.IsNullOrEmpty(productTitleDefault))
                        {
                            productTitleDefault = reader["title"].ToString();
                        }
                                               
                    }
                    //HttpContext.Current.Response.Write(filePath+"<br>");
                    productRelateList.Add(new FrontProductRelate
                    {
                        ProductID = (int)reader["product_id"],
                        Title = productTitleDefault,
                        FileName =filePath,
                        DestinationID = (short)reader["destination_id"],
                        DestinationTitle = reader["destination_title"].ToString(),
                        Star = (float)reader["star"],
                        LocationID = (short)reader["location_id"],
                        LocationTitle = reader["location_title"].ToString()
                    });
                }
            }
            
        }
        public string RenderProductRelateforRatePage(ProductDetail detail)
        {
            int countCol = 0;
            string ProductRelateDisplay = "";
            
            foreach (FrontProductRelate item in productRelateList)
            {
                if (detail.LocationID == item.LocationID)
                {
                    if(countCol==0)
                    {
                        ProductRelateDisplay = ProductRelateDisplay + "<div id=\"tophotel_footer\"> <h3>All hotels in "+item.LocationTitle+"</h3> </div>\n";
                        ProductRelateDisplay = ProductRelateDisplay + "<table id=\"content_tophotel_footer_b\">\n";
                        ProductRelateDisplay = ProductRelateDisplay + "<tr valign=\"top\">\n";
                    }
                    if (countCol % 3 == 0)
                    {
                        ProductRelateDisplay = ProductRelateDisplay + "</tr><tr>\n";
                    }
                    ProductRelateDisplay = ProductRelateDisplay + "<td class=\"all_col\">  <a href=\"/" + item.FileName + "\">" + item.Title + "</a> </td>\n";
                    countCol = countCol + 1;
                }

            }
            ProductRelateDisplay = ProductRelateDisplay + "</tr>\n";

            ProductRelateDisplay = ProductRelateDisplay + "</table>\n";
            ProductRelateDisplay = ProductRelateDisplay + "<div id=\"bg_end_footer\"></div><br />\n";
            return ProductRelateDisplay;
        }
        public string RenderProductRelateforRatePage(FrontProductDetail detail)
        {
            int countCol = 0;
            string ProductRelateDisplay = "";

            foreach (FrontProductRelate item in productRelateList)
            {
                if (detail.LocationID == item.LocationID)
                {
                    if (countCol == 0)
                    {
                        if(_langID==1)
                        {
                            ProductRelateDisplay = ProductRelateDisplay + "<div id=\"tophotel_footer\"> <h3>All hotels in " + item.LocationTitle + "</h3> </div>\n";
                        }else{
                            ProductRelateDisplay = ProductRelateDisplay + "<div id=\"tophotel_footer\"> <h3>โรงแรมทั้งหมดที่อยู่ใน" + item.LocationTitle + "</h3> </div>\n";
                        }
                       
                        ProductRelateDisplay = ProductRelateDisplay + "<table id=\"content_tophotel_footer_b\">\n";
                        ProductRelateDisplay = ProductRelateDisplay + "<tr valign=\"top\">\n";
                    }
                    if (countCol % 3 == 0)
                    {
                        ProductRelateDisplay = ProductRelateDisplay + "</tr><tr>\n";
                    }
                    ProductRelateDisplay = ProductRelateDisplay + "<td class=\"all_col\">  <a href=\"/" + item.FileName + "\">" + item.Title + "</a> </td>\n";
                    countCol = countCol + 1;
                }

            }
            ProductRelateDisplay = ProductRelateDisplay + "</tr>\n";

            ProductRelateDisplay = ProductRelateDisplay + "</table>\n";
            ProductRelateDisplay = ProductRelateDisplay + "<div id=\"bg_end_footer\"></div><br />\n";
            return ProductRelateDisplay;
        }
        public string RenderProductRelateClassforRatePage(FrontProductDetail detail)
        {
            
            int countCol = 0;
            string ProductRelateDisplay = "";
            if(_langID==1)
            {
                ProductRelateDisplay = ProductRelateDisplay + "<div id=\"tophotel_footer\"> <h2>Related " + Utility.GetProductType(detail.CategoryID)[0, 1] + " of " + detail.Title + "</h2> </div>\n";
            }else{
                ProductRelateDisplay = ProductRelateDisplay + "<div id=\"tophotel_footer\"> <h2>" + Utility.GetProductType(detail.CategoryID)[0, 4] + "ทั้งหมดที่ระดับดาวใกล้เคียงกับ" + detail.Title + "</h2> </div>\n";
            }
            
            ProductRelateDisplay = ProductRelateDisplay + "<table id=\"content_tophotel_footer_b\">\n";
            ProductRelateDisplay = ProductRelateDisplay + "<tr valign=\"top\">\n";
            foreach (FrontProductRelate item in productRelateList)
            {
                if (detail.LocationID == item.LocationID && (item.Star>=(detail.Star-0.5) && item.Star<(detail.Star+0.5)))
                {
                    
                    if (countCol % 3 == 0)
                    {
                        ProductRelateDisplay = ProductRelateDisplay + "</tr><tr>\n";
                    }
                    ProductRelateDisplay = ProductRelateDisplay + "<td class=\"all_col\">  <a href=\"/" + item.FileName + "\">" + item.Title + "</a> </td>\n";
                    countCol = countCol + 1;
                }

            }
            ProductRelateDisplay = ProductRelateDisplay + "</tr>\n";

            ProductRelateDisplay = ProductRelateDisplay + "</table>\n";
            ProductRelateDisplay = ProductRelateDisplay + "<div id=\"bg_end_footer\"></div><br />\n";
            return ProductRelateDisplay;
        }
        public string RenderProductRelateClassforRatePage(ProductDetail detail)
        {
            int countCol = 0;
            string ProductRelateDisplay = "";
            ProductRelateDisplay = ProductRelateDisplay + "<div id=\"tophotel_footer\"> <h2>Related " + Utility.GetProductType(detail.CategoryID)[0, 1] + " of " + detail.Title + "</h2> </div>\n";
            ProductRelateDisplay = ProductRelateDisplay + "<table id=\"content_tophotel_footer_b\">\n";
            ProductRelateDisplay = ProductRelateDisplay + "<tr valign=\"top\">\n";
            foreach (FrontProductRelate item in productRelateList)
            {
                if (detail.LocationID == item.LocationID && (item.Star >= (detail.Star - 0.5) && item.Star < (detail.Star + 0.5)))
                {
                    if (countCol % 3 == 0)
                    {
                        ProductRelateDisplay = ProductRelateDisplay + "</tr><tr>\n";
                    }
                    ProductRelateDisplay = ProductRelateDisplay + "<td class=\"all_col\">  <a href=\"/" + item.FileName + "\">" + item.Title + "</a> </td>\n";
                    countCol = countCol + 1;
                }

            }
            ProductRelateDisplay = ProductRelateDisplay + "</tr>\n";

            ProductRelateDisplay = ProductRelateDisplay + "</table>\n";
            ProductRelateDisplay = ProductRelateDisplay + "<div id=\"bg_end_footer\"></div><br />\n";
            return ProductRelateDisplay;
        }
    }
}