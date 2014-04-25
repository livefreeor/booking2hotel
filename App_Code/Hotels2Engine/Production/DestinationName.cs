using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Country
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class DestinationName : Hotels2BaseClass
    {
        public short DestinationID { get; set; }
        public byte LanguageID { get; set; }
        public string FileName { get; set; }
        public string FileName_golf { get; set; }
        public string FileName_day_trips { get; set; }
        public string FileName_water { get; set; }
        public string FileName_show { get; set; }
        public string FileName_health { get; set; }
        public string FileName_spa { get; set; }
        public string Title { get; set; }
        public string ShortDetail { get; set; }




        public DestinationName GetDestination(short destinationID, byte bytLangId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_destination_name WHERE destination_id = @destination_id AND lang_id=@lang_id", cn);
                cmd.Parameters.Add("@destination_id",SqlDbType.SmallInt).Value = destinationID;
                cmd.Parameters.Add("@lang_id",SqlDbType.TinyInt).Value = bytLangId;
                cn.Open();

                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (DestinationName)MappingObjectFromDataReader(reader);
                else
                    return null;

            }

        }
        public int IntsertDestinationName(short shrDesId, byte bytLangId, string strFileName , string strTitle, string strDescrip)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_destination_name (destination_id, lang_id,file_name,file_name_golf,file_name_day_trip,file_name_water_activity,file_name_show_event,file_name_health_check_up,file_name_spa,title,short_detail) VALUES(@destination_id,@lang_id,@file_name,@file_name_golf,@file_name_day_trip,@file_name_water_activity,@file_name_show_event,@file_name_health_check_up,@file_name_spa,@title,@short_detail)", cn);
                cmd.Parameters.Add("@destination_id", SqlDbType.SmallInt).Value = shrDesId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cmd.Parameters.Add("@file_name", SqlDbType.VarChar).Value = FilenameManage(strFileName, bytLangId);
                cmd.Parameters.Add("@file_name_golf", SqlDbType.VarChar).Value = FilenameManage(strFileName, "golf", bytLangId); ;
                cmd.Parameters.Add("@file_name_day_trip", SqlDbType.VarChar).Value = FilenameManage(strFileName, "day-trips", bytLangId); ;
                cmd.Parameters.Add("@file_name_water_activity", SqlDbType.VarChar).Value = FilenameManage(strFileName, "water-activity", bytLangId); ;
                cmd.Parameters.Add("@file_name_show_event", SqlDbType.VarChar).Value = FilenameManage(strFileName, "show-event", bytLangId); ;
                cmd.Parameters.Add("@file_name_health_check_up", SqlDbType.VarChar).Value = FilenameManage(strFileName, "health-check-up", bytLangId); ;
                cmd.Parameters.Add("@file_name_spa", SqlDbType.VarChar).Value = FilenameManage(strFileName, "spa", bytLangId);
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strTitle;
                cmd.Parameters.Add("@short_detail", SqlDbType.NVarChar).Value = strDescrip;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }

            return ret;
        }
        public bool UpdateDestinationName(short shrDesId, byte bytLangId, string strFileName, string strTitle, string strDescrip)
        {
            int ret = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_destination_name SET file_name=@file_name,file_name_golf=@file_name_golf,file_name_day_trip=@file_name_day_trip,file_name_water_activity=@file_name_water_activity,file_name_show_event=@file_name_show_event,file_name_health_check_up=@file_name_health_check_up,file_name_spa=@file_name_spa,title=@title,short_detail=@short_detail WHERE destination_id=@destination_id AND lang_id=@lang_id", cn);
                cmd.Parameters.Add("@destination_id", SqlDbType.SmallInt).Value = shrDesId;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangId;
                cmd.Parameters.Add("@file_name", SqlDbType.VarChar).Value = FilenameManage(strFileName,bytLangId);
                cmd.Parameters.Add("@file_name_golf", SqlDbType.VarChar).Value = FilenameManage(strFileName,"golf", bytLangId); ;
                cmd.Parameters.Add("@file_name_day_trip", SqlDbType.VarChar).Value = FilenameManage(strFileName, "day-trips", bytLangId); ;
                cmd.Parameters.Add("@file_name_water_activity", SqlDbType.VarChar).Value = FilenameManage(strFileName, "water-activity", bytLangId); ;
                cmd.Parameters.Add("@file_name_show_event", SqlDbType.VarChar).Value = FilenameManage(strFileName, "show-event", bytLangId); ;
                cmd.Parameters.Add("@file_name_health_check_up", SqlDbType.VarChar).Value = FilenameManage(strFileName, "health-check-up", bytLangId); ;
                cmd.Parameters.Add("@file_name_spa", SqlDbType.VarChar).Value = FilenameManage(strFileName, "spa", bytLangId);
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = strTitle;
                cmd.Parameters.Add("@short_detail", SqlDbType.NVarChar).Value = strDescrip;
                cn.Open();
                ret = ExecuteNonQuery(cmd);
            }

            return (ret == 1);
        }
        public string FilenameManage(string strToReplace, string strReplaceKey, byte bytLangId)
        {
            string[] strArr = strToReplace.Split(Convert.ToChar("."));
            string result = string.Empty;
            switch (bytLangId)
            {
                case 1:
                    result = strArr[0] + "-" + strReplaceKey + "." + strArr[1];
                    break;
                case 2:
                    result = strArr[0] + "-" + strReplaceKey + "-th." + strArr[1];
                    break;
            }

            return result;
        }
        public string FilenameManage(string strToReplace, byte bytLangId)
        {
            string[] strArr = strToReplace.Split(Convert.ToChar("."));
            string result = string.Empty;
            switch (bytLangId)
            {
                case 1:
                    result = strToReplace;
                    break;
                case 2:
                    result = strArr[0] + "-th." + strArr[1];
                    break;
            }

            return result;
        }


    }
}