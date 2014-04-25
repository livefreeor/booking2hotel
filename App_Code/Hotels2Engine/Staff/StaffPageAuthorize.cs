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
    public class StaffPageAuthorize : Hotels2BaseClass
    {
        public int PageId { get; set; }
        public string PageFileName { get; set; }
        public byte ModuleId { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateModify { get; set; }
        public bool Status { get; set; }


        public StaffPageAuthorize()
        {
        }

        
        public Dictionary<byte, string> getdicStaffModuleMain()
        {
            Dictionary<byte, string> iDic = new Dictionary<byte, string>();
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT main_module_id,title	FROM tbl_staff_main_module WHERE status = 1", cn);
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    iDic.Add((byte)reader[0], reader[1].ToString());
                }
            }

            return iDic;
        }

        public Dictionary<byte, string> getdicStaffModule(byte bytMainModuleId)
        {
            Dictionary<byte, string> iDic = new Dictionary<byte, string>();
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT module_id,title	FROM tbl_staff_module WHERE status = 1 AND main_module_id=@main_module_id", cn);
                cmd.Parameters.Add("@main_module_id", SqlDbType.TinyInt).Value = bytMainModuleId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    iDic.Add((byte)reader[0], reader[1].ToString());
                }
            }

            return iDic;
        }

       

        public string getStaffModuleFolderName(byte bytModulId)
        {
            Dictionary<byte, string> iDic = new Dictionary<byte, string>();
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT folder_name FROM tbl_staff_module WHERE status = 1 AND module_id= @module_id", cn);
                cmd.Parameters.Add("@module_id", SqlDbType.TinyInt).Value = bytModulId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return reader[0].ToString();
                else
                    return string.Empty;
            }
        }

        public string getStaffMainModuleFolderName(byte bytModulId)
        {
            Dictionary<byte, string> iDic = new Dictionary<byte, string>();
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT folder_name FROM tbl_staff_main_module WHERE status = 1 AND main_module_id= @main_module_id", cn);
                cmd.Parameters.Add("@main_module_id", SqlDbType.TinyInt).Value = bytModulId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return reader[0].ToString();
                else
                    return string.Empty;
            }
        }

        public IList<object> GetListPageAuthorizeByModulId(byte bytModulId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_staff_page WHERE module_id= @module_id", cn);
                cmd.Parameters.Add("@module_id", SqlDbType.TinyInt).Value = bytModulId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public int InsertPageFile(string PageFileName, byte bytModulId, DateTime dDateCreate, DateTime dDateModify)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_staff_page (page_file_name,module_id,date_create,date_modify,status) VALUES (@page_file_name,@module_id,@date_create,@date_modify,@status)", cn);
                cmd.Parameters.Add("@page_file_name", SqlDbType.VarChar).Value = PageFileName;
                cmd.Parameters.Add("@module_id", SqlDbType.TinyInt).Value = bytModulId;
                cmd.Parameters.Add("@date_create", SqlDbType.SmallDateTime).Value = dDateCreate;
                cmd.Parameters.Add("@date_modify", SqlDbType.SmallDateTime).Value = dDateModify;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = true;
                cn.Open();

                int ret = ExecuteNonQuery(cmd);
                return ret;
            }
        }

        public bool UpdateStatus(int intPageId, bool bolStatus)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_staff_page SET status=@status WHERE page_id=@page_id", cn);
                cmd.Parameters.Add("@page_id", SqlDbType.VarChar).Value = intPageId;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
                cn.Open();
                int ret = ExecuteNonQuery(cmd);
                return (ret == 1);
            }
        }

        //=================================================


        public string UpdatePageAuthorize(string pageId, byte bytCatId)
        {
            string result = "false";
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmddel = new SqlCommand("DELETE FROM tbl_staff_page_authorize WHERE cat_id=@cat_id", cn);
                cmddel.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCatId;
                cn.Open();
                ExecuteNonQuery(cmddel);
                string[] arrPage = pageId.Split(',');
                foreach (string Page in arrPage)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO tbl_staff_page_authorize (cat_id,page_id) VALUES(@cat_id,@page_id)", cn);
                    cmd.Parameters.Add("@page_id", SqlDbType.TinyInt).Value = byte.Parse(Page);
                    cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytCatId;
                    ExecuteNonQuery(cmd);
                }

                result = "true";
            }

            return result;
        }

        public ArrayList getListPageByCatId(byte bytcatId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                
                ArrayList IlistPage = new ArrayList();

                SqlCommand cmd = new SqlCommand("SELECT sm.folder_name, p.page_file_name FROM tbl_staff_page_authorize pa, tbl_staff_page p , tbl_staff_module sm WHERE sm.module_id = p.module_id AND p.page_id=pa.page_id AND pa.cat_id=@cat_id ", cn);
                cmd.Parameters.Add("@cat_id", SqlDbType.TinyInt).Value = bytcatId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    IlistPage.Add(reader[0].ToString()+"!"+reader[1].ToString());
                }
                return IlistPage;
            }
        }

        

        
    }
}