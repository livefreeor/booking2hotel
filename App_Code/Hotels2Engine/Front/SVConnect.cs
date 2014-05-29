using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
/// <summary>
/// Summary description for SVConnect
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class SVConnect
    {
        protected SqlConnection conn;

        protected void ConnectionOpen()
        {
            string connString = WebConfigurationManager.ConnectionStrings["hotels2thailandXConnectionString1"].ConnectionString;
            conn = new SqlConnection(connString);
            conn.Open();
            
        }
        public void Close()
        {
            conn.Close();
        }
        public string SqlCommand { get; set; }

        //public SqlDataReader GetDataReader{
        //    get
        //    {
        //        SVReader reader = new SVReader();
        //        return reader.GetDataReader(SqlCommand);
        //    }
        //}
        protected SqlDataReader reader;
        public SqlDataReader GetDataReader(string sqlCommand)
        {
            ConnectionOpen();
            SqlCommand cmd = new SqlCommand(sqlCommand, conn);
            reader = cmd.ExecuteReader();
            return reader;
        }

       
    }

    public class SVReader:SVConnect
    {
        protected SqlDataReader reader;
        public SqlDataReader GetDataReader(string sqlCommand)
        {
            ConnectionOpen();
            SqlCommand cmd = new SqlCommand(sqlCommand, conn);
            reader = cmd.ExecuteReader();
            return reader;
        }
        
    }
    
}