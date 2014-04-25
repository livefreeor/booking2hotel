using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using Hotels2thailand.DataAccess;
using Hotels2thailand.ProductOption;
using System.IO;
/// <summary>
/// Summary description for FrontProductPriceExtranet
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class FrontProductPriceExtranet:Hotels2BaseClass
    {
        private int ProductID;
        private DateTime DateStart;
        private DateTime DateEnd;
        private List<ExtranetPriceBase> PriceList=new List<ExtranetPriceBase>();
        private List<PriceDay> PriceDayList=new List<PriceDay>();
        private List<PriceHoliday> PriceHolidayList=new List<PriceHoliday>();
        private PriceSupplement priceSupplement=new PriceSupplement();
        private short SupplierID = 0;
        private int RoomNight = 0;
        private string conditionGroup = string.Empty;
        private List<CancellationExtranet> cancellationList;
        private List<PromotionPrice> promotionList = new List<PromotionPrice>();
        private List<PromotionBenefitDetail> promotionBenefitList;
        private List<ProductPolicyExtranet> policyList;
        private List<FrontSupplementPriceQuantity> supplementList;
        private List<FrontProductOptionWeekDay> optionWeekendList = new List<FrontProductOptionWeekDay>();
        private FrontRatePlanExtranet ratePlan = new FrontRatePlanExtranet();
        private bool HasRate = false;
        private bool checkTransfer;
        private int ProductTransferID;
        private byte ProductCategoryID;
        private fnCurrency currency;
       // private bool hasPriceRecord = false;
        private int countConditionActive = 0;
        private string refUrl = string.Empty;
        private string refQuery = string.Empty;
        private byte refCountryID = 0;
        private double discountPrice = -1;
        private double discountPriceHidden = 0;
         //private bool promotionChargeABF = false;
        private decimal priceABFTemp = 0;
        private decimal priceABFTotal = 0;
        private byte _refCountry = 0;
        private byte _langID = 1;
        private string _ipaddress = string.Empty;
        private List<FrontMemberDiscount> memberDiscountList;
        private List<FrontMemberDiscount> memberDiscountBenefitList;

        

        public byte LangID
        {
            set { _langID = value; }
        }

        public string RefUrl
        {
            set
            {
                refUrl = value;
            }
        }
        public string RefQuery
        {
            set
            {
                refQuery = value;
            }
        }

        public double DiscountPrice
        {
            set
            {
                discountPrice = value;
            }
        }
        public double GetDiscountPrice
        {
            get
            {
                return discountPrice;
            }
        }
        public string ConditionGroup
        {

            get
            {
                return conditionGroup;
            }
        }

        public byte RefCountry
        {
            get
            {
                return _refCountry;
            }
        }
        
        public decimal PriceABF
        {
            get
            {
                return priceABFTotal;
            }
        }
        public string IPAddress
        {

            set
            {
                _ipaddress=value;
            }
        }
        //private bool _memberAuthen = false;
        public bool memberAuthen{get;set;}
        
        private decimal profitInclude = Convert.ToDecimal(1.11);
        private decimal vatInclude = Convert.ToDecimal(1.177);
        private Allotment objAllotment = null;

        public FrontProductPriceExtranet(int productID, byte categoryID, DateTime dateStart, DateTime dateEnd)
        {
            ProductID = productID;
            ProductCategoryID = categoryID;
            DateStart = dateStart;
            DateEnd = dateEnd;
            SupplierID = 0;
            RoomNight = dateEnd.Subtract(dateStart).Days;
        }

        public List<ExtranetPriceBase> GetPriceBase()
        {
            return PriceList;
        }

        public void LoadPrice()
        {
            PriceDiscountHidden objDiscount = new PriceDiscountHidden();
            //HttpContext.Current.Response.Write(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]+"hello");
            //HttpContext.Current.Response.Flush();
            if (!string.IsNullOrEmpty(_ipaddress))
            {
                if (_ipaddress != "::1")
                {
                    refCountryID = Convert.ToByte(IPtoCounrty.GetCountryID(_ipaddress));
                    
                }
                else
                {
                    refCountryID = 241;
                }
            }
            else {
                if (HttpContext.Current != null && HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != "::1")
                {
                    refCountryID = Convert.ToByte(IPtoCounrty.GetCountryID(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]));
                }
                else
                {
                    refCountryID = 241;
                }
            }

            _refCountry = refCountryID;
            
            if (discountPrice == -1)
            {
                
                string refCountry = refCountryID.ToString();
                
                //HttpContext.Current.Response.Write(refCountry);
                using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                {
                    string strCommand = string.Empty;
                    strCommand = strCommand + "select p.destination_id,";
                    strCommand = strCommand + " ISNULL((";
                    strCommand = strCommand + " select top 1 sl.location_id";
                    strCommand = strCommand + " from tbl_product_location spl,tbl_location sl";
                    strCommand = strCommand + " where spl.location_id=sl.location_id ";
                    strCommand = strCommand + " and spl.product_id=p.product_id and sl.status=1";
                    strCommand = strCommand + " ),'') as location_id";
                    strCommand = strCommand + " from tbl_product p";
                    strCommand = strCommand + " where p.product_id=" + ProductID;

                    SqlCommand cmd = new SqlCommand(strCommand, cn);
                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();


                    //HttpContext.Current.Response.End();
                    if (reader.Read())
                    {
                        if (!string.IsNullOrEmpty(reader["location_id"].ToString()))
                        {
                            //has location
                            discountPrice = objDiscount.PriceDiscount(this.refQuery, this.refUrl, reader["location_id"].ToString(), reader["destination_id"].ToString(), ProductCategoryID.ToString(), ProductID.ToString(), refCountry, DateStart, DateEnd);
                        }
                        else
                        {
                            discountPrice = objDiscount.PriceDiscount(this.refQuery, this.refUrl, "", reader["destination_id"].ToString(), ProductCategoryID.ToString(), ProductID.ToString(), refCountry, DateStart, DateEnd);
                        }
                        
                    }
                   
                }
            }
            priceSupplement = new PriceSupplement();
            priceSupplement.LoadPriceSupplementByProductID(ProductID);
            //ProductCategoryID = GetProductCategory(ProductID);
            currency = new fnCurrency();
            currency.GetCurrency();

            switch (ProductCategoryID)
            {
                case 29:
                    //Hotel
                    PriceList = RateBase(1);
                    objAllotment = new Allotment(ProductID);
                    break;
                case 31:
                    //Golf
                    PriceList = RateBase(3);
                    break;
                case 32:
                    //Golf
                    PriceList = RateBase(4);
                    break;
                case 34:
                    //Day Trip
                    PriceList = RateBase(5);
                    break;
                case 36:
                    //Water
                    PriceList = RateBase(6);
                    break;
                case 38:
                    //show
                    PriceList = RateBase(7);
                    break;
                case 39:
                    //Health
                    PriceList = RateBase(8);
                    break;
                case 40:
                    //Spa
                    PriceList = RateBase(9);
                    break;
            }

            if (HasRate)
            {
                ratePlan.LoadRatePlanByConditionGroup(conditionGroup, DateStart, DateEnd);
                //HttpContext.Current.Response.Write(ratePlan.countItem);
                PriceDayList = RateDayList(SupplierID);
                PriceHolidayList = RateHolidayList(SupplierID);
                PromotionPrice promotion = new PromotionPrice(this, DateStart, DateEnd, refCountryID);

                promotionList = promotion.GetExtraPromotionPriceList();

                promotionBenefitList = promotion.GetPromotionBenefitList();
                ProductPolicyExtranet policyExtra = new ProductPolicyExtranet();

                //ProductPolicy policy = new ProductPolicy();
                //policy.DateCheck = DateStart;
                //policyList = policy.GetProductPolicy(ProductID);
                //FrontCancellationPolicy cancalation = new FrontCancellationPolicy(ProductID, DateStart);
                //cancellationList = cancalation.LoadCancellationPolicyByCondition();
                CancellationExtranet cancelation = new CancellationExtranet(ProductID, DateStart);
                cancellationList = cancelation.GetCancellation();
                ProductPolicyExtranet policy = new ProductPolicyExtranet();
                policyList = policy.GetExtraPolicy(ProductID, 1);

                FrontProductOptionWeekDay objOptionWeekend = new FrontProductOptionWeekDay();
                optionWeekendList = objOptionWeekend.GetOptionWeekDayList(ProductID);

                //For member only
                FrontMemberDiscount objMemberDiscount=new FrontMemberDiscount();
                memberDiscountList = objMemberDiscount.GetMemberDiscountList(ProductID, DateStart,DateEnd);

                //HttpContext.Current.Response.Write(memberDiscountList[0].Discount);
                //HttpContext.Current.Response.End();

               // HttpContext.Current.Response.Write("<br/>load data<br/>");
                //if (memberDiscountList!=null)
                //{
                //    HttpContext.Current.Response.Write("Load data complete <br/>");
                //}
                memberDiscountBenefitList = objMemberDiscount.GetMemberDiscountBenefitList(ProductID, DateStart,DateEnd);
                //End For member only



                if (ProductCategoryID == 34 || ProductCategoryID == 36)
                {
                    FrontSupplementPriceQuantity objSupplement = new FrontSupplementPriceQuantity();
                    supplementList = objSupplement.LoadSupplementPriceByProductID(ProductID, DateStart);
                }

            }

        }


        public string BookingForm()
        {

            string displayForm = "";
            string RenderOptionForm = "";
            displayForm = RenderAnnoucement();
            if (HasRate)
            {
                RenderOptionForm = RenderList();
                if (countConditionActive != 0)
                {

                    displayForm = displayForm + RenderList();
                    displayForm = displayForm + RenderGalaDinner();
                    displayForm = displayForm + RenderExtraOption();

                    if (ProductCategoryID == 29 || ProductCategoryID == 32)
                    {
                        displayForm = displayForm + RenderGuestBox();
                    }
                    displayForm = displayForm + RenderHiddenField();
                    //displayForm = displayForm + "<div class=\"booknow\"><input type=\"button\" id=\"btnBooking\" value=\"Book Now\"></div>\n";
                    //displayForm = displayForm + "<div class=\"row_rate\"></div>";
                    //displayForm = displayForm + "<div class=\"incase\"><p>In case you find the cheaper rate within 24 hours after booking is completed,..... </p></div>";
                    //displayForm = displayForm + "<div class=\"take_time\">It only takes 2 minutes!</div>";
                    FrontPayLater payLater = new FrontPayLater();
                    string payLaterPlan = payLater.GetPayLaterPlanByDate(ProductID, DateStart);
                    if (string.IsNullOrEmpty(payLaterPlan))
                    {
                        displayForm = displayForm + "<div class=\"row_rate\"><a href=\"javascript:void(0)\" class=\"tooltip\"><img src=\"/theme_color/blue/images/icon/high-low.jpg\" class=\"lowrates\" title=\"Low Rates Guarantee\" /><span class=\"tooltip_content\">";
                        displayForm = displayForm + "<p><strong>Low Rate Guarantee</strong> - If you find a lower rate within 24 hours of booking, then, at a minimum, they will match that rate.</p><br />\n";
                        displayForm = displayForm + "<p><strong>High Security Website</strong> - Our payment system is certified by Thai Bank in usage of security version SSL 256 Bit encryption , the most modern technological security system using nowadays websites in the world.</p>\n";

                    }
                    else
                    {
                        displayForm = displayForm + "<div class=\"row_rate\"><a href=\"javascript:void(0)\" class=\"tooltip\"><img src=\"/theme_color/blue/images/icon/high-low_paylater.jpg\" class=\"lowrates\" title=\"Low Rates Guarantee\" /><span class=\"tooltip_content\">";
                        //displayForm = displayForm + "<p>"+payLaterPlan+"</p>\n";
                        displayForm = displayForm + "<p><strong>Book Now Pay Later</strong> - Book Today, Pay Later Just $1 to secure your rooms / products today and pay the rest at date that is closure to check in</p><br/>";
                        displayForm = displayForm + "<p><strong>Low Rate Guarantee</strong> - If you find a lower rate within 24 hours of booking, then, at a minimum, they will match that rate.</p><br/>";
                        displayForm = displayForm + "<p><strong>High Security Website</strong> - Our payment system is certified by Thai Bank in usage of security version SSL 256 Bit encryption , the most modern technological security system using nowadays websites in the world.</p>";


                    }


                    displayForm = displayForm + "</span></a></div>\n";
                    displayForm = displayForm + "<div class=\"incase\"><p>Hotels2thailand.com is legally allowed to sell tourism products on the internet, authorized from Tourism Authority of Thailand (TAT)</p></div>\n";
                    displayForm = displayForm + "<div class=\"take_time\">It only takes 2 minutes!</div>\n";
                    displayForm = displayForm + "<div class=\"booknow\"><a href=\"#\" id=\"btnBooking\"></a></div>\n";
                    displayForm = displayForm + "<br class=\"clear-all\" /><br /><br />\n";
                }
                else
                {
                    displayForm = displayForm + "<br /><br /><div style=\"border:2px solid #ffcc66;margin:7px;padding:10px;\">\n";
                    displayForm = displayForm + "<font color=\"red\">\n";
                    displayForm = displayForm + "Sorry. Rate for selected period is not available. Please kindly contact to <a href=\"mailto:reservation@hotels2thailand.com\">reservation@hotels2thailand.com</a>\n";
                    displayForm = displayForm + "</font>\n";
                    displayForm = displayForm + "</div>\n";
                }

            }
            else
            {
                displayForm = displayForm + "<br /><br /><div style=\"border:2px solid #ffcc66;margin:7px;padding:10px;\">\n";
                displayForm = displayForm + "<font color=\"red\">\n";
                displayForm = displayForm + "Sorry. Rate for selected period is not available. Please kindly contact to <a href=\"mailto:reservation@hotels2thailand.com\">reservation@hotels2thailand.com</a>\n";
                displayForm = displayForm + "</font>\n";
                displayForm = displayForm + "</div>\n";
                //HttpContext.Current.Response.Write("Sorry we have not now");
            }

            return displayForm;
        }

        public List<ExtranetPriceBase> RateBase(int optionCate)
        {

            ExtranetPriceBase objPriceBase = new ExtranetPriceBase();
            objPriceBase.LangID = _langID;
            int productCheck = ProductID;
            if (optionCate == 3)
            {
                productCheck = ProductTransferID;
            }

            List<ExtranetPriceBase> PriceList = objPriceBase.GetPriceBase(productCheck, optionCate, ProductTransferID, DateStart, DateEnd);
            if (((optionCate == 1) || (optionCate == 4) || (optionCate == 5) || (optionCate == 6) || (optionCate == 7) || (optionCate == 8) || (optionCate == 9)) && PriceList.Count != 0)
            {
                HasRate = true;
                discountPriceHidden = discountPrice;
            }
            conditionGroup = objPriceBase.GetConditionGroup();

            if (!string.IsNullOrEmpty(conditionGroup))
            {
                if (optionCate != 3)
                {
                    SupplierID = PriceList[0].SupplierPrice;
                }
                conditionGroup = conditionGroup.Substring(0, conditionGroup.Length - 1);
            }

            if (optionCate == 2 || optionCate == 3)
            {
                discountPrice = 0;
            }

            return PriceList;
        }

        public List<PriceDay> RateDayList(int supplierID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "fr_supplement_weekend '" + supplierID + "'," + DateStart.Hotels2DateToSQlString() + "," + DateEnd.Hotels2DateToSQlString();
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<PriceDay> priceDayList = new List<PriceDay>();
                while (reader.Read())
                {
                    priceDayList.Add(new PriceDay
                    {
                        OptionID = (int)reader["option_id"],
                        DateStart = (DateTime)reader["date_start"],
                        DateEnd = (DateTime)reader["date_end"],
                        DateSun = (decimal)reader["day_sun"],
                        DateMon = (decimal)reader["day_mon"],
                        DateTue = (decimal)reader["day_tue"],
                        DateWed = (decimal)reader["day_wed"],
                        DateThu = (decimal)reader["day_thu"],
                        DateFri = (decimal)reader["day_fri"],
                        DateSat = (decimal)reader["day_sat"]
                    });
                }
                return priceDayList;
            }
            
        }

        public List<PriceHoliday> RateHolidayList(int supplierID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "fr_supplement_holiday '" + supplierID + "'," + DateStart.Hotels2DateToSQlString() + "," + DateEnd.Hotels2DateToSQlString();
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<PriceHoliday> priceHolidayList = new List<PriceHoliday>();
                while (reader.Read())
                {
                    priceHolidayList.Add(new PriceHoliday
                    {
                        OptionID = (int)reader["option_id"],
                        DateSupplement = (DateTime)reader["date_supplement"],
                        Supplement = (decimal)reader["supplement"]
                    });
                }
                return priceHolidayList;
            }
            
        }

        public string RenderHiddenField()
        {
            string displayHidden = "<input type=\"hidden\" name=\"sid\" value=\"" + SupplierID + "\" />\n";
            displayHidden = displayHidden + "<input type=\"hidden\" id=\"discount\" name=\"discount\" value=\"" + discountPriceHidden + "\" />\n";
            displayHidden = displayHidden + "<input type=\"hidden\" name=\"hotel_id\" value=\"" + ProductID + "\" />\n";
            displayHidden = displayHidden + "<input type=\"hidden\" name=\"cat_id\" value=\"" + ProductCategoryID + "\" />\n";
            displayHidden = displayHidden + "<input type=\"hidden\" name=\"date_start\" value=\"" + DateStart.ToString("yyyy-MM-dd") + "\" />\n";
            if (ProductCategoryID == 29)
            {
                displayHidden = displayHidden + "<input type=\"hidden\" name=\"date_end\" value=\"" + DateEnd.ToString("yyyy-MM-dd") + "\" />\n<br>";
            }
            displayHidden = displayHidden + "<input type=\"hidden\" id=\"currencyDisplay\" value=\"" + currency.CurrencyID + "\" />\n";
            return displayHidden;
        }

        public string GendropDownNumber(string ddName, int numStart, int numEnd, int numDefault)
        {
            string ddDisplay = "<select name=\"" + ddName + "\" id=\"" + ddName + "\">\n";
            for (int count = numStart; count <= numEnd; count++)
            {
                if (count != numDefault)
                {
                    ddDisplay = ddDisplay + "<option value=\"" + count + "\">" + count + "</option>\n";
                }
                else
                {
                    ddDisplay = ddDisplay + "<option value=\"" + count + "\" selected=\"selected\">" + count + "</option>\n";
                }
            }

            ddDisplay = ddDisplay + "</select>\n";
            return ddDisplay;
        }
        public string RenderGuestBox()
        {
            //string displayGuestBox = "<br><strong>Select Total Number of Guests:</strong><br><br><table id=\"guestBox\" cellspacing=\"0\" padding=\"7\">\n";
            //displayGuestBox = displayGuestBox + "<tr><td colspan=\"2\"></td></tr>";
            //displayGuestBox = displayGuestBox + "<tr><td width=\"100\">Adult</td><td>" + GendropDownNumber("adult", 1, 20, 1) + "</td></tr>\n";
            //displayGuestBox = displayGuestBox + "<tr><td>Children</td><td>" + GendropDownNumber("child", 0, 20, 0) + "</td></tr>\n";
            //displayGuestBox = displayGuestBox + "</table>\n";

            string displayGuestBox = "<br /><br /><table width=\"753\" cellspacing=\"1\" cellpadding=\"3\" class=\"number_guest\">\n";



            if (ProductCategoryID == 32)
            {
                displayGuestBox = displayGuestBox + "<tr><td class=\"extra\" align=\"left\" colspan=\"2\"><strong>Select Tee of time</strong> </td></tr>\n";
                displayGuestBox = displayGuestBox + "<tr><td width=\"736\" align=\"left\">Hour: " + DropdownUtility.TeeOffTime("tee_hour", 0, 23, 0, 1) + " Min: " + DropdownUtility.TeeOffTime("tee_min", 0, 59, 0, 2) + "\n";
                displayGuestBox = displayGuestBox + "</td></tr>\n";
            }

            if (ProductCategoryID == 29)
            {
                displayGuestBox = displayGuestBox + "<tr><td class=\"extra\" align=\"left\" colspan=\"2\"><strong>Select No. of Guest</strong> </td></tr>\n";
                displayGuestBox = displayGuestBox + "<tr><td width=\"100\">Adult</td><td width=\"636\" align=\"left\">" + GendropDownNumber("adult", 1, 20, 1) + "\n";
                displayGuestBox = displayGuestBox + "</td></tr>\n";
                displayGuestBox = displayGuestBox + "<tr class=\"bg_blue\"><td width=\"100\">Children</td><td align=\"left\">" + GendropDownNumber("child", 0, 20, 0) + "\n";
                displayGuestBox = displayGuestBox + "</td></tr>\n";
            }
            displayGuestBox = displayGuestBox + "</table>\n";
            return displayGuestBox;
        }
        public int CountPromotion(int conditionID)
        {
            int countItem = 0;

            foreach (PromotionPrice item in promotionList)
            {
                //HttpContext.Current.Response.Write(item.ConditionID);
                if (item.ConditionID == conditionID)
                {
                    countItem = countItem + 1;
                }
            }
            //HttpContext.Current.Response.Write(conditionID+"--"+countItem+"<br>");
            return countItem;
        }
        public List<int> GetListPromotionID()
        {
            //LoadPrice();
            int optionTmp = 0;
            int conditionTmp = 0;

            List<int> lstPromotion = new List<int>();
            lstPromotion.Add(0);
            if (PriceList.Count > 0)
            {
                foreach (ExtranetPriceBase item in PriceList)
                {

                    if (optionTmp != item.OptionID)
                    {

                        if (CountPromotion(item.ConditionID) > 0)
                        {

                            foreach (PromotionPrice promotion in promotionList)
                            {

                                if (item.ConditionID == promotion.ConditionID)
                                {
                                    if (!lstPromotion.Contains(promotion.PromotionID))
                                    {
                                        lstPromotion.Add(promotion.PromotionID);
                                    }

                                }
                            }

                        }

                    }
                    else
                    {
                        if (item.ConditionID != conditionTmp)
                        {
                            if (CountPromotion(item.ConditionID) > 0)
                            {
                                foreach (PromotionPrice promotion in promotionList)
                                {
                                    if (item.ConditionID == promotion.ConditionID)
                                    {
                                        if (!lstPromotion.Contains(promotion.PromotionID))
                                        {
                                            lstPromotion.Add(promotion.PromotionID);
                                        }
                                    }
                                }

                            }

                        }
                    }
                    optionTmp = item.OptionID;
                    conditionTmp = item.ConditionID;
                }
            }
            return lstPromotion;
        }

        private string policyDefault = string.Empty;

        public string GetPolicyContent(ExtranetPriceBase price)
        {

            string result = string.Empty;
            if (price.IsBreakfast > 0)
            {
                result = result + "<strong>Breakfast: Included</strong>";
            }
            string ConditionTitle = string.Empty;
            foreach (ProductPolicyExtranet item in policyList)
            {
                if (item.ConditionID == price.ConditionID)
                {
                    if (item.ConditionNameId == 2)
                        ConditionTitle = item.ConditionTitle;

                    result = result + "<div class=\"policy_list\"><strong>" + item.ContentTitle + ": </strong><span>" + item.Detail + "</span></div>";
                }

            }

            result = result + "<div class=\"policy_list\"><strong>Cancellation Policy</strong><br/>" + ConditionTitle +"<br/>" + RenderCancellation(price.ConditionID) + "</div>";
            result = result + "<div class=\"policy_list\"><strong>Help the children: </strong>You can help the children. Every booking will be sharing to non-profit children organization in Thailand.</div>";
            return result;
        }

        public string GetPolicyContentPromotion(ExtranetPriceBase price,int PromotionID)
        {

            string result = string.Empty;
            if (price.IsBreakfast > 0)
            {
                result = result + "<strong>Breakfast: Included</strong>";
            }
            
            foreach (ProductPolicyExtranet item in policyList)
            {
                if (item.ConditionID == price.ConditionID)
                {

                    result = result + "<div class=\"policy_list\"><strong>" + item.ContentTitle + ": </strong><span>" + item.Detail + "</span></div>";
                }

            }
            result = result + "<div class=\"policy_list\"><strong>Cancellation Policy</strong><br/>" + RenderCancellationPromotion(PromotionID)+"</div>";
            result = result + "<div class=\"policy_list\"><strong>Help the children: </strong>You can help the children. Every booking will be sharing to non-profit children organization in Thailand.</div>";
            return result;
        }

        public string RenderCancellationPromotion(int PromotionID)
        {
            string result = string.Empty;
            FrontPromotionCancel objCancel = new FrontPromotionCancel();
            IList<object> cancelList = objCancel.GetPromotionCancelByPromotionID(PromotionID);
            int lastDayCancel=0;
            if (cancelList.Count == 1)
            {
                FrontPromotionCancel nonRefundCancel=(FrontPromotionCancel)cancelList[0];
                if (nonRefundCancel.DayCancel == 100)
                {
                    result = result + "Non Refundable<br/>";
                }
                else {
                    result = result + Hotels2String.CancellationGenerateWording(true,nonRefundCancel.DayCancel, 0, 0, nonRefundCancel.ChangePercent, nonRefundCancel.ChangeNight) + " a<br/>";
                }
            }
            else if (cancelList.Count > 1)
            {
                foreach (FrontPromotionCancel item in cancelList)
                {
                   result = result + Hotels2String.CancellationGenerateWording(true,item.DayCancel, 0, 0, item.ChangePercent, item.ChangeNight) + "<br/>";
                   lastDayCancel = item.DayCancel;
                }
                result = result + "More than " + lastDayCancel + " Days prior to arrival There is a 7% administration charge<br/>";
            }
            
            return result;
        }

        public string RenderCancellation(int ConditionID)
        {
            string cancelDisplay = "";
            string[,] ResultProductType = new string[10, 3];
            int lastDayCancel = 0;

            List<CancellationExtranet> cancalPolicy = new List<CancellationExtranet>();

            foreach (CancellationExtranet item in cancellationList)
            {
                if (item.ConditionID == ConditionID)
                {
                    cancalPolicy.Add(new CancellationExtranet
                    {
                        CancelID = item.CancelID,
                        ConditionID =item.ConditionID,
                        DateStart =item.DateStart,
                        DateEnd = item.DateEnd,
                        Title = item.Title,
                        DayCancel = item.DayCancel,
                        ChargePercent = item.ChargePercent,
                        ChargeNight = item.ChargeNight
                    });

                }

            }

            //cancelDisplay = cancelDisplay + "<br/><strong>Cancellation Policy</strong><br/>";
            if (cancalPolicy.Count>0)
            {
                if (cancalPolicy.Count == 1)
                {
                    if (cancalPolicy[0].DayCancel == 100)
                    {
                        cancelDisplay = cancelDisplay + "Non Refundable <br/>";
                    }
                    else {
                        cancelDisplay = cancelDisplay + Hotels2String.CancellationGenerateWording(true,cancalPolicy[0].DayCancel, 0, 0, cancalPolicy[0].ChargePercent, cancalPolicy[0].ChargeNight) + " <br/>";
                    }
                }
                else { 
                // More than 1 cancellation
                    for (int countIndex = 0; countIndex < cancalPolicy.Count; countIndex++)
                    {
                            cancelDisplay = cancelDisplay + Hotels2String.CancellationGenerateWording(true,cancalPolicy[countIndex].DayCancel, 0, 0, cancalPolicy[countIndex].ChargePercent, cancalPolicy[countIndex].ChargeNight) + " <br/>";
                            lastDayCancel = cancalPolicy[countIndex].DayCancel;
                    }
                    cancelDisplay = cancelDisplay + "More than " + lastDayCancel + " Days prior to arrival There is a 7% administration charge <br/>";
                }

            }
            return cancelDisplay;

        }
        //Cancellation

        //-------------
        private string GenDropDownPrice(string ddPrice, string ddPriceName, int conditionID, decimal price, int roomQuanTity, short optionCatID)
        {
            string DropdownPrice;
            decimal priceDisplay = 0;
            string priceDisplayFormat = string.Empty;
            string className = string.Empty;

            switch (optionCatID)
            {

                case 38:
                case 48:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                    className = "class=\"ddPrice\"";
                    break;
                case 39:
                    className = "class=\"ddPriceExtraBed\"";
                    break;
                case 43:
                    className = "class=\"ddTransfer\"";
                    break;
                case 44:
                    className = "class=\"ddTransfer\"";
                    break;
            }

            DropdownPrice = "<select name=\"" + ddPrice + "\" " + className + " runat=\"server\">\n";
            for (int count = 0; count <= roomQuanTity; count++)
            {
                //if(PriceSupplementList.Count>0){
                //    priceDisplay =priceSupplement.GetPriceSupplement(price, conditionID, count);
                //}else{
                //    priceDisplay = price * count;
                //}
                priceDisplay = priceSupplement.GetPriceSupplement(DateStart, price, conditionID, count);
                //
                if (currency.CurrencyID == 25)
                {
                    priceDisplayFormat = priceDisplay.ToString("#,###");
                }
                else
                {
                    priceDisplayFormat = priceDisplay.ToString("#,###.##");
                }
                if (priceDisplayFormat == "")
                {
                    priceDisplayFormat = "0";
                }
                //
                //DropdownPrice = DropdownPrice + "<option value=\"" + ddPriceName + "_" + count + "\">" + count + " (" + priceDisplayFormat + ")</option>\n";
                DropdownPrice = DropdownPrice + "<option value=\"" + ddPriceName + "_" + count + "\">" + count + "</option>\n";
            }
            DropdownPrice = DropdownPrice + "</select>";
            return DropdownPrice;
        }
        public string RenderConditionItem(ExtranetPriceBase price, bool firstRow)
        {
            string conditionItem = "";

            decimal priceDisplay = 0;
            string priceDisplayFormat = string.Empty;
            string divPolicy = string.Empty;
            string nameValue = price.NumAdult + "_" + price.NumChild + "_" + price.NumExtra;

            string policyDisplay = string.Empty;
            string promotionTitle = string.Empty;
            string promotionDetail = string.Empty;
            string adultMax = string.Empty;
            string textAvailable = string.Empty;

            foreach (PromotionPrice item in promotionList)
            {
                if (item.ConditionID == price.ConditionID)
                {
                    
                    textAvailable = string.Empty;
                    if (!string.IsNullOrEmpty(objAllotment.CheckAllotAvaliable_Cutoff(price.SupplierPrice, price.OptionID, 1, DateStart, DateEnd)))
                    {
                        textAvailable = "<br/><span class=\"avai_14\">Available Now!</span>";
                    }

                    priceDisplay = CalculateAll(item.ConditionID, item.OptionID, item.PromotionID).PriceExcludeABF / Convert.ToDecimal(currency.CurrencyPrefix);
                    
                    //HttpContext.Current.Response.Write("bbb<br>");
                    if (currency.CurrencyID == 25)
                    {
                        priceDisplayFormat = (priceDisplay / RoomNight).ToString("#,###");
                    }
                    else
                    {
                        priceDisplayFormat = (priceDisplay / RoomNight).ToString("#,###.##");
                    }
                    
                    promotionTitle = CheckPromotionAcceptTitle(item.PromotionID);
                    //HttpContext.Current.Response.Write(promotionTitle + "<br/>");
                    divPolicy = "<div style=\"display:none;\" class=\"tooltip_content\">" + GetPolicyContent(price) + "</div>";

                    if (promotionTitle != "")
                    {
                        promotionDetail = string.Empty;
                        if (!string.IsNullOrEmpty(item.Detail))
                        {
                            //promotionDetail = "<br/>a"+Hotels2XMLContent.Hotels2XMlReaderPomotionDetail(item.Detail)+"a";
                            promotionDetail = "<p class=\"promotion_benefit_detail\">";
                            promotionDetail = promotionDetail + Hotels2XMLContent.Hotels2XMLReaderPromotionDetailExtranet_Front(item.Detail);
                            promotionDetail = promotionDetail + "</p>";
                        }
                        else {
                            promotionDetail = "<br/><br/>";
                        }

                        if (item.IsCancellation)
                        {
                            divPolicy = "<div style=\"display:none;\" class=\"tooltip_content\"><img src=\"/images/ico_special_offer.jpg\">&nbsp;<strong>Promotion:</strong><br/><span class=\"rate_promotion_detail\">" + item.Title + "</span>" + promotionDetail  + GetPolicyContentPromotion(price, item.PromotionID) + "</div>";
                        }
                        else {
                            divPolicy = "<div style=\"display:none;\" class=\"tooltip_content\"><img src=\"/images/ico_special_offer.jpg\">&nbsp;<strong>Promotion:</strong><br/><span class=\"rate_promotion_detail\">" + item.Title + "</span>" + promotionDetail  + GetPolicyContent(price) + "</div>";
                        }
                        //divPolicy = "<div style=\"display:none;\" class=\"tooltip_content\">" + GetPolicyContent(price) + "<br/><br/><img src=\"/images/ico_special_offer.jpg\">&nbsp;<strong>Promotion:</strong><br/><span class=\"rate_promotion_detail\">" + item.Title + "</span><br/>" + Hotels2XMLContent.Hotels2XMlReader(item.Detail) + "</div>";
                        
                        promotionTitle = "Special Offer";
                    }
                    policyDisplay = GenCondtionPolicy(price, promotionTitle, 1);

                    if (item.NumAdult > 3)
                    {
                        adultMax = "<img src=\"/theme_color/blue/images/icon/adult.png\" align=\"absmiddle\" />x" + item.NumAdult;
                        
                    }
                    else
                    {
                        adultMax = "<img src=\"/theme_color/blue/images/icon/ico_adult_" + item.NumAdult + ".png\" align=\"absmiddle\" />";
                    }

                    if (firstRow)
                    {
                        if (ProductCategoryID == 29)
                        {
                            //conditionItem = conditionItem + "<td><a href=\"javascript:void(0)\" class=\"tooltip\">" + adultMax + "<span class=\"tooltip_content\">Standard occupancy: " + item.NumAdult + "</span></a></td><td><a href=\"javascript:void(0)\" id=\"" + item.ConditionID + "\"  class=\"tooltip\">" + policyDisplay + divPolicy + "</a></td><td class=\"rate_green\"><span class=\"otp_" + item.OptionID + "\">" + currency.CurrencyCode + " " + priceDisplayFormat + textAvailable + "</span></td><td class=\"center\">" + GenDropDownPrice("ddPrice", item.ConditionID + "_" + item.OptionID + "_" + item.PromotionID + "_" + nameValue, item.ConditionID, priceDisplay, 20, item.OptionCategoryID) + "</td></tr>\n";
                            conditionItem = conditionItem + "<td><a href=\"javascript:void(0)\" class=\"tooltip\">" + adultMax + "<span class=\"tooltip_content\">Standard occupancy: " + item.NumAdult + "</span></a></td><td><a href=\"javascript:void(0)\" id=\"" + item.ConditionID + "\"  class=\"tooltip\">" + policyDisplay + divPolicy + "</a></td><td class=\"rate_green\">" + currency.CurrencyCode + " " + priceDisplayFormat + textAvailable + "</td><td class=\"center\">" + GenDropDownPrice("ddPrice", item.ConditionID + "_" + item.OptionID + "_" + item.PromotionID + "_" + nameValue, item.ConditionID, priceDisplay, 20, item.OptionCategoryID) + "</td></tr>\n";
                        }
                        else
                        {
                            conditionItem = conditionItem + "<td><a href=\"javascript:void(0)\" id=\"" + item.ConditionID + "\"  class=\"tooltip\">" + policyDisplay + divPolicy + "</a></td><td class=\"rate_green\">" + currency.CurrencyCode + " " + priceDisplayFormat + textAvailable + "</td><td class=\"center\">" + GenDropDownPrice("ddPrice", item.ConditionID + "_" + item.OptionID + "_" + item.PromotionID + "_" + nameValue, item.ConditionID, priceDisplay, 20, item.OptionCategoryID) + "</td></tr>\n";
                        }

                        firstRow = false;

                    }
                    else
                    {

                        if (ProductCategoryID == 29)
                        {
                            conditionItem = conditionItem + "<tr ###swaprow###><td><a href=\"javascript:void(0)\" class=\"tooltip\">" + adultMax + "<span class=\"tooltip_content\">Standard occupancy: " + item.NumAdult + "</span></a></td><td><a href=\"javascript:void(0)\" id=\"" + item.ConditionID + "\" class=\"tooltip\">" + policyDisplay + divPolicy + "</a></td><td class=\"rate_green\">" + currency.CurrencyCode + " " + priceDisplayFormat + textAvailable + "</td><td class=\"center\">" + GenDropDownPrice("ddPrice", item.ConditionID + "_" + item.OptionID + "_" + item.PromotionID + "_" + nameValue, item.ConditionID, priceDisplay, 20, item.OptionCategoryID) + "</td></tr>\n";
                        }
                        else
                        {
                            conditionItem = conditionItem + "<tr ###swaprow###><td><a href=\"javascript:void(0)\" id=\"" + item.ConditionID + "\" class=\"tooltip\">" + policyDisplay + divPolicy + "</a></td><td class=\"rate_green\">" + currency.CurrencyCode + " " + priceDisplayFormat + textAvailable + "</td><td class=\"center\">" + GenDropDownPrice("ddPrice", item.ConditionID + "_" + item.OptionID + "_" + item.PromotionID + "_" + nameValue, item.ConditionID, priceDisplay, 20, item.OptionCategoryID) + "</td></tr>\n";
                        }
                    }


                }
            }
            return conditionItem;
        }
        public void LoadExtraOptionPrice()
        {
            PriceList = RateBase(2);
        }
        public string RenderExtraOption()
        {
            
            checkTransfer = false;

            PriceList = RateBase(2);
            decimal priceDisplay = 0;
            string priceDisplayFormat = string.Empty;
            string optionDisplay = string.Empty;
            int optionTemp = 0;
            discountPrice = 0;
            optionDisplay = optionDisplay + "<br/><br/><div id=\"errorTransfer\" class=\"errorMsg\"></div><table id=\"table_rate\" cellspacing=\"1\" width=\"100%\"><tr ><td class=\"extra\">  Extra Option</td><td class=\"rate\"> Rate</td><td class=\"quantity\"> Quantity</td></tr>\n";
            foreach (ExtranetPriceBase item in PriceList)
            {
                if (item.OptionID != optionTemp)
                {
                    if (item.OptionCategoryID == 43 || item.OptionCategoryID == 44)
                    {
                        checkTransfer = true;
                    }

                    //priceDisplay = Math.Round(item.Rate/vatInclude) / Convert.ToDecimal(currency.CurrencyPrefix);

                    if (item.OptionCategoryID != 43 || item.OptionCategoryID != 44)
                    {
                        //Extra Bed ABF etc.
                        //priceDisplay = priceDisplay * RoomNight;
                        priceDisplay = CalculateAll(item.ConditionID, item.OptionID, 0).Price / Convert.ToDecimal(currency.CurrencyPrefix);
                    }
                    else
                    {
                        priceDisplay = (int)(item.Price / vatInclude) / Convert.ToDecimal(currency.CurrencyPrefix);
                    }


                    if (currency.CurrencyID == 25)
                    {
                        if (item.OptionCategoryID != 43 || item.OptionCategoryID != 44)
                        {
                            priceDisplayFormat = (priceDisplay / RoomNight).ToString("#,###");
                        }
                        else
                        {
                            priceDisplayFormat = priceDisplay.ToString("#,###");
                        }
                    }
                    else
                    {
                        if (item.OptionCategoryID != 43 || item.OptionCategoryID != 44)
                        {
                            priceDisplayFormat = (priceDisplay / RoomNight).ToString("#,###.##");
                        }
                        else
                        {
                            priceDisplayFormat = priceDisplay.ToString("#,###.##");
                        }
                    }
                    optionDisplay = optionDisplay + "<tr><td class=\"extra_option_title\">" + item.OptionTitle + "</td><td class=\"rate_green\"><span class=\"otp_" + item.OptionID + "\">" + priceDisplayFormat + "</span></td><td class=\"center\">" + GenDropDownPrice("ddPriceExtra_" + item.ConditionID + "_" + item.OptionID, item.ConditionID + "_" + item.OptionID + "_0_" + item.NumAdult + "_" + item.NumChild + "_" + item.NumExtra, item.ConditionID, priceDisplay, 20, item.OptionCategoryID) + "</td></tr>\n";
                }
                optionTemp = item.OptionID;


            }
            if (!checkTransfer)
            {
                if (ProductCategoryID == 29)
                {
                    GetTransferFromOtherProduct(ProductID);
                    optionDisplay = optionDisplay + RenderTransferOption();
                }
            }
            optionDisplay = optionDisplay + "</table>\n";
            return optionDisplay;
        }
        //

        public string RenderTeeOffTimeBox()
        {
            string displayTeeOff = "<br /><br /><table width=\"753\" cellspacing=\"1\" cellpadding=\"3\" class=\"number_guest\">\n";
            displayTeeOff = displayTeeOff + "<tr><td class=\"extra\" align=\"left\" colspan=\"2\"><strong>Select Number of Guests </strong> </td></tr>\n";
            displayTeeOff = displayTeeOff + "<tr><td width=\"100\">Adult</td><td width=\"636\" align=\"left\">" + GendropDownNumber("adult", 1, 20, 1) + "\n";
            displayTeeOff = displayTeeOff + "</td></tr>\n";
            displayTeeOff = displayTeeOff + "<tr class=\"bg_blue\"><td width=\"100\">Children</td><td align=\"left\">" + GendropDownNumber("child", 0, 20, 0) + "\n";
            displayTeeOff = displayTeeOff + "</td></tr>\n";
            displayTeeOff = displayTeeOff + "</table>\n";

            return displayTeeOff;
        }
        //Transfer Option

        public void GetTransferFromOtherProduct(int productID)
        {
            ProductTransferID = 0;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select top 1 p.product_id,";
                sqlCommand = sqlCommand + " ISNULL((";
                sqlCommand = sqlCommand + " select top 1 sp.product_id";
                sqlCommand = sqlCommand + " from tbl_product sp,tbl_product_location spl";
                sqlCommand = sqlCommand + " where sp.product_id=spl.product_id and spl.location_id=(";
                sqlCommand = sqlCommand + " select top 1 (spl2.location_id)";
                sqlCommand = sqlCommand + " from tbl_product sp2,tbl_product_location spl2";
                sqlCommand = sqlCommand + " where sp2.product_id=spl2.product_id and spl2.product_id=p.product_id";
                sqlCommand = sqlCommand + " ) and sp.cat_id=31 and sp.status=1";
                //sqlCommand = sqlCommand + " select top 1 spl.product_id from tbl_product sp,tbl_product_location spl where sp.product_id=spl.product_id and sp.cat_id=31  and sp.status=1 and sp.product_id=p.product_id";
                sqlCommand = sqlCommand + " ),'0') as location_transfer_id,";
                sqlCommand = sqlCommand + " 0  as destination_transfer_id";
                //sqlCommand = sqlCommand + " ISNULL((";
                //sqlCommand = sqlCommand + " select top 1 sp.product_id from tbl_product sp where sp.destination_id=p.destination_id and sp.cat_id=31 and sp.status=1";
                //sqlCommand = sqlCommand + " ),'0') as destination_transfer_id";
                sqlCommand = sqlCommand + " from tbl_product p";
                sqlCommand = sqlCommand + " where p.product_id=" + productID;

                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if ((int)reader["location_transfer_id"] > 0)
                {
                    ProductTransferID = (int)reader["location_transfer_id"];
                }
                else if ((int)reader["destination_transfer_id"] > 0)
                {
                    ProductTransferID = (int)reader["destination_transfer_id"];

                }
                //HttpContext.Current.Response.Write(ProductTransferID+"<br>");
            }
            
        }

        public string RenderTransferOption()
        {
            PriceList = RateBase(3);
            string optionDisplay = "";

            decimal priceDisplay = 0;
            string priceDisplayFormat = string.Empty;
            foreach (ExtranetPriceBase item in PriceList)
            {
                priceDisplay = (int)(item.Price / vatInclude) / Convert.ToDecimal(currency.CurrencyPrefix);
                if (currency.CurrencyID == 25)
                {
                    priceDisplayFormat = priceDisplay.ToString("#,###");
                }
                else
                {
                    priceDisplayFormat = priceDisplay.ToString("#,###.##");
                }
                //optionDisplay = optionDisplay + "<tr><td class=\"extra_option_title\">" + item.OptionTitle + "<br/>" + item.ConditionTitle + "</td><td class=\"rate_green\">" + priceDisplayFormat + "</td><td class=\"center\">" + GenDropDownPrice("ddPriceExtra_" + item.ConditionID + "_" + item.OptionID, item.ConditionID + "_" + item.OptionID + "_0_" + item.NumAdult + "_" + item.NumChild + "_" + item.NumExtra, item.ConditionID, priceDisplay, 10, item.OptionCategoryID) + "</td></tr>\n";
                optionDisplay = optionDisplay + "<tr><td class=\"extra_option_title\">" + item.OptionTitle + "</td><td class=\"rate_green\"><span class=\"otp_" + item.OptionID + "\">" + priceDisplayFormat + "</span></td><td class=\"center\">" + GenDropDownPrice("ddPriceExtra_" + item.ConditionID + "_" + item.OptionID, item.ConditionID + "_" + item.OptionID + "_0_" + item.NumAdult + "_" + item.NumChild + "_" + item.NumExtra, item.ConditionID, priceDisplay, 20, item.OptionCategoryID) + "</td></tr>\n";
            }

            return optionDisplay;
            //string optionDisplay = string.Empty;
            //if (!checkTransfer)
            //{
            //    PriceList = RateBase(2);
            //    decimal priceDisplay = 0;

            //    optionDisplay = optionDisplay + "<table id=\"table_rate\"><tr ><td class=\"extra\">  Extra Option</td><td class=\"rate\"> Rate</td><td class=\"quantity\"> Quantity</td></tr>\n";
            //    foreach (PriceBase item in PriceList)
            //    {
            //        priceDisplay = item.Rate;
            //        optionDisplay = optionDisplay + "<tr><td>" + item.OptionTitle + "</td><td>" + string.Format("{0:0,0}", priceDisplay) + "</td><td>" + GenDropDownPrice("ddPriceExtra_" + item.ConditionID, item.ConditionID + "_" + item.OptionID, priceDisplay, 10) + "</td></tr>\n";
            //    }
            //    optionDisplay = optionDisplay + "</table>\n";
            //}
            //return optionDisplay;
        }

        //
        //-------Gala--------

        public string RenderGalaDinner()
        {
            string galaDisplay = string.Empty;

            GalaDinner gala = new GalaDinner(ProductID, DateStart, DateEnd);
            List<GalaDinner> galaList = gala.GetGalaExtranet();

            if (galaList.Count > 0)
            {
                //galaDisplay = "<br><table id=\"CompulsoryGala\"><tr><td><strong>Compulsory Meals</strong><br>The following item(s) will be added to your booking automatically if the conditions are matched. This is due to the hotel's policy.<ul>\n";

                galaDisplay = "<br><table class=\"gala\" cellpadding=\"0\" cellspacing=\"1\" width=\"90%\" align=\"center\">\n";
                galaDisplay = galaDisplay + "<tr valign=\"top\">\n";
                galaDisplay = galaDisplay + "<td>\n";
                galaDisplay = galaDisplay + "<p>Compulsory Meals</p>\n";
                galaDisplay = galaDisplay + "<ul>\n";

                //Gala Detail
                //foreach (GalaDinner item in galaList)
                //{
                //    if (item.DefaultGala == 0)
                //    {
                //        // Select Only day
                //        if (item.RequireAdult)
                //        {
                //            galaDisplay = galaDisplay + "<li>" + item.Title + " require gala for adult x2</li>\n";
                //        }
                //        if (item.RequireChild)
                //        {
                //            galaDisplay = galaDisplay + "<li>" + item.Title + " require gala for children x2<br></li>";
                //        }

                //    }
                //    else
                //    {
                //        // Select all day
                //        if (item.RequireAdult)
                //        {
                //            galaDisplay = galaDisplay + "<li>" + item.Title + " require gala for adult x2xnight</li>\n";
                //        }
                //        if (item.RequireChild)
                //        {
                //            galaDisplay = galaDisplay + "<li>" + item.Title + " require gala for children x2xnight</li>\n";
                //        }

                //    }
                //}

                galaDisplay = galaDisplay + "<li>Compulsory Gala Dinner : Compulsory gala dinner have been added to your booking automatically as part of the hotel's periodic condition.</li>";
                galaDisplay = galaDisplay + "</ul>";
                //galaDisplay = galaDisplay + "<p class=\"ment\"><span>*</span>The compulsory meals have been added to your booking automatically as part of the hotel's periodic condition.</p></td>\n";
                galaDisplay = galaDisplay + "</tr>\n";
                galaDisplay = galaDisplay + "</table>\n";
            }
            return galaDisplay;
        }

        //-------End Gala----
        public string RenderList()
        {
            string result = string.Empty;
            switch (ProductCategoryID)
            {
                case 29:
                    result = RenderHotelRateTable();
                    break;
                case 32:
                case 38:
                case 39:
                case 40:
                    result = RenderGolfTable();
                    break;
                case 34:
                case 36:
                    result = RenderDaytripTable();
                    break;
            }
            return result;

        }

        //

        public string RenderHotelRateTable()
        {
            //LoadPrice();
            string productFilename = string.Empty;
            string policyDisplay = string.Empty;
            string adultMax = string.Empty;

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select d.folder_destination+'-'+pcat.folder_cat+'/'+pc.file_name_main as file_name_main";
                sqlCommand = sqlCommand + " from tbl_product p,tbl_product_content pc,tbl_destination d,tbl_destination_name dn,tbl_product_category pcat";
                sqlCommand = sqlCommand + " where p.product_id=pc.product_id and p.destination_id=d.destination_id and d.destination_id=dn.destination_id and p.cat_id=pcat.cat_id";
                sqlCommand = sqlCommand + " and p.product_id=" + ProductID + " and pc.lang_id=1 and dn.lang_id=1";
                
                SqlCommand cmd = new SqlCommand(sqlCommand,cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    productFilename = reader["file_name_main"].ToString();
                }

                string htmlItem = "<div id=\"errorRoom\" class=\"errorMsg\"></div><table id=\"table_rate_old\" cellspacing=\"1\" width=\"100%\">\n";
                //htmlItem = htmlItem + "<tr><td colspan=\"5\" class=\"roomtype\">" + DateStart.ToShortDateString() + "-" + DateEnd.ToShortDateString() + "</td></tr>\n";
                htmlItem = htmlItem + "<tr ><th class=\"roomtype\">Room Type</th><th class=\"max\">Max No.of Pax</th><th class=\"conditions\">Conditions</th><th class=\"conditions\">Average Rate <br/> per Night</th><th class=\"room_select\">No.of Room</th></tr>\n";
                int optionTmp = 0;
                int conditionTmp = 0;
                decimal priceDisplay = 0;
                int countItem = 0;
                string divPolicy = string.Empty;
                string priceDisplayFormat = string.Empty;
                string roomImage = string.Empty;
                string productTitle = string.Empty;
                string bgSwap = "class=\"bg_blue\"";
                string textAvailable = string.Empty;
                string urlRoomDetail = string.Empty;

                countConditionActive = 0;

                if (PriceList.Count > 0)
                {
                    foreach (ExtranetPriceBase item in PriceList)
                    {
                        if (item.IsRoomShow)
                        {
                            urlRoomDetail = productFilename.Replace(".asp", "_room_" + item.OptionID + ".asp");
                            productTitle = "<a href=\"/" + urlRoomDetail + "\" target=\"_blank\"  class=\"room_rate_title\">" + item.OptionTitle + "</a>";
                        }

                        //countItem=1;
                        productTitle = item.OptionTitle;
                       
                        //HttpContext.Current.Response.Write("aaa<br>");

                        //price without promotion
                        priceDisplay = CalculateAll(item.ConditionID, item.OptionID, 0).PriceExcludeABF / Convert.ToDecimal(currency.CurrencyPrefix);
                        //priceDisplay = priceDisplay / RoomNight;
                        if (priceDisplay > 0)
                        {
                            countConditionActive = countConditionActive + 1;
                            //hasPriceRecord = true;

                            if (!string.IsNullOrEmpty(item.OptionPicture))
                            {
                                roomImage = item.OptionPicture;
                            }
                            else
                            {
                                roomImage = "/thailand-hotels-pic/" + item.ProductCode + "_" + item.OptionID + "_b_1.jpg";
                            }

                                
                            
                            if (!File.Exists(HttpContext.Current.Request.MapPath(roomImage)))
                            {
                                roomImage = "";
                            }
                            else
                            {
                                roomImage = "<a href=\"#\"><img src=\"" + roomImage + "\"  class=\"small_room_img\"></a>";
                            }

                            if (currency.CurrencyID == 25)
                            {
                                priceDisplayFormat = (priceDisplay / RoomNight).ToString("#,###");
                            }
                            else
                            {
                                priceDisplayFormat = (priceDisplay / RoomNight).ToString("#,###.##");
                            }

                            //priceDisplay = CalculateAll(item.ConditionID, item.OptionID, 0).Price;
                            divPolicy = "<div style=\"display:none;\" class=\"tooltip_content\">" + GetPolicyContent(item) + "</div>";
                            policyDisplay = GenCondtionPolicy(item, "", 1);

                            if (item.NumAdult > 3)
                            {
                                adultMax = "<img src=\"/theme_color/blue/images/icon/adult.png\" align=\"absmiddle\" />x" + item.NumAdult;
                            }
                            else
                            {
                                adultMax = "<img src=\"/theme_color/blue/images/icon/ico_adult_" + item.NumAdult + ".png\" align=\"absmiddle\" />";
                            }

                            if (optionTmp != item.OptionID)
                            {


                                //first option

                                htmlItem = htmlItem.Replace("###RowSpan###", countItem.ToString());
                                htmlItem = htmlItem.Replace("###swaprow###", bgSwap);
                                htmlItem = htmlItem + "<tr ###swaprow###><td rowspan=\"###RowSpan###\" valign=\"top\">" + roomImage + productTitle + "</td>";


                                //HttpContext.Current.Response.Write(item.ConditionID+"<br>");
                                countItem = 1;
                               

                                if (CountPromotion(item.ConditionID) > 0)
                                {
                                    //check promotion
                                    //HttpContext.Current.Response.Write("<font color='red'>Hello</font><br>");
                                    htmlItem = htmlItem + RenderConditionItem(item, true);
                                    //countItem = countItem + CountPromotion(item.ConditionID);
                                    countItem = CountPromotion(item.ConditionID);
                                }
                                else
                                {
                                    
                                    textAvailable = string.Empty;
                                    if (!string.IsNullOrEmpty(objAllotment.CheckAllotAvaliable_Cutoff(item.SupplierPrice, item.OptionID, 1, DateStart, DateEnd)))
                                    {
                                        textAvailable = "<br/><span class=\"avai_14\">Available Now!</span>";
                                    }

                                    
                                    
                                    //htmlItem = htmlItem + "<td><a href=\"javascript:void(0)\" class=\"tooltip\">" + adultMax + "<span class=\"tooltip_content\">Standard occupancy: " + item.NumAdult + "</span></a></td><td><a href=\"javascript:void(0)\" id=\"" + item.ConditionID + "\" class=\"tooltip\">" + policyDisplay + divPolicy + "</a></td><td class=\"rate_green\"><span class=\"otp_" + item.OptionID + "\">" + currency.CurrencyCode + " " + priceDisplayFormat +textAvailable +"</span></td><td class=\"center\">" + GenDropDownPrice("ddPrice", item.ConditionID + "_" + item.OptionID + "_0_" + item.NumAdult + "_" + item.NumChild + "_" + item.NumExtra, item.ConditionID, priceDisplay, 20, item.OptionCategoryID) + "</td></tr>\n";
                                    htmlItem = htmlItem + "<td><a href=\"javascript:void(0)\" class=\"tooltip\">" + adultMax + "<span class=\"tooltip_content\">Standard occupancy: " + item.NumAdult + "</span></a></td><td><a href=\"javascript:void(0)\" id=\"" + item.ConditionID + "\" class=\"tooltip\">" + policyDisplay + divPolicy + "</a></td><td class=\"rate_green\">" + currency.CurrencyCode + " " + priceDisplayFormat + textAvailable + "</td><td class=\"center\">" + GenDropDownPrice("ddPrice", item.ConditionID + "_" + item.OptionID + "_0_" + item.NumAdult + "_" + item.NumChild + "_" + item.NumExtra, item.ConditionID, priceDisplay, 20, item.OptionCategoryID) + "</td></tr>\n"; 
                                    countItem = 1;
                                }

                                if (bgSwap == "class=\"bg_blue\"")
                                {
                                    bgSwap = "";
                                }
                                else
                                {
                                    bgSwap = "class=\"bg_blue\"";
                                }

                            }
                            else
                            {

                                //second option
                                if (item.ConditionID != conditionTmp)
                                {
                                    if (CountPromotion(item.ConditionID) > 0)
                                    {
                                        //check promotion
                                        htmlItem = htmlItem + RenderConditionItem(item, false);
                                        countItem = countItem + CountPromotion(item.ConditionID);

                                    }
                                    else
                                    {
                                        htmlItem = htmlItem + "<tr ###swaprow###><td><a href=\"javascript:void(0)\" class=\"tooltip\">" + adultMax + "<span class=\"tooltip_content\">Standard occupancy: " + item.NumAdult + "</span></a></td><td><a href=\"javascript:void(0)\" id=\"" + item.ConditionID + "\" class=\"tooltip\">" + policyDisplay + divPolicy + "</a></td><td class=\"rate_green\"><span class=\"otp_" + item.OptionID + "\">" + currency.CurrencyCode + " " + priceDisplayFormat +textAvailable+ "</span></td><td class=\"center\">" + GenDropDownPrice("ddPrice", item.ConditionID + "_" + item.OptionID + "_0_" + item.NumAdult + "_" + item.NumChild + "_" + item.NumExtra, item.ConditionID, priceDisplay, 20, item.OptionCategoryID) + "</td></tr>\n";

                                        countItem = countItem + 1;
                                        //HttpContext.Current.Response.Write(countItem);
                                    }

                                }
                            }
                            optionTmp = item.OptionID;
                            conditionTmp = item.ConditionID;
                        }
                    }

                    htmlItem = htmlItem.Replace("###swaprow###", bgSwap);
                    htmlItem = htmlItem.Replace("###RowSpan###", countItem.ToString());
                    htmlItem = htmlItem + "</table>";
                }
                return htmlItem;
            }

            
        }

        public string RenderGolfTable()
        {
            //LoadPrice();
            string productFilename = string.Empty;

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select pc.file_name_main from tbl_product_content pc where pc.product_id=" + ProductID + " and pc.lang_id=1";
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    productFilename = reader["file_name_main"].ToString();
                }

                string htmlItem = "<div id=\"errorRoom\" class=\"errorMsg\"></div><table id=\"table_rate_old\" cellspacing=\"1\" width=\"100%\">\n";
                //htmlItem = htmlItem + "<tr><td colspan=\"5\" class=\"roomtype\">" + DateStart.ToShortDateString() + "-" + DateEnd.ToShortDateString() + "</td></tr>\n";
                htmlItem = htmlItem + "<tr ><th class=\"roomtype\">Option</th><th class=\"conditions\">Conditions</th><th class=\"conditions\">Rate</th><th class=\"room_select\">Quantity</th></tr>\n";
                int optionTmp = 0;
                int conditionTmp = 0;
                decimal priceDisplay = 0;
                int countItem = 0;
                string divPolicy = string.Empty;
                string priceDisplayFormat = string.Empty;
                string roomImage = string.Empty;
                string productTitle = string.Empty;
                string bgSwap = "class=\"bg_blue\"";
                string policyDisplay = string.Empty;
                if (PriceList.Count > 0)
                {
                    foreach (ExtranetPriceBase item in PriceList)
                    {
                        //countItem=1;
                        productTitle = item.OptionTitle;
                        //if (item.IsRoomShow)
                        //{
                        //    productTitle = "<a href=\"" + productFilename.Replace(".asp", "_room_" + item.OptionID + ".asp") + "\" class=\"lightBoxFrame\">" + item.OptionTitle + "</a>";
                        //}
                        //HttpContext.Current.Response.Write("aaa<br>");

                        //price without promotion
                        priceDisplay = CalculateAll(item.ConditionID, item.OptionID, 0).Price / Convert.ToDecimal(currency.CurrencyPrefix);
                        //priceDisplay = priceDisplay / RoomNight;
                        if (priceDisplay > 0)
                        {
                            countConditionActive = countConditionActive + 1;
                            //hasPriceRecord = true;
                            roomImage = "/thailand-hotels-pic/" + item.ProductCode + "_" + item.OptionID + "_b_1.jpg";
                            if (!File.Exists(HttpContext.Current.Request.MapPath(roomImage)))
                            {
                                roomImage = "";
                            }
                            else
                            {
                                roomImage = "<a href=\"#\"><img src=\"" + roomImage + "\"  class=\"small_room_img\"></a>";
                            }

                            if (currency.CurrencyID == 25)
                            {
                                priceDisplayFormat = (priceDisplay / RoomNight).ToString("#,###");
                            }
                            else
                            {
                                priceDisplayFormat = (priceDisplay / RoomNight).ToString("#,###.##");
                            }

                            //priceDisplay = CalculateAll(item.ConditionID, item.OptionID, 0).Price;
                            divPolicy = "<div style=\"display:none;\" class=\"tooltip_content\">" + GetPolicyContent(item) + "</div>";
                            policyDisplay = GenCondtionPolicy(item, "", 1);
                            if (optionTmp != item.OptionID)
                            {


                                //first option

                                htmlItem = htmlItem.Replace("###RowSpan###", countItem.ToString());
                                htmlItem = htmlItem.Replace("###swaprow###", bgSwap);
                                htmlItem = htmlItem + "<tr ###swaprow###><td rowspan=\"###RowSpan###\" valign=\"top\">" + roomImage + productTitle + "</td>";


                                //HttpContext.Current.Response.Write(item.ConditionID+"<br>");
                                countItem = 1;
                                if (CountPromotion(item.ConditionID) > 0)
                                {
                                    //check promotion
                                    //HttpContext.Current.Response.Write("<font color='red'>Hello</font><br>");
                                    htmlItem = htmlItem + RenderConditionItem(item, true);
                                    //countItem = countItem + CountPromotion(item.ConditionID);
                                    countItem = CountPromotion(item.ConditionID);
                                }
                                else
                                {
                                    htmlItem = htmlItem + "<td><a href=\"javascript:void(0)\" id=\"" + item.ConditionID + "\" class=\"tooltip\">" + policyDisplay + divPolicy + "</a></td><td class=\"rate_green\"><span class=\"otp_" + item.OptionID + "\">" + currency.CurrencyCode + " " + priceDisplayFormat + "</span></td><td class=\"center\">" + GenDropDownPrice("ddPrice", item.ConditionID + "_" + item.OptionID + "_0_" + item.NumAdult + "_" + item.NumChild + "_" + item.NumExtra, item.ConditionID, priceDisplay, 20, item.OptionCategoryID) + "</td></tr>\n";
                                    countItem = 1;
                                }

                                if (bgSwap == "class=\"bg_blue\"")
                                {
                                    bgSwap = "";
                                }
                                else
                                {
                                    bgSwap = "class=\"bg_blue\"";
                                }

                            }
                            else
                            {

                                //second option
                                if (item.ConditionID != conditionTmp)
                                {
                                    if (CountPromotion(item.ConditionID) > 0)
                                    {
                                        //check promotion
                                        htmlItem = htmlItem + RenderConditionItem(item, false);
                                        countItem = countItem + CountPromotion(item.ConditionID);

                                    }
                                    else
                                    {
                                        htmlItem = htmlItem + "<tr ###swaprow###><td><a href=\"javascript:void(0)\" id=\"" + item.ConditionID + "\" class=\"tooltip\">" + policyDisplay + divPolicy + "</a></td><td class=\"rate_green\"><span class=\"otp_" + item.OptionID + "\">" + currency.CurrencyCode + " " + priceDisplayFormat + "</span></td><td class=\"center\">" + GenDropDownPrice("ddPrice", item.ConditionID + "_" + item.OptionID + "_0_" + item.NumAdult + "_" + item.NumChild + "_" + item.NumExtra, item.ConditionID, priceDisplay, 20, item.OptionCategoryID) + "</td></tr>\n";

                                        countItem = countItem + 1;
                                        //HttpContext.Current.Response.Write(countItem);
                                    }

                                }
                            }
                            optionTmp = item.OptionID;
                            conditionTmp = item.ConditionID;
                        }
                    }

                    htmlItem = htmlItem.Replace("###swaprow###", bgSwap);
                    htmlItem = htmlItem.Replace("###RowSpan###", countItem.ToString());
                    htmlItem = htmlItem + "</table>";
                }
                return htmlItem;
            }

            
        }
        public string RenderDaytripTable()
        {
            //LoadPrice();
            string productFilename = string.Empty;
            string policyDisplay = string.Empty;

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select pc.file_name_main from tbl_product_content pc where pc.product_id=" + ProductID + " and pc.lang_id=1";
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    productFilename = reader["file_name_main"].ToString();
                }

                string htmlItem = "<div id=\"errorRoom\" class=\"errorMsg\"></div><table id=\"table_rate_old\" cellspacing=\"1\" width=\"100%\">\n";
                //htmlItem = htmlItem + "<tr><td colspan=\"5\" class=\"roomtype\">" + DateStart.ToShortDateString() + "-" + DateEnd.ToShortDateString() + "</td></tr>\n";
                htmlItem = htmlItem + "<tr ><th class=\"roomtype\" width=\"350\">Program Tour</th><th class=\"conditions\">Conditions</th><th class=\"conditions\">Rate</th><th class=\"room_select\">Quantity</th></tr>\n";
                int optionTmp = 0;
                int conditionTmp = 0;
                decimal priceDisplay = 0;
                int countItem = 0;
                string divPolicy = string.Empty;
                string priceDisplayFormat = string.Empty;
                string roomImage = string.Empty;
                string productTitle = string.Empty;
                string bgSwap = "class=\"bg_blue\"";
                string OptionContent = string.Empty;

                FrontItinerary Itinerary = new FrontItinerary();
                List<object> ItinerayList = new List<object>();
                string ItineratyContent = string.Empty;

                if (PriceList.Count > 0)
                {
                    foreach (ExtranetPriceBase item in PriceList)
                    {
                        //countItem=1;
                        productTitle = item.OptionTitle;


                        if (item.IsRoomShow)
                        {

                            productTitle = "<a href=\"javascript:void(0)\" class=\"tooltip\">" + item.OptionTitle + "<div class=\"tooltip_content\">" + item.OptionDetail + "</div></a>";
                            //productTitle =productTitle+ "<div class=\"tooltip_content\">" + item.OptionDetail + "</div>";
                        }



                        ItinerayList = Itinerary.GetItineraryProgram(item.OptionID);
                        ItineratyContent = "";
                        if (ItinerayList.Count > 0)
                        {
                            ItineratyContent = "<br/><br/><a href=\"/Itinerary-information.aspx?itid=" + item.OptionID + "\" class=\"lightBoxFrame\"> <font color=\"green\">Itinerary</font>";

                            ItineratyContent = ItineratyContent + "</a>";
                        }

                        productTitle = productTitle + ItineratyContent;

                        priceDisplay = CalculateAll(item.ConditionID, item.OptionID, 0).Price / Convert.ToDecimal(currency.CurrencyPrefix);
                        //priceDisplay = priceDisplay / RoomNight;
                        if (priceDisplay > 0)
                        {
                            countConditionActive = countConditionActive + 1;
                            //hasPriceRecord = true;
                            roomImage = "/thailand-hotels-pic/" + item.ProductCode + "_" + item.OptionID + "_b_1.jpg";
                            if (!File.Exists(HttpContext.Current.Request.MapPath(roomImage)))
                            {
                                roomImage = "";
                            }
                            else
                            {
                                roomImage = "<a href=\"#\"><img src=\"" + roomImage + "\"  class=\"small_room_img\"></a>";
                            }

                            if (currency.CurrencyID == 25)
                            {
                                priceDisplayFormat = (priceDisplay / RoomNight).ToString("#,###");
                            }
                            else
                            {
                                priceDisplayFormat = (priceDisplay / RoomNight).ToString("#,###.##");
                            }

                            //priceDisplay = CalculateAll(item.ConditionID, item.OptionID, 0).Price;
                            divPolicy = "<div style=\"display:none;\" class=\"tooltip_content\">" + GetPolicyContent(item) + "</div>";
                            policyDisplay = GenCondtionPolicy(item, "", 1);
                            if (optionTmp != item.OptionID)
                            {


                                //first option

                                htmlItem = htmlItem.Replace("###RowSpan###", countItem.ToString());
                                htmlItem = htmlItem.Replace("###swaprow###", bgSwap);
                                htmlItem = htmlItem + "<tr ###swaprow###><td rowspan=\"###RowSpan###\" valign=\"top\">" + roomImage + productTitle + "</td>";


                                //HttpContext.Current.Response.Write(item.ConditionID+"<br>");
                                countItem = 1;
                                if (CountPromotion(item.ConditionID) > 0)
                                {
                                    //check promotion
                                    //HttpContext.Current.Response.Write("<font color='red'>Hello</font><br>");
                                    htmlItem = htmlItem + RenderConditionItem(item, true);
                                    //countItem = countItem + CountPromotion(item.ConditionID);
                                    countItem = CountPromotion(item.ConditionID);
                                }
                                else
                                {
                                    htmlItem = htmlItem + "<td><a href=\"javascript:void(0)\" id=\"" + item.ConditionID + "\" class=\"tooltip\">" + policyDisplay + divPolicy + "</a></td><td class=\"rate_green\"><span class=\"otp_" + item.OptionID + "\">" + RenderDaytripSupplement(item.ConditionID, priceDisplay) + "</span></td><td class=\"center\">" + GenDropDownPrice("ddPrice", item.ConditionID + "_" + item.OptionID + "_0_" + item.NumAdult + "_" + item.NumChild + "_" + item.NumExtra, item.ConditionID, priceDisplay, 20, item.OptionCategoryID) + "</td></tr>\n";
                                    countItem = 1;
                                }

                                if (bgSwap == "class=\"bg_blue\"")
                                {
                                    bgSwap = "";
                                }
                                else
                                {
                                    bgSwap = "class=\"bg_blue\"";
                                }

                            }
                            else
                            {

                                //second option
                                if (item.ConditionID != conditionTmp)
                                {
                                    if (CountPromotion(item.ConditionID) > 0)
                                    {
                                        //check promotion
                                        htmlItem = htmlItem + RenderConditionItem(item, false);
                                        countItem = countItem + CountPromotion(item.ConditionID);

                                    }
                                    else
                                    {
                                        htmlItem = htmlItem + "<tr ###swaprow###><td><a href=\"javascript:void(0)\" id=\"" + item.ConditionID + "\" class=\"tooltip\">" + policyDisplay + divPolicy + "</a></td><td class=\"rate_green\"><span class=\"otp_" + item.OptionID + "\">" + RenderDaytripSupplement(item.ConditionID, priceDisplay) + "</span></td><td class=\"center\">" + GenDropDownPrice("ddPrice", item.ConditionID + "_" + item.OptionID + "_0_" + item.NumAdult + "_" + item.NumChild + "_" + item.NumExtra, item.ConditionID, priceDisplay, 20, item.OptionCategoryID) + "</td></tr>\n";

                                        countItem = countItem + 1;
                                        //HttpContext.Current.Response.Write(countItem);
                                    }

                                }
                            }
                            optionTmp = item.OptionID;
                            conditionTmp = item.ConditionID;
                        }
                    }

                    htmlItem = htmlItem.Replace("###swaprow###", bgSwap);
                    htmlItem = htmlItem.Replace("###RowSpan###", countItem.ToString());
                    htmlItem = htmlItem + "</table>";
                }
                return htmlItem;
            }

            
        }

        public string RenderDaytripSupplement(int ConditionID, decimal Price)
        {
            string supplementDisplay = "<table cellpadding=\"0\" cellspacing=\"1\" class=\"supplement\" width=\"100%\">\n";
            int supplementCal = 0;
            string supplementTitle = string.Empty;
            decimal priceIncludeVat = Price * Convert.ToDecimal(1.177);
            decimal priceDisplay = 0;
            foreach (FrontSupplementPriceQuantity item in supplementList)
            {
                if (item.ConditionID == ConditionID)
                {
                    supplementCal = item.QuantityMax - item.QuantityMin;
                    if (supplementCal == 0)
                    {
                        supplementTitle = item.QuantityMax + " Pax";
                    }
                    if (supplementCal > 0)
                    {
                        supplementTitle = item.QuantityMin + "-" + item.QuantityMax + " Pax";
                    }
                    if (item.QuantityMax == 100)
                    {
                        if (supplementCal == 99)
                        {
                            supplementTitle = "1 Pax";
                        }
                        else
                        {
                            supplementTitle = (item.QuantityMin) + " Pax Up";
                        }
                    }
                    priceDisplay = (int)(priceIncludeVat + item.Supplement) / Convert.ToDecimal(1.177);

                    supplementDisplay = supplementDisplay + "<tr><td class=\"list\">&bull;" + supplementTitle + "</td><td>" + priceDisplay.ToString("#,###") + "</td></tr>\n";
                }
            }
            supplementDisplay = supplementDisplay + "</table>\n";
            return supplementDisplay;
        }

        public bool CheckDateUseAccept(int OptionID)
        {
            bool checkdDateUse = true;
            string[] arrDay;
            DateTime dateCheck;

            foreach (FrontProductOptionWeekDay item in optionWeekendList)
            {
                if (item.OptionID == OptionID)
                {
                    arrDay = (item.Sunday + "," + item.Monday + "," + item.Tuesday + "," + item.Wednesday + "," + item.Thursday + "," + item.Friday + "," + item.Saturday).Split(',');
                    for (int dayCount = 0; dayCount < RoomNight; dayCount++)
                    {
                        dateCheck = DateStart.AddDays(dayCount);
                        if (!bool.Parse(arrDay[(int)dateCheck.DayOfWeek]))
                        {
                            checkdDateUse = false;
                            break;
                        }
                    }
                }
            }
            return checkdDateUse;
        }

        private decimal CalculatePriceDiscount(double discountPrice, decimal datePrice)
        {

            if (discountPrice != 0)
            {

                if (discountPrice >= 1)
                {
                    datePrice = (decimal)((double)datePrice - discountPrice);
                }
                else
                {
                    datePrice = (decimal)((double)datePrice * discountPrice);
                }
            }

            return datePrice;
        }

        public decimal GetMemberDiscount(List<FrontMemberDiscount> memberDiscountList,int conditionID) 
        {

            decimal result = 1;

            if (this.memberAuthen || HttpContext.Current.Session["memberAccess"] != null)
            {
                if (memberDiscountList != null)
                {
                    foreach (FrontMemberDiscount item in memberDiscountList)
                    {
                        if (item.ConditionID == conditionID)
                        {
                            result = result - (item.Discount / 100);
                            break;
                        }
                    }
                }
            }


            
            //HttpContext.Current.Response.End();
            return result;
        }

        public string GetMemberDiscountBenefit(List<FrontMemberDiscount> memberDiscountList, int conditionID)
        {
            string result = string.Empty;

            if (memberDiscountList != null)
            {
                foreach (FrontMemberDiscount item in memberDiscountList)
                {
                    if (item.ConditionID == conditionID)
                    {
                        result = item.BenefitDetail + "<br/>";
                    }
                }
            }
            return result;
        }

        public string GetAllBenefit(int conditionID)
        {
            string result = string.Empty;
            if (this.memberAuthen || HttpContext.Current.Session["memberAccess"]!=null)
            {
                if (memberDiscountList != null)
                {
                    foreach (FrontMemberDiscount item in memberDiscountList)
                    {
                        if (item.ConditionID == conditionID)
                        {
                            result = result + "Discount " + (int)item.Discount + "<br/>";

                        }
                    }

                }

                if (memberDiscountBenefitList != null)
                {
                    foreach (FrontMemberDiscount item in memberDiscountBenefitList)
                    {
                        if (item.ConditionID == conditionID)
                        {
                            result = result + item.BenefitDetail + "<br/>";
                        }
                    }
                }
            }
            return result;
        }

        public string XmlMemberDiscountContent(int conditionID)
        {
            bool bolCheck = false;
            string memberBenefitContent = string.Empty;
            if (memberDiscountList != null)
            {
                foreach (FrontMemberDiscount item in memberDiscountList)
                {
                    if (item.ConditionID == conditionID)
                    {
                        bolCheck = true;
                        memberBenefitContent = memberBenefitContent + "<MemberDiscountPrice>";
                        memberBenefitContent = memberBenefitContent + "<MemberDiscountID>"+item.MemberDiscountID+"</MemberDiscountID>";
                        memberBenefitContent = memberBenefitContent + "<ProductID>"+item.ProductID+"</ProductID>";
                        memberBenefitContent = memberBenefitContent + "<ConditionID>"+item.ConditionID+"</ConditionID>";
                        memberBenefitContent = memberBenefitContent + "<MemberDiscountType>"+item.DiscountType+"</MemberDiscountType>";
                        memberBenefitContent = memberBenefitContent + "<Discount>"+item.Discount+"</Discount>";
                        memberBenefitContent = memberBenefitContent + "<DateStart>"+item.DateStart+"</DateStart>";
                        memberBenefitContent = memberBenefitContent + "<DateEnd>"+item.DateEnd+"</DateEnd>";
                        memberBenefitContent = memberBenefitContent + "</MemberDiscountPrice>";
                    }
                }

            }

            if (bolCheck)
            {
                //return "hello";
                return "<MemberBenefits>" + memberBenefitContent + "</MemberBenefits>";
            }
            else {
                return conditionID.ToString();
            }
            
        }

        public OptionPrice CalculateAll(int conditionID, int optionID, int promotionID)
        {
            decimal datePrice = 0;
            decimal basePrice = 0;
            decimal dayPrice = 0;
            decimal holidayPrice = 0;
            //decimal priceOwn = 0;
            decimal priceRack = 0;
            decimal pricePerDay = 0;
            decimal pricePerDayReal = 0;
            decimal pricePromotion = 0;
            DateTime dateCheck;
            bool hasPrice = true;
            OptionPrice price = new OptionPrice();
            // create by nui
            
            IList<OptionDayPrice> iListPricePerDay = new List<OptionDayPrice>();
            ///-----
            decimal datePriceOwn = 0;
            decimal basePriceOwn = 0;
            decimal pricePromotionOwn = 0;
            decimal pricePerDayOwn = 0;
            decimal memberPrice = 0;
            

            if (CheckDateUseAccept(optionID))
            {
                
                priceABFTotal = 0;

                memberPrice = GetMemberDiscount(memberDiscountList, conditionID);

                for (int dayCount = 0; dayCount < RoomNight; dayCount++)
                {
                    
                    dateCheck = DateStart.AddDays(dayCount);
                    


                    if (GetPrice(conditionID, dateCheck).Price != 0)
                    {
                        

                        datePrice = ratePlan.CalculateRatePlan(refCountryID,GetPrice(conditionID, dateCheck).Price, conditionID, dateCheck);
                        priceRack = priceRack + datePrice;
                        datePriceOwn = datePrice;
                        //HttpContext.Current.Response.Write(datePriceOwn+"<br>");
                        basePriceOwn = basePriceOwn + datePriceOwn;

                        //if (datePrice > 0)
                        //{
                        //    if (discountPrice != 0)
                        //    {
                        //        if (discountPrice >= 1)
                        //        {
                        //            datePrice = (decimal)((double)datePrice - discountPrice);
                        //        }
                        //        else
                        //        {
                        //            datePrice = (decimal)((double)datePrice * discountPrice);
                        //        }
                        //    }
                        //}

                        decimal decPriceBase = CalculatePriceDiscount(discountPrice, datePrice);
                        basePrice = basePrice + decPriceBase;

                        

                        if (basePrice != 0)
                        {
                            
                            priceABFTemp = 0;

                            //dayPrice = dayPrice + GetPriceDay(PriceDayList, optionID, dateCheck);

                            dayPrice = 0;
                            holidayPrice = 0;

                            //HttpContext.Current.Response.Write("Date:"+dateCheck+"- Price Base:"+datePrice+" Condition:"+conditionID+" Promotion:"+promotionID+"<br>");
                            
                            pricePromotion = GetPromotionPrice(datePrice, 0, 0, dateCheck, conditionID, promotionID);
                            pricePromotionOwn = GetPromotionPrice(datePriceOwn, 0, 0, dateCheck, conditionID, promotionID);

                            //HttpContext.Current.Response.Write("Date:" + dateCheck + "- Price Base:" + datePrice + " Condition:" + conditionID + " Promotion:" + promotionID + "Promotion Price:" + pricePromotion + "<br>");


                            if (priceABFTemp == 0)
                            { 
                                pricePromotion = CalculatePriceDiscount(discountPrice, pricePromotion);
                            }
                            

                            pricePerDay = pricePerDay + pricePromotion;
                            pricePerDayOwn = pricePerDayOwn + pricePromotionOwn;

                            pricePerDayReal = pricePerDayReal + pricePromotion - priceABFTemp;
                            priceABFTotal = priceABFTotal + priceABFTemp;

                            
                            // define date per day
                            OptionDayPrice cPriceperDay = new OptionDayPrice();
                            cPriceperDay.DateCheck = dateCheck;
                            //BasePricePerDAy

                            //HttpContext.Current.Response.Write(conditionID);
                            //HttpContext.Current.Response.Write(dateCheck + "--"+decPriceBase + "----" + memberPrice + "---" + (decimal)(((double)(Utility.PriceExcludeVat((decPriceBase * memberPrice)))) / currency.CurrencyPrefix) + "<br/>");

                            //HttpContext.Current.Response.Flush();

                            cPriceperDay.PriceBase = (decimal)(Utility.PriceExcludeVat((decPriceBase * memberPrice)));

                            cPriceperDay.PricePromotion = (decimal)(Utility.PriceExcludeVat((pricePromotion * memberPrice)));

                            cPriceperDay.PriceABF = (decimal)(Utility.PriceExcludeVat((priceABFTemp * memberPrice)));

                            //cPriceperDay.PriceBase = 3000;
                            //cPriceperDay.PricePromotion = 30000;
                            //cPriceperDay.PriceABF = 3000;

                            iListPricePerDay.Add(cPriceperDay);
                        }
                    }
                    else
                    {
                        hasPrice = false;
                        break;
                    }

                }

             
                // Price Include Promotion
                decimal sumBasePrice = pricePerDay;
                //------------------------
                // Price Include Promotion not ABF
                decimal sumBasePriceExcludeABF = pricePerDayReal;
                //------------------------

                if (hasPrice)
                {
                    //Check Promotion Accept
                    if (!CheckPromotionAccept(promotionID))
                    {
                        //except
                        price.Price = basePrice + dayPrice + holidayPrice;
                        price.PriceExcludeABF = basePrice + dayPrice + holidayPrice;
                        //price.Price = basePrice + dayPrice + holidayPrice;
                    }
                    else
                    {
                        //accept

                        price.Price = sumBasePrice + dayPrice + holidayPrice;
                        price.PriceExcludeABF = sumBasePriceExcludeABF + dayPrice + holidayPrice;
                        //price.Price = sumBasePrice + dayPrice + holidayPrice;
                    }

                    //if (memberDiscountList!=null)
                    //{
                    //    HttpContext.Current.Response.Write("Check member discount data:"+memberDiscountList.Count()+"<br/>");
                    //}


                   
                    price.Price = (price.Price * memberPrice);
                    price.PriceExcludeABF = (price.PriceExcludeABF * memberPrice);
                    //----------------
                    price.PriceOwn = pricePerDayOwn * memberPrice;
                    price.PriceRack = priceRack;

                    price.iListPricePerDay = iListPricePerDay;
                    //----------------
                    //price.PriceOwn = priceOwn + Math.Round((dayPrice + holidayPrice) / profitInclude);
                    //price.PriceRack = priceRack;
                }
                else
                {
                    price.Price = 0;
                    price.PriceExcludeABF = 0;
                    price.PriceOwn = 0;
                    price.PriceRack = 0;
                }
            }
            else
            {
                price.Price = 0;
                price.PriceExcludeABF = 0;
                price.PriceOwn = 0;
                price.PriceRack = 0;
            }


            //HttpContext.Current.Response.Write(price.PriceOwn+"<br>");
            return price;
        }

        public bool CheckPromotionAccept(int promotionID)
        {
            bool checkWeekend = true;

            if (promotionID != 0)
            {
                string[] arrDay;
                DateTime dateCheck;

                foreach (PromotionPrice item in promotionList)
                {
                    if (item.PromotionID == promotionID)
                    {
                        arrDay = (item.DaySun + "," + item.DayMon + "," + item.DayTue + "," + item.DayWed + "," + item.DayThu + "," + item.DayFri + "," + item.DaySat).Split(',');

                        for (int dayCount = 0; dayCount < RoomNight; dayCount++)
                        {
                            dateCheck = DateStart.AddDays(dayCount);
                            if (!bool.Parse(arrDay[(int)dateCheck.DayOfWeek]))
                            {
                                checkWeekend = false;
                                break;
                            }
                        }
                        PromotionPrice objPromotion = new PromotionPrice();

                        if (objPromotion.CheckPromotionAcceptOnHoliday(promotionID, DateStart, DateEnd) == false)
                        {

                            checkWeekend = false;
                            break;
                        }
                    }
                }
            }
            else
            {
                checkWeekend = false;
            }

            return checkWeekend;
        }

        public string CheckPromotionAcceptTitle(int promotionID)
        {
            string promotionTitle = string.Empty;

            string[] arrDay;
            DateTime dateCheck;

            foreach (PromotionPrice item in promotionList)
            {
                if (item.PromotionID == promotionID)
                {
                    promotionTitle = item.Title;
                    arrDay = (item.DaySun + "," + item.DayMon + "," + item.DayTue + "," + item.DayWed + "," + item.DayThu + "," + item.DayFri + "," + item.DaySat).Split(',');

                    for (int dayCount = 0; dayCount < RoomNight; dayCount++)
                    {
                        dateCheck = DateStart.AddDays(dayCount);
                        if (!bool.Parse(arrDay[(int)dateCheck.DayOfWeek]))
                        {
                            promotionTitle = string.Empty;
                            break;
                        }
                    }
                    PromotionPrice objPromotion = new PromotionPrice();

                    if (objPromotion.CheckPromotionAcceptOnHoliday(promotionID, DateStart, DateEnd) == false)
                    {

                        promotionTitle = string.Empty;
                    }
                    
                }
            }
            return promotionTitle;
        }

        private string GenCondtionPolicy(ExtranetPriceBase price, string promotionTitle, int intType)
        {
            string policyDisplay = string.Empty;

            if (price.IsBreakfast > 0)
            {
                policyDisplay = policyDisplay + "<li>Breakfast Included</li>";
            }

            foreach (ProductPolicyExtranet item in policyList)
            {
                if(item.ConditionID==price.ConditionID)
                {
                    if (item.ConditionNameId != 2)
                        policyDisplay = policyDisplay + "<li>"+item.ConditionTitle+"</li>";
                    break;
                }
            }

            if (promotionTitle != "")
            {
                policyDisplay = policyDisplay + "<li class=\"special\">" + promotionTitle + "</li>";
            }

            if (!string.IsNullOrEmpty(policyDisplay))
            {
                policyDisplay = "<ul class=\"condition_list\">" + policyDisplay + "</ul>";
            }
            return policyDisplay;
        }

        private OptionPrice GetPrice(int conditionID, DateTime dateCheck)
        {

            OptionPrice price = new OptionPrice();
            if (PriceList.Count > 0)
            {
                foreach (ExtranetPriceBase item in PriceList)
                {

                    //if (CheckDateBetween(dateCheck) && item.ConditionID == conditionID)
                    if (dateCheck.Subtract(item.DatePrice).Days == 0 && item.ConditionID == conditionID)
                    {
                        price.Price = item.Price;
                        //price.PriceOwn = item.RateOwn;
                        //price.PriceRack = item.RateRack;
                        break;
                    }
                }

            }
            //HttpContext.Current.Response.Write(conditionID + " " + dateCheck+" "+price.Price+"<br>");
            return price;
        }


        public decimal GetPriceDay(List<PriceDay> priceDayList, int OptionID, DateTime dateCheck)
        {

            decimal result = 0;
            if (priceDayList.Count > 0)
            {
                foreach (PriceDay item in priceDayList)
                {
                    if (dateCheck.Subtract(item.DateStart).Days >= 0 && item.DateEnd.Subtract(dateCheck).Days >= 0 && item.OptionID == OptionID)
                    {

                        switch ((int)dateCheck.DayOfWeek)
                        {
                            case 0:
                                result = item.DateSun;
                                break;
                            case 1:
                                result = item.DateMon;
                                break;
                            case 2:
                                result = item.DateTue;
                                break;
                            case 3:
                                result = item.DateWed;
                                break;
                            case 4:
                                result = item.DateThu;
                                break;
                            case 5:
                                result = item.DateFri;
                                break;
                            case 6:
                                result = item.DateSat;
                                break;
                        }
                        //HttpContext.Current.Response.Write(result + "<br/>");
                    }

                }
            }
            return (result * profitInclude);
        }

        public decimal GetPriceHoliday(List<PriceHoliday> priceHolidayList, int OptionID, DateTime dateCheck)
        {
            decimal result = 0;
            foreach (PriceHoliday item in priceHolidayList)
            {
                if (dateCheck.Subtract(item.DateSupplement).Days == 0 && item.OptionID == OptionID)
                {
                    result = item.Supplement;
                    break;
                }
            }
            return (result * profitInclude);
        }


        public bool CheckDateBetween(DateTime dateCheck)
        {
            bool result = false;
            //if (dateCheck.Subtract(dateStartSet).Days >= 0 && dateEndSet.Subtract(dateCheck).Days >= 0)
            if (dateCheck.Subtract(DateStart).Days >= 0 && DateEnd.Subtract(dateCheck).Days >= 0)
            {
                result = true;
            }
            return result;
        }


        public decimal GetPromotionPrice(decimal basePrice, decimal dayPrice, decimal holidayPrice, DateTime dateCheck, int conditionID, int promotionID)
        {
            

            int totalSet = 0;
            int setPromotionAccept = 0;
            DateTime DateBenefitCheck;
            decimal pricePromotion = basePrice;
            decimal breakfastCharge = 0;
            bool IsInSet = false;

            DateTime dateStartSet = DateStart;

            DateTime dateEndSet;
            PromotionPrice objPromotion = new PromotionPrice();

            if (promotionID != 0)
            {
                //HttpContext.Current.Response.Write(basePrice + "<br>");
                //HttpContext.Current.Response.Write(item.ConditionID + "-->" + conditionID + "," + item.PromotionID + "-->"+promotionID+"<br>");
                //HttpContext.Current.Response.Write(conditionID + "," + promotionID + "<br>");
                foreach (PromotionPrice item in promotionList)
                {
                    //HttpContext.Current.Response.Write(item.ConditionID + "," + item.PromotionID + "<br>");
                    if (item.ConditionID == conditionID && item.PromotionID == promotionID)
                    {
                        //HttpContext.Current.Response.Write("<font color=\"red\">"+item.ConditionID + "-->" + conditionID + "," + item.PromotionID + "-->" + promotionID + "</font><br>");
                        //HttpContext.Current.Response.Write("Promotion<br>");
                        //HttpContext.Current.Response.Write((int)(DateEnd.Subtract(DateStart).Days / item.DayMin)+"<br>");
                        totalSet = (int)(DateEnd.Subtract(DateStart).Days / item.DayMin);
                        for (int countSet = 1; countSet <= totalSet; countSet++)
                        {
                            setPromotionAccept = countSet;
                            dateEndSet = dateStartSet.AddDays(item.DayMin - 1);
                            if (dateCheck.Subtract(dateStartSet).Days >= 0 && dateEndSet.Subtract(dateCheck).Days >= 0)
                            {
                                IsInSet = true;
                                //HttpContext.Current.Response.Write(conditionID+"-->"+promotionID+"-->"+dateStartSet+"-->"+dateCheck+"-->"+dateEndSet+"-->"+countSet+"<br>");
                                break;
                            }
                            else
                            {
                                dateStartSet = dateStartSet.AddDays(item.DayMin);
                                //HttpContext.Current.Response.Write(dateStartSet + "-->" + dateCheck + "-->" + dateEndSet + "-->" + countSet + "<br>");
                                //HttpContext.Current.Response.Write(conditionID + "-->" + promotionID + "-->" + dateCheck + "-<br>");
                            }
                        }

                        //for (int countSet = 1; countSet <= totalSet; countSet++)
                        //{


                        // HttpContext.Current.Response.Write(setPromotionAccept+"<="+item.MaxSet);
                        if (setPromotionAccept <= item.MaxSet)
                        {

                            foreach (PromotionBenefitDetail benefit in promotionBenefitList)
                            {

                                if (benefit.PromotionID == item.PromotionID)
                                {

                                    if (item.IsHolidayCharge == 0 && !objPromotion.CheckPromotionAcceptOnHoliday(item.PromotionID,DateStart,DateEnd))
                                    {

                                        //Dont' Calculate immediately
                                        pricePromotion = basePrice;
                                        break;

                                    }
                                    if (benefit.PromotionType == 2 || benefit.PromotionType == 6)
                                    {

                                        switch (benefit.PromotionType)
                                        {
                                            case 2:
                                                pricePromotion = (basePrice - ((basePrice * benefit.Discount) / 100));

                                                break;

                                            case 6:
                                                pricePromotion = basePrice - benefit.Discount;
                                                //HttpContext.Current.Response.Write(conditionID+" "+basePrice.ToString() + " " + benefit.Discount + "<br/>");
                                                //HttpContext.Current.Response.Write(benefit.PromotionID+"AAA<br>");
                                                break;
                                        }

                                        //HttpContext.Current.Response.Write(dateCheck+"  "+pricePromotion+"<br>");
                                    }
                                    else
                                    {
                                        if (IsInSet)
                                        {

                                            DateBenefitCheck = dateStartSet.AddDays(benefit.DayDiscountStart - 1);
                                            //HttpContext.Current.Response.Write(DateBenefitCheck+"<br>");
                                            for (int countDayBenefit = 0; countDayBenefit < benefit.DayDiscountNum; countDayBenefit++)
                                            {
                                                //HttpContext.Current.Response.Write(conditionID + "-->" + promotionID + "-->" + dateCheck + "-->"+dateCheck.Subtract(DateBenefitCheck.AddDays(countDayBenefit)).Days+"<br>");
                                                //HttpContext.Current.Response.Write(conditionID+"-->"+promotionID+"-->"+dateStartSet+"-->"+dateCheck+"-->"+dateEndSet+"-->"+countSet+"<br>");
                                                if (dateCheck.Subtract(DateBenefitCheck.AddDays(countDayBenefit)).Days == 0)
                                                {

                                                    //Calculate


                                                    switch (benefit.PromotionType)
                                                    {
                                                        case 1:
                                                            //Free Night
                                                            pricePromotion = 0;
                                                            if (item.IsBreakfast == 3)
                                                            {
                                                                //HttpContext.Current.Response.Write(benefit.DayDiscountNum + "<br>");
                                                                //HttpContext.Current.Response.Write(dateCheck.ToShortDateString() + "--" + DateBenefitCheck.AddDays(countDayBenefit).ToShortDateString() + "--" + item.ConditionID + "--" + item.PromotionID + "--" + countDayBenefit + "--" + breakfastCharge + "<br>");
                                                                //isbreakfast 1=include breakfast
                                                                //2 exclude breakfast
                                                                //3 excluce breakfast charge
                                                                priceABFTemp = item.BreakfastCharge;

                                                                //priceABFTotal = priceABFTotal + item.BreakfastCharge;
                                                                breakfastCharge = item.BreakfastCharge;
                                                                //promotionChargeABF = true;
                                                            }
                                                            break;
                                                        case 2:
                                                        //Discount All Day (%) 
                                                        case 3:
                                                            //Discount Some Day (%)
                                                            pricePromotion = (basePrice - ((basePrice * benefit.Discount) / 100));
                                                            break;

                                                        case 5:
                                                            //Set Constant Price (Baht)
                                                            pricePromotion = benefit.Discount;
                                                            break;
                                                        case 4:
                                                        //Discount Specific Day (Baht)
                                                        case 6:
                                                            //Discount All Day (Baht)
                                                            pricePromotion = basePrice - benefit.Discount;
                                                            break;
                                                    }
                                                    //

                                                    break;
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                        }
                        //else { 

                        //}
                        //}
                        break;
                    }
                }

                //HttpContext.Current.Response.Write(basePrice + "  " + pricePromotion + "<br/>");
                pricePromotion = pricePromotion + breakfastCharge;
            }
            else {
                pricePromotion = basePrice;
            }


            return pricePromotion;
        }


        //end new
        private string RenderAnnoucement()
        {
            string annouceDisplay = string.Empty;
            //DateTime dateCheck;

            //List<FrontAnnoucement> annoucementList;

            FrontAnnoucement annoucement = new FrontAnnoucement(ProductID);
            annoucement.LangID = _langID;
            Utility.SetSessionDate();
            //if (HttpContext.Current.Session["dateStart"] == "''")
            //{
            //    annoucementList = annoucement.LoadAnnoucement();

            //}
            //else
            //{

            //    annoucementList = annoucement.LoadAnnoucementByDate(Convert.ToString(HttpContext.Current.Session["dateStart"]), Convert.ToString(HttpContext.Current.Session["dateStart"]));
            //    // annoucement.LoadAnnoucementByDate();
            //}



            //if (annoucementList.Count > 0)
            //{
            //    annouceDisplay = annouceDisplay + "</br><table class=\"annoucement\" style=\"border:1px solid #2e2721;background-color:#f0f8ff;\" width=\"95%\" align=\"center\"><tr><th class=\"roomtype\">Announcement</th></tr><td valign=\"top\">";
            //    foreach (FrontAnnoucement item in annoucementList)
            //    {
            //        annouceDisplay = annouceDisplay + item.Detail + "<br/>";
            //    }
            //    annouceDisplay = annouceDisplay + "</tr></td></table><br/><br/>";
            //}
            return annouceDisplay;

        }
    }
}