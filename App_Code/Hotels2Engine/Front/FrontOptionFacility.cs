using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Hotels2thailand.DataAccess;

/// <summary>
/// Summary description for FrontOptionFacility
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class FrontOptionFacility
    {
        public int FacilityID { get; set; }
        public string Title { get; set; }

        public FrontOptionFacility()
        {

        }

        public List<FrontOptionFacility> LoadFacilityByOptionID(int OptionID,byte langID)
        {
            DataConnect objConn = new DataConnect();
            string sqlCommand = "select fac_id,title from tbl_product_option_facility where option_id=" + OptionID + " and lang_id=" + langID;
            SqlDataReader reader = objConn.GetDataReader(sqlCommand);
            List<FrontOptionFacility> result = new List<FrontOptionFacility>();
            while(reader.Read())
            {
                result.Add(new FrontOptionFacility { 
                 FacilityID=(int)reader["fac_id"],
                  Title=reader["title"].ToString()
                });
            }
            objConn.Close();
            return result;
        }
    }
}