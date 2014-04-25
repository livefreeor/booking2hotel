using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Staff;

/// <summary>
/// Summary description for StaffAuthorize 
/// inlcuded tbl_authorize_staff and tbl_staff_authorize From LinqStaffDataContext
/// </summary>

namespace Hotels2thailand.Staffs
{

    public class StaffAuthorizeExtra : Hotels2BaseClass
    {
        public short StaffId { get; set; }
        public byte AuthorizeId { get; set; }
        public string Title { get; set; }


        public int insertStaffAuthorize(short shrStaff_id, byte bytAuthorizeId)
        {
            int ret = 0;
            using(SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_staff_authorize (staff_id,authorize_id) VALUES(@staff_id,@authorize_id)",cn);
                cmd.Parameters.Add("@staff_id",SqlDbType.SmallInt).Value = shrStaff_id;
                cmd.Parameters.Add("@authorize_id", SqlDbType.TinyInt).Value = bytAuthorizeId;
                
                cn.Open();
                ret = ExecuteNonQuery(cmd);


            }
            //=== STAFF ACTIVITY =====================================================================================================================================
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Staff, StaffLogActionType.Insert, StaffLogSection.NULL,
                null, "tbl_staff_authorize", "staff_id,authorize_id", "staff_id,authorize_id", shrStaff_id,bytAuthorizeId);
            //========================================================================================================================================================

            
            return ret;

        }

        public StaffAuthorizeExtra GetStaffAuthorize(short shrStaff_id)
        {
           
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT sfa.staff_id, sfa.authorize_id, asf.title FROM tbl_staff_authorize sfa, tbl_authorize_staff asf WHERE sfa.authorize_id=asf.authorize_id AND sfa.staff_id=@staff_id", cn);
                cmd.Parameters.Add("@staff_id", SqlDbType.SmallInt).Value = shrStaff_id;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (StaffAuthorizeExtra)MappingObjectFromDataReader(reader);
                else
                    return null;
            }
        }
        
        


        
    }
}