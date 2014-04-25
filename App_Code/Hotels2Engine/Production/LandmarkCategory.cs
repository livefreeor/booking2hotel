using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Production;
/// <summary>
/// Summary description for ProductPicCategory
/// </summary>
/// 
namespace Hotels2thailand.Production
{
    public class LandmarkCategory : Hotels2BaseClass
    {
        

        public static Dictionary<byte, string> getAllLandmarkCategory()
        {
            LandmarkCategory cLandCat = new LandmarkCategory();
            using (SqlConnection cn = new SqlConnection(cLandCat.ConnectionString))
            {
                Dictionary<byte, string> dataList = new Dictionary<byte, string>();
                SqlCommand cmd = new SqlCommand("SELECT landmark_cat_id, title FROM tbl_landmark_category ORDER BY title", cn);
                cn.Open();
                IDataReader reader = cLandCat.ExecuteReader(cmd);
                while (reader.Read())
                {
                    dataList.Add((byte)reader[0], reader[1].ToString());
                }
                return dataList;
                
            }
            
        }
    }
}