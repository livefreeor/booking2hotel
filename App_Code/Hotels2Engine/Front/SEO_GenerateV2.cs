using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Hotels2thailand.Production;

/// <summary>
/// Summary description for SEO_GenerateV2
/// </summary>

namespace Hotels2thailand.Front
{
    public class SEO_GenerateV2
    {
        /// <summary>
        /// GenVerSion2 
        /// Gen all Whitout Cannonical
        /// </summary>
        /// <param name="SeoStringType"></param>
        /// <param name="seoPageType"></param>
        /// <param name="seoProductCat"></param>
        /// <param name="strDestination"></param>
        /// <param name="strLocation"></param>
        /// <param name="strProduct"></param>
        /// <param name="strKeyword"></param>
        /// <param name="strPromotion"></param>
        /// <param name="strLowestRate"></param>
        /// <param name="strNumProductHotel"></param>
        /// <param name="strnumProductOther"></param>
        /// <returns></returns>
        
        public SEO_GenerateV2()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string Function_gen_optimize_string(SEO_StringType SeoStringType, SEO_PageType seoPageType, byte langID, byte seoProductCat, string strDestination, string strLocation, string strProduct, string strPromotion, string strLowestRate, string strProductNumHotel, string strNumProductOther, string strProductPaymentType, string strKeyword = "")
        {
            string SeoString = string.Empty;

            switch (langID)
            {
                case 1:
                    switch (SeoStringType)
                    {
                        case SEO_StringType.Meta:
                            SeoString = Meta_Gen(seoPageType, langID, seoProductCat, strDestination, strLocation, strProduct, strKeyword, strProduct, strLowestRate, strProductNumHotel, strNumProductOther);
                            break;

                        case SEO_StringType.Title:
                            SeoString = Title_Gen(seoPageType, langID, seoProductCat, strDestination, strLocation, strProduct, strPromotion, strProductPaymentType);
                            break;

                        case SEO_StringType.H1:
                            SeoString = "";
                            break;

                        case SEO_StringType.Link_Back_To_Home:
                            SeoString = "";
                            break;

                        case SEO_StringType.canonical:
                            SeoString = "";
                            break;
                    }
                    break;

                case 2:
                    switch (SeoStringType)
                    {
                        case SEO_StringType.Meta:
                            SeoString = Meta_Gen(seoPageType, langID, seoProductCat, strDestination, strLocation, strProduct, strKeyword, strProduct, strLowestRate, strProductNumHotel, strNumProductOther);
                            break;

                        case SEO_StringType.Title:
                            SeoString = Title_Gen(seoPageType, langID, seoProductCat, strDestination, strLocation, strProduct, strPromotion, strProductPaymentType);
                            break;

                        case SEO_StringType.H1:
                            SeoString = "";
                            break;

                        case SEO_StringType.Link_Back_To_Home:
                            SeoString = "";
                            break;

                        case SEO_StringType.canonical:
                            SeoString = "";
                            break;
                    }
                    break;
            }

            return SeoString.ToString();
        }

        public static string Function_gen_optimize_string(SEO_StringType SeoStringType, SEO_PageType seoPageType, byte seoProductCat, string strfilname)
        {
            string SeoString = string.Empty;
            switch (SeoStringType)
            {
                case SEO_StringType.Meta:

                    break;
                case SEO_StringType.Title:

                    break;
                case SEO_StringType.H1:

                    break;
                case SEO_StringType.Link_Back_To_Home:

                    break;
                case SEO_StringType.canonical:
                    SeoString = canonical_Gen(seoPageType, seoProductCat, strfilname);
                    break;
            }

            return SeoString.ToString();
        }


