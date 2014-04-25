using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Hotels2thailand.Production;

/// <summary>
/// Summary description for Booking
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    
    public class SEO_Generate 
    {
        
        /// <summary>
        /// GenVerSion1 
        /// Gen all Whitout Cannonical
        /// </summary>
        /// <param name="SeoStringType"></param>
        /// <param name="seoPageType"></param>
        /// <param name="seoProductCat"></param>
        /// <param name="strDestination"></param>
        /// <param name="strLocation"></param>
        /// <param name="strProduct"></param>
        /// <param name="strKeyword"></param>
        /// <returns></returns>
        public static string Function_gen_optimize_string(SEO_StringType SeoStringType, SEO_PageType seoPageType, byte seoProductCat, string strDestination, string strLocation, string strProduct, string strKeyword = "")
        {
            string SeoString = string.Empty;
            switch (SeoStringType)
            {
                case SEO_StringType.Meta:
                    SeoString = Meta_Gen(seoPageType, seoProductCat, strDestination, strLocation, strProduct, strKeyword);
                    break;
                case SEO_StringType.Title:
                    SeoString = Title_Gen(seoPageType, seoProductCat, strDestination, strLocation, strProduct);
                    break;
                case SEO_StringType.H1:
                    SeoString = H1_Gen(seoPageType, seoProductCat, strDestination, strLocation, strProduct);
                    break;
                case SEO_StringType.Link_Back_To_Home:
                    SeoString = BackHome_Gen(seoPageType, seoProductCat, strDestination, strLocation, strProduct);
                    break;
                case SEO_StringType.canonical:
                    
                    break;
            }

            return SeoString.ToString();
        }

        
        //GenVerSion2
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
        //public enum byte : int
        //{
        //    Hotel = 29,
        //    Airport_Transfer = 31,
        //    Golf_Courses = 32,
        //    Day_Trips = 34,
        //    Water_Activity = 36,
        //    Dinner_Cruise = 37,
        //    Show = 38,
        //    Health_Check_Up = 39,
        //    Spa = 40

        //}

        private static string GetProductCatString(byte seoProductCat)
        {
            string SeoString = string.Empty;
            switch(seoProductCat)
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
        private static string Meta_Gen(SEO_PageType seoPageType, byte seoProductCat, string strDestination, string strLocation, string strProduct, string strKeyWord)
        {
            StringBuilder SeoString = new StringBuilder();
            switch (seoPageType)
            {
                case SEO_PageType.Destination:
                    switch (seoProductCat)
                    {
                        //"Hotels";
                        case 29:
                            SeoString.Append("<meta name=\"description\" content=\"" + strDestination + " Hotels - Reservation hotels in " + strDestination + " with lower price.\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strDestination + " hotels, hotels in " + strDestination + ", hotels in " + strDestination + " Thailand\">\r\n");
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
                                    SeoString.Append("<meta name=\"description\" content=\"Bangkok Tours, Day Trips, Vacation Package, NightLife, Floating Market, Grand Palace, Wat Arun, Wat Pho, Sightseeing and All Activities and Attractions in Bangkok\">\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"bangkok tours, bangkok day trips,bangkok vacation, bangkok night life, grand palace, floating market, weekend market, wat arun, wat pho, bangkok sight seeing, bangkok attraction\">\r\n");
                                    break;
                                case "phuket":
                                    SeoString.Append("<meta name=\"description\" content=\"Phuket Tours, Day Trips,Diving, Vacation, Trip, NightLife, Spa, Masaage, Speed Boat, Canoe, Kayak and All Activities and Attractions in Phuket\">\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"phukt tours, phuket day trips, phuket vacation, phukettrip, phuket nightlife, phuket spa, phuket massage, phuket speed boat, phuket conoe, phuket kayak, phuket water activities\">\r\n");
                                    break;
                                default:
                                    SeoString.Append("<meta name=\"description\" content=\"" + strDestination + " daytrips, package tours, sightseeing " + strDestination + " and all activities in " + strDestination + " with lower price.\">\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"" + strDestination + " sightseeing, package tours in " + strDestination + ", trip in " + strDestination + " Thailand\">\r\n");
                                    break;
                            }
                            
                            break;
                        //"Water Activity";
                        case 36:
                            switch (strDestination.Trim().ToLower())
                            {
                                case "phuket":
                                    SeoString.Append("<meta name=\"description\" content=\"Phuket Water Activities : Diving Canoe Kayak Boat Trips Snorkeling Fishing Rafting with Online Booking\">\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"phukt tours, phuket, diving, canoe, kayak, boat trips, snorkeling, fishing, rafting\">\r\n");
                                    break;
                                default:
                                    SeoString.Append("<meta name=\"description\" content=\"" + strDestination + " Diving Canoe Kayak Boat Trips Snorkeling Fishing Rafting with Online Booking\">\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"" + strDestination + " tours, " + strDestination + ", diving, canoe, kayak, boat trips, snorkeling, fishing, rafting\">\r\n");
                                    break;
                            }
                            
                            break;
                        //"Shows";
                        case 38:
                            switch (strDestination.Trim().ToLower())
                            {
                                case "bangkok":
                                    SeoString.Append("<meta name=\"description\" content=\"Bangkok Shows & Events : Siam Niramit, Joe Louis theater, Thai Boxing, Booking All Shows & Events in Bangkok\">\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"bangkok shows, bangkok events, siam niramit, joe louis theater, thai boxing\">\r\n");
                                    break;
                                case "phuket":
                                    SeoString.Append("<meta name=\"description\" content=\"Phuket Shows & Events : Phuket Fantasea, Thai Boxing, Booking All Shows & Events in Phuket\">\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"phuket shows, phuket events, phuket fantasea, thai boxing\">\r\n");
                                    break;
                                case "pattaya":
                                    SeoString.Append("<meta name=\"description\" content=\"Pattaya Shows & Events : Tiffany Show, Pattaya Alcazar Show, Alangkarn Pattaya, Booking All Shows & Events in Pattaya\">\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"pattaya shows, pattaya events, tiffany show, pattaya alcazar show, alangkarn pattaya\">\r\n");
                                    break;
                                default:
                                    SeoString.Append("<meta name=\"description\" content=\"" + strDestination + " Shows & Events Booking All Shows & Events in Phuket " + strDestination + " with lower price.\">\r\n");
                                    SeoString.Append("<meta name=\"keywords\" content=\"" + strDestination + " shows,events " + strDestination + ", trip in " + strDestination + " Thailand\">\r\n");
                                    break;
                            }
                            
                            break;
                        //"Health Check Up";
                        case 39:
                            SeoString.Append("<meta name=\"description\" content=\"" + strDestination + " Health Check Up & Medical Tour " + strDestination + " Hospitals\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strDestination + " health check up,hospita " + strDestination + ", medical tour in " + strDestination + " Thailand\">\r\n");
                            break;
                        //"Spa";
                        case 40:
                            SeoString.Append("<meta name=\"description\" content=\"" + strDestination + " Spa" + strDestination + " Spa\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strDestination + " spa" + strDestination + ", spa in " + strDestination + " Thailand\">\r\n");
                            break;
                    }
                   
                    break;
                case SEO_PageType.Location:
                    switch (seoProductCat)
                    {
                        //"Hotels";
                        case 29:
                            SeoString.Append("<meta name=\"description\" content=\"" + strLocation + " Hotels - Reservation hotels in " + strLocation + " in " + strLocation + " with lower price.\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strLocation + " hotels, hotels in " + strLocation + ", hotels in " + strLocation + " Thailand\">\r\n");
                            break;
                        //"Golf Courses";
                        case 32:
                            SeoString.Append("<meta name=\"description\" content=\"" + strLocation + " Golf Courses All Golf Course in " + strLocation + "\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strLocation + " golf courses, golf courses in " + strLocation + ", golf course in " + strLocation + " Thailand\">\r\n");
                            break;
                        //"Day Trips"; no content";
                        case 34:
                           
                            break;
                        //"Water Activity no content";
                        case 36:
                            
                            break;
                        //"Shows" no content;
                        case 38:
                            
                            break;
                        //"Health Check Up";
                        case 39:
                           
                            break;
                        //"Spa";
                        case 40:
                           
                            break;
                    }
                    
                    break;
                case SEO_PageType.Detail:
                    switch (seoProductCat)
                    {
                        case 29:
                            SeoString.Append("<meta name=\"description\" content=\""+strProduct+" : "+strProduct+" Reservation\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\""+strProduct+" reservations, "+strProduct+" booking, "+strProduct+" "+ strDestination+"\">\r\n");
                            break;
                        //"Golf Courses";
                        case 32:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " : " + strProduct + " Golf Course Online Booking\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\""+strProduct+" reservations, "+strProduct+" booking, "+strProduct+" "+ strDestination+"\">\r\n");
                            break;
                        //"Day Trips";
                        case 34:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " : " + strDestination + " Tours, Day Trips, Vacation Package, Online Booking With Special Low Price\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strProduct + ", " + strDestination + " sightseeing, " + strKeyWord + "\">\r\n");
                            break;
                        //"Water Activity";
                        case 36:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " : " + strDestination + " Tours, Water Activities, Vacation Package, Online Booking With Special Low Price\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strProduct + ", " + strDestination + " Water Activities, " + strKeyWord + "\">\r\n");
                            break;
                        //"Shows";
                        case 38:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " : " + strDestination + " Shows & Events, Online Booking With Special Low Price\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strProduct + ", " + strDestination + " Shows & Events, " + strKeyWord + "\">\r\n");
                            break;
                        //"Health Check Up";
                        case 39:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " : " + strDestination + " Health Check Up\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strProduct + ", " + strDestination + " health chck up, " + strKeyWord + "\">\r\n");
                            break;
                        //"Spa";
                        case 40:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " : " + strDestination + " Spa\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strProduct + ", " + strDestination + " spa, " + strKeyWord + "\">\r\n");
                            break;
                    }

                    break;
                case SEO_PageType.Information:
                    switch (seoProductCat)
                    {
                        case 29:
                            SeoString.Append("<meta name=\"description\" content=\""+strProduct+" : "+strProduct+" Information\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\""+strProduct+" Information, "+strProduct+" booking, "+strProduct+" "+ strDestination+"\">\r\n");
                            break;
                        //"Golf Courses";
                        case 32:
                            SeoString.Append("<meta name=\"description\" content=\""+strProduct+" : "+strProduct+" Information\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\""+strProduct+" Information, "+strProduct+" booking, "+strProduct+" "+ strDestination+"\">\r\n");
                            break;
                        //"Day Trips";
                        case 34:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " Information : " + strDestination + " Day Trips " + strDestination + " Attraction\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strProduct + " Information, " + strKeyWord + "\">\r\n");
                            break;
                        //"Water Activity";
                        case 36:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " Information : " + strDestination + " Water Activities " + strDestination + " Attraction\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strProduct + " Information, " + strKeyWord + "\">\r\n");
                            break;
                        //"Shows";
                        case 38:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " Information : " + strDestination + " Shows & Events \">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strProduct + " Information, " + strKeyWord + "\">\r\n");
                            break;
                        //"Health Check Up";
                        case 39:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " Information : " + strDestination + " health \">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strProduct + " health, " + strKeyWord + "\">\r\n");
                            break;
                        //"Spa";
                        case 40:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " Information : " + strDestination + " spa \">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strProduct + " spa, " + strKeyWord + "\">\r\n");
                            break;
                    }
                    
                    break;
                case SEO_PageType.Photo:
                    switch (seoProductCat)
                    {
                        case 29:
                            SeoString.Append("<meta name=\"description\" content=\""+strProduct+" : "+strProduct+" Photo\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\""+strProduct+" Photo, "+strProduct+" pictures, "+strProduct+" "+ strDestination+"\">\r\n");
                            break;
                        //"Golf Courses";
                        case 32:
                            SeoString.Append("<meta name=\"description\" content=\""+strProduct+" : "+strProduct+" Photo\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\""+strProduct+" Photo, "+strProduct+" pictures, "+strProduct+" "+ strDestination+"\">\r\n");
                            break;
                        //"Day Trips";
                        case 34:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " Photo : " + strDestination + " Day Trips " + strDestination + " Attraction\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strProduct + " pictures, " + strKeyWord + "\">\r\n");
                            break;
                        //"Water Activity";
                        case 36:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " Photo : " + strDestination + " Water Activities " + strDestination + " Attraction\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strProduct + " pictures, " + strKeyWord + "\">\r\n");
                            break;
                        //"Shows";
                        case 38:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " Photo : " + strDestination + " Shows & Events \">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strProduct + " pictures, " + strKeyWord + "\">\r\n");
                            break;
                        //"Health Check Up";
                        case 39:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " Photo : " + strDestination + " medical tour \">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strProduct + " pictures, " + strKeyWord + "\">\r\n");
                            break;
                        //"Spa";
                        case 40:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " Photo : " + strDestination + " spa \">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strProduct + " pictures, " + strKeyWord + "\">\r\n");
                            break;
                    }

                    break;
                case SEO_PageType.Review:
                    switch (seoProductCat)
                    {
                        case 29:
                            SeoString.Append("<meta name=\"description\" content=\""+ strProduct +" : " + strProduct + " Reviews\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\""+ strProduct +" reviews, "+ strProduct +" booking, "+ strProduct +" "+ strDestination+"\">\r\n");
                            break;
                        //"Golf Courses";
                        case 32:
                            SeoString.Append("<meta name=\"description\" content=\""+ strProduct +" : " + strProduct + " Reviews\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\""+ strProduct +" reviews, "+ strProduct +" booking, "+ strProduct +" "+ strDestination+"\">\r\n");
                            break;
                        //"Day Trips";
                        case 34:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " Reviews : " + strDestination + " Day Trips " + strDestination + " Attraction\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strProduct + " reviews, " + strKeyWord + "\">\r\n");
                            break;
                        //"Water Activity";
                        case 36:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " Reviews : " + strDestination + " Water Activities " + strDestination + " Attraction\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strProduct + " reviews, " + strKeyWord + "\">\r\n");
                            break;
                        //"Shows";
                        case 38:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " Reviews : " + strDestination + " Shows & Events \">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strProduct + " reviews, " + strKeyWord + "\">\r\n");
                            break;
                        //"Health Check Up";
                        case 39:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " Reviews : " + strDestination + " hospitals \">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strProduct + " reviews, " + strKeyWord + "\">\r\n");
                            break;
                        //"Spa";
                        case 40:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " Reviews : " + strDestination + " spa \">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strProduct + " reviews, " + strKeyWord + "\">\r\n");
                            break;
                    }
                    
                    break;
                case SEO_PageType.Why:
                    switch (seoProductCat)
                    {
                        case 29:
                            SeoString.Append("<meta name=\"description\" content=\""+strProduct+" : "+strProduct+" Reservation\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\""+strProduct+" reservations, "+strProduct+" booking, "+strProduct+" "+ strDestination+"\">\r\n");
                            break;
                        //"Golf Courses";
                        case 32:
                            SeoString.Append("<meta name=\"description\" content=\""+strProduct+" : "+strProduct+" Reservation\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\""+strProduct+" reservations, "+strProduct+" booking, "+strProduct+" "+ strDestination+"\">\r\n");
                            break;
                        //"Day Trips";
                        case 34:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " Reservation : " + strDestination + " Day Trips " + strDestination + " Attraction\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strProduct + " sightseeing, " + strKeyWord + "\">\r\n");
                            break;
                        //"Water Activity";
                        case 36:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " Reservation : " + strDestination + " Water Activities " + strDestination + " Attraction\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strProduct + " water activities, " + strKeyWord + "\">\r\n");
                            break;
                        //"Shows";
                        case 38:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " Reservation : " + strDestination + " Shows & Events \">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strProduct + " sightseeing, " + strKeyWord + "\">\r\n");
                            break;
                        //"Health Check Up";
                        case 39:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " Reservation : " + strDestination + " medical tour \">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strProduct + " medical tour, " + strKeyWord + "\">\r\n");
                            break;
                        //"Spa";
                        case 40:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " Reservation : " + strDestination + " spa \">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strProduct + " spa, " + strKeyWord + "\">\r\n");
                            break;
                    }
                    
                    break;
                case SEO_PageType.Contact:
                    switch (seoProductCat)
                    {
                        case 29:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " : " + strProduct + " Contact\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\""+strProduct+" contact, "+strProduct+" booking, "+strProduct+" "+ strDestination+"\">\r\n");
                            break;
                        //"Golf Courses";
                        case 32:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " : " + strProduct + " Contact\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strProduct + " contact, " + strProduct + " booking, " + strProduct + " " + strDestination + "\">\r\n");
                            break;
                        //"Day Trips";
                        case 34:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " Contact : " + strDestination + " Day Trips " + strDestination + " Attraction\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strProduct + " contact, " + strKeyWord + "\">\r\n");
                            break;
                        //"Water Activity";
                        case 36:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " Contact : " + strDestination + " Water Activities " + strDestination + " Attraction\">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strProduct + " contact, " + strKeyWord + "\">\r\n");
                            break;
                        //"Shows";
                        case 38:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " Contact : " + strDestination + " Shows & Events \">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strProduct + " contact, " + strKeyWord + "\">\r\n");
                            break;
                        //"Health Check Up";
                        case 39:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " Contact : " + strDestination + " health check up \">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strProduct + " contact, " + strKeyWord + "\">\r\n");
                            break;
                        //"Spa";
                        case 40:
                            SeoString.Append("<meta name=\"description\" content=\"" + strProduct + " Contact : " + strDestination + " spa \">\r\n");
                            SeoString.Append("<meta name=\"keywords\" content=\"" + strProduct + " contact, " + strKeyWord + "\">\r\n");
                            break;
                    }
                    
                    break;
            }

            return SeoString.ToString();
        }

        private static string Title_Gen(SEO_PageType seoPageType, byte seoProductCat, string strDestination, string strLocation, string strProduct)
        {
            StringBuilder SeoString = new StringBuilder();
            switch (seoPageType)
            {
                case SEO_PageType.Destination:
                    switch (seoProductCat)
                    {
                        case 29:
                            SeoString.Append(strDestination + " Hotels and All Hotels in " + strDestination + "");
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
                                    SeoString.Append("Bangkok Tours, Day Trips, Attractions, Nightlife, Shopping, Dining, Gran Palace, Wat Pho, Wat Arun and All Activities in Bangkok");
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
                                case "bangkok":
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
                                    SeoString.Append("Bangkok Shows & Events : Siam Niramit, Joe Louis Theater, Thai Boxing All Shows & Events in Bangkok");
                                    break;
                                case "phuket":
                                    SeoString.Append("Phuket Shows & Events : Phuket Fantasea, Thai Boxing, Booking and All Shows & Events in Phuket");
                                    break;
                                case "pattaya":
                                    SeoString.Append("Pattaya Shows & Events : Tiffany Show, Pattaya Alcazar Show, Alangkarn Pattaya and All Shows & Events in Pattaya");
                                    break;
                                default:
                                    SeoString.Append(strDestination + " Shows & Events Booking All Shows & Events in " + strDestination);
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
                case SEO_PageType.Location:
                    switch (seoProductCat)
                    {
                        case 29:
                            SeoString.Append(strLocation + " Hotels , " + strDestination + " Hotels");
                            break;
                        //"Golf Courses";
                        case 32:
                            SeoString.Append(strLocation + " Golf Courses , " + strDestination + " Golf Course and All Golf Course in Thailand");
                            break;
                        //"Day Trips";
                        case 34:
                            break;
                        //"Water Activity";
                        case 36:
                           break;
                        //"Shows";
                        case 38:
                            break;
                        //"Health Check Up";
                        case 39:
                            break;
                        //"Spa";
                        case 40:
                           break;
                    }
                    break;
                case SEO_PageType.Detail:
                    switch (seoProductCat)
                    {
                        case 29:

                            if (strProduct.IndexOf(strDestination) >= 0)
                                SeoString.Append(strProduct);
                            else
                                SeoString.Append(strProduct + " " + strDestination);

                            break;
                        //"Golf Courses";
                        case 32:
                            SeoString.Append(strProduct + " " + strDestination + " : Thailand Golf Course");
                            break;
                        //"Day Trips";
                        case 34:
                            SeoString.Append(strProduct + " " + strDestination + " Day Trips, Attraction, Package Tours");
                            break;
                        //"Water Activity";
                        case 36:
                            SeoString.Append(strProduct + " " + strDestination + " Water Activities, Package Tours");
                            break;
                        //"Shows";
                        case 38:
                            SeoString.Append(strProduct + " " + strDestination );
                            break;
                        //"Health Check Up";
                        case 39:
                            SeoString.Append(strProduct + " " + strDestination);
                            break;
                        //"Spa";
                        case 40:
                            SeoString.Append(strProduct + " " + strDestination);
                            break;
                    }
                    break;
                case SEO_PageType.Information:
                    switch (seoProductCat)
                    {
                        case 29:

                            if (strProduct.IndexOf(strDestination) >= 0)
                                SeoString.Append(strProduct + " Information");
                            else
                                SeoString.Append(strProduct + " " + strDestination + " Information");

                            break;
                        //"Golf Courses";
                        case 32:
                            SeoString.Append(strProduct + " " + strDestination + " Information : Thailand Golf Courses");
                            break;
                        //"Day Trips";
                        case 34:
                            SeoString.Append(strProduct + " " + strDestination + " Day Trips, Attraction, Package Tours");
                            break;
                        //"Water Activity";
                        case 36:
                            SeoString.Append(strProduct + " " + strDestination + " Water Activities, Attraction, Package Tours");
                            break;
                        //"Shows";
                        case 38:
                            SeoString.Append(strProduct + " Information : " + strDestination);
                            break;
                        //"Health Check Up";
                        case 39:
                            SeoString.Append(strProduct + " Information : " + strDestination);
                            break;
                        //"Spa";
                        case 40:
                            SeoString.Append(strProduct + " Information : " + strDestination);
                            break;
                    }
                    
                    break;
                case SEO_PageType.Photo:
                    switch (seoProductCat)
                    {
                        case 29:
                            if (strProduct.IndexOf(strDestination) >= 0)
                                SeoString.Append(strProduct + " Pictures");
                            else
                                SeoString.Append(strProduct + " " + strDestination + " Pictures");

                            break;
                        //"Golf Courses";
                        case 32:
                            SeoString.Append(strProduct + " " + strDestination + " Pictures : Thailand Golf Course");
                            break;
                        //"Day Trips";
                        case 34:
                            SeoString.Append(strProduct + " " + strDestination + " Day Trips, Attraction, Package Tours");
                            break;
                        //"Water Activity";
                        case 36:
                            SeoString.Append(strProduct + " " + strDestination + " Water Activities, Attraction, Package Tours");
                            break;
                        //"Shows";
                        case 38:
                            SeoString.Append(strProduct + " Pictures : " + strDestination);
                            break;
                        //"Health Check Up";
                        case 39:
                            SeoString.Append(strProduct + " Pictures : " + strDestination);
                            break;
                        //"Spa";
                        case 40:
                            SeoString.Append(strProduct + " Pictures : " + strDestination);
                            break;
                    }
                    
                    break;
                case SEO_PageType.Review:
                    switch (seoProductCat)
                    {
                        case 29:
                            if (strProduct.IndexOf(strDestination) >= 0)
                                SeoString.Append(strProduct + " Reviews");
                            else
                                SeoString.Append(strProduct + " " + strDestination + " Reviews");

                            break;
                        //"Golf Courses";
                        case 32:
                            SeoString.Append(strProduct + " " + strDestination + "  Reviews : Thailand Golf Courses");
                            break;
                        //"Day Trips";
                        case 34:
                            SeoString.Append(strProduct + " " + strDestination + " Day Trips, Attraction, Package Tours");
                            break;
                        //"Water Activity";
                        case 36:
                            SeoString.Append(strProduct + " " + strDestination + " Water Activities, Attraction, Package Tours");
                            break;
                        //"Shows";
                        case 38:
                            SeoString.Append(strProduct + " Reviews : " + strDestination);
                            break;
                        //"Health Check Up";
                        case 39:
                            SeoString.Append(strProduct + " Reviews : " + strDestination);
                            break;
                        //"Spa";
                        case 40:
                            SeoString.Append(strProduct + " Reviews : " + strDestination);
                            break;
                    }
                    
                    break;
                case SEO_PageType.Why:
                    switch (seoProductCat)
                    {
                        case 29:
                            if (strProduct.IndexOf(strDestination) >= 0)
                                SeoString.Append(strProduct + " Why Choose Hotels2Thailand.com?");
                            else
                                SeoString.Append(strProduct + " " + strDestination + " Why Choose Hotels2Thailand.com?");

                            break;
                        //"Golf Courses";
                        case 32:
                            SeoString.Append(strProduct + " " + strDestination + "  Why Choose Hotels2Thailand.com? : Thailand Golf Courses");
                            break;
                        //"Day Trips";
                        case 34:
                            SeoString.Append(strProduct + " Why Choose Hotels2Thailand.com? : " + strDestination + " Day Trips, Attraction, Package Tours");
                            break;
                        //"Water Activity";
                        case 36:
                            SeoString.Append(strProduct + " " + strDestination + " Water Activities, Attraction, Package Tours");
                            break;
                        //"Shows";
                        case 38:
                            SeoString.Append(strProduct + " Why Choose Hotels2Thailand.com? : " + strDestination);
                            break;
                        //"Health Check Up";
                        case 39:
                            SeoString.Append(strProduct + " Why Choose Hotels2Thailand.com? : " + strDestination);
                            break;
                        //"Spa";
                        case 40:
                            SeoString.Append(strProduct + " Why Choose Hotels2Thailand.com? : " + strDestination);
                            break;
                    }
                    
                    break;
                    
                case SEO_PageType.Contact:
                    switch (seoProductCat)
                    {
                        case 29:

                            if (strProduct.IndexOf(strDestination) >= 0)
                                SeoString.Append(strProduct + " Contact Us");
                            else
                                SeoString.Append(strProduct + " " + strDestination + " Contact Us");

                            break;
                        //"Golf Courses";
                        case 32:
                            SeoString.Append(strProduct + " " + strDestination + " : Contact Us : Thailand Golf Courses");
                            break;
                        //"Day Trips";
                        case 34:
                            SeoString.Append(strProduct + " Contact Us : " + strDestination + " Day Trips, Attraction, Package Tours");
                            break;
                        //"Water Activity";
                        case 36:
                            SeoString.Append(strProduct + " Contact Us : " + strDestination + " Water Activities, Attraction, Package Tours");
                            break;
                        //"Shows";
                        case 38:
                            SeoString.Append(strProduct + " Contact Us : " + strDestination);
                            break;
                        //"Health Check Up";
                        case 39:
                            SeoString.Append(strProduct + " Contact Us : " + strDestination);
                            break;
                        //"Spa";
                        case 40:
                            SeoString.Append(strProduct + " Contact Us : " + strDestination);
                            break;
                    }
                    
                    break;
            }

            return SeoString.ToString();
        }

        private static string H1_Gen(SEO_PageType seoPageType, byte seoProductCat, string strDestination, string strLocation, string strProduct)
        {
            string SeoString = string.Empty;
            switch (seoPageType)
            {
                case SEO_PageType.Destination:
                    switch (seoProductCat)
                    {
                        case 29:
                            SeoString = strDestination + " Hotels Thailand Thailand\r\n"; 
                            break;
                        //"Golf Courses";
                        case 32:
                            SeoString = strDestination + " Golf Courses\r\n"; 
                            break;
                        //"Day Trips";
                        case 34:
                            switch (strDestination.Trim().ToLower())
                            {
                                case "bangkok":
                                    SeoString = strDestination + " Tours, Day Trips\r\n"; 
                                    break;
                                case "phuket":
                                    SeoString = strDestination + " Tours, Day Trips\r\n"; 
                                    break;
                                default:
                                    SeoString = strDestination + " Tours, Day Trips\r\n"; 
                                    break;
                            }
                            
                            break;
                        //"Water Activity";
                        case 36:
                            switch (strDestination.Trim().ToLower())
                            {
                                case "phuket":
                                    SeoString = strDestination + " Tours, Water Activities\r\n";
                                    break;
                                default:
                                    SeoString = strDestination + " Tours, Water Activities\r\n";
                                    break;
                            }
                            break;
                        //"Shows";
                        case 38:
                            switch (strDestination.Trim().ToLower())
                            {
                                case "bangkok":
                                    SeoString = strDestination + " Shows & Events\r\n";
                                    break;
                                case "phuket":
                                    SeoString = strDestination + " Shows & Events\r\n";
                                    break;
                                default:
                                    SeoString = strDestination + " Shows & Events\r\n";
                                    break;
                            }
                            break;
                        //"Health Check Up";
                        case 39:
                            SeoString = strDestination + " Health Check Up & Medical Tour & " + strDestination + " Hospitals\r\n";
                            break;
                        //"Spa";
                        case 40:
                            SeoString = strDestination + " Spa & " + strDestination + " Spa\r\n";
                            break;
                    }
                    
                    break;
                case SEO_PageType.Location:
                    switch (seoProductCat)
                    {
                        case 29:
                            SeoString = strLocation + " " + strDestination + " Hotels\r\n"; 
                            break;
                        //"Golf Courses";
                        case 32:
                            SeoString = strLocation + " " + strDestination + " Golf Course\r\n"; 
                            break;
                        //"Day Trips";
                        case 34:
                            
                            break;
                        //"Water Activity";
                        case 36:
                            
                            break;
                        //"Shows";
                        case 38:
                            
                            break;
                        //"Health Check Up";
                        case 39:
                           
                            break;
                        //"Spa";
                        case 40:
                           
                            break;
                    }
                    
                    break;
                case SEO_PageType.Detail:
                    switch (seoProductCat)
                    {
                        case 29:
                            SeoString = strProduct;
                            break;
                        //"Golf Courses";
                        case 32:
                            SeoString = strProduct;
                            break;
                        //"Day Trips";
                        case 34:
                            SeoString = strProduct;
                            break;
                        //"Water Activity";
                        case 36:
                            SeoString = strProduct;
                            break;
                        //"Shows";
                        case 38:
                            SeoString = strProduct;
                            break;
                        //"Health Check Up";
                        case 39:
                            SeoString = strProduct;
                            break;
                        //"Spa";
                        case 40:
                            SeoString = strProduct;
                            break;
                    }
                    
                    break;
                case SEO_PageType.Information:
                    switch (seoProductCat)
                    {
                        case 29:
                            SeoString = strProduct + " Information";
                            break;
                        //"Golf Courses";
                        case 32:
                            SeoString = strProduct + " Information";
                            break;
                        //"Day Trips";
                        case 34:
                            SeoString = strProduct + " Information";
                            break;
                        //"Water Activity";
                        case 36:
                            SeoString = strProduct + " Information";
                            break;
                        //"Shows";
                        case 38:
                            SeoString = strProduct + " Information";
                            break;
                        //"Health Check Up";
                        case 39:
                            SeoString = strProduct + " Information";
                            break;
                        //"Spa";
                        case 40:
                            SeoString = strProduct + " Information";
                            break;
                    }
                     
                    break;
                case SEO_PageType.Photo:
                    switch (seoProductCat)
                    {
                        case 29:
                            SeoString = strProduct + " Pictures";
                            break;
                        //"Golf Courses";
                        case 32:
                            SeoString = strProduct + " Pictures";
                            break;
                        //"Day Trips";
                        case 34:
                            SeoString = strProduct + " Pictures";
                            break;
                        //"Water Activity";
                        case 36:
                            SeoString = strProduct + " Pictures";
                            break;
                        //"Shows";
                        case 38:
                            SeoString = strProduct + " Pictures";
                            break;
                        //"Health Check Up";
                        case 39:
                            SeoString = strProduct + " Pictures";
                            break;
                        //"Spa";
                        case 40:
                            SeoString = strProduct + " Pictures";
                            break;
                    }
                    
                    break;
                case SEO_PageType.Review:
                    switch (seoProductCat)
                    {
                        case 29:
                            SeoString = strProduct + " Reviews";
                            break;
                        //"Golf Courses";
                        case 32:
                            SeoString = strProduct + " Reviews";
                            break;
                        //"Day Trips";
                        case 34:
                            SeoString = strProduct + " Reviews";
                            break;
                        //"Water Activity";
                        case 36:
                            SeoString = strProduct + " Reviews";
                            break;
                        //"Shows";
                        case 38:
                            SeoString = strProduct + " Reviews";
                            break;
                        //"Health Check Up";
                        case 39:
                            SeoString = strProduct + " Reviews";
                            break;
                        //"Spa";
                        case 40:
                            SeoString = strProduct + " Reviews";
                            break;
                    }
                    
                    break;
                case SEO_PageType.Why:
                    switch (seoProductCat)
                    {
                        case 29:
                            SeoString = strProduct + " : Why Choose Hotels2Thailand.com ?";
                            break;
                        //"Golf Courses";
                        case 32:
                            SeoString = strProduct + " : Why Choose Hotels2Thailand.com ?";
                            break;
                        //"Day Trips";
                        case 34:
                            SeoString = strProduct + " : Why Choose Hotels2Thailand.com ?";
                            break;
                        //"Water Activity";
                        case 36:
                            SeoString = strProduct + " : Why Choose Hotels2Thailand.com ?";
                            break;
                        //"Shows";
                        case 38:
                            SeoString = strProduct + " : Why Choose Hotels2Thailand.com ?";
                            break;
                        //"Health Check Up";
                        case 39:
                            SeoString = strProduct + " : Why Choose Hotels2Thailand.com ?";
                            break;
                        //"Spa";
                        case 40:
                            SeoString = strProduct + " : Why Choose Hotels2Thailand.com ?";
                            break;
                    }
                    
                    break;
                case SEO_PageType.Contact:
                    switch (seoProductCat)
                    {
                        case 29:
                            SeoString = strProduct + " Contact Us";
                            break;
                        //"Golf Courses";
                        case 32:
                            SeoString = strProduct + " Contact";
                            break;
                        //"Day Trips";
                        case 34:
                            SeoString = strProduct + " Contact";
                            break;
                        //"Water Activity";
                        case 36:
                            SeoString = strProduct + " Contact";
                            break;
                        //"Shows";
                        case 38:
                            SeoString = strProduct + " Contact";
                            break;
                        //"Health Check Up";
                        case 39:
                            SeoString = strProduct + " Contact";
                            break;
                        //"Spa";
                        case 40:
                            SeoString = strProduct + " Contact";
                            break;
                    }
                    
                    break;
                
            }

            return SeoString;
        }

        private static string BackHome_Gen(SEO_PageType seoPageType, byte seoProductCat, string strDestination, string strLocation, string strProduct)
        {
            string SeoString = string.Empty;
            switch (seoPageType)
            {
                case SEO_PageType.Destination:
                    switch (seoProductCat)
                    {
                        case 29:
                            SeoString = strDestination + " Hotels Thailand";
                            break;
                        //"Golf Courses";
                        case 32:
                            SeoString = strDestination + " Golf Courses Thailand Golf Course";
                            break;
                        //"Day Trips";
                        case 34:
                            switch (strDestination.Trim().ToLower())
                            {
                                case "bangkok":
                                    SeoString = strDestination + " Tours, Activities, Day Trips, Attractions, Sightseeing";
                                    break;
                                case "phuket":
                                    SeoString = strDestination + " Tours, Activities, Day Trips, Attractions, Sightseeing";
                                    break;
                                default:
                                    SeoString = strDestination + " Tours, Activities, Day Trips, Attractions, Sightseeing";
                                    break;
                            }
                            
                            break;
                        //"Water Activity";
                        case 36:
                            switch (strDestination.Trim().ToLower())
                            {
                                case "phuket":
                                    SeoString = strDestination + " Tours, Water Activities";
                                    break;
                                default:
                                    SeoString = strDestination + " Tours, Water Activities";
                                    break;
                            }
                            break;
                        //"Shows";
                        case 38:
                            switch (strDestination.Trim().ToLower())
                            {
                                case "bangkok":
                                    SeoString = strDestination + " Shows & Events";
                                    break;
                                case "phuket":
                                    SeoString = strDestination + " Shows & Events";
                                    break;
                                case "pattaya":
                                    SeoString = strDestination + " Shows & Events";
                                    break;
                                default:
                                    SeoString = strDestination + " Shows & Events";
                                    break;
                            }
                           
                            break;
                        //"Health Check Up";
                        case 39:
                            SeoString = strDestination + " Health Check Up & Medical Tour";
                            break;
                        //"Spa";
                        case 40:
                            SeoString = strDestination + " Spa";
                            break;
                    }
                    
                    break;
                case SEO_PageType.Location:
                    switch (seoProductCat)
                    {
                        case 29:
                            SeoString = strLocation + " " + strDestination + " Hotels Thailand";
                            break;
                        //"Golf Courses";
                        case 32:
                            SeoString = strLocation + " " + strDestination + " Golf Course";
                            break;
                        //"Day Trips";
                        case 34:
                            break;
                        //"Water Activity";
                        case 36:
                           break;
                        //"Shows";
                        case 38:
                            break;
                        //"Health Check Up";
                        case 39:
                            break;
                        //"Spa";
                        case 40:
                            break;
                    }
                   
                    break;
                case SEO_PageType.Detail:
                    switch (seoProductCat)
                    {
                        case 29:
                            SeoString = strProduct + " ," + strDestination + " Thailand";
                            break;
                        //"Golf Courses";
                        case 32:
                            SeoString = strLocation + " ," + strDestination + " Golf Course";
                            break;
                        //"Day Trips";
                        case 34:
                            SeoString = strLocation + " :" + strDestination + " Sightseeing Attraction Package Tours";
                            break;
                        //"Water Activity";
                        case 36:
                            SeoString = strLocation + " : " + strDestination + " Water Activities Attraction Package Tours";
                            break;
                        //"Shows";
                        case 38:
                            SeoString = strLocation + " : " + strDestination + " Shows & Events";
                            break;
                        //"Health Check Up";
                        case 39:
                            SeoString = strLocation + " : " + strDestination + " Health Check Up & Medical Tour & Hospitals";
                            break;
                        //"Spa";
                        case 40:
                            SeoString = strLocation + " : " + strDestination + " Spa";
                            break;
                    }
                    
                    break;
                case SEO_PageType.Information:
                    switch (seoProductCat)
                    {
                        case 29:
                           SeoString = strProduct + " , " + strDestination + " Hotels Thailand Hotels";
                            break;
                        //"Golf Courses";
                        case 32:
                            SeoString = strProduct + " , " + strDestination + " Golf Course";
                            break;
                        //"Day Trips";
                        case 34:
                            SeoString = strProduct + "  Information :" + strDestination + " Sightseeing, Attraction, Package Tours";
                            break;
                        //"Water Activity";
                        case 36:
                            SeoString = strProduct + "  Information :" + strDestination + " Water Activities, Attraction, Package Tours";
                            break;
                        //"Shows";
                        case 38:
                            SeoString = strProduct + "  Information :" + strDestination + " Shows & Events";
                            break;
                        //"Health Check Up";
                        case 39:
                            SeoString = strProduct + "  Information :" + strDestination + " Health Check Up & Medical Tour & Hospitals";
                            break;
                        //"Spa";
                        case 40:
                            SeoString = strProduct + "  Information :" + strDestination + " Spa";
                            break;
                    }
                    
                    break;
                case SEO_PageType.Photo:
                    switch (seoProductCat)
                    {
                        case 29:
                            SeoString = strProduct + " , " + strDestination + " Hotels Thailand Hotels";
                            break;
                        //"Golf Courses";
                        case 32:
                            SeoString = strProduct + " , " + strDestination + " Golf Course";
                            break;
                        //"Day Trips";
                        case 34:
                            SeoString = strProduct + "  Pictures :" + strDestination + " Sightseeing, Attraction, Package Tours";
                            break;
                        //"Water Activity";
                        case 36:
                            SeoString = strProduct + "  Pictures :" + strDestination + " Water Activities, Attraction, Package Tours";
                            break;
                        //"Shows";
                        case 38:
                            SeoString = strProduct + "  Pictures :" + strDestination + " Shows & Events";
                            break;
                        //"Health Check Up";
                        case 39:
                            SeoString = strProduct + "  Pictures :" + strDestination + " Health Check Up & Medical Tour & Hospitals";
                            break;
                        //"Spa";
                        case 40:
                            SeoString = strProduct + "  Pictures :" + strDestination + " Spa";
                            break;
                    }
                    
                    break;
                case SEO_PageType.Review:
                    switch (seoProductCat)
                    {
                        case 29:
                            SeoString = strProduct + " , " + strDestination + " Hotels Thailand Hotels";
                            break;
                        //"Golf Courses";
                        case 32:
                            SeoString = strProduct + " , " + strDestination + " Golf Course";
                            break;
                        //"Day Trips";
                        case 34:
                            SeoString = strProduct + "  Reviews :" + strDestination + " Sightseeing, Attraction, Package Tours";
                            break;
                        //"Water Activity";
                        case 36:
                            SeoString = strProduct + "  Reviews :" + strDestination + " Water Activities, Attraction, Package Tours";
                            break;
                        //"Shows";
                        case 38:
                            SeoString = strProduct + "  Reviews :" + strDestination + " Shows & Events";
                            break;
                        //"Health Check Up";
                        case 39:
                            SeoString = strProduct + "  Reviews :" + strDestination + " Health Check Up & Medical Tour & Hospitals";
                            break;
                        //"Spa";
                        case 40:
                            SeoString = strProduct + "  Reviews :" + strDestination + " Spa";
                            break;
                    }
                    
                    break;
                case SEO_PageType.Why:
                    switch (seoProductCat)
                    {
                        case 29:
                            SeoString = strProduct + " , " + strDestination + " Hotels Thailand Hotels";
                            break;
                        //"Golf Courses";
                        case 32:
                            SeoString = strProduct + " , " + strDestination + " Golf Course";
                            break;
                        //"Day Trips";
                        case 34:
                            SeoString = strProduct + "  Why Choose Hotels2Thailand.com? :" + strDestination + " Sightseeing, Attraction, Package Tours";
                            break;
                        //"Water Activity";
                        case 36:
                            SeoString = strProduct + "  Why Choose Hotels2Thailand.com? :" + strDestination + " Water Activities, Attraction, Package Tours";
                            break;
                        //"Shows";
                        case 38:
                            SeoString = strProduct + "  Why Choose Hotels2Thailand.com? :" + strDestination + " Shows & Events";
                            break;
                        //"Health Check Up";
                        case 39:
                            SeoString = strProduct + "  Why Choose Hotels2Thailand.com? :" + strDestination + " Health Check Up & Medical Tour & Hospitals";
                            break;
                        //"Spa";
                        case 40:
                            SeoString = strProduct + "  Why Choose Hotels2Thailand.com? :" + strDestination + " Spa";
                            break;
                    }
                    break;
                case SEO_PageType.Contact:
                    switch (seoProductCat)
                    {
                        case 29:
                            SeoString = strProduct + " , " + strDestination + " Hotels Thailand Hotels";
                            break;
                        //"Golf Courses";
                        case 32:
                            SeoString = strProduct + " , " + strDestination + " Thailand Golf";
                            break;
                        //"Day Trips";
                        case 34:
                            SeoString = strProduct + "  Contact Us :" + strDestination + " Sightseeing, Attraction, Package Tours";
                            break;
                        //"Water Activity";
                        case 36:
                            SeoString = strProduct + "  Contact Us :" + strDestination + " Water Activities, Attraction, Package Tours";
                            break;
                        //"Shows";
                        case 38:
                            SeoString = strProduct + "  Contact Us :" + strDestination + " Shows & Events";
                            break;
                        //"Health Check Up";
                        case 39:
                            SeoString = strProduct + "  Contact Us :" + strDestination + " Health Check Up & Medical Tour & Hospitals";
                            break;
                        //"Spa";
                        case 40:
                            SeoString = strProduct + "  Contact Us :" + strDestination + " Spa";
                            break;
                    }
                    break;

            }

            return SeoString;
        }

        private static string canonical_Gen(SEO_PageType seoPageType, byte seoProductCat, string strfileName)
        {
            string SeoString = string.Empty;
            switch (seoPageType)
            {
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

    }

    public enum SEO_StringType : int
    {
        Meta = 1,
        Title = 2,
        H1 = 3,
        Link_Back_To_Home = 4,
        canonical = 5
    }

    public enum SEO_PageType : int
    {
        Destination = 1,
        Location = 2,
        Detail = 3,
        Information = 4,
        Photo = 5,
        Review = 6,
        Why = 7,
        Contact = 8,
        Home = 9
    }
    
}