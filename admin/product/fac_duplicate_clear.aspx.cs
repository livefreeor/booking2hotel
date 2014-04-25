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
using Hotels2thailand;
using Hotels2thailand.Production;

public partial class admin_product_fac_duplicate_clear : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private string Con = WebConfigurationManager.ConnectionStrings["hotels2thailandXConnectionString"].ConnectionString;
    public void btnGo_Onclick(object sender, EventArgs e)
    {
        byte bytProductDes = byte.Parse(txtDes.Text);

        ArrayList arrProductId  = new ArrayList();
        using(SqlConnection cn = new SqlConnection(Con))
        {
            SqlCommand cmd = new SqlCommand("SELECT product_id FROM tbl_product WHERE cat_id = 29 AND destination_id=@destination_id",cn);
            cmd.Parameters.Add("@destination_id", SqlDbType.SmallInt).Value = bytProductDes;

            cn.Open();

            IDataReader reader  = cmd.ExecuteReader();
            while(reader.Read())
            {
                arrProductId.Add((int)reader[0]);
            }
        }

       
        
        foreach(int intProductId in arrProductId)
        {
            //if (intProductId == 52)
            //{
                ArrayList arrDupFac = new ArrayList();
                using (SqlConnection cn = new SqlConnection(Con))
                {
                    SqlCommand cmd = new SqlCommand("SELECT title  FROM tbl_facility_product WHERE product_id = @product_id GROUP BY title HAVING count(*) > 1", cn);
                    cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                    cn.Open();
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        arrDupFac.Add(reader[0]);
                    }

                }


                if (arrDupFac.Count > 0)
                {

                    foreach (string FacKey in arrDupFac)
                    {
                     
                        ArrayList arrFacId = new ArrayList();
                        using (SqlConnection cn = new SqlConnection(Con))
                        {
                            SqlCommand cmd1 = new SqlCommand("SELECT fac_id FROM tbl_facility_product WHERE product_id=@product_id AND title =@title", cn);
                            cmd1.Parameters.Add("@title", SqlDbType.NVarChar).Value = FacKey;
                            cmd1.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                            cn.Open();
                            IDataReader reader1 = cmd1.ExecuteReader();
                            while (reader1.Read())
                            {
                                arrFacId.Add((int)reader1[0]);
                            }
                            
                        }

                       
                        
                        int count = 1;
                        foreach (int intFacId in arrFacId)
                        {
                            if (count != arrFacId.Count)
                            {
                                using (SqlConnection cn = new SqlConnection(Con))
                                {
                                    SqlCommand cmd2 = new SqlCommand("DELETE FROM tbl_facility_product WHERE product_id=@product_id AND fac_id=@fac_id", cn);
                                    cmd2.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                                    cmd2.Parameters.Add("@fac_id", SqlDbType.Int).Value = intFacId;
                                    cn.Open();
                                    cmd2.ExecuteNonQuery();

                                    Response.Write(intFacId + "---" + intProductId + "<br/>");
                                    Response.Flush();
                                }
                            }

                            count = count + 1;
                        }
                    }
                }
           // }
           
           
                
        }
    }
}