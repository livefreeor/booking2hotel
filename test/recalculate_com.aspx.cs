using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Configuration;
using Hotels2thailand.Front;

public partial class test_recalculate_com : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public IList<int[]> GetBookingRecal()
    {
        using (SqlConnection cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["booking2hotelXConnectionString"].ConnectionString))
        {

            IList<int[]> ilist = new List<int[]>();
            StringBuilder result = new StringBuilder();
            result.Append("SELECT * FROM tbl_booking b, tbl_booking_product bp WHERE b.booking_id = bp.booking_id AND bp.manage_id IS NULL AND b.date_submit >  '2013-03-01' AND b.date_submit <= '2013-05-14' AND bp.product_id  IN (3544) AND b.status = 0 AND bp.status = 1");

            SqlCommand cmd = new SqlCommand(result.ToString(), cn);

            cn.Open();

            IDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int[] arrKey = {(int)reader["product_id"],(int)reader["booking_product_id"]};
                ilist.Add(arrKey);
                //dic.Add((int)reader["product_id"], (int)reader["booking_product_id"]);
                //Response.Write(reader["booking_id"].ToString());
                //Response.Flush();
            }

            return ilist;

        }
    }
    protected void GetAll_Click(object sender, EventArgs e)
    {
        foreach (int[] arrKey in GetBookingRecal())
        {
            Response.Write(arrKey[0] + "---" + arrKey[1] + "<br/>");
            Response.Flush();
        }
    }
    protected void btnget_oddy_Click(object sender, EventArgs e)
    {

    }


    protected void btnrecalAll_Click(object sender, EventArgs e)
    {

        foreach (int[] arrKey in GetBookingRecal()) 
        {
            //Response.Write(iduc.Key + "---" + iduc.Value + "<br/>");
            //Response.Flush();

            frontBookingCom objCom = new frontBookingCom(arrKey[0], arrKey[1]);
            objCom.loadComVal();
        }
        
    }
    protected void btnReacll_Oddy_Click(object sender, EventArgs e)
    {

    }
}