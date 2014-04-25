 using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for Hotels2Stringformat
/// </summary>
/// 
namespace Hotels2thailand
{


    public static class Hotels2LogWriter
    {
        public static void WriteFile(string strPath, string strTxt)
        {
            //HttpContext.Current.Response.Write(HttpContext.Current.Server.MapPath(strPath));
            //HttpContext.Current.Response.End();
            //FileInfo cLog = new FileInfo("log.html");
          
            strTxt = "Start... ----------------------------<br/><br/>" + strTxt + "<br/><br/>------------------------End";
            string filePath = Path.Combine(HttpRuntime.AppDomainAppPath, strPath);
            FileInfo log = new FileInfo(filePath);
            if (!log.Exists)
                log.Create();
           
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(strTxt);
            }

            //StreamWriter write = new StreamWriter(HttpContext.Current.Server.MapPath(strPath),true);

            ////write = File.CreateText(HttpContext.Current.Server.MapPath(strPath));
            //write.WriteLine(strTxt);
            //write.Close();

        }


        #region New Write Log (POP 2013-07-02)
        /// <summary>
        ///
        /// </summary>
        /// <param name="logtype">e=errorlog,d=debuglog,l=log</param>
        /// <param name="logMessage">Message</param>
        public static void writeToLogFile(string logtype, string logMessage)
        {
            writeToLogFile(logtype, logMessage, "", "");
        }
        public static void writeToLogFile(string functionname, string processlabel, Exception e)
        {
            writeToLogFile("e", functionname, processlabel, e.Message);
        }
        public static void writeToLogFile(string logtype, string functionname, string processlabel, string logMessage)
        {
            try
            {
                if (functionname != "") functionname = "@Func[" + functionname + "]";
                if (processlabel != "") processlabel = "#ProcLabel[" + processlabel + "]";
                string strLogMessage = string.Empty;
                string strLogDir = HttpContext.Current.Request.PhysicalApplicationPath.ToString() + "LogFiles";// System.Configuration.ConfigurationManager.AppSettings["logFilePath"].ToString();
                string strLogFile = "Log.log";
                if (logtype.ToLower() == "e")
                {
                    strLogFile = "LogError_" + Hotels2DateTime.Hotels2DateNow(DateTime.Now).ToString("yyyyMMdd") + ".log";
                }
                else if (logtype.ToLower() == "d")
                {
                    strLogFile = "LogDebug_" + Hotels2DateTime.Hotels2DateNow(DateTime.Now).ToString("yyyyMMdd") + ".log";
                }
                else if (logtype.ToLower() == "l")
                {
                    strLogFile = "Log_" + Hotels2DateTime.Hotels2DateNow(DateTime.Now).ToString("yyyyMMdd") + ".log";
                }
                strLogFile = strLogDir + "\\" + strLogFile;
                StreamWriter swLog;

                strLogMessage = string.Format("{0} > {1} {2} >> {3}", Hotels2DateTime.Hotels2DateNow(DateTime.Now).ToString("dd-MM-yyyy HH:mm:ss"), functionname, processlabel, logMessage);
                if (!Directory.Exists(strLogDir)) Directory.CreateDirectory(strLogDir);
                if (!File.Exists(strLogFile))
                {
                    swLog = new StreamWriter(strLogFile);
                }
                else
                {
                    swLog = File.AppendText(strLogFile);
                }

                swLog.WriteLine(strLogMessage);
                swLog.WriteLine();

                swLog.Close();

            }
            catch (Exception)
            {

            }
        }
        #endregion

    }
}