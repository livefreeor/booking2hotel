using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.Reviews;
using System.Text;
using Hotels2thailand.Production;
using Hotels2thailand.Front;

namespace Hotels2thailand.UI
{
    public partial class admin_review_site_admin_detail : Hotels2BasePage
    {
        public string reviewID
        {
            get { return Request.QueryString["review_id"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {

                Country cCountry = new Country();
                dropCountry.DataSource = cCountry.GetCountryAll();
                dropCountry.DataTextField = "Value";
                dropCountry.DataValueField = "Key";
                dropCountry.DataBind();

                ReviewWebsite Review = new ReviewWebsite();
                chkVisit.DataSource = Review.SelectReviewVisit(1);
                chkVisit.DataTextField = "Value";
                chkVisit.DataValueField = "Key";
                chkVisit.DataBind();

                Hotels2thailand.UI.Hotels2BasePage basePage = new Hotels2thailand.UI.Hotels2BasePage();
                dropNight.DataSource = basePage.dicGetNumber(60);
                dropNight.DataTextField = "Value";
                dropNight.DataValueField = "Key";
                dropNight.DataBind();

                ListItem newItem = new ListItem("Select One", "0");
                dropNight.Items.Insert(0, newItem);

                radioOften.DataSource = Review.SelectReviewOften(1);
                radioOften.DataTextField = "Value";
                radioOften.DataValueField = "Key";
                radioOften.DataBind();


                radiofindus.DataSource = Review.SelectReviewFindUs(1);
                radiofindus.DataTextField = "Value";
                radiofindus.DataValueField = "Key";
                radiofindus.DataBind();

                chkenhance.DataSource = Review.SelectReviewenhances(1);
                chkenhance.DataTextField = "Value";
                chkenhance.DataValueField = "Key";
                chkenhance.DataBind();

                chkenhanceProduct.DataSource = Review.SelectReviewenhancesProduct(1);
                chkenhanceProduct.DataTextField = "Value";
                chkenhanceProduct.DataValueField = "Key";
                chkenhanceProduct.DataBind();

                chkProblem.DataSource = Review.SelectReviewProblem(1);
                chkProblem.DataTextField = "Value";
                chkProblem.DataValueField = "Key";
                chkProblem.DataBind();

                Hotels2BasePage cBasePage = new Hotels2BasePage();


                birthday_day.DataSource = cBasePage.dicGetNumber(31);
                birthday_day.DataTextField = "Value";
                birthday_day.DataValueField = "Key";
                birthday_day.DataBind();

                birthday_year.DataSource = cBasePage.dicGetYear();
                birthday_year.DataTextField = "Value";
                birthday_year.DataValueField = "Key";
                birthday_year.DataBind();

                Customer cCustomer = new Customer();
                dropOcc.DataSource = cCustomer.getDicOccupation();
                dropOcc.DataTextField = "Value";
                dropOcc.DataValueField = "Key";
                dropOcc.DataBind();

                ReviewWebsite cReviewSite = new ReviewWebsite();
                cReviewSite = cReviewSite.getReviewListById(int.Parse(this.reviewID));

                foreach (ListItem item in chkVisit.Items)
                {
                    foreach (KeyValuePair<byte, string> select in cReviewSite.ReviewVisitList)
                    {
                        if (item.Value == select.Value)
                        {
                            item.Selected = true;
                        }
                    }
                }

                TxtvisitOther.Text = cReviewSite.visit_other_suggest;
                dropNight.SelectedValue = cReviewSite.visit_num_day.ToString();

                radioOften.SelectedValue = cReviewSite.often_visit_id.ToString() ;
                radioIsEverInter.SelectedValue = cReviewSite.book_before.ToString();
                
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "speed", "<script>StarValue('speed','" + cReviewSite.site_interaction_speed + "');</script>", false);
                txtSpeed.Text = cReviewSite.site_interaction_speed_suggest;
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "enquiry", "<script>StarValue('enquiry','" + cReviewSite.site_enquiry_response + "');</script>", false);
                txtresponse.Text = cReviewSite.site_enquiry_response_suggest;
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "Ability", "<script>StarValue('Ability','" + cReviewSite.site_problem_solution + "');</script>", false);
                txtability.Text = cReviewSite.site_problem_solution_suggest;
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "Knowledge", "<script>StarValue('Knowledge','" + cReviewSite.site_hotel_information + "');</script>", false);
                txtknow.Text = cReviewSite.site_hotel_information_suggest;

                radiofindus.SelectedValue = cReviewSite.find_us_id.ToString();
                txtfindusother.Text = cReviewSite.find_us_other;

                foreach (ListItem item in chkenhance.Items)
                {
                    foreach (KeyValuePair<byte, string> select in cReviewSite.ReviewEnhance)
                    {
                        if (item.Value == select.Value)
                        {
                            item.Selected = true;
                        }
                    }
                }
                txtchkenhance.Text = cReviewSite.enhance_other;


                foreach (ListItem item in chkenhanceProduct.Items)
                {
                    foreach (KeyValuePair<byte, string> select in cReviewSite.ReviewEnhanceProduct)
                    {
                        if (item.Value == select.Value)
                        {
                            item.Selected = true;
                        }
                    }
                }
                txtchkenhanceProduct1.Text = cReviewSite.product_destination_suggest;
                txtchkenhanceProduct2.Text = cReviewSite.product_other_suggest;
                

                foreach (ListItem item in chkProblem.Items)
                {
                    foreach (KeyValuePair<byte, string> select in cReviewSite.ReviewProblem)
                    {
                        if (item.Value == select.Value)
                        {
                            item.Selected = true;
                        }
                    }
                }
                txtchkProblem1.Text = cReviewSite.problem_hotel_found_suggest;
                txtchkProblem2.Text = cReviewSite.problem_other_suggest;

                radiobackAgain.SelectedValue = cReviewSite.rebook.ToString();
                txtNo.Text = cReviewSite.rebook_reason;
                txtComment.Text = cReviewSite.comment;
                DateTime dDate = new DateTime();

                cCustomer = cCustomer.GetCustomerbyId(cReviewSite.CustomerId);
                txtName.Text = cCustomer.FullName;
                txtEmail.Text = cCustomer.Email;
                if (cCustomer.Address != null)
                    txtaddress.Text = cCustomer.Address;
                if (cCustomer.OccupationID != null)
                    dropOcc.SelectedValue = cCustomer.OccupationID.ToString();
                if (cCustomer.DateBirth != null)
                {
                    dDate = (DateTime)cCustomer.DateBirth;
                    birthday_month.SelectedValue = dDate.Month.ToString();
                    birthday_day.SelectedValue = dDate.Day.ToString();
                    birthday_year.SelectedValue = dDate.Year.ToString();

                }

                dropCountry.SelectedValue = cCustomer.CountryID.ToString();
                txtTel.Text = cCustomer.CusPhone;
                txtmobile.Text = cCustomer.CusMobile;
                txtFax.Text = cCustomer.CusFax;

                radioMail.SelectedValue = cCustomer.Mail.ToString();


                //Hotels2thailand.Front.Booking cBooking = new Front.Booking();
                //GvBookingLIst.DataSource = cBooking.GetBookingByCustomerId(cReviewSite.CustomerId);
                //GvBookingLIst.DataBind();
            }
        }

        //public void btnCloseReview_Onclick(object sender, EventArgs e)
        //{
        //    ReviewWebsite cReviewSite = new ReviewWebsite();
        //    bool update = cReviewSite.CloseReviewSite(int.Parse(this.reviewID));

        //    if (update)
        //        Response.Redirect("review_site_admin_list.aspx");
        //}


        
    }
}