using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Booking;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Production;
using Hotels2thailand;


namespace Hotels2thailand.UI
{
    public partial class admin_booking_booking_detail : Hotels2BasePageExtra
    {
        //booking_id
        public string qBookingId
        {
            get
            {
                return Request.QueryString["bid"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                
                //dropStatus
                Status cStatus = new Status();
                dropStatusBooking.DataSource = cStatus.GetStatusByCatId(2);
                dropStatusBooking.DataTextField = "Value";
                dropStatusBooking.DataValueField = "Key";
                dropStatusBooking.DataBind();

                //DataBindBookingDetail 
                

                BookingdetailDisplay cBookingDetail = new BookingdetailDisplay();
                cBookingDetail = cBookingDetail.GetBookingDetailListByBookingId(int.Parse(this.qBookingId));
                string prefixTitle = cBookingDetail.PrefixTitle;
                if (cBookingDetail.PrefixId == 1)
                    prefixTitle = "";
                ltBookingName.Text = prefixTitle + " " + cBookingDetail.FullName;
                ltEmail.Text = cBookingDetail.Email;
                tPhone.Text = cBookingDetail.BookingPhone;
                ltMobile.Text = cBookingDetail.BookingMobile;

                //ltCountry.Text = cBookingDetail.CountryTitle;

                string style = "color:#6da71e";
                if (cBookingDetail.CountryId != cBookingDetail.CountryIdBytrackIp)
                    style = "color:#ff2020";

                ltCountry.Text = cBookingDetail.CountryTitle + "&nbsp;<label style=\"font-weight:bold;" + style + "\">[ " + cBookingDetail.CountryTitleBytrackIP + " ] <a href=\"http://whois.domaintools.com/" + cBookingDetail.RefIP + "\" target=\"_Blank\" > " + cBookingDetail.RefIP + "</a><label>";

                if (cBookingDetail.Status)
                {

                    ltrtxtremove.Text = "Retrieve Booking:";
                    lnCloseBooking.Text = "Retrieve Booking";
                    
                }
                else
                {
                    ltrtxtremove.Text = "Remove Booking:";

                    lnCloseBooking.Text = "Remove Now";
                }
                    

                ltarrF.Text = cBookingDetail.F_arr_No + " - " + cBookingDetail.F_arr_Time;
                ltDepF.Text = cBookingDetail.F_Dep_No + " - " + cBookingDetail.F_Dep_Time;

                BookingItemDisplay cBookingItem = new BookingItemDisplay();

                lItemDetail.Text = cBookingItem.GetBookingTransferDetail(int.Parse(this.qBookingId));

                ltBookingRecieved.Text = cBookingDetail.DateBookingREceive.ToString("dd MMM yyyy hh:mm tt");
                dropStatusBooking.SelectedValue = cBookingDetail.StatusId.ToString();

                hdStatus.Value = cBookingDetail.StatusId.ToString();
                hdStatusBooking.Value = cBookingDetail.Status.ToString();

                string strStatus = "<label style=\"color:#3a7b05;font-weight:bold;\">Active</label>";
                if (cBookingDetail.Status)
                    strStatus = "<label style=\"color:#ab0b03;font-weight:bold;\">Inactive</label>";
                ltStatus.Text = strStatus;

                string AppendQueryString = string.Empty;
                if (!string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qSupplierId))
                    AppendQueryString = "&pid=" + this.qProductId + "&supid=" + this.qSupplierId;

                lkvoucherPrint.NavigateUrl = "voucher_admin_preview.aspx?bpid=" + cBookingDetail.BookingProductId + AppendQueryString;
                lkEditBookingDetail.NavigateUrl = "javascript:popup('popup_edit_booking_detail.aspx?bid=" + cBookingDetail.BookingId + AppendQueryString + "',420,600)";
                lkEditProductDetail.NavigateUrl = "javascript:popup('popup_edit_product_detail.aspx?bid= " + this.qBookingId + "&bpid=" + cBookingDetail.BookingProductId + AppendQueryString + "',350,800)";
                //DataBind Product 

                getBookingProductList();

                GVBookingItemDataBound(cBookingDetail.BookingProductId);

              ltPaymentList.Text =   getBookingPayMent();
              ltBookingActivity.Text = getBookingActivity();
              ltConfirmBlock.Text = getBookingDetailButtom();
            }
        }

        public void btnSaveStatus_Onclick(object sender, EventArgs e)
        {
            int intBookingId = int.Parse(this.qBookingId);
            byte bytStatus = byte.Parse(dropStatusBooking.SelectedValue);
            StatusBooking cStatusBooking = new StatusBooking();
            cStatusBooking.UpdateBookingStatus(intBookingId, bytStatus);


            BookingActivityDisplay cBookingActivity = new BookingActivityDisplay();
            cBookingActivity.InsertAutoActivity(BookingActivityType.updateStatus, intBookingId);

            Response.Redirect(Request.Url.ToString());
        }


