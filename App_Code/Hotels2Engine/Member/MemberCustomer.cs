using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Booking;
using System.Data.SqlTypes;
using Hotels2thailand.DataAccess;
using Hotels2thailand.Production;

/// <summary>
/// Summary description for Booking
/// </summary>
/// 
namespace Hotels2thailand.Member
{
    public class Member_customer :Hotels2BaseClass
    {
       

        public int CustomerID { get; set; }
        public byte CountryID { get; set; }
        public short? OccupationID { get; set; }
        public byte? PrefixID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public DateTime? DateBirth { get; set; }
        public bool Mail { get; set; }
        public DateTime DateSubmit { get; set; }
        public int ProductId { get; set; }
        public string Password { get; set; }
        public bool Ismember { get; set; }
        public bool Isactive { get; set; }
        public bool Status { get; set; }

        private string _country_title = string.Empty;
        public string CountryTitle
        {
            get {
                if (string.IsNullOrEmpty(_country_title))
                {
                    Country cCountry = new Country();
                    _country_title = cCountry.GetCountryById(this.CountryID).Title;
                }
                return _country_title;
            }
        }
        private string _cusPhone = string.Empty;
        public string CusPhone
        {
            get 
            {
                if (string.IsNullOrEmpty(_cusPhone))
                {
                    _cusPhone =  this.getPhoneByCusId(this.CustomerID, 1); 
                 }
                return _cusPhone;
            }
        }

        private string _cus_mobile = string.Empty;
        public string CusMobile
        {
            get
            {
                if (string.IsNullOrEmpty(_cus_mobile))
                {
                    _cus_mobile = this.getPhoneByCusId(this.CustomerID, 2);
                }
                return _cus_mobile;
            }
        }

        private string _cus_fax = string.Empty;
        public string CusFax
        {
            get
            {
                if (string.IsNullOrEmpty(_cus_fax))
                {
                    _cus_fax = this.getPhoneByCusId(this.CustomerID, 3);
                }
                return _cus_fax;
            }
        }


        public Member_customer()
        {
            //CustomerID = 0;
            //CountryID = 208;
            //OccupationID = null;
            //PrefixID = 1;
            //FullName = string.Empty;
            //Email = string.Empty;
            //Address = null;
            //Zip = null;
            //DateBirth = null;
            //Mail = false;
            //DateSubmit = DateTime.Now;
        }

        public List<object> getMemberSearch(int intProduct_id, string KeySearch)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT cus_id,country_id,occupation_id,prefix_id,full_name,email,address,zip,date_birth,mail,date_submit,product_id,password,is_member ,is_active,status FROM tbl_customer WHERE product_id=@product_id AND (full_name LIKE '%' + @KeySearch + '%' OR email LIKE '%' + @KeySearch + '%')", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProduct_id;
                cmd.Parameters.Add("@KeySearch", SqlDbType.NVarChar).Value = KeySearch.Trim();
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public  List<object> getMember(int intProduct_id, int Pagenum, bool Isactivat, bool Status){
               
            using(SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("member_list", cn);
                    cmd.CommandType = CommandType.StoredProcedure; 
                    cmd.Parameters.Add("@productId", SqlDbType.Int).Value = intProduct_id;
                    cmd.Parameters.Add("@PageNum", SqlDbType.Int).Value = Pagenum;
                    cmd.Parameters.Add("@active", SqlDbType.Bit).Value = Isactivat;
                    cmd.Parameters.Add("@status", SqlDbType.Bit).Value = Status;
                    cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public int CountMember(int intProduct_id,  bool Isactivat, bool Status)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(cus_id) FROM tbl_customer WHERE product_id=@product_id AND is_active=@is_active AND status=@status", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProduct_id;
                cmd.Parameters.Add("@is_active", SqlDbType.Bit).Value = Isactivat;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = Status;
                cn.Open();
                return (int)ExecuteScalar(cmd);
            }
        }


        public Member_customer getMemberById(int intCusId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_customer WHERE cus_id = @cus_id", cn);
                cmd.Parameters.Add("@cus_id", SqlDbType.Int).Value = intCusId;
                cn.Open();

                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (Member_customer)MappingObjectFromDataReader(reader);
                else
                    return null;
            }
        }

        public string getPhoneByCusId(int cusId, byte phoneCat)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT number_phone FROM tbl_customer_phone  WHERE cat_id = @cat_id AND cus_id = @cus_id", cn);
                cmd.Parameters.Add("@cus_id", SqlDbType.Int).Value = cusId;
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = phoneCat;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    return reader[0].ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        public bool UpdateCustomer(string name, int cusId, string strEMail, DateTime? dDAtebirth, byte shrCOuntry)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_customer SET full_name=@full_name,email=@email,date_birth=@date_birth,country_id=@country_id WHERE cus_id=@cus_id", cn);
                
                cmd.Parameters.Add("@cus_id", SqlDbType.Int).Value = cusId;
                cmd.Parameters.Add("@full_name", SqlDbType.NVarChar).Value = name;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = strEMail;

