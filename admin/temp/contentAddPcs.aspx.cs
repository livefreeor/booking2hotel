using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand;
using Hotels2thailand.DataAccess;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class temp_contentAddPcs : System.Web.UI.Page
{
    private string connString = WebConfigurationManager.ConnectionStrings["hotels2thailandXConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        string product_id = Request.Form["product_id"];
        string action = Request.Form["action"];
        string title = Request.Form["title"];
        string title_second = Request.Form["title_second"];
        string address = Request.Form["address"];
        string detail_short = Request.Form["detail_short"];
        string detail  =Request.Form["detail"];
        string direction = Request.Form["direction"];

        string file_name_main = string.Empty;
        string file_name_information = string.Empty;
        string file_name_review = string.Empty;
        string file_name_photo = string.Empty;
        string file_name_why = string.Empty;
        string file_name_pdf = string.Empty;
        string file_name_contact = string.Empty;
        string strCommand=string.Empty;

        string latitude = Request.Form["latitude"];
        string longitude = Request.Form["longitude"];

        using (SqlConnection cn = new SqlConnection(connString))
        {
            strCommand="select file_name_main,file_name_information,file_name_review,file_name_photo,file_name_why,file_name_pdf,file_name_contact";
            strCommand = strCommand + " from tbl_product_content where product_id=" + product_id + " and lang_id=1";
            SqlCommand cmd = new SqlCommand(strCommand, cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if(reader.Read())
            {
                file_name_main = reader["file_name_main"].ToString().Replace(".asp","-th.asp");
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
        if(action=="insert")
        {
            strCommand = "insert into tbl_product_content (product_id,lang_id,title,title_second,address,detail_short,detail,direction,file_name_main,file_name_information,file_name_review,file_name_photo,file_name_why,file_name_pdf,file_name_contact,status)";
            strCommand = strCommand+" values(@product_id,@lang_id,@title,@title_second,@address,@detail_short,@detail,@direction,@file_name_main,@file_name_information,@file_name_review,@file_name_photo,@file_name_why,@file_name_pdf,@file_name_contact,@status)";

            using (SqlConnection cn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(strCommand, cn);

                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = product_id;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = 2;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = title;
                cmd.Parameters.Add("@title_second", SqlDbType.NVarChar).Value = title_second;
                cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = product_id;
                cmd.Parameters.Add("@detail_short", SqlDbType.NVarChar).Value = address;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = detail;
                cmd.Parameters.Add("@direction", SqlDbType.NVarChar).Value = direction;
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
        }else{
            strCommand = "update tbl_product_content set title=@title,title_second=@title_second,address=@address,detail=@detail,detail_short=@detail_short,direction=@direction";
            strCommand = strCommand + " where product_id=@product_id and lang_id=2";
            using (SqlConnection cn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(strCommand, cn);

                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = product_id;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = title;
                cmd.Parameters.Add("@title_second", SqlDbType.NVarChar).Value = title_second;
                cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = product_id;
                cmd.Parameters.Add("@detail_short", SqlDbType.NVarChar).Value = address;
                cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = detail;
                cmd.Parameters.Add("@direction", SqlDbType.NVarChar).Value = direction;
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
        Response.Write("Update data complete <a href=\"javascript:closeWindow()\">x close</a>");
    }
}