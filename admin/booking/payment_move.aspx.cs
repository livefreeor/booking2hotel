using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Hotels2thailand.Booking;

namespace Hotels2thailand.UI
{
    public partial class admin_payment_move : Hotels2BasePage
    {
       
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["move"]))
                {
                    int intBookingIdFrom = int.Parse(Request.QueryString["move"].Split(';')[0]);
                    int intBookingIdTo = int.Parse(Request.QueryString["move"].Split(';')[1]);
                    Display_PayMent(intBookingIdFrom, intBookingIdTo);

                    txtBookingFrom.Text = Request.QueryString["move"].Split(';')[0];
                    txtBookingTo.Text = Request.QueryString["move"].Split(';')[1];

                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "4", "<script>DarkmanPopUpAlert('400','Payment money Move Completed!!<br/> Please Check Booking Activity');</script>", false);
                }
                
            }
        }

        public void Display_PayMent(int intBookingIdFrom, int intBookingIdTo)
        {
            BookingPaymentDisplay cBookingPayment = new BookingPaymentDisplay();
            

            gridPaymentFrom.DataSource = cBookingPayment.GEtPaymentByBookingIdBookingMove(intBookingIdFrom);
            gridPaymentFrom.DataBind();
            gridPaymentTo.DataSource = cBookingPayment.GEtPaymentByBookingIdBookingMove(intBookingIdTo);
            gridPaymentTo.DataBind();


            BookingdetailDisplay cBookingDetailFrom = new BookingdetailDisplay();
            BookingdetailDisplay cBookingDetailTo = new BookingdetailDisplay();
            cBookingDetailFrom = cBookingDetailFrom.GetBookingDetailListByBookingId(intBookingIdFrom);
            cBookingDetailTo = cBookingDetailTo.GetBookingDetailListByBookingId(intBookingIdTo);

            lblBookingIdFrom.Text = txtBookingFrom.Text.Trim();
            lblBookingIdTo.Text = txtBookingTo.Text.Trim();


            if (cBookingDetailFrom.AffSiteId.HasValue)
                imgAffFrom.ImageUrl = "/images/true_b.png";
            else
                imgAffFrom.ImageUrl = "/images/false_b.png";


            if (cBookingDetailTo.AffSiteId.HasValue)
                imgaffTo.ImageUrl = "/images/true_b.png";
            else
                imgaffTo.ImageUrl = "/images/false_b.png";

            panelPayment_From.Visible = true;
            panelPaymeny_to.Visible = true;
            panelHeadrom.Visible = true;
            panelBookingTo.Visible = true;

            panelbtnMove.Visible = true;
        }
        public void Display_Payment_OnClick(object sender, EventArgs e)
        {

            int intBookingIdFrom = int.Parse(txtBookingFrom.Text.Trim());
            int intBookingIdTo = int.Parse(txtBookingTo.Text.Trim());

            Display_PayMent(intBookingIdFrom, intBookingIdTo);
        }

        public void gridPaymentFrom_OnrowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DateTime? dDateConfirmPayment = (DateTime?)DataBinder.Eval(e.Row.DataItem, "ConfirmPayment");
                DateTime? dDateConfirmSettle = (DateTime?)DataBinder.Eval(e.Row.DataItem, "ConfirmSettle");

                int intPaymentId = (int)gridPaymentFrom.DataKeys[e.Row.RowIndex].Value;

                Label lblPayment = e.Row.Cells[3].FindControl("lblIsPayment") as Label;
                Label lblSettle = e.Row.Cells[4].FindControl("lblIsSettle") as Label;

                if (dDateConfirmPayment.HasValue)
                {
                    DateTime dDateConfirm = (DateTime)dDateConfirmPayment;
                    lblPayment.Text = dDateConfirm.ToString("dd-MMM-yyyy");
                }
                else
                {
                    lblPayment.Text = "<img src=\"/images/false.png\" />";
                }

                if (dDateConfirmSettle.HasValue)
                {
                    DateTime dDateSettle = (DateTime)dDateConfirmSettle;
                    lblSettle.Text = dDateSettle.ToString("dd-MMM-yyyy");
                }
                else
                    lblSettle.Text = "<img src=\"/images/false.png\" />";
            }
        }


        public void gridPaymentTo_OnrowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DateTime? dDateConfirmPayment = (DateTime?)DataBinder.Eval(e.Row.DataItem, "ConfirmPayment");
                DateTime? dDateConfirmSettle = (DateTime?)DataBinder.Eval(e.Row.DataItem, "ConfirmSettle");

                int intPaymentId = (int)gridPaymentTo.DataKeys[e.Row.RowIndex].Value;

                Label lblPayment = e.Row.Cells[3].FindControl("lblIsPayment") as Label;
                Label lblSettle = e.Row.Cells[4].FindControl("lblIsSettle") as Label;

                if (dDateConfirmPayment.HasValue)
                {
                    DateTime dDateConfirm = (DateTime)dDateConfirmPayment;
                    lblPayment.Text = dDateConfirm.ToString("dd-MMM-yyyy");
                }
                else
                    lblPayment.Text = "<img src=\"/images/false.png\" />";

                if (dDateConfirmSettle.HasValue)
                {
                    DateTime dDateSettle = (DateTime)dDateConfirmSettle;
                    lblSettle.Text = dDateSettle.ToString("dd-MMM-yyyy");
                }
                else
                    lblSettle.Text = "<img src=\"/images/false.png\" />";
                    
            }
        }

        public void btnMoveMoney_OnClick(object sender, EventArgs e)
        {

            int intBookingIdFrom = int.Parse(txtBookingFrom.Text.Trim());
            int intBookingIdTo = int.Parse(txtBookingTo.Text.Trim());

            BookingdetailDisplay cBookingDetailFrom = new BookingdetailDisplay();
            cBookingDetailFrom = cBookingDetailFrom.GetBookingDetailListByBookingId(intBookingIdFrom);


            BookingPaymentDisplay cBookingPayment = new BookingPaymentDisplay();


            foreach (GridViewRow GvRow in gridPaymentFrom.Rows)
            {
                CheckBox GvChck = gridPaymentFrom.Rows[GvRow.RowIndex].Cells[0].FindControl("chkboxSelect") as CheckBox;
                if (GvChck.Checked)
                {

                    int PaymentId = (int)gridPaymentFrom.DataKeys[GvRow.RowIndex].Value;
                    cBookingPayment.BookingPaymenyMove(intBookingIdFrom, intBookingIdTo, cBookingDetailFrom.AffSiteId, PaymentId);
                }
            }

            BookingActivityDisplay cActivity = new BookingActivityDisplay();
            cActivity.InsertNewActivityBooking(intBookingIdTo, "move from booking id: " + intBookingIdFrom);

            cActivity.InsertNewActivityBooking(intBookingIdFrom, "move to booking id: " + intBookingIdTo);


            Response.Redirect(Request.Url.ToString().Split('?')[0] + "?move=" + txtBookingFrom.Text.Trim() + ";" + txtBookingTo.Text);
            //bool Ismove = cBookingPayment.BookingPaymenyMove(intBookingIdFrom, intBookingIdTo, cBookingDetailFrom.AffSiteId, );

        }
    }
}