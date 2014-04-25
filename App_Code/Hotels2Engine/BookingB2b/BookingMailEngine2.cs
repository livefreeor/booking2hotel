using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Hotels2thailand.DataAccess;
using System.Collections;
using System.Xml;
using System.Text;
using Hotels2thailand.Front;
using Hotels2thailand;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Booking;

/// <summary>
/// Summary description for Voucher
/// </summary>
/// 
namespace Hotels2thailand.BookingB2b
{
    public partial class BookingMailEngineB2b
    {
        protected string GetProductItemDeal(int intBookingProductId, string nightTitotal)
        {
            int ItemCount = 1;
            decimal PriceTotal = 0;
            BookingItemDisplay cBookingItem = new BookingItemDisplay();
            StringBuilder ProductItem = new StringBuilder();

            List<object> cBookingItemListDefault = cBookingItem.getBookingItemListByBookingProductId_ToCustomerDisplay(intBookingProductId, 1);
            List<object> cBookingItemListReal = cBookingItem.getBookingItemListByBookingProductId_ToCustomerDisplay(intBookingProductId, BookingLangId);

            List<object> cBookingItemList = null;
            if (cBookingItemListDefault.Count == cBookingItemListReal.Count)
                cBookingItemList = cBookingItemListReal;
            else
                cBookingItemList = cBookingItemListDefault;

            foreach (BookingItemDisplay pItem in cBookingItemList)
            {
                ProductItem.Append("<tr>");

                ProductItem.Append("<td align=\"center\">" + ItemCount + ".</td>");
                ProductItem.Append("<td style=\"padding:10px 0px 10px 0px; margin:0px; font-family:Tahoma;\">");

                ProductItem.Append("<table cellpadding=\"0\" cellspacing=\"0\">");
                ProductItem.Append("<tr><td style=\"margin:0px;padding:0px;font-family:Tahoma; font-size:11px; color:#6d6e71;\">");
                ProductItem.Append(pItem.ConditionTitle);
                ProductItem.Append("</td></tr>");

                ProductItem.Append("</table>");

                ProductItem.Append("</td>");

                ProductItem.Append("<td align=\"center\">" + pItem.Unit + "</td>");
               
                ProductItem.Append("<td align=\"right\">" + pItem.Price.Hotels2Currency() + " <label>" + WordingTranslate(this.BookingLangId, 1) + "</label></td>");
                ProductItem.Append("</tr>");
                ProductItem.Append("<tr><td colspan=\"5\" style=\"background-color:#c8c9ca; line-height:1px; height:1px;  padding:0px; \" height=\"1\"></td></tr>");


                ItemCount = ItemCount + 1;
                PriceTotal = PriceTotal + pItem.Price;
            }
            ProductItem.Append("<tr>");
            ProductItem.Append("<td colspan=\"3\" align= \"right\" style=\" font-size:16px;padding:5px 0px 5px 0px; font-weight:bold;\">" + WordingTranslate(this.BookingLangId, 2) + "</td> ");
            ProductItem.Append("<td colspan=\"2\"  align= \"right\" style=\" font-size:16px;font-family:Tahoma;\">" + PriceTotal.Hotels2Currency() + " " + WordingTranslate(this.BookingLangId, 1) + "</td>");
            ProductItem.Append("</tr>");



            return ProductItem.ToString();
        }
        protected string GetProductItemHOtel(int intBookingProductId, string nightTitotal)
        {
            int ItemCount = 1;
            decimal PriceTotal = 0;
            BookingItemDisplay cBookingItem = new BookingItemDisplay();
            StringBuilder ProductItem = new StringBuilder();

            List<object> cBookingItemListDefault = cBookingItem.getBookingItemListByBookingProductId_ToCustomerDisplay(intBookingProductId, 1);
            List<object> cBookingItemListReal = cBookingItem.getBookingItemListByBookingProductId_ToCustomerDisplay(intBookingProductId, BookingLangId);

            List<object> cBookingItemList = null;
            if (cBookingItemListDefault.Count == cBookingItemListReal.Count)
                cBookingItemList = cBookingItemListReal;
            else
                cBookingItemList = cBookingItemListDefault;

            foreach (BookingItemDisplay pItem in cBookingItemList)
            {
                ProductItem.Append("<tr>");

                ProductItem.Append("<td align=\"center\">" + ItemCount + ".</td>");
                ProductItem.Append("<td style=\"padding:10px 0px 10px 0px; margin:0px; font-family:Tahoma;\">");

                ProductItem.Append("<table cellpadding=\"0\" cellspacing=\"0\">");
                ProductItem.Append("<tr><td style=\"margin:0px;padding:0px;font-family:Tahoma; font-size:11px; color:#6d6e71;\">");
                ProductItem.Append(Hotels2String.GetOptionTitle(pItem.OptionTitle, pItem.BookingOptionTitle));
                ProductItem.Append("</td></tr>");
               
                if (pItem.OptionCAtID == 38)
                {

                    if (pItem.IsExtraNet && pItem.NumAdult.HasValue)
                    {
                       
                        ProductItem.Append("<tr><td style=\"margin:0px;padding:0px;font-family:Tahoma; font-size:11px; color:#6d6e71\">" );
                        ProductItem.Append("-&nbsp;" + pItem.ConditionTitle + Hotels2String.AppendConditionDetailExtraNet((byte)pItem.NumAdult, pItem.BreakfastBookingItem));
                        ProductItem.Append("</td></tr>");
                        
                    }
                    else
                    {
                        ProductItem.Append("<tr><td style=\"margin:0px;padding:0px;font-family:Tahoma; font-size:11px; color:#6d6e71\">");
                        if (pItem.NumAdult.HasValue && pItem.NumChild.HasValue)
                        {
                            if (pItem.NumAdult > 0)
                                ProductItem.Append("&nbsp;(For " + pItem.NumAdult + " Adult )&nbsp;");
                               
                        }
                        else
                        {
                            if (pItem.Condition_NumAdult > 0)
                                ProductItem.Append("&nbsp;(For " + pItem.Condition_NumAdult + " Adult )&nbsp;");
                               
                        }

                        if (pItem.BreakfastBookingItem > 0)
                            ProductItem.Append("<strong>(&nbsp;Breakfast included&nbsp; " + pItem.BreakfastBookingItem + " &nbsp;Pax &nbsp;)</strong>");
                        else
                            ProductItem.Append("<strong>(Room Only)</strong>");

                        ProductItem.Append("</td></tr>");
                    }


                }
                

                if (pItem.PromotionID.HasValue && pItem.PromotionCat.HasValue && !string.IsNullOrEmpty(pItem.PromotionDetail))
                {
                    ProductItem.Append("<tr><td style=\"margin:0px;padding:0px;font-family:Tahoma; font-size:11px;\">");
                    ProductItem.Append("<span style=\"color:#2b2b2b; font-weight:bold; display:block;\">");
                     ProductItem.Append("<img src=\"http://www.hotels2thailand.com/images_mail/hot.png\"  alt=\"\" />&nbsp;Special offer</span>");
                    ProductItem.Append("<span style=\"color:#d22315; font-size:11px;display:block; font-weight:bold; font-style:italic;\">");
                    ProductItem.Append(Hotels2XMLContent.Hotels2XMlReaderPomotionDetail(pItem.PromotionDetail));
                    ProductItem.Append("</td></tr>");
                    
                }
                ProductItem.Append("</table>");
                
                ProductItem.Append("</td>");
               
                ProductItem.Append("<td align=\"center\">" + pItem.Unit + "</td>");
                ProductItem.Append("<td align=\"center\">" + nightTitotal + "</td>");
                ProductItem.Append("<td align=\"right\">" + pItem.Price.Hotels2Currency() + " <label>" + WordingTranslate(this.BookingLangId, 1) + "</label></td>");
                ProductItem.Append("</tr>");
                 ProductItem.Append("<tr><td colspan=\"5\" style=\"background-color:#c8c9ca; line-height:1px; height:1px;  padding:0px; \" height=\"1\"></td></tr>");
                

                ItemCount = ItemCount + 1;
                PriceTotal = PriceTotal + pItem.Price;
            }
            ProductItem.Append("<tr>");
            ProductItem.Append("<td colspan=\"3\" align= \"right\" style=\" font-size:16px;padding:5px 0px 5px 0px; font-weight:bold;\">" + WordingTranslate (this.BookingLangId,2)+ "</td> ");
            ProductItem.Append("<td colspan=\"2\"  align= \"right\" style=\" font-size:16px;font-family:Tahoma;\">" + PriceTotal.Hotels2Currency() + " " + WordingTranslate(this.BookingLangId, 1) + "</td>");
            ProductItem.Append("</tr>");



            return ProductItem.ToString();
        }

