using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using System.Data.SqlClient;
using Hotels2thailand.ProductOption;
using System.IO;

/// <summary>
/// Summary description for ProductPriceMain
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class ProductPriceMain : Hotels2BaseClass
    {
        public int ProductID { get; set; }
        public short SupplierID { get; set; }
        public string Title { get; set; }
        public int OptionID { get; set; }
        public string OptionTitle { get; set; }
        public short OptionCateID { get; set; }
        public int ConditionID { get; set; }
        public bool IsExtranet { get; set; }
        public int IsBookNow { get; set; }
        public int PromotionID { get; set; }
        public string PromotionTitle { get; set; }
        public decimal Price { get; set; }
        public decimal PriceRack { get; set; }
        public decimal PriceDisplay { get; set; }
        public decimal NetPrice { get; set; }
        public bool HasAllotment { get; set; }
        public string PolicyDisplay { get; set; }
        public string PolicyContent { get; set; }
        public string CancellationDisplay { get; set; }
        public byte Breakfast { get; set; }
        public byte NumAdult { get; set; }
        public byte NumChild { get; set; }
        public byte NumExtra { get; set; }
        public byte Comission { get; set; }
        public string FilePath { get; set; }
        public string RoomImage { get; set; }
        public byte OptionPriority { get; set; }
        public byte ConditionPriority { get; set; }
        public string OptionPicture { get; set; }
        public string ProductCode { get; set; }
        public bool IsRoomShow { get; set; }
        public byte MarketID { get; set; }
        public string MemberBenefit { get; set; }
        public IList<OptionDayPrice> iListPricePerDayMain { get; set; }

        private int _productID;
        private DateTime _dateStart;
        private DateTime _dateEnd;
        private byte _langID;
        private bool _isExtranet;
        private byte _category;
        private byte manageID = 1;
        private bool HasTransfer=false;
        private bool HasExtraOption = false;
        private fnCurrency currency;
        private double discountPrice =0;
        private string refUrl = string.Empty;
        private string refQuery = string.Empty;
        private byte countryRef = 0;
        private List<FrontSupplementPriceQuantity> supplementList;
        private string _ipaddress = string.Empty;
        private byte refCountryID = 0;
        private decimal vatInclude = Convert.ToDecimal(1.177);
        FrontOptionPackage objPackage = null;
        List<FrontOptionPackage> objPackageList = null;
        FrontOptionMeal objMeal = null;
        List<FrontOptionMeal> objMealList = null;
        public byte CountryRef
        {
            set
            {
                countryRef = value;
            }
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
        public string IPAddress
        {
            set
            {
                _ipaddress=value;
                countryRef=Convert.ToByte(IPtoCounrty.GetCountryID(_ipaddress));
            }
        }
        private bool _memberAuthen = false;
 
        public bool memberAuthen
        {

            set
            {
                _memberAuthen = value;
            }
        }

        private int _memberID = 0;
        public int memberID
        {

            set
            {
                _memberID = value;
            }
        }

        public ProductPriceMain()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public ProductPriceMain(int productID, DateTime dateStart, DateTime dateEnd, byte langID)
        {
            _productID = productID;
            _dateStart = dateStart;
            _dateEnd = dateEnd;
            _langID = langID;

            string strCommand = string.Empty;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                strCommand = "select p.cat_id,p.extranet_active,pb.manage_id from tbl_product p,tbl_product_booking_engine pb where p.product_id=pb.product_id and p.product_id=" + _productID;
                SqlCommand cmd = new SqlCommand(strCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    _category = (byte)reader["cat_id"];
                    _isExtranet = (bool)reader["extranet_active"];    
                    manageID=(byte)reader["manage_id"];
                }
            }

            currency = new fnCurrency();
            currency.GetCurrency();
            
        }

       

        public List<ProductPriceMain> loadAll()
        {  
            List<ProductPriceMain> result = new List<ProductPriceMain>();
            ProductPrice objPrice = null;
            FrontProductPriceExtranet objPriceExtranet = null;
            OptionPrice objOptionPrice = new OptionPrice();
            ProductPolicy policy = null;
            List<ProductPolicy> policyList = null;
            FrontCancellationPolicy cancalation = null;
            List<FrontCancellationPolicy> cancellationList = null;

            ProductPolicyExtranet policyExtra = null;
            List<ProductPolicyExtranet> policyListExtra = null;
            List<CancellationExtranet> cancellationListExtra = null;
            CancellationExtranet cancelationExtra = null;
            Allotment allotment = new Allotment();
            
            string policyDisplay = string.Empty;
            string policyContentDisplay = string.Empty;

            decimal price = 0;
            decimal priceRack = 0;
            decimal priceNet = 0;
            decimal priceDisplay = 0;
            IList<OptionDayPrice> iListPricePerday = new List<OptionDayPrice>();
           

            int productTemp = 0;
            int promotionID = 0;
            string promotionTitle = string.Empty;
            string strCommand = string.Empty;
            byte categoryID = 0;
            string productTitleDefault = string.Empty;
            string optionTitleDefault = string.Empty;
            string filePath = string.Empty;
            string policyContent=string.Empty;
            

            //Check product is extranet
            

            if(!_isExtranet)
            {
                strCommand = "select * from fnConditionList2(" + _productID + "," + _dateStart.Hotels2DateToSQlString() + "," + _dateEnd.Hotels2DateToSQlString() + "," + _langID + ")";
                
            }else{
                strCommand = "select * from fnConditionExtranetList4(" + _productID + "," + _dateStart.Hotels2DateToSQlString() + "," + _dateEnd.Hotels2DateToSQlString() + ","+countryRef+"," + _langID + ")";
                
                objPackage = new FrontOptionPackage(_productID, _dateStart, _dateEnd);
               
            }

            //HttpContext.Current.Response.Write(strCommand+"<br>");
            //HttpContext.Current.Response.Flush();
            //HttpContext.Current.Response.End();
            string memberBenefit = string.Empty;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(strCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                //HttpContext.Current.Response.Write(strCommand);
                while (reader.Read())
                {
                    if ((int)reader["product_id"] != productTemp)
                    {
                        if ((bool)reader["extranet_active"])
                        {
                            
                            objPriceExtranet = new FrontProductPriceExtranet((int)reader["product_id"], _category, _dateStart, _dateEnd);
                            objPriceExtranet.IPAddress = _ipaddress;
                            objPriceExtranet.memberAuthen = _memberAuthen;
                            objPriceExtranet.LoadPrice();
                            discountPrice = objPriceExtranet.GetDiscountPrice;
                            refCountryID = objPriceExtranet.RefCountry;
                            cancelationExtra = new CancellationExtranet((int)reader["product_id"], _dateStart);
                            cancellationListExtra = cancelationExtra.GetCancellation();
                            policyExtra = new ProductPolicyExtranet(_category);
                            policyExtra.LangID = _langID;
                            policyExtra.ManageID = manageID;
                            policyListExtra = policyExtra.GetExtraPolicy((int)reader["product_id"], _langID);
                           
                        }
                        else
                        {
                            objPrice = new ProductPrice((int)reader["product_id"], _category, _dateStart, _dateEnd);
                            objPrice.LoadPrice();
                            discountPrice = objPrice.GetDiscountPrice;
                            policy = new ProductPolicy();
                            policy.LangID = _langID;
                            policy.DateCheck = _dateStart;
                            policyList = policy.GetProductPolicy((int)reader["product_id"]);
                            cancalation = new FrontCancellationPolicy((int)reader["product_id"], _dateStart);
                            cancellationList = cancalation.LoadCancellationPolicyByCondition();
                            if (_category == 31)
                            {
                                objPrice.TransferID = _productID;
                                objPrice.LoadTransferPrice();
                            }
                        }
                    }

                    

                    if ((bool)reader["extranet_active"])
                    {

                        
                        //HttpContext.Current.Response.Write(_ipaddress+"test");
                        objOptionPrice = objPriceExtranet.CalculateAll((int)reader["condition_id"], (int)reader["option_id"], (int)reader["promotion_id"]);
                        //HttpContext.Current.Response.Write(reader["condition_id"] + "--" + reader["option_id"] + "--" + reader["promotion_id"] + "s<br>");
                        price = (decimal)(Utility.PriceExcludeVat(objOptionPrice.Price));
                        priceRack = objOptionPrice.PriceRack;
                        priceNet = objOptionPrice.PriceOwn;
                        priceDisplay = objOptionPrice.PriceExcludeABF;

                        //(decimal)((double)price/currency.CurrencyPrefix)
                        //foreach (OptionDayPrice priceperday in objOptionPrice.iListPricePerDay)
                        //{
                        //    priceperday.PriceBase = (decimal)(((double)(Utility.PriceExcludeVat(priceperday.PriceBase))) / currency.CurrencyPrefix);
                        //    priceperday.PricePromotion = (decimal)(((double)(Utility.PriceExcludeVat(priceperday.PricePromotion))) / currency.CurrencyPrefix);
                        //    priceperday.PriceABF = (decimal)(((double)(Utility.PriceExcludeVat(priceperday.PriceABF))) / currency.CurrencyPrefix);
                        //}

                        iListPricePerday = objOptionPrice.iListPricePerDay;

                          //iListPricePerDayMain
                        //OptionDayPrice cPricePerday = new OptionDayPrice();
                        //HttpContext.Current.Response.Write(price+"<br/>");


                        List<string> promotionDetail = GetPromotionExtra((int)reader["promotion_id"], 1);
                        if ((byte)reader["breakfast"] > 0)
                        {
                            policyDisplay = "";
                            if(_langID==1)
                            {
                            policyContentDisplay = "<strong>Breakfast Included</strong><br/>";
                            }else{
                            policyContentDisplay = "<strong>รวมอาหารเช้า</strong><br/>";
                            }
                            
                        }
                        else
                        {
                            policyDisplay = "";
                            policyContentDisplay = "";
                        }

                        if (objPriceExtranet.CheckPromotionAccept((int)reader["promotion_id"]))
                        {
                            //promotionTitle = promotionDetail[0];
                            //policyDisplay = policy.GetConditionPolicyList(policyList, conditionID[conditionCount], promotionTitle, cancellationList);
                            policyDisplay = policyDisplay + policyExtra.GetConditionPolicyList(policyListExtra, (int)reader["condition_id"], reader["promotion_title"].ToString(), (byte)reader["breakfast"]);

                            if (bool.Parse(promotionDetail[1]))
                            {
                                //promotion has cancellation
                                policyContentDisplay = policyContentDisplay + policyExtra.GetPolicyContent(policyListExtra, cancellationListExtra, (int)reader["condition_id"], (int)reader["promotion_id"], reader["promotion_title"].ToString(), promotionDetail[2], bool.Parse(promotionDetail[1]));
                            }
                            else
                            {
                                policyContentDisplay = policyContentDisplay + policyExtra.GetPolicyContent(policyListExtra, cancellationListExtra, (int)reader["condition_id"], (int)reader["promotion_id"], reader["promotion_title"].ToString(), promotionDetail[2], bool.Parse(promotionDetail[1]));
                            }

                            promotionID = (int)reader["promotion_id"];
                            promotionTitle = reader["promotion_title"].ToString();
                        }
                        else
                        {
                            promotionID = 0;
                            promotionTitle = "";

                            policyDisplay = policyDisplay + policyExtra.GetConditionPolicyList(policyListExtra, (int)reader["condition_id"], "", (byte)reader["breakfast"]);

                            policyContentDisplay = policyContentDisplay + policyExtra.GetPolicyContent(policyListExtra, cancellationListExtra, (int)reader["condition_id"], 0, "", "", false);
                        }

                        //Response.Write(priceABF);

                        policyContent = policyContentDisplay;

                        policyDisplay = "<a href=\"javascript:void(0)\" class=\"tooltip\">" + policyDisplay;
                        policyDisplay = policyDisplay + "<span class=\"tooltip_content\">" + policyContentDisplay + "</span>";
                        policyDisplay = policyDisplay + "</a>";
                    }
                    else
                    {
                    //Contract Rate
                      


                        objOptionPrice = objPrice.CalculateAll((int)reader["condition_id"], (int)reader["option_id"], (int)reader["promotion_id"]);
                        price = objOptionPrice.Price;
                        priceRack = objOptionPrice.PriceRack;
                        priceNet = objOptionPrice.PriceOwn;
                        priceDisplay = objOptionPrice.PriceDisplay;
                        policyDisplay = "";
                        policyContentDisplay = "";

                        if (objPrice.CheckPromotionAccept((int)reader["promotion_id"]))
                        {
                            policyContentDisplay = policyContentDisplay + "<img src=\"http://www.booking2hotels.com/images/ico_special_offer.jpg\">&nbsp;<strong>Promotion:</strong><br/>" + GetPromotionTitle((int)reader["promotion_id"], _langID) + "<br/><br/>";
                            promotionID = (int)reader["promotion_id"];
                            promotionTitle = "<img src=\"http://www.booking2hotels.com/images/ico_special_offer.jpg\">&nbsp;<strong>Promotion:</strong><br/><span class=\"rate_promotion_detail\">" + reader["promotion_title"] + "</span><br/><br/>";
                        }
                        else
                        {
                            promotionID = 0;
                            promotionTitle = "";
                        }

                       
                        if ((byte)reader["breakfast"] > 0)
                        {
                            policyDisplay = "";
                            if(_langID==1)
                            {
                                policyContentDisplay = "<strong>Breakfast Included</strong><br/>";
                            }else{
                                policyContentDisplay = "<strong>รวมอาหารเช้า</strong><br/>";
                            }
                            
                        }
                        else
                        {
                            policyDisplay = "";
                            policyContentDisplay = "";
                        }


                        policyDisplay = policyDisplay + policy.GetConditionPolicyList(policyList, (int)reader["condition_id"], reader["promotion_title"].ToString(), cancellationList, (byte)reader["breakfast"]);
                        policyContentDisplay = policyContentDisplay + policy.GetPolicyContent(policyList, cancellationList, (int)reader["condition_id"]);
                        policyDisplay = "<div class=\"refun\"><a href=\"javascript:void(0)\" class=\"tooltip\">" + policyDisplay;
                        policyDisplay = policyDisplay + "<span class=\"tooltip_content\">" + promotionTitle + policyContentDisplay + "</span>";
                        policyDisplay = policyDisplay + "</a></div>";
                        policyContent = policyContentDisplay;
                    }

                    if(_langID==1)
                    {
                        productTitleDefault = reader["title"].ToString();
                        optionTitleDefault = reader["option_title"].ToString();
                        filePath = reader["file_path"].ToString();
                    }else{
                        productTitleDefault = reader["second_lang"].ToString();
                        optionTitleDefault = reader["option_second_lang"].ToString();
                        filePath = reader["file_path"].ToString().Replace(".asp","-th.asp");

                        if (string.IsNullOrEmpty(productTitleDefault))
                        {
                            productTitleDefault = reader["title"].ToString();
                        }
                        if (string.IsNullOrEmpty(optionTitleDefault))
                        {
                            optionTitleDefault = reader["option_title"].ToString();
                        }
                    }
                    
                    memberBenefit=objPriceExtranet.GetAllBenefit((int)reader["condition_id"]);
                    //HttpContext.Current.Response.Write(memberBenefit + "-" + (int)reader["condition_id"] + "-test<br>");
                    result.Add(new ProductPriceMain
                    {
                        ProductID = (int)reader["product_id"],
                        SupplierID = (short)reader["supplier_id"],
                        Title = productTitleDefault,
                        OptionID = (int)reader["option_id"],
                        OptionTitle = optionTitleDefault,
                        OptionCateID=(short)reader["cat_id"],
                        ConditionID = (int)reader["condition_id"],
                        IsExtranet = (bool)reader["isextranet"],
                        IsBookNow = (int)reader["is_book_now"],
                        PromotionID = promotionID,
                        PromotionTitle = promotionTitle,
                        // Price = (decimal)((double)price/currency.CurrencyPrefix),
                        //PriceRack = (decimal)((double)priceRack / currency.CurrencyPrefix),
                        Price = (decimal)((double)price),
                        PriceRack = (decimal)((double)priceRack),
                        PriceDisplay=priceDisplay,
                        NetPrice = priceNet,
                        HasAllotment = allotment.CheckAllotAvaliable((short)reader["supplier_id"], (int)reader["option_id"], 1, _dateStart, _dateEnd),
                        PolicyDisplay = policyDisplay,
                        PolicyContent=policyContent,
                        Breakfast = (byte)reader["breakfast"],
                        NumAdult = (byte)reader["num_adult"],
                        NumChild = (byte)reader["num_children"],
                        NumExtra = (byte)reader["num_extra"],
                        Comission = 0,
                        FilePath = filePath,
                        RoomImage = reader["room_image"].ToString(),
                        OptionPriority = (byte)reader["option_priority"],
                        ConditionPriority = (byte)reader["condition_priority"],
                        ProductCode=reader["product_code"].ToString(),
                        OptionPicture=reader["picture"].ToString(),
                        IsRoomShow=(bool)reader["isshow"],
                        MarketID=(byte)reader["market_id"],
                        MemberBenefit = memberBenefit,
                        iListPricePerDayMain = iListPricePerday

                    });

                    productTemp = (int)reader["product_id"];

                }

                objPackage = new FrontOptionPackage(_productID, _dateStart, _dateEnd);
                objPackageList = objPackage.GetPackageList();
                objMeal = new FrontOptionMeal(_productID, _dateStart, _dateEnd);
                objMealList = objMeal.GetMealList();

                
                cancelationExtra = new CancellationExtranet(_productID, _dateStart);
                cancellationListExtra = cancelationExtra.GetCancellation();
                policyExtra = new ProductPolicyExtranet(_category);
                policyExtra.LangID = _langID;
                policyListExtra = policyExtra.GetExtraPolicy(_productID, _langID);

                foreach (FrontOptionPackage item in objPackageList)
                {
                           if (item.Breakfast > 0)
                            {
                                policyDisplay = "";
                                if (_langID == 1)
                                {
                                    policyContentDisplay = "<strong>Breakfast Included</strong><br/>";
                                }
                                else
                                {
                                    policyContentDisplay = "<strong>รวมอาหารเช้า</strong><br/>";
                                }

                            }
                            else
                            {
                                policyDisplay = "";
                                policyContentDisplay = "";
                            }

                            //policyDisplay = policyDisplay + policyExtra.GetConditionPolicyList(policyListExtra, item.ConditionID, "", item.Breakfast);
                           policyDisplay = policyDisplay + policyExtra.GetConditionPolicyPackage(item.ConditionTitle,item.Breakfast);
                            policyContentDisplay = policyContentDisplay + policyExtra.GetPolicyContent(policyListExtra, cancellationListExtra, item.ConditionID, 0, "", "", false);
                            policyContent = policyContentDisplay;

                            policyDisplay = "<a href=\"javascript:void(0)\" class=\"tooltip\">" + policyDisplay;
                            policyDisplay = policyDisplay + "<span class=\"tooltip_content\">" + policyContentDisplay + "</span>";
                            policyDisplay = policyDisplay + "</a>";
                            item.PolicyDisplay = policyDisplay;
                            item.PolicyContent = policyContent;
                }
            }
            //foreach (ProductPriceMain item in result)
            //{
            //    HttpContext.Current.Response.Write(item.Price + "<br>");
            //}
            
            return result; 
        }

        public string GetPromotionTitle(int PromotionID, byte langID)
        {
            string result = string.Empty;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                string sqlCommand = "select top 1 pc.title from tbl_promotion pm,tbl_promotion_content pc";
                sqlCommand = sqlCommand + " where pm.promotion_id=pc.promotion_id  and pc.lang_id=1 and pc.promotion_id=" + PromotionID;
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    result = reader["title"].ToString();
                }
                return result;
            }

        }

        public List<string> GetPromotionExtra(int PromotionID, byte langID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                List<string> result = new List<string>();
                string sqlCommand = "select top 1 pmc_ex.title,pm_ex.iscancellation,pmc_ex.detail";
                sqlCommand = sqlCommand + " from tbl_promotion_extra_net pm_ex,tbl_promotion_content_extra_net pmc_ex";
                sqlCommand = sqlCommand + " where pm_ex.promotion_id=pmc_ex.promotion_id and pmc_ex.lang_id="+langID+" and pm_ex.promotion_id=" + PromotionID;
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    result.Add(reader["title"].ToString());
                    result.Add(reader["iscancellation"].ToString());
                    result.Add(reader["detail"].ToString());
                }
                return result;
            }

        }

        private string RenderDropDownQuantity(string strName,string optionValue,int minNum,int maxNum,int numDefault,int displayType)
        {
            string result = string.Empty;
            string className = string.Empty;

            switch (displayType)
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
            //HttpContext.Current.Response.Write(strName+"--"+className+"--"+displayType+"<br>");
            if (displayType!=0)
            {
                result = "<select name=\"" + strName + "\" " + className + " >\n";
                //result = "<select name=\"" + strName + "\" class=\"" + strName + "\">\n";
            }else{
                result = "<select name=\"" + strName + "\" class=\"" + strName + "\" id=\"" + strName + "\">\n";

            }
            

            //if (displayType == 1)
            //{
            //    result = "<select name=\"" + strName + "\" class=\"" + strName + "\">\n";
            //}
            //else {
            //    result = "<select name=\"" + strName + "\" class=\"" + strName + "\">\n";
            //}
            
            

            if(displayType!=0)
            {
                for (int countNum = minNum; countNum <= maxNum; countNum++)
                {
                    result = result + "<option value=\"" + optionValue + "_" + countNum + "\">" + countNum + "</option>\n";
                }
            }else{
                for (int countNum = minNum; countNum <= maxNum; countNum++)
                {
                    result = result + "<option value=\"" + countNum + "\">" + countNum + "</option>\n";
                }
            }
            
            result = result + "</select>\n";
            return result;
        }

        public decimal GetLowestProductPrice(int productID, DateTime dateStart, DateTime dateEnd)
        {
           
            _productID = productID;
            _dateStart = dateStart;
            _dateEnd = dateEnd;
            _langID = 1;

            string strCommand = string.Empty;
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                strCommand = "select cat_id,extranet_active from tbl_product where product_id=" + _productID;
                SqlCommand cmd = new SqlCommand(strCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    _category = (byte)reader["cat_id"];
                    _isExtranet = (bool)reader["extranet_active"];

                }
            }

            currency = new fnCurrency();
            currency.GetCurrency();
            this.IPAddress = "110.171.168.232";
            List<ProductPriceMain> productList = this.loadAll();
            
            decimal result=0;
            productList = productList.Where(x => x.Price > 0).ToList();
            switch (_category)
            {
                case 29:
                    productList = productList.OrderBy(x => x.OptionPriority).ThenBy(x => x.Price).ToList();
                    //return productList[0].Price;
                    
                    break;
                case 32:
                    productList = productList.OrderBy(x => x.OptionPriority).ThenBy(x => x.ConditionPriority).ThenBy(x => x.Price).ToList();
                    break;
                case 34:
                case 36:
                    productList = productList.OrderBy(x => x.OptionPriority).ThenBy(x => x.ConditionPriority).ThenBy(x => x.Price).ToList();
                    break;
                case 38:
                case 39:
                case 40:
                    productList = productList.OrderBy(x => x.OptionPriority).ToList();
                    break;
            }

            

            if(productList.Count>0)
            {
                result = productList[0].Price;
            }
            
            return result;
        }

        public string RenderConditionList(List<ProductPriceMain> productList)
        {
            string result="";
            result = RenderAnnoucement();
            switch(_category)
            {
                case 29:
                    productList = productList.OrderBy(x => x.OptionPriority).ThenBy(x => x.Price).ToList();
                    result = result + RenderHotelList(productList);
                break;
                case 32:
                    productList = productList.OrderBy(x => x.OptionPriority).ThenBy(x => x.ConditionPriority).ThenBy(x => x.Price).ToList();
                    result = result + RenderGolfList(productList);
                    break;
                case 34: case 36:
                    productList = productList.OrderBy(x => x.OptionPriority).ThenBy(x => x.ConditionPriority).ThenBy(x => x.Price).ToList();
                    result = result + RenderDayTripList(productList);
                    break;
                case 38:case 39: case 40:
                    productList = productList.OrderBy(x => x.OptionPriority).ThenBy(x => x.ConditionPriority).ToList();
                    result = result + RenderOtherProductList(productList);
                    break;
            }
            return result;
        }

        
        public string RenderHotelList(List<ProductPriceMain> productList)
        {
            //ProductPriceMain objtest = new ProductPriceMain();
            //List<ProductPriceMain> results = objtest.loadAll(pro, dateStart, dateEnd, 1, true);
            string productDisplay = string.Empty;
            Allotment objAllotment = null;
            objAllotment = new Allotment(ProductID);

            List<ProductPriceMain> roomList = productList.Where(x => x.Price > 0).ToList();

            
            string guaranteeRate = string.Empty;
            string guaranteeContent = string.Empty;
            
            if (_langID == 1)
            {
                guaranteeContent = "We guarantee the lowest rate. If you find the lower rate than us, please inform us within 24 hrs. We're pleased to pay 2x discount from different  value.";
            }
            else {
                guaranteeContent = "เราการันตีราคาถูกที่สุด หากพบราคาต่ำกว่านี้โปรดติดต่อเราภายใน 24ชั่วโมง เรายินดีคืนเงินจากส่วนต่างสองเท่า";
            }
             guaranteeRate=guaranteeRate+"<tr>";
             guaranteeRate = guaranteeRate + "<td colspan=\"6\" style=\"font-size:14px;line-height:18px;height:100px;background:url(../images/lowest2.gif) no-repeat 10px 10px #FFFFFF; padding-left:120px;\">\n";
             guaranteeRate=guaranteeRate+"<div style=\"font-size:12px;\" align=\"right\">\n";
             guaranteeRate=guaranteeRate+"<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\n";
             guaranteeRate=guaranteeRate+"<tr>\n";
             guaranteeRate=guaranteeRate+"<td valign=\"top\">\n";
             guaranteeRate=guaranteeRate+"<span style=\"font-family:helvetica; font-size:24px; color:#069; line-height:40px;\">100% Lowest Guarantee</span>\n";
             guaranteeRate=guaranteeRate+"<p style=\"margin-top:0px\" class=\"fnBlack12\">"+guaranteeContent+"</p>\n";
             guaranteeRate=guaranteeRate+"</td>\n";
             guaranteeRate = guaranteeRate + "<td width=\"255\" style=\"text-align:right;\" valign=\"top\"><img src=\"../images/ico_ceo_guarantee.png\" style=\"margin-right:10px\"/>\n";
             guaranteeRate=guaranteeRate+"<img src=\"../images/sign_ceo.gif\" width=\"136\" height=\"78\" />\n";
             guaranteeRate=guaranteeRate+"<span  class=\"fnBlack12\">Mr.Tanit Bordisorn<br />\n";
             guaranteeRate=guaranteeRate+" Chief executive officer</span></td></tr>\n";
             guaranteeRate=guaranteeRate+"</table>\n";   
             guaranteeRate=guaranteeRate+"</div>\n";     
             guaranteeRate=guaranteeRate+"</td>\n";
             guaranteeRate=guaranteeRate+"<tr>\n";

                if (roomList.Count > 0)
            {
                List<FrontMarket> marketList = new List<FrontMarket>();
                FrontMarket market;
                string conditionGroup = string.Empty;

                foreach (ProductPriceMain items in roomList)
                {
                    if (items.OptionCateID == 38)
                    {
                        conditionGroup = conditionGroup + items.ConditionID + ",";
                    }
                }


                conditionGroup = conditionGroup.Substring(0, conditionGroup.Length - 1);
                market = new FrontMarket();
                    if(!_isExtranet)
                    {
                        marketList = market.getMarketCountry(conditionGroup);
                    }
                

                productDisplay = productDisplay + "<div id=\"errorRoom\" class=\"errorMsg\"></div>\n";
                productDisplay = productDisplay + "<table class=\"tblListResult\" cellpadding=\"0\" cellspacing=\"1\" align=\"center\">\n";
                    if(_langID==1)
                    {
                        productDisplay = productDisplay + "<tr><th>Room Type</th><th>Max</th><th width=\"220\">Condition</th><th width=\"150\">Avg. Rate/Night</th><th width=\"80\">No.Room</th></tr>\n";
                    }else{
                        productDisplay = productDisplay + "<tr><th>ชนิดของห้องพัก</th><th>จำนวนผู้เข้าพัก</th><th width=\"220\">เงื่อนไข</th><th width=\"150\">ราคาเฉลี่ยต่อคืน</th><th width=\"80\">จำนวนห้อง</th></tr>\n";
                    }
                
                int OptionTemp = 0;
                int ProductTemp = 0;
                int conditionTemp = 0;
                int RowSpan = 0;
                int intNight = _dateEnd.Subtract(_dateStart).Days;

                string ico_available = "";
                decimal priceDisplay = 0;
                decimal priceSeiling = 0;
                decimal priceNet = 0;
                decimal grandPrice = 0;
                string ico_product_type = "";
                int qtyProduct = 1;

                string ddQuantity = string.Empty;
                string adultMax = string.Empty;
                string roomImage = string.Empty;
                string urlRoomDetail = string.Empty;
                string optionTitle = string.Empty;
                string marketPolicy = string.Empty;
                string textAvailable = string.Empty;
                    if(roomList.Count>0)
                    {
                        if ((roomList[0].ProductID == 1918 || roomList[0].ProductID == 624) && discountPrice>0)
                        {
                            productDisplay = productDisplay + guaranteeRate;
                        }
                        
                   
                    }
                foreach (ProductPriceMain item in roomList)
                {
                    textAvailable = "";
                    marketPolicy = "";

                    optionTitle = item.OptionTitle;

                    if (item.IsRoomShow)
                    {
                        urlRoomDetail = item.FilePath.Replace(".asp", "_room_" + item.OptionID + ".asp");
                        optionTitle = "<a href=\"" + urlRoomDetail + "\" target=\"_blank\"  class=\"room_rate_title\">" + item.OptionTitle + "</a>";
                    }
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
                        roomImage = "<a href=\"" + urlRoomDetail + "\"><img src=\"" + roomImage + "\"  class=\"small_room_img\"></a>";
                    }

                    if(item.OptionCateID==38)
                    {
                        if (marketList.Count != 0)
                        {
                            if(_langID==1)
                            {
                                marketPolicy = marketPolicy + "<a href=\"#\" class=\"tooltip\"><div class=\"indicatorRate\">Recheck your country rate</div><span class=\"tooltip_content\">";
                            }else{
                                marketPolicy = marketPolicy + "<a href=\"#\" class=\"tooltip\"><div class=\"indicatorRate\">ราคานี้ใช้ได้สำหรับบางประเทศ</div><span class=\"tooltip_content\">";
                            }
                            
                            marketPolicy = marketPolicy + RenderMarketDisplay(marketList, item.MarketID, item.ConditionID);

                        }
                    }

                    if (item.OptionCateID != 39 && item.OptionCateID != 40 && item.OptionCateID != 43 && item.OptionCateID != 44)
                    {
                        priceDisplay = (int)(item.PriceDisplay / intNight);
                        grandPrice = (item.Price * vatInclude) * qtyProduct;
                        priceSeiling = grandPrice / intNight / qtyProduct;
                        ico_product_type = "";
                        

                        

                        if (item.IsExtranet)
                        {
                            priceNet = (decimal)((double)item.NetPrice * (1 - ((double)item.Comission / 100)));
                            priceNet = priceNet / intNight;
                        }
                        else
                        {
                            priceNet = item.NetPrice / intNight;
                        }

                        if (item.NumAdult > 3)
                        {
                            adultMax = "<img src=\"../theme_color/blue/images/icon/adult.png\" />x" + item.NumAdult;
                        }
                        else
                        {
                            adultMax = "<img src=\"../theme_color/blue/images/icon/ico_adult_" + item.NumAdult + ".png\" />";
                        }

                        //HttpContext.Current.Response.Write(item.SupplierID + "---" + item.OptionID + "---1---" + _dateStart + "---" + _dateEnd+"<br>");
                        if (!string.IsNullOrEmpty(objAllotment.CheckAllotAvaliable_Cutoff(item.SupplierID, item.OptionID, 1, _dateStart, _dateEnd)))
                        {
                            //HttpContext.Current.Response.Write(objAllotment.CheckAllotAvaliable_Cutoff(item.SupplierID, item.OptionID, 1, _dateStart, _dateEnd) + "<br>");
                            if(_isExtranet)
                            {
                                textAvailable = "<br/><span class=\"avai_14\">Available Now!</span>";
                            }
                            
                        }
                        ddQuantity = RenderDropDownQuantity("ddPrice", item.ConditionID + "_" + item.OptionID + "_" + item.PromotionID + "_" + item.NumAdult + "_" + item.NumChild + "_" + item.NumExtra, 0, 20, 0, item.OptionCateID);
                        
                        if (item.OptionID != OptionTemp)
                        {
                           

                            if (item.IsExtranet)
                            {
                                ico_available = "<img src=\"/images/available.png\"/>";
                            }
                            else
                            {
                                ico_available = "";
                            }
                            productDisplay = productDisplay.Replace("###rowSpan###", RowSpan.ToString());
                            if(_langID==1)
                            {
                            productDisplay = productDisplay + "<tr><td rowspan=\"###rowSpan###\" valign=\"top\">" + roomImage + optionTitle + "</td><td class=\"align_center\"><a href=\"javascript:void(0)\" class=\"tooltip\">" + adultMax + "<span class=\"tooltip_content\">Standard Occupancy: " + item.NumAdult + "</span></a></td><td>" + item.PolicyDisplay + marketPolicy + "</td><td align=\"center\" class=\"rowEven fn16bGreen\">" + currency.CurrencyCode + " " + String.Format("{0:#,0}", priceDisplay) + textAvailable + "</td><td class=\"align_center\">" + ddQuantity + "</td></tr>\n";
                            }else{
                            productDisplay = productDisplay + "<tr><td rowspan=\"###rowSpan###\" valign=\"top\">" + roomImage + optionTitle + "</td><td class=\"align_center\"><a href=\"javascript:void(0)\" class=\"tooltip\">" + adultMax + "<span class=\"tooltip_content\">สามารถอยู่ได้: " + item.NumAdult + " คน</span></a></td><td>" + item.PolicyDisplay + marketPolicy + "</td><td align=\"center\" class=\"rowEven fn16bGreen\">" + currency.CurrencyCode + " " + String.Format("{0:#,0}", priceDisplay) + textAvailable + "</td><td class=\"align_center\">" + ddQuantity + "</td></tr>\n";
                            }
                            
                            RowSpan = 1;
                        }
                        else
                        {
                            if (item.ConditionID!=conditionTemp)
                            {
                                if(_langID==1)
                                {
                                productDisplay = productDisplay + "<tr><td class=\"align_center\"><a href=\"javascript:void(0)\" class=\"tooltip\">" + adultMax + "<span class=\"tooltip_content\">Standard Occupancy: " + item.NumAdult + "</span></a></td><td>" + item.PolicyDisplay + marketPolicy + "</td><td align=\"center\" class=\"rowEven fn16bGreen\">" + currency.CurrencyCode + " " + String.Format("{0:#,0}", priceDisplay) + textAvailable + "</td><td class=\"align_center\">" + ddQuantity + "</td></tr>\n";
                                }else{
                                productDisplay = productDisplay + "<tr><td class=\"align_center\"><a href=\"javascript:void(0)\" class=\"tooltip\">" + adultMax + "<span class=\"tooltip_content\">สามารถอยู่ได้: " + item.NumAdult + "คน</span></a></td><td>" + item.PolicyDisplay + marketPolicy + "</td><td align=\"center\" class=\"rowEven fn16bGreen\">" + currency.CurrencyCode + " " + String.Format("{0:#,0}", priceDisplay) + textAvailable + "</td><td class=\"align_center\">" + ddQuantity + "</td></tr>\n";
                                }
                                
                                RowSpan = RowSpan + 1;
                            }
                            
                            
                        }
                        OptionTemp = item.OptionID;
                        conditionTemp = item.ConditionID;
                        ProductTemp = item.ProductID;
                    }

                }
                productDisplay = productDisplay.Replace("###rowSpan###", RowSpan.ToString());
                productDisplay = productDisplay + RenderHiddenField(productList[0].ProductID, productList[0].SupplierID, _category, _dateStart, _dateEnd, currency.CurrencyID);
                productDisplay = productDisplay + RenderExtraOptionList(productList);
                productDisplay = productDisplay + RenderGuestBox();
                productDisplay = productDisplay + "</table>\n";
                //productDisplay = productDisplay + "</div>\n";
                
                FrontPayLater payLater = new FrontPayLater();
                string payLaterPlan = payLater.GetPayLaterPlanByDate(ProductID, _dateStart);
                if (string.IsNullOrEmpty(payLaterPlan))
                {
                    if(_langID==1)
                    {
                        productDisplay = productDisplay + "<div class=\"row_rate\"><a href=\"javascript:void(0)\" class=\"tooltip\"><img src=\"/theme_color/blue/images/icon/high-low.jpg\" class=\"lowrates\" title=\"Low Rates Guarantee\" /><span class=\"tooltip_content\">";
                        productDisplay = productDisplay + "<p><strong>Low Rate Guarantee</strong> - If you find a lower rate within 24 hours of booking, then, at a minimum, they will match that rate.</p><br />\n";
                        productDisplay = productDisplay + "<p><strong>High Security Website</strong> - Our payment system is certified by Thai Bank in usage of security version SSL 256 Bit encryption , the most modern technological security system using nowadays websites in the world.</p>\n";

                    }else{
                        productDisplay = productDisplay + "<div class=\"row_rate\"><a href=\"javascript:void(0)\" class=\"tooltip\"><img src=\"/theme_color/blue/images/icon/high-low-th.jpg\" class=\"lowrates\" title=\"Low Rates Guarantee\" /><span class=\"tooltip_content\">";
                        productDisplay = productDisplay + "<p><strong>รับประกันถูกที่สุด</strong> - หากท่านพบว่าราคาที่อื่นถูกกว่า ท่านสามารถเเจ้งกลับทางเราได้ภายใน 24 ชม นับจากที่ท่านจองกับเรา เราจะคืนส่วนต่างให้กับท่านทันที</p><br />\n";
                        productDisplay = productDisplay + "<p><strong>ระบบการชำระเงินออนไลน์ที่มีความปลอดภัยสูง</strong> - เวปไซต์โฮเทลทูเลือกใช้ระบบ SSL 256 Bit ซึ่งถือได้ว่าเป็นระบบเทคโนโลยีที่ทันสมัยที่สุดทางด้านความปลอดภัยบนเวปไซต์ </p>\n";

                    }

                }
                else
                {
                    productDisplay = productDisplay + "<div class=\"row_rate\"><a href=\"javascript:void(0)\" class=\"tooltip\"><img src=\"/theme_color/blue/images/icon/high-low_paylater.jpg\" class=\"lowrates\" title=\"Low Rates Guarantee\" /><span class=\"tooltip_content\">";
                    //productDisplay = productDisplay + "<p>"+payLaterPlan+"</p>\n";
                    productDisplay = productDisplay + "<p><strong>Book Now Pay Later</strong> - Book Today, Pay Later Just $1 to secure your rooms / products today and pay the rest at date that is closure to check in</p><br/>";
                    productDisplay = productDisplay + "<p><strong>Low Rate Guarantee</strong> - If you find a lower rate within 24 hours of booking, then, at a minimum, they will match that rate.</p><br/>";
                    productDisplay = productDisplay + "<p><strong>High Security Website</strong> - Our payment system is certified by Thai Bank in usage of security version SSL 256 Bit encryption , the most modern technological security system using nowadays websites in the world.</p>";


                }


                productDisplay = productDisplay + "</span></a></div>\n";
                    if(_langID==1)
                    {
                        productDisplay = productDisplay + "<div class=\"incase\"><p>Hotels2thailand.com is legally allowed to sell tourism products on the internet, authorized from Tourism Authority of Thailand (TAT)</p></div>\n";
                        productDisplay = productDisplay + "<div class=\"take_time\">It only takes 2 minutes!</div>\n";
                    }else{
                        productDisplay = productDisplay + "<div class=\"incase\"><p>เวปไซต์โฮเทลทูไทยแลนด์ได้รับอนุญาติให้เป็นเวปไซต์ผู้ประกอบการท่องเที่ยวโดยถูกต้องตามกฏหมาย</p></div>\n";
                        productDisplay = productDisplay + "<div class=\"take_time\">ใช้เวลาเพียงแค่ 2 นาทีเท่านั้น!</div>\n";
                    }
                
                productDisplay = productDisplay + "<div class=\"booknow\"><a href=\"#\" id=\"btnBooking\"></a>\n";
                productDisplay = productDisplay + "<br class=\"clear-all\" /><br /><br />\n";
            }
            else {
                productDisplay = productDisplay + "<div style=\"border: 2px solid rgb(255, 204, 102); margin: 7px; padding: 10px;\">";
                productDisplay = productDisplay + "<font color=\"red\">";
                    if(_langID==1)
                    {
                    productDisplay = productDisplay + "Sorry. Rate for selected period is not available. Please kindly contact to <a href=\"mailto:reservation@hotels2thailand.com\">reservation@hotels2thailand.com</a>";
                    }else{
                        productDisplay = productDisplay + "ไม่พบราคาสำหรับโรงแรมนี้กรุณาติดต่อ<a href=\"mailto:reservation@hotels2thailand.com\">reservation@hotels2thailand.com</a>";
                    
                    }
                
                productDisplay = productDisplay + "</font>";
                productDisplay = productDisplay + "</div>"; 
            }

            
            return productDisplay;
        }

        public string RenderGolfList(List<ProductPriceMain> productList)
        {
            //ProductPriceMain objtest = new ProductPriceMain();
            //List<ProductPriceMain> results = objtest.loadAll(pro, dateStart, dateEnd, 1, true);
            string productDisplay = string.Empty;
            productList = productList.Where(x => x.Price > 0).ToList();
            if (productList.Count > 0)
            {

                productDisplay = productDisplay + "<div id=\"errorRoom\" class=\"errorMsg\"></div>\n";
                productDisplay = productDisplay + "<table class=\"tblListResult\" cellpadding=\"0\" cellspacing=\"1\" align=\"center\">\n";
                productDisplay = productDisplay + "<tr><th>Option</th><th width=\"220\">Condition</th><th width=\"150\">Rate/Pax</th><th width=\"80\">Quantity</th></tr>\n";
                int OptionTemp = 0;
                int ProductTemp = 0;
                int ConditionTemp = 0;
                int RowSpan = 0;
                int intNight = _dateEnd.Subtract(_dateStart).Days;

                string ico_available = "";
                decimal priceDisplay = 0;
                decimal priceSeiling = 0;
                decimal priceNet = 0;
                decimal grandPrice = 0;
                string ico_product_type = "";
                int qtyProduct = 1;

                string ddQuantity = string.Empty;
                string adultMax = string.Empty;
                string roomImage = string.Empty;
                string urlRoomDetail = string.Empty;
                string optionTitle = string.Empty;

                foreach (ProductPriceMain item in productList)
                {

                    optionTitle = item.OptionTitle;

                    //if (item.IsRoomShow)
                    //{
                    //    urlRoomDetail = item.FilePath.Replace(".asp", "_room_" + item.OptionID + ".asp");
                    //    optionTitle = "<a href=\"" + urlRoomDetail + "\" target=\"_blank\"  class=\"room_rate_title\">" + item.OptionTitle + "</a>";
                    //}
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
                        roomImage = "<a href=\"/" + urlRoomDetail + "\"><img src=\"" + roomImage + "\"  class=\"small_room_img\"></a>";
                    }

                    

                    if (item.OptionCateID != 39 && item.OptionCateID != 40 && item.OptionCateID != 43 && item.OptionCateID != 44)
                    {
                        priceDisplay = item.Price / intNight;
                        grandPrice = (item.Price * vatInclude) * qtyProduct;
                        priceSeiling = grandPrice / intNight / qtyProduct;
                        ico_product_type = "";
                        ddQuantity = RenderDropDownQuantity("ddPrice", item.ConditionID + "_" + item.OptionID + "_" + item.PromotionID + "_" + item.NumAdult + "_" + item.NumChild + "_" + item.NumExtra, 0, 20, 0, item.OptionCateID);
                        if (item.IsExtranet)
                        {
                            priceNet = (decimal)((double)item.NetPrice * (1 - ((double)item.Comission / 100)));
                            priceNet = priceNet / intNight;
                        }
                        else
                        {
                            priceNet = item.NetPrice / intNight;
                        }

                        if (item.NumAdult > 3)
                        {
                            adultMax = "<img src=\"../theme_color/blue/images/icon/adult.png\" />x" + item.NumAdult;
                        }
                        else
                        {
                            adultMax = "<img src=\"../theme_color/blue/images/icon/ico_adult_" + item.NumAdult + ".png\" />";
                        }

                        if (item.OptionID != OptionTemp)
                        {
                            if (item.IsExtranet)
                            {
                                ico_available = "<img src=\"/images/available.png\"/>";
                            }
                            else
                            {
                                ico_available = "";
                            }
                            productDisplay = productDisplay.Replace("###rowSpan###", RowSpan.ToString());
                            productDisplay = productDisplay + "<tr><td rowspan=\"###rowSpan###\" valign=\"top\">" + roomImage + optionTitle + "</td><td>" + item.PolicyDisplay + "</td><td align=\"center\" class=\"rowEven fn16bGreen\">" + currency.CurrencyCode + " " + String.Format("{0:#,0}", priceDisplay) + "</td><td class=\"align_center\">" + ddQuantity + "</td></tr>\n";
                            RowSpan = 1;
                        }
                        else
                        {
                            if(item.ConditionID!=ConditionTemp)
                            {
                                productDisplay = productDisplay + "<tr><td>" + item.PolicyDisplay + "</td><td align=\"center\" class=\"rowEven fn16bGreen\">" + currency.CurrencyCode + " " + String.Format("{0:#,0}", priceDisplay) + "</td><td class=\"align_center\">" + ddQuantity + "</td></tr>\n";
                                RowSpan = RowSpan + 1;
                            }
                            
                        }
                        OptionTemp = item.OptionID;
                        ConditionTemp = item.ConditionID;
                        ProductTemp = item.ProductID;
                    }

                }
                productDisplay = productDisplay.Replace("###rowSpan###", RowSpan.ToString());
                productDisplay = productDisplay + RenderHiddenField(productList[0].ProductID, productList[0].SupplierID, _category, _dateStart, _dateEnd, currency.CurrencyID);
                productDisplay = productDisplay + RenderGuestBox();

                productDisplay = productDisplay + "</table>\n";
                //productDisplay = productDisplay + "</div>\n";

                FrontPayLater payLater = new FrontPayLater();
                string payLaterPlan = payLater.GetPayLaterPlanByDate(ProductID, _dateStart);
                if (string.IsNullOrEmpty(payLaterPlan))
                {
                    productDisplay = productDisplay + "<div class=\"row_rate\"><a href=\"javascript:void(0)\" class=\"tooltip\"><img src=\"/theme_color/blue/images/icon/high-low.jpg\" class=\"lowrates\" title=\"Low Rates Guarantee\" /><span class=\"tooltip_content\">";
                    productDisplay = productDisplay + "<p><strong>Low Rate Guarantee</strong> - If you find a lower rate within 24 hours of booking, then, at a minimum, they will match that rate.</p><br />\n";
                    productDisplay = productDisplay + "<p><strong>High Security Website</strong> - Our payment system is certified by Thai Bank in usage of security version SSL 256 Bit encryption , the most modern technological security system using nowadays websites in the world.</p>\n";

                }
                else
                {
                    productDisplay = productDisplay + "<div class=\"row_rate\"><a href=\"javascript:void(0)\" class=\"tooltip\"><img src=\"/theme_color/blue/images/icon/high-low_paylater.jpg\" class=\"lowrates\" title=\"Low Rates Guarantee\" /><span class=\"tooltip_content\">";
                    //productDisplay = productDisplay + "<p>"+payLaterPlan+"</p>\n";
                    productDisplay = productDisplay + "<p><strong>Book Now Pay Later</strong> - Book Today, Pay Later Just $1 to secure your rooms / products today and pay the rest at date that is closure to check in</p><br/>";
                    productDisplay = productDisplay + "<p><strong>Low Rate Guarantee</strong> - If you find a lower rate within 24 hours of booking, then, at a minimum, they will match that rate.</p><br/>";
                    productDisplay = productDisplay + "<p><strong>High Security Website</strong> - Our payment system is certified by Thai Bank in usage of security version SSL 256 Bit encryption , the most modern technological security system using nowadays websites in the world.</p>";


                }


                productDisplay = productDisplay + "</span></a></div>\n";
                productDisplay = productDisplay + "<div class=\"incase\"><p>Hotels2thailand.com is legally allowed to sell tourism products on the internet, authorized from Tourism Authority of Thailand (TAT)</p></div>\n";
                productDisplay = productDisplay + "<div class=\"take_time\">It only takes 2 minutes!</div>\n";
                productDisplay = productDisplay + "<div class=\"booknow\"><a href=\"#\" id=\"btnBooking\"></a>\n";
                productDisplay = productDisplay + "<br class=\"clear-all\" /><br /><br />\n";
            }
            else
            {
                productDisplay = productDisplay + "<div style=\"border: 2px solid rgb(255, 204, 102); margin: 7px; padding: 10px;\">";
                productDisplay = productDisplay + "<font color=\"red\">";
                productDisplay = productDisplay + "Sorry. Rate for selected period is not available. Please kindly contact to <a href=\"mailto:reservation@hotels2thailand.com\">reservation@hotels2thailand.com</a>";
                productDisplay = productDisplay + "</font>";
                productDisplay = productDisplay + "</div>";
            }


            return productDisplay;
        }

        public string RenderOtherProductList(List<ProductPriceMain> productList)
        {
            //ProductPriceMain objtest = new ProductPriceMain();
            //List<ProductPriceMain> results = objtest.loadAll(pro, dateStart, dateEnd, 1, true);
            string productDisplay = string.Empty;
            productList = productList.Where(x => x.Price > 0).ToList();
            if (productList.Count > 0)
            {

                productDisplay = productDisplay + "<div id=\"errorRoom\" class=\"errorMsg\"></div>\n";
                productDisplay = productDisplay + "<table class=\"tblListResult\" cellpadding=\"0\" cellspacing=\"1\" align=\"center\">\n";
                if(_langID==1)
                {
                productDisplay = productDisplay + "<tr><th>Option</th><th width=\"220\">Condition</th><th width=\"150\">Rate/Pax</th><th width=\"80\">Quantity</th></tr>\n";
                }else{
                productDisplay = productDisplay + "<tr><th>โปรแกรม</th><th width=\"220\">เงื่อนไข</th><th width=\"150\">ราคาต่อคน</th><th width=\"80\">จำนวน</th></tr>\n";
                }
                
                int OptionTemp = 0;
                int ProductTemp = 0;
                int ConditionTemp = 0;
                int RowSpan = 0;
                int intNight = _dateEnd.Subtract(_dateStart).Days;

                string ico_available = "";
                decimal priceDisplay = 0;
                decimal priceSeiling = 0;
                decimal priceNet = 0;
                decimal grandPrice = 0;
                string ico_product_type = "";
                int qtyProduct = 1;

                string ddQuantity = string.Empty;
                string adultMax = string.Empty;
                string roomImage = string.Empty;
                string urlRoomDetail = string.Empty;
                string optionTitle = string.Empty;

                foreach (ProductPriceMain item in productList)
                {

                    optionTitle = item.OptionTitle;

                    //if (item.IsRoomShow)
                    //{
                    //    urlRoomDetail = item.FilePath.Replace(".asp", "_room_" + item.OptionID + ".asp");
                    //    optionTitle = "<a href=\"" + urlRoomDetail + "\" target=\"_blank\"  class=\"room_rate_title\">" + item.OptionTitle + "</a>";
                    //}
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
                        roomImage = "<a href=\"/" + urlRoomDetail + "\"><img src=\"" + roomImage + "\"  class=\"small_room_img\"></a>";
                    }



                    if (item.OptionCateID != 39 && item.OptionCateID != 40 && item.OptionCateID != 43 && item.OptionCateID != 44)
                    {
                        priceDisplay = item.Price / intNight;
                        grandPrice = (item.Price * vatInclude) * qtyProduct;
                        priceSeiling = grandPrice / intNight / qtyProduct;
                        ico_product_type = "";
                        ddQuantity = RenderDropDownQuantity("ddPrice", item.ConditionID + "_" + item.OptionID + "_" + item.PromotionID + "_" + item.NumAdult + "_" + item.NumChild + "_" + item.NumExtra, 0, 20, 0, item.OptionCateID);
                        if (item.IsExtranet)
                        {
                            priceNet = (decimal)((double)item.NetPrice * (1 - ((double)item.Comission / 100)));
                            priceNet = priceNet / intNight;
                        }
                        else
                        {
                            priceNet = item.NetPrice / intNight;
                        }

                        if (item.NumAdult > 3)
                        {
                            adultMax = "<img src=\"../theme_color/blue/images/icon/adult.png\" />x" + item.NumAdult;
                        }
                        else
                        {
                            adultMax = "<img src=\"../theme_color/blue/images/icon/ico_adult_" + item.NumAdult + ".png\" />";
                        }

                        if (item.OptionID != OptionTemp)
                        {
                            if (item.IsExtranet)
                            {
                                ico_available = "<img src=\"/images/available.png\"/>";
                            }
                            else
                            {
                                ico_available = "";
                            }
                            productDisplay = productDisplay.Replace("###rowSpan###", RowSpan.ToString());
                            productDisplay = productDisplay + "<tr><td rowspan=\"###rowSpan###\" valign=\"top\">" + roomImage + optionTitle + "</td><td>" + item.PolicyDisplay + "</td><td align=\"center\" class=\"rowEven fn16bGreen\">" + currency.CurrencyCode + " " + String.Format("{0:#,0}", priceDisplay) + "</td><td class=\"align_center\">" + ddQuantity + "</td></tr>\n";
                            RowSpan = 1;
                        }
                        else
                        {
                            if(item.ConditionID!=ConditionTemp)
                            {
                                productDisplay = productDisplay + "<tr><td>" + item.PolicyDisplay + "</td><td align=\"center\" class=\"rowEven fn16bGreen\">" + currency.CurrencyCode + " " + String.Format("{0:#,0}", priceDisplay) + "</td><td class=\"align_center\">" + ddQuantity + "</td></tr>\n";
                                RowSpan = RowSpan + 1;
                            }
                            
                        }
                        OptionTemp = item.OptionID;
                        ProductTemp = item.ProductID;
                        ConditionTemp = item.ConditionID;
                    }

                }
                productDisplay = productDisplay.Replace("###rowSpan###", RowSpan.ToString());
                productDisplay = productDisplay + RenderHiddenField(productList[0].ProductID, productList[0].SupplierID, _category, _dateStart, _dateEnd, currency.CurrencyID);


                productDisplay = productDisplay + "</table>\n";
                //productDisplay = productDisplay + "</div>\n";

                FrontPayLater payLater = new FrontPayLater();
                string payLaterPlan = payLater.GetPayLaterPlanByDate(ProductID, _dateStart);
                if (string.IsNullOrEmpty(payLaterPlan))
                {
                    productDisplay = productDisplay + "<div class=\"row_rate\"><a href=\"javascript:void(0)\" class=\"tooltip\"><img src=\"/theme_color/blue/images/icon/high-low.jpg\" class=\"lowrates\" title=\"Low Rates Guarantee\" /><span class=\"tooltip_content\">";
                    productDisplay = productDisplay + "<p><strong>Low Rate Guarantee</strong> - If you find a lower rate within 24 hours of booking, then, at a minimum, they will match that rate.</p><br />\n";
                    productDisplay = productDisplay + "<p><strong>High Security Website</strong> - Our payment system is certified by Thai Bank in usage of security version SSL 256 Bit encryption , the most modern technological security system using nowadays websites in the world.</p>\n";

                }
                else
                {
                    productDisplay = productDisplay + "<div class=\"row_rate\"><a href=\"javascript:void(0)\" class=\"tooltip\"><img src=\"/theme_color/blue/images/icon/high-low_paylater.jpg\" class=\"lowrates\" title=\"Low Rates Guarantee\" /><span class=\"tooltip_content\">";
                    //productDisplay = productDisplay + "<p>"+payLaterPlan+"</p>\n";
                    productDisplay = productDisplay + "<p><strong>Book Now Pay Later</strong> - Book Today, Pay Later Just $1 to secure your rooms / products today and pay the rest at date that is closure to check in</p><br/>";
                    productDisplay = productDisplay + "<p><strong>Low Rate Guarantee</strong> - If you find a lower rate within 24 hours of booking, then, at a minimum, they will match that rate.</p><br/>";
                    productDisplay = productDisplay + "<p><strong>High Security Website</strong> - Our payment system is certified by Thai Bank in usage of security version SSL 256 Bit encryption , the most modern technological security system using nowadays websites in the world.</p>";


                }


                productDisplay = productDisplay + "</span></a></div>\n";
                productDisplay = productDisplay + "<div class=\"incase\"><p>Hotels2thailand.com is legally allowed to sell tourism products on the internet, authorized from Tourism Authority of Thailand (TAT)</p></div>\n";
                productDisplay = productDisplay + "<div class=\"take_time\">It only takes 2 minutes!</div>\n";
                productDisplay = productDisplay + "<div class=\"booknow\"><a href=\"#\" id=\"btnBooking\"></a>\n";
                productDisplay = productDisplay + "<br class=\"clear-all\" /><br /><br />\n";
            }
            else
            {
                productDisplay = productDisplay + "<div style=\"border: 2px solid rgb(255, 204, 102); margin: 7px; padding: 10px;\">";
                productDisplay = productDisplay + "<font color=\"red\">";
                productDisplay = productDisplay + "Sorry. Rate for selected period is not available. Please kindly contact to <a href=\"mailto:reservation@hotels2thailand.com\">reservation@hotels2thailand.com</a>";
                productDisplay = productDisplay + "</font>";
                productDisplay = productDisplay + "</div>";
            }


            return productDisplay;
        }

        public string RenderDayTripList(List<ProductPriceMain> productList)
        {
           
            FrontSupplementPriceQuantity objSupplement = new FrontSupplementPriceQuantity();
            supplementList = objSupplement.LoadSupplementPriceByProductID(_productID, _dateStart);
            productList = productList.Where(x => x.Price > 0).ToList();
            string productDisplay = string.Empty;
            productList = productList.Where(x => x.Price > 0).ToList();
            if (productList.Count > 0)
            {

                productDisplay = productDisplay + "<div id=\"errorRoom\" class=\"errorMsg\"></div>\n";
                productDisplay = productDisplay + "<table class=\"tblListResult\" cellpadding=\"0\" cellspacing=\"1\" align=\"center\">\n";
                if(_langID==1)
                {
                productDisplay = productDisplay + "<tr><th>Option</th><th width=\"220\">Condition</th><th width=\"150\">Rate/Pax</th><th width=\"80\">Quantity</th></tr>\n";
                }else{
                productDisplay = productDisplay + "<tr><th>โปรแกรม</th><th width=\"220\">เงื่อนไข</th><th width=\"150\">ราคาต่อคน</th><th width=\"80\">จำนวน</th></tr>\n";
                }
                
                int OptionTemp = 0;
                int ConditionTemp = 0;
                int ProductTemp = 0;
                int RowSpan = 0;
                int intNight = _dateEnd.Subtract(_dateStart).Days;

                string ico_available = "";
                decimal priceDisplay = 0;
                decimal priceSeiling = 0;
                decimal priceNet = 0;
                decimal grandPrice = 0;
                string ico_product_type = "";
                int qtyProduct = 1;

                string ddQuantity = string.Empty;
                string adultMax = string.Empty;
                string roomImage = string.Empty;
                string urlRoomDetail = string.Empty;
                string optionTitle = string.Empty;
                FrontItinerary Itinerary = new FrontItinerary();
                List<object> ItinerayList = new List<object>();
                string ItineratyContent = string.Empty;


                foreach (ProductPriceMain item in productList)
                {

                    optionTitle = item.OptionTitle;

                    ItinerayList = Itinerary.GetItineraryProgram(item.OptionID);
                    ItineratyContent = "";
                    if (ItinerayList.Count > 0)
                    {
                        ItineratyContent = "<br class=\"clear-all\" /><div class=\"itinerary_info\"><a href=\"/Itinerary-information.aspx?itid=" + item.OptionID + "\" class=\"lightBoxFrame\"><font color=\"green\">Itinerary</font>";
                        ItineratyContent = ItineratyContent + "</a></div>";
                    }

                    //if (item.IsRoomShow)
                    //{
                    //    urlRoomDetail = item.FilePath.Replace(".asp", "_room_" + item.OptionID + ".asp");
                    //    optionTitle = "<a href=\"" + urlRoomDetail + "\" target=\"_blank\"  class=\"room_rate_title\">" + item.OptionTitle + "</a>";
                    //}

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
                        roomImage = "<a href=\"/" + urlRoomDetail + "\"><img src=\"" + roomImage + "\"  class=\"small_room_img\"></a>";
                    }



                    if (item.OptionCateID != 39 && item.OptionCateID != 40 && item.OptionCateID != 43 && item.OptionCateID != 44)
                    {
                        priceDisplay = item.Price / intNight;
                        grandPrice = (item.Price * vatInclude) * qtyProduct;
                        priceSeiling = grandPrice / intNight / qtyProduct;
                        ico_product_type = "";
                        ddQuantity = RenderDropDownQuantity("ddPrice", item.ConditionID + "_" + item.OptionID + "_" + item.PromotionID + "_" + item.NumAdult + "_" + item.NumChild + "_" + item.NumExtra, 0, 20, 0, item.OptionCateID);
                        if (item.IsExtranet)
                        {
                            priceNet = (decimal)((double)item.NetPrice * (1 - ((double)item.Comission / 100)));
                            priceNet = priceNet / intNight;
                        }
                        else
                        {
                            priceNet = item.NetPrice / intNight;
                        }

                        if (item.NumAdult > 3)
                        {
                            adultMax = "<img src=\"../theme_color/blue/images/icon/adult.png\" />x" + item.NumAdult;
                        }
                        else
                        {
                            adultMax = "<img src=\"../theme_color/blue/images/icon/ico_adult_" + item.NumAdult + ".png\" />";
                        }

                        if (item.OptionID != OptionTemp)
                        {
                            if (item.IsExtranet)
                            {
                                ico_available = "<img src=\"/images/available.png\"/>";
                            }
                            else
                            {
                                ico_available = "";
                            }
                            productDisplay = productDisplay.Replace("###rowSpan###", RowSpan.ToString());
                            productDisplay = productDisplay + "<tr><td rowspan=\"###rowSpan###\" valign=\"top\">" + roomImage + optionTitle + ItineratyContent + "</td><td>" + item.PolicyDisplay + "</td><td align=\"center\" class=\"rowEven fn16bGreen\">" + RenderDaytripSupplement(item.ConditionID, item.Price, supplementList) + "</td><td class=\"align_center\">" + ddQuantity + "</td></tr>\n";
                            RowSpan = 1;
                        }
                        else
                        {
                            if(item.ConditionID!=ConditionTemp)
                            {
                                productDisplay = productDisplay + "<tr><td>" + item.PolicyDisplay + "</td><td align=\"center\" class=\"rowEven fn16bGreen\">" + RenderDaytripSupplement(item.ConditionID, priceDisplay,supplementList) + "</td><td class=\"align_center\">" + ddQuantity + "</td></tr>\n";
                                RowSpan = RowSpan + 1;
                            }
                            
                        }
                        OptionTemp = item.OptionID;
                        ConditionTemp = item.ConditionID;
                        ProductTemp = item.ProductID;
                    }

                }
                productDisplay = productDisplay.Replace("###rowSpan###", RowSpan.ToString());
                productDisplay = productDisplay + RenderHiddenField(productList[0].ProductID, productList[0].SupplierID, _category, _dateStart, _dateEnd, currency.CurrencyID);
                productDisplay = productDisplay + RenderGuestBox();

                productDisplay = productDisplay + "</table>\n";
                //productDisplay = productDisplay + "</div>\n";

                FrontPayLater payLater = new FrontPayLater();
                string payLaterPlan = payLater.GetPayLaterPlanByDate(ProductID, _dateStart);
                if (string.IsNullOrEmpty(payLaterPlan))
                {
                    productDisplay = productDisplay + "<div class=\"row_rate\"><a href=\"javascript:void(0)\" class=\"tooltip\"><img src=\"/theme_color/blue/images/icon/high-low.jpg\" class=\"lowrates\" title=\"Low Rates Guarantee\" /><span class=\"tooltip_content\">";
                    productDisplay = productDisplay + "<p><strong>Low Rate Guarantee</strong> - If you find a lower rate within 24 hours of booking, then, at a minimum, they will match that rate.</p><br />\n";
                    productDisplay = productDisplay + "<p><strong>High Security Website</strong> - Our payment system is certified by Thai Bank in usage of security version SSL 256 Bit encryption , the most modern technological security system using nowadays websites in the world.</p>\n";

                }
                else
                {
                    productDisplay = productDisplay + "<div class=\"row_rate\"><a href=\"javascript:void(0)\" class=\"tooltip\"><img src=\"/theme_color/blue/images/icon/high-low_paylater.jpg\" class=\"lowrates\" title=\"Low Rates Guarantee\" /><span class=\"tooltip_content\">";
                    //productDisplay = productDisplay + "<p>"+payLaterPlan+"</p>\n";
                    productDisplay = productDisplay + "<p><strong>Book Now Pay Later</strong> - Book Today, Pay Later Just $1 to secure your rooms / products today and pay the rest at date that is closure to check in</p><br/>";
                    productDisplay = productDisplay + "<p><strong>Low Rate Guarantee</strong> - If you find a lower rate within 24 hours of booking, then, at a minimum, they will match that rate.</p><br/>";
                    productDisplay = productDisplay + "<p><strong>High Security Website</strong> - Our payment system is certified by Thai Bank in usage of security version SSL 256 Bit encryption , the most modern technological security system using nowadays websites in the world.</p>";


                }


                productDisplay = productDisplay + "</span></a></div>\n";
                productDisplay = productDisplay + "<div class=\"incase\"><p>Hotels2thailand.com is legally allowed to sell tourism products on the internet, authorized from Tourism Authority of Thailand (TAT)</p></div>\n";
                productDisplay = productDisplay + "<div class=\"take_time\">It only takes 2 minutes!</div>\n";
                productDisplay = productDisplay + "<div class=\"booknow\"><a href=\"#\" id=\"btnBooking\"></a>\n";
                productDisplay = productDisplay + "<br class=\"clear-all\" /><br /><br />\n";
            }
            else
            {
                productDisplay = productDisplay + "<div style=\"border: 2px solid rgb(255, 204, 102); margin: 7px; padding: 10px;\">";
                productDisplay = productDisplay + "<font color=\"red\">";
                productDisplay = productDisplay + "Sorry. Rate for selected period is not available. Please kindly contact to <a href=\"mailto:reservation@hotels2thailand.com\">reservation@hotels2thailand.com</a>";
                productDisplay = productDisplay + "</font>";
                productDisplay = productDisplay + "</div>";
            }


            return productDisplay;
        }

        public string RenderDaytripSupplement(int ConditionID, decimal Price, IList<FrontSupplementPriceQuantity> SupplementList)
        {
            //string supplementDisplay = "<table cellpadding=\"0\" cellspacing=\"1\" class=\"supplement\" width=\"100%\">\n";
            string supplementDisplay = "";
            int supplementCal = 0;
            string supplementTitle = string.Empty;
            decimal priceIncludeVat = Price * Convert.ToDecimal(1.177);
            decimal priceDisplay = 0;
            int countTemp = 0;
            
            SupplementList = SupplementList.Where(x => x.ConditionID == ConditionID).ToList();
            int countTotal = SupplementList.Count();

            foreach (FrontSupplementPriceQuantity item in SupplementList)
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
                    countTemp = countTemp + 1;
                    priceDisplay = (int)(priceIncludeVat + item.Supplement) / vatInclude;
                    if(countTemp!=countTotal)
                    {
                        supplementDisplay = supplementDisplay + "<p class=\"lh_16 fn16Green\">" + currency.CurrencyCode + " " + priceDisplay.ToString("#,###") + "<span>(" + supplementTitle + ")</span></p>";
                    }else{
                        supplementDisplay = supplementDisplay + "<p class=\"lh_16 lh_end fn16Green\">" + currency.CurrencyCode + " " + priceDisplay.ToString("#,###") + "<span>(" + supplementTitle + ")</span></p>";
                    }
                    
                    //supplementDisplay = supplementDisplay + "<tr><td class=\"list\">&bull;" + supplementTitle + "</td><td>" + priceDisplay.ToString("#,###") + "</td></tr>\n";
                    
                }
            }
            //supplementDisplay = supplementDisplay + "</table>\n";
            if(countTemp==1)
            {
                supplementDisplay = "<p class=\"lh_end fn16Green\">" + currency.CurrencyCode + " " + priceDisplay.ToString("#,###") + "</p>";
            }
            return supplementDisplay;
        }

        public string RenderExtraOptionList(List<ProductPriceMain> productList)
        {
            //ProductPriceMain objtest = new ProductPriceMain();
            //List<ProductPriceMain> results = objtest.loadAll(pro, dateStart, dateEnd, 1, true);
            productList = productList.OrderBy(x => x.OptionPriority).ThenBy(x => x.ConditionPriority).ThenBy(x => x.Price).ToList();

            string productDisplay = string.Empty;
            productDisplay = productDisplay + "<tr><td colspan=\"5\"><div id=\"errorTransfer\" class=\"errorMsg\"></div></td></tr>";
            //productDisplay = productDisplay + "<table class=\"tblListResult\" cellpadding=\"0\" cellspacing=\"1\" align=\"center\">";
            if(_langID==1)
            {
            productDisplay = productDisplay + "<tr><th colspan=\"3\">Extra Option</th><th>Rate</th><th>Quantity</th></tr>";
            }else{
                productDisplay = productDisplay + "<tr><th colspan=\"3\">บริการเสริม</th><th>ราคา</th><th>จำนวน</th></tr>";
            
            }
            
            int OptionTemp = 0;
            int ProductTemp = 0;
            int RowSpan = 0;
            int intNight = _dateEnd.Subtract(_dateStart).Days;

            string ico_available = "";
            decimal priceDisplay = 0;
            decimal priceSeiling = 0;
            decimal priceNet = 0;
            decimal grandPrice = 0;
            string ico_product_type = "";
            int qtyProduct = 1;
            string ddQuantity = string.Empty;

            ProductPrice objPrice = null;
            FrontProductPriceExtranet objPriceExtranet = null;
            if (productList.Count>0)
            {
                if (productList[0].IsExtranet)
                {
                    objPriceExtranet = new FrontProductPriceExtranet(_productID, _category, _dateStart, _dateEnd);
                    objPriceExtranet.LoadExtraOptionPrice();

                    foreach (ProductPriceMain item in productList)
                    {
                        if (item.OptionCateID == 39 || item.OptionCateID == 40 || item.OptionCateID == 43 || item.OptionCateID == 44)
                        {

                            item.Price = Utility.PriceExcludeVat(objPriceExtranet.CalculateAll(item.ConditionID, item.OptionID, 0).Price);
                            HasExtraOption = true;
                        }
                    }

                }
                else
                {
                    
                    objPrice = new ProductPrice(_productID, _category, _dateStart, _dateEnd);
                    objPrice.LoadExtraOptionPrice();
                    foreach (ProductPriceMain item in productList)
                    {
                        
                        if (item.OptionCateID == 39 || item.OptionCateID == 40 || item.OptionCateID == 43 || item.OptionCateID == 44)
                        {
                            //HttpContext.Current.Response.Write(item.OptionCateID+"--"+ item.ConditionID+"----"+item.OptionID + "<br>");
                            item.Price = Utility.PriceExcludeVat(objPrice.CalculateAll(item.ConditionID, item.OptionID, 0).Price);
                            HasExtraOption = true;
                        }
                    }
                }
                productList = productList.Where(x => x.Price > 0).ToList();
                foreach (ProductPriceMain item in productList)
                {
                    if (item.OptionCateID != 38)
                    {
                        if (item.OptionCateID == 43 || item.OptionCateID == 44)
                        {
                            HasTransfer = true;
                        }
                        //HttpContext.Current.Response.Write(item.Price+"<br>");
                        priceDisplay = (int)(item.Price / intNight);
                        grandPrice = (item.Price * vatInclude) * qtyProduct;
                        priceSeiling = grandPrice / intNight / qtyProduct;
                        ico_product_type = "";

                        if (item.IsExtranet)
                        {
                            priceNet = (decimal)((double)item.NetPrice * (1 - ((double)item.Comission / 100)));
                            priceNet = priceNet / intNight;
                        }
                        else
                        {
                            priceNet = item.NetPrice / intNight;
                        }

                        if (item.OptionID != OptionTemp)
                        {
                            ddQuantity = RenderDropDownQuantity("ddPriceExtra_" + item.ConditionID + "_" + item.OptionID, item.ConditionID + "_" + item.OptionID + "_" + item.PromotionID + "_" + item.NumAdult + "_" + item.NumChild + "_" + item.NumExtra, 0, 20, 0, item.OptionCateID);
                            productDisplay = productDisplay + "<tr><td colspan=\"3\">" + item.OptionTitle + "</td><td class=\"rowEven fn16bGreen\">" + currency.CurrencyCode +" " +String.Format("{0:#,0}", priceDisplay) + "</td><td class=\"align_center\">" + ddQuantity + "</td></tr>";
                        }

                        OptionTemp = item.OptionID;
                        ProductTemp = item.ProductID;
                    }

                }
            }
            

            
            productDisplay = productDisplay + RenderProductTransferOutside();
            productDisplay=productDisplay+RenderGalaDinner();
            
            //productDisplay = productDisplay + "</table>";
            
            if(!HasExtraOption)
            {
                
                productDisplay = "";
            }
            return productDisplay;
        }

       

        public string RenderProductTransferOutside()
        {
            int ProductTransferID = 0;
            
            string sqlCommand = string.Empty;

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                sqlCommand = "select top 1 p.product_id,";
                sqlCommand = sqlCommand + " ISNULL((";
                sqlCommand = sqlCommand + " select top 1 sp.product_id";
                sqlCommand = sqlCommand + " from tbl_product sp,tbl_product_location spl";
                sqlCommand = sqlCommand + " where sp.product_id=spl.product_id and spl.location_id=(";
                sqlCommand = sqlCommand + " select top 1 (spl2.location_id)";
                sqlCommand = sqlCommand + " from tbl_product sp2,tbl_product_location spl2";
                sqlCommand = sqlCommand + " where sp2.product_id=spl2.product_id and spl2.product_id=p.product_id";
                sqlCommand = sqlCommand + " ) and sp.cat_id=31 and sp.status=1";
                sqlCommand = sqlCommand + " ),'0') as location_transfer_id,";
                sqlCommand = sqlCommand + " 0  as destination_transfer_id";
                sqlCommand = sqlCommand + " from tbl_product p";
                sqlCommand = sqlCommand + " where p.product_id=" + _productID;

                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if ((int)reader["location_transfer_id"] > 0)
                    {
                        ProductTransferID = (int)reader["location_transfer_id"];
                    }
                    if ((int)reader["destination_transfer_id"] > 0)
                    {
                        ProductTransferID = (int)reader["destination_transfer_id"];

                    }
                }   

            }

            
            ProductPriceMain objtest = new ProductPriceMain(ProductTransferID, _dateStart, _dateStart.AddDays(1), _langID);
            List<ProductPriceMain> results = objtest.loadAll();

            string productDisplay = string.Empty;
            //productDisplay = productDisplay + "<div style=\"text-align:left\"><table class=\"tblListResult\" cellpadding=\"0\" cellspacing=\"1\" align=\"center\">";
            //productDisplay = productDisplay + "<tr><th>Room type</th><th>Max</th><th>Condition</th><th></th><th>Avg. Rate/Night</th><th>No.Room</th></tr>";
            int OptionTemp = 0;
            int ProductTemp = 0;
            int RowSpan = 0;
            int intNight = _dateEnd.Subtract(_dateStart).Days;

            string ico_available = "";
            decimal priceDisplay = 0;
            decimal priceSeiling = 0;
            decimal priceNet = 0;
            decimal grandPrice = 0;
            string ico_product_type = "";
            int qtyProduct = 1;
            string ddQuantity = string.Empty;
            results = results.Where(x => x.Price > 0).ToList();
            results = results.OrderBy(x => x.OptionPriority).ThenBy(x => x.ConditionPriority).ThenBy(x => x.Price).ToList();
            foreach (ProductPriceMain item in results)
            {
                if (item.OptionCateID != 38)
                {
                    if (item.OptionCateID == 43 || item.OptionCateID == 44)
                    {
                        HasTransfer = true;
                    }

                    priceDisplay = item.Price;
                    grandPrice = (item.Price * vatInclude) * qtyProduct;
                    priceSeiling = grandPrice / intNight / qtyProduct;
                    ico_product_type = "";

                    if (item.IsExtranet)
                    {
                        priceNet = (decimal)((double)item.NetPrice * (1 - ((double)item.Comission / 100)));
                        priceNet = priceNet / intNight;
                    }
                    else
                    {
                        priceNet = item.NetPrice / intNight;
                    }

                    if (item.OptionID != OptionTemp)
                    {

                        ddQuantity = RenderDropDownQuantity("ddPriceExtra_" + item.ConditionID + "_" + item.OptionID, item.ConditionID + "_" + item.OptionID + "_" + item.PromotionID + "_" + item.NumAdult + "_" + item.NumChild + "_" + item.NumExtra, 0, 20, 0, item.OptionCateID);
                        productDisplay = productDisplay + "<tr><td colspan=\"3\">" + item.OptionTitle + "</td><td class=\"rowEven fn16bGreen\">" + currency.CurrencyCode +" "+String.Format("{0:#,0}", priceDisplay) + "</td><td class=\"align_center\">" + ddQuantity + "</td></tr>";
                        RowSpan = 1;
                    }
                    
                    OptionTemp = item.OptionID;
                    ProductTemp = item.ProductID;
                    HasExtraOption = true;
                }

            }
            productDisplay = productDisplay.Replace("###rowSpan###", RowSpan.ToString());
            //productDisplay = productDisplay + "</table></div>";
            return productDisplay;
        }

        public string RenderGalaDinner()
        {
            string galaDisplay = string.Empty;

            GalaDinner gala = new GalaDinner(_productID, _dateStart, _dateEnd);
            List<GalaDinner> galaList=null;

            if(_isExtranet)
            {
                galaList=gala.GetGalaExtranet();
            }else{
                galaList=gala.GetGala();
            }
           

            if (galaList.Count > 0)
            {
                galaDisplay = "";
                galaDisplay = galaDisplay + "<tr valign=\"top\">\n";
                galaDisplay = galaDisplay + "<td colspan=\"5\">\n";
                galaDisplay = galaDisplay + "&nbsp;</td>";
                galaDisplay = galaDisplay + "</tr>";
                galaDisplay = galaDisplay + "<tr valign=\"top\">\n";
                galaDisplay = galaDisplay + "<td colspan=\"5\" style=\"border:2px solid #ffbd59;\">\n";
                galaDisplay = galaDisplay + "<div style=\"background:url(../images/ico_gala_dinner.jpg) no-repeat top left; padding-left:40px; height:45px; line-height:45px; font-family:Helvetica; color:#333; font-size:16px; font-weight:bold;\">Compulsory Meals</div>\n";
                galaDisplay = galaDisplay + "<p style=\"padding:0px 5px 10px 45px; color:#555; font-size:12px;\">\n";
                galaDisplay = galaDisplay + "Compulsory Gala Dinner: Compulsory gala dinner have been added to your booking automatically as part of the hotel's periodic condition.</li>";
                galaDisplay = galaDisplay + "</p>";
                galaDisplay = galaDisplay + "</tr>\n";
                galaDisplay = galaDisplay + "";
            }
            return galaDisplay;
        }

        public string RenderHiddenField(int ProductID,int SupplierID,byte categoryID,DateTime dateStart,DateTime dateEnd,byte CurrencyID)
        {
            string displayHidden = "<input type=\"hidden\" name=\"sid\" value=\"" + SupplierID + "\" />\n";
            displayHidden = displayHidden + "<input type=\"hidden\" id=\"discount\" name=\"discount\" value=\"" + discountPrice + "\" />\n";
            displayHidden = displayHidden + "<input type=\"hidden\" name=\"hotel_id\" value=\"" + ProductID + "\" />\n";
            displayHidden = displayHidden + "<input type=\"hidden\" name=\"cat_id\" value=\"" + categoryID + "\" />\n";
            displayHidden = displayHidden + "<input type=\"hidden\" name=\"date_start\" value=\"" + dateStart.ToString("yyyy-MM-dd") + "\" />\n";
            displayHidden = displayHidden + "<input type=\"hidden\" name=\"cusID\" value=\"" + _memberAuthen + "\" />\n";
            if (_category == 29)
            {
                displayHidden = displayHidden + "<input type=\"hidden\" name=\"date_end\" value=\"" + dateEnd.ToString("yyyy-MM-dd") + "\" />\n<br>";
            }
            displayHidden = displayHidden + "<input type=\"hidden\" id=\"currencyDisplay\" value=\"" + CurrencyID + "\" />\n";
            return displayHidden;
        }

        public string RenderGuestBox()
        {

            string displayGuestBox = "";



            if (_category == 32)
            {
                displayGuestBox = displayGuestBox + "<tr><th style=\"text-align:left !important\" align=\"left\" colspan=\"5\">Select Tee of time</strong> </th></tr>\n";
                displayGuestBox = displayGuestBox + "<tr><td width=\"736\" align=\"left\" colspan=\"5\">Hour: " + DropdownUtility.TeeOffTime("tee_hour", 0, 23, 0, 1) + " Min: " + DropdownUtility.TeeOffTime("tee_min", 0, 59, 0, 2) + "\n";
                displayGuestBox = displayGuestBox + "</td></tr>\n";
            }

            if (_category == 29)
            {
                displayGuestBox = displayGuestBox + "<tr><td colspan=\"5\">&nbsp;</td></tr>\n";
                if(_langID==1)
                {
                displayGuestBox = displayGuestBox + "<tr><th style=\"text-align:left !important\" align=\"left\" colspan=\"5\">Select No. of Guest</th></tr>\n";
                displayGuestBox = displayGuestBox + "<tr><td width=\"100\" colspan=\"5\"><label style=\"float:left; display:block;width:100px;\">Adult: </label>" + RenderDropDownQuantity("adult", "", 1, 20, 1, 0) + "</td>\n";
                displayGuestBox = displayGuestBox + "</td></tr>\n";
                displayGuestBox = displayGuestBox + "<tr class=\"bg_blue\"><td width=\"100\" colspan=\"5\"><label style=\"float:left; display:block;width:100px;\">Children: </label> " + RenderDropDownQuantity("child", "", 0, 20, 1, 0) + "</td>\n";
                displayGuestBox = displayGuestBox + "</td></tr>\n";
                }else{
                displayGuestBox = displayGuestBox + "<tr><th style=\"text-align:left !important\" align=\"left\" colspan=\"5\">จำนวนผู้เข้าพัก</th></tr>\n";
                displayGuestBox = displayGuestBox + "<tr><td width=\"100\" colspan=\"5\"><label style=\"float:left; display:block;width:100px;\">ผู้ใหญ่: </label>" + RenderDropDownQuantity("adult", "", 1, 20, 1, 0) + "</td>\n";
                displayGuestBox = displayGuestBox + "</td></tr>\n";
                displayGuestBox = displayGuestBox + "<tr class=\"bg_blue\"><td width=\"100\" colspan=\"5\"><label style=\"float:left; display:block;width:100px;\">เด็ก: </label> " + RenderDropDownQuantity("child", "", 0, 20, 1, 0) + "</td>\n";
                displayGuestBox = displayGuestBox + "</td></tr>\n";
                }
                
                
            }
            //displayGuestBox = displayGuestBox + "</table>\n";
            return displayGuestBox;
        }
        private string RenderAnnoucement()
        {
            string annouceDisplay = string.Empty;
           
            List<FrontAnnoucement> annoucementList;

            FrontAnnoucement annoucement = new FrontAnnoucement(_productID);
            annoucement.LangID = _langID;
            Utility.SetSessionDate();
            if (HttpContext.Current.Session["dateStart"] == "''")
            {
                annoucementList = annoucement.LoadAnnoucement();

            }
            else
            {

                annoucementList = annoucement.LoadAnnoucementByDate(Convert.ToString(HttpContext.Current.Session["dateStart"]), Convert.ToString(HttpContext.Current.Session["dateEnd"]));
                // annoucement.LoadAnnoucementByDate();
            }



            if (annoucementList.Count > 0)
            {
                if(_langID==1)
                {
                annouceDisplay = annouceDisplay + "</br><table class=\"annoucement\" style=\"border:1px solid #2e2721;background-color:#f0f8ff;\" width=\"95%\" align=\"center\"><tr><th class=\"roomtype\">Annoucement</th></tr><td valign=\"top\">";
                }else{
                annouceDisplay = annouceDisplay + "</br><table class=\"annoucement\" style=\"border:1px solid #2e2721;background-color:#f0f8ff;\" width=\"95%\" align=\"center\"><tr><th class=\"roomtype\">ประกาศ</th></tr><td valign=\"top\">";
                }
                
                foreach (FrontAnnoucement item in annoucementList)
                {
                    annouceDisplay = annouceDisplay + item.Detail + "<br/>";
                }
                annouceDisplay = annouceDisplay + "</tr></td></table><br/><br/>";
            }
            return annouceDisplay;

        }
        public string RenderMarketDisplay(List<FrontMarket> marketList, byte marketID, int conditionID)
        {
            bool hasExcept = false;
            bool hasAccept = false;

            byte[] arrCountryGroup = { 1, 6, 11, 12, 13 };
            //Array arrCountryGroup = Array ( 1, 6, 11, 12, 13 );

            string countryResult = string.Empty;
            string countryTitle = string.Empty;

            int countCol = 0;

            foreach (FrontMarket item in marketList)
            {
                if (!item.IsExcept)
                {
                    hasAccept = true;
                }
            }

            foreach (FrontMarket item in marketList)
            {
                if (item.IsExcept)
                {
                    hasExcept = true;
                }
            }

            string countryAccept = string.Empty;
            string countryExcept = string.Empty;

            bool bolUnGroup = false;

            if (marketList.Count == 0)
            {
                //world wide
                if(_langID==1)
                {
                    countryAccept = "Worldwide";
                }else{
                    countryAccept = "ทั่วโลก";
                }
                
            }
            else
            {
                if (hasExcept)
                {

                    //display except market
                    //countryResult = countryResult + "<span class=\"fb16\">" + marketList[0].Title + "</span><hr noshade=\"noshade\"/>";
                    if (hasAccept)
                    {
                        if(_langID==1)
                        {
                            countryAccept = countryAccept + "<strong>This rate accepts for these countries:</strong><br/>";
                        }else{
                            countryAccept = countryAccept + "<strong>ราคาสำหรับกลุ่มประเทศเหล่านี้:</strong><br/>";
                        }
                        
                        countryAccept = countryAccept + "<table class=\"lst_country_market\">";
                        countryAccept = countryAccept + "<tr>";
                        foreach (FrontMarket item in marketList)
                        {
                            if (!item.IsExcept && item.MarketID == marketID && item.ConditionID == conditionID && Array.IndexOf(arrCountryGroup, item.GroupID) < 0)
                            {
                                bolUnGroup = true;
                                countryTitle = item.Title;
                                if (countCol % 4 == 0)
                                {
                                    countryAccept = countryAccept + "</tr><tr>";
                                }
                                countryAccept = countryAccept + "<td>" + item.CountryTitle + "</td>";
                                countCol = countCol + 1;
                            }
                        }
                        countryAccept = countryAccept + "</tr>";
                        countryAccept = countryAccept + "</table>";
                    }

                    if (bolUnGroup)
                    {
                        countryResult = countryResult + countryAccept;
                    }


                    countCol = 0;
                    if(_langID==1)
                    {
                    countryExcept = countryExcept + "<strong>This rate is not used for these countries: </strong><br/>";
                    }else{
                    countryExcept = countryExcept + "<strong>ราคานี้ไม่สามารถใช้ได้กับกลุ่มประเทศเหล่านี้ </strong><br/>";
                    }
                    
                    countryExcept = countryExcept + "<table class=\"lst_country_market\">";
                    countryExcept = countryExcept + "<tr>";
                    foreach (FrontMarket item in marketList)
                    {
                        if (item.IsExcept && item.MarketID == marketID && item.ConditionID == conditionID && Array.IndexOf(arrCountryGroup, item.GroupID) < 0)
                        {
                            countryTitle = item.Title;
                            if (countCol % 4 == 0)
                            {
                                countryExcept = countryExcept + "</tr><tr>";
                            }
                            countryExcept = countryExcept + "<td>" + item.CountryTitle + "</td>";
                            countCol = countCol + 1;
                        }
                    }
                    countryExcept = countryExcept + "</tr>";
                    countryExcept = countryExcept + "</table>";
                    countryResult = countryResult + countryExcept;
                    if(_langID==1)
                    {
                    countryResult = countryResult + "<span class=\"except_market_short_list\">For guests from above country exception, please contact <span>reservation@hotels2thailand.com</span> </span>";
                    }else{
                    countryResult = countryResult + "<span class=\"except_market_short_list\">โปรดติดต่อ <span>reservation@hotels2thailand.com</span> </span>";
                    }
                    
                }
                else
                {
                    //display accept market
                    //countryResult = countryResult + "<span class=\"fb16\">" + marketList[0].Title + "</span><hr noshade=\"noshade\"/>";
                    countryAccept = countryAccept + "<table class=\"lst_country_market\">";
                    countryAccept = countryAccept + "<tr>";
                    foreach (FrontMarket item in marketList)
                    {
                        if (!item.IsExcept && item.MarketID == marketID && item.ConditionID == conditionID && Array.IndexOf(arrCountryGroup, item.GroupID) < 0)
                        {
                            countryTitle = item.Title;
                            bolUnGroup = true;
                            if (countCol % 4 == 0)
                            {
                                countryAccept = countryAccept + "</tr><tr>";
                            }
                            countryAccept = countryAccept + "<td>" + item.CountryTitle + "</td>";
                            countCol = countCol + 1;
                        }
                    }
                    countryAccept = countryAccept + "</tr>";
                    countryAccept = countryAccept + "</table>";
                    if (bolUnGroup)
                    {
                        countryResult = countryResult + countryAccept;
                    }

                    if(_langID==1)
                    {
                        countryResult = countryResult + "<br/><span class=\"except_market_short_list\">For guests from above country exception, please contact reservation@hotels2thailand.com </span>";
                    }else{
                        countryResult = countryResult + "<br/><span class=\"except_market_short_list\">กรุณาติดต่อ reservation@hotels2thailand.com </span>";
                    }
                    
                }
            }
            //countryResult = "hello";
            if (countryResult != "")
            {
                countryResult = "<span class=\"fb16\">" + countryTitle + "</span><hr noshade=\"noshade\"/>" + countryResult + "<br/></span>";
            }
            return countryResult;
        }

        public string GenerateXmlforExpressCheckout(List<ProductPriceMain> productList)
        {
            //foreach(ProductPriceMain item in productList)
            //{
            //    HttpContext.Current.Response.Write(item.Price+"<br/>");
            //}
            string result = string.Empty;
            string countryTitle = string.Empty;
            string sqlCommand = "select title from tbl_country where country_id="+refCountryID;
            using(SqlConnection cn=new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                countryTitle = (string)cmd.ExecuteScalar();
            }

            productList = productList.OrderBy(x => x.OptionPriority).ThenBy(x => x.Price).ToList();


            if (productList.Count > 0)
            {
                result = result + "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n";
                result = result + "<RoomRate>\n";
                result = result + "<SupplierID>" + productList[0].SupplierID + "</SupplierID>\n";
                result = result + "<Discount>" + discountPrice + "</Discount>\n";
                result = result + "<CountryID>" + refCountryID + "</CountryID>";
                result = result + "<Country>" + countryTitle + "</Country>\n";
                result = result + RenderXmlPackage();
                result = result + RenderXmlMeal();
                result = result + RenderXmlHotelList(productList);
                result = result + RenderXmlCurrency();
                if (_memberAuthen)
                {
                    result = result + "<IsMember>true</IsMember>\n";
                }
                else
                {
                    result = result + "<IsMember>false</IsMember>\n";
                }
                result = result + "<MemberID>" + _memberID + "</MemberID>\n";
                result = result + "<ErrorMessage>OK</ErrorMessage>\n";
                result = result + "</RoomRate>";
            }
            else {
                result = result + "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n";
                result = result + "<RoomRate>\n";
                result = result + "<ErrorMessage>No Rate</ErrorMessage>\n";
                result = result + "</RoomRate>\n";
            }
            
            //result = result + RenderXmlCurrency();
            return result;
        }

       

        private string RenderXmlCurrency()
        { 
            string result=string.Empty;
            result = result + "<Exchange>\n";
            using(SqlConnection cn=new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("select currency_id,title,prefix,code from tbl_currency where status=1", cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    result = result + "<Currency>\n";
                    result = result + "<CurrencyID>" + reader["currency_id"] + "</CurrencyID>\n";
                    result = result + "<CurrencyCode>" + reader["code"] + "</CurrencyCode>\n";
                    result = result + "<CurrencyTitle>" + reader["title"] + "</CurrencyTitle>\n";
                    result = result + "<CurrencyPrefix>" + reader["prefix"] + "</CurrencyPrefix>\n";
                    result = result + "</Currency>\n";
                }
            }
            result = result + "</Exchange>\n";
            return result;
        }

        private string RenderXmlPackage()
        {
            //FrontOptionPackage objPackage = new FrontOptionPackage(_productID, _dateStart, _dateEnd);
            string result = string.Empty;
            decimal priceDisplay = 0;
            result = result + "<Packages>\n";

            foreach (FrontOptionPackage item in objPackageList)
            {
                priceDisplay = (int)(item.Price/vatInclude);
                result = result + "<Option id=\"" + item.OptionID + "\">\n";
                result = result + "<OptionTitle>" + item.OptionTitle + "</OptionTitle>\n";
                result = result + "<OptionDetail>" + item.OptionDetail + "</OptionDetail>\n";
                result = result + "<OptionImage>http://www.booking2hotels.com" + item.OptionImage + "</OptionImage>";
                result = result + "<ConditionID>" + item.ConditionID + "</ConditionID>\n";
                result = result + "<ConditionDetail>" + HttpContext.Current.Server.HtmlEncode(item.PolicyDisplay.Replace("Special Offer", "")) + "</ConditionDetail>\n";
                result = result + "<ConditionValue>" + item.ConditionID + "_" + item.OptionID + "_0_" + item.NumAdult + "_" + item.NumChild + "_0</ConditionValue>\n";
                result = result + "<PolicyContent>" + HttpContext.Current.Server.HtmlEncode(item.PolicyContent) + "</PolicyContent>\n";
                result = result + "<PromotionTitle></PromotionTitle>\n";
                result = result + "<MaxAdult>" + item.NumAdult + "</MaxAdult>\n";
                result = result + "<MaxChild>" + item.NumChild + "</MaxChild>\n";
                result = result + "<Price>" + priceDisplay + "</Price>\n";
                result = result + "<PriceRack>" + priceDisplay + "</PriceRack>\n";
                
                result = result + "</Option>\n";
            }
            result = result + "</Packages>\n";
            return result;
        }
        private string RenderXmlMeal()
        {
            //FrontOptionPackage objPackage = new FrontOptionPackage(_productID, _dateStart, _dateEnd);
            string result = string.Empty;
            decimal priceDisplay = 0;
            result = result + "<Meals>\n";

            foreach (FrontOptionMeal item in objMealList)
            {
                priceDisplay = (int)(item.Price / vatInclude);
                result = result + "<Option id=\"" + item.OptionID + "\">\n";
                result = result + "<OptionTitle>" + item.OptionTitle + "</OptionTitle>\n";
                result = result + "<OptionDetail>" + item.OptionDetail.Replace("'","&apos;") +"</OptionDetail>\n";
                result = result + "<OptionImage>http://www.booking2hotels.com" + item.OptionImage + "</OptionImage>";
                result = result + "<ConditionID>" + item.ConditionID + "</ConditionID>\n";
                result = result + "<ConditionDetail>" + HttpContext.Current.Server.HtmlEncode(item.PolicyDisplay.Replace("Special Offer", "")) + "</ConditionDetail>\n";
                result = result + "<ConditionValue>" + item.ConditionID + "_" + item.OptionID + "_0_" + item.NumAdult + "_" + item.NumChild + "_0</ConditionValue>\n";
                result = result + "<PolicyContent>" + HttpContext.Current.Server.HtmlEncode(item.PolicyContent) + "</PolicyContent>\n";
                result = result + "<PromotionTitle></PromotionTitle>\n";
                result = result + "<MaxAdult>" + item.NumAdult + "</MaxAdult>\n";
                result = result + "<MaxChild>" + item.NumChild + "</MaxChild>\n";
                result = result + "<Price>" + priceDisplay + "</Price>\n";
                result = result + "<PriceRack>" + priceDisplay + "</PriceRack>\n";
                result = result + "</Option>\n";
            }
            result = result + "</Meals>\n";
            return result;
        }

        private string RenderXmlPricePerday(IList<OptionDayPrice> OptionDayPrice)
        {
            string result = string.Empty;

            

            foreach (OptionDayPrice item in OptionDayPrice)
            {
                result = result + "<PricePerday>\n";
                result = result + "<dm_date>" + item.DateCheck.ToString("yyyy-MM-dd") +"</dm_date>\n";
                result = result + "<dm_pricebase>"+item.PriceBase+"</dm_pricebase>\n";
                result = result + "<dm_pricepro>"+item.PricePromotion+"</dm_pricepro>\n";
                result = result + "<dm_priceAbf>" + item.PriceABF + "</dm_priceAbf>\n";
                result = result + "<dm_isPro>" + item.IsDatePromotion.ToString() + "</dm_isPro>\n";
                result = result + "</PricePerday>\n";
            }
            
            return result;
        }
        private string RenderXmlHotelList(List<ProductPriceMain> productList)
        {
            string result = string.Empty;
            List<ProductPriceMain> roomList = productList.Where(x => x.Price > 0).ToList();
            decimal priceDisplay = 0;
            int intNight = _dateEnd.Subtract(_dateStart).Days;
            Allotment objAllotment = null;
            objAllotment = new Allotment(ProductID);
            result = result + "<Options>\n";
            foreach (ProductPriceMain item in roomList)
            {

                if (item.OptionCateID != 39 && item.OptionCateID != 40 && item.OptionCateID != 43 && item.OptionCateID != 44)
                {
                    priceDisplay = item.Price / intNight;
                    
                    result = result + "<Option id=\"" + item.OptionID + "\">\n";
                    result = result + "<OptionTitle>" + HttpContext.Current.Server.HtmlEncode(item.OptionTitle) + "</OptionTitle>\n";
                    result = result + "<OptionImage>http://www.booking2hotels.com"+HttpContext.Current.Server.HtmlEncode(item.RoomImage)+"</OptionImage>";
                    result = result + "<OptionCateID>" + item.OptionCateID + "</OptionCateID>\n";
                    result = result + "<ConditionID>" + item.ConditionID + "</ConditionID>\n";
                    result = result + "<ConditionDetail>" + HttpContext.Current.Server.HtmlEncode(item.PolicyDisplay.Replace("Special Offer","")) + "</ConditionDetail>\n";
                    result = result + "<ConditionValue>" + item.ConditionID + "_" + item.OptionID + "_" + item.PromotionID + "_" + item.NumAdult + "_" + item.NumChild + "_" + item.NumExtra + "</ConditionValue>\n";
                    result = result + "<PolicyContent>" + HttpContext.Current.Server.HtmlEncode(item.PolicyContent) + "</PolicyContent>\n";
                    result = result + "<PromotionTitle>" + item.PromotionTitle + "</PromotionTitle>\n";
                    result = result + "<MaxAdult>"+item.NumAdult+"</MaxAdult>\n";
                    result = result + "<MaxChild>" + item.NumChild + "</MaxChild>\n";
                    result = result + "<Price>" + priceDisplay + "</Price>\n";
                    result = result + "<PriceRack>" + ((item.PriceRack / intNight)/vatInclude) + "</PriceRack>\n";
                    result = result + "<PricePerdays>\n" + RenderXmlPricePerday(item.iListPricePerDayMain) + "</PricePerdays>\n";
                    result = result + "<MemberBenefit>"+HttpContext.Current.Server.HtmlEncode(item.MemberBenefit)+"</MemberBenefit>\n";
                    //HttpContext.Current.Response.Write(item.SupplierID + "---" + item.OptionID + "---1---" + _dateStart + "---" + _dateEnd+"<br>");
                    if (!string.IsNullOrEmpty(objAllotment.CheckAllotAvaliable_Cutoff(item.SupplierID, item.OptionID, 1, _dateStart, _dateEnd)))
                    {
                        if (_isExtranet)
                        {
                            result = result + "<RoomAvailable>true</RoomAvailable>\n";
                        }
                        else
                        {
                            result = result + "<RoomAvailable>false</RoomAvailable>\n";
                        }

                    }
                    else {
                        result = result + "<RoomAvailable>false</RoomAvailable>\n";
                    }

                    if (item.IsRoomShow)
                    {
                        result = result + "<ShowRoomDetail>true</ShowRoomDetail>\n";
                    }
                    else
                    {
                        result = result + "<ShowRoomDetail>false</ShowRoomDetail>\n";
                    }

                    result = result + "</Option>\n";
                    
                }

            }
            result = result + "</Options>\n";
            result = result + "<ExtraOption>\n";
            result = result + RenderXmlExtraOptionList(productList);
            result = result + "</ExtraOption>\n";
            result = result + RenderXmlGalaDinner();
            //result = productList.Count.ToString();
            return result;
        }
        public string RenderXmlExtraOptionList(List<ProductPriceMain> productList)
        {
            string result = string.Empty;
            productList = productList.OrderBy(x => x.OptionPriority).ThenBy(x => x.ConditionPriority).ThenBy(x => x.Price).ToList();

            
            int intNight = _dateEnd.Subtract(_dateStart).Days;

            
            decimal priceDisplay = 0;
            
            int qtyProduct = 1;
           
            ProductPrice objPrice = null;
            FrontProductPriceExtranet objPriceExtranet = null;
            if (productList.Count > 0)
            {
                if (productList[0].IsExtranet)
                {
                    objPriceExtranet = new FrontProductPriceExtranet(_productID, _category, _dateStart, _dateEnd);
                    objPriceExtranet.LoadExtraOptionPrice();

                    foreach (ProductPriceMain item in productList)
                    {
                        if (item.OptionCateID == 39 || item.OptionCateID == 40 || item.OptionCateID == 43 || item.OptionCateID == 44)
                        {

                            item.Price = objPriceExtranet.CalculateAll(item.ConditionID, item.OptionID, 0).Price;
                            HasExtraOption = true;
                        }
                    }

                }
                else
                {

                    objPrice = new ProductPrice(_productID, _category, _dateStart, _dateEnd);
                    objPrice.LoadExtraOptionPrice();
                    foreach (ProductPriceMain item in productList)
                    {

                        if (item.OptionCateID == 39 || item.OptionCateID == 40 || item.OptionCateID == 43 || item.OptionCateID == 44)
                        {
                            //HttpContext.Current.Response.Write(item.OptionCateID+"--"+ item.ConditionID+"----"+item.OptionID + "<br>");
                            item.Price = objPrice.CalculateAll(item.ConditionID, item.OptionID, 0).Price;
                            HasExtraOption = true;
                        }
                    }
                }

                productList = productList.Where(x => x.Price > 0).ToList();

                foreach (ProductPriceMain item in productList)
                {
                    if (item.OptionCateID != 38)
                    {
                        if (item.OptionCateID == 43 || item.OptionCateID == 44)
                        {
                            HasTransfer = true;
                        }

                        priceDisplay = item.Price / intNight;
                        
		                result=result+"<Option id=\""+item.OptionID+"\">\n";
			            result=result+"<OptionTitle>"+item.OptionTitle+"</OptionTitle>\n";
                        result = result + "<OptionCateID>" + item.OptionCateID + "</OptionCateID>\n";
			            result=result+"<ConditionID>"+item.ConditionID+"</ConditionID>\n";
                        result=result+"<ConditionValue>" + item.ConditionID + "_" + item.OptionID + "_" + item.PromotionID + "_" + item.NumAdult + "_" + item.NumChild + "_" + item.NumExtra + "</ConditionValue>\n";
			            result=result+"<Price>"+((int)(priceDisplay/(decimal)1.177))+"</Price>\n";
		                result=result+"</Option>\n";
	                    
                           
                    }

                }
            }



            result = result + RenderXmlProductTransferOutside();

            

            //productDisplay = productDisplay + "</table>";

            if (!HasExtraOption)
            {

                result = "";
            }
            return result;
        }



        public string RenderXmlProductTransferOutside()
        {
            int ProductTransferID = 0;

            string sqlCommand = string.Empty;

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                sqlCommand = "select top 1 p.product_id,";
                sqlCommand = sqlCommand + " ISNULL((";
                sqlCommand = sqlCommand + " select top 1 sp.product_id";
                sqlCommand = sqlCommand + " from tbl_product sp,tbl_product_location spl";
                sqlCommand = sqlCommand + " where sp.product_id=spl.product_id and spl.location_id=(";
                sqlCommand = sqlCommand + " select top 1 (spl2.location_id)";
                sqlCommand = sqlCommand + " from tbl_product sp2,tbl_product_location spl2";
                sqlCommand = sqlCommand + " where sp2.product_id=spl2.product_id and spl2.product_id=p.product_id";
                sqlCommand = sqlCommand + " ) and sp.cat_id=31 and sp.status=1";
                sqlCommand = sqlCommand + " ),'0') as location_transfer_id,";
                sqlCommand = sqlCommand + " 0  as destination_transfer_id";
                sqlCommand = sqlCommand + " from tbl_product p";
                sqlCommand = sqlCommand + " where p.product_id=" + _productID;

                SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if ((int)reader["location_transfer_id"] > 0)
                    {
                        ProductTransferID = (int)reader["location_transfer_id"];
                    }
                    if ((int)reader["destination_transfer_id"] > 0)
                    {
                        ProductTransferID = (int)reader["destination_transfer_id"];

                    }
                }

            }


            ProductPriceMain objtest = new ProductPriceMain(ProductTransferID, _dateStart, _dateStart.AddDays(1), _langID);
            List<ProductPriceMain> results = objtest.loadAll();

            string result = string.Empty;
            
            int intNight = _dateEnd.Subtract(_dateStart).Days;

            
            decimal priceDisplay = 0;
            
            results = results.Where(x => x.Price > 0).ToList();
            results = results.OrderBy(x => x.OptionPriority).ThenBy(x => x.ConditionPriority).ThenBy(x => x.Price).ToList();
            foreach (ProductPriceMain item in results)
            {
                if (item.OptionCateID != 38)
                {
                    if (item.OptionCateID == 43 || item.OptionCateID == 44)
                    {
                        HasTransfer = true;
                    }

                    priceDisplay = item.Price;


                    
                    result = result + "<Option id=\"" + item.OptionID + "\">\n";
                    result = result + "<OptionTitle>" + item.OptionTitle + "</OptionTitle>\n";
                    result = result + "<OptionCateID>" + item.OptionCateID + "</OptionCateID>\n";
                    result = result + "<ConditionID>" + item.ConditionID + "</ConditionID>\n";
                    result = result + "<ConditionValue>" + item.ConditionID + "_" + item.OptionID + "_" + item.PromotionID + "_" + item.NumAdult + "_" + item.NumChild + "_" + item.NumExtra + "</ConditionValue>\n";
                    result = result + "<Price>" + priceDisplay + "</Price>\n";
                    result = result + "</Option>\n";
                    

                   
                    HasExtraOption = true;
                }

            }

            return result;
        }
        public string RenderXmlGalaDinner()
        {
            string result = string.Empty;

            GalaDinner gala = new GalaDinner(_productID, _dateStart, _dateEnd);
            List<GalaDinner> galaList = null;

            if (_isExtranet)
            {
                galaList = gala.GetGalaExtranet();
            }
            else
            {
                galaList = gala.GetGala();
            }


            if (galaList.Count > 0)
            {
                result = "<GalaDinner>true</GalaDinner>\n";
            }
            else {
                result = "<GalaDinner>false</GalaDinner>\n";
            }
            return result;
        }
    }
}