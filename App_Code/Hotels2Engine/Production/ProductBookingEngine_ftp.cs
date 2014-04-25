using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Hotels2thailand.Production;
using Hotels2thailand.Staffs;
/// <summary>
/// Summary description for Product
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public partial class ProductBookingEngine_FTP : Hotels2BaseClass
    {
        public int FtpId { get; set; }
        public int intProductId { get; set; }
        public string Server { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Comment { get; set; }
        public string Prot { get; set; }
        public bool Status { get; set; }

        public ProductBookingEngine_FTP() { }


        public int InsertFTp(int intProductID,string strServer, string strUserName, string strPassword, string strComment, string strPort, bool bolStatus)
        {
            int ret = 0;

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_product_ftp (product_id,server,user_name,password,comment,port,status) VALUES (@product_id,@server,@user_name,@password,@comment,@port,@status)", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductID;
                cmd.Parameters.Add("@server", SqlDbType.VarChar).Value = strServer;
                cmd.Parameters.Add("@user_name", SqlDbType.VarChar).Value = strUserName;
                cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = strPassword;
                cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = strComment;
                cmd.Parameters.Add("@port", SqlDbType.VarChar).Value = strPort;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }

            return ret;
        }

        public IList<object> GetProductFtp(int intProductId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_product_ftp WHERE product_id=@product_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProductId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

    }
}