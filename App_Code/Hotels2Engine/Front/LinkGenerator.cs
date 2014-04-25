using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;
using System.Collections;

/// <summary>
/// Summary description for LinkGenerator
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class LinkGenerator:Hotels2BaseClass
    {
        private string UrlPath = "http://www.hotels2thailandnew.com";
        //private string UrlPath = "http://localhost/bluehouse";
        private string UrlReturn = string.Empty;
        private List<LocationLink> linkDataList;
        private byte LangID = 1;

        
        public LinkGenerator()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public LinkGenerator(byte langID)
        {
            LangID = langID;
            if (LangID==2)
            {
                UrlPath = "http://thai.hotels2thailandnew.com";
            }
        }

        public void LoadData(byte langID) {
            linkDataList = GetDataPath();
            LangID = langID;
        }

        public List<LocationLink> GetDataPath()
        {
            

            List<LocationLink> LocationList = new List<LocationLink>();
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select d.destination_id,dn.title as destination_title,dn.file_name as destination_filename,l.location_id,ln.title as location_title,ln.file_name as location_filename,d.folder_destination,l.folder_location";
                sqlCommand = sqlCommand + " from tbl_destination d,tbl_destination_name dn,tbl_location l,tbl_location_name ln";
                sqlCommand = sqlCommand + " where d.destination_id=dn.destination_id and d.destination_id=l.destination_id and l.location_id=ln.location_id and dn.lang_id=" + LangID + " and ln.lang_id=" + LangID;
                sqlCommand = sqlCommand + " and d.status=1 and l.status=1";
                sqlCommand = sqlCommand + " order by d.title,l.title";
                SqlCommand cmd = new SqlCommand(sqlCommand,cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //HttpContext.Current.Response.Write(reader["destination_id"]+"<br>");

                    LocationList.Add(new LocationLink
                    {
                        DestinationID = (short)reader["destination_id"],
                        DestinationTitle = reader["destination_title"].ToString(),
                        DestinationFileName = reader["destination_filename"].ToString(),
                        LocationID = (short)reader["location_id"],
                        LocationTitle = reader["location_title"].ToString(),
                        LocationFileName = reader["location_filename"].ToString(),
                        FolderDestination = reader["folder_destination"].ToString(),
                        FolderLocation = reader["folder_location"].ToString()
                    });
                }

                //return LocationList;
            }

            if (LocationList.Count == 0)
            {
                using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                {
                    string sqlCommand = "select d.destination_id,dn.title as destination_title,dn.file_name as destination_filename,l.location_id,ln.title as location_title,ln.file_name as location_filename,d.folder_destination,l.folder_location";
                    sqlCommand = sqlCommand + " from tbl_destination d,tbl_destination_name dn,tbl_location l,tbl_location_name ln";
                    sqlCommand = sqlCommand + " where d.destination_id=dn.destination_id and d.destination_id=l.destination_id and l.location_id=ln.location_id and dn.lang_id=1 and ln.lang_id=1";
                    sqlCommand = sqlCommand + " and d.status=1 and l.status=1";
                    sqlCommand = sqlCommand + " order by d.title,l.title";
                    SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        //HttpContext.Current.Response.Write(reader["destination_id"]+"<br>");

                        LocationList.Add(new LocationLink
                        {
                            DestinationID = (short)reader["destination_id"],
                            DestinationTitle = reader["destination_title"].ToString(),
                            DestinationFileName = reader["destination_filename"].ToString(),
                            LocationID = (short)reader["location_id"],
                            LocationTitle = reader["location_title"].ToString(),
                            LocationFileName = reader["location_filename"].ToString(),
                            FolderDestination = reader["folder_destination"].ToString(),
                            FolderLocation = reader["folder_location"].ToString()
                        });
                    }

                    //return LocationList;
                }
            }
            return LocationList;

        }

        public LocationLink GetProductPath(int productID)
        { 
            

            LocationLink item = new LocationLink();
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "SELECT DISTINCT(pl.location_id),d.destination_id,dn.title as destination_title,dn.file_name as destination_filename,ln.title as location_title,ln.file_name as location_filename,d.folder_destination,l.folder_location";
                sqlCommand = sqlCommand + " FROM tbl_product_location pl , tbl_location l, tbl_destination d, tbl_product_category pc, tbl_product p , tbl_location_name ln, tbl_destination_name dn";
                sqlCommand = sqlCommand + " WHERE pl.location_id = l.location_id AND l.destination_id = d.destination_id AND pc.cat_id = p. cat_id AND pl.product_id = p.product_id ";
                sqlCommand = sqlCommand + " AND ln.location_id = l.location_id  AND dn.destination_id = d.destination_id AND dn.lang_id = " + LangID + " AND ln.lang_id = " + LangID + " AND p.product_id=" + productID;
                sqlCommand = sqlCommand + " and d.status=1 and l.status=1";
                sqlCommand = sqlCommand + " order by dn.title,ln.title";
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    item.DestinationID = (short)reader["destination_id"];
                    item.DestinationTitle = reader["destination_title"].ToString();
                    item.DestinationFileName = reader["destination_filename"].ToString();
                    item.LocationID = (short)reader["location_id"];
                    item.LocationTitle = reader["location_title"].ToString();
                    item.LocationFileName = reader["location_filename"].ToString();
                    item.FolderDestination = reader["folder_destination"].ToString();
                    item.FolderLocation = reader["folder_location"].ToString();
                }
                return item;

            }
            
        }

        public List<LocationLink> GetLocationList(byte productCatID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "SELECT DISTINCT(pl.location_id),d.destination_id,dn.title as destination_title,dn.file_name as destination_filename,ln.title as location_title,ln.file_name as location_filename,d.folder_destination,l.folder_location";
                sqlCommand = sqlCommand + " FROM tbl_product_location pl , tbl_location l, tbl_destination d, tbl_product_category pc, tbl_product p , tbl_location_name ln, tbl_destination_name dn";
                sqlCommand = sqlCommand + " WHERE pl.location_id = l.location_id AND l.destination_id = d.destination_id AND pc.cat_id = p. cat_id AND pl.product_id = p.product_id ";
                sqlCommand = sqlCommand + " AND ln.location_id = l.location_id  AND dn.destination_id = d.destination_id AND dn.lang_id = " + LangID + " AND ln.lang_id = " + LangID + " AND p.cat_id = " + productCatID;
                sqlCommand = sqlCommand + " and d.status=1 and l.status=1";
                sqlCommand = sqlCommand + " order by dn.title,ln.title";

                //HttpContext.Current.Response.Write(sqlCommand);
                List<LocationLink> LocationList = new List<LocationLink>();
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    //HttpContext.Current.Response.Write(reader["destination_id"]+"<br>");

                    LocationList.Add(new LocationLink
                    {
                        DestinationID = (short)reader["destination_id"],
                        DestinationTitle = reader["destination_title"].ToString(),
                        DestinationFileName = reader["destination_filename"].ToString(),
                        LocationID = (short)reader["location_id"],
                        LocationTitle = reader["location_title"].ToString(),
                        LocationFileName = reader["location_filename"].ToString(),
                        FolderDestination = reader["folder_destination"].ToString(),
                        FolderLocation = reader["folder_location"].ToString()
                    });
                }
                linkDataList = LocationList;
                return LocationList;
            }
            
        }

        public string GetLocationPath(int locationID)
        { 
            //List<LocationLink> data=
            foreach(LocationLink item in linkDataList)
            {
                if(item.LocationID==locationID){
                   
                    UrlReturn = UrlPath + "/" + item.LocationFileName;
                    break;
                }
            }
            return UrlReturn;
        }
        public string GetDestinationPath(int destinationID)
        {
            foreach (LocationLink item in linkDataList)
            {
                if (item.DestinationID == destinationID)
                {
                    UrlReturn = UrlPath + "/" + item.DestinationFileName;
                    break;
                }
            }
            return UrlReturn;
        }
        public string GetProductPath(int destinationID, byte cateID, string Url)
        {

            foreach (LocationLink item in linkDataList)
            {
                if (item.DestinationID== destinationID)
                {
                    UrlReturn = UrlPath+"/" + item.FolderDestination + "-" + Utility.GetProductType(cateID)[0, 3] + "/" + Url;
                    break;
                }
            }
            return UrlReturn;
        }

        public string GetProductReviewPath(int destinationID, byte cateID, string Url)
        {

            foreach (LocationLink item in linkDataList)
            {
                if (item.DestinationID == destinationID)
                {
                    UrlReturn = UrlPath + "/" + item.FolderDestination + "-" + Utility.GetProductType(cateID)[0, 3] + "/" + Url+"/write_review.aspx";
                    break;
                }
            }
            return UrlReturn;
        }

        public string GetFolderPath(int destinationID,byte cateID)
        {
            foreach (LocationLink item in linkDataList)
            {
                if (item.DestinationID == destinationID)
                {
                    UrlReturn = HttpContext.Current.Server.MapPath("../" + item.FolderDestination + "-" + Utility.GetProductType(cateID)[0, 3]);
                    break;
                }
            }
            return UrlReturn;
        }
    }
}