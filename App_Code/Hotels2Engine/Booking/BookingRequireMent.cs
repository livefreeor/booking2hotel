using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;

using Hotels2thailand;
/// <summary>
/// Summary description for ConfirmOrderOpen
/// </summary>
/// 

//public class BookingRequireMentGeneralHealthAndSpa
//{
//    public int RequireId { get; set; }
//    public int BookingItemId { get; set; }
//    public string Comment { get; set; }
//}
//public class BookingRequireMentHotel
//{
//    public int RequireId { get; set; }
//    public int BookingItemId { get; set; }
//    public string Comment { get; set; }
//    public byte TypeSmoke { get; set; }
//    public byte TypeBed { get; set; }
//    public byte TypeFloor { get; set; }
//}
public class BookingRequireMent : Hotels2BaseClass
{

    public int RequirID { get; set; }
    public int BookingItemID { get; set; }
    public byte ProductCat { get; set; }
    public int OptionID { get; set; }
    public string OptionTitle { get; set; }
    public string OptionTitleEngDefault { get; set; }
    public string Comment { get; set; }
    public byte Smoke { get; set; }
    public byte Bed { get; set; }
    public byte Floor { get; set; }



    public BookingRequireMent()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public List<object> GetRequireMentByBooingProductID(int intBookingproductId, byte ProductCAt)
    {
        StringBuilder query = new StringBuilder();
        switch (ProductCAt)
        {
            case 29:
                query.Append("SELECT re.require_id,re.booking_item_id, p.cat_id, bi.option_id");
                query.Append(",(SELECT opc.title FROM tbl_product_option_content opc WHERE opc.option_id = bi.option_id AND opc.lang_id = b.lang_id)");
                query.Append(",(SELECT opc.title FROM tbl_product_option_content opc WHERE opc.option_id = bi.option_id AND opc.lang_id = 1)");
                query.Append(", re.comment,re.type_smoking, re.type_bed, re.type_floor");
                query.Append(" FROM tbl_booking_item_require_hotel re, tbl_booking_product bp,tbl_booking_item bi, tbl_product p,  tbl_booking b");
                query.Append(" WHERE bp.booking_product_id = bi.booking_product_id AND bi.booking_item_id = re.booking_item_id AND p.product_id=bp.product_id");
                query.Append(" AND b.booking_id = bp.booking_id");
                query.Append(" AND bp.booking_product_id = @booking_product_id");
                break;
            case 40:
                query.Append("SELECT re.require_id,re.booking_item_id,p.cat_id,bi.option_id");
                query.Append(",(SELECT opc.title FROM tbl_product_option_content opc WHERE opc.option_id = bi.option_id AND opc.lang_id = b.lang_id)");
                query.Append(",(SELECT opc.title FROM tbl_product_option_content opc WHERE opc.option_id = bi.option_id AND opc.lang_id = 1)");
                query.Append(",re.comment");
                query.Append(" FROM tbl_booking_item_require_spa re, tbl_booking_product bp,tbl_booking_item bi, tbl_product p, tbl_booking b");
                query.Append(" WHERE bp.booking_product_id = bi.booking_product_id AND bi.booking_item_id = re.booking_item_id AND p.product_id=bp.product_id");
                query.Append(" AND b.booking_id = bp.booking_id");
                query.Append(" AND bp.booking_product_id = @booking_product_id");

                break;
            case 39:
                query.Append("SELECT re.require_id,re.booking_item_id,p.cat_id,bi.option_id");
                query.Append(",(SELECT opc.title FROM tbl_product_option_content opc WHERE opc.option_id = bi.option_id AND opc.lang_id = b.lang_id)");
                query.Append(",(SELECT opc.title FROM tbl_product_option_content opc WHERE opc.option_id = bi.option_id AND opc.lang_id = 1)");
                query.Append(",re.comment");
                query.Append(" FROM tbl_booking_item_require_health re, tbl_booking_product bp,tbl_booking_item bi, tbl_product p, tbl_booking b");
                query.Append(" WHERE bp.booking_product_id = bi.booking_product_id AND bi.booking_item_id = re.booking_item_id AND p.product_id=bp.product_id");
                query.Append(" AND b.booking_id = bp.booking_id");
                query.Append(" AND bp.booking_product_id = @booking_product_id");

                break;
            default:
                query.Append("SELECT re.require_id,re.booking_item_id,p.cat_id,bi.option_id");
                query.Append(",(SELECT opc.title FROM tbl_product_option_content opc WHERE opc.option_id = bi.option_id AND opc.lang_id = b.lang_id)");
                query.Append(",(SELECT opc.title FROM tbl_product_option_content opc WHERE opc.option_id = bi.option_id AND opc.lang_id = 1)");
                query.Append(",re.comment");
                query.Append(" FROM tbl_booking_item_require_general re, tbl_booking_product bp,tbl_booking_item bi, tbl_product p, tbl_booking b");
                query.Append(" WHERE bp.booking_product_id = bi.booking_product_id AND bi.booking_item_id = re.booking_item_id AND p.product_id=bp.product_id");
                query.Append(" AND b.booking_id = bp.booking_id");
                query.Append(" AND bp.booking_product_id = @booking_product_id");

                break;

        }
        using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        {

            ArrayList arrObj = new ArrayList();
            SqlCommand cmd = new SqlCommand(query.ToString(), cn);
            cmd.Parameters.Add("@booking_product_id", SqlDbType.Int).Value = intBookingproductId;
            cn.Open();
            return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));

        }
    }


    public static Dictionary<byte, string> getTypeSmoke()
    {
        Dictionary<byte, string> Type = new Dictionary<byte, string>();
        Type.Add(1, "Non-Smoking Room");
        Type.Add(2, "Smoking Room");
        Type.Add(3, "-");
        return Type;
    }
    public static Dictionary<byte, string> getTypeBed()
    {
        Dictionary<byte, string> Type = new Dictionary<byte, string>();
        Type.Add(1, "King Bed");
        Type.Add(2, "Twin Bed");
        Type.Add(3, "-");
        return Type;
    }
    public static Dictionary<byte, string> getFloor()
    {
        Dictionary<byte, string> Type = new Dictionary<byte, string>();
        Type.Add(1, "High Floor");
        Type.Add(2, "Low Floor");
        Type.Add(3, "-");
        return Type;
    }
    public string requireTypeFloor(byte byttype)
    {
        string type = string.Empty;
        switch (byttype)
        {
            case 1:
                type = "High Floor";
                break;
            case 2:
                type = "Low Floor";
                break;
            case 3:
                type = "-";
                break;
        }
        return type;
    }
    public string requireTypeBed(byte byttype)
    {
        string type = string.Empty;
        switch (byttype)
        {
            case 1:
                type = "King Bed";
                break;
            case 2:
                type = "Twin Bed";
                break;
            case 3:
                type = "-";
                break;
        }
        return type;
    }
    public string requireTypeSmoke(byte byttype)
    {
        string type = string.Empty;
        switch (byttype)
        {
            case 1:
                type = "Non-Smoking Room";
                break;
            case 2:
                type = "Smoking Room";
                break;
            case 3:
                type = "-";
                break;
         }
        return type;
    }

    public ArrayList GetRequireMentByBooinProductIDAndProductCatSingle(int intRequirID, byte ProductCAt)
    {
        StringBuilder query = new StringBuilder();
        switch (ProductCAt)
        {
            case 29:
                query.Append("SELECT comment,type_smoking, type_bed, type_floor, require_id");
                query.Append(" FROM tbl_booking_item_require_hotel");
                query.Append(" WHERE require_id=@require_id");
                //query.Append(" bi.booking_item_id = @booking_item_id");
                break;
            case 40:
                query.Append("SELECT comment, require_id");
                query.Append(" FROM tbl_booking_item_require_spa");
                query.Append(" WHERE require_id=@require_id");
                //query.Append(" bi.booking_item_id = @booking_item_id");

                break;
            case 39:
                query.Append("SELECT comment, require_id");
                query.Append(" FROM tbl_booking_item_require_health");
                query.Append(" WHERE require_id=@require_id");
                //query.Append(" bi.booking_item_id = @booking_item_id");

                break;
            default:
                query.Append("SELECT comment, require_id");
                query.Append(" FROM tbl_booking_item_require_general");
                query.Append(" WHERE require_id=@require_id");
                //query.Append(" bi.booking_item_id = @booking_item_id");

                break;

        }
        using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        {

            ArrayList arrObj = new ArrayList();
            SqlCommand cmd = new SqlCommand(query.ToString(), cn);
            cmd.Parameters.Add("@require_id", SqlDbType.Int).Value = intRequirID;
            cn.Open();
            IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
            if (reader.Read())
            {
                
                switch (ProductCAt)
                {
                    case 29:
                        arrObj.Add(reader[0]);
                        arrObj.Add(reader[1]);
                        arrObj.Add(reader[2]);
                        arrObj.Add(reader[3]);
                        arrObj.Add(reader[4]);
                        break;
                    case 40:
                        arrObj.Add(reader[0]);
                        arrObj.Add(reader[1]);
                        break;
                    case 39:
                        arrObj.Add(reader[0]);
                        arrObj.Add(reader[1]);
                        break;
                    default:
                        arrObj.Add(reader[0]);
                        arrObj.Add(reader[1]);
                        break;
                }

            }
            else
            {
                return null;
            }
            return arrObj;

        }


    }

    public IList<object> GetRequireMentByBooinProductIDAndProductCat(int intBookingProductId, byte ProductCAt, byte bytBookingLang)
    {
        StringBuilder query = new StringBuilder();
        switch (ProductCAt)
        {
            case 29:
                query.Append("SELECT ");
                query.Append("(SELECT opc.title FROM tbl_product_option_content opc WHERE opc.option_id = bi.option_id AND opc.lang_id = b.lang_id)");
                query.Append(",(SELECT opc.title FROM tbl_product_option_content opc WHERE opc.option_id = bi.option_id AND opc.lang_id = 1)");
                query.Append(" ,br.comment, br.type_smoking, br.type_bed, br.type_floor");
                query.Append(" FROM tbl_booking b , tbl_booking_item bi, tbl_booking_product bp,tbl_booking_item_require_hotel br , tbl_product_option op");
                query.Append(" WHERE b.booking_id = bp.booking_id AND bi.booking_product_id = bp.booking_product_id AND bi.booking_item_id = br.booking_item_id ");
                query.Append(" AND op.option_id = bi.option_id AND bi.booking_product_id = @booking_product_id AND op.cat_id NOT IN (39,40,43,44,47)");
                break;
            case 40:
                query.Append("SELECT ");
                query.Append("(SELECT opc.title FROM tbl_product_option_content opc WHERE opc.option_id = bi.option_id AND opc.lang_id = b.lang_id)");
                query.Append(",(SELECT opc.title FROM tbl_product_option_content opc WHERE opc.option_id = bi.option_id AND opc.lang_id = 1)");
                query.Append(" ,br.comment");
                query.Append(" FROM tbl_booking b , tbl_booking_item bi, tbl_booking_product bp,tbl_booking_item_require_spa br ,tbl_product_option op");
                query.Append(" WHERE b.booking_id = bp.booking_id AND bi.booking_product_id = bp.booking_product_id AND bi.booking_item_id = br.booking_item_id");
                query.Append(" AND op.option_id = bi.option_id AND bi.booking_product_id = @booking_product_id AND op.cat_id NOT IN (39,40,43,44,47)");
                break;
            case 39:
                query.Append("SELECT ");
                query.Append("(SELECT opc.title FROM tbl_product_option_content opc WHERE opc.option_id = bi.option_id AND opc.lang_id = b.lang_id)");
                query.Append(",(SELECT opc.title FROM tbl_product_option_content opc WHERE opc.option_id = bi.option_id AND opc.lang_id = 1)");
                query.Append(" ,br.comment");
                query.Append(" FROM tbl_booking b , tbl_booking_item bi, tbl_booking_product bp,tbl_booking_item_require_health br ,tbl_product_option op");
                query.Append(" WHERE b.booking_id = bp.booking_id AND bi.booking_product_id = bp.booking_product_id AND bi.booking_item_id = br.booking_item_id ");
                query.Append(" AND op.option_id = bi.option_id  AND bi.booking_product_id = @booking_product_id AND op.cat_id NOT IN (39,40,43,44,47)");
                break;
            default:
                query.Append("SELECT ");
                query.Append("(SELECT opc.title FROM tbl_product_option_content opc WHERE opc.option_id = bi.option_id AND opc.lang_id = b.lang_id)");
                query.Append(",(SELECT opc.title FROM tbl_product_option_content opc WHERE opc.option_id = bi.option_id AND opc.lang_id = 1)");
                query.Append(" ,br.comment");
                query.Append(" FROM tbl_booking b , tbl_booking_item bi, tbl_booking_product bp,tbl_booking_item_require_general br , tbl_product_option op");
                query.Append(" WHERE b.booking_id = bp.booking_id AND bi.booking_product_id = bp.booking_product_id AND bi.booking_item_id = br.booking_item_id ");
                query.Append(" AND op.option_id = bi.option_id  AND bi.booking_product_id = @booking_product_id AND op.cat_id NOT IN (39,40,43,44,47)");
                break;

        }
        using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        {
            IList<object> iObj = new List<object>();

            SqlCommand cmd = new SqlCommand(query.ToString(), cn);
            cmd.Parameters.Add("@booking_product_id", SqlDbType.Int).Value = intBookingProductId;
            cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytBookingLang;
            cn.Open();
            IDataReader reader = ExecuteReader(cmd);
            while (reader.Read())
            {
                //(reader["ReleaseDate"] == DBNull.Value ? DateTime.Now : (DateTime)reader["ReleaseDate"]),
                ArrayList arrObj = new ArrayList();
                switch (ProductCAt)
                {
                    case 29:
                        arrObj.Add((reader[0] == DBNull.Value ? reader[1] : reader[0]));
                        arrObj.Add(reader[2]);
                        arrObj.Add(reader[3]);
                        arrObj.Add(reader[4]);
                        arrObj.Add(reader[5]);
                        break;
                    case 40:
                        arrObj.Add((reader[0] == DBNull.Value ? reader[1] : reader[0]));
                        arrObj.Add(reader[2]);
                        break;
                    case 39:
                        arrObj.Add((reader[0] == DBNull.Value ? reader[1] : reader[0]));
                        arrObj.Add(reader[2]);
                        break;
                    default:
                        arrObj.Add((reader[0] == DBNull.Value ? reader[1] : reader[0]));
                        arrObj.Add(reader[2]);
                        break;
                }
                iObj.Add(arrObj);
            }
            return iObj;

        }


    }

    public IList<object> GetRequireMentByBooinProductIDAndProductCat(int intBookingProductId, byte ProductCAt)
    {
        StringBuilder query = new StringBuilder();
        switch (ProductCAt)
        {
            case 29:
                query.Append("SELECT ");
                query.Append("(SELECT opc.title FROM tbl_product_option_content opc WHERE opc.option_id = bi.option_id AND opc.lang_id = b.lang_id)");
                query.Append(",(SELECT opc.title FROM tbl_product_option_content opc WHERE opc.option_id = bi.option_id AND opc.lang_id = 1)");
                query.Append(" ,br.comment, br.type_smoking, br.type_bed, br.type_floor");
                query.Append(" FROM tbl_booking b , tbl_booking_item bi, tbl_booking_product bp,tbl_booking_item_require_hotel br , tbl_product_option op");
                query.Append(" WHERE b.booking_id = bp.booking_id AND bi.booking_product_id = bp.booking_product_id AND bi.booking_item_id = br.booking_item_id ");
                query.Append(" AND op.option_id = bi.option_id AND bi.booking_product_id = @booking_product_id AND op.cat_id NOT IN (39,40,43,44,47)");
                break;
            case 40:
                query.Append("SELECT ");
                query.Append("(SELECT opc.title FROM tbl_product_option_content opc WHERE opc.option_id = bi.option_id AND opc.lang_id = b.lang_id)");
                query.Append(",(SELECT opc.title FROM tbl_product_option_content opc WHERE opc.option_id = bi.option_id AND opc.lang_id = 1)");
                query.Append(" ,br.comment");
                query.Append(" FROM tbl_booking b , tbl_booking_item bi, tbl_booking_product bp,tbl_booking_item_require_spa br , tbl_product_option op");
                query.Append(" WHERE b.booking_id = bp.booking_id AND bi.booking_product_id = bp.booking_product_id AND bi.booking_item_id = br.booking_item_id ");
                query.Append(" AND op.option_id = bi.option_id  AND bi.booking_product_id = @booking_product_id AND op.cat_id NOT IN (39,40,43,44,47)");
                break;
            case 39 :
                query.Append("SELECT ");
                query.Append("(SELECT opc.title FROM tbl_product_option_content opc WHERE opc.option_id = bi.option_id AND opc.lang_id = b.lang_id)");
                query.Append(",(SELECT opc.title FROM tbl_product_option_content opc WHERE opc.option_id = bi.option_id AND opc.lang_id = 1)");
                query.Append(" ,br.comment");
                query.Append(" FROM tbl_booking b , tbl_booking_item bi, tbl_booking_product bp,tbl_booking_item_require_health br , tbl_product_option op");
                query.Append(" WHERE b.booking_id = bp.booking_id AND bi.booking_product_id = bp.booking_product_id AND bi.booking_item_id = br.booking_item_id ");
                query.Append(" AND op.option_id = bi.option_id AND bi.booking_product_id = @booking_product_id AND op.cat_id NOT IN (39,40,43,44,47)");
                break;
            default:
                query.Append("SELECT ");
                query.Append("(SELECT opc.title FROM tbl_product_option_content opc WHERE opc.option_id = bi.option_id AND opc.lang_id = b.lang_id)");
                query.Append(",(SELECT opc.title FROM tbl_product_option_content opc WHERE opc.option_id = bi.option_id AND opc.lang_id = 1)");
                query.Append(" ,br.comment");
                query.Append(" FROM tbl_booking b , tbl_booking_item bi, tbl_booking_product bp,tbl_booking_item_require_general br, tbl_product_option op");
                query.Append(" WHERE b.booking_id = bp.booking_id AND bi.booking_product_id = bp.booking_product_id AND bi.booking_item_id = br.booking_item_id ");
                query.Append(" AND op.option_id = bi.option_id AND bi.booking_product_id = @booking_product_id AND op.cat_id NOT IN (39,40,43,44,47)");
                break;

        }
        using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        {
            IList<object> iObj = new List<object>();
            
            SqlCommand cmd = new SqlCommand(query.ToString(), cn);
            cmd.Parameters.Add("@booking_product_id", SqlDbType.Int).Value = intBookingProductId;
            cn.Open();
            IDataReader reader = ExecuteReader(cmd);
            while (reader.Read())
            {
                ArrayList arrObj = new ArrayList();
                switch (ProductCAt)
                {
                    case 29:
                        arrObj.Add((reader[0] == DBNull.Value ? reader[1] : reader[0]));
                        arrObj.Add(reader[2]);
                        arrObj.Add(reader[3]);
                        arrObj.Add(reader[4]);
                        arrObj.Add(reader[5]);
                        break;
                    case 40:
                        arrObj.Add((reader[0] == DBNull.Value ? reader[1] : reader[0]));
                        arrObj.Add(reader[2]);
                        break;
                    case 39:
                        arrObj.Add((reader[0] == DBNull.Value ? reader[1] : reader[0]));
                        arrObj.Add(reader[2]);
                        break;
                    default:
                        arrObj.Add((reader[0] == DBNull.Value ? reader[1] : reader[0]));
                        arrObj.Add(reader[2]);
                        break;
                }
                iObj.Add(arrObj);
            }
            return iObj;
            
        }


    }


    public int InsertNewBookingRequireMentGeneral(int intItemID, string Comment)
    {


        using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        {
            IList<object> iObj = new List<object>();
            ArrayList arrObj = new ArrayList();
            SqlCommand cmd = new SqlCommand("INSERT INTO tbl_booking_item_require_general (booking_item_id,comment) VALUES (@booking_item_id,@comment); SET @require_id=SCOPE_IDENTITY();", cn);
            cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = Comment;
            cmd.Parameters.Add("@booking_item_id", SqlDbType.Int).Value = intItemID;
            cmd.Parameters.Add("@require_id", SqlDbType.Int).Direction = ParameterDirection.Output;
            cn.Open();
            ExecuteNonQuery(cmd);
            int ret = (int)cmd.Parameters["@require_id"].Value;

            return ret;
        }


    }
    public bool UpdateBookingRequireMentGeneral(int reqid, string Comment)
    {
        using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        {
            IList<object> iObj = new List<object>();
            ArrayList arrObj = new ArrayList();
            SqlCommand cmd = new SqlCommand("UPDATE tbl_booking_item_require_general SET comment=@comment WHERE require_id=@require_id", cn);
            cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = Comment;
            cmd.Parameters.Add("@require_id", SqlDbType.Int).Value = reqid;
            
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            
            return (ret==1);
        }
    }
    public int InsertNewBookingRequireMenthealth(int intItemID, string Comment)
    {


        using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        {
            IList<object> iObj = new List<object>();
            ArrayList arrObj = new ArrayList();
            SqlCommand cmd = new SqlCommand("INSERT INTO tbl_booking_item_require_health (booking_item_id,comment) VALUES (@booking_item_id,@comment); SET @require_id=SCOPE_IDENTITY();", cn);
            cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = Comment;
            cmd.Parameters.Add("@booking_item_id", SqlDbType.Int).Value = intItemID;
            cmd.Parameters.Add("@require_id", SqlDbType.Int).Direction = ParameterDirection.Output;
            cn.Open();
            ExecuteNonQuery(cmd);
            int ret = (int)cmd.Parameters["@require_id"].Value;

            return ret;
        }


    }

    public bool UpdateBookingRequireMenthealth(int reqid, string Comment)
    {
        using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        {
            IList<object> iObj = new List<object>();
            ArrayList arrObj = new ArrayList();
            SqlCommand cmd = new SqlCommand("UPDATE tbl_booking_item_require_health SET comment=@comment WHERE require_id=@require_id", cn);
            cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = Comment;
            cmd.Parameters.Add("@require_id", SqlDbType.Int).Value = reqid;
            
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            
            return (ret==1);
        }
    }
    public int InsertNewBookingRequireMentSpa(int intItemID, string Comment)
    {


        using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        {
            IList<object> iObj = new List<object>();
            ArrayList arrObj = new ArrayList();
            SqlCommand cmd = new SqlCommand("INSERT INTO tbl_booking_item_require_spa (booking_item_id,comment) VALUES (@booking_item_id,@comment); SET @require_id=SCOPE_IDENTITY();", cn);
            cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = Comment;
            cmd.Parameters.Add("@booking_item_id", SqlDbType.Int).Value = intItemID;
            cmd.Parameters.Add("@require_id", SqlDbType.Int).Direction = ParameterDirection.Output;
            cn.Open();
            ExecuteNonQuery(cmd);
            int ret = (int)cmd.Parameters["@require_id"].Value;

            return ret;
        }


    }

    public bool UpdateBookingRequireMentSpa(int reqid, string Comment)
    {
        using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        {
            IList<object> iObj = new List<object>();
            ArrayList arrObj = new ArrayList();
            SqlCommand cmd = new SqlCommand("UPDATE tbl_booking_item_require_spa SET comment=@comment WHERE require_id=@require_id", cn);
            cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = Comment;
            cmd.Parameters.Add("@require_id", SqlDbType.Int).Value = reqid;
            
            cn.Open();
            int ret = ExecuteNonQuery(cmd);
            
            return (ret==1);
        }
    }
    public int InsertNewBookingRequireMentHotel(int intItemID, string Comment, byte bytSmoke, byte bytBed, byte bytFloor)
    {
        
        
        using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        {
            IList<object> iObj = new List<object>();
            ArrayList arrObj = new ArrayList();
            SqlCommand cmd = new SqlCommand("INSERT INTO tbl_booking_item_require_hotel (booking_item_id,comment,type_smoking,type_bed,type_floor) VALUES (@booking_item_id,@comment,@type_smoking,@type_bed,@type_floor); SET @booking_item_id=SCOPE_IDENTITY()", cn);
            cmd.Parameters.Add("@booking_item_id", SqlDbType.Int).Value = intItemID;
            cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = Comment;
            cmd.Parameters.Add("@type_smoking", SqlDbType.Int).Value = bytSmoke;
            cmd.Parameters.Add("@type_bed", SqlDbType.Int).Value = bytBed;
            cmd.Parameters.Add("@type_floor", SqlDbType.Int).Value = bytFloor;
            cmd.Parameters.Add("@require_id", SqlDbType.Int).Direction = ParameterDirection.Output;
            cn.Open();
            ExecuteNonQuery(cmd);
            int ret = (int)cmd.Parameters["@booking_item_id"].Value;

            return ret;
        }


    }

    public bool UpdateBookingRequireMentHotel(int reqid, string Comment, byte bytSmoke, byte bytBed, byte bytFloor)
    {
        using (SqlConnection cn = new SqlConnection(this.ConnectionString))
        {
            IList<object> iObj = new List<object>();
            ArrayList arrObj = new ArrayList();
            SqlCommand cmd = new SqlCommand("UPDATE tbl_booking_item_require_hotel SET comment=@comment,type_smoking=@type_smoking,type_bed=@type_bed,type_floor=@type_floor WHERE require_id=@require_id", cn);
            cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = Comment;
            cmd.Parameters.Add("@type_smoking", SqlDbType.Int).Value = bytSmoke;
            cmd.Parameters.Add("@type_bed", SqlDbType.Int).Value = bytBed;
            cmd.Parameters.Add("@type_floor", SqlDbType.Int).Value = bytFloor;
            cmd.Parameters.Add("@require_id", SqlDbType.Int).Value = reqid;

            cn.Open();
            int ret = ExecuteNonQuery(cmd);

            return (ret == 1);
        }
    }
    
}