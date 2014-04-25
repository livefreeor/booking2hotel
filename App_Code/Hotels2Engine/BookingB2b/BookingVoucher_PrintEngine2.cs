using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Collections;
using System.Xml;
using System.Text;
using Hotels2thailand.Front;

using Hotels2thailand.ProductOption;
using Hotels2thailand;

using Hotels2thailand.Booking;

/// <summary>
/// Summary description for Voucher
/// </summary>
/// 
namespace Hotels2thailand.BookingB2b
{
    public partial class BookingVoucher_PrintEngineB2b
    {
        /// <summary>
        /// get Extra Option Ex.. Gala , Extra Bed , breakfast ---- Hotel Only 
        /// </summary>
        /// <param name="intBookingProductId"></param>
        /// <returns></returns>
        private string GetProductItemExtra(int intBookingProductId, byte BookingLangId)
        {
            StringBuilder TiemResult = new StringBuilder();
            string strFeildHead = string.Empty;
            string strFeildHead1 = string.Empty;
            string strFeildHead2 = string.Empty;

            switch (BookingLangId)
            {
                case 1:
                    strFeildHead = "No.";
                    strFeildHead1 = "Extra Option";
                    strFeildHead2 = "Quantity";
                    break;
                case 2:
                    strFeildHead = "ลำดับ";
                    strFeildHead1 = "สินค้าเพิ่มเติม";
                    strFeildHead2 = "จำนวน";
                    break;
            }

            Hotels2thailand.Booking.BookingItemDisplay cBookingItem = new Hotels2thailand.Booking.BookingItemDisplay();
            string ExtraTptionNotIn = "IN (39,40,43,44,47)";
            List<object> cBookingItemListDefault = cBookingItem.getBookingItemListByBookingProductIdAndCatId_ToCustomerDisplay(intBookingProductId, ExtraTptionNotIn, 1);
            List<object> cBookingItemListReal = cBookingItem.getBookingItemListByBookingProductIdAndCatId_ToCustomerDisplay(intBookingProductId, ExtraTptionNotIn, BookingLangId);

            List<object> cBookingItemList = null;
            if (cBookingItemListDefault.Count == cBookingItemListReal.Count)
                cBookingItemList = cBookingItemListReal;
            else
                cBookingItemList = cBookingItemListDefault;
            int Count = 1;
            if (cBookingItemList.Count > 0)
            {
                TiemResult.Append("<table cellpadding=\"0\" cellspacing=\"1\" class=\"option\" style=\"margin-top:0px;\">");

                TiemResult.Append("<tr>");
                TiemResult.Append("<th width=\"20\">" + strFeildHead + "</th>");
                TiemResult.Append("<th>" + strFeildHead1 + "</th>");
                TiemResult.Append("<th width=\"91\">" + strFeildHead2 + "</th>");
                TiemResult.Append("</tr>");
                foreach (BookingItemDisplay items in cBookingItemList)
                {


                    TiemResult.Append("<tr>");
                    TiemResult.Append("<td class=\"info\">" + Count + ".</td>");
                    TiemResult.Append("<td>" + Hotels2String.GetOptionTitle(items.OptionTitle, items.BookingOptionTitle) + "</td>");
                    TiemResult.Append("<td align=\"center\">" + items.Unit + "</td>");
                    TiemResult.Append("</tr>");
                    Count = Count + 1;
                }
                TiemResult.Append("</table></br>");
            }
            return TiemResult.ToString();
        }


