using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Account;
using Hotels2thailand.Booking;
using Hotels2thailand;

public partial class ajax_account_settle_list : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Page.IsPostBack)
        {

            Response.Write(SettleList());
            Response.End();
            
        }
    }

    public string SettleList()
    {
        StringBuilder result = new StringBuilder();
        BookingPaymentDisplay cBookingPayment = new BookingPaymentDisplay();
        try
        {
            string ListType = Request.QueryString["type"];
            string PayMentList = Request.Form["txt_payment_id"].ToString().Trim().Replace("\r\n", ",");
            AccountSettleDetail cAccountSettle = new AccountSettleDetail();
            IList<object> SettleList = cAccountSettle.GetListPaymentListSettled(PayMentList);

            result.Append("<table id=\"settle_list\" cellpadding=\"0\" cellspacing=\"2\">");
            if (ListType == "acc")
                result.Append("<tr style=\"background-color:#3f5d9d;\"><th>Payment id</th><th>Guest</th><th>Product Name</th><th>Check in</th><th>Check out</th><th>Price</th></tr>");
            if (ListType == "bht")
            {
                result.Append("<tr style=\"background-color:#3f5d9d;\"><th>Payment id</th><th>Booking id</th><th>Guest</th><th>Product Name</th><th>Check in</th>");
                result.Append("<th>Check out</th><th>Price</th><th>Cost</th><th>Trans.</th><th>Profit</th><th>%</th></tr>");
            }

            //int intBookingIdTemp = 0;
            int count = 1;
            string bg = string.Empty;

            decimal TotalPrice = 0.0m;
            decimal TotalCost = 0.0m;
            decimal TotalTrans = 0.0m;
            decimal TotalProfit = 0.0m;
            decimal TotalPercent = 0.0m;
            IList<object> iPaymentList = null;
            foreach (AccountSettleDetail settleItem in SettleList)
            {
                //intBookingIdTemp = settleItem.BookingId;
                decimal Cost = 0.0m;

                iPaymentList = cBookingPayment.GetBookingPayment_PaidOnlyByBookingId(settleItem.BookingId);
                int PaymentCount = iPaymentList.Count();
                //if()
                if (PaymentCount == 1 && settleItem.PriceTotal == settleItem.PaidTotal)
                    Cost = settleItem.Cost;
                else
                {
                    int index = 0;
                    foreach (BookingPaymentDisplay item in iPaymentList)
                    {
                        if (item.PaymentId == settleItem.BookingPaymentID)
                        {
                            if (PaymentCount == (index + 1) && settleItem.PriceTotal == settleItem.PaidTotal)
                            {
                                Cost = settleItem.Cost;
                            }

                        }

                        
                        index = index + 1;
                    }
                }

                decimal Transaction = settleItem.Amount * (decimal)0.02675;
                decimal Profit = settleItem.Amount - Cost - Transaction;

                decimal Percent = (Profit * 100) / settleItem.Amount;

                if (count % 2 == 0)
                    bg = "#eceff5";
                else
                    bg = "#ffffff";

                result.Append("<tr style=\"background-color:" + bg + "\" id=\"" + settleItem.BookingPaymentID + "\">");

                result.Append("<td>" + settleItem.BookingPaymentID + "<input type=\"checkbox\" checked=\"checked\" value=\"" + settleItem.BookingPaymentID + "\" name=\"checkbox_checked\" style=\"display:none;\" /></td>");

                if (ListType == "bht")
                {
                    result.Append("<td>" + settleItem.BookingId + "</td>");
                }

                result.Append("<td style=\"text-align:left;padding-left:5px;\" >" + settleItem.BookingName + "</td>");
                result.Append("<td style=\"text-align:left;padding-left:5px;\" >" + settleItem.Producttitle + "</td>");
                result.Append("<td>" + settleItem.CheckIn.ToString("MMM dd, yy") + "</td>");
                result.Append("<td>" + settleItem.CheckOut.ToString("MMM dd, yy") + "</td>");
                result.Append("<td><a href=\"\" id=\"price_" + settleItem.BookingPaymentID + "\"  onclick=\"changePrice('" + settleItem.BookingPaymentID + "');return false;\" >" + settleItem.Amount.ToString("#,#.00") + "</a>");
                result.Append("<input type=\"text\" class=\"textBoxStyle_color\" style=\"display:none;width:60px;\" id=\"txt_price_" + settleItem.BookingPaymentID + "\" value=\"" + settleItem.Amount.ToString("#,#.00") + "\" /></td>");

                if (ListType == "bht")
                {
                    result.Append("<td><a href=\"\" id=\"cost_" + settleItem.BookingPaymentID + "\"  onclick=\"changeCost('" + settleItem.BookingPaymentID + "');return false;\" >" + Cost.ToString("#,#.00") + "</a>");

                    result.Append("<input type=\"text\" class=\"textBoxStyle_color\" style=\"display:none;width:60px;\" id=\"txt_cost_" + settleItem.BookingPaymentID + "\" value=\"" + settleItem.Amount.ToString("#,#.00") + "\"  /></td>");


                    result.Append("<td><label id=\"trans_" + settleItem.BookingPaymentID + "\">" + Transaction.ToString("#,#.00") + "</label></td>");
                    result.Append("<td><label id=\"profit_" + settleItem.BookingPaymentID + "\">" + Profit.ToString("#,#.00") + "</td>");
                    result.Append("<td><label id=\"percent_" + settleItem.BookingPaymentID + "\">" + Percent.ToString("#,#.00") + "</td>");
                    
                }

                result.Append("</tr>");

                count = count + 1;
               

                TotalPrice = TotalPrice + settleItem.Amount;
                TotalCost = TotalCost + Cost;
                TotalTrans = TotalTrans + Transaction;
                TotalProfit = TotalProfit + Profit;
                
            }
            TotalPercent = (TotalProfit * 100) / TotalPrice;
            result.Append("<tr style=\"background-color:#ffffff\">");
            if (ListType == "acc")
            {
                result.Append("<td colspan=\"5\" style=\"text-align:right;font-weight:bold;\">Total&nbsp;</td>");
                result.Append("<td style=\"font-weight:bold;\"><label id=\"total_price\">" + TotalPrice.ToString("#,#.00") + "</label></td>");
           
            }

            if (ListType == "bht")
            {
                result.Append("<td colspan=\"6\" style=\"text-align:right;font-weight:bold;\">Total&nbsp;</td>");
                result.Append("<td style=\"font-weight:bold;\"><label id=\"total_price\">" + TotalPrice.ToString("#,#.00") + "</label></td>");
                result.Append("<td style=\"font-weight:bold;\"><label id=\"total_cost\">" + TotalCost.ToString("#,#.00") + "</label></td>");
                result.Append("<td style=\"font-weight:bold;\"><label id=\"total_trans\">" + TotalTrans.ToString("#,#.00") + "</label></td>");
                result.Append("<td style=\"font-weight:bold;\"><label id=\"total_profit\">" + TotalProfit.ToString("#,#.00") + "</label></td>");
                result.Append("<td style=\"font-weight:bold;\"><label id=\"total_percent\">" + TotalPercent.ToString("#,#.00") + "</label></td>");
            }
            result.Append("</tr>");

            result.Append("</table>");
        }
        catch (Exception ex)
        {
            Response.Write("error: " + ex.Message );
            Response.End();
        }


        return result.ToString();
        
    }



    
    
}