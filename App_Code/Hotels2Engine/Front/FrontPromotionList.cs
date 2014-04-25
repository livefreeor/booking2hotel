using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Production;

/// <summary>
/// Summary description for FrontOptionPackage
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class FrontPromotionList : Hotels2BaseClass
    {

        public int PromotionID { get; set; }
        
        public int ProductID { get; set; }
        public short SupplierID { get; set; }
       
        public string Title { get; set; }
        public string Detail { get; set; }
        
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime DateUseStart { get; set; }
        public DateTime DateUseEnd { get; set; }

        public bool DayMon { get; set; }
        public bool DayTue { get; set; }
        public bool DayWed { get; set; }
        public bool DayThu { get; set; }
        public bool DayFri { get; set; }
        public bool DaySat { get; set; }
        public bool DaySun { get; set; }

        public byte IsHolidayCharge { get; set; }



        public IList<object> GetPromotionList(int intProductId, string strCurrentIP, byte bytLangId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("fr_PromotionList_ByProductID", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = intProductId;
                cmd.Parameters.Add("@CurrentIP",SqlDbType.VarChar).Value= strCurrentIP;
                cmd.Parameters.Add("@langId", SqlDbType.TinyInt).Value = bytLangId;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public string GetXmlPromotionFeed(int intProductID, string strCurrentIP, byte bytLangId)
        {
            StringBuilder result = new StringBuilder();
            PromotionContentExtranet cProcontent = new PromotionContentExtranet();
            PromotionConditionActive cConditionPromotionActive = new PromotionConditionActive();
            result.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
            IList<object> iListPro = GetPromotionList(intProductID, strCurrentIP, bytLangId);

            result.Append("<Promotions>\n");
            //result.Append("<IPRequest>" + strCurrentIP + "</IPRequest>\n");
            if(iListPro.Count > 0)
            {
                foreach(FrontPromotionList pro in iListPro)
                {
                    result.Append("<Promotion>\n");
                    result.Append("<PromotionId>" + pro.PromotionID + "</PromotionId>\n");
                    result.Append("<Title>" + pro.Title + "</Title>\n");
                    result.Append("<Date_book_start>" + pro.DateStart.ToString("yyyy-MM-dd") + "</Date_book_start>\n");
                    result.Append("<Date_book_end>" + pro.DateEnd.ToString("yyyy-MM-dd") + "</Date_book_end>\n");
                    result.Append("<Date_stay_start>" + pro.DateUseStart.ToString("yyyy-MM-dd") + "</Date_stay_start>\n");
                    result.Append("<Date_stay_end>" + pro.DateUseEnd.ToString("yyyy-MM-dd") + "</Date_stay_end>\n");

                    if (string.IsNullOrEmpty(pro.Detail))
                    {
                        result.Append("<PromotionShow></PromotionShow>\n");
                      
                    }else
                        result.Append(pro.Detail);
                    int CountRoomActive = 0;
                    IList<object> iListConditionPro = cConditionPromotionActive.getActiveConditionPromotion(pro.PromotionID, pro.ProductID,
                pro.SupplierID, bytLangId);
                    //CountRoomActive = iListConditionPro.Count;
                   
                   
                     
                    result.Append("<RoomTypes>\n");
                    if (iListConditionPro.Count > 0)
                    {
                        var optionIdList = iListConditionPro.Select(s => s.GetType().GetProperty("OptionId").GetValue(s, null)).Distinct();

                        CountRoomActive = optionIdList.Count();
                        string optiontitle = string.Empty;
                        foreach (int optionId in optionIdList)
                        {
                            cConditionPromotionActive = (PromotionConditionActive)iListConditionPro
                                .FirstOrDefault(o => (int)o.GetType().GetProperty("OptionId")
                                .GetValue(o, null) == optionId);
                            result.Append("<Room>\n");
                            result.Append("<OptionID>" + cConditionPromotionActive.OptionId + "</OptionID>\n");
                            result.Append("<OptionTitle>" + cConditionPromotionActive.OptionTitle + "</OptionTitle>\n");
                            result.Append("</Room>\n");
                        }
                    }

                    result.Append("</RoomTypes>\n");


                    result.Append("<Roomactive>" + CountRoomActive + "</Roomactive>\n");

                    result.Append("</Promotion>\n");
                }

            }
            result.Append("</Promotions>\n");

            return result.ToString();

        }


        //public string GenPromotionDetailXML()
        //{
        //    string result = string.Empty;
        //    if (!string.IsNullOrEmpty(Request.Form["benefitList"]))
        //    {
        //        string[] benefitVal = Request.Form["benefitList"].Split(',');


        //        result = result + "<PromotionShow>";
        //        result = result + "<head>Special Benefit</head>";
        //        result = result + "<List>";

        //        foreach (string benefit in benefitVal)
        //        {
        //            result = result + "<item>" + Request.Form["hd_benefit_" + benefit].ToString().Hotels2SPcharacter_remove() + "</item>";
        //        }

        //        result = result + "</List>";
        //        result = result + "</PromotionShow>";

        //    }
        //    return result;
        //}
    }
}