using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using Hotels2thailand;
using Hotels2thailand.Production;
using Hotels2thailand.DataAccess;
using System.Data.SqlClient;
using System.IO;
using System.Web.Configuration;
using Hotels2thailand.ProductOption;
public partial class book : System.Web.UI.Page
{
    private string connString = WebConfigurationManager.ConnectionStrings["booking2hotelXConnectionString"].ConnectionString;
    private byte langID = 1;
    private bool has_allotment = true;
    public string GetProductTitle(int OptionID, List<PriceBase> PriceList)
    {
        string result = string.Empty;
        foreach (PriceBase item in PriceList)
        {
            if (item.OptionID == OptionID)
            {
                result = item.OptionTitle;
            }
        }
        return result;
    }
    public string GetOptionImage(int OptionID, List<PriceBase> PriceList)
    {
        string result = string.Empty;
        foreach (PriceBase item in PriceList)
        {
            if (item.OptionID == OptionID)
            {
                result = item.OptionPicture;
            }
        }
        return result;
    }
    public string GetProductExtraTitle(int OptionID, List<ExtranetPriceBase> PriceList)
    {
        string result = string.Empty;
        foreach (ExtranetPriceBase item in PriceList)
        {
            if (item.OptionID == OptionID)
            {
                result = item.OptionTitle;
            }
        }
        return result;
    }

