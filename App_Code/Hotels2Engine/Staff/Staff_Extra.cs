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
    public partial class StaffExtra : Hotels2BaseClass
    {
        public short Staff_Id { get; set; }
        public byte Cat_Id { get; set; }
       
        public string Title { get; set; }
        public string UserName { get; set; }

        private string _passwod = string.Empty;

        public string PassWord
        {
            get { return _passwod; }

            set { _passwod = value; }
        }

        public DateTime LastAccess { get; set; }


        public bool Status { get; set; }
        public string Email { get; set; }

        public byte AuthorizeId { get; set; }
        public string AuthorizeTitle { get; set; }
        //public byte MethodId { get; set; }
        //public string MethodTitle { get; set; }

        //public short SupplierId { get; set; }
       

        public int CheckStaffUser(string stringUser, short shrSupplierId)
        {
            int IsHaveStaff = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(sf.staff_id) FROM tbl_staff sf , tbl_supplier s WHERE sf.supplier_id = @supplier_id AND sf.supplier_id = s.supplier_id AND sf.user_name=@user_name", cn);
                cmd.Parameters.Add("@user_name", SqlDbType.VarChar).Value = stringUser;
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cn.Open();
                IsHaveStaff = (int)ExecuteScalar(cmd);
            }

            return IsHaveStaff;
        }

        public StaffExtra GetStaffExtraByStaffID(short StaffID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT st.staff_id,st.cat_id,st.title,st.user_name,st.password,st.last_access,st.status,st.email ,");
                query.Append(" ast.authorize_id,  ast.title AS titleauthorize ");
                query.Append(" FROM tbl_staff st, tbl_staff_product stp, tbl_authorize_staff ast , tbl_staff_authorize sta");
                query.Append(" WHERE st.staff_id = stp.staff_id  AND sta.staff_id = st.staff_id AND sta.authorize_id = ast.authorize_id ");
                query.Append(" AND st.staff_id =@staff_id");

                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@staff_id",SqlDbType.SmallInt).Value = StaffID;
                cn.Open();
                IDataReader reader =  ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (StaffExtra)MappingObjectFromDataReader(reader);
                else
                    return null;
            }
        }

        public StaffExtra getStaffbyEmailAndUserName(string UserName, string email)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT st.staff_id,st.cat_id,st.title,st.user_name,st.password,st.last_access,st.status,st.email ,");
                query.Append(" ast.authorize_id,  ast.title AS titleauthorize");
                query.Append(" FROM tbl_staff st, tbl_staff_product stp, tbl_authorize_staff ast , tbl_staff_authorize sta");
                query.Append(" WHERE st.staff_id = stp.staff_id  AND sta.staff_id = st.staff_id AND sta.authorize_id = ast.authorize_id ");
                
                query.Append(" AND st.user_name =@user_name AND st.email=@email");
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@user_name", SqlDbType.VarChar).Value = UserName;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (StaffExtra)MappingObjectFromDataReader(reader);
                else
                    return null;
            }
        }
        

        public  ArrayList supplierCheckAlreadyExtra(short shrSupplierID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                ArrayList StaffList = new ArrayList();
                StringBuilder query = new StringBuilder();
                query.Append("SELECT st.staff_id");
                query.Append(" FROM tbl_staff st,  tbl_staff_authorize sa, tbl_staff_product sp , tbl_product p");
                query.Append(" WHERE st.staff_id = sa.staff_id AND sp.staff_id = st.staff_id AND p.product_id = sp.product_id");
                query.Append(" AND sa.authorize_id = 1 AND p.supplier_price = @supplier_price AND p.Isextranet= 1");
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@supplier_price", SqlDbType.SmallInt).Value = shrSupplierID;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    StaffList.Add((short)reader[0]);
                }
                return StaffList;

            }
        }

        public List<object> GetStaffListExtraByStaffID(short StaffID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {

                StringBuilder query = new StringBuilder();
                query.Append("SELECT DISTINCT(st.staff_id),st.cat_id,st.title,st.user_name,st.password,st.last_access,st.status,st.email ,");
                query.Append(" ast.authorize_id,  ast.title AS titleauthorize");
                query.Append(" FROM tbl_staff st, tbl_staff_product stp, tbl_authorize_staff ast , tbl_staff_authorize sta");
                query.Append(" WHERE st.staff_id = stp.staff_id  AND sta.staff_id = st.staff_id AND sta.authorize_id = ast.authorize_id ");
               
                query.Append(" AND stp.product_id IN (SELECT product_id FROM tbl_staff_product WHERE staff_id = @staff_id )");
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@staff_id", SqlDbType.SmallInt).Value = StaffID;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public short GetStaffAdmin(int intChainId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {

                StringBuilder query = new StringBuilder();
                query.Append("SELECT sp.staff_id FROM tbl_staff_product sp , tbl_staff_authorize sa WHERE sa.staff_id = sp.staff_id AND product_id = (SELECT TOP 1 product_id  FROM tbl_product_chain WHERE chain_id  = " + intChainId + " AND status = 1) AND sa.authorize_id = 1 ");

                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (short)reader[0];
                else
                    return 0;
            }
        }
        public List<object> GetStaffListExtraByProductID(int ProductID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {

                StringBuilder query = new StringBuilder();
                query.Append("SELECT st.staff_id,st.cat_id,st.title,st.user_name,st.password,st.last_access,st.status,st.email ,");
                query.Append(" ast.authorize_id,  ast.title AS titleauthorize");
                query.Append(" FROM tbl_staff st, tbl_staff_product stp, tbl_authorize_staff ast, tbl_staff_authorize sta");
                query.Append(" WHERE st.staff_id = stp.staff_id  AND sta.staff_id = st.staff_id AND sta.authorize_id = ast.authorize_id ");
                
                query.Append(" AND stp.product_id = @product_id");
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@product_id", SqlDbType.Int).Value = ProductID;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
        
        /// <summary>
        /// insert new staff Extranet From Admin USER(partner staff)
        /// </summary>
        /// <param name="SupplierId"></param>
        /// <param name="name"></param>
        /// <param name="Username"></param>
        /// <param name="PassWord"></param>
        /// <returns></returns>
        public short InSertNewStaff_Extranet(string name, string Username, string PassWord, string Email)
        {
            int ret = 0;
            short staff_id = 0;
            //StaffSessionAuthorize cStaffAuth = new StaffSessionAuthorize();
            // byte staffCat =  cStaffAuth.CurrentClassStaff.Cat_Id;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_staff (cat_id,title,user_name,password,last_access,status,email) VALUES(@cat_id,@title,@user_name,@password,@last_access,@status,@email); SET @staff_id = SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = 6;
                //cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = SupplierId;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = name;
                cmd.Parameters.Add("@user_name", SqlDbType.VarChar).Value = Username;
                cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = PassWord.Hotels2MD5EncryptedData();
                cmd.Parameters.Add("@last_access", SqlDbType.SmallDateTime).Value = DateTime.Now.Hotels2ThaiDateTime();
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = true;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = Email;
                cmd.Parameters.Add("@staff_id", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cn.Open();


                ret = ExecuteNonQuery(cmd);
                staff_id = (short)cmd.Parameters["@staff_id"].Value;

            }
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Staff, StaffLogActionType.Insert, StaffLogSection.NULL,
                null, "tbl_staff", "cat_id,title,user_name,password,last_access,status,email", "staff_id", staff_id);
            //========================================================================================================================================================
            return staff_id;
        }

        public bool UpdateStaffExtra(short shrStaffId, string strName , string strUsername, string strEmail, bool bolStatus)
        {
            int ret = 0;
            //=== STAFF ACTIVITY =====================================================================================================================================
            ArrayList objOldValue =  StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_staff", "title,user_name,email,status", "staff_id", shrStaffId);
            //========================================================================================================================================================
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_staff SET title=@title, user_name=@user_name, email=@email, status=@status WHERE staff_id =@staff_id", cn);
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = strName;
                cmd.Parameters.Add("@user_name", SqlDbType.VarChar).Value = strUsername;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = strEmail;
                cmd.Parameters.Add("@staff_id", SqlDbType.SmallInt).Value = shrStaffId;
                cn.Open();
                ret = ExecuteNonQuery(cmd);

            }

            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Staff, StaffLogActionType.Update, StaffLogSection.NULL, null,
                "tbl_staff", "title,user_name,email,status", objOldValue, "staff_id", shrStaffId);
            //========================================================================================================================================================

            return (ret == 1);
        }
    }
}