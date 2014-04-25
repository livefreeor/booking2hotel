using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using System.Data.SqlClient;
using Hotels2thailand.ProductOption;
/// <summary>
/// Summary description for FrontProductAvailable
/// </summary>
/// 
namespace Hotels2thailand.Front
{

    public class FrontProductAvailable:Hotels2BaseClass
    {
        public int ProductID { get; set; }
        public short SupplierID { get; set; }
        public string Title { get; set; }
        public int OptionID { get; set; }
        public string OptionTitle { get; set; }
        public int ConditionID { get; set; }
        public bool IsExtranet { get; set; }
        public int IsBookNow { get; set; }
        public int PromotionID { get; set; }
        public string PromotionTitle { get; set; }
        public decimal Price { get; set; }
        public decimal NetPrice { get; set; }
        public bool HasAllotment { get; set; }
        public string PolicyDisplay { get; set; }
        public string CancellationDisplay { get; set; }
        public byte Breakfast { get; set; }
        public int Comission { get; set; }
        public string FilePath { get; set; }
        public byte Priority { get; set; }
        private short DestinationID;
        private short LocationID;
        private byte Unit;
        private DateTime DateStart;
        private DateTime DateEnd;
        private decimal BudgetMin;
        private decimal BudgetMax;

        private string _keyword=string.Empty;
        public string Keyword
        {
            set { _keyword = value; }
        }

        public FrontProductAvailable()
        {
        }
        public string GetPromotionTitle(int PromotionID)
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
        public FrontProductAvailable(short destinationID,short locationID, byte unit,DateTime dateStart,DateTime dateEnd,decimal budgetMin,decimal budgetMax)
        {
            DestinationID = destinationID;
            LocationID = locationID;
            Unit = unit;
            DateStart = dateStart;
            DateEnd = dateEnd;
            BudgetMin = budgetMin;
            BudgetMax = budgetMax;
        }

        public List<string> GetPromotionExtra(int PromotionID, byte langID)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                List<string> result = new List<string>();
                string sqlCommand = "select top 1 pmc_ex.title,pm_ex.iscancellation,pmc_ex.detail";
                sqlCommand = sqlCommand + " from tbl_promotion_extra_net pm_ex,tbl_promotion_content_extra_net pmc_ex";
                sqlCommand = sqlCommand + " where pm_ex.promotion_id=pmc_ex.promotion_id and pmc_ex.lang_id=1 and pm_ex.promotion_id=" + PromotionID;
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