    public string GetOptionImageExtra(int OptionID, List<ExtranetPriceBase> PriceList)
    {
        string result = string.Empty;
        foreach (ExtranetPriceBase item in PriceList)
        {
            if (item.OptionID == OptionID)
            {
                result = item.OptionPicture;
            }
        }
        return result;
    }
    public string GetOtherProductTitle(int OptionID, List<PriceBase> PriceList)
    {
        string result = string.Empty;
        foreach (PriceBase item in PriceList)
        {
            if (item.OptionID == OptionID)
            {
                result = item.ProductTitle;
            }
        }
        return result;
    }
    public string GetOtherProductExtraTitle(int OptionID, List<ExtranetPriceBase> PriceList)
    {
        string result = string.Empty;
        foreach (ExtranetPriceBase item in PriceList)
        {
            if (item.OptionID == OptionID)
            {
                result = item.ProductTitle;
            }
        }
        return result;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Write("<h1>We are in the process of updating the system temporarily.</h2>");
        //Response.End();
        string pathName = string.Empty;
        langID = byte.Parse(Request.Form["ln"]);
        if (langID == 1)
        {
            //pathName = HttpContext.Current.Server.MapPath("/hotels-template/bluehouse/booking_for_engine.html");
            pathName = HttpContext.Current.Server.MapPath("/Layout/booking.html");
        }
        else
        {
            pathName = HttpContext.Current.Server.MapPath("/Layout/booking-th.html");
        }

        

        

        

        byte categoryID = byte.Parse(Request.Form["cat_id"]);
        string ConditionSelect = Request.Form["ddPrice"];
        string ExtraSelect = Request.Form["ddPriceExtra"];
        string adult = Request.Form["adult"];
        string child = Request.Form["child"];
        double discountPrice = double.Parse(Request.Form["discount"]);
        string RoomResult = string.Empty;
        string selectOption = string.Empty;
        int ProductID = int.Parse(Request.Form["hotel_id"]);

        string HotelUrl = "";
        string HotelFolder = "";
        string HotelHeader = "";
        byte ManageID = 0;
        byte paymentMethod = 2;
        bool IsMember = bool.Parse(Request.Form["mm"]);
            
        using (SqlConnection cn = new SqlConnection(connString))
        {
            string strSql = "select product_id,booking_type_id,folder,web_site_name,manage_id from tbl_product_booking_engine where product_id=" + ProductID;
            SqlCommand cmd = new SqlCommand(strSql, cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            
            HotelUrl = reader["web_site_name"].ToString();
            HotelFolder = reader["folder"].ToString();
            ManageID=(byte)reader["manage_id"];
            paymentMethod = (byte)reader["booking_type_id"];
        }

        StreamReader objReader = new StreamReader(pathName);
        string layout = objReader.ReadToEnd();       
        objReader.Close();
        pathName = HttpContext.Current.Server.MapPath("/hotels-template/" + HotelFolder + "/header.html");

        objReader = new StreamReader(pathName);
        HotelHeader = objReader.ReadToEnd();
        objReader.Close();
        

        

        
        decimal totalPriceDeposit = 0;
        
        decimal vatInclude = Convert.ToDecimal(1.177);

        DateTime dateStart = Hotels2thailand.Hotels2DateTime.Hotels2DateSplitYear(Request.Form["date_start"], "-");
        DateTime dateEnd;
        if (categoryID == 29)
        {
            dateEnd = Hotels2thailand.Hotels2DateTime.Hotels2DateSplitYear(Request.Form["date_end"], "-");
        }
        else
        {
            dateEnd = dateStart.AddDays(1);

        }
        bool checkExtranet = false;

        using (SqlConnection cn = new SqlConnection(connString))
        {
            SqlCommand cmd = new SqlCommand("select extranet_active from tbl_product where product_id=" + ProductID, cn);
            cn.Open();
            checkExtranet = (bool)cmd.ExecuteScalar();
        }
        FrontProductDeposit productDeposit = new FrontProductDeposit(ProductID);
        productDeposit.GetDeposit(dateStart);



        fnCurrency currency = new fnCurrency();
        currency.GetCurrency();
        string[] condition = null;
        int[] conditionID = null;
        int[] optionID = null;
        int[] quantity = null;
        int[] promotionID = null;
        int[] maxAdult = null;
        if (!string.IsNullOrEmpty(ConditionSelect))
        {
            condition = ConditionSelect.Split(',');

            int countSelect = ConditionSelect.Count();
             conditionID = new int[countSelect];
             optionID = new int[countSelect];
             quantity = new int[countSelect];
             promotionID = new int[countSelect];
             maxAdult = new int[countSelect];
        }


        GatewayMethod objGateway = new GatewayMethod(ProductID);
        byte gatewayID = objGateway.GetGateway();

        FrontPayLater payLater = new FrontPayLater();
        payLater.GetPayLaterByDate(ProductID, dateStart);
        string ProductTitle = string.Empty;
        string Address = string.Empty;
        string imagePath = string.Empty;
        decimal totalAbf = 0;
        using (SqlConnection cn = new SqlConnection(connString))
        {

            string sqlCommand = "select p.product_code,pc.title,";
            sqlCommand = sqlCommand + " (select top 1 spc.title from tbl_product_content spc where spc.product_id=p.product_id and spc.lang_id=" + langID + ") as second_lang,";
            sqlCommand = sqlCommand + " (select top 1 spc.address from tbl_product_content spc where spc.product_id=p.product_id and spc.lang_id=" + langID + ") as address_second_lang,";
            sqlCommand = sqlCommand + " pc.address,p.cat_id,";
            sqlCommand = sqlCommand + " (select top 1 spp.pic_code from tbl_product_pic spp where spp.product_id=p.product_id and spp.cat_id=1 and spp.type_id=6 and spp.status=1) as picture";
            sqlCommand = sqlCommand + " from tbl_product p,tbl_product_content pc";
            sqlCommand = sqlCommand + " where p.product_id=pc.product_id and pc.lang_id=1 and p.product_id=" + ProductID;
            SqlCommand cmd = new SqlCommand(sqlCommand, cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            if (langID == 1)
            {
                ProductTitle = reader["title"].ToString();
                Address = reader["address"].ToString();
            }
            else
            {
                ProductTitle = reader["second_lang"].ToString();
                Address = reader["address_second_lang"].ToString();
                if (string.IsNullOrEmpty(ProductTitle))
                {
                    ProductTitle = reader["title"].ToString();
                    Address = reader["address"].ToString();
                }
            }

            imagePath = "<img src=\"http://www.hotels2thailand.com/thailand-" + Utility.GetProductType((byte)reader["cat_id"])[0, 1] + "-pic/" + reader["product_code"] + "_1.jpg\"/>";

            if (!File.Exists(HttpContext.Current.Server.MapPath("/thailand-" + Utility.GetProductType((byte)reader["cat_id"])[0, 1] + "-pic/" + reader["product_code"] + "_1.jpg")))
            {
                imagePath = "";
            }

            if (!string.IsNullOrEmpty(reader["picture"].ToString()))
            {
                imagePath = "<img src=\"" + reader["picture"] + "\" alt=\"" + reader["title"] + "\">";
            }
        }


        //General Process

        List<ProductPolicy> policyList = null;
        List<FrontCancellationPolicy> cancellationList = null;
        ProductPolicy policy = null;
        FrontCancellationPolicy cancalation = null;
        ProductPrice priceProduct = null;
        List<PriceBase> PriceListBase = null;
        List<PriceBase> OptionTitleList = null;
        PriceSupplement priceSupplement = null;
        ProductPolicyExtranet policyExtra = null;
        List<CancellationExtranet> cancellationListExtra = null;
        List<ProductPolicyExtranet> policyListExtra = null;
        CancellationExtranet cancelationExtra = null;
        FrontProductPriceExtranet priceProductExtra = null;
        List<ExtranetPriceBase> PriceListBaseExtra = null;
        List<ExtranetPriceBase> OptionTitleListExtra = null;


        if (!checkExtranet)
        {

            policy = new ProductPolicy();
            policy.LangID = langID;
            policy.DateCheck = dateStart;
            policyList = policy.GetProductPolicy(ProductID);

            cancalation = new FrontCancellationPolicy(ProductID, dateStart);
            cancellationList = cancalation.LoadCancellationPolicyByCondition();

            priceProduct = new ProductPrice(ProductID, categoryID, dateStart, dateEnd);
            priceProduct.DiscountPrice = discountPrice;
            priceProduct.LoadPrice();

            PriceListBase = priceProduct.GetPriceBase();
            OptionTitleList = PriceListBase;

        }
        else
        {
            priceProduct = new ProductPrice(ProductID, categoryID, dateStart, dateEnd);
            priceProduct.DiscountPrice = discountPrice;

            cancelationExtra = new CancellationExtranet(ProductID, dateStart);
            cancellationListExtra = cancelationExtra.GetCancellation();

            policyExtra = new ProductPolicyExtranet();
            policyExtra.LangID = langID;
            policyListExtra = policyExtra.GetExtraPolicy(ProductID, 1);

            priceProductExtra = new FrontProductPriceExtranet(ProductID, categoryID, dateStart, dateEnd);
            priceProductExtra.DiscountPrice = discountPrice;
            priceProductExtra.LoadPrice();
            if(IsMember)
            {
                priceProductExtra.memberAuthen = true;
            }
            PriceListBaseExtra = priceProductExtra.GetPriceBase();
            OptionTitleListExtra = PriceListBaseExtra;

        }

        priceSupplement = new PriceSupplement();
        priceSupplement.LoadPriceSupplementByProductID(ProductID);
        //End General Process

        layout = layout.Replace("<!--###ProductTitle###-->",ProductTitle);


        switch (categoryID)
        {
            case 29:
                if (langID == 1)
                {
                    RoomResult = RoomResult + "<div id=\"bookingDisplay\">\n";
                    RoomResult = RoomResult + "<p id=\"productTitle\" class=\"fnLarge\">\n";
                    RoomResult = RoomResult + ProductTitle;
                    RoomResult = RoomResult + "</p>\n";
                    RoomResult = RoomResult + "<span id=\"productAddress\" class=\"fnGrayLight\">Address : " + Address + "</span>\n";
                    RoomResult = RoomResult + "<div id=\"bookingCheck\">\n";
                    RoomResult = RoomResult + "<span id=\"bookingSummaryTitle\" class=\"fnBig fnOrange\">Booking Summary</span>\n";
                    RoomResult = RoomResult + "<ul id=\"bookingCheckIn\">\n";
                    RoomResult = RoomResult + "<li><strong>Check in:</strong><br />" + dateStart.ToString("dddd,MMMM dd, yyyy") + "</li>\n";
                    RoomResult = RoomResult + "<li><strong>Check out:</strong><br />" + dateEnd.ToString("dddd,MMMM dd, yyyy") + "</li>\n";
                    RoomResult = RoomResult + "<li class=\"bookingGuest\"><strong>Adult:</strong><br />" + adult + "</li>\n";
                    RoomResult = RoomResult + "<li class=\"bookingGuest\"><strong>Child:</strong><br />" + child + "</li>\n";
                    RoomResult = RoomResult + "</ul>\n";
                    RoomResult = RoomResult + "<br class=\"clearAll\" />\n";
                    RoomResult = RoomResult + "</div>\n";
                }
                else
                {
                    RoomResult = RoomResult + "<div id=\"bookingDisplay\">\n";
                    RoomResult = RoomResult + "<p id=\"productTitle\" class=\"fnLarge\">\n";
                    RoomResult = RoomResult + ProductTitle;
                    RoomResult = RoomResult + "</p>\n";
                    RoomResult = RoomResult + "<span id=\"productAddress\" class=\"fnGrayLight\">ที่อยู่ : " + Address + "</span>\n";
                    RoomResult = RoomResult + "<div id=\"bookingCheck\">\n";
                    RoomResult = RoomResult + "<span id=\"bookingSummaryTitle\" class=\"fnBig fnOrange\">รายละเอียดการจอง</span>\n";
                    RoomResult = RoomResult + "<ul id=\"bookingCheckIn\">\n";
                    RoomResult = RoomResult + "<li><strong>เช็คอิน:</strong><br />" + dateStart.ToString("dddd,MMMM dd, yyyy") + "</li>\n";
                    RoomResult = RoomResult + "<li><strong>เช็คเอ้าท์:</strong><br />" + dateEnd.ToString("dddd,MMMM dd, yyyy") + "</li>\n";
                    RoomResult = RoomResult + "<li class=\"bookingGuest\"><strong>จำนวนผู้ใหญ่:</strong><br />" + adult + "</li>\n";
                    RoomResult = RoomResult + "<li class=\"bookingGuest\"><strong>จำนวนเด็ก:</strong><br />" + child + "</li>\n";
                    RoomResult = RoomResult + "</ul>\n";
                    RoomResult = RoomResult + "<br class=\"clearAll\" />\n";
                    RoomResult = RoomResult + "</div>\n";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_pic\">" + imagePath + "</div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_detail\">";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_name\">" + ProductTitle + "</div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_address\">" + ProductTitle + " ที่อยู่ : " + Address + "</div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_in\"><span>เช็คอิน :</span>&nbsp;&nbsp;" + dateStart.ToString("dddd,MMMM dd, yyyy") + " </div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_adult\"><span>จำนวนผู้ใหญ่ :</span>&nbsp;    " + adult + "</div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_in\"><span>เช็คเอ้าท์ :</span>" + dateEnd.ToString("dddd,MMMM dd, yyyy") + "  </div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_adult\"><span>จำนวนเด็ก :</span>    " + child + "</div>";
                    //RoomResult = RoomResult + "</div> ";
                    //RoomResult = RoomResult + "<div class=\"clear-all\"></div> ";
                }

                break;
            case 32:
                if (langID == 1)
                {


                    RoomResult = RoomResult + "<div id=\"bookingDisplay\">\n";
                    RoomResult = RoomResult + "<p id=\"productTitle\" class=\"fnLarge\">\n";
                    RoomResult = RoomResult + ProductTitle;
                    RoomResult = RoomResult + "</p>\n";
                    RoomResult = RoomResult + "<span id=\"productAddress\" class=\"fnGrayLight\">Address : " + Address + "</span>\n";
                    RoomResult = RoomResult + "<div id=\"bookingCheck\">\n";
                    RoomResult = RoomResult + "<span id=\"bookingSummaryTitle\" class=\"fnBig fnOrange\">Booking Summary</span>\n";
                    RoomResult = RoomResult + "<ul id=\"bookingCheckIn\">\n";
                    RoomResult = RoomResult + "<li class=\"otherProduct\"><strong>Tee-Off Date and Time:</strong><br />" + dateStart.ToString("dddd,MMMM dd, yyyy") + " " + ("0" + Request.Form["tee_hour"]).StringRight(2) + ":" + ("0" + Request.Form["tee_min"]).StringRight(2) + "</li>\n";
                    RoomResult = RoomResult + "</ul>\n";
                    RoomResult = RoomResult + "<br class=\"clearAll\" />\n";
                    RoomResult = RoomResult + "</div>\n";


                }
                else
                {
                    RoomResult = RoomResult + "<div id=\"bookingDisplay\">\n";
                    RoomResult = RoomResult + "<p id=\"productTitle\" class=\"fnLarge\">\n";
                    RoomResult = RoomResult + ProductTitle;
                    RoomResult = RoomResult + "</p>\n";
                    RoomResult = RoomResult + "<span id=\"productAddress\" class=\"fnGrayLight\">ที่่อยู่ : " + Address + "</span>\n";
                    RoomResult = RoomResult + "<div id=\"bookingCheck\">\n";
                    RoomResult = RoomResult + "<span id=\"bookingSummaryTitle\" class=\"fnBig fnOrange\">รายละเอียดการจอง</span>\n";
                    RoomResult = RoomResult + "<ul id=\"bookingCheckIn\">\n";
                    RoomResult = RoomResult + "<li class=\"otherProduct\"><strong>วันที่เวลาออกรอบ:</strong><br />" + dateStart.ToString("dddd,MMMM dd, yyyy") + " " + ("0" + Request.Form["tee_hour"]).StringRight(2) + ":" + ("0" + Request.Form["tee_min"]).StringRight(2) + "</li>\n";
                    RoomResult = RoomResult + "</ul>\n";
                    RoomResult = RoomResult + "<br class=\"clearAll\" />\n";
                    RoomResult = RoomResult + "</div>\n";
                }

                break;
            case 34:
            case 36:
                if (langID == 1)
                {
                    //RoomResult = RoomResult + "<div class=\"book_hotel_pic\">" + imagePath + "</div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_detail\">";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_name\">" + ProductTitle + "</div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_address\">" + ProductTitle + " Address : " + Address + "</div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_in\"><span>Trip Date: </span>&nbsp;&nbsp;" + dateStart.ToString("dddd,MMMM dd, yyyy") + " </div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_adult\">&nbsp;    </div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_in\"></div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_adult\"></div>";
                    //RoomResult = RoomResult + "</div> ";
                    //RoomResult = RoomResult + "<div class=\"clear-all\"></div> ";

                    RoomResult = RoomResult + "<div id=\"bookingDisplay\">\n";
                    RoomResult = RoomResult + "<p id=\"productTitle\" class=\"fnMedium\">\n";
                    RoomResult = RoomResult + ProductTitle;
                    RoomResult = RoomResult + "</p>\n";
                    RoomResult = RoomResult + "<span id=\"productAddress\" class=\"fnGrayLight\">Address : " + Address + "</span>\n";
                    RoomResult = RoomResult + "<div id=\"bookingCheck\">\n";
                    RoomResult = RoomResult + "<span id=\"bookingSummaryTitle\" class=\"fnBig fnOrange\">Booking Summary</span>\n";
                    RoomResult = RoomResult + "<ul id=\"bookingCheckIn\">\n";
                    RoomResult = RoomResult + "<li class=\"otherProduct\"><strong>Trip Date:</strong><br />" + dateStart.ToString("dddd,MMMM dd, yyyy") + "</li>\n";
                    RoomResult = RoomResult + "</ul>\n";
                    RoomResult = RoomResult + "<br class=\"clearAll\" />\n";
                    RoomResult = RoomResult + "</div>\n";
                }
                else
                {
                    //RoomResult = RoomResult + "<div class=\"book_hotel_pic\">" + imagePath + "</div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_detail\">";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_name\">" + ProductTitle + "</div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_address\">" + ProductTitle + " ที่อยู่ : " + Address + "</div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_in\"><span>Trip Date: </span>&nbsp;&nbsp;" + dateStart.ToString("dddd,MMMM dd, yyyy") + " </div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_adult\">&nbsp;    </div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_in\"></div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_adult\"></div>";
                    //RoomResult = RoomResult + "</div> ";
                    //RoomResult = RoomResult + "<div class=\"clear-all\"></div> ";

                    RoomResult = RoomResult + "<div id=\"bookingDisplay\">\n";
                    RoomResult = RoomResult + "<p id=\"productTitle\" class=\"fnMedium\">\n";
                    RoomResult = RoomResult + ProductTitle;
                    RoomResult = RoomResult + "</p>\n";
                    RoomResult = RoomResult + "<span id=\"productAddress\" class=\"fnGrayLight\">ที่อยู่ : " + Address + "</span>\n";
                    RoomResult = RoomResult + "<div id=\"bookingCheck\">\n";
                    RoomResult = RoomResult + "<span id=\"bookingSummaryTitle\" class=\"fnBig fnOrange\">Booking Summary</span>\n";
                    RoomResult = RoomResult + "<ul id=\"bookingCheckIn\">\n";
                    RoomResult = RoomResult + "<li class=\"otherProduct\"><strong>Trip Date:</strong><br />" + dateStart.ToString("dddd,MMMM dd, yyyy") + "</li>\n";
                    RoomResult = RoomResult + "</ul>\n";
                    RoomResult = RoomResult + "<br class=\"clearAll\" />\n";
                    RoomResult = RoomResult + "</div>\n";
                }

                break;
            case 38:
                if (langID == 1)
                {
                    //RoomResult = RoomResult + "<div class=\"book_hotel_pic\">" + imagePath + "</div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_detail\">";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_name\">" + ProductTitle + "</div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_address\">" + ProductTitle + " Address : " + Address + "</div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_in\"><span>Show or Event Date :</span>&nbsp;&nbsp;" + dateStart.ToString("dddd,MMMM dd, yyyy") + " </div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_adult\">&nbsp;    </div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_in\"></div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_adult\"></div>";
                    //RoomResult = RoomResult + "</div> ";
                    //RoomResult = RoomResult + "<div class=\"clear-all\"></div> ";

                    RoomResult = RoomResult + "<div id=\"bookingDisplay\">\n";
                    RoomResult = RoomResult + "<p id=\"productTitle\" class=\"fnMedium\">\n";
                    RoomResult = RoomResult + ProductTitle;
                    RoomResult = RoomResult + "</p>\n";
                    RoomResult = RoomResult + "<span id=\"productAddress\" class=\"fnGrayLight\">Address : " + Address + "</span>\n";
                    RoomResult = RoomResult + "<div id=\"bookingCheck\">\n";
                    RoomResult = RoomResult + "<span id=\"bookingSummaryTitle\" class=\"fnBig fnOrange\">Booking Summary</span>\n";
                    RoomResult = RoomResult + "<ul id=\"bookingCheckIn\">\n";
                    RoomResult = RoomResult + "<li class=\"otherProduct\"><strong>Show or Event Date:</strong><br />" + dateStart.ToString("dddd,MMMM dd, yyyy") + "</li>\n";
                    RoomResult = RoomResult + "</ul>\n";
                    RoomResult = RoomResult + "<br class=\"clearAll\" />\n";
                    RoomResult = RoomResult + "</div>\n";
                }
                else
                {
                    //RoomResult = RoomResult + "<div class=\"book_hotel_pic\">" + imagePath + "</div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_detail\">";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_name\">" + ProductTitle + "</div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_address\">" + ProductTitle + " ที่อยู่ : " + Address + "</div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_in\"><span>Show or Event Date :</span>&nbsp;&nbsp;" + dateStart.ToString("dddd,MMMM dd, yyyy") + " </div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_adult\">&nbsp;    </div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_in\"></div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_adult\"></div>";
                    //RoomResult = RoomResult + "</div> ";
                    //RoomResult = RoomResult + "<div class=\"clear-all\"></div> ";
                    RoomResult = RoomResult + "<div id=\"bookingDisplay\">\n";
                    RoomResult = RoomResult + "<p id=\"productTitle\" class=\"fnMedium\">\n";
                    RoomResult = RoomResult + ProductTitle;
                    RoomResult = RoomResult + "</p>\n";
                    RoomResult = RoomResult + "<span id=\"productAddress\" class=\"fnGrayLight\">ที่อยู่ : " + Address + "</span>\n";
                    RoomResult = RoomResult + "<div id=\"bookingCheck\">\n";
                    RoomResult = RoomResult + "<span id=\"bookingSummaryTitle\" class=\"fnBig fnOrange\">Booking Summary</span>\n";
                    RoomResult = RoomResult + "<ul id=\"bookingCheckIn\">\n";
                    RoomResult = RoomResult + "<li class=\"otherProduct\"><strong>Show or Event Date:</strong><br />" + dateStart.ToString("dddd,MMMM dd, yyyy") + "</li>\n";
                    RoomResult = RoomResult + "</ul>\n";
                    RoomResult = RoomResult + "<br class=\"clearAll\" />\n";
                    RoomResult = RoomResult + "</div>\n";
                }


                break;
            case 39:
                if (langID == 1)
                {
                    //RoomResult = RoomResult + "<div class=\"book_hotel_pic\">" + imagePath + "</div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_detail\">";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_name\">" + ProductTitle + "</div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_address\">" + ProductTitle + " Address : " + Address + "</div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_in\"><span>Date:</span>&nbsp;&nbsp;" + dateStart.ToString("dddd,MMMM dd, yyyy") + " </div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_adult\">&nbsp;    </div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_in\"></div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_adult\"></div>";
                    //RoomResult = RoomResult + "</div> ";
                    //RoomResult = RoomResult + "<div class=\"clear-all\"></div> ";

                    RoomResult = RoomResult + "<div id=\"bookingDisplay\">\n";
                    RoomResult = RoomResult + "<p id=\"productTitle\" class=\"fnMedium\">\n";
                    RoomResult = RoomResult + ProductTitle;
                    RoomResult = RoomResult + "</p>\n";
                    RoomResult = RoomResult + "<span id=\"productAddress\" class=\"fnGrayLight\">Address : " + Address + "</span>\n";
                    RoomResult = RoomResult + "<div id=\"bookingCheck\">\n";
                    RoomResult = RoomResult + "<span id=\"bookingSummaryTitle\" class=\"fnBig fnOrange\">Booking Summary</span>\n";
                    RoomResult = RoomResult + "<ul id=\"bookingCheckIn\">\n";
                    RoomResult = RoomResult + "<li class=\"otherProduct\"><strong>Date:</strong><br />" + dateStart.ToString("dddd,MMMM dd, yyyy") + "</li>\n";
                    RoomResult = RoomResult + "</ul>\n";
                    RoomResult = RoomResult + "<br class=\"clearAll\" />\n";
                    RoomResult = RoomResult + "</div>\n";
                }
                else
                {
                    //RoomResult = RoomResult + "<div class=\"book_hotel_pic\">" + imagePath + "</div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_detail\">";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_name\">" + ProductTitle + "</div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_address\">" + ProductTitle + " Address : " + Address + "</div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_in\"><span>Date:</span>&nbsp;&nbsp;" + dateStart.ToString("dddd,MMMM dd, yyyy") + " </div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_adult\">&nbsp;    </div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_in\"></div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_adult\"></div>";
                    //RoomResult = RoomResult + "</div> ";
                    //RoomResult = RoomResult + "<div class=\"clear-all\"></div> ";

                    RoomResult = RoomResult + "<div id=\"bookingDisplay\">\n";
                    RoomResult = RoomResult + "<p id=\"productTitle\" class=\"fnMedium\">\n";
                    RoomResult = RoomResult + ProductTitle;
                    RoomResult = RoomResult + "</p>\n";
                    RoomResult = RoomResult + "<span id=\"productAddress\" class=\"fnGrayLight\">ที่อยู่ : " + Address + "</span>\n";
                    RoomResult = RoomResult + "<div id=\"bookingCheck\">\n";
                    RoomResult = RoomResult + "<span id=\"bookingSummaryTitle\" class=\"fnBig fnOrange\">Booking Summary</span>\n";
                    RoomResult = RoomResult + "<ul id=\"bookingCheckIn\">\n";
                    RoomResult = RoomResult + "<li class=\"otherProduct\"><strong>Date:</strong><br />" + dateStart.ToString("dddd,MMMM dd, yyyy") + "</li>\n";
                    RoomResult = RoomResult + "</ul>\n";
                    RoomResult = RoomResult + "<br class=\"clearAll\" />\n";
                    RoomResult = RoomResult + "</div>\n";
                }

                break;
            case 40:
                if (langID == 1)
                {
                    //RoomResult = RoomResult + "<div class=\"book_hotel_pic\">" + imagePath + "</div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_detail\">";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_name\">" + ProductTitle + "</div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_address\">" + ProductTitle + " Address : " + Address + "</div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_in\"><span>Date:</span>&nbsp;&nbsp;" + dateStart.ToString("dddd,MMMM dd, yyyy") + " </div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_adult\">&nbsp;    </div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_in\"></div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_adult\"></div>";
                    //RoomResult = RoomResult + "</div> ";
                    //RoomResult = RoomResult + "<div class=\"clear-all\"></div> ";

                    RoomResult = RoomResult + "<div id=\"bookingDisplay\">\n";
                    RoomResult = RoomResult + "<p id=\"productTitle\" class=\"fnMedium\">\n";
                    RoomResult = RoomResult + ProductTitle;
                    RoomResult = RoomResult + "</p>\n";
                    RoomResult = RoomResult + "<span id=\"productAddress\" class=\"fnGrayLight\">Address : " + Address + "</span>\n";
                    RoomResult = RoomResult + "<div id=\"bookingCheck\">\n";
                    RoomResult = RoomResult + "<span id=\"bookingSummaryTitle\" class=\"fnBig fnOrange\">Booking Summary</span>\n";
                    RoomResult = RoomResult + "<ul id=\"bookingCheckIn\">\n";
                    RoomResult = RoomResult + "<li class=\"otherProduct\"><strong>Date:</strong><br />" + dateStart.ToString("dddd,MMMM dd, yyyy") + "</li>\n";
                    RoomResult = RoomResult + "</ul>\n";
                    RoomResult = RoomResult + "<br class=\"clearAll\" />\n";
                    RoomResult = RoomResult + "</div>\n";
                }
                else
                {
                    //RoomResult = RoomResult + "<div class=\"book_hotel_pic\">" + imagePath + "</div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_detail\">";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_name\">" + ProductTitle + "</div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_address\">" + ProductTitle + " Address : " + Address + "</div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_in\"><span>Date:</span>&nbsp;&nbsp;" + dateStart.ToString("dddd,MMMM dd, yyyy") + " </div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_adult\">&nbsp;    </div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_in\"></div>";
                    //RoomResult = RoomResult + "<div class=\"book_hotel_adult\"></div>";
                    //RoomResult = RoomResult + "</div> ";
                    //RoomResult = RoomResult + "<div class=\"clear-all\"></div> ";

                    RoomResult = RoomResult + "<div id=\"bookingDisplay\">\n";
                    RoomResult = RoomResult + "<p id=\"productTitle\" class=\"fnMedium\">\n";
                    RoomResult = RoomResult + ProductTitle;
                    RoomResult = RoomResult + "</p>\n";
                    RoomResult = RoomResult + "<span id=\"productAddress\" class=\"fnGrayLight\">ที่อยู่ : " + Address + "</span>\n";
                    RoomResult = RoomResult + "<div id=\"bookingCheck\">\n";
                    RoomResult = RoomResult + "<span id=\"bookingSummaryTitle\" class=\"fnBig fnOrange\">Booking Summary</span>\n";
                    RoomResult = RoomResult + "<ul id=\"bookingCheckIn\">\n";
                    RoomResult = RoomResult + "<li class=\"otherProduct\"><strong>Date:</strong><br />" + dateStart.ToString("dddd,MMMM dd, yyyy") + "</li>\n";
                    RoomResult = RoomResult + "</ul>\n";
                    RoomResult = RoomResult + "<br class=\"clearAll\" />\n";
                    RoomResult = RoomResult + "</div>\n";
                }

                break;

        }
        bool hasRoomSelect = false;
        int promotionTmp = 0;
        string formDisplay = string.Empty;
        decimal priceTotal = 0;
        decimal priceSummary = 0;
        //decimal priceCalculate = 0;
        decimal priceOption = 0;
        decimal priceReal = 0;
        int roomNight = dateEnd.Subtract(dateStart).Days;
        decimal priceAverage = 0;
        decimal priceABF = 0;
        if (!string.IsNullOrEmpty(ConditionSelect))
        {
            for (int conditionCount = 0; conditionCount < condition.Count(); conditionCount++)
            {
                string[] item = condition[conditionCount].Split('_');
                for (int itemCount = 0; itemCount < item.Count(); itemCount++)
                {
                    conditionID[conditionCount] = Int32.Parse(item[0]);
                    optionID[conditionCount] = Int32.Parse(item[1]);
                    promotionID[conditionCount] = Int32.Parse(item[2]);
                    quantity[conditionCount] = Int32.Parse(item[6]);
                    maxAdult[conditionCount] = Int32.Parse(item[3]);
                }
                selectOption = selectOption + "<input type=\"hidden\" name=\"room_" + conditionID[conditionCount] + "_" + optionID[conditionCount] + "_" + promotionID[conditionCount] + "\" value=\"" + quantity[conditionCount] + "\">\n";
                if (promotionID[conditionCount] != promotionTmp)
                {
                    promotionTmp = promotionID[conditionCount];
                }

            }

            



            switch (categoryID)
            {
                case 29:
                    if (langID == 1)
                    {
                        RoomResult = RoomResult + "<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" id=\"bookingList\">\n";
                        RoomResult = RoomResult + "<tr><th>Option</th><th>Avg./Night</th><th>Night</th><th>Qty.</th><th>Total</th></tr>\n";
                    }
                    else
                    {
                        RoomResult = RoomResult + "<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" id=\"bookingList-th\">\n";
                        RoomResult = RoomResult + "<tr><th>ชนิดห้องพัก</th><th> ราคาเฉลี่ยต่อคืน </th><th> จำนวนคืน </th><th> จำนวนห้อง </th><th> ราคารวม </th></tr>\n";
                        //RoomResult = RoomResult + "<th>ชนิดของห้องพัก</th> <th>จำนวนห้อง</th> <th>จำนวนคืน</th> <th>ราคาเฉลี่ยต่อคืน</th> <th class=\"bold\">ราคารวม</th>\n";
                        //RoomResult = RoomResult + "<tr><th>ชนิดของห้องพัก</th><th>ราคาเฉลี่ยต่อคืน</th><th>จำนวนคืน</th><th>จำนวนห้อง</th><th>ราคารวม</th></tr>\n";
                        //RoomResult = RoomResult + "<tr><th>ชนิดของห้องพัก</th><th>ราคาเฉลี่ยต่อคืน</th><th>จำนวนคืน</th><th>จำนวนห้อง</th><th>ราคารวม</th></tr>\n";
                    }

                    break;
                default:
                    if (langID == 1)
                    {
                        RoomResult = RoomResult + "<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" id=\"bookingList\">\n";
                        RoomResult = RoomResult + "<tr><th>Trip</th><th>Price</th><th>&nbsp;</th><th>Quantity</th><th>Total</th></tr>\n";
                        //RoomResult = RoomResult + "<th>Trip</th><th width=\"141\">Quantity</th><th>Price</th><th>Subtotal</th>\n";
                    }
                    else
                    {
                        RoomResult = RoomResult + "<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" id=\"bookingList-th\">\n";
                        RoomResult = RoomResult + "<tr><th>โปรแกรม</th><th>ราคา</th><th>&nbsp;</th><th>จำนวน</th><th>ราคารวม</th></tr>\n";
                        //RoomResult = RoomResult + "<th>Trip</th><th width=\"141\">Quantity</th><th>Price</th><th>Subtotal</th>\n";
                    }

                    break;
            }





            string promotionTitle = string.Empty;
            string itemTitle = string.Empty;

            string policyDisplay = string.Empty;
            string policyContentDisplay = string.Empty;
            OptionPrice priceCalculate;

            Allotment objAllotment = null;
            objAllotment = new Allotment(int.Parse(Request.Form["hotel_id"]));

            string imgOption = string.Empty;

            /*
            Check for gala
             */

           

            for (int conditionCount = 0; conditionCount < conditionID.Count(); conditionCount++)
            {
                //Response.Write(quantity[conditionCount]+"<br>");
                if (quantity[conditionCount] != 0)
                {

                    if (!checkExtranet)
                    {

                        // priceCalculate = priceProduct.CalculateAll(conditionID[conditionCount], optionID[conditionCount], promotionID[conditionCount]).Price;
                        priceCalculate = priceProduct.CalculateAll(conditionID[conditionCount], optionID[conditionCount], promotionID[conditionCount]);
                        priceABF = Utility.ExcludeVat(priceProduct.PriceABF * quantity[conditionCount]);
                        totalAbf = 0;


                        priceOption = (int)(priceSupplement.GetPriceSupplement(dateStart, priceCalculate.PriceExcludeABF, conditionID[conditionCount], quantity[conditionCount]));
                        priceAverage = (int)((priceOption / quantity[conditionCount]) / roomNight);
                        priceReal = (priceAverage * quantity[conditionCount]) * roomNight;
                        priceTotal = priceTotal + priceReal + priceABF;
                        priceSummary = priceSummary + priceOption + priceABF;

                        if (priceProduct.CheckPromotionAccept(promotionID[conditionCount]))
                        {
                            promotionTitle = GetPromotionTitle(promotionID[conditionCount]);
                        }

                        policy.LangID = langID;
                        policyDisplay = policy.GetConditionPolicyList(policyList, conditionID[conditionCount], promotionTitle, cancellationList);
                        policyContentDisplay = policy.GetPolicyContent(policyList, cancellationList, conditionID[conditionCount]);
                        //policyDisplay = "<a href=\"javascript:void(0)\" class=\"tooltip\">" + policyDisplay;
                        //policyDisplay = policyDisplay + "<span class=\"tooltip_content\">" + policyContentDisplay + "</span>";
                        //policyDisplay = policyDisplay + "</a>\n";

                        //itemTitle = GetProductTitle(optionID[conditionCount], PriceListBase) + policyDisplay;
                        //policyDisplay = "<a href=\"javascript:void(0)\" class=\"tooltip\">View Condition";
                        //policyDisplay = policyDisplay + "<span class=\"tooltip_content\">" + policyContentDisplay + "</span>";
                        //policyDisplay = policyDisplay + "</a>\n";

                        //RoomResult = RoomResult + "<tr><td class=\"bookingListItem\">\n";
                        //RoomResult = RoomResult + "<img src=\"http://www.hotels2thailand.com" + GetOptionImage(optionID[conditionCount], PriceListBase) + "\" />\n";
                        //RoomResult = RoomResult + "<p class=\"OptionDetail\">\n";
                        //RoomResult = RoomResult + "<span class=\"fnBig fnBlack\">" + itemTitle + "</span>\n";
                        //RoomResult = RoomResult + "<span class=\"fnBlueSky\">" + promotionTitle + "</span>\n";

                        //RoomResult = RoomResult + "<span class=\"bookingOptionConditionView\">\n";
                        //RoomResult = RoomResult + policyDisplay;
                        //RoomResult = RoomResult + "</span>\n";
                        //RoomResult = RoomResult + "</p>\n";
                        //RoomResult = RoomResult + "<br class=\"clearAll\" />\n";
                        //RoomResult = RoomResult + "</td><td>THB " + priceAverage + "</td><td>" + roomNight + "</td><td>" + quantity[conditionCount] + "</td><td>THB " + priceReal + "</td></tr>\n";    

                        if (langID == 1)
                        {
                            policyDisplay = "<a href=\"javascript:void(0)\" data-reveal-id=\"myModal_" + conditionID[conditionCount] + "\">View Conditions";
                        }
                        else
                        {
                            policyDisplay = "<a href=\"javascript:void(0)\" class=\"tooltip\">รายละเอียด";
                        }


                        policyDisplay = policyDisplay + "<span  id=\"myModal_" + conditionID[conditionCount] + "\" class=\"reveal-modal\">" + policyContentDisplay + "</span>";
                        policyDisplay = policyDisplay + "</a>\n";

                        itemTitle = GetProductTitle(optionID[conditionCount], PriceListBase) + policyDisplay;
                        RoomResult = RoomResult + "<tr><td class=\"bookingListItem\">\n";
                        imgOption = GetOptionImage(optionID[conditionCount], PriceListBase);
                        if (!string.IsNullOrEmpty(imgOption))
                        {
                            RoomResult = RoomResult + "<img src=\"http://www.booking2hotels.com" + imgOption + "\" />\n";
                        }



                        RoomResult = RoomResult + "<div class=\"OptionDetail\">\n";

                        switch (categoryID)
                        {
                            case 29:
                                RoomResult = RoomResult + "<span class=\"fnBig fnBlack\">" + GetProductTitle(optionID[conditionCount], PriceListBase) + "</span>\n";
                                break;
                            default:
                                RoomResult = RoomResult + "<span class=\"fnSmall fnBlack\"><strong>" + GetProductTitle(optionID[conditionCount], PriceListBase) + "</strong></span><br/>\n";
                                break;
                        }


                        RoomResult = RoomResult + "<span class=\"fnBlueSky\">" + promotionTitle + "</span>\n";

                        RoomResult = RoomResult + "<span class=\"bookingOptionConditionView\">\n";
                        RoomResult = RoomResult + policyDisplay;
                        RoomResult = RoomResult + "</span>\n";
                        RoomResult = RoomResult + "</div>\n";
                        RoomResult = RoomResult + "<br class=\"clearAll\" />\n";

                        switch (categoryID)
                        {
                            case 29:
                                RoomResult = RoomResult + "</td><td>THB " + priceAverage.ToString("#,###") + "</td><td>" + roomNight + "</td><td>" + quantity[conditionCount] + "</td><td>THB " + priceReal.ToString("#,###") + "</td></tr>\n";
                                break;
                            default:
                                RoomResult = RoomResult + "</td><td>THB " + priceAverage.ToString("#,###") + "</td><td></td><td>" + quantity[conditionCount] + "</td><td>THB " + priceReal.ToString("#,###") + "</td></tr>\n";
                                break;
                        }
                        has_allotment = false;
                    }
                    else
                    {
                        //Room Calculate
                        hasRoomSelect = true;
                        priceCalculate = priceProductExtra.CalculateAll(conditionID[conditionCount], optionID[conditionCount], promotionID[conditionCount]);
                        priceABF = priceProductExtra.PriceABF * quantity[conditionCount];
                        totalAbf = 0;

                        priceOption = (int)(priceSupplement.GetPriceSupplement(dateStart, priceCalculate.PriceExcludeABF, conditionID[conditionCount], quantity[conditionCount]));
                        priceAverage = (int)((priceOption / quantity[conditionCount]) / roomNight);
                        priceReal = (priceAverage * quantity[conditionCount]) * roomNight;
                        priceTotal = priceTotal + priceReal + priceABF;
                        priceSummary = priceSummary + priceOption + priceABF;
                        List<string> promotionDetail = GetPromotionExtra(promotionID[conditionCount], 1);
                        if (priceProductExtra.CheckPromotionAccept(promotionID[conditionCount]))
                        {
                            promotionTitle = promotionDetail[0];
                            //policyDisplay = policy.GetConditionPolicyList(policyList, conditionID[conditionCount], promotionTitle, cancellationList);
                            policyDisplay = policyExtra.GetConditionPolicyList(policyListExtra, conditionID[conditionCount], promotionTitle);
                            if (bool.Parse(promotionDetail[1]))
                            {
                                //promotion has cancellation
                                policyContentDisplay = policyExtra.GetPolicyContent(policyListExtra, cancellationListExtra, conditionID[conditionCount], promotionID[conditionCount], promotionTitle, promotionDetail[2], bool.Parse(promotionDetail[1]));
                            }
                            else
                            {
                                policyContentDisplay = policyExtra.GetPolicyContent(policyListExtra, cancellationListExtra, conditionID[conditionCount], promotionID[conditionCount], promotionTitle, promotionDetail[2], bool.Parse(promotionDetail[1]));
                            }
                        }
                        else
                        {
                            policyDisplay = policyExtra.GetConditionPolicyList(policyListExtra, conditionID[conditionCount], "");
                            policyContentDisplay = policyExtra.GetPolicyContent(policyListExtra, cancellationListExtra, conditionID[conditionCount], 0, "", "", false);
                        }

                        //Response.Write(priceABF);
                        if (objAllotment.CheckAllotAvaliable(PriceListBaseExtra[0].SupplierPrice, optionID[conditionCount], quantity[conditionCount], dateStart, dateEnd))
                        {
                            has_allotment = has_allotment & true;
                        }
                        else
                        {
                            has_allotment = has_allotment & false;
                        }




                        if (langID == 1)
                        {
                            policyDisplay = "<a href=\"javascript:void(0)\"  data-reveal-id=\"myModal_" + conditionID[conditionCount] + "\">View Condition";
                        }
                        else
                        {
                            policyDisplay = "<a href=\"javascript:void(0)\" class=\"tooltip\">รายละเอียด";
                        }

                        policyDisplay = policyDisplay + "</a>\n";
                        policyDisplay = policyDisplay + "<span  id=\"myModal_" + conditionID[conditionCount] + "\" class=\"reveal-modal\">" + policyContentDisplay + "</span>";

                        itemTitle = GetProductExtraTitle(optionID[conditionCount], PriceListBaseExtra) + policyDisplay;
                        imgOption = GetOptionImageExtra(optionID[conditionCount], PriceListBaseExtra);

                        RoomResult = RoomResult + "<tr><td class=\"bookingListItem\">\n";
                        if (!string.IsNullOrEmpty(imgOption))
                        {
                            RoomResult = RoomResult + "<img src=\"http://www.booking2hotels.com" + imgOption + "\" />\n";
                        }

                        RoomResult = RoomResult + "<div class=\"OptionDetail\">\n";
                        RoomResult = RoomResult + "<span class=\"fnBig fnBlack\">" + GetProductExtraTitle(optionID[conditionCount], PriceListBaseExtra) + "</span>\n";
                        RoomResult = RoomResult + "<span class=\"fnBlueSky\">" + promotionTitle + "</span>\n";

                        RoomResult = RoomResult + "<span class=\"bookingOptionConditionView\">\n";
                        RoomResult = RoomResult + policyDisplay;
                        RoomResult = RoomResult + "</span>\n";
                        RoomResult = RoomResult + "</div>\n";
                        RoomResult = RoomResult + "<br class=\"clearAll\" />\n";
                        RoomResult = RoomResult + "</td><td>THB " + ((int)Utility.PriceExcludeVat(priceAverage)).ToString("#,###.##") + "</td><td>" + roomNight + "</td><td>" + quantity[conditionCount] + "</td><td>THB " + ((int)Utility.PriceExcludeVat(priceAverage * roomNight * quantity[conditionCount])).ToString("#,###.##") + "</td></tr>\n";

                    }

                    totalPriceDeposit = totalPriceDeposit + productDeposit.GetPriceDeposit(priceReal, quantity[conditionCount], roomNight);
                    //RoomResult = RoomResult + RenderItemList(itemTitle, quantity[conditionCount], dateEnd.Subtract(dateStart).Days, priceAverage, priceReal, priceABF, categoryID);
                }

            }
        }

        //Calculate Package
        string[] packageOption = new string[2];
        decimal ratePackage = 0;
        FrontOptionPackage objPackage = new FrontOptionPackage(ProductID, dateStart, dateEnd);
        //Response.Write(objPackage.GetPackageList().Count());

        switch (categoryID)
        {
            case 29:
                if (langID == 1)
                {
                    RoomResult = RoomResult + "<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" id=\"bookingList\">\n";
                    RoomResult = RoomResult + "<tr><th>Option</th><th>Avg./Night</th><th>Night</th><th>Qty.</th><th>Total</th></tr>\n";
                }
                else
                {
                    RoomResult = RoomResult + "<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" id=\"bookingList-th\">\n";
                    RoomResult = RoomResult + "<tr><th>ชนิดห้องพัก</th><th> ราคาเฉลี่ยต่อคืน </th><th> จำนวนคืน </th><th> จำนวนห้อง </th><th> ราคารวม </th></tr>\n";
                    //RoomResult = RoomResult + "<th>ชนิดของห้องพัก</th> <th>จำนวนห้อง</th> <th>จำนวนคืน</th> <th>ราคาเฉลี่ยต่อคืน</th> <th class=\"bold\">ราคารวม</th>\n";
                    //RoomResult = RoomResult + "<tr><th>ชนิดของห้องพัก</th><th>ราคาเฉลี่ยต่อคืน</th><th>จำนวนคืน</th><th>จำนวนห้อง</th><th>ราคารวม</th></tr>\n";
                    //RoomResult = RoomResult + "<tr><th>ชนิดของห้องพัก</th><th>ราคาเฉลี่ยต่อคืน</th><th>จำนวนคืน</th><th>จำนวนห้อง</th><th>ราคารวม</th></tr>\n";
                }

                break;
            default:
                if (langID == 1)
                {
                    RoomResult = RoomResult + "<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" id=\"bookingList\">\n";
                    RoomResult = RoomResult + "<tr><th>Trip</th><th>Price</th><th>&nbsp;</th><th>Quantity</th><th>Total</th></tr>\n";
                    //RoomResult = RoomResult + "<th>Trip</th><th width=\"141\">Quantity</th><th>Price</th><th>Subtotal</th>\n";
                }
                else
                {
                    RoomResult = RoomResult + "<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" id=\"bookingList-th\">\n";
                    RoomResult = RoomResult + "<tr><th>โปรแกรม</th><th>ราคา</th><th>&nbsp;</th><th>จำนวน</th><th>ราคารวม</th></tr>\n";
                    //RoomResult = RoomResult + "<th>Trip</th><th width=\"141\">Quantity</th><th>Price</th><th>Subtotal</th>\n";
                }

                break;
        }
        foreach (FrontOptionPackage itemPackage in objPackage.GetPackageList())
        {

            if (Request.Form["ddPackage_" + itemPackage.ConditionID + "_" + itemPackage.OptionID] != null)
            {
                packageOption = Request.Form["ddPackage_" + itemPackage.ConditionID + "_" + itemPackage.OptionID].Split('_');
                //Response.Write("ddPackage_" + itemPackage.ConditionID + "_" + itemPackage.OptionID + "<br>");
                if (int.Parse(packageOption[6]) != 0)
                {
                    hasRoomSelect = true;
                    ratePackage = Utility.PriceExcludeVat(itemPackage.Price);


                    selectOption = selectOption + "<input type=\"hidden\" name=\"ddPackage_" + itemPackage.ConditionID + "_" + itemPackage.OptionID + "\" value=\"" + int.Parse(packageOption[6]) + "\">\n";




                    //Response.Write(rateExtraOption* int.Parse(extraOption[6]));
                    priceTotal = priceTotal + itemPackage.Price * int.Parse(packageOption[6]);
                    priceSummary = priceSummary + itemPackage.Price * int.Parse(packageOption[6]) ;
                    //RoomResult = RoomResult + "<tr>\n";
                    //RoomResult = RoomResult + "<td colspan=\"2\" align=\"left\">" + GetProductExtraTitle(int.Parse(extraOption[1]), PriceListBaseExtra) + "</td>\n";
                    //RoomResult = RoomResult + "<td>" + extraOption[6] + "</td>\n";
                    //RoomResult = RoomResult + "<td>-</td>\n";
                    //RoomResult = RoomResult + "<td>-</td>\n";
                    //RoomResult = RoomResult + "<td>" + rateExtraOption * int.Parse(extraOption[6]) + " Baht</td>\n";
                    //RoomResult = RoomResult + "</tr>\n";

                    RoomResult = RoomResult + "<tr><td class=\"bookingListItem\">\n";
                    RoomResult = RoomResult + "<div class=\"OptionDetail\">\n";
                    RoomResult = RoomResult + "<span class=\"fnBig fnBlack\">" + itemPackage.OptionTitle + "</span>\n";
                    RoomResult = RoomResult + "</div>\n";
                    RoomResult = RoomResult + "<br class=\"clearAll\" />\n";
                    RoomResult = RoomResult + "</td><td></td><td></td><td>" + packageOption[6] + "</td><td>THB " + ((int)(ratePackage * int.Parse(packageOption[6]))).ToString("#,###.##") + "</td></tr>\n";
                    totalPriceDeposit = totalPriceDeposit + productDeposit.GetPriceDeposit(itemPackage.Price, int.Parse(packageOption[6]), roomNight);

                }
            }
        }
        //

        //Calculate Meal
        string[] mealOption = new string[2];
        decimal rateMeal = 0;
        FrontOptionMeal objMeal = new FrontOptionMeal(ProductID, dateStart, dateEnd);
        //Response.Write(objPackage.GetPackageList().Count());
        foreach (FrontOptionMeal itemMeal in objMeal.GetMealList())
        {

            if (Request.Form["ddMeal_" + itemMeal.ConditionID + "_" + itemMeal.OptionID] != null)
            {
                //Response.Write(Request.Form["ddMeal_" + itemMeal.ConditionID + "_" + itemMeal.OptionID]);
                //Response.Flush();
                mealOption = Request.Form["ddMeal_" + itemMeal.ConditionID + "_" + itemMeal.OptionID].Split('_');
                if (int.Parse(mealOption[6]) != 0)
                {
                    rateMeal = Utility.PriceExcludeVat(itemMeal.Price);


                    selectOption = selectOption + "<input type=\"hidden\" name=\"ddMeal_" + itemMeal.ConditionID + "_" + itemMeal.OptionID + "\" value=\"" + int.Parse(mealOption[6]) + "\">\n";




                    priceTotal = priceTotal + itemMeal.Price * int.Parse(mealOption[6]);
                    priceSummary = priceSummary + itemMeal.Price * int.Parse(mealOption[6]);
                    RoomResult = RoomResult + "<tr><td class=\"bookingListItem\">\n";
                    RoomResult = RoomResult + "<div class=\"OptionDetail\">\n";
                    RoomResult = RoomResult + "<span class=\"fnBig fnBlack\">" + itemMeal.OptionTitle + "</span>\n";
                    RoomResult = RoomResult + "</div>\n";
                    RoomResult = RoomResult + "<br class=\"clearAll\" />\n";
                    RoomResult = RoomResult + "</td><td></td><td></td><td>" + mealOption[6] + "</td><td>THB " + ((int)(rateMeal * int.Parse(mealOption[6]))).ToString("#,###.##") + "</td></tr>\n";
                    totalPriceDeposit = totalPriceDeposit + productDeposit.GetPriceDeposit(itemMeal.Price, int.Parse(mealOption[6]), roomNight);

                }
            }
        }
        //

        bool checkTransfer = false;
        decimal rateExtraOption = 0;

        String[] extraOption = new String[2];
        int optionTmp = 0;

        if (!checkExtranet)
        {
            PriceListBase = priceProduct.RateBase(2);



            foreach (PriceBase extraItem in PriceListBase)
            {

                if (extraItem.OptionID != optionTmp)
                {

                    if (Request.Form["ddPriceExtra_" + extraItem.ConditionID + "_" + extraItem.OptionID] != null)
                    {
                        extraOption = Request.Form["ddPriceExtra_" + extraItem.ConditionID + "_" + extraItem.OptionID].Split('_');

                        if (int.Parse(extraOption[6]) != 0)
                        {
                            rateExtraOption = (int)(extraItem.Rate / vatInclude);
                            if (extraItem.OptionCategoryID == 43 || extraItem.OptionCategoryID == 44)
                            {
                                checkTransfer = true;
                            }
                            selectOption = selectOption + "<input type=\"hidden\" name=\"ddPriceExtra_" + extraItem.ConditionID + "\" value=\"" + int.Parse(extraOption[6]) + "\">\n";


                            if (extraItem.OptionCategoryID != 43 || extraItem.OptionCategoryID != 44)
                            {

                                priceOption = priceProduct.CalculateAll(extraItem.ConditionID, extraItem.OptionID, 0).Price;

                                priceAverage = (int)((priceOption / Convert.ToInt32(extraOption[6])) / roomNight);
                                priceReal = (priceAverage * Convert.ToInt32(extraOption[6])) * roomNight;
                                priceTotal = priceTotal + priceReal;

                                priceTotal = priceTotal + rateExtraOption * int.Parse(extraOption[6]) * roomNight;

                                //RoomResult = RoomResult + "<tr>\n";
                                //RoomResult = RoomResult + "<td align=\"left\">" + GetProductTitle(int.Parse(extraOption[1]), PriceListBase) + "</td>\n";
                                //RoomResult = RoomResult + "<td>" + extraOption[6] + "</td>\n";
                                //RoomResult = RoomResult + "<td>" + roomNight + "</td>\n";
                                //RoomResult = RoomResult + "<td>-</td>\n";
                                //RoomResult = RoomResult + "<td>" + (rateExtraOption * int.Parse(extraOption[6]) * roomNight).ToString("#,###") + " Baht</td>\n";
                                //RoomResult = RoomResult + "</tr>\n";
                                RoomResult = RoomResult + "<tr><td class=\"bookingListItem\" colspan=\"2\">\n";
                                RoomResult = RoomResult + "<div class=\"OptionDetail\">\n";
                                RoomResult = RoomResult + "<span class=\"fnBig fnBlack\">" + GetProductTitle(int.Parse(extraOption[1]), PriceListBase) + "</span>\n";
                                RoomResult = RoomResult + "</div>\n";
                                RoomResult = RoomResult + "<br class=\"clearAll\" />\n";
                                RoomResult = RoomResult + "</td><td>" + roomNight + "</td><td>" + extraOption[6] + "</td><td>THB " + ((int)(rateExtraOption * int.Parse(extraOption[6]) * roomNight)).ToString("#,###") + "</td></tr>\n";
                                
                            }
                            else
                            {
                                priceTotal = priceTotal + rateExtraOption * int.Parse(extraOption[6]);
                                
                                //RoomResult = RoomResult + "<tr>\n";
                                //RoomResult = RoomResult + "<td colspan=\"2\" align=\"left\">" + GetProductTitle(int.Parse(extraOption[1]), PriceListBase) + "</td>\n";
                                //RoomResult = RoomResult + "<td>" + extraOption[6] + "</td>\n";
                                //RoomResult = RoomResult + "<td>-</td>\n";
                                //RoomResult = RoomResult + "<td>-</td>\n";
                                //RoomResult = RoomResult + "<td>" + rateExtraOption * int.Parse(extraOption[6]) + " Baht</td>\n";
                                //RoomResult = RoomResult + "</tr>\n";
                                RoomResult = RoomResult + "<tr><td class=\"bookingListItem\" colspan=\"2\">\n";
                                RoomResult = RoomResult + "<div class=\"OptionDetail\">\n";
                                RoomResult = RoomResult + "<span class=\"fnBig fnBlack\">" + GetProductTitle(int.Parse(extraOption[1]), PriceListBase) + "</span>\n";
                                RoomResult = RoomResult + "</div>\n";
                                RoomResult = RoomResult + "<br class=\"clearAll\" />\n";
                                RoomResult = RoomResult + "</td><td>" + roomNight + "</td><td>" + extraOption[6] + "</td><td>THB " + ((int)(rateExtraOption * int.Parse(extraOption[6]))).ToString("#,###") + "</td></tr>\n";

                            }
                        }
                    }
                }
                optionTmp = extraItem.OptionID;
            }
        }
        else
            
        {
            priceProductExtra.LoadExtraOptionPrice();
            PriceListBaseExtra = priceProductExtra.RateBase(2);

            foreach (ExtranetPriceBase extraItem in PriceListBaseExtra)
            {
                if (extraItem.OptionID != optionTmp)
                {

                    if (Request.Form["ddPriceExtra_" + extraItem.ConditionID + "_" + extraItem.OptionID] != null)
                    {
                        extraOption = Request.Form["ddPriceExtra_" + extraItem.ConditionID + "_" + extraItem.OptionID].Split('_');

                        if (int.Parse(extraOption[6]) != 0)
                        {
                            rateExtraOption = extraItem.Price;
                            //Response.Write(extraItem.ConditionID + " " + extraItem.OptionID+"<br>");
                            if (extraItem.OptionCategoryID == 43 || extraItem.OptionCategoryID == 44)
                            {
                                checkTransfer = true;
                            }

                            selectOption = selectOption + "<input type=\"hidden\" name=\"ddPriceExtra_" + extraItem.ConditionID + "\" value=\"" + int.Parse(extraOption[6]) + "\">\n";


                            if (extraItem.OptionCategoryID != 43 && extraItem.OptionCategoryID != 44)
                            {
                                priceOption = priceProductExtra.CalculateAll(extraItem.ConditionID, extraItem.OptionID, 0).Price * Convert.ToInt32(extraOption[6]);
                                
                                priceAverage = (int)((priceOption / Convert.ToInt32(extraOption[6])) / roomNight);
                                priceReal = (priceAverage * Convert.ToInt32(extraOption[6])*roomNight);

                                priceTotal = priceTotal + priceOption;
                                priceSummary = priceSummary + priceOption;
                               // priceTotal = priceTotal + rateExtraOption * int.Parse(extraOption[6]) * roomNight;
                                //RoomResult = RoomResult + "<tr>\n";
                                //RoomResult = RoomResult + "<td align=\"left\">" + GetProductExtraTitle(int.Parse(extraOption[1]), PriceListBaseExtra) + "</td>\n";
                                //RoomResult = RoomResult + "<td>" + extraOption[6] + "</td>\n";
                                //RoomResult = RoomResult + "<td>" + roomNight + "</td>\n";
                                //RoomResult = RoomResult + "<td>-</td>\n";
                                //RoomResult = RoomResult + "<td>" + (rateExtraOption * int.Parse(extraOption[6]) * roomNight).ToString("#,###") + " Baht</td>\n";
                                //RoomResult = RoomResult + "</tr>\n";

                                RoomResult = RoomResult + "<tr><td class=\"bookingListItem\">\n";
                                RoomResult = RoomResult + "<div class=\"OptionDetail\">\n";
                                RoomResult = RoomResult + "<span class=\"fnBig fnBlack\">" + GetProductExtraTitle(int.Parse(extraOption[1]), PriceListBaseExtra) + "</span>\n";
                                RoomResult = RoomResult + "</div>\n";
                                RoomResult = RoomResult + "<br class=\"clearAll\" />\n";
                                RoomResult = RoomResult + "</td><td></td><td>" + roomNight + "</td><td>" + extraOption[6] + "</td><td>" + ((int)Utility.PriceExcludeVat(priceReal)).ToString("#,###.##") + " Baht</td></tr>\n";
                                totalPriceDeposit = totalPriceDeposit + productDeposit.GetPriceDeposit(priceReal, byte.Parse(extraOption[6]), roomNight);
                                //Response.Write(extraItem.OptionCategoryID + "<br>");
                            }
                            else
                            {
                                //Response.Write(rateExtraOption* int.Parse(extraOption[6]));
                                priceTotal = priceTotal + rateExtraOption * int.Parse(extraOption[6]);
                                priceSummary = priceSummary + rateExtraOption * int.Parse(extraOption[6]) ;
                                //RoomResult = RoomResult + "<tr>\n";
                                //RoomResult = RoomResult + "<td colspan=\"2\" align=\"left\">" + GetProductExtraTitle(int.Parse(extraOption[1]), PriceListBaseExtra) + "</td>\n";
                                //RoomResult = RoomResult + "<td>" + extraOption[6] + "</td>\n";
                                //RoomResult = RoomResult + "<td>-</td>\n";
                                //RoomResult = RoomResult + "<td>-</td>\n";
                                //RoomResult = RoomResult + "<td>" + rateExtraOption * int.Parse(extraOption[6]) + " Baht</td>\n";
                                //RoomResult = RoomResult + "</tr>\n";

                                RoomResult = RoomResult + "<tr><td class=\"bookingListItem\">\n";
                                RoomResult = RoomResult + "<div class=\"OptionDetail\">\n";
                                RoomResult = RoomResult + "<span class=\"fnBig fnBlack\">" + GetProductExtraTitle(int.Parse(extraOption[1]), PriceListBaseExtra) + "</span>\n";
                                RoomResult = RoomResult + "</div>\n";
                                RoomResult = RoomResult + "<br class=\"clearAll\" />\n";
                                RoomResult = RoomResult + "</td><td></td><td></td><td>" + extraOption[6] + "</td><td>" + ((int)(Utility.ExcludeVat(rateExtraOption) * int.Parse(extraOption[6]))).ToString("#,###.##") + " Baht</td></tr>\n";
                                if (IsDepositFull(ProductID))
                                {
                                    totalPriceDeposit = totalPriceDeposit + rateExtraOption * int.Parse(extraOption[6]);
                                }
                                
                            }
                        }
                    }
                }
                optionTmp = extraItem.OptionID;
            }
        }

        

        if (!checkTransfer)
        {
            priceProduct.GetTransferFromOtherProduct(ProductID);
            PriceListBase = priceProduct.RateBase(3);
            foreach (PriceBase extraItem in PriceListBase)
            {
                if (Request.Form["ddPriceExtra_" + extraItem.ConditionID + "_" + extraItem.OptionID] != null)
                {

                    extraOption = Request.Form["ddPriceExtra_" + extraItem.ConditionID + "_" + extraItem.OptionID].Split('_');
                    if (int.Parse(extraOption[6]) != 0)
                    {
                        rateExtraOption = (int)(extraItem.Rate / vatInclude);
                        selectOption = selectOption + "<input type=\"hidden\" name=\"ddPriceExtra_" + extraItem.ConditionID + "\" value=\"" + int.Parse(extraOption[6]) + "\">\n";
                        priceTotal = priceTotal + rateExtraOption * int.Parse(extraOption[6]);
                        priceSummary = priceSummary + rateExtraOption + int.Parse(extraOption[6]);
                        //RoomResult = RoomResult + "<tr>\n";
                        //RoomResult = RoomResult + "<td align=\"left\">" + GetProductTitle(int.Parse(extraOption[1]), PriceListBase) + "</td>\n";
                        //RoomResult = RoomResult + "<td valign=\"middle\">" + extraOption[6] + "</td>\n";
                        //RoomResult = RoomResult + "<td valign=\"middle\">-</td>\n";
                        //RoomResult = RoomResult + "<td valign=\"middle\">-</td>\n";
                        //RoomResult = RoomResult + "<td valign=\"middle\" class=\"book_vat_sub\" align=\"right\">" + (rateExtraOption * int.Parse(extraOption[6])).ToString("#,###") + " Baht  </td>\n";
                        //RoomResult = RoomResult + "</tr>\n";

                        RoomResult = RoomResult + "<tr><td class=\"bookingListItem\">\n";
                        RoomResult = RoomResult + "<div class=\"OptionDetail\">\n";
                        RoomResult = RoomResult + "<span class=\"fnBig fnBlack\">" + GetProductTitle(int.Parse(extraOption[1]), PriceListBase) + "</span>\n";
                        RoomResult = RoomResult + "</div>\n";
                        RoomResult = RoomResult + "<br class=\"clearAll\" />\n";
                        RoomResult = RoomResult + "</td><td></td><td></td><td>" + extraOption[6] + "</td><td>" + ((int)(rateExtraOption * int.Parse(extraOption[6]) * roomNight)).ToString("#,###") + " Baht</td></tr>\n";

                        checkTransfer = true;
                    }
                }
            }
        }





        //Gala
        GalaDinner gala = new GalaDinner(ProductID, dateStart, dateEnd);
        List<GalaDinner> galaList = null;
        if (!checkExtranet)
        {
            galaList = gala.GetGala();
        }
        else
        {
            galaList = gala.GetGalaExtranet();
        }

        if (!hasRoomSelect)
        {
            galaList.Clear();
        }

        int numNightRoom = dateEnd.Subtract(dateStart).Days;
        int numNightGala = 0;

        DateTime dateCheck;
        DateTime dateGalaCheck;

        string GalaResult = "";
        
        GalaResult = GalaResult + "<tr>\n";
        if (langID == 1)
        {
            GalaResult = GalaResult + "<td class=\"bookingListItem\" colspan=\"4\" align=\"left\" class=\"gala\">Compulsory Meals\n";
        }
        else
        {
            GalaResult = GalaResult + "<td class=\"bookingListItem\" colspan=\"4\" align=\"left\" class=\"gala\">ห้องพักบังคับรวมมื้ออาหาร\n";
        }


        decimal priceGala = 0;

        foreach (GalaDinner item in galaList)
        {

            if (item.DefaultGala == 1)
            {
                // Select Only day
                if (item.RequireAdult)
                {
                    //Response.Write(priceTotal + "/" + item.Rate + "<br>");

                    priceTotal = priceTotal + item.Rate * Convert.ToDecimal(adult);
                    priceSummary = priceSummary + item.Rate * Convert.ToDecimal(adult);

                    priceGala = priceGala + Utility.ExcludeVat(item.Rate * Convert.ToDecimal(adult));

                    if (langID == 1)
                    {
                        GalaResult = GalaResult + "<p>" + item.Title + " — Adult " + ((int)(Utility.ExcludeVat(item.Rate) * Convert.ToDecimal(adult))) + "</p>\n";
                    }
                    else
                    {
                        GalaResult = GalaResult + "<p>" + item.Title + " — Adult " + ((int)(Utility.ExcludeVat(item.Rate) * Convert.ToDecimal(adult))) + "</p>\n";
                    }



                }

                if (item.RequireChild)
                {
                    if (byte.Parse(child) > 0)
                    {
                        priceTotal = priceTotal + item.Rate * Convert.ToDecimal(child);
                        priceSummary = priceSummary + item.Rate * Convert.ToDecimal(child);

                        priceGala = priceGala + Utility.ExcludeVat(item.Rate * Convert.ToDecimal(child));
                        GalaResult = GalaResult + "<p>" + item.Title + " — Child " + ((int)(Utility.ExcludeVat(item.Rate) * Convert.ToDecimal(child))) + "</p>\n";
                    }
                }

            }
            else
            {
                numNightGala = item.DateUseEnd.Subtract(dateStart).Days;
                // Select all day
                if (item.RequireAdult)
                {
                    //for (int countNightGala = 0; countNightGala < numNightGala;countNightGala++ )
                    //{
                    //dateGalaCheck = item.DateUseEnd.AddDays(countNightGala);
                    for (int countNight = 0; countNight < numNightRoom; countNight++)
                    {
                        dateCheck = dateStart.AddDays(countNight);
                        if (dateCheck.CompareTo(item.DateUseStart) >= 0 && item.DateUseEnd.CompareTo(dateCheck) >= 0)
                        {
                            priceTotal = priceTotal + item.Rate * Convert.ToDecimal(adult);
                            priceSummary = priceSummary + item.Rate * Convert.ToDecimal(adult);

                            priceGala = priceGala + Utility.ExcludeVat(item.Rate * Convert.ToDecimal(adult));
                            GalaResult = GalaResult + "<p>" + item.Title + " — Adult " + ((int)(Utility.ExcludeVat(item.Rate) * Convert.ToDecimal(adult))) + "</p>\n";
                        }
                    }
                    //}
                }
                if (item.RequireChild)
                {
                    if (byte.Parse(child) > 0)
                    {
                        for (int countNight = 0; countNight < numNightRoom; countNight++)
                        {
                            dateCheck = dateStart.AddDays(countNight);
                            if (dateCheck.CompareTo(item.DateUseStart) >= 0 && item.DateUseEnd.CompareTo(dateCheck) >= 0)
                            {
                                priceTotal = priceTotal + item.Rate * Convert.ToDecimal(child);
                                priceSummary = priceSummary + item.Rate * Convert.ToDecimal(child);

                                priceGala = priceGala + Utility.ExcludeVat(item.Rate * Convert.ToDecimal(child));
                                GalaResult = GalaResult + "<p>" + item.Title + " — Child " + ((int)(Utility.ExcludeVat(item.Rate) * Convert.ToDecimal(child))) + "</p>\n";
                            }
                        }
                    }
                }

            }
        }
        GalaResult = GalaResult + "</td>\n";
        GalaResult = GalaResult + "<td>" + priceGala + "</td>\n";
        GalaResult = GalaResult + "</tr>\n";

        if (priceGala != 0)
        {
            RoomResult = RoomResult + GalaResult;
        }

        RoomResult = RoomResult + "</table>\n";
        RoomResult = RoomResult + "<br />\n";
        if (langID == 1)
        {
            RoomResult = RoomResult + "&nbsp;&nbsp;<a href=\"javascript:void(0)\" onclick=\"history.back()\">&lt;&lt; Edit booking detail</a>\n";
        }
        else
        {
            RoomResult = RoomResult + "&nbsp;&nbsp;<a href=\"javascript:void(0)\" onclick=\"history.back()\">&lt;&lt; แก้ไขรายการที่จอง</a>\n";
        }

        RoomResult = RoomResult + "<br />\n";
        RoomResult = RoomResult + "<br class=\"clearAll\" /></div>\n";

        //decimal priceVatInc = priceTotal + totalAbf;
        decimal priceVatInc = priceSummary;

        string PriceSubTotalDisplay = "";
        string priceVatIncDisplay = "";
        string priceVatDisplay = "";
        string priceSummaryDisplay = "";

        if (currency.CurrencyID != 25)
        {
            PriceSubTotalDisplay = " (" + ((int)(priceTotal) / Convert.ToDecimal(currency.CurrencyPrefix)).ToString("#,###.##") + " " + currency.CurrencyCode + ")";
            priceVatIncDisplay = " (" + (priceVatInc / Convert.ToDecimal(currency.CurrencyPrefix)).ToString("#,###.##") + " " + currency.CurrencyCode + ")";
            priceVatDisplay = " (" + (((int)(priceVatInc) - (int)(priceTotal)) / Convert.ToDecimal(currency.CurrencyPrefix)).ToString("#,###.##") + " " + currency.CurrencyCode + ")";
            priceSummaryDisplay = " (" + (priceVatInc / Convert.ToDecimal(currency.CurrencyPrefix)).ToString("#,###.##") + " " + currency.CurrencyTitle + ")";
        }

        //Response.Write(totalPriceDeposit);
        //if (productDeposit.DepositCateID == 3)
        //{
        //    totalPriceDeposit = productDeposit.Deposit;
        //    if (totalPriceDeposit > priceVatInc)
        //    {
        //        totalPriceDeposit = priceVatInc;
        //    }
        //}
        //totalPriceDeposit = productDeposit.Deposit;
        if (totalPriceDeposit > priceVatInc)
        {
            totalPriceDeposit = priceVatInc;
        }
        string totalResult = string.Empty;

        string depositWord = string.Empty;
        string depositWordMini = string.Empty;

        if (totalPriceDeposit <= priceVatInc)
        {
            switch (productDeposit.DepositCateID)
            {
                case 1:
                    depositWord = productDeposit.Deposit + "% deposit in the amount of <span class=\"fnRed fnBig\">" + totalPriceDeposit.ToString("#,###.##") + " Thai Baht</span>";
                    depositWordMini = productDeposit.Deposit + "% deposit in the amount of <span class=\"fnRed fnMedium\">" + totalPriceDeposit.ToString("#,###.##") + " Thai Baht</span>";
                    break;

                case 2:
                    depositWord = productDeposit.Deposit + " night deposit in the amount of <span class=\"fnRed fnBig\">" + totalPriceDeposit.ToString("#,###.##") + " Thai Baht</span>";
                    break;

                case 3:
                    depositWord = " deposit in the amount of <span class=\"fnRed fnBig\">" + totalPriceDeposit.ToString("#,###.##") + " Thai Baht</span>";
                    depositWordMini = " deposit in the amount of <span class=\"fnRed fnMedium\">" + totalPriceDeposit.ToString("#,###.##") + " Thai Baht</span>";
                    break;
            }
        }

        if (langID == 1)
        {
            if (has_allotment)
            {
                totalResult = totalResult + "<p id=\"bookingRoomAvailable\"><strong>The room is available.</strong> You’ll receive voucher confirmation immediately after payment is successful</p>\n";
            }
            totalResult = totalResult + "<table id=\"bookingMiniTotal\" cellspacing=\"0\">\n";
            totalResult = totalResult + "<tr><td><strong>Total</strong></td><td align=\"right\"><strong>" + ((int)(Utility.PriceExcludeVat(priceTotal))).ToString("#,###.##") + " Baht</strong></td></tr>\n";
            totalResult = totalResult + "<tr>\n";
            totalResult = totalResult + "<td class=\"bookingVatCharge\"><strong>Vat & Service Charge</strong></td><td class=\"bookingVatCharge\" align=\"right\"><strong>" + ((priceVatInc - totalAbf) - ((int)Utility.PriceExcludeVat(priceTotal))).ToString("#,###.##") + " Baht</strong></td></tr>\n";
            //totalResult = totalResult + "<tr><td class=\"bookingTotalPrice\">Grand Total</td><td align=\"right\" class=\"bookingTotalPrice\">" + ((int)priceVatInc).ToString("#,###") + " Baht" + priceVatIncDisplay + "</td></tr>\n";
            totalResult = totalResult + "<tr><td colspan=\"2\"><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tr><td class=\"bookingTotalPrice\">Grand Total</td><td align=\"right\" class=\"bookingTotalPrice\">" + (priceVatInc).ToString("#,###.##") + " Baht</td></tr>\n";
            if (!string.IsNullOrEmpty(priceVatIncDisplay))
            {
                totalResult = totalResult + "<tr><td colspan=\"2\"><span class=\"fnGray\">*Rates in other currencies are approximate." + priceVatIncDisplay + "</span></td></tr>\n";
            }
            totalResult = totalResult + "</table></td></tr>\n";
            //totalResult = totalResult + "<tr><td class=\"bookingVatCharge\" colspan=\"2\"><strong>" + depositWordMini + "</strong></td></tr>\n";

            totalResult = totalResult + "<tr><td colspan=\"2\">\n";
            totalResult = totalResult + "<i class=\"fnRed\">(Include of Tax and Service Charge)</i>\n";
            totalResult = totalResult + "<p class=\"fnSmall\">\n";
            totalResult = totalResult + "No hidden cost and no surprised charge. You can see the grand total in net rate without additional charge when you check in at hotel or use the service.";
            totalResult = totalResult + "</p>\n";
            totalResult = totalResult + "</td></tr>\n";
            if (gatewayID!=5)
            {
                totalResult = totalResult + "<tr><td colspan=\"2\"><img src=\"/images/logo_booking_offer.gif\" /><br /><br /></td></tr>\n";
            }else{
                totalResult = totalResult + "<tr><td colspan=\"2\"><img src=\"/images/logo_booking_offer2.gif\" /><br /><br /></td></tr>\n";
            }
            
            totalResult = totalResult + "</table>\n";
        }
        else
        {
            if (has_allotment)
            {
                totalResult = totalResult + "<p id=\"bookingRoomAvailable\"><strong class=\"fnOrange\">ขณะนี้มีห้องว่าง</strong> คุณจะได้รับเอกสารยืนยันการจองทันทีหลังจากชำระเงินเป็นที่เรียบร้อยแล้ว</p><br/>\n";
            }
            totalResult = totalResult + "<table id=\"bookingMiniTotal\" cellspacing=\"0\">\n";
            totalResult = totalResult + "<tr><td><strong>ราคารวม</strong></td><td align=\"right\"><strong>" + ((int)(priceTotal)).ToString("#,###") + " Baht</strong></td></tr>\n";
            totalResult = totalResult + "<tr>\n";
            totalResult = totalResult + "<td class=\"bookingVatCharge\">ภาษีมูลค่าเพิ่มและค่าธรรมเนียมบริการ</td><td class=\"bookingVatCharge\" align=\"right\"><strong>" + ((int)(priceVatInc - totalAbf) - (int)(priceTotal)).ToString("#,###") + " Baht" + priceVatDisplay + "</strong></td></tr>\n";
            totalResult = totalResult + "<tr><td  colspan=\"2\"><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tr><td class=\"bookingTotalPrice\">ราคารวมสุทธิ</td><td align=\"right\" class=\"bookingTotalPrice\">" + ((int)priceVatInc).ToString("#,###") + " Baht" + priceVatIncDisplay + "</td></tr></table></td></tr>\n";
            totalResult = totalResult + "<tr><td colspan=\"2\">\n";
            totalResult = totalResult + "<i class=\"fnRed\">(รวมภาษีมูลค่าเพิ่มและค่าธรรมเนียมบริการแล้ว)</i>\n";
            totalResult = totalResult + "<p class=\"fnSmall\">\n";
            totalResult = totalResult + "ไม่มีค่าใช้จ่ายแอบแฝง ราคาที่คุณเห็นนี้จะเป็นราคาสุทธิที่ปราศจากค่าใช้จ่ายเพิ่มเติ่ม คุณไม่ต้องจ่ายค่าห้องพักเพิ่มเติมเมื่อคุณเช็คอินหรือเช็คเอ้าท์ที่โรงแรมหรือใช้บริการอื่นกับเรา";
            totalResult = totalResult + "</p>\n";
            totalResult = totalResult + "</td></tr>\n";
            totalResult = totalResult + "<tr><td colspan=\"2\"><img src=\"/images/logo_booking_offer.gif\" /><br /><br /></td></tr>\n";
            totalResult = totalResult + "</table>\n";
        }




        //switch (categoryID)
        //{
        //    case 29:
        //        if (langID == 1)
        //        {
        //            RoomResult = RoomResult + "<tr>\n";
        //            RoomResult = RoomResult + "<td colspan=\"4\" class=\"book_vat\">Vat & Service Charge</td> \n";
        //            RoomResult = RoomResult + "<td class=\"book_vat_sub\">" + ((int)(priceVatInc - totalAbf) - (int)(priceTotal)).ToString("#,###") + " Baht" + priceVatDisplay + "</td>\n";
        //            RoomResult = RoomResult + "</tr>\n";
        //            RoomResult = RoomResult + "<tr>\n";
        //            RoomResult = RoomResult + "<td colspan=\"4\" class=\"book_vat\">Total Price</td> \n";
        //            RoomResult = RoomResult + "<td class=\"book_total_price\">" + ((int)priceVatInc).ToString("#,###") + " Baht" + priceVatIncDisplay + "</td>\n";
        //            RoomResult = RoomResult + "</tr>\n";
        //        }
        //        else
        //        {
        //            RoomResult = RoomResult + "<tr>\n";
        //            RoomResult = RoomResult + "<td colspan=\"4\" class=\"book_vat\">ภาษีมูลค่าเพิ่มและค่าธรรมเนียมบริการ</td> \n";
        //            RoomResult = RoomResult + "<td class=\"book_vat_sub\">" + ((int)(priceVatInc - totalAbf) - (int)(priceTotal)).ToString("#,###") + " Baht" + priceVatDisplay + "</td>\n";
        //            RoomResult = RoomResult + "</tr>\n";
        //            RoomResult = RoomResult + "<tr>\n";
        //            RoomResult = RoomResult + "<td colspan=\"4\" class=\"book_vat\">ราคารวมสุทธิ (รวมภาษีมูลค่าเพิ่มและค่าธรรมเนียมบริการแล้ว)</td> \n";
        //            RoomResult = RoomResult + "<td class=\"book_total_price\">" + ((int)priceVatInc).ToString("#,###") + " Baht" + priceVatIncDisplay + "</td>\n";
        //            RoomResult = RoomResult + "</tr>\n";
        //        }

        //        break;
        //    case 32:
        //    case 34:
        //    case 36:
        //    case 38:
        //    case 39:
        //    case 40:
        //        if (langID == 1)
        //        {
        //            RoomResult = RoomResult + "<tr>\n";
        //            RoomResult = RoomResult + "<td colspan=\"3\" class=\"book_vat\">Vat & Service Charge</td> \n";
        //            RoomResult = RoomResult + "<td class=\"book_vat_sub\">" + ((int)(priceVatInc) - (int)(priceTotal)).ToString("#,###") + " Baht" + priceVatDisplay + "</td>\n";
        //            RoomResult = RoomResult + "</tr>\n";
        //            RoomResult = RoomResult + "<tr>\n";
        //            RoomResult = RoomResult + "<td colspan=\"3\" class=\"book_vat\">Total Price</td> \n";
        //            RoomResult = RoomResult + "<td class=\"book_total_price\"><span>THB</span>" + ((int)priceVatInc).ToString("#,###") + " Baht" + priceVatIncDisplay + "</td>\n";
        //            RoomResult = RoomResult + "</tr>\n";
        //        }
        //        else
        //        {
        //            RoomResult = RoomResult + "<tr>\n";
        //            RoomResult = RoomResult + "<td colspan=\"3\" class=\"book_vat\">ภาษีมูลค่าเพิ่มและค่าธรรมเนียมบริการ</td> \n";
        //            RoomResult = RoomResult + "<td class=\"book_vat_sub\">" + ((int)(priceVatInc) - (int)(priceTotal)).ToString("#,###") + " Baht" + priceVatDisplay + "</td>\n";
        //            RoomResult = RoomResult + "</tr>\n";
        //            RoomResult = RoomResult + "<tr>\n";
        //            RoomResult = RoomResult + "<td colspan=\"3\" class=\"book_vat\">ราคารวมสุทธิ (รวมภาษีมูลค่าเพิ่มและค่าธรรมเนียมบริการแล้ว)</td> \n";
        //            RoomResult = RoomResult + "<td class=\"book_total_price\"><span>THB</span>" + ((int)priceVatInc).ToString("#,###") + " Baht" + priceVatIncDisplay + "</td>\n";
        //            RoomResult = RoomResult + "</tr>\n";
        //        }

        //        break;
        //}

        //RoomResult = RoomResult + "<tr>\n";
        //RoomResult = RoomResult + "<td colspan=\"5\" class=\"pic_price\"> \n";
        //if (payLater.ProductID == 0)
        //{
        //    if (langID == 1)
        //    {
        //        RoomResult = RoomResult + "<div class=\"book_pic_price2\"><span>Total Price :</span>" + ((int)priceVatInc).ToString("#,###") + " Baht" + priceVatIncDisplay + "\n";
        //        RoomResult = RoomResult + "<span class=\"book_included\">(Included of Tax and Service Charge)</span></div> \n";
        //    }
        //    else
        //    {
        //        RoomResult = RoomResult + "<div class=\"book_pic_price2\"><span>ภาษีมูลค่าเพิ่มและค่าธรรมเนียมบริการ</span>" + ((int)priceVatInc).ToString("#,###") + " Baht" + priceVatIncDisplay + "\n";
        //        RoomResult = RoomResult + "<span class=\"book_included\">ราคารวมสุทธิ (รวมภาษีมูลค่าเพิ่มและค่าธรรมเนียมบริการแล้ว)</span></div> \n";
        //    }

        //}
        //else
        //{
        //    RoomResult = RoomResult + "<div class=\"book_pic_price\"><span>ภาษีมูลค่าเพิ่มและค่าธรรมเนียมบริการ</span>" + ((int)priceVatInc).ToString("#,###") + " Baht" + priceVatIncDisplay + "<br /> \n";
        //    RoomResult = RoomResult + "<span class=\"book_included\">ราคารวมสุทธิ (รวมภาษีมูลค่าเพิ่มและค่าธรรมเนียมบริการแล้ว)</span></div> \n";
        //    RoomResult = RoomResult + "<div class=\"book_charged\"><span>**</span>You will be charged only <span>$1</span> <br />for holding guarantee of your booking.</div> \n";

        //}


        //RoomResult = RoomResult + "</td>\n";
        //RoomResult = RoomResult + "</tr>\n";
        //RoomResult = RoomResult + "</table>\n";
        //RoomResult = RoomResult + "<div class=\"book_back\">\n";
        //if (langID == 1)
        //{
        //    RoomResult = RoomResult + "<span>If this information is not correct, Click  </span> \n";
        //    RoomResult = RoomResult + "<a href=\"javascript:void(0)\" onclick=\"history.back()\"><img src=\"../theme_color/blue/images/layout_mail/book_bak.jpg\" /></a>\n";
        //    RoomResult = RoomResult + "<span>to Edit your Booking Detail</span>\n";
        //    RoomResult = RoomResult + "<div class=\"clear-all\"></div>\n";
        //}
        //else
        //{

        //    RoomResult = RoomResult + "<span>ในกรณีที่ต้องการแก้ไขข้อมูลการจอง กรุณาคลิก</span> \n";
        //    RoomResult = RoomResult + "<a href=\"javascript:void(0)\" onclick=\"history.back()\"><img src=\"../theme_color/blue/images/layout_mail/book_bak.jpg\" /></a>\n";
        //    //RoomResult=RoomResult+"<span>to Edit your Booking Detail</span>\n";
        //    RoomResult = RoomResult + "<div class=\"clear-all\"></div>\n";
        //}

        //RoomResult = RoomResult + "</div>\n";

        layout = layout.Replace("<!--###Tracking###-->", "<script language=\"javascript\" src=\"http://track.hotels2thailand.com/application/track.aspx?ht2thPageID=9010470000000&ht2thAD=&affID=&camID=&keyword=&datein=&dateout=&desID=&locID=&bookingID=&ht2thRefer=\"  type=\"text/javascript\"></script>");
        string Keyword  = Utility.GetKeywordReplace(layout, "<!--###BookingSummaryStart###-->", "<!--###BookingSummaryEnd###-->");


        //For some hotel is not allotment and not receive booking on request
        if (!has_allotment && (IsProductNotReceiveFullyBook(int.Parse(Request.Form["hotel_id"]))))
        {
            layout = layout.Replace(Keyword, "<div style=\"padding:10px;\"><img src=\"images/ico_delete_circle.gif\" style=\"float:left; margin-right:15px;\"> Your selected date is currently fully booked. For more information, please kindly contact to us at <a href=\"mailto:reservation@booking2hotel.com\">reservation@booking2hotel.com</a><br/><br>&nbsp;&nbsp;<a href=\"javascript:void(0)\" onclick=\"history.back()\">&lt;&lt; Edit booking detail</a></div>");
            Keyword = Utility.GetKeywordReplace(layout, "<!--###totalBookingMiniStart###-->", "<!--###totalBookingMiniEnd###-->");
            layout = layout.Replace(Keyword, "");
            Keyword = Utility.GetKeywordReplace(layout, "<!--###BookingInformationStart###-->", "<!--###BookingInformationStart###-->");
            layout = layout.Replace(Keyword, "");
            Keyword = Utility.GetKeywordReplace(layout, "<!--###HotelHeaderStart###-->", "<!--###HotelHeaderEnd###-->");
            layout = layout.Replace(Keyword, HotelHeader);
            Keyword = Utility.GetKeywordReplace(layout, "<!--###ProceedCheckoutStart###-->", "<!--###ProceedCheckoutEnd###-->");
            layout = layout.Replace(Keyword, "");
            
            layout = layout.Replace("<!--###cssHotelBook###-->", "<link href=\"http://www.booking2hotels.com/hotels-template/" + HotelFolder + "/css/bookForm.css\" rel=\"stylesheet\" type=\"text/css\" />");

            Response.Write(layout);
            Response.End();
        }
        else
        {
            layout = layout.Replace(Keyword, RoomResult);
        }
        //

        

        Keyword = Utility.GetKeywordReplace(layout, "<!--###totalBookingMiniStart###-->", "<!--###totalBookingMiniEnd###-->");
        layout = layout.Replace(Keyword, totalResult);

        Country country = new Country();
        string formInfo = string.Empty;

        if (langID == 1)
        {
            formInfo = formInfo + "<div id=\"formCustomerBox\">\n";
            formInfo = formInfo + "<div class=\"formCustomer\">\n";
            formInfo = formInfo + "<span class=\"fnLarge fnGray\">Your information</span>\n";
            formInfo = formInfo + "<br /><br />\n";
        }
        else
        {
            formInfo = formInfo + "<div id=\"formCustomerBox\">\n";
            formInfo = formInfo + "<div class=\"formCustomer\">\n";
            formInfo = formInfo + "<span class=\"fnLarge fnGray\">ข้อมูลลูกค้า</span>\n";
            formInfo = formInfo + "<br /><br />\n";
        }


        if (langID == 1)
        {
            string cNameDefault = string.Empty;
            string cEmailDefault = string.Empty;

            

            formInfo = formInfo + "<h3 class=\"fnBig fnBlue\">About you</h3><br />\n";
            formInfo = formInfo + "<div><label>Prefix:<span class=\"fnRed\">*</span></label> <select name=\"prefix\" id=\"prefix\"><option value=\"1\">None</option><option value=\"2\" selected=\"selected\">Mr.</option><option value=\"4\">Miss.</option><option value=\"3\">Mrs.</option></select></div>\n";
            if (IsMember)
            {
                Customer objCustomer = new Customer(int.Parse(Request.Form["mmid"]));
                formInfo = formInfo + "<div><label>Full name:<span class=\"fnRed\">*</span></label><input type=\"text\"  name=\"first_name\" id=\"first_name\" class=\"required\" value=\""+objCustomer.FullName+"\" /></div>\n";
                formInfo = formInfo + "<div><label>Email address:<span class=\"fnRed\">*</span></label><input type=\"text\" name=\"email\" id=\"email\" class=\"required email\" value=\""+objCustomer.Email+"\"/></div>\n";
                formInfo = formInfo + "<div><label>Repeat email:<span class=\"fnRed\">*</span></label><input type=\"text\" name=\"re_email\" id=\"re_email\" class=\"required\"  value=\"" + objCustomer.Email + "\"/></div>\n";
                formInfo = formInfo + "<div><label>Phone:<span class=\"fnRed\">*</span></label><input type=\"text\" name=\"phone\" id=\"phone\" class=\"required\"/></div>\n";
                formInfo = formInfo + "<div><label>Country:<span class=\"fnRed\">*</span></label>" + DropdownUtility.CountryList("country", country.GetCountryAll()) + "</div>\n";

            }
            else {
                formInfo = formInfo + "<div><label>Full name:<span class=\"fnRed\">*</span></label><input type=\"text\"  name=\"first_name\" id=\"first_name\" class=\"required\" /></div>\n";
                formInfo = formInfo + "<div><label>Email address:<span class=\"fnRed\">*</span></label><input type=\"text\" name=\"email\" id=\"email\" class=\"required email\" /></div>\n";
                formInfo = formInfo + "<div><label>Repeat email:<span class=\"fnRed\">*</span></label><input type=\"text\" name=\"re_email\" id=\"re_email\" class=\"required\" /></div>\n";
                formInfo = formInfo + "<div><label>Phone:<span class=\"fnRed\">*</span></label><input type=\"text\" name=\"phone\" id=\"phone\" class=\"required\"/></div>\n";
                formInfo = formInfo + "<div><label>Country:<span class=\"fnRed\">*</span></label>" + DropdownUtility.CountryList("country", country.GetCountryAll()) + "</div>\n";

            }
            formInfo = formInfo + "<br />\n";
            formInfo = formInfo + "<p id=\"asterisk\">Fields marked with a red asterisk ( <span class=\"fnRed\">*</span> ) are required.</p>\n";
            formInfo = formInfo + "</div>\n";

            if (checkTransfer)
            {

                formInfo = formInfo + "<div class=\"formCustomer\">\n";
                formInfo = formInfo + "<h3 class=\"fnBig fnBlue\">Flight detail:</h3><br />\n";
                formInfo = formInfo + "<div><label>Arrival Flight:</label><input type=\"text\" name=\"flight_name_in\" id=\"flight_name_in\"/></div>\n";
                formInfo = formInfo + "<div><label>Arrival Time:</label><input type=\"text\"  name=\"flight_ci\" id=\"flight_ci\" style=\"width:120px;\" /><input type=\"hidden\"  name=\"Hdflight_ci\" id=\"Hdflight_ci\" style=\"width:120px;\" /> Hour " + DropdownUtility.Number("time_hour_arv", 0, 23, 0) + " Min " + DropdownUtility.Number("time_min_arv", 0, 59, 0) + "</div>\n";
                formInfo = formInfo + "<div><label>Departure Flight:</label><input type=\"text\" name=\"flight_name_out\" id=\"flight_name_out\"/></div>\n";
                formInfo = formInfo + "<div><label>Departure Time:</label><input type=\"text\"  name=\"flight_co\" id=\"flight_co\" style=\"width:120px;\" /><input type=\"hidden\"  name=\"Hdflight_co\" id=\"Hdflight_co\" style=\"width:120px;\" /> Hour " + DropdownUtility.Number("time_hour_dep", 0, 23, 0) + " Min " + DropdownUtility.Number("time_min_dep", 0, 59, 0) + "</div>\n";
                formInfo = formInfo + "<div><label>Remark</label><textarea style=\"width:300px; height:100px;\" name=\"transfer_detail\">Pickup from airport and transfer to " + ProductTitle + "</textarea></div>\n";
                formInfo = formInfo + "</div>\n";
            }
            

        }
        else
        {

            //formInfo = formInfo + "<h3 class=\"fnBig fnBlue\">About you</h3><br />\n";
            formInfo = formInfo + "<div><label>คำนำหน้า:<span class=\"fnRed\">*</span></label> <select name=\"prefix\" id=\"prefix\"><option value=\"1\">ไม่ระบุ</option><option value=\"2\" selected=\"selected\">นาย</option><option value=\"4\">นางสาว</option><option value=\"3\">นาง</option></select></div>\n";
            formInfo = formInfo + "<div><label>ชื่อผู้เข้าพัก:<span class=\"fnRed\">*</span></label><input type=\"text\"  name=\"first_name\" id=\"first_name\" class=\"required\" /></div>\n";
            formInfo = formInfo + "<div><label>อีเมล:<span class=\"fnRed\">*</span></label><input type=\"text\" name=\"email\" id=\"email\" class=\"required email\" /></div>\n";
            formInfo = formInfo + "<div><label>กรอกอีเมลอีกครั้ง:<span class=\"fnRed\">*</span></label><input type=\"text\" name=\"re_email\" id=\"re_email\" class=\"required\" /></div>\n";
            formInfo = formInfo + "<div><label>เบอร์โทรติดต่อ:<span class=\"fnRed\">*</span></label><input type=\"text\" name=\"phone\" id=\"phone\" class=\"required\"/></div>\n";
            formInfo = formInfo + "<div><label>สัญชาติ:<span class=\"fnRed\">*</span></label>" + DropdownUtility.CountryList("country", country.GetCountryAll()) + "</div>\n";
            formInfo = formInfo + "<br />\n";
            formInfo = formInfo + "<p id=\"asterisk\">กรุณากรอกข้อมูลให้ครบถ้วนโดยเฉพาะช่องที่มีเครื่องหมาย( <span class=\"fnRed\">*</span> )</p>\n";
            formInfo = formInfo + "</div>\n";

            if (checkTransfer)
            {

                formInfo = formInfo + "<div class=\"formCustomer\">\n";
                formInfo = formInfo + "<h3 class=\"fnBig fnBlue\">Flight detail:</h3><br />\n";
                formInfo = formInfo + "<div><label>เที่ยวบินขาเข้า:</label><input type=\"text\" name=\"flight_name_in\" id=\"flight_name_in\"/></div>\n";
                formInfo = formInfo + "<div><label>วันและเวลา:</label><input type=\"text\"  name=\"flight_ci\" id=\"flight_ci\" style=\"width:120px;\" /> Hour " + DropdownUtility.Number("time_hour_arv", 0, 23, 0) + " Min " + DropdownUtility.Number("time_min_arv", 0, 59, 0) + "</div>\n";
                formInfo = formInfo + "<div><label>เที่ยวบินขาออก:</label><input type=\"text\" name=\"flight_name_out\" id=\"flight_name_out\"/></div>\n";
                formInfo = formInfo + "<div><label>วันและเวลา:</label><input type=\"text\"  name=\"flight_co\" id=\"flight_co\" style=\"width:120px;\" /> Hour " + DropdownUtility.Number("time_hour_dep", 0, 23, 0) + " Min " + DropdownUtility.Number("time_min_dep", 0, 59, 0) + "</div>\n";
                formInfo = formInfo + "<div><label>&nbsp;</label><textarea style=\"width:300px; height:100px;\" name=\"transfer_detail\">กรุณามารับที่สนามบินและส่งลูกค้าไปยัง" + ProductTitle + "</textarea></div>\n";
                formInfo = formInfo + "</div>\n";
            }

            formInfo = formInfo + "</table>\n";
        }



        //string optionRequire = string.Empty;
        //optionRequire = optionRequire + "\n";
        //optionRequire = optionRequire + "<div class=\"head_step\">\n";
        //optionRequire = optionRequire + "<div class=\"head_step_left\"></div>\n";
        //if (langID == 1)
        //{
        //    optionRequire = optionRequire + "<div class=\"head_title\">Guest Information & Preferences	</div>    \n";
        //}
        //else
        //{
        //    optionRequire = optionRequire + "<div class=\"head_title\">รายละเอียดผู้เข้าพัก	</div>    \n";
        //}

        //optionRequire = optionRequire + "<div class=\"head_step_right\"></div>\n";
        //optionRequire = optionRequire + "</div>\n";
        //optionRequire = optionRequire + "<div class=\"bg_step\">\n";

        switch (categoryID)
        {
            case 29:
                if (langID == 1)
                {
                    formInfo = formInfo + "<div class=\"formCustomer\">\n";
                    formInfo = formInfo + "<h3 class=\"fnBig fnBlue\">Guest & Information Preference</h3>\n";
                    formInfo = formInfo + "<span class=\"fnGrayLight\">(Subject to availability, can not guarantee)</span>\n";
                    formInfo = formInfo + "</div>\n";
                }
                else
                {
                    formInfo = formInfo + "<div class=\"formCustomer\">\n";
                    formInfo = formInfo + "<h3 class=\"fnBig fnBlue\">รายละเอียดผู้เข้าพัก</h3>\n";
                    formInfo = formInfo + "<span class=\"fnGrayLight\">(ความต้องการพิเศษและคำขอพิเศษทั้งหมดขึ้นอยู่กับทางโรงแรมซึ่งจะไม่สามารถรับประกันได้แต่เราจะพยายามอย่างดีที่สุดเพื่อให้ท่านได้คำขอพิเศษตามที่ท่านได้ระบุไว้)</span>\n";
                    formInfo = formInfo + "</div>\n";

                }

                foreach (FrontOptionPackage itemPackage in objPackage.GetPackageList())
                {

                    if (Request.Form["ddPackage_" + itemPackage.ConditionID + "_" + itemPackage.OptionID] != null)
                    {
                        packageOption = Request.Form["ddPackage_" + itemPackage.ConditionID + "_" + itemPackage.OptionID].Split('_');
                        //Response.Write("ddPackage_" + itemPackage.ConditionID + "_" + itemPackage.OptionID + "<br>");
                        if (int.Parse(packageOption[6]) != 0)
                        {
                            if (int.Parse(packageOption[6]) == 1){
                                formInfo = formInfo + "<div class=\"formCustomer\">\n";
                                formInfo = formInfo + "<div class=\"requirementCustomer\">\n";
                                formInfo = formInfo + "<span class=\"fnMedium\"><strong>" + itemPackage.OptionTitle + "</strong></span><br /><br />\n";
                                formInfo = formInfo + "<div><label><strong>Bed type:</strong></label>\n";
                                formInfo = formInfo + "<span class=\"guest-requirement-col-1\"><input type=\"radio\" name=\"bed_type_" + itemPackage.ConditionID + "_1\" value=\"3\" checked=\"checked\" />No preference</span>\n";
                                formInfo = formInfo + "<span class=\"guest-requirement-col-2\"><input type=\"radio\" name=\"bed_type_" + itemPackage.ConditionID + "_1\" value=\"1\" />1 King size bed</span>\n";
                                formInfo = formInfo + "<span class=\"guest-requirement-col-3\"><input type=\"radio\" name=\"bed_type_" + itemPackage.ConditionID + "_1\" value=\"2\" />Twin beds</span><br class=\"clearAll\">\n";
                                formInfo = formInfo + "</div>\n";
                                if (ProductID!=3590)
                                {
                                    formInfo = formInfo + "<div><label><strong>Smoke:</strong></label>\n";
                                    formInfo = formInfo + "<span class=\"guest-requirement-col-1\"><input type=\"radio\" name=\"smoke_type_" + itemPackage.ConditionID + "_1\" value=\"3\" checked=\"checked\" />No preference</span>\n";
                                    formInfo = formInfo + "<span class=\"guest-requirement-col-2\"><input type=\"radio\" name=\"smoke_type_" + itemPackage.ConditionID + "_1\" value=\"1\" />Non-Smoking</span>\n";
                                    formInfo = formInfo + "<span class=\"guest-requirement-col-3\"><input type=\"radio\" name=\"smoke_type_" + itemPackage.ConditionID + "_1\" value=\"2\" />Smoking</span><br class=\"clearAll\">\n";
                                    formInfo = formInfo + "</div>\n";
                                }else{
                                    formInfo = formInfo + "<div><label><strong>Smoke:</strong></label>\n";
                                    formInfo = formInfo + "<span class=\"guest-requirement-col-1\"></span>\n";
                                    formInfo = formInfo + "<span class=\"guest-requirement-col-2\"><input type=\"radio\" name=\"smoke_type_" + itemPackage.ConditionID + "_1\" value=\"1\" checked=\"checked\" />Non-Smoking</span>\n";
                                    formInfo = formInfo + "<span class=\"guest-requirement-col-3\"></span><br class=\"clearAll\">\n";
                                    formInfo = formInfo + "</div>\n";
                                    }
                                

                                formInfo = formInfo + "<div><label><strong>floor:</strong></label>\n";
                                formInfo = formInfo + "<span class=\"guest-requirement-col-1\"><input type=\"radio\" name=\"floor_type_" + itemPackage.ConditionID + "_1\" value=\"3\" checked=\"checked\" />No preference</span>\n";
                                formInfo = formInfo + "<span class=\"guest-requirement-col-2\"><input type=\"radio\" name=\"floor_type_" + itemPackage.ConditionID + "_1\" value=\"1\" />High floor</span>\n";
                                formInfo = formInfo + "<span class=\"guest-requirement-col-3\"><input type=\"radio\" name=\"floor_type_" + itemPackage.ConditionID + "_1\" value=\"2\" />Low floor</span><br class=\"clearAll\">\n";
                                formInfo = formInfo + "</div>\n";
                                formInfo = formInfo + "<textarea name=\"sp_require_" + itemPackage.ConditionID + "_1\"></textarea>\n";
                                formInfo = formInfo + "</div>\n";
                                formInfo = formInfo + "</div>\n";
                            }else{
                                for (int roomCount = 1; roomCount <= int.Parse(packageOption[6]); roomCount++)
                                {
                                    formInfo = formInfo + "<div class=\"formCustomer\">\n";
                                    formInfo = formInfo + "<div class=\"requirementCustomer\">\n";
                                    formInfo = formInfo + "<span class=\"fnMedium\"><strong>Room#" + roomCount + ": " + itemPackage.OptionTitle + "</strong></span><br /><br />\n";

                                    formInfo = formInfo + "<div><label><strong>Bed type:</strong></label>\n";
                                    formInfo = formInfo + "<span class=\"guest-requirement-col-1\"><input type=\"radio\" name=\"bed_type_" + itemPackage.ConditionID + "_" + roomCount + "\" value=\"3\" checked=\"checked\" />No preference</span>\n";
                                    formInfo = formInfo + "<span class=\"guest-requirement-col-2\"><input type=\"radio\" name=\"bed_type_" + itemPackage.ConditionID + "_" + roomCount + "\" value=\"1\" />1 King size bed</span>\n";
                                    formInfo = formInfo + "<span class=\"guest-requirement-col-3\"><input type=\"radio\" name=\"bed_type_" + itemPackage.ConditionID + "_" + roomCount + "\" value=\"2\" />Twin beds</span><br class=\"clearAll\">\n";
                                    formInfo = formInfo + "</div>\n";
                                    if (ProductID != 3590)
                                    {
                                    formInfo = formInfo + "<div><label><strong>Smoke:</strong></label>\n";
                                    formInfo = formInfo + "<span class=\"guest-requirement-col-1\"><input type=\"radio\" name=\"smoke_type_" + itemPackage.ConditionID + "_" + roomCount + "\" value=\"3\" checked=\"checked\" />No preference</span>\n";
                                    formInfo = formInfo + "<span class=\"guest-requirement-col-2\"><input type=\"radio\" name=\"smoke_type_" + itemPackage.ConditionID + "_" + roomCount + "\" value=\"1\" />Non-Smoking</span>\n";
                                    formInfo = formInfo + "<span class=\"guest-requirement-col-3\"><input type=\"radio\" name=\"smoke_type_" + itemPackage.ConditionID + "_" + roomCount + "\" value=\"2\" />Smoking</span><br class=\"clearAll\">\n";
                                    formInfo = formInfo + "</div>\n";
                                    }else{
                                    formInfo = formInfo + "<div><label><strong>Smoke:</strong></label>\n";
                                    formInfo = formInfo + "<span class=\"guest-requirement-col-1\"></span>\n";
                                    formInfo = formInfo + "<span class=\"guest-requirement-col-2\"><input type=\"radio\" name=\"smoke_type_" + itemPackage.ConditionID + "_" + roomCount + "\" value=\"1\"  checked=\"checked\"/>Non-Smoking</span>\n";
                                    formInfo = formInfo + "<span class=\"guest-requirement-col-3\"></span><br class=\"clearAll\">\n";
                                    formInfo = formInfo + "</div>\n";
                                    }
                                    
                                    formInfo = formInfo + "<div><label><strong>floor:</strong></label>\n";
                                    formInfo = formInfo + "<span class=\"guest-requirement-col-1\"><input type=\"radio\" name=\"floor_type_" + itemPackage.ConditionID + "_" + roomCount + "\" value=\"3\" checked=\"checked\" />No preference</span>\n";
                                    formInfo = formInfo + "<span class=\"guest-requirement-col-2\"><input type=\"radio\" name=\"floor_type_" + itemPackage.ConditionID + "_" + roomCount + "\" value=\"1\" />High floor</span>\n";
                                    formInfo = formInfo + "<span class=\"guest-requirement-col-3\"><input type=\"radio\" name=\"floor_type_" + itemPackage.ConditionID + "_" + roomCount + "\" value=\"2\" />Low floor</span><br class=\"clearAll\">\n";
                                    formInfo = formInfo + "</div>\n";
                                    formInfo = formInfo + "<textarea name=\"sp_require_" + itemPackage.ConditionID + "_" + roomCount + "\"></textarea>\n";
                                    formInfo = formInfo + "</div>\n";
                                    formInfo = formInfo + "</div>\n";
                                }
                            }
                        }
                    }
                }

                if (conditionID != null)
                {
                    for (int productCount = 0; productCount < conditionID.Count(); productCount++)
                    {
                        if (quantity[productCount] > 0)
                        {
                            if (quantity[productCount] == 1)
                            {
                                if (langID == 1)
                                {
                                    if (!checkExtranet)
                                    {
                                        //optionRequire = optionRequire + "<div class=\"book_room\">Room : " + GetProductTitle(optionID[productCount], OptionTitleList) + "<span>(Subject to availability, can not guarantee)</span></div>\n";
                                        formInfo = formInfo + "<div class=\"formCustomer\">\n";
                                        formInfo = formInfo + "<div class=\"requirementCustomer\">\n";
                                        formInfo = formInfo + "<span class=\"fnMedium\"><strong>" + GetProductTitle(optionID[productCount], OptionTitleList) + "</strong></span><br /><br />\n";
                                    }
                                    else
                                    {
                                        //optionRequire = optionRequire + "<div class=\"book_room\">Room : " + GetProductExtraTitle(optionID[productCount], OptionTitleListExtra) + "<span>(Subject to availability, can not guarantee)</span></div>\n";
                                        formInfo = formInfo + "<div class=\"formCustomer\">\n";
                                        formInfo = formInfo + "<div class=\"requirementCustomer\">\n";
                                        formInfo = formInfo + "<span class=\"fnMedium\"><strong>" + GetProductExtraTitle(optionID[productCount], OptionTitleListExtra) + "</strong></span><br /><br />\n";
                                    }


                                    formInfo = formInfo + "<div><label><strong>Bed type:</strong></label>\n";
                                    formInfo = formInfo + "<span class=\"guest-requirement-col-1\"><input type=\"radio\" name=\"bed_type_" + conditionID[productCount] + "_1\" value=\"3\" checked=\"checked\" />No preference</span>\n";
                                    formInfo = formInfo + "<span class=\"guest-requirement-col-2\"><input type=\"radio\" name=\"bed_type_" + conditionID[productCount] + "_1\" value=\"1\" />1 King size bed</span>\n";
                                    formInfo = formInfo + "<span class=\"guest-requirement-col-3\"><input type=\"radio\" name=\"bed_type_" + conditionID[productCount] + "_1\" value=\"2\" />Twin beds</span><br class=\"clearAll\">\n";
                                    formInfo = formInfo + "</div>\n";
                                    if (ProductID != 3590)
                                    {
                                        formInfo = formInfo + "<div><label><strong>Smoke:</strong></label>\n";
                                        formInfo = formInfo + "<span class=\"guest-requirement-col-1\"><input type=\"radio\" name=\"smoke_type_" + conditionID[productCount] + "_1\" value=\"3\" checked=\"checked\" />No preference</span>\n";
                                        formInfo = formInfo + "<span class=\"guest-requirement-col-2\"><input type=\"radio\" name=\"smoke_type_" + conditionID[productCount] + "_1\" value=\"1\" />Non-Smoking</span>\n";
                                        formInfo = formInfo + "<span class=\"guest-requirement-col-3\"><input type=\"radio\" name=\"smoke_type_" + conditionID[productCount] + "_1\" value=\"2\" />Smoking</span><br class=\"clearAll\">\n";
                                        formInfo = formInfo + "</div>\n";
                                    }
                                    else
                                    {
                                        formInfo = formInfo + "<div><label><strong>Smoke:</strong></label>\n";
                                        formInfo = formInfo + "<span class=\"guest-requirement-col-1\"></span>\n";
                                        formInfo = formInfo + "<span class=\"guest-requirement-col-2\"><input type=\"radio\" name=\"smoke_type_" + conditionID[productCount] + "_1\" value=\"1\"  checked=\"checked\"/>Non-Smoking</span>\n";
                                        formInfo = formInfo + "<span class=\"guest-requirement-col-3\"></span><br class=\"clearAll\">\n";
                                        formInfo = formInfo + "</div>\n";
                                    }

                                    formInfo = formInfo + "<div><label><strong>floor:</strong></label>\n";
                                    formInfo = formInfo + "<span class=\"guest-requirement-col-1\"><input type=\"radio\" name=\"floor_type_" + conditionID[productCount] + "_1\" value=\"3\" checked=\"checked\" />No preference</span>\n";
                                    formInfo = formInfo + "<span class=\"guest-requirement-col-2\"><input type=\"radio\" name=\"floor_type_" + conditionID[productCount] + "_1\" value=\"1\" />High floor</span>\n";
                                    formInfo = formInfo + "<span class=\"guest-requirement-col-3\"><input type=\"radio\" name=\"floor_type_" + conditionID[productCount] + "_1\" value=\"2\" />Low floor</span><br class=\"clearAll\">\n";
                                    formInfo = formInfo + "</div>\n";
                                    formInfo = formInfo + "<textarea name=\"sp_require_" + conditionID[productCount] + "_1\"></textarea>\n";
                                    formInfo = formInfo + "</div>\n";
                                    formInfo = formInfo + "</div>\n";

                                }
                                else
                                {
                                    if (!checkExtranet)
                                    {
                                        //optionRequire = optionRequire + "<div class=\"book_room\">Room : " + GetProductTitle(optionID[productCount], OptionTitleList) + "<span>(Subject to availability, can not guarantee)</span></div>\n";
                                        formInfo = formInfo + "<div class=\"formCustomer\">\n";
                                        formInfo = formInfo + "<div class=\"requirementCustomer\">\n";
                                        formInfo = formInfo + "<span class=\"fnMedium\"><strong>" + GetProductTitle(optionID[productCount], OptionTitleList) + "</strong></span><br /><br />\n";
                                    }
                                    else
                                    {
                                        //optionRequire = optionRequire + "<div class=\"book_room\">Room : " + GetProductExtraTitle(optionID[productCount], OptionTitleListExtra) + "<span>(Subject to availability, can not guarantee)</span></div>\n";
                                        formInfo = formInfo + "<div class=\"formCustomer\">\n";
                                        formInfo = formInfo + "<div class=\"requirementCustomer\">\n";
                                        formInfo = formInfo + "<span class=\"fnMedium\"><strong>" + GetProductExtraTitle(optionID[productCount], OptionTitleListExtra) + "</strong></span><br /><br />\n";
                                    }


                                    formInfo = formInfo + "<div><label><strong>ชนิดเตียง:</strong></label>\n";
                                    formInfo = formInfo + "<span class=\"guest-requirement-col-1\"><input type=\"radio\" name=\"bed_type_" + conditionID[productCount] + "_1\" value=\"3\" checked=\"checked\" />ไม่ระบุ</span>\n";
                                    formInfo = formInfo + "<span class=\"guest-requirement-col-2\"><input type=\"radio\" name=\"bed_type_" + conditionID[productCount] + "_1\" value=\"1\" />เตียงใหญ่</span>\n";
                                    formInfo = formInfo + "<span class=\"guest-requirement-col-3\"><input type=\"radio\" name=\"bed_type_" + conditionID[productCount] + "_1\" value=\"2\" />เตียงคู่</span><br class=\"clearAll\">\n";
                                    formInfo = formInfo + "</div>\n";

                                    if (ProductID != 3590)
                                    {
                                        formInfo = formInfo + "<div><label><strong>สูบบุหรี่:</strong></label>\n";
                                        formInfo = formInfo + "<span class=\"guest-requirement-col-1\"><input type=\"radio\" name=\"smoke_type_" + conditionID[productCount] + "_1\" value=\"3\" checked=\"checked\" />ไม่ระบุ</span>\n";
                                        formInfo = formInfo + "<span class=\"guest-requirement-col-2\"><input type=\"radio\" name=\"smoke_type_" + conditionID[productCount] + "_1\" value=\"1\" />ไม่สูบบุหรี่</span>\n";
                                        formInfo = formInfo + "<span class=\"guest-requirement-col-3\"><input type=\"radio\" name=\"smoke_type_" + conditionID[productCount] + "_1\" value=\"2\" />สูบบุหรี่</span><br class=\"clearAll\">\n";
                                        formInfo = formInfo + "</div>\n";
                                    }
                                    else
                                    {
                                        formInfo = formInfo + "<div><label><strong>สูบบุหรี่:</strong></label>\n";
                                        formInfo = formInfo + "<span class=\"guest-requirement-col-1\"></span>\n";
                                        formInfo = formInfo + "<span class=\"guest-requirement-col-2\"><input type=\"radio\" name=\"smoke_type_" + conditionID[productCount] + "_1\" value=\"1\" checked=\"checked\" />ไม่สูบบุหรี่</span>\n";
                                        formInfo = formInfo + "<span class=\"guest-requirement-col-3\"></span><br class=\"clearAll\">\n";
                                        formInfo = formInfo + "</div>\n";
                                    }

                                    formInfo = formInfo + "<div><label><strong>ระดับชั้น:</strong></label>\n";
                                    formInfo = formInfo + "<span class=\"guest-requirement-col-1\"><input type=\"radio\" name=\"floor_type_" + conditionID[productCount] + "_1\" value=\"3\" checked=\"checked\" />ไม่ระบุ</span>\n";
                                    formInfo = formInfo + "<span class=\"guest-requirement-col-2\"><input type=\"radio\" name=\"floor_type_" + conditionID[productCount] + "_1\" value=\"1\" />ชั้นสูงๆ</span>\n";
                                    formInfo = formInfo + "<span class=\"guest-requirement-col-3\"><input type=\"radio\" name=\"floor_type_" + conditionID[productCount] + "_1\" value=\"2\" />ชั้นล่างๆ</span><br class=\"clearAll\">\n";
                                    formInfo = formInfo + "</div>\n";
                                    formInfo = formInfo + "<textarea name=\"sp_require_" + conditionID[productCount] + "_1\"></textarea>\n";
                                    formInfo = formInfo + "</div>\n";
                                    formInfo = formInfo + "</div>\n";

                                }


                            }
                            else
                            {
                                for (int roomCount = 1; roomCount <= quantity[productCount]; roomCount++)
                                {
                                    if (langID == 1)
                                    {
                                        if (!checkExtranet)
                                        {
                                            //optionRequire = optionRequire + "<div class=\"book_room\">Room : " + GetProductTitle(optionID[productCount], OptionTitleList) + "<span>(Subject to availability, can not guarantee)</span></div>\n";
                                            formInfo = formInfo + "<div class=\"formCustomer\">\n";
                                            formInfo = formInfo + "<div class=\"requirementCustomer\">\n";
                                            formInfo = formInfo + "<span class=\"fnMedium\"><strong>Room#" + roomCount + ": " + GetProductTitle(optionID[productCount], OptionTitleList) + "</strong></span><br /><br />\n";

                                        }
                                        else
                                        {
                                            //optionRequire = optionRequire + "<div class=\"book_room\">Room : " + GetProductExtraTitle(optionID[productCount], OptionTitleListExtra) + "<span>(Subject to availability, can not guarantee)</span></div>\n";
                                            formInfo = formInfo + "<div class=\"formCustomer\">\n";
                                            formInfo = formInfo + "<div class=\"requirementCustomer\">\n";
                                            formInfo = formInfo + "<span class=\"fnMedium\"><strong>Room#" + roomCount + ": " + GetProductExtraTitle(optionID[productCount], OptionTitleListExtra) + "</strong></span><br /><br />\n";

                                        }

                                        formInfo = formInfo + "<div><label><strong>Bed type:</strong></label>\n";
                                        formInfo = formInfo + "<span class=\"guest-requirement-col-1\"><input type=\"radio\" name=\"bed_type_" + conditionID[productCount] + "_" + roomCount + "\" value=\"3\" checked=\"checked\" />No preference</span>\n";
                                        formInfo = formInfo + "<span class=\"guest-requirement-col-2\"><input type=\"radio\" name=\"bed_type_" + conditionID[productCount] + "_" + roomCount + "\" value=\"1\" />1 King size bed</span>\n";
                                        formInfo = formInfo + "<span class=\"guest-requirement-col-3\"><input type=\"radio\" name=\"bed_type_" + conditionID[productCount] + "_" + roomCount + "\" value=\"2\" />Twin beds</span><br class=\"clearAll\">\n";
                                        formInfo = formInfo + "</div>\n";
                                        if (ProductID != 3590)
                                        {
                                            formInfo = formInfo + "<div><label><strong>Smoke:</strong></label>\n";
                                            formInfo = formInfo + "<span class=\"guest-requirement-col-1\"><input type=\"radio\" name=\"smoke_type_" + conditionID[productCount] + "_" + roomCount + "\" value=\"3\" checked=\"checked\" />No preference</span>\n";
                                            formInfo = formInfo + "<span class=\"guest-requirement-col-2\"><input type=\"radio\" name=\"smoke_type_" + conditionID[productCount] + "_" + roomCount + "\" value=\"1\" />Non-Smoking</span>\n";
                                            formInfo = formInfo + "<span class=\"guest-requirement-col-3\"><input type=\"radio\" name=\"smoke_type_" + conditionID[productCount] + "_" + roomCount + "\" value=\"2\" />Smoking</span><br class=\"clearAll\">\n";
                                            formInfo = formInfo + "</div>\n";
                                        }
                                        else
                                        {
                                            formInfo = formInfo + "<div><label><strong>Smoke:</strong></label>\n";
                                            formInfo = formInfo + "<span class=\"guest-requirement-col-1\"></span>\n";
                                            formInfo = formInfo + "<span class=\"guest-requirement-col-2\"><input type=\"radio\" name=\"smoke_type_" + conditionID[productCount] + "_" + roomCount + "\" value=\"1\" checked=\"checked\" />Non-Smoking</span>\n";
                                            formInfo = formInfo + "<span class=\"guest-requirement-col-3\"></span><br class=\"clearAll\">\n";
                                            formInfo = formInfo + "</div>\n";
                                        }
                                        formInfo = formInfo + "<div><label><strong>floor:</strong></label>\n";
                                        formInfo = formInfo + "<span class=\"guest-requirement-col-1\"><input type=\"radio\" name=\"floor_type_" + conditionID[productCount] + "_" + roomCount + "\" value=\"3\" checked=\"checked\" />No preference</span>\n";
                                        formInfo = formInfo + "<span class=\"guest-requirement-col-2\"><input type=\"radio\" name=\"floor_type_" + conditionID[productCount] + "_" + roomCount + "\" value=\"1\" />High floor</span>\n";
                                        formInfo = formInfo + "<span class=\"guest-requirement-col-3\"><input type=\"radio\" name=\"floor_type_" + conditionID[productCount] + "_" + roomCount + "\" value=\"2\" />Low floor</span><br class=\"clearAll\">\n";
                                        formInfo = formInfo + "</div>\n";
                                        formInfo = formInfo + "<textarea name=\"sp_require_" + conditionID[productCount] + "_" + roomCount + "\"></textarea>\n";
                                        formInfo = formInfo + "</div>\n";
                                        formInfo = formInfo + "</div>\n";


                                    }
                                    else
                                    {
                                        if (!checkExtranet)
                                        {
                                            //optionRequire = optionRequire + "<div class=\"book_room\">Room : " + GetProductTitle(optionID[productCount], OptionTitleList) + "<span>(Subject to availability, can not guarantee)</span></div>\n";
                                            formInfo = formInfo + "<div class=\"formCustomer\">\n";
                                            formInfo = formInfo + "<div class=\"requirementCustomer\">\n";
                                            formInfo = formInfo + "<span class=\"fnMedium\"><strong>ห้องพัก :" + GetProductTitle(optionID[productCount], OptionTitleList) + roomCount + "</strong></span><br /><br />\n";

                                        }
                                        else
                                        {
                                            //optionRequire = optionRequire + "<div class=\"book_room\">Room : " + GetProductExtraTitle(optionID[productCount], OptionTitleListExtra) + "<span>(Subject to availability, can not guarantee)</span></div>\n";
                                            formInfo = formInfo + "<div class=\"formCustomer\">\n";
                                            formInfo = formInfo + "<div class=\"requirementCustomer\">\n";
                                            formInfo = formInfo + "<span class=\"fnMedium\"><strong>ห้องพัก :" + GetProductExtraTitle(optionID[productCount], OptionTitleListExtra) + roomCount + "</strong></span><br /><br />\n";

                                        }

                                        formInfo = formInfo + "<div><label><strong>ชนิดเตียง:</strong></label>\n";
                                        formInfo = formInfo + "<span class=\"guest-requirement-col-1\"><input type=\"radio\" name=\"bed_type_" + conditionID[productCount] + "_" + roomCount + "\" value=\"3\" checked=\"checked\" />ไม่ระบุ</span>\n";
                                        formInfo = formInfo + "<span class=\"guest-requirement-col-2\"><input type=\"radio\" name=\"bed_type_" + conditionID[productCount] + "_" + roomCount + "\" value=\"1\" />เตียงใหญ่</span>\n";
                                        formInfo = formInfo + "<span class=\"guest-requirement-col-3\"><input type=\"radio\" name=\"bed_type_" + conditionID[productCount] + "_" + roomCount + "\" value=\"2\" />เตียงคู่</span><br class=\"clearAll\">\n";
                                        formInfo = formInfo + "</div>\n";
                                        if (ProductID != 3590)
                                        {
                                            formInfo = formInfo + "<div><label><strong>สูบบุหรี่ :</strong></label>\n";
                                            formInfo = formInfo + "<span class=\"guest-requirement-col-1\"><input type=\"radio\" name=\"smoke_type_" + conditionID[productCount] + "_" + roomCount + "\" value=\"3\" checked=\"checked\" />ไม่ระบุ</span>\n";
                                            formInfo = formInfo + "<span class=\"guest-requirement-col-2\"><input type=\"radio\" name=\"smoke_type_" + conditionID[productCount] + "_" + roomCount + "\" value=\"1\" />ไม่สูบบุหรี่</span>\n";
                                            formInfo = formInfo + "<span class=\"guest-requirement-col-3\"><input type=\"radio\" name=\"smoke_type_" + conditionID[productCount] + "_" + roomCount + "\" value=\"2\" />สูบบุหรี่</span><br class=\"clearAll\">\n";
                                            formInfo = formInfo + "</div>\n";
                                        }
                                        else
                                        {
                                            formInfo = formInfo + "<div><label><strong>สูบบุหรี่ :</strong></label>\n";
                                            formInfo = formInfo + "<span class=\"guest-requirement-col-1\"></span>\n";
                                            formInfo = formInfo + "<span class=\"guest-requirement-col-2\"><input type=\"radio\" name=\"smoke_type_" + conditionID[productCount] + "_" + roomCount + "\" value=\"1\" checked=\"checked\" />ไม่สูบบุหรี่</span>\n";
                                            formInfo = formInfo + "<span class=\"guest-requirement-col-3\"></span><br class=\"clearAll\">\n";
                                            formInfo = formInfo + "</div>\n";
                                        }
                                        formInfo = formInfo + "<div><label><strong>ระดับชั้น:</strong></label>\n";
                                        formInfo = formInfo + "<span class=\"guest-requirement-col-1\"><input type=\"radio\" name=\"floor_type_" + conditionID[productCount] + "_" + roomCount + "\" value=\"3\" checked=\"checked\" />ไม่ระบุ</span>\n";
                                        formInfo = formInfo + "<span class=\"guest-requirement-col-2\"><input type=\"radio\" name=\"floor_type_" + conditionID[productCount] + "_" + roomCount + "\" value=\"1\" />ชั้นสูงๆ</span>\n";
                                        formInfo = formInfo + "<span class=\"guest-requirement-col-3\"><input type=\"radio\" name=\"floor_type_" + conditionID[productCount] + "_" + roomCount + "\" value=\"2\" />ชั้นล่างๆ</span><br class=\"clearAll\">\n";
                                        formInfo = formInfo + "</div>\n";
                                        formInfo = formInfo + "<textarea name=\"sp_require_" + conditionID[productCount] + "_" + roomCount + "\"></textarea>\n";
                                        formInfo = formInfo + "</div>\n";
                                        formInfo = formInfo + "</div>\n";
                                    }


                                }
                            }

                        }
                    }
                }
                //formInfo = formInfo + "</div>\n";
                break;
            case 32:
            case 34:
            case 36:
            case 38:
            case 39:
            case 40:
                //formInfo = formInfo + "<div class=\"formCustomer\">\n";
                //formInfo = formInfo + "<div class=\"formCustomer\">\n";
                //formInfo = formInfo + "<h3 class=\"fnBig fnBlue\">Guest & Information Preference</h3>\n";
                //formInfo = formInfo + "<span class=\"fnGrayLight\">(Subject to availability, can not guarantee)</span>\n";



                for (int productCount = 0; productCount < conditionID.Count(); productCount++)
                {
                    if (quantity[productCount] > 0)
                    {
                        if (langID == 1)
                        {
                            formInfo = formInfo + "<div class=\"formCustomer\">\n";
                            formInfo = formInfo + "<h3 class=\"fnBig fnBlue\">Special Requirement</h3>\n";
                            formInfo = formInfo + "<span class=\"fnGrayLight\">(Subject to availability, can not guarantee)</span><br/>\n";
                            formInfo = formInfo + "<textarea name=\"sp_require_" + conditionID[productCount] + "_1\" style=\"top:0px; position:static; left:0px;width:420px;height:100px;\"></textarea>\n";
                            formInfo = formInfo + "</div>\n";
                        }
                        else
                        {
                            formInfo = formInfo + "<div class=\"formCustomer\">\n";
                            formInfo = formInfo + "<h3 class=\"fnBig fnBlue\">ความต้องการพิเศษ</h3>\n";
                            formInfo = formInfo + "<span class=\"fnGrayLight\">(ความต้องการพิเศษและคำขอพิเศษทั้งหมดขึ้นอยู่กับทางโรงแรมซึ่งจะไม่สามารถรับประกันได้แต่เราจะพยายามอย่างดีที่สุดเพื่อให้ท่านได้คำขอพิเศษตามที่ท่านได้ระบุไว้)</span><br/><br/>\n";
                            formInfo = formInfo + "<textarea name=\"sp_require_" + conditionID[productCount] + "_1\" style=\"top:0px; position:static; left:0px;width:420px;height:100px;\"></textarea>\n";
                            formInfo = formInfo + "</div>\n";

                        }
                        //formInfo = formInfo + "<div class=\"requirementCustomer\">\n";
                        //formInfo = formInfo + "<span class=\"fnMedium\"><strong>Room :" + GetProductTitle(optionID[productCount], OptionTitleList) + "</strong></span><br /><br />\n";

                        //formInfo = formInfo + "</div>\n";
                        //optionRequire = optionRequire + "<table class=\"guest\" cellpadding=\"3\" cellspacing=\"1\">\n";
                        //optionRequire = optionRequire + "<tr>\n";
                        //optionRequire = optionRequire + "<th align=\"left\">คำขอพิเศษ </th>\n";
                        //optionRequire = optionRequire + "</tr>\n";
                        //optionRequire = optionRequire + "<tr>\n";
                        //optionRequire = optionRequire + "<td>\n";
                        //optionRequire = optionRequire + "<textarea name=\"sp_require_" + conditionID[productCount] + "_1\" style=\"width:500px;height:100px;\"></textarea>\n";
                        //optionRequire = optionRequire + "</td>\n";
                        //optionRequire = optionRequire + "</tr >\n";
                        //optionRequire = optionRequire + "</table >\n";
                        //optionRequire = optionRequire + "<div class=\"clear-all\"></div><br />\n";

                        break;

                    }
                }

                break;
        }

        //optionRequire = optionRequire + "<input type=\"hidden\" name=\"ln\" value=\"" + langID + "\">\n";
        //optionRequire = optionRequire + "<input type=\"hidden\" name=\"discount\" value=\"" + Request.Form["discount"] + "\">\n";
        //optionRequire = optionRequire + "<input type=\"hidden\" name=\"adult\" value=\"" + adult + "\">\n";
        //optionRequire = optionRequire + "<input type=\"hidden\" name=\"child\" value=\"" + child + "\">\n";
        //optionRequire = optionRequire + "<input type=\"hidden\" name=\"sid\" value=\"" + Request.Form["sid"] + "\">\n";
        //optionRequire = optionRequire + "<input type=\"hidden\" name=\"hotel_id\" value=\"" + Request.Form["hotel_id"] + "\">\n";
        //optionRequire = optionRequire + "<input type=\"hidden\" name=\"date_start\" value=\"" + Request.Form["date_start"] + "\">\n";
        //optionRequire = optionRequire + "<input type=\"hidden\" name=\"date_end\" value=\"" + Request.Form["date_end"] + "\">\n";
        //optionRequire = optionRequire + "<input type=\"hidden\" name=\"refCountry\" value=\"" + Request.Form["refCountry"] + "\">\n";

        if (has_allotment)
        {
            formInfo = formInfo + "<input type=\"hidden\" name=\"al\" value=\"1\">\n";
        }
        else {
            formInfo = formInfo + "<input type=\"hidden\" name=\"al\" value=\"0\">\n";
        }

       if(IsMember)
        {
            formInfo = formInfo + "<input type=\"hidden\" name=\"au_m\" value=\"1\">\n";
        }
        else {
            formInfo = formInfo + "<input type=\"hidden\" name=\"au_m\" value=\"0\">\n";
        }

        formInfo = formInfo + "<input type=\"hidden\" name=\"ln\" value=\"" + langID + "\">\n";
        formInfo = formInfo + "<input type=\"hidden\" name=\"discount\" value=\"" + Request.Form["discount"] + "\">\n";
        formInfo = formInfo + "<input type=\"hidden\" name=\"adult\" value=\"" + adult + "\">\n";
        formInfo = formInfo + "<input type=\"hidden\" name=\"child\" value=\"" + child + "\">\n";
        formInfo = formInfo + "<input type=\"hidden\" name=\"sid\" value=\"" + Request.Form["sid"] + "\">\n";
        formInfo = formInfo + "<input type=\"hidden\" name=\"hotel_id\" value=\"" + Request.Form["hotel_id"] + "\">\n";
        formInfo = formInfo + "<input type=\"hidden\" name=\"date_start\" value=\"" + Request.Form["date_start"] + "\">\n";
        formInfo = formInfo + "<input type=\"hidden\" name=\"date_end\" value=\"" + Request.Form["date_end"] + "\">\n";
        formInfo = formInfo + "<input type=\"hidden\" name=\"refCountry\" value=\"" + Request.Form["refCountry"] + "\">\n";

        //for Agency 
        formInfo = formInfo + "<input type=\"hidden\" name=\"hdAgencyID\" value=\"" + Request.Form["hdAgencyID"] + "\">\n";

        if (categoryID == 32)
        {
            //optionRequire = optionRequire + "<input type=\"hidden\" name=\"tee_off_hour\" value=\"" + Request.Form["tee_hour"] + "\">\n";
            //optionRequire = optionRequire + "<input type=\"hidden\" name=\"tee_off_min\" value=\"" + Request.Form["tee_min"] + "\">\n";
            formInfo = formInfo + "<input type=\"hidden\" name=\"tee_off_hour\" value=\"" + Request.Form["tee_hour"] + "\">\n";
            formInfo = formInfo + "<input type=\"hidden\" name=\"tee_off_min\" value=\"" + Request.Form["tee_min"] + "\">\n";
        }

        //optionRequire = optionRequire + "<input type=\"hidden\" name=\"sum_price\" value=\"" + (int)(priceVatInc) + "\">\n";
        //optionRequire = optionRequire + selectOption;
        //optionRequire = optionRequire + "</div>\n";
        //optionRequire = optionRequire + "<!--bg setp-->\n";
        //optionRequire = optionRequire + "<div class=\"end_step\"></div>\n";
        if(productDeposit.Deposit==0)
        {
            formInfo = formInfo + "<input type=\"hidden\" name=\"sum_price\" value=\"" + (int)priceVatInc + "\">\n";
        }else{
            if (priceVatInc>totalPriceDeposit)
            {
                formInfo = formInfo + "<input type=\"hidden\" name=\"sum_price\" value=\"" + (int)totalPriceDeposit + "\">\n";
            }else{
                formInfo = formInfo + "<input type=\"hidden\" name=\"sum_price\" value=\"" + (int)priceVatInc + "\">\n";
            }
        }
        
        
        formInfo = formInfo + selectOption;



        string paymentForm = string.Empty;

        
        

        if (payLater.ProductID == 0)
        {

            if (langID == 1)
            {
                
                formInfo = formInfo + "<div id=\"paymentMethod\">\n";
                formInfo = formInfo + "<h3 class=\"fnBig fnBlue\">Payment Method</h3>\n";

                if (totalPriceDeposit == (int)priceVatInc)
                {
                    formInfo = formInfo + "<span class=\"fnBig fnGray\">Grand Total Price : " + ((int)priceVatInc).ToString("#,###") + " Baht" + priceSummaryDisplay + "</span> <a href=\"javascript:void(0)\" class=\"tooltip fnGold\">(Included of Tax and Service Charge)<span class=\"tooltip_content\"><strong>No hidden cost and no surprised charge.</strong> You can see the grand total in net rate without additional charge when you check in at hotel or use the service.</span></a><br /><br />\n";
                }else{
                    if (int.Parse(Request.Form["hotel_id"]) == 3465)
                    {
                        formInfo = formInfo + "<span class=\"fnBig fnGray\">Grand Total Price : " + ((int)priceVatInc).ToString("#,###") + " Baht" + priceSummaryDisplay + "</span> <a href=\"javascript:void(0)\" class=\"tooltip fnGold\">(Included of Tax and Service Charge)<span class=\"tooltip_content\"><strong>No hidden cost and no surprised charge.</strong> You can see the grand total in net rate without additional charge when you check in at hotel or use the service.</span></a><br />\n";

                        formInfo = formInfo + "Please pay this total room rate when you check in at the hotel<br><br>";
                    }
                    else
                    {
                        formInfo = formInfo + "<br/><p><span class=\"fnSmall fnBlue fnBold\">Total " + ((int)priceVatInc).ToString("#,###") + " Baht" + priceSummaryDisplay + "</span> <a href=\"javascript:void(0)\" class=\"tooltip fnGold\">(Included of Tax and Service Charge)<span class=\"tooltip_content\"><strong>No hidden cost and no surprised charge.</strong> You can see the grand total in net rate without additional charge when you check in at hotel or use the service.</span></a>. You are requested to pay the " + depositWord + " to secure your booking. The outstanding balance can be paid upon your check in time.</p><br />";
                        formInfo = formInfo + "<p><span class=\"fnRed fnBold\">*</span> You will be charged in local currency (Thai Baht). The displayed amount in your currency is indicative and based on today’s exchange rate.</span><br /></p><br/>\n";
                    }
                }

                if (paymentMethod == 2)
                {
                    if (has_allotment || ManageID == 2)
                    {
                        if (gatewayID == 3 || gatewayID==13)
                        {
                            formInfo = formInfo + "<div class=\"card_offer\"><input type=\"radio\" name=\"bank_id\" value=\"" + gatewayID + "\" checked=\"checked\"  style=\"float:left; margin-top:8px;\" /><img src=\"../theme_color/blue/images/layout_mail/VisaMastercard.jpg\" /></div>\n";

                        }else{
                            formInfo = formInfo + "<div class=\"card_offer\"><input type=\"radio\" name=\"bank_id\" value=\"" + gatewayID + "\" checked=\"checked\"  style=\"float:left; margin-top:8px;\" /><img src=\"../theme_color/blue/images/layout_mail/VisaMastercard.jpg\" /><img src=\"../theme_color/blue/images/layout_mail/jcb.jpg\" /></div>\n";
                        }
                    }
                    else {
                        formInfo = formInfo + "<div class=\"card_offer\"><input type=\"hidden\" name=\"bank_id\" value=\"" + gatewayID + "\"/></div>\n";
                    }
                }else{
                    if (int.Parse(Request.Form["hotel_id"]) == 3465)
                    {
                        //Only C House Executive Condomenium
                        formInfo = formInfo + "<input type=\"hidden\" id=\"cardnum\" name=\"cardnum\" value=\"only_c-house\"/></td>\n";

                    }
                    else {
                        formInfo = formInfo + "<div class=\"requirementCustomer\">\n";
                        formInfo = formInfo + "<h3 class=\"fnBig fnBlue\">Credit Card Details</h3>\n";
                        formInfo = formInfo + "<table id=\"cardForm\" cellpadding=\"0\" cellspacing=\"0\">\n";
                        formInfo = formInfo + "<tr>\n";
                        formInfo = formInfo + "<td class=\"right\">Card Type  &nbsp;</td>\n";
                        formInfo = formInfo + "<td>\n";
                        formInfo = formInfo + "<select id=\"cardType\" name=\"cardType\" style=\"width:70px;\">\n";
                        formInfo = formInfo + "<option value=\"Visa\">Visa</option>\n";
                        formInfo = formInfo + "<option value=\"MasterCard\">Master Card</option>\n";
                        formInfo = formInfo + "<option value=\"JCB\">JCB</option>\n";
                        formInfo = formInfo + "</select>\n";
                        formInfo = formInfo + "</td>\n";

                        formInfo = formInfo + "</tr>\n";
                        formInfo = formInfo + "<tr>\n";
                        formInfo = formInfo + "<td class=\"right\" valign=\"top\">Credit Card Number <span class=\"fnRed\">*</span></td>\n";
                        formInfo = formInfo + "<td><input type=\"text\" id=\"cardnum\" name=\"cardnum\" maxlength=\"16\" class=\"digitCard\" autocomplete=\"off\"/></td>\n";
                        formInfo = formInfo + "</tr>\n";
                        formInfo = formInfo + "<tr>\n";
                        formInfo = formInfo + "<td class=\"right\" valign=\"top\">Security Code (CVV2)&nbsp;<span class=\"fnRed\">*</span></td>\n";
                        formInfo = formInfo + "<td><input name=\"card_cvv\" id=\"card_cvv\" maxlength=\"3\" style=\"width: 50px;\" class=\"digitCard required valid\" type=\"text\" autocomplete=\"off\">&nbsp;&nbsp;<span class=\"cvv\">Last 3 digits on the back of the card</span>&nbsp;&nbsp;<a href=\"javascript:void(0)\" class=\"tooltip fnGold\"><img src=\"../images/ico_card_info.png\" /><span class=\"tooltip_content\"><img src=\"../images/cvv_card.jpg\" /></span></a></td>\n";
                        formInfo = formInfo + "</tr>\n";
                        formInfo = formInfo + "<tr>\n";
                        formInfo = formInfo + "<td class=\"right\">Expiry Date (mm/yyyy) <span class=\"fnRed\">*</span></td>\n";
                        formInfo = formInfo + "<td>\n";
                        formInfo = formInfo + "<select name=\"card_month\" id=\"card_month\" style=\"width:50px;\">\n";
                        formInfo = formInfo + "<option value=\"1\">1</option>\n";
                        formInfo = formInfo + "<option value=\"2\">2</option>\n";
                        formInfo = formInfo + "<option value=\"3\">3</option>\n";
                        formInfo = formInfo + "<option value=\"4\">4</option>\n";
                        formInfo = formInfo + "<option value=\"5\">5</option>\n";
                        formInfo = formInfo + "<option value=\"6\" selected=\"selected\">6</option>\n";
                        formInfo = formInfo + "<option value=\"7\">7</option>\n";
                        formInfo = formInfo + "<option value=\"8\">8</option>\n";
                        formInfo = formInfo + "<option value=\"9\">9</option>\n";
                        formInfo = formInfo + "<option value=\"10\">10</option>\n";
                        formInfo = formInfo + "<option value=\"11\">11</option>\n";
                        formInfo = formInfo + "<option value=\"12\">12</option>\n";
                        formInfo = formInfo + "</select>\n";
                        formInfo = formInfo + "<select name=\"card_year\" id=\"card_year\" style=\"width:50px;\">\n";
                        formInfo = formInfo + "<option value=\"2011\" selected=\"selected\">2011</option>\n";
                        formInfo = formInfo + "<option value=\"2012\">2012</option>\n";
                        formInfo = formInfo + "<option value=\"2013\">2013</option>\n";
                        formInfo = formInfo + "<option value=\"2014\">2014</option>\n";
                        formInfo = formInfo + "<option value=\"2015\">2015</option>\n";
                        formInfo = formInfo + "<option value=\"2016\">2016</option>\n";
                        formInfo = formInfo + "<option value=\"2017\">2017</option>\n";
                        formInfo = formInfo + "<option value=\"2018\">2018</option>\n";
                        formInfo = formInfo + "<option value=\"2019\">2019</option>\n";
                        formInfo = formInfo + "<option value=\"2020\">2020</option>\n";
                        formInfo = formInfo + "<option value=\"2021\">2021</option>\n";
                        formInfo = formInfo + "</select>\n";
                        formInfo = formInfo + "</td>\n";
                        formInfo = formInfo + "</tr>\n";
                        formInfo = formInfo + "<tr>\n";
                        formInfo = formInfo + "<td class=\"right\" valign=\"top\">Card Holder Name <span class=\"fnRed\">*</span></td>\n";
                        formInfo = formInfo + "<td><input name=\"card_name\" id=\"card_name\" class=\"required valid\" maxlength=\"40\" type=\"text\"></td>\n";
                        formInfo = formInfo + "</tr>\n";
                        formInfo = formInfo + "<tr>\n";
                        formInfo = formInfo + "<td class=\"right\" valign=\"top\">Bank Name <span class=\"fnRed\">*</span></td>\n";
                        formInfo = formInfo + "<td><input name=\"bank_name\" id=\"bank_name\" class=\"required valid\" maxlength=\"40\" type=\"text\"></td>\n";
                        formInfo = formInfo + "</tr>\n";
                        formInfo = formInfo + "</table>\n";
                        formInfo = formInfo + "</div>\n";
                    }
                    
                    
                }
            }
            

            formInfo = formInfo + "<br class=\"clearAll\" />\n";
            if (langID == 1)
            {
                if (has_allotment || ManageID == 2)
                {
                    formInfo = formInfo + "<div><a href=\"javascript:void(0)\" onclick=\"viewBooking();\" style=\"float:left\"><img src=\"/images/btn_edit_booking.gif\" id=\"editBooking\" /></a><input type=\"submit\" class=\"btnPayment-en\" id=\"btnPayment\" name=\"submit\" value=\"\" /></div>\n";
                }
                else { 
                    formInfo = formInfo + "<div><a href=\"javascript:void(0)\" onclick=\"viewBooking();\" style=\"float:left\"><img src=\"/images/btn_edit_booking.gif\" id=\"editBooking\" /></a><input type=\"submit\" class=\"btnConfirm\" id=\"btnPayment\" name=\"submit\" value=\"\" /></div>\n";
                }
            }
            else
            {
                formInfo = formInfo + "<div><a href=\"javascript:void(0)\" onclick=\"viewBooking();\" style=\"float:left\"><img src=\"/images/btn_edit_booking_th.gif\" id=\"editBooking\" /></a><input type=\"submit\" class=\"btnPayment-th\" id=\"btnPayment\" name=\"submit\" value=\"\" /></div>\n";
            }

            formInfo = formInfo + "<br class=\"clearAll\" />\n";
            formInfo = formInfo + "</div>\n";
            formInfo = formInfo + "</div>\n";
            Keyword = Utility.GetKeywordReplace(layout, "<!--###BookingInformationStart###-->", "<!--###BookingInformationEnd###-->");
            layout = layout.Replace(Keyword, formInfo);

            Keyword = Utility.GetKeywordReplace(layout, "<!--###HotelHeaderStart###-->", "<!--###HotelHeaderEnd###-->");
            layout = layout.Replace(Keyword, HotelHeader);

            layout = layout.Replace("<!--###cssHotelBook###-->", "<link href=\"http://www.booking2hotels.com/hotels-template/"+HotelFolder+"/css/bookForm.css\" rel=\"stylesheet\" type=\"text/css\" />");
            

            
            Response.Write(layout);
            Response.End();

            //old
            //    paymentForm = paymentForm + "<div class=\"bg_step\">\n";
            //    paymentForm = paymentForm + "<div class=\"make_payment\">\n";
            //    paymentForm = paymentForm + "<span>Total Price :</span> " + Math.Round(priceVatInc).ToString("#,###") + " Baht" + priceSummaryDisplay + " <span class=\"include\">(Included of Tax and Service Charge)</span> \n";


            //    GatewayMethod objGateway = new GatewayMethod(ProductID);

            //    byte gatewayID = objGateway.GetGateway();

            //    if (gatewayID == 3)
            //    {
            //        paymentForm = paymentForm + "<div class=\"book_visa\"><input type=\"radio\" checked=\"checked\" name=\"bank_id\" value=\"3\" /><img src=\"../theme_color/blue/images/layout_mail/VisaMastercard.jpg\" /></div> \n";
            //        paymentForm = paymentForm + "<div class=\"book_visa\"><input type=\"radio\" name=\"bank_id\" value=\"6\" align=\"top\"/><img src=\"../theme_color/blue/images/layout_mail/jcb.jpg\" /></div> \n";
            //    }
            //    else
            //    {
            //        paymentForm = paymentForm + "<div class=\"book_visa\"><input type=\"radio\" name=\"bank_id\" value=\"6\" checked=\"checked\" /><img src=\"../theme_color/blue/images/layout_mail/VisaMastercard.jpg\" /><img src=\"../theme_color/blue/images/layout_mail/jcb.jpg\" /></div>\n";
            //    }




            //paymentForm = paymentForm + "</div>\n";
            //paymentForm = paymentForm + "<div class=\"check_out\">\n";
            //paymentForm = paymentForm + "<input type=\"submit\" value=\"\" class=\"check_buttom\" />\n";
            //paymentForm = paymentForm + "</div>\n";
            //paymentForm = paymentForm + "<div class=\"clear-all\"></div><br />\n";
            //paymentForm = paymentForm + "</div> \n";
            //paymentForm=paymentForm+"<div class=\"end_step\"></div>";

        }
        else
        {
            paymentForm = paymentForm + "<div class=\"bg_write\">\n";
            paymentForm = paymentForm + "<table id=\"cardForm\"cellpadding=\"0\" cellspacing=\"0\">\n";
            paymentForm = paymentForm + "<tr>\n";
            paymentForm = paymentForm + "<td class=\"right\">Card Type  &nbsp;</td>\n";
            paymentForm = paymentForm + "<td>\n";
            paymentForm = paymentForm + "<select id=\"cardType\" name=\"cardType\" style=\"width:70px;\">\n";
            paymentForm = paymentForm + "<option value=\"Visa\">Visa</option>\n";
            paymentForm = paymentForm + "<option value=\"MasterCard\">Master Card</option>\n";
            paymentForm = paymentForm + "<option value=\"JCB\">JCB</option>\n";
            paymentForm = paymentForm + "</select>\n";
            paymentForm = paymentForm + "</td>\n";
            paymentForm = paymentForm + "<td rowspan=\"6\" valign=\"top\" >\n";
            paymentForm = paymentForm + "<div style=\"padding-left:10px;border-left:2px solid #555;\">\n";
            paymentForm = paymentForm + "<span style=\"font-size:16px;color:#555;line-height:18px;\">Total Price :<span style=\"color:#406c04\">" + ((int)priceVatInc).ToString("#,###") + " Baht" + priceSummaryDisplay + "</span><br />\n";
            paymentForm = paymentForm + "<span style=\"font-size:12px;color:#397c96;font-weight:normal\">(Included of Tax and Service Charge)</span>\n";
            paymentForm = paymentForm + "</span>\n";
            paymentForm = paymentForm + "<br/>\n";
            paymentForm = paymentForm + "<span style=\"text-align:left;width:220px;font-size:12px;color:#555;font-weight:normal;line-height:20px;\">**You will be charged only <span style=\"font-weight:bold;color:#406c04\">$1</span> for holding guarantee of your booking.</span>\n";
            paymentForm = paymentForm + "</div>\n";
            paymentForm = paymentForm + "</td>\n";
            paymentForm = paymentForm + "</tr>\n";
            paymentForm = paymentForm + "<tr>\n";
            paymentForm = paymentForm + "<td class=\"right\" valign=\"top\">Credit Card Number <span>*</span></td>\n";
            paymentForm = paymentForm + "<td><input type=\"text\" id=\"cardnum\" name=\"cardnum\" class=\"digitCard\" autocomplete=\"off\"  maxlength=\"16\"/></td>\n";
            paymentForm = paymentForm + "</tr>\n";
            paymentForm = paymentForm + "<tr>\n";
            paymentForm = paymentForm + "<td class=\"right\" valign=\"top\">Security Code (CVV2)&nbsp;<a href=\"/images/cvv_card.jpg\" class=\"imgFloat\" onclick=\"return false\"><img src=\"../theme_color/blue/images/layout_mail/cvv.jpg\" /></a>&nbsp;<span>*</span></td>\n";
            paymentForm = paymentForm + "<td><input name=\"card_cvv\" id=\"card_cvv\" maxlength=\"3\" style=\"width: 50px;\" class=\"digitCard required valid\" type=\"text\" autocomplete=\"off\"><span class=\"cvv\">Last 3 digits on the back of the card</span></td>\n";
            paymentForm = paymentForm + "</tr>\n";
            paymentForm = paymentForm + "<tr>\n";
            paymentForm = paymentForm + "<td class=\"right\">Expiry Date (mm/yyyy) <span>*</span></td>\n";
            paymentForm = paymentForm + "<td>\n";
            paymentForm = paymentForm + "<select name=\"card_month\" id=\"card_month\" style=\"width:50px;\">\n";
            paymentForm = paymentForm + "<option value=\"1\">1</option>\n";
            paymentForm = paymentForm + "<option value=\"2\">2</option>\n";
            paymentForm = paymentForm + "<option value=\"3\">3</option>\n";
            paymentForm = paymentForm + "<option value=\"4\">4</option>\n";
            paymentForm = paymentForm + "<option value=\"5\">5</option>\n";
            paymentForm = paymentForm + "<option value=\"6\" selected=\"selected\">6</option>\n";
            paymentForm = paymentForm + "<option value=\"7\">7</option>\n";
            paymentForm = paymentForm + "<option value=\"8\">8</option>\n";
            paymentForm = paymentForm + "<option value=\"9\">9</option>\n";
            paymentForm = paymentForm + "<option value=\"10\">10</option>\n";
            paymentForm = paymentForm + "<option value=\"11\">11</option>\n";
            paymentForm = paymentForm + "<option value=\"12\">12</option>\n";
            paymentForm = paymentForm + "</select>\n";
            paymentForm = paymentForm + "<select name=\"card_year\" id=\"card_year\" style=\"width:50px;\">\n";
            paymentForm = paymentForm + "<option value=\"2011\" selected=\"selected\">2011</option>\n";
            paymentForm = paymentForm + "<option value=\"2012\">2012</option>\n";
            paymentForm = paymentForm + "<option value=\"2013\">2013</option>\n";
            paymentForm = paymentForm + "<option value=\"2014\">2014</option>\n";
            paymentForm = paymentForm + "<option value=\"2015\">2015</option>\n";
            paymentForm = paymentForm + "<option value=\"2016\">2016</option>\n";
            paymentForm = paymentForm + "<option value=\"2017\">2017</option>\n";
            paymentForm = paymentForm + "<option value=\"2018\">2018</option>\n";
            paymentForm = paymentForm + "<option value=\"2019\">2019</option>\n";
            paymentForm = paymentForm + "<option value=\"2020\">2020</option>\n";
            paymentForm = paymentForm + "<option value=\"2021\">2021</option>\n";
            paymentForm = paymentForm + "</select>\n";
            paymentForm = paymentForm + "</td>\n";
            paymentForm = paymentForm + "</tr>\n";
            paymentForm = paymentForm + "<tr>\n";
            paymentForm = paymentForm + "<td class=\"right\" valign=\"top\">Card Holder Name <span>*</span></td>\n";
            paymentForm = paymentForm + "<td><input name=\"card_name\" id=\"card_name\" class=\"required valid\" maxlength=\"40\" type=\"text\"></td>\n";
            paymentForm = paymentForm + "</tr>\n";
            paymentForm = paymentForm + "<tr>\n";
            paymentForm = paymentForm + "<td class=\"right\" valign=\"top\">Bank Name <span>*</span></td>\n";
            paymentForm = paymentForm + "<td><input name=\"bank_name\" id=\"bank_name\" class=\"required valid\" maxlength=\"40\" type=\"text\"></td>\n";
            paymentForm = paymentForm + "</tr>\n";
            paymentForm = paymentForm + "</table>\n";

            //paymentForm = paymentForm + "<div class=\"total_review\">\n";
            //paymentForm = paymentForm + "<div><span style=\"color:#000;\">Total Price :</span> " + Math.Round(priceVatInc).ToString("#,###") + " Baht" + priceSummaryDisplay + "<br />\n";
            //paymentForm = paymentForm + "<span class=\"included\">(Included of Tax and Service Charge)</span></div><br />\n";
            //paymentForm = paymentForm + "<div class=\"charged\"><span>**</span>You will be charged only <span>$1</span> <br />for holding guarantee of your booking.</div> \n";
            //paymentForm = paymentForm + "</div>\n";
            paymentForm = paymentForm + "<div class=\"clear-all\"></div>\n";
            paymentForm = paymentForm + "<input type=\"submit\" value=\"\" class=\"check_buttom\" style=\"margin:10px 0 0 240px\" />\n";
            paymentForm = paymentForm + "</div>\n";

        }

        /* Bank Transfer*/
        //paymentForm = paymentForm + "<br /><br /><br /><div class=\"bg_write\">\n";
        //paymentForm = paymentForm + "<input type=\"hidden\" name=\"cTransfer\" value=\"1\">\n";
        //paymentForm = paymentForm + "<div class=\"make_payment\"><img src=\"../theme_color/blue/images/layout_mail/bank.jpg\" style=\"margin-left:35px;\" />\n";
        //paymentForm = paymentForm + "</div>\n";
        //paymentForm = paymentForm + "<div class=\"check_out\" style=\"margin:15px 0 0 0px;\"><input type=\"submit\" value=\"\" class=\"transfer\" /></div>\n";
        //paymentForm = paymentForm + "<div class=\"clear-all\"></div>\n";
        //paymentForm = paymentForm + "<p class=\"post\">You will be taken to a page that summarizes your booking and instructions on how to make a payment. You may print out that page for your future reference.</p>\n";
        //paymentForm = paymentForm + "</div>\n";
        if (langID == 2)
        {
            paymentForm = paymentForm + "<br /><br /><br />\n";
            paymentForm = paymentForm + "<input type=\"hidden\" id=\"cTransfer\" name=\"cTransfer\" value=\"0\">\n";
            paymentForm = paymentForm + "<div class=\"bg_write\">\n";
            paymentForm = paymentForm + "<div class=\"make_payment\"><img src=\"../theme_color/blue/images/layout_mail/bank.jpg\" style=\"margin-left:35px;\" />\n";
            paymentForm = paymentForm + "</div>\n";
            paymentForm = paymentForm + "<div class=\"check_out\" style=\"margin:15px 0 0 0px;\"><input type=\"submit\" value=\"\" class=\"transfer\" onclick=\"$('#cTransfer').val('1');\"/></div>\n";
            paymentForm = paymentForm + "<div class=\"clear-all\"></div>\n";
            paymentForm = paymentForm + "<p class=\"post\">You will be taken to a page that summarizes your booking and instructions on how to make a payment. You may print out that page for your future reference.</p>\n";
            paymentForm = paymentForm + "</div>\n";
        }

        /**/
        Keyword = Utility.GetKeywordReplace(layout, "<!--###PaymentFormStart###-->", "<!--###PaymentFormEnd###-->");


        if (has_allotment && int.Parse(Request.Form["hotel_id"]) == 3466)
        {
            
        }else{
            layout = layout.Replace(Keyword, paymentForm);
        }
        Response.Write(layout);
    }

    public string GetRoomDetail()
    {
        string result = "<div class=\"bg_step\">";
        result = result + "<div class=\"book_hotel_pic\"><img src=\"../theme_color/blue/images/layout_mail/keeree.jpg\" /></div>";
        result = result + "<div class=\"book_hotel_detail\">";
        result = result + "<div class=\"book_hotel_name\">Krabi La Playa Resort</div>";
        result = result + "<div class=\"book_hotel_address\">Krabi La Playa Resort Address : 143 Moo 3 Tambol Ao Nang,Amphur Muang, Krabi 81000</div>";
        result = result + "<div class=\"book_hotel_in\"><span>Check in :</span>&nbsp;&nbsp;Tuesday,August 23, 2011 </div>";
        result = result + "<div class=\"book_hotel_adult\"><span>Adult(s) :</span>&nbsp;    1</div>";
        result = result + "<div class=\"book_hotel_in\"><span>Check out :</span>Wednesday,August 24, 2011  </div>";
        result = result + "<div class=\"book_hotel_adult\"><span>Children :</span>    0</div>";
        result = result + "</div> ";
        result = result + "<div class=\"clear-all\"></div> ";
        return result;
    }

    public string RenderItemList(string itemTitle, decimal itemQuantity, decimal itemNight, decimal itemPriceAverage, decimal itemPriceReal, decimal itemPriceABF, byte itemType)
    {
        string result = "";
        switch (itemType)
        {
            case 29:


                //result = result + "<tr>\n";
                //result = result + "<td class=\"book_room_type\">" + itemTitle + "</td>\n";
                //result = result + "<td valign=\"middle\">" + itemQuantity + "</td> \n";
                //result = result + "<td valign=\"middle\">" + itemNight + "</td> \n";
                //result = result + "<td valign=\"middle\">" + itemPriceAverage.ToString("#,###") + "</td> \n";
                //result = result + "<td valign=\"middle\" class=\"book_vat_sub\" align=\"right\">" + itemPriceReal.ToString("#,###") + " Baht</td>\n";
                //result = result + "</tr>\n";


                if (itemPriceABF != 0)
                {
                    result = result + "<tr>\n";
                    result = result + "<td class=\"book_room_type\" colspan=\"4\">Compulsory ABF for free night compulsory</td>\n";
                    result = result + "<td valign=\"middle\" class=\"book_vat_sub\" align=\"right\">" + itemPriceABF.ToString("#,###") + " Baht</td>\n";
                    result = result + "</tr>\n";
                }

                break;
            default:
                result = result + "<tr>\n";
                result = result + "<td class=\"book_room_type\">" + itemTitle + "</td>\n";
                result = result + "<td valign=\"middle\">" + itemQuantity + "</td>\n";
                result = result + "<td valign=\"middle\">" + itemPriceAverage.ToString("#,###") + "</td>\n";
                result = result + "<td valign=\"middle\" class=\"book_vat_sub\" align=\"right\">" + itemPriceReal.ToString("#,###") + " Baht</td>\n";
                result = result + "</tr>\n";
                break;

        }



        return result;
    }


    public string GetPromotionTitle(int PromotionID)
    {
        string result = string.Empty;
        using (SqlConnection cn = new SqlConnection(connString))
        {
            string sqlCommand = "select top 1 pc.title,";
            sqlCommand = sqlCommand + "(select top 1 spc.title from tbl_promotion_content spc where spc.promotion_id=pm.promotion_id and spc.lang_id=" + langID + ") as second_lang ";
            sqlCommand = sqlCommand + " from tbl_promotion pm,tbl_promotion_content pc";
            sqlCommand = sqlCommand + " where pm.promotion_id=pc.promotion_id  and pc.lang_id=1 and pc.promotion_id=" + PromotionID;
            SqlCommand cmd = new SqlCommand(sqlCommand, cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                if (langID == 1)
                {
                    result = reader["title"].ToString();
                }
                else
                {
                    result = reader["second_lang"].ToString();
                    if (string.IsNullOrEmpty(result))
                    {
                        result = reader["title"].ToString();
                    }
                }

            }
            return result;
        }

    }

 

    public List<string> GetPromotionExtra(int PromotionID, byte langID)
    {
        using (SqlConnection cn = new SqlConnection(connString))
        {
            List<string> result = new List<string>();
            string sqlCommand = "select top 1 pmc_ex.title,";
            sqlCommand = sqlCommand + " (select top 1 spc.title from tbl_promotion_content_extra_net spc where spc.promotion_id=pm_ex.promotion_id and spc.lang_id=" + langID + ") as second_lang,";
            sqlCommand = sqlCommand + " pm_ex.iscancellation,pmc_ex.detail";
            sqlCommand = sqlCommand + " from tbl_promotion_extra_net pm_ex,tbl_promotion_content_extra_net pmc_ex";
            sqlCommand = sqlCommand + " where pm_ex.promotion_id=pmc_ex.promotion_id and pmc_ex.lang_id=1 and pm_ex.promotion_id=" + PromotionID;
            SqlCommand cmd = new SqlCommand(sqlCommand, cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                if (langID == 1)
                {
                    result.Add(reader["title"].ToString());
                    result.Add(reader["iscancellation"].ToString());
                    result.Add(reader["detail"].ToString());
                }
                else
                {
                    if (string.IsNullOrEmpty(reader["second_lang"].ToString()))
                    {
                        result.Add(reader["title"].ToString());
                    }
                    else
                    {
                        result.Add(reader["second_lang"].ToString());
                    }

                    result.Add(reader["iscancellation"].ToString());
                    result.Add(reader["detail"].ToString());
                }

            }
            return result;
        }

    }

    public string RenderPayLaterForm()
    {
        string result = "<table id=\"cardForm\"><tr>\n";
        result = result + "<td class=\"card_bg\" width=\"25%\"><font class=\"card_detail_color\">Card Type:<span class=\"mandatory_mark\"> *</span></font></td>\n";
        result = result + "<td class=\"card_bg\">\n";
        result = result + "<select id=\"cardType\" name=\"cardType\">\n";
        result = result + "<option value=\"Visa\">Visa</option>\n";
        result = result + "<option value=\"MasterCard\">Master Card</option>\n";
        result = result + "<option value=\"JCB\">JCB</option>\n";
        result = result + "</select>\n";
        result = result + "</td>\n";
        result = result + "</tr>\n";
        result = result + "<tr>\n";
        result = result + "<td class=\"card_bg\"><font class=\"card_detail_color\">Credit Card Number:<span class=\"mandatory_mark\"> *</span></font></td>\n";
        result = result + "<td class=\"card_bg\"> \n";
        result = result + "<input type=\"text\" id=\"cardnum\" name=\"cardnum\" maxlength=\"16\" class=\"digitCard\" autocomplete=\"off\"/>\n";
        result = result + "</td>\n";
        result = result + "</tr>\n";
        result = result + "<tr>\n";
        result = result + "<td class=\"card_bg\"><font class=\"card_detail_color\">Security Code (CVV2) <a href=\"/images/card_cvv.jpg\" class=\"imgFloat\"><img src=\"/images/ico_question.jpg\" /></a>:<span class=\"mandatory_mark\"> *</span></font></td>\n";
        result = result + "<td class=\"card_bg\"><input name=\"card_cvv\" id=\"card_cvv\" maxlength=\"3\" style=\"width: 35px;\" class=\"digitCard\" type=\"text\" autocomplete=\"off\"> Last 3 digits on the back of the card</td>\n";
        result = result + "</tr>\n";
        result = result + "<tr>\n";
        result = result + "<td class=\"card_bg\"><font class=\"card_detail_color\">Expiry Date (mm/yyyy):<span class=\"mandatory_mark\"> *</span></font></td>\n";

        result = result + "<td class=\"card_bg\">\n";
        result = result + "<select name=\"card_month\" id=\"card_month\">\n";
        result = result + "<option value=\"1\">1</option>\n";
        result = result + "<option value=\"2\">2</option>\n";
        result = result + "<option value=\"3\">3</option>\n";
        result = result + "<option value=\"4\">4</option>\n";
        result = result + "<option value=\"5\">5</option>\n";
        result = result + "<option value=\"6\" selected=\"selected\">6</option>\n";
        result = result + "<option value=\"7\">7</option>\n";

        result = result + "<option value=\"8\">8</option>\n";
        result = result + "<option value=\"9\">9</option>\n";
        result = result + "<option value=\"10\">10</option>\n";
        result = result + "<option value=\"11\">11</option>\n";
        result = result + "<option value=\"12\">12</option>\n";
        result = result + "</select>\n";
        result = result + "<select name=\"card_year\" id=\"card_year\">\n";
        result = result + "<option value=\"2011\" selected=\"selected\">2011</option>\n";
        result = result + "<option value=\"2012\">2012</option>\n";
        result = result + "<option value=\"2013\">2013</option>\n";
        result = result + "<option value=\"2014\">2014</option>\n";
        result = result + "<option value=\"2015\">2015</option>\n";
        result = result + "<option value=\"2016\">2016</option>\n";
        result = result + "<option value=\"2017\">2017</option>\n";
        result = result + "<option value=\"2018\">2018</option>\n";
        result = result + "<option value=\"2019\">2019</option>\n";
        result = result + "<option value=\"2020\">2020</option>\n";
        result = result + "<option value=\"2021\">2021</option>\n";
        result = result + "</select>\n";
        result = result + "</td>\n";
        result = result + "</tr>\n";
        result = result + "<tr>\n";
        result = result + "<td class=\"card_bg\"><font class=\"card_detail_color\">Card Holder Name:</font><span class=\"mandatory_mark\"> *</span></td>\n";
        result = result + "<td class=\"card_bg\"><input name=\"card_name\" id=\"card_name\" maxlength=\"40\" type=\"text\"></td>\n";
        result = result + "</tr>\n";
        result = result + "<tr>\n";
        result = result + "<td class=\"card_bg\"><font class=\"card_detail_color\">Bank Name:<span class=\"mandatory_mark\"> *</span></font></td>\n";
        result = result + "<td class=\"card_bg\"><input name=\"bank_name\" id=\"bank_name\" maxlength=\"40\" type=\"text\"></td>\n";
        result = result + "</tr>\n";
        result = result + "</table>\n";
        result = result + "<input type=\"hidden\" name=\"bank_id\" value=\"6\"/>";
        result = result + "<br/>\n";

        result = result + "<table id=\"cardForm_note\">";
        result = result + "<tr><td>You can secure your booking for payment only <strong><font color=\"#539131\">$1</font></strong> holding guarantee of booking. We do not charge whole amount on your credit card when you book. You will be asked to pay the remaining total amount before check in date.";
        result = result + "<br/>If the booking is canceled, your holding guarantee will not be refunded.</td></tr>";
        result = result + "</table>";
        result = result + "<br/>\n";
        return result;
    }

    private bool IsProductNotReceiveFullyBook(int ProductID)
    {
        string[] arrProduct = { "3466", "3470", "3473", "3475", "3527", "3500", "3476", "3464", "3512", "3595", "3485", "3486", "3597", "3461", "3482", "3570", "3464", "3514", "3605", "3535", "3545", "3590", "3455", "3517", "3536", "3515", "3613", "3539", "3619" };
        bool result = false;
        foreach (string item in arrProduct)
        {
            if (ProductID == int.Parse(item))
            {
                result = true;
                break;
            }
        }
        return result;
    }

    private bool IsDepositFull(int ProductID)
    {
        string[] arrProduct = { "3456", "3457", "3458", "3459" };
        bool result=false;
        foreach(string item in arrProduct)
        {
            if(ProductID==int.Parse(item))
            {
                result = true;
                break;
            }
        }
        return result;
    }
    
}