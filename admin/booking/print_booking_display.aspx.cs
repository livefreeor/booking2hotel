using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Hotels2thailand.Booking;

namespace Hotels2thailand.UI
{
    public partial class admin_print_booking_display : Hotels2BasePage
    {
        //booking_id
        public string qBookingProductId
        {
            get
            {
                return Request.QueryString["bpid"];
            }
        }
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
                //int intBookingItemId = int.Parse(this.qBookingProductId);
                //BookingItemDisplay ItemList = new BookingItemDisplay();
                //int dd = ItemList.getBookingItemListByBookingProductIdAndCatId(intBookingItemId, "NOT IN (39,40,43,44,47)").Count;
                //Response.Write(dd);
                //Response.End();
               //GVBookingItemDataBound();
               //GetBookingItemTanfer();
               //txtSubject.Text = "Blue House Travel: Please Book and Confirm this Booking ( Booking ID:" + this.qBookingId + ")";
               //txtBcc.Text = "sent@hotels2thailand.com;sent2@hotels2thailand.com";


                //Updaterequirement();
                //UpdateBookingItemTanfer();

            }
        }

        public void submit_Onclick(object sender, EventArgs e)
        {

           
            BookingBooking_PrintEngine cBookingPrint = new BookingBooking_PrintEngine(int.Parse(this.qBookingProductId));
            string BookingPrint = cBookingPrint.getBookingPrint();
            BookingPrint = BookingPrint.Replace("<!--##@voucherPrint_hotelAttn##-->", txtAttendName.Text);

            Response.Write(BookingPrint);
            Response.End();

        }

        //public void GetBookingItemTanfer()
        //{

        //    BookingItemDisplay cBookingItem = new BookingItemDisplay();
        //    List<object> cBookingItemList = cBookingItem.getBookingItemListByBookingProductId(int.Parse(this.qBookingProductId));
        //        //.Where(cat => (int)cat.GetType().GetProperty("OptionCAtID").GetValue(cat, null) == 44 || (int)cat.GetType().GetProperty("OptionCAtID").GetValue(cat, null) == 43).FirstOrDefault();
            
        //    foreach (BookingItemDisplay item in cBookingItemList)
        //    {
        //        if (item.OptionCAtID == 44 || item.OptionCAtID == 43 || item.OptionCAtID == 52 || item.OptionCAtID == 53 || item.OptionCAtID == 54)
        //        {
        //            if ((GVBookingItem.Rows.Count % 2) == 0)
        //                paneltranfer.CssClass = "divtransfer_bg";
        //            //<div style="width:100%; margin:5px 0px 0px 0px; padding:5px 5px 5px 10px;  border:1px solid #333333;">
        //            paneltranfer.Visible = true;
        //            //optionTitle.Text = "ddd";
        //            //Response.Write(item.BookingOptionTitle);
        //            optionTitle.Text = item.BookingOptionTitle;
        //            txtComment.Text = item.BookingItemDetail;

                    

        //            break;
        //        }
        //    }
        //}

        //public void UpdateBookingItemTanfer()
        //{

        //    BookingItemDisplay cBookingItem = new BookingItemDisplay();
        //    List<object> cBookingItemList = cBookingItem.getBookingItemListByBookingProductId(int.Parse(this.qBookingProductId));
        //        //.Where(cat => (int)cat.GetType().GetProperty("OptionCAtID").GetValue(cat, null) == 44).ToList();



            
        //        foreach (BookingItemDisplay item in cBookingItemList)
        //        {
        //           // if (item.OptionCAtID == 44)
        //            //{
        //                cBookingItem.UpdateBookingItemDetail(item.BookingItemID, txtComment.Text);
        //           // }
        //        }
               

            
        //}
        //public void GVBookingItemDataBound()
        //{
        //    int intBookingProductID = int.Parse(this.qBookingProductId);
        //    BookingProductDisplay cBookingProductID = new BookingProductDisplay();
        //    cBookingProductID = cBookingProductID.getBookingProductDisplayByBookingProductId(intBookingProductID);
            
        //    BookingRequireMent require = new BookingRequireMent();
        //    try
        //    {
                
        //        GVBookingItem.DataSource = require.GetRequireMentByBooingProductID(intBookingProductID, cBookingProductID.ProductCategory);
                
        //    }catch(Exception ex)
        //    {
        //        Response.Write(ex.Message);
        //        Response.End();
        //    }

        //    GVBookingItem.DataBind();
        //}


        //public void GVBookingItem_ONrowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        //BookingRequireMent require = new BookingRequireMent();
        //        byte bytProductcat = (byte)DataBinder.Eval(e.Row.DataItem, "ProductCat");
        //        string bytOptionTitle = string.Empty;

        //        if (DataBinder.Eval(e.Row.DataItem, "OptionTitle") != null)
        //            bytOptionTitle = DataBinder.Eval(e.Row.DataItem, "OptionTitle").ToString();
        //        else
        //            bytOptionTitle = DataBinder.Eval(e.Row.DataItem, "OptionTitleEngDefault").ToString();
        //            //.ToString();
        //        int intRequireID = (int)DataBinder.Eval(e.Row.DataItem, "RequirID");
        //        string strComment = DataBinder.Eval(e.Row.DataItem, "Comment").ToString();

        //        string smoke = DataBinder.Eval(e.Row.DataItem, "Smoke").ToString();
        //        string Bed = DataBinder.Eval(e.Row.DataItem, "Bed").ToString();
        //        string Floor = DataBinder.Eval(e.Row.DataItem, "Floor").ToString();

        //        DropDownList GvdropSmoke = e.Row.Cells[0].FindControl("dropSmoke") as DropDownList;
        //        DropDownList GvdropRoom = e.Row.Cells[0].FindControl("dropRoom") as DropDownList;
        //        DropDownList GvsropFloor = e.Row.Cells[0].FindControl("sropFloor") as DropDownList;
        //        TextBox GvtxtComment = e.Row.Cells[0].FindControl("txtComment") as TextBox;
        //        Label GvoptionTitle = e.Row.Cells[0].FindControl("optionTitle") as Label;
        //        GvoptionTitle.Text = bytOptionTitle;
        //        //ArrayList obj = null;
        //        GvdropSmoke.DataSource = BookingRequireMent.getTypeSmoke();
        //        GvdropSmoke.DataTextField = "Value";
        //        GvdropSmoke.DataValueField = "Key";
        //        GvdropSmoke.DataBind();

        //        GvdropRoom.DataSource = BookingRequireMent.getTypeBed();
        //        GvdropRoom.DataTextField = "Value";
        //        GvdropRoom.DataValueField = "Key";
        //        GvdropRoom.DataBind();

        //        GvsropFloor.DataSource = BookingRequireMent.getFloor();
        //        GvsropFloor.DataTextField = "Value";
        //        GvsropFloor.DataValueField = "Key";
        //        GvsropFloor.DataBind();

        //        GvdropSmoke.SelectedValue = "3";
        //        GvdropRoom.SelectedValue = "3";
        //        GvsropFloor.SelectedValue = "3";
        //      switch (bytProductcat)
        //        {
        //          case 29 :
        //                GvdropSmoke.Visible = true;
        //                GvdropRoom.Visible = true;
        //                GvsropFloor.Visible = true;
        //                GvdropSmoke.SelectedValue = smoke;
        //                GvdropRoom.SelectedValue = Bed;
        //                GvsropFloor.SelectedValue = Floor;
        //                GvtxtComment.Text = strComment;
        //                //obj = require.GetRequireMentByBooinProductIDAndProductCatSingle(intRequireID, bytProductcat);
        //          break;
        //          case 40:
        //          GvtxtComment.Text = strComment;
        //          //obj = require.GetRequireMentByBooinProductIDAndProductCatSingle(intRequireID, bytProductcat);
                      
        //              break;
        //         case 39:
        //              GvtxtComment.Text = strComment;
        //              //obj = require.GetRequireMentByBooinProductIDAndProductCatSingle(intRequireID, bytProductcat);
                      
        //              break;
        //         default:
        //              GvtxtComment.Text = strComment;
        //              //obj = require.GetRequireMentByBooinProductIDAndProductCatSingle(intRequireID, bytProductcat);
                      
        //              break;
        //        }
                
        //    }
        //}
        //public void Updaterequirement()
        //{
        //    foreach (GridViewRow GvRow in GVBookingItem.Rows)
        //    {
        //        if (GvRow.RowType == DataControlRowType.DataRow)
        //        {
        //            DropDownList GvdropSmoke = GVBookingItem.Rows[GvRow.RowIndex].Cells[0].FindControl("dropSmoke") as DropDownList;
        //            DropDownList GvdropRoom = GVBookingItem.Rows[GvRow.RowIndex].Cells[0].FindControl("dropRoom") as DropDownList;
        //            DropDownList GvsropFloor = GVBookingItem.Rows[GvRow.RowIndex].Cells[0].FindControl("sropFloor") as DropDownList;
        //            TextBox GvtxtComment = GVBookingItem.Rows[GvRow.RowIndex].Cells[0].FindControl("txtComment") as TextBox;
        //            Label GvoptionTitle = GVBookingItem.Rows[GvRow.RowIndex].Cells[0].FindControl("optionTitle") as Label;
        //            Literal GvHdCAt = GVBookingItem.Rows[GvRow.RowIndex].Cells[0].FindControl("hdCat") as Literal;

        //            int intBookingItemId = (int)GVBookingItem.DataKeys[GvRow.RowIndex].Value;

        //            BookingRequireMent require = new BookingRequireMent();
        //            ArrayList obj = require.GetRequireMentByBooinProductIDAndProductCatSingle(intBookingItemId, byte.Parse(GvHdCAt.Text));
        //            if (obj == null)
        //            {
        //                switch (byte.Parse(GvHdCAt.Text))
        //                {
        //                    case 29:
        //                        require.InsertNewBookingRequireMentHotel(intBookingItemId, GvtxtComment.Text, byte.Parse(GvdropSmoke.SelectedValue), byte.Parse(GvdropRoom.SelectedValue), byte.Parse(GvsropFloor.SelectedValue));
        //                        break;
        //                    case 40:
        //                        require.InsertNewBookingRequireMentSpa(intBookingItemId, GvtxtComment.Text);
        //                        break;
        //                    case 39:
        //                        require.InsertNewBookingRequireMenthealth(intBookingItemId, GvtxtComment.Text);
        //                        break;
        //                    default:
        //                        require.InsertNewBookingRequireMentGeneral(intBookingItemId, GvtxtComment.Text);
        //                        break;
        //                }
        //            }
        //            else
        //            {
        //                int intREquiredID = 0;
        //                switch (byte.Parse(GvHdCAt.Text))
        //                {
        //                    case 29:
        //                        intREquiredID = (int)obj[4];
        //                        require.UpdateBookingRequireMentHotel(intREquiredID, GvtxtComment.Text, byte.Parse(GvdropSmoke.SelectedValue), byte.Parse(GvdropRoom.SelectedValue), byte.Parse(GvsropFloor.SelectedValue));
        //                        break;
        //                    case 40:
        //                        intREquiredID = (int)obj[1];
        //                        require.UpdateBookingRequireMentSpa(intREquiredID, GvtxtComment.Text);
        //                        break;
        //                    case 39:
        //                        intREquiredID = (int)obj[1];
        //                        require.UpdateBookingRequireMenthealth(intREquiredID, GvtxtComment.Text);
        //                        break;
        //                    default:
        //                        intREquiredID = (int)obj[1];
        //                        require.UpdateBookingRequireMentGeneral(intREquiredID, GvtxtComment.Text);
        //                        break;
        //                }

        //            }
        //        }
        //    }
        //}

        

        //public void submitMail_Onclick(object sender, EventArgs e)
        //{
        //    panelMAilDisplay.Visible = true;
        //    panelRequireMent.Visible = false;

        //    Updaterequirement();
        //    UpdateBookingItemTanfer();

        //    BookingBooking_PrintEngine cBookingPrint = new BookingBooking_PrintEngine(int.Parse(this.qBookingProductId));
        //    string BookingPrint = cBookingPrint.getBookingPrint();
        //    BookingPrint = BookingPrint.Replace("<!--##@voucherPrint_hotelAttn##-->", txtAttendName.Text);

        //    editor.Content = BookingPrint;
            
        //}

        //public void submit_click(object sender, EventArgs e)
        //{
        //    string BodyTosend = "<p>" + txtMessage.Text + "</p><br/><br/>" + editor.Content;
        //    bool Issent = Hotels2MAilSender.SendmailBooking("reservation@hotels2thailand.com", txtMailTO.Text, txtSubject.Text, txtBcc.Text, BodyTosend);
        //    if (Issent)
        //    {
        //        ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>DarkmanPopUpComfirm('450','Mail was sent successfully.<br/> would like to close this window now !?','window.close();');</script>", false);
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>DarkmanPopUpAlert('450','Send fail !!<br/> Please Contact R&D Team!!');</script>", false);
        //    }
        //}
        
    }
}