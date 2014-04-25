using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;
namespace Hotels2thailand.UI
{
    public partial class admin_product_thai_content_add : Hotels2BasePage
    {
        private string connString = WebConfigurationManager.ConnectionStrings["hotels2thailandXConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.qProductId))
                {
                    Product cProduct = new Product();
                    cProduct = cProduct.GetProductById(int.Parse(this.qProductId));
                    Destitle.Text = cProduct.DestinationTitle;
                    txthead.Text = cProduct.Title;





                    int productID = int.Parse(Request.QueryString["pid"]);

                    string latitude = string.Empty;
                    string longitude = string.Empty;

                    string sqlCommand = string.Empty;
                    string result = string.Empty;
                    using (SqlConnection cn = new SqlConnection(connString))
                    {
                        sqlCommand = "select top 1 coor_lat,coor_long from tbl_product where product_id=" + productID;

                        SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                        cn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            latitude = reader["coor_lat"].ToString();
                            longitude = reader["coor_long"].ToString();
                        }
                    }

                    using (SqlConnection cn = new SqlConnection(connString))
                    {
                        sqlCommand = "select  product_id,title,title_second,address,detail_short,detail,direction ";
                        sqlCommand = sqlCommand + " from tbl_product_content  where product_id=" + productID + " and lang_id=1";
                        sqlCommand = sqlCommand + " union all";
                        sqlCommand = sqlCommand + " select  product_id,title,title_second,address,detail_short,detail,direction";
                        sqlCommand = sqlCommand + " from tbl_product_content  where product_id=" + productID + " and lang_id=2";

                        SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                        cn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        //result = result + "<form action=\"contentAddPcs.aspx\" method=\"post\">\n";
                        result = result + "<table id=\"listItem\" cellspacing=\"2\" cellpadding=\"5\">\n";
                        result = result + "<tr><th>Englist</th>\n";
                        result = result + "<th>Thai</th></tr>\n";

                        int countContent = 1;
                        while (reader.Read())
                        {
                            if (countContent == 1)
                            {
                                result = result + "<tr>\n";
                                result = result + "<td valign=\"top\">\n";
                                result = result + "Title:<br/>";
                                result = result + "<input type=\"text\" name=\"title_default\" class=\"text400\" value=\"" + reader["title"] + "\"/><br />\n";
                                //result = result + "Title Second:<br />";
                                //result = result + "<input type=\"text\" name=\"title_second_default\"  class=\"text400\"  value=\"" + reader["title_second"] + "\"/><br />\n";
                                result = result + "Address:<br />\n";
                                result = result + "<textarea class=\"textArea400\" name=\"address_default\">" + reader["address"] + "</textarea><br />\n";
                                result = result + "Short Detail:<br />\n";
                                result = result + "<textarea class=\"textArea400\" name=\"detail_short_default\">" + reader["detail_short"] + "</textarea><br />\n";
                                result = result + "Detail:<br />";
                                result = result + "<textarea class=\"textArea400\" name=\"detail_default\">" + System.Web.HttpUtility.HtmlEncode(reader["detail"].ToString()) + "</textarea><br />\n";
                                //result = result + "Direction:<br />\n";
                                //result = result + "<textarea class=\"textArea400\" name=\"direction_default\">" + reader["direction"] + "</textarea><br />\n";
                                result = result + "</td>\n";


                                //System.Web.HttpUtility.HtmlDecode(reader["detail"].ToString())
                            }
                            else
                            {
                                result = result + "<td>\n";
                                result = result + "Title:<br/>\n";
                                result = result + "<input type=\"text\" class=\"text400\" name=\"title\" value=\"" + reader["title"] + "\"/><br />\n";
                                //result = result + "Title Second:<br />";
                                //result = result + "<input type=\"text\"  class=\"text400\" name=\"title_second\"  value=\"" + reader["title_second"] + "\"/><br />\n";
                                result = result + "Address:<br />\n";
                                result = result + "<textarea class=\"textArea400\" name=\"address\">" + reader["address"] + "</textarea><br />\n";
                                result = result + "Short Detail:<br />\n";
                                result = result + "<textarea class=\"textArea400\" name=\"detail_short\">" + reader["detail_short"] + "</textarea><br />\n";
                                result = result + "Detail:<br />\n";
                                result = result + "<textarea class=\"textArea400\" name=\"detail\">" + System.Web.HttpUtility.HtmlEncode(reader["detail"].ToString()) + "</textarea><br />\n";
                                //result = result + "Direction:<br />\n";
                                //result = result + "<textarea class=\"textArea400\" name=\"direction\">" + reader["direction"] + "</textarea><br />\n";
                                result = result + "Latitude:<br/>\n";
                                result = result + "<input type=\"text\" class=\"text400\" name=\"latitude\" value=\"" + latitude + "\"/><br />\n";
                                result = result + "Longitude:<br />";
                                result = result + "<input type=\"text\"  class=\"text400\" name=\"longitude\"  value=\"" + longitude + "\"/><br />\n";
                                result = result + "</td>\n";
                                result = result + "</tr>\n";
                                

                                result = result + "</table>";
                            }
                            countContent = countContent + 1;

                        }

                        //No thai content
                        if (countContent == 2)
                        {
                            result = result + "<td>\n";
                            result = result + "Title:<br/>\n";
                            result = result + "<input type=\"text\" class=\"text400\"\" name=\"title\"/><br />\n";
                            //result = result + "Title Second:<br />\n";
                            //result = result + "<input type=\"text\" class=\"text400\"\" name=\"title_second\"/><br />\n";
                            result = result + "Address:<br />\n";
                            result = result + "<textarea class=\"textArea400\" name=\"address\"></textarea><br />\n";
                            result = result + "Short Detail:<br />";
                            result = result + "<textarea class=\"textArea400\" name=\"detail_short\"></textarea><br />";
                            result = result + "Detail:<br />";
                            result = result + "<textarea class=\"textArea400\" name=\"detail\"></textarea><br />";
                            //result = result + "Direction:<br />";
                            //result = result + "<textarea class=\"textArea400\" name=\"direction\"></textarea><br />";
                            result = result + "Latitude:<br/>\n";
                            result = result + "<input type=\"text\" class=\"text400\" name=\"latitude\" value=\"" + latitude + "\"/><br />\n";
                            result = result + "Longitude:<br />";
                            result = result + "<input type=\"text\"  class=\"text400\" name=\"longitude\"  value=\"" + longitude + "\"/><br />\n";

                            result = result + "</td>\n";
                            result = result + "</tr>\n";
                            result = result + "</table>";
                            
                            
                            result = result + "<input type=\"hidden\" name=\"action\" value=\"insert\"/>";
                        }
                        else
                        {
                            result = result + "<input type=\"hidden\" name=\"action\" value=\"update\"/>";
                        }
                        result = result + "<input type=\"hidden\" name=\"product_id\" value=\"" + productID + "\"/>";
                        //result = result + "</form>\n";
                        lblMain.Text = result;
                    }
                }
            }
        }


        public void btnSave_Onclick(object sender, EventArgs e)
        {

            string product_id = Request.Form["product_id"];

            string action = Request.Form["action"];


            string title = Request.Form["title"];
            //string title_second = Request.Form["title_second"];
            string address = Request.Form["address"];
            string detail_short = Request.Form["detail_short"];
            string detail = Request.Form["detail"];
            //string direction = Request.Form["direction"];

            string file_name_main = string.Empty;
            string file_name_information = string.Empty;
            string file_name_review = string.Empty;
            string file_name_photo = string.Empty;
            string file_name_why = string.Empty;
            string file_name_pdf = string.Empty;
            string file_name_contact = string.Empty;
            string strCommand = string.Empty;

            string latitude = Request.Form["latitude"];
            string longitude = Request.Form["longitude"];

            using (SqlConnection cn = new SqlConnection(connString))
            {
                strCommand = "select file_name_main,file_name_information,file_name_review,file_name_photo,file_name_why,file_name_pdf,file_name_contact";
                strCommand = strCommand + " from tbl_product_content where product_id=" + product_id + " and lang_id=1";
                SqlCommand cmd = new SqlCommand(strCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    file_name_main = reader["file_name_main"].ToString().Replace(".asp", "-th.asp");
                    file_name_information = reader["file_name_information"].ToString().Replace(".asp", "-th.asp");
                    file_name_review = reader["file_name_review"].ToString().Replace(".asp", "-th.asp");
                    file_name_photo = reader["file_name_photo"].ToString().Replace(".asp", "-th.asp");
                    file_name_why = reader["file_name_why"].ToString().Replace(".asp", "-th.asp");
                    file_name_pdf = reader["file_name_pdf"].ToString().Replace(".pdf", "-th.pdf");
                    file_name_contact = reader["file_name_contact"].ToString().Replace(".asp", "-th.asp");
                }
            }

            //Response.Write(action);
            //Response.Write(file_name_main + "<br>");
            //Response.Write(file_name_information + "<br>");
            //Response.Write(file_name_review + "<br>");
            //Response.Write(file_name_photo + "<br>");
            //Response.Write(file_name_why + "<br>");
            //Response.Write(file_name_pdf + "<br>");
            //Response.Write(file_name_contact + "<br>");
            //Response.End();
            if (action == "insert")
            {
                strCommand = "insert into tbl_product_content (product_id,lang_id,title,address,detail_short,detail,file_name_main,file_name_information,file_name_review,file_name_photo,file_name_why,file_name_pdf,file_name_contact,status)";
                strCommand = strCommand + " values(@product_id,@lang_id,@title,@address,@detail_short,@detail,@file_name_main,@file_name_information,@file_name_review,@file_name_photo,@file_name_why,@file_name_pdf,@file_name_contact,@status)";

                using (SqlConnection cn = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand(strCommand, cn);

                    cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = product_id;
                    cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = 2;
                    cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = title;
                    //cmd.Parameters.Add("@title_second", SqlDbType.NVarChar).Value = title_second;
                    cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = address;
                    cmd.Parameters.Add("@detail_short", SqlDbType.NVarChar).Value = detail_short;
                    cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = detail;
                    //cmd.Parameters.Add("@direction", SqlDbType.NVarChar).Value = direction;
                    cmd.Parameters.Add("@file_name_main", SqlDbType.NVarChar).Value = file_name_main;
                    cmd.Parameters.Add("@file_name_information", SqlDbType.NVarChar).Value = file_name_information;
                    cmd.Parameters.Add("@file_name_review", SqlDbType.NVarChar).Value = file_name_review;
                    cmd.Parameters.Add("@file_name_photo", SqlDbType.NVarChar).Value = file_name_photo;
                    cmd.Parameters.Add("@file_name_why", SqlDbType.NVarChar).Value = file_name_why;
                    cmd.Parameters.Add("@file_name_pdf", SqlDbType.NVarChar).Value = file_name_pdf;
                    cmd.Parameters.Add("@file_name_contact", SqlDbType.NVarChar).Value = file_name_contact;
                    cmd.Parameters.Add("@status", SqlDbType.Bit).Value = 1;
                    cn.Open();
                    cmd.ExecuteNonQuery();

                }
            }
            else
            {
                strCommand = "update tbl_product_content set title=@title,address=@address,detail=@detail,detail_short=@detail_short";
                strCommand = strCommand + " where product_id=@product_id and lang_id=2";
                using (SqlConnection cn = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand(strCommand, cn);

                    cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = product_id;
                    cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = title;
                    //cmd.Parameters.Add("@title_second", SqlDbType.NVarChar).Value = title_second;
                    cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = address;
                    cmd.Parameters.Add("@detail_short", SqlDbType.NVarChar).Value = detail_short;
                    cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = detail;
                    //cmd.Parameters.Add("@direction", SqlDbType.NVarChar).Value = direction;
                    cn.Open();
                    cmd.ExecuteNonQuery();

                }
            }

            using (SqlConnection cn = new SqlConnection(connString))
            {
                strCommand = "update tbl_product set coor_lat=@coor_lat,coor_long=@coor_long where product_id=@product_id";
                SqlCommand cmd = new SqlCommand(strCommand, cn);

                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = product_id;
                cmd.Parameters.Add("@coor_lat", SqlDbType.VarChar).Value = latitude;
                cmd.Parameters.Add("@coor_long", SqlDbType.VarChar).Value = longitude;
                cn.Open();
                cmd.ExecuteNonQuery();
            }

            Response.Redirect(Request.Url.ToString());

        }
    }
}