using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Hotels2thailand.DataAccess;
using Hotels2thailand;
using System.Web.Configuration;
using System.Data;

/// <summary>
/// Summary description for PriceDiscountHidden
/// </summary>
/// 

namespace Hotels2thailand.Front
{
    public class PriceDiscountHidden
    {
        public PriceDiscountHidden()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public double PriceDiscount(string strQuery, string strReferer, string strLocationID,string strDestinationID, string strProductcatID, string strProductID, string strCountryID, DateTime dateCheckIn, DateTime dateCheckOut)
        {
            double intResult = 0;
            string strConn = WebConfigurationManager.ConnectionStrings["booking2hotelXConnectionString"].ConnectionString;
            bool bolCheckTime = false;
            bool bolChecklocation = false;
            bool bolCheckDestination = false;
            bool bolCheckCat = false;
            bool bolCheckProduct = false;
            bool bolCheckCountry = false;
            bool bolCheckQuery = false;
            bool bolCheckRefer = false;
            Dictionary<string, string> dicCookiedata = new Dictionary<string, string>();
            string strCookieQuery = Hotels2Cookie.CookieGet("discount", "query").Replace("||", "&");
            string strCookieReferer = Hotels2Cookie.CookieGet("discount", "referer").Replace("||", "&");
            string strCookieCountryID = Hotels2Cookie.CookieGet("discount", "countryID");
            string strCookieDiscountID = Hotels2Cookie.CookieGet("discount", "discountID");
            string sqlDiscount = "";

            sqlDiscount = "SELECT discount_id,title,detail,discount_baht,discount_percent,time_booking_start,time_booking_end,con_query_string,con_destination_id,con_location_id,con_country_id,con_product_id,con_product_cat_id,con_referer";
            sqlDiscount = sqlDiscount + " FROM tbl_discount";
            sqlDiscount = sqlDiscount + " WHERE date_booking_start<=GETDATE() AND date_booking_end>=GETDATE() ";
            sqlDiscount = sqlDiscount + " AND date_stay_start<='" + dateCheckIn.Hotels2DateToSQlStringNoSingleCode() + "' AND date_stay_end>='" + dateCheckIn.Hotels2DateToSQlStringNoSingleCode() + "' ";
            sqlDiscount = sqlDiscount + " AND date_stay_start<='" + dateCheckOut.Hotels2DateToSQlStringNoSingleCode() + "' AND date_stay_end>='" + dateCheckOut.Hotels2DateToSQlStringNoSingleCode() + "' ";
            sqlDiscount = sqlDiscount + " AND status=1 AND con_min_night<=" + dateCheckOut.Subtract(dateCheckIn).Days;
            sqlDiscount = sqlDiscount + " ORDER BY priority ASC, discount_id DESC";

            // Check Current Benefit //
            using (SqlConnection objConn = new SqlConnection(strConn))
            {
               SqlCommand objCom = new SqlCommand(sqlDiscount, objConn);
               objConn.Open();
               IDataReader reader = objCom.ExecuteReader();

               while (reader.Read())
               {
                   bolCheckTime = checkTime((DateTime)reader["time_booking_start"], (DateTime)reader["time_booking_end"]);
                   bolChecklocation = checkLocation(reader["con_location_id"].ToString(), strLocationID);
                   bolCheckDestination = checkDestination(reader["con_destination_id"].ToString(), strDestinationID);
                   bolCheckCat = checkProductCat(reader["con_product_cat_id"].ToString(), strProductcatID);
                   bolCheckCountry = checkCountry(reader["con_country_id"].ToString(), strCountryID);
                   bolCheckProduct = checkProduct(reader["con_product_id"].ToString(), strProductID);
                   bolCheckQuery = checkQuery(reader["con_query_string"].ToString(), strQuery);
                   bolCheckRefer = checkrefer(reader["con_referer"].ToString(), strReferer);

                   if (bolCheckTime && bolChecklocation && bolCheckDestination && bolCheckCat && bolCheckCountry && bolCheckProduct && bolCheckQuery && bolCheckRefer)
                   {
                       if ((decimal)reader["discount_baht"] > 0)
                       {
                           intResult = Convert.ToDouble(reader["discount_baht"]);
                       }
                       else
                       {
                          intResult = 1 - (Convert.ToDouble(reader["discount_percent"]) / 100);
                       }

                       dicCookiedata.Add("query", strQuery.Replace("&","||"));
                       dicCookiedata.Add("referer", strReferer.Replace("&", "||"));
                       dicCookiedata.Add("countryID", strCountryID);
                       dicCookiedata.Add("discountID", reader["discount_id"].ToString());
                       Hotels2Cookie.CookieSet("discount", dicCookiedata, 30);
                       break;
                    }
                }
            }
            // Check Current Benefit //

            // Check Cookije Benefit //
            if (intResult == 0 && !String.IsNullOrEmpty(strCookieDiscountID))
            {
                using (SqlConnection objConn = new SqlConnection(strConn))
                {
                    SqlCommand objCom = new SqlCommand(sqlDiscount, objConn);
                    objConn.Open();
                    IDataReader reader = objCom.ExecuteReader();

                    while (reader.Read())
                    {
                        bolCheckTime = checkTime((DateTime)reader["time_booking_start"], (DateTime)reader["time_booking_end"]);
                        bolChecklocation = checkLocation(reader["con_location_id"].ToString(), strLocationID);
                        bolCheckDestination = checkDestination(reader["con_destination_id"].ToString(), strDestinationID);
                        bolCheckCat = checkProductCat(reader["con_product_cat_id"].ToString(), strProductcatID);
                        bolCheckCountry = checkCountry(reader["con_country_id"].ToString(), strCookieCountryID);
                        bolCheckProduct = checkProduct(reader["con_product_id"].ToString(), strProductID);
                        bolCheckQuery = checkQuery(reader["con_query_string"].ToString(), strCookieQuery);
                        bolCheckRefer = checkrefer(reader["con_referer"].ToString(), strCookieReferer);


                        if (bolCheckTime && bolChecklocation && bolCheckDestination && bolCheckCat && bolCheckCountry && bolCheckProduct && bolCheckQuery && bolCheckRefer)
                        {
                            if ((decimal)reader["discount_baht"] > 0)
                            {
                                intResult = Convert.ToDouble(reader["discount_baht"]);
                            }
                            else
                            {
                                intResult = 1 - (Convert.ToDouble(reader["discount_percent"]) / 100);
                            }

                            dicCookiedata.Add("query", strCookieQuery.Replace("&", "||"));
                            dicCookiedata.Add("referer", strCookieReferer.Replace("&", "||"));
                            dicCookiedata.Add("countryID", strCookieCountryID);
                            dicCookiedata.Add("discountID", reader["discount_id"].ToString());
                            Hotels2Cookie.CookieSet("discount", dicCookiedata, 30);
                            break;
                        }
                    }
                }
            }
            // Check Cookije Benefit //

            return intResult;
        }

