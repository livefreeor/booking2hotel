using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.DataAccess;

/// <summary>
/// Summary description for ProductPriceList
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public static class Utility
    {
        public static string rootPath()
        {
            return "http://174.36.32.56";
        }
        public static DateTime ConvertDateInput(string dateInput)
        { 
        //change date input form to datetime
        //dateInput Y-M-D
            string[] dateSplit=dateInput.Split('-');

            return new DateTime(int.Parse(dateSplit[0]),int.Parse(dateSplit[1]),int.Parse(dateSplit[2]));
        }
        public static DateTime ConvertDateInput2(string dateInput)
        {
            //change date input form to datetime
            //dateInput Y-M-D
            string[] dateSplit = dateInput.Split('-');
            return new DateTime(int.Parse(dateSplit[0]), int.Parse(dateSplit[1]), int.Parse(dateSplit[2]));
        }
        public static DateTime ConvertDateInput3(string dateInput)
        {
            //change date input form to datetime
            //dateInput YMD
            //dateInput = dateInput.Replace("'", "");
            //HttpContext.Current.Response.Write(dateInput);
            //HttpContext.Current.Response.End();

            string[] dateSplit = dateInput.Split('-');
            return new DateTime(int.Parse(dateSplit[0]), int.Parse(dateSplit[1]), int.Parse(dateSplit[2]));
        }
        public static string DateToStringYMD(this DateTime Dateinput)
        {
            string strYear = Dateinput.Year.ToString();
            string strMouth = Dateinput.Month.ToString();
            string strDay = Dateinput.Day.ToString();

            string strSqlDateResult =  strYear + "-" + strMouth + "-" + strDay;
            return strSqlDateResult;
        }

        public static string[,] GetProductType(byte productTypeID)
        {
            string[,] strProductType = GlobalVar.GetProductCategoryTitle();
            string[,] ResultProductType = new string[1, 5];

            for (int count = 0; count < (strProductType.Length/5); count++)
            {
                if (strProductType[count, 0] == productTypeID.ToString())
                {
                    ResultProductType[0, 0] = strProductType[count, 0];
                    ResultProductType[0, 1] = strProductType[count, 1];
                    ResultProductType[0, 2] = strProductType[count, 2];
                    ResultProductType[0, 3] = strProductType[count, 3];
                    ResultProductType[0, 4] = strProductType[count, 4];
                    break;
                }
            }
            return ResultProductType;

        }
        public static string GetKeywordReplace(string Content, string tagStart, string tagEnd)
        {
            int startIndex = Content.IndexOf(tagStart);
            int endIndex = Content.LastIndexOf(tagEnd) + tagEnd.Length;
            endIndex = endIndex - startIndex;
            return Content.Substring(startIndex, endIndex);
        }
        public static string GetHotelClassImage(double hotelClass,byte displayType)
        {
            string result = string.Empty;

            switch (hotelClass.ToString())
            {
                case "1":
                    result ="one-point-zero";
                break;
                case "1.5":
                    result ="one-point-five";
                break;
                case "2":
                result = "two-point-zero";
                break;
                case "2.5":
                    result ="two-point-five";
                break;
                case "3":
                result = "three-point-zero";
                break;
                case "3.5":
                    result ="three-point-five";
                break;
                case "4":
                result = "four-point-zero";
                break;
                case "4.5":
                    result ="four-point-five";
                break;
                case "5":
                result = "five-point-zero";
                break;
            }
            return result;
        }

        public static string GetHotelReviewText(double reviewScore,byte langID)
        {
            string result = string.Empty;
            if(langID==1)
            {
             switch(Convert.ToInt16(reviewScore)/2)
            {
                case 1:
                    result = "Bad";
                    break;
                case 2:
                    result = "Poor";
                    break;
                case 3:
                    result = "Average";
                    break;
                case 4:
                    result = "Superb";
                    break;
                case 5:
                    result = "Great";
                    break;
            }
            }else{
             switch(Convert.ToInt16(reviewScore)/2)
            {
                case 1:
                    result = "แย่มากๆ";
                    break;
                case 2:
                    result = "ค่อนข้างแย่";
                    break;
                case 3:
                    result = "ใช้ได้";
                    break;
                case 4:
                    result = "ค่อนข้างดี";
                    break;
                case 5:
                    result = "ดีมาก";
                    break;
            }
            }
           
            return result;
        }

        public static void SetSessionDate()
        { 
            if(HttpContext.Current.Session["dateStart"]==null)
            {
                HttpContext.Current.Session["dateStart"] = "''";
                HttpContext.Current.Session["dateEnd"] = "''";
            }
        }

        public static string RenderRateRadio(string Prefix,string Title,int maxSize,int defaultRate,int styleType)
        {
            string result=string.Empty;
            string selectDefault = string.Empty;
            result=result+"<li><strong>"+Prefix+"</strong><br/>";
            for (int countRate = 1; countRate <= maxSize;countRate++ )
            {
                if(countRate==defaultRate){
                    selectDefault = "checked=\"checked\"";
                }
                result=result+"<input name=\""+Title+"\" type=\"radio\" class=\"star\" value=\""+countRate+"\" "+selectDefault+"/>\n";
            }
            result = result + "</li>\n";
            return result;
        }


        public static decimal IncludeVat(decimal price)
        {
            return Math.Round(price * Convert.ToDecimal(1.177));
        }
        public static decimal ExcludeVat(decimal price)
        {
            return Math.Round(price / Convert.ToDecimal(1.177));
        }

        public static decimal PriceExcludeVat(decimal price)
        {
            return price / Convert.ToDecimal(1.177);
        }

       
        
    }


}