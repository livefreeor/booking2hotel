using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;



/// <summary>
/// Summary description for Hotels2Stringformat
/// </summary>
/// 
namespace Hotels2thailand
{
    public static class Hotels2FileName
    {

        public static string Hotels2FilenameGenerate(this string StringSource)
        {
            string FileName = StringSource.ToLower().Trim().Replace(' ', '-');

            string Result = (FileName + ".asp").Trim();
            return Result;
        }

        public static string FilenameManage(string strToReplace, byte bytLangId)
        {
            string[] strArr = strToReplace.Split('.');
            string result = string.Empty;
            switch (bytLangId)
            {
                case 1:
                    result = strToReplace;
                    break;
                case 2:
                    result = strArr[0] + "-th." + strArr[1];
                    break;
            }

            return result;
        }


        public static string FilenameManagePDF(string strToReplace, byte bytLangId)
        {
            string[] strArr = strToReplace.Split('.');
            string result = string.Empty;
            switch (bytLangId)
            {
                case 1:
                    result = strArr[0] + ".pdf";
                    break;
                case 2:
                    result = strArr[0] + "-th.pdf";
                    break;
            }

            return result;
        }

        public static string FilenameManage(string strToReplace, string strReplaceKey, byte bytLangId)
        {
            string[] strArr = strToReplace.Split('.');
            string result = string.Empty;
            switch (bytLangId)
            {
                case 1:
                    result = strArr[0] + "_" + strReplaceKey + "." + strArr[1];
                    break;
                case 2:
                    result = strArr[0] + "_" + strReplaceKey + "-th." + strArr[1];
                    break;
            }

            return result;
        }
    }
}