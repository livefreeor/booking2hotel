using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for BookingGeneralRequirement
/// </summary>
public class BookingGeneralRequirement:Hotels2BaseClass
{
    public int RequireID { get; set; }
    public int BookingItemID { get; set; }
    public string Comment { get; set; }

	public BookingGeneralRequirement(int bookingItemID,string comment)
	{
        BookingItemID = bookingItemID;
        Comment = comment;
	}
    public int Insert(BookingGeneralRequirement data)
    {
        int ret = 0;
        using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("insert into tbl_booking_item_require_general(booking_item_id,comment) values(@booking_item_id,@comment);SET @require_id=SCOPE_IDENTITY()", cn);
            cmd.Parameters.Add("@booking_item_id", SqlDbType.Int).Value = data.BookingItemID;
            cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = data.Comment;
            cmd.Parameters.Add("@require_id", SqlDbType.Int).Direction = ParameterDirection.Output;
            cn.Open();
            ExecuteNonQuery(cmd);
            ret = (int)cmd.Parameters["@require_id"].Value;
        }
        return ret;
    }
}