        private static string Title_Gen(SEO_PageType seoPageType, byte langID, byte seoProductCat, string strDestination, string strLocation, string strProduct, string strPromotion, string strProductPaymentType)
        {
            StringBuilder SeoString = new StringBuilder();
            bool bolDesInProduct = false;
            string strTitle = "";
            int intProductLen = strProduct.Length;
            int intDesLen = strDestination.Length;
            int intPromoLen = strPromotion.Length;

            if (strProduct.IndexOf(strDestination) >= 0)
            {
                bolDesInProduct = true;
            }
            else
            {
                bolDesInProduct = false;
            }

            switch (langID)
            {
                case 1:
                    switch (seoPageType)
                    {
                        // Title for home //
                        case SEO_PageType.Home:
                            SeoString.Append("Thailand Hotels Lowest Rate Guaranteed, Booking Thailand Hotels");
                            break;
                        // Title for home //

                        // Title for destination //
                        case SEO_PageType.Destination:
                            switch (seoProductCat)
                            {
                                case 29:
                                    SeoString.Append(strDestination + " Hotels Lowest Rate Guarantee, Booking " + strDestination + " Hotels");
                                    break;
                                //"Golf Courses";
                                case 32:
                                    SeoString.Append(strDestination + " Golf Courses : All Golf Course in " + strDestination + "");
                                    break;
                                //"Day Trips";
                                case 34:
                                    switch (strDestination.Trim().ToLower())
                                    {
                                        case "bangkok":
                                            SeoString.Append("Bangkok Tours, Day Trips, Attractions, Nightlife, Shopping, Dining, Grand Palace, Wat Pho, Wat Arun and All Activities in Bangkok");
                                            break;
                                        case "phuket":
                                            SeoString.Append("Phuket Tours, Day Trips, Attractions, Nightlife, Shopping, Dining, Diving, Spa, Canoe, Kayak and All Activities in Phuket");
                                            break;
                                        default:
                                            SeoString.Append(strDestination + " Tours, Day Trips, Attractions, Nightlife, Shopping, Dining");
                                            break;
                                    }
                                    break;
                                //"Water Activity";
                                case 36:
                                    switch (strDestination.Trim().ToLower())
                                    {
                                        case "phuket":
                                            SeoString.Append("Phuket Water Activities : Diving Canoe Kayak Boat Trips Snorkeling Fishing Rafting with Online Booking");
                                            break;
                                        default:
                                            SeoString.Append(strDestination + "  Water Activities : Diving Canoe Kayak Boat Trips Snorkeling Fishing Rafting with Online Booking");
                                            break;
                                    }
                                    break;
                                //"Shows";
                                case 38:
                                    switch (strDestination.Trim().ToLower())
                                    {
                                        case "bangkok":
                                            SeoString.Append("Bangkok Shows & Events : Siam Niramit, Calypso Cabaret, Online booking All Shows & Events in Bangkok with LOWEST RATE GUARANTEED");
                                            break;
                                        case "phuket":
                                            SeoString.Append("Phuket Shows & Events : Phuket Fantasea, Phuket Simon Cabaret, Online Booking All Shows & Events in Phuket with LOWEST RATE GUARANTEED");
                                            break;
                                        case "pattaya":
                                            SeoString.Append("Pattaya Shows & Events : Tiffany Show, Alcazar Show, Alangkarn Pattaya, Online booking All Shows & Events in Pattaya with LOWEST RATE GUARANTEED");
                                            break;
                                        default:
                                            SeoString.Append(strDestination + " Shows & Events Online booking All Shows & Events in " + strDestination + " with LOWEST RATE GUARANTEED");
                                            break;
                                    }
                                    break;
                                //"Health Check Up";
                                case 39:
                                    SeoString.Append(strDestination + " Health Check Up & Medical Tour & " + strDestination + " Hospitals");
                                    break;
                                //"Spa";
                                case 40:
                                    SeoString.Append(strDestination + " Spa & " + strDestination + " spa");
                                    break;
                            }
                            break;
                        // Title for destination //

                        // Title for location //
                        case SEO_PageType.Location:
                            switch (seoProductCat)
                            {
                                case 29:
                                    SeoString.Append(strLocation + " Hotels Lowest Rate Guarantee, " + strLocation + " " + strDestination + " Hotels");
                                    break;
                                //"Golf Courses";
                                case 32:
                                    SeoString.Append(strLocation + " Golf Courses , " + strDestination + " Golf Course and All Golf Course in Thailand");
                                    break;
                            }
                            break;
                        // Title for location //

                        // Title for product detail //
                        case SEO_PageType.Detail:
                            switch (seoProductCat)
                            {
                                case 29:

                                    strTitle = strProduct;

                                    // Book Now Pay Later //
                                    if (strProductPaymentType == "2")
                                    {
                                        if (bolDesInProduct) // has destination in product title
                                        {
                                            if ((intProductLen + 19) <= 65)
                                            {
                                                strTitle = strProduct + " Book Now Pay Later";
                                            }

                                            if ((intProductLen + 28) <= 65)
                                            {
                                                strTitle = strProduct + " Thailand Book Now Pay Later";
                                            }
                                        }
                                        else // don't have destination in product title
                                        {
                                            if ((intProductLen + intDesLen + 1) <= 65)
                                            {
                                                strTitle = strProduct + " " + strDestination;
                                            }

                                            if ((intProductLen + 19) <= 65)
                                            {
                                                strTitle = strProduct + " Book Now Pay Later";
                                            }

                                            if ((intProductLen  + 28) <= 65)
                                            {
                                                strTitle = strProduct + " Thailand Book Now Pay Later";
                                            }

                                            if ((intProductLen + intDesLen + 1 + 19) <= 65)
                                            {
                                                strTitle = strProduct + " " + strDestination + " Book Now Pay Later";
                                            }

                                            if ((intProductLen + intDesLen + 1 + 28) <= 65)
                                            {
                                                strTitle = strProduct + " " + strDestination + " Thailand Book Now Pay Later";
                                            }
                                        }

                                        SeoString.Append(strTitle);
                                        break;
                                    }
                                    // Book Now Pay Later //

                                    // Promotion //
                                    if(String.IsNullOrEmpty(strPromotion))
                                    {
                                        if (bolDesInProduct) // has destination in product title
                                        {
                                            if ((intProductLen + intPromoLen + 1 + 19) <= 65)
                                            {
                                                strTitle = strProduct + " " + strPromotion;
                                            }

                                            if ((intProductLen + intPromoLen + 1 + 28) <= 65)
                                            {
                                                strTitle = strProduct + " " + strPromotion;
                                            }
                                        }
                                        else // don't have destination in product title
                                        {
                                            if ((intProductLen + intDesLen + 1) <= 65)
                                            {
                                                strTitle = strProduct + " " + strDestination;
                                            }

                                            if ((intProductLen + intPromoLen + 1 + 19) <= 65)
                                            {
                                                strTitle = strProduct + " " + strPromotion;
                                            }

                                            if ((intProductLen + intPromoLen + 1 + 28) <= 65)
                                            {
                                                strTitle = strProduct + " " + strPromotion;
                                            }

                                            if ((intProductLen + intDesLen + intPromoLen + 1 + 1 + 19) <= 65)
                                            {
                                                strTitle = strProduct + " " + strDestination + " " + strPromotion;
                                            }

                                            if ((intProductLen + intDesLen + intPromoLen + 1 + 1 + 28) <= 65)
                                            {
                                                strTitle = strProduct + " " + strDestination + " " + strPromotion;
                                            }
                                        }

                                        SeoString.Append(strTitle);
                                        break;
                                    }
                                    // Promotion //

                                    // Normal Case //
                                    if (bolDesInProduct) // has destination in product title
                                    {
                                        if ((intProductLen + 23) <= 65)
                                        {
                                            strTitle = strProduct + " Lowest Rate Guaranteed";
                                        }

                                        if ((intProductLen + 32) <= 65)
                                        {
                                            strTitle = strProduct + " Thailand Lowest Rate Guaranteed";
                                        }
                                    }
                                    else // don't have destination in product title
                                    {
                                        if ((intProductLen + intDesLen + 1) <= 65)
                                        {
                                            strTitle = strProduct + " " + strDestination;
                                        }

                                        if ((intProductLen + 23) <= 65)
                                        {
                                            strTitle = strProduct + " Lowest Rate Guaranteed";
                                        }

                                        if ((intProductLen + 32) <= 65)
                                        {
                                            strTitle = strProduct + " Thailand Lowest Rate Guaranteed";
                                        }

                                        if ((intProductLen + intDesLen + 1 + 23) <= 65)
                                        {
                                            strTitle = strProduct + " " + strDestination + " Lowest Rate Guaranteed";
                                        }

                                        if ((intProductLen + intDesLen + 1 + 32) <= 65)
                                        {
                                            strTitle = strProduct + " " + strDestination + " Thailand Lowest Rate Guaranteed";
                                        }

                                    }
                                    // Normal Case //

                                    SeoString.Append(strTitle);
                                    break;

                                //"Golf Courses,Shows,Health Check Up,Spa";
                                case 32: case 38: case 39: case 40:
                                    strTitle = strProduct;

                                    if (! bolDesInProduct)
                                    {
                                        strTitle = strTitle + " " + strDestination;
                                    }

                                    if (intProductLen + intDesLen + 23 < 65)
                                    {
                                        strTitle = strTitle + " Lowest Rate Guaranteed";
                                    }

                                    SeoString.Append(strTitle);
                                    break;

                                //Day Trips,Water Activity,
                                case 34: case 36:
                                    strTitle = strProduct;

                                    if (! bolDesInProduct)
                                    {
                                        strTitle = strTitle + " " + strDestination;
                                    }

                                    if (intProductLen + intDesLen + 24 < 65)
                                    {
                                        strTitle = strTitle + " Booking with Lower Rate";
                                    }

                                    SeoString.Append(strTitle);
                                    break;
                            }
                            break;
                        // Title for product detail //

                        case SEO_PageType.Information:
                            break;
                        case SEO_PageType.Photo:
                            break;
                        case SEO_PageType.Review:
                            break;
                        case SEO_PageType.Why:
                            break;
                        case SEO_PageType.Contact:
                            break;
                    }
                    break;

                case 2:
                    switch (seoPageType)
                    {
                        // Title for home //
                        case SEO_PageType.Home:
                            SeoString.Append("โรงแรม รีสอร์ท ที่พัก จองโรงแรมทั่วไทยราคาถูก บริการโดยคนไทยค่ะ");
                            break;
                        // Title for home //

                        // Title for destination //
                        case SEO_PageType.Destination:
                            switch (seoProductCat)
                            {
                                case 29:
                                    SeoString.Append("โรงแรมใน" + strDestination + " จองโรงแรมใน" + strDestination + "ราคาถูก บริการโดยคนไทยค่ะ");
                                    break;
                                //"Golf Courses";
                                case 32:
                                    SeoString.Append("สนามกล์อฟ จองสนามกล์อฟใน" + strDestination + " และสนามกล์อฟทั่วไทย");
                                    break;
                                //"Day Trips";
                                case 34:
                                    switch (strDestination.Trim().ToLower())
                                    {
                                        case "bangkok":
                                            SeoString.Append("กรุงเทพเดย์ทริป ทัวร์กรุงเทพ วัดพระแก้ว ตลาดน้ำ สถานที่น่าสนใจอื่นๆ");
                                            break;
                                        case "phuket":
                                            SeoString.Append("ภูเก็ตเดย์ทริป ทัวร์ภูเก็ต ดำน้ำ ดูประการัง และสถานที่น่าสนใจอื่นๆ");
                                            break;
                                        default:
                                            SeoString.Append(strDestination + "เดย์ทริป ทัวร์" + strDestination + " เที่ยวสถานที่น่าสนใจใน" + strDestination);
                                            break;
                                    }
                                    break;
                                //"Water Activity";
                                case 36:
                                    switch (strDestination.Trim().ToLower())
                                    {
                                        case "ภูเก็ต":
                                            SeoString.Append("ดำน้ำ ตกปลา ดูประการัง ภูเก็ต เกาะพีพี อ่าวมาหยา เกาะสิมิลัน");
                                            break;
                                        default:
                                            SeoString.Append("กิจกรรมทางน้ำที่"+ strDestination +" ดำน้ำ ตกปลา ล่องแพ และกิจกรรมอื่นๆ");
                                            break;
                                    }
                                    break;
                                //"Shows";
                                case 38:
                                    switch (strDestination.Trim().ToLower())
                                    {
                                        case "bangkok":
                                            SeoString.Append("บัตรเข้าชมการแสดงในกรุงเทพ สยามนิรมิต มาดามทุสโซ และการแสดงที่น่าสนใจอื่นๆ");
                                            break;
                                        case "phuket":
                                            SeoString.Append("บัตรเข้าชมการแสดงในภูเก็ต ภูเก็ตแฟนตสซี คาบาเร่ต์ และการแสดงที่น่าสนใจอื่นๆ");
                                            break;
                                        case "pattaya":
                                            SeoString.Append("บัตรเข้าชมการแสดงในพัทยา ทิฟฟานี่ อัลคาซ่า และการแสดงที่น่าสนใจอื่นๆ");
                                            break;
                                        default:
                                            SeoString.Append("จองบัตรเข้าชมการแสดงใน" + strDestination + " รับประกันราคาถูกที่สุด");
                                            break;
                                    }
                                    break;
                                //"Health Check Up";
                                case 39:
                                    SeoString.Append("แพ็คเกจตรวจสุขภาพใน " + strDestination + " โรงพยาบาลใน " + strDestination);
                                    break;
                                //"Spa";
                                case 40:
                                    SeoString.Append("แพ็คเกจสปาใน " + strDestination + " รับประกันราคาถูกที่สุด");
                                    break;
                            }
                            break;
                        // Title for destination //

                        // Title for location //
                        case SEO_PageType.Location:
                            switch (seoProductCat)
                            {
                                case 29:
                                    SeoString.Append("โรงแรมใน" + strLocation + " จองโรงแรมใน" + strLocation + "ราคาถูก บริการโดยคนไทยค่ะ");
                                    break;
                                //"Golf Courses";
                                case 32:
                                    SeoString.Append("สนามกล์อฟ จองสนามกล์อฟใน" + strLocation + " และสนามกล์อฟทั่วไทย");
                                    break;
                            }
                            break;
                        // Title for location //

                        // Title for product detail //
                        case SEO_PageType.Detail:
                            switch (seoProductCat)
                            {
                                case 29:

                                    strTitle = strProduct;

                                    // Book Now Pay Later //
                                    if (strProductPaymentType == "2")
                                    {
                                        if (bolDesInProduct) // has destination in product title
                                        {
                                            strTitle = strProduct + " จองก่อนจ่ายที่หลัง";
                                        }
                                        else // don't have destination in product title
                                        {
                                            if ((intProductLen + intDesLen + 1) <= 65)
                                            {
                                                strTitle = strProduct + " " + strDestination;
                                            }

                                            strTitle = strProduct + " จองก่อนจ่ายที่หลัง";
                                        }

                                        SeoString.Append(strTitle);
                                        break;
                                    }
                                    // Book Now Pay Later //

                                    // Promotion //
                                    if (String.IsNullOrEmpty(strPromotion))
                                    {
                                        if (bolDesInProduct) // has destination in product title
                                        {
                                            if ((intProductLen + intPromoLen + 1 + 19) <= 65)
                                            {
                                                strTitle = strProduct + " " + strPromotion;
                                            }

                                            if ((intProductLen + intPromoLen + 1 + 28) <= 65)
                                            {
                                                strTitle = strProduct + " " + strPromotion;
                                            }
                                        }
                                        else // don't have destination in product title
                                        {
                                            if ((intProductLen + intDesLen + 1) <= 65)
                                            {
                                                strTitle = strProduct + " " + strDestination;
                                            }

                                            if ((intProductLen + intPromoLen + 1 + 19) <= 65)
                                            {
                                                strTitle = strProduct + " " + strPromotion;
                                            }

                                            if ((intProductLen + intPromoLen + 1 + 28) <= 65)
                                            {
                                                strTitle = strProduct + " " + strPromotion;
                                            }

                                            if ((intProductLen + intDesLen + intPromoLen + 1 + 1 + 19) <= 65)
                                            {
                                                strTitle = strProduct + " " + strDestination + " " + strPromotion;
                                            }

                                            if ((intProductLen + intDesLen + intPromoLen + 1 + 1 + 28) <= 65)
                                            {
                                                strTitle = strProduct + " " + strDestination + " " + strPromotion;
                                            }
                                        }

                                        SeoString.Append(strTitle);
                                        break;
                                    }
                                    // Promotion //

                                    // Normal Case //
                                    if (bolDesInProduct) // has destination in product title
                                    {
                                        strTitle = strProduct + " รับประกันราคาถูกที่สุด";
                                    }
                                    else // don't have destination in product title
                                    {
                                        if ((intProductLen + intDesLen + 1) <= 65)
                                        {
                                            strTitle = strProduct + " " + strDestination;
                                        }

                                        if ((intProductLen + 23) <= 65)
                                        {
                                            strTitle = strProduct + " รับประกันราคาถูกที่สุด";
                                        }

                                        if ((intProductLen + 32) <= 65)
                                        {
                                            strTitle = strProduct + " รับประกันราคาถูกที่สุด";
                                        }

                                        if ((intProductLen + intDesLen + 1 + 23) <= 65)
                                        {
                                            strTitle = strProduct + " " + strDestination + " รับประกันราคาถูกที่สุด";
                                        }

                                        if ((intProductLen + intDesLen + 1 + 32) <= 65)
                                        {
                                            strTitle = strProduct + " " + strDestination + " รับประกันราคาถูกที่สุด";
                                        }

                                    }
                                    // Normal Case //

                                    SeoString.Append(strTitle);
                                    break;

                                //"Golf Courses,Shows,Health Check Up,Spa";
                                case 32:
                                case 38:
                                case 39:
                                case 40:
                                    strTitle = strProduct;

                                    if (!bolDesInProduct)
                                    {
                                        strTitle = strTitle + " " + strDestination;
                                    }

                                    if (intProductLen + intDesLen + 23 < 65)
                                    {
                                        strTitle = strTitle + " รับประกันราคาถูกที่สุด";
                                    }

                                    SeoString.Append(strTitle);
                                    break;

                                //Day Trips,Water Activity,
                                case 34:
                                case 36:
                                    strTitle = strProduct;

                                    if (!bolDesInProduct)
                                    {
                                        strTitle = strTitle + " " + strDestination;
                                    }

                                    if (intProductLen + intDesLen + 24 < 65)
                                    {
                                        strTitle = strTitle + " รับประกันราคาถูกที่สุด";
                                    }

                                    SeoString.Append(strTitle);
                                    break;
                            }
                            break;
                        // Title for product detail //

                        case SEO_PageType.Information:
                            break;
                        case SEO_PageType.Photo:
                            break;
                        case SEO_PageType.Review:
                            break;
                        case SEO_PageType.Why:
                            break;
                        case SEO_PageType.Contact:
                            break;
                    }
                    break;
                case 3:
                    break;
            }

            return SeoString.ToString();
        }

