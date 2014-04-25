using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.Production;

/// <summary>
/// Summary description for Booking
/// </summary>
/// 
namespace Hotels2thailand.Front
{

    public class LocationGroup:Hotels2BaseClass
    {
        public int ProductID { get; set; }
        public string ProductTitle { get; set; }
        public string PathName { get; set; }
        private byte _langID = 1;

        public byte LangID
        {
            set { _langID = value; }
        }
        private string getSqlCondition(short shrDestiantionID, short shrLocationId)
        {
            string SqlCondition = string.Empty;
           
            switch (shrDestiantionID)
            {
                //### Bangkok ###
                case 30:
                    switch (shrLocationId)
                    {
                        //### Group1 ### China Town,Khaosan Road,Ratchadapisek,River Side,Suvarnabhumi Airport    
                        case 90:case 189:case 70:case 125:case 62:
                            SqlCondition = "pl.location_id IN (90,189,70,125,62)";
                            break;
                        //### Group2 ### Sukhumvit,Sathorn
                        case 59:case 124:
                            SqlCondition = "pl.location_id IN (59,124)";
                            break;
                        //### Group3 ### Chatuchak Market (JJ Market ),Don Muang Airport,Other Area,Pattanakarn,Petchburi Road,Silom
                        case 106:case 291:case 284:case 65:case 58:case 91:
                            SqlCondition = "pl.location_id IN (106,291,284,65,58,91)";
                            break;
                        default:
                            SqlCondition = "d.destination_id=30";
                            break;
                    }
                    break;
                //### Phuket ###
                case 31:
                    switch (shrLocationId)
                    {
                        //### Group1 ### Karon Beach,Patong Beach
                        case 68:case 81:
                            SqlCondition = "pl.location_id IN (68,81)";
                            break;
                        //### Group2 ### All Others
                        default:
                            SqlCondition = "d.destination_id=31 AND location_id NOT IN (68,81)";
                            break;
                    }
                    break;
                //### Chiang Mai ###
                case 32:
                    SqlCondition = "d.destination_id=32";
                    break;
                //### Pattaya ###
                case 33:
                    SqlCondition = "d.destination_id=33";
                    break;
                //### Koh Samui ###
                case 34:
                    SqlCondition = "d.destination_id=34";
                    break;
                //### Krabi ###
                case 35:
                    SqlCondition = "d.destination_id=35";
                    break;
                //### North ### Chiang Rai,Phitsanulok,Mae Hong Son,Lamphun,Phetchabun,Sukhothai,Lampang,Loei,Tak
                case 36:case 40:case 43:case 60:case 66:case 73:case 74:case 76:case 83:
                    SqlCondition = "d.destination_id IN (36,40,43,60,66,73,74,76,83)";
                    break;
                //### Middle+North East ### Ayutthaya,Kanchanaburi,Khao Yai,Nakornpathom,Nakornnayok,Samutprakarn,Ratchaburi,Nakhon Phanom,Nakhonratchasima,Uthai Thani,Khon Kaen,Nakhon Si Thammarat,Nong Khai,Ubon Ratchathani,Udon Thani
                case 44:case 45:case 52:case 56:case 57:case 59:case 62: case 84:case 63:case 67:case 68:case 69:case 77:case 78:case 79:
                    SqlCondition = "d.destination_id IN (44,45,52,56,57,59,62,84,63,67,68,69,77,78,79)";
                    break;
                //### Sea1(Near Bangkok) ### Cha Am,Hua Hin,Rayong,Koh Samet
                case 37:case 38:case 42:case 50:
                    SqlCondition = "d.destination_id IN (37,38,42,50)";
                    break;
                //### Sea2(East) ### Koh Chang,Prachuap Khiri Khan,Koh Kood,Koh Tao,Trat,Chanthaburi,Phetchaburi
                case 46:case 48:case 49:case 65:case 75:case 58:case 61:
                    SqlCondition = "d.destination_id IN (46,48,49,65,75,58,61)";
                    break;
                //### Sea3(South) ### Phang Nga,Koh Phangan,Trang,Chumphon,Other Destinations,Songkhla,Hat Yai,Surat Thani 
                case 51:case 53:case 54:case 55:case 64:case 70: case 71:case 72:
                    SqlCondition = "d.destination_id IN (51,53,54,55,64,70,71,72)";
                    break;
                default:
                    SqlCondition = "d.destination_id=" + shrDestiantionID;
                    break;
            }

            return SqlCondition;
        }
        public List<LocationGroup> getLocationGroup(byte bytProductCatID, short shrDestiantionID, short shrLocationId, byte bytLangId)
        {
            string sqlCommand = string.Empty;
            sqlCommand=sqlCommand+"SELECT p.product_id , pc.title, (d.folder_destination + '-' + pcat.folder_cat + '/' + pc.file_name_main) as filepath,";
            sqlCommand = sqlCommand + " (SELECT spc.title from tbl_product_content spc where spc.product_id=p.product_id and spc.lang_id=" + bytLangId + ") as second_lang";
            sqlCommand=sqlCommand+" FROM  tbl_product p, tbl_product_content pc, tbl_product_category pcat , tbl_product_location pl , tbl_destination d";
            sqlCommand=sqlCommand+" WHERE p.product_id = pc.product_id AND p.product_id = pl.product_id AND p.cat_id = pcat.cat_id ";
            sqlCommand = sqlCommand + " AND p.destination_id = d.destination_id AND p.status = 1 AND pc.lang_id = 1 AND pcat.cat_id = " + bytProductCatID;
            sqlCommand=sqlCommand+" AND " + getSqlCondition(shrDestiantionID, shrLocationId);
            sqlCommand=sqlCommand+" ORDER BY pc.title";
            //HttpContext.Current.Response.Write(sqlCommand+"<br>");
            //HttpContext.Current.Response.Flush();
            using(SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<LocationGroup> result = new List<LocationGroup>();

                string filePath=string.Empty;
                string defaultTitle=string.Empty;

                while(reader.Read())
                {
                    filePath=reader["filepath"].ToString();
                    if (bytLangId==1)
                    {
                        defaultTitle = reader["title"].ToString();
                    }else{
                        filePath = filePath.Replace(".asp", "-th.asp");
                        defaultTitle=reader["second_lang"].ToString();
                        if (string.IsNullOrEmpty(defaultTitle))
                        {
                            defaultTitle = reader["title"].ToString();
                        }
                       
                    }
                    result.Add(new LocationGroup { 
                    ProductID=(int)reader["product_id"],
                    ProductTitle=defaultTitle,
                    PathName=filePath
                    });
                   
                }
                return result;  
            }
        }
    }
        
   
        
}