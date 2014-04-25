using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Staffs;
using Hotels2thailand;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using System.Text;
using System.Text.RegularExpressions;




/// <summary>
/// Summary description for StaffSession
/// </summary>
/// 
namespace Hotels2thailand.Staffs
{
    public class StaffPageAuthorizeResult : Hotels2BaseClass
    {
        public int PageId { get; set; }
        public string PageFileName { get; set; }
        public byte ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string ModuleFolderName { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateModify { get; set; }
        public bool Status { get; set; }

        public int staffCAtIsAuthorize { get; set; }
        public StaffPageAuthorizeResult()
        {

        }


        public List<object> GetStaffPageResult(bool Status, byte bytModulId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT sp.page_id,sp.page_file_name,sp.module_id, sm.title, sm.folder_name, sp.date_create, sp.date_modify, sp.status");
                query.Append(" FROM tbl_staff_page sp, tbl_staff_module sm");
                query.Append(" WHERE sm.module_id = sp.module_id AND sp.status = @status AND sp.module_id= @module_id");
               
                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@module_id", SqlDbType.TinyInt).Value = bytModulId;
                cmd.Parameters.Add("@status",SqlDbType.Bit).Value =Status ;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public List<object> GetStaffPageResult_Action(bool Status, byte bytModulId, byte bytstaff_catId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT sp.page_id,sp.page_file_name,sp.module_id, sm.title, sm.folder_name, sp.date_create, sp.date_modify, sp.status");
                query.Append(",(SELECT COUNT(spa.cat_id) FROM tbl_staff_page_authorize spa WHERE spa.page_id = sp.page_id AND spa.cat_id = @cat_id)");
                query.Append(" FROM tbl_staff_page sp, tbl_staff_module sm");
                query.Append(" WHERE sm.module_id = sp.module_id AND sp.status = @status AND sp.module_id= @module_id");

                SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                cmd.Parameters.Add("@module_id", SqlDbType.TinyInt).Value = bytModulId;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = Status;
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytstaff_catId;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }
    }
}