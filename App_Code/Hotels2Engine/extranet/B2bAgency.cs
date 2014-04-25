using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for B2bAgency
/// </summary>
namespace Hotels2thailand.Production
{
    public class B2bAgency : Hotels2BaseClass
    {
        public int agency_id { get; set; }
        public string agency_name { get; set; }
        public string contact_name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public decimal commission { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool status { get; set; }

        public void CheckB2bAgencyLogin(string strUsername, string strPassword)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("select Agency_ID from tbl_b2b_agency where username=@username and password=@password and status = 1", cn);
                cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = strUsername;
                cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = strPassword; 
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    agency_id = (int)reader["Agency_ID"];
                }
                else
                {
                    agency_id = 0;
                }
            }
        }

        public int CheckUsernameAvailability(string strUsername)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                int AgencyID = 0;
                SqlCommand cmd = new SqlCommand("select Agency_ID from tbl_b2b_agency where username=@username ", cn);
                cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = strUsername;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    AgencyID = (int)reader["Agency_ID"];
                }
                return AgencyID;
            }
        }

        public B2bAgency GetB2bAgencyProfile(int AgencyID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(" select * from tbl_b2b_agency where agency_id=@agency_id", cn);
                cmd.Parameters.Add("@agency_id", SqlDbType.Int).Value = AgencyID;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);

                if (reader.Read())
                    return (B2bAgency)MappingObjectFromDataReader(reader);
                else
                    return null;


            }
        }

        public int InsertB2bAgencyProfile(string strAgencyName, string strContactName, string strAddress, string strPhone, string strEmail, decimal decCommission, string strUsername, string strPassword, bool boolStatus)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                int ret = 0;
                StringBuilder cStringBuilder = new StringBuilder();
                cStringBuilder.Append(" Insert into tbl_b2b_agency ");
                cStringBuilder.Append(" (agency_name,contact_name,address,phone,email,commission,username,password,status) ");
                cStringBuilder.Append(" Values (@agency_name,@contact_name,@address,@phone,@email,@commission,@username,@password,@status); ");
                cStringBuilder.Append(" SET @agency_id = SCOPE_IDENTITY(); ");
                SqlCommand cmd = new SqlCommand(cStringBuilder.ToString(), cn);
                cmd.Parameters.Add("@agency_name", SqlDbType.VarChar).Value = strAgencyName;
                cmd.Parameters.Add("@contact_name", SqlDbType.VarChar).Value = strContactName;
                cmd.Parameters.Add("@address", SqlDbType.VarChar).Value = strAddress;
                cmd.Parameters.Add("@phone", SqlDbType.VarChar).Value = strPhone;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = strEmail;
                cmd.Parameters.Add("@commission", SqlDbType.Decimal).Value = decCommission;
                cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = strUsername;
                cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = strPassword;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = boolStatus;
                cmd.Parameters.Add("@agency_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                ExecuteNonQuery(cmd);
                return (int)cmd.Parameters["@agency_id"].Value;
            }
        }

        public bool UpdateB2bAgencyProfile(int AgencyID, string strAgencyName, string strContactName, string strAddress, string strPhone, string strEmail, decimal decCommission, string strUsername, string strPassword , bool boolStatus)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                int ret = 0;
                StringBuilder cStringBuilder = new StringBuilder();
                cStringBuilder.Append(" Update tbl_b2b_agency ");
                cStringBuilder.Append(" Set agency_name=@agency_name,contact_name=@contact_name,address=@address ");
                cStringBuilder.Append(" ,phone=@phone,email=@email,commission=@commission,username=@username,password =@password,status=@status");
                cStringBuilder.Append(" where agency_id=@agency_id ");
                SqlCommand cmd = new SqlCommand(cStringBuilder.ToString(), cn);
                cmd.Parameters.Add("@agency_id", SqlDbType.Int).Value = AgencyID;
                cmd.Parameters.Add("@agency_name", SqlDbType.VarChar).Value = strAgencyName;
                cmd.Parameters.Add("@contact_name", SqlDbType.VarChar).Value = strContactName;
                cmd.Parameters.Add("@address", SqlDbType.VarChar).Value = strAddress;
                cmd.Parameters.Add("@phone", SqlDbType.VarChar).Value = strPhone;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = strEmail;
                cmd.Parameters.Add("@commission", SqlDbType.Decimal).Value = decCommission;
                cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = strUsername;
                cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = strPassword;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = boolStatus;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }

        public IList<object> GetAgencyList()
        {
            using (SqlConnection Conn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Select * from tbl_b2b_agency order by agency_name", Conn);
                Conn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public static string GetAgencyImg(int intAgencyID)
        {
            string strHTML = "<img src='" + System.Web.VirtualPathUtility.ToAbsolute("~/images/agency/imgAgency_" + intAgencyID + ".jpg") + "' />";
            return strHTML;
        }
    }
}