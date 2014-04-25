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

/// <summary>
/// Summary description for Booking
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class Customer :Hotels2BaseClass
    {
        private LinqBookingDataContext dcCustomer = new LinqBookingDataContext();

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
        
        public DateTime? DateSubmit { get; set; }
        public int? ProductID { get; set; }
        public string Password { get; set; }
        public bool IsMember { get; set; }
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

        


        public Customer()
        {
            CustomerID = 0;
            CountryID = 208;
            OccupationID = null;
            PrefixID = 1;
            FullName = string.Empty;
            Email = string.Empty;
            Address = null;
            Zip = null;
            DateBirth = null;
            Mail = false;
            DateSubmit = DateTime.Now;
            Password = null;
            IsMember = false;
        }

        public Customer(int customerID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_customer WHERE cus_id = @cus_id", cn);
                cmd.Parameters.Add("@cus_id", SqlDbType.Int).Value = customerID;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    this.CustomerID = (int)reader["cus_id"];
                    this.ProductID = (int)reader["product_id"];
                    this.CountryID = (byte)reader["country_id"];
                    this.OccupationID = null;
                    this.PrefixID = (byte)reader["prefix_id"];
                    this.FullName = reader["full_name"].ToString();
                    this.Email = reader["email"].ToString();
                    this.Address = reader["address"].ToString();
                    this.Zip = reader["zip"].ToString();
                    this.DateBirth = (DateTime)reader["date_birth"];
                    this.Mail = (bool)reader["mail"];
                    this.DateSubmit = DateTime.Now;
                    this.Password = reader["password"].ToString();
                    this.IsMember = (bool)reader["is_member"];
                }
                else
                {
                    this.CustomerID = 0;
                    this.ProductID = 0;
                    this.CountryID = 208;
                    this.OccupationID = null;
                    this.PrefixID = 1;
                    this.FullName = string.Empty;
                    this.Email = string.Empty;
                    this.Address = null;
                    this.Zip = null;
                    this.DateBirth = null;
                    this.Mail = false;
                    this.DateSubmit = DateTime.Now;
                    this.Password = null;
                    this.IsMember = false;
                }
            }
        }

        public int Insert(Customer data)
        {
            int ret = 0;
            //tbl_customer customer=new tbl_customer{
            //country_id=data.CountryID,
            //occupation_id = data.OccupationID,
            //prefix_id = data.PrefixID,
            //full_name = data.FullName,
            //email = data.Email,
            //address = data.Address,
            //zip = data.Zip,
            //date_birth = data.DateBirth,
            //mail = data.Mail,
            //date_submit=data.DateSubmit
            //};

            //dcCustomer.tbl_customers.InsertOnSubmit(customer);
            //dcCustomer.SubmitChanges();
            string sqlCommand = string.Empty;

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                sqlCommand = sqlCommand + "insert into tbl_customer(country_id,prefix_id,full_name,email,date_birth,mail,date_submit,product_id,password,is_member)";
                sqlCommand = sqlCommand + "values(@country_id,@prefix_id,@full_name,@email,@date_birth,@mail,@date_submit,@product_id,@password,@is_member); SET @customer_id = SCOPE_IDENTITY()";

                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cmd.Parameters.Add("@country_id", SqlDbType.TinyInt).Value = CountryID;
                cmd.Parameters.Add("@prefix_id", SqlDbType.TinyInt).Value = data.PrefixID;
                cmd.Parameters.Add("@full_name", SqlDbType.NVarChar).Value = data.FullName;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = data.Email;
                //cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = data.Address;
                //cmd.Parameters.Add("@zip", SqlDbType.NVarChar).Value = data.Zip;
                if (data.DateBirth != null)
                {
                    cmd.Parameters.Add("@date_birth", SqlDbType.SmallDateTime).Value = data.DateBirth;
                }
                else {
                    cmd.Parameters.Add("@date_birth", SqlDbType.SmallDateTime).Value = DBNull.Value;
                }
                cmd.Parameters.Add("@mail", SqlDbType.Bit).Value = data.Mail;
                cmd.Parameters.Add("@is_member", SqlDbType.Bit).Value = data.IsMember;
                cmd.Parameters.Add("@date_submit", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = data.ProductID;
                cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = data.Password;
                cmd.Parameters.Add("@customer_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                ExecuteNonQuery(cmd);
                ret = (int)cmd.Parameters["@customer_id"].Value;
            }

            return ret;
        }

       

        public bool UpdateCustomer(int CusId, byte bytCountry,  string strfull_name, string strEmail, string strAddress, DateTime strDate_birth, bool bolMail)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_customer SET country_id=@country_id, full_name=@full_name, email=@email, address=@address, date_birth=@date_birth, mail=@mail WHERE cus_id=@cus_id", cn);
                cmd.Parameters.Add("@cus_id", SqlDbType.Int).Value = CusId;
                cmd.Parameters.Add("@country_id", SqlDbType.TinyInt).Value = bytCountry;
                //cmd.Parameters.Add("@occupation_id", SqlDbType.SmallInt).Value = srOcc;
                cmd.Parameters.Add("@full_name", SqlDbType.NVarChar).Value = strfull_name;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = strEmail;
                cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = strAddress;
                cmd.Parameters.Add("@mail", SqlDbType.Bit).Value = bolMail;
                cmd.Parameters.Add("@date_birth", SqlDbType.SmallDateTime).Value = strDate_birth;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret==1);
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

        public int InsertPhonebyCusId(int cusId, byte phoneCat, string strPhoneNum)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_customer_phone (cat_id,cus_id,number_phone) VALUES(@cat_id,@cus_id,@number_phone)", cn);
                cmd.Parameters.Add("@cus_id", SqlDbType.Int).Value = cusId;
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = phoneCat;
                cmd.Parameters.Add("@number_phone", SqlDbType.VarChar).Value = strPhoneNum;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return ret;
            }
        }

        public bool UpdatePhonebyCusId(int cusId, byte phoneCat, string strPhoneNum)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_customer_phone SET number_phone=@number_phone WHERE cat_id=@cat_id AND cus_id=@cus_id", cn);
                cmd.Parameters.Add("@cus_id", SqlDbType.Int).Value = cusId;
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = phoneCat;
                cmd.Parameters.Add("@number_phone", SqlDbType.VarChar).Value = strPhoneNum;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }

        public int InsertWithCheckEmail(Customer data)
        {

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {


                string strCommand = "select ISNULL(MAX(cus_id),0) from tbl_customer where email='" + data.Email + "' and product_id=" + data.ProductID;
                SqlCommand cmd = new SqlCommand(strCommand, cn);
                cn.Open();
                int result = (int)cmd.ExecuteScalar();
                //var customer = dcCustomer.tbl_customers.SingleOrDefault(cs=>cs.email == data.Email);
                if (result == 0)
                {
                    result = Insert(data);
                }

                return result;
            }
            
          
         }

        //Customer For Newsletter by Mr.Darkman ^_^ 

        public IList<object> getCustomerListSubscribe()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT cus_id,country_id,occupation_id,prefix_id,full_name,email,address,zip,date_birth,mail,date_submit FROM tbl_customer WHERE mail =1 ", cn);
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public IList<object> getCustomerMember(int intProduct_id)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT cus_id,country_id,occupation_id,prefix_id,full_name,email,address,zip,date_birth,mail,date_submit FROM tbl_customer WHERE product_id=@product_id AND is_member =1  ", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProduct_id;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public IList<object> getCustomerListAll(int intProduct_id)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT cus_id,country_id,occupation_id,prefix_id,full_name,email,address,zip,date_birth,mail,date_submit FROM tbl_customer WHERE product_id=@product_id", cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = intProduct_id;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }


        public int InsertMemberWithCheckEmail(Customer data)
        {

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {


                string strCommand = "select ISNULL(MAX(cus_id),0) from tbl_customer where email='" + data.Email + "' and product_id=" + data.ProductID;
                SqlCommand cmd = new SqlCommand(strCommand, cn);
                cn.Open();
                int result = (int)cmd.ExecuteScalar();
                //var customer = dcCustomer.tbl_customers.SingleOrDefault(cs=>cs.email == data.Email);
                if (result == 0)
                {
                    result = Insert(data);
                }
                else {
                    result = 0;
                }

                return result;
            }


        }

        public int UpdateCustomerAccount(Customer data)
        {
            int ret = 0;
            string sqlCommand = string.Empty;

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                sqlCommand = sqlCommand + "update tbl_customer set password=@password where cus_id="+data.CustomerID;

                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = data.Password;
                cn.Open();
                ret =ExecuteNonQuery(cmd);
            }

            return ret;
        }

        public int ActivateCustomerAccount(int customerID)
        {
            int ret = 0;
            string sqlCommand = string.Empty;

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                sqlCommand = sqlCommand + "update tbl_customer set is_active=1 where cus_id=" + customerID;

                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                //cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = data.Password;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }

            return ret;
        }

        public int ResetNewPassword(int customerID,string newPassword)
        {
            int ret = 0;
            string sqlCommand = string.Empty;

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                sqlCommand = sqlCommand + "update tbl_customer set password='"+newPassword+"' where cus_id=" + customerID;
                //HttpContext.Current.Response.Write(sqlCommand);
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                //cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = data.Password;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }

            return ret;
        }

        public Customer GetCustomerbyEmail(string email,int productID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_customer WHERE email = @email and product_id=@product_id", cn);
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = productID;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    return (Customer)MappingObjectFromDataReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }

        public Customer GetCustomerbyLogin(string userName,string passWord,int ProductID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_customer WHERE email = @email and password=@password and product_id=@product_id and is_active=1", cn);
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value =userName;
                cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = passWord;
                cmd.Parameters.Add("@product_id",SqlDbType.Int).Value=ProductID;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    return (Customer)MappingObjectFromDataReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }

        public Customer GetCustomerbyId(int intCus_id)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_customer WHERE cus_id = @cus_id", cn);
                cmd.Parameters.Add("@cus_id", SqlDbType.Int).Value = intCus_id;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    return (Customer)MappingObjectFromDataReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }

        //Occupation
        public Dictionary<short, string> getDicOccupation()
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT occupation_id, title FROM tbl_occupation", cn);
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                Dictionary<short, string> dicResult = new Dictionary<short, string>();
                while (reader.Read())
                {
                    dicResult.Add((short)reader[0], reader[1].ToString());
                }

                return dicResult;
            }
        }

   }

        
    public class CustomerPhone{

        private LinqBookingDataContext dcPhone = new LinqBookingDataContext();

        public int PhoneID { get; set; }
        public byte Category { get; set; }
        public int CustomerID { get; set; }
        public string CountryCode { get; set; }
        public string LocalCode { get; set; }
        public string PhoneNumber { get; set; }

        public int Insert(CustomerPhone data)
        {
            tbl_customer_phone phone = new tbl_customer_phone { 
                 cat_id=data.Category,
                 cus_id=data.CustomerID,
                 code_country=data.CountryCode,
                 code_local=data.LocalCode,
                 number_phone=data.PhoneNumber
            };
            dcPhone.tbl_customer_phones.InsertOnSubmit(phone);
            dcPhone.SubmitChanges();
            return phone.phone_id;
        }
    }
}