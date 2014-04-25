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


namespace Hotels2thailand.General
{
    public class dataGen : Hotels2BaseClass
    {
        public dataGen()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string GenCall(string strGenType, byte intLangID)
        {
            string strXMLContent = "";
            string strXMLHeader = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n";
            string strPath = HttpContext.Current.Server.MapPath("/MaptoTheMoon");
            string strFile = "";

            switch (strGenType.ToLower())
            {
                case "destination":
                    strXMLContent = GenDestination(intLangID);
                    strFile = "dataDestination.xml";
                    break;
                case "location":
                    strXMLContent = GenLocation(intLangID);
                    strFile = "dataLocation.xml";
                    break;
                case "product_cat":
                    strXMLContent = GenProductCat(intLangID);
                    strFile = "dataCategory.xml";
                    break;
                case "product":
                    strXMLContent = GenProduct(intLangID);
                    strFile = "dataProduct.xml";
                    break;
            }

            strXMLContent = strXMLHeader + strXMLContent;
            strPath = strPath + "/" + strFile;
            GenFile(strXMLContent, strPath);

            return strXMLContent;
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
                HttpContext.Current.Response.End();
            }
        }

        private string GenDestination(byte intLangID)
        {
            string strConn = WebConfigurationManager.ConnectionStrings["hotels2thailandXConnectionString"].ConnectionString;
            string strXMl = "";

            strXMl = "<destinations>\r\n";

            using (SqlConnection objConn = new SqlConnection(strConn))
            {
                string sqlData;
                sqlData = "SELECT dn.title,dn.destination_id FROM tbl_destination_name dn WHERE lang_id=" + intLangID.ToString() + " ORDER BY dn.title ASC";
                SqlCommand objCom = new SqlCommand(sqlData, objConn);
                objConn.Open();
                IDataReader reader = objCom.ExecuteReader();

                while (reader.Read())
                {
                    strXMl = strXMl + "<destination>\r\n";
                    strXMl = strXMl + "<title>" + reader["title"] + "</title>\r\n";
                    strXMl = strXMl + "<destination_id>" + reader["destination_id"] + "</destination_id>\r\n";
                    strXMl = strXMl + "</destination>\r\n";
                }
            }

            strXMl = strXMl + "</destinations>\r\n";
            return strXMl;
        }

        private string GenLocation(byte intLangID)
        {
            string strConn = WebConfigurationManager.ConnectionStrings["hotels2thailandXConnectionString"].ConnectionString;
            string strXMl = "";

            strXMl = "<locations>\r\n";

            using (SqlConnection objConn = new SqlConnection(strConn))
            {
                string sqlData;
                sqlData = "SELECT ln.location_id,l.destination_id,ln.title FROM tbl_location_name ln, tbl_location l WHERE l.location_id=ln.location_id AND ln.lang_id=" + intLangID.ToString() + " ORDER BY l.destination_id ASC,ln.title ASC";
                SqlCommand objCom = new SqlCommand(sqlData, objConn);
                objConn.Open();
                IDataReader reader = objCom.ExecuteReader();

                while (reader.Read())
                {
                    strXMl = strXMl + "<location>\r\n";
                    strXMl = strXMl + "<title>" + reader["title"] + "</title>\r\n";
                    strXMl = strXMl + "<location_id>" + reader["location_id"] + "</location_id>\r\n";
                    strXMl = strXMl + "<destination_id>" + reader["destination_id"] + "</destination_id>\r\n";
                    strXMl = strXMl + "</location>\r\n";
                }
            }

            strXMl = strXMl + "</locations>\r\n";
            return strXMl;
        }

        private string GenProductCat(byte intLangID)
        {
            string strConn = WebConfigurationManager.ConnectionStrings["hotels2thailandXConnectionString"].ConnectionString;
            string strXMl = "";

            strXMl = "<categories>\r\n";

            using (SqlConnection objConn = new SqlConnection(strConn))
            {
                string sqlData;
                sqlData = "SELECT cat_id,title FROM tbl_product_category ORDER BY title ASC";
                SqlCommand objCom = new SqlCommand(sqlData, objConn);
                objConn.Open();
                IDataReader reader = objCom.ExecuteReader();

                while (reader.Read())
                {
                    strXMl = strXMl + "<category>\r\n";
                    strXMl = strXMl + "<title>" + reader["title"] + "</title>\r\n";
                    strXMl = strXMl + "<category_id>" + reader["cat_id"] + "</category_id>\r\n";
                    strXMl = strXMl + "</category>\r\n";
                }
            }

            strXMl = strXMl + "</categories>\r\n";
            return strXMl;
        }

        private string GenProduct(byte intLangID)
        {
            string strConn = WebConfigurationManager.ConnectionStrings["hotels2thailandXConnectionString"].ConnectionString;
            string strXMl = "";

            strXMl = "<products>\r\n";

            using (SqlConnection objConn = new SqlConnection(strConn))
            {
                string sqlData;
                sqlData = "SELECT pc.product_id,p.destination_id,pc.title,pc.detail,pc.address,p.cat_id,";
                sqlData = sqlData + " (SELECT TOP 1 spl.location_id FROM tbl_product_location spl WHERE p.product_id=spl.product_id) AS location_id";
                sqlData = sqlData + " FROM tbl_product_content pc, tbl_product p";
                sqlData = sqlData + " WHERE p.product_id=pc.product_id AND pc.lang_id=" + intLangID.ToString();
                sqlData = sqlData + " ORDER BY pc.product_id ASC";

                SqlCommand objCom = new SqlCommand(sqlData, objConn);
                objConn.Open();
                IDataReader reader = objCom.ExecuteReader();

                while (reader.Read())
                {
                    strXMl = strXMl + "<product>\r\n";
                    strXMl = strXMl + "<title>" + reader["title"] + "</title>\r\n";
                    strXMl = strXMl + "<product_id>" + reader["product_id"] + "</product_id>\r\n";
                    strXMl = strXMl + "<category_id>" + reader["cat_id"] + "</category_id>\r\n";
                    strXMl = strXMl + "<destination_id>" + reader["destination_id"] + "</destination_id>\r\n";
                    strXMl = strXMl + "<location_id>" + reader["location_id"] + "</location_id>\r\n";
                    strXMl = strXMl + "<address>" + reader["address"].ToString().Hotels2SPcharacter_remove() +"</address>\r\n";
                    strXMl = strXMl + "<detail>" + reader["detail"].ToString().Hotels2SPcharacter_remove() + "</detail>\r\n";
                    strXMl = strXMl + "<product>\r\n";
                }
            }

            strXMl = strXMl + "</products>\r\n";
            return strXMl;
        }

    }
}