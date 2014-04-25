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


/// <summary>
/// Summary description for Voucher
/// </summary>
/// 
namespace Hotels2thailand.Booking
{
    public partial class BookingBooking_PrintEngineExtranet 
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
            List<object> cBookingItemList = cBookingItem.getBookingItemListByBookingProductIdAndCatId_ToCustomerDisplay(intBookingProductId, ExtraTptionNotIn);
            int Count = 1;
            decimal Subtotal = 0;
            if (cBookingItemList.Count > 0)
            {
                TiemResult.Append("<table cellpadding=\"0\" cellspacing=\"2\" class=\"option\">");
                TiemResult.Append("<tr>");
                TiemResult.Append("<th width=\"20\">No.</th>");
                TiemResult.Append("<th style=\"text-align:left; padding-left:215px;\" >Extra Option</th>");
                TiemResult.Append("<th width=\"88\">Quantity</th>");
                TiemResult.Append("<th width=\"140\">Price</th> ");
                TiemResult.Append("</tr>");
                foreach (BookingItemDisplay items in cBookingItemList)
                {


                    TiemResult.Append("<tr>");
                    TiemResult.Append("<td class=\"info\" >" + Count + ".</td>");
                    TiemResult.Append("<td>" + items.OptionTitle + "</td>");
                    TiemResult.Append("<td align=\"center\">" + items.Unit + "</td>");
                    TiemResult.Append("<td>" + items.PriceSupplier.Hotels2Currency() + "</td>");
                    TiemResult.Append("</tr>");


                    Count = Count + 1;
                    Subtotal = Subtotal + items.PriceSupplier;
                }

                TiemResult.Append("<tr>");

                TiemResult.Append("<td class=\"sub_total\" colspan=\"3\">Sub Total </td>");
                TiemResult.Append("<td class=\"price\">" + Subtotal.Hotels2Currency() + "</td>");
                TiemResult.Append("</tr>");
                TiemResult.Append("</table>");
                
            }
            return TiemResult.ToString();
        }
        
        private string GetProductItem_hotel(int intBookingProductId, string NightREsult, byte LangId)
        {
            StringBuilder TiemResult = new StringBuilder();
            BookingItemDisplay cBookingItem = new BookingItemDisplay();
            PromotionContent cProContent = new PromotionContent();
            string ExtraTptionNotIn = "NOT IN (39,40,43,44,47)";
            List<object> cBookingItemList = cBookingItem.getBookingItemListByBookingProductIdAndCatId_ToCustomerDisplay(intBookingProductId, ExtraTptionNotIn);
            int Count = 1;
            decimal Subtotal = 0;
            foreach (BookingItemDisplay items in cBookingItemList)
            {
                TiemResult.Append("<tr>");

                TiemResult.Append("<td class=\"info\" >" + Count + ".</td>");



                if (items.IsExtraNet && items.NumAdult.HasValue)
                {
                    TiemResult.Append("<td>" + items.OptionTitle + "<br/>");

                    TiemResult.Append(items.ConditionTitle + Hotels2String.AppendConditionDetailExtraNet((byte)items.NumAdult, items.BreakfastBookingItem));
                    TiemResult.Append("");
                    TiemResult.Append("");
                    TiemResult.Append("");
                }
                else
                {
                    if (items.BreakfastBookingItem > 0)
                        TiemResult.Append("<td>" + items.OptionTitle + "&nbsp;<span style=\"font-weight:bold\">(&nbsp;Breakfast included&nbsp; " + items.BreakfastBookingItem + " &nbsp;Pax &nbsp;)</span>");
                    else
                        TiemResult.Append("<td>" + items.OptionTitle + "&nbsp;<span style=\"font-weight:bold\">(&nbsp;No breakfast&nbsp;)</span>");
                }


                //if (items.BreakfastBookingItem > 0)
                //    TiemResult.Append("<td>" + items.OptionTitle + "&nbsp;<span style=\"font-weight:bold\">(&nbsp;Breakfast included&nbsp; " + items.BreakfastBookingItem + " &nbsp;Pax &nbsp;)</span>");
                //else
                //    TiemResult.Append("<td>" + items.OptionTitle + "&nbsp;<span style=\"font-weight:bold\">(&nbsp;No breakfast&nbsp;)</span>");

                if (items.PromotionID.HasValue && items.PromotionCat.HasValue && !string.IsNullOrEmpty(items.PromotionDetail))
                {
                    if ((byte)items.PromotionCat == 1)
                    {
                        TiemResult.Append(Hotels2XMLContent.Hotels2XMlReaderPomotionDetail(items.PromotionDetail));
                    }

                }
                TiemResult.Append("</td>");

                //TiemResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">" + items.Breakfast + "</td>");

                TiemResult.Append("<td align=\"center\">" + NightREsult + "</td>");

                TiemResult.Append("<td align=\"center\">" + items.Unit + "</td>");

                TiemResult.Append("<td>" + items.PriceSupplier.Hotels2Currency() + "</td>");

                TiemResult.Append("</tr>");

                Count = Count + 1;
                Subtotal = Subtotal + items.PriceSupplier;
            }
            TiemResult.Append("<tr>");
            TiemResult.Append("<td class=\"sub_total\" colspan=\"4\">Sub Total </td>");
            TiemResult.Append("<td class=\"price\">" + Subtotal.Hotels2Currency() + "</td>");
            TiemResult.Append("</tr>");

            return TiemResult.ToString();
        }

        //============================================================================================================================


    }
}