using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ProductPriceList
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class Navigator:Hotels2BaseClass
    {
        private string urlDomain = "";
        private string urlDomainTH = "";
        public short ProductCatID { get; set; }
        public short DestinationID { get; set; }
        public short LocationID { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        
        private List<LocationLink> LocationList;
        private string navDisplay;
        private byte _langID = 1;

        public byte LangID
        {
            set { _langID = value;
                if (_langID == 1)
                {
                    navDisplay = "<a href=\"" + urlDomain + "\">Home</a>";
                }
                else
                {
                    navDisplay = "<a href=\"" + urlDomainTH + "\">หน้าแรก</a>";
                }
            }
        }

        public Navigator()
        {
            navDisplay = "<a href=\"" + urlDomain + "\">Home</a>";     
        }

       
        public void LoadLocationLinkByProductID(int ProductID,byte langID)
        {
            LocationList = new List<LocationLink>();
            string strCommand = string.Empty;
            using(SqlConnection cn=new SqlConnection(this.ConnectionString))
            {
                strCommand=strCommand+"select d.destination_id,dn.title as destination_title,dn.file_name as destination_filename,l.location_id,ln.title as location_title,ln.file_name as location_filename,d.folder_destination,l.folder_location";
                strCommand=strCommand+" from tbl_destination d,tbl_destination_name dn,tbl_location l,tbl_location_name ln,tbl_product p,tbl_product_location pl";
                strCommand = strCommand + " where d.destination_id=dn.destination_id and d.destination_id=l.destination_id and l.location_id=ln.location_id and dn.lang_id=" + langID + " and ln.lang_id=" + langID + " and p.destination_id=d.destination_id and p.product_id=pl.product_id and pl.location_id=l.location_id";
                strCommand=strCommand+" and d.status=1 and l.status=1 and p.product_id="+ProductID;
                SqlCommand cmd = new SqlCommand(strCommand, cn);
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
            }
            

            if (LocationList.Count==0)
            {
                using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                {
                    strCommand = "select d.destination_id,dn.title as destination_title,dn.file_name as destination_filename,l.location_id,ln.title as location_title,ln.file_name as location_filename,d.folder_destination,l.folder_location";
                    strCommand = strCommand + " from tbl_destination d,tbl_destination_name dn,tbl_location l,tbl_location_name ln,tbl_product p,tbl_product_location pl";
                    strCommand = strCommand + " where d.destination_id=dn.destination_id and d.destination_id=l.destination_id and l.location_id=ln.location_id and dn.lang_id=1 and ln.lang_id=1 and p.destination_id=d.destination_id and p.product_id=pl.product_id and pl.location_id=l.location_id";
                    strCommand = strCommand + " and d.status=1 and l.status=1 and p.product_id=" + ProductID;
                    SqlCommand cmd = new SqlCommand(strCommand, cn);
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
                }
            }

        }
        public void LoadDestinationLinkByProductID(FrontProductDetail detail,byte langID)
        {
            string strField = "";
            //This function is not include location
            switch (detail.CategoryID)
            {
                case 29:
                    strField = "dn.file_name";
                    break;
                case 32:
                    strField = "dn.file_name_golf";
                    break;
                case 34:
                    strField = "dn.file_name_day_trip";
                    break;
                case 36:
                    strField = "dn.file_name_water_activity";
                    break;
                case 38:
                    strField = "dn.file_name_show_event";
                    break;
                case 39:
                    strField = "dn.file_name_health_check_up";
                    break;
                case 40:
                    strField = "dn.file_name_spa";
                    break;
            }

            LocationList = new List<LocationLink>();
            DataConnect myConn = new DataConnect();

            string sqlCommand = "select d.destination_id,dn.title as destination_title," + strField + " as destination_filename,d.folder_destination";
            sqlCommand = sqlCommand + " from tbl_destination d,tbl_destination_name dn";
            sqlCommand = sqlCommand + " where d.destination_id=dn.destination_id and dn.lang_id="+langID+" and d.destination_id="+detail.DestinationID;
            sqlCommand = sqlCommand + " and d.status=1";
            sqlCommand = sqlCommand + " order by dn.title";

            //HttpContext.Current.Response.Write(sqlCommand);
            //HttpContext.Current.Response.Flush();
            //HttpContext.Current.Response.End();
            SqlDataReader reader = myConn.GetDataReader(sqlCommand);

            while (reader.Read())
            {
                //HttpContext.Current.Response.Write(reader["destination_id"]+"<br>");
                LocationList.Add(new LocationLink
                {
                    DestinationID = (short)reader["destination_id"],
                    DestinationTitle = reader["destination_title"].ToString(),
                    DestinationFileName = reader["destination_filename"].ToString(),
                    //LocationID = (short)reader["location_id"],
                    //LocationTitle = reader["location_title"].ToString(),
                    //LocationFileName = reader["location_filename"].ToString(),
                    FolderDestination = reader["folder_destination"].ToString()
                    //FolderLocation = reader["folder_location"].ToString()
                });
            }
            myConn.Close();
        }
        public void LoadLocationLink() 
        {
            LocationList = new List<LocationLink>();
            DataConnect myConn = new DataConnect();

            string sqlCommand = "select d.destination_id,dn.title as destination_title,dn.file_name as destination_filename,l.location_id,ln.title as location_title,ln.file_name as location_filename,d.folder_destination,l.folder_location";
            sqlCommand=sqlCommand+" from tbl_destination d,tbl_destination_name dn,tbl_location l,tbl_location_name ln";
            sqlCommand = sqlCommand + " where d.destination_id=dn.destination_id and d.destination_id=l.destination_id and l.location_id=ln.location_id and dn.lang_id="+_langID+" and ln.lang_id="+_langID;
            sqlCommand=sqlCommand+" and d.status=1 and l.status=1";
            //sqlCommand = sqlCommand + " and (select count(pl.product_id) from tbl_product_location pl where pl.location_id=l.location_id)>0";
            //sqlCommand = sqlCommand + " order by d.destination_id,l.location_id";
            sqlCommand = sqlCommand + " order by dn.title,ln.title";

            //HttpContext.Current.Response.Write(sqlCommand);
            //HttpContext.Current.Response.Flush();
            //HttpContext.Current.Response.End();
            SqlDataReader reader = myConn.GetDataReader(sqlCommand);

            while(reader.Read())
            {
                //HttpContext.Current.Response.Write(reader["destination_id"]+"<br>");
                LocationList.Add(new LocationLink
                { 
                DestinationID=(short)reader["destination_id"],
                DestinationTitle = reader["destination_title"].ToString(),
                DestinationFileName=reader["destination_filename"].ToString(),
                LocationID = (short)reader["location_id"],
                LocationTitle = reader["location_title"].ToString(),
                LocationFileName = reader["location_filename"].ToString(), 
                FolderDestination=reader["folder_destination"].ToString(),
                FolderLocation=reader["folder_location"].ToString()
                });
            }
            myConn.Close();
        }

        public void LoadLocationLink(short locationID) 
        {
            LocationList = new List<LocationLink>();
            DataConnect myConn = new DataConnect();

            string sqlCommand = "select d.destination_id,dn.title as destination_title,dn.file_name as destination_filename,l.location_id,ln.title as location_title,ln.file_name as location_filename,d.folder_destination,l.folder_location";
            sqlCommand=sqlCommand+" from tbl_destination d,tbl_destination_name dn,tbl_location l,tbl_location_name ln";
            sqlCommand=sqlCommand+" where d.destination_id=dn.destination_id and d.destination_id=l.destination_id and l.location_id=ln.location_id and dn.lang_id=1 and ln.lang_id=1";
            sqlCommand = sqlCommand + " and l.location_id=" + locationID;

            //HttpContext.Current.Response.Write(sqlCommand);
            //HttpContext.Current.Response.Flush();
            //HttpContext.Current.Response.End();
            SqlDataReader reader = myConn.GetDataReader(sqlCommand);

            if(reader.Read())
            {
                //HttpContext.Current.Response.Write(reader["destination_id"]+"<br>");
                LocationList.Add(new LocationLink
                { 
                DestinationID=(short)reader["destination_id"],
                DestinationTitle = reader["destination_title"].ToString(),
                DestinationFileName=reader["destination_filename"].ToString(),
                LocationID = (short)reader["location_id"],
                LocationTitle = reader["location_title"].ToString(),
                LocationFileName = reader["location_filename"].ToString(), 
                FolderDestination=reader["folder_destination"].ToString(),
                FolderLocation=reader["folder_location"].ToString()
                });
            }
            myConn.Close();
        }

        
        public void LoadDestinationLink(int CategoryID)
        {
            string strField = "";
            //This function is not include location
            switch(CategoryID)
            {
                case 29:
                    strField = "dn.file_name";
                    break;
                case 32:
                    strField = "dn.file_name_golf";
                    break;
                case 34:
                    strField = "dn.file_name_day_trip";
                    break;
                case 36:
                    strField = "dn.file_name_water_activity";
                    break;
                case 38:
                    strField = "dn.file_name_show_event";
                    break;
                case 39:
                    strField = "dn.file_name_health_check_up";
                    break;
                case 40:
                    strField = "dn.file_name_spa";
                    break;
            }
           
            LocationList = new List<LocationLink>();
            DataConnect myConn = new DataConnect();

            string sqlCommand = "select d.destination_id,dn.title as destination_title,"+strField+" as destination_filename,d.folder_destination";
            sqlCommand=sqlCommand+" from tbl_destination d,tbl_destination_name dn";
            sqlCommand=sqlCommand+" where d.destination_id=dn.destination_id and dn.lang_id="+_langID;
            sqlCommand=sqlCommand+" and d.status=1";
            sqlCommand = sqlCommand + " order by dn.title";

            //HttpContext.Current.Response.Write(sqlCommand);
            //HttpContext.Current.Response.Flush();
            //HttpContext.Current.Response.End();
            SqlDataReader reader = myConn.GetDataReader(sqlCommand);

            while (reader.Read())
            {
                //HttpContext.Current.Response.Write(reader["destination_id"]+"<br>");
                LocationList.Add(new LocationLink
                {
                    DestinationID = (short)reader["destination_id"],
                    DestinationTitle = reader["destination_title"].ToString(),
                    DestinationFileName = reader["destination_filename"].ToString(),
                    //LocationID = (short)reader["location_id"],
                    //LocationTitle = reader["location_title"].ToString(),
                    //LocationFileName = reader["location_filename"].ToString(),
                    FolderDestination = reader["folder_destination"].ToString()
                    //FolderLocation = reader["folder_location"].ToString()
                });
            }
            myConn.Close();
        }

        public string RenderLocationList(short destinationID)
        {
            LinkGenerator link = new LinkGenerator();
            link.LoadData(_langID);

            string listDisplay = "<ul class=\"landmarks_colleft\">";
            foreach(LocationLink item in LocationList)
            {
                if(item.DestinationID==destinationID){
                    if(_langID==1)
                    {
                        listDisplay=listDisplay+"<li class=\"header_top\"><a href=\"/"+item.LocationFileName+"\">"+item.LocationTitle+" Hotels</a></li>";
                    }else{
                        listDisplay=listDisplay+"<li class=\"header_top\"><a href=\"/"+item.LocationFileName+"\">โรงแรมใน"+item.LocationTitle+"</a></li>";
                    }
                    
                }
            }
            listDisplay = listDisplay + "</ul>";
            return listDisplay;
        }

        public string countData()
        {
            return LocationList.Count.ToString();
        }
        public string[,] GetProductType(byte productTypeID)
        {
            
            return Utility.GetProductType(productTypeID);
           
        }

        public LocationLink getDestination(short destinationID)
        {
            LocationLink locationItem=null ;

            foreach(LocationLink item in LocationList)
            {
                //HttpContext.Current.Response.Write(item.DestinationID+"<br>");
                if(item.DestinationID==destinationID)
                {
                    locationItem = item;
                    //HttpContext.Current.Response.Write("hello");
                    break;
                }
            }

            return locationItem;
        }

        public LocationLink getLocation(short locationID)
        {
            LocationLink locationItem = null;

            foreach (LocationLink item in LocationList)
            {
                if (item.LocationID == locationID)
                {
                    locationItem = item;
                }
            }

            return locationItem;
        }

        //other page ex. thailand-why-choose
        public string GenNavigator(string PageNavigator)
        {
            return navDisplay + " &gt; " + PageNavigator;
        }

        //category ex. thailand-hotels
        public string GenNavigator(byte productTypeID)
        {
            string result=string.Empty;
            if(_langID==1)
            {
            result=navDisplay + " &gt; Thailand " + GetProductType(productTypeID)[0, 1];
            }else{
            result=navDisplay + " &gt; " + GetProductType(productTypeID)[0, 4]+"ในประเทศไทย";
            }
            return result;

        }
        // destination category ex. bangkok-hotels
        public string GenNavigator(byte productTypeID,short destinationID)
        {
            LocationLink destinationItem = getDestination(destinationID);
            string[,] productTypeItem = GetProductType(productTypeID);
            string destinationPath = string.Empty;
            string result = string.Empty;

            destinationPath = productTypeItem[0, 2];
            if(_langID==1)
            {
                result=navDisplay + " &gt; " + "<a href=\"" + urlDomain + "/" + destinationPath + "\"> Thailand " + productTypeItem[0, 1] + "</a> &gt; " + destinationItem.DestinationTitle + " " + productTypeItem[0, 1];
            }else{
                destinationPath = destinationPath.Replace(".asp", "-th.asp");
                result=navDisplay + " &gt; " + "<a href=\"" + urlDomainTH + "/" + destinationPath + "\">" + productTypeItem[0, 4] + "ในประเทศไทย</a> &gt; " + productTypeItem[0, 4]+"ใน"+destinationItem.DestinationTitle;
            }

            return result;
            //return "";

        }

        // location category ex. bangkok-hotels
        public string GenNavigator(byte productTypeID, short destinationID,short locationID)
        {
            string[,] productTypeItem = GetProductType(productTypeID);
            string result = string.Empty;

            LocationLink destinationItem = getLocation(locationID);
            if(_langID==1)
            {
                result = navDisplay + " &gt; " + "<a href=\"" + productTypeItem[0, 2] + "\"> Thailand " + productTypeItem[0, 1] + "</a> &gt; " + "<a href=\"" + destinationItem.DestinationFileName + "\">" + destinationItem.DestinationTitle + " " + productTypeItem[0, 1] + "</a> &gt; " + destinationItem.LocationTitle + " " + productTypeItem[0, 1];
            }else{

                result = navDisplay + " &gt; " + "<a href=\"" + productTypeItem[0, 2].Replace(".asp", "-th.asp") + "\">" + productTypeItem[0, 4] + "ในประเทศไทย</a> &gt; " + "<a href=\"" + destinationItem.DestinationFileName + "\">" + productTypeItem[0, 4] + "ใน" + destinationItem.DestinationTitle + "</a> &gt; " + productTypeItem[0, 4] + "ใน" + destinationItem.LocationTitle;
            }

            return result;

        }

        // product ex. ramada-hotel
        public string GenNavigator(byte productTypeID,short locationID,string productTitle)
        {
            //HttpContext.Current.Response.Write(locationID + "<br>");
            //HttpContext.Current.Response.Flush();
            string result = string.Empty;
            string[,] productTypeItem = GetProductType(productTypeID);
            LocationLink locationItem = getLocation(locationID);

            if(_langID==1)
            {
                result=navDisplay + " &gt; " + "<a href=\"" + urlDomain + "/" + productTypeItem[0, 2] + "\">Thailand " + productTypeItem[0, 1] + "</a> &gt; " + "<a href=\"" + urlDomain + "/" + locationItem.DestinationFileName + "\">" + locationItem.DestinationTitle + " " + productTypeItem[0, 1] + "</a> &gt; <a href=\"" + urlDomain + "/" + locationItem.LocationFileName + "\">" + locationItem.LocationTitle + " " + productTypeItem[0, 1] + "</a> > " + productTitle;
            }else{
                result = navDisplay + " &gt; " + "<a href=\"" + urlDomainTH + "/" + productTypeItem[0, 2].Replace(".asp", "-th.asp") + "\">" + productTypeItem[0, 4] + "ในประเทศไทย</a> &gt; " + "<a href=\"" + urlDomain + "/" + locationItem.DestinationFileName + "\">" + productTypeItem[0, 4] + "ใน" + locationItem.DestinationTitle + "</a> &gt; <a href=\"" + urlDomain + "/" + locationItem.LocationFileName + "\">" + productTypeItem[0, 4] + "ใน" + locationItem.LocationTitle + "</a> > " + productTitle;
            }

            return result;

        }
        // other product ex. golf,spa,day trip
        public string GenNavigatorOtherProduct(byte productTypeID, short destinationID,string productTitle)
        {
            string result = string.Empty;
            LocationLink destinationItem = getDestination(destinationID);
            string[,] productTypeItem = GetProductType(productTypeID);
            if(_langID==1)
            {
                result=navDisplay + " &gt; " + "<a href=\"" + urlDomain + "/" + productTypeItem[0, 2] + "\">" + productTypeItem[0, 1] + "</a> &gt; <a href=\"" + urlDomain + "/" + destinationItem.DestinationFileName + "\">" + destinationItem.DestinationTitle + " " + productTypeItem[0, 1] + "</a> &gt; " + productTitle;
            }else{
                result = navDisplay + " &gt; " + "<a href=\"" + urlDomainTH + "/" + productTypeItem[0, 2].Replace(".asp", "-th.asp") + "\">" + productTypeItem[0, 4] + "ในประเทศไทย</a> &gt; <a href=\"" + urlDomain + "/" + destinationItem.DestinationFileName + "\">" + productTypeItem[0, 4] + "ใน" + destinationItem.DestinationTitle + "</a> &gt; " + productTitle;
            }
            return result;
            //return "";

        }

        public string GenNavigatorWriteReview(byte productTypeID, short locationID, string productUrl)
        {
            string[,] productTypeItem = GetProductType(productTypeID);
            LocationLink locationItem = getLocation(locationID);
            string result = string.Empty;

            if(_langID==1)
            {
                navDisplay = navDisplay + " &gt; " + "<a href=\"" + urlDomain + "/" + productTypeItem[0, 2] + "\">Thailand " + productTypeItem[0, 1] + "</a> &gt; " + "<a href=\"" + urlDomain + "/" + locationItem.DestinationFileName + "\">" + locationItem.DestinationTitle + " " + productTypeItem[0, 1] + "</a> &gt;";
                if (productTypeID == 29)
                {
                    navDisplay = navDisplay + "<a href=\"" + urlDomain + "/" + locationItem.LocationFileName + "\">" + locationItem.LocationTitle + " " + productTypeItem[0, 1] + "</a> &gt;";
                }
                navDisplay = navDisplay + productUrl + "&gt; Write Review";
            }else{
                navDisplay = navDisplay + " &gt; " + "<a href=\"" + urlDomain + "/" + productTypeItem[0, 2] + "\">" + productTypeItem[0, 1] + "ในประเทศไทย</a> &gt; " + "<a href=\"" + urlDomain + "/" + locationItem.DestinationFileName + "\">" + productTypeItem[0, 1] + "ใน" + locationItem.DestinationTitle + "</a> &gt;";
                if (productTypeID == 29)
                {
                    navDisplay = navDisplay + "<a href=\"" + urlDomain + "/" + productTypeItem[0, 4] + "\">" + locationItem.LocationTitle + "ใน" + locationItem.LocationFileName + "</a> &gt;";
                }
                navDisplay = navDisplay + productUrl + "&gt; เขียนรีวิว";
            }

            

            return navDisplay;
        }


        // product link from list
        public string GenProductLink(byte productTypeID, short locationID, string ProductTitle, string Url)
        {
            string[,] productTypeItem = GetProductType(productTypeID);
            LocationLink locationItem = getLocation(locationID);

            return "http://www.hotels2thailand.com/";
        }
    }
}