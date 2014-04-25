using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Collections;
using Hotels2thailand.Staffs;
using Hotels2thailand.LinqProvider.Production;
/// <summary>
/// Summary description for Product
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class Location:Hotels2BaseClass
    {
        private LinqProductionDataContext dcProduct = new LinqProductionDataContext();
        public short LocationID { get; set; }
        public short DestinationID { get; set; }
        public bool Status { get; set; }
        public string Title { get; set; }
        public string FolderName { get; set; }

        //prop Optional
        public string TitleEng { get; set; }
        public string TitleTHai { get; set; }
        public string FileNameENG { get; set; }
        public string FileNameTHAI { get; set; }


        public Dictionary<string, string> GetLocationAll()
        {
            Dictionary<string, string> dataList = new Dictionary<string, string>();
            var result = from c in dcProduct.tbl_locations
                         orderby c.title
                         select c;

            foreach (var item in result)
            {
                dataList.Add(item.location_id.ToString(), item.title.ToString());
            }
            return dataList;
        }

        public List<object> getLocationByDetinationOptional(short shrDesId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT location_id,destination_id,status,title,folder_location");
            query.Append(",(SELECT locn.title FROM tbl_location_name locn WHERE locn.location_id=loc.location_id AND locn.lang_id = 1) AS titleENG");
            query.Append(",(SELECT locn.title FROM tbl_location_name locn WHERE locn.location_id=loc.location_id AND locn.lang_id = 2) AS titleTHAI");
            query.Append(",(SELECT locn.file_name FROM tbl_location_name locn WHERE locn.location_id=loc.location_id AND locn.lang_id = 1) AS filenameENG");
            query.Append(",(SELECT locn.file_name FROM tbl_location_name locn WHERE locn.location_id=loc.location_id AND locn.lang_id = 2) AS filenameTHAI");
            query.Append(" FROM tbl_location loc WHERE loc.destination_id=@destination_id ORDER BY loc.title");
            query.Append("");
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query.ToString(),cn);
                cmd.Parameters.Add("@destination_id", SqlDbType.SmallInt).Value = shrDesId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public bool UpdateLocation(short shrLocationId, string strtitle, string strFolderName)
        {
            int ret = 0;

            ArrayList arrOldVal = StaffActivity.ActionUpdateMethodStaff_log_FirstStep("tbl_location", "title,folder_location", "location_id", shrLocationId);


            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_location SET title=@title , folder_location=@folder_location WHERE location_id=@location_id", cn);
                cmd.Parameters.Add("@location_id", SqlDbType.SmallInt).Value = shrLocationId;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = strtitle;
                cmd.Parameters.Add("@folder_location", SqlDbType.VarChar).Value = strFolderName;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }


            StaffActivity.ActionUpdateMethodStaff_log_Laststep(StaffLogModule.Central_Data_location, StaffLogActionType.Update, StaffLogSection.NULL, null,
                "tbl_location", "title,folder_location", arrOldVal, "location_id", shrLocationId);

            return (ret == 1);
        }

        public short InsertLocation(short shrDesId, bool bolStatus, string strTitle, string strfolder)
        {
            int ret = 0;
            short intLocationId = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_location (destination_id,status,title,folder_location) VALUES (@destination_id,@status,@title,@folder_location);SET @location_id = SCOPE_IDENTITY();", cn);
                cmd.Parameters.Add("@destination_id", SqlDbType.SmallInt).Value = shrDesId;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = bolStatus;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = strTitle;
                cmd.Parameters.Add("@folder_location", SqlDbType.VarChar).Value = strfolder;
                cmd.Parameters.Add("@location_id", SqlDbType.SmallInt).Direction = ParameterDirection.Output;

                cn.Open();
                ret = ExecuteNonQuery(cmd);
                intLocationId = (short)cmd.Parameters["@location_id"].Value;
            }
            StaffActivity.ActionInsertMethodStaff_log(StaffLogModule.Central_Data_location, StaffLogActionType.Insert, StaffLogSection.NULL, null,
                "tbl_location", "destination_id,status,title,folder_location", "location_id", intLocationId);

            return intLocationId;
        }

        public Dictionary<string, string> dicGetLocationListByDesId(short intDestinationId)
        {
            Dictionary<string, string> dataList = new Dictionary<string, string>();

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_location WHERE destination_id=@destination_id", cn);
                cmd.Parameters.Add("@destination_id", SqlDbType.SmallInt).Value = intDestinationId;
                cn.Open();

                IDataReader reader = ExecuteReader(cmd);
                while (reader.Read())
                {
                    dataList.Add(reader["location_id"].ToString(), reader["title"].ToString());
                }
            }

            return dataList;
        }
           
        public List<object> GetLocationListByDesId(short intDestinationId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_location WHERE destination_id=@destination_id", cn);
                cmd.Parameters.Add("@destination_id", SqlDbType.SmallInt).Value = intDestinationId;
                cn.Open();
                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }

        }

        public string getLocationFolderById(short shrLocationId)
        {

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT folder_location FROM tbl_location WHERE location_id=@location_id", cn);
                cmd.Parameters.Add("@location_id", SqlDbType.SmallInt).Value = shrLocationId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return reader[0].ToString();
                else
                    return null;
               
            }

            
            
        }
        public string getLocationTitleById(short shrLocationId)
        {

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT title FROM tbl_location WHERE location_id=@location_id", cn);
                cmd.Parameters.Add("@location_id", SqlDbType.SmallInt).Value = shrLocationId;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return reader[0].ToString();
                else
                    return null;

            }


        }

    }
}