        public void Updaterequirement(object sender, EventArgs e)
        {
            foreach (GridViewRow GvRow in GVBookingItem.Rows)
            {
                if (GvRow.RowType == DataControlRowType.DataRow)
                {
                    DropDownList GvdropSmoke = GVBookingItem.Rows[GvRow.RowIndex].Cells[0].FindControl("dropSmoke") as DropDownList;
                    DropDownList GvdropRoom = GVBookingItem.Rows[GvRow.RowIndex].Cells[0].FindControl("dropRoom") as DropDownList;
                    DropDownList GvsropFloor = GVBookingItem.Rows[GvRow.RowIndex].Cells[0].FindControl("sropFloor") as DropDownList;
                    TextBox GvtxtComment = GVBookingItem.Rows[GvRow.RowIndex].Cells[0].FindControl("txtComment") as TextBox;
                    Label GvoptionTitle = GVBookingItem.Rows[GvRow.RowIndex].Cells[0].FindControl("optionTitle") as Label;
                    Literal GvHdCAt = GVBookingItem.Rows[GvRow.RowIndex].Cells[0].FindControl("hdCat") as Literal;

                    int intBookingItemId = (int)GVBookingItem.DataKeys[GvRow.RowIndex].Value;

                    BookingRequireMent require = new BookingRequireMent();
                    ArrayList obj = require.GetRequireMentByBooinProductIDAndProductCatSingle(intBookingItemId, byte.Parse(GvHdCAt.Text));
                    if (obj == null)
                    {
                        switch (byte.Parse(GvHdCAt.Text))
                        {
                            case 29:
                                require.InsertNewBookingRequireMentHotel(intBookingItemId, GvtxtComment.Text, byte.Parse(GvdropSmoke.SelectedValue), byte.Parse(GvdropRoom.SelectedValue), byte.Parse(GvsropFloor.SelectedValue));
                                break;
                            case 40:
                                require.InsertNewBookingRequireMentSpa(intBookingItemId, GvtxtComment.Text);
                                break;
                            case 39:
                                require.InsertNewBookingRequireMenthealth(intBookingItemId, GvtxtComment.Text);
                                break;
                            default:
                                require.InsertNewBookingRequireMentGeneral(intBookingItemId, GvtxtComment.Text);
                                break;
                        }
                    }
                    else
                    {
                        int intREquiredID = 0;
                        switch (byte.Parse(GvHdCAt.Text))
                        {
                            case 29:
                                intREquiredID = (int)obj[4];
                                require.UpdateBookingRequireMentHotel(intREquiredID, GvtxtComment.Text, byte.Parse(GvdropSmoke.SelectedValue), byte.Parse(GvdropRoom.SelectedValue), byte.Parse(GvsropFloor.SelectedValue));
                                break;
                            case 40:
                                intREquiredID = (int)obj[1];
                                require.UpdateBookingRequireMentSpa(intREquiredID, GvtxtComment.Text);
                                break;
                            case 39:
                                intREquiredID = (int)obj[1];
                                require.UpdateBookingRequireMenthealth(intREquiredID, GvtxtComment.Text);
                                break;
                            default:
                                intREquiredID = (int)obj[1];
                                require.UpdateBookingRequireMentGeneral(intREquiredID, GvtxtComment.Text);
                                break;
                        }

                    }
                }
            }

            Response.Redirect(Request.Url.ToString());
        }
        public void GVBookingItemDataBound(int intBookingProductID)
        {
            
            BookingProductDisplay cBookingProductID = new BookingProductDisplay();
            cBookingProductID = cBookingProductID.getBookingProductDisplayByBookingProductId(intBookingProductID);

            BookingRequireMent require = new BookingRequireMent();
            try
            {

                GVBookingItem.DataSource = require.GetRequireMentByBooingProductID(intBookingProductID, cBookingProductID.ProductCategory);

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                Response.End();
            }

            GVBookingItem.DataBind();
        }

        public void GVBookingItem_ONrowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //BookingRequireMent require = new BookingRequireMent();
                byte bytProductcat = (byte)DataBinder.Eval(e.Row.DataItem, "ProductCat");
                string bytOptionTitle = string.Empty;

                if (DataBinder.Eval(e.Row.DataItem, "OptionTitle") != null)
                    bytOptionTitle = DataBinder.Eval(e.Row.DataItem, "OptionTitle").ToString();
                else
                    bytOptionTitle = DataBinder.Eval(e.Row.DataItem, "OptionTitleEngDefault").ToString();
                //.ToString();
                int intRequireID = (int)DataBinder.Eval(e.Row.DataItem, "RequirID");
                string strComment = DataBinder.Eval(e.Row.DataItem, "Comment").ToString();

                string smoke = DataBinder.Eval(e.Row.DataItem, "Smoke").ToString();
                string Bed = DataBinder.Eval(e.Row.DataItem, "Bed").ToString();
                string Floor = DataBinder.Eval(e.Row.DataItem, "Floor").ToString();

                DropDownList GvdropSmoke = e.Row.Cells[0].FindControl("dropSmoke") as DropDownList;
                DropDownList GvdropRoom = e.Row.Cells[0].FindControl("dropRoom") as DropDownList;
                DropDownList GvsropFloor = e.Row.Cells[0].FindControl("sropFloor") as DropDownList;
                TextBox GvtxtComment = e.Row.Cells[0].FindControl("txtComment") as TextBox;
                Label GvoptionTitle = e.Row.Cells[0].FindControl("optionTitle") as Label;
                GvoptionTitle.Text = bytOptionTitle;
                //ArrayList obj = null;
                GvdropSmoke.DataSource = BookingRequireMent.getTypeSmoke();
                GvdropSmoke.DataTextField = "Value";
                GvdropSmoke.DataValueField = "Key";
                GvdropSmoke.DataBind();

                GvdropRoom.DataSource = BookingRequireMent.getTypeBed();
                GvdropRoom.DataTextField = "Value";
                GvdropRoom.DataValueField = "Key";
                GvdropRoom.DataBind();

                GvsropFloor.DataSource = BookingRequireMent.getFloor();
                GvsropFloor.DataTextField = "Value";
                GvsropFloor.DataValueField = "Key";
                GvsropFloor.DataBind();

