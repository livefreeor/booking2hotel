using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Production;
/// <summary>
/// Summary description for Product
/// </summary>
///

namespace Hotels2thailand.Production
{
    public class LocationName:Hotels2BaseClass
    {

        private LinqProductionDataContext dcProduct = new LinqProductionDataContext();

        public short LocationID { get; set; }
        public byte LanguageID { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }

        public LocationName GetLocationNameByID(short locationID, byte bytLangID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_location_name WHERE location_id=@location_id AND lang_id=@lang_id", cn);
                cmd.Parameters.Add("@location_id", SqlDbType.SmallInt).Value = locationID;
                cmd.Parameters.Add("@lang_id", SqlDbType.TinyInt).Value = bytLangID;
                cn.Open();
                IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
                if (reader.Read())
                    return (LocationName)MappingObjectFromDataReader(reader);
                else
                    return null;
            }
        }

        public bool Insert(LocationName location)
        {
            tbl_location_name locationName = new tbl_location_name { 
            location_id=location.LocationID,
            lang_id=location.LanguageID,
            file_name=location.FileName,
            title=location.Title
            };

            dcProduct.tbl_location_names.InsertOnSubmit(locationName);
            try{
                dcProduct.SubmitChanges();
                Staffs.StaffActivity.ActionInsertMethodStaff_log(Staffs.StaffLogModule.Central_Data_location, Staffs.StaffLogActionType.Insert, Staffs.StaffLogSection.NULL,
                    null, "tbl_location", "location_id,lang_id,file_name,title", "location_id,lang_id", location.LocationID, location.LanguageID);
                return true;
            }catch{
                return false;
            }
        }
        public bool Update(LocationName location)
        {
           ArrayList arrOld_Val =  Staffs.StaffActivity.ActionUpdateMethodStaff_log_FirstStep(location.FileName, location.Title);

            tbl_location_name rsLocationName=dcProduct.tbl_location_names.Single(ln=>ln.location_id==LocationID && ln.lang_id==location.LanguageID);
            rsLocationName.file_name=location.FileName;
            rsLocationName.title=location.Title;
           
            try{
             dcProduct.SubmitChanges();
             Staffs.StaffActivity.ActionUpdateMethodStaff_log_Laststep(Staffs.StaffLogModule.Central_Data_location, Staffs.StaffLogActionType.Update, Staffs.StaffLogSection.NULL, null, "tbl_location", "file_name,title", arrOld_Val, "location_id,lang_id", location.LocationID, location.LanguageID);
             return true;
            }catch{
                return false;
            }
        }
    }
}