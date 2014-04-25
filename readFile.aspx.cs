using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Hotels2thailand.Front;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;
using System.Collections;
public partial class vtest_readFile : System.Web.UI.Page
{


    public void GenOtherPage()
    {
        StreamReader objReader = new StreamReader(Server.MapPath("../weblayout/PageLayout/layout_detail2.html"));
        string read = objReader.ReadToEnd();

        StreamReader objReaderContent = new StreamReader(Server.MapPath("../weblayout/PageLayout/why_choose_detail_en.html"));
        string readContent = objReaderContent.ReadToEnd();
        WebPageRoot webData = new WebPageRoot();
        Navigator nav = new Navigator();
        nav.LoadLocationLink();
        read = read.Replace("<!--###HeaderInclude###-->", webData.AddHeader());
        read = read.Replace("<!--###ContentInclude###-->", readContent);
        read = read.Replace("<!--###NavigatorInclude###-->", nav.GenNavigator("Why Choose Hotels2thailand"));

        read = read.Replace("<!--###PopularProductInclude###-->", webData.AddPopularProductByDestination(30, 29));
        GenerateFile gf = new GenerateFile(Server.MapPath("../weblayout/"), "thailand-hotels-why-choose.html", read);
        gf.CreateFile();

        objReaderContent.Close();
        objReader.Close();
    }
    public string GetProductRate(ProductDetail detail)
    {

        Layout vlayout = new Layout();

        string rateTable = "<div id=\"content_rate\">";
        rateTable = rateTable + "<div class=\"currency\">  CURRENCY</div>";
        rateTable = rateTable + "<div class=\"curren_form\">";
        rateTable = rateTable + "<form>";
        rateTable = rateTable + "<select name=\"test\" id=\"test\"> <option value=\"1\">-----Thailand Baht----</option> <option value=\"1\">-----Thailand Baht----</option> </select>";
        rateTable = rateTable + "</form>";
        rateTable = rateTable + "</div>";
        rateTable = rateTable + "<div class=\"chang_date\"><a href=\"#\"></a></div>";
        rateTable = rateTable + "<div class=\"clear-all\"> </div>";
        rateTable = rateTable + "<div id=\"RoomPeriod\"></div>";
        rateTable = rateTable + "</div>";

        string layout = vlayout.GetLayoutProductDetail(detail.ProductID,1);
        layout = layout.Replace("<!--###HotelTabInclude###-->", detail.RenderTab(1));
        layout = layout.Replace("<!--###RateInclude###-->", rateTable);
        return layout;
    }

    public string GenProductReview()
    {
        return "";
    }
    public string GenProductWhy(ProductDetail detail)
    {
        Layout vlayout = new Layout();

        StreamReader objReader = new StreamReader(HttpContext.Current.Server.MapPath("../weblayout/PageLayout/why_choose_detail_en.html"));
        string read = objReader.ReadToEnd();

        string layout = vlayout.GetLayoutProductDetail(detail.ProductID,2);
        layout = layout.Replace("<!--###HotelTabInclude###-->", detail.RenderTab(5));
        layout = layout.Replace("<!--###RateInclude###-->", read);
        return layout;
    }
    public string GenProductContact(ProductDetail detail)
    {
        Layout vlayout = new Layout();

        StreamReader objReader = new StreamReader(HttpContext.Current.Server.MapPath("../weblayout/PageLayout/contact_us_en.html"));
        string read = objReader.ReadToEnd();

        string layout = vlayout.GetLayoutProductDetail(detail.ProductID,2);
        layout = layout.Replace("<!--###HotelTabInclude###-->", detail.RenderTab(6));
        layout = layout.Replace("<!--###RateInclude###-->", read);
        return layout;
    }

    public string GenProductGallery()
    {
        return "";
    }

    public void GenIndexPage()
    {
        Layout vlayout = new Layout();
        string layout = vlayout.GetLayoutHome();

        GenerateFile gf;

        gf = new GenerateFile(Server.MapPath("../"), "index.html",layout);
        gf.CreateFile();

    }

