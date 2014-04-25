using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Hotels2thailand.DataAccess;
using Hotels2thailand;
using System.Web.Configuration;
using System.Data;
using System.IO;

/// <summary>
/// Summary description for SiteMapGen
/// </summary>
/// 

namespace Hotels2thailand.General
{
    public class SiteMapGen : Hotels2BaseClass
    {
        public SiteMapGen()
        {
         
        }

        private string strXMLHeader = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n";
        private string strXMLURLSetHead = "<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">\r\n";
        private string strXMLURLSetFoot = "</urlset>\r\n";
        private string strURLBase = "http://www.hotels2thailand.com/";

        public string GenCall(string strGenType, byte intLangID)
        {
            string strPath = HttpContext.Current.Server.MapPath("/MaptoTheMoon");
            string strPathHotelMain = strPath + "/HotelMain.xml";
            string strPathOptionMajor = strPath + "/OptionMajor.xml";
            string strPathOptionMinor = strPath + "/OptionMinor.xml";
            string strPathProductMain = strPath + "/ProductMain.xml";
            string strPathProductDetail = strPath + "/ProductDetail.xml";
            string strXMLHotelMain = "";
            string strXMLOptionMajor = "";
            string strXMLOptionMinor = "";
            string strXMLProductMain = "";
            string strXMLProductDetail = "";

            switch (strGenType.ToLower())
            {
                case "all":
                    strXMLHotelMain = GenXMLHotelMain(intLangID);
                    GenFile(strXMLHotelMain, strPathHotelMain);

                    strXMLOptionMajor = GenXMLHotelOption("major", intLangID);
                    GenFile(strXMLOptionMajor, strPathOptionMajor);

                    strXMLOptionMinor = GenXMLHotelOption("minor", intLangID);
                    GenFile(strXMLOptionMinor, strPathOptionMinor);

                    strXMLProductMain = GenXMLProductMain(intLangID);
                    GenFile(strXMLProductMain, strPathProductMain);

                    strXMLProductDetail = GenXMLProductDetail(intLangID);
                    GenFile(strXMLProductDetail, strPathProductDetail);
                    break;

                case "hotel main":
                    strXMLHotelMain = GenXMLHotelMain(intLangID);
                    GenFile(strXMLHotelMain, strPathHotelMain);
                    break;

                case "option major":
                    strXMLOptionMajor = GenXMLHotelOption("major", intLangID);
                    GenFile(strXMLOptionMajor, strPathOptionMajor);
                    break;

                case "option minor":
                    strXMLOptionMinor = GenXMLHotelOption("minor", intLangID);
                    GenFile(strXMLOptionMinor, strPathOptionMinor);
                    break;

                case "product main":
                    strXMLProductMain = GenXMLProductMain(intLangID);
                    GenFile(strXMLProductMain, strPathProductMain);
                    break;

                case "product detail":
                    strXMLProductDetail = GenXMLProductDetail(intLangID);
                    GenFile(strXMLProductDetail, strPathProductDetail);
                    break;

            }

            return "<strong>Complete</strong>";
        }

        private void GenFile(string strXML, string strFileLocation) // Generate file
        {
            StreamWriter StrWer = default(StreamWriter);
            try
            {
                StrWer = File.CreateText(strFileLocation);
                StrWer.Write(strXML);
                StrWer.Close();
                HttpContext.Current.Response.Write(strFileLocation + "<br>");
            }
            catch 
            {
                HttpContext.Current.Response.Write("<font color=\"red\">Could not create file :" + strFileLocation + "</font>");
                StrWer.Close();
                HttpContext.Current.Response.End();
            }
        }

        private string GenURLXML(string strURL, string strModify, string strChangeFreq, string strPriority) // Generate URL XML
        {
            string strXML;
            string strXMLlastMod = "";
            string strXMLChange = "";
            string strXMLPrior = "";

            if (!String.IsNullOrEmpty(strModify))
            {
                strXMLlastMod = "<lastmod>" + strModify + "</lastmod>\r\n";
            }

            if (!String.IsNullOrEmpty(strChangeFreq))
            {
                strXMLChange = "<changefreq>" + strChangeFreq + "</changefreq>\r\n";
            }

            if (!String.IsNullOrEmpty(strPriority))
            {
                strXMLPrior = "<priority>" + strPriority + "</priority>\r\n";
            }

            strXML = "<url>\r\n";
            strXML = strXML + "<loc>" + strURL + "</loc>\r\n";
            strXML = strXML + strXMLlastMod;
            strXML = strXML + strXMLChange;
            strXML = strXML + strXMLPrior;
            strXML = strXML + "</url>\r\n";

            return strXML;
        }

