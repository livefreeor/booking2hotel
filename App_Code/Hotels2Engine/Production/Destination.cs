using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
/// <summary>
/// Summary description for destination
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class Destination:Hotels2BaseClass
    {
        public short DestinationID { get; set; }
        public byte CountryID { get; set; }
        //public byte GroupID { get; set; }
        public bool Status { get; set; }
        public string Title { get; set; }
        public string FolderDestination { get; set; }

        

        public Dictionary<string,string> GetDestinationAll()
        {
            Dictionary<string, string> dataList = new Dictionary<string, string>();

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT destination_id, title FROM tbl_destination ORDER BY title", cn);
               
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    dataList.Add(reader[0].ToString(), reader[1].ToString());
                }
            }
            
            return dataList;
        }
        public IList<object> GetDestinationAllList()
        {
            Dictionary<string, string> dataList = new Dictionary<string, string>();

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_destination ORDER BY title", cn);

                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }            
        }

        public bool UpdateDestination( short shrDestinationId, string strTitle, string folder)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_destination SET title=@title,folder_destination=@folder_destination WHERE destination_id = @destination_id;", cn);
               
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = strTitle;
                cmd.Parameters.Add("@folder_destination", SqlDbType.VarChar).Value = folder;
                cmd.Parameters.Add("@destination_id", SqlDbType.SmallInt).Value = shrDestinationId;
                cn.Open();

                ret = ExecuteNonQuery(cmd);
            }
            return (ret==1);
        }

        public short InsertNewDestination( string strTitle, string folder)
        {
            short ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_destination (country_id,status,title,folder_destination) VALUES(@country_id,@status,@title,@folder_destination); SET @des_id=SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@country_id",SqlDbType.SmallInt).Value = 208;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = true;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = strTitle;
                cmd.Parameters.Add("@folder_destination", SqlDbType.VarChar).Value = folder;
                cmd.Parameters.Add("@des_id", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cn.Open();
                ExecuteNonQuery(cmd);
                ret = (short)cmd.Parameters["@des_id"].Value;
            }
            return ret;
        }


        public Dictionary<string, string> GetDestinationExtranetOnly()
        {
            Dictionary<string, string> dataList = new Dictionary<string, string>();

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT des.destination_id, des.title FROM tbl_destination des WHERE (SELECT COUNT(p.product_id) FROM tbl_product p WHERE p.destination_id = des.destination_id AND p.isextranet = 1) > 0 ORDER BY des.title", cn);

                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    dataList.Add(reader[0].ToString(), reader[1].ToString());
                }
            }

            return dataList;
        }

        public Destination GetDestinationById(int intDestinationId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT destination_id, country_id,status,title,folder_destination FROM tbl_destination WHERE destination_id=@destination_id", cn);
                cmd.Parameters.Add("@destination_id", SqlDbType.TinyInt).Value = intDestinationId; 
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (Destination)MappingObjectFromDataReader(reader);
                else
                    return null;

            }
        }


        //SELECT  folder_destination FROM tbl_product p ,tbl_destination d WHERE d.destination_id = p.destination_id AND product_id = 

        public string GetDestinationFolderNameByProcutId(int intProductId)
        {
            string strDesFolderName = string.Empty;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT  folder_destination FROM tbl_product p ,tbl_destination d WHERE d.destination_id = p.destination_id AND product_id =@product_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    strDesFolderName = reader[0].ToString();
                }
                return strDesFolderName;
            }
        }


        public string GetDestinationTitleByProcutId(int intProductId, byte langId)
        {
            string strDesFolderName = string.Empty;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT  dn.title  FROM tbl_product p ,tbl_destination_name dn WHERE dn.destination_id = p.destination_id AND p.product_id =@product_id AND dn.lang_id = @lang_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = langId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    strDesFolderName = reader[0].ToString();
                }
                return strDesFolderName;
            }

        }
        public Dictionary<string, string> GetB2BDestinationByProductCatID(int intProductCatID)
        { 
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder cStringBuilder = new StringBuilder();
                cStringBuilder.Append(" select d.destination_id,d.title from tbl_destination d, tbl_product p , tbl_product_booking_engine pb ");
                cStringBuilder.Append(" where p.destination_id = d.destination_id and p.product_id = pb.product_id  and pb.cat_id = @cat_id and pb.is_b2b = 1 ");
                cStringBuilder.Append(" group by d.destination_id,d.title ORDER BY d.title ");
                SqlCommand cmd = new SqlCommand(cStringBuilder.ToString(), cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.Int).Value = intProductCatID;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                Dictionary<string, string> dicDes = new Dictionary<string, string>();
                while (reader.Read())
                {
                    dicDes.Add(reader[0].ToString(), reader[1].ToString());
                }
                return dicDes;
            }
        }        
    }
}