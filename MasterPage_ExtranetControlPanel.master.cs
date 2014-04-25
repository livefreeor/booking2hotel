using System;
using System.Collections;

using System.Collections.Specialized;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hotels2thailand.UI;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Configuration;
using Hotels2thailand.Staffs;
using Hotels2thailand;

namespace Hotels2thailand.UI.MasterPage
{
    public partial class Masterpage_MasterPage_Control_Panel : System.Web.UI.MasterPage
    {

        private string Member_module
        {
            get { return ConfigurationManager.AppSettings["hotel_member_list"].ToString(); }
        }

        private string Newsletter_module
        {
            get { return ConfigurationManager.AppSettings["hotel_newsletter_list"].ToString(); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           // SiteMap.CurrentNode.ParentNode.Url = "hhhh";

            if (!this.Page.IsPostBack)
            {

                string[] arrMemberACtive = { "3449" };
              
                //if ((this.Page as Hotels2BasePageExtra).CurrentStaffobj.Cat_Id != 2)
                //{
                    
                //    panel_member.Visible = false;
                //}

                Production.ProductContent cProductcontents = new Production.ProductContent();
                int intProduct = 0;



                Staffs.StaffProduct_Extra cStaffProductExtra = new Staffs.StaffProduct_Extra();
                Staffs.StaffSessionAuthorize cStaffSession = new Staffs.StaffSessionAuthorize();
                List<object> listProductList = cStaffProductExtra.getProductByStaffId(cStaffSession.CurrentStaffId, 1);
                dropHotelStaffExtranet.DataSource = listProductList;
                dropHotelStaffExtranet.DataTextField = "ProductTitle";
                dropHotelStaffExtranet.DataValueField = "ProductID";
                dropHotelStaffExtranet.DataBind();
                   
                

                if (!string.IsNullOrEmpty(Request.QueryString["pid"]) && !string.IsNullOrEmpty(Request.QueryString["supid"]))
                {

                    string AppendQueryString = "?pid=" + Request.QueryString["pid"] + "&supid=" + Request.QueryString["supid"];
                    lnDash.NavigateUrl = lnDash.NavigateUrl + AppendQueryString;

                    lnStaffList.NavigateUrl = lnStaffList.NavigateUrl + AppendQueryString;
                    lnStaffAddStaff.NavigateUrl = lnStaffAddStaff.NavigateUrl + AppendQueryString;
                    lnloadtariff.NavigateUrl = lnloadtariff.NavigateUrl + AppendQueryString;
                    lnRoomControl.NavigateUrl = lnRoomControl.NavigateUrl + AppendQueryString;
                    lnRate.NavigateUrl = lnRate.NavigateUrl + AppendQueryString;
                    lntransfer.NavigateUrl = lntransfer.NavigateUrl + AppendQueryString;
                    lnExtraBed.NavigateUrl = lnExtraBed.NavigateUrl + AppendQueryString;
                    lnGala.NavigateUrl = lnGala.NavigateUrl + AppendQueryString;
                    lnMeal.NavigateUrl = lnMeal.NavigateUrl + AppendQueryString;

                    lnAllotment.NavigateUrl = lnAllotment.NavigateUrl + AppendQueryString;
                    lnAllotEdit.NavigateUrl = lnAllotEdit.NavigateUrl + AppendQueryString;
                    lnPromotion.NavigateUrl = lnPromotion.NavigateUrl + AppendQueryString;
                    lnProList.NavigateUrl = lnProList.NavigateUrl + AppendQueryString;
                    lnmin.NavigateUrl = lnmin.NavigateUrl + AppendQueryString;
                   //lnRatePlan.NavigateUrl = lnRatePlan.NavigateUrl + AppendQueryString;
                    lnBookingreport.NavigateUrl = lnBookingreport.NavigateUrl + AppendQueryString;
                    lnRoom.NavigateUrl = lnRoom.NavigateUrl + AppendQueryString; 
                    lnBookingList.NavigateUrl = lnBookingList.NavigateUrl + AppendQueryString;
                    lnBookingSearch.NavigateUrl = lnBookingSearch.NavigateUrl + AppendQueryString;
                    lndeposit.NavigateUrl = lndeposit.NavigateUrl + AppendQueryString;
                    //lnAcknowledge.NavigateUrl = lnAcknowledge.NavigateUrl + AppendQueryString;


                    lnmemberlist.NavigateUrl = lnmemberlist.NavigateUrl + AppendQueryString;
                    lnmemberprice.NavigateUrl = lnmemberprice.NavigateUrl + AppendQueryString;
                   // lnmemberbenefit.NavigateUrl = lnmemberbenefit.NavigateUrl + AppendQueryString;

                    lnHolidays.NavigateUrl = lnHolidays.NavigateUrl + AppendQueryString;

                    lnPackageManage.NavigateUrl = lnPackageManage.NavigateUrl + AppendQueryString;
                    lnPackageList.NavigateUrl = lnPackageList.NavigateUrl + AppendQueryString;

                    lnReview.NavigateUrl = lnReview.NavigateUrl + AppendQueryString;
                    lnReviewAdd.NavigateUrl = lnReviewAdd.NavigateUrl + AppendQueryString;


                    hlNewsCreate.NavigateUrl = hlNewsCreate.NavigateUrl + AppendQueryString;
                    hlSentbox.NavigateUrl = hlSentbox.NavigateUrl + AppendQueryString + "&temp=" + 1;
                    hloutbox.NavigateUrl = hloutbox.NavigateUrl + AppendQueryString + "&temp=" + 2;

                    //nlDrafts.NavigateUrl = nlDrafts.NavigateUrl + AppendQueryString + "&temp=" + 5;
                    nlDel.NavigateUrl = nlDel.NavigateUrl + AppendQueryString + "&temp=" + 6;

                    //lnNewsletteraull.NavigateUrl = lnNewsletteraull.NavigateUrl + AppendQueryString + "&mc=5";
                    //lnNewsletter_mem.NavigateUrl = lnNewsletter_mem.NavigateUrl + AppendQueryString + "&mc=7";


                    intProduct = int.Parse(Request.QueryString["pid"]);

                    cProductcontents = cProductcontents.GetProductContentById(intProduct, 1);
                    if (cProductcontents != null)
                        lblProductTitle.Text = cProductcontents.Title;
                    else
                    {
                        Production.Product cProduct = new Production.Product();
                        lblProductTitle.Text = cProduct.GetProductById(intProduct).Title;
                    }

                    lblProductTitle.Visible = true;
                    paneldrop.Visible = false;
                    paneltxt.Visible = true;
                    btnchangehotel.Visible = false;


                    CheckMenuHotelModule();
                  
                }
                else
                {
                    paneldrop.Visible =  true;
                    paneltxt.Visible =  false;
                    Staffs.StaffSessionAuthorize cSessionAuthor = new Staffs.StaffSessionAuthorize();

                    if (cStaffProductExtra.getProductByStaffId(cSessionAuthor.CurrentStaffId).Count > 0)
                    {
                        if (Request.Cookies["SessionKey"] != null)
                        {
                            
                            
                            intProduct = int.Parse(Request.Cookies["SessionKey"]["ProductActive"]);

                            StaffAuthorizeExtra cStaffAuthorize = new StaffAuthorizeExtra();
                            cStaffAuthorize = cStaffAuthorize.GetStaffAuthorize(cSessionAuthor.CurrentStaffId);
                            if (cStaffAuthorize.AuthorizeId != 1)
                            {
                                ScriptManager.RegisterStartupScript(this, Page.GetType(), "1", "<script>hiddenStaffAdminMenu('menu_staff_admin');</script>", false);
                                
                            }

                            
                            dropHotelStaffExtranet.SelectedValue = intProduct.ToString();

                            CheckMenuHotelModule();
                        }
                    }
                    else
                    {
                        //lblProductTitle.Text = "Please Contact Admin";
                    }
                    
                }
            }
        }

        public void CheckMenuHotelModule()
        {
            //Newsletter_module
            string qProductId = (this.Page as Hotels2BasePageExtra).CurrentProductActiveExtra.ToString();
            foreach (string product in this.Member_module.Split(','))
            {
                if (qProductId == product)
                {
                    panel_member.Visible = true;
                    break;
                }
            }

            foreach (string product in this.Newsletter_module.Split(','))
            {
                if (qProductId == product)
                {
                    panel_Newsletter.Visible = true;
                    break;
                }

            }
        }


        public void btnchangehotel_OnClick(object sender, EventArgs e)
        {

            //Response.Write(dropHotelStaffExtranet.SelectedValue);
            //Response.End();

            int intProduct = int.Parse(dropHotelStaffExtranet.SelectedValue);


            StaffProduct_Extra cStaffProduct = new StaffProduct_Extra();
            short shrSupId = cStaffProduct.GetSupplierIdByProductId(intProduct);

            StaffSessionAuthorize.UpdateCookieSessionExtra_ProductActive(dropHotelStaffExtranet.SelectedValue, shrSupId.ToString());


            //
            //dropHotelStaffExtranet.SelectedValue;
            //

            Response.Redirect("/extranet/mainextra.aspx");
             
        }


        
       
}
}