    public string GenProductInformation(ProductDetail detail)
    {

        Layout vlayout = new Layout();
        ProductRelate productRelate = new ProductRelate();

        //Get Facility
        string sqlCommand = "select fac_id,title from tbl_facility_product";
        sqlCommand = sqlCommand + " where product_id=" + detail.ProductID + " and lang_id=1";

        DataConnect objConn = new DataConnect();
        SqlDataReader reader = objConn.GetDataReader(sqlCommand);
        

       

       
        //End Get Facility

        string content = "<div id=\"content_why\">\n";
        //--box info
        content = content + "<div id=\"why_us_header\"><h4>Hotels Information</h4></div>\n";
            content = content + "<div id=\"info\">\n";
            content = content + "<div class=\"info_print\">\n";
            content = content + "<a href=\"#\" class=\"info_map\">Map & Location</a>\n";
            content = content + "<a href=\"#\" class=\"info_free\">Free Discount Coupon </a>\n";
            content = content + "<a href=\"#\" class=\"info_tell\">Tell your friend </a>\n";
            content = content + "<a href=\"#\" class=\"info_book\">Bookmark </a>\n";
            content = content + "<a href=\"#\" class=\"info_view\">View this page as PDF</a>\n";
            content = content + "<a href=\"#\" class=\"info_version\">Print Version</a>\n";
            content = content + "<br class=\"clear-all\" />\n";
            content = content + "</div><br />\n";

            content = content + "<h4>LOCATION : CENTARA DUANGTAWAN HOTEL CHIANG MAI </h4>\n";
            content = content + "<p></p>\n";

            content = content + "<h4>SERVICES & RECREATIONS : CENTARA DUANGTAWAN HOTEL CHIANG MAI </h4>\n";
            content = content + "<div id=\"service_bg\"> <div class=\"service\">Service </div> </div><!--bg-->\n"; 

            content=content+"<ul>";
            while(reader.Read())
            {
                content = content + "<li>" + reader["title"] + "</li>\n";
            }
            content=content+"</ul>";

            content = content + "<br class=\"clear-all\" />\n";

            content = content + productRelate.RenderLocationRelate(detail);
			content=content+"</div><!--info-->"; 
            
            content=content+"<div class=\"backtotop_r\"><a href=\"#\">Back to top</a></div>\n";
            content = content + "</div>";
            string layout = vlayout.GetLayoutProductDetail(detail.ProductID,2);
            layout = layout.Replace("<!--###HotelTabInclude###-->", detail.RenderTab(1));
            layout = layout.Replace("<!--###InformationInclude###-->", content);
            objConn.Close();
            return layout;
    }
    

    public void GenPageProduct(int productID,byte categoryID)
    {
        ProductDetail detail = new ProductDetail(productID);
        detail.Load();

        LinkGenerator link = new LinkGenerator();

        string pathDestination = link.GetFolderPath(detail.DestinationID, categoryID);
        

        if (Directory.Exists(pathDestination))
        {
            Response.Write(pathDestination);
        }
        else
        {
            DirectoryInfo di = Directory.CreateDirectory(pathDestination);
            //Response.Write("Create Directory Successful");
        }

        GenerateFile gf;

        gf = new GenerateFile(pathDestination, detail.FileMain, GetProductRate(detail));
        gf.CreateFile();

        gf = new GenerateFile(pathDestination, detail.FileWhy, GenProductWhy(detail));
        gf.CreateFile();

        gf = new GenerateFile(pathDestination, detail.FileInfo, GenProductInformation(detail));
        gf.CreateFile();

    }
    
    public void GenProductList()
    {
        
        Layout vlayout = new Layout();
        string layout = vlayout.GetLayoutProductList(30, 29);

        ProductList list=new ProductList();
       List<ProductList> lists=list.GetProductList(30,0,29,DateTime.Now,DateTime.Now);

       string content = "<ul id=\"hotel_lsit\">";
       

       content = content + list.RenderList(); l

       content = content + "</ul>";

       layout = layout.Replace("<!--###BoxSortInclude###-->",vlayout.GetBoxSort());
       layout = layout.Replace("<!--###includeList###-->", content);

        //LinkGenerator link = new LinkGenerator();
        //string pathDestination = link.GetDestinationPath();

       GenerateFile gf;
       gf = new GenerateFile(Server.MapPath("../"), "bangkok-hotels.asp", layout);
       gf.CreateFile();

       Response.Write(layout);

    }

