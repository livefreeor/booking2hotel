using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.Staffs;
using Hotels2thailand.LinqProvider.Supplier;

/// <summary>
/// Summary description for SupplierContact
/// </summary>
/// 
namespace Hotels2thailand.Suppliers
{

    public class SupplierContact : Hotels2BaseClass
    {
        public SupplierContact()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();

        //===================== DEPARTMENT =======================================

        public  int InsertDepartment(string strTitle)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_supplier_department (title) VALUES (@department_id,@title); SET @department_id = SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = strTitle;
                cmd.Parameters.Add("@department_id", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cn.Open();

                ExecuteNonQuery(cmd);
                ret = (byte)cmd.Parameters["@department_id"].Value;
            }
            //int Insert = dcSupplier.ExecuteCommand("INSERT INTO tbl_supplier_department (department_id,title) VALUES ({0},{1})", depId, strTitle);
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Supplier_contact, StaffLogActionType.Insert, StaffLogSection.Supplier,
                short.Parse(HttpContext.Current.Request.QueryString["supid"]), "tbl_supplier_department", "title", "department_id", ret);
            //========================================================================================================================================================

            return ret;
        }

        public IDictionary<int,string> getdicDepartment()
        {
            var Result= from sd in dcSupplier.tbl_supplier_departments
                        select sd;
            IDictionary<int, string> dicDep = new Dictionary<int,string>();
            
            foreach(var item in Result)
            {
                dicDep.Add(item.department_id,item.title);
            }

            return dicDep;
        }

        public IDictionary<byte, string> getdicDepartmentHaveRecord(short shrSupplierId)
        {
            var Result = from sd in dcSupplier.tbl_supplier_departments
                         select sd;
            IDictionary<byte, string> dicDep = new Dictionary<byte, string>();
            SupplierContact cSupplierContact = new SupplierContact();
            
            foreach (var item in Result)
            {
                if (cSupplierContact.getSupplierStaffContactListByDepId(shrSupplierId,item.department_id).Count > 0)
                {
                    dicDep.Add(item.department_id, item.title);
                }
            }

            return dicDep;
        }


        //===================== STAFF CONTACT ====================================

        public IList<tbl_supplier_staff_contact> getSupplierStaffContactList()
        {
            var Result = from ss in dcSupplier.tbl_supplier_staff_contacts
                         select ss;
            return Result.ToList();
        }

        public IList<tbl_supplier_staff_contact> getSupplierStaffContactListByDepId( short shrSupplierId, byte bytDepId)
        {
            var Result = from ss in dcSupplier.tbl_supplier_staff_contacts
                         where ss.department_id == bytDepId && ss.supplier_id == shrSupplierId
                         orderby ss.status descending
                         select ss;
            return Result.ToList();
        }

        public tbl_supplier_staff_contact getSupplierStaffContactbyId(int intstaffId)
        {
            var Result = dcSupplier.tbl_supplier_staff_contacts.SingleOrDefault(ss=>ss.staff_id == intstaffId);
            if(Result == null)
                return null;
            else
            {
                return Result;
            }
        }

        public static int InsertStaffContact(short shrSupplierId, byte bytDep, string strTitle, string strComment, bool bolStatus)
        {
            //LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();
            //int intInsert = dcSupplier.ExecuteCommand("INSERT INTO tbl_supplier_staff_contact (supplier_id,department_id,title,comment,status)VALUES({0},{1},{2},{3},{4})", shrSupplierId, bytDep, strTitle, strComment, bolStatus);
            //return intInsert;

            short ret = 0;
            SupplierContact cSupContact = new SupplierContact();
            using (SqlConnection cn = new SqlConnection(cSupContact.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_supplier_staff_contact (supplier_id,department_id,title,comment,status)VALUES(@supplier_id,@department_id,@title,@comment,@status); SET @staff_id = SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@supplier_id", SqlDbType.VarChar).Value = shrSupplierId;
                cmd.Parameters.Add("@department_id", SqlDbType.VarChar).Value = bytDep;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = strTitle;
                cmd.Parameters.Add("@comment", SqlDbType.VarChar).Value = strComment;
                cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = bolStatus;
                cmd.Parameters.Add("@staff_id", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                cn.Open();

                cSupContact.ExecuteNonQuery(cmd);
                ret = (short)cmd.Parameters["@staff_id"].Value;
            }
            //int Insert = dcSupplier.ExecuteCommand("INSERT INTO tbl_supplier_department (department_id,title) VALUES ({0},{1})", depId, strTitle);
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Supplier_contact, StaffLogActionType.Insert, StaffLogSection.Supplier,
                shrSupplierId, "tbl_supplier_staff_contact", "supplier_id,department_id,title,comment,status", "staff_id", ret);
            //========================================================================================================================================================

            return ret;
        }

        public static bool UpdateStaffContact(int intStaffId, byte bytDep, string strTitle, string strComment, bool bolStatus)
        {
            //LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();
            //int intUpdate = dcSupplier.ExecuteCommand("UPDATE tbl_supplier_staff_contact SET department_id={0},title={1},comment={2},status={3} WHERE staff_id={4}", bytDep, strTitle, strComment, bolStatus, intStaffId);

            //return (intUpdate == 1);
            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_supplier_staff_contact", "department_id,title,comment,status", "staff_id", intStaffId);
            //============================================================================================================================
            int ret = 0;
            SupplierContact cSupContact = new SupplierContact();
            using (SqlConnection cn = new SqlConnection(cSupContact.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_supplier_staff_contact SET department_id=@department_id,title=@title,comment=@comment,status=@status WHERE staff_id=@staff_id", cn);
                
                cmd.Parameters.Add("@department_id", SqlDbType.TinyInt).Value = bytDep;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = strTitle;
                cmd.Parameters.Add("@comment", SqlDbType.VarChar).Value = strComment;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
                cmd.Parameters.Add("@staff_id", SqlDbType.Int).Value = intStaffId;
                cn.Open();

               ret =  cSupContact.ExecuteNonQuery(cmd);
                
            }
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Supplier_contact, StaffLogActionType.Update, StaffLogSection.Supplier, short.Parse(HttpContext.Current.Request.QueryString["supid"]),
                "tbl_supplier_staff_contact", "department_id,title,comment,status", arroldValue, "staff_id", intStaffId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }

        public  static bool UpdateStaffContactStatus(int intstaffId, bool bolStatus)
        {
            //LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();
            //int intUpdate = dcSupplier.ExecuteCommand("UPDATE tbl_supplier_staff_contact SET status={0} WHERE intStaffId={1}", bolStatus, intstaffId);
            //return (intUpdate == 1);

            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_supplier_staff_contact", "status", "staff_id", intstaffId);
            //============================================================================================================================
            int ret = 0;
            SupplierContact cSupContact = new SupplierContact();
            using (SqlConnection cn = new SqlConnection(cSupContact.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_supplier_staff_contact SET status=@status WHERE staff_id=@staff_id)", cn);

                
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
                cmd.Parameters.Add("@staff_id", SqlDbType.Int).Value = intstaffId;
                cn.Open();

                ret = cSupContact.ExecuteNonQuery(cmd);

            }
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Supplier_contact, StaffLogActionType.Update, StaffLogSection.Supplier, short.Parse(HttpContext.Current.Request.QueryString["supid"]),
                "tbl_supplier_staff_contact", "status", arroldValue, "staff_id", intstaffId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }

        //===================== STAFF CONTACT PHONE ==============================

        public tbl_supplier_staff_contact_phone GetStaffPhonebyId(int intPhoneId)
        {
            var Result = dcSupplier.tbl_supplier_staff_contact_phones.SingleOrDefault(sp => sp.phone_id == intPhoneId);
            if (Result == null)
                return null;
            else
            {
                return Result;
            }
        }

        public static IList<tbl_supplier_staff_contact_phone> GetListPhoneListByStaffContactId(int intStaffId)
        {
            LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();
            var Result = from sph in dcSupplier.tbl_supplier_staff_contact_phones
                         where sph.staff_id == intStaffId
                         orderby sph.status descending
                         select sph;
            return Result.ToList();
        }

        public static int InsertStaffPhone(byte bytCatId, int intstaffId, string strCodeCountry, string strCodeLocal, string strPhoneNum , bool bolStatus)
        {
            //LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();
            //int intInsert = dcSupplier.ExecuteCommand("INSERT INTO tbl_supplier_staff_contact_phone (cat_id,staff_id,code_country,code_local,phone_number,status)VALUES({0},{1},{2},{3},{4},{5})", bytCatId
            //   , intstaffId, strCodeCountry, strCodeLocal, strPhoneNum, bolStatus);
            //return intInsert;

            int ret = 0;
            SupplierContact cSupContact = new SupplierContact();
            using (SqlConnection cn = new SqlConnection(cSupContact.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_supplier_staff_contact_phone (cat_id,staff_id,code_country,code_local,phone_number,status)VALUES(@cat_id,@staff_id,@code_country,@code_local,@phone_number,@status); SET @phone_id = SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCatId;
                cmd.Parameters.Add("@staff_id", SqlDbType.VarChar).Value = intstaffId;
                cmd.Parameters.Add("@code_country", SqlDbType.VarChar).Value = strCodeCountry;
                cmd.Parameters.Add("@code_local", SqlDbType.VarChar).Value = strCodeLocal;
                cmd.Parameters.Add("@phone_number", SqlDbType.VarChar).Value = strPhoneNum;
                cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = bolStatus;
                cmd.Parameters.Add("@phone_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();

                cSupContact.ExecuteNonQuery(cmd);
                ret = (int)cmd.Parameters["@phone_id"].Value;
            }
            //int Insert = dcSupplier.ExecuteCommand("INSERT INTO tbl_supplier_department (department_id,title) VALUES ({0},{1})", depId, strTitle);
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Supplier_contact, StaffLogActionType.Insert, StaffLogSection.Supplier,
                short.Parse(HttpContext.Current.Request.QueryString["supid"]), "tbl_supplier_staff_contact_phone", "cat_id,staff_id,code_country,code_local,phone_number,status", "phone_id", ret);
            //========================================================================================================================================================

            return ret;
        }

        public static bool UpdateStaffPhone(int intPhoneId, byte bytCatId, string strCodeCountry, string strCodeLocal, string strPhoneNum)
        {
            //LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();
            //int intUpdate = dcSupplier.ExecuteCommand("UPDATE tbl_supplier_staff_contact_phone SET cat_id={0},code_country={1},code_local={2},phone_number={3} WHERE phone_id={4}", bytCatId,
            //    strCodeCountry, strCodeLocal, strPhoneNum, intPhoneId);

            //return (intUpdate == 1);

            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_supplier_staff_contact_phone", "cat_id,staff_id,code_country,code_local,phone_number", "phone_id", intPhoneId);
            //============================================================================================================================
            int ret = 0;
            SupplierContact cSupContact = new SupplierContact();
            using (SqlConnection cn = new SqlConnection(cSupContact.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_supplier_staff_contact_phone SET cat_id=@cat_id,code_country=@code_country,code_local=@code_local,phone_number=@phone_number WHERE phone_id=@phone_id", cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCatId;
                cmd.Parameters.Add("@code_country", SqlDbType.VarChar).Value = strCodeCountry;
                cmd.Parameters.Add("@code_local", SqlDbType.VarChar).Value = strCodeLocal;
                cmd.Parameters.Add("@phone_number", SqlDbType.VarChar).Value = strPhoneNum;
                cmd.Parameters.Add("@phone_id", SqlDbType.Int).Value = intPhoneId;
                cn.Open();

                ret = cSupContact.ExecuteNonQuery(cmd);

            }
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Supplier_contact, StaffLogActionType.Update, StaffLogSection.Supplier, short.Parse(HttpContext.Current.Request.QueryString["supid"]),
                "tbl_supplier_staff_contact_phone", "cat_id,staff_id,code_country,code_local,phone_number", arroldValue, "phone_id", intPhoneId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }
        public static bool UpdateStaffPhoneStatus(int intPhoneId, bool bolStatus)
        {
            //LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();
            //int intUpdate = dcSupplier.ExecuteCommand("UPDATE tbl_supplier_staff_contact_phone SET status={0} WHERE phone_id={1}", bolStatus, intPhoneId);
               

            //return (intUpdate == 1);

            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_supplier_staff_contact_phone", "status", "phone_id", intPhoneId);
            //============================================================================================================================
            int ret = 0;
            SupplierContact cSupContact = new SupplierContact();
            using (SqlConnection cn = new SqlConnection(cSupContact.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_supplier_staff_contact_phone SET status=@status WHERE phone_id=@phone_id", cn);
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;

                cmd.Parameters.Add("@phone_id", SqlDbType.Int).Value = intPhoneId;
                cn.Open();

                ret = cSupContact.ExecuteNonQuery(cmd);

            }
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Supplier_contact, StaffLogActionType.Update, StaffLogSection.Supplier, short.Parse(HttpContext.Current.Request.QueryString["supid"]),
                "tbl_supplier_staff_contact_phone", "status", arroldValue, "phone_id", intPhoneId);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }

        public static bool DelStaffContactPhone(int intPhoneId)
        {
            IList<object[]> arroldValue = null;


            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            arroldValue = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_supplier_staff_contact_phone", "phone_id", intPhoneId);
            //============================================================================================================================
            LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();
            int intUpdate = dcSupplier.ExecuteCommand("DELETE FROM tbl_supplier_staff_contact_phone WHERE phone_id={0}",intPhoneId);

            //#Staff_Activity_Log================================================================================================ STEP 2 ==
            StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Supplier_contact, StaffLogActionType.Delete, StaffLogSection.Supplier, short.Parse(HttpContext.Current.Request.QueryString["supid"]),
                "tbl_supplier_staff_contact_phone", arroldValue, "phone_id", intPhoneId);
            //============================================================================================================================
            return (intUpdate == 1);
        }

        //===================== STAFF CONTACT EMAIL===============================
        public static IList<tbl_supplier_staff_contact_email> GetListEmailByStaffContactId(int intStaffId)
        {
            LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();
            var Result = from spe in dcSupplier.tbl_supplier_staff_contact_emails
                         where spe.staff_id == intStaffId
                         orderby spe.status descending
                         select spe;
            return Result.ToList();
        }

        public tbl_supplier_staff_contact_email GetStaffEmailById(int EmailId)
        {
            var Result = dcSupplier.tbl_supplier_staff_contact_emails.SingleOrDefault(se => se.email_id == EmailId);
            if (Result == null)
                return null;
            else
            {
                return Result;
            }
        }

        public static int InsertStaffEmail(int intstaffId, string strEmail, bool bolStatus)
        {
            //LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();
            //int intInsert = dcSupplier.ExecuteCommand("INSERT INTO tbl_supplier_staff_contact_email (staff_id,email,status)VALUES({0},{1},{2})", intstaffId, intEmail, bolStatus);
            //return intInsert;

            int ret = 0;
            SupplierContact cSupContact = new SupplierContact();
            using (SqlConnection cn = new SqlConnection(cSupContact.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_supplier_staff_contact_email (staff_id,email,status)VALUES(@staff_id,@email,@status); SET @email_id = SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@staff_id", SqlDbType.Int).Value = intstaffId;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = strEmail;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
                
                cmd.Parameters.Add("@email_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();

                cSupContact.ExecuteNonQuery(cmd);
                ret = (int)cmd.Parameters["@email_id"].Value;
            }
            //int Insert = dcSupplier.ExecuteCommand("INSERT INTO tbl_supplier_department (department_id,title) VALUES ({0},{1})", depId, strTitle);
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Supplier_contact, StaffLogActionType.Insert, StaffLogSection.Supplier,
                short.Parse(HttpContext.Current.Request.QueryString["supid"]), "tbl_supplier_staff_contact_email", "staff_id,email,status", "email_id", ret);
            //========================================================================================================================================================

            return ret;
        }

        public static bool UpdateStaffEmail(int intEmail, string strEmail)
        {
            //LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();
            //int intUpdate = dcSupplier.ExecuteCommand("UPDATE tbl_supplier_staff_contact_email SET email={0} WHERE email_id={1}", strEmail, intEmail);

            //return (intUpdate == 1);

            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_supplier_staff_contact_email", "email", "email_id", intEmail);
            //============================================================================================================================
            int ret = 0;
            SupplierContact cSupContact = new SupplierContact();
            using (SqlConnection cn = new SqlConnection(cSupContact.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_supplier_staff_contact_email SET email=@email WHERE email_id=@email_id", cn);
                cmd.Parameters.Add("@email_id", SqlDbType.Int).Value = intEmail;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = strEmail;
                
                cn.Open();

                ret = cSupContact.ExecuteNonQuery(cmd);

            }
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Supplier_contact, StaffLogActionType.Update, StaffLogSection.Supplier, short.Parse(HttpContext.Current.Request.QueryString["supid"]),
                "tbl_supplier_staff_contact_email", "email", arroldValue, "email_id", intEmail);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }

        public static bool UpdateStaffEmailStatus(int intEmail, bool bolStatus)
        {
            //LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();
            //int intUpdate = dcSupplier.ExecuteCommand("UPDATE tbl_supplier_staff_contact_email SET status={0} WHERE email_id={1}", bolStatus, intEmail);

            //return (intUpdate == 1);

            ArrayList arroldValue = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_supplier_staff_contact_email", "email", "email_id", intEmail);
            //============================================================================================================================
            int ret = 0;
            SupplierContact cSupContact = new SupplierContact();
            using (SqlConnection cn = new SqlConnection(cSupContact.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_supplier_staff_contact_email SET status=@status WHERE email_id=@email_id", cn);
                cmd.Parameters.Add("@email_id", SqlDbType.Int).Value = intEmail;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;

                cn.Open();

                ret = cSupContact.ExecuteNonQuery(cmd);

            }
            //#Staff_Activity_Log================================================================================================ STEP 2 ============
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Supplier_contact, StaffLogActionType.Update, StaffLogSection.Supplier, short.Parse(HttpContext.Current.Request.QueryString["supid"]),
                "tbl_supplier_staff_contact_email", "status", arroldValue, "email_id", intEmail);
            //==================================================================================================================== COMPLETED ========
            return (ret == 1);
        }

        public static bool DelStaffEmail(int intEmail)
        {
            //LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();
            //int intUpdate = dcSupplier.ExecuteCommand("DELETE FROM tbl_supplier_staff_contact_email WHERE email_id={0}", intEmail);

            //return (intUpdate == 1);

            IList<object[]> arroldValue = null;


            //#Staff_Activity_Log================================================================================================ STEP 1 ==
            arroldValue = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_supplier_staff_contact_email", "email_id", intEmail);
            //============================================================================================================================
            LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();
            int intUpdate = dcSupplier.ExecuteCommand("DELETE FROM tbl_supplier_staff_contact_email WHERE email_id={0}", intEmail);

            //#Staff_Activity_Log================================================================================================ STEP 2 ==
            StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Supplier_contact, StaffLogActionType.Delete, StaffLogSection.Supplier, short.Parse(HttpContext.Current.Request.QueryString["supid"]),
                "tbl_supplier_staff_contact_email", arroldValue, "email_id", intEmail);
            //============================================================================================================================
            return (intUpdate == 1);
        }

        //===================== STAFF PHONE CATEGORY ===============================

        public static IDictionary<int, string> getPhoneCat()
        {
            LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();
            IDictionary<int, string> IdicPhonecat = new Dictionary<int, string>();

            var Result = from pc in dcSupplier.tbl_phone_cats
                         select pc;

            foreach (var Item in Result)
            {
                IdicPhonecat.Add(Item.cat_id, Item.title);
            }

            return IdicPhonecat;
        }
        public static string GetCatTitlebyCatId(byte bytPhoneCatId )
        {
            LinqSupplierDataContext dcSupplier = new LinqSupplierDataContext();

            var Result = dcSupplier.tbl_phone_cats.SingleOrDefault(pc => pc.cat_id == bytPhoneCatId);
            if (Result == null)
                return null;
            else
            {
                return Result.title;
            }
        }

    }
}