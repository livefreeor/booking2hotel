using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class admin_product_fac_del : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            string result = string.Empty;
            ArrayList arrList = new ArrayList();
            using (SqlConnection cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["hotels2thailandXConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT(title) FROM tbl_facility_product ORDER BY title",cn);
                cn.Open();
                IDataReader reader = cmd.ExecuteReader();
               
                while (reader.Read())
                {
                    arrList.Add(reader[0].ToString().Replace("\"", "&quot;"));
                }

                

            }

            int count = 0;
            foreach (string Item in arrList)
            {
                result = result + "<input type=\"checkbox\" name=\"chk_title\"  value=\"" + count + "\" />";
                result = result + "<input type=\"text\" name=\"chk_title_" + count + "\" style=\"width:500px;\"   value=\"" + Item + "\" readonly=\"readonly\" /><br/>";
                count = count + 1;
            }

            lblREsult.Text = result;

            string RsultDel = string.Empty;
            ArrayList arrListDel = new ArrayList();
            count = 0;
            using (SqlConnection cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["hotels2thailandXConnectionString"].ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("SELECT DISTINCT(title) FROM tbl_fac_del_temp WHERE cat_id= 1 ORDER BY title", cn);
                cn.Open();
                IDataReader reader = cmd.ExecuteReader();
             
                while (reader.Read())
                {

                    arrListDel.Add(reader[0].ToString());
                }
            }
            foreach (string Item in arrListDel)
            {
                RsultDel = RsultDel + Item + "<br/>";

                count = count + 1;
            }
            lblDelResult.Text = RsultDel;
        }
    }

    public void Del_Onclick(object sender, EventArgs e)
    {
        string connString = WebConfigurationManager.ConnectionStrings["hotels2thailandXConnectionString"].ConnectionString;

        if (!string.IsNullOrEmpty(Request.Form["chk_title"]))
        {
            string Check = Request.Form["chk_title"];
            string DeleTitle = string.Empty;
            foreach (string val in Request.Form["chk_title"].ToString().Split(','))
            {
               
                DeleTitle = Request.Form["chk_title_" + val].ToString();
               
                using (SqlConnection cn = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT fac_id,title FROM tbl_facility_product WHERE title = @title", cn);
                    cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = DeleTitle;
                    cn.Open();
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        //Response.Write(reader[1]);
                        //Response.Flush();
                        using (SqlConnection cn1 = new SqlConnection(connString))
                        {
                            SqlCommand cmd1 = new SqlCommand("INSERT INTO tbl_fac_del_temp (id,cat_id,title) VALUES (@id,1,@title)", cn1);
                            cmd1.Parameters.Add("@id", SqlDbType.Int).Value = (int)reader[0];
                            cmd1.Parameters.Add("@title", SqlDbType.NVarChar).Value = reader[1].ToString();
                            cn1.Open();
                            cmd1.ExecuteNonQuery();
                        }

                        using (SqlConnection cn2 = new SqlConnection(connString))
                        {
                            SqlCommand cmd2 = new SqlCommand("DELETE FROM tbl_facility_product WHERE fac_id = @fac_id", cn2);
                            cmd2.Parameters.Add("@fac_id", SqlDbType.Int).Value = (int)reader[0];
                            cn2.Open();
                            cmd2.ExecuteNonQuery();
                        }


                    }
                }
            }
        }

       Response.Redirect(Request.Url.ToString());
        
    }
}