                if (dDAtebirth.HasValue)
                    cmd.Parameters.Add("@date_birth", SqlDbType.SmallDateTime).Value = dDAtebirth;
                else
                    cmd.Parameters.AddWithValue("@date_birth", DBNull.Value);

                cmd.Parameters.Add("@country_id", SqlDbType.TinyInt).Value = shrCOuntry;
                cn.Open();
                return (ExecuteNonQuery(cmd) == 1);
            }
        }


        public bool UpdateStatus( int cusId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                cn.Open();
                bool bolStatus = false ;
                SqlCommand cmdsel = new SqlCommand("SELECT status FROM tbl_customer WHERE cus_id = @cus_id", cn);
                cmdsel.Parameters.Add("@cus_id", SqlDbType.Int).Value = cusId;
                IDataReader reader = ExecuteReader(cmdsel, CommandBehavior.SingleRow);
                if (reader.Read())
                    bolStatus = (bool)reader[0];

                reader.Close();
                if (bolStatus)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE tbl_customer SET status=@status WHERE cus_id=@cus_id", cn);

                    cmd.Parameters.Add("@cus_id", SqlDbType.Int).Value = cusId;
                    cmd.Parameters.Add("@status", SqlDbType.Bit).Value = false; 

                    return (ExecuteNonQuery(cmd) == 1);
                }
                else
                {

                    SqlCommand cmd = new SqlCommand("UPDATE tbl_customer SET status=@status WHERE cus_id=@cus_id", cn);

                    cmd.Parameters.Add("@cus_id", SqlDbType.Int).Value = cusId;
                    cmd.Parameters.Add("@status", SqlDbType.Bit).Value = true; ;

                    return (ExecuteNonQuery(cmd) == 1);
                }
            }
        }

        public bool GenNewPassword(int intCusId, string NewPass)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_customer SET password=@password WHERE cus_id =@cus_id",cn);
                cmd.Parameters.Add("@cus_id", SqlDbType.Int).Value = intCusId;
                cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = NewPass.Hotels2MD5EncryptedData();
                cn.Open();
                return (ExecuteNonQuery(cmd) == 1);
            }

        }

        public bool UpdateActivate(int cusId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                cn.Open();
                bool bolStatus = false;
                SqlCommand cmdsel = new SqlCommand("SELECT is_active FROM tbl_customer WHERE cus_id = @cus_id", cn);
                cmdsel.Parameters.Add("@cus_id", SqlDbType.Int).Value = cusId;
                IDataReader reader = ExecuteReader(cmdsel, CommandBehavior.SingleRow);
                if (reader.Read())
                    bolStatus = (bool)reader[0];

                reader.Close();
                if (bolStatus)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE tbl_customer SET is_active=@is_active WHERE cus_id=@cus_id", cn);

                    cmd.Parameters.Add("@cus_id", SqlDbType.Int).Value = cusId;
                    cmd.Parameters.Add("@is_active", SqlDbType.Bit).Value = false; ;

                    return (ExecuteNonQuery(cmd) == 1);
                }
                else
                {

                    SqlCommand cmd = new SqlCommand("UPDATE tbl_customer SET is_active=@is_active WHERE cus_id=@cus_id", cn);

                    cmd.Parameters.Add("@cus_id", SqlDbType.Int).Value = cusId;
                    cmd.Parameters.Add("@is_active", SqlDbType.Bit).Value = true; ;

                    return (ExecuteNonQuery(cmd) == 1);
                }
            }
        }

        
        public bool UpdatePhone(string phone, int cusId)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmdsel = new SqlCommand("SELECT COUNT(*) FROM tbl_customer_phone WHERE cus_id=@cus_id AND cat_id = 1", cn);
                cmdsel.Parameters.Add("@cus_id", SqlDbType.Int).Value = cusId;
               
                cn.Open();
                if ((int)ExecuteScalar(cmdsel) > 0)
                {

                    SqlCommand cmd = new SqlCommand("UPDATE tbl_customer_phone SET number_phone=@number_phone WHERE cus_id=@cus_id AND cat_id = 1", cn);

                    cmd.Parameters.Add("@cus_id", SqlDbType.Int).Value = cusId;
                    cmd.Parameters.Add("@number_phone", SqlDbType.VarChar).Value = phone;
                    

                    ret = ExecuteNonQuery(cmd);

                }
                else
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO tbl_customer_phone  (number_phone,cus_id,cat_id) VALUES (@number_phone,@cus_id,1)", cn);
                    cmd.Parameters.Add("@cus_id", SqlDbType.Int).Value = cusId;
                    
                    cmd.Parameters.Add("@number_phone", SqlDbType.VarChar).Value = phone;
                    
                    ret = ExecuteNonQuery(cmd);
                }
            }

            return (ret == 1);
        }
        

        
        

       

   }

        
   
}