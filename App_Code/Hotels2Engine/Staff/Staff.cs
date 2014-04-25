using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Linq;
using System.Web;
using System.Reflection;
using Hotels2thailand.LinqProvider.Staff;
using Hotels2thailand.Suppliers;



/// <summary>
/// Summary description for Staff
/// </summary>
/// 
namespace Hotels2thailand.Staffs
{
    public partial class Staff : Hotels2BaseClass
    {
          

        // declare Property for Staff class!
        public short Staff_Id { get; set; }
        public byte Cat_Id { get; set; }
        //public short Supplier_Id { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }

        private string _passwod = string.Empty;

	    public string PassWord
	    {
            get { return _passwod; }

		    set {  _passwod = value;}
	    }

        public DateTime LastAccess{ get; set; }
        

        public bool Status { get; set; }
        public string Email { get; set; }
        //external property 
        private string _cat_title = string.Empty;

        public string CatTitle
        {
            get
            {
                if (string.IsNullOrEmpty(_cat_title))
                    _cat_title = Staff.getStaffCategoryById(this.Cat_Id);
                return _cat_title;
            }
        }

        //private string  _supplier_title = string.Empty;

        //public string  Suppliertitle
        //{
        //    get 
        //    { 
        //        if(string.IsNullOrEmpty(_supplier_title))
        //        {
        //            Supplier clSup = new Supplier();
        //            _supplier_title = clSup.getSupplierById(this.Supplier_Id).SupplierTitle;
        //        }
        //        return _supplier_title;  
        //    }
        // }
        

        
        //declare Instance Linq for Staff Module
        LinqStaffDataContext dcStaff = new LinqStaffDataContext();



        public bool Update()
        {
            return Staff.updateStaff(this.Staff_Id, this.Cat_Id, this.Title, this.UserName, this.Status);

        }

        //===============================================================================================
        //================================= Staff =======================================================

        
       

        public List<object> getStaffListBlueHouseByCategory(short shrCat)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT staff_id,cat_id,title,user_name,password,last_access,status FROM tbl_staff WHERE cat_id=@cat_id", cn);
                //partner cat = 6;
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = shrCat;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
             
        }