        protected string GetProductItem_Golf(int intBookingProductId, string nightTitotal)
        {
            int ItemCount = 1;
            decimal PriceTotal = 0;
            BookingItemDisplay cBookingItem = new BookingItemDisplay();
            StringBuilder ProductItem = new StringBuilder();
            List<object> cBookingItemListDefault = cBookingItem.getBookingItemListByBookingProductId_ToCustomerDisplay(intBookingProductId, 1);
            List<object> cBookingItemListReal = cBookingItem.getBookingItemListByBookingProductId_ToCustomerDisplay(intBookingProductId, BookingLangId);

            List<object> cBookingItemList = null;
            if (cBookingItemListDefault.Count == cBookingItemListReal.Count)
                cBookingItemList = cBookingItemListReal;
            else
                cBookingItemList = cBookingItemListDefault;



            foreach (BookingItemDisplay pItem in cBookingItemList)
            {


                ProductItem.Append("<tr>");

                ProductItem.Append("<td align=\"center\">" + ItemCount + ".</td>");
                ProductItem.Append("<td style=\"padding:10px 0px 10px 0px; margin:0px; font-family:Tahoma;\">");

                ProductItem.Append(Hotels2String.GetOptionTitle(pItem.OptionTitle, pItem.BookingOptionTitle));

                ProductItem.Append("</td>");

                ProductItem.Append("<td align=\"center\">" + pItem.Unit + "</td>");

                ProductItem.Append("<td align=\"right\">" + pItem.Price.Hotels2Currency() + " <label>" + WordingTranslate(this.BookingLangId, 1) + "</label></td>");
                ProductItem.Append("</tr>");
                ProductItem.Append("<tr><td colspan=\"4\" style=\"background-color:#c8c9ca; line-height:1px; height:1px;  padding:0px; \" height=\"1\"></td></tr>");


                ItemCount = ItemCount + 1;
                PriceTotal = PriceTotal + pItem.Price;
            }
            ProductItem.Append("<tr>");
            ProductItem.Append("<td colspan=\"2\" align= \"right\" style=\" font-size:16px;padding:5px 0px 5px 0px; font-weight:bold;\">" + WordingTranslate(this.BookingLangId, 2) + "</td> ");
            ProductItem.Append("<td colspan=\"2\"  align= \"right\" style=\" font-size:16px;font-family:Tahoma;\">" + PriceTotal.Hotels2Currency() + " " + WordingTranslate(this.BookingLangId, 1) + "</td>");
            ProductItem.Append("</tr>");

            return ProductItem.ToString();
        }