        private string GetProductItemHotel(int intBookingProductId, string NightREsult, byte BookingLangId)
        {
            StringBuilder TiemResult = new StringBuilder();
            BookingItemDisplay cBookingItem = new BookingItemDisplay();
            PromotionContent cProContent = new PromotionContent();
            string ExtraTptionNotIn = "NOT IN (39,40,43,44,47)";


            List<object> cBookingItemListDefault = cBookingItem.getBookingItemListByBookingProductIdAndCatId_ToCustomerDisplay(intBookingProductId, ExtraTptionNotIn, 1);
            List<object> cBookingItemListReal = cBookingItem.getBookingItemListByBookingProductIdAndCatId_ToCustomerDisplay(intBookingProductId, ExtraTptionNotIn, BookingLangId);

            List<object> cBookingItemList = null;
            if (cBookingItemListDefault.Count == cBookingItemListReal.Count)
                cBookingItemList = cBookingItemListReal;
            else
                cBookingItemList = cBookingItemListDefault;
            
            //HttpContext.Current.Response.Write(cBookingItemList.Count);
            //HttpContext.Current.Response.End();
           int Count = 1;
            foreach (BookingItemDisplay items in cBookingItemList)
            {
                     TiemResult.Append("<tr>");
                     TiemResult.Append("<td class=\"info\" >" + Count + ".</td>");
                     TiemResult.Append("<td>" + Hotels2String.GetOptionTitle(items.OptionTitle, items.BookingOptionTitle) + "<br />	<span class=\"breakfast\">");

                     if (items.IsExtraNet && items.NumAdult.HasValue)
                     {

                         TiemResult.Append("<br/>" + items.ConditionTitle + Hotels2String.AppendConditionDetailExtraNet((byte)items.NumAdult, items.BreakfastBookingItem));
                         
                     }
                     else
                     {
                         if (items.NumAdult.HasValue && items.NumChild.HasValue)
                         {
                             if (items.NumAdult > 0)
                                 TiemResult.Append("&nbsp;(For " + items.NumAdult + " Adult )&nbsp;");
                             
                         }
                         else
                         {
                             if (items.Condition_NumAdult > 0)
                                 TiemResult.Append("&nbsp;(For " + items.Condition_NumAdult + " Adult )&nbsp;");
                         
                         }


                         if (items.BreakfastBookingItem > 0)
                             TiemResult.Append("&nbsp;<strong>(&nbsp;Breakfast included&nbsp; " + items.BreakfastBookingItem + " &nbsp;Pax &nbsp;)</strong>");
                         else
                             TiemResult.Append("&nbsp;<strong>(Room Only)</strong>");
                     }



                     if (items.PromotionID.HasValue && !string.IsNullOrEmpty(items.PromotionDetail))
                     {
                         TiemResult.Append("<br/><span>"+ Hotels2XMLContent.Hotels2XMlReaderPomotionDetail(items.PromotionDetail) + "</span>");

                     }

                     TiemResult.Append("</span></td>");
                     TiemResult.Append("<td align=\"center\">" + NightREsult + "</td>");
                     TiemResult.Append("<td align=\"center\">" + items.Unit + "</td>");
                     TiemResult.Append("</tr>");



                Count = Count + 1;
            }

            return TiemResult.ToString();
        }

        //============================================================================================================================

        private string GetProductItem_Golf(int intBookingProductId, string NightREsult, byte BookingLangId)
        {
            StringBuilder TiemResult = new StringBuilder();
            BookingItemDisplay cBookingItem = new BookingItemDisplay();
            PromotionContent cProContent = new PromotionContent();
            string ExtraTptionNotIn = "NOT IN (39,40,43,44,47)";
            List<object> cBookingItemListDefault = cBookingItem.getBookingItemListByBookingProductIdAndCatId_ToCustomerDisplay(intBookingProductId, ExtraTptionNotIn, 1);
            List<object> cBookingItemListReal = cBookingItem.getBookingItemListByBookingProductIdAndCatId_ToCustomerDisplay(intBookingProductId, ExtraTptionNotIn, BookingLangId);

            List<object> cBookingItemList = null;
            
            if (cBookingItemListDefault.Count == cBookingItemListReal.Count)
                cBookingItemList = cBookingItemListReal;
            else
                cBookingItemList = cBookingItemListDefault;
            int Count = 1;
            foreach (BookingItemDisplay items in cBookingItemList)
            {
                TiemResult.Append("<tr>");

                TiemResult.Append("<td class=\"info\" >" + Count + ".</td>");

                TiemResult.Append("<td>" + Hotels2String.GetOptionTitle(items.OptionTitle, items.BookingOptionTitle));
                
                TiemResult.Append("</td>");

                TiemResult.Append("<td align=\"center\">" + items.Unit + "</td>");

                TiemResult.Append("</tr>");

                Count = Count + 1;
            }

            return TiemResult.ToString();
        }

        
        //============================================================================================================================

