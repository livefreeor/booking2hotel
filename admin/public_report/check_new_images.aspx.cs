using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Hotels2thailand.DataAccess;
using System.IO;

public partial class public_report_check_new_images : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataConnect objConn = new DataConnect();
        string sqlCommand = "select p.product_code,p.title,pp.pic_code";
        sqlCommand = sqlCommand+" from tbl_product p,tbl_product_pic pp";
        sqlCommand = sqlCommand+" where p.product_id=pp.product_id and p.status=1 and p.is_new_pic=1";

        SqlDataReader reader = objConn.GetDataReader(sqlCommand);
        Response.Write("<table cellpadding=\"5\" cellspacing=\"2\" bgcolor=\"#555555\"><tr bgcolor=\"#ffffff\"><td>Code</td><td>Title</td><td>Image Path</td></tr>");
        while(reader.Read())
        {
            try
            {
                if (!File.Exists(Server.MapPath(reader["pic_code"].ToString())))
                {
                    Response.Write("<tr bgcolor=\"#ffffff\"><td>" + reader["product_code"] + "</td><td>" + reader["title"] + "</td><td>" + reader["pic_code"] + "</td></tr>");
                }

            }
            catch
            {
                Response.Write("<tr bgcolor=\"#ffffff\"><td colspan=\"3\"><font color=\"red\">Wrong format :" + reader["pic_code"] + "</font></td></tr>");
            }
        }
        Response.Write("</table>");
    }
}