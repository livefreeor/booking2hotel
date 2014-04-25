using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ProductRelate
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class ProductRelate:Hotels2BaseClass
    {
        public string Title { get; set; }
        public string Filename { get; set; }
        public float Star { get; set; }
        public short DestinationID { get; set; }
        public short LocationID { get; set; }
        private byte _langID = 1;

        public byte LangID
        {
            set { _langID = value; }
        }
        public ProductRelate()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string RenderLocationRelate(ProductDetail product)
        {
            LinkGenerator link = new LinkGenerator();
            link.LoadData(_langID);
            string data = string.Empty;
            List<ProductRelate> productRelate = GetProductInLocation(product);


            data = data + "<h4>RELATED HOTELS OF "+product.Title+"</h4>\n";

            data = data + "<div id=\"related\">\n";
            foreach (ProductRelate item in productRelate)
            {
                data = data + "<a href=\"" + link.GetProductPath(item.DestinationID, product.CategoryID, product.FileMain) + "\">" + item.Title + "</a>\n";
            }
            data = data + "</div>\n";
            return data;
        }
        public List<ProductRelate> GetProductInLocation(ProductDetail product)
        {
            bool hasLanguage = false;
            List<ProductRelate> productRelateList = new List<ProductRelate>();
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select pc.title,pc.file_name_main,p.star,p.destination_id,pl.location_id";
                sqlCommand = sqlCommand + " from tbl_product p,tbl_product_location pl,tbl_product_content pc";
                sqlCommand = sqlCommand + " where p.product_id=pl.product_id and p.product_id= pc.product_id and pc.lang_id="+_langID+" and pl.location_id=" + product.LocationID;
                sqlCommand = sqlCommand + " and";
                sqlCommand = sqlCommand + " (star=(" + product.Star + "+0.5) or star=(" + product.Star + "-0.5) or star=" + product.Star + ")";
                sqlCommand = sqlCommand + " order by title";

                //HttpContext.Current.Response.Write(sqlCommand);
                //HttpContext.Current.Response.End();
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                

                while (reader.Read())
                {
                    hasLanguage = true;
                    productRelateList.Add(new ProductRelate
                    {
                        Title = reader["title"].ToString(),
                        Filename = reader["file_name_main"].ToString(),
                        Star = (float)reader["star"],
                        DestinationID = (short)reader["destination_id"],
                        LocationID = (short)reader["location_id"]
                    });

                }
                

            }

            if(!hasLanguage)
            {
                using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                {
                    string sqlCommand = "select pc.title,pc.file_name_main,p.star,p.destination_id,pl.location_id";
                    sqlCommand = sqlCommand + " from tbl_product p,tbl_product_location pl,tbl_product_content pc";
                    sqlCommand = sqlCommand + " where p.product_id=pl.product_id and p.product_id= pc.product_id and pc.lang_id=1 and pl.location_id=" + product.LocationID;
                    sqlCommand = sqlCommand + " and";
                    sqlCommand = sqlCommand + " (star=(" + product.Star + "+0.5) or star=(" + product.Star + "-0.5) or star=" + product.Star + ")";
                    sqlCommand = sqlCommand + " order by title";

                    //HttpContext.Current.Response.Write(sqlCommand);
                    //HttpContext.Current.Response.End();
                    SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    

                    while (reader.Read())
                    {
                        productRelateList.Add(new ProductRelate
                        {
                            Title = reader["title"].ToString(),
                            Filename = reader["file_name_main"].ToString(),
                            Star = (float)reader["star"],
                            DestinationID = (short)reader["destination_id"],
                            LocationID = (short)reader["location_id"]
                        });

                    }


                }
            }
            return productRelateList;
        }

        public List<ProductRelate> GetProductInDestination(ProductDetail product)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select pc.title,pc.file_name_main,p.star,p.destination_id,pl.location_id";
                sqlCommand = sqlCommand + " from tbl_product p,tbl_product_location pl,tbl_product_content pc";
                sqlCommand = sqlCommand + " where p.product_id=pl.product_id and p.product_id= pc.product_id and p.destination=" + product.DestinationID;
                sqlCommand = sqlCommand + " and";
                sqlCommand = sqlCommand + " (star=(" + product.Star + "+0.5) or star=(" + product.Star + "-0.5) or star=" + product.Star + ")";
                sqlCommand = sqlCommand + " order by title";
                sqlCommand = sqlCommand + " }";

                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                List<ProductRelate> productRelate = new List<ProductRelate>();

                while (reader.Read())
                {
                    productRelate.Add(new ProductRelate
                    {
                        Title = reader["title"].ToString(),
                        Filename = reader["file_name"].ToString(),
                        Star = (float)reader["star"],
                        DestinationID = (short)reader["destination_id"],
                        LocationID = (short)reader["location_id"]
                    });
                }
                return productRelate;
            }

            
        }
    }
}