        private static string Meta_Gen(SEO_PageType seoPageType, byte langID, byte seoProductCat, string strDestination, string strLocation, string strProduct, string strKeyWord, string strPromotion, string strLowestRate, string strProductNumHotel, string strNumProductOther)
        {
            StringBuilder SeoString = new StringBuilder();
            string strMetaDes = "";
            string strMetaKey = "";
            //bool bolDesInProduct = false;
            int intProductLen = strProduct.Length;
            int intDesLen = strDestination.Length;
            int intPromoLen = strPromotion.Length;

            //if (strProduct.IndexOf(strDestination) >= 0)
            //{
            //    bolDesInProduct = true;
            //}
            //else
            //{
            //    bolDesInProduct = false;
            //    strProduct = strProduct + " " + strDestination;
            //    intProductLen = strProduct.Length;
            //}

            switch (langID)
            {
                case 1:
                    switch (seoPageType)
                    {
                        // Meta for home //
                        case SEO_PageType.Home:
                            SeoString.Append("<meta name=\"description\" content=\"Thailand Hotels, Hotels2Thailand offers (" + DateTime.Today.ToString("MMMMM dd") + ") " + strProductNumHotel + " hotels and " + strNumProductOther + " travel products (show, day trip etc.) in Thailand with LOWEST RATE GUARANTEE.\"/>\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"Thailand Hotels,Hotels,holiday,travel,Bangkok,Phuket,Pattaya,Koh Samui, Koh Chang, Hat Yai,Koh Phagan,Chiang Mai,Hotels2Thailand,Hotel2Thailand,online reservertion,booking,discount,cheap,low rate,lowest rate,acommodation,accomodation,acomodation,accommodation\"/>\r\n");
                            break;
                        // Meta for home //

                        // Meta for destination //
                        case SEO_PageType.Destination:
                            switch (seoProductCat)
                            {
                                //"Hotels";
                                case 29:
                                    SeoString.Append("<meta name=\"description\" content=\"" + strDestination + " Hotels Thailand Hotels2Thailand offer (" + DateTime.Today.ToString("MMMMM dd") + ") " + strProductNumHotel + " hotels in " + strDestination + ". Online booking " + strDestination + " Hotels with LOWEST RATE GUARANTEE. \"/>\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"" + strDestination + " Hotels,Hotels,resorts,travel,holidays,package,vocation,Hotels2Thailand,booking,online,reservation,accommodation,lodging,discount,cheap,lowest rate\"/>\r\n");
                                    break;
                                //"Golf Courses";
                                case 32:
                                    SeoString.Append("<meta name=\"description\" content=\"" + strDestination + " Golf Courses All Golf Course in " + strDestination + "\">\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"" + strDestination + " golf courses, golf courses in " + strDestination + ", golf courses in " + strDestination + " Thailand\">\r\n");
                                    break;
                                //"Day Trips";
                                case 34:
                                    switch (strDestination.Trim().ToLower())
                                    {
                                        case "bangkok":
                                            SeoString.Append("<meta name=\"description\" content=\"Bangkok Tours, Day Trips, Vacation Package, NightLife, Floating Market, Grand Palace, Wat Arun, Wat Pho, Sightseeing and All Activities and Attractions in Bangkok\"/>\r\n");
                                            SeoString.Append("<meta name=\"keywords\" content=\"bangkok tours, bangkok day trips,bangkok vacation, bangkok night life, grand palace, floating market, weekend market, wat arun, wat pho, bangkok sight seeing, bangkok attraction\"/>\r\n");
                                            break;
                                        case "phuket":
                                            SeoString.Append("<meta name=\"description\" content=\"Phuket Tours, Day Trips,Diving, Vacation, Trip, NightLife, Spa, Masaage, Speed Boat, Canoe, Kayak and All Activities and Attractions in Phuket\"/>\r\n");
                                            SeoString.Append("<meta name=\"keywords\" content=\"phukt tours, phuket day trips, phuket vacation, phukettrip, phuket nightlife, phuket spa, phuket massage, phuket speed boat, phuket conoe, phuket kayak, phuket water activities\"/>\r\n");
                                            break;
                                        default:
                                            SeoString.Append("<meta name=\"description\" content=\"" + strDestination + " daytrips, package tours, sightseeing " + strDestination + " and all activities in " + strDestination + " with lower price.\"/>\r\n");
                                            SeoString.Append("<meta name=\"keywords\" content=\"" + strDestination + " sightseeing, package tours in " + strDestination + ", trip in " + strDestination + " Thailand\">\r\n");
                                            break;
                                    }

                                    break;
                                //"Water Activity";
                                case 36:
                                    switch (strDestination.Trim().ToLower())
                                    {
                                        case "phuket":
                                            SeoString.Append("<meta name=\"description\" content=\"Phuket Water Activities : Diving Canoe Kayak Boat Trips Snorkeling Fishing Rafting with Online Booking\"/>\r\n");
                                            SeoString.Append("<meta name=\"keywords\" content=\"phukt tours, phuket, diving, canoe, kayak, boat trips, snorkeling, fishing, rafting\">\r\n");
                                            break;
                                        default:
                                            SeoString.Append("<meta name=\"description\" content=\"" + strDestination + " Diving Canoe Kayak Boat Trips Snorkeling Fishing Rafting with Online Booking\"/>\r\n");
                                            SeoString.Append("<meta name=\"keywords\" content=\"" + strDestination + " tours, " + strDestination + ", diving, canoe, kayak, boat trips, snorkeling, fishing, rafting\"/>\r\n");
                                            break;
                                    }

                                    break;
                                //"Shows";
                                case 38:
                                    switch (strDestination.Trim().ToLower())
                                    {
                                        case "bangkok":
                                            SeoString.Append("<meta name=\"description\" content=\"Bangkok Shows & Events : Siam Niramit, Joe Louis theater, Thai Boxing, Booking All Shows & Events in Bangkok\"/>\r\n");
                                            SeoString.Append("<meta name=\"keywords\" content=\"bangkok shows, bangkok events, siam niramit, joe louis theater, thai boxing\">\r\n");
                                            break;
                                        case "phuket":
                                            SeoString.Append("<meta name=\"description\" content=\"Phuket Shows & Events : Phuket Fantasea, Thai Boxing, Booking All Shows & Events in Phuket\"/>\r\n");
                                            SeoString.Append("<meta name=\"keywords\" content=\"phuket shows, phuket events, phuket fantasea, thai boxing\">\r\n");
                                            break;
                                        case "pattaya":
                                            SeoString.Append("<meta name=\"description\" content=\"Pattaya Shows & Events : Tiffany Show, Pattaya Alcazar Show, Alangkarn Pattaya, Booking All Shows & Events in Pattaya\"/>\r\n");
                                            SeoString.Append("<meta name=\"keywords\" content=\"pattaya shows, pattaya events, tiffany show, pattaya alcazar show, alangkarn pattaya\">\r\n");
                                            break;
                                        default:
                                            SeoString.Append("<meta name=\"description\" content=\"" + strDestination + " Shows & Events Booking All Shows & Events in " + strDestination + " with lower price.\"/>\r\n");
                                            SeoString.Append("<meta name=\"keywords\" content=\"" + strDestination + " shows,events " + strDestination + ", trip in " + strDestination + " Thailand\">\r\n");
                                            break;
                                    }

                                    break;
                                //"Health Check Up";
                                case 39:
                                    SeoString.Append("<meta name=\"description\" content=\"" + strDestination + " Health Check Up & Medical Tour " + strDestination + " Hospitals\">\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"" + strDestination + " health check up,hospita " + strDestination + ", medical tour in " + strDestination + " Thailand\"/>\r\n");
                                    break;
                                //"Spa";
                                case 40:
                                    SeoString.Append("<meta name=\"description\" content=\"" + strDestination + " Spa" + strDestination + " Spa\">\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"" + strDestination + " spa" + strDestination + ", spa in " + strDestination + " Thailand\"/>\r\n");
                                    break;
                            }
                            break;
                        // Meta for destination //

                        // Meta for location //
                        case SEO_PageType.Location:
                            switch (seoProductCat)
                            {
                                //"Hotels";
                                case 29:
                                    SeoString.Append("<meta name=\"description\" content=\"" + strLocation + " Hotels " + strDestination + " Thailand Hotels2Thailand offer (" + DateTime.Today.ToString("MMMMM dd") + ") " + strProductNumHotel + " hotels in " + strLocation + ". Booking " + strLocation + " Hotels with LOWEST RATE GUARANTEE.\"/>\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\""+strLocation+" Hotels,"+strDestination+" Hotels," + strLocation + " " + strDestination + ",Hotels,resorts,travel,holidays,package,vocation,Hotels2Thailand,booking,online,reservation,accommodation,lodging,discount,cheap,lowest rate\"/>\r\n");
                                    break;
                                //"Golf Courses";
                                case 32:
                                    SeoString.Append("<meta name=\"description\" content=\"" + strLocation + " Golf Courses All Golf Course in " + strLocation + "\">\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"" + strLocation + " golf courses, golf courses in " + strLocation + ", golf course in " + strLocation + " Thailand\"/>\r\n");
                                    break;
                            }
                            break;
                        // Meta for location //

                        case SEO_PageType.Detail:
                            switch (seoProductCat)
                            {
                                case 29:

                                    // Default Case //
                                    strMetaDes = strProduct + " Online Booking with LOWEST RATE GUARANTEED";
                                    strMetaKey = strProduct + "," + strDestination + " Hotel," + strLocation + " Hotel,Hotels,resorts,travel,holidays,package,vocation,Hotels2Thailand,booking,online,reservation,accomodation,lodging,discount,cheap,lowest rate";
                                    // Default Case //

                                    // Promotion //
                                    if (String.IsNullOrEmpty(strPromotion))
                                    {
                                        if ((2*intProductLen + intPromoLen + 70) <= 150) // promotion + product
                                        {
                                            strMetaDes = strProduct + " Thailand Promotion " + strPromotion + ". (" + DateTime.Today.ToString("MMMMM dd") + ") Online Booking " + strProduct + " with LOWEST RATE GUARANTEED";
                                        }

                                        if ((intProductLen + intPromoLen + 98) <= 150) // promotion + price
                                        {
                                            strMetaDes = strProduct + " Thailand Promotion " + strPromotion + ". price starts from " + strLowestRate + " Baht (" + DateTime.Today.ToString("MMMMM dd") + ") Online Booking with LOWEST RATE GUARANTEED";
                                        }

                                        if ((2 * intProductLen + intPromoLen + 98) <= 150) // promotion + product + price
                                        {
                                            strMetaDes = strProduct + " Thailand Promotion " + strPromotion + ". price starts from " + strLowestRate + " Baht (" + DateTime.Today.ToString("MMMMM dd") + ") Online Booking " + strProduct + " with LOWEST RATE GUARANTEED";
                                        }
                                        SeoString.Append("<meta name=\"description\" content=\"" + strMetaDes + "\"/>\r\n");
                                        SeoString.Append("<meta name=\"keywords\" content=\"" + strMetaKey + "\"/>\r\n");
                                        break;
                                    }
                                    // Promotion //

                                    // Normal case //
                                    if ((2 * intProductLen + 98) <= 150) // product + price
                                    {
                                        strMetaDes = strProduct + ". price starts from " + strLowestRate + " Baht (" + DateTime.Today.ToString("MMMMM dd") + ") Online Booking " + strProduct + " with LOWEST RATE GUARANTEED";
                                    }

                                    if ((intProductLen + 98) <= 150) // product + price
                                    {
                                        strMetaDes = strProduct + ". price starts from " + strLowestRate + " Baht (" + DateTime.Today.ToString("MMMMM dd") + ") Online Booking with LOWEST RATE GUARANTEED";
                                    }
                                    // Normal case //
                                    SeoString.Append("<meta name=\"description\" content=\"" + strMetaDes + "\"/>\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"" + strMetaKey + "\"/>\r\n");
                                    break;

                                //"Golf Courses";
                                case 32:
                                    // Default Case //
                                    strMetaDes = strProduct + " Online Booking with LOWEST RATE GUARANTEED";
                                    strMetaKey = strProduct + "," + strDestination + " Golf Courses,golf,Thailand,travel,holidays,package,vocation,Hotels2Thailand,booking,online,reservation,discount,cheap,lowest rate";
                                    // Default Case //

                                    // Normal case //
                                    if ((2 * intProductLen + 85) <= 150) // product + price
                                    {
                                        strMetaDes = strProduct + " . price starts from " + strLowestRate + " Baht (" + DateTime.Today.ToString("MMMMM dd") + ") Online Booking " + strProduct + " with LOWEST RATE GUARANTEED";
                                    }

                                    if ((intProductLen + 85) <= 150) // product + price
                                    {
                                        strMetaDes = strProduct + " . price starts from " + strLowestRate + " Baht (" + DateTime.Today.ToString("MMMMM dd") + ") Online Booking with LOWEST RATE GUARANTEED";
                                    }
                                    // Normal case //

                                    SeoString.Append("<meta name=\"description\" content=\"" + strMetaDes + "\"/>\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"" + strMetaKey + "\"/>\r\n");
                                    break;

                                //"Day Trips";
                                case 34:
                                    // Default Case //
                                    strMetaDes = strProduct + " Online Booking with LOWER RATE";
                                    strMetaKey = strProduct + "," + strDestination + " Day Trips,tour,Thailand,travel,holidays,package,vocation,Hotels2Thailand,booking,online,reservation,discount,cheap,lowest rate";
                                    // Default Case //

                                    // Normal case //
                                    if ((2 * intProductLen + 73) <= 150) // product + price
                                    {
                                        strMetaDes = strProduct + " . price starts from " + strLowestRate + " Baht (" + DateTime.Today.ToString("MMMMM dd") + ") Online Booking " + strProduct + " with LOWER RATE";
                                    }

                                    if ((intProductLen + 73) <= 150) // product + price
                                    {
                                        strMetaDes = strProduct + " . price starts from " + strLowestRate + " Baht (" + DateTime.Today.ToString("MMMMM dd") + ") Online Booking with LOWER RATE";
                                    }
                                    // Normal case //
                                    SeoString.Append("<meta name=\"description\" content=\"" + strMetaDes + "\"/>\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"" + strMetaKey + "\"/>\r\n");
                                    break;

                                //"Water Activity";
                                case 36:
                                    // Default Case //
                                    strMetaDes = strProduct + " Online Booking with LOWER RATE";
                                    strMetaKey = strProduct + "," + strDestination + " Water Activity,day trips,tour,Thailand,travel,holidays,package,vocation,Hotels2Thailand,booking,online,reservation,discount,cheap,lowest rate";
                                    // Default Case //

                                    // Normal case //
                                    if ((2 * intProductLen + 73) <= 150) // product + price
                                    {
                                        strMetaDes = strProduct + " . price starts from " + strLowestRate + " Baht (" + DateTime.Today.ToString("MMMMM dd") + ") Online Booking " + strProduct + " with LOWER RATE";
                                    }

                                    if ((intProductLen + 73) <= 150) // product + price
                                    {
                                        strMetaDes = strProduct + " . price starts from " + strLowestRate + " Baht (" + DateTime.Today.ToString("MMMMM dd") + ") Online Booking with LOWER RATE";
                                    }
                                    // Normal case //
                                    SeoString.Append("<meta name=\"description\" content=\"" + strMetaDes + "\"/>\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"" + strMetaKey + "\"/>\r\n");
                                    break;

                                //"Shows";
                                case 38:
                                    // Default Case //
                                    strMetaDes = strProduct + " Online Booking with LOWEST RATE GUARANTEED";
                                    strMetaKey = strProduct + "," + strDestination + " Show & Event Ticket,travel,holidays,package,vocation,Hotels2Thailand,booking,online,reservation,discount,cheap,lowest rate";
                                    // Default Case //

                                    // Normal case //
                                    if ((2 * intProductLen + 91) <= 150) // product + price
                                    {
                                        strMetaDes = strProduct + " ticket. price starts from " + strLowestRate + " Baht (" + DateTime.Today.ToString("MMMMM dd") + ") Online Booking " + strProduct + " with LOWEST RATE GUARANTEED";
                                    }

                                    if ((intProductLen + 91) <= 150) // product + price
                                    {
                                        strMetaDes = strProduct + " ticket. price starts from " + strLowestRate + " Baht (" + DateTime.Today.ToString("MMMMM dd") + ") Online Booking with LOWEST RATE GUARANTEED";
                                    }
                                    // Normal case //

                                    SeoString.Append("<meta name=\"description\" content=\"" + strMetaDes + "\"/>\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"" + strMetaKey + "\"/>\r\n");
                                    break;

                                //"Health Check Up";
                                case 39:
                                    // Default Case //
                                    strMetaDes = strProduct + " Online Booking with LOWEST RATE GUARANTEED";
                                    strMetaKey = strProduct + "," + strDestination + " Health Checkup,medical,health,travel,holidays,package,vocation,Hotels2Thailand,booking,online,reservation,discount,cheap,lowest rate";
                                    // Default Case //

                                    // Normal case //
                                    if ((2 * intProductLen + 91) <= 150) // product + price
                                    {
                                        strMetaDes = strProduct + " ticket. price starts from " + strLowestRate + " Baht (" + DateTime.Today.ToString("MMMMM dd") + ") Online Booking " + strProduct + " with LOWEST RATE GUARANTEED";
                                    }

                                    if ((intProductLen + 91) <= 150) // product + price
                                    {
                                        strMetaDes = strProduct + " ticket. price starts from " + strLowestRate + " Baht (" + DateTime.Today.ToString("MMMMM dd") + ") Online Booking with LOWEST RATE GUARANTEED";
                                    }
                                    // Normal case //

                                    SeoString.Append("<meta name=\"description\" content=\"" + strMetaDes + "\"/>\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"" + strMetaKey + "\"/>\r\n");
                                    break;

                                //"Spa";
                                case 40:
                                    // Default Case //
                                    strMetaDes = strProduct + " Online Booking with LOWEST RATE GUARANTEED";
                                    strMetaKey = strProduct + "," + strDestination + " Spa,massage,travel,holidays,package,vocation,Hotels2Thailand,booking,online,reservation,discount,cheap,lowest rate";
                                    // Default Case //

                                    // Normal case //
                                    if ((2 * intProductLen + 91) <= 150) // product + price
                                    {
                                        strMetaDes = strProduct + " ticket. price starts from " + strLowestRate + " Baht (" + DateTime.Today.ToString("MMMMM dd") + ") Online Booking " + strProduct + " with LOWEST RATE GUARANTEED";
                                    }

                                    if ((intProductLen + 91) <= 150) // product + price
                                    {
                                        strMetaDes = strProduct + " ticket. price starts from " + strLowestRate + " Baht (" + DateTime.Today.ToString("MMMMM dd") + ") Online Booking with LOWEST RATE GUARANTEED";
                                    }
                                    // Normal case //

                                    SeoString.Append("<meta name=\"description\" content=\"" + strMetaDes + "\"/>\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"" + strMetaKey + "\"/>\r\n");
                                    break;
                            }

                            break;

                        case SEO_PageType.Information:
                            break;
                        case SEO_PageType.Photo:
                            break;
                        case SEO_PageType.Review:
                            break;
                        case SEO_PageType.Why:
                            break;
                        case SEO_PageType.Contact:
                            break;
                    }
                    break;

                case 2:
                
                    switch (seoPageType)
                    {
                        // Meta for home //
                        case SEO_PageType.Home:
                            SeoString.Append("<meta name=\"description\" content=\"จองโรงแรม ที่พัก รีสอร์ท สนามกล์อฟ ทัวร์ เดย์ทริป ดำน้ำ โชว์ ทั่วไทย รับประกันราคาถูกที่สุด\"/>\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"โรงแรม,จองโรงแรม,ที่พัก,รีสอร์ท,เดย์ทริป,ดำน้ำ,สนามกล์อฟ,กรุงเทพ,พัทยา,สมุย,ภูเก็ต,กระบี่\"/>\r\n");
                            break;
                        // Meta for home //

                        // Meta for destination //
                        case SEO_PageType.Destination:
                            switch (seoProductCat)
                            {
                                //"Hotels";
                                case 29:
                                    SeoString.Append("<meta name=\"description\" content=\"บริการรับจองโรงแรม รีสอร์ท ที่พัก ใน" + strDestination + " และทั่วไทย รับประกันราคาถูกที่สุด บริการโดยคยไทย  \"/>\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"จองโรงแรม,โรงแรม,ที่พัก," + strDestination + ",รีสอร์ท\"/>\r\n");
                                    break;
                                //"Golf Courses";
                                case 32:
                                    SeoString.Append("<meta name=\"description\" content=\"จองสนามกล์อฟและกล์อฟแพ็คเกจใน" + strDestination + " และทั่วไทย\">\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"สนามกล์อฟ," + strDestination + ",กล์อฟแพ็คเกจ\">\r\n");
                                    break;
                                //"Day Trips";
                                case 34:
                                    switch (strDestination.Trim().ToLower())
                                    {
                                        case "bangkok":
                                            SeoString.Append("<meta name=\"description\" content=\"กรุงเทพเดย์ทริป ทัวร์ ตลาดน้ำ วัดพระแก้ว และสถานที่ท่องเที่ยวที่น่าสนใจอื่นๆ รับประกันราคาถูกสุด\"/>\r\n");
                                            SeoString.Append("<meta name=\"keywords\" content=\"กรุงเทพ,ตลาดน้ำ,ตลาดน้ำดำเนินสะดวก,วัดพระแก้ว,วัดอรุณ,วัดโพธิ์,จัตุจักร,เดย์ทริป,ทัวร์\"/>\r\n");
                                            break;
                                        case "phuket":
                                            SeoString.Append("<meta name=\"description\" content=\"ภูเก็ตเดย์ทริป ทัวร์ สปา ท่องเที่ยวสถานที่น่าสนใจในภูเก็ต รับประกันราคาถูกสุด\"/>\r\n");
                                            SeoString.Append("<meta name=\"keywords\" content=\"ภูเก็ต,เดย์ทริป,ทัวร์,แคนนู,คายัค\"/>\r\n");
                                            break;
                                        default:
                                            SeoString.Append("<meta name=\"description\" content=\"" + strDestination + "เดย์ทริป แพ็คเกจทัวร์ รับประกันราคาถูกสุด\"/>\r\n");
                                            SeoString.Append("<meta name=\"keywords\" content=\"" + strDestination + "เดย์ทริป,ทัวร์,อพคเกจทัวร์\">\r\n");
                                            break;
                                    }

                                    break;
                                //"Water Activity";
                                case 36:
                                    switch (strDestination.Trim().ToLower())
                                    {
                                        case "phuket":
                                            SeoString.Append("<meta name=\"description\" content=\"ทัวร์ภูเก็ต ดำน้ำ ตกปลา ดูประการังที่ภูเก็ต ทัวร์อ่าวมาหยา และสถานที่ทางทะเลที่น่าสนใจอื่นๆ รับประกันราคาถูกที่สุด\"/>\r\n");
                                            SeoString.Append("<meta name=\"keywords\" content=\"phukt tours, phuket, diving, canoe, kayak, boat trips, snorkeling, fishing, rafting\">\r\n");
                                            break;
                                        default:
                                            SeoString.Append("<meta name=\"description\" content=\"ดำน้ำ ตกปลา ดูประการังที่" + strDestination + " และสถานที่ที่น่าสนใจอื่นๆ รับประกันราคาถูกที่สุด\"/>\r\n");
                                            SeoString.Append("<meta name=\"keywords\" content=\"" + strDestination + " ทัวร์,ดำน้ำ,ตกปลา,ดูประการัง,ล่องแก่ง,กิจกรรมทางน้ำ\"/>\r\n");
                                            break;
                                    }

                                    break;
                                //"Shows";
                                case 38:
                                    switch (strDestination.Trim().ToLower())
                                    {
                                        case "bangkok":
                                            SeoString.Append("<meta name=\"description\" content=\"จองบัตรเข้าชมการแสดงและโชว์ในกรุงเทพ สยามนิรมิตร มาดาทุสโซ สยามโอเชียนเวิร์ล และสถานที่ที่น่าสนใจอื่นๆในกรุงเทพ\"/>\r\n");
                                            SeoString.Append("<meta name=\"keywords\" content=\"การแสดงในกรุงเทพ,โชว์ในกรุงเทพ,สยามนิรมิตร,มาดามทุสโซ,สยามโอเชียนเวิร์ล,คาบาเรต์\">\r\n");
                                            break;
                                        case "phuket":
                                            SeoString.Append("<meta name=\"description\" content=\"จองบัตรเข้าชมการแสดงและโชว์ในภูเก็ต ภูเก็ตแฟนตาซี ไซม่อนคาบาเรต์ และสถานที่ที่น่าสนใจอื่นๆในภูเก็ต\"/>\r\n");
                                            SeoString.Append("<meta name=\"keywords\" content=\"การแสดงในภูเก็ต,โชว์ในภูเก็ต,ภูเก็ตแฟนตาซี,ไซม่อน คาบาเรต์,สยามนิรมิตร\">\r\n");
                                            break;
                                        case "pattaya":
                                            SeoString.Append("<meta name=\"description\" content=\"จองบัตรเข้าชมการแสดงและโชว์ในพัทยา ทิฟฟานี่โชว์ อลังการ สวนนงนุช ริบลี่ส์ และสถานที่ที่น่าสนใจอื่นๆในพัทยา\"/>\r\n");
                                            SeoString.Append("<meta name=\"keywords\" content=\"การแสดงในพัทยา,โชว์ในพัทยา,ทิฟฟานี่โชว์,อลังการ,สวนนงนุช,ริบลี่ส์,อันเดอร์วอเอร์เวิร์ล\">\r\n");
                                            break;
                                        default:
                                            SeoString.Append("<meta name=\"description\" content=\"" + strDestination + " จองบัตรเข้าชมการแสดงและโชว์ใน" + strDestination + " และสถานที่ที่น่าสนใจอื่นๆใน" + strDestination + " รับประกันราคาถูกที่สุด\"/>\r\n");
                                            SeoString.Append("<meta name=\"keywords\" content=\"การแสดงใน" + strDestination + ",โชว์ใน" + strDestination + ",เที่ยวใน" + strDestination + "\">\r\n");
                                            break;
                                    }

                                    break;
                                //"Health Check Up";
                                case 39:
                                    SeoString.Append("<meta name=\"description\" content=\"แพ็คเกจตรวจสุขภาพใน" + strDestination + " โรงพยาบาลใน " + strDestination + "\">\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"ตรวจสุขภาพใน" + strDestination + ",ทัวร์สุขภาพใน" + strDestination + ",โรงพยาบาลใน" + strDestination + "\"/>\r\n");
                                    break;
                                //"Spa";
                                case 40:
                                    SeoString.Append("<meta name=\"description\" content=\"แพ็คเกจสปาใน" + strDestination + " รับประกันราคาถูกที่สุด\">\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"" + strDestination + ",แพ็คเกจสปา,สปา\"/>\r\n");
                                    break;
                            }
                            break;
                        // Meta for destination //

                        // Meta for location //
                        case SEO_PageType.Location:
                            switch (seoProductCat)
                            {
                                //"Hotels";
                                case 29:
                                    SeoString.Append("<meta name=\"description\" content=\"โรงแรมใน" + strLocation + " จองโรงแรมใน" + strLocation + " รับประกันราคาถูกที่สุด บริการโดยคนไทยค่ะ\"/>\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"จองโรงแรมใน" + strLocation + ",โรงแรมใน" + strLocation + "," + strLocation + ",ที่พัก,รีสอร์ท," + strDestination + "\"/>\r\n");
                                    break;
                                //"Golf Courses";
                                case 32:
                                    SeoString.Append("<meta name=\"description\" content=\"จองสนามกล์อฟใน" + strLocation + " สนามกล์อฟใน" + strLocation + "\">\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"จองสนามกล์อฟใน" + strLocation + ",สนามกล์อฟใน" + strLocation + "," + strDestination + "\"/>\r\n");
                                    break;
                            }
                            break;
                        // Meta for location //

                        case SEO_PageType.Detail:
                            switch (seoProductCat)
                            {
                                case 29:

                                    // Default Case //
                                    strMetaDes = "จอง" + strProduct + " รับประกันราคาถูกที่สุด บริการโดยคนไทยค่ะ";
                                    strMetaKey = strProduct + "," + strDestination + "," + strLocation + ",จองโรงแรม,ที่พัก,รีสอร์ท,โฮเทลทู,โฮเทลทูไทยแลนด์,ราคาถูก,คนไทย";
                                    // Default Case //

                                    // Promotion //
                                    if (String.IsNullOrEmpty(strPromotion))
                                    {
                                        if ((2*intProductLen + intPromoLen + 70) <= 150) // promotion + product
                                        {
                                            strMetaDes = strProduct + " โปรโมชั่นพิเศษ " + strPromotion + " จอง" + strProduct + " รับประกันราคาถูกที่สุด";
                                        }

                                        if ((intProductLen + intPromoLen + 98) <= 150) // promotion + price
                                        {
                                            strMetaDes = strProduct + " โปรโมชั่นพิเศษ " + strPromotion + " ราคาเริ่มที่" + strLowestRate + " บาท รับประกันราคาถูกที่สุด";
                                        }

                                        if ((2 * intProductLen + intPromoLen + 98) <= 150) // promotion + product + price
                                        {
                                            strMetaDes = strProduct + " โปรโมชั่นพิเศษ " + strPromotion + " ราคาเริ่มที่ " + strLowestRate + " บาท จอง" + strProduct + " รับประกันราคาถูกที่สุด";
                                        }
                                        SeoString.Append("<meta name=\"description\" content=\"" + strMetaDes + "\"/>\r\n");
                                        SeoString.Append("<meta name=\"keywords\" content=\"" + strMetaKey + "\"/>\r\n");
                                        break;
                                    }
                                    // Promotion //

                                    // Normal case //
                                    if ((2 * intProductLen + 98) <= 150) // product + price
                                    {
                                        strMetaDes = strProduct + " ราคาเริ่มต้นที่ " + strLowestRate + " บาท จอง" + strProduct + " รับประกันราคาถูกที่สุด";
                                    }

                                    if ((intProductLen + 98) <= 150) // product + price
                                    {
                                        strMetaDes = strProduct + " ราคาเริ่มต้นที่ " + strLowestRate + " บาท รับประกันราคาถูกที่สุด";
                                    }
                                    // Normal case //
                                    SeoString.Append("<meta name=\"description\" content=\"" + strMetaDes + "\"/>\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"" + strMetaKey + "\"/>\r\n");
                                    break;

                                //"Golf Courses";
                                case 32:
                                    // Default Case //
                                    strMetaDes = strProduct + " จอง" + strProduct;
                                    strMetaKey = strProduct + "," + strDestination + " สนามกล์อฟ,กล์อฟ,โฮเทลทู,โฮเทลทูไทยแลนด์";
                                    // Default Case //

                                    // Normal case //
                                    if ((2 * intProductLen + 85) <= 150) // product + price
                                    {
                                        strMetaDes = strProduct + " ราคาเริ่มต้นที่ " + strLowestRate + " บาท จอง" + strProduct;
                                    }

                                    if ((intProductLen + 85) <= 150) // product + price
                                    {
                                        strMetaDes = strProduct + " ราคาเริ่มต้นที่ " + strLowestRate + " บาท จอง" + strProduct;
                                    }
                                    // Normal case //

                                    SeoString.Append("<meta name=\"description\" content=\"" + strMetaDes + "\"/>\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"" + strMetaKey + "\"/>\r\n");
                                    break;

                                //"Day Trips";
                                case 34:
                                    // Default Case //
                                    strMetaDes = strProduct + " รับประกันราคาถูกที่สุด";
                                    strMetaKey = strProduct + "," + strDestination + " เดย์ทริป,ทัวร์,ท่องเที่ยว,วันหยุด,แพ็คเกจ,โฮเทลทู,โฮเทลทูไทยแลนด์,ออนไลน์";
                                    // Default Case //

                                    // Normal case //
                                    if ((2 * intProductLen + 73) <= 150) // product + price
                                    {
                                        strMetaDes = strProduct + " ราคาเริ่มต้นที่ " + strLowestRate + " บาท จอง" + strProduct + " รับประกันราคาถูกที่สุด";
                                    }

                                    if ((intProductLen + 73) <= 150) // product + price
                                    {
                                        strMetaDes = strProduct + " ราคาเริ่มต้นที่ " + strLowestRate + " บาท จอง" + strProduct + " รับประกันราคาถูกที่สุด";
                                    }
                                    // Normal case //
                                    SeoString.Append("<meta name=\"description\" content=\"" + strMetaDes + "\"/>\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"" + strMetaKey + "\"/>\r\n");
                                    break;

                                //"Water Activity";
                                case 36:
                                    // Default Case //
                                    strMetaDes = strProduct + " รับประกันราคาถูกที่สุด";
                                    strMetaKey = strProduct + "," + strDestination + " ดำน้ำ,ตกปลา,ดูประการัง,ทัวร์,แพ็คเกจ,วันหยุด,ท่องเที่ยว,โฮเทลทู,โฮเทลทูไทยแลนด์";
                                    // Default Case //

                                    // Normal case //
                                    if ((2 * intProductLen + 73) <= 150) // product + price
                                    {
                                        strMetaDes = strProduct + " ราคาเริ่มต้นที่ " + strLowestRate + " บาท จอง" + strProduct + " รับประกันราคาถูกที่สุด";
                                    }

                                    if ((intProductLen + 73) <= 150) // product + price
                                    {
                                        strMetaDes = strProduct + " ราคาเริ่มต้นที่ " + strLowestRate + " บาท จอง" + strProduct + " รับประกันราคาถูกที่สุด";
                                    }
                                    // Normal case //
                                    SeoString.Append("<meta name=\"description\" content=\"" + strMetaDes + "\"/>\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"" + strMetaKey + "\"/>\r\n");
                                    break;

                                //"Shows";
                                case 38:
                                    // Default Case //
                                    strMetaDes = strProduct + " รับประกันราคาถูกที่สุด";
                                    strMetaKey = strProduct + "," + strDestination + " โชว์,การแสดง,นิทรรศการ,ทัวร์,แพ็คเกจ,วันหยุด,ท่องเที่ยว,โฮเทลทู,โฮเทลทูไทยแลนด์";
                                    // Default Case //

                                    // Normal case //
                                    if ((2 * intProductLen + 91) <= 150) // product + price
                                    {
                                        strMetaDes = strProduct + " บัตรเข้าชม ราคาเริ่มต้นที่ " + strLowestRate + strLowestRate + " บาท จอง" + strProduct + " รับประกันราคาถูกที่สุด";
                                    }

                                    if ((intProductLen + 91) <= 150) // product + price
                                    {
                                        strMetaDes = strProduct + " บัตรเข้าชม ราคาเริ่มต้นที่ " + strLowestRate + strLowestRate + " บาท จอง" + strProduct + " รับประกันราคาถูกที่สุด";
                                    }
                                    // Normal case //

                                    SeoString.Append("<meta name=\"description\" content=\"" + strMetaDes + "\"/>\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"" + strMetaKey + "\"/>\r\n");
                                    break;

                                //"Health Check Up";
                                case 39:
                                    // Default Case //
                                    strMetaDes = strProduct + " รับประกันราคาถูกที่สุด";
                                    strMetaKey = strProduct + "," + strDestination + " ตรวจสุขภาพ,โรงพยาบาล,ท่องเที่ยวเชิงสุขภาพ,ทัวร์,แพ็คเกจ,วันหยุด,ท่องเที่ยว,โฮเทลทู,โฮเทลทูไทยแลนด์";
                                    // Default Case //

                                    // Normal case //
                                    if ((2 * intProductLen + 91) <= 150) // product + price
                                    {
                                        strMetaDes = strProduct + " ราคาเริ่มต้นที่ " + strLowestRate + " บาท จอง" + strProduct + " รับประกันราคาถูกที่สุด";
                                    }

                                    if ((intProductLen + 91) <= 150) // product + price
                                    {
                                        strMetaDes = strProduct + " ราคาเริ่มต้นที่ " + strLowestRate + " บาท จอง" + strProduct + " รับประกันราคาถูกที่สุด";
                                    }
                                    // Normal case //

                                    SeoString.Append("<meta name=\"description\" content=\"" + strMetaDes + "\"/>\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"" + strMetaKey + "\"/>\r\n");
                                    break;

                                //"Spa";
                                case 40:
                                    // Default Case //
                                    strMetaDes = strProduct + " รับประกันราคาถูกที่สุด";
                                    strMetaKey = strProduct + "," + strDestination + " สปา,นวดแผนโบราณ,นวด,ทัวร์,แพ็คเกจ,วันหยุด,ท่องเที่ยว,โฮเทลทู,โฮเทลทูไทยแลนด์";
                                    // Default Case //

                                    // Normal case //
                                    if ((2 * intProductLen + 91) <= 150) // product + price
                                    {
                                        strMetaDes = strProduct + " ราคาเริ่มต้นที่ " + strLowestRate + " บาท จอง" + strProduct + " รับประกันราคาถูกที่สุด";
                                    }

                                    if ((intProductLen + 91) <= 150) // product + price
                                    {
                                        strMetaDes = strProduct + " ราคาเริ่มต้นที่ " + strLowestRate + " บาท จอง" + strProduct + " รับประกันราคาถูกที่สุด";
                                    }
                                    // Normal case //

                                    SeoString.Append("<meta name=\"description\" content=\"" + strMetaDes + "\"/>\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"" + strMetaKey + "\"/>\r\n");
                                    break;
                            }

                            break;

                        case SEO_PageType.Information:
                            break;
                        case SEO_PageType.Photo:
                            break;
                        case SEO_PageType.Review:
                            break;
                        case SEO_PageType.Why:
                            break;
                        case SEO_PageType.Contact:
                            break;
                    }
                    break;

                case 3:
                    break;
            }

            return SeoString.ToString();
        }


