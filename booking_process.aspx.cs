using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Front;
using Hotels2thailand;
using System.Data.SqlClient;
using Hotels2thailand.DataAccess;

using Hotels2thailand.Affiliate;
using Hotels2thailand.Booking;
using System.Web.Configuration;
using Newtonsoft.Json;

public partial class booking_process : System.Web.UI.Page
{
    private string connString = WebConfigurationManager.ConnectionStrings["booking2hotelXConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {

        
        
        string sumPrice=Request.Form["sum_price"];
        double discount = double.Parse(Request.Form["discount"]);
        byte prefix = byte.Parse(Request.Form["prefix"]);
        byte langID = byte.Parse(Request.Form["ln"]);
        string first_name=Request.Form["first_name"];
        string email=Request.Form["email"];
        string re_email = Request.Form["re_email"];
        string phone=Request.Form["phone"];
        string flight_arv_num = Request.Form["flight_name_in"];
        string flight_dep_num = Request.Form["flight_name_out"];


        string strCountry = Request.Form["country"];

        string[] arrCountry = strCountry.Split(',');
        byte country = byte.Parse(arrCountry[0]);
        string country_code = arrCountry[1];


        byte refCountry = byte.Parse(Request.Form["refCountry"]);
        string phone_code=Request.Form["phone_code"];
        short supplierID = short.Parse(Request.Form["sid"]);
        int hotelID=int.Parse(Request.Form["hotel_id"]);
        string bedType=Request.Form["bed_type"];
        string transfer_detail = string.Empty;
        DateTime teeOfDate;
        decimal vatInclude = Convert.ToDecimal(1.177);

        string sqlCommand = string.Empty;
        byte categoryID = 0;
        double total_commission = 0;

        string htmlPage = string.Empty;

        string memberAuthen = Request.Form["au_m"];

        

        htmlPage = htmlPage + "<html xmlns=\"http://www.w3.org/1999/xhtml\">\n";
        htmlPage = htmlPage + "<head>\n";
        htmlPage = htmlPage + "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\n";
        htmlPage = htmlPage + "<title>Booking2hotel:Booking Process</title>\n";

        
        htmlPage = htmlPage + "</head>\n";
        htmlPage = htmlPage + "<body>\n";
        htmlPage = htmlPage + "<img src=\"/theme_color/blue/images/icon/preload.gif\"  style=\" position:absolute; top:40%; left:36%\" />\n";
        htmlPage = htmlPage + "</body>\n";
        htmlPage = htmlPage + "</html>\n";
        Response.Write(htmlPage);

        bool checkExtranet = false;
        byte extranet_comission = 17;
        byte manageID = 1;
        using (SqlConnection cn = new SqlConnection(connString))
        {
            sqlCommand = "select p.cat_id,p.extranet_active,pe.manage_id";
            sqlCommand=sqlCommand+" from tbl_product p,tbl_product_booking_engine pe";
            sqlCommand=sqlCommand+" where p.product_id=pe.product_id and p.product_id="+hotelID;

            SqlCommand cmd = new SqlCommand(sqlCommand, cn);
            cn.Open();
            SqlDataReader reader=cmd.ExecuteReader();
            if(reader.Read()){
                categoryID = (byte)reader["cat_id"];
                checkExtranet = (bool)reader["extranet_active"];
                manageID = (byte)reader["manage_id"];
            }
           
        }
       

  


        if (checkExtranet)
        {
            extranet_comission=0;
            
        }
        

        
        byte adult=0;
        byte child=0;

        if (categoryID == 29)
        {
            adult = byte.Parse(Request.Form["adult"]);
            child = byte.Parse(Request.Form["child"]);
        }


        DateTime dateStart=Request.Form["date_start"].Hotels2DateSplitYear("-");
        DateTime dateEnd;
        if(categoryID==29){
            dateEnd = Request.Form["date_end"].Hotels2DateSplitYear("-");
        }else{
            dateEnd = dateStart.AddDays(1);
        }

        FrontProductDeposit productDeposit = new FrontProductDeposit(hotelID);
        productDeposit.GetDeposit(dateStart);
        FrontOptionPackage objPackage = new FrontOptionPackage(hotelID, dateStart, dateEnd);
        FrontOptionMeal objMeal = new FrontOptionMeal(hotelID, dateStart, dateEnd);
        //ProductPrice pricelist = new ProductPrice(hotelID, dateStart, dateEnd);
       
        //Insert Customer

        /*Don't forget check new customer with email*/
        Customer customer = new Customer();

        customer.CountryID = country;
        customer.PrefixID = prefix;
        customer.FullName = first_name;
        customer.Email =email;
        customer.Mail = true;
        customer.ProductID = hotelID;
        customer.Password = "";
        int customerID=customer.InsertWithCheckEmail(customer);
        //End Insert Customer

        // Insert Customer Phone
        /*Don't forget check new customer with id for update new phone number*/

        CustomerPhone Phone = new CustomerPhone();
        Phone.Category = 2;
        Phone.CustomerID = customerID;
        //Phone.CountryCode = "66";//Get from country class
        Phone.LocalCode = "66";//Get from country class
        Phone.PhoneNumber = phone;
        Phone.Insert(Phone);
        // End Insert Customer Phone

        DateTime timeArv=new DateTime();
        DateTime timeDep = new DateTime();

        
        
        if (!string.IsNullOrEmpty(Request.Form["Hdflight_ci"]))
        {
            timeArv = Utility.ConvertDateInput(Request.Form["Hdflight_ci"]);
            timeArv = timeArv.AddHours(Convert.ToDouble(Request.Form["time_hour_arv"]));
            timeArv = timeArv.AddMinutes(Convert.ToDouble(Request.Form["time_min_arv"]));
        
        }

        if (!string.IsNullOrEmpty(Request.Form["Hdflight_co"]))
        {
            timeDep = Utility.ConvertDateInput(Request.Form["Hdflight_co"]);
            timeDep = timeDep.AddHours(Convert.ToDouble(Request.Form["time_hour_dep"]));
            timeDep = timeDep.AddMinutes(Convert.ToDouble(Request.Form["time_min_dep"]));
        }


        FrontPayLater payLater = new FrontPayLater();
        payLater.GetPayLaterByDate(hotelID, dateStart);

        //Insert New Booking
        //HttpCookie affCookie=Request.Cookies["site_id"];

        Booking booking = new Booking();
        booking.CountryID = country;
        booking.StatusID = 68;
        booking.StatusAffiliateID = 73;
        booking.CustomerID=customerID;
        booking.LanguageID = langID;

        booking.AgentcyId = (!string.IsNullOrEmpty(Request.Form["hdAgencyID"]) ? int.Parse(Request.Form["hdAgencyID"]) : booking.AgentcyId);
       
        booking.PrefixID = prefix;
        booking.NameFull = first_name;
        booking.Email = email;
        //Only select Transfer
        if (!String.IsNullOrEmpty(Request.Form["Hdflight_ci"]))
        {
            booking.FlightArrivalNumber = flight_arv_num;
            booking.FlightArrivalTime = timeArv;
        }

        if (!String.IsNullOrEmpty(Request.Form["Hdflight_co"]))
        {
            booking.FlightDepartureNumber = flight_dep_num;
            booking.FlightDepartureTime = timeDep;
        }
        booking.RefererIP = Request.ServerVariables["REMOTE_ADDR"];
        //booking.RefererIP = "127.0.0.1";
        booking.DateSubmit = DateTime.Now.Hotels2ThaiDateTime();
        booking.DateModify = DateTime.Now.Hotels2ThaiDateTime();
        booking.Status = false;

        if(payLater.ProductID==0)
        {
            booking.PaymentTypeID = 1;
        }else{
            booking.PaymentTypeID = 2;
        }
        if (checkExtranet)
        {
            booking.IsExtranet = true;
        }
        else
        {
            booking.IsExtranet = false;
        }
        //---------

        

        booking.Deposit = productDeposit.Deposit;
        booking.DepositCategory = productDeposit.DepositCateID;
        if (productDeposit.DepositCateID == 100)
        {
            booking.DepositPrice = 0;
        }
        else {
            booking.DepositPrice = decimal.Parse(Request.Form["sum_price"]);
        }
        

        if (memberAuthen=="1")
            {
                booking.IsMember =true;
            }

        int bookingID=booking.Insert(booking);

        int bookingHotelID = 0;
        using (SqlConnection cn = new SqlConnection(connString))
        {
            sqlCommand = "select isnull(MAX(booking_hotel_id),0) from tbl_booking_hotels where product_id=" + hotelID;
            SqlCommand cmd = new SqlCommand(sqlCommand, cn);
            cn.Open();
            bookingHotelID = (int)cmd.ExecuteScalar();
            if (bookingHotelID == 0)
            {
                bookingHotelID = 15000;
            }
            else {
                bookingHotelID = bookingHotelID + 1;
            }
            FrontBookingHotel bookingHotel = new FrontBookingHotel();
            bookingHotel.BookingID = bookingID;
            bookingHotel.ProductID = hotelID;
            bookingHotel.BookingHotelID=bookingHotelID;
            
            bookingHotel.Insert(bookingHotel);
        }

        // End Insert New Booking

        //Insert Booking Product
        FrontBookingProduct product = new FrontBookingProduct();
        product.BookingID = bookingID;
        product.SupplierID = supplierID;
        product.ProductID = hotelID;
        if(categoryID==32){
            teeOfDate = dateStart.AddHours(Convert.ToDouble(Request.Form["tee_off_hour"]));
            teeOfDate = teeOfDate.AddMinutes(Convert.ToDouble(Request.Form["tee_off_min"]));
            product.DateCheckIn = teeOfDate;
        }else{
        product.DateCheckIn=dateStart;
        }
        
        product.DateCheckOut=dateEnd;
        product.StatusID = 10;
        product.NumAdult = adult;
        product.NumChild = child;
        product.NumGolfer = 0;
        if(discount!=0)
        {  
            //if(discount>=0)
            //{
            //    product.Detail = "Discount Hidden :"+discount+" Baht";
            //}else{
            //    product.Detail = "Discount Hidden :" + discount + " Percent";
            //}
            product.Detail = discount.ToString();
        }else{
            product.Detail = null;
        }
        
        product.Status = true;
        product.PrepaidDate = DateTime.Now.AddDays(3);
        int bookingProductID=product.Insert(product);
        frontBookingCom objCom = new frontBookingCom(hotelID,bookingProductID);
        objCom.loadComVal();
        // End Insert Booking Product

        //Insert Booking Item
        FrontBookingItem item = new FrontBookingItem();
        

        //
        
       
        
        
        
        BookingRoomRequirement requirement;
        BookingGeneralRequirement requirementGeneral;
        byte roomQuantity;
        decimal priceTotal = 0;
        decimal priceOwnTotal = 0;
        decimal priceOption = 0;
        int itemID;
        byte unit;
        byte typeBed;
        byte typeFloor;
        byte typeSmoke;
        string comment;
        OptionPrice price;
        string optionTmp = string.Empty;
        string optionCheck=string.Empty;
        decimal priceAverate = 0;
        decimal subTotalPrice = 0;
        int numNight = dateEnd.Subtract(dateStart).Days;

        
        //General Process
        ProductPrice productList = null;
        PriceSupplement priceSupplement = null;
        List<PriceBase> rateList= null;

        FrontProductPriceExtranet productExtraList = null;
        List<ExtranetPriceBase> rateExtraList = null;

        string ProductTitle = string.Empty;
        bool hasRoomSelect = false;

        priceSupplement = new PriceSupplement();
        priceSupplement.LoadPriceSupplementByProductID(hotelID);

        productList = new ProductPrice(hotelID, categoryID, dateStart, dateEnd);
        productList.LangID = langID;
        productList.DiscountPrice = discount;

        productList.LoadPrice();

        rateList = productList.RateBase(99);
        if(!checkExtranet)
        {
            
            ProductTitle = rateList[0].ProductTitle;

            foreach (PriceBase temp in rateList)
            {
                roomQuantity = 0;
                
                foreach (int promotion in productList.GetListPromotionID())
                {
                    optionCheck = "room_" + temp.ConditionID + "_" + temp.OptionID + "_" + promotion;
                    //Response.Write(optionCheck + "<br>");
                    //Response.Flush();
                    if (!string.IsNullOrEmpty(Request.Form[optionCheck]))
                    {
                        roomQuantity = byte.Parse(Request.Form[optionCheck]);
                        if (roomQuantity > 0 && optionCheck != optionTmp)
                        {
                            //Response.Write(optionCheck + "--" + optionTmp + "--" + roomQuantity + "<br/>");
                            //Response.Flush();
                            unit = roomQuantity;
                            //price=new CalculatePrice((int)reader[0],dateStart,dateEnd);
                            price = productList.CalculateAll(temp.ConditionID, temp.OptionID, promotion);
                            priceAverate = (int)(price.Price / numNight) * numNight;
                            priceOption = priceSupplement.GetPriceSupplement(dateStart, priceAverate, temp.ConditionID, unit);
                            subTotalPrice = (int)(priceOption * vatInclude);
                            //Response.Write(price.PriceOwn + "a<br>");
                            priceTotal = priceTotal + subTotalPrice;
                            priceOwnTotal = priceOwnTotal + price.PriceOwn;
                            
                            item.BookingID = bookingID;
                            item.BookingProductID = bookingProductID;
                            item.OptionID = temp.OptionID;
                            item.OptionTitle = temp.OptionTitle;
                            item.ConditionID = temp.ConditionID;
                            item.ConditionTitle = temp.ConditionTitle;
                            item.ConditionDetail = GetPolicyDetailXml(temp.ConditionID, dateStart);
                            item.Unit = unit;
                            //item.PriceSupplier = price.PriceOwn * unit;
                            //item.Price = price.Price * unit;
                         
                            item.PriceSupplier = priceSupplement.GetPriceQwnSupplement(dateStart, price.PriceOwn, temp.ConditionID, unit);


                            item.Price = item.PriceSupplier;
                            if (productList.CheckPromotionAccept(promotion))
                            {
                                FrontPromotion promotionDetail = new FrontPromotion();
                                promotionDetail.GetPromotionByID(promotion);
                                item.PromotionID = promotion;
                                item.PromotionTitle = promotionDetail.Title;
                                item.PromotionDetail = GetPromotionDetailXml(promotionDetail);
                            }



                            item.PriceDisplay = item.PriceSupplier;
                            item.Status = true;
                            item.Breakfast = temp.Breakfast;
                            item.NumChildren = temp.NumChild;
                            item.NumAdult = temp.NumAdult;

                            Hotels2LogWriter.WriteFile("admin/logfile/bookingitem.html", item.PriceDisplay + "--" + item.PriceSupplier);

                            itemID = item.Insert(item);
                            

                            switch (categoryID)
                            {
                                case 29:
                                    for (int countReq = 1; countReq <= unit; countReq++)
                                    {

                                        comment = Request.Form["sp_require_" + item.ConditionID + "_" + countReq];
                                        if (comment == "Special Requirement")
                                        {
                                            comment = "";
                                        }
                                        typeBed = byte.Parse(Request.Form["bed_type_" + item.ConditionID + "_" + countReq]);
                                        typeFloor = byte.Parse(Request.Form["floor_type_" + item.ConditionID + "_" + countReq]);
                                        typeSmoke = byte.Parse(Request.Form["smoke_type_" + item.ConditionID + "_" + countReq]);




                                        requirement = new BookingRoomRequirement(itemID, typeBed, typeSmoke, typeFloor, comment);
                                        requirement.Insert(requirement);
                                    }
                                    break;
                                case 32:
                                case 34:
                                case 36:
                                case 38:
                                case 39:

                                    if (Request.Form["sp_require_" + item.ConditionID + "_1"] != null)
                                    {
                                        comment = Request.Form["sp_require_" + item.ConditionID + "_1"];
                                        if (comment == "Special Requirement")
                                        {
                                            comment = "";
                                        }
                                        requirementGeneral = new BookingGeneralRequirement(itemID, comment);
                                        requirementGeneral.Insert(requirementGeneral);
                                    }




                                    break;

                                case 40:

                                    if (Request.Form["sp_require_" + item.ConditionID + "_1"] != null)
                                    {
                                        comment = Request.Form["sp_require_" + item.ConditionID + "_1"].ToString();
                                        if (comment == "Special Requirement")
                                        {
                                            comment = "";
                                        }
                                        BookingSpaRequirement requirementSpa = new BookingSpaRequirement(itemID, comment);
                                        requirementSpa.Insert(requirementSpa);
                                    }


                                    break;

                            }
                            //for

                        }
                        break;
                    }


                }

                optionTmp = optionCheck;

            }
        }else{
            ProductPolicyExtranet policyExtra = new ProductPolicyExtranet();
            List<ProductPolicyExtranet> policyExtraList = policyExtra.GetExtraPolicy(hotelID, 1);
            CancellationExtranet cancellationExtra = new CancellationExtranet(hotelID, dateStart);
            List<CancellationExtranet> cancellationExtraList = cancellationExtra.GetCancellation();

            productExtraList = new FrontProductPriceExtranet(hotelID, categoryID, dateStart, dateEnd);
            productExtraList.LangID = langID;
            productExtraList.DiscountPrice = discount;

            productExtraList.LoadPrice();
            if(memberAuthen=="1")
            {
                productExtraList.memberAuthen = true;
            }
            rateExtraList = productExtraList.RateBase(99);


            if (rateExtraList.Count > 0)
            {
                ProductTitle = rateExtraList[0].ProductTitle;

                foreach (ExtranetPriceBase temp in rateExtraList)
                {
                    roomQuantity = 0;

                    foreach (int promotion in productExtraList.GetListPromotionID())
                    {
                        optionCheck = "room_" + temp.ConditionID + "_" + temp.OptionID + "_" + promotion;


                        if (!string.IsNullOrEmpty(Request.Form[optionCheck]))
                        {
                            roomQuantity = byte.Parse(Request.Form[optionCheck]);
                        }
                        else
                        {
                            roomQuantity = 0;
                        }

                        if (roomQuantity > 0 && optionCheck != optionTmp)
                        {
                            hasRoomSelect = true;
                            //Response.Write(optionCheck + "<br>");
                            //Response.Flush();

                            unit = roomQuantity;
                            price = productExtraList.CalculateAll(temp.ConditionID, temp.OptionID, promotion);
                            priceAverate = (int)(price.Price / numNight) * numNight;
                            priceOption = priceSupplement.GetPriceSupplement(dateStart, priceAverate, temp.ConditionID, unit);
                            subTotalPrice = (int)(priceOption * vatInclude);
                            priceTotal = priceTotal + subTotalPrice;
                            priceOwnTotal = priceOwnTotal + price.PriceOwn;

                            item.BookingID = bookingID;
                            item.BookingProductID = bookingProductID;
                            item.OptionID = temp.OptionID;
                            item.OptionTitle = temp.OptionTitle;
                            item.ConditionID = temp.ConditionID;
                            item.ConditionTitle = temp.ConditionTitle;
                            item.ConditionDetail = GetPolicyDetailExtraXml(policyExtraList, cancellationExtraList, item.ConditionID);
                            item.Unit = unit;
                            item.PromotionID = null;
                            item.PromotionTitle = string.Empty;
                            item.PromotionDetail = string.Empty;

                            total_commission = (double)price.PriceOwn;

                            //edit price  catch to int
                            item.PriceSupplier = (int)(decimal)total_commission * unit;


                            //Hotels2LogWriter.WriteFile("admin/logfile/bookingitem.html", ((decimal)total_commission * unit) + "--" + item.PriceSupplier);
                            //Response.Write(item.PriceSupplier + "a<br>");
                            //edit price
                            item.Price = (int)item.PriceSupplier;

                            if (productExtraList.CheckPromotionAccept(promotion))
                            {
                                FrontPromotionExtranet promotionDetail = new FrontPromotionExtranet();
                                promotionDetail.GetPromotionExtraByID(promotion);
                                item.PromotionID = promotion;
                                item.PromotionTitle = promotionDetail.Title;
                                item.PromotionDetail = GetPromotionDetailExtraXml(promotionDetail);
                                //Use Policy for Promotion
                                FrontPromotionCancel objPromotionPolicyCancel = new FrontPromotionCancel();
                                IList<object> cancellationPromotionExtraList = objPromotionPolicyCancel.GetPromotionCancelByPromotionID(promotion);

                                if (cancellationPromotionExtraList.Count > 0)
                                {
                                    item.ConditionDetail = GetPolicyPromotionExtraXml(policyExtraList, cancellationPromotionExtraList, item.ConditionID);
                                }
                            }
                            //edit price
                            item.PriceDisplay = (int)item.PriceSupplier;
                            item.Status = true;
                            item.Breakfast = temp.Breakfast;
                            item.NumChildren = temp.NumChild;
                            item.NumAdult = temp.NumAdult;

                            if (HttpContext.Current.Session["memberAccess"] != null)
                            {
                                item.MemberDetail = productExtraList.XmlMemberDiscountContent(temp.ConditionID);
                            }
                            else
                            {
                                item.MemberDetail = "no authen";
                            }
                            itemID = item.Insert(item);


                            switch (categoryID)
                            {
                                case 29:
                                    for (int countReq = 1; countReq <= unit; countReq++)
                                    {

                                        comment = Request.Form["sp_require_" + item.ConditionID + "_" + countReq];
                                        typeBed = byte.Parse(Request.Form["bed_type_" + item.ConditionID + "_" + countReq]);
                                        typeFloor = byte.Parse(Request.Form["floor_type_" + item.ConditionID + "_" + countReq]);
                                        typeSmoke = byte.Parse(Request.Form["smoke_type_" + item.ConditionID + "_" + countReq]);




                                        requirement = new BookingRoomRequirement(itemID, typeBed, typeSmoke, typeFloor, comment);
                                        requirement.Insert(requirement);
                                    }
                                    break;
                                case 32:
                                case 34:
                                case 36:
                                case 38:
                                case 39:

                                    if (Request.Form["sp_require_" + item.ConditionID + "_1"] != null)
                                    {
                                        comment = Request.Form["sp_require_" + item.ConditionID + "_1"];
                                        requirementGeneral = new BookingGeneralRequirement(itemID, comment);
                                        requirementGeneral.Insert(requirementGeneral);
                                    }




                                    break;

                                case 40:

                                    if (Request.Form["sp_require_" + item.ConditionID + "_1"] != null)
                                    {
                                        comment = Request.Form["sp_require_" + item.ConditionID + "_1"].ToString();
                                        BookingSpaRequirement requirementSpa = new BookingSpaRequirement(itemID, comment);
                                        requirementSpa.Insert(requirementSpa);
                                    }


                                    break;

                            }
                            //for
                            optionTmp = optionCheck;
                            break;
                        }




                    }



                }
            }
            string[] packageOption = new string[2];
            decimal ratePackage = 0;
            foreach (FrontOptionPackage itemPackage in objPackage.GetPackageList())
            {

                if (Request.Form["ddPackage_" + itemPackage.ConditionID + "_" + itemPackage.OptionID] != null)
                {
                    packageOption = Request.Form["ddPackage_" + itemPackage.ConditionID + "_" + itemPackage.OptionID].Split('_');
                    //Response.Write("ddPackage_" + itemPackage.ConditionID + "_" + itemPackage.OptionID + "<br>");
                    roomQuantity = byte.Parse(Request.Form["ddPackage_" + itemPackage.ConditionID + "_" + itemPackage.OptionID]);
                    if (roomQuantity != 0)
                    {
                            hasRoomSelect = true;
                            unit = roomQuantity;
                            //price = productExtraList.CalculateAll(itemPackage.ConditionID, itemPackage.OptionID, 0);
                            //priceAverate = (int)(price.Price / numNight) * numNight;
                            priceOption = itemPackage.Price;
                            subTotalPrice = (int)(priceOption * vatInclude);
                            priceTotal = priceTotal + subTotalPrice;
                            priceOwnTotal = priceOwnTotal + itemPackage.Price;

                            item.BookingID = bookingID;
                            item.BookingProductID = bookingProductID;
                            item.OptionID = itemPackage.OptionID;
                            item.OptionTitle = itemPackage.OptionTitle;
                            item.OptionDetail = itemPackage.OptionDetail;
                            item.ConditionID = itemPackage.ConditionID;
                            item.ConditionTitle = itemPackage.ConditionTitle;
                            item.ConditionDetail = GetPolicyDetailExtraXml(policyExtraList, cancellationExtraList, itemPackage.ConditionID);
                            item.Unit = unit;
                            item.PromotionID = null;
                            item.PromotionTitle = string.Empty;
                            item.PromotionDetail = string.Empty;

                            total_commission = (double)itemPackage.Price;

                            item.PriceSupplier = (decimal)total_commission * unit;
                            //Response.Write(item.PriceSupplier + "a<br>");
                            item.Price = item.PriceSupplier;

                            //if (productExtraList.CheckPromotionAccept(promotion))
                            //{
                            //    FrontPromotionExtranet promotionDetail = new FrontPromotionExtranet();
                            //    promotionDetail.GetPromotionExtraByID(promotion);
                            //    item.PromotionID = promotion;
                            //    item.PromotionTitle = promotionDetail.Title;
                            //    item.PromotionDetail = GetPromotionDetailExtraXml(promotionDetail);
                            //    //Use Policy for Promotion
                            //    FrontPromotionCancel objPromotionPolicyCancel = new FrontPromotionCancel();
                            //    IList<object> cancellationPromotionExtraList = objPromotionPolicyCancel.GetPromotionCancelByPromotionID(promotion);

                            //    if (cancellationPromotionExtraList.Count > 0)
                            //    {
                            //        item.ConditionDetail = GetPolicyPromotionExtraXml(policyExtraList, cancellationPromotionExtraList, item.ConditionID);
                            //    }
                            //}
                            item.PriceDisplay = item.PriceSupplier;
                            item.Status = true;
                            item.Breakfast = itemPackage.Breakfast;
                            item.NumChildren = itemPackage.NumChild;
                            item.NumAdult = itemPackage.NumAdult;
                            item.ConditionIsAdult = itemPackage.IsAdult;
                            if (Session["memberAccess"] != null)
                            {
                                item.MemberDetail = productExtraList.XmlMemberDiscountContent(item.ConditionID);
                            }
                            itemID = item.Insert(item);

                            for (int roomCount = 1; roomCount <= roomQuantity; roomCount++)
                            {
                                comment = Request.Form["sp_require_" + itemPackage.ConditionID + "_" + roomCount];
                                typeBed = byte.Parse(Request.Form["bed_type_" + itemPackage.ConditionID + "_" + roomCount]);
                                typeFloor = byte.Parse(Request.Form["floor_type_" + itemPackage.ConditionID + "_" + roomCount]);
                                typeSmoke = byte.Parse(Request.Form["smoke_type_" + itemPackage.ConditionID + "_" + roomCount]);




                                requirement = new BookingRoomRequirement(itemID, typeBed, typeSmoke, typeFloor, comment);
                                requirement.Insert(requirement);
                            }
                    }
                }
            }

            string[] mealOption = new string[2];
            decimal rateMeal = 0;
            foreach (FrontOptionMeal itemMeal in objMeal.GetMealList())
            {

                if (Request.Form["ddMeal_" + itemMeal.ConditionID + "_" + itemMeal.OptionID] != null)
                {
                    mealOption = Request.Form["ddMeal_" + itemMeal.ConditionID + "_" + itemMeal.OptionID].Split('_');
                    //Response.Write("ddPackage_" + itemPackage.ConditionID + "_" + itemPackage.OptionID + "<br>");
                    roomQuantity = byte.Parse(Request.Form["ddMeal_" + itemMeal.ConditionID + "_" + itemMeal.OptionID]);
                    if (roomQuantity != 0)
                    {

                        unit = roomQuantity;
                        //price = productExtraList.CalculateAll(itemPackage.ConditionID, itemPackage.OptionID, 0);
                        //priceAverate = (int)(price.Price / numNight) * numNight;
                        priceOption = itemMeal.Price;
                        subTotalPrice = (int)(priceOption * vatInclude);
                        priceTotal = priceTotal + subTotalPrice;
                        priceOwnTotal = priceOwnTotal + itemMeal.Price;

                        item.BookingID = bookingID;
                        item.BookingProductID = bookingProductID;
                        item.OptionID = itemMeal.OptionID;
                        item.OptionTitle = itemMeal.OptionTitle;
                        item.OptionDetail = itemMeal.OptionDetail;
                        item.ConditionID = itemMeal.ConditionID;
                        item.ConditionTitle = itemMeal.ConditionTitle;
                        item.ConditionDetail = GetPolicyDetailExtraXml(policyExtraList, cancellationExtraList, itemMeal.ConditionID);
                        item.Unit = unit;
                        item.PromotionID = null;
                        item.PromotionTitle = string.Empty;
                        item.PromotionDetail = string.Empty;

                        total_commission = (double)itemMeal.Price;

                        item.PriceSupplier = (decimal)total_commission * unit;
                        item.Price = item.PriceSupplier;


                        item.PriceDisplay = item.PriceSupplier;
                        item.Status = true;
                        item.Breakfast = itemMeal.Breakfast;
                        item.NumChildren = itemMeal.NumChild;
                        item.NumAdult = itemMeal.NumAdult;
                        if (Session["memberAccess"] != null)
                        {
                            item.MemberDetail = productExtraList.XmlMemberDiscountContent(item.ConditionID);
                        }
                        itemID = item.Insert(item);

                    }
                }
            }
        }



        int numNightRoom = dateEnd.Subtract(dateStart).Days;

        //
        //Insert Extra Option
        decimal priceExtra = 0;
        bool checkTransfer = false;

        int nightCalculate = 1;

        if(!checkExtranet)
        {
            foreach (PriceBase temp in rateList)
            {
                optionCheck = "ddPriceExtra_" + temp.ConditionID;
                transfer_detail="";
                if (!string.IsNullOrEmpty(Request.Form[optionCheck]))
                {
                    roomQuantity = byte.Parse(Request.Form[optionCheck]);
                
                    if (roomQuantity > 0 && optionCheck != optionTmp)
                    {

                        if (temp.OptionCategoryID == 43 || temp.OptionCategoryID == 44)
                        {
                            checkTransfer = true;
                            transfer_detail = Request.Form["transfer_detail"];
                            nightCalculate = 1;
                        }
                        else {
                            nightCalculate = numNightRoom;
                        }
                        unit = roomQuantity;
                        //price=new CalculatePrice((int)reader[0],dateStart,dateEnd);
                        priceExtra = temp.Rate;
                        priceTotal = priceTotal + temp.Rate*unit;
                        priceOwnTotal = priceOwnTotal + temp.RateOwn;

                    
                        FrontBookingItem items = new FrontBookingItem
                        {
                            BookingID = bookingID,
                            BookingProductID = bookingProductID,
                            OptionID = temp.OptionID,
                            OptionTitle=temp.OptionTitle,
                            ConditionID = temp.ConditionID,
                            ConditionTitle = temp.ConditionTitle,
                            ConditionDetail = GetPolicyDetailXml(temp.ConditionID, dateStart),
                            Unit = unit,
                            PriceSupplier = temp.RateOwn * unit * nightCalculate,
                            Price = temp.Rate * unit * nightCalculate,
                            PromotionID = null,
                            PromotionTitle = "",
                            PromotionDetail = "",
                            Detail=transfer_detail,
                            //for change currency
                            PriceDisplay = temp.Rate * unit * nightCalculate,
                            //
                            Breakfast=temp.Breakfast,
                            NumAdult=temp.NumAdult,
                            NumChildren=temp.NumChild,
                            Status = true
                        };


                        itemID = items.Insert(items);
                    }
                
                }
                optionTmp = optionCheck;
            }
        }else{

            
        foreach (ExtranetPriceBase temp in rateExtraList)
        {
            optionCheck = "ddPriceExtra_" + temp.ConditionID;
            transfer_detail="";
            if (!string.IsNullOrEmpty(Request.Form[optionCheck]))
            {
                roomQuantity = byte.Parse(Request.Form[optionCheck]);
                
                if (roomQuantity > 0 && optionCheck != optionTmp)
                {

                    if (temp.OptionCategoryID == 43 || temp.OptionCategoryID == 44)
                    {
                        checkTransfer = true;
                        transfer_detail = Request.Form["transfer_detail"];
                        nightCalculate = 1;
                    }
                    else {
                        nightCalculate = numNightRoom;
                    }
                    unit = roomQuantity;
                    //price=new CalculatePrice((int)reader[0],dateStart,dateEnd);
                    priceExtra = temp.Price;
                    priceTotal = priceTotal + temp.Price * unit;
                    //priceOwnTotal = priceOwnTotal + (double)temp.Price* (1 - ((double)extranet_comission / 100))

                    //double total_commission = (double)subTotalPrice * (1 - ((double)extranet_comission / 100));

                    //item.PriceSupplier = (decimal)total_commission;
                    total_commission = (double)temp.Price;
                    FrontBookingItem items = new FrontBookingItem
                    {
                        BookingID = bookingID,
                        BookingProductID = bookingProductID,
                        OptionID = temp.OptionID,
                        OptionTitle=temp.OptionTitle,
                        ConditionID = temp.ConditionID,
                        ConditionTitle = temp.ConditionTitle,
                        ConditionDetail = GetPolicyDetailXml(temp.ConditionID, dateStart),
                        Unit = unit,
                        PriceSupplier = (decimal)total_commission * unit * nightCalculate,
                        Price = temp.Price * unit * nightCalculate,
                        PromotionID = null,
                        PromotionTitle = "",
                        PromotionDetail = "",
                        Detail=transfer_detail,
                        //for change currency
                        PriceDisplay = temp.Price * unit * nightCalculate,
                        //
                        Breakfast = temp.Breakfast,
                        NumAdult = temp.NumAdult,
                        NumChildren = temp.NumChild,

                        Status = true
                    };


                    itemID = items.Insert(items);
                }
                
            }
            optionTmp = optionCheck;
        }
        }
        

        //Add Transfer

        //Check Gala Dinner
        


        DateTime dateCheck;
        int galaSet = 0;

        GalaDinner objGala = new GalaDinner(hotelID, dateStart, dateEnd);
        objGala.LangID = langID;
        List<GalaDinner> galaList = null;
        byte galaRequire = 0;

        if (!checkExtranet)
        {
            galaList = objGala.GetGala();
            foreach (GalaDinner gala in galaList)
            {
                if (gala.DefaultGala == 1)
                {
                    if (gala.RequireAdult)
                    {
                        galaRequire = adult;
                        
                        
                        FrontBookingItem items = new FrontBookingItem
                        {
                            BookingID = bookingID,
                            BookingProductID = bookingProductID,
                            OptionID = gala.OptionID,
                            OptionTitle=gala.Title,
                            ConditionID = gala.ConditionID,
                            ConditionTitle = gala.ConditionTitle,
                            ConditionDetail = GetPolicyDetailXml(item.ConditionID, dateStart),
                            Unit = galaRequire,
                            PriceSupplier = gala.RateOwn  * galaRequire,
                            Price = gala.Rate * galaRequire,
                            PromotionID = null,
                            PromotionTitle = "",
                            PromotionDetail = "",
                            //for change currency
                            PriceDisplay = gala.Rate * galaRequire,
                            //
                            NumAdult = gala.NumAdult,
                            NumChildren = gala.NumChild,
                            Status = true
                        };


                        itemID = items.Insert(items);
                    }

                    if (gala.RequireChild)
                    {
                        galaRequire = child;
                        if (galaRequire > 0)
                        {
                           
                            FrontBookingItem items = new FrontBookingItem
                            {
                                BookingID = bookingID,
                                BookingProductID = bookingProductID,
                                OptionID = gala.OptionID,
                                OptionTitle = gala.Title,
                                ConditionID = gala.ConditionID,
                                ConditionTitle = gala.ConditionTitle,
                                ConditionDetail = GetPolicyDetailXml(item.ConditionID, dateStart),
                                Unit = galaRequire,
                                PriceSupplier = gala.RateOwn * galaRequire,
                                Price = gala.Rate * galaRequire,
                                PromotionID = null,
                                PromotionTitle = "",
                                PromotionDetail = "",
                                //for change currency
                                PriceDisplay = gala.Rate * galaRequire,
                                //
                                Status = true
                            };


                            itemID = items.Insert(items);
                        }
                    }

                }
                else
                {
                    if (gala.RequireAdult)
                    {
                        galaSet = 0;
                        //for (int countNightGala = 0; countNightGala < numNightGala;countNightGala++ )
                        //{
                        //dateGalaCheck = item.DateUseEnd.AddDays(countNightGala);
                        for (int countNight = 0; countNight < numNightRoom; countNight++)
                        {
                            dateCheck = dateStart.AddDays(countNight);
                            if (dateCheck.CompareTo(gala.DateUseStart) >= 0 && gala.DateUseEnd.CompareTo(dateCheck) >= 0)
                            {
                                galaSet = galaSet + 1;
                            }
                        }
                        
                        FrontBookingItem items = new FrontBookingItem
                        {
                            BookingID = bookingID,
                            BookingProductID = bookingProductID,
                            OptionID = gala.OptionID,
                            OptionTitle = gala.Title,
                            ConditionID = gala.ConditionID,
                            ConditionTitle = gala.ConditionTitle,
                            ConditionDetail = GetPolicyDetailXml(item.ConditionID, dateStart),
                            Unit = Convert.ToByte(adult * galaSet),
                            PriceSupplier = gala.RateOwn * adult * galaSet,
                            Price = gala.Rate * adult * galaSet,
                            PromotionID = null,
                            PromotionTitle = "",
                            PromotionDetail = "",
                            //for change currency
                            PriceDisplay = gala.Rate * adult * galaSet,
                            //
                            Status = true
                        };


                        itemID = items.Insert(items);
                        //}
                    }

                    if (child > 0)
                    {
                        galaSet = 0;
                        if (gala.RequireChild)
                        {
                            //for (int countNightGala = 0; countNightGala < numNightGala;countNightGala++ )
                            //{
                            //dateGalaCheck = item.DateUseEnd.AddDays(countNightGala);
                            for (int countNight = 0; countNight < numNightRoom; countNight++)
                            {
                                dateCheck = dateStart.AddDays(countNight);
                                if (dateCheck.CompareTo(gala.DateUseStart) >= 0 && gala.DateUseEnd.CompareTo(dateCheck) >= 0)
                                {
                                    galaSet = galaSet + 1;
                                }
                            }

                           
                            FrontBookingItem items = new FrontBookingItem
                            {
                                BookingID = bookingID,
                                BookingProductID = bookingProductID,
                                OptionID = gala.OptionID,
                                OptionTitle = gala.Title,
                                ConditionID = gala.ConditionID,
                                ConditionTitle = gala.ConditionTitle,
                                ConditionDetail = GetPolicyDetailXml(item.ConditionID, dateStart),
                                Unit = Convert.ToByte(child * galaSet),
                                PriceSupplier = gala.RateOwn * child * galaSet,
                                Price = gala.Rate * child * galaSet,
                                PromotionID = null,
                                PromotionTitle = "",
                                PromotionDetail = "",
                                //for change currency
                                PriceDisplay = gala.Rate * adult * galaSet,
                                //
                                Status = true
                            };


                            itemID = items.Insert(items);
                            //}
                        }
                    }
                }



            }
        }
        else {
            galaList = objGala.GetGalaExtranet();
            if(!hasRoomSelect){
                galaList.Clear();
            }
            foreach (GalaDinner gala in galaList)
            {
                if (gala.DefaultGala == 1)
                {
                    if (gala.RequireAdult)
                    {
                        galaRequire = adult;
                        //Response.Write(gala.Rate + "--" + extranet_comission + "<br/>");
                        total_commission = (double)gala.Rate;
                        FrontBookingItem items = new FrontBookingItem
                        {
                            BookingID = bookingID,
                            BookingProductID = bookingProductID,
                            OptionID = gala.OptionID,
                            OptionTitle = gala.Title,
                            ConditionID = gala.ConditionID,
                            ConditionTitle = gala.ConditionTitle,
                            ConditionDetail = GetPolicyDetailXml(item.ConditionID, dateStart),
                            Unit = galaRequire,
                            PriceSupplier =  (decimal)total_commission * galaRequire,
                            Price = gala.Rate * galaRequire,
                            PromotionID = null,
                            PromotionTitle = "",
                            PromotionDetail = "",
                            //for change currency
                            PriceDisplay = gala.Rate * galaRequire,
                            //
                            NumAdult = gala.NumAdult,
                            NumChildren = gala.NumChild,
                            Status = true
                        };


                        itemID = items.Insert(items);
                    }

                    if (gala.RequireChild)
                    {
                        galaRequire = child;
                        if (galaRequire > 0)
                        {
                            total_commission = (double)gala.Rate;
                            FrontBookingItem items = new FrontBookingItem
                            {
                                BookingID = bookingID,
                                BookingProductID = bookingProductID,
                                OptionID = gala.OptionID,
                                OptionTitle = gala.Title,
                                ConditionID = gala.ConditionID,
                                ConditionTitle = gala.ConditionTitle,
                                ConditionDetail = GetPolicyDetailXml(item.ConditionID, dateStart),
                                Unit = galaRequire,
                                PriceSupplier = (decimal)total_commission * galaRequire,
                                Price = gala.Rate * galaRequire,
                                PromotionID = null,
                                PromotionTitle = "",
                                PromotionDetail = "",
                                //for change currency
                                PriceDisplay = gala.Rate * galaRequire,
                                //
                                Status = true
                            };


                            itemID = items.Insert(items);
                        }
                    }

                }
                else
                {
                    if (gala.RequireAdult)
                    {
                        galaSet = 0;
                        //for (int countNightGala = 0; countNightGala < numNightGala;countNightGala++ )
                        //{
                        //dateGalaCheck = item.DateUseEnd.AddDays(countNightGala);
                        for (int countNight = 0; countNight < numNightRoom; countNight++)
                        {
                            dateCheck = dateStart.AddDays(countNight);
                            if (dateCheck.CompareTo(gala.DateUseStart) >= 0 && gala.DateUseEnd.CompareTo(dateCheck) >= 0)
                            {
                                galaSet = galaSet + 1;
                            }
                        }
                        total_commission = (double)gala.Rate;
                        FrontBookingItem items = new FrontBookingItem
                        {
                            BookingID = bookingID,
                            BookingProductID = bookingProductID,
                            OptionID = gala.OptionID,
                            OptionTitle = gala.Title,
                            ConditionID = gala.ConditionID,
                            ConditionTitle = gala.ConditionTitle,
                            ConditionDetail = GetPolicyDetailXml(item.ConditionID, dateStart),
                            Unit = Convert.ToByte(adult * galaSet),
                            PriceSupplier = (decimal)total_commission * adult * galaSet,
                            Price = gala.Rate * adult * galaSet,
                            PromotionID = null,
                            PromotionTitle = "",
                            PromotionDetail = "",
                            //for change currency
                            PriceDisplay = gala.Rate * adult * galaSet,
                            //
                            Status = true
                        };


                        itemID = items.Insert(items);
                        //}
                    }

                    if (child > 0)
                    {
                        galaSet = 0;
                        if (gala.RequireChild)
                        {
                            //for (int countNightGala = 0; countNightGala < numNightGala;countNightGala++ )
                            //{
                            //dateGalaCheck = item.DateUseEnd.AddDays(countNightGala);
                            for (int countNight = 0; countNight < numNightRoom; countNight++)
                            {
                                dateCheck = dateStart.AddDays(countNight);
                                if (dateCheck.CompareTo(gala.DateUseStart) >= 0 && gala.DateUseEnd.CompareTo(dateCheck) >= 0)
                                {
                                    galaSet = galaSet + 1;
                                }
                            }

                            total_commission = (double)gala.Rate;
                            FrontBookingItem items = new FrontBookingItem
                            {
                                BookingID = bookingID,
                                BookingProductID = bookingProductID,
                                OptionID = gala.OptionID,
                                OptionTitle = gala.Title,
                                ConditionID = gala.ConditionID,
                                ConditionTitle = gala.ConditionTitle,
                                ConditionDetail = GetPolicyDetailXml(item.ConditionID, dateStart),
                                Unit = Convert.ToByte(child * galaSet),
                                PriceSupplier = (decimal)total_commission * child * galaSet,
                                Price = gala.Rate * child * galaSet,
                                PromotionID = null,
                                PromotionTitle = "",
                                PromotionDetail = "",
                                //for change currency
                                PriceDisplay = gala.Rate * adult * galaSet,
                                //
                                Status = true
                            };


                            itemID = items.Insert(items);
                            //}
                        }
                    }
                }



            }
        }
       

       
        
        //End Check Gala Dinner

        if(checkTransfer==false)
        {
            
            productList.GetTransferFromOtherProduct(hotelID);

            int productIDtmp = 0;
            String[] extraOption = new String[2];
            List<PriceBase> rateOtherProduct = productList.RateBase(3);

            foreach (PriceBase temp in rateOtherProduct)
            {
                transfer_detail = "";
                optionCheck = "ddPriceExtra_" + temp.ConditionID;

                if (!string.IsNullOrEmpty(Request.Form[optionCheck]))
                {
                    roomQuantity = byte.Parse(Request.Form[optionCheck]);

                    if (roomQuantity > 0 && optionCheck != optionTmp)
                    {
                        if (temp.ProductID != productIDtmp)
                        {// Add Booking Product
                            product.ProductID = temp.ProductID;
                            product.SupplierID = temp.SupplierPrice;
                            bookingProductID = product.Insert(product);
                            productIDtmp = temp.ProductID;
                        }
                        transfer_detail = Request.Form["transfer_detail"];
                        unit = roomQuantity;
                        //price=new CalculatePrice((int)reader[0],dateStart,dateEnd);
                        priceExtra = temp.Rate;
                        priceTotal = priceTotal + temp.Rate*unit;
                        priceOwnTotal = priceOwnTotal + temp.RateOwn;

                        item.BookingID = bookingID;
                        item.BookingProductID = bookingProductID;
                        item.OptionID = temp.OptionID;
                        item.OptionTitle = temp.OptionTitle;
                        item.ConditionID = temp.ConditionID;
                        item.ConditionTitle = temp.ConditionTitle;
                        item.ConditionDetail = GetPolicyDetailXml(temp.ConditionID, dateStart);
                        item.Unit = unit;
                        item.PriceSupplier = temp.RateOwn * unit;
                        item.Price = item.PriceSupplier;
                        item.PromotionID = null;
                        item.PromotionTitle = "";
                        item.PromotionDetail = "";
                        item.Detail = transfer_detail;
                        //for change currency
                        item.PriceDisplay = item.PriceSupplier;
                        //
                        item.Status = true;
                        if (Session["memberAccess"] != null)
                        {
                            item.MemberDetail = productExtraList.XmlMemberDiscountContent(item.ConditionID);
                        }
                        itemID = item.Insert(item);
                    }

                }
                optionTmp = optionCheck;
            }
            
        }
        //End 

        
         
        /*Don't forget insert requirement
         * 
         * 
         */

        //End Insert Booking Item

       

        BookingPhone BookingPhone = new BookingPhone();
        BookingPhone.CategoryID = 1;
        BookingPhone.BookingID = bookingID;
        BookingPhone.CodeCountry = phone_code;
        BookingPhone.CodeLocal = phone_code;
        BookingPhone.NumberPhone = phone;
        BookingPhone.Insert(BookingPhone);




        //double price_comission = ((Convert.ToDouble(priceTotal) * 0.97) - Convert.ToDouble(priceOwnTotal));
        
        //price_comission=((price_comission*40)/100);

        //string sqlInsertCommission = "insert into tbl_site_order(site_id,booking_id,main_site_id,comission,comission_percent,date_submit)";
        //sqlInsertCommission = sqlInsertCommission + "values(3333," + bookingID + ",1," + price_comission + ",40,"+DateTime.Now.Hotels2ThaiDateTime().Hotels2DateToSQlString()+")";

        //DataConnect objConn = new DataConnect();
        //objConn.ExecuteNonQuery(sqlInsertCommission);


        
        // Insert booking payment
        byte gatewayID = 0;

 
       
        decimal paymentTotal = productDeposit.GetPriceDeposit(bookingID);

        BookingMailEngine objMail = new BookingMailEngine(bookingID);
        
        //string sqlGateway = "select gateway_id from tbl_gateway where gateway_active=1";
        //gatewayID = (byte)objPayment.ExecuteScalar(sqlGateway);
        //objPayment.Close();
        //

        if (string.IsNullOrEmpty(Request.Form["bank_id"]))
        {
            gatewayID = 0;
        }else{
            gatewayID = byte.Parse(Request.Form["bank_id"]);
            
        }
        
        //gatewayID=0 is credit card
        string folderClient = string.Empty;
        string urlReturn = string.Empty;
        using (SqlConnection cn = new SqlConnection(connString))
        {
            string strCommand = "select folder,web_site_name,url_return";
            strCommand = strCommand + " from tbl_product_booking_engine";
            strCommand = strCommand + " where product_id=" + hotelID;
            SqlCommand cmd = new SqlCommand(strCommand, cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            folderClient=reader["web_site_name"]+"/"+reader["folder"];
            urlReturn = reader["url_return"].ToString();
        }

        int PaymentID = 0;
        string tracking = "";
        int PaymentBankID = 0;
        string mailStaffList = string.Empty;
        //Response.Write(Request.Form["cTransfer"]);


        objCom.loadComCalculate();

        
        

        if (Request.Form["bank_id"]=="0")
        {
            

            BookingPayment payment = new BookingPayment();
            payment.BookingID = bookingID;
            payment.PaymentCategoryID = 2;
            //don't forget select gateway from db
            payment.GatewayID = 1;
            //payment.Amount = paymentTotal;
            payment.Amount = decimal.Parse(Request.Form["sum_price"]);
            payment.Title = "Website";
            payment.Status = true;
            payment.DatePayment = DateTime.Now.Hotels2DateNow();
            payment.Insert(payment);
            
            
            objMail.SendMailBookingRecevied_BankTranfer();

            Response.Redirect("payment_transfer_thankyou.asp");
        }else{
            if (string.IsNullOrEmpty(Request.Form["cardnum"]))
            {

                if (manageID == 2)
                {
                    //Set Gateway for bht manage
                    //if (hotelID == 3449)ฅฅ
                    //{

                        GatewayMethod objGateway = new GatewayMethod(hotelID);
                        if (!objGateway.IsHighRiskCountryBYCountryID(country))
                        {
                            if (country == refCountry)
                            {

                                //Security Check Complete
                                gatewayID = 6;
                            }
                        }
                       
                        else
                        {
                            if (country == 208 && refCountry == 208)
                            {

                                gatewayID = 6;
                            }
                        }

                    //if hotel b2b go tp BLL 
                        if (objMail.cProductBookingEngine.Is_B2b)
                        {
                            gatewayID = 6;
                        }
                       

                    //}
                }

                BookingPayment payment = new BookingPayment();
                payment.BookingID = bookingID;
                payment.PaymentCategoryID = 1;
                //don't forget select gateway from db
                //payment.GatewayID = gatewayID;
                payment.GatewayID = gatewayID;

                payment.Amount = decimal.Parse(Request.Form["sum_price"]);
                payment.Title = "Website";
                payment.Status = true;
                payment.DatePayment = DateTime.Now.Hotels2DateNow();
                PaymentID = payment.Insert(payment);
                
                

                //Payment Process
                FrontPaymentMethod formPayment = new FrontPaymentMethod();
                //Response.Write(tracking);
                BookingPaymentDisplay bookingPaymentHotel = new BookingPaymentDisplay();


                PaymentBankID=bookingPaymentHotel.InsertBookingPaymentBank(PaymentID);

                PaymentInfo paymentInfo = new PaymentInfo(PaymentBankID);

                BookingStaff objStaff = new BookingStaff();
               // BookingMailEngine objMail = new BookingMailEngine(bookingID);

                
                foreach (string staffHotel in objStaff.GetStringEmailSendMail(bookingID))
                {
                   //Response.Write(staffHotel+"<br/>");
                    mailStaffList = mailStaffList + staffHotel+";";
                    
                }

                
                mailStaffList = mailStaffList.StringLeft(mailStaffList.Count()-1);


                //dont uncomment 

                objMail.SendMailBookingRecevied_Notification(mailStaffList);
                objMail.SendMailBookingRecevied();

                 
                PaymentInfo objPayment= paymentInfo.getPaymentInfo();

                
               

                if (objPayment.ManageID == 2)
                {
                    //Bht manage
                    Response.Write(formPayment.GetPaymentForm(objPayment));
                }
                else {
                    if (int.Parse(Request.Form["al"]) == 1)
                    {

                        string strFormDataForcyberSource = string.Empty;
                        string keyfield = "first_name,last_name,email,phone,req_address_1,req_address_2,user_ip_address,country,req_city,req_postal_code,sel_drop_state,user_ip_address,Session_current_id";

                        IDictionary<string, string> idicForm = new Dictionary<string, string>();

                        if (gatewayID == 15)
                        {
                            foreach (var key in Request.Form.AllKeys)
                            {
                                foreach (string keyname in keyfield.Split(','))
                                {

                                    if (key == keyname)
                                    {
                                        idicForm.Add(key, Request.Params[key]);
                                        break;
                                    }
                                    
                                }
                                //Response.Write("<input type=\"hidden\" id=\"" + key + "\" name=\"" + key + "\" value=\"" + Request.Params[key] + "\"/>\n");
                               
                            }
                        }


                        Response.Write(formPayment.GetPaymentForm(objPayment, idicForm));
                    }
                    else {
                        Response.Redirect(objPayment.WebsiteName+"/"+objPayment.FolderName.Trim()+"_thankyou.html");
                        //Response.Write(formPayment.GetPaymentForm(objPayment));
                    }
                }
                
                //Response.Redirect(urlReturn);
                //Response.Redirect("/vGenerator/test_payment_update.aspx?bid="+bookingID);
                //###
            }
            else
            {
                
                //Book now pay later

                BookingPayment payment = new BookingPayment();
                payment.BookingID = bookingID;
                payment.PaymentCategoryID = 3;
                payment.GatewayID = 3;
                //Amount for check authorize
                payment.Amount = decimal.Parse(Request.Form["sum_price"]);
                payment.Title = "Website";
                payment.Status = true;
                payment.DatePayment = DateTime.Now.Hotels2DateNow();
                PaymentID=payment.Insert(payment);

                BookingPaymentDisplay bookingPaymentHotel = new BookingPaymentDisplay();


                PaymentBankID = bookingPaymentHotel.InsertBookingPaymentBank(PaymentID);

                byte cardType = 1;
                PayLaterCard card = new PayLaterCard();
                string cardData = string.Empty;
                if (hotelID!=3465)
                {
                    string cardTypeName = Request.Form["cardType"];
                    switch (cardTypeName)
                    {
                        case "Visa":
                            cardType = 1;
                            break;

                        case "MasterCard":
                            cardType = 2;
                            break;
                        case "JCB":
                            cardType = 3;
                            break;
                    }

                    //if (Request.Form["cardType"] == "Visa")
                    //{
                    //    cardType = 1;
                    //}
                    //else {
                    //    cardType = 2;
                    //}

                    string cardNum = Request.Form["cardnum"].Trim();
                    string cardCvv = Request.Form["card_cvv"];
                    byte cardMonth = byte.Parse(Request.Form["card_month"]);
                    short cardYear = short.Parse(Request.Form["card_year"]);
                    string cardName = Request.Form["card_name"];
                    string bankName = Request.Form["bank_name"];
                    cardData = card.CombindDataCard(cardType, cardNum, cardCvv, cardMonth, cardYear, cardName, bankName, bookingID, bookingHotelID);

                }
                
                BookingStaff objStaff = new BookingStaff();
               // BookingMailEngine objMail = new BookingMailEngine(bookingID);

                foreach (string staffHotel in objStaff.GetStringEmailSendMail(bookingID))
                {
                    //Response.Write(staffHotel+"<br/>");
                    
                    mailStaffList = mailStaffList + staffHotel + ";";
                }
                mailStaffList = mailStaffList.StringLeft(mailStaffList.Count() - 1);
                objMail.SendMailBookingRecevied_Notification_offline(mailStaffList, cardData);
                objMail.SendMailBookingRecevied();
                //Response.Write(PaymentBankID);
                //Response.Flush();

                PaymentInfo paymentInfo = new PaymentInfo(PaymentBankID);
                PaymentInfo objPayment = paymentInfo.getPaymentInfo();

                Response.Redirect(objPayment.WebsiteName+"/"+objPayment.FolderName+"_thankyou.html");
                //string bodyForm = "<form name=\"thankyou\" action=\""+objPayment.WebsiteName+"/"+objPayment.FolderName+"_thankyou.html"+"\" method=\"post\">\n";
                //bodyForm = bodyForm + "<input type=\"hidden\" name=\"booking_id\" value=\"" + bookingID + "\" />\n";
                //bodyForm = bodyForm + "</form>\n";
                //string HeadForm = "<html><body onload=\"thankyou.submit()\">" + bodyForm + "</body></html>";

                //Response.Write(HeadForm);

            }
        }
        

        //payment.Status = false;

        // End insert booking payment
        

        
    }

    public string GetPromotionDetailXml(FrontPromotion promotion)
    {
        string xml = string.Empty;
        xml=xml+"<?xml version=\"1.0\" encoding=\"utf-8\"?>";
        xml=xml+"<Promotions>";
	    xml=xml+"<Promotion>";
	    xml=xml+"<PromotionID>"+promotion.PromotionID+"</PromotionID>";
	    xml=xml+"<CategoryID>"+promotion.CategoryID+"</CategoryID>";
	    xml=xml+"<ProductID>"+promotion.ProductID+"</ProductID>";
	    xml=xml+"<SupplierID>"+promotion.SupplierID+"</SupplierID>";
	    xml=xml+"<Title>"+promotion.Title+"</Title>";
        xml = xml + "<TitleLang>"+promotion.TitleLang+"</TitleLang>";
        xml = xml + "<DetailLang>"+promotion.DetailLang+"</DetailLang>";
        xml = xml + "<DateStart>" + promotion.DateStart + "</DateStart>";
        xml = xml + "<DateEnd>" + promotion.DateEnd + "</DateEnd>";
        xml = xml + "<DateUseStart>" + promotion.DateUseStart + "</DateUseStart>";
        xml = xml + "<DateUseEnd>" + promotion.DateUseEnd + "</DateUseEnd>";
        xml = xml + "<TimeStart>" + promotion.TimeStart + "</TimeStart>";
        xml = xml + "<TimeEnd>" + promotion.TimeEnd + "</TimeEnd>";
        xml = xml + "<QuantityMin>" + promotion.QuantityMin + "</QuantityMin>";
        xml = xml + "<DayMin>" + promotion.DayMin + "</DayMin>";
        xml = xml + "<DayAdvanceNum>" + promotion.DayAdvanceNum + "</DayAdvanceNum>";
        xml = xml + "<DateSubmit>" + promotion.DateSubmit + "</DateSubmit>";
        xml = xml + "<DayMon>" + promotion.DayMon + "</DayMon>";
        xml = xml + "<DayTue>" + promotion.DayTue + "</DayTue>";
        xml = xml + "<DayWed>" + promotion.DayWed + "</DayWed>";
        xml = xml + "<DayThu>" + promotion.DayThu + "</DayThu>";
        xml = xml + "<DayFri>" + promotion.DayFri + "</DayFri>";
        xml = xml + "<DaySat>" + promotion.DaySat + "</DaySat>";
        xml = xml + "<DaySun>" + promotion.DaySun + "</DaySun>";
        xml = xml + "<IsWeekendAll>" + promotion.IsHolidayCharge + "</IsWeekendAll>";
        xml = xml + "<IsHolidayCharge>" + promotion.IsHolidayCharge + "</IsHolidayCharge>";
        xml = xml + "<MaxSet>" + promotion.MaxSet + "</MaxSet>";
        xml = xml + "<IsBreakfast>" + promotion.IsBreakfast + "</IsBreakfast>";
        xml = xml + "<BreakfastCharge>" + promotion.BreakfastCharge + "</BreakfastCharge>";
        xml = xml + "<Status>" + promotion.Status + "</Status>";
        xml = xml + "<Comment>" + promotion.Comment + "</Comment>";

        using (SqlConnection cn = new SqlConnection(connString))
        {
            string sqlCommand = "select benefit_id,promotion_id,day_discount_start,day_discount_num,type_id,discount,priority from tbl_promotion_benefit where promotion_id=" + promotion.PromotionID;
            SqlCommand cmd = new SqlCommand(sqlCommand, cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                xml = xml + "<PromotionBenefits>";
                xml = xml + "<Benefit id=\"" + reader["benefit_id"] + "\">";
                xml = xml + "<PromotionID>" + reader["promotion_id"] + "</PromotionID>";
                xml = xml + "<DayDiscountStart>" + reader["day_discount_start"] + "</DayDiscountStart>";
                xml = xml + "<DayDiscountNum>" + reader["day_discount_num"] + "</DayDiscountNum>";
                xml = xml + "<TypeID>" + reader["type_id"] + "</TypeID>";
                xml = xml + "<Discount>" + reader["discount"] + "</Discount>";
                xml = xml + "<Priority>" + reader["priority"] + "</Priority>";
                xml = xml + "</Benefit>";
                xml = xml + "</PromotionBenefits>";
            }
        }
        
	
	    xml=xml+"</Promotion>";
        xml=xml+"</Promotions>";
    return xml;
    }
    public string GetPromotionDetailExtraXml(FrontPromotionExtranet promotion)
    {
        string xml = string.Empty;
        xml = xml + "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
        xml = xml + "<Promotions>";
        xml = xml + "<Promotion>";
        xml = xml + "<PromotionID>" + promotion.PromotionID + "</PromotionID>";
        xml = xml + "<CategoryID>" + promotion.CategoryID + "</CategoryID>";
        xml = xml + "<ProductID>" + promotion.ProductID + "</ProductID>";
        xml = xml + "<SupplierID>" + promotion.SupplierID + "</SupplierID>";
        xml = xml + "<Title>" + promotion.Title + "</Title>";
        xml = xml + "<TitleLang>" + promotion.TitleLang + "</TitleLang>";
        xml = xml + "<DetailLang>" + promotion.DetailLang + "</DetailLang>";
        xml = xml + "<DateStart>" + promotion.DateStart + "</DateStart>";
        xml = xml + "<DateEnd>" + promotion.DateEnd + "</DateEnd>";
        xml = xml + "<TimeStart>" + promotion.TimeStart + "</TimeStart>";
        xml = xml + "<TimeEnd>" + promotion.TimeEnd + "</TimeEnd>";
        xml = xml + "<QuantityMin>" + promotion.QuantityMin + "</QuantityMin>";
        xml = xml + "<DayMin>" + promotion.DayMin + "</DayMin>";
        xml = xml + "<DayAdvanceNum>" + promotion.DayAdvanceNum + "</DayAdvanceNum>";
        xml = xml + "<DateSubmit>" + promotion.DateSubmit + "</DateSubmit>";
        xml = xml + "<DayMon>" + promotion.DayMon + "</DayMon>";
        xml = xml + "<DayTue>" + promotion.DayTue + "</DayTue>";
        xml = xml + "<DayWed>" + promotion.DayWed + "</DayWed>";
        xml = xml + "<DayThu>" + promotion.DayThu + "</DayThu>";
        xml = xml + "<DayFri>" + promotion.DayFri + "</DayFri>";
        xml = xml + "<DaySat>" + promotion.DaySat + "</DaySat>";
        xml = xml + "<DaySun>" + promotion.DaySun + "</DaySun>";
        xml = xml + "<IsWeekendAll>" + promotion.IsHolidayCharge + "</IsWeekendAll>";
        xml = xml + "<IsHolidayCharge>" + promotion.IsHolidayCharge + "</IsHolidayCharge>";
        xml = xml + "<MaxSet>" + promotion.MaxSet + "</MaxSet>";
        xml = xml + "<IsBreakfast>" + promotion.IsBreakfast + "</IsBreakfast>";
        xml = xml + "<BreakfastCharge>" + promotion.BreakfastCharge + "</BreakfastCharge>";
        xml = xml + "<Status>" + promotion.Status + "</Status>";
        xml = xml + "<Comment>" + promotion.Comment + "</Comment>";
        xml = xml + "<IsCancellation>" + promotion.IsCancellation + "</IsCancellation>";
        using (SqlConnection cn = new SqlConnection(connString))
        {
            string sqlCommand = "select benefit_id,promotion_id,day_discount_start,day_discount_num,type_id,discount,priority from tbl_promotion_benefit_extra_net where promotion_id=" + promotion.PromotionID;
            SqlCommand cmd = new SqlCommand(sqlCommand, cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                xml = xml + "<PromotionBenefits>";
                xml = xml + "<Benefit id=\"" + reader["benefit_id"] + "\">";
                xml = xml + "<PromotionID>" + reader["promotion_id"] + "</PromotionID>";
                xml = xml + "<DayDiscountStart>" + reader["day_discount_start"] + "</DayDiscountStart>";
                xml = xml + "<DayDiscountNum>" + reader["day_discount_num"] + "</DayDiscountNum>";
                xml = xml + "<TypeID>" + reader["type_id"] + "</TypeID>";
                xml = xml + "<Discount>" + reader["discount"] + "</Discount>";
                xml = xml + "<Priority>" + reader["priority"] + "</Priority>";
                xml = xml + "</Benefit>";
                xml = xml + "</PromotionBenefits>";
            }
        }
        

        xml = xml + "</Promotion>";
        xml = xml + "</Promotions>";
        return xml;
    }

    public string GetPolicyDetailXml(int conditionID, DateTime DateStart)
    {
        string xml = string.Empty;

        BookingPolicy policies = new BookingPolicy();
        List<BookingPolicy> policyList = policies.GetPolicy(conditionID, DateStart);

        BookingPolicyCancel policyCancel = new BookingPolicyCancel();
        List<object> cancelList = new List<object>();

        xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n";
        xml = xml + "<Policies>\n";


        foreach (BookingPolicy item in policyList)
        {
            xml = xml + "<Policy id=\"" + item.PolicyID + "\">\n";
            xml = xml + "<CategoryID>" + item.PolicyCategory + "</CategoryID>\n";
            xml = xml + "<TypeID>" + item.PolicyType + "</TypeID>\n";
            xml = xml + "<LanguageID>1</LanguageID>\n";
            xml = xml + "<PolicyTitle>" + item.Title + "</PolicyTitle>\n";
            xml = xml + "<PolicyTitleDisplay>" + item.Title + "</PolicyTitleDisplay>\n";
            xml = xml + "<PolicyDetailDisplay>" + item.Detail + "</PolicyDetailDisplay>\n";
            xml = xml + "<PolicyDateStart>" + Utility.DateToStringYMD(item.DateStart) + "</PolicyDateStart>\n";
            xml = xml + "<PolicyDateEnd>" + Utility.DateToStringYMD(item.DateEnd) + "</PolicyDateEnd>\n";

            if (item.PolicyType == 1)
            {
                xml = xml + "<Cancellations>\n";
                cancelList = policyCancel.GetPolicyCancel(item.ConditionID, DateStart);
                foreach (BookingPolicyCancel cancel in cancelList)
                {
                    xml = xml + "<Cancellation>\n";
                    xml = xml + "<DayCancel>" + cancel.DayCancel + "</DayCancel>\n";

                    if (cancel.HotelChargePercent == 0)
                    {
                        xml = xml + "<HotelChargePercent/>\n";
                    }
                    else
                    {
                        xml = xml + "<HotelChargePercent>" + cancel.HotelChargePercent + "</HotelChargePercent>\n";
                    }

                    if (cancel.BHTChargePercent == 0)
                    {
                        xml = xml + "<BhtChargePercent/>\n";
                    }
                    else
                    {
                        xml = xml + "<BhtChargePercent>" + cancel.BHTChargePercent + "</BhtChargePercent>\n";
                    }

                    if (cancel.HotelChargeRoomNight == 0)
                    {
                        xml = xml + "<HotelChargeRoom/>\n";
                    }
                    else
                    {
                        xml = xml + "<HotelChargeRoom>" + cancel.HotelChargeRoomNight + "</HotelChargeRoom>\n";
                    }

                    if (cancel.BHTChargeRoomNight == 0)
                    {
                        xml = xml + "<BhtChargeRoom/>\n";
                    }
                    else
                    {
                        xml = xml + "<BhtChargeRoom>" + cancel.BHTChargeRoomNight + "</BhtChargeRoom>\n";
                    }
                    xml = xml + "<Detail>Detail Layout</Detail>\n";
                    xml = xml + "</Cancellation>\n";
                }
                xml = xml + "</Cancellations>\n";
            }
            xml = xml + "</Policy>\n";
        }

        xml = xml + "</Policies>\n";
        return xml;
    }

    public string GetPolicyDetailExtraXml(List<ProductPolicyExtranet> policyExtraList, List<CancellationExtranet> cancellationExtraList, int conditionID)
    {
        string xml = string.Empty;
        xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n";
        xml = xml + "<Policies>\n";
        foreach (ProductPolicyExtranet item in policyExtraList)
        {
            if (item.ConditionID == conditionID)
            {
                xml = xml + "<Policy id=\"" + item.ContentID + "\">\n";
                xml = xml + "<CategoryID>0</CategoryID>\n";
                xml = xml + "<TypeID>0</TypeID>\n";
                xml = xml + "<LanguageID>1</LanguageID>\n";
                xml = xml + "<PolicyTitle>" + item.Title + "</PolicyTitle>\n";
                xml = xml + "<PolicyTitleDisplay>" + item.Title + "</PolicyTitleDisplay>\n";
                xml = xml + "<PolicyDetailDisplay>" + item.Detail + "</PolicyDetailDisplay>\n";
                xml = xml + "<PolicyDateStart/>\n";
                xml = xml + "<PolicyDateEnd/>\n";
                xml = xml + "</Policy>\n";
            }
        }
        int countCancel = 1;
        if (cancellationExtraList.Count > 0)
        {
            bool hasCancellation = false;

            foreach (CancellationExtranet cancel in cancellationExtraList)
            {
                if (cancel.ConditionID == conditionID)
                {
                    if (countCancel == 1)
                    {
                        xml = xml + "<Policy id=\"" + cancel.CancelID + "\">\n";
                        xml = xml + "<CategoryID>2</CategoryID>\n";
                        xml = xml + "<TypeID>1</TypeID>\n";
                        xml = xml + "<LanguageID>1</LanguageID>\n";
                        xml = xml + "<PolicyTitle>Cancellation</PolicyTitle>\n";
                        xml = xml + "<PolicyTitleDisplay>Cancellation</PolicyTitleDisplay>\n";
                        xml = xml + "<PolicyDetailDisplay>Cancellation</PolicyDetailDisplay>\n";
                        xml = xml + "<PolicyDateStart/>\n";
                        xml = xml + "<PolicyDateEnd/>\n";
                        xml = xml + "<Cancellations>\n";
                        countCancel = countCancel + 1;
                        hasCancellation = true;
                    }
                    xml = xml + "<Cancellation>\n";
                    xml = xml + "<DayCancel>" + cancel.DayCancel + "</DayCancel>\n";
                    xml = xml + "<HotelChargePercent>" + cancel.ChargePercent + "</HotelChargePercent>\n";
                    xml = xml + "<BhtChargePercent/>\n";
                    xml = xml + "<HotelChargeRoom>" + cancel.ChargeNight + "</HotelChargeRoom>\n";
                    xml = xml + "<BhtChargeRoom/>\n";
                    xml = xml + "<Detail>Detail Layout</Detail>\n";
                    xml = xml + "</Cancellation>\n";
                    
                }

            }
            if (hasCancellation)
            {
                xml = xml + "</Cancellations>\n";
                xml = xml + "</Policy>\n";
            }
            
           
        }
        xml = xml + "</Policies>\n";
        return xml;
    }
    public string GetPolicyPromotionExtraXml(List<ProductPolicyExtranet> policyExtraList, IList<object> cancellationPromotionExtraList, int conditionID)
    {
        string xml = string.Empty;
        xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n";
        xml = xml + "<Policies>\n";
        foreach (ProductPolicyExtranet item in policyExtraList)
        {
            if (item.ConditionID == conditionID)
            {
                xml = xml + "<Policy id=\"" + item.ContentID + "\">\n";
                xml = xml + "<CategoryID>0</CategoryID>\n";
                xml = xml + "<TypeID>0</TypeID>\n";
                xml = xml + "<LanguageID>1</LanguageID>\n";
                xml = xml + "<PolicyTitle>" + item.Title + "</PolicyTitle>\n";
                xml = xml + "<PolicyTitleDisplay>" + item.Title + "</PolicyTitleDisplay>\n";
                xml = xml + "<PolicyDetailDisplay>" + item.Detail + "</PolicyDetailDisplay>\n";
                xml = xml + "<PolicyDateStart/>\n";
                xml = xml + "<PolicyDateEnd/>\n";
                xml = xml + "</Policy>\n";
            }
        }
        int countCancel = 1;
        if (cancellationPromotionExtraList.Count > 0)
        {
            foreach (FrontPromotionCancel cancel in cancellationPromotionExtraList)
            {
                
                    if (countCancel == 1)
                    {
                        xml = xml + "<Policy id=\"" + cancel.PromotionID + "\">\n";
                        xml = xml + "<CategoryID>2</CategoryID>\n";
                        xml = xml + "<TypeID>1</TypeID>\n";
                        xml = xml + "<LanguageID>1</LanguageID>\n";
                        xml = xml + "<PolicyTitle>Cancellation</PolicyTitle>\n";
                        xml = xml + "<PolicyTitleDisplay>Cancellation</PolicyTitleDisplay>\n";
                        xml = xml + "<PolicyDetailDisplay>Cancellation</PolicyDetailDisplay>\n";
                        xml = xml + "<PolicyDateStart/>\n";
                        xml = xml + "<PolicyDateEnd/>\n";
                        xml = xml + "<Cancellations>\n";
                        countCancel = countCancel + 1;
                    }
                    xml = xml + "<Cancellation>\n";
                    xml = xml + "<DayCancel>" + cancel.DayCancel + "</DayCancel>\n";
                    xml = xml + "<HotelChargePercent>" + cancel.ChangePercent + "</HotelChargePercent>\n";
                    xml = xml + "<BhtChargePercent/>\n";
                    xml = xml + "<HotelChargeRoom>" + cancel.ChangeNight + "</HotelChargeRoom>\n";
                    xml = xml + "<BhtChargeRoom/>\n";
                    xml = xml + "<Detail>Detail Layout</Detail>\n";
                    xml = xml + "</Cancellation>\n";

                }
            xml = xml + "</Cancellations>\n";
            xml = xml + "</Policy>\n";
        }
        xml = xml + "</Policies>\n";
        return xml;
    }
    //public string GetPolicyDetailExtraXml(List<ProductPolicyExtranet> policyExtraList,List<CancellationExtranet> cancellationExtraList,int conditionID, DateTime DateStart)
    //{
    //    string xml = string.Empty;
    //    xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n";
    //    xml = xml + "<Policies>\n";
    //    foreach(ProductPolicyExtranet item in policyExtraList)
    //    {
    //            if(item.ConditionID==conditionID){
    //                xml = xml + "<Policy id=\"" + item.ContentID+ "\">";
    //                xml = xml + "<LanguageID>" + item.LangID + "</LanguageID>";
    //                xml = xml + "<PolicyTitle>" + item.ConditionTitle + "</PolicyTitle>";
    //                xml = xml + "<PolicyTitleDisplay>" + item.Title + "</PolicyTitleDisplay>";
    //                xml = xml + "<PolicyDetailDisplay>" + item.Detail + "</PolicyDetailDisplay>";
    //                xml = xml + "</Policy>";
    //            }
    //    }
    //    xml = xml + "</Policies>\n";

    //    int countCancel = 1;

    //    if (cancellationExtraList.Count>0)
    //    {   

            
    //        foreach (CancellationExtranet cancel in cancellationExtraList)
    //        {
    //            if(cancel.ConditionID==conditionID)
    //            {
    //                if(countCancel==1)
    //                {
    //                    xml = xml + "<Cancellation>";
    //                    xml = xml + "<CancellationTitle>" + cancellationExtraList[0].Title + "</CancellationTitle>";
    //                    xml = xml + "<DateStart>" + Utility.DateToStringYMD(cancellationExtraList[0].DateStart) + "</DateStart>";
    //                    xml = xml + "<DateEnd>" + Utility.DateToStringYMD(cancellationExtraList[0].DateEnd) + "</DateEnd>";
    //                    countCancel = countCancel + 1;
    //                }
    //                xml = xml + "<CancellationRange>";
    //                xml = xml + "<DayCancel>" + cancellationExtraList[0].DayCancel + "</DayCancel>";
    //                xml = xml + "<ChargeNight>" + cancellationExtraList[0].ChargeNight + "</ChargeNight>";
    //                xml = xml + "<ChargePercent>" + cancellationExtraList[0].ChargePercent + "</ChargePercent>";
    //                xml = xml + "</CancellationRange>";
                    
    //            }

    //        }
    //        xml = xml + "</Cancellation>";
    //    }
        
    //    return xml;
    //}

}