        protected string GetProductItem_Spa(int intBookingProductId, string nightTitotal)
        {
            int ItemCount = 1;
            decimal PriceTotal = 0;
            BookingItemDisplay cBookingItem = new BookingItemDisplay();
            StringBuilder ProductItem = new StringBuilder();

            List<object> cBookingItemListDefault = cBookingItem.getBookingItemListByBookingProductId_ToCustomerDisplay(intBookingProductId, 1);
            List<object> cBookingItemListReal = cBookingItem.getBookingItemListByBookingProductId_ToCustomerDisplay(intBookingProductId, BookingLangId);

            List<object> cBookingItemList = null;
            if (cBookingItemListDefault.Count == cBookingItemListReal.Count)
                cBookingItemList = cBookingItemListReal;
            else
                cBookingItemList = cBookingItemListDefault;

            foreach (BookingItemDisplay pItem in cBookingItemList)
            {
            

                ProductItem.Append("<tr>");

                ProductItem.Append("<td align=\"center\">" + ItemCount + ".</td>");
                ProductItem.Append("<td style=\"padding:10px 0px 10px 0px; margin:0px; font-family:Tahoma;\">");

                ProductItem.Append(Hotels2String.GetOptionTitle(pItem.OptionTitle, pItem.BookingOptionTitle));
                
                ProductItem.Append("</td>");
               
                ProductItem.Append("<td align=\"center\">" + pItem.Unit + "</td>");
               
                ProductItem.Append("<td align=\"right\">" + pItem.Price.Hotels2Currency() + " <label>" + WordingTranslate(this.BookingLangId, 1) + "</label></td>");
                ProductItem.Append("</tr>");
                 ProductItem.Append("<tr><td colspan=\"4\" style=\"background-color:#c8c9ca; line-height:1px; height:1px;  padding:0px; \" height=\"1\"></td></tr>");
                

                ItemCount = ItemCount + 1;
                PriceTotal = PriceTotal + pItem.Price;
            }
                ProductItem.Append("<tr>");
                ProductItem.Append("<td colspan=\"2\" align= \"right\" style=\" font-size:16px;padding:5px 0px 5px 0px; font-weight:bold;\">" + WordingTranslate (this.BookingLangId,2)+ "</td> ");
                ProductItem.Append("<td colspan=\"2\"  align= \"right\" style=\" font-size:16px;font-family:Tahoma;\">" + PriceTotal.Hotels2Currency() + " " + WordingTranslate(this.BookingLangId, 1) + "</td>");
                ProductItem.Append("</tr>");

            return ProductItem.ToString();
        }

