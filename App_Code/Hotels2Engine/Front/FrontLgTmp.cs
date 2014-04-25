using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for FrontLgTmp
/// </summary>
public class FrontLgTmp:Hotels2BaseClass
{

    public int TmpID { get; set; }
    public int TmpID2 { get; set; }
    public string Tmp { get; set; }

	public FrontLgTmp()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int Insert(FrontLgTmp data)
    {

        int ret = 0;
        using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("insert into tbl_booking_item_require_hotel (booking_item_id,comment,type_smoking,type_bed,type_floor) values(@booking_item_id,@comment,@type_smoking,@type_bed,@type_floor);SET @require_id=SCOPE_IDENTITY()", cn);
            cmd = new SqlCommand("insert into tbl_lg_tmp(tmp2_id,tmp) values(@tmp2_id,@tmp)",cn);

            cmd.Parameters.Add("@tmp2_id", SqlDbType.Int).Value = data.TmpID2;
            cmd.Parameters.Add("@tmp", SqlDbType.NVarChar).Value = data.Tmp;
      
            cn.Open();
            ExecuteNonQuery(cmd);
            ret = (int)cmd.Parameters["@tmp2_id"].Value;
        }

        return ret;
    }
}