        private string GenXMLHotelMain(byte intLangID) // Hotel Main,Info,Photo,Review,Why,Map,PDF,Tell,Print
        {
            string strXMLReturn = "";
            string sqlHotel = "";
            string strURLMain = "";
            string strURLInfo = "";
            string strURLPhoto = "";
            string strURLReview = "";
            string strURLWhy = "";
            string strURLMap = "";
            string strURLPDF = "";
            string strURLTell = "";
            string strURLPrint = "";
            string strXMLMain = "";
            string strXMLInfo = "";
            string strXMLPhoto = "";
            string strXMLReview = "";
            string strXMLWhy = "";
            string strXMLMap = "";
            string strXMLPDF = "";
            string strXMLTell = "";
            string strXMLPrint = "";

            sqlHotel = "SELECT pc.product_id,pc.title,pc.file_name_main,pc.file_name_contact,pc.file_name_information,pc.file_name_pdf,pc.file_name_photo,pc.file_name_review,pc.file_name_why,d.folder_destination";
            sqlHotel = sqlHotel + " FROM tbl_product p, tbl_product_content pc, tbl_destination d";
            sqlHotel = sqlHotel + " WHERE p.product_id=pc.product_id AND p.destination_id=d.destination_id AND pc.lang_id=" + intLangID.ToString() + " AND p.status=1";
            sqlHotel = sqlHotel + " ORDER BY p.product_id ASC";

            using (SqlConnection objConn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand objCom = new SqlCommand(sqlHotel, objConn);
                objConn.Open();
                IDataReader reader = objCom.ExecuteReader();

                strXMLReturn = strXMLReturn + strXMLHeader + strXMLURLSetHead;

                while (reader.Read())
                {
                    strURLMain = strURLBase + reader["folder_destination"] + "-hotels/" + reader["file_name_main"].ToString().Hotels2SPcharacter_remove();
                    strURLInfo = strURLBase + reader["folder_destination"] + "-hotels/" + reader["file_name_information"].ToString().Hotels2SPcharacter_remove();
                    strURLPhoto = strURLBase + reader["folder_destination"] + "-hotels/" + reader["file_name_photo"].ToString().Hotels2SPcharacter_remove();
                    strURLReview = strURLBase + reader["folder_destination"] + "-hotels/" + reader["file_name_review"].ToString().Hotels2SPcharacter_remove();
                    strURLWhy = strURLBase + reader["folder_destination"] + "-hotels/" + reader["file_name_why"].ToString().Hotels2SPcharacter_remove();
                    strURLPDF = strURLBase + reader["folder_destination"] + "-hotels/" + reader["file_name_pdf"].ToString().Hotels2SPcharacter_remove();
                    strURLMap = strURLBase + "thailand-hotels-map.aspx?pid=" + reader["product_id"];
                    strURLTell = strURLBase + "thailand-hotels-tell.aspx?pd=" + reader["product_id"];
                    strURLPrint = strURLBase + "thailand-hotels-print.aspx?pd=" + reader["product_id"];

                    strXMLMain = GenURLXML(strURLMain, "", "", "");
                    strXMLInfo = GenURLXML(strURLInfo, "", "", "");
                    strXMLPhoto = GenURLXML(strURLPhoto, "", "", "");
                    strXMLReview = GenURLXML(strURLReview, "", "", "");
                    strXMLWhy = GenURLXML(strURLWhy, "", "", "");
                    strXMLMap = GenURLXML(strURLMap, "", "", "");
                    strXMLPDF = GenURLXML(strURLPDF, "", "", "");
                    strXMLTell = GenURLXML(strURLTell, "", "", "");
                    strXMLPrint = GenURLXML(strURLPrint, "", "", "");

                    strXMLReturn = strXMLReturn + strXMLMain + strXMLInfo + strXMLPhoto + strXMLReview + strXMLWhy + strXMLMap + strXMLPDF + strXMLTell + strXMLPrint;
                }

                strXMLReturn = strXMLReturn + strXMLURLSetFoot;
            }

            return strXMLReturn;
        }