        private static string canonical_Gen(SEO_PageType seoPageType, byte seoProductCat, string strfileName)
        {
            string SeoString = string.Empty;
            switch (seoPageType)
            {
                case SEO_PageType.Home:
                    SeoString = "<link rel=\"canonical\" href=\"" + strfileName + "\"/>\r\n";
                    break;
                case SEO_PageType.Destination:
                    SeoString = "<link rel=\"canonical\" href=\"" + strfileName + "\"/>\r\n";
                    break;
                case SEO_PageType.Location:
                    SeoString = "<link rel=\"canonical\" href=\"" + strfileName + "\"/>\r\n";
                    break;
                case SEO_PageType.Detail:
                    SeoString = "<link rel=\"canonical\" href=\"" + strfileName + "\"/>\r\n";
                    break;
                case SEO_PageType.Information:
                    SeoString = "<link rel=\"canonical\" href=\"" + strfileName + "\"/>\r\n";
                    break;
                case SEO_PageType.Photo:
                    SeoString = "<link rel=\"canonical\" href=\"" + strfileName + "\"/>\r\n";
                    break;
                case SEO_PageType.Review:
                    SeoString = "<link rel=\"canonical\" href=\"" + strfileName + "\"/>\r\n";
                    break;
                case SEO_PageType.Why:
                    SeoString = "<link rel=\"canonical\" href=\"" + strfileName + "\"/>\r\n";
                    break;
                case SEO_PageType.Contact:
                    SeoString = "<link rel=\"canonical\" href=\"" + strfileName + "\"/>\r\n";
                    break;
            }

            return SeoString;
        }


        private static string GetProductCatString(byte seoProductCat)
        {
            string SeoString = string.Empty;
            switch (seoProductCat)
            {
                case 29:
                    SeoString = "Hotels";
                    break;
                case 31:
                    SeoString = "Airport Transfer";
                    break;
                case 32:
                    SeoString = "Golf Courses";
                    break;
                case 34:
                    SeoString = "Day Trips";
                    break;
                case 36:
                    SeoString = "Water Activity";
                    break;
                case 37:
                    SeoString = "Dinner Cruise";
                    break;
                case 38:
                    SeoString = "Shows";
                    break;
                case 39:
                    SeoString = "Health Check Up";
                    break;
                case 40:
                    SeoString = "Spa";
                    break;
            }

            return SeoString;
        }

    }


}