        private string GetProductItem_Spa(int intBookingProductId, string NightREsult, byte BookingLangId)
        {
            StringBuilder TiemResult = new StringBuilder();
            BookingItemDisplay cBookingItem = new BookingItemDisplay();
            PromotionContent cProContent = new PromotionContent();
            string ExtraTptionNotIn = "NOT IN (39,40,43,44,47)";
            List<object> cBookingItemListDefault = cBookingItem.getBookingItemListByBookingProductIdAndCatId_ToCustomerDisplay(intBookingProductId, ExtraTptionNotIn, 1);
            List<object> cBookingItemListReal = cBookingItem.getBookingItemListByBookingProductIdAndCatId_ToCustomerDisplay(intBookingProductId, ExtraTptionNotIn, BookingLangId);

            List<object> cBookingItemList = null;
            
            if (cBookingItemListDefault.Count == cBookingItemListReal.Count)
                cBookingItemList = cBookingItemListReal;
            else
                cBookingItemList = cBookingItemListDefault;
            int Count = 1;
            foreach (BookingItemDisplay items in cBookingItemList)
            {
                TiemResult.Append("<tr>");

                TiemResult.Append("<td class=\"info\">" + Count + ".</td>");

                TiemResult.Append("<td>" + Hotels2String.GetOptionTitle(items.OptionTitle, items.BookingOptionTitle));

                TiemResult.Append("</td>");

                TiemResult.Append("<td align=\"center\">" + items.Unit + "</td>");

                TiemResult.Append("</tr>");

                Count = Count + 1;
            }

            return TiemResult.ToString();
        }

        //============================================================================================================================

        private string GetProductItem_Transfer(int intBookingProductId, string NightREsult, byte BookingLangId)
        {
            StringBuilder TiemResult = new StringBuilder();
            BookingItemDisplay cBookingItem = new BookingItemDisplay();
            PromotionContent cProContent = new PromotionContent();
            string ExtraTptionNotIn = "IN (43,44)";
            List<object> cBookingItemListDefault = cBookingItem.getBookingItemListByBookingProductIdAndCatId_ToCustomerDisplay(intBookingProductId, ExtraTptionNotIn, 1);
            List<object> cBookingItemListReal = cBookingItem.getBookingItemListByBookingProductIdAndCatId_ToCustomerDisplay(intBookingProductId, ExtraTptionNotIn, BookingLangId);

            List<object> cBookingItemList = null;
            if (cBookingItemListDefault.Count == cBookingItemListReal.Count)
                cBookingItemList = cBookingItemListReal;
            else
                cBookingItemList = cBookingItemListDefault;
            int Count = 1;
            foreach (BookingItemDisplay items in cBookingItemList)
            {
                TiemResult.Append("<tr>");

                TiemResult.Append("<td class=\"info\">" + Count + ".</td>");

                TiemResult.Append("<td>" + Hotels2String.GetOptionTitle(items.OptionTitle, items.BookingOptionTitle));
                if (string.IsNullOrEmpty(items.BookingItemDetail))
                TiemResult.Append("<p style=\"margin:2px 0px 0px 5px;padding:0px;\">" + items.BookingItemDetail + "</p>");
                
                TiemResult.Append("</td>");

                TiemResult.Append("<td align=\"center\">" + items.Unit + "</td>");

                TiemResult.Append("</tr>");

                Count = Count + 1;
            }

            return TiemResult.ToString();
        }
        
        //============================================================================================================================

