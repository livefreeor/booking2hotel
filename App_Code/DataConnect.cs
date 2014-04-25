using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;
using System.Web.Caching;

/// <summary>
/// Summary description for DataConnect
/// </summary>
/// 
namespace Hotels2thailand.DataAccess
{
    public class DataConnect
    {
        protected SqlConnection conn;
     
        protected void ConnectionOpen()
        {
            string connString = WebConfigurationManager.ConnectionStrings["booking2hotelXConnectionString"].ConnectionString;
            conn = new SqlConnection(connString);
            conn.Open();
        }

        //public string SqlCommand { get; set; }

        protected SqlDataReader reader;

        public SqlDataReader GetDataReader(string sqlCommand)
        {
            ConnectionOpen();
            SqlCommand cmd = new SqlCommand(sqlCommand, conn);
            reader = cmd.ExecuteReader();
            return reader;
        }

        public int ExecuteNonQuery(string sqlCommand)
        {
            ConnectionOpen();
            SqlCommand cmd = new SqlCommand(sqlCommand + ";select SCOPE_IDENTITY()", conn);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        public int ExecuteScalar(string sqlCommand)
        {
            ConnectionOpen();
            int result = 0;
            SqlCommand cmd = new SqlCommand(sqlCommand, conn);
            if (cmd.ExecuteScalar() != System.DBNull.Value)
            {
                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return result;
        }
        public void Close()
        {
            conn.Close();
        }


        //====== DARKMAN VERSION ===

        private string _connectionString_old = WebConfigurationManager.ConnectionStrings["hotels2thailand_Old"].ConnectionString;
        protected string ConnectionString_old
        {
            get { return _connectionString_old; }
        }

        private string _connectionString_booking_old = WebConfigurationManager.ConnectionStrings["booking2hotelXConnectionString_old"].ConnectionString;
        protected string ConnectionString_booking_old
        {
            get { return _connectionString_booking_old; }
        }
        private string _connectionString = WebConfigurationManager.ConnectionStrings["booking2hotelXConnectionString"].ConnectionString;
        protected string ConnectionString
        {
            get { return _connectionString; }
        }

        

        protected DataTable dtDatatable(SqlCommand cmd)
        {
            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();
                conn.Open();
                cmd.Connection = conn;
                da.SelectCommand = cmd;
                da.Fill(dt);
                return dt;
            }
        }

        protected DataSet dsDataset(SqlCommand cmd)
        {
            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            {
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                conn.Open();
                cmd.Connection = conn;
                da.SelectCommand = cmd;
                da.Fill(ds);
                return ds;
            }
        }

        protected int ExecuteNonQuery(DbCommand cmd)
        {
            return cmd.ExecuteNonQuery();
        }

        protected IDataReader ExecuteReader(DbCommand cmd)
        {
            return ExecuteReader(cmd, CommandBehavior.Default);
        }

        protected IDataReader ExecuteReader(DbCommand cmd, CommandBehavior behavior)
        {
            return cmd.ExecuteReader(behavior);
        }

        protected object ExecuteScalar(DbCommand cmd)
        {
            return cmd.ExecuteScalar();
        }


    }
}