        private string GenXMLHotelOption(string strType, byte intLangID)
        {

            string strDesID= "(30,31,32,33,34)"; // Bangkok,Phuket,Chiang Mai,Pattaya,Koh Samui
            string strCondition = "";
            string sqlOption = "";
            string strURLOption = "";
            string strXMLOption = "";
            string strXMLReturn = "";

            if (strType == "major")
            {
                strCondition = " IN " + strDesID;
            }
            else
            {
                strCondition = " NOT IN " + strDesID;
            }

            sqlOption = "SELECT poc.option_id,pc.file_name_main,d.folder_destination";
            sqlOption = sqlOption + " FROM tbl_product_option po, tbl_product_option_content poc, tbl_product p, tbl_product_content pc, tbl_destination d";
            sqlOption = sqlOption + " WHERE d.destination_id=p.destination_id AND p.product_id=po.product_id AND p.product_id=pc.product_id AND po.option_id=poc.option_id AND po.cat_id=38 AND pc.lang_id=" + intLangID.ToString() + " AND po.status=1 AND poc.lang_id=1 AND p.destination_id" + strCondition;
            sqlOption = sqlOption + " ORDER BY po.option_id ASC";

            using (SqlConnection objConn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand objCom = new SqlCommand(sqlOption, objConn);
                objConn.Open();
                IDataReader reader = objCom.ExecuteReader();

                strXMLReturn = strXMLReturn + strXMLHeader + strXMLURLSetHead;

                while (reader.Read())
                {
                    strURLOption = strURLBase + reader["folder_destination"] + "-hotels/" + reader["file_name_main"].ToString().Replace(".asp", "").Hotels2SPcharacter_remove() + "_room_" + reader["option_id"].ToString() + ".asp";
                    strXMLOption = GenURLXML(strURLOption, "", "", "");
                    strXMLReturn = strXMLReturn + strXMLOption;
                }

                strXMLReturn = strXMLReturn + strXMLURLSetFoot;

            }


            return strXMLReturn;
        }

        private string GenXMLProductMain(byte intLangID) // Destination Location for all product
        {
            // Destination //
            string sqlDestination;
            string strXMLDestination = "";
            string strXMLReturn = "";
            string strURLHotel = "";
            string strURLDayTrip = "";
            string strURLGolf = "";
            string strURLWater = "";
            string strURLShow = "";
            string strURLHealth = "";
            string strURLSpa = "";
            string strXMLHotel = "";
            string strXMLDayTrip = "";
            string strXMLGolf = "";
            string strXMLWater = "";
            string strXMLShow = "";
            string strXMLHealth = "";
            string strXMLSpa = "";

            sqlDestination = "SELECT dn.destination_id,dn.[file_name],dn.file_name_day_trip,dn.file_name_golf,dn.file_name_health_check_up,dn.file_name_show_event,dn.file_name_spa,dn.file_name_water_activity,";
            sqlDestination = sqlDestination + " convert(int,(SELECT COUNT(sp.product_id) FROM tbl_product sp WHERE sp.destination_id=d.destination_id AND sp.status=1 AND sp.cat_id=29)) AS num_hotel,";
            sqlDestination = sqlDestination + " convert(int,(SELECT COUNT(sp.product_id) FROM tbl_product sp WHERE sp.destination_id=d.destination_id AND sp.status=1 AND sp.cat_id=31)) AS num_golf,";
            sqlDestination = sqlDestination + " convert(int,(SELECT COUNT(sp.product_id) FROM tbl_product sp WHERE sp.destination_id=d.destination_id AND sp.status=1 AND sp.cat_id=34)) AS num_day_trip,";
            sqlDestination = sqlDestination + " convert(int,(SELECT COUNT(sp.product_id) FROM tbl_product sp WHERE sp.destination_id=d.destination_id AND sp.status=1 AND sp.cat_id=36)) AS num_water,";
            sqlDestination = sqlDestination + " convert(int,(SELECT COUNT(sp.product_id) FROM tbl_product sp WHERE sp.destination_id=d.destination_id AND sp.status=1 AND sp.cat_id=38)) AS num_show,";
            sqlDestination = sqlDestination + " convert(int,(SELECT COUNT(sp.product_id) FROM tbl_product sp WHERE sp.destination_id=d.destination_id AND sp.status=1 AND sp.cat_id=39)) AS num_health,";
            sqlDestination = sqlDestination + " convert(int,(SELECT COUNT(sp.product_id) FROM tbl_product sp WHERE sp.destination_id=d.destination_id AND sp.status=1 AND sp.cat_id=40)) AS num_spa";
            sqlDestination = sqlDestination + " FROM tbl_destination d, tbl_destination_name dn";
            sqlDestination = sqlDestination + " WHERE d.destination_id=dn.destination_id AND d.status=1 AND dn.lang_id=1";
            sqlDestination = sqlDestination + " ORDER BY d.destination_id ASC";

            using (SqlConnection objConn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand objCom = new SqlCommand(sqlDestination, objConn);
                objConn.Open();
                IDataReader reader = objCom.ExecuteReader();

                while (reader.Read())
                {
                    // Hotel //
                    if ((int)reader["num_hotel"] > 0)
                    {
                        strURLHotel = strURLBase + reader["file_name"].ToString().Hotels2SPcharacter_remove();
                        strXMLHotel = GenURLXML(strURLHotel, "", "", "");
                    }
                    else
                    {
                        strXMLHotel = "";
                    }
                    // Hotel //

                    // DayTrip //
                    if ((int)reader["num_day_trip"] > 0)
                    {
                        strURLDayTrip = strURLBase + reader["file_name_day_trip"].ToString().Hotels2SPcharacter_remove();
                        strXMLDayTrip = GenURLXML(strURLDayTrip, "", "", "");
                    }
                    else
                    {
                        strXMLDayTrip = "";
                    }
                    // DayTrip //

                    // Golf //
                    if ((int)reader["num_golf"] > 0)
                    {
                        strURLGolf = strURLBase + reader["file_name_golf"].ToString().Hotels2SPcharacter_remove();
                        strXMLGolf = GenURLXML(strURLGolf, "", "", "");
                    }
                    else
                    {
                        strXMLGolf = "";
                    }
                    // Golf //

                    // Water //
                    if ((int)reader["num_water"] > 0)
                    {
                        strURLWater = strURLBase + reader["file_name_water_activity"].ToString().Hotels2SPcharacter_remove();
                        strXMLWater = GenURLXML(strURLWater, "", "", "");
                    }
                    else
                    {
                        strXMLWater = "";
                    }
                    // Water //

                    // Show //
                    if ((int)reader["num_show"] > 0)
                    {
                        strURLShow = strURLBase + reader["file_name_show_event"].ToString().Hotels2SPcharacter_remove();
                        strXMLShow = GenURLXML(strURLShow, "", "", "");
                    }
                    else
                    {
                        strXMLShow = "";
                    }
                    // Show //

                    // Health //
                    if ((int)reader["num_health"] > 0)
                    {
                        strURLHealth = strURLBase + reader["file_name_health_check_up"].ToString().Hotels2SPcharacter_remove();
                        strXMLHealth = GenURLXML(strURLHealth, "", "", "");
                    }
                    else
                    {
                        strXMLHealth = "";
                    }
                    // Health //

                    // Spa //
                    if ((int)reader["num_spa"] > 0)
                    {
                        strURLSpa = strURLBase + reader["file_name_spa"].ToString().Hotels2SPcharacter_remove();
                        strXMLSpa = GenURLXML(strURLSpa, "", "", "");
                    }
                    else
                    {
                        strXMLSpa = "";
                    }
                    // Spa //

                    strXMLDestination = strXMLDestination + strXMLHotel + strXMLDayTrip + strXMLGolf + strXMLWater + strXMLShow + strXMLHealth + strXMLSpa;

                }
            }


            // Destination //

            // Location //
            string sqlLocation;
            string strURLLocation = "";
            string strXMLLocation = "";

            sqlLocation = "SELECT ln.location_id,ln.[file_name]";
            sqlLocation = sqlLocation + " FROM tbl_location l,tbl_location_name ln";
            sqlLocation = sqlLocation + " WHERE l.location_id=ln.location_id AND l.status=1 AND ln.lang_id=1";
            sqlLocation = sqlLocation + " ORDER BY l.location_id ASC";

            using (SqlConnection objConn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand objCom = new SqlCommand(sqlLocation, objConn);
                objConn.Open();
                IDataReader reader = objCom.ExecuteReader();

                while (reader.Read())
                {
                    strURLLocation = strURLBase + reader["file_name"].ToString().Hotels2SPcharacter_remove();
                    strXMLLocation = strXMLLocation + GenURLXML(strURLLocation, "", "", "");
                }
            }
            // Location //

            strXMLReturn = strXMLHeader + strXMLURLSetHead + strXMLDestination + strXMLLocation + strXMLURLSetFoot;

            return strXMLReturn;
        }