        private string GetProductItem_Show_water_Daytrip(int intBookingProductId, string NightREsult, byte BookingLangId)
        {
            StringBuilder TiemResult = new StringBuilder();
            BookingItemDisplay cBookingItem = new BookingItemDisplay();
            PromotionContent cProContent = new PromotionContent();
            string ExtraTptionNotIn = "NOT IN (39,40,43,44,47)";
            List<object> cBookingItemListDefault = cBookingItem.getBookingItemListByBookingProductIdAndCatId_ToCustomerDisplay(intBookingProductId, ExtraTptionNotIn, 1);
            List<object> cBookingItemListReal = cBookingItem.getBookingItemListByBookingProductIdAndCatId_ToCustomerDisplay(intBookingProductId, ExtraTptionNotIn, BookingLangId);

            List<object> cBookingItemList = null;
            if (cBookingItemListDefault.Count == cBookingItemListReal.Count)
                cBookingItemList = cBookingItemListReal;
            else
                cBookingItemList = cBookingItemListDefault;
            int Count = 1;
            foreach (BookingItemDisplay items in cBookingItemList)
            {
                TiemResult.Append("<tr>");

                TiemResult.Append("<td class=\"info\">" + Count + ".</td>");

                TiemResult.Append("<td>" + Hotels2String.GetOptionTitle(items.OptionTitle, items.BookingOptionTitle) + "<br/>");

                //TiemResult.Append(items.ConditionTitle);

                if (items.NumAdult.HasValue && items.NumChild.HasValue)
                {
                    if (items.NumAdult > 0 && items.NumChild == 0)
                        TiemResult.Append("&nbsp;(For " + items.NumAdult + " Adult )&nbsp;");
                    if (items.NumChild > 0 && items.NumAdult == 0)
                        TiemResult.Append("&nbsp;(For " + items.NumChild + " Child )&nbsp;");
                    if (items.NumChild > 0 && items.NumAdult > 0)
                        TiemResult.Append("&nbsp;(For " + items.NumAdult + " Adult or " + items.NumChild + " Child)&nbsp;");
                }
                else
                {
                    if (items.Condition_NumAdult > 0 && items.Condition_NumChild == 0)
                        TiemResult.Append("&nbsp;(For " + items.Condition_NumAdult + " Adult )&nbsp;");
                    if (items.Condition_NumChild > 0 && items.Condition_NumAdult == 0)
                        TiemResult.Append("&nbsp;(For " + items.Condition_NumChild + " Child)&nbsp;");
                    if (items.Condition_NumChild > 0 && items.Condition_NumAdult > 0)
                        TiemResult.Append("&nbsp;(For " + items.Condition_NumChild + " Adult or " + items.Condition_NumAdult + "Child)&nbsp;");
                }


                //if (items.NumAdult > 0 && items.NumChild == 0)
                //    TiemResult.Append("&nbsp;(For Adult)&nbsp;");
                //if (items.NumChild > 0 && items.NumAdult == 0)
                //    TiemResult.Append("&nbsp;(For Child)&nbsp;");
                //if (items.NumChild > 0 && items.NumAdult > 0)
                //    TiemResult.Append("&nbsp;(For Adult & Children)&nbsp;");

                TiemResult.Append("</td>");

                TiemResult.Append("<td align=\"center\">" + DateTimeCheck_TimeOnly(items.OptionTimeStart) + "-" + DateTimeCheck_TimeOnly(items.OptionTimeEnd) + "</td>");

                //TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">" + DateTimeCheck_TimeOnly(items.OptionTimePickup) + "</td>");

                TiemResult.Append("<td align=\"center\">" + items.Unit + "</td>");

                TiemResult.Append("</tr>");

                Count = Count + 1;
            }

            return TiemResult.ToString();
        }

        //============================================================================================================================

        private string GetProductItem_Health(int intBookingProductId, string NightREsult, byte LangId)
        {
            StringBuilder TiemResult = new StringBuilder();
            BookingItemDisplay cBookingItem = new BookingItemDisplay();
            PromotionContent cProContent = new PromotionContent();
            string ExtraTptionNotIn = "NOT IN (39,40,43,44,47)";
            List<object> cBookingItemList = cBookingItem.getBookingItemListByBookingProductIdAndCatId_ToCustomerDisplay(intBookingProductId, ExtraTptionNotIn);
            int Count = 1;
            BookingGuest_Engine cGuest = new BookingGuest_Engine();
            foreach (BookingItemDisplay items in cBookingItemList)
            {
                cGuest = cGuest.getGuestByBookingItem(intBookingProductId, items.BookingItemID);
                TiemResult.Append("<tr class=\"mStrong\">");

                TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">" + Count + ".</td>");

                TiemResult.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + cGuest.GuestName + "</td>");

                TiemResult.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + DateTimeCheck(cGuest.DateBirth) + "</td>");

                TiemResult.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + cGuest.PassportId + "</td>");

                TiemResult.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + Hotels2String.GetOptionTitle(items.OptionTitle, items.BookingOptionTitle) + "</td>");

                TiemResult.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + DateTimeCheck_TimeOnly(items.OptionTimePickup) + "</td>");

                TiemResult.Append("</tr>");

                Count = Count + 1;
            }

            return TiemResult.ToString();
        }
        //============================================================================================================================

        
    }
}