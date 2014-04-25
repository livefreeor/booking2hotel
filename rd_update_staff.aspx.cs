using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Booking;

public partial class rd_update_staff : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void btnUpdate_Onclick(object sender, EventArgs e)
    {
        BookingImport cBookingim = new BookingImport();
        int count = 0;
        using (SqlConnection cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["booking2hotelXConnectionString"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("SELECT s.staff_id FROM tbl_staff s , tbl_staff_authorize sa WHERE s.staff_id = sa.staff_id  AND sa.authorize_id = 1", cn);
            cn.Open();
            IDataReader reader = cmd.ExecuteReader();
          
            while (reader.Read())
            {
                int Isval = 0;
                using (SqlConnection cn2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["booking2hotelXConnectionString"].ConnectionString))
                {
                    SqlCommand cmd2 = new SqlCommand("SELECT COUNT(*) FROM tbl_staff_module_authorize_extranet WHERE staff_id = " + (short)reader[0] + " AND module_id=" + txtModuleID.Text.Trim() + " AND method_id=" + txtMethod.Text.Trim() + "",cn2);
                    cn2.Open();
                    Isval = (int)cmd2.ExecuteScalar();
                }

                if (Isval == 0)
                {
                    using (SqlConnection cn1 = new SqlConnection(WebConfigurationManager.ConnectionStrings["booking2hotelXConnectionString"].ConnectionString))
                    {
                        SqlCommand cmd1 = new SqlCommand("INSERT INTO tbl_staff_module_authorize_extranet (staff_id,module_id,method_id) VALUES(" + (short)reader[0] + "," + txtModuleID.Text.Trim() + "," + txtMethod.Text.Trim() + ")", cn1);
                        cn1.Open();
                        cmd1.ExecuteNonQuery();
                        count = count + 1;
                    }
                }

                Isval = 0;
            }
        }

        Response.Write(count);
        Response.Flush();
    }
        
    }
    