        protected string GetProductItem_show_day_water(int intBookingProductId, string nightTitotal)
        {
            int ItemCount = 1;
            decimal PriceTotal = 0;
            BookingItemDisplay cBookingItem = new BookingItemDisplay();
            StringBuilder ProductItem = new StringBuilder();

            List<object> cBookingItemListDefault = cBookingItem.getBookingItemListByBookingProductId_ToCustomerDisplay(intBookingProductId, 1);
            List<object> cBookingItemListReal = cBookingItem.getBookingItemListByBookingProductId_ToCustomerDisplay(intBookingProductId, BookingLangId);

            List<object> cBookingItemList = null;
            if (cBookingItemListDefault.Count == cBookingItemListReal.Count)
                cBookingItemList = cBookingItemListReal;
            else
                cBookingItemList = cBookingItemListDefault;

            foreach (BookingItemDisplay pItem in cBookingItemList)
            {
                ProductItem.Append("<tr>");

                ProductItem.Append("<td align=\"center\">" + ItemCount + ".</td>");
                ProductItem.Append("<td style=\"padding:10px 0px 10px 0px; margin:0px; font-family:Tahoma;\">");

                ProductItem.Append(Hotels2String.GetOptionTitle(pItem.OptionTitle, pItem.BookingOptionTitle) + "<br/>");
                //ProductItem.Append(pItem.ConditionTitle);

                if (pItem.NumAdult.HasValue && pItem.NumChild.HasValue)
                {
                    if (pItem.NumAdult > 0 && pItem.NumChild == 0)
                        ProductItem.Append("&nbsp;(For " + pItem.NumAdult + " Adult )&nbsp;");
                    if (pItem.NumChild > 0 && pItem.NumAdult == 0)
                        ProductItem.Append("&nbsp;(For " + pItem.NumChild + " Child )&nbsp;");
                    if (pItem.NumChild > 0 && pItem.NumAdult > 0)
                        ProductItem.Append("&nbsp;(For " + pItem.NumAdult + " Adult or " + pItem.NumChild + " Child)&nbsp;");
                }
                else
                {
                    if (pItem.Condition_NumAdult > 0 && pItem.Condition_NumChild == 0)
                        ProductItem.Append("&nbsp;(For " + pItem.Condition_NumAdult + " Adult )&nbsp;");
                    if (pItem.Condition_NumChild > 0 && pItem.Condition_NumAdult == 0)
                        ProductItem.Append("&nbsp;(For " + pItem.Condition_NumChild + " Child)&nbsp;");
                    if (pItem.Condition_NumChild > 0 && pItem.Condition_NumAdult > 0)
                        ProductItem.Append("&nbsp;(For " + pItem.Condition_NumChild + " Adult or " + pItem.Condition_NumAdult + "Child)&nbsp;");
                }

                ProductItem.Append("</td>");

                ProductItem.Append("<td align=\"center\">" + DateTimeCheck_TimeOnly(pItem.OptionTimeStart) + "-" + DateTimeCheck_TimeOnly(pItem.OptionTimeEnd) + "</td>");
                ProductItem.Append("<td align=\"center\">" + pItem.Unit + "</td>");
               
                ProductItem.Append("<td align=\"right\">" + pItem.Price.Hotels2Currency() + " <label>" + WordingTranslate(this.BookingLangId, 1) + "</label></td>");
                ProductItem.Append("</tr>");
                ProductItem.Append("<tr><td colspan=\"5\" style=\"background-color:#c8c9ca; line-height:1px; height:1px;  padding:0px; \" height=\"1\"></td></tr>");


                ItemCount = ItemCount + 1;
                PriceTotal = PriceTotal + pItem.Price;
            }
            ProductItem.Append("<tr>");
            ProductItem.Append("<td colspan=\"3\" align= \"right\" style=\" font-size:16px;padding:5px 0px 5px 0px; font-weight:bold;\">" + WordingTranslate(this.BookingLangId, 2) + "</td> ");
            ProductItem.Append("<td colspan=\"2\"  align= \"right\" style=\" font-size:16px;font-family:Tahoma;\">" + PriceTotal.Hotels2Currency() + " " + WordingTranslate(this.BookingLangId, 1) + "</td>");
            ProductItem.Append("</tr>");

            return ProductItem.ToString();
        }

