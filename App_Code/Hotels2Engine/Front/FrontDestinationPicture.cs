using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Hotels2thailand.DataAccess;

/// <summary>
/// Summary description for FrontDestinationPicture
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class FrontDestinationPicture:Hotels2BaseClass
    {
        public short PictureId { get; set; }
        public short DestinationID { get; set; }
        public short? LocationID { get; set; }
        public string PathImage { get; set; }
        public string Url { get; set; }
        public string AltImage { get; set; }
        public byte Weight { get; set; }

        public FrontDestinationPicture()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public List<FrontDestinationPicture> getDestinationImage(short destinationID, byte langID)
        {
            List<FrontDestinationPicture> lstImage = new List<FrontDestinationPicture>();
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select dp.pic_id,dp.destination_id,isnull(dp.location_id,0) as location_id,dp.lang_id,dp.path,(d.folder_destination+'-hotels/'+dp.url) as url,dp.alt,dp.weight ";
                sqlCommand = sqlCommand + " from tbl_destination_picture dp,tbl_destination d where dp.destination_id=d.destination_id and dp.lang_id=" + langID + " and dp.destination_id=" + destinationID;

                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lstImage.Add(new FrontDestinationPicture
                    {
                        PictureId = (short)reader["pic_id"],
                        DestinationID = (short)reader["destination_id"],
                        LocationID = (short)reader["location_id"],
                        PathImage = reader["path"].ToString(),
                        Url = reader["url"].ToString(),
                        AltImage = reader["alt"].ToString(),
                        Weight = (byte)reader["weight"]
                    });

                }
                return lstImage;
            }
            

        }

        public List<FrontDestinationPicture> getLocationImage(short destinationID,short locationID, byte langID)
        {
            List<FrontDestinationPicture> lstImage = new List<FrontDestinationPicture>();
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select dp.pic_id,dp.destination_id,isnull(dp.location_id,0) as location_id,dp.lang_id,dp.path,(d.folder_destination+'-hotels/'+dp.url) as url,dp.alt,dp.weight ";
                sqlCommand = sqlCommand + " from tbl_destination_picture dp,tbl_destination d where dp.destination_id=d.destination_id and dp.lang_id=" + langID + " and dp.destination_id=" + destinationID;

                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lstImage.Add(new FrontDestinationPicture
                    {
                        PictureId = (short)reader["pic_id"],
                        DestinationID = (short)reader["destination_id"],
                        LocationID = (short)reader["location_id"],
                        PathImage = reader["path"].ToString(),
                        Url = reader["url"].ToString(),
                        AltImage = reader["alt"].ToString(),
                        Weight = (byte)reader["weight"]
                    });

                }

                List<FrontDestinationPicture> lstImageLocation = new List<FrontDestinationPicture>();

                foreach (FrontDestinationPicture item in lstImage)
                {
                    if (item.LocationID == locationID)
                    {
                        lstImageLocation.Add(item);
                    }
                }

                if (lstImageLocation.Count != 0)
                {
                    lstImage = lstImageLocation;
                }
                //return lstImage;    
                return lstImageLocation;
            }
            
        }
    }
}