        // Check time of day //
        private bool checkTime(DateTime timeStart, DateTime timeEnd)
        {
            bool bolResult = false;
            int intHourstart;
            int intHourEnd;
            int intHourCurrent;

            DateTime timeCurrent = DateTime.Now.Hotels2ThaiDateTime();

            intHourstart = timeStart.Hour;
            intHourEnd = timeEnd.Hour;
            intHourCurrent = timeCurrent.Hour;

            if (timeEnd == timeStart)
            {
                bolResult = true;
            }
            else if (timeEnd > timeStart)
            {
                if (intHourstart <= intHourCurrent && intHourEnd >= intHourCurrent)
                {
                    bolResult = true;
                }
            }
            else
            {
                if(intHourCurrent >= intHourstart || intHourCurrent <= intHourEnd)
                {
                    bolResult = true;
                }
            }

            return bolResult;
        }
        // Check time of day //

        // Check location ID //
        private bool checkLocation(string strLocationList, string strLocationID)
        {
            bool bolResult = false;
            string[] arrLocation;
            string[] arrLocationInput;

            if (!String.IsNullOrEmpty(strLocationList))
            {
                arrLocation = strLocationList.Split(',');
                arrLocationInput = strLocationID.Split(',');

                for (int intCount = 0; intCount < arrLocation.Count(); intCount++)
                {
                    for (int intCountInput = 0; intCountInput < arrLocationInput.Count(); intCountInput++)
                    {
                        if(arrLocation[intCount]==arrLocationInput[intCountInput])
                        {
                            bolResult = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                bolResult = true;
            }

            return bolResult;
        }
        // Check location ID //

        // Check destination ID //
        private bool checkDestination(string strDestinationList, string strDestinationID)
        {
            bool bolResult = false;
            string[] arrDestination;

            if (!String.IsNullOrEmpty(strDestinationList))
            {
                arrDestination = strDestinationList.Split(',');
                for (int intCount = 0; intCount < arrDestination.Count(); intCount++)
                {
                    if (strDestinationID == arrDestination[intCount])
                    {
                        bolResult = true;
                        break;
                    }
                }
            }
            else
            {
                bolResult = true;
            }

            return bolResult;
        }
        // Check destination ID //


        // Check ProductCat ID //
        private bool checkProductCat(string strProductCatList, string strProductCatID)
        {
            bool bolResult = false;
            string[] arrProductCat;

            if (!String.IsNullOrEmpty(strProductCatList))
            {
                arrProductCat = strProductCatList.Split(',');
                for (int intCount = 0; intCount < arrProductCat.Count(); intCount++)
                {
                    if (strProductCatID == arrProductCat[intCount])
                    {
                        bolResult = true;
                        break;
                    }
                }
            }
            else
            {
                bolResult = true;
            }

            return bolResult;
        }
        // Check ProductCat ID //

        // Check Country ID //
        private bool checkCountry(string strCountryList, string strCountryID)
        {
            bool bolResult = false;
            string[] arrCountry;

            if (!String.IsNullOrEmpty(strCountryList))
            {
                arrCountry = strCountryList.Split(',');
                for (int intCount = 0; intCount < arrCountry.Count(); intCount++)
                {
                    if (strCountryID == arrCountry[intCount])
                    {
                        bolResult = true;
                        break;
                    }
                }
            }
            else
            {
                bolResult = true;
            }

            return bolResult;
        }
        // Check Country ID //

        // Check Product ID //
        private bool checkProduct(string strProductList, string strProductID)
        {
            bool bolResult = false;
            string[] arrProduct;

            if (!String.IsNullOrEmpty(strProductList))
            {
                arrProduct = strProductList.Split(',');
                for (int intCount = 0; intCount < arrProduct.Count(); intCount++)
                {
                    if (strProductID == arrProduct[intCount])
                    {
                        bolResult = true;
                        break;
                    }
                }
            }
            else
            {
                bolResult = true;
            }

            return bolResult;
        }
        // Check Product ID //

        // Check Referer //
        private bool checkrefer(string strReferList, string strReferer)
        {
            bool bolResult = false;
            string[] arrRefer;

            if (!String.IsNullOrEmpty(strReferList))
            {
                arrRefer = strReferList.Split(',');
                for (int intCount = 0; intCount < arrRefer.Count(); intCount++)
                {
                    if (strReferer.IndexOf(arrRefer[intCount]) > 0)
                    {
                        bolResult = true;
                        break;
                    }
                }
            }
            else
            {
                bolResult = true;
            }
            return bolResult;
        }
        // Check Referer //

        // Check query string //
        private bool checkQuery(string strQueryList, string strQuery)
        {
            bool bolResult = false;
            string[] arrQuery;

            if (!String.IsNullOrEmpty(strQueryList))
            {
                arrQuery = strQueryList.Split(',');
                for (int intCount = 0; intCount < arrQuery.Count(); intCount++)
                {
                    if (strQuery.IndexOf(arrQuery[intCount]) > 0)
                    {
                        bolResult = true;
                        break;
                    }
                }
            }
            else
            {
                bolResult = true;
            }
            return bolResult;
        }
        // Check query string //

    }
}