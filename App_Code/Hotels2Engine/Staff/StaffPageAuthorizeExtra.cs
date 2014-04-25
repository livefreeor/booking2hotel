using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using Hotels2thailand;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;




/// <summary>
/// Summary description for StaffSession
/// </summary>
/// 
namespace Hotels2thailand.Staffs
{
    public class StaffPageAuthorizeExtra : Hotels2BaseClass
    {
        public short StaffId { get; set; }
        public byte ModuleID { get; set; }
        public string ModuleTitle { get; set; }
        public string ModuleFolderName { get; set; }
        public byte MethodId { get; set; }
        public string MethodTitle { get; set; }
        public bool ModuleStatus { get; set; }

        public List<object> GetModuleByStaffID(short  shrStaff_id)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT sm.staff_id, sm.module_id, sme.title, sme.folder_name,");
                query.Append(" sm.method_id, sam.title, sme.status");
                query.Append(" FROM tbl_staff_module_authorize_extranet sm , tbl_staff_module_extra sme,");
                query.Append(" tbl_staff_authorize_method sam");
                query.Append(" WHERE  sm.module_id = sme.module_id AND sam.method_id = sm.method_id");
                query.Append(" AND sm.staff_id = @staff_id");

                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@staff_id", SqlDbType.SmallInt).Value = shrStaff_id;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }


        public int intsertStaffModule(short shrStaff_id, byte bytModule_id, byte bytMethod_id)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_staff_module_authorize_extranet (staff_id,module_id,method_id) VALUES(@staff_id,@module_id,@method_id)", cn);
                cmd.Parameters.Add("@staff_id", SqlDbType.SmallInt).Value = shrStaff_id;
                cmd.Parameters.Add("@module_id", SqlDbType.TinyInt).Value = bytModule_id;
                cmd.Parameters.Add("@method_id", SqlDbType.TinyInt).Value = bytMethod_id;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }

            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Staff, StaffLogActionType.Insert, StaffLogSection.NULL,
                null, "tbl_staff_module_authorize_extranet", "staff_id,module_id,method_id", "staff_id,module_id,method_id", shrStaff_id, bytModule_id, bytMethod_id);
            //========================================================================================================================================================
            return ret;
        }


        public bool UpdateStaffModule(short shrStaff_id, byte bytModule_id, byte Method_id_old, byte bytMethod_id)
        {
            //=== STAFF ACTIVITY =====================================================================================================================================
            ArrayList arrOld_Value = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_staff_module_authorize_extranet", "module_id,method_id", "staff_id,module_id,method_id", shrStaff_id, bytModule_id, Method_id_old);
            //========================================================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_staff_module_authorize_extranet SET module_id=@module_id,method_id=@method_id WHERE staff_id=@staff_id AND module_id=@module_id AND method_id=@method_old_id", cn);
                cmd.Parameters.Add("@staff_id", SqlDbType.SmallInt).Value = shrStaff_id;
                cmd.Parameters.Add("@module_id", SqlDbType.TinyInt).Value = bytModule_id;
                cmd.Parameters.Add("@method_id", SqlDbType.TinyInt).Value = bytMethod_id;
                cmd.Parameters.Add("@method_old_id", SqlDbType.TinyInt).Value = Method_id_old;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Staff, StaffLogActionType.Update, StaffLogSection.NULL, null, "tbl_staff_module_authorize_extranet", "module_id,method_id", arrOld_Value, "staff_id,module_id,method_id", shrStaff_id, bytModule_id, bytMethod_id);
            //========================================================================================================================================================

           return (ret == 1);
        }


        public bool DeleteStaffModule(short shrStaff_id, byte bytModule_id, byte bytMethod_id)
        {

            //=== STAFF ACTIVITY =====================================================================================================================================
            IList<object[]> IlistOld_Value = StaffActivity.ActionDeleteMethodStaff_log_FirstStep("tbl_staff_module_authorize_extranet", "staff_id,module_id,method_id",  shrStaff_id, bytModule_id, bytMethod_id);
            //========================================================================================================================================================
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tbl_staff_module_authorize_extranet WHERE staff_id=@staff_id AND module_id=@module_id AND method_id=@method_id", cn);
                cmd.Parameters.Add("@staff_id", SqlDbType.SmallInt).Value = shrStaff_id;
                cmd.Parameters.Add("@module_id", SqlDbType.TinyInt).Value = bytModule_id;
                cmd.Parameters.Add("@method_id", SqlDbType.TinyInt).Value = bytMethod_id;
                cn.Open();

                ret = ExecuteNonQuery(cmd);
            }

            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionDeleteMethodStaff_log_LastStep(StaffLogModule.Staff, StaffLogActionType.Delete, StaffLogSection.NULL, null,
                "tbl_staff_module_authorize_extranet", IlistOld_Value, "staff_id,module_id,method_id", shrStaff_id, bytModule_id, bytMethod_id);
            //========================================================================================================================================================

            return (ret == 1);
        }
    }
}