    public void GenBrowsePage()
    {
        ProductDetail detail = new ProductDetail(66);
        detail.Load();

        Layout vlayout = new Layout();
        string layout = vlayout.GetLayoutBrowse();
        ProductList list = new ProductList();
        list.MaxRecord = 6;
        List<ProductList> lists = list.GetProductList(30, 0, 29, DateTime.Now, DateTime.Now);
        string content = string.Empty;

        foreach (ProductList item in lists)
        {
                content = content + item.RenderItemFeature(item,detail);
        }

        layout = layout.Replace("<!--###FeatureInclude###-->", content);

        int countRow = 0;
        content = "";
        foreach (ProductList item in lists)
        {
            if((countRow%2)==0){
                content = content + item.RenderPopular(item,detail, 0);
            }else{
                content = content + item.RenderPopular(item,detail, 1);
            }
            countRow = countRow + 1;
        }
        layout = layout.Replace("<!--###PopularInclude###-->", content);
        Response.Write(layout);
    }
    public void GenProductDestinationList()
    {
        Layout vlayout = new Layout();
        string layout = vlayout.GetLayoutProductList(31, 29);

        StreamReader objReader = new StreamReader(HttpContext.Current.Server.MapPath("../weblayout/PageLayout/destinationList.html"));
        string read = objReader.ReadToEnd();
       

        LinkGenerator location = new LinkGenerator();
        //List<LocationLink> locationList = location.GetLocationList(29);

        int countLocation = 58;
        int columnMax = 4;
        int piece = countLocation % columnMax;
        int itemCol = (int)(countLocation / columnMax);
        int itemTotal = 0;

        string data="<div id=\"tophotel_footer\">";
        data = data + "<h3>All Hotels in Bangkok</h3>";         
        data = data + "</div>";
     	data = data + "<div id=\"content_tophotel_footer_b\">";
        for (int countCol = 1; countCol <= columnMax;countCol++ )
        {
            data = data + "<ul class=\"all_col_"+countCol+"\">";
            if (piece != 0)
            {
                itemTotal = itemCol + 1;
                piece = piece - 1;
            }
            else {
                itemTotal = itemCol;
            }

            for (int itemCount = 1; itemCount <= itemTotal;itemCount++ )
            {
                data = data + "<li class=\"ball_green_b\">  <a href=\"#\">13 Coins Airport Grand Resort Bangkok</a></li>";
            }
            data = data + "</ul>";
        }
        data = data + "<div class=\"clear-all\"></div>";
        data = data + "</div>";

        data = data + "</div><!--popular_list--> \n";
        data = data + "<div class=\"bg_down_colrigh\"></div>\n";
        data = data + "</div>\n";
        
        read=read.Replace("<!--###DestinationListInclude###-->",data);

        layout = layout.Replace("<!--###includeList###-->", read);
        objReader.Close();
        GenerateFile gf;

        gf = new GenerateFile(Server.MapPath("../"), "thailand-hotels.asp", layout);
        gf.CreateFile();
    }
    public void GenWhyPriceLow()
    { 
        Layout vlayout = new Layout();
        string layout = vlayout.GetLayoutOtherPage();
         StreamReader objReader = new StreamReader(HttpContext.Current.Server.MapPath("../weblayout/PageLayout/content_price_low.html"));

        string read = objReader.ReadToEnd();
        layout = layout.Replace("<!--###ContentInclude###-->", read);
        objReader.Close();
        Response.Write(layout);
    
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        //string destID = Request.Form["destinationID"];

        DataConnect objConn = new DataConnect();
        string sqlCommand = "select p.product_id";
        sqlCommand = sqlCommand + " from tbl_product p,tbl_product_location pl";
        sqlCommand = sqlCommand + " where p.product_id=pl.product_id and p.destination_id IN (30) and p.cat_id=29";

        SqlDataReader reader = objConn.GetDataReader(sqlCommand);
        while (reader.Read())
        {
            //Response.Write(reader["product_id"]+"<br>");
            GenPageProduct((int)reader["product_id"], 29);
        }


        //GenProductDestinationList();
        //Session["first"] = "visa";
       // Response.Write(Session["first"]);
        //GenProductList();
        //GenPageProduct(2745,29);
        //GenIndexPage();
        //GenBrowsePage();
        //GenWhyPriceLow();
    }
}