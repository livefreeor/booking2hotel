using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Hotels2thailand.Production;
using Hotels2thailand.Suppliers;
using Hotels2thailand.Newsletters;


using System.Data.SqlClient;

namespace Hotels2thailand.UI
{
    public partial class admin_product_product : Hotels2BasePage
    {

        

     
        public string strAccQueryString
        {
            get
            {
                return Request.QueryString["acid"];
            }
        }

       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                string qAppend = "&pid=" + this.qProductId + "&supid=" + this.qSupplierId + "&pdcid=" + this.qProductCat;

                lnkhotel.NavigateUrl = "product.aspx?page=sta" + qAppend;
                lnkAdvance.NavigateUrl = "product.aspx?page=av" + qAppend;
                lnkPI.NavigateUrl = "product.aspx?page=pi" + qAppend;
                lnkFTP.NavigateUrl = "product.aspx?page=ft" + qAppend;
                lnkContact.NavigateUrl = "product.aspx?page=con" + qAppend;
                lnkCommission.NavigateUrl = "product.aspx?page=com" + qAppend;
                lnkSales.NavigateUrl = "product.aspx?page=sal" + qAppend;
                lnkMail.NavigateUrl = "product.aspx?page=mal" + qAppend;

                //lnkPicture.NavigateUrl = "product.aspx?page=sal" + this.AppendCurrentQueryString();

                lnkPicture.NavigateUrl = "~/admin/product/product_picture.aspx?" + this.AppendCurrentQueryString();

                if (!string.IsNullOrEmpty(this.qProductId))
                {
                    panelMenu.Visible = true;

                    if (string.IsNullOrEmpty(qProductPage))
                    {
                        dropDesBind();
                        dropStatus();
                        panelStatus.Visible = true;
                        panelAdvance.Visible = false;
                        panelFTP.Visible = false;
                        panelContact.Visible = false;
                        panelCom.Visible = false;
                        panelSales.Visible = false;
                        panelPI.Visible = false;
                        panelMailSetting.Visible = false;
                        ProductStatusDatabind();
                    }

                    if (qProductPage == "sta")
                    {
                        dropDesBind();
                        dropStatus();
                        panelStatus.Visible = true;
                        panelAdvance.Visible = false;
                        panelFTP.Visible = false;
                        panelContact.Visible = false;
                        panelCom.Visible = false;
                        panelSales.Visible = false;
                        panelPI.Visible = false;
                        panelMailSetting.Visible = false;
                        ProductStatusDatabind();
                    }

                    if (qProductPage == "pi")
                    {
                        dropDesBind();
                        dropStatus();
                        panelStatus.Visible = false;
                        panelAdvance.Visible = false;
                        panelFTP.Visible = false;
                        panelContact.Visible = false;
                        panelCom.Visible = false;
                        panelSales.Visible = false;
                        panelPI.Visible = true;
                        panelMailSetting.Visible = false;
                        PaymentDataBind();
                    }

                    if (qProductPage == "av")
                    {
                        panelStatus.Visible = false;
                        panelAdvance.Visible = true;
                        panelFTP.Visible = false;
                        panelContact.Visible = false;
                        panelCom.Visible = false;
                        panelSales.Visible = false;
                        panelPI.Visible = false;
                        panelMailSetting.Visible = false;
                        ProductAdvanceDatabind();
                    }

                    if (qProductPage == "ft")
                    {
                        panelStatus.Visible = false;
                        panelAdvance.Visible = false;
                        panelFTP.Visible = true;
                        panelContact.Visible = false;
                        panelCom.Visible = false;
                        panelSales.Visible = false;
                        panelPI.Visible = false;
                        panelMailSetting.Visible = false;
                        FtpBind();
                    }
                    if (qProductPage == "con")
                    {
                        panelStatus.Visible = false;
                        panelAdvance.Visible = false;
                        panelFTP.Visible = false;
                        panelContact.Visible = true;
                        panelCom.Visible = false;
                        panelSales.Visible = false;
                        panelPI.Visible = false;
                        panelMailSetting.Visible = false;

                        GvDepartmentDataBind();
                        DropDepDataBind();
                        dropPhontCatDataBind();

                        dropcatInsertDataBind();

                        panelContactDetailDataBind();
                    }
                    if (qProductPage == "com")
                    {
                        panelStatus.Visible = false;
                        panelAdvance.Visible = false;
                        panelFTP.Visible = false;
                        panelContact.Visible = false;
                        panelCom.Visible = true;
                        panelSales.Visible = false;
                        panelPI.Visible = false;
                        panelMailSetting.Visible = false;
                        ComBind();
                        GetActivityList();
                    }


                    if (qProductPage == "sal")
                    {
                        panelStatus.Visible = false;
                        panelAdvance.Visible = false;
                        panelFTP.Visible = false;
                        panelContact.Visible = false;
                        panelCom.Visible = false;
                        panelSales.Visible = true;
                        panelPI.Visible = false;
                        panelMailSetting.Visible = false;
                        SaleDataBind();
                    }

                    if (qProductPage == "mal")
                    {
                        panelStatus.Visible = false;
                        panelAdvance.Visible = false;
                        panelFTP.Visible = false;
                        panelContact.Visible = false;
                        panelCom.Visible = false;
                        panelSales.Visible = false;
                        panelPI.Visible = false;
                        panelMailSetting.Visible = true;

                        MailSettingDataBind();
                    }

                }
                else
                {

                    radioStatus.SelectedValue = "True";
                    radioActive.SelectedValue = "False";
                    panelStatus.Visible = true;
                    dropDesBind();
                    dropStatus();

                    Product cProduct = new Product();

                    txtHotelCode.Text = cProduct.GetLatestProductCode(29, short.Parse(dropDes.SelectedValue));
                }