        protected string GetProductItem_health(int intBookingProductId, string nightTitotal)
        {
            int ItemCount = 1;
            decimal PriceTotal = 0;
            BookingItemDisplay cBookingItem = new BookingItemDisplay();
            StringBuilder ProductItem = new StringBuilder();
            BookingGuest_Engine cGuest = new BookingGuest_Engine();


            List<object> cBookingItemListDefault = cBookingItem.getBookingItemListByBookingProductId_ToCustomerDisplay(intBookingProductId, 1);
            List<object> cBookingItemListReal = cBookingItem.getBookingItemListByBookingProductId_ToCustomerDisplay(intBookingProductId, BookingLangId);

            List<object> cBookingItemList = null;
            if (cBookingItemListDefault.Count == cBookingItemListReal.Count)
                cBookingItemList = cBookingItemListReal;
            else
                cBookingItemList = cBookingItemListDefault;

            foreach (BookingItemDisplay pItem in cBookingItemList)
            {
                cGuest = cGuest.getGuestByBookingItem(intBookingProductId, pItem.BookingItemID);
                ProductItem.Append("<tr align=\"center\" style=\"font-family:Tahoma, Geneva, sans-serif; font-size:14px; line-height:29px;\">");
                ProductItem.Append("<td  style=\"border-bottom:solid 1px #d1d2d4; border-right:solid 1px #d1d2d4;\">" + ItemCount + ".</td>");
                ProductItem.Append("<td style=\"border-bottom:solid 1px #d1d2d4; border-right:solid 1px #d1d2d4;\">" + cGuest.GuestName + "");
                
                ProductItem.Append("</td>");
                ProductItem.Append("<td  style=\"border-bottom:solid 1px #d1d2d4; border-right:solid 1px #d1d2d4;\">" + DateTimeCheck(cGuest.DateBirth) + "</td>");
                ProductItem.Append("<td style=\"border-bottom:solid 1px #d1d2d4; border-right:solid 1px #d1d2d4;\">" + cGuest.PassportId + "</td>");
                ProductItem.Append("<td style=\"border-bottom:solid 1px #d1d2d4; border-right:solid 1px #d1d2d4;\">" + Hotels2String.GetOptionTitle(pItem.OptionTitle, pItem.BookingOptionTitle) + "</td>");
                ProductItem.Append("<td  style=\"border-bottom:solid 1px #d1d2d4;\">" + pItem.Price.Hotels2Currency() + " Baht</td>");
                ProductItem.Append("</tr>");


                ItemCount = ItemCount + 1;
                PriceTotal = PriceTotal + pItem.Price;
            }
            ProductItem.Append("<tr>");
            ProductItem.Append("<td colspan=\"5\" style=\"padding-right:15px; text-align:right; line-height:28px; border-right:solid 1px #d1d2d4; font-size:15px; font-weight:bold; color:#007ab6; font-family:Tahoma, Geneva, sans-serif;\">Total</td> ");
            ProductItem.Append("<td colspan=\"1\" style=\"text-align:center; line-height:28px;	color:#00a651; font-size:15px; font-weight:bold; border-bottom:solid 1px #d1d2d4; font-family:Tahoma, Geneva, sans-serif; background:#f2f2f2\"> " + PriceTotal.Hotels2Currency() + " Baht</td>");
            ProductItem.Append("</tr>");



            return ProductItem.ToString();
        }

