using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using Hotels2thailand.Staffs;

/// <summary>
/// Summary description for SupplierContact
/// </summary>
/// 
namespace Hotels2thailand.Suppliers
{

    public class SupplierContactPhoneEmail : Hotels2BaseClass
    {
        public SupplierContactPhoneEmail()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public Dictionary<int, string> GetStaffContact(short shrSupplierId, string departId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                Dictionary<int, string> dicStaffContact = new Dictionary<int, string>();
                SqlCommand cmd = new SqlCommand("SELECT staff_id,title FROM tbl_supplier_staff_contact WHERE supplier_id=@supplier_id AND department_id IN(" + departId + ")", cn);

                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
               

                cn.Open();

                IDataReader reader = ExecuteReader(cmd);

                while (reader.Read())
                {
                    dicStaffContact.Add((int)reader[0],reader[1].ToString());
                    

                }
                return dicStaffContact;
            }
        }
        public Dictionary<int,string> GetContactPhone(int intStaffId, string CatId)
        {
           
            Dictionary<int, string> dicStaffContact = new Dictionary<int, string>();
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT sscp.phone_id, '(' + sscp.code_country + ')'+ sscp.code_local + '-' + sscp.phone_number");
                query.Append(" FROM tbl_supplier_staff_contact_phone sscp");
                query.Append(" WHERE  sscp.staff_id = @staff_id ");
                query.Append(" AND sscp.status = 1 AND sscp.cat_id IN (" + CatId + ")");

                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@staff_id", SqlDbType.Int).Value = intStaffId;
                cn.Open();

                IDataReader reader = ExecuteReader(cmd);

                while (reader.Read())
                {
                    dicStaffContact.Add((int)reader[0], reader[1].ToString());
                }
            }

            return dicStaffContact;
        }
        public Dictionary<int, string> GetContactEmail(int intStaffId)
        {
            Dictionary<int, string> dicStaffContact = new Dictionary<int, string>();
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT  ssce.email_id, ssce.email");
                query.Append(" FROM tbl_supplier_staff_contact_email ssce");
                query.Append(" WHERE ssce.staff_id = @staff_id");
                query.Append(" AND ssce.status = 1 ");

                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@staff_id", SqlDbType.Int).Value = intStaffId;
                cn.Open();

                IDataReader reader = ExecuteReader(cmd);

                while (reader.Read())
                {
                    dicStaffContact.Add((int)reader[0], reader[1].ToString());

                }
            }

            return  dicStaffContact;
        }
        
        //======================= Phone Fax Email Display Booking ==========================

        public string GetstringContact(short shrSupplierId ,string departId, string CatId)
        {
            string result = string.Empty;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT   '(' + sscp.code_country + ')'+ sscp.code_local + '-' + sscp.phone_number");
                query.Append(" FROM tbl_supplier_staff_contact ssc, tbl_supplier_staff_contact_phone sscp");
                query.Append(" WHERE ssc.supplier_id = @supplier_id AND ssc.staff_id = sscp.staff_id AND ssc.department_id In(" + departId + ") ");
                query.Append(" AND ssc.status = 1 AND sscp.cat_id IN (" + CatId + ")");
                
                SqlCommand cmd = new SqlCommand(query.ToString(),cn);
                cmd.Parameters.Add("@supplier_id",SqlDbType.SmallInt).Value = shrSupplierId;
                cn.Open();
                
                IDataReader reader = ExecuteReader(cmd);
                
                while (reader.Read())
                {
                   result = result + reader[0].ToString() + ", ";

                }

                
            }
            if (string.IsNullOrEmpty(result))
                result = "N/As ";
            return result.Hotels2RightCrl(2);
        }

        public string GetstringContactEmail(short shrSupplierId, string departId)
        {
            string result = string.Empty;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT  ssce.email");
                query.Append(" FROM tbl_supplier_staff_contact ssc, tbl_supplier_staff_contact_email ssce");
                query.Append(" WHERE ssc.supplier_id = @supplier_id AND ssc.staff_id = ssce.staff_id AND ssc.department_id In(" + departId + ") ");
                query.Append(" AND ssc.status = 1 ");

                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@supplier_id", SqlDbType.SmallInt).Value = shrSupplierId;
                cn.Open();

                IDataReader reader = ExecuteReader(cmd);

                while (reader.Read())
                {
                    result = result + reader[0].ToString() + ", ";

                }
            }
            if (string.IsNullOrEmpty(result))
                result = "N/As ";

            return result.Hotels2RightCrl(2);
        }

        //        -- Phone Mobile Res And Sales ssc.department_id In( 3,1)  sscp.cat_id IN ( 2,1)
        //SELECT   '(' + sscp.code_country + ')'+ sscp.code_local + '-' + sscp.phone_number
        //FROM tbl_supplier_staff_contact ssc, tbl_supplier_staff_contact_phone sscp
        //WHERE supplier_id = 3506 AND ssc.staff_id = sscp.staff_id AND ssc.department_id In( 3,1) 
        //AND ssc.status = 1 AND sscp.cat_id IN ( 2,1)

        //-- Fax Res And Sales ssc.department_id In( 3,1)  sscp.cat_id IN ( 3)
        //SELECT  '(' + sscp.code_country + ')'+ sscp.code_local + '-' + sscp.phone_number
        //FROM tbl_supplier_staff_contact ssc, tbl_supplier_staff_contact_phone sscp
        //WHERE supplier_id = 3506 AND ssc.staff_id = sscp.staff_id AND ssc.department_id In( 3,1) 
        //AND ssc.status = 1 AND sscp.cat_id IN ( 3)


        //-- Phone Mobile  Account ssc.department_id In( 2)  sscp.cat_id IN ( 2,1)
        //SELECT  '(' + sscp.code_country + ')'+ sscp.code_local + '-' + sscp.phone_number
        //FROM tbl_supplier_staff_contact ssc, tbl_supplier_staff_contact_phone sscp
        //WHERE supplier_id = 3506 AND ssc.staff_id = sscp.staff_id AND ssc.department_id In( 2) 
        //AND ssc.status = 1 AND sscp.cat_id IN ( 2,1)

        //-- Fax Account ssc.department_id In( 2) sscp.cat_id IN (3)
        //SELECT  '(' + sscp.code_country + ')'+ sscp.code_local + '-' + sscp.phone_number 
        //FROM tbl_supplier_staff_contact ssc, tbl_supplier_staff_contact_phone sscp
        //WHERE supplier_id = 3506 AND ssc.staff_id = sscp.staff_id AND ssc.department_id In( 2) 
        //AND ssc.status = 1 AND sscp.cat_id IN (3)



        //-- Email Account ssc.department_id In( 2) 
        //SELECT  ssce.email
        //FROM tbl_supplier_staff_contact ssc, tbl_supplier_staff_contact_email ssce
        //WHERE supplier_id = 3506 AND ssc.staff_id = ssce.staff_id AND ssc.department_id In( 2) 
        //AND ssc.status = 1 

        //-- Email Res & sales  ssc.department_id In(3,1) 
        //SELECT  ssce.email
        //FROM tbl_supplier_staff_contact ssc, tbl_supplier_staff_contact_email ssce
        //WHERE supplier_id = 3506 AND ssc.staff_id = ssce.staff_id AND ssc.department_id In(3,1) 
        //AND ssc.status = 1 
    }
}