                if (dropManage.SelectedValue == "2")
                {
                    checkmailNotice.Visible = true;
                }
                else
                {
                    checkmailNotice.Visible = false;
                }

            }
            
        }

        public void FtpBind()
        {
            ProductBookingEngine_FTP cFtp = new ProductBookingEngine_FTP();

            GVFTP.DataSource = cFtp.GetProductFtp(int.Parse(this.qProductId));
            GVFTP.DataBind();
        }


        public void ComBind()
        {

            int intProductID = int.Parse(this.qProductId);
            ProductBookingEngineCommission cProductCom = new ProductBookingEngineCommission();
            dropCat.DataSource = cProductCom.GetDicCom_cat();
            dropCat.DataTextField = "Value";
            dropCat.DataValueField = "Key";
            dropCat.DataBind();
            //byte bytComCat = byte.Parse(dropCat.SelectedValue);

            ProductBookingEngineCommission cCom = new ProductBookingEngineCommission();
            cCom = cCom.GetCommission(intProductID);

            if (cCom != null)
            {

                
                dateExpired.DateStart = cCom.ContractExpired;
                dateExpired.DataBind();

                dropCat.SelectedValue = cCom.CatId.ToString();

                txtComVal.Text = cCom.Commission.ToString("#,##0.###");
                date_Created.Text = "Commission Created Since: " + cCom.Date_Submit.ToString("ddd, dd MMM, yyyy");



                if (cCom.CatId == 3)
                {
                    revenue_com_step.Visible = true;
                    com_val.Visible = false;

                    ComRevenue cRevenue = new ComRevenue();
                    IList<object> ListRevenue = cRevenue.GetRevenueList(cCom.RevenueID);
                    ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
                    if (mpContentPlaceHolder != null)
                    {

                        int count = 1;

                        foreach (ComRevenue rev in ListRevenue)
                        {
                            TextBox txtREvStart = (TextBox)mpContentPlaceHolder.FindControl("txtPricerange" + count);
                            TextBox txtREvCom = (TextBox)mpContentPlaceHolder.FindControl("Comreal" + count);
                            txtREvStart.Text = rev.revenueEnd.ToString("#,###.##");
                            txtREvCom.Text = rev.Commssion.ToString("#,###.##");

                            count = count + 1;
                        }
                    }


                }
                else
                {
                    revenue_com_step.Visible = false;
                    com_val.Visible = true;
                }

            }
            else
            {
                revenue_com_step.Visible = false;
                com_val.Visible = true;
            }

            
        }

        public void GVFTP_OnrowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        public void dropDesBind()
        {
            Destination cDestination = new Destination();
            dropDes.DataSource = cDestination.GetDestinationAll();
            dropDes.DataTextField = "Value";
            dropDes.DataValueField = "Key";
            dropDes.DataBind();
        }


        public void dropStatus()
        {
            Status cStatus = new Status();
            dropacStatus.DataSource = cStatus.GetStatusProductByCatId();
            dropacStatus.DataTextField = "Value";
            dropacStatus.DataValueField = "Key";
            dropacStatus.DataBind();
        }

        public void ProductStatusDatabind()
        {
            Product cProduct = new Product();
            ProductContent cProductContent = new ProductContent();
            int inProductid = int.Parse(this.qProductId);

            cProduct = cProduct.GetProductById(inProductid);
            
            cProductContent = cProductContent.GetProductContentById(inProductid,1);
            txtHotelName.Text = cProductContent.Title;
            txtComment.Text = cProduct.Comment;
            txtHotelCode.Text = cProduct.ProductCode;
            txtAddress.Text = cProductContent.Address ;
           // txtcontactEmail.Text = cProduct.ProductEmail;
            txtPhone.Text = cProduct.ProductPhone;
            txtEmailSup.Text = cProduct.ProductEmail;
            //txtLat.Text = cProduct.Latitude;
            //txtLong.Text = cProduct.Longitude;


            radioStatus.SelectedValue = cProduct.Status.ToString();

            radioActive.SelectedValue = cProduct.ExtranetActive.ToString();
            

            dropDes.SelectedValue = cProduct.DestinationID.ToString();
            dropacStatus.SelectedValue = cProduct.StatusID.ToString();


        }

        public void ProductAdvanceDatabind()
        {
            ProductBookingEngine cProductEngine = new ProductBookingEngine();
            dropBookingType.DataSource = cProductEngine.getdicBookingPaymentType();
            dropBookingType.DataTextField = "Value";
            dropBookingType.DataValueField = "Key";
            dropBookingType.DataBind();

            ListItem item = new ListItem("Please select bookign type", "0");
            dropBookingType.Items.Insert(0, item);

            dropCurrency.DataSource = cProductEngine.getdicCurreny();
            dropCurrency.DataTextField = "Value";
            dropCurrency.DataValueField = "Key";
            dropCurrency.DataBind();
            dropCurrency.SelectedValue = "1";

            Hotels2thailand.Booking.Gateway cGateway = new Booking.Gateway();
            dropGateway.DataSource = cGateway.getGateWayListProduct();
            dropGateway.DataTextField = "Value";
            dropGateway.DataValueField = "Key";
            dropGateway.DataBind();

            
            dropSale.DataSource = cProductEngine.getdicSales();
            dropSale.DataTextField = "Value";
            dropSale.DataValueField = "Key";
            dropSale.DataBind();
            dropSale.SelectedValue = "2";

            cProductEngine = cProductEngine.GetProductbookingEngine(int.Parse(this.qProductId));
            if (cProductEngine != null)
            {
               
                dropBookingType.SelectedValue = cProductEngine.BookingTypeID.ToString();
                dropCurrency.SelectedValue = cProductEngine.CurrencyID.ToString();
                dropGateway.SelectedValue = cProductEngine.GateWayId.ToString();
                dropSale.SelectedValue = cProductEngine.SalesID.ToString();
                dropManage.SelectedValue = cProductEngine.ManageId.ToString(); 
                dropVat.SelectedValue = cProductEngine.IsVat_Show.ToString();
                txtWebsiteName.Text = cProductEngine.WebsiteName.Trim();
                chkIsB2b.Checked = cProductEngine.Is_B2b;
                txtB2bMap.Text = cProductEngine.B2bMap;

                dropB2bCat.SelectedValue = cProductEngine.B2bCat.ToString();
                //txtEmail.Text = cProductEngine.Email.Trim();
                //txtEmailPass.Text = cProductEngine.EmailPass.Trim();
                txtfolder.Text = cProductEngine.Folder.Trim();
                txtMerchant.Text = cProductEngine.MerchatID.Trim();
                txtTeminal.Text = cProductEngine.TerminalID.Trim();
                txtUrl.Text = cProductEngine.UrlReturn.Trim();
                txtUpdate.Text = cProductEngine.URLUpdate.Trim();
                txtRedirect.Text = cProductEngine.URLSiteRedirect.Trim();
                txtcontactEmail.Text = cProductEngine.EmailContactMail.Trim();
                checkmailNotice.Checked = cProductEngine.IsMailNotice;


                txtProfileID.Text = cProductEngine.ProfileID;
                txtAccessCode.Text = cProductEngine.AccessKey;
                txtSecretCode.Text = cProductEngine.SecretKey;

            }
                


        }

        public void btnSaveStatus_Onclick(object sender, EventArgs e)
        {
            
            ProductBookingEngine cProduct = new ProductBookingEngine();
            int product_id = 0;

            product_id = cProduct.InsertOrUpdateProduct(txtHotelName.Text, txtHotelCode.Text.Trim(), txtAddress.Text, txtComment.Text, short.Parse(dropDes.SelectedValue), byte.Parse(dropacStatus.SelectedValue), bool.Parse(radioStatus.SelectedValue), bool.Parse(radioActive.SelectedValue), txtPhone.Text, "", "", txtEmailSup.Text);


           

            if (product_id > 1)
            {

                if (string.IsNullOrEmpty(this.qProductId))
                {
                    cProduct.InsertOrUpdateProductEngine(product_id, 1, 1, "", "", "", 1, "", "", "", "", "", "", 1, 1, true, "", true,false,"",29,"","","");
                    Response.Redirect("product.aspx?pid=" + product_id + this.AppendCurrentQueryString());
                }
                else
                    Response.Redirect(Request.Url.ToString());
            }
        }


        public void btnSaveAdvance_Onclick(object sender, EventArgs e)
        {
            ProductBookingEngine cProduct = new ProductBookingEngine();
            int intProductId = int.Parse(Request.QueryString["pid"]);
            int ret = 0;
            cProduct.InsertOrUpdateProductEngine(intProductId, byte.Parse(dropBookingType.SelectedValue), byte.Parse(dropGateway.SelectedValue), txtMerchant.Text, txtTeminal.Text, txtfolder.Text, byte.Parse(dropCurrency.SelectedValue), txtUrl.Text, txtUpdate.Text, txtRedirect.Text, "", "", txtWebsiteName.Text, byte.Parse(dropSale.SelectedValue), byte.Parse(dropManage.SelectedValue), bool.Parse(dropVat.SelectedValue), txtcontactEmail.Text, checkmailNotice.Checked, chkIsB2b.Checked, txtB2bMap.Text, byte.Parse(dropB2bCat.SelectedValue), txtAccessCode.Text.Trim(),txtSecretCode.Text.Trim(), txtProfileID.Text.Trim());

            //Response.Write(chkIsB2b.Checked);

            if(ret == 1)
                Response.Redirect(Request.Url.ToString());
        }

        protected void btnSaveFtp_Click(object sender, EventArgs e)
        {
            ProductBookingEngine_FTP cBookingFtp = new ProductBookingEngine_FTP();
            cBookingFtp.InsertFTp(int.Parse(this.qProductId),txtServer.Text, txtuser.Text, txtPass.Text, port.Text, com.Text,true);

            Response.Redirect(Request.Url.ToString());
        }
        protected void dropDes_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.qProductId))
            {
                Product cProduct = new Product();

                txtHotelCode.Text = cProduct.GetLatestProductCode(29, short.Parse(dropDes.SelectedValue));
            }
        }

        protected void dropManage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropManage.SelectedValue == "2")
            {
                checkmailNotice.Visible = true;
            }
            else
            {
                checkmailNotice.Visible = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            
           
            byte bytComCat = byte.Parse(dropCat.SelectedValue);
            int intProductId = int.Parse(this.qProductId);
            decimal decCom = 0;
            if (!string.IsNullOrEmpty(txtComVal.Text))
                decCom = decimal.Parse(txtComVal.Text);

            ProductBookingEngineCommission cCom = new ProductBookingEngineCommission
            {
                ProductID = intProductId,
                CatId = bytComCat,
                ContractExpired = dateExpired.GetDatetStart.Hotels2ThaiDateTime(),
                 Date_Submit = DateTime.Now.Hotels2ThaiDateTime(),
                  Commission = decCom
               // Commission = decimal.Parse();
            };

            int intRevId = 0;
            ProductBookingEngineCommission cGEtdata = cCom.GetCommission(intProductId);
            ComActivity cComAc = new ComActivity();
             ComActivityType type = 0;
            if (cGEtdata  == null)
            {
               intRevId =  cCom.InsertCom(cCom);
               
                 type = ComActivityType.AddnewCommission;
            }
            else
            {
                cCom.RevenueID = cGEtdata.RevenueID;
                
                cCom.UpdateCom(cCom);
                intRevId = cGEtdata.RevenueID;
                 type = ComActivityType.UpdateCommssion;
            }


            IList<ComRevenue> iLComAc = new List<ComRevenue>();
            if (bytComCat == 3)
            {
               ComRevenue cRev = new ComRevenue();
                cRev.DelAllCom(intRevId);
                ArrayList controlList = new ArrayList();
                AddControls(Page.Controls, controlList);

                int intCountPriceRangr = 0;
                //int intCountCom = 0;
                foreach (Control control in controlList)
                {
                    if (control is TextBox)
                    {
                        if (Regex.IsMatch(control.ID, "^txtPricerange"))
                            intCountPriceRangr = intCountPriceRangr + 1;
                    }
                }

                decimal decRevStart = 0;
                decimal decRevEnd = 0;
                decimal ComVal = 0;
                decimal decRevStartTmp = 0;

                for (int i = 1; i <= intCountPriceRangr; i++)
                {

                    if (!string.IsNullOrEmpty(((TextBox)b.Parent.FindControl("txtPricerange" + i)).Text) && !string.IsNullOrEmpty(((TextBox)b.Parent.FindControl("Comreal" + i)).Text))
                    {

                        decRevEnd = decimal.Parse(((TextBox)b.Parent.FindControl("txtPricerange" + i)).Text);
                        decRevStart = decRevStartTmp + 1; 

                        ComVal = decimal.Parse(((TextBox)b.Parent.FindControl("Comreal" + i)).Text);

                        ComRevenue cComREv = new ComRevenue
                        {
                            RevenueID = intRevId,
                            revenueStart = decRevStart,
                            revenueEnd = decRevEnd,
                            Commssion = ComVal,
                            Status = true
                        };
                        iLComAc.Add(cComREv);
                        cRev.InsertRevenue(cComREv);

                        decRevStartTmp = decRevEnd;
                    }

                    
                }
                    
            }

            cComAc.InsertAutoActivity(new ComActivity
            {
                 DateActivity = DateTime.Now.Hotels2ThaiDateTime(),
                 ProductId = intProductId,
                  StaffId = this.CurrentStaffId
                   
            }, type, cCom, iLComAc);


            Response.Redirect(Request.Url.ToString());
        }

        public void GetActivityList()
        {
            ComActivity cComAc = new ComActivity();
            int intProductId = int.Parse(this.qProductId);

            IList<object> iListData = cComAc.GetComActivity(intProductId);
            StringBuilder result = new StringBuilder();
           result.Append("<h4><img   src=\"../../images/content.png\" /> Commission Activity</h4>");
            //result.Append("<p class=\"contentheadedetail\">List Supplier of This Product, you can Change or Add Supplier to List</p><br />");
            result.Append("<table cellpadding=\"0\" cellspacing=\"2\" width=\"100%\" bgcolor=\"#d8dfea\" border=\"0\" style=\"text-align:center;\">");
            //result.Append("<tr style=\"background-color:#3f5d9d;color:#ffffff;font-weight:bold;height:20px;line-height:20px;\"><td colspan=\"4\" style=\"font-size:14px;font-weight:bold\">Product Activity</td></tr>");
            result.Append("<tr style=\"background-color:#3f5d9d;color:#ffffff;font-weight:bold;height:10px;line-height:10px;\"><td style=\"width:5%\">No.</td><td style=\"width:10%\">Staff Name</td><td style=\"width:85%;\">Detail</td></tr>");
            string strDateAc = string.Empty;

            int count = iListData.Count();

            foreach (ComActivity acITem in cComAc.GetComActivity(intProductId))
            {

               
                result.Append("<tr style=\"background-color:#ffffff; height:25px;\"><td>" + count + "</td><td>" + acITem.StaffName + "</td><td style=\"text-align:left;padding:0px 0px 2px 2px;\">" + acITem.Detail + "<br/><p style=\"margin:2px 0px 0px 0px;padding:2px 0px 0px 0px;font-size:10px;border:0px;  color:#3f5d9d; text-align:right;\">" + acITem.DateActivity.ToString("MMM dd, yyyy ; HH:mm ") + "</p></td></tr>");
                count = count - 1;
            }
          
            result.Append("</table><br/><br/>");

            activity.Text = result.ToString();

        }

        protected void btnSaveAc_Click(object sender, EventArgs e)
        {
            ComActivity cComAc = new ComActivity { 
             StaffId = this.CurrentStaffId,
             DateActivity = DateTime.Now.Hotels2ThaiDateTime(),
             Detail = txt_activity.Text,
             ProductId = int.Parse(this.qProductId)
                
            };

            if (cComAc.InsertActivity(cComAc) == 1)
                Response.Redirect(Request.Url.ToString());

            

        }
        protected void dropCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropCat.SelectedValue == "3")
            {
                revenue_com_step.Visible = true;
                com_val.Visible = false;
            }
            else
            {
                revenue_com_step.Visible = false;
                com_val.Visible = true;
            }
        }

        public void SaleDataBind()
        {
            int intProductId = int.Parse(this.qProductId);
            ProductBookingEngine cProductBookingEngine = new ProductBookingEngine();
            cProductBookingEngine = cProductBookingEngine.GetProductbookingEngine(intProductId);

            Sales_Com_type cComType = new Sales_Com_type();
            dropComType.DataSource = cComType.getDicComType();
            dropComType.DataTextField = "Value";
            dropComType.DataValueField = "Key";
            dropComType.DataBind();


           
            Product_sales cSales = new Product_sales();
            cSales = cSales.getSales(cProductBookingEngine.SalesID);




            if (cSales != null && cSales.SaleId != 2)
            {

                //lblSaleName.Text = cSales.SaleName;
                txtSalesName.Text = cSales.SaleName;
                txtComvalues.Text = cSales.Commission.ToString();
                txtPhone.Text = cSales.Phone;
                txtFax.Text = cSales.Fax;
                txtmail.Text = cSales.Email;
                txtComment.Text = cSales.Comment;

            }
            else
            {
                Sales_ref.Visible = false;
                panelBHT.Visible = true;
            }
        }

        public void MailSettingDataBind()
        {
            NewslettersSetting mySetting = new NewslettersSetting();
            mySetting = mySetting.GetSettingbyId(int.Parse(this.qProductId));
            if (mySetting != null)
            {
                host.Text = mySetting.Host;
                mailUser.Text = mySetting.Mailuser;
                mailPass.Text = mySetting.Mailpass;
                MailDisplay.Text = mySetting.Maildisplay;
                namedisplay.Text = mySetting.Displayfrom;
                timedelay.Text = Convert.ToString(mySetting.Timedelay);
                Datemodify.Text = Convert.ToString(mySetting.DateModify);
                txtPort.Text = mySetting.Port.ToString();
                radioSSL.SelectedValue = mySetting.SSL.ToString();
                RadioIsActive.SelectedValue = mySetting.IsActive.ToString();
            }
        }

        public void PaymentDataBind()
        {
            // Show title Supplier
            int intProduct = int.Parse(this.qProductId);

            ProductBookingEngine cProductBookingEngine = new ProductBookingEngine();
           cProductBookingEngine  = cProductBookingEngine.GetProductbookingEngine(intProduct);
            txtdue_date.Text = cProductBookingEngine.DuePayment.ToString();
            datePicker_Com_date_start.DateStart = cProductBookingEngine.ComStart;
            datePicker_Com_date_start.DataBind();
            txtnummonth.Text = cProductBookingEngine.ComNum.ToString();

            //Response.Write(cProductBookingEngine.ComType.ToString());
            //Response.End();

            dropComMonthType.SelectedValue = cProductBookingEngine.ComType.ToString();

            Product cProduct = new Product();
            cProduct = cProduct.GetProductById(intProduct);

            Supplier cSupplier = new Supplier();
            cSupplier = cSupplier.getSupplierById(cProduct.SupplierPrice);
            lblhead.Text = cSupplier.SupplierTitle;
            

            SupplierAccount cSupAcc = new SupplierAccount();
            gridSupplierAccount.DataSource = cSupAcc.getSupplierAccountAllBySupplierID(cProduct.SupplierPrice);
            gridSupplierAccount.DataBind();

            if (string.IsNullOrEmpty(this.strAccQueryString))
            {
                FormSupAccAdd.Visible = false;
            }
            else
            {
                FormSupAccAdd.Visible = true;
            }

            if (cSupplier != null && string.IsNullOrEmpty(this.strAccQueryString))
            {
                FormSupAccAdd.DefaultMode = FormViewMode.Insert;
            }
            else
            {
                FormSupAccAdd.DefaultMode = FormViewMode.Edit;
            }  
        }

        protected void FormSupAccAdd_OnCommand(object sender, FormViewCommandEventArgs e)
        {
            if (e.CommandName == "UpdateCancel")
            {
                FormSupAccAdd.Visible = false;

                //Response.Redirect(Request.Url.ToString().Split('?')[0] + "?" + this.QueryStringFilter(Request.Url.ToString(), "acid").Hotels2LeftClr(1));
               Response.Redirect(Request.Url.ToString());
            }
        }

        protected void gridSupplierAccount_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "updateflagDefault")
            {
                int intProduct = int.Parse(this.qProductId);
                Product cProduct = new Product();
                cProduct = cProduct.GetProductById(intProduct);

                SupplierAccount clSupplierAcc = new SupplierAccount();
                clSupplierAcc = clSupplierAcc.getSupplierAccountById(short.Parse(e.CommandArgument.ToString()));

                if (clSupplierAcc.getSupplierAccountAllBySupplierIDAndFlagDefaultTrue(cProduct.SupplierPrice).Count > 0)
                {
                    foreach (SupplierAccount item in clSupplierAcc.getSupplierAccountAllBySupplierIDAndFlagDefaultTrue(cProduct.SupplierPrice))
                    {
                        if (item.AccountId != short.Parse(e.CommandArgument.ToString()))
                        {
                            SupplierAccount clSupplierAccs = new SupplierAccount();
                            clSupplierAccs = clSupplierAccs.getSupplierAccountById(item.AccountId);
                            clSupplierAccs.FlagDefault = false;
                            clSupplierAccs.Update();
                        }

                    }
                }
                if (clSupplierAcc.FlagDefault)
                {
                    clSupplierAcc.FlagDefault = false;
                }
                else
                {
                    clSupplierAcc.FlagDefault = true;
                }
                clSupplierAcc.Update();
            }

            Response.Redirect(Request.Url.ToString());
        }

        protected void gridSupplierAccount_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                bool bolStatus = (bool)DataBinder.Eval(e.Row.DataItem, "FlagDefault");
                ImageButton linkStaffStat = (ImageButton)e.Row.Cells[2].FindControl("imgButton");
                short shrAccount = (short)gridSupplierAccount.DataKeys[e.Row.DataItemIndex].Value;
                HyperLink hlLink = (HyperLink)e.Row.Cells[2].FindControl("hlAccountName");
                hlLink.NavigateUrl = Request.Url.ToString() + "&acid=" + shrAccount;
                //HyperLinkField hlField = (HyperLinkField)e.Row.Cells[1].FindControl("imgButton");
                //Text='<%# Eval("AccountNumber")   Eval("AccountTypeTitle")%>'
                //Image imgStaffStat = (Image)e.Row.Cells[4].FindControl("imagestaffstat");


                
            }
        }

        protected void FormSupAccAdd_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            int intProduct = int.Parse(this.qProductId);
            Product cProduct = new Product();
            cProduct = cProduct.GetProductById(intProduct);
            e.Values["SupplierId"] = cProduct.SupplierPrice;

        }

        protected void FormSupAccAdd_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            //Server.Transfer("supplier_account_list.aspx?supid=" + Request.QueryString["supid"]);
            Response.Redirect(Request.Url.ToString());
        }

        protected void FormSupAccAdd_Updated(object sender, FormViewUpdatedEventArgs e)
        {
            Response.Redirect(Request.Url.ToString());
        }

        protected void FormSupAccAdd_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            int intProduct = int.Parse(this.qProductId);
            Product cProduct = new Product();
            cProduct = cProduct.GetProductById(intProduct);
            e.NewValues["SupplierId"] = cProduct.SupplierPrice;
        }

        protected void supplierCreate_Onclick(object sender, EventArgs e)
        {
            if (FormSupAccAdd.Visible == false)
            {
                FormSupAccAdd.Visible = true;
            }
        }

        protected void btnDuedateSave_Click(object sender, EventArgs e)
        {
            ProductBookingEngine cProductBookingEngine = new ProductBookingEngine();
            cProductBookingEngine.UpdatePaymentdetail(int.Parse(this.qProductId), byte.Parse(txtdue_date.Text), byte.Parse(txtnummonth.Text), byte.Parse(dropComMonthType.SelectedValue), datePicker_Com_date_start.GetDatetStart);

            Response.Redirect(Request.Url.ToString());
        }

        public void DropDepDataBind()
        {
            SupplierContact cSupplierContact = new SupplierContact();
            DropDep.DataSource = cSupplierContact.getdicDepartment();
            DropDep.DataTextField = "Value";
            DropDep.DataValueField = "Key";
            DropDep.DataBind();
        }

        public void dropPhontCatDataBind()
        {
            dropPhontCat.DataSource = SupplierContact.getPhoneCat();
            dropPhontCat.DataTextField = "Value";
            dropPhontCat.DataValueField = "Key";
            dropPhontCat.DataBind();
        }




        //================= LEFT PAN ==============================
        //============== INSERT BOX =======================


        public void btnSave_Onclick(object sender, EventArgs e)
        {
            short shrSupplierId = short.Parse(this.qSupplierId);
            byte bytDepId = byte.Parse(dropcatInsert.SelectedValue);
            int InsertContact = SupplierContact.InsertStaffContact(shrSupplierId, bytDepId, txtTitleName.Text, string.Empty, true);

            //Response.Redirect(Request.Url.ToString());

            GvDepartmentDataBind();


        }

        public void dropcatInsertDataBind()
        {
            SupplierContact cSupplierContact = new SupplierContact();
            dropcatInsert.DataSource = cSupplierContact.getdicDepartment();
            dropcatInsert.DataTextField = "Value";
            dropcatInsert.DataValueField = "Key";
            dropcatInsert.DataBind();
        }

        //============== CONTACT LIST BOX =======================
        public void GvDepartmentDataBind()
        {
            
            SupplierContact cSupplierContact = new SupplierContact();
            GvDepartment.DataSource = cSupplierContact.getdicDepartmentHaveRecord(short.Parse(this.qSupplierId));
            GvDepartment.DataBind();
        }

        public void GvDepartment_OnRowdataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                byte bytDepId = (byte)GvDepartment.DataKeys[e.Row.RowIndex].Value;
                GridView GvChild = e.Row.Cells[0].FindControl("GVContactList") as GridView;
                SupplierContact cSupplierContact = new SupplierContact();
                GvChild.DataSource = cSupplierContact.getSupplierStaffContactListByDepId(short.Parse(this.qSupplierId), bytDepId);
                GvChild.DataBind();
            }
        }

        public void GVContactList_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                bool bolStatus = (bool)DataBinder.Eval(e.Row.DataItem, "status");

                HyperLink Hypertaff = e.Row.Cells[0].FindControl("lstaff") as HyperLink;
                if (!bolStatus)
                {
                    Hypertaff.CssClass = "contactStaffDis";
                }
            }
        }
        //========================================================

        // RIGHT PAN
        //====================== PANEL CONTACT DETAIL ==============

        public void panelContactDetailDataBind()
        {
            if (!string.IsNullOrEmpty(this.qcontactId))
            {
                txteditLocal.Text = "2";

                int intstaffId = int.Parse(this.qcontactId);
                SupplierContact cSupplierContact = new SupplierContact();


                lblHeadtitle.Text = cSupplierContact.getSupplierStaffContactbyId(intstaffId).title;
                DropDep.SelectedValue = cSupplierContact.getSupplierStaffContactbyId(intstaffId).department_id.ToString();
                txtTitle.Text = cSupplierContact.getSupplierStaffContactbyId(intstaffId).title;
                txtCommentContact.Text = cSupplierContact.getSupplierStaffContactbyId(intstaffId).comment;

                if (cSupplierContact.getSupplierStaffContactbyId(intstaffId).status)
                {
                    radiocontactenable.Checked = true;
                    radiocontactDisable.Checked = false;
                }
                else
                {
                    radiocontactDisable.Checked = true;
                    radiocontactenable.Checked = false;
                }

                GVPhoneListDataBind();
                GvEmailDataBind();
            }
            else
            {
                screenBlock.Visible = true;
                panelContactInsert.Visible = false;
            }
        }
        public void GVPhoneListDataBind()
        {
            int intstaffId = int.Parse(this.qcontactId);
            GVPhoneList.DataSource = SupplierContact.GetListPhoneListByStaffContactId(intstaffId);
            GVPhoneList.DataBind();
        }

        public void GVPhoneList_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPhone = e.Row.Cells[0].FindControl("lablePhone") as Label;

                int intStaffId = (int)DataBinder.Eval(e.Row.DataItem, "staff_id");
                byte bytCatId = (byte)DataBinder.Eval(e.Row.DataItem, "cat_id");
                string strPhone = (string)DataBinder.Eval(e.Row.DataItem, "phone_number");
                string strcountryCode = (string)DataBinder.Eval(e.Row.DataItem, "code_country");
                string strlocalcode = (string)DataBinder.Eval(e.Row.DataItem, "code_local");
                bool bolStatus = (bool)DataBinder.Eval(e.Row.DataItem, "status");

                string CatTitle = SupplierContact.GetCatTitlebyCatId(bytCatId);

                string strPhoneShowformat = CatTitle + "&nbsp;:&nbsp;(" + strcountryCode + ")" + strlocalcode + "-" + strPhone;
                lblPhone.Text = strPhoneShowformat;

                DropDownList dropPhontCatEdit = e.Row.Cells[0].FindControl("dropPhontCatEdit") as DropDownList;
                dropPhontCatEdit.DataSource = SupplierContact.getPhoneCat();
                dropPhontCatEdit.DataTextField = "Value";
                dropPhontCatEdit.DataValueField = "Key";
                dropPhontCatEdit.DataBind();
                dropPhontCatEdit.SelectedValue = bytCatId.ToString();

                TextBox txtCountryCode = e.Row.Cells[0].FindControl("txteditCountryCodeEdit") as TextBox;
                txtCountryCode.Text = strcountryCode;
                TextBox txtLocalCode = e.Row.Cells[0].FindControl("txteditLocalEdit") as TextBox;
                txtLocalCode.Text = strlocalcode;
                TextBox txtPhone = e.Row.Cells[0].FindControl("txtPhoneEdit") as TextBox;
                txtPhone.Text = strPhone;

                Button btnDisable = e.Row.Cells[0].FindControl("btnDisable") as Button;

                if (!bolStatus)
                {
                    btnDisable.Text = "Enable";
                    lblPhone.CssClass = "headPhoneCatDis";
                }
                else
                {
                    lblPhone.CssClass = "headPhoneCat";
                }

            }
        }

        public void btnSavephone_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.qcontactId))
            {
                int intstaffId = int.Parse(this.qcontactId);
                byte bytPhoneCat = byte.Parse(dropPhontCat.SelectedValue);
                string strCountryCode = txteditCountryCode.Text;
                string strLocalCode = txteditLocal.Text;
                string strPhone = txtPhone.Text;

                int insertPhone = SupplierContact.InsertStaffPhone(bytPhoneCat, intstaffId, strCountryCode, strLocalCode, strPhone, true);
            }

            GVPhoneListDataBind();
        }

        public void GvEmailDataBind()
        {
            int intstaffId = int.Parse(this.qcontactId);
            GvEmail.DataSource = SupplierContact.GetListEmailByStaffContactId(intstaffId);
            GvEmail.DataBind();
        }

        public void GvEmail_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblEmail = e.Row.Cells[0].FindControl("LabelEmail") as Label;
                string strEmail = (string)DataBinder.Eval(e.Row.DataItem, "email");
                bool bolStatus = (bool)DataBinder.Eval(e.Row.DataItem, "status");
                lblEmail.Text = strEmail;

                Button btnEmailsaveeditDis = e.Row.Cells[0].FindControl("btnEmailsaveeditDis") as Button;
                if (!bolStatus)
                {
                    btnEmailsaveeditDis.Text = "Enable";
                    lblEmail.CssClass = "headPhoneCatDis";
                }
                else
                {
                    lblEmail.CssClass = "headPhoneCat";
                }
            }
        }

        public void btnEmailsave_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.qcontactId))
            {
                int intstaffId = int.Parse(this.qcontactId);
                string strEmail = txteditemail.Text;

                int intInsertEmail = SupplierContact.InsertStaffEmail(intstaffId, strEmail, true);
            }

            GvEmailDataBind();
        }

        public void dropPhontCat_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropPhontCat.SelectedValue == "1")
            {
                txteditLocal.Text = "2";
            }
            else if (dropPhontCat.SelectedValue == "2")
            {
                txteditLocal.Text = "81";
            }
            else if (dropPhontCat.SelectedValue == "3")
            {
                txteditLocal.Text = "2";
            }
        }

        public void btnContactsave_Onclick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.qcontactId))
            {
                int intstaffId = int.Parse(this.qcontactId);
                byte DepId = byte.Parse(DropDep.SelectedValue);

                bool bolStatus = true;
                if (radiocontactDisable.Checked)
                {
                    bolStatus = false;
                }

                SupplierContact.UpdateStaffContact(intstaffId, DepId, txtTitle.Text, txtCommentContact.Text, bolStatus);
            }

            Response.Redirect(Request.Url.ToString());
        }

        public void btnSavephoneEdit_OnClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[] argument = btn.CommandArgument.Split(',');
            int PhoneId = int.Parse(argument[0]);
            int RowIndex = int.Parse(argument[1]);

            if (btn.CommandName == "phoneEdit")
            {
                foreach (GridViewRow item in GVPhoneList.Rows)
                {
                    if (RowIndex == item.RowIndex)
                    {
                        DropDownList dropPhontCatEdit = GVPhoneList.Rows[item.RowIndex].Cells[0].FindControl("dropPhontCatEdit") as DropDownList;
                        TextBox txtCountryCode = GVPhoneList.Rows[item.RowIndex].Cells[0].FindControl("txteditCountryCodeEdit") as TextBox;
                        TextBox txtLocalCode = GVPhoneList.Rows[item.RowIndex].Cells[0].FindControl("txteditLocalEdit") as TextBox;
                        TextBox txtPhone = GVPhoneList.Rows[item.RowIndex].Cells[0].FindControl("txtPhoneEdit") as TextBox;

                        bool update = SupplierContact.UpdateStaffPhone(PhoneId, byte.Parse(dropPhontCatEdit.SelectedValue), txtCountryCode.Text, txtLocalCode.Text, txtPhone.Text);
                    }
                }
            }

            if (btn.CommandName == "phoneDis")
            {
                foreach (GridViewRow item in GVPhoneList.Rows)
                {
                    if (RowIndex == item.RowIndex)
                    {
                        SupplierContact cSupplierContact = new SupplierContact();
                        if (cSupplierContact.GetStaffPhonebyId(PhoneId).status)
                        {
                            SupplierContact.UpdateStaffPhoneStatus(PhoneId, false);

                        }
                        else
                        {
                            SupplierContact.UpdateStaffPhoneStatus(PhoneId, true);
                        }
                    }
                }
            }

            if (btn.CommandName == "PhoneDel")
            {
                foreach (GridViewRow item in GVPhoneList.Rows)
                {
                    if (RowIndex == item.RowIndex)
                    {
                        SupplierContact.DelStaffContactPhone(PhoneId);
                    }
                }
            }

            GVPhoneListDataBind();

        }

        protected void settingUpdate_Click(object sender, EventArgs e)
        {
            NewslettersSetting mySetting = new NewslettersSetting();
            int intProductID = int.Parse(this.qProductId);
            mySetting = mySetting.GetSettingbyId(intProductID);
            if (mySetting == null)
            {
                NewslettersSetting InsertNews = new NewslettersSetting();
                InsertNews.ProductID = intProductID;
                InsertNews.Host = host.Text;
                InsertNews.Mailuser = mailUser.Text;
                InsertNews.Mailpass = mailPass.Text;
                InsertNews.Maildisplay = MailDisplay.Text;
                InsertNews.Displayfrom = namedisplay.Text;
                InsertNews.Timedelay = Convert.ToInt32(timedelay.Text);
                InsertNews.Port = int.Parse(txtPort.Text);
                InsertNews.SSL = bool.Parse(radioSSL.SelectedValue);
                InsertNews.IsActive = bool.Parse(RadioIsActive.SelectedValue);
                InsertNews.InsertNewsSetting(InsertNews);
                
                
            }
            else
            {
                mySetting.ProductID = intProductID;
                mySetting.Host = host.Text;
                mySetting.Mailuser = mailUser.Text;
                mySetting.Mailpass = mailPass.Text;
                mySetting.Maildisplay = MailDisplay.Text;
                mySetting.Displayfrom = namedisplay.Text;
                mySetting.Timedelay = Convert.ToInt32(timedelay.Text);
                mySetting.Port = int.Parse(txtPort.Text);
                mySetting.SSL = bool.Parse(radioSSL.SelectedValue);
                mySetting.IsActive = bool.Parse(RadioIsActive.SelectedValue);
                mySetting.UpdateMailsetting(mySetting);


            }


            Response.Redirect(Request.Url.ToString());
        }

        public void btnEmailsaveedit_OnClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[] argument = btn.CommandArgument.Split(',');
            int EmailId = int.Parse(argument[0]);
            int RowIndex = int.Parse(argument[1]);

            if (btn.CommandName == "EmailEdit")
            {
                foreach (GridViewRow item in GvEmail.Rows)
                {
                    if (RowIndex == item.RowIndex)
                    {
                        TextBox txteditemailedit = GvEmail.Rows[item.RowIndex].Cells[0].FindControl("txteditemailedit") as TextBox;
                        SupplierContact.UpdateStaffEmail(EmailId, txteditemailedit.Text);
                    }
                }
            }

            if (btn.CommandName == "EmailDis")
            {
                foreach (GridViewRow item in GvEmail.Rows)
                {
                    if (RowIndex == item.RowIndex)
                    {
                        SupplierContact cSupplierContact = new SupplierContact();
                        if (cSupplierContact.GetStaffEmailById(EmailId).status)
                        {
                            SupplierContact.UpdateStaffEmailStatus(EmailId, false);
                        }
                        else
                        {
                            SupplierContact.UpdateStaffEmailStatus(EmailId, true);
                        }
                    }
                }
            }

            if (btn.CommandName == "Emaildel")
            {
                foreach (GridViewRow item in GvEmail.Rows)
                {
                    if (RowIndex == item.RowIndex)
                    {
                        SupplierContact.DelStaffEmail(EmailId);
                    }
                }
            }

            GvEmailDataBind();
        }
}
}