        private string GenXMLProductDetail(byte intLangID) // All product except hotels 
        {
            string sqlProduct = "";
            string strURLProduct = "";
            string strXMLProduct = "";
            string strXMLreturn = "";

            sqlProduct = "SELECT pc.file_name_main,p.cat_id,c.folder_cat,d.folder_destination";
            sqlProduct = sqlProduct + " FROM tbl_product_content pc, tbl_product p, tbl_product_category c,tbl_destination d";
            sqlProduct = sqlProduct + " WHERE d.destination_id=p.destination_id AND c.cat_id=p.cat_id AND p.product_id=pc.product_id AND p.cat_id NOT IN (29,31,37) AND p.status=1 AND pc.lang_id=" + intLangID.ToString();
            sqlProduct = sqlProduct + " ORDER BY p.product_id ASC";

            using (SqlConnection objConn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand objCom = new SqlCommand(sqlProduct, objConn);
                objConn.Open();
                IDataReader reader = objCom.ExecuteReader();

                strXMLreturn = strXMLHeader + strXMLURLSetHead;

                while (reader.Read())
                {
                    strURLProduct = strURLBase + reader["folder_destination"] + "-" + reader["folder_cat"] + "/" + reader["file_name_main"].ToString().Hotels2SPcharacter_remove();
                    strXMLProduct = GenURLXML(strURLProduct, "", "", "");
                    strXMLreturn = strXMLreturn + strXMLProduct;
                }


                strXMLreturn = strXMLreturn + strXMLURLSetFoot;
            }

            return strXMLreturn;
        }

    }
}