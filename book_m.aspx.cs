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
using System.Text;
using Hotels2thailand.ProductOption;

public partial class book_m : System.Web.UI.Page
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
        string pathName = string.Empty;
        langID = byte.Parse(Request.Form["ln"]);

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
            ManageID = (byte)reader["manage_id"];
            paymentMethod = (byte)reader["booking_type_id"];
        }

        string layout = "";
        string Keyword = "";

        decimal totalPriceDeposit = 0;

        decimal totalRoom = 0;

        decimal totalPackage = 0;

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
        string Phone = string.Empty;
        string imagePath = string.Empty;
        decimal totalAbf = 0;
        using (SqlConnection cn = new SqlConnection(connString))
        {

            string sqlCommand = "select p.product_code,pc.title,";
            sqlCommand = sqlCommand + " (select top 1 spc.title from tbl_product_content spc where spc.product_id=p.product_id and spc.lang_id=" + langID + ") as second_lang,";
            sqlCommand = sqlCommand + " (select top 1 spc.address from tbl_product_content spc where spc.product_id=p.product_id and spc.lang_id=" + langID + ") as address_second_lang,";
            sqlCommand = sqlCommand + " pc.address,p.cat_id,p.product_phone,";
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
                Phone = reader["product_phone"].ToString();
            }
            else
            {
                ProductTitle = reader["second_lang"].ToString();
                Address = reader["address_second_lang"].ToString();
                Phone = reader["product_phone"].ToString();
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
            if (IsMember)
            {
                priceProductExtra.memberAuthen = true;
            }
            PriceListBaseExtra = priceProductExtra.GetPriceBase();
            OptionTitleListExtra = PriceListBaseExtra;

        }

        priceSupplement = new PriceSupplement();
        priceSupplement.LoadPriceSupplementByProductID(ProductID);
        //End General Process

        bool hasRoomSelect = false;
        int promotionTmp = 0;
        string formDisplay = string.Empty;
        decimal priceTotal = 0;
        decimal priceSummary = 0;
        decimal priceOption = 0;
        decimal priceReal = 0;
        int roomNight = dateEnd.Subtract(dateStart).Days;
        decimal priceAverage = 0;
        decimal priceABF = 0;


        StringBuilder cHead = new StringBuilder();

        cHead.Append("<!DOCTYPE html>");
        cHead.Append("<html lang='en'>");
        cHead.Append("<head>");
        cHead.Append("<meta charset='utf-8' />");
        cHead.Append("<!------BOOKING------->");
        cHead.Append("<link rel='stylesheet' type='text/css' href='/css/redmond/jquery-ui-1.8.11.custom.css'>");
        cHead.Append("<script language='javascript' src='/js/jquery-1.7.1.min.js' type='text/javascript'></script>");
        cHead.Append("<script type='text/javascript' src='/js/jquery-ui-1.8.11.custom.min.js'></script> ");
        cHead.Append("<script language='javascript' src='/js/ht2thUtility.js' type='text/javascript'></script>");
        cHead.Append("<script language='javascript' src='/js/jquery.validate-2.js' type='text/javascript'></script>");
        cHead.Append("<script type='text/javascript' src='/js/booking.check.js?vs=1'></script>");
        cHead.Append("<script type='text/javascript' src='/js/jquery.reveal.js'></script>");
        cHead.Append("<script language='javascript' type='text/javascript' src='/scripts/darkman_utility.js'></script>");
        cHead.Append("<link href='http://engine.booking2hotels.com/css/mobile-bht.css' rel='stylesheet' />");
        cHead.Append("<link href='/css/reveal_m.css' rel='stylesheet' />");
        cHead.Append("<!------BOOKING------->");
        cHead.Append("<script language='javascript'>");
        cHead.Append("$(document).ready(function(){");
        cHead.Append("$('#flight_ci').datepicker({");
        cHead.Append(" minDate: 1,");
        cHead.Append(" dateFormat: 'dd MM yy',");
        cHead.Append(" altField: '#Hdflight_ci',");
        cHead.Append(" altFormat: 'yy-mm-d'");
        cHead.Append("});");
        cHead.Append("$('#flight_co').datepicker({");
        cHead.Append(" minDate: 1,");
        cHead.Append(" dateFormat: 'dd MM yy',");
        cHead.Append(" altField: '#Hdflight_co',");
        cHead.Append(" altFormat: 'yy-mm-d'");
        cHead.Append(" });");
        cHead.Append("});");
        cHead.Append("function showdetail(obj) {");
        cHead.Append("$('.detail').slideToggle('slow', function () {");
        cHead.Append("if ($('.detail').css('display') == 'none') {");
        cHead.Append("$(obj).removeClass();");
        cHead.Append("$(obj).addClass('showdetail');");
        cHead.Append("}");
        cHead.Append(" else {");
        cHead.Append("$(obj).removeClass();");
        cHead.Append("$(obj).addClass('showdetail-up');");
        cHead.Append("if ($('.condition').css('display') != 'none') {");
        cHead.Append("$('.condition').slideToggle();");
        cHead.Append("$('#lbtshowCondition').removeClass();");
        cHead.Append("$('#lbtshowCondition').addClass('showCondition');");
        cHead.Append(" }");
        cHead.Append(" }");
        cHead.Append(" });");
        cHead.Append("}");
        cHead.Append("function showCondition(obj) {");
        cHead.Append("$('.condition').slideToggle('slow', function () {");
        cHead.Append("if ($('.condition').css('display') == 'none') {");
        cHead.Append("$(obj).removeClass();");
        cHead.Append("$(obj).addClass('showCondition');");
        cHead.Append("}");
        cHead.Append(" else {");
        cHead.Append("$(obj).removeClass();");
        cHead.Append("$(obj).addClass('showCondition-up');");
        cHead.Append("if ($('.detail').css('display') != 'none') {");
        cHead.Append("$('.detail').slideToggle();");
        cHead.Append("$('#lbtshowdetail').removeClass();");
        cHead.Append("$('#lbtshowdetail').addClass('showdetail');");
        cHead.Append("}");
        cHead.Append(" }");
        cHead.Append(" });");
        cHead.Append("}");
        cHead.Append("function showguest(obj) {");
        cHead.Append(" $('.guest').slideToggle('slow', function () {");
        cHead.Append("if ($('.guest').css('display') == 'none') {");
        cHead.Append(" $(obj).removeClass();");
        cHead.Append(" $(obj).addClass('icon-guest');");
        cHead.Append(" }");
        cHead.Append(" else {");
        cHead.Append(" $(obj).removeClass();");
        cHead.Append("  $(obj).addClass('icon-guest-up');");
        cHead.Append(" }");
        cHead.Append("});");
        cHead.Append("}");
        cHead.Append("</script>");
        cHead.Append("<title>" + ProductTitle + "</title>");
        cHead.Append("<meta name='viewport' content='width=device-width,initial-scale=1.0,maximum-scale=1.0' />");
        cHead.Append("</head>");
        cHead.Append("<body>");
        cHead.Append("<form id='formBooking' action='booking_process.aspx' method='post'>");
        cHead.Append("<div class='wrap'>");
        cHead.Append("<header style='background: #000; padding-top: 15px; padding-bottom: 15px; color: #fff; line-height: 13px;'>");
        cHead.Append("<a href='javascript:void(0)' onclick='history.back()'>");
        cHead.Append("<button class='backstep' onclick='return false;' >Back</button></a>");
        cHead.Append("</header>");


        StringBuilder cRoom = new StringBuilder();

        StringBuilder cCondition = new StringBuilder();

        StringBuilder cPolicy = new StringBuilder();
        cPolicy.Append("<span id=\"policyContractCancelDesc\" class=\"reveal-modal\">");

        #region // ROOM

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
                if (quantity[conditionCount] != 0)
                {

                    if (!checkExtranet)
                    {
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



                        cCondition.Append("<span class='conditiontitle' >" + GetProductTitle(optionID[conditionCount], PriceListBase) + "</span><br>" + policyContentDisplay + "<br><br>");

                        cRoom.Append("<div class='room-summary'>");
                        cRoom.Append("<span class='optiontitlesum'>" + GetProductTitle(optionID[conditionCount], PriceListBase) + "</span><br />");
                        cRoom.Append("<span class='promotionsum'>" + promotionTitle + "</span><span class='nightsum'>x " + quantity[conditionCount] + "</span><br />");
                        cRoom.Append("<hr class='line-dot'>");
                        cRoom.Append("<p class='listsummary'><span class='text-left'>Night  : " + roomNight + " </span>Avg. / Night  : THB " + priceAverage.ToString("#,###") + " </p>");
                        cRoom.Append("<p class='roomtotal'>THB " + priceReal.ToString("#,###") + " </p>");
                        cRoom.Append("</div>");

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

                        cPolicy.Append("<p>");
                        cPolicy.Append("<span class=\"headerPolicy\">" + GetProductExtraTitle(optionID[conditionCount], PriceListBaseExtra) + "</span>");

                        List<string> promotionDetail = GetPromotionExtra(promotionID[conditionCount], 1);
                        if (priceProductExtra.CheckPromotionAccept(promotionID[conditionCount]))
                        {
                            promotionTitle = promotionDetail[0];
                            policyDisplay = policyExtra.GetConditionPolicyList(policyListExtra, conditionID[conditionCount], promotionTitle);
                            if (bool.Parse(promotionDetail[1]))
                            {
                                //promotion has cancellation
                                policyContentDisplay = policyExtra.GetPolicyContent(policyListExtra, cancellationListExtra, conditionID[conditionCount], promotionID[conditionCount], promotionTitle, promotionDetail[2], bool.Parse(promotionDetail[1]));
                                cPolicy.Append(policyExtra.GetPolicyCancellation(policyListExtra, cancellationListExtra, conditionID[conditionCount], promotionID[conditionCount], promotionTitle, promotionDetail[2], bool.Parse(promotionDetail[1])));
                            }
                            else
                            {
                                policyContentDisplay = policyExtra.GetPolicyContent(policyListExtra, cancellationListExtra, conditionID[conditionCount], promotionID[conditionCount], promotionTitle, promotionDetail[2], bool.Parse(promotionDetail[1]));
                                cPolicy.Append(policyExtra.GetPolicyCancellation(policyListExtra, cancellationListExtra, conditionID[conditionCount], promotionID[conditionCount], promotionTitle, promotionDetail[2], bool.Parse(promotionDetail[1])));
                            }
                        }
                        else
                        {
                            policyDisplay = policyExtra.GetConditionPolicyList(policyListExtra, conditionID[conditionCount], "");
                            policyContentDisplay = policyExtra.GetPolicyContent(policyListExtra, cancellationListExtra, conditionID[conditionCount], 0, "", "", false);
                            cPolicy.Append(policyExtra.GetPolicyCancellation(policyListExtra, cancellationListExtra, conditionID[conditionCount], 0, "", "", false));
                        }

                        cPolicy.Append("</p>");
                        cPolicy.Append("<br>");

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

                        cCondition.Append("<span class='conditiontitle' >" + GetProductExtraTitle(optionID[conditionCount], PriceListBaseExtra) + "</span><br>" + policyContentDisplay + "<br><br>");

                        cRoom.Append("<div class='room-summary'>");
                        cRoom.Append("<span class='optiontitlesum'>" + GetProductExtraTitle(optionID[conditionCount], PriceListBaseExtra) + "</span><br />");
                        cRoom.Append("<span class='promotionsum'>" + promotionTitle + "</span><span class='nightsum'>x " + quantity[conditionCount] + "</span><br />");
                        cRoom.Append("<hr class='line-dot'>");
                        cRoom.Append("<p class='listsummary'><span class='text-left'>Night  : " + roomNight + " </span>Avg. / Night  : THB " + ((int)Utility.PriceExcludeVat(priceAverage)).ToString("#,###.##") + " </p>");
                        cRoom.Append("<p class='roomtotal'>THB " + ((int)Utility.PriceExcludeVat(priceAverage * roomNight * quantity[conditionCount])).ToString("#,###.##") + " </p>");
                        cRoom.Append("</div>");

                    }

                    totalPriceDeposit = totalPriceDeposit + productDeposit.GetPriceDeposit(priceReal, quantity[conditionCount], roomNight);
                    totalRoom = totalRoom + quantity[conditionCount];
                }
            }
        }

        #endregion

        //Calculate Package
        string[] packageOption = new string[2];
        decimal ratePackage = 0;
        FrontOptionPackage objPackage = new FrontOptionPackage(ProductID, dateStart, dateEnd);


        StringBuilder cPackage = new StringBuilder();

        #region // Package

        foreach (FrontOptionPackage itemPackage in objPackage.GetPackageList())
        {

            if (Request.Form["ddPackage_" + itemPackage.ConditionID + "_" + itemPackage.OptionID] != null)
            {
                packageOption = Request.Form["ddPackage_" + itemPackage.ConditionID + "_" + itemPackage.OptionID].Split('_');
                if (int.Parse(packageOption[6]) != 0)
                {
                    hasRoomSelect = true;
                    ratePackage = Utility.PriceExcludeVat(itemPackage.Price);


                    selectOption = selectOption + "<input type=\"hidden\" name=\"ddPackage_" + itemPackage.ConditionID + "_" + itemPackage.OptionID + "\" value=\"" + int.Parse(packageOption[6]) + "\">\n";

                    priceTotal = priceTotal + itemPackage.Price * int.Parse(packageOption[6]);
                    priceSummary = priceSummary + itemPackage.Price * int.Parse(packageOption[6]);

                    cPackage.Append("<div class='room-summary'>");
                    cPackage.Append("<span class='optiontitlesum'>" + itemPackage.OptionTitle + "</span><br />");
                    cPackage.Append("<span class='nightsum'>x " + packageOption[6] + "</span><br />");
                    cPackage.Append("<hr class='line-dot'>");
                    cPackage.Append(" <p class='roomtotal'>THB " + ((int)(ratePackage * int.Parse(packageOption[6]))).ToString("#,###.##") + " </p>");
                    cPackage.Append("</div>");

                    totalPriceDeposit = totalPriceDeposit + productDeposit.GetPriceDeposit(itemPackage.Price, int.Parse(packageOption[6]), roomNight);
                    totalPackage = totalPackage + Convert.ToInt32(packageOption[6]);
                }
            }
        }

        #endregion

        //

        //Calculate Meal
        string[] mealOption = new string[2];
        decimal rateMeal = 0;
        FrontOptionMeal objMeal = new FrontOptionMeal(ProductID, dateStart, dateEnd);

        StringBuilder cMeal = new StringBuilder();

        #region // Meal

        foreach (FrontOptionMeal itemMeal in objMeal.GetMealList())
        {

            if (Request.Form["ddMeal_" + itemMeal.ConditionID + "_" + itemMeal.OptionID] != null)
            {
                mealOption = Request.Form["ddMeal_" + itemMeal.ConditionID + "_" + itemMeal.OptionID].Split('_');
                if (int.Parse(mealOption[6]) != 0)
                {
                    rateMeal = Utility.PriceExcludeVat(itemMeal.Price);


                    selectOption = selectOption + "<input type=\"hidden\" name=\"ddMeal_" + itemMeal.ConditionID + "_" + itemMeal.OptionID + "\" value=\"" + int.Parse(mealOption[6]) + "\">\n";

                    priceTotal = priceTotal + itemMeal.Price * int.Parse(mealOption[6]);
                    priceSummary = priceSummary + itemMeal.Price * int.Parse(mealOption[6]);

                    cMeal.Append("<div class='room-summary'>");
                    cMeal.Append("<span class='optiontitlesum'>" + itemMeal.OptionTitle + "</span><br />");
                    cMeal.Append("<span class='nightsum'>x " + mealOption[6] + "</span><br />");
                    cMeal.Append("<hr class='line-dot'>");
                    cMeal.Append(" <p class='roomtotal'>THB " + ((int)(rateMeal * int.Parse(mealOption[6]))).ToString("#,###.##") + " </p>");
                    cMeal.Append("</div>");

                    totalPriceDeposit = totalPriceDeposit + productDeposit.GetPriceDeposit(itemMeal.Price, int.Parse(mealOption[6]), roomNight);

                }
            }
        }

        #endregion

        //Extra Bed

        bool checkTransfer = false;
        decimal rateExtraOption = 0;

        String[] extraOption = new String[2];
        int optionTmp = 0;

        StringBuilder cExtra = new StringBuilder();

        #region // Extra

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

                                cExtra.Append("<div class='room-summary'>");
                                cExtra.Append("<span class='optiontitlesum'>" + GetProductTitle(int.Parse(extraOption[1]), PriceListBase) + "</span><br />");
                                cExtra.Append("<span class='nightsum'>x " + extraOption[6] + "</span><br />");
                                cExtra.Append("<hr class='line-dot'>");
                                cExtra.Append("<p class='listsummary'><span class='text-left'>Night  : " + roomNight + " </span>Avg. / Night  : THB " + rateExtraOption.ToString("#,###") + " </p>");
                                cExtra.Append("<p class='roomtotal'>THB " + ((int)(rateExtraOption * int.Parse(extraOption[6]) * roomNight)).ToString("#,###") + " </p>");
                                cExtra.Append("</div>");

                            }
                            else
                            {
                                priceTotal = priceTotal + rateExtraOption * int.Parse(extraOption[6]);

                                cExtra.Append("<div class='room-summary'>");
                                cExtra.Append("<span class='optiontitlesum'>" + GetProductTitle(int.Parse(extraOption[1]), PriceListBase) + "</span><br />");
                                cExtra.Append("<span class='nightsum'>x " + extraOption[6] + "</span><br />");
                                cExtra.Append("<hr class='line-dot'>");
                                cExtra.Append("<p class='listsummary'>Avg. / Night  : THB " + rateExtraOption.ToString("#,###") + " </p>");
                                cExtra.Append("<p class='roomtotal'>THB " + ((int)(rateExtraOption * int.Parse(extraOption[6]))).ToString("#,###") + " </p>");
                                cExtra.Append("</div>");

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
                            if (extraItem.OptionCategoryID == 43 || extraItem.OptionCategoryID == 44)
                            {
                                checkTransfer = true;
                            }

                            selectOption = selectOption + "<input type=\"hidden\" name=\"ddPriceExtra_" + extraItem.ConditionID + "\" value=\"" + int.Parse(extraOption[6]) + "\">\n";


                            if (extraItem.OptionCategoryID != 43 && extraItem.OptionCategoryID != 44)
                            {
                                priceOption = priceProductExtra.CalculateAll(extraItem.ConditionID, extraItem.OptionID, 0).Price * Convert.ToInt32(extraOption[6]);

                                priceAverage = (int)((priceOption / Convert.ToInt32(extraOption[6])) / roomNight);
                                priceReal = (priceAverage * Convert.ToInt32(extraOption[6]) * roomNight);

                                priceTotal = priceTotal + priceOption;
                                priceSummary = priceSummary + priceOption;

                                cExtra.Append("<div class='room-summary'>");
                                cExtra.Append("<span class='optiontitlesum'>" + GetProductExtraTitle(int.Parse(extraOption[1]), PriceListBaseExtra) + "</span><br />");
                                cExtra.Append("<span class='nightsum'>x " + extraOption[6] + "</span><br />");
                                cExtra.Append("<hr class='line-dot'>");
                                cExtra.Append("<p class='listsummary'><span class='text-left'>Night  : " + roomNight + " </span>Avg. / Night  : THB " + ((int)Utility.PriceExcludeVat(priceAverage)).ToString("#,###.##") + "</p>");
                                cExtra.Append("<p class='roomtotal'>THB " + ((int)Utility.PriceExcludeVat(priceReal)).ToString("#,###.##") + " </p>");
                                cExtra.Append("</div>");

                                totalPriceDeposit = totalPriceDeposit + productDeposit.GetPriceDeposit(priceReal, byte.Parse(extraOption[6]), roomNight);
                            }
                            else
                            {
                                priceTotal = priceTotal + rateExtraOption * int.Parse(extraOption[6]);
                                priceSummary = priceSummary + rateExtraOption * int.Parse(extraOption[6]);

                                cExtra.Append("<div class='room-summary'>");
                                cExtra.Append("<span class='optiontitlesum'>" + GetProductExtraTitle(int.Parse(extraOption[1]), PriceListBaseExtra) + "</span><br />");
                                cExtra.Append("<span class='nightsum'>x " + extraOption[6] + "</span><br />");
                                cExtra.Append("<hr class='line-dot'>");
                                cExtra.Append("<p class='listsummary'> Avg. / Night  : THB " + ((int)Utility.ExcludeVat(rateExtraOption)).ToString("#,###.##") + "</p>");
                                cExtra.Append("<p class='roomtotal'>THB " + ((int)(Utility.ExcludeVat(rateExtraOption) * int.Parse(extraOption[6]))).ToString("#,###.##") + " </p>");
                                cExtra.Append("</div>");

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

                        cExtra.Append("<div class='room-summary'>");
                        cExtra.Append("<span class='optiontitlesum'>" + GetProductTitle(int.Parse(extraOption[1]), PriceListBase) + "</span><br />");
                        cExtra.Append("<span class='nightsum'>x " + extraOption[6] + "</span><br />");
                        cExtra.Append("<hr class='line-dot'>");
                        cExtra.Append("<p class='listsummary'><span class='text-left'>Night  : " + roomNight + " </span> Avg. / Night  : THB " + rateExtraOption.ToString("#,###.##") + "</p>");
                        cExtra.Append("<p class='roomtotal'>THB " + extraOption[6] + "</td><td>" + ((int)(rateExtraOption * int.Parse(extraOption[6]) * roomNight)).ToString("#,###") + " </p>");
                        cExtra.Append("</div>");

                        checkTransfer = true;
                    }
                }
            }
        }

        #endregion

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
        StringBuilder cGala = new StringBuilder();

        #region // Gala


        decimal priceGala = 0;

        foreach (GalaDinner item in galaList)
        {
            cGala.Append("<div class='room-summary'>");
            cGala.Append("<span class='optiontitlesum'>" + item.Title + "</span><br />");
            if (item.DefaultGala == 1)
            {
                // Select Only day
                if (item.RequireAdult)
                {
                    priceTotal = priceTotal + item.Rate * Convert.ToDecimal(adult);
                    priceSummary = priceSummary + item.Rate * Convert.ToDecimal(adult);

                    priceGala = priceGala + Utility.ExcludeVat(item.Rate * Convert.ToDecimal(adult));

                    if (langID == 1)
                    {
                        cGala.Append("<hr class='line-dot'>");
                        cGala.Append("<p class='listsummary'><span class='text-left'>Adult  : " + adult + " </span> THB " + ((int)(Utility.ExcludeVat(item.Rate) * Convert.ToDecimal(adult))).ToString("#,###.##") + "</p>");
                    }
                    else
                    {
                        cGala.Append("<hr class='line-dot'>");
                        cGala.Append("<p class='listsummary'><span class='text-left'>Adult  : " + adult + " </span> THB " + ((int)(Utility.ExcludeVat(item.Rate) * Convert.ToDecimal(adult))).ToString("#,###.##") + "</p>");
                    }
                }

                if (item.RequireChild)
                {
                    if (byte.Parse(child) > 0)
                    {
                        priceTotal = priceTotal + item.Rate * Convert.ToDecimal(child);
                        priceSummary = priceSummary + item.Rate * Convert.ToDecimal(child);

                        priceGala = priceGala + Utility.ExcludeVat(item.Rate * Convert.ToDecimal(child));

                        cGala.Append("<hr class='line-dot'>");
                        cGala.Append("<p class='listsummary'><span class='text-left'>Adult  : " + child + " </span> THB " + ((int)(Utility.ExcludeVat(item.Rate) * Convert.ToDecimal(child))).ToString("#,###.##") + "</p>");

                    }
                }

            }
            else
            {
                numNightGala = item.DateUseEnd.Subtract(dateStart).Days;
                // Select all day
                if (item.RequireAdult)
                {
                    for (int countNight = 0; countNight < numNightRoom; countNight++)
                    {
                        dateCheck = dateStart.AddDays(countNight);
                        if (dateCheck.CompareTo(item.DateUseStart) >= 0 && item.DateUseEnd.CompareTo(dateCheck) >= 0)
                        {
                            priceTotal = priceTotal + item.Rate * Convert.ToDecimal(adult);
                            priceSummary = priceSummary + item.Rate * Convert.ToDecimal(adult);

                            priceGala = priceGala + Utility.ExcludeVat(item.Rate * Convert.ToDecimal(adult));

                            cGala.Append("<hr class='line-dot'>");
                            cGala.Append("<p class='listsummary'><span class='text-left'>Child  : " + adult + " </span> THB " + ((int)(Utility.ExcludeVat(item.Rate) * Convert.ToDecimal(adult))).ToString("#,###.##") + "</p>");

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

                                cGala.Append("<hr class='line-dot'>");
                                cGala.Append("<p class='listsummary'><span class='text-left'>Child  : " + child + " </span> THB " + ((int)(Utility.ExcludeVat(item.Rate) * Convert.ToDecimal(child))).ToString("#,###.##") + "</p>");
                            }
                        }
                    }
                }

            }
            cGala.Append("<p class='roomtotal'>THB " + priceGala.ToString("#,###.##") + " </p>");
            cGala.Append("</div>");
        }

        #endregion


        StringBuilder cTitle = new StringBuilder();


        cTitle.Append(cHead.ToString());
        cTitle.Append("<div class='content'>");
        cTitle.Append("<span class='optionTitleBook'>Booking Summary</span><br />");
        cTitle.Append("<hr class='line'>");
        cTitle.Append("<strong>" + ProductTitle + "</strong>");
        cTitle.Append("<br />");
        cTitle.Append("<span class='addressinfo'>Address : " + Address + "</span>");
        cTitle.Append("<br />");
        string[] temp = Phone.Split('t');
        string[] temp2 = Phone.Split('T');
        if (temp.Length > 1 || temp.Length > 1)
        {
            cTitle.Append("<span class='telinfo'>" + Phone + "</span>");
        }
        else
        {
            cTitle.Append("<span class='telinfo'>Tel. " + Phone + "</span>");
        }
        cTitle.Append("<br />");



        //---------------------------------------------------Show Detail----------------------------------------------

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
                    depositWord = "You are requested to <span class='price_doposit'>pay the " + productDeposit.Deposit + "% deposit</span> in the amount of<span class='price_doposit'> " + totalPriceDeposit.ToString("#,###.##") + " Thai Baht</span><br>";
                    depositWordMini = "<span>Deposit(" + productDeposit.Deposit + "%) : THB " + totalPriceDeposit.ToString("#,###.##") + "</span>";

                    break;

                case 2:
                    depositWord = "You are requested to <span class='price_doposit'>pay the " + productDeposit.Deposit + " night deposit</span> in the amount of<span class='price_doposit'> " + totalPriceDeposit.ToString("#,###.##") + " Thai Baht</span><br>";
                    depositWordMini = "<span>Deposit(" + productDeposit.Deposit + " night) : THB " + totalPriceDeposit.ToString("#,###.##") + "</span>";
                    break;

                case 3:
                    depositWord = "You are requested to pay the deposit in the amount of<span class='price_doposit'> " + totalPriceDeposit.ToString("#,###.##") + " Thai Baht</span><br>";
                    depositWordMini = "<span>Deposit : THB " + totalPriceDeposit.ToString("#,###.##") + "</span>";
                    break;
            }
        }

        //For some hotel is not allotment and not receive booking on request
        if (!has_allotment && (IsProductNotReceiveFullyBook(int.Parse(Request.Form["hotel_id"]))))
        {
            cTitle.Append("<div style=\"padding:10px;\"><img src=\"images/ico_delete_circle.gif\" style=\"float:left; margin-right:15px;\"> Your selected date is currently fully booked. For more information, please kindly contact to us at <a href=\"mailto:reservation@booking2hotel.com\">reservation@booking2hotel.com</a><br/></div>");
            cTitle.Append("</div>");
            cTitle.Append("</form>");
            cTitle.Append("</body>");
            cTitle.Append("</html>");
            Response.Write(cTitle.ToString());
            Response.End();
        }
        else
        {
            cTitle.Append("<hr class='line'>");
            cTitle.Append("<span class='text-left'>Check In :</span> <span class='text-right'>" + dateStart.ToString("dddd, MMMM dd, yyyy") + "</span>");
            cTitle.Append("<br />");
            cTitle.Append("<hr class='line-dot'>");
            cTitle.Append("<span class='text-left'>Check Out :</span>  <span class='text-right'>" + dateEnd.ToString("dddd, MMMM dd, yyyy") + "</span>");
            cTitle.Append("<br />");
            cTitle.Append("<hr class='line-dot'>");
            cTitle.Append("<span class='text-left'>Guest :</span> <span class='text-right'>" + adult + " Adult(s) " + child + " Child(s)</span><br />");
            cTitle.Append("<hr class='line-dot'>");
            cTitle.Append("<span class='text-left'>For :</span><span class='text-right'>" + roomNight + " Night(s)");
            if (totalRoom > 0)
            {
                cTitle.Append(" , " + totalRoom + " Room(s)");
            }
            if (totalPackage > 0)
            {
                cTitle.Append(" , " + totalPackage + " Package(s)");
            }
            cTitle.Append("</span><br />");
            cTitle.Append("<hr class='line-dot'>");
            cTitle.Append("<span class='text-left'>Total</span><span class='text-right'>THB " + ((int)(Utility.PriceExcludeVat(priceTotal))).ToString("#,###.##") + " </span>");
            cTitle.Append("<br />");
            cTitle.Append("<hr class='line-dot'>");
            cTitle.Append("<span class='text-left'>Vat & Service Charge</span><span class='text-right'>THB " + ((priceVatInc - totalAbf) - ((int)Utility.PriceExcludeVat(priceTotal))).ToString("#,###.##") + " </span>");
            cTitle.Append("<br />");
            cTitle.Append("<hr class='line-dot'>");
            cTitle.Append("<span class='text-total'>Total Price : </span><span class='text-totalrate'>THB " + (priceVatInc).ToString("#,###.##") + "</span><br />");
            cTitle.Append("<hr class='doubleUnderline'>");
            cTitle.Append("<button id='lbtshowCondition' class='showCondition' onclick='showCondition(this); return false;' >Booking Condition</button>");
            cTitle.Append("<button id='lbtshowdetail' class='showdetail' onclick='showdetail(this); return false;'>Booking Detial</button>");
            cTitle.Append("<div class='clear'></div>");
            cTitle.Append("<div class='detail'>");
            cTitle.Append(cRoom.ToString());
            cTitle.Append(cPackage.ToString());
            cTitle.Append(cMeal.ToString());
            cTitle.Append(cExtra.ToString());
            cTitle.Append(cGala.ToString());
            cTitle.Append("</div>");
            cTitle.Append("<div class='condition'>");
            cTitle.Append(cCondition.ToString());
            cTitle.Append("</div>");

        }
        //


        //---------------------------------------------------Your Infomation----------------------------------------------

        Country country = new Country();
        string formInfo = string.Empty;
        StringBuilder cInfo = new StringBuilder();
        if (has_allotment)
        {
            cInfo.Append("<div id='Alotment_check'>");
            cInfo.Append("<span class='available-img'></span>");
            cInfo.Append("<span class='available-title'>Room Available</span>");
            cInfo.Append("<p>You’ll receive voucher confirmation immediately after payment is successful</p>");
            cInfo.Append("</div>");
        }
        cInfo.Append("<br class='clear'>");
        cInfo.Append("<div id='paymentbox'>");
        cInfo.Append("<span class='optionTitleBook'>Your Information</span><br />");
        cInfo.Append("<span class='fieldtext'>Fields marked with an asterisk (*) are required.</span>");
        cInfo.Append("<br />");
        cInfo.Append("<br />");
        cInfo.Append("<div>");
        cInfo.Append("<span class='titleform'>Prefix <span class=\"fontRed\">*</span> : </span>");
        cInfo.Append("<select name=\"prefix\" id=\"prefix\" class='mobile-ddl'><option value=\"1\">None</option><option value=\"2\" selected=\"selected\">Mr.</option><option value=\"4\">Miss.</option><option value=\"3\">Mrs.</option></select>");
        cInfo.Append("<br />");
        if (IsMember)
        {
            Customer objCustomer = new Customer(int.Parse(Request.Form["mmid"]));
            cInfo.Append("<span class='titleform'>Name <span class=\"fontRed\">*</span> : </span>");
            cInfo.Append("<input type='text' class='required mobile-textbox' placeholder='name' name=\"first_name\" id=\"first_name\" value=\"" + objCustomer.FullName + "\" /><br />");
            cInfo.Append("<span class='titleform'>Email <span class=\"fontRed\">*</span> :</span>");
            cInfo.Append("<input type='text' class='required email mobile-textbox' placeholder='email address' name=\"email\" id=\"email\" value=\"" + objCustomer.Email + "\" /><br />");
            cInfo.Append("<span class='titleform'>Email Repeat <span class=\"fontRed\">*</span> :</span>");
            cInfo.Append("<input type='text' class='required mobile-textbox' placeholder='Repeat email address'  name=\"re_email\" id=\"re_email\" value=\"" + objCustomer.Email + "\" /><br />");
            cInfo.Append("<span class='titleform'>Phone <span class=\"fontRed\">*</span> :</span>");
            cInfo.Append("<input type='text' class='required mobile-textbox' placeholder='Phone' name=\"phone\" id=\"phone\" /><br />");
            cInfo.Append("<span class='titleform'>Country <span class=\"fontRed\">*</span> :</span>" + DropdownUtility.CountryList("country", country.GetCountryAll()));
        }
        else
        {
            cInfo.Append("<span class='titleform'>Name <span class=\"fontRed\">*</span> : </span>");
            cInfo.Append("<input type='text' class='required mobile-textbox' placeholder='name' name=\"first_name\" id=\"first_name\" / ><br />");
            cInfo.Append("<span class='titleform'>Email <span class=\"fontRed\">*</span> :</span>");
            cInfo.Append("<input type='text' class='required email mobile-textbox' placeholder='email address' name=\"email\" id=\"email\" /><br />");
            cInfo.Append("<span class='titleform'>Email Repeat <span class=\"fontRed\">*</span> :</span>");
            cInfo.Append("<input type='text' class='required mobile-textbox' placeholder='Repeat email address'  name=\"re_email\" id=\"re_email\" /><br />");
            cInfo.Append("<span class='titleform'>Phone <span class=\"fontRed\">*</span> :</span>");
            cInfo.Append("<input type='text' class='required mobile-textbox' placeholder='Phone' name=\"phone\" id=\"phone\" /><br />");
            cInfo.Append("<span class='titleform'>Country <span class=\"fontRed\">*</span> :</span>" + DropdownUtility.CountryList("country", country.GetCountryAll()));
            cInfo.Append("<br />");
            cInfo.Append("<br />");
            cInfo.Append("</div>");
            cInfo.Append("</div>");
        }

        if (checkTransfer)
        {
            cInfo.Append("<hr class='line'>");
            cInfo.Append("<div id='transferbox'>");
            cInfo.Append("<span class='optionTitleBook'>Flight detail:</span><br />");
            cInfo.Append("<br />");
            cInfo.Append("<div>");
            cInfo.Append("<span class='titleform'>Arrival Flight: </span>");
            cInfo.Append("<input type='text' class='mobile-textbox' placeholder='Arrival flight' name=\"flight_name_in\" id=\"flight_name_in\" /><br />");
            cInfo.Append("<span class='titleform'>Arrival Date: </span>");
            cInfo.Append("<input type='text' name='flight_ci' id='flight_ci' readonly class='mobile-textbox-Date' />");
            cInfo.Append("<input type=\"hidden\"  name=\"Hdflight_ci\" id=\"Hdflight_ci\" style=\"width:120px;\" />");
            cInfo.Append("<br />");
            cInfo.Append("<span class='titleform'>Hour </span>");
            cInfo.Append(DropdownUtility.Number("time_hour_arv", 0, 23, 0));
            cInfo.Append("<span class='titleform'>Min </span>");
            cInfo.Append(DropdownUtility.Number("time_min_arv", 0, 59, 0));
            cInfo.Append("<br />");
            cInfo.Append("<span class='titleform'>Departure Flight:</span>");
            cInfo.Append("<input type='text' class='mobile-textbox' placeholder='Departure flight' name=\"flight_name_out\" id=\"flight_name_out\" /><br />");
            cInfo.Append("<span class='titleform'>Departure Date:</span>");
            cInfo.Append("<input type='text' name='flight_co' id='flight_co' readonly class='mobile-textbox-Date' />");
            cInfo.Append("<input type=\"hidden\"  name=\"Hdflight_co\" id=\"Hdflight_co\" style=\"width:120px;\" />");
            cInfo.Append("<br />");
            cInfo.Append("<span class='titleform'>Hour </span>");
            cInfo.Append(DropdownUtility.Number("time_hour_dep", 0, 23, 0));
            cInfo.Append("<span class='titleform'>Min</span>");
            cInfo.Append(DropdownUtility.Number("time_min_dep", 0, 59, 0));
            cInfo.Append("<br />");
            cInfo.Append("<span class='titleform'>Remark :</span>");
            cInfo.Append("<textarea class='mobile-textbox' name='transfer_detail' cols='65' rows='3'>Pickup from airport and transfer to " + ProductTitle + "</textarea>");
            cInfo.Append("</div>");
            cInfo.Append("</div>");
        }


        cInfo.Append("<div class='clear'></div>");

        #region // Guest & Information Preference

        switch (categoryID)
        {
            case 29:
                cInfo.Append("<hr class='line'>");
                cInfo.Append("<span class='text_guest'>");
                cInfo.Append("<h1>Guest & Information Preference</h1>");
                cInfo.Append("<h2>(Subject to availability, can not guarantee)</h2>");
                cInfo.Append("</span>");
                cInfo.Append("<a id='g'></a><span class='link_show_daily_rate'><a href='#g' class='showguest'>");
                cInfo.Append("<button class='icon-guest' onclick='showguest(this); return false;' title='Guest & Information Preference'></button>");
                cInfo.Append("</a></span>");
                cInfo.Append("<br class='clearAll'>");
                cInfo.Append("<br />");
                cInfo.Append("<br />");
                cInfo.Append("<hr class='line'>");
                cInfo.Append("<div class='guest'>");

                foreach (FrontOptionPackage itemPackage in objPackage.GetPackageList())
                {

                    if (Request.Form["ddPackage_" + itemPackage.ConditionID + "_" + itemPackage.OptionID] != null)
                    {
                        packageOption = Request.Form["ddPackage_" + itemPackage.ConditionID + "_" + itemPackage.OptionID].Split('_');
                        if (int.Parse(packageOption[6]) != 0)
                        {
                            if (int.Parse(packageOption[6]) == 1)
                            {
                                cInfo.Append("<p class='optionTitle'>" + itemPackage.OptionTitle + "</strong></p>");
                                cInfo.Append("<div>");
                                cInfo.Append("<label><strong>Bed type:</strong></label>");
                                cInfo.Append("<br />");
                                cInfo.Append("<span class='guest-requirement-col-1'>");
                                cInfo.Append("<input type='radio' name=\"bed_type_" + itemPackage.ConditionID + "_1\" value='3' checked='checked' />No preference</span>");
                                cInfo.Append("<span class='guest-requirement-col-2'>");
                                cInfo.Append("<input type='radio' name=\"bed_type_" + itemPackage.ConditionID + "_1\" value='1' />King size</span>");
                                cInfo.Append("<span class='guest-requirement-col-3'>");
                                cInfo.Append("<input type='radio' name=\"bed_type_" + itemPackage.ConditionID + "_1\" value='2' />Twin beds</span><br class='clearAll'>");
                                cInfo.Append("</div>");
                                cInfo.Append("<hr class='line-dot'>");
                                if (ProductID != 3590 && ProductID != 3687 && ProductID != 3618)
                                {
                                    cInfo.Append("<div>");
                                    cInfo.Append("<label><strong>Smoke:</strong></label><br />");
                                    cInfo.Append("<span class='guest-requirement-col-1'>");
                                    cInfo.Append("<input type='radio' name=\"smoke_type_" + itemPackage.ConditionID + "_1\" value='3' checked='checked' />No preference</span>");
                                    cInfo.Append("<span class='guest-requirement-col-2'>");
                                    cInfo.Append("<input type='radio' name=\"smoke_type_" + itemPackage.ConditionID + "_1\" value='1' />Non-Smoking</span>");
                                    cInfo.Append("<span class='guest-requirement-col-3'>");
                                    cInfo.Append("<input type='radio' name=\"smoke_type_" + itemPackage.ConditionID + "_1\" value='2' />Smoking</span><br class='clearAll'>");
                                    cInfo.Append("</div>");
                                }
                                else
                                {
                                    cInfo.Append("<div>");
                                    cInfo.Append("<label><strong>Smoke:</strong></label><br />");
                                    cInfo.Append("<span class='guest-requirement-col-1'></span>");
                                    cInfo.Append("<span class='guest-requirement-col-2'>");
                                    cInfo.Append("<input type='radio' name=\"smoke_type_" + itemPackage.ConditionID + "_1\" value='1' checked=\"checked\" />Non-Smoking</span>");
                                    cInfo.Append("<span class='guest-requirement-col-3'></span><br class='clearAll'>");
                                    cInfo.Append("</div>");
                                }
                                cInfo.Append("<hr class='line-dot'>");
                                if (ProductID != 3618)
                                {
                                    cInfo.Append("<div>");
                                    cInfo.Append("<label><strong>floor:</strong></label><br />");
                                    cInfo.Append("<span class='guest-requirement-col-1'>");
                                    cInfo.Append("<input type='radio' name=\"floor_type_" + itemPackage.ConditionID + "_1\" value='3' checked='checked' />No preference</span>");
                                    cInfo.Append("<span class='guest-requirement-col-2'>");
                                    cInfo.Append("<input type='radio' name=\"floor_type_" + itemPackage.ConditionID + "_1\" value='1' />High floor</span>");
                                    cInfo.Append("<span class='guest-requirement-col-3'>");
                                    cInfo.Append("<input type='radio' name=\"floor_type_" + itemPackage.ConditionID + "_1\" value='2' />Low floor</span>");
                                    cInfo.Append("<br />");
                                }
                                else
                                {
                                    cInfo.Append("<div>");
                                    cInfo.Append("<label><strong>floor:</strong></label><br />");
                                    cInfo.Append("<span class='guest-requirement-col-1'>");
                                    cInfo.Append("<input type='radio' name=\"floor_type_" + itemPackage.ConditionID + "_1\" value='3' checked='checked' />No preference</span>");
                                    cInfo.Append("<span class='guest-requirement-col-2'>");
                                    cInfo.Append("<span class='guest-requirement-col-3'></span><br class='clearAll'>");
                                    cInfo.Append("<br />");
                                }
                                cInfo.Append("<br />");
                                cInfo.Append("<textarea class='mobile-textbox' name=\"sp_require_" + itemPackage.ConditionID + "_1\" cols='65' rows='3'></textarea>");
                                cInfo.Append("<br class='clearAll'>");
                                cInfo.Append("<hr class='line'>");
                                cInfo.Append("</div>");

                            }
                            else
                            {
                                for (int roomCount = 1; roomCount <= int.Parse(packageOption[6]); roomCount++)
                                {

                                    cInfo.Append("<p class='optionTitle'>Room#" + roomCount + ": " + itemPackage.OptionTitle + "</strong></p>");
                                    cInfo.Append("<div>");
                                    cInfo.Append("<label><strong>Bed type:</strong></label>");
                                    cInfo.Append("<br />");
                                    cInfo.Append("<span class='guest-requirement-col-1'>");
                                    cInfo.Append("<input type='radio' name=\"bed_type_" + itemPackage.ConditionID + "_" + roomCount + "\" value='3' checked='checked' />No preference</span>");
                                    cInfo.Append("<span class='guest-requirement-col-2'>");
                                    cInfo.Append("<input type='radio' name=\"bed_type_" + itemPackage.ConditionID + "_" + roomCount + "\" value='1' />King size</span>");
                                    cInfo.Append("<span class='guest-requirement-col-3'>");
                                    cInfo.Append("<input type='radio' name=\"bed_type_" + itemPackage.ConditionID + "_" + roomCount + "\" value='2' />Twin beds</span><br class='clearAll'>");
                                    cInfo.Append("</div>");
                                    cInfo.Append("<hr class='line-dot'>");
                                    if (ProductID != 3590 && ProductID != 3687 && ProductID != 3618)
                                    {
                                        cInfo.Append("<div>");
                                        cInfo.Append("<label><strong>Smoke:</strong></label><br />");
                                        cInfo.Append("<span class='guest-requirement-col-1'>");
                                        cInfo.Append("<input type='radio' name=\"smoke_type_" + itemPackage.ConditionID + "_" + roomCount + "\" value='3' checked='checked' />No preference</span>");
                                        cInfo.Append("<span class='guest-requirement-col-2'>");
                                        cInfo.Append("<input type='radio' name=\"smoke_type_" + itemPackage.ConditionID + "_" + roomCount + "\" value='1' />Non-Smoking</span>");
                                        cInfo.Append("<span class='guest-requirement-col-3'>");
                                        cInfo.Append("<input type='radio' name=\"smoke_type_" + itemPackage.ConditionID + "_" + roomCount + "\" value='2' />Smoking</span><br class='clearAll'>");
                                        cInfo.Append("</div>");
                                    }
                                    else
                                    {
                                        cInfo.Append("<div>");
                                        cInfo.Append("<label><strong>Smoke:</strong></label><br />");
                                        cInfo.Append("<span class='guest-requirement-col-1'></span>");
                                        cInfo.Append("<span class='guest-requirement-col-2'>");
                                        cInfo.Append("<input type='radio' name=\"smoke_type_" + itemPackage.ConditionID + "_" + roomCount + "\" value='1' checked=\"checked\" />Non-Smoking</span>");
                                        cInfo.Append("<span class='guest-requirement-col-3'></span><br class='clearAll'>");
                                        cInfo.Append("</div>");
                                    }
                                    cInfo.Append("<hr class='line-dot'>");
                                    if (ProductID != 3618)
                                    {
                                        cInfo.Append("<div>");
                                        cInfo.Append("<label><strong>floor:</strong></label><br />");
                                        cInfo.Append("<span class='guest-requirement-col-1'>");
                                        cInfo.Append("<input type='radio' name=\"floor_type_" + itemPackage.ConditionID + "_" + roomCount + "\" value='3' checked='checked' />No preference</span>");
                                        cInfo.Append("<span class='guest-requirement-col-2'>");
                                        cInfo.Append("<input type='radio' name=\"floor_type_" + itemPackage.ConditionID + "_" + roomCount + "\" value='1' />High floor</span>");
                                        cInfo.Append("<span class='guest-requirement-col-3'>");
                                        cInfo.Append("<input type='radio' name=\"floor_type_" + itemPackage.ConditionID + "_" + roomCount + "\" value='2' />Low floor</span>");
                                        cInfo.Append("<br />");
                                    }
                                    else
                                    {
                                        cInfo.Append("<div>");
                                        cInfo.Append("<label><strong>floor:</strong></label><br />");
                                        cInfo.Append("<span class='guest-requirement-col-1'>");
                                        cInfo.Append("<input type='radio' name=\"floor_type_" + itemPackage.ConditionID + "_" + roomCount + "\" value='3' checked='checked' />No preference</span>");
                                        cInfo.Append("<span class='guest-requirement-col-2'>");
                                        cInfo.Append("<span class='guest-requirement-col-3'></span><br class='clearAll'>");
                                        cInfo.Append("<br />");
                                    }
                                    cInfo.Append("<br />");
                                    cInfo.Append("<textarea class='mobile-textbox' name=\"sp_require_" + itemPackage.ConditionID + "_" + roomCount + "\" cols='65' rows='3'></textarea>");
                                    cInfo.Append("<br class='clearAll'>");
                                    cInfo.Append("<hr class='line'>");
                                    cInfo.Append("</div>");

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
                                        cInfo.Append("<p class='optionTitle'>" + GetProductTitle(optionID[productCount], OptionTitleList) + "</strong></p>");
                                    }
                                    else
                                    {
                                        cInfo.Append("<p class='optionTitle'>" + GetProductExtraTitle(optionID[productCount], OptionTitleListExtra) + "</strong></p>");
                                    }

                                    cInfo.Append("<div>");
                                    cInfo.Append("<label><strong>Bed type:</strong></label>");
                                    cInfo.Append("<br />");
                                    cInfo.Append("<span class='guest-requirement-col-1'>");
                                    cInfo.Append("<input type='radio' name=\"bed_type_" + conditionID[productCount] + "_1\" value='3' checked='checked' />No preference</span>");
                                    cInfo.Append("<span class='guest-requirement-col-2'>");
                                    cInfo.Append("<input type='radio' name=\"bed_type_" + conditionID[productCount] + "_1\" value='1' />King size</span>");
                                    cInfo.Append("<span class='guest-requirement-col-3'>");
                                    cInfo.Append("<input type='radio' name=\"bed_type_" + conditionID[productCount] + "_1\" value='2' />Twin beds</span><br class='clearAll'>");
                                    cInfo.Append("</div>");
                                    cInfo.Append("<hr class='line-dot'>");
                                    if (ProductID != 3590 && ProductID != 3687 && ProductID != 3618)
                                    {
                                        cInfo.Append("<div>");
                                        cInfo.Append("<label><strong>Smoke:</strong></label><br />");
                                        cInfo.Append("<span class='guest-requirement-col-1'>");
                                        cInfo.Append("<input type='radio' name=\"smoke_type_" + conditionID[productCount] + "_1\" value='3' checked='checked' />No preference</span>");
                                        cInfo.Append("<span class='guest-requirement-col-2'>");
                                        cInfo.Append("<input type='radio' name=\"smoke_type_" + conditionID[productCount] + "_1\" value='1' />Non-Smoking</span>");
                                        cInfo.Append("<span class='guest-requirement-col-3'>");
                                        cInfo.Append("<input type='radio' name=\"smoke_type_" + conditionID[productCount] + "_1\" value='2' />Smoking</span><br class='clearAll'>");
                                        cInfo.Append("</div>");
                                    }
                                    else
                                    {
                                        cInfo.Append("<div>");
                                        cInfo.Append("<label><strong>Smoke:</strong></label><br />");
                                        cInfo.Append("<span class='guest-requirement-col-1'></span>");
                                        cInfo.Append("<span class='guest-requirement-col-2'>");
                                        cInfo.Append("<input type='radio' name=\"smoke_type_" + conditionID[productCount] + "_1\" value='1' checked=\"checked\" />Non-Smoking</span>");
                                        cInfo.Append("<span class='guest-requirement-col-3'></span><br class='clearAll'>");
                                        cInfo.Append("</div>");
                                    }
                                    cInfo.Append("<hr class='line-dot'>");
                                    if (ProductID != 3618)
                                    {
                                        cInfo.Append("<div>");
                                        cInfo.Append("<label><strong>floor:</strong></label><br />");
                                        cInfo.Append("<span class='guest-requirement-col-1'>");
                                        cInfo.Append("<input type='radio' name=\"floor_type_" + conditionID[productCount] + "_1\" value='3' checked='checked' />No preference</span>");
                                        cInfo.Append("<span class='guest-requirement-col-2'>");
                                        cInfo.Append("<input type='radio' name=\"floor_type_" + conditionID[productCount] + "_1\" value='1' />High floor</span>");
                                        cInfo.Append("<span class='guest-requirement-col-3'>");
                                        cInfo.Append("<input type='radio' name=\"floor_type_" + conditionID[productCount] + "_1\" value='2' />Low floor</span>");
                                        cInfo.Append("<br />");
                                    }
                                    else
                                    {
                                        cInfo.Append("<div>");
                                        cInfo.Append("<label><strong>floor:</strong></label><br />");
                                        cInfo.Append("<span class='guest-requirement-col-1'>");
                                        cInfo.Append("<input type='radio' name=\"floor_type_" + conditionID[productCount] + "_1\" value='3' checked='checked' />No preference</span>");
                                        cInfo.Append("<span class='guest-requirement-col-2'>");
                                        cInfo.Append("<span class='guest-requirement-col-3'></span><br class='clearAll'>");
                                        cInfo.Append("<br />");
                                    }
                                    cInfo.Append("<br />");
                                    cInfo.Append("<textarea class='mobile-textbox' name=\"sp_require_" + conditionID[productCount] + "_1\" cols='65' rows='3'></textarea>");
                                    cInfo.Append("<br class='clearAll'>");
                                    cInfo.Append("<hr class='line'>");
                                    cInfo.Append("</div>");

                                }
                                else
                                {

                                    if (!checkExtranet)
                                    {
                                        cInfo.Append("<p class='optionTitle'>" + GetProductTitle(optionID[productCount], OptionTitleList) + "</strong></p>");
                                    }
                                    else
                                    {
                                        cInfo.Append("<p class='optionTitle'>" + GetProductExtraTitle(optionID[productCount], OptionTitleListExtra) + "</strong></p>");
                                    }

                                    cInfo.Append("<div>");
                                    cInfo.Append("<label><strong>ชนิดเตียง:</strong></label>");
                                    cInfo.Append("<br />");
                                    cInfo.Append("<span class='guest-requirement-col-1'>");
                                    cInfo.Append("<input type='radio' name=\"bed_type_" + conditionID[productCount] + "_1\" value='3' checked='checked' />ไม่ระบุ</span>");
                                    cInfo.Append("<span class='guest-requirement-col-2'>");
                                    cInfo.Append("<input type='radio' name=\"bed_type_" + conditionID[productCount] + "_1\" value='1' />เตียงใหญ่</span>");
                                    cInfo.Append("<span class='guest-requirement-col-3'>");
                                    cInfo.Append("<input type='radio' name=\"bed_type_" + conditionID[productCount] + "_1\" value='2' />เตียงคู่</span><br class='clearAll'>");
                                    cInfo.Append("</div>");
                                    cInfo.Append("<hr class='line-dot'>");
                                    if (ProductID != 3590 && ProductID != 3687 && ProductID != 3618)
                                    {
                                        cInfo.Append("<div>");
                                        cInfo.Append("<label><strong>สูบบุหรี่:</strong></label><br />");
                                        cInfo.Append("<span class='guest-requirement-col-1'>");
                                        cInfo.Append("<input type='radio' name=\"smoke_type_" + conditionID[productCount] + "_1\" value='3' checked='checked' />ไม่ระบุ</span>");
                                        cInfo.Append("<span class='guest-requirement-col-2'>");
                                        cInfo.Append("<input type='radio' name=\"smoke_type_" + conditionID[productCount] + "_1\" value='1' />ไม่สูบบุหรี่</span>");
                                        cInfo.Append("<span class='guest-requirement-col-3'>");
                                        cInfo.Append("<input type='radio' name=\"smoke_type_" + conditionID[productCount] + "_1\" value='2' />สูบบุหรี่</span><br class='clearAll'>");
                                        cInfo.Append("</div>");
                                    }
                                    else
                                    {
                                        cInfo.Append("<div>");
                                        cInfo.Append("<label><strong>สูบบุหรี่:</strong></label><br />");
                                        cInfo.Append("<span class='guest-requirement-col-1'></span>");
                                        cInfo.Append("<span class='guest-requirement-col-2'>");
                                        cInfo.Append("<input type='radio' name=\"smoke_type_" + conditionID[productCount] + "_1\" value='1' checked=\"checked\" />ไม่สูบบุหรี่</span>");
                                        cInfo.Append("<span class='guest-requirement-col-3'></span><br class='clearAll'>");
                                        cInfo.Append("</div>");
                                    }
                                    cInfo.Append("<hr class='line-dot'>");
                                    if (ProductID != 3618)
                                    {
                                        cInfo.Append("<div>");
                                        cInfo.Append("<label><strong>ระดับชั้น:</strong></label><br />");
                                        cInfo.Append("<span class='guest-requirement-col-1'>");
                                        cInfo.Append("<input type='radio' name=\"floor_type_" + conditionID[productCount] + "_1\" value='3' checked='checked' />ไม่ระบุ</span>");
                                        cInfo.Append("<span class='guest-requirement-col-2'>");
                                        cInfo.Append("<input type='radio' name=\"floor_type_" + conditionID[productCount] + "_1\" value='1' />ชั้นสูงๆ</span>");
                                        cInfo.Append("<span class='guest-requirement-col-3'>");
                                        cInfo.Append("<input type='radio' name=\"floor_type_" + conditionID[productCount] + "_1\" value='2' />ชั้นล่างๆ</span>");
                                        cInfo.Append("<br />");
                                    }
                                    else
                                    {
                                        cInfo.Append("<div>");
                                        cInfo.Append("<label><strong>ระดับชั้น:</strong></label><br />");
                                        cInfo.Append("<span class='guest-requirement-col-1'>");
                                        cInfo.Append("<input type='radio' name=\"floor_type_" + conditionID[productCount] + "_1\" value='3' checked='checked' />ไม่ระบุ</span>");
                                        cInfo.Append("<span class='guest-requirement-col-2'>");
                                        cInfo.Append("<span class='guest-requirement-col-3'></span><br class='clearAll'>");
                                        cInfo.Append("<br />");
                                    }
                                    cInfo.Append("<br />");
                                    cInfo.Append("<textarea class='mobile-textbox' name=\"sp_require_" + conditionID[productCount] + "_1\" cols='65' rows='3'></textarea>");
                                    cInfo.Append("<br class='clearAll'>");
                                    cInfo.Append("<hr class='line'>");
                                    cInfo.Append("</div>");

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
                                            cInfo.Append("<p class='optionTitle'>Room#" + roomCount + ": " + GetProductTitle(optionID[productCount], OptionTitleList) + "</strong></p>");
                                        }
                                        else
                                        {
                                            cInfo.Append("<p class='optionTitle'>Room#" + roomCount + ": " + GetProductExtraTitle(optionID[productCount], OptionTitleListExtra) + "</strong></p>");
                                        }

                                        cInfo.Append("<div>");
                                        cInfo.Append("<label><strong>Bed type:</strong></label>");
                                        cInfo.Append("<br />");
                                        cInfo.Append("<span class='guest-requirement-col-1'>");
                                        cInfo.Append("<input type='radio' name=\"bed_type_" + conditionID[productCount] + "_" + roomCount + "\" value='3' checked='checked' />No preference</span>");
                                        cInfo.Append("<span class='guest-requirement-col-2'>");
                                        cInfo.Append("<input type='radio' name=\"bed_type_" + conditionID[productCount] + "_" + roomCount + "\" value='1' />King size</span>");
                                        cInfo.Append("<span class='guest-requirement-col-3'>");
                                        cInfo.Append("<input type='radio' name=\"bed_type_" + conditionID[productCount] + "_" + roomCount + "\" value='2' />Twin beds</span><br class='clearAll'>");
                                        cInfo.Append("</div>");
                                        cInfo.Append("<hr class='line-dot'>");
                                        if (ProductID != 3590 && ProductID != 3687 && ProductID != 3618)
                                        {
                                            cInfo.Append("<div>");
                                            cInfo.Append("<label><strong>Smoke:</strong></label><br />");
                                            cInfo.Append("<span class='guest-requirement-col-1'>");
                                            cInfo.Append("<input type='radio' name=\"smoke_type_" + conditionID[productCount] + "_" + roomCount + "\" value='3' checked='checked' />No preference</span>");
                                            cInfo.Append("<span class='guest-requirement-col-2'>");
                                            cInfo.Append("<input type='radio' name=\"smoke_type_" + conditionID[productCount] + "_" + roomCount + "\" value='1' />Non-Smoking</span>");
                                            cInfo.Append("<span class='guest-requirement-col-3'>");
                                            cInfo.Append("<input type='radio' name=\"smoke_type_" + conditionID[productCount] + "_" + roomCount + "\" value='2' />Smoking</span><br class='clearAll'>");
                                            cInfo.Append("</div>");
                                        }
                                        else
                                        {
                                            cInfo.Append("<div>");
                                            cInfo.Append("<label><strong>Smoke:</strong></label><br />");
                                            cInfo.Append("<span class='guest-requirement-col-1'></span>");
                                            cInfo.Append("<span class='guest-requirement-col-2'>");
                                            cInfo.Append("<input type='radio' name=\"smoke_type_" + conditionID[productCount] + "_" + roomCount + "\" value='1' checked=\"checked\" />Non-Smoking</span>");
                                            cInfo.Append("<span class='guest-requirement-col-3'></span><br class='clearAll'>");
                                            cInfo.Append("</div>");
                                        }
                                        cInfo.Append("<hr class='line-dot'>");
                                        if (ProductID != 3618)
                                        {
                                            cInfo.Append("<div>");
                                            cInfo.Append("<label><strong>floor:</strong></label><br />");
                                            cInfo.Append("<span class='guest-requirement-col-1'>");
                                            cInfo.Append("<input type='radio' name=\"floor_type_" + conditionID[productCount] + "_" + roomCount + "\" value='3' checked='checked' />No preference</span>");
                                            cInfo.Append("<span class='guest-requirement-col-2'>");
                                            cInfo.Append("<input type='radio' name=\"floor_type_" + conditionID[productCount] + "_" + roomCount + "\" value='1' />High floor</span>");
                                            cInfo.Append("<span class='guest-requirement-col-3'>");
                                            cInfo.Append("<input type='radio' name=\"floor_type_" + conditionID[productCount] + "_" + roomCount + "\" value='2' />Low floor</span>");
                                            cInfo.Append("<br />");
                                        }
                                        else
                                        {
                                            cInfo.Append("<div>");
                                            cInfo.Append("<label><strong>floor:</strong></label><br />");
                                            cInfo.Append("<span class='guest-requirement-col-1'>");
                                            cInfo.Append("<input type='radio' name=\"floor_type_" + conditionID[productCount] + "_" + roomCount + "\" value='3' checked='checked' />No preference</span>");
                                            cInfo.Append("<span class='guest-requirement-col-2'>");
                                            cInfo.Append("<span class='guest-requirement-col-3'></span><br class='clearAll'>");
                                            cInfo.Append("<br />");
                                        }
                                        cInfo.Append("<br />");
                                        cInfo.Append("<textarea class='mobile-textbox' name=\"sp_require_" + conditionID[productCount] + "_" + roomCount + "\" cols='65' rows='3'></textarea>");
                                        cInfo.Append("<br class='clearAll'>");
                                        cInfo.Append("<hr class='line'>");
                                        cInfo.Append("</div>");

                                    }
                                    else
                                    {
                                        if (!checkExtranet)
                                        {
                                            cInfo.Append("<p class='optionTitle'>ห้องพัก :" + GetProductTitle(optionID[productCount], OptionTitleList) + roomCount + "</strong></p>");
                                        }
                                        else
                                        {
                                            cInfo.Append("<p class='optionTitle'>ห้องพัก :" + GetProductExtraTitle(optionID[productCount], OptionTitleListExtra) + roomCount + "</strong></p>");
                                        }

                                        cInfo.Append("<div>");
                                        cInfo.Append("<label><strong>ชนิดเตียง:</strong></label>");
                                        cInfo.Append("<br />");
                                        cInfo.Append("<span class='guest-requirement-col-1'>");
                                        cInfo.Append("<input type='radio' name=\"bed_type_" + conditionID[productCount] + "_" + roomCount + "\" value='3' checked='checked' />ไม่ระบุ</span>");
                                        cInfo.Append("<span class='guest-requirement-col-2'>");
                                        cInfo.Append("<input type='radio' name=\"bed_type_" + conditionID[productCount] + "_" + roomCount + "\" value='1' />เตียงใหญ่</span>");
                                        cInfo.Append("<span class='guest-requirement-col-3'>");
                                        cInfo.Append("<input type='radio' name=\"bed_type_" + conditionID[productCount] + "_" + roomCount + "\" value='2' />เตียงคู่</span><br class='clearAll'>");
                                        cInfo.Append("</div>");
                                        cInfo.Append("<hr class='line-dot'>");
                                        if (ProductID != 3590 && ProductID != 3687 && ProductID != 3618)
                                        {
                                            cInfo.Append("<div>");
                                            cInfo.Append("<label><strong>สูบบุหรี่:</strong></label><br />");
                                            cInfo.Append("<span class='guest-requirement-col-1'>");
                                            cInfo.Append("<input type='radio' name=\"smoke_type_" + conditionID[productCount] + "_" + roomCount + "\" value='3' checked='checked' />ไม่ระบุ</span>");
                                            cInfo.Append("<span class='guest-requirement-col-2'>");
                                            cInfo.Append("<input type='radio' name=\"smoke_type_" + conditionID[productCount] + "_" + roomCount + "\" value='1' />ไม่สูบบุหรี่</span>");
                                            cInfo.Append("<span class='guest-requirement-col-3'>");
                                            cInfo.Append("<input type='radio' name=\"smoke_type_" + conditionID[productCount] + "_" + roomCount + "\" value='2' />สูบบุหรี่</span><br class='clearAll'>");
                                            cInfo.Append("</div>");
                                        }
                                        else
                                        {
                                            cInfo.Append("<div>");
                                            cInfo.Append("<label><strong>สูบบุหรี่:</strong></label><br />");
                                            cInfo.Append("<span class='guest-requirement-col-1'></span>");
                                            cInfo.Append("<span class='guest-requirement-col-2'>");
                                            cInfo.Append("<input type='radio' name=\"smoke_type_" + conditionID[productCount] + "_" + roomCount + "\" value='1' checked=\"checked\" />ไม่สูบบุหรี่</span>");
                                            cInfo.Append("<span class='guest-requirement-col-3'></span><br class='clearAll'>");
                                            cInfo.Append("</div>");
                                        }
                                        cInfo.Append("<hr class='line-dot'>");
                                        if (ProductID != 3618)
                                        {
                                            cInfo.Append("<div>");
                                            cInfo.Append("<label><strong>ระดับชั้น:</strong></label><br />");
                                            cInfo.Append("<span class='guest-requirement-col-1'>");
                                            cInfo.Append("<input type='radio' name=\"floor_type_" + conditionID[productCount] + "_" + roomCount + "\" value='3' checked='checked' />ไม่ระบุ</span>");
                                            cInfo.Append("<span class='guest-requirement-col-2'>");
                                            cInfo.Append("<input type='radio' name=\"floor_type_" + conditionID[productCount] + "_" + roomCount + "\" value='1' />ชั้นสูงๆ</span>");
                                            cInfo.Append("<span class='guest-requirement-col-3'>");
                                            cInfo.Append("<input type='radio' name=\"floor_type_" + conditionID[productCount] + "_" + roomCount + "\" value='2' />ชั้นล่างๆ</span>");
                                            cInfo.Append("<br />");
                                        }
                                        else
                                        {
                                            cInfo.Append("<div>");
                                            cInfo.Append("<label><strong>ระดับชั้น:</strong></label><br />");
                                            cInfo.Append("<span class='guest-requirement-col-1'>");
                                            cInfo.Append("<input type='radio' name=\"floor_type_" + conditionID[productCount] + "_" + roomCount + "\" value='3' checked='checked' />ไม่ระบุ</span>");
                                            cInfo.Append("<span class='guest-requirement-col-2'>");
                                            cInfo.Append("<span class='guest-requirement-col-3'></span><br class='clearAll'>");
                                            cInfo.Append("<br />");
                                        }
                                        cInfo.Append("<br />");
                                        cInfo.Append("<textarea class='mobile-textbox' name=\"sp_require_" + conditionID[productCount] + "_" + roomCount + "\" cols='65' rows='3'></textarea>");
                                        cInfo.Append("<br class='clearAll'>");
                                        cInfo.Append("<hr class='line'>");
                                        cInfo.Append("</div>");

                                    }


                                }
                            }

                        }
                    }
                }

                cInfo.Append("</div>");
                cInfo.Append("<br class='clear'>");

                break;
            case 32:
            case 34:
            case 36:
            case 38:
            case 39:
            case 40:
                for (int productCount = 0; productCount < conditionID.Count(); productCount++)
                {
                    if (quantity[productCount] > 0)
                    {
                        if (langID == 1)
                        {
                            cInfo.Append("<hr class='line'>");
                            cInfo.Append("<span class='text_guest'>");
                            cInfo.Append("<h1>Special Requirement</h1>");
                            cInfo.Append("<h2>(Subject to availability, can not guarantee)</h2>");
                            cInfo.Append("</span>");
                            cInfo.Append("<a id='g'></a><span class='link_show_daily_rate'><a href='#g' class='showguest'>");
                            cInfo.Append("<button class='icon-guest' onclick='showguest(this); return false;' title='Guest & Information Preference'></button>");
                            cInfo.Append("</a></span>");
                            cInfo.Append("<div class='guest'>");
                            cInfo.Append("<hr class='line-dot'>");
                            cInfo.Append("<div>");
                            cInfo.Append("<br />");
                            cInfo.Append("<textarea class='mobile-textbox' name=\"sp_require_" + conditionID[productCount] + "_1\"  cols='65' rows='3'></textarea>");
                            cInfo.Append("<br class='clearAll'>");
                            cInfo.Append("</div>");
                            cInfo.Append("</div>");
                            cInfo.Append("<br class='clear'>");
                            cInfo.Append("<hr class='line'>");
                        }
                        else
                        {
                            cInfo.Append("<hr class='line'>");
                            cInfo.Append("<span class='text_guest'>");
                            cInfo.Append("<h1>ความต้องการพิเศษ</h1>");
                            cInfo.Append("<h2>(ความต้องการพิเศษและคำขอพิเศษทั้งหมดขึ้นอยู่กับทางโรงแรมซึ่งจะไม่สามารถรับประกันได้แต่เราจะพยายามอย่างดีที่สุดเพื่อให้ท่านได้คำขอพิเศษตามที่ท่านได้ระบุไว้)</h2>");
                            cInfo.Append("</span>");
                            cInfo.Append("<a id='g'></a><span class='link_show_daily_rate'><a href='#g' class='showguest'>");
                            cInfo.Append("<button class='icon-guest' onclick='showguest(this); return false;' title='Guest & Information Preference'></button>");
                            cInfo.Append("</a></span>");
                            cInfo.Append("<div class='guest'>");
                            cInfo.Append("<hr class='line-dot'>");
                            cInfo.Append("<div>");
                            cInfo.Append("<br />");
                            cInfo.Append("<textarea class='mobile-textbox' name=\"sp_require_" + conditionID[productCount] + "_1\"  cols='65' rows='3'></textarea>");
                            cInfo.Append("<br class='clearAll'>");
                            cInfo.Append("</div>");
                            cInfo.Append("</div>");
                            cInfo.Append("<br class='clear'>");
                            cInfo.Append("<hr class='line'>");
                        }

                        break;
                    }
                }

                break;
        }

        #endregion



        formInfo = formInfo + cInfo.ToString();


        cPolicy.Append("<a class='close-reveal-modal' onclick='return false; ' title='Close'><img src='/images/ico_delete_circle.gif'></a>");
        cPolicy.Append("</span>");

        formInfo = formInfo + cPolicy.ToString();

        if (has_allotment)
        {
            formInfo = formInfo + "<input type=\"hidden\" name=\"al\" value=\"1\">\n";
        }
        else
        {
            formInfo = formInfo + "<input type=\"hidden\" name=\"al\" value=\"0\">\n";
        }

        if (IsMember)
        {
            formInfo = formInfo + "<input type=\"hidden\" name=\"au_m\" value=\"1\">\n";
        }
        else
        {
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
            formInfo = formInfo + "<input type=\"hidden\" name=\"tee_off_hour\" value=\"" + Request.Form["tee_hour"] + "\">\n";
            formInfo = formInfo + "<input type=\"hidden\" name=\"tee_off_min\" value=\"" + Request.Form["tee_min"] + "\">\n";
        }

        if (productDeposit.Deposit == 0)
        {
            formInfo = formInfo + "<input type=\"hidden\" name=\"sum_price\" value=\"" + (int)priceVatInc + "\">\n";
        }
        else
        {
            if (priceVatInc > totalPriceDeposit)
            {
                formInfo = formInfo + "<input type=\"hidden\" name=\"sum_price\" value=\"" + (int)totalPriceDeposit + "\">\n";
            }
            else
            {
                formInfo = formInfo + "<input type=\"hidden\" name=\"sum_price\" value=\"" + (int)priceVatInc + "\">\n";
            }
        }


        formInfo = formInfo + selectOption;



        string paymentForm = string.Empty;

        StringBuilder cPaymen = new StringBuilder();

        if (payLater.ProductID == 0)
        {

            if (langID == 1)
            {

                cPaymen.Append("<div class='text_payment'>");
                cPaymen.Append("<h1>Payment Method</h1>");

                if (totalPriceDeposit == (int)priceVatInc)
                {
                    cPaymen.Append("<span class='totalprice' style='text-size:16px;'>Grand Total Price : " + ((int)priceVatInc).ToString("#,###") + " Baht " + priceSummaryDisplay + " </span><br><span style='font-size: 11px;'>(Included of Tax and Service Charge.)</span>");

                }
                else
                {
                    if (int.Parse(Request.Form["hotel_id"]) == 3465)
                    {
                        cPaymen.Append("<span class='totalprice' style='text-size:16px;'>Grand Total Price : " + ((int)priceVatInc).ToString("#,###") + " Baht " + priceSummaryDisplay + "<br></span> <span style='font-size: 11px;'>(Included of Tax and Service Charge.)</span>");
                        cPaymen.Append("<br />");
                        cPaymen.Append("<span class='pay_checkin'>&quot;Please pay this total room rate when you check in at the hotel.&quot;</span>");
                    }
                    else
                    {
                        cPaymen.Append("<span class='totalprice'>Total " + ((int)priceVatInc).ToString("#,###") + " Baht " + priceSummaryDisplay + " </span><br><span style='font-size: 11px;'>(Included of Tax and Service Charge.)</span>");
                        cPaymen.Append("<br />");
                        cPaymen.Append(depositWord);
                        cPaymen.Append("<span class='pay_checkin'>&quot;The outstanding balance can be paid upon your check in time.&quot;</span>");
                        cPaymen.Append("<br />");
                        cPaymen.Append("<br />");
                        cPaymen.Append("<span class=\"fontRed\">*</span> You will be charged in local currency (Thai Baht). The displayed amount in your currency is indicative and based on today’s exchange rate.");
                    }
                }

                cPaymen.Append("</div>");
                cPaymen.Append("<br />");

                if (paymentMethod == 2)
                {
                    if (has_allotment || ManageID == 2)
                    {
                        if (gatewayID == 3 || gatewayID == 13)
                        {
                            cPaymen.Append("<div class=\"card_offer\"><input type=\"radio\" name=\"bank_id\" value=\"" + gatewayID + "\" checked=\"checked\"  style=\"float:left; margin-top:8px;\" /><img src=\"../theme_color/blue/images/layout_mail/VisaMastercard.jpg\" /></div>\n");
                        }
                        else
                        {
                            cPaymen.Append("<div class=\"card_offer\"><input type=\"radio\" name=\"bank_id\" value=\"" + gatewayID + "\" checked=\"checked\"  style=\"float:left; margin-top:8px;\" /><img src=\"../theme_color/blue/images/layout_mail/VisaMastercard.jpg\" /><img src=\"../theme_color/blue/images/layout_mail/jcb.jpg\" /></div>\n");
                        }
                    }
                    else
                    {
                        cPaymen.Append("<div class=\"card_offer\"><input type=\"hidden\" name=\"bank_id\" value=\"" + gatewayID + "\"/></div>\n");
                    }

                }
                else
                {
                    if (int.Parse(Request.Form["hotel_id"]) == 3465)
                    {
                        //Only C House Executive Condomenium
                        cPaymen.Append("<input type=\"hidden\" id=\"cardnum\" name=\"cardnum\" value=\"only_c-house\"/>");

                    }
                    else
                    {
                        cPaymen.Append("<h1>Credit Card Details</h1>");
                        cPaymen.Append("<span class='titleform'>Card Type <span class=\"fontRed\">*</span> : </span>");
                        cPaymen.Append("<select id='cardType' name='cardType' class='mobile-ddl'>");
                        cPaymen.Append("<option value='Visa'>Visa</option>");
                        cPaymen.Append("<option value='MasterCard'>Master Card</option>");
                        cPaymen.Append("<option value='JCB'>JCB</option>");
                        cPaymen.Append("</select>");
                        cPaymen.Append("<br />");
                        cPaymen.Append("<span class='titleform'>Credit Card Number <span class=\"fontRed\">*</span> : </span>");
                        cPaymen.Append("<input type='text' class='mobile-textbox' id=\"cardnum\" name=\"cardnum\" placeholder='Credit Card Number'><br />");
                        cPaymen.Append("<span class='titleform'>Security Code (CVV2) <span class=\"fontRed\">*</span> :</span>");
                        cPaymen.Append("<input type='text' class='mobile-textbox-mini' name=\"card_cvv\" id=\"card_cvv\" placeholder='code'><br />");
                        cPaymen.Append("<span class='titleform'>Expiry Date (mm/yyyy) <span class=\"fontRed\">*</span> :</span>");
                        cPaymen.Append("<select name='card_month' id='card_month' class='mobile-ddl'>");
                        cPaymen.Append("<option value='1'>1</option>");
                        cPaymen.Append("<option value='2'>2</option>");
                        cPaymen.Append("<option value='3'>3</option>");
                        cPaymen.Append("<option value='4'>4</option>");
                        cPaymen.Append("<option value='5'>5</option>");
                        cPaymen.Append("<option value='6' selected='selected'>6</option>");
                        cPaymen.Append("<option value='7'>7</option>");
                        cPaymen.Append("<option value='8'>8</option>");
                        cPaymen.Append("<option value='9'>9</option>");
                        cPaymen.Append("<option value='10'>10</option>");
                        cPaymen.Append("<option value='11'>11</option>");
                        cPaymen.Append("<option value='12'>12</option>");
                        cPaymen.Append("</select>");
                        cPaymen.Append("<select name='card_year' id='card_year' class='mobile-ddl'>");
                        cPaymen.Append("<option value='2011'>2011</option>");
                        cPaymen.Append("<option value='2012'>2012</option>");
                        cPaymen.Append("<option value='2013'>2013</option>");
                        cPaymen.Append("<option value='2014' selected='selected'>2014</option>");
                        cPaymen.Append("<option value='2015'>2015</option>");
                        cPaymen.Append("<option value='2016'>2016</option>");
                        cPaymen.Append("<option value='2017'>2017</option>");
                        cPaymen.Append("<option value='2018'>2018</option>");
                        cPaymen.Append("<option value='2019'>2019</option>");
                        cPaymen.Append("<option value='2020'>2020</option>");
                        cPaymen.Append("<option value='2021'>2021</option>");
                        cPaymen.Append("</select>");
                        cPaymen.Append("<br />");
                        cPaymen.Append("<span class='titleform'>Card Holder Name <span class=\"fontRed\">*</span> : </span>");
                        cPaymen.Append("<input type='text' class='mobile-textbox' name=\"card_name\" id=\"card_name\" placeholder='Card Holder Name'><br />");
                        cPaymen.Append("<span class='titleform'>Bank Name <span class=\"fontRed\">*</span> : </span>");
                        cPaymen.Append("<input type='text' class='mobile-textbox' name=\"bank_name\" id=\"bank_name\" placeholder='Bank Name'><br />");
                        cPaymen.Append("<hr class='line'>");

                    }
                }
            }

            cPaymen.Append("<br>");
            cPaymen.Append("<div class=\"card_offer text-agree\"><input type=\"checkbox\" name=\"chkPolicy\" id=\"chkPolicy\" value=\"0\">I have read and agree to the <a href=\"javascript:void(0)\" data-reveal-id=\"policyContractCancelDesc\" >Cancellation Policy and User Agreement</a></div><br>");
            cPaymen.Append("<hr class='line'>");
            cPaymen.Append("<div class='security-img'>");
            cPaymen.Append("<img src='http://engine.booking2hotels.com/images/Mobile/secu.png'>");
            cPaymen.Append("</div>");
            cPaymen.Append("</div>");
            cPaymen.Append("<footer style='background: #99c93a;'>");
            cPaymen.Append("<span>Total Price : THB " + ((int)priceVatInc).ToString("#,###") + " </span>");
            if (totalPriceDeposit != (int)priceVatInc)
            {
                if (int.Parse(Request.Form["hotel_id"]) != 3465)
                {
                    cPaymen.Append("<br />");
                    cPaymen.Append(depositWordMini);
                }
            }
            cPaymen.Append("</footer>");
            cPaymen.Append("<div id='submitPan'>");
            cPaymen.Append("<input type='submit' class='submitbooking' id='btnPayment' name='submit' value='Continue to payment'>");
            cPaymen.Append("</div>");
            cPaymen.Append("</div>");
            cPaymen.Append("</form>");
            cPaymen.Append("</body>");
            cPaymen.Append("</html>");

            cTitle.Append(formInfo);
            cTitle.Append(cPaymen.ToString());

            Response.Write(cTitle.ToString());
            Response.End();
        }
        else
        {
            cPaymen.Append("<h1>Credit Card Details</h1>");
            cPaymen.Append("<span class='titleform'>Card Type <span class=\"fontRed\">*</span> : </span>");
            cPaymen.Append("<select id='cardType' name='cardType'>");
            cPaymen.Append("<option value='Visa'>Visa</option>");
            cPaymen.Append("<option value='MasterCard'>Master Card</option>");
            cPaymen.Append("<option value='JCB'>JCB</option>");
            cPaymen.Append("</select>");
            cPaymen.Append("<br />");
            cPaymen.Append("<span style=\"font-size:16px;color:#555;line-height:18px;\">Total Price :<span style=\"color:#406c04\">" + ((int)priceVatInc).ToString("#,###") + " Baht" + priceSummaryDisplay + "</span><br />");
            cPaymen.Append("<span style=\"font-size:12px;color:#397c96;font-weight:normal\">(Included of Tax and Service Charge)</span>\n");
            cPaymen.Append("<br />");
            cPaymen.Append("<span style=\"text-align:left;width:220px;font-size:12px;color:#555;font-weight:normal;line-height:20px;\">**You will be charged only <span style=\"font-weight:bold;color:#406c04\">$1</span> for holding guarantee of your booking.</span>\n");
            cPaymen.Append("<br />");
            cPaymen.Append("<span class='titleform'>Credit Card Number <span class=\"fontRed\">*</span> : </span>");
            cPaymen.Append("<input type='Credit' id=\"cardnum\" name=\"cardnum\" placeholder='Credit Card Number' class='mobile-textbox'><br />");
            cPaymen.Append("<span class='titleform'>Security Code (CVV2) <span class=\"fontRed\">*</span> :</span>");
            cPaymen.Append("<input type='Code' name=\"card_cvv\" id=\"card_cvv\" placeholder='code' class='mobile-textbox-mini'><br />");
            cPaymen.Append("<span class='titleform'>Expiry Date (mm/yyyy) <span class=\"fontRed\">*</span> :</span>");
            cPaymen.Append("<select name='card_month' id='card_month' class='mobile-ddl'>");
            cPaymen.Append("<option value='1'>1</option>");
            cPaymen.Append("<option value='2'>2</option>");
            cPaymen.Append("<option value='3'>3</option>");
            cPaymen.Append("<option value='4'>4</option>");
            cPaymen.Append("<option value='5'>5</option>");
            cPaymen.Append("<option value='6' selected='selected'>6</option>");
            cPaymen.Append("<option value='7'>7</option>");
            cPaymen.Append("<option value='8'>8</option>");
            cPaymen.Append("<option value='9'>9</option>");
            cPaymen.Append("<option value='10'>10</option>");
            cPaymen.Append("<option value='11'>11</option>");
            cPaymen.Append("<option value='12'>12</option>");
            cPaymen.Append("</select>");
            cPaymen.Append("<select name='card_year' id='card_year' class='mobile-ddl'>");
            cPaymen.Append("<option value='2011'>2011</option>");
            cPaymen.Append("<option value='2012'>2012</option>");
            cPaymen.Append("<option value='2013'>2013</option>");
            cPaymen.Append("<option value='2014' selected='selected'>2014</option>");
            cPaymen.Append("<option value='2015'>2015</option>");
            cPaymen.Append("<option value='2016'>2016</option>");
            cPaymen.Append("<option value='2017'>2017</option>");
            cPaymen.Append("<option value='2018'>2018</option>");
            cPaymen.Append("<option value='2019'>2019</option>");
            cPaymen.Append("<option value='2020'>2020</option>");
            cPaymen.Append("<option value='2021'>2021</option>");
            cPaymen.Append("</select>");
            cPaymen.Append("<br />");
            cPaymen.Append("<span class='titleform'>Card Holder Name <span class=\"fontRed\">*</span> : </span>");
            cPaymen.Append("<input type='cardname' name=\"card_name\" id=\"card_name\" class='mobile-textbox' placeholder='Card Holder Name'><br />");
            cPaymen.Append("<span class='titleform'>Bank Name <span class=\"fontRed\">*</span> : </span>");
            cPaymen.Append("<input type='bank' name=\"bank_name\" id=\"bank_name\" class='mobile-textbox' placeholder='Bank Name'><br />");
            cPaymen.Append("<hr class='line'>");

        }

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

        cPaymen.Append("<br>");
        cPaymen.Append("<div class=\"card_offer\"><input type=\"checkbox\" name=\"chkPolicy\" id=\"chkPolicy\" value=\"0\">I have read and agree to the <a href=\"javascript:void(0)\" data-reveal-id=\"policyContractCancelDesc\" >Cancellation Policy and User Agreement</a></div><br>");
        cPaymen.Append("<hr class='line'>");
        cPaymen.Append("<div class='security-img'>");
        cPaymen.Append("<img src='http://engine.booking2hotels.com/images/Mobile/secu.png'>");
        cPaymen.Append("</div>");
        cPaymen.Append("</div>");
        cPaymen.Append("<footer style='background: #99c93a;'>");
        cPaymen.Append("<span>Total Price : THB " + ((int)priceVatInc).ToString("#,###") + " </span>");
        if (totalPriceDeposit != (int)priceVatInc)
        {
            if (int.Parse(Request.Form["hotel_id"]) != 3465)
            {
                cPaymen.Append("<br />");
                cPaymen.Append(depositWordMini);
            }
        }
        cPaymen.Append("</footer>");
        cPaymen.Append("<div id='submitPan'>");
        cPaymen.Append("<a href='thanks.html'>");
        cPaymen.Append("<input type='submit' class='submitbooking' id='btnPayment' name='submit' value='Continue to payment'>");
        cPaymen.Append("</div>");
        cPaymen.Append("</form>");
        cPaymen.Append("</body>");
        cPaymen.Append("</html>");

        cTitle.Append(formInfo);
        if (has_allotment && int.Parse(Request.Form["hotel_id"]) == 3466)
        {

        }
        else
        {
            cTitle.Append(cPaymen.ToString());
        }
        Response.Write(cTitle.ToString());
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

}