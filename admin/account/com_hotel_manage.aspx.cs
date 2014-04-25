using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Account;
using Hotels2thailand.Suppliers;
using Hotels2thailand.Production;
using Hotels2thailand.Booking;
using Hotels2thailand.Front;


namespace Hotels2thailand.UI
{
    public partial class admin_account_com_hotel_manage : Hotels2BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Flat_hotelList cHotelManage = new Flat_hotelList();
                IList<object> iListHotel = cHotelManage.getProductListComHotelManage();
                gvHotelLists.DataSource = iListHotel;
                gvHotelLists.DataBind();
                if (iListHotel.Count > 0)
                    gvHotelLists.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        protected void gvHotelLists_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
}
}