                GvdropSmoke.SelectedValue = "3";
                GvdropRoom.SelectedValue = "3";
                GvsropFloor.SelectedValue = "3";
                switch (bytProductcat)
                {
                    case 29:
                        GvdropSmoke.Visible = true;
                        GvdropRoom.Visible = true;
                        GvsropFloor.Visible = true;
                        GvdropSmoke.SelectedValue = smoke;
                        GvdropRoom.SelectedValue = Bed;
                        GvsropFloor.SelectedValue = Floor;
                        GvtxtComment.Text = strComment;
                        //obj = require.GetRequireMentByBooinProductIDAndProductCatSingle(intRequireID, bytProductcat);
                        break;
                    case 40:
                        GvtxtComment.Text = strComment;
                        //obj = require.GetRequireMentByBooinProductIDAndProductCatSingle(intRequireID, bytProductcat);

                        break;
                    case 39:
                        GvtxtComment.Text = strComment;
                        //obj = require.GetRequireMentByBooinProductIDAndProductCatSingle(intRequireID, bytProductcat);

                        break;
                    default:
                        GvtxtComment.Text = strComment;
                        //obj = require.GetRequireMentByBooinProductIDAndProductCatSingle(intRequireID, bytProductcat);

                        break;
                }

            }
        }

        public void getBookingProductList()
        {
            StringBuilder result = new StringBuilder();
            BookingProductList cBookingProduct = new BookingProductList();
            //SupplierContactPhoneEmail cSupplierContact = new SupplierContactPhoneEmail();
            Status cStatus = new Status();
            

            cBookingProduct = cBookingProduct.getTOP1ProductListShowFirstByBookingId(int.Parse(this.qBookingId));
                if (cBookingProduct.DateTimeCheckIn.HasValue)
                {
                    DateTime dDateCheckIn = (DateTime)cBookingProduct.DateTimeCheckIn;
                    ltCheckIn.Text = dDateCheckIn.ToString("ddd, MMM dd, yyyy");

                }

                if (cBookingProduct.DateTimeCheckOut != null)
                {
                    DateTime dDateCheckOut = (DateTime)cBookingProduct.DateTimeCheckOut;
                    ltCheckOut.Text = dDateCheckOut.ToString("ddd, MMM dd, yyyy");
                }

                ltNumAdult.Text = cBookingProduct.NumAdult.ToString();
                ltNumChild.Text = cBookingProduct.NumChild.ToString();
                int intBookingProductId = cBookingProduct.BookingProductId;

                BookingItemDisplay cBookingItem = new BookingItemDisplay();
                BookingProductDisplay cBookingProductDis = new BookingProductDisplay();

                byte intBookingLang = cBookingProduct.LangId;


                List<object> cBookingItemListDefault = cBookingItem.getBookingItemListByBookingProductId(intBookingProductId, 1);
                List<object> cBookingItemListReal = cBookingItem.getBookingItemListByBookingProductId(intBookingProductId, intBookingLang);

                List<object> cBookingItemList = null;
                if (cBookingItemListDefault.Count == cBookingItemListReal.Count)
                    cBookingItemList = cBookingItemListReal;
                else
                    cBookingItemList = cBookingItemListDefault;

                result.Append("<table cellpadding=\"0\" cellspacing=\"0\"  width=\"100%\"  style=\" font-size:11px;color:#6d6e71; font-weight:normal;\">");
                result.Append("<tr>");
                result.Append("<td align=\"center\" height=\"25\" style=\"width:5%;font-family:Tahoma; height:25px; background-color:#f2ebbd\">No.</td>");
                result.Append("<td align=\"center\" height=\"25\" style=\"width:45%;font-family:Tahoma; height:25px; background-color:#f2ebbd\">Room type</td>");
                result.Append("<td align=\"center\" height=\"25\" style=\"width:15%;font-family:Tahoma; height:25px; background-color:#f2ebbd\">Allot</td>");
                result.Append("<td align=\"center\" height=\"25\" style=\"width:15%;font-family:Tahoma; height:25px; background-color:#f2ebbd\">Quantity</td>");

                result.Append("<td align=\"center\" height=\"25\" style=\"width:20%;font-family:Tahoma; height:25px; background-color:#f2ebbd\">Subtotal</td></tr>");
                result.Append("<tr><td colspan=\"5\" style=\"background-color:#c8c9ca; line-height:1px; height:1px;\" height=\"1\"></td></tr>");
                int count = 0;
                foreach (BookingItemDisplay pItem in cBookingItemList)
                {
                    count = count + 1;
                    result.Append("<tr>");
                    result.Append("<td align=\"center\">" + count + ".</td>");
                    result.Append("<td style=\"padding:10px 0px 10px 0px; margin:0px; font-family:Tahoma;\">");
                    result.Append("<table cellpadding=\"0\" cellspacing=\"0\">");
                    result.Append("<tr>");
                    result.Append("<td style=\"margin:0px;padding:0px;font-family:Tahoma; font-size:11px; color:#6d6e71;\">");
                    result.Append(Hotels2String.GetOptionTitle(pItem.OptionTitle, pItem.BookingOptionTitle));
                    
                    
                    result.Append("</td>");
                    result.Append("</tr>");

                    bool bolIsadult = true;
                    if (pItem.ConditionIsAdult.HasValue)
                    {
                        bolIsadult = (bool)pItem.ConditionIsAdult;
                    }


                    result.Append("<tr><td style=\"margin:0px;padding:0px;font-family:Tahoma; font-size:11px; color:#6d6e71\"> -" + Hotels2String.AppendConditionDetailExtraNet((byte)pItem.NumAdult, pItem.BreakfastBookingItem, pItem.OptionCAtID, pItem.ConditionTitle, bolIsadult) + "</td></tr>");
                    
                    //if (pItem.OptionCAtID == 38)
                    //{
                        
                    //    result.Append("<tr><td style=\"margin:0px;padding:0px;font-family:Tahoma; font-size:11px; color:#6d6e71\"> -" + pItem.ConditionTitle + Hotels2String.AppendConditionDetailExtraNet((byte)pItem.NumAdult, pItem.BreakfastBookingItem) + "</td></tr>");
                    //}
                    //else
                    //{
                    //    result.Append("<tr><td style=\"margin:0px;padding:0px;font-family:Tahoma; font-size:11px; color:#6d6e71\">"+ Hotels2String.AppendConditionDetailExtraNet((byte)pItem.NumAdult, pItem.BreakfastBookingItem)+ "</td></tr>");
                    //}
                    
                   
                    if (pItem.PromotionID.HasValue && !string.IsNullOrEmpty(pItem.PromotionDetail))
                    {
                        result.Append("<tr><td style=\"margin:0px;padding:0px;font-family:Tahoma; font-size:11px;\">");
                        result.Append("<span style=\"color:#2b2b2b; font-weight:bold; display:block;\">");
                        result.Append("<img src=\"http://www.booking2hotels.com/images_mail/hot.png\"  alt=\"\" />&nbsp;Special offer</span>");
                        result.Append("<span style=\" color:#d22315; font-size:11px;display:block; font-weight:bold; font-style:italic;\">" + Hotels2XMLContent.Hotels2XMlReaderPomotionDetail(pItem.PromotionDetail) + "</span>");
                        result.Append("</td></tr>");
                    }
                    
                    result.Append("</table>");

                    result.Append("</td>");
                    Allotment cAllot = new Allotment();

                    if (pItem.StatusAllot)
                        result.Append("<td align=\"center\"><strong style=\"color:#608000;\">Allotment used</strong></td>");
                    else
                    {
                        if (cAllot.CheckAllotAvaliable(pItem.SupplierId, pItem.OptionID, pItem.Unit, pItem.dDateStart, pItem.dDateEnd))
                            result.Append("<td align=\"center\"><strong>Yes</strong></td>");
                        else
                            result.Append("<td align=\"center\"><strong>No</strong></td>");
                    }

                    result.Append("<td align=\"center\">" + pItem.Unit + "</td>");
                    result.Append("<td align=\"right\">" + pItem.Price.Hotels2Currency() + " <label>Baht</label></td>");
                    result.Append("</tr>");

                    //result.Append("<tr><td colspan=\"5\" style=\"background-color:#c8c9ca; line-height:1px; height:1px;  padding:0px; \" height=\"1\"></td></tr>");
                    //result.Append("<tr>");
                    //result.Append("<td align=\"center\">2.</td>");
                    //result.Append("<td style=\"padding:10px 0px 10px 0px;font-family:Tahoma;\">Extra Bed</td>");
                    //result.Append("<td align=\"center\">1</td>");
                    //result.Append("<td align=\"center\">-</td>");
                    //result.Append("<td align=\"right\" style=\"font-family:Tahoma\">850 Baht</td>");
                    //result.Append("</tr>");

                    
                }
                result.Append("<tr><td colspan=\"5\" style=\"background-color:#c8c9ca; line-height:1px; height:1px;  padding:0px; \" height=\"1\"></td></tr>");
                result.Append("<tr>");
                result.Append("<td colspan=\"3\" align= \"right\" style=\" font-size:14px;padding:5px 0px 5px 0px; font-weight:bold;\">Total</td>");
                result.Append("<td colspan=\"2\"  align= \"right\" style=\" font-size:14px;font-family:Tahoma;\">" + cBookingProduct.TotalPriceSales.Hotels2Currency() + " Baht</td>");
                result.Append("</tr>");
                result.Append("<tr><td colspan=\"5\" style=\"background-color:#c8c9ca; line-height:2px; height:2px;  padding:0px; \" height=\"2\"></td></tr>");
                result.Append("</table>");

                ltBookingItem.Text = result.ToString();

            
               //Total & Balance
                BookingTotalAndBalance cTotal = new BookingTotalAndBalance();
                
                cTotal = cTotal.CalcullatePriceTotalByBookingId(int.Parse(this.qBookingId));
                decimal Balance = cTotal.getbalanceByBookingId(int.Parse(this.qBookingId));

                ltGrandTotal.Text = "<span>" + cTotal.SumPrice.Hotels2Currency() + " Baht</span>";
                ltPaid.Text = "<span>" + GrandPaidTotal() + "</span>";

                if (Balance < 0)
                    ltBalance.Text = "<span style=\"color:red;\">" + Balance.Hotels2Currency() + " Baht</span>";
                else
                    ltBalance.Text = "<span style=\"color:#72ac58;\">" + Balance.Hotels2Currency() + " Baht</span>";
                
        }


        protected string GrandPaidTotal()
        {
            BookingTotalAndBalance cBookingtotalPrice = new BookingTotalAndBalance();
            BookingdetailDisplay cBookingDetailDisplay = new BookingdetailDisplay();
            string result = "0";
            decimal Total = cBookingtotalPrice.GetPriceTotalPaidByBookingId(int.Parse(this.qBookingId));
            result = Total.Hotels2Currency();
            //result = cBookingDetailDisplay.GetPriceTotalByBookingId(this.BookingId).Hotels2Currency();

            return result.ToString();
        }


        public string getBookingPayMent()
        {
            BookingPaymentDisplay cPaymentDisplay = new BookingPaymentDisplay();
            BookingPaymentCat cPaymentCat = new BookingPaymentCat();
            Gateway cGateWay = new Gateway();
            StringBuilder result = new StringBuilder();
            BookingdetailDisplay cBooking = new BookingdetailDisplay();
            cBooking = cBooking.GetBookingDetailListByBookingId(int.Parse(this.qBookingId));
            result.Append("<h4><img   src=\"../../images/content.png\" /> Booking Payment</h4>");
            result.Append("<p class=\"contentheadedetail\">List payment of this booking &nbsp;<a href=\"\" onclick=\"newPaymentForm('" + cBooking.LangId + "');return false;\">Make new payment to resubmit</a>&nbsp;");

            string AppendQueryString = string.Empty;
            if (!string.IsNullOrEmpty(this.qProductId) && !string.IsNullOrEmpty(this.qSupplierId))
                AppendQueryString = "&pid=" + this.qProductId + "&supid=" + this.qSupplierId;

            result.Append("&nbsp;|&nbsp;<a href=\"Javascript:popup('popup_card_information.aspx?bid=" + this.qBookingId + AppendQueryString + "',400,600);\" >Decode information</a>");
           // result.Append("&nbsp;|&nbsp;<a href=\"\" onclick=\"getCard('" + cBooking.FullName + "');return false;\"> Card Detail</a>");
            

            result.Append("</p>");
            result.Append("<table id=\"tbl_payment_list\" cellpadding=\"0\" cellspacing=\"2\" class=\"tbl_acknow\">");
            result.Append("<tr class=\"header_field\">");
            result.Append("<td style=\"width:2%\">No.</td><td style=\"width:10%\">Payment ID</td><td style=\"width:8%\">Method</td>");
            result.Append("<td style=\"width:10%\">Amount</td style=\"width:10%\"><td style=\"width:15%\">Confirm payment</td><td style=\"width:23%\">Menu</td></tr>");
            int count = 1;
            foreach (BookingPaymentDisplay paymentItem in cPaymentDisplay.GEtPaymentByBookingId(int.Parse(this.qBookingId)))
            {
                result.Append("<tr  style=\"background-color:#ffffff; height:25px;\" id=\"" + paymentItem.PaymentId + "\">");
                result.Append("<td>" + count + "</td>");
                result.Append("<td>" + paymentItem.intBookingPaymentBank + "</td>");
                //result.Append("<td>" + paymentItem.PaymentId + "/[" + paymentItem.intBookingPaymentBank + "]</td>");
                //result.Append("<td>" + paymentItem.PaymentId + "</br>" + paymentItem.Title + "<br/>" + paymentItem.DatePayment.ToString("dddd, MMM dd, yyyy: hh:mm tt") + "</td>");
                result.Append("<td><span>" + paymentItem.CatTitle + "</span>");

                result.Append("</td>");

                //string GatwayTitle = paymentItem.GateWayTitle;
                //if (paymentItem.CatId == 2)
                //    GatwayTitle = "None";

                //result.Append("<td><span>" + GatwayTitle + "</span>");

                //result.Append("</td>");


                result.Append("<td><span>" + paymentItem.Amount.Hotels2Currency() + "</span></td>");
                result.Append("<td>" + PicStatusNameConfirm(paymentItem.ConfirmPayment, paymentItem.PaymentId, 1, paymentItem.CatId, paymentItem.Comment) + "</td>");

                //result.Append("<td>" + PicStatusNameConfirm(paymentItem.ConfirmSettle, paymentItem.PaymentId, 2) + "</td>");
                result.Append("<td>");

                //if (paymentItem.PaymentTypeId == 2)
                //{
                //    if (paymentItem.ConfirmPayment.HasValue)
                //        result.Append("<input type=\"button\" class=\"btStyleWhite_small\" style=\"width:60px;\" value=\"Charge\" id=\"charge_payment_" + paymentItem.PaymentId + "\"  onclick=\"Cannotresubmit();return false;\" />&nbsp;");
                //    else
                //        result.Append("<input type=\"button\" class=\"btStyleGreen_small\" style=\"width:60px;\" value=\"Charge\" id=\"charge_payment_" + paymentItem.PaymentId + "\" onclick=\"window.open('http://www.hotels2thailand.com/booking_resubmit.aspx?pcode=" + EncodeId(paymentItem.PaymentId) + "','_blank');return false;\" />&nbsp;");

                //}
                if (paymentItem.CatId != 3)
                {

                    if (paymentItem.ConfirmPayment.HasValue)
                    {
                        
                        result.Append("<input type=\"button\" class=\"btn\"  value=\"Resubmit\" id=\"resubmit_payment_" + paymentItem.PaymentId + "\"  onclick=\"Cannotresubmit();return false;\" />");
                        result.Append("&nbsp;<input type=\"button\" class=\"btn\" value=\"Edit\" id=\"resubmit_edit_" + paymentItem.PaymentId + "\"  onclick=\"CannotEdit();return false;\" />");
                    }
                    else
                    {
                        
                        result.Append("<input type=\"button\" class=\"btn\"  value=\"Resubmit\" id=\"resubmit_payment_" + paymentItem.PaymentId + "\"  onclick=\"window.open('resubmit_send.aspx?bid=" + this.qBookingId + "&payid=" + paymentItem.PaymentId + AppendQueryString + "','_blank');return false;\" />");
                        result.Append("&nbsp;<input type=\"button\" class=\"btn\" value=\"Edit\" id=\"resubmit_edit_" + paymentItem.PaymentId + "\"  onclick=\"Editpayment('" + paymentItem.PaymentId + "', '" + cBooking.LangId + "');return false;\" />");
                    }
                }
                else
                {

                  
                        result.Append("<input type=\"button\" class=\"btn\"  value=\"Resubmit\" id=\"resubmit_payment_" + paymentItem.PaymentId + "\"  onclick=\"Cannotresubmit();return false;\" />");
                        result.Append("&nbsp;<input type=\"button\" class=\"btn\" value=\"Edit\" id=\"resubmit_edit_" + paymentItem.PaymentId + "\"  onclick=\"Editpayment('" + paymentItem.PaymentId + "', '" + cBooking.LangId + "');return false;\" />");
                   
                }

                



                //result.Append("&nbsp;<input type=\"button\" class=\"btn\" value=\"Edit\" id=\"resubmit_edit_" + paymentItem.PaymentId + "\"  onclick=\"Editpayment('" + paymentItem.PaymentId + "', '" + cBooking.LangId + "');return false;\" />");
                result.Append("</td>");
                result.Append("</tr>");
                count = count + 1;
            }

            result.Append("</table>");

            return result.ToString();
        }

        public string PicStatusNameConfirm(DateTime? DateConfirm, int intPaymentId, byte ConfirmType)
        {
            string imageName = string.Empty;
            if (DateConfirm.HasValue)
            {
                DateTime dDate = (DateTime)DateConfirm;
                imageName = "<img src=\"../../images/true.png\"/></br>" + dDate.ToString("ddd, MMM dd, yyyy ; HH:mm") + "<img src=\"../../images/refresh.png\" onclick=\"DarkmanPopUpComfirm(400,'Would you like to swicth back now!!??' ,'confirmswitchbackPayment(" + intPaymentId + "," + ConfirmType + ");');return false;\"  style=\"cursor:pointer;\" />";
            }

            else
                imageName = "<img src=\"../../images/false.png\"/></br><a href=\"\" onclick=\"DarkmanPopUpComfirm(400,'Would you like to confirm now!!??' ,'BookingPaymentConfirm(" + intPaymentId + "," + ConfirmType + ");');return false;\">Confirm Now</a>";

            return imageName;
        }

        public string PicStatusNameConfirm(DateTime? DateConfirm, int intPaymentId, byte ConfirmType, byte bytTransferType, string transferDetail)
        {
            string imageName = string.Empty;
            if (DateConfirm.HasValue)
            {
                DateTime dDate = (DateTime)DateConfirm;
                imageName = "<img src=\"../../images/true.png\"/></br>" + dDate.ToString("ddd, MMM dd, yyyy ; HH:mm") + "<img src=\"../../images/refresh.png\" onclick=\"DarkmanPopUpComfirm(400,'Would you like to swicth back now!!??' ,'confirmswitchbackPayment(" + intPaymentId + "," + ConfirmType + ");');return false;\"  style=\"cursor:pointer;\" />";
                if (bytTransferType == 2)
                {
                    if (string.IsNullOrEmpty(transferDetail))
                        imageName = imageName + "<br/><a href=\"\" onclick=\"PaymentDetailInsert('" + intPaymentId + "');return false;\">please insert transfer detail!</a>";
                    else
                        imageName = imageName + "<br/><a href=\"\" onclick=\"GetPaymentDetail('" + intPaymentId + "');return false;\">transfer detail</a>";
                }

            }
            else
            {
                if (bytTransferType == 2)
                {

                    //CannotPaymentComfirm
                    if (string.IsNullOrEmpty(transferDetail))
                    {
                        imageName = "<img src=\"../../images/false_gray.png\"/></br><a href=\"\" style=\"color:#cccccc;\" onclick=\"CannotPaymentComfirm();return false;\">Confirm Now</a>";
                        imageName = imageName + "<br/><a href=\"\" onclick=\"PaymentDetailInsert('" + intPaymentId + "');return false;\">please insert transfer detail!</a>";
                    }

                    else
                    {
                        imageName = "<img src=\"../../images/false.png\"/></br><a href=\"\" onclick=\"DarkmanPopUpComfirm(400,'Would you like to confirm now!!??' ,'BookingPaymentConfirm(" + intPaymentId + "," + ConfirmType + ");');return false;\">Confirm Now</a>";
                        imageName = imageName + "<br/><a href=\"\" onclick=\"PaymentDetailInsert('" + intPaymentId + "');return false;\">transfer detail</a>";
                    }

                }
                else
                {
                    imageName = "<img src=\"../../images/false.png\"/></br><a href=\"\" onclick=\"DarkmanPopUpComfirm(400,'Would you like to confirm now!!??' ,'BookingPaymentConfirm(" + intPaymentId + "," + ConfirmType + ");');return false;\">Confirm Now</a>";
                }

            }

            return imageName;
        }

        protected string EncodeId(int IdtoEncod)
        {
            string Random = Hotels2String.Hotels2RandomStringNuM(20);
            string strToEndCode = IdtoEncod + Random;
            string EncodeResult = strToEndCode.Hotel2EncrytedData_SecretKey();
            return HttpUtility.UrlEncode(EncodeResult);
        }


        public string getBookingActivity()
        {
            BookingActivityDisplay cActivityDisplay = new BookingActivityDisplay();
            List<object> cActivityDisplayList = new List<object>();
            string AcHead = "Booking Activity";
            cActivityDisplayList = cActivityDisplay.GetActivityBookingList(int.Parse(this.qBookingId));
            int count = 1;
            StringBuilder result = new StringBuilder();
            result.Append("<h4><img   src=\"../../images/content.png\" /> " + AcHead + "</h4>");

            result.Append("<table cellpadding=\"0\" cellspacing=\"2\" class=\"tbl_acknow\" width=\"100%\" >");
            //result.Append("<tr style=\"background-color:#3f5d9d;color:#ffffff;font-weight:bold;height:20px;line-height:20px;\"><td colspan=\"4\" style=\"font-size:14px;font-weight:bold\">Product Activity</td></tr>");
            result.Append("<tr class=\"header_field\" ><td style=\"width:5%\">No.</td><td style=\"width:10%\">Staff</td><td style=\"width:85%;\">Detail</td></tr>");
            string strDateAc = string.Empty;
            count = cActivityDisplayList.Count();
            foreach (BookingActivityDisplay acITem in cActivityDisplayList)
            {

                if (acITem.DateActivity.HasValue)
                {
                    DateTime dDate = (DateTime)acITem.DateActivity;
                    strDateAc = dDate.ToString("MMM dd, yyyy ; HH:mm ");
                }
                else
                {
                    strDateAc = "N/A";
                }
                result.Append("<tr style=\"background-color:#ffffff; height:25px;\"><td>" + count + "</td><td>" + acITem.StaffName + "</td><td style=\"text-align:left;padding:0px 0px 2px 2px;\">" + acITem.Detail + "<br/><p style=\"margin:2px 0px 0px 0px;padding:2px 0px 0px 0px;font-size:10px; color:#3f5d9d; text-align:right;\">" + strDateAc + "</p></td></tr>");
                count = count - 1;
            }

            result.Append("<tr style=\"background-color:#ffffff; height:25px;\"><td colspan=\"3\"><a href=\"\" onclick=\"addnewactivity();return false;\">add new activity</a></td></tr>");
            
            result.Append("</table>");

            return result.ToString();
        }


        public string getBookingDetailButtom()
        {
            StringBuilder result = new StringBuilder();

            BookingTotalAndBalance cTotalBalance = new BookingTotalAndBalance();
            BookingPaymentDisplay cPaymentDisplay = new BookingPaymentDisplay();
            BookingdetailDisplay cBookingDetail = new BookingdetailDisplay();
            cBookingDetail = cBookingDetail.GetBookingDetailListByBookingId(int.Parse(this.qBookingId));
            //decimal decBalance = cTotalBalance.getbalanceByBookingId(int.Parse(this.qBookingId));
            //int intPayment = cPaymentDisplay.GEtPaymentByBookingId(int.Parse(this.qBookingId)).Count();
            int PaymentNotConfirm = cPaymentDisplay.GEtPaymentByBookingIdNotConfirm(int.Parse(this.qBookingId));

            result.Append("<table id=\"booking_detail_confirm\" cellpadding=\"0\" cellspacing=\"2\" width=\"100%\"  class=\"tbl_acknow\">");
            result.Append("<tr class=\"header_field\"><td colspan=\"4\">Booking Confirm</td></tr>");
            result.Append("<tr class=\"header_field\"><td colspan=\"2\">Confirm Voucher</td><td >Confirm Input</td></tr>");
            result.Append("<tr style=\"background-color:#ffffff; height:35px;\"><td>" + PicStatusNameConfirmSendVoucher(cBookingDetail.ConfirmOpen, 4, PaymentNotConfirm) + "</td><td>" + PicStatusNameConfirmOpenmail(cBookingDetail.ConfirmVoucher) + "</td><td>" + PicStatusNameConfirmInput(cBookingDetail.ConfirmInput, 18) + "</td></tr>");
            result.Append("</table>");

            result.Append("<p id=\"voucher_send_btn\" >");

            if (cBookingDetail.PaymentTypeID == 1)
            {
                
                
                //result.Append("<a href=\"\" onclick=\"window.open('voucher_send.aspx?bid=" + this.qBookingId + "','_blank');return false;\" >Send Voucher to customer</a>");
                //if (decBalance >= 0)
                //    result.Append("<a href=\"\" onclick=\"window.open('voucher_send.aspx?bid=" + this.qBookingId + "','_blank');return false;\" >Send Voucher to customer</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href=\"\" onclick=\"window.open('review_send.aspx?bid=" + this.qBookingId + "','_blank');return false;\">Send Review</a>");
                //else
                //    result.Append("<a href=\"\" onclick=\"CannotSendVoucher();return false;\" style=\"background:#f5f6f6;color:#333333;\">Send Voucher to customer>></a>");
            }


            //result.Append("&nbsp;|&nbsp;<a href=\"\" onclick=\"window.open('review_send.aspx?bid=" + this.qBookingId + "','_blank');return false;\"  style=\"width:150px;\">Send Review</a>");

            //if (!cBookingDetail.Status)
            //    result.Append("<a href=\"\" onclick=\"closebooking();return false;\" style=\" background:#ef2d2d\" >Close this Booking</a>");
            //else
            //    result.Append("<a href=\"\" onclick=\"Openbooking();return false;\" style=\" background:#3f5d9d\" >Open this Booking</a>");

            result.Append("</p>");
            result.Append("<div style=\"clear:both\"></div>");

            

            return result.ToString();
        }

        public string PicStatusNameConfirmOpenmail(DateTime? DateConfirm)
        {
            string imageName = string.Empty;
            if (DateConfirm.HasValue)
            {
                DateTime dDate = (DateTime)DateConfirm;
                imageName = "<img src=\"../../images/true.png\"/></br>" + dDate.ToString("ddd, MMM dd, yyyy ; HH:mm");
            }

            else
                imageName = "<img src=\"../../images/false.png\"/></br><p style=\"font-weight:bold;font-size:11px;margin:0px;padding:0px;color:#72ac58\">Open Voucher</p>";

            return imageName;
        }

        public string PicStatusNameConfirmInput(DateTime? DateConfirm, byte ConfirmCat)
        {

            string imageName = string.Empty;
            if (DateConfirm.HasValue)
            {
                DateTime dDate = (DateTime)DateConfirm;
                //imageName = "<img src=\"../../images/true.png\"/></br>" + dDate.ToString("ddd, MMM dd, yyyy ; hh:mm tt") + "<img src=\"../../images/refresh.png\" onclick=\"confirmswitchback('" + intBookingProductId + "');return false;\" style=\"cursor:pointer;\" />";
                imageName = "<img src=\"../../images/true.png\"/></br>" + dDate.ToString("ddd, MMM dd, yyyy ; HH:mm") + "<img src=\"../../images/refresh.png\" onclick=\"DarkmanPopUpComfirm(400,'Would You Like To Swicth back Now!!?' ,'ConfirmSwitchBackBooking(" + ConfirmCat + ")');return false;\" style=\"cursor:pointer;\" />";
            }

            else
                imageName = "<img src=\"../../images/false.png\"/></br><a href=\"\" onclick=\"BookingConfirmBookingInput(" + ConfirmCat + ");return false;\">Confirm Now</a>";

            return imageName;
        }

        public string PicStatusNameConfirm(DateTime? DateConfirm, byte ConfirmCat)
        {
            string imageName = string.Empty;
            if (DateConfirm.HasValue)
            {
                DateTime dDate = (DateTime)DateConfirm;
                //imageName = "<img src=\"../../images/true.png\"/></br>" + dDate.ToString("ddd, MMM dd, yyyy ; hh:mm tt") + "<img src=\"../../images/refresh.png\" onclick=\"confirmswitchback('" + intBookingProductId + "');return false;\" style=\"cursor:pointer;\" />";
                imageName = "<img src=\"../../images/true.png\"/></br>" + dDate.ToString("ddd, MMM dd, yyyy ; HH:mm") + "<img src=\"../../images/refresh.png\" onclick=\"DarkmanPopUpComfirm(400,'Would You Like To Swicth back Now!!?' ,'ConfirmSwitchBackBooking(" + ConfirmCat + ")');return false;\" style=\"cursor:pointer;\" />";
            }

            else
                imageName = "<img src=\"../../images/false.png\"/></br><a href=\"\" onclick=\"DarkmanPopUpComfirm(400,'Would You Like To Confirm Now!!??' ,'BookingConfirm(" + ConfirmCat + ")');return false;\">Confirm Now</a>";

            return imageName;
        }


        public string PicStatusNameConfirmSendVoucher(DateTime? DateConfirm, byte ConfirmCat, int PaymentNotconfirm)
        {
            string imageName = string.Empty;
            if (DateConfirm.HasValue)
            {
                DateTime dDate = (DateTime)DateConfirm;
                //imageName = "<img src=\"../../images/true.png\"/></br>" + dDate.ToString("ddd, MMM dd, yyyy ; hh:mm tt") + "<img src=\"../../images/refresh.png\" onclick=\"confirmswitchback('" + intBookingProductId + "');return false;\" style=\"cursor:pointer;\" />";
                imageName = "<img src=\"../../images/true.png\"/></br>" + dDate.ToString("ddd, MMM dd, yyyy ; HH:mm") + "<img src=\"../../images/refresh.png\" onclick=\"DarkmanPopUpComfirm(400,'Would You Like To Swicth back Now!!?' ,'ConfirmSwitchBackBooking(" + ConfirmCat + ")');return false;\" style=\"cursor:pointer;\" />";
            }

            else
            {
                 if (PaymentNotconfirm> 0)
                     imageName = "<img src=\"../../images/false.png\"/></br><a href=\"\" onclick=\"CannotSendVoucher();return false;\">Confirm Now</a>";
                 else
                    imageName = "<img src=\"../../images/false.png\"/></br><a href=\"\" onclick=\"DarkmanPopUpComfirm(400,'Would You Like To Confirm And Send Voucher  Now!!??' ,'BookingConfirmSendVoucher(" + ConfirmCat + ")');return false;\">Confirm Now</a>";

            }
                
            return imageName;
        }
        //protected void lkUpdateStatus_Click(object sender, EventArgs e)
        //{
        //    BookingdetailDisplay cBooking = new BookingdetailDisplay();
        //    int intBookingId = int.Parse(this.qBookingId);
           
        //    cBooking = cBooking.GetBookingDetailListByBookingId(intBookingId);

        //    if (cBooking.StatusId != 85)
        //    {

        //        ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>addnewactivity_AndCloseBooking();</script>", false);
        //        //onclick=\"addnewactivity('" + this.qBookingId + "','booking');return false;\"
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>window.open('review_send.aspx?bid=" + this.qBookingId + "','_blank');</script>", false);
                
        //    }
        //    //cBooking.UpdateBookingstatus(intBookingId);

        //    //Response.Redirect(Request.Url.ToString());
        //}
}
}