        public List<object> getStaffListPartnerBySupplier(short shrSupplierId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT staff_id,cat_id,title,user_name,password,last_access,status FROM tbl_staff WHERE cat_id=@cat_id AND supplier_id=@supplier_id", cn);
                //partner cat = 6;
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = 6;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                
                cn.Open();
                return  MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
              
        }

       
        //Get StaffById
        //Return Single record 
        //if file not found, then return default values
        public Staff getStaffById(short intStaff)
        {
           using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT staff_id,cat_id,title,user_name,password,last_access,status FROM tbl_staff WHERE staff_id=@staff_id", cn);
                cmd.Parameters.Add("@staff_id", SqlDbType.SmallInt).Value = intStaff;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (Staff)MappingObjectFromDataReader(reader);
                else
                    return null;
            }
        }

        /// <summary>
        /// Check Username & Password for Staff ; instance method ; Returns instance of Staff
        /// </summary>
        /// <param name="staff"></param>
        /// <returns></returns>
        public  Staff HaveStaffLogin(string strUserName , string strPassword)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_staff WHERE user_name=@user_name AND password=@password",cn);
                cmd.Parameters.Add("@user_name", SqlDbType.VarChar).Value = strUserName;
                cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = strPassword.Hotels2MD5EncryptedData();
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (Staff)MappingObjectFromDataReader(reader);
                else
                    return null;
             

            }
            
            
        }

        /// <summary>
        /// Insert New Staff(All Property) ; return int when insert completed
        /// </summary>
        /// <param name="clStaff"></param>
        /// <returns></returns>
        public int insertNewStaff(Staff clStaff)
        {
           var varInsert = new tbl_staff
                {
                    staff_id    = clStaff.Staff_Id,
                    cat_id      = clStaff.Cat_Id,
                    title       = clStaff.Title,
                    user_name   = clStaff.UserName,
                    password    = clStaff.PassWord,
                    last_access = clStaff.LastAccess.Hotels2ThaiDateTime(),
                    status      = clStaff.Status
                };
            int ret = 0;

           using (SqlConnection cn = new SqlConnection(this.ConnectionString))
           {
               SqlCommand cmd = new SqlCommand("INSERT INTO tbl_staff (cat_id,title,user_name,password,last_access,status)VALUES(@cat_id,@title,@user_name,@password,@last_access,@status);SET @staff_id=SCOPE_IDENTITY();", cn);
               cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = clStaff.Cat_Id;
               cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = clStaff.Title;
               cmd.Parameters.Add("@user_name", SqlDbType.VarChar).Value = clStaff.UserName;
               cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = clStaff.PassWord;
               cmd.Parameters.Add("@last_access", SqlDbType.SmallDateTime).Value = clStaff.LastAccess.Hotels2ThaiDateTime();
               cmd.Parameters.Add("@status", SqlDbType.Bit).Value = clStaff.Status;
               cmd.Parameters.Add("@staff_id", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
               cn.Open();
               ExecuteNonQuery(cmd);
               ret = (short)cmd.Parameters["@staff_id"].Value;
           }
              

             //=== STAFF ACTIVITY =====================================================================================================================================
             StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Staff, StaffLogActionType.Insert, StaffLogSection.NULL,
                 null, "tbl_staff", "cat_id,title,user_name,password,last_access,status", "staff_id", varInsert.staff_id);
             //========================================================================================================================================================
             return ret;
        }

        //Update Staff (All property)
        //return bool IsCompleted??
        public bool updateStaffs(Staff clStaff)
        {
            int ret = 0;
            //StaffSessionAuthorize clStaffSession = new StaffSessionAuthorize();
            //    var varUpdate = dcStaff.tbl_staffs.Single(sf => sf.staff_id == clStaff.Staff_Id);
            //    //#Staff_Activity_Log================================================================================================ STEP 1 ==
            //    //ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep(varUpdate.cat_id, varUpdate.title, varUpdate.user_name, varUpdate.last_access, varUpdate.status);
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_staff", "cat_id,title,user_name,status", "staff_id", clStaff.Staff_Id);
            //    //============================================================================================================================
            //    varUpdate.cat_id        = clStaff.Cat_Id;
            //    varUpdate.title         = clStaff.Title;
            //    varUpdate.user_name     = clStaff.UserName;
            //    if (clStaffSession.getCurrentStaff_Id == this.Staff_Id)
            //    {
            //        varUpdate.last_access = clStaff.LastAccess.Hotels2ThaiDateTime();
            //    }
                
            //    varUpdate.status        = clStaff.Status;
                
                using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE tbl_staff SET cat_id=@cat_id,title=@title,user_name=@user_name,status=@status WHERE staff_id=@staff_id", cn);
                    cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = clStaff.Cat_Id;
                    cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = clStaff.Title;
                    cmd.Parameters.Add("@user_name", SqlDbType.VarChar).Value = clStaff.UserName;
                    
                    //cmd.Parameters.Add("@last_access", SqlDbType.SmallDateTime).Value = clStaff.LastAccess.Hotels2ThaiDateTime();
                    cmd.Parameters.Add("@status", SqlDbType.Bit).Value = clStaff.Status;
                    cmd.Parameters.Add("@staff_id", SqlDbType.SmallInt).Value = clStaff.Staff_Id;
                    cn.Open();
                    ret = ExecuteNonQuery(cmd);
                    
                }
                
                // Linq method to Update
                dcStaff.SubmitChanges();
               
                //#Staff_Activity_Log================================================================================================ STEP 2 ============
                StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Staff, StaffLogActionType.Update, StaffLogSection.NULL, null, "tbl_staff", "cat_id,title,user_name,status", arroldValue, "staff_id", clStaff.Staff_Id);
                //==================================================================================================================== COMPLETED ========
                return (ret == 1);
           
        }

        public static int insertStaff(byte bytCat_Id, short shrSupplier_id,
            string strTitle, string strUsername, string strPassword)
        {
            Staff clStaff = new Staff
            {
                Staff_Id    = 0,
                Cat_Id      = bytCat_Id,
                
                Title       = strTitle,
                UserName    = strUsername,
                PassWord    =  strPassword.Hotels2MD5EncryptedData(),
                LastAccess  = DateTime.Now.Hotels2ThaiDateTime(),
                Status      = true
            };

            return clStaff.insertNewStaff(clStaff);
        }

        //Static Update 
        public static bool updateStaff(short shrStaff_Id, byte bytCat_Id,
            string strTitle, string strUsername, bool bolStatus)
        {
            Staff   clstaff = new Staff{
                    Staff_Id    = shrStaff_Id,
                    Cat_Id      = bytCat_Id,
                    Title       = strTitle,
                    UserName    = strUsername,
                    //PassWord    = MD5EncryptedData(strPassWord),
                    //LastAccess  = DateTime.Now.Hotels2ThaiDateTime(),
                    Status = bolStatus
                   
            };
            //bool bolUpdate  = 
            return clstaff.updateStaffs(clstaff);
        }
        public static bool updateStaffs(short shrStaff_Id, byte bytCat_Id,
            string strTitle, string strUsername)
        {
            Staff clstaff = new Staff
            {
                Staff_Id = shrStaff_Id,
                Cat_Id = bytCat_Id,
                Title = strTitle,
                UserName = strUsername,
                //PassWord    = MD5EncryptedData(strPassWord),
                //LastAccess = DateTime.Now.Hotels2ThaiDateTime(),
                
            };
            //bool bolUpdate  = 
            return clstaff.updateStaffs(clstaff);
        }
        
        public static bool updateStaffPassWord(short shrstaff_id, string strPassword)
        {
            Staff cStaff = new Staff();
            int ret = 0;

            using (SqlConnection cn = new SqlConnection(cStaff.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_staff SET password=@password WHERE staff_id=@staff_id",cn);
                cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = strPassword.Hotels2MD5EncryptedData();
                cmd.Parameters.Add("@staff_id",SqlDbType.SmallInt).Value = shrstaff_id;
                cn.Open();
               ret =  cStaff.ExecuteNonQuery(cmd);
            }

            return (ret == 1);

           
        }

        public bool updateLastAccess(short Staff_id)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_staff SET last_access=@last_access WHERE staff_id=@staff_id", cn);
                cmd.Parameters.Add("staff_id", SqlDbType.SmallInt).Value = Staff_id;
                cmd.Parameters.Add("last_access", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
            
        }

        //===============================================================================================
        //================================= Staff Category ==============================================

        // get Staff_category 
        // Return Dictionary <int,string>
        // int == Cat_id
        // string == title
        public static Dictionary<byte, string> getStaffCategoryAll()
        {
           Staff cStaff = new Staff();
           Dictionary<byte, string> dicStaffCat = new Dictionary<byte, string>();

            using (SqlConnection cn = new SqlConnection(cStaff.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT cat_id, title FROM tbl_staff_cat", cn);
                
                cn.Open();
                IDataReader reader = cStaff.ExecuteReader(cmd);
                while (reader.Read())
                {
                    dicStaffCat.Add((byte)reader[0], reader[1].ToString());
                }
            }

            return dicStaffCat;
           
        }

        public static Dictionary<byte, string> getCategoryByBlueHouseStaff()
        {
            Staff cStaff = new Staff();
            Dictionary<byte, string> dicStaffCat = new Dictionary<byte, string>();

            using (SqlConnection cn = new SqlConnection(cStaff.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT cat_id, title FROM tbl_staff_cat WHERE cat_id <> 6", cn);

                cn.Open();
                IDataReader reader = cStaff.ExecuteReader(cmd);
                while (reader.Read())
                {
                    dicStaffCat.Add((byte)reader[0], reader[1].ToString());
                }
            }

            return dicStaffCat;
        }

        

        

        public static string getStaffCategoryById(byte intId)
        {
            Staff cStaff = new Staff();
            using (SqlConnection cn = new SqlConnection(cStaff.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT title FROM tbl_staff_cat WHERE cat_id = @cat_id", cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.SmallInt).Value = intId;
                cn.Open();
                IDataReader reader = cStaff.ExecuteReader(cmd);
                if (reader.Read())
                    return reader[0].ToString();
                else
                    return string.Empty;
            }
        }

       

        
       
    }
}