        public List<FrontProductAvailable> LoadProductList()
        {
            ProductPrice objPrice=null;
            FrontProductPriceExtranet objPriceExtranet=null;
            string strCommand = string.Empty;
            if(string.IsNullOrEmpty(_keyword))
            {
                strCommand = "fr_report_room_available '" + DestinationID + "','" + LocationID + "'," + DateStart.Hotels2DateToSQlString() + "," + DateEnd.Hotels2DateToSQlString();
            }else{
                strCommand = "select * from fn_report_room_available_with_keyword('" + _keyword + "'," + DateStart.Hotels2DateToSQlString() + "," + DateEnd.Hotels2DateToSQlString() + ")";
            }

            
            List<FrontProductAvailable> result=new List<FrontProductAvailable>();
            FrontProductAvailable objAvi = new FrontProductAvailable();
            Allotment allotment=new Allotment();

            ProductPolicy policy = null;
            List<ProductPolicy> policyList = null;
            FrontCancellationPolicy cancalation = null;
            List<FrontCancellationPolicy> cancellationList = null;
            
            ProductPolicyExtranet policyExtra = null;
            List<ProductPolicyExtranet> policyListExtra = null;
            List<CancellationExtranet> cancellationListExtra = null;
            CancellationExtranet cancelationExtra = null;

            string policyDisplay = string.Empty;
            string policyContentDisplay = string.Empty;

            decimal price = 0;
            decimal priceNet = 0;
            OptionPrice objOptionPrice = new OptionPrice();
            int productTemp = 0;
            int promotionID = 0;
            string promotionTitle = string.Empty;

            using (SqlConnection cn=new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd=new SqlCommand(strCommand,cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                
                while(reader.Read())
                {
                    if((int)reader["product_id"]!=productTemp)
                    {
                        if ((bool)reader["isextranet"])
                        {
                            objPriceExtranet = new FrontProductPriceExtranet((int)reader["product_id"], 29, DateStart, DateEnd);
                            objPriceExtranet.LoadPrice();
                            cancelationExtra = new CancellationExtranet((int)reader["product_id"], DateStart);
                            cancellationListExtra = cancelationExtra.GetCancellation();
                            policyExtra = new ProductPolicyExtranet();
                            policyListExtra = policyExtra.GetExtraPolicy((int)reader["product_id"], 1);
                        }
                        else {
                            objPrice = new ProductPrice((int)reader["product_id"], 29, DateStart, DateEnd);
                            objPrice.LoadPrice();
                            policy = new ProductPolicy();
                            policy.DateCheck = DateStart;
                            policyList = policy.GetProductPolicy((int)reader["product_id"]);
                            cancalation = new FrontCancellationPolicy((int)reader["product_id"], DateStart);
                            cancellationList = cancalation.LoadCancellationPolicyByCondition();
                        }
                    }

                    if ((bool)reader["isextranet"])
                    {
                        
                        
                        objOptionPrice = objPriceExtranet.CalculateAll((int)reader["condition_id"], (int)reader["option_id"], (int)reader["promotion_id"]);

                        price = objOptionPrice.Price;
                        priceNet = objOptionPrice.PriceOwn;

                        

                        List<string> promotionDetail = GetPromotionExtra((int)reader["promotion_id"], 1);
                        if ((byte)reader["breakfast"] > 0)
                        {
                            policyDisplay = "Breakfast Included";
                            policyContentDisplay = "<strong>Breakfast Included</strong><br/>";
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
                            policyDisplay = policyDisplay+policyExtra.GetConditionPolicyList(policyListExtra, (int)reader["condition_id"], reader["promotion_title"].ToString());
                            if (bool.Parse(promotionDetail[1]))
                            {
                                //promotion has cancellation
                                policyContentDisplay = policyContentDisplay+policyExtra.GetPolicyContent(policyListExtra, cancellationListExtra, (int)reader["condition_id"], (int)reader["promotion_id"], reader["promotion_title"].ToString(), promotionDetail[2], bool.Parse(promotionDetail[1]));
                            }
                            else
                            {
                                policyContentDisplay = policyContentDisplay+policyExtra.GetPolicyContent(policyListExtra, cancellationListExtra, (int)reader["condition_id"], (int)reader["promotion_id"], reader["promotion_title"].ToString(), promotionDetail[2], bool.Parse(promotionDetail[1]));
                            }
                            promotionID = (int)reader["promotion_id"];
                            promotionTitle = reader["promotion_title"].ToString();
                        }
                        else
                        {
                            promotionID = 0;
                            promotionTitle = "";

                            policyDisplay = policyDisplay + policyExtra.GetConditionPolicyList(policyListExtra, (int)reader["condition_id"], "");

                            policyContentDisplay = policyContentDisplay + policyExtra.GetPolicyContent(policyListExtra, cancellationListExtra, (int)reader["condition_id"], 0, "", "", false);
                        }

                        //Response.Write(priceABF);



                        policyDisplay = "<a href=\"javascript:void(0)\" class=\"tooltip\">" + policyDisplay;
                        policyDisplay = policyDisplay + "<span class=\"tooltip_content\">" + policyContentDisplay + "</span>";
                        policyDisplay = policyDisplay + "</a>\n";
                    }
                    else
                    {
                        

                        
                        
                        objOptionPrice = objPrice.CalculateAll((int)reader["condition_id"], (int)reader["option_id"], (int)reader["promotion_id"]);
                        price = objOptionPrice.Price;
                        priceNet = objOptionPrice.PriceOwn;

                        policyDisplay = "";
                        policyContentDisplay = "";

                        if (objPrice.CheckPromotionAccept((int)reader["promotion_id"]))
                        {
                            policyContentDisplay = policyContentDisplay + "<img src=\"/images/ico_special_offer.jpg\">&nbsp;<strong>Promotion:</strong><br/>" + GetPromotionTitle((int)reader["promotion_id"]) + "<br/><br/>";
                             promotionID = (int)reader["promotion_id"];
                             promotionTitle = reader["promotion_title"].ToString();
                        }
                        else {
                            promotionID = 0;
                            promotionTitle = "";
                        }

                        if ((byte)reader["breakfast"] > 0)
                        {
                            policyDisplay = "Breakfast Included";
                            policyContentDisplay = policyContentDisplay+"<strong>Breakfast Included</strong><br/>";
                        }
                       

                        policyDisplay =policyDisplay+ policy.GetConditionPolicyList(policyList, (int)reader["condition_id"], reader["promotion_title"].ToString(), cancellationList);
                        policyContentDisplay = policyContentDisplay+policy.GetPolicyContent(policyList, cancellationList, (int)reader["condition_id"]);
                        policyDisplay = "<div class=\"refun\"><a href=\"javascript:void(0)\" class=\"tooltip\">" + policyDisplay;
                        policyDisplay = policyDisplay + "<span class=\"tooltip_content\">" + policyContentDisplay + "</span>";
                        policyDisplay = policyDisplay + "</a></div>\n";
                    }

                    result.Add(new FrontProductAvailable
                    {
                        ProductID = (int)reader["product_id"],
                        SupplierID = (short)reader["supplier_id"],
                        Title = reader["title"].ToString(),
                        OptionID = (int)reader["option_id"],
                        OptionTitle=reader["option_title"].ToString(),
                        ConditionID = (int)reader["condition_id"],
                        IsExtranet = (bool)reader["isextranet"],
                        IsBookNow = (int)reader["is_book_now"],
                        PromotionID = promotionID,
                        PromotionTitle = promotionTitle,
                        Price = price,
                        NetPrice=priceNet,
                        HasAllotment=allotment.CheckAllotAvaliable((short)reader["supplier_id"],(int)reader["option_id"],Unit,DateStart,DateEnd),
                        PolicyDisplay = policyDisplay,
                        Breakfast = (byte)reader["breakfast"],
                        Comission = (int)reader["comission"],
                        FilePath = reader["file_path"].ToString(),
                        Priority = (byte)reader["priority"]
                    });
                    productTemp = (int)reader["product_id"];
                    
                }
            }
            return result;
        }

        
    }
}