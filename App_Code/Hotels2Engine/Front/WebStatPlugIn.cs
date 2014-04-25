using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for WebStatPlugIn
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public static class WebStatPlugIn
    {
        
        public static string ClickTailTopPart()
        {
            string result = string.Empty;
            result = result + "<script type=\"text/javascript\">\n";
            result = result + "var WRInitTime=(new Date()).getTime();\n";
            result = result + "</script>\n";
            return result;
        }

        public static string TrackingIncludeFunction()
        {
            string result = string.Empty;
            result = result+"<%Option Explicit%>\n";
            result = result+"<!--#include virtual=\"/function/fnGenPageTrack.asp\"-->\n";
            return result;
        }
        public static string TrackingScript(byte categoryID,int contentID,byte langID)
        {
            string result = string.Empty;
            result = "<%=fnGenPageTrack(" + categoryID + "," + contentID + ","+langID+")%>";
            return result;
        }

        public static string TrackingQuickSearch()
        {
            string result = "<script language=\"javascript\" type=\"text/javascript\">\n";
            result = result + "tKeyword=GetValueQueryString(\"k\");\n";
            result = result + "var H2THUrl='%3Cscript%20language%3D%22javascript%22%20src%3D%22http%3A//track.hotels2thailand.com/application/track.aspx%3Fht2thPageID%3D9010400000000%26ht2thAD%3D%26affID%3D%26camID%3D%26keyword%3D'+tKeyword+'%26datein%3D%26dateout%3D%26desID%3D%26locID%3D%26bookingID%3D%26ht2thRefer%3D%22%20type%3D%22text/javascript%22%3E%3C/script%3E'\n";
            result = result + "document.write(unescape(H2THUrl));\n";
            result = result + "</script>";
            return result;
        }

        
        public static string ClickTailBottomPart()
        {
            string result = string.Empty;
            result = result + "<div id=\"ClickTaleDiv\" style=\"display: none;\"></div>\n";
            result = result + "<script type='text/javascript'>\n";
            result = result + "document.write(unescape(\"%3Cscript%20src='\"+\n";
            result = result + "(document.location.protocol=='https:'?\n";
            result = result + "'https://clicktale.pantherssl.com/':\n";
            result = result + "'http://s.clicktale.net/')+\n";
            result = result + "\"WRc3.js'%20type='text/javascript'%3E%3C/script%3E\"));\n";
            result = result + "</script>\n";
            result = result + "<script type=\"text/javascript\">\n";
            result = result + "var ClickTaleSSL=1;\n";
            result = result + "if(typeof ClickTale=='function') ClickTale(19989,0.06,\"www02\");\n";
            result = result + "</script>\n";
            return result;
        }

        public static string GoogleAnalytics(byte langID)
        {
            string result = string.Empty;

            if (langID==1)
            {
              result = result + "<script type=\"text/javascript\">\n";
              result = result + "var _gaq = _gaq || [];\n";
              result = result + "_gaq.push(['_setAccount', 'UA-182150-1']);\n";
              result = result + "_gaq.push(['_setDomainName', 'hotels2thailand.com']);\n";
              result = result + "_gaq.push(['_trackPageview']);\n";
              result = result + "(function() {\n";
              result = result + "var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;\n";
              result = result + "ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';\n";
              result = result + "var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);\n";
              result = result + "})();\n";
              result = result + "</script>\n";
            }else{
            result = result + "<script type=\"text/javascript\">\n";
 
             result = result + "var _gaq = _gaq || [];\n";
             result = result + "_gaq.push(['_setAccount', 'UA-30161753-1']);\n";
             result = result + "_gaq.push(['_setDomainName', 'hotels2thailand.com']);\n";
             result = result + "_gaq.push(['_trackPageview']);\n";
             result = result + "(function() {\n";
             result = result + "var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;\n";
             result = result + "ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';\n";
             result = result + "var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);\n";
             result = result + " })();\n";
 
            result = result + "</script>\n";
            }
              
            return result;
        }
    }
}