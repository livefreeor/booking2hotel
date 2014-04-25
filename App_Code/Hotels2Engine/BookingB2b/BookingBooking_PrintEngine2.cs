using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Collections;
using System.Xml;
using System.Text;
using Hotels2thailand.Front;
using Hotels2thailand;
using Hotels2thailand.Staffs;
using Hotels2thailand.Suppliers;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Booking;

/// <summary>
/// Summary description for Voucher
/// </summary>
/// 
namespace Hotels2thailand.BookingB2b
{
    public partial class BookingBooking_PrintEngineB2b 
    {

        /// <summary>
        /// get Extra Option Ex.. Gala , Extra Bed , breakfast ---- Hotel Only 
        /// </summary>
        /// <param name="intBookingProductId"></param>
        /// <returns></returns>
        private string GetProductItemExtra(int intBookingProductId)
        {
            StringBuilder TiemResult = new StringBuilder();
            BookingItemDisplay cBookingItem = new BookingItemDisplay();
            string ExtraTptionNotIn = "IN (39,40,43,44,47)";
            List<object> cBookingItemList = cBookingItem.getBookingItemListByBookingProductIdAndCatId_ToCustomerDisplay(intBookingProductId, ExtraTptionNotIn, 1);
            //List<object> cBookingItemList = cBookingItem.getBookingItemListByBookingProductIdAndCatId_ToCustomerDisplay(intBookingProductId, ExtraTptionNotIn);
            int Count = 1;
            decimal Subtotal = 0;
            if (cBookingItemList.Count > 0)
            {
                TiemResult.Append("<table width=\"100%\" border=\"0\" cellpadding=\"2\" cellspacing=\"1\" bgcolor=\"#3A3A3A\">");
                TiemResult.Append("<tr class=\"mStrong\">");
                TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">No.</td>");
                TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">Extra Option</td>");
                TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">Quantity</td>");
                TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">Price</td>");
                TiemResult.Append("</tr>");
                foreach (BookingItemDisplay items in cBookingItemList)
                {


                    TiemResult.Append("<tr class=\"mStrong\">");
                    TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">" + Count + ".</td>");
                    TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">" + Hotels2String.GetOptionTitle(items.OptionTitle, items.BookingOptionTitle) + "</td>");
                    TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">" + items.Unit + "</td>");
                    TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">" + items.PriceSupplier.Hotels2Currency() + "</td>");
                    TiemResult.Append("</tr>");


                    Count = Count + 1;
                    Subtotal = Subtotal + items.PriceSupplier;
                }

                TiemResult.Append("<tr>");

                TiemResult.Append("<td colspan=\"3\" align=\"center\" bgcolor=\"#FFFFFF\" class=\"l1\">Sub Total </td>");
                TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\"><strong>" + Subtotal.Hotels2Currency() + "</strong></td>");
                TiemResult.Append("</tr>");
                TiemResult.Append("</table>");
                TiemResult.Append("<br />");
            }
            return TiemResult.ToString();
        }

        private string GetProductItem_hotel(int intBookingProductId, string NightREsult, byte BookingLangId)
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
            decimal Subtotal = 0;
            foreach (BookingItemDisplay items in cBookingItemList)
            {
                TiemResult.Append("<tr>");

                TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">" + Count + ".</td>");
                TiemResult.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + Hotels2String.GetOptionTitle(items.OptionTitle, items.BookingOptionTitle));
                if (items.IsExtraNet && items.NumAdult.HasValue)
                {
                    
                    
                    TiemResult.Append("<br/>" + items.ConditionTitle + Hotels2String.AppendConditionDetailExtraNet((byte)items.NumAdult, items.BreakfastBookingItem));
                    TiemResult.Append("");
                    TiemResult.Append("");
                    TiemResult.Append("");
                }
                else
                {
                    if (items.NumAdult.HasValue && items.NumChild.HasValue)
                    {
                        if (items.NumAdult > 0 )
                            TiemResult.Append("&nbsp;(For " + items.NumAdult + " Adult )&nbsp;");
                        //if (items.NumChild > 0 && items.NumAdult == 0)
                        //    TiemResult.Append("&nbsp;(For " + items.NumChild + " Child )&nbsp;");
                        //if (items.NumChild > 0 && items.NumAdult > 0)
                        //    TiemResult.Append("&nbsp;(For " + items.NumAdult + " Adult & " + items.NumChild + " Children)&nbsp;");
                    }
                    else
                    {
                        if (items.Condition_NumAdult > 0 )
                            TiemResult.Append("&nbsp;(For " + items.Condition_NumAdult + " Adult )&nbsp;");
                        //if (items.Condition_NumChild > 0 && items.Condition_NumAdult == 0)
                        //    TiemResult.Append("&nbsp;(For " + items.Condition_NumChild + " Child)&nbsp;");
                        //if (items.Condition_NumChild > 0 && items.Condition_NumAdult > 0)
                        //    TiemResult.Append("&nbsp;(For " + items.Condition_NumChild + " Adult & " + items.Condition_NumAdult + "Children)&nbsp;");
                    }

                    if (items.BreakfastBookingItem > 0)
                        TiemResult.Append("<strong>(&nbsp;Breakfast included&nbsp; " + items.BreakfastBookingItem + " &nbsp;Pax &nbsp;)</strong>");
                    else
                        TiemResult.Append("<strong>(Room Only)</strong>");
                }
               


                if (items.PromotionID.HasValue && items.PromotionCat.HasValue && !string.IsNullOrEmpty(items.PromotionDetail))
                {
                    if ((byte)items.PromotionCat == 1)
                    {
                        TiemResult.Append(Hotels2XMLContent.Hotels2XMlReaderPomotionDetail(items.PromotionDetail));
                    }

                }
                TiemResult.Append("</td>");


                TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">" + NightREsult + "</td>");

                TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">" + items.Unit + "</td>");

                TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">" + items.PriceSupplier.Hotels2Currency() + "</td>");

                TiemResult.Append("</tr>");

                Count = Count + 1;
                Subtotal = Subtotal + items.PriceSupplier;
            }
            TiemResult.Append("<tr>");
            TiemResult.Append("<td colspan=\"4\" align=\"center\" bgcolor=\"#FFFFFF\" class=\"l1\">Sub Total </td>");
            TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\"><strong>" + Subtotal.Hotels2Currency() + "</strong></td>");
            TiemResult.Append("</tr>");

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
            decimal Subtotal = 0;
            foreach (BookingItemDisplay items in cBookingItemList)
            {
                TiemResult.Append("<tr>");

                TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">" + Count + ".</td>");

                TiemResult.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + Hotels2String.GetOptionTitle(items.OptionTitle, items.BookingOptionTitle));

                TiemResult.Append("</td>");

                TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">" + items.Unit + "</td>");

                TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">" + items.PriceSupplier + "</td>");

                TiemResult.Append("</tr>");

                Count = Count + 1;
                Subtotal = Subtotal + items.PriceSupplier;
            }
            TiemResult.Append("<tr>");
            TiemResult.Append("<td colspan=\"3\" align=\"center\" bgcolor=\"#FFFFFF\" class=\"l1\">Sub Total </td>");
            TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\"><strong>" + Subtotal.Hotels2Currency() + "</strong></td>");
            TiemResult.Append("</tr>");

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
            decimal Subtotal = 0;
            foreach (BookingItemDisplay items in cBookingItemList)
            {
                TiemResult.Append("<tr>");

                TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">" + Count + ".</td>");

                TiemResult.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + Hotels2String.GetOptionTitle(items.OptionTitle, items.BookingOptionTitle));

                TiemResult.Append("</td>");

                TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">" + items.Unit + "</td>");

                TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">" + items.PriceSupplier.Hotels2Currency() + "</td>");

                TiemResult.Append("</tr>");

                Count = Count + 1;
                Subtotal = Subtotal + items.PriceSupplier;
            }
            TiemResult.Append("<tr>");
            TiemResult.Append("<td colspan=\"3\" align=\"center\" bgcolor=\"#FFFFFF\" class=\"l1\">Sub Total </td>");
            TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\"><strong>" + Subtotal.Hotels2Currency() + "</strong></td>");
            TiemResult.Append("</tr>");

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
            decimal Subtotal = 0;
            foreach (BookingItemDisplay items in cBookingItemList)
            {
                TiemResult.Append("<tr>");

                TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">" + Count + ".</td>");

                TiemResult.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + Hotels2String.GetOptionTitle(items.OptionTitle, items.BookingOptionTitle));
                if (string.IsNullOrEmpty(items.BookingItemDetail))
                TiemResult.Append("<p style=\"margin:2px 0px 0px 5px;padding:0px;\">" + items.BookingItemDetail + "</p>");

                TiemResult.Append("</td>");

                TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">" + items.Unit + "</td>");
                TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">" + items.PriceSupplier.Hotels2Currency() + "</td>");
                TiemResult.Append("</tr>");

                Count = Count + 1;
                Subtotal = Subtotal + items.PriceSupplier;
            }

            TiemResult.Append("<tr>");
            TiemResult.Append("<td colspan=\"3\" align=\"center\" bgcolor=\"#FFFFFF\" class=\"l1\">Sub Total </td>");
            TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\"><strong>" + Subtotal.Hotels2Currency() + "</strong></td>");
            TiemResult.Append("</tr>");

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
            decimal Subtotal = 0;
            foreach (BookingItemDisplay items in cBookingItemList)
            {
                TiemResult.Append("<tr>");

                TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">" + Count + ".</td>");

                TiemResult.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + Hotels2String.GetOptionTitle(items.OptionTitle, items.BookingOptionTitle) + "</br>");
                TiemResult.Append(items.ConditionTitle);

                //if (items.NumAdult > 0 && items.NumChild == 0)
                //    TiemResult.Append("&nbsp;(For Adult)&nbsp;");
                //if (items.NumChild > 0 && items.NumAdult == 0)
                //    TiemResult.Append("&nbsp;(For Child)&nbsp;");
                //if (items.NumChild > 0 && items.NumAdult > 0)
                //    TiemResult.Append("&nbsp;(For Adult & Children)&nbsp;");

                TiemResult.Append("</td>");

                TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">" + DateTimeCheck_TimeOnly(items.OptionTimeStart) + "-" + DateTimeCheck_TimeOnly(items.OptionTimeEnd) + "</td>");

               // TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">" + DateTimeCheck_TimeOnly(items.OptionTimePickup) + "</td>");

                TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">" + items.Unit + "</td>");
                TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">" + items.PriceSupplier.Hotels2Currency() + "</td>");
                TiemResult.Append("</tr>");

                Count = Count + 1;
                Subtotal = Subtotal + items.PriceSupplier;
            }
            TiemResult.Append("<tr>");
            TiemResult.Append("<td colspan=\"4\" align=\"center\" bgcolor=\"#FFFFFF\" class=\"l1\">Sub Total </td>");
            TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\"><strong>" + Subtotal.Hotels2Currency() + "</strong></td>");
            TiemResult.Append("</tr>");
            return TiemResult.ToString();
        }

        //============================================================================================================================

        private string GetProductItem_Health(int intBookingProductId, string NightREsult, byte BookingLangId)
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
            decimal Subtotal = 0;
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
                TiemResult.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + items.PriceSupplier.Hotels2Currency() + "</td>");
                TiemResult.Append("</tr>");

                Count = Count + 1;
                Subtotal = Subtotal + items.PriceSupplier;
            }
            TiemResult.Append("<tr>");
            TiemResult.Append("<td colspan=\"5\" align=\"center\" bgcolor=\"#FFFFFF\" class=\"l1\">Sub Total </td>");
            TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\"><strong>" + Subtotal.Hotels2Currency() + "</strong></td>");
            TiemResult.Append("</tr>");
            return TiemResult.ToString();
        }
    }
}