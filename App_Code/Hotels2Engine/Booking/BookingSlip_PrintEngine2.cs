using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Collections;
using System.Xml;
using System.Text;
using Hotels2thailand.Front;
using Hotels2thailand;

using Hotels2thailand.Suppliers;
using Hotels2thailand.ProductOption;


/// <summary>
/// Summary description for Voucher
/// </summary>
/// 
namespace Hotels2thailand.Booking
{
    public partial class BookingSlip_PrintEngine
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



            int Count = 1;
            decimal Subtotal = 0;
            if (cBookingItemList.Count > 0)
            {
                TiemResult.Append("<table width=\"100%\" border=\"0\" cellpadding=\"2\" cellspacing=\"2\" bgcolor=\"#3A3A3A\">");
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
                        if (items.NumAdult > 0)
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


                if ( items.PromotionID.HasValue && items.PromotionCat.HasValue && !string.IsNullOrEmpty(items.PromotionDetail))
                {
                    if ((byte)items.PromotionCat == 1)
                    {
                        TiemResult.Append(Hotels2XMLContent.Hotels2XMlReaderPomotionDetail(items.PromotionDetail));
                    }

                }

                TiemResult.Append("</td>");
                //TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">" + items.Breakfast + "</td>");

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

                TiemResult.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + Hotels2String.GetOptionTitle(items.OptionTitle, items.BookingOptionTitle) + "<br/>");
                TiemResult.Append(items.ConditionTitle);
                //if (items.NumAdult > 0 && items.NumChild == 0)
                //    TiemResult.Append("&nbsp;(For Adult)&nbsp;");
                //if (items.NumChild > 0 && items.NumAdult == 0)
                //    TiemResult.Append("&nbsp;(For Child)&nbsp;");
                //if (items.NumChild > 0 && items.NumAdult > 0)
                //    TiemResult.Append("&nbsp;(For Adult & Children)&nbsp;");

                TiemResult.Append("</td>");

                TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">" + DateTimeCheck_TimeOnly(items.OptionTimeStart) + "-" + DateTimeCheck_TimeOnly(items.OptionTimeEnd) + "</td>");

                //TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">" + DateTimeCheck_TimeOnly(items.OptionTimePickup) + "</td>");

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


        private string GetAccountlist(short shrSupplierId)
        {
            StringBuilder result = new StringBuilder();

            SupplierAccount cAccount = new SupplierAccount();
            IList<object> iAccount = cAccount.getSupplierAccountAllBySupplierID(shrSupplierId);
            result.Append("<table style=\"width:100%;background-color:#000000; font-size:12px;font-family:tahoma\" cellpadding=\"0\" cellspacing=\"2\"  >");
            
            foreach (SupplierAccount acc in iAccount)
            {
                result.Append("<tr style=\"background-color:#ffffff;height:30px;\">");
                result.Append("<td><img src=\"/images/select.png\" /></td>");
                result.Append("<td>" + acc.BankTitle + "</td>");
                result.Append("<td>"+acc.AccountName+"</td>");
                result.Append("<td>"+acc.AccountNumber+"</td>");
                result.Append("<td>"+acc.AccountBranch+"</td>");
                result.Append("</tr>");
            }
            
            
            
            result.Append("</table>");


            return result.ToString();
        }
    }
}