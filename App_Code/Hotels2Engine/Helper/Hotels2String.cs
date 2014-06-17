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
    

    public static class Hotels2String
    {

        //public static string[] Hotel2Split(this string strinput, char chrotslit)
        //{
        //    string[] strsplit = strinput.Split(chrotslit);
        //    return strsplit;
        //}

        public static string Hotels2MidClr2(this string strinput, int strstart, int strLen)
        {
            return strinput.Substring(strstart, strLen);
        }

        /// <summary>
        /// Remove First Character 
        /// </summary>
        /// <param name="strinput"></param>
        /// <param name="intLen"></param>
        /// <returns></returns>
        public static string Hotels2LeftClr(this string strinput, int intLen)
        {
            string Strnew = string.Empty;
            if (intLen > 0 && strinput.Length >= intLen)
            {
                Strnew = strinput.Substring(intLen);
                return Strnew;
            }
            else
            {
                return Strnew;
            }
        }
        /// <summary>
        /// remove and replace only '&' to &amp;
        /// </summary>
        /// <param name="strinput"></param>
        /// <returns></returns>
        public static string Hotels2SPcharacter_removeONe(this string strinput)
        {
            if (strinput.IndexOf("&amp;") < 0)
                strinput = strinput.Replace("&", "&amp;");
            else
                strinput = strinput.Replace("&amp;", "&").Replace("&", "&amp;");



            //if (strinput.IndexOf("&lt;") < 0)
            //    strinput = strinput.Replace("<", "&lt;");
            //else
            //    strinput = strinput.Replace("&lt;", "<").Replace("<", "&lt;");

            //if (strinput.IndexOf("&gt;") < 0)
            //    strinput = strinput.Replace(">", "&gt;");
            //else
            //    strinput = strinput.Replace("&gt;", ">").Replace(">", "&gt;");


            //if (strinput.IndexOf("&quot;") < 0)
            //    strinput = strinput.Replace("\"", "&quot;");
            //else
            //    strinput = strinput.Replace("&quot;", "\"").Replace("\"", "&quot;");


            //if (strinput.IndexOf("&apos;") < 0)
            //    strinput = strinput.Replace("'", "&apos;");
            //else
            //    strinput = strinput.Replace("&apos;", "'").Replace("'", "&apos;");

            return strinput.Trim();
            //return strinput.Replace("<", "&lt;").Replace(">", "&gt;").Replace("'", "&apos;").Replace("\"", "&quot;").Replace("&", "&amp;").Trim();
        }

        public static bool IsMatchEmail(string input)
        {
            bool IsMatch = false;
            bool condition = true;
            //string emailformat = "\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

            string emailformat =
            @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
     + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
     + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
     + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
            //string emailformat = "/^[\\w\\-\\.\\+]+\\@[a-zA-Z0-9\\.\\-]+\\.[a-zA-z0-9]{2,4}$/";
            Regex regura = new Regex(emailformat);

            if (regura.IsMatch(input))
            {
                IsMatch = true;

                if (Regex.IsMatch(input, @"[^\u0000-\u007F]"))
                {
                    IsMatch = true;

                    int length = input.Length;
                    char mychar = input[length - 1];


                    while (condition == true)
                    {
                        if (regura.IsMatch(input))
                        {
                            IsMatch = true;
                        }
                        else
                        {
                            IsMatch = false;
                            break;
                        }

                        if (Convert.ToString(mychar) == ".")
                        {
                            IsMatch = false;
                            break;
                        }
                        else
                        {
                            IsMatch = true;

                        }

                        ArrayList Arrystring = new ArrayList();
                        Arrystring.Add(";");
                        Arrystring.Add("/");
                        Arrystring.Add("\\");
                        Arrystring.Add(":");
                        Arrystring.Add("'");
                        Arrystring.Add("\"");
                        Arrystring.Add("*");
                        Arrystring.Add(",");

                        foreach (string mystr in Arrystring)
                        {
                            char chartofind = Convert.ToChar(mystr);
                            int count = 0;
                            char[] chars = input.ToCharArray();
                            foreach (char c in chars)
                            {
                                if (c == chartofind)
                                {
                                    count++;
                                }
                            }
                            if (count > 0)
                            {
                                IsMatch = false;
                                break;
                            }
                            else
                            {
                                IsMatch = true;

                            }
                        }
                        condition = false;
                    }
                }
            }

            return IsMatch;

        }
        public static string Hotels2SPcharacter_remove(this string strinput)
        {
            if (strinput.IndexOf("&amp;") < 0)
                strinput = strinput.Replace("&", "&amp;");
            else
                strinput = strinput.Replace("&amp;", "&").Replace("&", "&amp;"); 


             
            if (strinput.IndexOf("&lt;") < 0)
                strinput = strinput.Replace("<", "&lt;");
            else
                strinput = strinput.Replace("&lt;", "<").Replace("<", "&lt;");
            
            if (strinput.IndexOf("&gt;") < 0)
                strinput = strinput.Replace(">", "&gt;");
            else
                strinput = strinput.Replace("&gt;", ">").Replace(">", "&gt;");


            if (strinput.IndexOf("&quot;") < 0)
                 strinput = strinput.Replace("\"", "&quot;");
            else
                strinput = strinput.Replace("&quot;", "\"").Replace("\"", "&quot;");
            

            if (strinput.IndexOf("&apos;") < 0)
                strinput = strinput.Replace("'", "&apos;");
            else
                strinput = strinput.Replace("&apos;", "'").Replace("'", "&apos;");

            return strinput.Trim();
            //return strinput.Replace("<", "&lt;").Replace(">", "&gt;").Replace("'", "&apos;").Replace("\"", "&quot;").Replace("&", "&amp;").Trim();
        }

        public static IDictionary<int, string> dicGetNumber(int start, int stop)
        {
            IDictionary<int, string> iDic = new Dictionary<int, string>();

            for (int count = start; count <= stop; count++)
            {
                iDic.Add(count, count.ToString());
            }
            return iDic;
        }
        /// <summary>
        /// Remove Last Character 
        /// </summary>
        /// <param name="strinput"></param>
        /// <param name="intLen"></param>
        /// <returns></returns>
        public static string Hotels2RightCrl(this string strinput, int intLen)
        {
            string Strnew = string.Empty;
            if (intLen > 0 && strinput.Length >= intLen)
            {
                return strinput.Substring(0, strinput.Length - intLen);
            }
            else
            {
                return strinput;
            }
        }

        public static string StringRight(this string param, int numLength)
        {
            return param.Substring(param.Length - numLength, numLength);
        }
        public static string StringLeft(this string param, int numLength)
        {
            return param.Substring(0, numLength);
        }


        /// <summary>
        /// Converts the input plain-text to HTML version, replacing carriage returns
        /// and spaces with <br /> and &nbsp;
        /// </summary>
        public static string Hotels2ConvertToHtml(this string content)
        {
            content = HttpUtility.HtmlEncode(content);
            content = content.Replace("  ", "&nbsp;&nbsp;").Replace(
               "\t", "&nbsp;&nbsp;&nbsp;").Replace("\n", "<br>");
            return content;
        }

        public static string Hotels2RandomString(int num)
        {
            StringBuilder mycode = new StringBuilder();
            mycode.Remove(0, mycode.Length);
            string txt = "";
            txt = "abcdefghigklmnopqrstuvwxyz";

            mycode.Append(txt);

            string totaltxt = mycode.ToString();
            char[] myChar = totaltxt.ToCharArray();
            int MyLength = totaltxt.Length;

            Random myRandom = new Random();
            mycode.Remove(0, mycode.Length);

            for (int i = 0; i < num; i++)
            {
                mycode.Append(myChar[myRandom.Next(0, MyLength)]);
            }
            string result = mycode.ToString();
            return result;
        }

        public static string Hotels2RandomStringNuM(int num)
        {
            StringBuilder mycode = new StringBuilder();
            mycode.Remove(0, mycode.Length);
            string txt = "";
            txt = "abcdefghijklmnopqrstuvwxyzABCDFGHIJKLMNOPQRSTUVWXYZ1234567890";

            mycode.Append(txt);

            string totaltxt = mycode.ToString();
            char[] myChar = totaltxt.ToCharArray();
            int MyLength = totaltxt.Length;

            Random myRandom = new Random();
            mycode.Remove(0, mycode.Length);

            for (int i = 0; i < num; i++)
            {
                mycode.Append(myChar[myRandom.Next(0, MyLength)]);
            }
            string result = mycode.ToString();
            return result;
        }
        public static string Hotel2EncrytedData_SecretKey_DES(this string ToEncryptedData)
        {
            MemoryStream encryotedPassword = new MemoryStream();
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = Encoding.ASCII.GetBytes("F=8fuiej");

            byte[] iV = { 11, 12, 33, 50, 78, 25, 72, 84 };
            des.IV = iV;

            ICryptoTransform myEncryptor = des.CreateEncryptor();
            CryptoStream myCryptoStream = new CryptoStream(encryotedPassword, myEncryptor, CryptoStreamMode.Write);

            byte[] pwd = Encoding.ASCII.GetBytes(ToEncryptedData);
            myCryptoStream.Write(pwd, 0, pwd.Length);
            myCryptoStream.Close();
            return Convert.ToBase64String(encryotedPassword.ToArray());
        }

        public static string Hotels2DecryptedData_SecretKey_DES(this string ToDecryptedData)
        {
            MemoryStream decryptedPassword = new MemoryStream();
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            des.Key = Encoding.ASCII.GetBytes("F=8fuiej");

            byte[] iV = { 11, 12, 33, 50, 78, 25, 72, 84 };
            des.IV = iV;

            ICryptoTransform myDecryptor = des.CreateDecryptor();
            CryptoStream myCryptoStream = new CryptoStream(decryptedPassword, myDecryptor, CryptoStreamMode.Write);
            byte[] encryptedPassword = Convert.FromBase64String(ToDecryptedData);
            myCryptoStream.Write(encryptedPassword, 0, encryptedPassword.Length);
            myCryptoStream.Close();
            return Encoding.ASCII.GetString(decryptedPassword.ToArray());
        }

        public static string Hotel2EncrytedData_SecretKey(this string EncryptedData)
        {
            byte[] data = Encoding.ASCII.GetBytes(EncryptedData);
            string encodeString = Convert.ToBase64String(data);
            return encodeString;
        }

        public static string Hotels2DecryptedData_SecretKey(this string DecryptedData)
        {
            byte[] decodeString = Convert.FromBase64String(DecryptedData);
            string data = Encoding.ASCII.GetString(decodeString);
            return data;
        }

        // EnCode String 
        public static string Hotels2EncryptedData(this string EncryptedData)
        {
            byte[] data = Encoding.ASCII.GetBytes(EncryptedData);
            string encodeString = Convert.ToBase64String(data);
            return encodeString;
        }

        //Decode String 
        public static string Hotels2DecryptedData(this string DecryptedData)
        {
            byte[] decodeString = Convert.FromBase64String(DecryptedData);
            string data = Encoding.ASCII.GetString(decodeString);
            return data;
        }
        public static string CreateMD5Hash(this string input)
        {
            // Use input string to calculate MD5 hash
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
                // To force the hex string to lower-case letters instead of
                // upper-case, use he following line instead:
                // sb.Append(hashBytes[i].ToString("x2")); 
            }
            return sb.ToString();
        }
        //Encode by MD5 (cant Decode)
        public static string Hotels2MD5EncryptedData(this string input)
        {
            MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();
            byte[] data = Encoding.ASCII.GetBytes(input);
            byte[] encryptedData = md5Provider.ComputeHash(data);
            string encryptedString = Convert.ToBase64String(encryptedData);
            return encryptedString;
        }

        public static bool Hotels2CharFindLast(this string input, char chrtoFind)
        {
            int length = input.Length;
            char mychar = input[length - 1];
            if (mychar == chrtoFind)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public bool findSemicolon(string input)
        //{
        //    string newinput = input.Trim();
        //    int Length = input.Length;
        //    char[] mychar = input.ToCharArray[];
        //}

        public static int Hotels2CharFind(this string stringToSearch, char charToFind)
        {
            int count = 0;
            char[] chars = stringToSearch.ToCharArray();
            foreach (char c in chars)
            {
                if (c == charToFind)
                {
                    count++;
                }
            }
            return count;
            

        }

        public static bool HotelsStringIsMatch(this string strStringCompare, string StringToCompare)
        {
            bool Ismatch = false;

            string strFirst = strStringCompare.ToLower().Trim().Replace(" ", "-");
            string strToCompare = StringToCompare.ToLower().Trim().Replace(" ", "-");

            if (strFirst == strToCompare)
            {
                Ismatch = true;
            }

            return Ismatch;
        }

        public static string EncodeIdToURL(string IdtoEncod)
        {
            string Random = Hotels2String.Hotels2RandomStringNuM(20);
            string strToEndCode = IdtoEncod + Random;
            string EncodeResult = strToEndCode.Hotel2EncrytedData_SecretKey();
            return HttpUtility.UrlEncode(EncodeResult);
        }

        public static string DecodeIdFromURL(string IdtoEncod)
        {
            string QueryStringResult = HttpUtility.UrlDecode(IdtoEncod).Hotels2DecryptedData_SecretKey();
            QueryStringResult = QueryStringResult.Hotels2RightCrl(20);

            return QueryStringResult;




            //string Random = Hotels2String.Hotels2RandomStringNuM(20);
            //string strToEndCode = IdtoEncod + Random;
            //string EncodeResult = strToEndCode.Hotel2EncrytedData_SecretKey();
            //return HttpUtility.UrlEncode(EncodeResult);
        }
        public static string CancellationGenerateWording_th(bool Isextranet, byte bytDayCancel, byte BHTChargePercent, byte bytBHTChargeNight, byte bytHotelChargePercent, byte bytHotelChargeNight)
        {
            string result = string.Empty;
            string dayCancel = "Less than " + bytDayCancel + " Day" + IsFill_S(bytDayCancel) + " to prior to arrival,";

            if (bytDayCancel == 0)
                dayCancel = "No-Show, ";
            string Charge = string.Empty;


            if (Isextranet)
            {
                if (bytHotelChargePercent == 0 && bytHotelChargeNight != 0)
                    Charge = "there is " + bytHotelChargeNight + " night " + IsFill_S(bytHotelChargeNight) + " plus 7% charged of administration fee.";

                if (bytHotelChargePercent != 0 && bytHotelChargeNight == 0)
                {
                    if (bytHotelChargePercent == 100)
                        Charge = "there is " + bytHotelChargePercent + " % charged";
                    else
                        Charge = "there is " + bytHotelChargePercent + " % charged plus 7% charged of administration fee.";

                }

            }
            else
            {
                if (bytBHTChargeNight != 0 && BHTChargePercent != 0)
                    Charge = "there is " + bytBHTChargeNight + " night " + IsFill_S(bytHotelChargeNight) + " plus " + BHTChargePercent + "% charged of administration fee.";

                if (bytBHTChargeNight == 0 && BHTChargePercent != 0)
                    Charge = "there is " + BHTChargePercent + " % charged of administration fee.";
            }


            result = dayCancel + " " + Charge;
            return result;
        }

        public static string CancellationGenerateWording(bool Isextranet, byte bytDayCancel, byte BHTChargePercent, byte bytBHTChargeNight, byte bytHotelChargePercent, byte bytHotelChargeNight)
        {
            string result = string.Empty;

                string dayCancel = "Less than " + bytDayCancel + " Day" + IsFill_S(bytDayCancel) + " to prior to arrival,";

                if (bytDayCancel == 0)
                    dayCancel = "No-Show, ";
                string Charge = string.Empty;


                if (Isextranet)
                {
                    if (bytHotelChargePercent == 0 && bytHotelChargeNight != 0)
                        Charge = "there is " + bytHotelChargeNight + " night " + IsFill_S(bytHotelChargeNight) + " plus 7% charged of administration fee.";

                    if (bytHotelChargePercent != 0 && bytHotelChargeNight == 0)
                    {
                        if (bytHotelChargePercent == 100)
                            Charge = "there is " + bytHotelChargePercent + " % charged";
                        else
                            Charge = "there is " + bytHotelChargePercent + " % charged plus 7% charged of administration fee.";

                    }

                }
                else
                {
                    if (bytBHTChargeNight != 0 && BHTChargePercent != 0)
                        Charge = "there is " + bytBHTChargeNight + " night " + IsFill_S(bytHotelChargeNight) + " plus " + BHTChargePercent + "% charged of administration fee.";

                    if (bytBHTChargeNight == 0 && BHTChargePercent != 0)
                        Charge = "there is " + BHTChargePercent + " % charged of administration fee.";
                }

                result = dayCancel + " " + Charge;
            

            
            return result;
        }

        public static string CancellationGenerateWording(bool Isextranet, byte bytDayCancel, byte BHTChargePercent, byte bytBHTChargeNight, byte bytHotelChargePercent, byte bytHotelChargeNight,byte manageID)
        {
            string result = string.Empty;
            string bhtCharged = string.Empty;
            string dayCancel = "Less than " + bytDayCancel + " Day" + IsFill_S(bytDayCancel) + " to prior to arrival,";

            if (bytDayCancel == 0)
                dayCancel = "No-Show, ";
            string Charge = string.Empty;


            if (manageID == 2)
                bhtCharged = "plus 7% charged of administration fee.";
            

            if (Isextranet)
            {
                if (bytHotelChargePercent == 0 && bytHotelChargeNight != 0)
                    Charge = "there is " + bytHotelChargeNight + " night " + IsFill_S(bytHotelChargeNight) + " " + bhtCharged;

                if (bytHotelChargePercent != 0 && bytHotelChargeNight == 0)
                {
                    if (bytHotelChargePercent == 100)
                        Charge = "there is " + bytHotelChargePercent + " % charged";
                    else
                        Charge = "there is " + bytHotelChargePercent + " % charged " + bhtCharged;

                }

            }
            else
            {
                if (bytBHTChargeNight != 0 && BHTChargePercent != 0)
                    Charge = "there is " + bytBHTChargeNight + " night " + IsFill_S(bytHotelChargeNight) + " plus " + BHTChargePercent + "% charged of administration fee.";

                if (bytBHTChargeNight == 0 && BHTChargePercent != 0)
                    Charge = "there is " + BHTChargePercent + " % charged of administration fee.";
            }

            result = dayCancel + " " + Charge;



            return result;
        }

        public static string CancellationGenerateWording(bool Isextranet, byte bytDayCancel, byte BHTChargePercent, byte bytBHTChargeNight, byte bytHotelChargePercent, byte bytHotelChargeNight, byte bytProductCat, byte bytBookingLangId, byte manageID)
        {
            string result = string.Empty;
            string bhtCharged = string.Empty;
            if (bytBookingLangId == 1)
            {
                string dayCancel = "Less than " + bytDayCancel + " Day" + IsFill_S(bytDayCancel) + " to prior to arrival,";

                if (bytDayCancel == 0)
                    dayCancel = "No-Show, ";
                string Charge = string.Empty;


                if (manageID == 2)
                    bhtCharged = "plus 7% charged of administration fee.";
                else
                    bhtCharged = "charged";

                if (Isextranet)
                {
                    if (bytHotelChargePercent == 0 && bytHotelChargeNight != 0)
                        Charge = "there is " + bytHotelChargeNight + " night " + IsFill_S(bytHotelChargeNight) + " " + bhtCharged + "";

                    if (bytHotelChargePercent != 0 && bytHotelChargeNight == 0)
                    {
                        if (bytHotelChargePercent == 100)
                            Charge = "there is " + bytHotelChargePercent + " % charged";
                        else
                            Charge = "there is " + bytHotelChargePercent + " % charged " + bhtCharged;

                    }

                }
                else
                {
                    if (bytBHTChargeNight != 0 && BHTChargePercent != 0)
                        Charge = "there is " + bytBHTChargeNight + " night " + IsFill_S(bytHotelChargeNight) + " plus " + BHTChargePercent + "% charged of administration fee.";

                    if (bytBHTChargeNight == 0 && BHTChargePercent != 0)
                        Charge = "there is " + BHTChargePercent + " % charged of administration fee.";
                }

                result = dayCancel + " " + Charge;
            }

            //Thai Version
            if (bytBookingLangId == 2)
            {
                string txt1 = "หากท่านยกเลิกการจองห้องพักภายใน ";
                string txt2 = "ก่อนวันเช็คอิน";
                string txt3 = "ในกรณีที่ท่านไม่เช็คอินที่โรงแรมตามที่กำหนด";
                if (bytProductCat != 29)
                {
                    txt1 = "หากท่านยกเลิกการจองภายใน ";
                    txt2 = "ก่อนวันใช้บริการ";
                    txt3 = "ในกรณีที่ท่านไม่มาใช้บริการตามที่ได้กำหนด";
                }


                string dayCancel = txt1 + bytDayCancel + " วัน" + txt2;

                if (bytDayCancel == 0)
                    dayCancel = txt3;
                string Charge = string.Empty;

                if (manageID == 2)
                    bhtCharged = "และบวกค่าธรรมเนียมการจอง 7%";


                if (Isextranet)
                {
                    if (bytHotelChargePercent == 0 && bytHotelChargeNight != 0)
                        Charge = "ท่านจะถูกเรียกเก็บเงินเท่ากับการเข้าพัก" + bytHotelChargeNight + " คืนแรก " + bhtCharged;

                    if (bytHotelChargePercent != 0 && bytHotelChargeNight == 0)
                    {
                        if (bytHotelChargePercent == 100)
                            Charge = "ท่านจะถูกเรียกเก็บเงินเท่ากับ " + bytHotelChargePercent + "% ของยอดการจอง";
                        else
                            Charge = "ท่านจะถูกเรียกเก็บเงินเท่ากับ " + bytHotelChargePercent + "% ของยอดการจอง" + bhtCharged;

                    }

                }
                else
                {
                    if (bytBHTChargeNight != 0 && BHTChargePercent != 0)
                        Charge = "ท่านจะถูกเรียกเก็บเงินเท่ากับการเข้าพัก " + bytBHTChargeNight + " คืนแรก บวกค่าธรรมเนียมการจอง " + BHTChargePercent + "%";

                    if (bytBHTChargeNight == 0 && BHTChargePercent != 0)
                        Charge = "ท่านจะถูกเรียกเก็บเงินเท่ากับ " + BHTChargePercent + " % ของยอดการจอง";
                }

                result = dayCancel + " " + Charge;
            }
            return result;
        }

        public static string CancellationGenerateWording(bool Isextranet, byte bytDayCancel, byte BHTChargePercent, byte bytBHTChargeNight, byte bytHotelChargePercent, byte bytHotelChargeNight, byte bytProductCat, byte bytBookingLangId)
        {
            string result = string.Empty;

            if (bytBookingLangId == 1)
            {
                string dayCancel = "Less than " + bytDayCancel + " Day" + IsFill_S(bytDayCancel) + " to prior to arrival,";

                if (bytDayCancel == 0)
                    dayCancel = "No-Show, ";
                string Charge = string.Empty;


                if (Isextranet)
                {
                    if (bytHotelChargePercent == 0 && bytHotelChargeNight != 0)
                        Charge = "there is " + bytHotelChargeNight + " night " + IsFill_S(bytHotelChargeNight) + " plus 7% charged of administration fee.";

                    if (bytHotelChargePercent != 0 && bytHotelChargeNight == 0)
                    {
                        if (bytHotelChargePercent == 100)
                            Charge = "there is " + bytHotelChargePercent + " % charged";
                        else
                            Charge = "there is " + bytHotelChargePercent + " % charged plus 7% charged of administration fee.";

                    }

                }
                else
                {
                    if (bytBHTChargeNight != 0 && BHTChargePercent != 0)
                        Charge = "there is " + bytBHTChargeNight + " night " + IsFill_S(bytHotelChargeNight) + " plus " + BHTChargePercent + "% charged of administration fee.";

                    if (bytBHTChargeNight == 0 && BHTChargePercent != 0)
                        Charge = "there is " + BHTChargePercent + " % charged of administration fee.";
                }

                result = dayCancel + " " + Charge;
            }

            //Thai Version
            if (bytBookingLangId == 2)
            {
                string txt1 = "หากท่านยกเลิกการจองห้องพักภายใน ";
                string txt2 = "ก่อนวันเช็คอิน";
                string txt3 = "ในกรณีที่ท่านไม่เช็คอินที่โรงแรมตามที่กำหนด";
                if (bytProductCat != 29)
                {
                    txt1 = "หากท่านยกเลิกการจองภายใน ";
                    txt2 = "ก่อนวันใช้บริการ";
                    txt3 = "ในกรณีที่ท่านไม่มาใช้บริการตามที่ได้กำหนด";
                }


                string dayCancel = txt1 + bytDayCancel + " วัน" + txt2;

                if (bytDayCancel == 0)
                    dayCancel = txt3;
                string Charge = string.Empty;


                if (Isextranet)
                {
                    if (bytHotelChargePercent == 0 && bytHotelChargeNight != 0)
                        Charge = "ท่านจะถูกเรียกเก็บเงินเท่ากับการเข้าพัก" + bytHotelChargeNight + " คืนแรก บวกค่าธรรมเนียมการจอง 7%";

                    if (bytHotelChargePercent != 0 && bytHotelChargeNight == 0)
                    {
                        if (bytHotelChargePercent == 100)
                            Charge = "ท่านจะถูกเรียกเก็บเงินเท่ากับ " + bytHotelChargePercent + "% ของยอดการจอง";
                        else
                            Charge = "ท่านจะถูกเรียกเก็บเงินเท่ากับ " + bytHotelChargePercent + "% ของยอดการจองและบวกค่าธรรมเนียมการจอง 7%";

                    }

                }
                else
                {
                    if (bytBHTChargeNight != 0 && BHTChargePercent != 0)
                        Charge = "ท่านจะถูกเรียกเก็บเงินเท่ากับการเข้าพัก " + bytBHTChargeNight + " คืนแรก บวกค่าธรรมเนียมการจอง " + BHTChargePercent + "%";

                    if (bytBHTChargeNight == 0 && BHTChargePercent != 0)
                        Charge = "ท่านจะถูกเรียกเก็บเงินเท่ากับ " + BHTChargePercent + " % ของยอดการจอง";
                }

                result = dayCancel + " " + Charge;
            }
            return result;
        }

        public static string getImageGateWay(string gateWay)
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(gateWay))
            {
                string bank = gateWay.Trim().Hotels2RightCrl(1);

                string[] arrBank = gateWay.Hotels2RightCrl(1).Split(';');

                foreach (string strbank in arrBank)
                {
                    //bank_transfer_sign.png
                    string[] item = strbank.Split(',');
                    if (byte.Parse(item[2]) == 1)
                        result = result + "<li><img src=\"http://www.booking2hotels.com/images/bank_" + item[0] + ".png\" />: " + item[3] + "</li>";
                    else
                        result = result + "<li><img src=\"http://www.booking2hotels.com/images/bank_transfer_sign.png\" />: " + item[3] + "</li>";
                    //string[] item = strbank.Split(',');
                    //if (byte.Parse(item[2]) == 1)
                    //    result = result + "<li><img src=\"/images/bank_" + item[0] + ".png\" /></li>";
                    //else
                    //    result = result + "<li><img src=\"/images/bank_transfer_sign.png\" /></li>";
                }
            }
            else
            {
                result = result + "No GateWay";
            }

            return result;
        }

        public static string numberOrdinal(byte Number)
        {
            if (((Number % 100) / 10) != 1)
            {
                if ((Number % 10) == 1)
                    return "st";
                else if ((Number % 10) == 2)
                    return "nd";
                else if ((Number % 10) == 3)
                    return "rd";
                else
                    return "th";
            }
            else
                return "th";
        }

        public static string AppendConditionDetailExtraNet(byte bytNumadult, byte bytABf, short bytOptionCat, string ConditionTitle, bool IsAdult)
        {

            string result = string.Empty;


            string strAdult = "&nbsp;(For " + bytNumadult + " Adult)";
            string strAbf = string.Empty;
            if (bytABf == 0)
                strAbf = "&nbsp;<strong>(Room Only)</strong>";
            else
                strAbf = "&nbsp;<strong>( Breakfast included  " + bytABf + " pax per room )</strong>";

      

            switch (bytOptionCat)
            {
                    //Room
                case 38:
                    result = ConditionTitle + strAdult + strAbf;
                    break;
                    //package
                case 57:
                    if (IsAdult)
                        strAdult = "&nbsp;(For " + bytNumadult + " Adult)";
                    else
                        strAdult = "&nbsp;(For " + bytNumadult + "  Child)";
                    
                    result = ConditionTitle + strAdult;
                    break;
                    //meal
                case 58:
                    break;
                    //gala
                case 47 :
                    break;
                    //Transfer
                case 44: case 43:
                    break;
                    //Extrabed
                case 39:
                    if (bytABf > 0)
                        result = "&nbsp;<strong>( Breakfast included )</strong>";
                    else
                        result = "&nbsp;<strong>( Breakfast excluded)</strong>";
                    break;
                default :

                    break;
            }

            return result;
        }
        public static string AppendConditionDetailExtraNet(byte bytNumadult, byte bytABf)
        {

            string result = string.Empty;
            string strAdult = "&nbsp;(For " + bytNumadult + " Adult)";
            string strAbf = string.Empty;
            if (bytABf == 0)
                strAbf = "&nbsp;<strong>(Room Only)</strong>";
            else
                strAbf = "&nbsp;<strong>( Breakfast included  " + bytABf + "  Pax )</strong>";

            result = strAbf + strAdult;
            return result;
        }

        public static string AppendConditionDetailExtraNet_Package(bool bolIsAdult)
        {
            string result = string.Empty;
            if (bolIsAdult)
            {
                result = "&nbsp;(For Adult)";
            }
            else
            {
                result = "&nbsp;(For Child)";
            }
            
            return result;
        }

        public static string AppendConditionDetailExtraNet(byte bytABf)
        {

            string result = string.Empty;
            
            if (bytABf == 0)
                result = "&nbsp;<strong>(Room Only)</strong>";
            else
                result = "&nbsp;<strong>( Breakfast included  " + bytABf + "  Pax )</strong>";

            return result;
        }

        public static string IsFill_S(int num)
        {
            if (num > 1)
                return "s";
            else
                return "";
        }


        public static string GetOptionTitle(string OptionTitle, string OptionTitlebooking)
        {
            string result = string.Empty;

            if(string.IsNullOrEmpty(OptionTitlebooking))
                result= OptionTitle;
            else
                result = OptionTitlebooking;

            return result;
        }

        public static string GetImgBank(byte bytGateWay, byte Manage, byte bytBookingType)
        {
            string result = string.Empty;
            string imgResult = string.Empty;
            string des = string.Empty;
            if (bytBookingType == 1)
            {
                result = "contact.png";
                des = "Offline Charge";
            }
            else
            {
                if (Manage == 2)
                {
                    result = "bht_logo.jpg";
                    des = "Online- BHT Manage";
                }
                else
                {
                    switch (bytGateWay)
                    {
                        //KBANK
                        case 3:
                            result = "bank_KASIKORN.jpg";
                            des = "Online- Kbank";
                            break;
                        //BBL
                        case 5:
                        case 6:
                            result = "bank_bk.jpg";
                            des = "Online- Bangkok Bank";
                            break;
                        //KRUNG SRI
                        case 9:
                            result = "bank_yudhya.jpg";
                            des = "Online- Krung Sri";
                            break;
                        //SCB
                        case 10:
                            result = "bank_siam.jpg";
                            des = "Online- SCB";
                            break;
                        //TMB
                        case 12:
                            result = "bank_tmb.jpg";
                            des = "Online- TMB";
                            break;

                    }
                }
            }

            imgResult = "<img style=\"width:20px;\" src=\"/images/" + result + "\" title=\"" + des + "\" />";
            return imgResult;
        }
    }
}