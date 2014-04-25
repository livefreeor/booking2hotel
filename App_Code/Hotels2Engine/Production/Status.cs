using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
/// <summary>
/// Summary description for Status
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class Status : Hotels2BaseClass
    {
        public short StatusID { get; set; }
        public byte StatusCatID { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public short Priority { get; set; }
        public bool bolStatus { get; set; }


       
        public Status() { }

        public Dictionary<string, string> GetStatusProductByCatId()
        {
            Dictionary<string, string> dataList = new Dictionary<string, string>();
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT status_id ,title FROM tbl_status WHERE cat_id = 1 AND status = 1 ORDER BY priority", cn);
                //cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCatId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);

                while (reader.Read())
                {
                    dataList.Add(reader[0].ToString(), reader[1].ToString());
                }
                return dataList;
            }
        }
        public Dictionary<string, string> GetStatusByCatId(byte bytCatId)
        {
            Dictionary<string, string> dataList = new Dictionary<string, string>();
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT status_id ,title FROM tbl_status WHERE cat_id = @cat_id AND status = 1 AND status_id IN (68,72,71,83,85) ORDER BY priority", cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCatId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                
                while (reader.Read())
                {
                    dataList.Add(reader[0].ToString(), reader[1].ToString());
                }
                return dataList;
            }
        }

        public Dictionary<string, string> GetStatusByCatIdAccount(byte bytCatId)
        {
            Dictionary<string, string> dataList = new Dictionary<string, string>();
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT status_id ,title FROM tbl_status WHERE cat_id = @cat_id AND status = 1 ORDER BY priority", cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCatId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);

                while (reader.Read())
                {
                    dataList.Add(reader[0].ToString(), reader[1].ToString());
                }
                return dataList;
            }
        }
        public Dictionary<string, string> GetStatusByCatIdbhtManage(byte bytCatId)
        {
            Dictionary<string, string> dataList = new Dictionary<string, string>();
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT status_id ,title FROM tbl_status WHERE cat_id = @cat_id AND status = 1 AND status_id NOT IN (92,30) ORDER BY priority", cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCatId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);

                while (reader.Read())
                {
                    dataList.Add(reader[0].ToString(), reader[1].ToString());
                }
                return dataList;
            }
        }


        public List<object> GetStatusByCatIdBookingBhtManage(byte bytCatId)
        {
            Dictionary<string, string> dataList = new Dictionary<string, string>();
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_status WHERE cat_id = @cat_id AND status = 1 AND status_id NOT IN (92,30) ORDER BY priority", cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCatId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
        public List<object> GetStatusByCatIdBooking(byte bytCatId)
        {
            Dictionary<string, string> dataList = new Dictionary<string, string>();
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_status WHERE cat_id = @cat_id AND status = 1 AND status_id IN (68,72,71,83,85) ORDER BY priority", cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCatId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> GetStatusByCatIdBookingForAcccount(byte bytCatId)
        {
            Dictionary<string, string> dataList = new Dictionary<string, string>();
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_status WHERE cat_id = @cat_id AND status = 1 ORDER BY priority", cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCatId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> GetStatusByCatIdBookingProduct(short shrBookingStatus)
        {
            Dictionary<string, string> dataList = new Dictionary<string, string>();
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_status WHERE cat_id = 3 AND status_id IN (" + StatusBookingGroup(shrBookingStatus) + ") AND status = 1 ORDER BY priority", cn);
                
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public static string GetStatusTitleById(short shrStatusId)
        {
            Status cStatus = new Status();
            using (SqlConnection cn = new SqlConnection(cStatus.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT title FROM tbl_status WHERE status_id = @status_id", cn);
                cmd.Parameters.Add("@status_id", SqlDbType.SmallInt).Value = shrStatusId;
                cn.Open();
                IDataReader reader = cStatus.ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return reader[0].ToString();
                else
                    return "";
               
            }
            
        }

        public static string StatusBookingGroup(short bytStatusBooking)
        {
            string strStatusBookingProduct = string.Empty;
            switch (bytStatusBooking)
            {
                case 68 :
                    strStatusBookingProduct = "10,11,12,13,22,36,70,29,69";
                    break;
                case 32 :
                    strStatusBookingProduct = "15,28,17";
                    break;
                case 27:
                    strStatusBookingProduct = "26";
                    break;
                default:
                    strStatusBookingProduct = "15,28,17";
                    break;
            }

            return strStatusBookingProduct;
        }
        public static string CheckStatusGroup(short bytStatusBooking)
        {
            string strStatusBookingProduct = string.Empty;
            switch (bytStatusBooking)
            {
                case 10:
                case 12:
                case 13:
                case 22:
                case 36:
                case 70:
                case 29:
                case 69:
                    strStatusBookingProduct = "68";
                    break;
                case 15:
                case 28:
                case 17:
                    strStatusBookingProduct = "32";
                    break;
                case 26:
                    strStatusBookingProduct = "27";
                    break;

            }

            return strStatusBookingProduct;
        }
        
    }
}