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
    public class FrontPackageList : Hotels2BaseClass
    {

        public int OptionId { get; set; }
        
        public int ProductID { get; set; }
       
       
        public string Title { get; set; }
        public string Detail { get; set; }
        
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime DateUseStart { get; set; }
        public DateTime DateUseEnd { get; set; }


        public IList<object> GetPackageList(int intProductId,  byte bytLangId)
        {
            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("fr_PackageList_ByProductID", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = intProductId;
             
                cmd.Parameters.Add("@langId", SqlDbType.TinyInt).Value = bytLangId;
                cn.Open();

                return MappingObjectCollectionFromDataReader(ExecuteReader(cmd));
            }
        }

        public string GetXmlPackageFeed(int intProductID,  byte bytLangId)
        {
            StringBuilder result = new StringBuilder();
            PromotionContentExtranet cProcontent = new PromotionContentExtranet();
            PromotionConditionActive cConditionPromotionActive = new PromotionConditionActive();
            result.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
            IList<object> iListPack = GetPackageList(intProductID,  bytLangId);

            result.Append("<Packages>\n");
            //result.Append("<IPRequest>" + strCurrentIP + "</IPRequest>\n");
            if (iListPack.Count > 0)
            {
                foreach (FrontPackageList pack in iListPack)
                {
                    result.Append("<Package>\n");
                    result.Append("<OptionID>" + pack.OptionId + "</OptionID>\n");
                    result.Append("<Title>" + pack.Title + "</Title>\n");
                    result.Append("<Detail>" + HttpContext.Current.Server.HtmlEncode(pack.Detail.Replace("'", "&apos;").Replace("\n", "").Replace("\r", "")) + "</Detail>\n");
                    result.Append("<Date_book_start>" + pack.DateStart.ToString("yyyy-MM-dd") + "</Date_book_start>\n");
                    result.Append("<Date_book_end>" + pack.DateEnd.ToString("yyyy-MM-dd") + "</Date_book_end>\n");
                    result.Append("<Date_stay_start>" + pack.DateUseStart.ToString("yyyy-MM-dd") + "</Date_stay_start>\n");
                    result.Append("<Date_stay_end>" + pack.DateUseEnd.ToString("yyyy-MM-dd") + "</Date_stay_end>\n");
                    result.Append("</Package>\n");
                }

            }
            result.Append("</Packages>\n");

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