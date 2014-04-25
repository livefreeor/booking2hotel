using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;

/// <summary>
/// Summary description for Hotels2Cookie
/// </summary>
/// 

namespace Hotels2thailand
{
    public static class Hotels2Cookie
    {

        public static string CookieGet(string cookieName, string cookieKey)
        {
            string strReturn = "";

            if (HttpContext.Current.Request.Cookies[cookieName] != null)
            {
                strReturn = HttpContext.Current.Request.Cookies[cookieName][cookieKey];
            }

            if (String.IsNullOrEmpty(strReturn))
            {
                strReturn = "";
            }

            return strReturn;
        }

        public static void CookieSet(string strCookieName,Dictionary<string,string> cookieData, byte intExpireDay)
        {
            HttpCookie cookies = new HttpCookie(strCookieName);
            foreach (KeyValuePair<string, string> item in cookieData)
            {
               cookies.Values[item.Key] = item.Value; 
            }
            cookies.Expires = DateTime.Now.AddDays(intExpireDay);
            HttpContext.Current.Response.Cookies.Add(cookies);
        }
    }
}