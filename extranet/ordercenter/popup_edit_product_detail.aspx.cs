using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Booking;
using Hotels2thailand.ProductOption;
using Hotels2thailand.Production;
using Hotels2thailand;


namespace Hotels2thailand.UI
{
    public partial class extranet_ordercenter_popup_edit_product_detail : Hotels2BasePageExtra
    {
        public string qBookingProductID
        {
            get
            {
                return Request.QueryString["bpid"];
            }
        }
        public string qBookingID
        {
            get
            {
                return Request.QueryString["bid"];
            }
        }

        public IList<object> ListObjOption
        {
            get
            {
                Option cOption = new Option();
                IList<object> iLisOption = cOption.GetProductOptiontAll_Supplier(this.CurrentProductActiveExtra, this.CurrentSupplierId);
                return iLisOption;
            }
        }

        public BookingProductDisplay cBookingProduct
        {
            get
            {
                BookingProductDisplay cBookingProductID = new BookingProductDisplay();
                return cBookingProductID.getBookingProductDisplayByBookingProductId(int.Parse(this.qBookingProductID));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int intBookingProduct_id = int.Parse(this.qBookingProductID);

                //dropOption.DataSource = this.ListObjOption;
                //dropOption.DataTextField = "Title";
                //dropOption.DataValueField = "OptionID";
                //dropOption.DataBind();

                //ConditionDataBind(int.Parse(dropOption.SelectedValue));


                dropAdult.DataSource = this.dicGetNumber(10);
                dropAdult.DataTextField = "Value";
                dropAdult.DataValueField = "Key";
                dropAdult.DataBind();


                dropChild.DataSource = this.dicGetNumberstart0(10);
                dropChild.DataTextField = "Value";
                dropChild.DataValueField = "Key";
                dropChild.DataBind();


                //dropUnit.DataSource = this.dicGetNumber(20);
                //dropUnit.DataTextField = "Value";
                //dropUnit.DataValueField = "Key";
                //dropUnit.DataBind();


                dropAdult.SelectedValue = this.cBookingProduct.NumAdult.ToString();
                dropChild.SelectedValue = this.cBookingProduct.NumChild.ToString();


                dDatePicker_checkin.DateStart = (DateTime)this.cBookingProduct.DateTimeCheckIn;
                dDatePicker_checkout.DateStart = (DateTime)this.cBookingProduct.DateTimeCheckOut;
                //dDatePicker_arr.DateStart = DateTime.Now.Hotels2ThaiDateTime();
                dDatePicker_checkout.DataBind();
                dDatePicker_checkin.DataBind();


                BookingItemList();
            }
        }

        //public void ConditionDataBind(int intOptionId)
        //{
        //    ProductConditionExtra cCondition = new ProductConditionExtra();
             
        //    dropCondition.DataSource = cCondition.getConditionListByOptionId(intOptionId, 1);
        //    dropCondition.DataTextField = "TitleCondition";
        //    dropCondition.DataValueField = "ConditionId";
        //    dropCondition.DataBind();
        //}



        public void BookingItemList()
        {
            int intBookingProduct_id = int.Parse(this.qBookingProductID);

            StringBuilder result = new StringBuilder();

            BookingItemDisplay cBookingItem = new BookingItemDisplay();
            List<object> cBookingItemListDefault = cBookingItem.getBookingItemListByBookingProductId(intBookingProduct_id, 1);
            List<object> cBookingItemListReal = cBookingItem.getBookingItemListByBookingProductId(intBookingProduct_id, this.cBookingProduct.BookingLang);

            List<object> cBookingItemList = null;
            if (cBookingItemListDefault.Count == cBookingItemListReal.Count)
                cBookingItemList = cBookingItemListReal;
            else
                cBookingItemList = cBookingItemListDefault;

            GVitemList.DataSource = cBookingItemList;
            GVitemList.DataBind();

        }


        public void GVitemList_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //DropDownList dropRoomType = e.Row.Cells[1].FindControl("GVdropRoomType") as DropDownList;
                //DropDownList dropCondition = e.Row.Cells[2].FindControl("GVdropCondition") as DropDownList;
                Label lblRoomType = e.Row.Cells[1].FindControl("lblRoomtype") as Label;
                Label lblCondition = e.Row.Cells[2].FindControl("lblCondition") as Label;
                TextBox txtPrice = e.Row.Cells[2].FindControl("GvtxtPrice") as TextBox;
                DropDownList GvdropQuantity = e.Row.Cells[3].FindControl("GvdropQuantity") as DropDownList;

                int intBookingItemId = (int)GVitemList.DataKeys[e.Row.DataItemIndex].Value;
                int intOptionId = (int)DataBinder.Eval(e.Row.DataItem,"OptionID");
                int intConditionId = (int)DataBinder.Eval(e.Row.DataItem, "ConditionID");
                byte bytUnit = (byte)DataBinder.Eval(e.Row.DataItem, "Unit");
                decimal Price = (decimal)DataBinder.Eval(e.Row.DataItem, "Price");

                // silly fool  Programe by nui darkman
                ProductConditionExtra cConditionEx = new ProductConditionExtra();
                cConditionEx = cConditionEx.getConditionByConditionId(intConditionId);