        protected string GetProductItem_transfer(int intBookingProductId, string nightTitotal)
        {
            int ItemCount = 1;
            decimal PriceTotal = 0;
            BookingItemDisplay cBookingItem = new BookingItemDisplay();
            StringBuilder ProductItem = new StringBuilder();


            List<object> cBookingItemListDefault = cBookingItem.getBookingItemListByBookingProductId_ToCustomerDisplay(intBookingProductId, 1);
            List<object> cBookingItemListReal = cBookingItem.getBookingItemListByBookingProductId_ToCustomerDisplay(intBookingProductId, BookingLangId);

            List<object> cBookingItemList = null;
            if (cBookingItemListDefault.Count == cBookingItemListReal.Count)
                cBookingItemList = cBookingItemListReal;
            else
                cBookingItemList = cBookingItemListDefault;


            foreach (BookingItemDisplay pItem in cBookingItemList)
            {
                ProductItem.Append("<tr>");

                ProductItem.Append("<td align=\"center\">" + ItemCount + ".</td>");
                ProductItem.Append("<td style=\"padding:10px 0px 10px 0px; margin:0px; font-family:Tahoma;\">");

                ProductItem.Append(Hotels2String.GetOptionTitle(pItem.OptionTitle, pItem.BookingOptionTitle));


                ProductItem.Append("</td>");

                
                ProductItem.Append("<td align=\"center\">" + pItem.Unit + "</td>");

                ProductItem.Append("<td align=\"right\">" + pItem.Price.Hotels2Currency() + " <label>" + WordingTranslate(this.BookingLangId, 1) + "</label></td>");
                ProductItem.Append("</tr>");
                ProductItem.Append("<tr><td colspan=\"4\" style=\"background-color:#c8c9ca; line-height:1px; height:1px;  padding:0px; \" height=\"1\"></td></tr>");


                ItemCount = ItemCount + 1;
                PriceTotal = PriceTotal + pItem.Price;
            }
            ProductItem.Append("<tr>");
            ProductItem.Append("<td colspan=\"2\" align= \"right\" style=\" font-size:16px;padding:5px 0px 5px 0px; font-weight:bold;\">" + WordingTranslate(this.BookingLangId, 2) + "</td> ");
            ProductItem.Append("<td colspan=\"2\"  align= \"right\" style=\" font-size:16px;font-family:Tahoma;\">" + PriceTotal.Hotels2Currency() + " " + WordingTranslate(this.BookingLangId, 1) + "</td>");
            ProductItem.Append("</tr>");

            return ProductItem.ToString();
        }
    }
}