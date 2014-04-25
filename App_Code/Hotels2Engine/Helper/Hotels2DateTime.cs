using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for Hotels2DateTime
/// </summary>
/// 
namespace Hotels2thailand
{
    public  enum DateInterval { 
       Year, 
       Month, 
       Weekday, 
       Day, 
       Hour, 
       Minute, 
       Second 
    } 
    public static class Hotels2DateTime
    {
        
        /// <summary>
        /// Extension Method; Return DateTime.Now.AddHousrs (+14)
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static DateTime Hotels2DateNow(this DateTime instance)
        {
             return DateTime.Now.AddHours(12);
        }

        /// <summary>
        /// Extension Method; Return DateTime.AddHousrs(+14)
        /// </summary>
        /// <param name="CurrentTime"></param>
        /// <returns></returns>
        public static DateTime Hotels2ThaiDateTime(this DateTime CurrentTime)
        {
            return CurrentTime.AddHours(12);
        }

        /// <summary>
        /// Datediff Function
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public static long Hotels2DateDiff(this DateTime date1, DateInterval interval,  DateTime date2)
        { 

           TimeSpan ts = date2 - date1; 

            switch (interval) { 
                case DateInterval.Year: 
                    return date2.Year - date1.Year; 
                case DateInterval.Month: 
                    return (date2.Month - date1.Month) + (12 * (date2.Year - date1.Year)); 
                case DateInterval.Weekday: 
                    return Fix(ts.TotalDays) / 7; 
                case DateInterval.Day: 
                    return Fix(ts.TotalDays); 
                case DateInterval.Hour: 
                    return Fix(ts.TotalHours); 
                case DateInterval.Minute: 
                    return Fix(ts.TotalMinutes); 
                default: 
                    return Fix(ts.TotalSeconds); 
            } 
        }

        private static long Fix(double Number)
        {
            if (Number >= 0)
            {
                return (long)Math.Floor(Number);
            }
            return (long)Math.Ceiling(Number);

        } 
       //=======================================================

        public static string Hotels2LongDateTimeFormat(this DateTime CurrentTime)
        {
            return CurrentTime.ToLongDateString() +"&nbsp;"+ CurrentTime.ToLongTimeString();
        }

        public static string Hotels2LongDateTimeFormat(this string CurrentStringTime)
        {
            DateTime MyDateTime = new DateTime();
            MyDateTime = Convert.ToDateTime(CurrentStringTime);
            return MyDateTime.ToString("F");
        }

        public static string GetMonthName(int intMonth, bool isfull)
        {
           
            string[] Month = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

            string[] MonthFull = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            if (isfull)
                return MonthFull[intMonth - 1];
            else
                return Month[intMonth - 1];
        }
        public static DateTime Hotels2DateSplit(this string strTimeCurrent, string chrCharToSplit)
        {
            
            char[] chrTimeCurrent = strTimeCurrent.ToCharArray();
            char chrSplitItem = Convert.ToChar(chrCharToSplit);
            var result = Array.FindAll(chrTimeCurrent, tc => tc == chrSplitItem);

            if (result.Count() > 2)
            {
                return DateTime.Now.Hotels2DateNow();
            }
            else
            {
                string[] strDateItem = strTimeCurrent.Split(chrSplitItem);

                int intYear = Convert.ToInt32(strDateItem[2]);
                int intMonth = Convert.ToInt32(strDateItem[0]);
                int intDay = Convert.ToInt32(strDateItem[1]);

                DateTime dDateTime = new DateTime(intYear, intMonth, intDay);
                return dDateTime;
            }
         }

        public static DateTime Hotels2DateSplitYear(this string strTimeCurrent, string chrCharToSplit)
        {

            char[] chrTimeCurrent = strTimeCurrent.ToCharArray();
            char chrSplitItem = Convert.ToChar(chrCharToSplit);
            var result = Array.FindAll(chrTimeCurrent, tc => tc == chrSplitItem);

            if (result.Count() > 2)
            {
                return DateTime.Now.Hotels2DateNow();
            }
            else
            {
                string[] strDateItem = strTimeCurrent.Split(chrSplitItem);

                int intYear = Convert.ToInt32(strDateItem[0]);
                int intMonth = Convert.ToInt32(strDateItem[1]);
                int intDay = Convert.ToInt32(strDateItem[2]);

                DateTime dDateTime = new DateTime(intYear, intMonth, intDay);
                return dDateTime;
            }
        }

        public static string Hotels2DateToSQlString(this DateTime Dateinput)
        {
            string strYear = Dateinput.Year.ToString();
            string strMouth = Dateinput.Month.ToString();
            string strDay = Dateinput.Day.ToString();

            string strSqlDateResult = "'" + strYear + "-" + strMouth + "-" + strDay + "'";
            return strSqlDateResult;
        }

        public static string Hotels2DateToSQlStringNoSingleCode(this DateTime Dateinput)
        {
            string strYear = Dateinput.Year.ToString();
            string strMouth = Dateinput.Month.ToString();
            string strDay = Dateinput.Day.ToString();

            string strSqlDateResult = strYear + "-" + strMouth + "-" + strDay ;
            return strSqlDateResult;
        }
        public static IDictionary<string, string> GetYearList()
        {
            IDictionary<string, string> IdicYearlist = new Dictionary<string, string>();
            int yearCurrent  = DateTime.Now.Year;
            int Yearstart = yearCurrent - 2;
            int YearMax = yearCurrent + 3;
            
            for (int i = Yearstart; i <= YearMax; i++)
            {
                IdicYearlist.Add(i.ToString(), i.ToString());
            }
            return IdicYearlist;
                
        }

        public static IDictionary<string, string> GetMonthList()
        {
            IDictionary<string, string> Idicmonthlist = new Dictionary<string, string>();
            Idicmonthlist.Add("1", "January"); Idicmonthlist.Add("2", "February"); Idicmonthlist.Add("3", "March"); Idicmonthlist.Add("4", "April"); 
            Idicmonthlist.Add("5", "May");Idicmonthlist.Add("6", "June"); Idicmonthlist.Add("7", "July"); Idicmonthlist.Add("8", "August"); 
            Idicmonthlist.Add("9", "September"); Idicmonthlist.Add("10", "October");Idicmonthlist.Add("11", "November"); Idicmonthlist.Add("12", "December");
            return Idicmonthlist;

        }


    }
}