                string strOptionTitle = DataBinder.Eval(e.Row.DataItem, "OptionTitle").ToString();
                string strConditionTitle = DataBinder.Eval(e.Row.DataItem, "ConditionTitle").ToString() + Hotels2String.AppendConditionDetailExtraNet(cConditionEx.NumAult, cConditionEx.Breakfast);


                //dropRoomType.DataSource = this.ListObjOption;
                //dropRoomType.DataTextField = "Title";
                //dropRoomType.DataValueField = "OptionID";
                //dropRoomType.DataBind();


                //ProductConditionExtra cCondition = new ProductConditionExtra();
                //dropCondition.DataSource = cCondition.getConditionListByOptionId(int.Parse(dropRoomType.SelectedValue), 1);
                //dropCondition.DataTextField = "TitleCondition";
                //dropCondition.DataValueField = "ConditionId";
                //dropCondition.DataBind();


                GvdropQuantity.DataSource = this.dicGetNumber(20);
                GvdropQuantity.DataTextField = "Value";
                GvdropQuantity.DataValueField = "Key";
                GvdropQuantity.DataBind();

                lblCondition.Text = strConditionTitle;
                lblRoomType.Text = strOptionTitle;
                txtPrice.Text = Price.Hotels2Currency();
                //dropRoomType.SelectedVstrOptionTitlealue = intOptionId.ToString();
                //dropCondition.SelectedValue = intConditionId.ToString();


                GvdropQuantity.SelectedValue = bytUnit.ToString();
            }
        }

        //Onselelct INdexChange FRom GridViewROw
        public void GVdropRoomType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList dropOption = (DropDownList)sender;

            DropDownList dropCondition = (DropDownList)((Control)sender).Parent.Parent.FindControl("GVdropCondition");

            ProductConditionExtra cCondition = new ProductConditionExtra();

            dropCondition.DataSource = cCondition.getConditionListByOptionId(int.Parse(dropOption.SelectedValue), 1);
            dropCondition.DataTextField = "TitleCondition";
            dropCondition.DataValueField = "ConditionId";
            dropCondition.DataBind();
        }



        public void mainSave_Onclick(object sender, EventArgs e)
        {

            BookingProductDisplay cBookingProductDis = new BookingProductDisplay();

            bool result = cBookingProductDis.UpdateBookingProductByBookingProductId(int.Parse(this.qBookingID), int.Parse(this.qBookingProductID), dDatePicker_checkin.GetDatetStart, dDatePicker_checkout.GetDatetStart, byte.Parse(dropAdult.SelectedValue), byte.Parse(dropChild.SelectedValue), 0);

            foreach (GridViewRow GvRow in GVitemList.Rows)
            {
                if (GvRow.RowType == DataControlRowType.DataRow)
                {
                    int intBookingItemId = (int)GVitemList.DataKeys[GvRow.RowIndex].Value;
                    TextBox txtPrice = GVitemList.Rows[GvRow.RowIndex].Cells[2].FindControl("GvtxtPrice") as TextBox;
                    DropDownList GvdropQuantity = GVitemList.Rows[GvRow.RowIndex].Cells[3].FindControl("GvdropQuantity") as DropDownList;

                    BookingItemDisplay cBookingItemDis = new BookingItemDisplay();

                    cBookingItemDis.UpdateBookingItem(intBookingItemId, decimal.Parse(txtPrice.Text), decimal.Parse(txtPrice.Text), byte.Parse(GvdropQuantity.SelectedValue), true);
                }
                

            }


            ScriptManager.RegisterStartupScript(this, Page.GetType(), null, "<script>window.close();if (window.opener && !window.opener.closed) {window.opener.location.reload();}</script>", false);
        }

        //public void GvimgREmove_ONclick(object sender, EventArgs e)
        //{

        //}


        // From Insert Box ------------------------------------
        //public void dropOption_OnSelectedIndexChanged(object sender, EventArgs e)
        //{


            //DropDownList dropOption = (DropDownList)sender;

            //DropDownList dropCondition = (DropDownList)((Control)sender).Parent.Parent.FindControl("GVdropCondition");
            //Response.Write(dropCondition.GetType().Name);
            //Response.End();

            //ProductConditionExtra cCondition = new ProductConditionExtra();

            //dropCondition.DataSource = cCondition.getConditionListByOptionId(int.Parse(dropOption.SelectedValue), 1);
            //dropCondition.DataTextField = "Title";
            //dropCondition.DataValueField = "ConditionId";
            //dropCondition.DataBind();

           // GridViewRow GvRow = (GridViewRow)(sender as Control).Parent.Parent;
            //ConditionDataBind(int.Parse(dropOption.SelectedValue));
       // }

        

        //public void btnInsertNewItem_Onclick(object sender, EventArgs e)
        //{
        //    int intBookingProductId = int.Parse(this.qBookingProductID);



        //    BookingItemDisplay cBookingItem = new BookingItemDisplay();
        //    //cBookingItem.InsertBookingItem_BookingProcess();
        //    //cOption.InsertOption(s

        //}
    }
}